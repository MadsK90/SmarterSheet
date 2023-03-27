namespace SmarterSheet.Extensions.Templating;

internal sealed class TemplateResolver
{
    private readonly IDictionary<Type, IValueParser> _valueParsers;
    private readonly IDictionary<Type, Dictionary<string, Property>> _modelProperties;

    public TemplateResolver()
    {
        var assemblyTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).ToArray();

        _valueParsers = InitstantiateValueParsers(assemblyTypes);
        _modelProperties = CreateModelStructures(assemblyTypes);
    }

    public Row? CreateRow<T>(T model, ReadOnlySpan<Column> columns) where T: SheetModelBase, new()
    {
        var structure = GetModelStructure<T>(columns);
        if (structure == null)
            return null;

        var cells = CreateCells(structure, model);
        if (!cells.Any())
            return null;

        return new Row
        {
            Id = model.RowId,
            Cells = cells.ToArray()
        };
    }

    public IEnumerable<Row> CreateRows<T>(IEnumerable<T> models, ReadOnlySpan<Column> columns) where T : SheetModelBase, new()
    {
        var structure = GetModelStructure<T>(columns);
        if (structure == null)
            return Array.Empty<Row>();

        var rows = new List<Row>();

        foreach(var model in models)
        {
            var cells = CreateCells(structure, model);
            if (!cells.Any())
                continue;

            rows.Add(new Row
            {
                Id = model.RowId,
                Cells = cells.ToArray()
            });
        }

        return rows;
    }

    public T? ResolveModel<T>(Row row) where T : SheetModelBase, new()
    {
        var structure = GetModelStructure<T>(row.Columns);
        if(structure == null)
            return null;

        var model = new T
        {
            RowId = row.Id,
            RowNumber = row.RowNumber,
            Discussions = row.Discussions,
            Attachments = row.Attachments
        };

        if (!SetProperties(ref model, row, structure))
            return null;

        return model;
    }

    public IEnumerable<T> ResolveModels<T>(Sheet sheet) where T : SheetModelBase, new()
    {
        if(sheet.Rows == null)
            return Array.Empty<T>();

        var structure = GetModelStructure<T>(sheet.Columns);
        if (structure == null)
            return Array.Empty<T>();

        var models = new List<T>(sheet.TotalRowCount);

        foreach(var row in sheet.Rows)
        {
            var model = new T
            {
                RowId = row.Id,
                RowNumber = row.RowNumber,
                Discussions = row.Discussions,
                Attachments = row.Attachments
            };

            if(!SetProperties(ref model, row, structure))
                return Array.Empty<T>();

            models.Add(model);
        }

        if (!models.Any())
            return Array.Empty<T>();

        return models;
    }

    private Dictionary<Property, ulong>? GetModelStructure<T>(ReadOnlySpan<Column> sheetColumns)
    {
        if(!_modelProperties.TryGetValue(typeof(T), out var model))
        {
            Log.Error($"Failed to find model of type '{typeof(T)}'");
            return null;
        }

        var columns = new Dictionary<Property, ulong>();

        foreach(var prop in model)
        {
            var column = sheetColumns.FirstOrDefault(x => x.Title.ToLowerInvariant() == prop.Key);
            if(column == null || column == default)
            {
                Log.Error($"Couldn't find any column with the name of '{prop.Key}'");
                return null;
            }

            columns.Add(prop.Value, column.Id);
        }

        if(!columns.Any())
        {
            Log.Error($"Couldn't get model structure");
            return null;
        }

        return columns;
    }

    private bool SetProperties<T>(ref T model, Row row, Dictionary<Property, ulong> structure) where T : SheetModelBase, new()
    {
        if(row.Cells == null || !row.Cells.Any())
        {
            Log.Error("Row doesn't have any cells");
            return false;
        }

        foreach (var pair in structure)
        {
            var cell = row.Cells.FirstOrDefault(x => x.ColumnId == pair.Value);
            if (cell == null)
                continue;

            if (cell.Value == null)
                continue;

            if (!_valueParsers.ContainsKey(pair.Key.PropertyInfo.PropertyType))
            {
                Log.Error($"Couldn't find any parser of type '{pair.Key.PropertyInfo.PropertyType}'.");
                return false;
            }

            if (!_valueParsers[pair.Key.PropertyInfo.PropertyType].Parse(cell.Value, pair.Key, ref model))
            {
                Log.Error($"Failed to parse value '{cell.Value}' in model '{typeof(T)}'");
                return false;
            }
        }

        return true;
    }

    private IEnumerable<Cell> CreateCells<T>(Dictionary<Property, ulong> structure, T model) where T: SheetModelBase, new()
    {
        var cells = new List<Cell>(structure.Count);

        foreach (var pair in structure)
        {
            if (!_valueParsers.ContainsKey(pair.Key.PropertyInfo.PropertyType))
            {
                Log.Error($"Couldn't find any parser with the type '{pair.Key.PropertyInfo.PropertyType}'");
                return Array.Empty<Cell>();
            }

            if (!_valueParsers[pair.Key.PropertyInfo.PropertyType].Parse(in model, pair.Key, pair.Value, out var cell))
            {
                Log.Error($"Failed to parse value '{model}' with type '{pair.Key}'");
                return Array.Empty<Cell>();
            }

            if (cell == null || (cell.DisplayValue == null && cell.Value == null))
                continue;

            cells.Add(cell);
        }

        return cells;
    }

    private static Dictionary<Type, IValueParser> InitstantiateValueParsers(ReadOnlySpan<Type> assemblyTypes)
    {
        var types = assemblyTypes.Where(typeof(IValueParser).IsAssignableFrom).ToArray();
        if (!types.Any())
            throw new Exception("No ValueParsers could be found in assembly.");

        var valueParsers = new Dictionary<Type, IValueParser>();

        foreach(var type in types)
        {
            var parserAttribute = type.GetCustomAttribute<ValueParserAttribute>();
            if(parserAttribute == null || parserAttribute == default)
                continue;

            var valueParser = (IValueParser?)Activator.CreateInstance(type);
            if (valueParser == null)
                continue;

            valueParsers.Add(parserAttribute.ValueType, valueParser);
        }

        return valueParsers;
    }

    private static Dictionary<Type, Dictionary<string, Property>> CreateModelStructures(ReadOnlySpan<Type> assemblyTypes)
    {
        var modelProperties = new Dictionary<Type, Dictionary<string, Property>>();

        var types = assemblyTypes.Where(typeof(SheetModelBase).IsAssignableFrom).ToArray();
        if (!types.Any())
            return modelProperties;
        
        foreach (var type in types)
        {
            var properties = type.GetProperties()
                .Where(x => x.IsDefined(typeof(ColumnNameAttribute), false)).ToArray();

            modelProperties.Add(type, CreatePropertyLookup(properties, type));
        }

        return modelProperties;

        #region Local Functions

        static Dictionary<string, Property> CreatePropertyLookup(ReadOnlySpan<PropertyInfo> properties, Type type)
        {
            var lookup = new Dictionary<string, Property>();

            foreach (var property in properties)
            {
                var columnNameAttribute = property.GetCustomAttribute<ColumnNameAttribute>();
                if (columnNameAttribute == null || columnNameAttribute == default)
                    continue;

                if (columnNameAttribute.Name == "")
                    continue;

                var columnName = columnNameAttribute.Name.ToLowerInvariant();

                if (lookup.ContainsKey(columnName))
                {
                    Log.Error($"SheetRowModel of type:'{type.Name}' allready has a property with column name:'{columnNameAttribute.Name}'");
                    continue;
                }

                lookup.Add(columnName, new Property
                {
                    PropertyInfo = property,
                    IsFormula = columnNameAttribute.Formula
                });
            }        

            return lookup;
        }

        #endregion
    }
}
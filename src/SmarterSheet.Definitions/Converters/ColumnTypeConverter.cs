namespace SmarterSheet.Definitions.Converters;

public sealed class ColumnTypeConverter : JsonConverter<ColumnType>
{
    public override ColumnType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.String)
            return ColumnType.NULL;

        var str = reader.GetString();
        if(str == null)
            return ColumnType.NULL;

        if(!ColumnTypeExtensions.TryParse(str, out var columnType, false, true))
            return ColumnType.NULL;

        return columnType;
    }

    public override void Write(Utf8JsonWriter writer, ColumnType value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToStringFast());
    }
}
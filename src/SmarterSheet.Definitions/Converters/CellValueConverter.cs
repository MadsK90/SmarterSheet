namespace SmarterSheet.Definitions.Converters;

public class CellValueConverter : JsonConverter<object>
{
    public override bool CanConvert(Type typeToConvert)
    {
        return typeof(string) == typeToConvert;
    }

    public override object Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            var str = reader.GetString();
            if (str == null)
                return "";

            return str;
        }

        if (reader.TokenType == JsonTokenType.Number)
        {
            if (reader.TryGetInt64(out var longValue))
                return longValue.ToString();

            if (reader.TryGetDouble(out var doubleValue))
                return doubleValue.ToString();

            throw new Exception("");
        }

        if (reader.TokenType == JsonTokenType.True)
        {
            return "true";
        }
        else if (reader.TokenType == JsonTokenType.False)
        {
            return "false";
        }

        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, object value, JsonSerializerOptions options)
    {
        if (int.TryParse(value.ToString(), out var i))
        {
            writer.WriteNumberValue(i);
            return;
        }

        if (bool.TryParse(value.ToString(), out var b))
        {
            writer.WriteBooleanValue(b);
            return;
        }

        writer.WriteStringValue(value.ToString());
    }
}
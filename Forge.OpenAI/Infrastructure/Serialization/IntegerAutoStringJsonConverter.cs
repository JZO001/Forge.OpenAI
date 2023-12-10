using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Forge.OpenAI.Infrastructure.Serialization
{

    public class IntegerAutoStringJsonConverter : JsonConverter<int?>
    {

        public const string AUTO = "auto";

        public override int? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.String:
                    {
                        string stringValue = reader.GetString();
                        if (!string.IsNullOrEmpty(stringValue) && stringValue.Equals(AUTO, StringComparison.OrdinalIgnoreCase)) return -1;
                        break;
                    }

                case JsonTokenType.Number:
                    return reader.GetInt32();

                case JsonTokenType.Null:
                    return null;
            }

            throw new JsonException($"Unexpected token type: {reader.TokenType}");
        }

        public override void Write(Utf8JsonWriter writer, int? value, JsonSerializerOptions options)
        {
            if (value.HasValue)
            {
                if (value.Value < 0)
                    writer.WriteStringValue(AUTO);
                else
                    writer.WriteNumberValue(value.Value);
            }
            else
            {
                writer.WriteNullValue();
            }
        }

    }

}

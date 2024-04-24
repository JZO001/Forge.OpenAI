using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Forge.OpenAI.Models.Shared;

namespace Forge.OpenAI.Infrastructure.Serialization
{

    public class ToolChoiceConverter : JsonConverter<object>
    {

        public override object Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                return reader.GetString();
            }

            return JsonSerializer.Deserialize<ToolChoice>(ref reader, options);
        }

        public override void Write(Utf8JsonWriter writer, object value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, options);
        }

    }

}

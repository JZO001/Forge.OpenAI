using Forge.OpenAI.Models.Shared;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Forge.OpenAI.Infrastructure.Serialization
{

    public class ResponseFormatConverter : JsonConverter<object>
    {

        public override object Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                return reader.GetString();
            }

            return JsonSerializer.Deserialize<ResponseFormat>(ref reader, options);
        }

        public override void Write(Utf8JsonWriter writer, object value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, options);
        }

    }

}

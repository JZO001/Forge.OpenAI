using System;
using System.Text.Json.Serialization;

namespace Forge.OpenAI.Authentication
{

    internal class AuthInfoJson
    {

        [JsonConstructor]
        public AuthInfoJson(string apiKey, string organization = null)
        {
            if (!string.IsNullOrEmpty(apiKey) && !apiKey.StartsWith("sk-", System.StringComparison.OrdinalIgnoreCase))
            {
                throw new Exception($"ApiKey parameter '{apiKey}' must start with 'sk-'");
            }

            ApiKey = apiKey;

            if (!string.IsNullOrEmpty(organization) && !organization.StartsWith("org-", System.StringComparison.OrdinalIgnoreCase))
            {
                throw new Exception($"Organization parameter must start with 'org-'");
            }

            Organization = organization;
        }

        [JsonPropertyName("apiKey")]
        public string ApiKey { get; }

        [JsonPropertyName("organization")]
        public string Organization { get; }

    }

}

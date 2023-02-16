using System;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Forge.OpenAI.Authentication
{

    /// <summary>Represents the authentication data from a Json configuration</summary>
    public class AuthenticationInfo
    {

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public const string OPENAI_KEY = "OPENAI_KEY";
        public const string OPENAI_API_KEY = "OPENAI_API_KEY";
        public const string OPENAI_SECRET_KEY = "OPENAI_SECRET_KEY";
        public const string TEST_OPENAI_SECRET_KEY = "TEST_OPENAI_SECRET_KEY";
        public const string ORGANIZATION = "ORGANIZATION";
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

        private static readonly object DEFAULT_LOCK_OBJECT = new object();
        private static AuthenticationInfo DEFAULT;

        /// <summary>Initializes a new instance of the <see cref="AuthenticationInfo" /> class.</summary>
        /// <param name="apiKey">The API key.</param>
        /// <exception cref="System.ArgumentNullException">apiKey</exception>
        /// <exception cref="System.Security.Authentication.InvalidCredentialException"></exception>
        public AuthenticationInfo(string apiKey)
        {
            if (string.IsNullOrWhiteSpace(apiKey)) throw new ArgumentNullException(nameof(apiKey));

            if (!apiKey.Contains("sk-"))
            {
                throw new Exception($"{apiKey} parameter must start with 'sk-'");
            }

            ApiKey = apiKey;
        }

        /// <summary>Initializes a new instance of the <see cref="AuthenticationInfo" /> class.</summary>
        /// <param name="apiKey">The API key.</param>
        /// <param name="organization">The organization.</param>
        /// <exception cref="System.ArgumentNullException">apiKey</exception>
        /// <exception cref="System.Security.Authentication.InvalidCredentialException"></exception>
        [JsonConstructor]
        public AuthenticationInfo(string apiKey, string organization = null)
            : this(apiKey)
        {
            if (!string.IsNullOrWhiteSpace(organization))
            {
                if (!organization.Contains("org-"))
                {
                    throw new Exception($"{nameof(organization)} parameter must start with 'org-'");
                }

                Organization = organization;
            }
        }

        /// <summary>Gets the API key.</summary>
        /// <value>The API key.</value>
        [JsonPropertyName("apiKey")]
        public string ApiKey { get; }

        /// <summary>Gets the organization.</summary>
        /// <value>The organization.</value>
        [JsonPropertyName("organization")]
        public string Organization { get; }

        /// <summary>
        /// Allows implicit casting from a string, so that a simple string API key can be provided in place of an instance of <see cref="AuthenticationInfo"/>.
        /// </summary>
        /// <param name="key">The API key to convert into a <see cref="AuthenticationInfo"/>.</param>
        public static implicit operator AuthenticationInfo(string key) => new AuthenticationInfo(key);

        /// <summary>
        /// The default authentication to use when no other auth is specified.
        /// This can be set manually, or automatically loaded via environment variables or a config file.
        /// </summary>
        public static AuthenticationInfo Default
        {
            get
            {
                if (DEFAULT == null)
                {
                    lock (DEFAULT_LOCK_OBJECT)
                    {
                        if (DEFAULT == null)
                        {
                            DEFAULT = LoadFromEnvironmentVariables() ??
                                LoadFromJsonConfigFile() ??
                                LoadFromJsonConfigFile(directory: Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)) ??
                                LoadFromEnvConfigFile() ??
                                LoadFromEnvConfigFile(directory: Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), filename: ".openai");
                        }
                    }
                }

                return DEFAULT;
            }
            set
            {
                lock (DEFAULT_LOCK_OBJECT) DEFAULT = value;
            }
        }

        /// <summary>
        /// Attempts to load api keys from environment variables, as "OPENAI_KEY" (or "OPENAI_SECRET_KEY", for backwards compatibility)
        /// </summary>
        /// <param name="organization">
        /// For users who belong to multiple organizations, you can pass a header to specify which organization is used for an API request.
        /// Usage from these API requests will count against the specified organization's subscription quota.
        /// </param>
        /// <returns>
        /// Returns the loaded <see cref="AuthenticationInfo"/> any api keys were found,
        /// or <see langword="null"/> if there were no matching environment vars.
        /// </returns>
        public static AuthenticationInfo LoadFromEnvironmentVariables(string organization = null)
        {
            var apiKey = Environment.GetEnvironmentVariable(OPENAI_KEY);

            if (string.IsNullOrWhiteSpace(apiKey))
            {
                apiKey = Environment.GetEnvironmentVariable(OPENAI_API_KEY);
            }

            if (string.IsNullOrWhiteSpace(apiKey))
            {
                apiKey = Environment.GetEnvironmentVariable(OPENAI_SECRET_KEY);
            }

            if (string.IsNullOrWhiteSpace(apiKey))
            {
                apiKey = Environment.GetEnvironmentVariable(TEST_OPENAI_SECRET_KEY);
            }

            return string.IsNullOrEmpty(apiKey) ? null : new AuthenticationInfo(apiKey, organization);
        }

        /// <summary>
        /// Attempts to load api keys from a json configuration file, by default ".openai" in the current directory,
        /// optionally traversing up the directory tree.
        /// </summary>
        /// <param name="directory">
        /// The directory to look in, or <see langword="null"/> for the current directory.
        /// </param>
        /// <param name="filename">
        /// The filename of the config file.
        /// </param>
        /// <param name="searchUp">
        /// Whether to recursively traverse up the directory tree if the <paramref name="filename"/> is not found in the <paramref name="directory"/>.
        /// </param>
        /// <returns>
        /// Returns the loaded <see cref="AuthenticationInfo"/> any api keys were found,
        /// or <see langword="null"/> if it was not successful in finding a config
        /// (or if the config file didn't contain correctly formatted API keys)
        /// </returns>
        public static AuthenticationInfo LoadFromJsonConfigFile(string directory = null, string filename = ".openai", bool searchUp = true)
        {
            Func<string, (string, string)> loader = (filePath) =>
            {
                (string, string) result = (string.Empty, string.Empty);
                try
                {
                    AuthInfoJson authInfo = JsonSerializer.Deserialize<AuthInfoJson>(File.ReadAllText(filePath));
                    result = (authInfo.ApiKey, authInfo.Organization);
                }
                catch (Exception ex)
                {
                    Trace.TraceError(ex.Message);
                }
                return result;
            };

            return LoadFromExternalSource(loader, directory, filename, searchUp);
        }

        /// Attempts to load api keys from an environment configuration file, by default ".env" in the current directory,
        /// optionally traversing up the directory tree.
        /// <param name="directory">The directory.</param>
        /// <param name="filename">The filename.</param>
        /// <param name="searchUp">if set to <c>true</c> [search up].</param>
        /// <returns>
        ///   AuthenticationInfo or null
        /// </returns>
        public static AuthenticationInfo LoadFromEnvConfigFile(string directory = null, string filename = ".env", bool searchUp = true)
        {
            Func<string, (string, string)> loader = (filePath) =>
            {
                (string, string) result = (string.Empty, string.Empty);
                try
                {
                    var lines = File.ReadAllLines(filePath);
                    string apiKey = null;
                    string organization = null;

                    foreach (var line in lines)
                    {
                        var parts = line.Split('=', ':');

                        for (var i = 0; i < parts.Length; i++)
                        {
                            var part = parts[i];
                            var nextPart = parts[i + 1];

                            switch (part)
                            {
                                case OPENAI_KEY:
                                case OPENAI_API_KEY:
                                case OPENAI_SECRET_KEY:
                                case TEST_OPENAI_SECRET_KEY:
                                    apiKey = nextPart.Trim();
                                    break;
                                case ORGANIZATION:
                                    organization = nextPart.Trim();
                                    break;
                            }
                        }
                    }

                    result = (apiKey, organization);
                }
                catch (Exception ex)
                {
                    Trace.TraceError(ex.Message);
                }
                return result;
            };

            return LoadFromExternalSource(loader, directory, filename, searchUp);
        }

        private static AuthenticationInfo LoadFromExternalSource(Func<string, (string, string)> loader, string directory, string filename, bool searchUp)
        {
            if (string.IsNullOrWhiteSpace(directory)) directory = Environment.CurrentDirectory;

            AuthenticationInfo
#if NETCOREAPP3_1_OR_GREATER
                ?
#endif
                authInfo = null;

            var currentDirectory = new DirectoryInfo(directory);

            string apiKey = string.Empty;
            string organization = string.Empty;

            while (authInfo == null && currentDirectory.Parent != null)
            {
                var filePath = Path.Combine(currentDirectory.FullName, filename);

                if (File.Exists(filePath))
                {
                    (string, string) loaderResult = loader(filePath);
                    if (string.IsNullOrWhiteSpace(apiKey)) apiKey = loaderResult.Item1;
                    if (string.IsNullOrWhiteSpace(organization)) organization = loaderResult.Item2;

                    if (!string.IsNullOrWhiteSpace(apiKey) && !string.IsNullOrWhiteSpace(organization)) break;
                }

                if (searchUp)
                {
                    currentDirectory = currentDirectory.Parent;
                }
                else
                {
                    break;
                }
            }

            if (!string.IsNullOrWhiteSpace(apiKey))
            {
                authInfo = new AuthenticationInfo(apiKey, organization);
            }

#if NETCOREAPP3_1_OR_GREATER
            return authInfo!;
#else
            return authInfo;
#endif
        }

    }

}

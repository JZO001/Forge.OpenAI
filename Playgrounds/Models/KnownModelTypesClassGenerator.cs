using Forge.OpenAI.Models.Models;
using System.Text;

namespace Models
{

    internal class KnownModelTypesClassGenerator
    {

        public static string GenerateModelsLookup(ModelsResponse response)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("namespace Forge.OpenAI.Services");
            sb.AppendLine("{");
            sb.AppendLine("    public class KnownModelTypes");
            sb.AppendLine("    {");
            sb.AppendLine("#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member");

            foreach (var result in response.Models.OrderBy(i => i.Id))
            {
                var fieldName = result.Id.Replace("-", "");

                sb.AppendLine(@$"        public const string {GenerateName(result.Id)} = ""{result.Id}"";");
            }

            sb.AppendLine("#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member");
            sb.AppendLine("    }");
            sb.AppendLine("}");

            return sb.ToString();
        }

        private static string GenerateName(string name)
        {
            int index = name.IndexOf("-");

            while (index != -1)
            {
                string nextChar = name.Substring(index + 1, 1);

                name = name.Remove(index, 2);
                name = name.Insert(index, nextChar.ToUpper());
                index = name.IndexOf("-");
            }

            string fistChar = name.Substring(0, 1);
            name = fistChar.ToUpperInvariant() + name.Substring(1, name.Length - 1);

            return name.Replace(":", "_").Replace(".", "_");
        }

    }

}

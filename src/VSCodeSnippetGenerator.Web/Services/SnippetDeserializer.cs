using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using VSCodeSnippetGenerator.Web.Models;

namespace VSCodeSnippetGenerator.Web.Services
{
    public class SnippetDeserializer
    {
        public SnippetInput GetSnippetInput(string snippetText)
        {
            snippetText ??= string.Empty;

            snippetText = IsJsonObject(snippetText)
                ? snippetText
                : WrapWithBrackets(snippetText);

            var deserializedSnipped = JsonConvert.DeserializeObject<Dictionary<string, SnippetDetails>>(snippetText)
                .FirstOrDefault();

            var lines = deserializedSnipped.Value?.Body ?? Enumerable.Empty<string>();

            return new SnippetInput
            {
                Name = deserializedSnipped.Key,
                Prefix = deserializedSnipped.Value?.Prefix,
                Body = string.Join(Environment.NewLine, lines),
                ConvertToTabs = IsIndentedWithTabs(lines),
                Description = deserializedSnipped.Value?.Description,
                HasDescription = !(deserializedSnipped.Value?.Description is null)
            };
        }

        private bool IsJsonObject(string snippet)
            => Regex.IsMatch(snippet, @"^\s*{.*}\s*$", RegexOptions.Singleline);

        private string WrapWithBrackets(string snippet)
            => "{" + snippet + "}";

        private bool IsIndentedWithTabs(IEnumerable<string> lines)
            => lines.Any(l => l?.StartsWith('\t') == true);
    }
}
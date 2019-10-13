using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using VSCodeSnippetGenerator.Web.Extensions;
using VSCodeSnippetGenerator.Web.Models;

namespace VSCodeSnippetGenerator.Web.Services
{
    public class SnippetGenerator
    {
        public string GetSnippet(SnippetInput input)
        {
            var shape = ShapeSnippet(input);
            var json = JsonConvert.SerializeObject(shape, Formatting.Indented);
            return GetJsonBody(json);
        }

        private Dictionary<string, Dictionary<string, object>> ShapeSnippet(SnippetInput input)
        {
            var snippetDetails = new Dictionary<string, object>
            {
                { "prefix", input.Prefix ?? string.Empty },
                {
                    "body",
                    input.Body?.Any() == true
                        ? EnsureRequestedIndentation(input.Body, input.ConvertToTabs, input.TabLength).GetLines()
                        : new List<string> { string.Empty }
                }
            };

            if (input.HasDescription)
            {
                snippetDetails.Add("description", input.Description ?? string.Empty);
            }

            return new Dictionary<string, Dictionary<string, object>>
            {
                { input.Name ?? string.Empty, snippetDetails }
            };
        }

        private string EnsureRequestedIndentation(string body, bool convertToTabs, int? tabLength)
        {
            if (!convertToTabs || tabLength is null)
            {
                return body;
            }

            return body.Replace(new string(' ', (int)tabLength), "\t");
        }

        private string GetJsonBody(string json)
        {
            var lines = json.GetLines();

            lines = lines
                .Take(lines.Count() - 1)
                .Skip(1)
                .Select(l => l.Substring(2));

            return string.Join(Environment.NewLine, lines);
        }
    }
}
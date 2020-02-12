using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using VSCodeSnippetGenerator.Web.Extensions;
using VSCodeSnippetGenerator.Web.Models;

namespace VSCodeSnippetGenerator.Web.Services
{
    public class SnippetGenerator
    {
        public const int Indentation = 4;

        public string GetSnippet(SnippetInput input)
        {
            var shape = ShapeSnippet(input);

            using var stringWriter = new StringWriter();
            using var jsonWriter = new JsonTextWriter(stringWriter)
            {
                Formatting = Formatting.Indented,
                Indentation = Indentation
            };

            var serilizer = new JsonSerializer()
            {
                NullValueHandling = NullValueHandling.Ignore
            };

            serilizer.Serialize(jsonWriter, shape);

            var json = stringWriter.ToString();

            return GetJsonBody(json);
        }

        private Dictionary<string, SnippetDetails> ShapeSnippet(SnippetInput input)
        {
            var snippetDetails = new SnippetDetails
            {
                Prefix = input.Prefix ?? string.Empty,
                Body = input.Body?.Any() == true
                    ? EscapeDollarSigns(
                        EnsureRequestedIndentation(input.Body, input.ConvertToTabs, input.TabLength)).GetLines()
                    : new List<string> { string.Empty },
                Description = input.HasDescription ? input.Description ?? string.Empty : null
            };

            return new Dictionary<string, SnippetDetails>
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

        private string EscapeDollarSigns(string body)
        {
            var variables = new[]
            {
                "TM_FILENAME_BASE", "TM_FILENAME", "TM_SELECTED_TEXT", "TM_CURRENT_LINE",
                "TM_CURRENT_WORD", "TM_LINE_INDEX", "TM_LINE_NUMBER", "TM_DIRECTORY", "TM_FILEPATH",
                "CLIPBOARD", "WORKSPACE_NAME", "CURRENT_YEAR", "CURRENT_YEAR_SHORT", "CURRENT_MONTH",
                "CURRENT_MONTH_NAME", "CURRENT_MONTH_NAME_SHORT", "CURRENT_DATE", "CURRENT_DAY_NAME",
                "CURRENT_DAY_NAME_SHORT", "CURRENT_HOUR", "CURRENT_MINUTE","CURRENT_SECOND",
                "CURRENT_SECONDS_UNIX", "BLOCK_COMMENT_START", "BLOCK_COMMENT_END", "LINE_COMMENT"
            };

            return Regex.Replace(body, $@"(?<!\\)\$(?!{string.Join('|', variables)}|\d|{{)", @"\$");
        }

        private string GetJsonBody(string json)
        {
            var lines = json.GetLines();

            lines = lines
                .Take(lines.Count() - 1)
                .Skip(1)
                .Select(l => l.Substring(Indentation));

            return string.Join(Environment.NewLine, lines);
        }
    }
}
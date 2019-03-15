﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace VSCodeSnippetGenerator.Web.Pages
{
    [BindProperties]
    public class IndexModel : PageModel
    {
        [Display(Prompt = "prop")]
        public string Name { get; set; }

        [Display(Prompt = "prop")]
        public string Prefix { get; set; }

        [Display(Prompt = "An automatically implemented property. C# 3.0 or higher")]
        public string Description { get; set; }

        [Display(Name = "Has Description?")]
        public bool HasDescription { get; set; }

        [Display(Prompt = "public ${1:int} ${2:MyProperty} { get; set; }$0")]
        public string Body { get; set; }

        public string Snippet { get; set; }

        [Display(Name = "Convert to Tabs?")]
        public bool ConvertToTabs { get; set; } = true;

        [Display(Name = "Spaces per Tab")]
        [Range(1, 8)]
        public int? TabLength { get; set; } = 4;

        public void OnGet() => Snippet = GetOnlyBody(SerializeEmptySnippet());

        public void OnPost()
        {
            if (!ModelState.IsValid)
            {
                Snippet = GetOnlyBody(SerializeEmptySnippet());
                return;
            }

            Snippet = GetOnlyBody(SerializeSnippet());
        }

        private string GetOnlyBody(string json)
        {
            var lines = ReadAllLines(json);
            lines = lines
                .Take(lines.Count() - 1)
                .Skip(1)
                .Select(l => l.Substring(2));

            return string.Join(Environment.NewLine, lines);
        }

        private string SerializeSnippet()
            => JsonConvert.SerializeObject(GetSnippet(Name, Prefix, Body, Description, HasDescription, TabLength, ConvertToTabs),
                Formatting.Indented);

        private string SerializeEmptySnippet()
            => JsonConvert.SerializeObject(GetSnippet(null, null, null, null, false, null, false), Formatting.Indented);

        private Dictionary<string, Dictionary<string, object>> GetSnippet(string name, string prefix, string body,
            string description, bool hasDescription,
            int? tabLength, bool convertToTabs)
        {
            var snippetDetails = new Dictionary<string, object>
            {
                { nameof(prefix), prefix ?? string.Empty },
                {
                    nameof(body),
                    body != null && body.Any()
                        ? ReadAllLines(convertToTabs && tabLength != null
                            ? body.Replace(new string(' ', (int)TabLength), "\t")
                            : body)
                        : new List<string> { string.Empty }
                }
            };

            if (hasDescription)
            {
                snippetDetails.Add(nameof(description), description ?? string.Empty);
            }

            return new Dictionary<string, Dictionary<string, object>>
            {
                { name ?? string.Empty, snippetDetails }
            };
        }

        private IEnumerable<string> ReadAllLines(string body)
        {
            using (var reader = new StringReader(body))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    yield return line;
                }
            }
        }
    }
}

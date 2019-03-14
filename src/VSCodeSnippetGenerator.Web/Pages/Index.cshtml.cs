using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public string Name { get; set; }
        public string Prefix { get; set; }
        public string Description { get; set; }
        public string Body { get; set; }
        public string Snippet { get; set; }

        [Display(Name = "Convert to Tabs?")]
        public bool ConvertToTabs { get; set; } = true;

        [Display(Name = "Spaces per Tab")]
        public int? TabLength { get; set; } = 4;

        public void OnGet() => Snippet = SerializeSnippet();

        public void OnPost() => Snippet = SerializeSnippet();

        private string SerializeSnippet()
            => JsonConvert.SerializeObject(GetSnippet(Name, Prefix, Body, Description), Formatting.Indented);

        private Dictionary<string, object> GetSnippet(string name, string prefix, string body, string description)
            => new Dictionary<string, object>
            {
                {
                    name ?? string.Empty, new
                    {
                        Prefix = prefix ?? string.Empty,
                        Body = body != null && body.Any()
                            ? ReadAllLines(
                                ConvertToTabs && TabLength != null
                                    ? body.Replace(new string(' ', (int)TabLength), "\t")
                                    : body)
                            : new List<string> { string.Empty },
                        Description = description ?? string.Empty
                    }
                }
            };

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

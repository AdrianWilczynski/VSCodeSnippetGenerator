using System.Collections.Generic;

namespace VSCodeSnippetGenerator.Web.Models
{
    public class Snippet
    {
        public string Prefix { get; set; }
        public IEnumerable<string> Body { get; set; }
        public string Description { get; set; }
    }
}
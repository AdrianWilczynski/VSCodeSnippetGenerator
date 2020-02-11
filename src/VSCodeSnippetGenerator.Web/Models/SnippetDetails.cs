using System.Collections.Generic;
using Newtonsoft.Json;

namespace VSCodeSnippetGenerator.Web.Models
{
    public class SnippetDetails
    {
        [JsonProperty("prefix")]
        public string Prefix { get; set; }

        [JsonProperty("body")]
        public IEnumerable<string> Body { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
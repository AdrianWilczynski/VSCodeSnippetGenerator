using System.ComponentModel.DataAnnotations;

namespace VSCodeSnippetGenerator.Web.Models
{
    public class SnippetInput
    {
        public static SnippetInput Empty => new SnippetInput();

        [Display(Prompt = "Property")]
        public string Name { get; set; }

        [Display(Prompt = "prop")]
        public string Prefix { get; set; }

        [Display(Prompt = "An automatically implemented property.")]
        public string Description { get; set; }

        [Display(Name = "Has Description?")]
        public bool HasDescription { get; set; }

        [Display(Prompt = "public ${1:int} ${2:MyProperty} { get; set; }$0")]
        public string Body { get; set; }

        [Display(Name = "Convert to Tabs?")]
        public bool ConvertToTabs { get; set; } = true;

        [Display(Name = "Spaces per Tab")]
        [Range(2, 8)]
        public int? TabLength { get; set; } = 4;
    }
}
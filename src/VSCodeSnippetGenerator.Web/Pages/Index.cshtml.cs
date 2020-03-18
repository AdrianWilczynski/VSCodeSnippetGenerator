using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using VSCodeSnippetGenerator.Web.Models;
using VSCodeSnippetGenerator.Web.Services;

namespace VSCodeSnippetGenerator.Web.Pages
{
    [BindProperties]
    public class IndexModel : PageModel
    {
        private readonly SnippetGenerator _snippetGenerator;
        private readonly SnippetDeserializer _snippetDeserializer;

        public IndexModel(SnippetGenerator snippetGenerator, SnippetDeserializer snippetDeserializer)
        {
            _snippetGenerator = snippetGenerator;
            _snippetDeserializer = snippetDeserializer;
        }

        public SnippetInput SnippetInput { get; set; } = new SnippetInput();

        [Display(Name = "Snippet")]
        public string SnippetOutput { get; set; }

        [Display(Name = "Press \"Tab\" to indent")]
        public bool TabToIndent { get; set; }

        public void OnGet() => SnippetOutput = _snippetGenerator.GetSnippet(SnippetInput.Empty);

        public void OnPost()
        {
            if (!ModelState.IsValid)
            {
                SnippetOutput = _snippetGenerator.GetSnippet(SnippetInput.Empty);
                return;
            }

            SnippetOutput = _snippetGenerator.GetSnippet(SnippetInput);
        }

        public void OnPostDeserialize()
        {
            try
            {
                SnippetInput = _snippetDeserializer.GetSnippetInput(SnippetOutput);
            }
            catch (JsonException exception)
            {
                ModelState.AddModelError(nameof(SnippetOutput), exception.Message);
            }
        }
    }
}

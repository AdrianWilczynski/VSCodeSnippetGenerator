using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VSCodeSnippetGenerator.Web.Models;
using VSCodeSnippetGenerator.Web.Services;

namespace VSCodeSnippetGenerator.Web.Pages
{
    [BindProperties]
    public class IndexModel : PageModel
    {
        private readonly SnippetGenerator _snippetGenerator;

        public IndexModel(SnippetGenerator snippetGenerator)
        {
            _snippetGenerator = snippetGenerator;
        }

        public SnippetInput SnippetInput { get; set; } = new SnippetInput();
        public string SnippetOutput { get; set; }

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
    }
}

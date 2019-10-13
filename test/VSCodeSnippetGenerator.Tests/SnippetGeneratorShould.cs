using System.Text.RegularExpressions;
using VSCodeSnippetGenerator.Web.Models;
using VSCodeSnippetGenerator.Web.Services;
using Xunit;

namespace VSCodeSnippetGenerator.Tests
{
    public class SnippetGeneratorShould
    {
        [Fact]
        public void GeneratePropertySnippet()
        {
            var input = new SnippetInput
            {
                Name = "Property",
                Prefix = "prop",
                Description = "An automatically implemented property.",
                HasDescription = true,
                Body = "public ${1:int} ${2:MyProperty} { get; set; }$0"
            };

            var generator = new SnippetGenerator();

            var snippet = generator.GetSnippet(input);

            var lines = Regex.Split(snippet, @"\r?\n");

            Assert.Matches("^\"Property\": {$", lines[0]);
            Assert.Matches("^\\s+\"prefix\": \"prop\",$", lines[1]);
            Assert.Matches("^\\s+\"body\": \\[$", lines[2]);
            Assert.Matches("^\\s+\"public \\${1:int} \\${2:MyProperty} { get; set; }\\$0\"$", lines[3]);
            Assert.Matches("^\\s+],$", lines[4]);
            Assert.Matches("^\\s+\"description\": \"An automatically implemented property.\"$", lines[5]);
            Assert.Matches("^}$", lines[6]);
        }
    }
}

using System.Text.RegularExpressions;
using VSCodeSnippetGenerator.Web.Services;
using Xunit;

namespace VSCodeSnippetGenerator.Tests
{
    public class SnippetDeserializerShould
    {
        [Fact]
        public void DeserializeSnippet()
        {
            const string snippet = @"""xUnit Fact"": {
		        ""prefix"": ""xu-fact"",
		        ""body"": [
			        ""[Fact]"",
			        ""public void ${1:TestName}()"",
			        ""{"",
			        ""\t$0"",
			        ""}""
		        ]
	        },";

            var deserializer = new SnippetDeserializer();

            var snippetInput = deserializer.GetSnippetInput(snippet);

            Assert.Equal("xUnit Fact", snippetInput.Name);
            Assert.Equal("xu-fact", snippetInput.Prefix);
            Assert.Null(snippetInput.Description);
            Assert.False(snippetInput.HasDescription);
            Assert.True(snippetInput.ConvertToTabs);

            var lines = Regex.Split(snippetInput.Body, @"\r?\n");

            Assert.Equal("[Fact]", lines[0]);
            Assert.Equal("public void ${1:TestName}()", lines[1]);
            Assert.Equal("{", lines[2]);
            Assert.Equal("\t$0", lines[3]);
            Assert.Equal("}", lines[4]);
        }

        [Fact]
        public void DeserializeSingleLineSnippet()
        {
            const string snippet = @"""Property"": {
		        ""prefix"": ""prop"",
		        ""body"": ""public int MyProperty { get; set; }""
	        }";

            var deserializer = new SnippetDeserializer();

            var snippetInput = deserializer.GetSnippetInput(snippet);

            Assert.Equal("Property", snippetInput.Name);
            Assert.Equal("prop", snippetInput.Prefix);
            Assert.Equal("public int MyProperty { get; set; }", snippetInput.Body);
        }
    }
}
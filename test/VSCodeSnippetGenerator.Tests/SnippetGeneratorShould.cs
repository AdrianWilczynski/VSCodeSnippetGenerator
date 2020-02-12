using System;
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

            Assert.Equal("\"Property\": {", lines[0]);
            Assert.Equal("    \"prefix\": \"prop\",", lines[1]);
            Assert.Equal("    \"body\": [", lines[2]);
            Assert.Equal("        \"public ${1:int} ${2:MyProperty} { get; set; }$0\"", lines[3]);
            Assert.Equal("    ],", lines[4]);
            Assert.Equal("    \"description\": \"An automatically implemented property.\"", lines[5]);
            Assert.Equal("}", lines[6]);
        }

        [Fact]
        public void GenerateEmptySnippet()
        {
            var snippet = new SnippetGenerator().GetSnippet(SnippetInput.Empty);

            var lines = Regex.Split(snippet, @"\r?\n");

            Assert.Equal("\"\": {", lines[0]);
            Assert.Equal("    \"prefix\": \"\",", lines[1]);
            Assert.Equal("    \"body\": [", lines[2]);
            Assert.Equal("        \"\"", lines[3]);
            Assert.Equal("    ]", lines[4]);
            Assert.Equal("}", lines[5]);
        }

        [Fact]
        public void GenerateMultilineSnippet()
        {
            var input = new SnippetInput
            {
                Name = "Property",
                Prefix = "prop",
                Body = "public ${1:int} ${2:MyProperty} { get; set; }$0" + Environment.NewLine + "// Second Line"
            };

            var generator = new SnippetGenerator();

            var snippet = generator.GetSnippet(input);

            var lines = Regex.Split(snippet, @"\r?\n");

            Assert.Equal("\"Property\": {", lines[0]);
            Assert.Equal("    \"prefix\": \"prop\",", lines[1]);
            Assert.Equal("    \"body\": [", lines[2]);
            Assert.Equal("        \"public ${1:int} ${2:MyProperty} { get; set; }$0\",", lines[3]);
            Assert.Equal("        \"// Second Line\"", lines[4]);
            Assert.Equal("    ]", lines[5]);
            Assert.Equal("}", lines[6]);
        }


        [Fact]
        public void ConvertSpacesToTabs()
        {
            var input = new SnippetInput
            {
                Name = "Property",
                Prefix = "prop",
                Body = "    public ${1:int} ${2:MyProperty} { get; set; }$0",
                ConvertToTabs = true,
                TabLength = 4
            };

            var generator = new SnippetGenerator();

            var snippet = generator.GetSnippet(input);

            var lines = Regex.Split(snippet, @"\r?\n");

            Assert.Equal("\"Property\": {", lines[0]);
            Assert.Equal("    \"prefix\": \"prop\",", lines[1]);
            Assert.Equal("    \"body\": [", lines[2]);
            Assert.Equal("        \"\\tpublic ${1:int} ${2:MyProperty} { get; set; }$0\"", lines[3]);
            Assert.Equal("    ]", lines[4]);
            Assert.Equal("}", lines[5]);
        }

        [Fact]
        public void EscapeDollarSigns()
        {
            var input = new SnippetInput { Body = "$name = ${1:name};" };

            var snippet = new SnippetGenerator().GetSnippet(input);

            var lines = Regex.Split(snippet, @"\r?\n");

            Assert.Equal("        \"\\\\$name = ${1:name};\"", lines[3]);
        }
    }
}

using System;
using System.Text.RegularExpressions;
using VSCodeSnippetGenerator.Web.Models;
using VSCodeSnippetGenerator.Web.Services;
using Xunit;

namespace VSCodeSnippetGenerator.UnitTests
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

        [Fact]
        public void PreserveSpacesNotUsedForIndentation()
        {
            var input = new SnippetInput
            {
                Body = "    // SOME    TEXT",
                TabLength = 4,
                ConvertToTabs = true
            };

            var snippet = new SnippetGenerator().GetSnippet(input);

            var lines = Regex.Split(snippet, @"\r?\n");

            Assert.EndsWith("\"\\t// SOME    TEXT\"", lines[3]);
        }

        [Fact]
        public void ConvertSpacesToTabsForDeeplyNestedCode()
        {
            var input = new SnippetInput
            {

                Body = @"public void MethodName()
{
    if (true)
    {
        WriteLine();
    }
}",
                ConvertToTabs = true,
                TabLength = 2
            };

            var generator = new SnippetGenerator();

            var snippet = generator.GetSnippet(input);

            var lines = Regex.Split(snippet, @"\r?\n");

            Assert.EndsWith("\"public void MethodName()\",", lines[3]);
            Assert.EndsWith("\"{\",", lines[4]);
            Assert.EndsWith("\"\\t\\tif (true)\",", lines[5]);
            Assert.EndsWith("\"\\t\\t{\",", lines[6]);
            Assert.EndsWith("\"\\t\\t\\t\\tWriteLine();\",", lines[7]);
            Assert.EndsWith("\"\\t\\t}\",", lines[8]);
            Assert.EndsWith("\"}\"", lines[9]);
        }
    }
}

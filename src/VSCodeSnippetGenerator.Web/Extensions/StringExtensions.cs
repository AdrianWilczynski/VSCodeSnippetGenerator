using System.Collections.Generic;
using System.IO;

namespace VSCodeSnippetGenerator.Web.Extensions
{
    public static class StringExtensions
    {
        public static IEnumerable<string> GetLines(this string value)
        {
            using var reader = new StringReader(value);
            while (reader.ReadLine() is string line)
            {
                yield return line;
            }
        }
    }
}
using System.Collections.Generic;
using System.IO;

namespace VSCodeSnippetGenerator.Web.Extensions
{
    public static class StringExtensions
    {
        public static IEnumerable<string> GetLines(this string value)
        {
            using (var reader = new StringReader(value))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    yield return line;
                }
            }
        }
    }
}
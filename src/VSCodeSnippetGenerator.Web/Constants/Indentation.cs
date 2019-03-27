using System.Collections.Generic;
using System.Linq;

namespace VSCodeSnippetGenerator.Web.Constants
{
    public static class Indentation
    {
        public static IEnumerable<int> SpacesPerTab => new List<int> { 2, 4 };
    }
}
using System.Collections.Generic;
using System.Linq;

namespace VSCodeSnippetGenerator.Web.Constants
{
    public static class Indentation
    {
        public static IEnumerable<(string language, string tool, int tabLength)> PerLanguage => new List<(string language, string, int)>
        {
            ( "C#", Tools.VisualStudio2019, 4 ),
            ( "CSS",  Tools.VisualStudio2019, 4),
            ( "HTML",  Tools.VisualStudio2019, 4),
            ( "JS/TS", Tools.Prettier, 2 ),
            ( "JS/TS",  Tools.VisualStudio2019, 4 ),
            ( "JSON",  Tools.VisualStudio2019, 2 ),
            ( "SQL",  Tools.VisualStudio2019, 4 ),
            ( "XML",  Tools.VisualStudio2019, 2 )
        }.OrderBy(i => i.language);

        private static class Tools
        {
            public const string VisualStudio2019 = "Visual Studio 2019";
            public const string Prettier = "Prettier";
        }
    }
}
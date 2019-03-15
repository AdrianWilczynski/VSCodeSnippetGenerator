using System.Collections.Generic;
using System.Linq;

namespace VSCodeSnippetGenerator.Web.Constants
{
    public static class Indentation
    {
        public static IEnumerable<(string language, string tool, int tabLength)> PerLanguage => new List<(string language, string, int)>
        {
            ( "C#", Tools.VisualStudio, 4 ),
            ( "CSS",  Tools.VisualStudio, 4),
            ( "HTML",  Tools.VisualStudio, 4),
            ( "JS/TS", Tools.Prettier, 2 ),
            ( "JS/TS",  Tools.VisualStudio, 4 ),
            ( "JSON",  Tools.VisualStudio, 2 ),
            ( "SQL",  Tools.VisualStudio, 4 ),
            ( "XML",  Tools.VisualStudio, 2 )
        }.OrderBy(i => i.language);

        private static class Tools
        {
            public const string VisualStudio = "Visual Studio";
            public const string Prettier = "Prettier";
        }
    }
}
using System.Collections.Generic;
using System.Linq;

namespace VSCodeSnippetGenerator.Web.Constants
{
    public static class Indentation
    {
        public static IEnumerable<(string language, int tabLength)> PerLanguage => new List<(string language, int tabLength)>
        {
            ( "C#", 4 ),
            ( "CSS", 2),
            ( "HTML", 4),
            ( "JS/TS (Prettier)", 2 ),
            ( "JS/TS (VSCode/Visual Studio 2019)", 4 ),
            ( "JSON", 2 ),
            ( "SQL", 4 ),
            ( "XML", 2 )
        }.OrderBy(i => i.language);
    }
}
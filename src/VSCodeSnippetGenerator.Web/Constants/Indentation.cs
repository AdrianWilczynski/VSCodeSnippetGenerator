using System.Collections.Generic;
using System.Linq;

namespace VSCodeSnippetGenerator.Web.Constants
{
    public static class Indentation
    {
        public static IEnumerable<(string language, string tool, int tabLength)> PerLanguage => new List<(string language, string, int)>
        {
            ( "C#", "Visual Studio 2019", 4 ),
            ( "CSS", "Visual Studio 2019", 4),
            ( "HTML", "Visual Studio 2019", 4),
            ( "JS/TS", "Prettier", 2 ),
            ( "JS/TS", "Visual Studio 2019", 4 ),
            ( "JSON", "Visual Studio 2019", 2 ),
            ( "SQL", "Visual Studio 2019", 4 ),
            ( "XML", "Visual Studio 2019", 2 )
        }.OrderBy(i => i.language);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkullTribalIntrusionServer.ServerSystems.Languages
{
    public static class Language
    {
        public static Dictionary<int, string> Lang;

        public static void CreateLanguage()
        {
            Lang.Add(100, "");
        }
    }
}

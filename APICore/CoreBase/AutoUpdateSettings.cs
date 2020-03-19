using System.Collections.Generic;
using System.IO;

namespace APICore.CoreBase
{
    public class AutoUpdateSettings
    {
        public static string InforUpdateFileName = "AutoUpdate/FileInforUpdate.txt";
        public static string SoftwarePath = Directory.GetCurrentDirectory() + @"/AutoUpdate/SoftComplete/";
        public static string ExeFile = SoftwarePath + "VicSpaManager.exe";
        public static List<string> AutoUpdateListFile;
        public static string CurentVersion;
    }
}

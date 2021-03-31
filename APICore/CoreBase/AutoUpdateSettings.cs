using System.Collections.Generic;
using System.IO;

namespace APICore.CoreBase
{
    public class AutoUpdateSettings
    {
        #region Vic Spa, ứng dụng quản lý spa
        public static string InforUpdateFileName = "AutoUpdate/FileInforUpdate.txt";
        public static string SoftwarePath = Directory.GetCurrentDirectory() + @"/AutoUpdate/SoftComplete/";
        public static string ExeFile = SoftwarePath + "VicSpaManager.exe";
        public static List<string> AutoUpdateListFile;
        public static string CurentVersion;
        #endregion

        #region Run2Backup, ứng dụng chạy và backup data cho game
        public static string Run2BackupPath = Directory.GetCurrentDirectory() + @"/AutoUpdate/Run2Backup/";
        public static string Run2BackupExeFile = Run2BackupPath + "Run2Backup.exe";
        public static string Run2BackupLogFileName = Directory.GetCurrentDirectory()+ @"/AutoUpdate/Run2Backup/Count.txt";
        public static string Run2BackupCurentVersion;
        #endregion
    }
}

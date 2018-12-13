using System;
using System.Data;

namespace CurseFunction
{
    public class Settings
    {
        public static bool debug=true;

        public static bool isAutoStart = false;

        public static bool needResfresh = false;

        public static bool needQuit = false;

        public static DataTable dt = new DataTable();
        public static Db db;

        #region Log
        public static XLog Xlog;
        public static void LogException(Exception e)
        {
            if (Settings.debug)
            {
                Xlog.Dump();
                throw e;
            }
            else
            {
                Xlog.Add(e.Message,"程序异常",XLog.AsyncLogLevel.ERROR);
            }
        }
        public static void LogException(string msg)
        {
            LogException(new Exception(msg));
        }
        public static void Log(string message)
        {
            Xlog.Add(message, "更新提醒", XLog.AsyncLogLevel.INFO);
        }
        #endregion

        public static string deviceId;
        
        public static SoftConfig sc;

        public static string baseHref = "https://www.curseforge.com";
        public static string topCategoryThumb = "https://media.forgecdn.net/avatars/thumbnails/54/513/64/64/636135265289061589.png";
        public static string addonPath = "Interface\\AddOns";

        public static void OpenLink(string href)
        {
            System.Diagnostics.Process.Start(href);
        }
        
    }
}

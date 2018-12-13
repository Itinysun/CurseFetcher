using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace CurseFunction
{
    public class CommonFunction
    {
        public static string GetMachineName()
        {
            try
            {
                return System.Environment.MachineName;
            }
            catch
            {
                return "my-pc";
            }
        }
        public static string HumanTime(DateTime dt)
        {
            TimeSpan ts;
            DateTime now = DateTime.Now;
            string tail;
            string counter;
            if (now > dt)
            {
                ts = now - dt;
                tail = "前";
            }
            else
            {
                ts = dt - now;
                tail = "后";
            }
            double num;
            if (ts.TotalDays > 0)
            {
                num = ts.Days;
                counter = "天";
            }
            else if (ts.TotalHours > 0)
            {
                num = ts.Hours;
                counter = "小时";
            }
            else if (ts.TotalMinutes > 0)
            {
                num = ts.Minutes;
                counter = "分钟";
            }
            else
            {
                num = ts.Seconds;
                counter = "秒";
            }
            return string.Format("{0}{1}{2}", num, counter, tail);
        }
        public static string GetMachineGuid()
        {
            try
            {
                string location = @"SOFTWARE\Microsoft\Cryptography";
                string name = "MachineGuid";
                using (RegistryKey localMachineX64View = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
                {
                    using (RegistryKey rk = localMachineX64View.OpenSubKey(location))
                    {
                        if (rk != null)
                        {
                            object machineGuid = rk.GetValue(name);
                            if (machineGuid != null)
                                return machineGuid.ToString();
                        }
                    }
                }

            }
            catch
            {
            }
            return "default";
        }
        /// <summary>
        /// Add application to Startup of windows
        /// </summary>
        /// <param name="appName"></param>
        /// <param name="path"></param>
        public static void AddStartup(string appName, string path)
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                key.SetValue(appName, "\"" + path + "\" -autostart");
            }
        }

        /// <summary>
        /// Remove application from Startup of windows
        /// </summary>
        /// <param name="appName"></param>
        public static void RemoveStartup(string appName)
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                key.DeleteValue(appName, false);
            }
        }

        /// <summary>
        /// 删除路径串
        /// </summary>
        /// <param name="pathStrs">逗号分隔相对路径</param>
        public static int DeletePaths(string pathStrs)
        {
            if (null == pathStrs)
                return 0;
            var ps = pathStrs.Split(',');
            if(null!=ps && ps.Length > 0)
            {
                foreach(string p in ps)
                {
                    string full = Path.Combine(Settings.sc.game_path, "Interface\\AddOns", p);
                    if (Directory.Exists(full))
                        Directory.Delete(full, true);
                }
                return ps.Length;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 取最高层目录
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static string GetRootDir(string filename)
        {
            if (string.IsNullOrEmpty(filename))
                return null;
            var ps = filename.Split('/','\\');
            if(null!=ps && ps.Length > 0)
            {
                return ps[0];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 判断ip地址是否正确，正确返回true 错误false
        /// </summary>
        /// <param name="strLocalIP">需要判断的字符串(IP地址)</param>
        /// <returns>TRUE OR FALSE</returns>
        public static bool IsRightIP(string strLocalIP)
        {
            if (string.IsNullOrEmpty(strLocalIP))
            {
                return false;
            }
            bool bFlag = false;
            bool Result = true;

            Regex regex = new Regex("^[0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3}$");
            bFlag = regex.IsMatch(strLocalIP);
            if (bFlag == true)
            {
                string[] strTemp = strLocalIP.Split(new char[] { '.' });
                int nDotCount = strTemp.Length - 1; //字符串中.的数量，若.的数量小于3，则是非法的ip地址
                if (3 == nDotCount)//判断 .的数量
                {
                    for (int i = 0; i < strTemp.Length; i++)
                    {
                        if (Convert.ToInt32(strTemp[i]) > 255)   //大于255不符合返回false
                        {
                            Result = false;
                        }
                    }
                }
                else
                {
                    Result = false;
                }
            }
            else
            {
                //输入非数字则提示，不符合IP格式
                //MessageBox.Show("不符合IP格式");
                Result = false;
            }
            return Result;
        }

        /// <summary>
        /// 判断端口是否合法
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        public static bool IsRightPort(string port)
        {
            bool isPort = false;
            isPort = Int32.TryParse(port, out int portNum);
            if (isPort && portNum >= 0 && portNum <= 65535)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}

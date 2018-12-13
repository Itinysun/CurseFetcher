using CurseFunction;
using System;
using System.IO;
using System.Threading;
using System.Collections.Generic;

namespace CurseFetcher
{
    class Worker
    {
        private AddonListItem addon;
        public Worker(AddonListItem ali)
        {
            addon = ali;
            result = new WorkerResult
            {
                success = false,
                msg = new List<string>()
            };
        }
        private WorkerResult result;
        public WorkerResult Update()
        {
            var h = Curl.GetHtmlAsync(Settings.baseHref + addon.href);

            if (null == h)
            {
                Settings.LogException(GenMsg("抓取远程内容失败！插件更新退出"));
                return result;
            }

            var cdl = Curse.PraseDownload(h);

            if(null == cdl || string.IsNullOrEmpty(cdl.version) || string.IsNullOrEmpty(cdl.href))
            {
                Settings.LogException(new Exception(GenMsg("无法获取插件最新版本，可能解析错误！插件更新退出")));
                return result;
            }
            if(!string.IsNullOrEmpty(addon.local_version) && cdl.version.Equals(addon.local_version)){
                Settings.Log(GenMsg("插件已经最新，插件更新退出"));
                return result;
            }
            string cachePath;
            try
            {
                cachePath = Path.Combine(Settings.sc.cachePath, addon.addon_id);
                if (!Directory.Exists(cachePath))
                    Directory.CreateDirectory(cachePath);
            }
            catch(Exception e)
            {
                Settings.LogException(e);
                return result;
            }
            
            string localFile = string.Format(@"{0}\{1}\{2}", Settings.sc.cachePath, addon.addon_id, cdl.version);
            string tmp = localFile + ".tmp";
            string tar = localFile + ".zip";
            if (File.Exists(tar))
            {
                Settings.Log(GenMsg("该插件已有本地缓存跳过下载"));
            }
            else
            {
                if(Curl.Download(Settings.baseHref + cdl.href, tmp))
                {
                    Settings.Log(GenMsg("插件包下载成功"));
                    File.Move(tmp, tar);
                }
                else
                {
                    Settings.LogException(new Exception(GenMsg("下载最新版本失败！插件更新退出")));
                    return result;
                }
            }
            string addonPath="";
            try
            {
                addonPath = Path.Combine(Settings.sc.game_path, Settings.addonPath);
                if (!Directory.Exists(addonPath))
                {
                    Directory.CreateDirectory(addonPath);
                    Settings.Log(GenMsg("游戏插件目录不存在，创建成功"));
                }
            }
            catch
            {
                Settings.LogException(new Exception(GenMsg("创建游戏插件目录失败！插件更新退出:"+addonPath)));
                return result;
            }
            try
            {
                //安装结果是顶级路径，便于卸载时删除，返回null说明安装失败
                var installResult = SharpZip.DecomparessFile(tar, addonPath);
                if (null!=installResult)
                {
                    addon.local_version = cdl.version;
                    addon.update_time = DateTime.Now.ToString();
                    addon.file_paths = installResult;
                    Settings.db.UpdateAddon(addon);
                    Settings.Xlog.Add(GenMsg(" 插件更新成功，最新版本["+cdl.version+"]"),"插件更新",XLog.AsyncLogLevel.INFO);
                    result.success = true;
                    return result;
                }
                else
                {
                    Settings.LogException(new Exception(GenMsg("插件安装失败，尝试接解压到插件目录出错;"+addonPath)));
                    return result;
                }
                
            }catch(Exception e)
            {
                Settings.LogException(e);
                GenMsg("插件安装失败!");
                return result;
            }

        }
        public string GenMsg(string msg)
        {
            var str = string.Format("[{0}]{1}", addon.title, msg);
            result.msg.Add(str);
            return str;
        }
        public struct WorkerResult
        {
            public bool success;
            public List<string> msg;
        }
    }
}

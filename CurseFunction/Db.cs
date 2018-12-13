using LiteDB;
using System.Collections.Generic;
using System;

namespace CurseFunction
{
    public class Db
    {
        /// <summary>
        /// 数据库路径
        /// </summary>
        private readonly string dbPath;

        public Db(string path)
        {
            dbPath = path;
        }



        /// <summary>
        /// 根据设备ID读取配置
        /// </summary>
        /// <param name="deviceId">设备ID</param>
        /// <returns>SoftConfig</returns>
        public SoftConfig ReadConfig(string deviceId)
        {
            try
            {
                using (var instance = new LiteDatabase(dbPath))
                {
                    var cfg = instance.GetCollection<SoftConfig>("config");
                    var result = cfg.FindOne(Query.EQ("device_id", deviceId));
                    if (null != result)
                    {
                        return result;
                    }
                    else
                    {
                        return new SoftConfig();
                    }
                }
            }catch(Exception e)
            {
                Settings.LogException(e);
                return null;
            }

        }
        /// <summary>
        /// 更新设备配置
        /// </summary>
        /// <param name="sc">SoftConfig</param>
        public void SaveConfig(SoftConfig sc)
        {
            try
            {
                using (var instance = new LiteDatabase(dbPath))
                {
                    var cfg = instance.GetCollection<SoftConfig>("config");
                    cfg.Upsert(sc);
                }
            }
            catch(Exception e)
            {
                Settings.LogException(e);
            }

        }

        /// <summary>
        /// 获取插件列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<AddonListItem> GetAddonList()
        {
            try
            {
                using (var instance = new LiteDatabase(dbPath))
                {
                    var addons = instance.GetCollection<AddonListItem>("list");
                    var all = addons.FindAll();
                    return all;
                }
            }
            catch (Exception e)
            {
                Settings.LogException(e);
                return null;
            }

        }

        /// <summary>
        /// 新增插件
        /// </summary>
        /// <param name="ali">AddonListItem</param>
        public void AddAddon(AddonListItem ali)
        {
            try
            {
                using (var instance = new LiteDatabase(dbPath))
                {
                    var ads = instance.GetCollection<AddonListItem>("list");
                    ads.Insert(ali);
                }
            }
            catch (Exception e)
            {
                Settings.LogException(e);
            }

        }

        /// <summary>
        /// 更新插件
        /// </summary>
        /// <param name="ad"></param>
        public void UpdateAddon(AddonListItem ad)
        {
            try
            {
                using (var instance = new LiteDatabase(dbPath))
                {
                    var addons = instance.GetCollection<AddonListItem>("list");
                    addons.Update(ad);
                }
            }
            catch (Exception e)
            {
                Settings.LogException(e);
            }

        }

        /// <summary>
        /// 删除插件
        /// </summary>
        /// <param name="addon_id"></param>
        public string RemoveAddon(string addon_id)
        {
            try
            {
                using (var instance = new LiteDatabase(dbPath))
                {
                    var ads = instance.GetCollection<AddonListItem>("list");
                    var addon = ads.FindOne(x => x.addon_id.Equals(addon_id));
                    if (null == addon)
                    {
                        return null;
                    }
                    else
                    {
                        ads.Delete(addon.Id);
                        return addon.file_paths;
                    }
                }
            }
            catch (Exception e)
            {
                Settings.LogException(e);
                return null;
            }

        }

        /// <summary>
        /// 检测是否安装此插件
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool CheckInstalled(string id)
        {
            try
            {
                using (var instance = new LiteDatabase(dbPath))
                {
                    var addons = instance.GetCollection<AddonListItem>("list");
                    var check = addons.FindOne(x => x.addon_id.Equals(id));
                    return null != check;
                }
            }
            catch (Exception e)
            {
                Settings.LogException(e);
                return false;
            }

        }

    }
}

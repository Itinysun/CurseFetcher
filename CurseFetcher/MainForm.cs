using CurseFetcher.item;
using CurseFunction;
using System;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using System.Collections.Generic;


namespace CurseFetcher
{
    public partial class MainForm : Form
    {
        
        public MainForm(string[] args)
        {
            InitializeComponent();

            try
            {
                //初始化日志
                Settings.Xlog = new XLog(CreatPath(@"\logs"), 1000);
            }
            catch
            {
                Settings.LogException("日志系统异常！可能无法写入日志");
            }           

            try
            {
                //初始化配置
                InitSettings();
            }catch(Exception e1)
            {
                ShowTrace("初始化配置出现异常！");
                Settings.LogException(e1);
            }
            

            //检测是否自动启动，如果自启动并设置自退出则提示用户（此处仅提示）
            CheckAutoStart(args);

            //初始化插件列表
            InitGrid();

            Settings.Xlog.Add("插件列表加载完毕", "系统日志");

            //初始化自动定期检测计时器
            CheckTimer.Interval = 3600;
            CheckTimer.Start();
            Settings.Xlog.Add("自动定期检测计时器启动", "系统日志");
        }

        /// <summary>
        /// 初始化配置
        /// </summary>
        private void InitSettings()
        {
            //初始化设备ID
            Settings.deviceId = CommonFunction.GetMachineGuid();
            Settings.Xlog.Add("软件启动,设备ID：" + Settings.deviceId, "系统日志");

            //初始化数据库，如果不存在则创建空数据库
            var dbPath = CreatPath(@"\data") + @"\db.data";
            Settings.db = new Db(dbPath);
            Settings.Xlog.Add("初始化数据库成功，数据库地址：" + dbPath, "系统日志");

            //根据设备ID读取设备配置，如果无配置则创建空配置
            Settings.sc = Settings.db.ReadConfig(Settings.deviceId);
            Settings.Xlog.Add("载入软件配置成功", "系统日志");

            //初始化缓存路径
            Settings.sc.cachePath = CreatPath(@"\cache");
            Settings.Xlog.Add("缓存路径：" + Settings.sc.cachePath, "系统日志");

            //初始化网络访问代理配置
            Curl.InitProxy();

            //如果缺少必要配置，必须先进行配置,此处修改适配8.1改动
            if (string.IsNullOrEmpty(Settings.sc.device_name) || string.IsNullOrEmpty(Settings.sc.game_path) || !Settings.sc.game_path.Contains("_retail_"))
            {
                using (var c = new Config())
                {
                    c.StartPosition = FormStartPosition.CenterScreen;
                    c.FormClosed += C_FormClosed;
                    c.ShowDialog();
                }
            }
        }

        private void C_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Settings.needQuit)
                Quit();
            else
            {
                try
                {
                    Curl.InitProxy();
                }catch(Exception ex)
                {
                    ShowTrace("加载网络代理失败！");
                    Settings.LogException(ex);
                }
                
            }
                
        }

        /// <summary>
        /// 检测是否自动启动，如果自启动并设置自退出则提示用户（此处仅提示）
        /// </summary>
        /// <param name="args"></param>
        private void CheckAutoStart(string[] args)
        {
            if (null != args && args.Length > 0)
            {
                var start = args[0].Trim();
                if ("autostart" == start)
                {
                    Settings.isAutoStart = true;
                    if (Settings.sc.auto_quit)
                    {
                        ShowTrace(new List<string> { "检测到自动退出设置，系统将在更新完插件后自动退出" });
                    }
                }
                Settings.Xlog.Add("启动参数：" + start, "系统日志");
            }
            else
            {
                Settings.Xlog.Add("无启动参数", "系统日志");
            }
        }

        /// <summary>
        /// 插件市场关闭后，检测是否有插件需要安装
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void M_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Settings.needResfresh)
            {
                Settings.needResfresh = false;
                CheckUpdate();
            }
        }

        /// <summary>
        /// 关于界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripButtonAbout_Click(object sender, EventArgs e)
        {
            using (var a = new AboutBox1())
            {
                a.ShowDialog();
            }
        }

        /// <summary>
        /// 配置界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsButtonConfig_Click(object sender, EventArgs e)
        {
            using (var c=new Config())
            {
                c.FormClosed += C_FormClosed;
                c.ShowDialog();
            }
        }

        /// <summary>
        /// 检测更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TbStartUpdate_Click(object sender, EventArgs e)
        {
            TbStartUpdate.Enabled = false;
            toolStripContainer1.UseWaitCursor = true;
            StartUpdate();
            TbStartUpdate.Enabled = true;
            toolStripContainer1.UseWaitCursor = false;
        }

        /// <summary>
        /// 安装新插件-调用插件市场界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripButtonAdd_Click(object sender, EventArgs e)
        {
            using (var m = new Market(Settings.baseHref))
            {
                m.FormClosed += M_FormClosed;
                m.ShowDialog();
            }
        }

        /// <summary>
        /// 删除插件，同时删除本地文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsDel_Click(object sender, EventArgs e)
        {
            var row = dataGridView1.CurrentRow;
            if (null != row)
            {
                try
                {
                    var id = row.Cells["addon_id"].Value.ToString();
                    var title = row.Cells["title"].Value.ToString();
                    var paths = Settings.db.RemoveAddon(id);
                    try
                    {
                        if (null != paths)
                        {
                            var count = CommonFunction.DeletePaths(paths);
                            ShowTrace("已自动清理插件目录数量：" + count.ToString());
                        }
                        else
                        {
                            ShowTrace("删除插件目录失败，请手动清理：" + title);
                        }
                            
                    }
                    catch
                    {
                        ShowTrace("删除插件目录失败，请手动清理：" + title);
                    }
                    Settings.dt.Rows.RemoveAt(row.Index);
                    ShowTrace("插件删除成功：" + title);
                }
                catch (Exception ex)
                {
                    Settings.LogException(ex);
                    ShowTrace("插件删除失败：" + ex.Message);
                }

            }
            else
            {
                MessageBox.Show("请选择一个插件");
            }
        }

        /// <summary>
        /// 程序退出时，输出所有缓存区日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
            NfIcon.Visible = true;
            NfIcon.ShowBalloonTip(1000, "CurseFetcher", "程序已经最小化到托盘中，右键彻底退出", ToolTipIcon.Info);
            e.Cancel = true;
        }

        /// <summary>
        /// 创建路径，如果创建失败则退出程序
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        private string CreatPath(string dir)
        {
            string d = Application.StartupPath + dir;
            try
            {
                if (!Directory.Exists(d))
                {
                    Directory.CreateDirectory(d);
                }
                return d;
            }
            catch (Exception e)
            {
                Settings.LogException(e);
                MessageBox.Show("创建路径失败，请确保程序所在文件夹具有读写权限，或者使用管理员身份运行");
                Quit();
                return null;
            }
        }

        private void NfIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                //还原窗体显示    
                WindowState = FormWindowState.Normal;
                //激活窗体并给予它焦点
                this.Activate();
                //任务栏区显示图标
                this.ShowInTaskbar = true;
            }
        }

        private void TsQuit_Click(object sender, EventArgs e)
        {
            Quit();
        }

        private void TsShow_Click(object sender, EventArgs e)
        {
            //还原窗体显示    
            WindowState = FormWindowState.Normal;
            //激活窗体并给予它焦点
            this.Activate();
            //任务栏区显示图标
            this.ShowInTaskbar = true;
        }

        private void TsAbout_Click(object sender, EventArgs e)
        {
            using (var a = new AboutBox1())
            {
                a.ShowDialog();
            }
        }
        private void Quit()
        {
            Settings.Xlog.Dump();
            NfIcon.Dispose();
            Application.Exit();
        }

        private void TbQQ_Click(object sender, EventArgs e)
        {
            using(var qq = new QQ())
            {
                qq.ShowDialog();
            }
        }
    }
}

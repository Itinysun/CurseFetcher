using CurseFunction;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Windows.Forms;


namespace CurseFetcher
{
    public partial class MainForm : Form
    {
        private DateTime lastCheck;
        private List<AddonListItem> updateList = new List<AddonListItem>();


        /// <summary>
        /// 更新检测计时器的主要工作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckTimer_Tick(object sender, EventArgs e)
        {
            if (NeedCheck(lastCheck))
            {
                CheckUpdate();
                lastCheck = DateTime.Now;
                ShowTrace(new List<string> { "下次自动更新检测为24小时后" });
                if(Settings.isAutoStart && Settings.sc.auto_quit)
                {
                    Quit();
                }
            }
        }

        /// <summary>
        /// 启动更新任务，并在更新后刷新插件列表
        /// </summary>
        private void StartUpdate()
        {
            foreach(var ad in updateList)
            {
                var w = new Worker(ad);
                var msg = w.Update();
                ShowTrace(msg.msg);
                UpdateStatus(ad.addon_id, msg.success ? "更新成功" : "更新失败");
            }
            updateList.Clear();
            FillData();
        }

        /// <summary>
        /// 检测需要更新的插件
        /// </summary>
        private void CheckUpdate()
        {
            updateList.Clear();
            try
            {
                var list = Settings.db.GetAddonList();
                foreach (var ad in list)
                {
                    if (NeedCheck(ad.update_time))
                    {
                        updateList.Add(ad);
                        UpdateStatus(ad.addon_id, "更新中");
                    }
                }
                if (updateList.Count > 0)
                {
                    lastCheck = DateTime.Now;
                    ThreadStart update = new ThreadStart(StartUpdate);
                    Thread progressThread = new Thread(update)
                    {
                        IsBackground = true
                    };
                    progressThread.Start();
                }
                else
                {
                    ShowTrace("没有需要更新的插件");
                }
            }
            catch (Exception ex)
            {
                ShowTrace("检测更新过程出现异常！检测失败");
                Settings.LogException(ex);
            }

        }

        #region need check
        /// <summary>
        /// 如果时间距现在超过1天则确定需要更新
        /// </summary>
        /// <param name="dt">时间</param>
        /// <returns></returns>
        private bool NeedCheck(DateTime dt)
        {
            if (null == dt)
                return true;
            TimeSpan ts = DateTime.Now - dt;
            if (ts.TotalDays > 1)
                return true;
            return false;
        }

        /// <summary>
        /// 如果时间距现在超过1天则确定需要更新
        /// </summary>
        /// <param name="dt">字符类型时间</param>
        /// <returns></returns>
        private bool NeedCheck(string dt)
        {
            if (string.IsNullOrEmpty(dt))
            {
                return true;
            }
            if (DateTime.TryParse(dt, out DateTime ndt))
            {
                return NeedCheck(ndt);
            }
            return true;
        }
        #endregion

        #region 更新列表中的插件状态
        private delegate void UpdateStatusHandle(string id, string content);
        private void UpdateStatusFunction(string id, string content)
        {
            foreach (DataRow row in Settings.dt.Rows)
            {
                if (id == row["addon_id"].ToString())
                {
                    row["update_time"] = content;
                }
            }
        }
        /// <summary>
        /// 更新插件列表中的插件状态
        /// </summary>
        /// <param name="id">插件的ID</param>
        /// <param name="content">状态内容</param>
        private void UpdateStatus(string id, string content)
        {
            if (dataGridView1.InvokeRequired)
            {
                this.Invoke(new UpdateStatusHandle(UpdateStatusFunction), id, content);
            }
            else
            {
                UpdateStatusFunction(id, content);
            }
        }
        #endregion

        #region showtrace
        private delegate void UpdateTrace(List<string> msg);
        private void UpdateTraceFunction(List<string> msg)
        {
            if (RbMsg.Lines.Length > 500)
            {
                RbMsg.Clear();
            }
            if (msg.Count>0)
            {
                foreach (var str in msg)
                {
                    RbMsg.Text = RbMsg.Text.Insert(0,string.Format("[{0}]{1}{2}", DateTime.Now.ToShortTimeString(), str, Environment.NewLine));
                }
            } 
        }
        /// <summary>
        /// 显示必要的系统日志
        /// </summary>
        /// <param name="msg">日志列表</param>
        public void ShowTrace(List<string> msg)
        {
            if (RbMsg.InvokeRequired)
            {
                this.Invoke(new UpdateTrace(UpdateTraceFunction), msg);
            }
            else
            {
                UpdateTraceFunction(msg);
            }
        }

        public void ShowTrace(string msg)
        {
            ShowTrace(new List<string> { msg });
        }
        #endregion
    }
}

using CurseFunction;
using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CurseFetcher.item
{
    public partial class Market : Form
    {
        private void GetDetail(CurseListItem cli)
        {
            currentItem = cli;
            ThreadStart detail = new ThreadStart(PraseDetail);
            Thread detailThread = new Thread(detail)
            {
                IsBackground = true
            };
            detailThread.Start();
        }
        private void PraseDetail()
        {
            CleanToolstrip(toolStripDetail);
            UpdateDetailHeader(currentItem);
            string html = Curl.GetHtmlAsync(baseHref + currentItem.href);
            SetProgress(50);
            if (null != html)
            {
                try
                {
                    CurseDetail cd = Curse.PraseDetail(html);
                    currentDetail = cd;
                    ShowDetailContent(cd);
                }catch(Exception e)
                {
                    Settings.LogException(e);
                }

            }
            else
            {
                SetStatus("Error!");
                SetProgress(0);
            }
            try
            {
                foreach (var li in flowLayoutPanel1.Controls)
                {
                    var cli = (ListItem)li;
                    cli.Invoke(new EventHandler(delegate {
                        cli.SetClickEnable(true);
                        cli.Refresh();
                    }));
                }
            }catch(Exception e)
            {
                Settings.LogException(e);
            }

            SetStatus(null);
            SetProgress(100);
        }

        delegate void UpdateDetailHeaderDelegate(CurseListItem li);
        void UpdateDetailHeader(CurseListItem li)
        {
            if (splitContainer2.InvokeRequired)
            {
                this.Invoke(new UpdateDetailHeaderDelegate(UpdateDetailHeaderFunction), li);
            }
        }
        void UpdateDetailHeaderFunction(CurseListItem li)
        {
            if (null == li)
            {
                splitContainer2.Visible = false;
                labelDetailTitle.Text = "";
                labelDetialUpdate.Text = "";
                pictureBoxDetail.ImageLocation = "";
            }
            else
            {
                splitContainer2.Show();
                labelDetailTitle.Text = li.title;
                labelDetialUpdate.Text = li.update;
                pictureBoxDetail.ImageLocation = li.thumbnails;
                if (li.isInstall)
                {
                    BtDetailInstall.Text = "已安装";
                    BtDetailInstall.Enabled = false;
                    BtDetailInstall.BackColor = Color.Gray;
                }
                else
                {
                    BtDetailInstall.Text = "安装";
                    BtDetailInstall.Enabled = true;
                    BtDetailInstall.BackColor = Color.MediumTurquoise;
                }
            }

        }

        delegate void ShowDetailContentDelegate(CurseDetail cd);
        void ShowDetailContent(CurseDetail cd)
        {
            if (toolStripContainer2.InvokeRequired)
            {
                this.Invoke(new ShowDetailContentDelegate(ShowDetailContentFunction), cd);
            }
        }
        void ShowDetailContentFunction(CurseDetail cd)
        {
            if (null != cd)
            {
                foreach (var p in cd.pages)
                {
                    ToolStripButton tb = new ToolStripButton();
                    tb.Name = p.title;
                    tb.Text = p.title;
                    tb.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                    tb.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));

                    tb.Click += DetailPage_click;
                    toolStripDetail.Items.Add(tb);
                }
                richTextBoxContent.Text = cd.content;
            }
        }

        private void DetailPage_click(object sender, EventArgs e)
        {
            try
            {
                var t = ((ToolStripButton)sender).Name;
                foreach (var p in currentDetail.pages)
                {
                    if (p.title.ToLower().Equals(t.ToLower()))
                    {
                        string html = Curl.GetHtmlAsync(baseHref + p.href);
                        if (null != html)
                        {
                            CurseDetail cd = Curse.PraseDetail(html);
                            richTextBoxContent.Text = cd.content;
                        }
                    }
                }
            }catch(Exception ex)
            {
                Settings.LogException(ex);
            }

        }

        delegate void UpdateDownloadDelegate(string id);
        void UpdateDownloadFunction(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                MessageBox.Show("下载失败！");
            }
            else
            {
                foreach (var li in flowLayoutPanel1.Controls)
                {
                    var cli = (ListItem)li;
                    if (id == cli.item.id)
                    {
                        cli.item.isInstall = true;
                        cli.pictureBoxInstalled.Show();
                    }
                }
                var row = Settings.dt.NewRow();
                row["title"] = currentItem.title;
                row["local_version"] = "安装中";
                row["update_time"] = string.Empty;
                row["addon_id"] = currentItem.id;
                Settings.dt.Rows.Add(row);
            }
            
        }
        private void UpdateDownloadFlag(string id)
        {
            this.Invoke(new UpdateDownloadDelegate(UpdateDownloadFunction),id);
        }
        private void BtDetailInstall_Click(object sender, EventArgs e)
        {
            if (currentItem.isInstall)
                return;
            ThreadStart add = new ThreadStart(InstallAddon);
            Thread addThread = new Thread(add)
            {
                IsBackground = true
            };
            addThread.Start();
        }
        private void InstallAddon()
        {
            try
            {
                var href = currentItem.href + "/download";
                if (!String.IsNullOrEmpty(href))
                {
                    currentItem.isInstall = true;
                    UpdateDetailHeader(currentItem);
                    UpdateDownloadFlag(currentItem.id);
                    AddonListItem ad = new AddonListItem
                    {
                        addon_id = currentItem.id,
                        title = currentItem.title,
                        local_version = "",
                        href = href
                    };
                    Settings.db.AddAddon(ad);
                    Settings.needResfresh = true;
                }
                else
                {
                    UpdateDownloadFlag(null);
                }
            }catch(Exception e)
            {
                Settings.LogException(e);
            }


        }
    }
}

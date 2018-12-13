using CurseFunction;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace CurseFetcher.item
{
    public partial class Market : Form
    {
        #region List
        delegate void addListItemDelegate(ListItem tn);
        void AddListItem(ListItem tn)
        {
            if (flowLayoutPanel1.InvokeRequired)
                this.Invoke(new addListItemDelegate(AddListItemFunction), tn);
            else
                AddListItemFunction(tn);
        }
        void AddListItemFunction(ListItem tn)
        {
            flowLayoutPanel1.Controls.Add(tn);
        }


        private void GetList(string href)
        {
            try
            {
                currentUrl = baseHref + href;
                SetEnable(false);
                CleanToolstrip(toolStrip1);
                CleanList();
                ThreadStart list = new ThreadStart(PraseList);
                Thread progressThread = new Thread(list)
                {
                    IsBackground = true
                };
                progressThread.Start();
            }
            catch(Exception e)
            {
                Settings.LogException(e);
                SetStatus("获取插件列表失败！");
            }

        }

        private void PraseList()
        {
            SetProgress(1);
            SetStatus("Working...");
            string html = Curl.GetHtmlAsync(currentUrl);
            if (null == html)
            {
                SetProgress(100);
                SetStatus(null);
                MessageBox.Show("获取远程内容失败，请重试！");
                return;
            }
            SetProgress(50);
            InitPage(html);
            SetProgress(60);
            if (!treeInited)
                InitTree(html);
            SetProgress(70);
            try
            {
                var lis = Curse.PraseList(html);
                if (null == lis)
                {
                    SetStatus("No Result!");
                }
                else
                {
                    int i = 0;
                    foreach (var li in lis)
                    {
                        ListItem ad = new ListItem(li, i);
                        ad.SetOnClickListener(onListItemClickListener);
                        AddListItem(ad);
                        i++;
                    }
                }
            }catch(Exception e)
            {
                Settings.LogException(e);
            }
            SetStatus(null);
            SetProgress(100);
        }
        #endregion

        #region pages
        delegate void addPagesDelegate(ToolStripButton tn);
        void AddPages(ToolStripButton tn)
        {
            if (toolStrip1.InvokeRequired)
                this.Invoke(new addPagesDelegate(addPagesFunction), tn);
            else
                addPagesFunction(tn);
        }
        void addPagesFunction(ToolStripButton tn)
        {
            toolStrip1.Items.Add(tn);
        }
        private List<CursePageLink> pages = new List<CursePageLink>();

        /// <summary>
        /// 解析列表的分页
        /// </summary>
        /// <param name="html"></param>
        private void InitPage(string html)
        {
            try
            {
                pages = Curse.PrasePages(html);
                if (null == pages)
                    return;
                foreach (var pg in pages)
                {
                    ToolStripButton tb = new ToolStripButton
                    {
                        Name = pg.title,
                        Text = pg.title,
                        DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text,
                        Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)))
                    };

                    if (pg.iscurrent || pg.isdot)
                        tb.Enabled = false;
                    else
                        tb.Click += ListPage_Click;
                    AddPages(tb);
                }
            }catch(Exception ex)
            {
                Settings.LogException(ex);
            }
        }

        private void ListPage_Click(object sender, EventArgs e)
        {
            var t = ((ToolStripButton)sender).Name;
            foreach (var p in pages)
            {
                if (p.title.ToLower().Equals(t.ToLower()))
                {
                    GetList(p.href);
                }
            }
        }
        #endregion

        #region clean
        delegate void CleanListDelegate();
        void CleanList()
        {
            if (flowLayoutPanel1.InvokeRequired)
            {
                this.Invoke(new CleanListDelegate(CleanListFunction));
            }
            else
            {
                CleanListFunction();
            }
        }
        private void CleanListFunction()
        {
            int count = flowLayoutPanel1.Controls.Count;
            if (count > 0)
            {
                for (int i = count; i > 0; i--)
                {
                    flowLayoutPanel1.Controls[i - 1].Dispose();
                }
            }
            flowLayoutPanel1.Controls.Clear();
        }
        delegate void CleanToolstripDelegate(ToolStrip ts);
        void CleanToolstrip(ToolStrip ts)
        {
            if (ts.InvokeRequired)
            {
                ts.Invoke(new CleanToolstripDelegate(CleanToolStripFunction), ts);
            }
            else
            {
                CleanToolStripFunction(ts);
            }
        }
        void CleanToolStripFunction(ToolStrip ts)
        {
            var count = ts.Items.Count;
            if (count > 0)
            {
                for (int i = count; i > 0; i--)
                {
                    ts.Items[i - 1].Dispose();
                }
            }
            ts.Items.Clear();
        }
        #endregion

        #region clickEvent
        private class OnListItemClickListener : ListItem.IOnClickListener
        {
            private readonly Market form; // 用于刷新列表

            public OnListItemClickListener(Market form)
            {
                this.form = form;
            }

            public void OnClick(int id)
            {
                this.form.SelectListItem(id);
            }
        }
        private OnListItemClickListener onListItemClickListener;
        public void SelectListItem(int id)
        {
            SetStatus("Working...");
            SetProgress(1);
            foreach (var li in flowLayoutPanel1.Controls)
            {
                var cli = (ListItem)li;
                if (id == cli.id)
                {
                    cli.SetSelected(true);

                    GetDetail(cli.item);

                }
                else
                {
                    cli.SetSelected(false);
                }
                cli.SetClickEnable(false);
                cli.Refresh();
            }
        }
        #endregion

        #region sort
        private int currentSort = 4;
        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Resort(4);
        }
        private void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Resort(5);
        }
        private void RadioButton3_CheckedChanged(object sender, EventArgs e)
        {
            Resort(2);
        }
        private void Resort(int i)
        {
            currentSort = i;
            GetList(GetFullUrl());
        }
        private string GetFullUrl()
        {

            if (currentSort != 4)
            {
                return currentCategory + "?filter-game-version=&filter-sort=" + currentSort.ToString();
            }
            return currentCategory;
        }
        #endregion

        #region search
        private void ButtonSearch_Click(object sender, EventArgs e)
        {
            string s = System.Web.HttpUtility.UrlEncode(textBoxSearch.Text.Trim());
            panel3.Hide();
            treeView1.Enabled = false;
            disableSelectEvent = true;
            treeView1.SelectedNode = treeView1.Nodes[0];
            disableSelectEvent = false;
            GetList("/wow/addons/search?search=" + s);
        }

        private void ButtonReset_Click(object sender, EventArgs e)
        {
            panel3.Show();
            treeView1.Enabled = true;
            treeView1.SelectedNode = treeView1.Nodes[0];
            textBoxSearch.Text = "";
        }
        #endregion
    }
}

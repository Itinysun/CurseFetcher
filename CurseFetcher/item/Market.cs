using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CurseFunction;
using System.Threading.Tasks;

namespace CurseFetcher.item
{
    public partial class Market : Form
    {
        
        private string baseHref = "";
        private string currentCategory = "";
        private string currentUrl = "";
        private bool treeInited = false;
        private CurseListItem currentItem = null;
        private CurseDetail currentDetail = null;
        private Dictionary<string, CurseMenuItem> dicts = new Dictionary<string, CurseMenuItem>();
        private bool disableSelectEvent = false;
        public Market(string b)
        {
            baseHref = b;
            InitializeComponent();
            this.onListItemClickListener = new OnListItemClickListener(this);
            pictureBoxCurrent.ImageLocation = Settings.topCategoryThumb;
        }

        private void Market_Load(object sender, EventArgs e)
        {
            GetList("/wow/addons");
        }

        private void Market_FormClosing(object sender, FormClosingEventArgs e)
        {
            richTextBoxContent.Dispose();
            GC.Collect();
        }

        #region progressbar
        // 定义委托，异步调用
        delegate void ShowProgressDelegate(int totalStep, int currentStep);
        /// <summary>
        /// 设置当前进度
        /// </summary>
        /// <param name="state"></param>
        void SetProgress(int state)
        {
            this.Invoke(new ShowProgressDelegate(ShowProgress), new object[] { 100, state });
        }
        /// <summary>
        /// 刷新进度条
        /// </summary>
        /// <param name="totalStep"></param>
        /// <param name="currentStep"></param>
        void ShowProgress(int totalStep, int currentStep)
        {
            this.toolStripProgressBar1.Maximum = totalStep;
            this.toolStripProgressBar1.Value = currentStep;
        }
        #endregion

        #region status
        delegate void setStatusDelegate(string tn);
        void SetStatus(string tn)
        {
            this.Invoke(new setStatusDelegate(SetStatusFunction),tn);
        }
        void SetStatusFunction(string tn)
        {
            if (null == tn)
            {
                toolStripStatusLabel1.Text="";
                treeView1.Enabled = true;
                textBoxSearch.Enabled = true;
                buttonSearch.Enabled = true;
                buttonReset.Enabled = true;
            }
            else
            {
                toolStripStatusLabel1.Text = tn;
            }
        }
        private void SetEnable(bool en)
        {
            treeView1.Enabled = en;
            textBoxSearch.Enabled = en;
            buttonSearch.Enabled = en;
            buttonReset.Enabled = en;
            splitContainer2.Visible = en;
        }
        #endregion

        #region tree
        delegate void addTreeNodeDelegate(TreeNode tn);
        void AddTreeNode(TreeNode tn)
        {
            if (treeView1.InvokeRequired)
                this.Invoke(new addTreeNodeDelegate(AddTreeNodeFunction),tn);
            else
                AddTreeNodeFunction(tn);
        }
        void AddTreeNodeFunction(TreeNode tn)
        {
            treeView1.Nodes.Add(tn);
        }
        private void InitTree(string html)
        {
            try
            {
                var mls = Curse.PraseCategory(html);
                int index = 0;
                foreach (var m in mls)
                {
                    TreeNode tn = new TreeNode
                    {
                        Text = m.title,
                        Name = index.ToString()
                    };
                    dicts.Add(index.ToString(), m);
                    index++;
                    if (m.childs.Count > 0)
                    {
                        foreach (var cm in m.childs)
                        {
                            TreeNode ctn = new TreeNode();
                            ctn.Text = cm.title;
                            ctn.Name = index.ToString();
                            tn.Nodes.Add(ctn);
                            dicts.Add(index.ToString(), cm);
                            index++;
                        }
                    }
                    AddTreeNode(tn);
                }
                treeInited = true;
            }catch(Exception e)
            {
                Settings.LogException(e);
            }

        }
        private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (!disableSelectEvent)
            {
                labelCurrent.Text = e.Node.Text;
                CurseMenuItem m = dicts[e.Node.Name];
                string cat = m.path.Replace(" ", "-").TrimEnd('/');
                string path = "/wow/addons" + cat;
                pictureBoxCurrent.ImageLocation = m.thumb;
                currentCategory = path;
                textBoxSearch.Text = "";
                GetList(GetFullUrl());
            }
        }
        #endregion


    }
}

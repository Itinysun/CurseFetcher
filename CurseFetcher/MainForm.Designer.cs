namespace CurseFetcher
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.RbMsg = new System.Windows.Forms.RichTextBox();
            this.CheckTimer = new System.Windows.Forms.Timer(this.components);
            this.NfIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.TsShow = new System.Windows.Forms.ToolStripMenuItem();
            this.TsAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.TsQuit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripButtonAdd = new System.Windows.Forms.ToolStripButton();
            this.TsDel = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonAbout = new System.Windows.Forms.ToolStripButton();
            this.TsButtonConfig = new System.Windows.Forms.ToolStripButton();
            this.TbStartUpdate = new System.Windows.Forms.ToolStripButton();
            this.TbQQ = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.toolStripContainer1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.RbMsg);
            this.splitContainer1.Size = new System.Drawing.Size(566, 367);
            this.splitContainer1.SplitterDistance = 314;
            this.splitContainer1.TabIndex = 7;
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.dataGridView1);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(566, 289);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(566, 314);
            this.toolStripContainer1.TabIndex = 0;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(566, 289);
            this.dataGridView1.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonAdd,
            this.toolStripSeparator1,
            this.TsDel,
            this.toolStripSeparator2,
            this.toolStripButtonAbout,
            this.toolStripSeparator3,
            this.TsButtonConfig,
            this.TbStartUpdate,
            this.toolStripSeparator4,
            this.TbQQ});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(566, 25);
            this.toolStrip1.Stretch = true;
            this.toolStrip1.TabIndex = 0;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // RbMsg
            // 
            this.RbMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RbMsg.Location = new System.Drawing.Point(0, 0);
            this.RbMsg.Name = "RbMsg";
            this.RbMsg.Size = new System.Drawing.Size(566, 49);
            this.RbMsg.TabIndex = 0;
            this.RbMsg.Text = "";
            // 
            // CheckTimer
            // 
            this.CheckTimer.Tick += new System.EventHandler(this.CheckTimer_Tick);
            // 
            // NfIcon
            // 
            this.NfIcon.ContextMenuStrip = this.contextMenuStrip1;
            this.NfIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("NfIcon.Icon")));
            this.NfIcon.Text = "CurseFetcher";
            this.NfIcon.Visible = true;
            this.NfIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.NfIcon_MouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsShow,
            this.TsAbout,
            this.TsQuit});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(125, 70);
            // 
            // TsShow
            // 
            this.TsShow.Name = "TsShow";
            this.TsShow.Size = new System.Drawing.Size(124, 22);
            this.TsShow.Text = "显示程序";
            this.TsShow.Click += new System.EventHandler(this.TsShow_Click);
            // 
            // TsAbout
            // 
            this.TsAbout.Name = "TsAbout";
            this.TsAbout.Size = new System.Drawing.Size(124, 22);
            this.TsAbout.Text = "关于";
            this.TsAbout.Click += new System.EventHandler(this.TsAbout_Click);
            // 
            // TsQuit
            // 
            this.TsQuit.Name = "TsQuit";
            this.TsQuit.Size = new System.Drawing.Size(124, 22);
            this.TsQuit.Text = "退出程序";
            this.TsQuit.Click += new System.EventHandler(this.TsQuit_Click);
            // 
            // toolStripButtonAdd
            // 
            this.toolStripButtonAdd.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAdd.Image")));
            this.toolStripButtonAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAdd.Name = "toolStripButtonAdd";
            this.toolStripButtonAdd.Size = new System.Drawing.Size(52, 22);
            this.toolStripButtonAdd.Text = "安装";
            this.toolStripButtonAdd.ToolTipText = "安装";
            this.toolStripButtonAdd.Click += new System.EventHandler(this.ToolStripButtonAdd_Click);
            // 
            // TsDel
            // 
            this.TsDel.Image = ((System.Drawing.Image)(resources.GetObject("TsDel.Image")));
            this.TsDel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsDel.Name = "TsDel";
            this.TsDel.Size = new System.Drawing.Size(52, 22);
            this.TsDel.Text = "删除";
            this.TsDel.Click += new System.EventHandler(this.TsDel_Click);
            // 
            // toolStripButtonAbout
            // 
            this.toolStripButtonAbout.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButtonAbout.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAbout.Image")));
            this.toolStripButtonAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAbout.Name = "toolStripButtonAbout";
            this.toolStripButtonAbout.Size = new System.Drawing.Size(52, 22);
            this.toolStripButtonAbout.Text = "关于";
            this.toolStripButtonAbout.Click += new System.EventHandler(this.ToolStripButtonAbout_Click);
            // 
            // TsButtonConfig
            // 
            this.TsButtonConfig.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.TsButtonConfig.Image = ((System.Drawing.Image)(resources.GetObject("TsButtonConfig.Image")));
            this.TsButtonConfig.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsButtonConfig.Name = "TsButtonConfig";
            this.TsButtonConfig.Size = new System.Drawing.Size(76, 22);
            this.TsButtonConfig.Text = "软件配置";
            this.TsButtonConfig.Click += new System.EventHandler(this.TsButtonConfig_Click);
            // 
            // TbStartUpdate
            // 
            this.TbStartUpdate.Image = ((System.Drawing.Image)(resources.GetObject("TbStartUpdate.Image")));
            this.TbStartUpdate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TbStartUpdate.Name = "TbStartUpdate";
            this.TbStartUpdate.Size = new System.Drawing.Size(76, 22);
            this.TbStartUpdate.Text = "检测更新";
            this.TbStartUpdate.Click += new System.EventHandler(this.TbStartUpdate_Click);
            // 
            // TbQQ
            // 
            this.TbQQ.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.TbQQ.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.TbQQ.Image = ((System.Drawing.Image)(resources.GetObject("TbQQ.Image")));
            this.TbQQ.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TbQQ.Name = "TbQQ";
            this.TbQQ.Size = new System.Drawing.Size(60, 22);
            this.TbQQ.Text = "神秘按钮";
            this.TbQQ.Click += new System.EventHandler(this.TbQQ_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(566, 367);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "魔兽世界插件更新器";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonAdd;
        private System.Windows.Forms.ToolStripButton TsDel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton TsButtonConfig;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolStripButtonAbout;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.RichTextBox RbMsg;
        private System.Windows.Forms.ToolStripButton TbStartUpdate;
        private System.Windows.Forms.Timer CheckTimer;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem TsQuit;
        private System.Windows.Forms.ToolStripMenuItem TsAbout;
        private System.Windows.Forms.ToolStripMenuItem TsShow;
        public System.Windows.Forms.NotifyIcon NfIcon;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton TbQQ;
    }
}


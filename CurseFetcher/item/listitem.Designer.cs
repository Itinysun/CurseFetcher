namespace CurseFetcher.item
{
    partial class ListItem
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListItem));
            this.pictureBoxThumb = new System.Windows.Forms.PictureBox();
            this.labeltitle = new System.Windows.Forms.Label();
            this.labeldescription = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labelupdate = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labeldownload = new System.Windows.Forms.Label();
            this.pictureBoxInstalled = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxThumb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxInstalled)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxThumb
            // 
            this.pictureBoxThumb.ImageLocation = "";
            this.pictureBoxThumb.InitialImage = global::CurseFetcher.Properties.Resources.anvilBlack;
            this.pictureBoxThumb.Location = new System.Drawing.Point(10, 10);
            this.pictureBoxThumb.Name = "pictureBoxThumb";
            this.pictureBoxThumb.Size = new System.Drawing.Size(62, 62);
            this.pictureBoxThumb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxThumb.TabIndex = 0;
            this.pictureBoxThumb.TabStop = false;
            this.pictureBoxThumb.Click += new System.EventHandler(this.ListItem_Click);
            // 
            // labeltitle
            // 
            this.labeltitle.AutoSize = true;
            this.labeltitle.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labeltitle.ForeColor = System.Drawing.Color.DodgerBlue;
            this.labeltitle.Location = new System.Drawing.Point(80, 10);
            this.labeltitle.Name = "labeltitle";
            this.labeltitle.Size = new System.Drawing.Size(43, 22);
            this.labeltitle.TabIndex = 1;
            this.labeltitle.Text = "title";
            this.labeltitle.Click += new System.EventHandler(this.ListItem_Click);
            // 
            // labeldescription
            // 
            this.labeldescription.AutoEllipsis = true;
            this.labeldescription.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labeldescription.Location = new System.Drawing.Point(80, 50);
            this.labeldescription.Name = "labeldescription";
            this.labeldescription.Size = new System.Drawing.Size(432, 28);
            this.labeldescription.TabIndex = 2;
            this.labeldescription.Text = "description";
            this.labeldescription.Click += new System.EventHandler(this.ListItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label1.Location = new System.Drawing.Point(80, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "更新时间：";
            this.label1.Click += new System.EventHandler(this.ListItem_Click);
            // 
            // labelupdate
            // 
            this.labelupdate.AutoSize = true;
            this.labelupdate.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.labelupdate.Location = new System.Drawing.Point(142, 34);
            this.labelupdate.Name = "labelupdate";
            this.labelupdate.Size = new System.Drawing.Size(65, 12);
            this.labelupdate.TabIndex = 4;
            this.labelupdate.Text = "updatetime";
            this.labelupdate.Click += new System.EventHandler(this.ListItem_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label2.Location = new System.Drawing.Point(316, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "下载量：";
            this.label2.Click += new System.EventHandler(this.ListItem_Click);
            // 
            // labeldownload
            // 
            this.labeldownload.AutoSize = true;
            this.labeldownload.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.labeldownload.Location = new System.Drawing.Point(375, 34);
            this.labeldownload.Name = "labeldownload";
            this.labeldownload.Size = new System.Drawing.Size(53, 12);
            this.labeldownload.TabIndex = 6;
            this.labeldownload.Text = "download";
            this.labeldownload.Click += new System.EventHandler(this.ListItem_Click);
            // 
            // pictureBoxInstalled
            // 
            this.pictureBoxInstalled.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxInstalled.Image")));
            this.pictureBoxInstalled.InitialImage = null;
            this.pictureBoxInstalled.Location = new System.Drawing.Point(480, 19);
            this.pictureBoxInstalled.Name = "pictureBoxInstalled";
            this.pictureBoxInstalled.Size = new System.Drawing.Size(40, 40);
            this.pictureBoxInstalled.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxInstalled.TabIndex = 7;
            this.pictureBoxInstalled.TabStop = false;
            // 
            // ListItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.pictureBoxInstalled);
            this.Controls.Add(this.labeldownload);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelupdate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labeldescription);
            this.Controls.Add(this.labeltitle);
            this.Controls.Add(this.pictureBoxThumb);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Name = "ListItem";
            this.Size = new System.Drawing.Size(523, 98);
            this.Click += new System.EventHandler(this.ListItem_Click);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ListItem_Paint);
            this.MouseEnter += new System.EventHandler(this.Listitem_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.Listitem_MouseLeave);
            this.MouseHover += new System.EventHandler(this.ListItem_MouseHover);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxThumb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxInstalled)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.PictureBox pictureBoxThumb;
        public System.Windows.Forms.Label labeltitle;
        public System.Windows.Forms.Label labeldescription;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label labelupdate;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label labeldownload;
        public System.Windows.Forms.PictureBox pictureBoxInstalled;
    }
}

using CurseFunction;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace CurseFetcher.item
{
    public partial class ListItem : UserControl
    {
        public int id;
        public CurseListItem item;
        public ListItem(CurseListItem it,int index)
        {
            item = it;
            InitializeComponent();
            if(null!=item.title)
                labeltitle.Text = item.title;
            if (null != item.description)
                labeldescription.Text = item.description;
            if (null != item.update)
                labelupdate.Text = item.update;
            if (null != item.download)
                labeldownload.Text = item.download;
            if (null != item.thumbnails)
            {
                //System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(this.updatePic));
               pictureBoxThumb.ImageLocation = item.thumbnails;
            }
               pictureBoxInstalled.Visible=it.isInstall;
            id = index;
        }


        /// <summary>
        /// 暂时这个代码没有启用
        /// </summary>
        private void UpdatePic()
        {
            this.BeginInvoke(new System.Threading.ThreadStart(delegate ()
            {
                string filename = this.item.thumbnails.Substring(item.thumbnails.LastIndexOf("/"));
                if (!Directory.Exists(@"\cache"))
                {
                    Directory.CreateDirectory(@"\cache");
                }
                string path = @"\cache\" + filename;
                if (!File.Exists(path))
                {
                    if (Curl.Download(item.thumbnails, path))
                    {
                        this.pictureBoxThumb.Load(path);
                    }
                }
            }));
        }

        public bool isSelected = false;
        private bool selectEnable = true;
        bool mouseHover = false;
        Color HoverBackgroundColor = Color.FromArgb(229, 243, 251);
        Color HoverBorderColor = Color.FromArgb(112, 192, 231);
        Color SelectedBackColor = Color.FromArgb(204, 255, 255);
        Color SelectedBorderColor = Color.FromArgb(204, 204, 255);
        Color NormalBackgroundColor = Color.Transparent;

        private void Listitem_MouseEnter(object sender, EventArgs e)
        {
            if (!isSelected)
            {
                this.mouseHover = true;
                this.BackColor = HoverBackgroundColor;
            }
           
        }

        private void Listitem_MouseLeave(object sender, EventArgs e)
        {
            if (!this.isSelected && this.GetChildAtPoint(this.PointToClient(MousePosition)) == null)
            {
                this.BackColor = NormalBackgroundColor;
            }
            this.mouseHover = false;
        }
        private void ListItem_MouseHover(object sender, EventArgs e)
        {
            if (!isSelected)
            {
                this.mouseHover = true;
                this.BackColor = HoverBackgroundColor;
            }
            
        }
        private void ListItem_Paint(object sender, PaintEventArgs e)
        {
            if (isSelected)
            {
                ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle, this.SelectedBorderColor, ButtonBorderStyle.Solid);
            }else if (mouseHover)
            {
                ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle, this.HoverBorderColor, ButtonBorderStyle.Solid);
            }
        }

        private IOnClickListener onClickListener;
        public interface IOnClickListener
        {
            void OnClick(int id);
        }
        public void SetOnClickListener(IOnClickListener listener)
        {
            this.onClickListener = listener;
        }
        public void SetClickEnable(bool status)
        {
            selectEnable = status;
            if (!isSelected)
            {
                this.Cursor = status ? Cursors.Hand : Cursors.WaitCursor;                
            }                
        }
        public void SetSelected(bool status = true)
        {
            this.isSelected = status;
            if (status)
            {
                this.Cursor = Cursors.No;
                this.BackColor = SelectedBackColor;
            }
            else
            {
                this.BackColor = NormalBackgroundColor;
                this.BorderStyle = BorderStyle.None;
            }
        }
        private void ListItem_Click(object sender, EventArgs e)
        {
            if (!this.selectEnable)
                return;
            if (this.onClickListener != null)
            {
                this.onClickListener.OnClick(this.id);
            }
        }
    }
}

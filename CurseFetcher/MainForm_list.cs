using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using CurseFunction;

namespace CurseFetcher
{
    public partial class MainForm : Form
    {
        private List<string> updateTask;
        private void InitGrid()
        {
            dataGridView1.DataSource = Settings.dt;
            AddGridHeader("名称", "title");
            AddGridHeader("版本", "local_version");
            AddGridHeader("更新时间", "update_time");
            AddGridHeader(null, "addon_id");
            FillData();
            updateTask = new List<string>();
        }
        private void AddGridHeader(string t, string n)
        {

            Settings.dt.Columns.Add(new DataColumn
            {
                ColumnName = n
            });
            if (null == t)
                dataGridView1.Columns[n].Visible = false;
            else
                dataGridView1.Columns[n].HeaderText = t;
        }
        /// <summary>
        /// 刷新内存中插件列表到DataGridView中
        /// </summary>
        public void FillData()
        {
            if (dataGridView1.InvokeRequired)
            {
                this.Invoke(new FillDataHandle(FillDataFunction));
            }
            else
            {
                FillDataFunction();
            }
        }
        
        private delegate void FillDataHandle();
        private void FillDataFunction()
        {
            Settings.dt.Clear();
            try
            {
              var list = Settings.db.GetAddonList();
                if (null != list)
                {
                    foreach (var li in list)
                    {
                        var row = Settings.dt.NewRow();
                        row["title"] = li.title;
                        row["local_version"] = Curse.ConvertStringToDateTime(li.local_version).ToShortDateString();
                        row["update_time"] = li.update_time;
                        row["addon_id"] = li.addon_id;
                        Settings.dt.Rows.Add(row);
                    }
                }
            }
            catch(Exception e)
            {
                Settings.LogException(e);
                ShowTrace("刷新插件列表失败！");
            }
        }
    }
}

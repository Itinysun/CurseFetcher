using CurseFunction;
using System;
using System.IO;
using System.Windows.Forms;

namespace CurseFetcher.item
{
    public partial class Config : Form
    {
        private SoftConfig sc;
        public Config()
        {
            InitializeComponent();
            sc = Settings.sc;
            if (string.IsNullOrEmpty(sc.device_id))
                sc.device_id = Settings.deviceId;
            if (string.IsNullOrEmpty(sc.device_name))
                sc.device_name = CommonFunction.GetMachineName();

            CbAutoQuit.Enabled = sc.auto_start;

            TbDevice_name.DataBindings.Add("Text", sc, "device_name",false,DataSourceUpdateMode.OnPropertyChanged);
            TbGamePath.DataBindings.Add("Text", sc, "game_path", false, DataSourceUpdateMode.OnPropertyChanged);

            CbProxyEnable.DataBindings.Add("Checked", sc, "proxy_enable", false, DataSourceUpdateMode.OnPropertyChanged);
            TbPassword.DataBindings.Add("Text", sc, "proxy_password", false, DataSourceUpdateMode.OnPropertyChanged);
            TbProxyUsername.DataBindings.Add("Text", sc, "proxy_username", false, DataSourceUpdateMode.OnPropertyChanged);
            TbProxyPort.DataBindings.Add("Text", sc, "proxy_port", false, DataSourceUpdateMode.OnPropertyChanged);
            TbPorxyIp.DataBindings.Add("Text", sc, "proxy_ip", false, DataSourceUpdateMode.OnPropertyChanged);

            CbAutostart.DataBindings.Add("Checked", sc, "auto_start", false, DataSourceUpdateMode.OnPropertyChanged);
            CbAutoQuit.DataBindings.Add("Checked", sc, "auto_quit", false, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void LinkLabel7niu_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Settings.OpenLink("https://portal.qiniu.com/signup?code=3l9qmxdee234i");
        }

        /// <summary>
        /// 如果配置错误，禁止关闭窗口（强制设置游戏路径）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Config_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (string.IsNullOrEmpty(sc.game_path) || !sc.game_path.Contains("_retail_"))
            {
                var res = MessageBox.Show("设定游戏路径非常重要，依然退出请点-是-，返回设定点-否-  \n\r注意8.1正确游戏路径应含有_retail_", "游戏路径未设定，确定退出？", MessageBoxButtons.YesNo);
                if (DialogResult.Yes== res)
                {
                    Settings.needQuit = true;
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        /// <summary>
        /// 当开始代理时，检测代理格式设置是否正确
        /// </summary>
        /// <returns></returns>
        private bool CheckProxy()
        {
            if (sc.proxy_enable)
            {
                if (!CommonFunction.IsRightIP(sc.proxy_ip))
                {
                    ShowError("代理服务器IP格式错误");
                    return false;
                }
                if (!CommonFunction.IsRightPort(sc.proxy_port))
                {
                    ShowError("代理服务器端口格式错误");
                    return false;
                }
            }
            return true;
        }

        private bool CheckCloud()
        {
            if(sc.device_auto_upload || sc.device_auto_download)
            {

            }
            return false;
        }

        private void BtSave_Click(object sender, EventArgs e)
        {
            if (CheckProxy())
            {
                Settings.sc = sc;
                Settings.db.SaveConfig(sc);
                this.Close();
            }
        }

        private void BtSelectPath_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            of.CheckFileExists = true;
            of.Title = "请选择Wow.exe所在目录";
            of.Multiselect = false;
            of.Filter = "Wow.exe|Wow.exe";
            of.FileName = "Wow.exe";
            of.InitialDirectory = Environment.ExpandEnvironmentVariables("%programfiles%");
            if (of.ShowDialog() == DialogResult.OK)
            {
                var filename = of.FileName;
                TbGamePath.Text = Path.GetDirectoryName(filename);
            }
        }

        private void CbAutostart_CheckedChanged(object sender, EventArgs e)
        {
            if (CbAutostart.Checked)
            {
                CbAutoQuit.Enabled = true;
                try
                {
                    CommonFunction.AddStartup("CurseFetcher", Application.ExecutablePath);
                }
                catch
                {
                    ShowError("尝试加入自动启动失败！请确保具有足够权限");
                }
                
            }
            else
            {
                CbAutoQuit.Enabled = false;
                try
                {
                    CommonFunction.RemoveStartup("CurseFetcher");
                }
                catch
                {
                    ShowError("尝试取消自动启动失败！请确保具有足够权限");
                }
                
            }
        }

        /// <summary>
        /// 显示错误提示信息
        /// </summary>
        /// <param name="msg"></param>
        private void ShowError(string msg)
        {
            MessageBox.Show(msg, "保存失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}

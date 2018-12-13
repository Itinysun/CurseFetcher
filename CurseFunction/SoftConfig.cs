using System.ComponentModel;

namespace CurseFunction
{
    public class SoftConfig : INotifyPropertyChanged
    {

        public int Id { get; set; }

        public string cachePath { get; set; }

        private bool _auto_start;
        public bool auto_start
        {
            get
            {
                return _auto_start;
            }
            set
            {
                _auto_start = value;
                SendChangeInfo("auto_start");
            }
        }

        private bool _auto_quit;
        public bool auto_quit
        {
            get
            {
                return _auto_quit;
            }
            set
            {
                _auto_quit = value;
                SendChangeInfo("auto_quit");
            }
        }

        private string _proxy_ip;
        public string proxy_ip
        {
            get
            {
                return _proxy_ip;
            }
            set
            {
                _proxy_ip = value.Trim();
                SendChangeInfo("proxy_ip");
            }
        }

        private string _proxy_port;
        public string proxy_port
        {
            get
            {
                return _proxy_port;
            }
            set
            {
                _proxy_port = value.Trim();
                SendChangeInfo("proxy_port");
            }
        }

        private string _proxy_username;
        public string proxy_username
        {
            get
            {
                return _proxy_username;
            }
            set
            {
                _proxy_username = value.Trim();
                SendChangeInfo("proxy_username");
            }
        }

        private string _proxy_password;
        public string proxy_password
        {
            get
            {
                return _proxy_password;
            }
            set
            {
                _proxy_password = value.Trim();
                SendChangeInfo("proxy_password");
            }
        }

        private bool _proxy_enable;
        public bool proxy_enable
        {
            get
            {
                return _proxy_enable;
            }
            set
            {
                _proxy_enable = value;
                SendChangeInfo("proxy_enable");
            }
        }

        public string device_id{get;set;}

        private string _device_name;
        public string device_name
        {
            get
            {
                return _device_name;
            }
            set
            {
                _device_name = value.Trim();
                SendChangeInfo("device_name");
            }
        }

        private bool _device_auto_upload;
        public bool device_auto_upload
        {
            get
            {
                return _device_auto_upload;
            }
            set
            {
                _device_auto_upload = value;
                SendChangeInfo("device_auto_upload");
            }
        }

        private bool _device_auto_download;
        public bool device_auto_download
        {
            get
            {
                return _device_auto_download;
            }
            set
            {
                _device_auto_download = value;
                SendChangeInfo("device_auto_download");
            }
        }

        private string _game_path;
        public string game_path
        {
            get
            {
                return _game_path;
            }
            set
            {
                _game_path = value.Trim();
                SendChangeInfo("game_path");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void SendChangeInfo(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CurseFetcher.item
{
    public partial class QQ : Form
    {
        public QQ()
        {
            InitializeComponent();
            richTextBox1.AppendText("很高兴大家来尝试这个又丑功能又少的小软件，本软件的宗旨就是功能简单，安全放心。\r\n");
            richTextBox1.AppendText("如果日后用的人多界面和功能都不是问题，这里留个群是为了喜欢用简单纯粹软件的人，收集一下大家的需求。\r\n");
            richTextBox1.AppendText("请注意:\r\n");
            richTextBox1.AppendText("不需要打赏，因为此软件不需要连接其他服务器，安全并且没有额外运行成本\r\n");
            richTextBox1.AppendText("打赏也没有地方给你展示，因为软件没有自动更新。不能为了你们的赞助就更新一个版本是不是？\r\n");
            richTextBox1.AppendText("需求不一定都会满足因为要保持软件的精简（懒：手动删除线，手动扇子娘）\r\n");
        }
    }
}

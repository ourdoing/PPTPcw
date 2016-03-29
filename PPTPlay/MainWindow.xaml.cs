using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Runtime.InteropServices;

namespace WpfApplication1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        #region 模拟键盘
        [DllImport("USER32.DLL")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);  //导入寻找windows窗体的方法
        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);  //导入为windows窗体设置焦点的方法
        [DllImport("USER32.DLL")]
        public static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);  //导入模拟键盘的方法
        #endregion
        public void sever()
        {
            int recv;
            byte[] data = new byte[1024];
            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 11751);
            Socket network = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            network.Bind(ipep);
            IPEndPoint send = new IPEndPoint(IPAddress.Any, 0);
            EndPoint Remote = (EndPoint)(send);
           // string ip = Remote.ToString();
            recv = network.ReceiveFrom(data, ref Remote);
            network.SendTo(data, data.Length, SocketFlags.None, Remote);
            string ip = Remote.ToString();
            string[] sp = ip.Split(':');
            while (true)
            {
                data = new byte[1024];
                recv = network.ReceiveFrom(data, ref Remote);
                string shuju = Encoding.ASCII.GetString(data, 0, recv);
                switch (shuju)
                {
                    case "next": Uppage();
                       textBox1.Dispatcher.Invoke(new Action(
                   delegate
                   {
                       textBox1.Text += "IP地址为：" + sp[0] + "," + "端口号为：" + sp[1] + "下一页" + "\r\n";
                   }
                    ));
                        break;

                    case "pre": Nextpage();
                        textBox1.Dispatcher.Invoke(new Action(
                  delegate
                  {
                      textBox1.Text+="IP地址为：" + sp[0] + "," + "端口号为：" + sp[1] + "上一页" + "\r\n";
                  }
                   ));
                        break;

                    default: break;
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Thread backthread = new Thread(sever);
            // 使线程成为一个后台线程
            //backthread.IsBackground = true;
            // 通过Start方法启动线程
            backthread.Start();
        }
        public void Uppage()
        {
            //按住上键
            keybd_event(0x28, 0, 0, 0);
            //松开上键
            keybd_event(0x28, 0, 2, 0);
            //MessageBox.Show("你说入的的是上键");
        }
        public void Nextpage()
        {
            //按住下键
            keybd_event(0x26, 0, 0, 0);
            //松开下键
            keybd_event(0x26, 0, 2, 0);
            //MessageBox.Show("你说入的的是下键");
        }
    }
}

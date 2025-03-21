using NinjaMagisk;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace NinjaMagiskWindowsTools
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            tabControl1.TabPages[tabControl1.SelectedIndex].Focus();
        }
        public int[] s = { 0, 0, 0 };//用来记录窗体是否打开过
        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            tabControl1.TabPages[tabControl1.SelectedIndex].Focus();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            verLabel.Text = Application.ProductVersion;
            this.Text += Application.ProductVersion;
            tabControl1.TabPages[tabControl1.SelectedIndex].Focus();
            object3d.SelectedItem = "是";
            desktop.SelectedItem = "是";
            doc.SelectedItem = "是";
            download.SelectedItem = "是";
            music.SelectedItem = "是";
            photo.SelectedItem = "是";
            video.SelectedItem = "是";
        }

        private void 操作ToolStripMenuItem_Click(object sender, EventArgs e) //开始执行
        {
            Windows.ThisPCFolders._3DObject(object3d.SelectedItem is "是" ? true : false);
            Windows.ThisPCFolders.Desktop(desktop.SelectedItem is "是" ? true : false);
            Windows.ThisPCFolders.Documents(doc.SelectedItem is "是" ? true : false);
            Windows.ThisPCFolders.Downloads(download.SelectedItem is "是" ? true : false);
            Windows.ThisPCFolders.Music(music.SelectedItem is "是" ? true : false);
            Windows.ThisPCFolders.Pictures(photo.SelectedItem is "是" ? true : false);
            Windows.ThisPCFolders.Videos(video.SelectedItem is "是" ? true : false);
            if (hidenfile.SelectedItem != null)
                Windows.ShowHiddenFile(hidenfile.SelectedItem is "显示" ? true : false);
            if (openfolder.SelectedItem != null)
                if (openfolder.SelectedItem is "此电脑")
                    Windows.ExplorerLaunchTo.ThisPC();
                else
                    Windows.ExplorerLaunchTo.QuickAcess();
            if (extension.SelectedItem != null)
                Windows.ShowFileExtension(extension.SelectedItem is "显示" ? true : false);
            if (defender.SelectedItem != null)
                if (defender.SelectedItem is "启用")
                    Windows.WindowsSecurityCenter.Enable();
                else
                    Windows.WindowsSecurityCenter.Disable();
        }
        private void 打开配置GToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 另存为配置HToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 退出TToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Windows.DesktopIconSettings.OpenDesktopIconSettings();
        }

        private void Button14_Click(object sender, EventArgs e) 
        {
            Process.Start("ncpa.cpl").Close(); //打开网络连接
        }

        private void Button13_Click(object sender, EventArgs e) //打开设备管理器
        {
            Process.Start("devmgmt.msc").Close();
        }

        private void Button12_Click(object sender, EventArgs e)
        {
            Process.Start("taskschd.msc").Close (); //打开任务计划程序
        }

        private void Button11_Click(object sender, EventArgs e)
        {
            string firewallPath;
            if (!Environment.Is64BitProcess)
            {
                firewallPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.Windows)}\\Sysnative\\Firewall.cpl";
            }
            else
            {
                firewallPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.System)}\\Firewall.cpl";
            }
            Process.Start(firewallPath).Close(); //打开防火墙
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            Process.Start("control.exe", "powercfg.cpl").Close(); //打开电源选项
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            Process.Start("control.exe","system").Close(); //打开系统属性
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            Process.Start("UserAccountControlSettings").Close(); //打开用户账户控制设置
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            string features;
            if (!Environment.Is64BitProcess)
            {
                features = $"{Environment.GetFolderPath(Environment.SpecialFolder.Windows)}\\Sysnative\\optionalfeatures.exe";
            }
            else
            {
                features = $"{Environment.GetFolderPath(Environment.SpecialFolder.System)}\\optionalfeatures.exe";
            }
            Process.Start(features).Close(); //打开Windows功能

        }

        private void Button6_Click(object sender, EventArgs e)
        {
            Process.Start("appwiz.cpl").Close(); //打开程序和功能
        }

        private void Button15_Click(object sender, EventArgs e) //在Github上检查更新
        {


        }
    }
}

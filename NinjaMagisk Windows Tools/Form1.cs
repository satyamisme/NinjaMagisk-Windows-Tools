using NinjaMagisk;
using System;
using System.Diagnostics;
using System.Security.Principal;
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
        public int[] s = { 0, 0, 0 }; //Used to record whether the form has been opened
        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            tabControl1.TabPages[tabControl1.SelectedIndex].Focus();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string cfgPath = $"{Application.StartupPath}\\config.ini";
            if (!System.IO.File.Exists(cfgPath))
            {
                System.IO.File.Create(cfgPath).Close();
            }
            // Get the current Windows identity
            WindowsIdentity identity = WindowsIdentity.GetCurrent();

            // Get the current subject
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            privileges.Text = identity.Name + "\n";
            if (principal.IsInRole(WindowsBuiltInRole.Administrator))
            {
                privileges.Text += "Administrator\n";
                SaveConfigOnCurrentUser();
            }

            // Build permission information string
            if (identity.IsSystem)
                privileges.Text += "System Account\n";
            if (identity.IsAnonymous)
                privileges.Text += "Anonymous account\n";
            if (identity.IsGuest)
                privileges.Text += "Guest Account\n";
            //// string permissionInfo = $"Username: {identity.Name}\n" +
            // $"Authentication type: {identity.AuthenticationType}\n" +
            // $"Is it an administrator: {principal.IsInRole(WindowsBuiltInRole.Administrator)}\n" +
            // $"Is it a system account: {}\n" +
            // $"Is it an anonymous account: {}\n" +
            // $"Is it a guest account: {}\n" +
            // $"token: {identity.Token}";
            verLabel.Text = Application.ProductVersion;
            this.Text += Application.ProductVersion;
            tabControl1.TabPages[tabControl1.SelectedIndex].Focus();
            object3d.SelectedItem = "是";
            desktop.SelectedItem = "Yes";
            doc.SelectedItem = "Yes";
            download.SelectedItem = "是";
            music.SelectedItem = "Yes";
            photo.SelectedItem = "Yes";
            video.SelectedItem = "Yes";

        }
        private void SaveConfigOnCurrentUser()
        {

        }
        private void operation ToolStripMenuItem_Click(object sender, EventArgs e) //Start execution
        {
            Windows.Explorer.ThisPCFolders._3DObject(object3d.SelectedItem is "是" ? true : false);
            Windows.Explorer.ThisPCFolders.Desktop(desktop.SelectedItem is "是" ? true : false);
            Windows.Explorer.ThisPCFolders.Documents(doc.SelectedItem is "是" ? true : false);
            Windows.Explorer.ThisPCFolders.Downloads(download.SelectedItem is "是" ? true : false);
            Windows.Explorer.ThisPCFolders.Music(music.SelectedItem is "是" ? true : false);
            Windows.Explorer.ThisPCFolders.Pictures(photo.SelectedItem is "是" ? true : false);
            Windows.Explorer.ThisPCFolders.Videos(video.SelectedItem is "是" ? true : false);
            if (hidenfile.SelectedItem != null)
                Windows.Explorer.ShowHiddenFile(hidenfile.SelectedItem is "显示" ? true : false);
            if (openfolder.SelectedItem != null)
                if (openfolder.SelectedItem is "This Computer")
                    Windows.Explorer.ExplorerLaunchTo.ThisPC();
                else
                    Windows.Explorer.ExplorerLaunchTo.QuickAcess();
            if (extension.SelectedItem != null)
                Windows.Explorer.ShowFileExtension(extension.SelectedItem is "显示" ? true : false);
            if (defender.SelectedItem != null)
                if (defender.SelectedItem is "Enable")
                    Windows.WindowsSecurityCenter.Enable();
                else
                    Windows.WindowsSecurityCenter.Disable();
        }
        private void open configuration GToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                FileName = "config.ini",
                DefaultExt = "ini", // default extension
                Filter = "Configuration Information (*.ini)|*.ini",
                InitialDirectory = Application.StartupPath,
                RestoreDirectory = true,
            };
            openFileDialog.ShowDialog();
            string openedfilepath = openFileDialog.FileName;
            string D3DSCACHE = NinjaMagisk.Text.Config.ReadConfig(openedfilepath, "Folder.D3DSCACHE");
            string DXCACHE = NinjaMagisk.Text.Config.ReadConfig(openedfilepath, "Folder.NVIDIA.DXCACHE");
            string Temp = NinjaMagisk.Text.Config.ReadConfig(openedfilepath, "Folder.Temp");
            string cef89 = NinjaMagisk.Text.Config.ReadConfig(openedfilepath, "Folder.KuGou.CEFCACHE89");
            string ext = NinjaMagisk.Text.Config.ReadConfig(openedfilepath, "Setting.EXT.Show");
            string hiden = NinjaMagisk.Text.Config.ReadConfig(openedfilepath, "Setting.Hiden.Show");
            string folder = NinjaMagisk.Text.Config.ReadConfig(openedfilepath, "Setting.Folder.Default");
            string object3d = NinjaMagisk.Text.Config.ReadConfig(openedfilepath, "Setting.ThisPC.3D-Object");
            string video = NinjaMagisk.Text.Config.ReadConfig(openedfilepath, "Setting.ThisPC.Video");
            string music = NinjaMagisk.Text.Config.ReadConfig(openedfilepath, "Setting.ThisPC.Music");
            string photo = NinjaMagisk.Text.Config.ReadConfig(openedfilepath, "Setting.ThisPC.Photo");
            string doc = NinjaMagisk.Text.Config.ReadConfig(openedfilepath, "Setting.ThisPC.Documents");
            string download = NinjaMagisk.Text.Config.ReadConfig(openedfilepath, "Setting.ThisPC.Download");
            string desktop = NinjaMagisk.Text.Config.ReadConfig(openedfilepath, "Setting.ThisPC.Desktop");
            string defender = NinjaMagisk.Text.Config.ReadConfig(openedfilepath, "Setting.Windows.WindowsDefender");
            string update = NinjaMagisk.Text.Config.ReadConfig(openedfilepath, "Setting.Windows.WindowsUpdate");
        }
        private void save configuration JToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BothConfig($"{Application.StartupPath}\\config.ini"); //Save configuration
        }
        private void BothConfig(string savedPath)
        {
            NinjaMagisk.LogLibraries.ClearFile(savedPath);
            NinjaMagisk.Text.Config.WriteConfig(savedPath, "Folder.D3DSCACHE", $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\D3DSCache");
            NinjaMagisk.Text.Config.WriteConfig(savedPath, "Folder.NVIDIA.DXCACHE", $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\NVIDIA\\DXCache");
            NinjaMagisk.Text.Config.WriteConfig(savedPath, "Folder.NVIDIA.Installler2", $"{Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)}\\NVIDIA\\Installer2");
            NinjaMagisk.Text.Config.WriteConfig(savedPath, "Folder.Temp", $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\Temp");
            NinjaMagisk.Text.Config.WriteConfig(savedPath, "Folder.KuGou.CEFCACHE89", $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\KuGou8\\CefCache89");
            if (extension.SelectedItem == null)
            {
            }
            else
            {
                NinjaMagisk.Text.Config.WriteConfig(savedPath, "Setting.EXT.Show", (bool)extension.SelectedItem ? "true" : "false");
            }

            if (hidenfile.SelectedItem == null)
            {
            }
            else
            {
                NinjaMagisk.Text.Config.WriteConfig(savedPath, "Setting.Hiden.Show", (bool)hidenfile.SelectedItem ? "true" : "false");
            }

            if (openfolder.SelectedItem == null)
            {
            }
            else
            {
                NinjaMagisk.Text.Config.WriteConfig(savedPath, "Setting.Folder.Default", (bool)openfolder.SelectedItem ? "PC" : "QUICKLY");
            }
            NinjaMagisk.Text.Config.WriteConfig(savedPath, "Setting.ThisPC.3D-Object", object3d.SelectedItem is "是" ? "true" : "false");
            NinjaMagisk.Text.Config.WriteConfig(savedPath, "Setting.ThisPC.Video", video.SelectedItem is "是" ? "true" : "false");
            NinjaMagisk.Text.Config.WriteConfig(savedPath, "Setting.ThisPC.Music", music.SelectedItem is "是" ? "true" : "false");
            NinjaMagisk.Text.Config.WriteConfig(savedPath, "Setting.ThisPC.Photo", photo.SelectedItem is "是" ? "true" : "false");
            NinjaMagisk.Text.Config.WriteConfig(savedPath, "Setting.ThisPC.Documents", doc.SelectedItem is "是" ? "true" : "false");
            NinjaMagisk.Text.Config.WriteConfig(savedPath, "Setting.ThisPC.Download", download.SelectedItem is "是" ? "true" : "false");
            NinjaMagisk.Text.Config.WriteConfig(savedPath, "Setting.ThisPC.Desktop", desktop.SelectedItem is "是" ? "true" : "false");
            if (defender.SelectedItem == null)
            {
            }
            else
            {
                NinjaMagisk.Text.Config.WriteConfig(savedPath, "Setting.Windows.WindowsDefender", defender.SelectedItem is "启用" ? "true" : "false");
            }

            if (update.SelectedItem == null)
            {
            }
            else
            {
                NinjaMagisk.Text.Config.WriteConfig(savedPath, "Setting.Windows.WindowsUpdate", update.SelectedItem is "启用" ? "true" : "false");
            }

        }
        private void Save as configuration HToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                FileName = "config.ini",
                DefaultExt = "ini", // default extension
                OverwritePrompt = true, // prompt whether to overwrite
                Filter = "Configuration Information (*.ini)|*.ini",
                InitialDirectory = Application.StartupPath,
                RestoreDirectory = true,
            };
            saveFileDialog.ShowDialog();
            BothConfig(saveFileDialog.FileName);
        }

        private void 退出TToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Windows.Explorer.DesktopIconSettings.OpenDesktopIconSettings();
        }

        private void Button14_Click(object sender, EventArgs e)
        {
            Process.Start("ncpa.cpl").Close(); //Open the network connection
        }

        private void Button13_Click(object sender, EventArgs e) //Open the device manager
        {
            Process.Start("devmgmt.msc").Close();
        }

        private void Button12_Click(object sender, EventArgs e)
        {
            Process.Start("taskschd.msc").Close(); //Open the task scheduler
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
            Process.Start(firewallPath).Close(); //Open the firewall
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            Process.Start("control.exe", "powercfg.cpl").Close(); //Open power options
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            Process.Start("control.exe", "system").Close(); //Open system properties
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            Process.Start("UserAccountControlSettings").Close(); //Open User Account Control Settings
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
            Process.Start(features).Close(); //Open Windows features

        }

        private void Button6_Click(object sender, EventArgs e)
        {
            Process.Start("appwiz.cpl").Close(); //Open programs and functions
        }

        private void Button15_Click(object sender, EventArgs e) //Check for updates on Github
        {
            bool v = NinjaMagisk.Network.Ping("api.github.com");
            if (!v)
            {
                MessageBox.Show("Network connection abnormality");
                return;
            }
            var info = NinjaMagisk.Update.GetUpdateJson("https://api.github.com/repos/Rainbow-SPY/NinjaMagisk-Windows-Tools/releases/latest", NinjaMagisk.Update.Platform.Github);
            string[] strings = info.Split(';');
            string TagName = strings[0];
            string Name = strings[1];
            MessageBox.Show(TagName + "\n\n" + Name);

        }

        private void Button14_Click_1(object sender, EventArgs e)
        {
            Process.Start("https://github.com/Rainbow-SPY/NinjaMagisk-Windows-Tools");
        }

        private void Extension_Click(object sender, EventArgs e)
        {
            label6.Visible = true;
        }

        private void Hidenfile_Click(object sender, EventArgs e)
        {
            label21.Visible = true;
        }

        private void Object3d_Click(object sender, EventArgs e)
        {
            label22.Visible = true;
        }

        private void Defender_SelectionChangeCommitted(object sender, EventArgs e)
        {
            label7.Visible = true;
        }

        private void Undate_SelectedIndexChanged(object sender, EventArgs e)
        {
            label22.Visible = true;
        }

        private void Button19_Click(object sender, EventArgs e) //打开 NVIDIA Installer2
        {
            if (!System.IO.File.Exists($"{Application.StartupPath}\\config.ini"))
            {
                Windows.Explorer.OpenFolderInExplorer($"{Environment.GetEnvironmentVariable("ProgramW6432")}\\NVIDIA Corporation\\Installer2");
            }
            else
            {
                MessageBox.Show($"{Environment.GetEnvironmentVariable("ProgramW6432")}\\NVIDIA Corporation\\Installer2");
                string Installer2 = NinjaMagisk.Text.Config.ReadConfig($"{Application.StartupPath}\\config.ini", "Folder.NVIDIA.Installler2");
                if (Installer2 == null)
                    Windows.Explorer.OpenFolderInExplorer($"{Environment.GetEnvironmentVariable("ProgramW6432")}\\NVIDIA Corporation\\Installer2");
                else
                    Windows.Explorer.OpenFolderInExplorer(Installer2);
            }
        }
        private void Button16_Click(object sender, EventArgs e)
        {
            if (!System.IO.File.Exists($"{Application.StartupPath}\\config.ini"))
            {
                Windows.Explorer.OpenFolderInExplorer($"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\D3DSCache");
            }
            else
            {
                string D3DSCACHE = NinjaMagisk.Text.Config.ReadConfig($"{Application.StartupPath}\\config.ini", "Folder.D3DSCACHE");
                if (D3DSCACHE == null)
                    Windows.Explorer.OpenFolderInExplorer($"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\D3DSCache");
                else
                    Windows.Explorer.OpenFolderInExplorer(D3DSCACHE);
            }
        }
        private void Button17_Click(object sender, EventArgs e)
        {
            if (!System.IO.File.Exists($"{Application.StartupPath}\\config.ini"))
            {
                Windows.Explorer.OpenFolderInExplorer($"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\NVIDIA\\DXCache");
            }
            else
            {
                string DXCACHE = NinjaMagisk.Text.Config.ReadConfig($"{Application.StartupPath}\\config.ini", "Folder.NVIDIA.DXCACHE");
                if (DXCACHE == null)
                    Windows.Explorer.OpenFolderInExplorer($"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\NVIDIA\\DXCache");
                else
                    Windows.Explorer.OpenFolderInExplorer(DXCACHE);
            }
        }
        private void Button18_Click(object sender, EventArgs e)
        {
            if (!System.IO.File.Exists($"{Application.StartupPath}\\config.ini"))
            {
                Windows.Explorer.OpenFolderInExplorer($"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\Temp");
            }
            else
            {
                string Temp = NinjaMagisk.Text.Config.ReadConfig($"{Application.StartupPath}\\config.ini", "Folder.Temp");
                if (Temp == null)
                    Windows.Explorer.OpenFolderInExplorer($"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\Temp");
                else
                    Windows.Explorer.OpenFolderInExplorer(Temp);
            }
        }
        private void Button21_Click(object sender, EventArgs e)
        {
            if (!System.IO.File.Exists($"{Application.StartupPath}\\config.ini"))
            {
                Windows.Explorer.OpenFolderInExplorer($"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\KuGou8\\CefCache89");
            }
            else
            {
                string cef89 = NinjaMagisk.Text.Config.ReadConfig($"{Application.StartupPath}\\config.ini", "Folder.KuGou.CEFCACHE89");
                if (cef89 == null)
                    Windows.Explorer.OpenFolderInExplorer($"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\KuGou8\\CefCache89");
                else
                    Windows.Explorer.OpenFolderInExplorer(cef89);
            }
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            Process.Start("services.msc").Dispose();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Process.Start("diskmgmt.msc").Dispose();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            Process.Start("lusrmgr.msc").Dispose();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Process.Start("gpedit.msc").Dispose();
        }
        private void Taskschd_Click(object sender, EventArgs e)
        {
            Process.Start("taskschd.msc").Dispose();
        }

    }
}

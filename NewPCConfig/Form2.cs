using NinjaMagisk;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using static NinjaMagisk.LogLibraries;
using File = System.IO.File;

namespace NewPCConfig
{
    public partial class Form2 : Form
    {
        private readonly string _iniFilePath;
        public Form2(string iniPath)
        {
            InitializeComponent();
            _iniFilePath = iniPath;
            LoadIniFile();
        }
        #region LogLibraries
        public void LogDialog(LogLevel logLevel, string message)
        {
            //richTextBox1.Text += $"{DateTime.Now:HH:mm:ss} [{logLevel}]: {message}\n";
            WriteLog(logLevel, message);
        }
        public void LogDialog(LogLevel logLevel, LogKind logKind, string message)
        {
            //richTextBox1.Text += $"{DateTime.Now:HH:mm:ss} [{_logKind}] [{_logLevel}]: {_message}\n";
            LogLibraries.WriteLog(logLevel, logKind, message);
        }

        #endregion
        private void LoadIniFile()
        {
            if (File.Exists(_iniFilePath))
            {
                try
                {
                    var lines = File.ReadAllLines(_iniFilePath);
                    foreach (var line in lines)
                    {
                        var parts = line.Split('=');
                        if (parts.Length < 2)
                        {
                            MessageBox.Show($"无效的行: {line}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            continue; // 跳过无效行
                        }
                        string value = parts[1].Trim(); // 去除多余的空格

                        if (line.StartsWith("defender="))
                        {
                            switch (value)
                            {
                                case "Enable":
                                    NinjaMagisk.Windows.WindowsSecurityCenter.Enable();
                                    break;
                                case "Disable":
                                    NinjaMagisk.Windows.WindowsSecurityCenter.Disable();
                                    break;
                                default:
                                    MessageBox.Show("Invalid value", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    break;
                            }
                        }
                        else if (line.StartsWith("extension="))
                        {
                            HandleSetting(value, NinjaMagisk.Windows.ShowFileExtension, "文件扩展名");
                        }
                        else if (line.StartsWith("hidefile"))
                        {
                            HandleSetting(value, NinjaMagisk.Windows.ShowHiddenFile, "隐藏文件");
                        }
                        else if (line.StartsWith("thispc"))
                        {
                            HandleSetting(value, NinjaMagisk.Windows.AddDesktopLink.AddThisPC, "此电脑");
                        }
                        else if (line.StartsWith("user"))
                        {
                            HandleSetting(value, NinjaMagisk.Windows.AddDesktopLink.AddUserFolder, "个人文件夹");
                        }
                        else if (line.StartsWith("net"))
                        {
                            HandleSetting(value, NinjaMagisk.Windows.AddDesktopLink.AddInternet, "网络");
                        }
                        else if (line.StartsWith("control"))
                        {
                            HandleSetting(value, NinjaMagisk.Windows.AddDesktopLink.AddControlPan, "控制面板");
                        }
                        else if (line.StartsWith("openfolder"))
                        {
                            switch (value)
                            {
                                case "PC":
                                    NinjaMagisk.Windows.ExplorerLaunchTo.ThisPC();
                                    break;
                                case "QAc":
                                    NinjaMagisk.Windows.ExplorerLaunchTo.QuickAcess();
                                    break;
                                default:
                                    MessageBox.Show($"无效的打开文件夹设置值: {value}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    break;
                            }
                        }
                        // 添加更多 ComboBox 的处理逻辑
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"读取文件失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("INI 文件未找到！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // 处理设置的通用方法
        private void HandleSetting(string value, Action<bool> action, string settingName)
        {
            switch (value)
            {
                case "Show":
                    action(true);
                    break;
                case "Hide":
                    action(false);
                    break;
                default:
                    MessageBox.Show($"无效的 {settingName} 值: {value}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            LogLibraries.LogToUi = (logLevel, message) =>
            {
                // 确保在 UI 线程中更新 RichTextBox
                if (richTextBox1.InvokeRequired)
                {
                    richTextBox1.Invoke(new Action(() =>
                    {
                        richTextBox1.AppendText($"{DateTime.Now:HH:mm:ss} [{logLevel}]: {message}\n");
                    }));
                }
                else
                {
                    richTextBox1.AppendText($"{DateTime.Now:HH:mm:ss} [{logLevel}]: {message}\n");
                }
            };
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            Hide();
        }

        private void RichTextBox1_TextChanged(object sender, EventArgs e)
        {
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.ScrollToCaret();
        }
    }
}

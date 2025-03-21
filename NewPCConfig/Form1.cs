using System;
using System.IO;
using System.Windows.Forms;

namespace NewPCConfig
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            comboBox1.Text = "启用"; //defender
            comboBox2.Text = "隐藏"; //extension
            comboBox3.Text = "显示"; //hidefile
            comboBox4.Text = "隐藏"; //thispc
            comboBox5.Text = "隐藏"; //user
            comboBox6.Text = "隐藏"; //net
            comboBox7.Text = "隐藏"; //control
            comboBox8.Text = "最近访问"; //openfolder

        }

        private void 开始执行F5ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("您确定要开始执行操作吗?", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.No)
                return;

            Hide();
            // 定义临时文件路径
            string tempFilePath = Path.GetTempFileName(); // 创建一个临时文件
            tempFilePath = Path.ChangeExtension(tempFilePath, ".ini"); // 将文件扩展名改为 .ini

            NinjaMagisk.Text.Config.WriteConfig(tempFilePath, "defender", $"{(comboBox1.SelectedItem.ToString() == "启用" ? "Enable" : "Disable")}");
            NinjaMagisk.Text.Config.WriteConfig(tempFilePath, "extension", $"{(comboBox2.SelectedItem.ToString() == "显示" ? "Show" : "Hide")}");
            NinjaMagisk.Text.Config.WriteConfig(tempFilePath, "hidefile", $"{(comboBox3.SelectedItem.ToString() == "显示" ? "Show" : "Hide")}");
            NinjaMagisk.Text.Config.WriteConfig(tempFilePath, "thispc", $"{(comboBox4.SelectedItem.ToString() == "显示" ? "Show" : "Hide")}");
            NinjaMagisk.Text.Config.WriteConfig(tempFilePath, "user", $"{(comboBox5.SelectedItem.ToString() == "显示" ? "Show" : "Hide")}");
            NinjaMagisk.Text.Config.WriteConfig(tempFilePath, "net", $"{(comboBox6.SelectedItem.ToString() == "显示" ? "Show" : "Hide")}");
            NinjaMagisk.Text.Config.WriteConfig(tempFilePath, "control", $"{(comboBox7.SelectedItem.ToString() == "显示" ? "Show" : "Hide")}");
            NinjaMagisk.Text.Config.WriteConfig(tempFilePath, "openfolder", $"{(comboBox8.SelectedItem.ToString() == "此电脑" ? "PC" : "QAc")}");
            Form2 form2 = new Form2(tempFilePath);
            form2.Show();

            // 写入 ComboBox 的值到 .ini 文件
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace SupermarketSimulatorSaveChanger
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }
        public static void OpenExplorerAtPath(string path)
        {
            if (System.IO.Directory.Exists(path))
            {
                Process.Start("explorer.exe", path);
            }
            else
            {
                MessageBox.Show($"This path don't exists:\n{path}");
            }
        }

        // Dialog buttons
        private void B_OpenCloud_Click(object sender, EventArgs e) // Cloud Path
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                cloudPath.Text = folderBrowserDialog1.SelectedPath;
                Form1.overWriteConfig("CloudPath", cloudPath.Text);
            }
        }
        private void B_userProfile_Click(object sender, EventArgs e) //UserProfile
        {

            folderBrowserDialog1.SelectedPath = Form1.DriveLetter + @":\Users\";
            DialogResult folder = folderBrowserDialog1.ShowDialog();

            if (folder == DialogResult.OK)
            {
                string[] path = folderBrowserDialog1.SelectedPath.Split('\\');
                string _userName = path[path.Length - 1];
                userProfile.Text = _userName;
                Form1.overWriteConfig("ProfileName", _userName);
                Form1.username = _userName;
                Form1.localLowFolderPath = $@"{Form1.DriveLetter}:\Users\{Form1.username}\AppData\LocalLow";
                Form1.sForm.config();
                if (!Directory.Exists(Form1.SaveFiles()))
                {
                    MessageBox.Show("Game save folder not exists.\nPlease start new game on selected profile");
                    return;
                }

            }

        }

        private void B_OpenFile_Click(object sender, EventArgs e) // Game .exe Path
        {
            openFileDialog1.InitialDirectory = "C:";
            openFileDialog1.Filter = "App|*.exe";
            openFileDialog1.FileName = "Supermarket Simulator.exe";
            DialogResult file = openFileDialog1.ShowDialog();
            if (file == DialogResult.OK)
            {
                exePath.Text = openFileDialog1.FileName;
                Form1.overWriteConfig("GamePath", openFileDialog1.FileName);
            }
        }

        // ------------------------------

        // Folder buttons
        private void B_openLocalFolder_Click(object sender, EventArgs e) // Profile Save's Folder
        {
            OpenExplorerAtPath(Form1.SaveFiles());
        }

        private void B_openCloudFolder_Click(object sender, EventArgs e) // Cloud Save's Folder
        {
            if (cloudPath.Text != "")
            {
                OpenExplorerAtPath(cloudPath.Text);
            }
            else
            {
                MessageBox.Show("Cloud folder path is empty or not existed");
            }
        }

        private void B_configFolder_Click(object sender, EventArgs e) // Config File Path
        {
            OpenExplorerAtPath(Form1.cfgFolder);
        }

        // ------------------------------




      
    }
}

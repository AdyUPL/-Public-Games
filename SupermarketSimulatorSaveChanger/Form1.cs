using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SupermarketSimulatorSaveChanger
{
    public partial class Form1 : Form
    {

        static string username = Environment.UserName;
        static string DriveLetter = Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System)).Substring(0, 1);
        static string localLowFolderPath = $@"{DriveLetter}:\Users\{username}\AppData\LocalLow";
        static string cfgFolder = $@"{DriveLetter}:\Users\{username}\AppData\LocalLow\SupermarketSimulator_SaveManager";
        static string cfgFile = cfgFolder + @"\config.txt";




        public Form1()
        {
            InitializeComponent();
            config();
        }
        void config()
        {
            if (!Directory.Exists(cfgFolder))
            {
                Directory.CreateDirectory(cfgFolder);
            }
            if (!File.Exists(cfgFile))
            {
                using (StreamWriter sw = new StreamWriter(cfgFile, false))
                {
                    sw.WriteLine("[LastLoad]:");
                    for (int i = 1; i <= 5; i++)
                    {
                        sw.WriteLine("[" + i + "]:");
                    }
                }
            }
            else
            {
                using (StreamReader sr = new StreamReader(cfgFile))
                {
                    string line;
                    int i = 0;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] parts = line.Split(':');
                        if (parts.Length == 2)
                        {
                            if (i == 0) {
                                if (parts[1].Trim() != "")
                                {
                                    loadedSave.Text = "Loaded slot: " + parts[1].Trim();
                                }
                                else
                                {
                                    loadedSave.Text = "Loaded slot: none";
                                }
                            }
                            Label label = this.Controls.Find("label" + i, true).FirstOrDefault() as Label;
                            if (label != null)
                            {
                                if (parts[1].Trim() != "") {
                                    label.Text = parts[1].Trim();
                                }
                            }
                        }
                        i++;
                    }
                }
            }
        }

        public string SaveFiles(int type)
        {

            string DriveLetter = Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System)).Substring(0, 1);
            string localLowFolderPath = $@"{DriveLetter}:\Users\{username}\AppData\LocalLow";

            if (type == 1) {  // return Save file path
                localLowFolderPath = localLowFolderPath + @"\Nokta Games\Supermarket Simulator\SaveFile.es3";
            }
            else // return Save folder path
            {
                localLowFolderPath = localLowFolderPath + @"\Nokta Games\Supermarket Simulator";
            }

            return localLowFolderPath;
        }

        void overWriteConfig(int btn)
        {
            Form2 dialog = new Form2("Save slot", btn);
            DialogResult dialogResult = dialog.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                string enteredName = dialog.newName;
                string[] lines = File.ReadAllLines(cfgFile);

                int lineIndexToOverride = FindLineIndexWithSection(lines, $"[{btn}]:");
                if (lineIndexToOverride >= 0 && lineIndexToOverride < lines.Length)
                {
                    lines[lineIndexToOverride] = $"[{btn}]:" + enteredName;
                }
                else
                {
                    MessageBox.Show("Config file is corrupted");
                }

                File.WriteAllLines(cfgFile, lines);
            }
        }
        static int FindLineIndexWithSection(string[] lines, string section)
        {
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].StartsWith(section))
                {
                    return i;
                }
            }
            return -1;
        }

        public void Action(string type,int btn)
        {
            // TYPES:
            // S - Save
            // L - Load
            // R - Rename
            string savenum = btn.ToString();
            string path= SaveFiles(0) + @"\" + savenum;

            if (type == "S") {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                overWriteConfig(btn);

                File.Copy(SaveFiles(1), Path.Combine(path, Path.GetFileName(SaveFiles(1))),true);
                MessageBox.Show("Complete");
            }

            if (type == "L"){
                if (Directory.Exists(path))
                {
                   File.Copy(Path.Combine(path, Path.GetFileName(SaveFiles(1))), SaveFiles(1),true);
                    string[] lines = File.ReadAllLines(cfgFile);

                    if (lines.Length > 0)
                    {
                        string newLine = $"[LastLoad]: {btn.ToString()}";
                        lines[0] = newLine;
                    }

                    File.WriteAllLines(cfgFile, lines);
                    MessageBox.Show("Complete");
                }
                else
                {
                    MessageBox.Show("Save is empty");
                }
                
            }

            if (type == "R"){
                overWriteConfig(btn);
                MessageBox.Show("Complete");
            }

            config();

        }

        private void btn_Click(object sender, EventArgs e)
        {
           Button btn = sender as Button;
           string type = btn.Name.Substring(4,1);
           int num = int.Parse(btn.Name.Substring(btn.Name.Length - 1));
           Action(type, num);

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }

}

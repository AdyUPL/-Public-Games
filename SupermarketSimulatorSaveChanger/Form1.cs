using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;

namespace SupermarketSimulatorSaveChanger
{
    public partial class Form1 : Form
    {

        static string username = Environment.UserName;
        static string DriveLetter = Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System)).Substring(0, 1);
        static string localLowFolderPath = $@"{DriveLetter}:\Users\{username}\AppData\LocalLow";
        static string cfgFolder = $@"{DriveLetter}:\Users\{username}\AppData\LocalLow\SupermarketSimulator_SaveManager";
        static string cfgFile = cfgFolder + @"\config.txt";

        static int currentSlot = 0;


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
                    sw.WriteLine("[GamePath]:");
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
                        
                        if (i == 0)
                        {
                            if (parts[1].Trim() != "")
                            {
                                loadedSave.Text = "Loaded slot: " + parts[1].Trim();
                                currentSlot = Int32.Parse(parts[1].Trim());

                            }
                            else
                            {
                                loadedSave.Text = "Loaded slot: none";
                            }
                        }

                        if (i == 1 && parts[1].Trim()!="" && parts[2].Trim()!="")
                        {
                            textBox1.Text = parts[1].Trim()+":"+parts[2].Trim(); //TU NIE DZIALA
                        }

                        if (parts.Length == 2)
                        {
                            
                            if (i >= 2) { 
                            Label label = this.Controls.Find("label" + (i-1), true).FirstOrDefault() as Label;
                            if (label != null)
                            {
                                if (parts[1].Trim() != "") {
                                    label.Text = parts[1].Trim();
                                }
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

        void overWriteConfig(string text,int btn,int type)
        {
            if( type == 0) { 
            Form2 dialog = new Form2(text, btn);
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
            if (type == 1)
            {
                string[] lines = File.ReadAllLines(cfgFile);
                int lineIndexToOverride = FindLineIndexWithSection(lines, $"[GamePath]:");
                if (lineIndexToOverride >= 0 && lineIndexToOverride < lines.Length)
                {
                    lines[lineIndexToOverride] = $"[GamePath]:" + text;
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

        public void Action(string type,int btn,bool overwrite)
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

                File.Copy(SaveFiles(1), Path.Combine(path, Path.GetFileName(SaveFiles(1))), true);
               
                if (overwrite) { 
                    overWriteConfig("Save slot: ", btn,0);
                    MessageBox.Show("Complete");
                }

               
                
            }

            if (type == "L"){
                if (Directory.Exists(path))
                {
                    // Save old
                    Action("S", currentSlot,false);

                    // Load new
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
                if (textBox1.Text != "")
                {
                    Process.Start(textBox1.Text);
                }
                
            }

            if (type == "R"){
                overWriteConfig("Rename slot: ",btn, 0);
                MessageBox.Show("Complete");
            }

            config();

        }

        private void btn_Click(object sender, EventArgs e)
        {
           Button btn = sender as Button;
           string type = btn.Name.Substring(4,1);
           int num = int.Parse(btn.Name.Substring(btn.Name.Length - 1));
           Action(type, num, true);

        }

        private void btn_OpenFile_Click(object sender, EventArgs e)
        {
            //OpenFileDialog ofd = new OpenFileDialog();
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                overWriteConfig(openFileDialog1.FileName, 0, 1);
                textBox1.Text = openFileDialog1.FileName;
            }
        }
    }

}

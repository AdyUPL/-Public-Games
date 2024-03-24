using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;

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
                    sw.WriteLine("[CloudPath]:");
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
                            textBox1.Text = parts[1].Trim()+":"+parts[2].Trim();
                        }

                        if (parts.Length == 2)
                        {
                            
                            if (i >= 2 && i < 7) { 
                            Label label = this.Controls.Find("label" + (i-1), true).FirstOrDefault() as Label;
                            if (label != null)
                            {
                                if (parts[1].Trim() != "") {
                                    label.Text = parts[1].Trim();
                                }
                            }
                            }
                        }
                        if (i == 7 && parts[1].Trim() != "" && parts[2].Trim() != "")
                        {
                            textBox2.Text = parts[1].Trim() + ":" + parts[2].Trim();
                        }

                        i++;
                    }
                }
            }
        }

        public string SaveFiles(int type)
        {

            if (type == 1) {  // return Save file path
                return localLowFolderPath + @"\Nokta Games\Supermarket Simulator\SaveFile.es3";
            }
            else // return Save folder path
            {
                return localLowFolderPath + @"\Nokta Games\Supermarket Simulator";
            }
        }

        void overWriteConfig(string text,int btn,int type)
        {
            if( type == 0) {
                string slotName="";

                Control slotControl = Controls.Find("Label" + btn,true).FirstOrDefault();
                if (slotControl != null && slotControl is Label)
                {
                     slotName = (slotControl as Label).Text;
                }

            Form2 dialog = new Form2(text, btn, slotName);
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
        void overWriteConfig(string lineBeginString, string newString) {
            string[] lines = File.ReadAllLines(cfgFile);
            int lineIndexToOverride = FindLineIndexWithSection(lines, $"[{lineBeginString}]:");
            if (lineIndexToOverride >= 0 && lineIndexToOverride < lines.Length)
            {
                lines[lineIndexToOverride] = $"[{lineBeginString}]:" + newString;
            }
            else
            {
                MessageBox.Show("Config file is corrupted");
            }

            File.WriteAllLines(cfgFile, lines);
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

        public void Action(string type, int btn, bool overwrite,bool skipSaveCheck)
        {
            // TYPES:
            // S - Save
            // L - Load
            // R - Rename
            string savenum = btn.ToString();
            string path;
            if (textBox2.Text != "" || textBox2.Text != null)
            {
                path = textBox2.Text + @"\" + savenum;
            }
            else
            {
                path = SaveFiles(0) + @"\" + savenum;
            }

            if (type == "S") {
                if (!File.Exists( SaveFiles(1) ) && !skipSaveCheck){
                    MessageBox.Show("Can't save.\nYou don't start game yet\nTurn on the game and click \"New Game\"");
                    return;
                }

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
                    Action("S", currentSlot,false,true);

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
           Action(type, num, true,false);

        }
        private void btn_OpenFile_Click(object sender, EventArgs e)
        {
            //OpenFileDialog ofd = new OpenFileDialog();
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                overWriteConfig("GamePath", openFileDialog1.FileName);
               // overWriteConfig(openFileDialog1.FileName, 0, 1);
                textBox1.Text = openFileDialog1.FileName;
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            overWriteConfig("CloudPath", textBox2.Text);
        }

        private void btn_OpenCloud_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBox2.Text = folderBrowserDialog1.SelectedPath;
                overWriteConfig("CloudPath", textBox2.Text);
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            overWriteConfig("GamePath", textBox1.Text);
        }
        public static void OpenExplorerAtPath(string path)
        {
            if (System.IO.Directory.Exists(path))
            {
                Process.Start("explorer.exe", path);
            }
            else
            {
                Console.WriteLine("Podana ścieżka nie istnieje.");
            }
        }

        private void B_openLocalFolder_Click(object sender, EventArgs e)
        {
            OpenExplorerAtPath( SaveFiles(0) );
        }

        private void B_openCloudFolder_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                OpenExplorerAtPath( textBox2.Text );
            }
            else
            {
                MessageBox.Show("Cloud folder path is empty or not existed");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenExplorerAtPath( cfgFolder );
        }
    }

}

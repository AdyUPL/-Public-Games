using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;
using System.Runtime.CompilerServices;

namespace SupermarketSimulatorSaveChanger
{
    public partial class Form1 : Form
    {

        public static string username = Environment.UserName;
        public static string DriveLetter = Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System)).Substring(0, 1);
        public static string localLowFolderPath = $@"{DriveLetter}:\Users\{username}\AppData\LocalLow";
        public static string cfgFolder = $@"{DriveLetter}:\Users\{username}\AppData\LocalLow\SupermarketSimulator_SaveManager";
        public static string cfgFile = cfgFolder + @"\config.txt";

        static int currentSlot = 0;
        static bool dialogOk = false;
        public static string dialogReturnedName = "";

        static Settings set = new Settings();
        //static Form1 sForm = new Form1();

        public static Form1 sForm;

        public Form1()
        {
            InitializeComponent();
            sForm = this;
            config();
        }
        public void config()
        {

            if (!Directory.Exists(cfgFolder))
            {
                Directory.CreateDirectory(cfgFolder);
            }
            if (!File.Exists(cfgFile))
            {
                string slots = "";
                    for (int i = 1; i <= 5; i++)
                    {
                        slots+="[" + i + "]:\n";
                    }
                createConfig($"[LastLoad]:\n,[GamePath]:\n,{slots},[CloudPath]:\n,[ProfileName]:\n");
            }
            else
            {
                //######  checkmarks  ######
                bool checkLast=false;
                bool checkGame=false;
                int _CountSlot = 0;
                bool checkSlots=false;
                bool checkCloud=false;
                bool checkProfile=false;
                //##########################

                int saveSlot = 1;
                foreach (string line in File.ReadAllLines(cfgFile))
                {
                    string[] frag = line.Split(':');
                    if (line.Contains("[LastLoad]"))
                    {
                        checkLast = true;
                        if (frag[1] != "") { 
                            loadedSave.Text = "Loaded slot: " + frag[1];
                            currentSlot = Int32.Parse(frag[1]);
                        }
                    }

                    if (line.Contains("[GamePath]"))
                    {
                        checkGame = true;
                        if (frag[1] != "" && (frag.Length-1)==2)
                        {
                            set.exePath.Text = frag[1] + ":" + frag[2];
                        }
                    }

                    if (line.Contains("["+ saveSlot + "]") && saveSlot<=5)
                    {
                        _CountSlot++;
                        Label label = this.Controls.Find("label" + saveSlot, true).FirstOrDefault() as Label;
                        if (label != null)
                        {
                            if (frag[1] != "")
                            {
                                label.Text = frag[1];
                            }
                        }
                        if (_CountSlot == 5)
                        {
                            checkSlots = true;
                        }
                        saveSlot++;
                    }

                    if (line.Contains("[CloudPath]"))
                    {
                        checkCloud = true;
                        if (frag[1] != "" && (frag.Length - 1) == 2)
                        {
                            set.cloudPath.Text = frag[1] + ":" + frag[2];
                        }
                    }

                    if (line.Contains("[ProfileName]"))
                    {
                        checkProfile = true;
                        if (frag[1] != "")
                        {
                            set.userProfile.Text = frag[1];
                            // owerwite main path
                            username = frag[1];
                            localLowFolderPath = $@"{DriveLetter}:\Users\{username}\AppData\LocalLow";
                            //--------------------
                        }
                    }
                }

                
                if (!checkLast)
                {
                    createConfig("[LastLoad]:\n"); 
                }
                if (!checkGame)
                {
                    createConfig("[GamePath]:\n");
                }
                if (!checkSlots)
                {
                    OpenExplorerAtPath(cfgFolder);
                    MessageBox.Show($"Config file is corrupted!\n" +
                        $"Please rename it to config_bak.txt\n" +
                        $"The Config folder should open, otherwise navigate to the directory:\n" +
                        $"{cfgFolder}");
                    Close();
                }
                if (!checkCloud)
                {
                    createConfig("[CloudPath]:\n");
                }
                if (!checkProfile)
                {
                    createConfig("[ProfileName]:\n");
                }


            }
        }
        void createConfig(string text)
        {
            foreach (string line in text.Split(','))
            {
                File.AppendAllText(cfgFile, line);
            }
        }

        public static string SaveFiles()
        {
                return localLowFolderPath + @"\Nokta Games\Supermarket Simulator";
        }       

        public static void overWriteConfig(string lineBeginString, string newString) {
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

        void copyFiles(string source, string destination)
        {
           foreach(string file in Directory.GetFiles(source, "*.es3"))
            {
                File.Copy(file, Path.Combine(destination, Path.GetFileName(file)), true);
            }

        }

        public static void Action(string type, int btn, bool overwrite,bool skipSaveCheck)
        {
            // TYPES:
            // S - Save
            // L - Load
            // R - Rename
            string savenum = btn.ToString();
            string path;
            if (set.cloudPath.Text != "")
            {
                path = set.cloudPath.Text + @"\" + savenum;
            }
            else
            {
                path = SaveFiles() + @"\" + savenum;
            }

            if (type == "S") {
                bool saveExists = false;
                foreach (string file in Directory.GetFiles(SaveFiles(), "*.es3"))
                {
                    saveExists = true;
                }

                if (!saveExists && !skipSaveCheck){
                    MessageBox.Show("Can't save.\nYou don't start game yet\nTurn on the game and click \"New Game\"");
                    return;
                }

                if (!Directory.Exists(path))
                { 
                    Directory.CreateDirectory(path);
                }
                    // Delete old files
                    foreach (string file in Directory.GetFiles(path, "*.es3"))
                {
                    File.Delete(file);
                }

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                sForm.copyFiles(SaveFiles(), path);
               
                if (overwrite) { 
                    overWriteConfig(btn.ToString(),dialogReturnedName);
                    MessageBox.Show("Complete");
                }             
            }

            if (type == "L"){
                if (Directory.Exists(path))
                {
                    //Check files to load
                    if (Directory.GetFiles(path).Count() == 0)
                    {
                        MessageBox.Show("Current save is empty!\nCan't load");
                        return;
                    }

                    // Save old
                    Action("S", currentSlot,false,true);

                    // Delete files
                    foreach(string file in Directory.GetFiles(SaveFiles(), "*.es3"))
                    {
                        File.Delete(file);
                    }

                    // Load new
                    sForm.copyFiles(path, SaveFiles());

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
                if (set.exePath.Text != "")
                {
                    Process.Start(set.exePath.Text);
                }
                
            }

            if (type == "R"){
                overWriteConfig(btn.ToString(), dialogReturnedName);
                MessageBox.Show("Complete");
            }

            sForm.config();

        }

        private void btn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            string type = btn.Name.Substring(4, 1);
            int num = int.Parse(btn.Name.Substring(btn.Name.Length - 1));



            if (type == "S" || type == "R")
            {
                string slotName = "";
                Control slotControl = Controls.Find("Label" + num, true).FirstOrDefault();

                if (slotControl != null && slotControl is Label)
                {
                    slotName = (slotControl as Label).Text;
                }

                Form2 dialog = new Form2(btn.Text, slotName);
                DialogResult dialogResult = dialog.ShowDialog();

                if (dialogResult == DialogResult.Cancel)
                {
                    MessageBox.Show("Canceled");
                    return;
                }

                dialogReturnedName = dialog.newName;
            }

            Action(type, num, true, false);

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

        private void BTN_settings_Click(object sender, EventArgs e)
        {
            set.ShowDialog();
        }
    }

}

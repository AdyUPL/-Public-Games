namespace SupermarketSimulatorSaveChanger
{
    partial class Settings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.B_configFolder = new System.Windows.Forms.Button();
            this.B_openCloudFolder = new System.Windows.Forms.Button();
            this.B_openLocalFolder = new System.Windows.Forms.Button();
            this.text_CloudFolderPath = new System.Windows.Forms.Label();
            this.text_GamePath = new System.Windows.Forms.Label();
            this.B_cloudPath = new System.Windows.Forms.Button();
            this.B_exePath = new System.Windows.Forms.Button();
            this.cloudPath = new System.Windows.Forms.TextBox();
            this.exePath = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.userProfile = new System.Windows.Forms.TextBox();
            this.B_userProfile = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // B_configFolder
            // 
            this.B_configFolder.Location = new System.Drawing.Point(251, 148);
            this.B_configFolder.Name = "B_configFolder";
            this.B_configFolder.Size = new System.Drawing.Size(113, 23);
            this.B_configFolder.TabIndex = 27;
            this.B_configFolder.Text = "Open Config Folder";
            this.B_configFolder.UseVisualStyleBackColor = true;
            this.B_configFolder.Click += new System.EventHandler(this.B_configFolder_Click);
            // 
            // B_openCloudFolder
            // 
            this.B_openCloudFolder.Location = new System.Drawing.Point(251, 90);
            this.B_openCloudFolder.Name = "B_openCloudFolder";
            this.B_openCloudFolder.Size = new System.Drawing.Size(113, 23);
            this.B_openCloudFolder.TabIndex = 28;
            this.B_openCloudFolder.Text = "Open Cloud Folder";
            this.B_openCloudFolder.UseVisualStyleBackColor = true;
            this.B_openCloudFolder.Click += new System.EventHandler(this.B_openCloudFolder_Click);
            // 
            // B_openLocalFolder
            // 
            this.B_openLocalFolder.Location = new System.Drawing.Point(251, 30);
            this.B_openLocalFolder.Name = "B_openLocalFolder";
            this.B_openLocalFolder.Size = new System.Drawing.Size(113, 23);
            this.B_openLocalFolder.TabIndex = 29;
            this.B_openLocalFolder.Text = "Open Local Folder";
            this.B_openLocalFolder.UseVisualStyleBackColor = true;
            this.B_openLocalFolder.Click += new System.EventHandler(this.B_openLocalFolder_Click);
            // 
            // text_CloudFolderPath
            // 
            this.text_CloudFolderPath.AutoSize = true;
            this.text_CloudFolderPath.Location = new System.Drawing.Point(21, 74);
            this.text_CloudFolderPath.Name = "text_CloudFolderPath";
            this.text_CloudFolderPath.Size = new System.Drawing.Size(87, 13);
            this.text_CloudFolderPath.TabIndex = 25;
            this.text_CloudFolderPath.Text = "Cloud folder path";
            // 
            // text_GamePath
            // 
            this.text_GamePath.AutoSize = true;
            this.text_GamePath.Location = new System.Drawing.Point(21, 122);
            this.text_GamePath.Name = "text_GamePath";
            this.text_GamePath.Size = new System.Drawing.Size(228, 26);
            this.text_GamePath.TabIndex = 26;
            this.text_GamePath.Text = "Game .exe file path\r\n(filled in, starts the game after loading the save)";
            this.text_GamePath.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // B_cloudPath
            // 
            this.B_cloudPath.Location = new System.Drawing.Point(200, 90);
            this.B_cloudPath.Name = "B_cloudPath";
            this.B_cloudPath.Size = new System.Drawing.Size(36, 23);
            this.B_cloudPath.TabIndex = 23;
            this.B_cloudPath.Text = "...";
            this.B_cloudPath.UseVisualStyleBackColor = true;
            this.B_cloudPath.Click += new System.EventHandler(this.B_OpenCloud_Click);
            // 
            // B_exePath
            // 
            this.B_exePath.Location = new System.Drawing.Point(200, 151);
            this.B_exePath.Name = "B_exePath";
            this.B_exePath.Size = new System.Drawing.Size(36, 23);
            this.B_exePath.TabIndex = 24;
            this.B_exePath.Text = "...";
            this.B_exePath.UseVisualStyleBackColor = true;
            this.B_exePath.Click += new System.EventHandler(this.B_OpenFile_Click);
            // 
            // cloudPath
            // 
            this.cloudPath.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.cloudPath.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cloudPath.Location = new System.Drawing.Point(12, 93);
            this.cloudPath.Name = "cloudPath";
            this.cloudPath.ReadOnly = true;
            this.cloudPath.Size = new System.Drawing.Size(182, 20);
            this.cloudPath.TabIndex = 21;
            this.cloudPath.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // exePath
            // 
            this.exePath.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.exePath.Cursor = System.Windows.Forms.Cursors.Hand;
            this.exePath.Location = new System.Drawing.Point(12, 151);
            this.exePath.Name = "exePath";
            this.exePath.ReadOnly = true;
            this.exePath.ShortcutsEnabled = false;
            this.exePath.Size = new System.Drawing.Size(182, 20);
            this.exePath.TabIndex = 22;
            this.exePath.TabStop = false;
            this.exePath.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.exePath.WordWrap = false;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // userProfile
            // 
            this.userProfile.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.userProfile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.userProfile.Location = new System.Drawing.Point(12, 33);
            this.userProfile.Name = "userProfile";
            this.userProfile.ReadOnly = true;
            this.userProfile.Size = new System.Drawing.Size(182, 20);
            this.userProfile.TabIndex = 22;
            this.userProfile.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // B_userProfile
            // 
            this.B_userProfile.Location = new System.Drawing.Point(200, 33);
            this.B_userProfile.Name = "B_userProfile";
            this.B_userProfile.Size = new System.Drawing.Size(36, 23);
            this.B_userProfile.TabIndex = 24;
            this.B_userProfile.Text = "...";
            this.B_userProfile.UseVisualStyleBackColor = true;
            this.B_userProfile.Click += new System.EventHandler(this.B_userProfile_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(226, 26);
            this.label1.TabIndex = 26;
            this.label1.Text = "User profile name\r\n( example: C:\\Users\\Public for name \"Public\" )";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(380, 191);
            this.Controls.Add(this.B_configFolder);
            this.Controls.Add(this.B_openCloudFolder);
            this.Controls.Add(this.B_openLocalFolder);
            this.Controls.Add(this.text_CloudFolderPath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.text_GamePath);
            this.Controls.Add(this.B_cloudPath);
            this.Controls.Add(this.B_userProfile);
            this.Controls.Add(this.B_exePath);
            this.Controls.Add(this.userProfile);
            this.Controls.Add(this.cloudPath);
            this.Controls.Add(this.exePath);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Settings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button B_configFolder;
        private System.Windows.Forms.Button B_openCloudFolder;
        private System.Windows.Forms.Button B_openLocalFolder;
        private System.Windows.Forms.Label text_CloudFolderPath;
        private System.Windows.Forms.Label text_GamePath;
        private System.Windows.Forms.Button B_cloudPath;
        private System.Windows.Forms.Button B_exePath;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button B_userProfile;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox cloudPath;
        public System.Windows.Forms.TextBox exePath;
        public System.Windows.Forms.TextBox userProfile;
    }
}
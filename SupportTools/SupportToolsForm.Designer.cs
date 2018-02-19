namespace SupportTools
{
    partial class SupportToolsForm
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageWifiCache = new System.Windows.Forms.TabPage();
            this.lblStatusContent = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.pbrClearCache = new System.Windows.Forms.ProgressBar();
            this.btnClearCache = new System.Windows.Forms.Button();
            this.tabPageSettings = new System.Windows.Forms.TabPage();
            this.gbxClearpassLogging = new System.Windows.Forms.GroupBox();
            this.txtLogPath = new System.Windows.Forms.TextBox();
            this.chkEnableLogging = new System.Windows.Forms.CheckBox();
            this.gbxClearpassStartPageUrl = new System.Windows.Forms.GroupBox();
            this.txtClearpassStartPageUrl = new System.Windows.Forms.TextBox();
            this.gbxClearpassCredentials = new System.Windows.Forms.GroupBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblUsername = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.lblVersion = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPageWifiCache.SuspendLayout();
            this.tabPageSettings.SuspendLayout();
            this.gbxClearpassLogging.SuspendLayout();
            this.gbxClearpassStartPageUrl.SuspendLayout();
            this.gbxClearpassCredentials.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageWifiCache);
            this.tabControl1.Controls.Add(this.tabPageSettings);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(400, 299);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPageWifiCache
            // 
            this.tabPageWifiCache.Controls.Add(this.lblStatusContent);
            this.tabPageWifiCache.Controls.Add(this.lblStatus);
            this.tabPageWifiCache.Controls.Add(this.pbrClearCache);
            this.tabPageWifiCache.Controls.Add(this.btnClearCache);
            this.tabPageWifiCache.Location = new System.Drawing.Point(4, 22);
            this.tabPageWifiCache.Name = "tabPageWifiCache";
            this.tabPageWifiCache.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageWifiCache.Size = new System.Drawing.Size(392, 273);
            this.tabPageWifiCache.TabIndex = 0;
            this.tabPageWifiCache.Text = "WifiCache";
            this.tabPageWifiCache.UseVisualStyleBackColor = true;
            // 
            // lblStatusContent
            // 
            this.lblStatusContent.AutoSize = true;
            this.lblStatusContent.Location = new System.Drawing.Point(130, 45);
            this.lblStatusContent.Name = "lblStatusContent";
            this.lblStatusContent.Size = new System.Drawing.Size(0, 13);
            this.lblStatusContent.TabIndex = 4;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(87, 45);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(37, 13);
            this.lblStatus.TabIndex = 3;
            this.lblStatus.Text = "Status";
            // 
            // pbrClearCache
            // 
            this.pbrClearCache.Location = new System.Drawing.Point(6, 35);
            this.pbrClearCache.Maximum = 7;
            this.pbrClearCache.Name = "pbrClearCache";
            this.pbrClearCache.Size = new System.Drawing.Size(75, 23);
            this.pbrClearCache.Step = 1;
            this.pbrClearCache.TabIndex = 2;
            // 
            // btnClearCache
            // 
            this.btnClearCache.Location = new System.Drawing.Point(6, 6);
            this.btnClearCache.Name = "btnClearCache";
            this.btnClearCache.Size = new System.Drawing.Size(75, 23);
            this.btnClearCache.TabIndex = 1;
            this.btnClearCache.Text = "Clear Cache";
            this.btnClearCache.UseVisualStyleBackColor = true;
            this.btnClearCache.Click += new System.EventHandler(this.btnClearCache_Click);
            // 
            // tabPageSettings
            // 
            this.tabPageSettings.Controls.Add(this.lblVersion);
            this.tabPageSettings.Controls.Add(this.gbxClearpassLogging);
            this.tabPageSettings.Controls.Add(this.gbxClearpassStartPageUrl);
            this.tabPageSettings.Controls.Add(this.gbxClearpassCredentials);
            this.tabPageSettings.Location = new System.Drawing.Point(4, 22);
            this.tabPageSettings.Name = "tabPageSettings";
            this.tabPageSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSettings.Size = new System.Drawing.Size(392, 273);
            this.tabPageSettings.TabIndex = 1;
            this.tabPageSettings.Text = "Settings";
            this.tabPageSettings.UseVisualStyleBackColor = true;
            // 
            // gbxClearpassLogging
            // 
            this.gbxClearpassLogging.Controls.Add(this.txtLogPath);
            this.gbxClearpassLogging.Controls.Add(this.chkEnableLogging);
            this.gbxClearpassLogging.Location = new System.Drawing.Point(6, 170);
            this.gbxClearpassLogging.Name = "gbxClearpassLogging";
            this.gbxClearpassLogging.Size = new System.Drawing.Size(217, 68);
            this.gbxClearpassLogging.TabIndex = 2;
            this.gbxClearpassLogging.TabStop = false;
            this.gbxClearpassLogging.Text = "Clearpass logging";
            // 
            // txtLogPath
            // 
            this.txtLogPath.Location = new System.Drawing.Point(6, 42);
            this.txtLogPath.Name = "txtLogPath";
            this.txtLogPath.Size = new System.Drawing.Size(202, 20);
            this.txtLogPath.TabIndex = 1;
            this.txtLogPath.DoubleClick += new System.EventHandler(this.txtLogPath_DoubleClick);
            // 
            // chkEnableLogging
            // 
            this.chkEnableLogging.AutoSize = true;
            this.chkEnableLogging.Location = new System.Drawing.Point(6, 19);
            this.chkEnableLogging.Name = "chkEnableLogging";
            this.chkEnableLogging.Size = new System.Drawing.Size(96, 17);
            this.chkEnableLogging.TabIndex = 0;
            this.chkEnableLogging.Text = "Enable logging";
            this.chkEnableLogging.UseVisualStyleBackColor = true;
            this.chkEnableLogging.CheckedChanged += new System.EventHandler(this.chkEnableLogging_CheckedChanged);
            // 
            // gbxClearpassStartPageUrl
            // 
            this.gbxClearpassStartPageUrl.Controls.Add(this.txtClearpassStartPageUrl);
            this.gbxClearpassStartPageUrl.Location = new System.Drawing.Point(6, 117);
            this.gbxClearpassStartPageUrl.Name = "gbxClearpassStartPageUrl";
            this.gbxClearpassStartPageUrl.Size = new System.Drawing.Size(217, 47);
            this.gbxClearpassStartPageUrl.TabIndex = 1;
            this.gbxClearpassStartPageUrl.TabStop = false;
            this.gbxClearpassStartPageUrl.Text = "Clearpass Start Page Url";
            // 
            // txtClearpassStartPageUrl
            // 
            this.txtClearpassStartPageUrl.Location = new System.Drawing.Point(6, 19);
            this.txtClearpassStartPageUrl.Name = "txtClearpassStartPageUrl";
            this.txtClearpassStartPageUrl.Size = new System.Drawing.Size(202, 20);
            this.txtClearpassStartPageUrl.TabIndex = 0;
            // 
            // gbxClearpassCredentials
            // 
            this.gbxClearpassCredentials.Controls.Add(this.lblPassword);
            this.gbxClearpassCredentials.Controls.Add(this.lblUsername);
            this.gbxClearpassCredentials.Controls.Add(this.txtPassword);
            this.gbxClearpassCredentials.Controls.Add(this.txtUsername);
            this.gbxClearpassCredentials.Location = new System.Drawing.Point(6, 6);
            this.gbxClearpassCredentials.Name = "gbxClearpassCredentials";
            this.gbxClearpassCredentials.Size = new System.Drawing.Size(128, 105);
            this.gbxClearpassCredentials.TabIndex = 0;
            this.gbxClearpassCredentials.TabStop = false;
            this.gbxClearpassCredentials.Text = "Clearpass credentials";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(6, 59);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(53, 13);
            this.lblPassword.TabIndex = 3;
            this.lblPassword.Text = "Password";
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(7, 20);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(55, 13);
            this.lblUsername.TabIndex = 2;
            this.lblUsername.Text = "Username";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(6, 75);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '•';
            this.txtPassword.Size = new System.Drawing.Size(100, 20);
            this.txtPassword.TabIndex = 1;
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(6, 36);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(100, 20);
            this.txtUsername.TabIndex = 0;
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(4, 257);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(0, 13);
            this.lblVersion.TabIndex = 3;
            // 
            // SupportToolsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 299);
            this.Controls.Add(this.tabControl1);
            this.Name = "SupportToolsForm";
            this.Text = "Support Tools";
            this.Deactivate += new System.EventHandler(this.SupportToolsForm_Deactivate);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SupportToolsForm_FormClosed);
            this.Load += new System.EventHandler(this.SupportToolsForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPageWifiCache.ResumeLayout(false);
            this.tabPageWifiCache.PerformLayout();
            this.tabPageSettings.ResumeLayout(false);
            this.tabPageSettings.PerformLayout();
            this.gbxClearpassLogging.ResumeLayout(false);
            this.gbxClearpassLogging.PerformLayout();
            this.gbxClearpassStartPageUrl.ResumeLayout(false);
            this.gbxClearpassStartPageUrl.PerformLayout();
            this.gbxClearpassCredentials.ResumeLayout(false);
            this.gbxClearpassCredentials.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageWifiCache;
        private System.Windows.Forms.TabPage tabPageSettings;
        private System.Windows.Forms.GroupBox gbxClearpassCredentials;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Button btnClearCache;
        private System.Windows.Forms.GroupBox gbxClearpassStartPageUrl;
        private System.Windows.Forms.TextBox txtClearpassStartPageUrl;
        private System.Windows.Forms.Label lblStatusContent;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ProgressBar pbrClearCache;
        private System.Windows.Forms.GroupBox gbxClearpassLogging;
        private System.Windows.Forms.TextBox txtLogPath;
        private System.Windows.Forms.CheckBox chkEnableLogging;
        private System.Windows.Forms.Label lblVersion;
    }
}


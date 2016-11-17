namespace GUI_SecChecker_v2
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_pass = new System.Windows.Forms.TextBox();
            this.tb_domain = new System.Windows.Forms.TextBox();
            this.tb_login = new System.Windows.Forms.TextBox();
            this.bt_GetDataAD = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tb_PathMPReport = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.bt_BrowseMPReport = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.tb_PathKSCReport = new System.Windows.Forms.TextBox();
            this.bt_BrowseKSCReport = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.tb_PathSEPReport = new System.Windows.Forms.TextBox();
            this.bt_BrowseSEPReport = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.tb_PathSCCMReport = new System.Windows.Forms.TextBox();
            this.bt_BrowseSCCMReport = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.bt_GetAllHost = new System.Windows.Forms.Button();
            this.chb_ADFromFile = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(143, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Pass";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(140, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Login";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(130, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Domain";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(204, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Account for read AD";
            // 
            // tb_pass
            // 
            this.tb_pass.Location = new System.Drawing.Point(207, 95);
            this.tb_pass.Name = "tb_pass";
            this.tb_pass.Size = new System.Drawing.Size(100, 20);
            this.tb_pass.TabIndex = 5;
            this.tb_pass.Text = "VS1997vs";
            // 
            // tb_domain
            // 
            this.tb_domain.Location = new System.Drawing.Point(207, 43);
            this.tb_domain.Name = "tb_domain";
            this.tb_domain.Size = new System.Drawing.Size(100, 20);
            this.tb_domain.TabIndex = 6;
            this.tb_domain.Text = "szbsbrf";
            // 
            // tb_login
            // 
            this.tb_login.Location = new System.Drawing.Point(207, 69);
            this.tb_login.Name = "tb_login";
            this.tb_login.Size = new System.Drawing.Size(100, 20);
            this.tb_login.TabIndex = 7;
            this.tb_login.Text = "KartashevVS";
            // 
            // bt_GetDataAD
            // 
            this.bt_GetDataAD.Location = new System.Drawing.Point(207, 134);
            this.bt_GetDataAD.Name = "bt_GetDataAD";
            this.bt_GetDataAD.Size = new System.Drawing.Size(100, 23);
            this.bt_GetDataAD.TabIndex = 4;
            this.bt_GetDataAD.Text = "Get Data from AD";
            this.bt_GetDataAD.UseVisualStyleBackColor = true;
            this.bt_GetDataAD.Click += new System.EventHandler(this.bt_GetDataAD_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(151, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(22, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "AD";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(572, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(23, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "MP";
            // 
            // tb_PathMPReport
            // 
            this.tb_PathMPReport.Location = new System.Drawing.Point(368, 89);
            this.tb_PathMPReport.Name = "tb_PathMPReport";
            this.tb_PathMPReport.Size = new System.Drawing.Size(209, 20);
            this.tb_PathMPReport.TabIndex = 15;
            this.tb_PathMPReport.Text = "C:\\Users\\KartashevVS\\Desktop\\2016-10-21\\2016-11-15\\SZB\\AD";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(500, 55);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(99, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Path To MP Report";
            // 
            // bt_BrowseMPReport
            // 
            this.bt_BrowseMPReport.Location = new System.Drawing.Point(633, 86);
            this.bt_BrowseMPReport.Name = "bt_BrowseMPReport";
            this.bt_BrowseMPReport.Size = new System.Drawing.Size(75, 23);
            this.bt_BrowseMPReport.TabIndex = 16;
            this.bt_BrowseMPReport.Text = "Browse";
            this.bt_BrowseMPReport.UseVisualStyleBackColor = true;
            this.bt_BrowseMPReport.Click += new System.EventHandler(this.bt_BrowseMPReport_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(567, 125);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(28, 13);
            this.label9.TabIndex = 14;
            this.label9.Text = "KSC";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(491, 171);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(104, 13);
            this.label10.TabIndex = 14;
            this.label10.Text = "Path To KSC Report";
            // 
            // tb_PathKSCReport
            // 
            this.tb_PathKSCReport.Location = new System.Drawing.Point(368, 202);
            this.tb_PathKSCReport.Name = "tb_PathKSCReport";
            this.tb_PathKSCReport.Size = new System.Drawing.Size(209, 20);
            this.tb_PathKSCReport.TabIndex = 15;
            // 
            // bt_BrowseKSCReport
            // 
            this.bt_BrowseKSCReport.Location = new System.Drawing.Point(633, 200);
            this.bt_BrowseKSCReport.Name = "bt_BrowseKSCReport";
            this.bt_BrowseKSCReport.Size = new System.Drawing.Size(75, 23);
            this.bt_BrowseKSCReport.TabIndex = 16;
            this.bt_BrowseKSCReport.Text = "Browse";
            this.bt_BrowseKSCReport.UseVisualStyleBackColor = true;
            this.bt_BrowseKSCReport.Click += new System.EventHandler(this.bt_BrowseKSCReport_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(567, 256);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(28, 13);
            this.label11.TabIndex = 14;
            this.label11.Text = "SEP";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(495, 298);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(104, 13);
            this.label12.TabIndex = 14;
            this.label12.Text = "Path To SEP Report";
            // 
            // tb_PathSEPReport
            // 
            this.tb_PathSEPReport.Location = new System.Drawing.Point(364, 331);
            this.tb_PathSEPReport.Name = "tb_PathSEPReport";
            this.tb_PathSEPReport.Size = new System.Drawing.Size(209, 20);
            this.tb_PathSEPReport.TabIndex = 15;
            // 
            // bt_BrowseSEPReport
            // 
            this.bt_BrowseSEPReport.Location = new System.Drawing.Point(633, 329);
            this.bt_BrowseSEPReport.Name = "bt_BrowseSEPReport";
            this.bt_BrowseSEPReport.Size = new System.Drawing.Size(75, 23);
            this.bt_BrowseSEPReport.TabIndex = 16;
            this.bt_BrowseSEPReport.Text = "Browse";
            this.bt_BrowseSEPReport.UseVisualStyleBackColor = true;
            this.bt_BrowseSEPReport.Click += new System.EventHandler(this.bt_BrowseSEPReport_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(558, 389);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(37, 13);
            this.label13.TabIndex = 14;
            this.label13.Text = "SCCM";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(482, 428);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(113, 13);
            this.label14.TabIndex = 14;
            this.label14.Text = "Path To SCCM Report";
            // 
            // tb_PathSCCMReport
            // 
            this.tb_PathSCCMReport.Location = new System.Drawing.Point(364, 464);
            this.tb_PathSCCMReport.Name = "tb_PathSCCMReport";
            this.tb_PathSCCMReport.Size = new System.Drawing.Size(209, 20);
            this.tb_PathSCCMReport.TabIndex = 15;
            // 
            // bt_BrowseSCCMReport
            // 
            this.bt_BrowseSCCMReport.Location = new System.Drawing.Point(633, 464);
            this.bt_BrowseSCCMReport.Name = "bt_BrowseSCCMReport";
            this.bt_BrowseSCCMReport.Size = new System.Drawing.Size(75, 23);
            this.bt_BrowseSCCMReport.TabIndex = 16;
            this.bt_BrowseSCCMReport.Text = "Browse";
            this.bt_BrowseSCCMReport.UseVisualStyleBackColor = true;
            this.bt_BrowseSCCMReport.Click += new System.EventHandler(this.bt_BrowseSCCMReport_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(22, 46);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(102, 13);
            this.label16.TabIndex = 12;
            this.label16.Text = "Указывать через ;";
            // 
            // bt_GetAllHost
            // 
            this.bt_GetAllHost.Location = new System.Drawing.Point(795, 76);
            this.bt_GetAllHost.Name = "bt_GetAllHost";
            this.bt_GetAllHost.Size = new System.Drawing.Size(75, 56);
            this.bt_GetAllHost.TabIndex = 16;
            this.bt_GetAllHost.Text = "1) Get All Host";
            this.bt_GetAllHost.UseVisualStyleBackColor = true;
            this.bt_GetAllHost.Click += new System.EventHandler(this.bt_GetAllHost_Click);
            // 
            // chb_ADFromFile
            // 
            this.chb_ADFromFile.AutoSize = true;
            this.chb_ADFromFile.Location = new System.Drawing.Point(94, 138);
            this.chb_ADFromFile.Name = "chb_ADFromFile";
            this.chb_ADFromFile.Size = new System.Drawing.Size(113, 17);
            this.chb_ADFromFile.TabIndex = 18;
            this.chb_ADFromFile.Text = "AD из Файла MP";
            this.chb_ADFromFile.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(895, 610);
            this.Controls.Add(this.chb_ADFromFile);
            this.Controls.Add(this.bt_BrowseSCCMReport);
            this.Controls.Add(this.bt_BrowseSEPReport);
            this.Controls.Add(this.tb_PathSCCMReport);
            this.Controls.Add(this.bt_BrowseKSCReport);
            this.Controls.Add(this.tb_PathSEPReport);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.tb_PathKSCReport);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.bt_GetAllHost);
            this.Controls.Add(this.bt_BrowseMPReport);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.tb_PathMPReport);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tb_pass);
            this.Controls.Add(this.tb_domain);
            this.Controls.Add(this.tb_login);
            this.Controls.Add(this.bt_GetDataAD);
            this.Name = "Form1";
            this.Text = "f_Main";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_pass;
        private System.Windows.Forms.TextBox tb_domain;
        private System.Windows.Forms.TextBox tb_login;
        private System.Windows.Forms.Button bt_GetDataAD;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tb_PathMPReport;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button bt_BrowseMPReport;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tb_PathKSCReport;
        private System.Windows.Forms.Button bt_BrowseKSCReport;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tb_PathSEPReport;
        private System.Windows.Forms.Button bt_BrowseSEPReport;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox tb_PathSCCMReport;
        private System.Windows.Forms.Button bt_BrowseSCCMReport;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button bt_GetAllHost;
        private System.Windows.Forms.CheckBox chb_ADFromFile;
    }
}


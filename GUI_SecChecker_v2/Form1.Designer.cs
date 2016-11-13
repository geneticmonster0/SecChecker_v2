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
            this.tb_instruction = new System.Windows.Forms.RichTextBox();
            this.tb_pass = new System.Windows.Forms.TextBox();
            this.tb_domain = new System.Windows.Forms.TextBox();
            this.tb_login = new System.Windows.Forms.TextBox();
            this.bt_GetDataAD = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tb_PathMPReport = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.bt_BrowseMPReport = new System.Windows.Forms.Button();
            this.bt_ReadMPReport = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.tb_PathKSCReport = new System.Windows.Forms.TextBox();
            this.bt_BrowseKSCReport = new System.Windows.Forms.Button();
            this.bt_ReadKSCReport = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.tb_PathSEPReport = new System.Windows.Forms.TextBox();
            this.bt_BrowseSEPReport = new System.Windows.Forms.Button();
            this.bt_ReadSEPReport = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.tb_PathSCCMReport = new System.Windows.Forms.TextBox();
            this.bt_BrowseSCCMReport = new System.Windows.Forms.Button();
            this.bt_ReadSCCMReport = new System.Windows.Forms.Button();
            this.bt_RemoveTrashFromMP = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.bt_RemoveTrashFromAD = new System.Windows.Forms.Button();
            this.bt_RemoveTrashFromKSC = new System.Windows.Forms.Button();
            this.dgv_ksc = new System.Windows.Forms.DataGridView();
            this.bt_DisplayKSC = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ksc)).BeginInit();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(354, 150);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Pass";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(351, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Login";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(341, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Domain";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(415, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Account for read AD";
            // 
            // tb_instruction
            // 
            this.tb_instruction.Location = new System.Drawing.Point(12, 41);
            this.tb_instruction.Name = "tb_instruction";
            this.tb_instruction.Size = new System.Drawing.Size(185, 158);
            this.tb_instruction.TabIndex = 8;
            this.tb_instruction.Text = "1) Create C:\\Temp\\\n2) Execute Set-ExecutionPolicy Unrestricted\n\n";
            // 
            // tb_pass
            // 
            this.tb_pass.Location = new System.Drawing.Point(418, 143);
            this.tb_pass.Name = "tb_pass";
            this.tb_pass.Size = new System.Drawing.Size(100, 20);
            this.tb_pass.TabIndex = 5;
            this.tb_pass.Text = "Symc4now!";
            // 
            // tb_domain
            // 
            this.tb_domain.Location = new System.Drawing.Point(418, 91);
            this.tb_domain.Name = "tb_domain";
            this.tb_domain.Size = new System.Drawing.Size(100, 20);
            this.tb_domain.TabIndex = 6;
            this.tb_domain.Text = "example;example";
            // 
            // tb_login
            // 
            this.tb_login.Location = new System.Drawing.Point(418, 117);
            this.tb_login.Name = "tb_login";
            this.tb_login.Size = new System.Drawing.Size(100, 20);
            this.tb_login.TabIndex = 7;
            this.tb_login.Text = "administrator";
            // 
            // bt_GetDataAD
            // 
            this.bt_GetDataAD.Location = new System.Drawing.Point(418, 182);
            this.bt_GetDataAD.Name = "bt_GetDataAD";
            this.bt_GetDataAD.Size = new System.Drawing.Size(100, 23);
            this.bt_GetDataAD.TabIndex = 4;
            this.bt_GetDataAD.Text = "Get Data from AD";
            this.bt_GetDataAD.UseVisualStyleBackColor = true;
            this.bt_GetDataAD.Click += new System.EventHandler(this.bt_GetDataAD_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(516, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Получение данных";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(362, 57);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(22, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "AD";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(362, 219);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(23, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "MP";
            // 
            // tb_PathMPReport
            // 
            this.tb_PathMPReport.Location = new System.Drawing.Point(175, 294);
            this.tb_PathMPReport.Name = "tb_PathMPReport";
            this.tb_PathMPReport.Size = new System.Drawing.Size(209, 20);
            this.tb_PathMPReport.TabIndex = 15;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(281, 261);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(99, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Path To MP Report";
            // 
            // bt_BrowseMPReport
            // 
            this.bt_BrowseMPReport.Location = new System.Drawing.Point(418, 291);
            this.bt_BrowseMPReport.Name = "bt_BrowseMPReport";
            this.bt_BrowseMPReport.Size = new System.Drawing.Size(75, 23);
            this.bt_BrowseMPReport.TabIndex = 16;
            this.bt_BrowseMPReport.Text = "Browse";
            this.bt_BrowseMPReport.UseVisualStyleBackColor = true;
            this.bt_BrowseMPReport.Click += new System.EventHandler(this.bt_BrowseMPReport_Click);
            // 
            // bt_ReadMPReport
            // 
            this.bt_ReadMPReport.Location = new System.Drawing.Point(418, 320);
            this.bt_ReadMPReport.Name = "bt_ReadMPReport";
            this.bt_ReadMPReport.Size = new System.Drawing.Size(75, 43);
            this.bt_ReadMPReport.TabIndex = 16;
            this.bt_ReadMPReport.Text = "Read MP Report";
            this.bt_ReadMPReport.UseVisualStyleBackColor = true;
            this.bt_ReadMPReport.Click += new System.EventHandler(this.bt_ReadMPReport_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(771, 30);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(28, 13);
            this.label9.TabIndex = 14;
            this.label9.Text = "KSC";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(690, 72);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(104, 13);
            this.label10.TabIndex = 14;
            this.label10.Text = "Path To KSC Report";
            // 
            // tb_PathKSCReport
            // 
            this.tb_PathKSCReport.Location = new System.Drawing.Point(584, 105);
            this.tb_PathKSCReport.Name = "tb_PathKSCReport";
            this.tb_PathKSCReport.Size = new System.Drawing.Size(209, 20);
            this.tb_PathKSCReport.TabIndex = 15;
            // 
            // bt_BrowseKSCReport
            // 
            this.bt_BrowseKSCReport.Location = new System.Drawing.Point(827, 102);
            this.bt_BrowseKSCReport.Name = "bt_BrowseKSCReport";
            this.bt_BrowseKSCReport.Size = new System.Drawing.Size(75, 23);
            this.bt_BrowseKSCReport.TabIndex = 16;
            this.bt_BrowseKSCReport.Text = "Browse";
            this.bt_BrowseKSCReport.UseVisualStyleBackColor = true;
            this.bt_BrowseKSCReport.Click += new System.EventHandler(this.bt_BrowseKSCReport_Click);
            // 
            // bt_ReadKSCReport
            // 
            this.bt_ReadKSCReport.Location = new System.Drawing.Point(827, 131);
            this.bt_ReadKSCReport.Name = "bt_ReadKSCReport";
            this.bt_ReadKSCReport.Size = new System.Drawing.Size(75, 43);
            this.bt_ReadKSCReport.TabIndex = 16;
            this.bt_ReadKSCReport.Text = "Read KSC Report";
            this.bt_ReadKSCReport.UseVisualStyleBackColor = true;
            this.bt_ReadKSCReport.Click += new System.EventHandler(this.bt_ReadKSCReport_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(771, 219);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(28, 13);
            this.label11.TabIndex = 14;
            this.label11.Text = "SEP";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(690, 261);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(104, 13);
            this.label12.TabIndex = 14;
            this.label12.Text = "Path To SEP Report";
            // 
            // tb_PathSEPReport
            // 
            this.tb_PathSEPReport.Location = new System.Drawing.Point(584, 294);
            this.tb_PathSEPReport.Name = "tb_PathSEPReport";
            this.tb_PathSEPReport.Size = new System.Drawing.Size(209, 20);
            this.tb_PathSEPReport.TabIndex = 15;
            // 
            // bt_BrowseSEPReport
            // 
            this.bt_BrowseSEPReport.Location = new System.Drawing.Point(827, 291);
            this.bt_BrowseSEPReport.Name = "bt_BrowseSEPReport";
            this.bt_BrowseSEPReport.Size = new System.Drawing.Size(75, 23);
            this.bt_BrowseSEPReport.TabIndex = 16;
            this.bt_BrowseSEPReport.Text = "Browse";
            this.bt_BrowseSEPReport.UseVisualStyleBackColor = true;
            this.bt_BrowseSEPReport.Click += new System.EventHandler(this.bt_BrowseSEPReport_Click);
            // 
            // bt_ReadSEPReport
            // 
            this.bt_ReadSEPReport.Location = new System.Drawing.Point(827, 320);
            this.bt_ReadSEPReport.Name = "bt_ReadSEPReport";
            this.bt_ReadSEPReport.Size = new System.Drawing.Size(75, 43);
            this.bt_ReadSEPReport.TabIndex = 16;
            this.bt_ReadSEPReport.Text = "Read SEP Report";
            this.bt_ReadSEPReport.UseVisualStyleBackColor = true;
            this.bt_ReadSEPReport.Click += new System.EventHandler(this.bt_ReadSEPReport_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(362, 474);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(37, 13);
            this.label13.TabIndex = 14;
            this.label13.Text = "SCCM";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(281, 516);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(113, 13);
            this.label14.TabIndex = 14;
            this.label14.Text = "Path To SCCM Report";
            // 
            // tb_PathSCCMReport
            // 
            this.tb_PathSCCMReport.Location = new System.Drawing.Point(175, 549);
            this.tb_PathSCCMReport.Name = "tb_PathSCCMReport";
            this.tb_PathSCCMReport.Size = new System.Drawing.Size(209, 20);
            this.tb_PathSCCMReport.TabIndex = 15;
            // 
            // bt_BrowseSCCMReport
            // 
            this.bt_BrowseSCCMReport.Location = new System.Drawing.Point(418, 546);
            this.bt_BrowseSCCMReport.Name = "bt_BrowseSCCMReport";
            this.bt_BrowseSCCMReport.Size = new System.Drawing.Size(75, 23);
            this.bt_BrowseSCCMReport.TabIndex = 16;
            this.bt_BrowseSCCMReport.Text = "Browse";
            this.bt_BrowseSCCMReport.UseVisualStyleBackColor = true;
            this.bt_BrowseSCCMReport.Click += new System.EventHandler(this.bt_BrowseSCCMReport_Click);
            // 
            // bt_ReadSCCMReport
            // 
            this.bt_ReadSCCMReport.Location = new System.Drawing.Point(418, 575);
            this.bt_ReadSCCMReport.Name = "bt_ReadSCCMReport";
            this.bt_ReadSCCMReport.Size = new System.Drawing.Size(75, 43);
            this.bt_ReadSCCMReport.TabIndex = 16;
            this.bt_ReadSCCMReport.Text = "Read SCCM Report";
            this.bt_ReadSCCMReport.UseVisualStyleBackColor = true;
            this.bt_ReadSCCMReport.Click += new System.EventHandler(this.bt_ReadSCCMReport_Click);
            // 
            // bt_RemoveTrashFromMP
            // 
            this.bt_RemoveTrashFromMP.Location = new System.Drawing.Point(15, 712);
            this.bt_RemoveTrashFromMP.Name = "bt_RemoveTrashFromMP";
            this.bt_RemoveTrashFromMP.Size = new System.Drawing.Size(75, 56);
            this.bt_RemoveTrashFromMP.TabIndex = 16;
            this.bt_RemoveTrashFromMP.Text = "1) Remove trash from MP";
            this.bt_RemoveTrashFromMP.UseVisualStyleBackColor = true;
            this.bt_RemoveTrashFromMP.Click += new System.EventHandler(this.bt_RemoveTrashFromMP_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(12, 669);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(96, 13);
            this.label15.TabIndex = 13;
            this.label15.Text = "Обработка даных";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(233, 94);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(102, 13);
            this.label16.TabIndex = 12;
            this.label16.Text = "Указывать через ;";
            // 
            // bt_RemoveTrashFromAD
            // 
            this.bt_RemoveTrashFromAD.Location = new System.Drawing.Point(122, 712);
            this.bt_RemoveTrashFromAD.Name = "bt_RemoveTrashFromAD";
            this.bt_RemoveTrashFromAD.Size = new System.Drawing.Size(75, 56);
            this.bt_RemoveTrashFromAD.TabIndex = 16;
            this.bt_RemoveTrashFromAD.Text = "2) Remove trash from AD";
            this.bt_RemoveTrashFromAD.UseVisualStyleBackColor = true;
            this.bt_RemoveTrashFromAD.Click += new System.EventHandler(this.bt_RemoveTrashFromAD_Click);
            // 
            // bt_RemoveTrashFromKSC
            // 
            this.bt_RemoveTrashFromKSC.Location = new System.Drawing.Point(224, 712);
            this.bt_RemoveTrashFromKSC.Name = "bt_RemoveTrashFromKSC";
            this.bt_RemoveTrashFromKSC.Size = new System.Drawing.Size(75, 56);
            this.bt_RemoveTrashFromKSC.TabIndex = 16;
            this.bt_RemoveTrashFromKSC.Text = "3) Remove trash from KSC";
            this.bt_RemoveTrashFromKSC.UseVisualStyleBackColor = true;
            this.bt_RemoveTrashFromKSC.Click += new System.EventHandler(this.bt_RemoveTrashFromKSC_Click);
            // 
            // dgv_ksc
            // 
            this.dgv_ksc.AllowUserToOrderColumns = true;
            this.dgv_ksc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_ksc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_ksc.Location = new System.Drawing.Point(622, 468);
            this.dgv_ksc.Name = "dgv_ksc";
            this.dgv_ksc.Size = new System.Drawing.Size(585, 402);
            this.dgv_ksc.TabIndex = 17;
            // 
            // bt_DisplayKSC
            // 
            this.bt_DisplayKSC.Location = new System.Drawing.Point(908, 131);
            this.bt_DisplayKSC.Name = "bt_DisplayKSC";
            this.bt_DisplayKSC.Size = new System.Drawing.Size(75, 43);
            this.bt_DisplayKSC.TabIndex = 16;
            this.bt_DisplayKSC.Text = "KSC To DGV";
            this.bt_DisplayKSC.UseVisualStyleBackColor = true;
            this.bt_DisplayKSC.Click += new System.EventHandler(this.bt_DisplayKSC_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1219, 882);
            this.Controls.Add(this.dgv_ksc);
            this.Controls.Add(this.bt_ReadSCCMReport);
            this.Controls.Add(this.bt_BrowseSCCMReport);
            this.Controls.Add(this.bt_DisplayKSC);
            this.Controls.Add(this.bt_ReadSEPReport);
            this.Controls.Add(this.bt_BrowseSEPReport);
            this.Controls.Add(this.bt_ReadKSCReport);
            this.Controls.Add(this.tb_PathSCCMReport);
            this.Controls.Add(this.bt_BrowseKSCReport);
            this.Controls.Add(this.tb_PathSEPReport);
            this.Controls.Add(this.bt_ReadMPReport);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.tb_PathKSCReport);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.bt_RemoveTrashFromKSC);
            this.Controls.Add(this.bt_RemoveTrashFromAD);
            this.Controls.Add(this.bt_RemoveTrashFromMP);
            this.Controls.Add(this.bt_BrowseMPReport);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.tb_PathMPReport);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tb_instruction);
            this.Controls.Add(this.tb_pass);
            this.Controls.Add(this.tb_domain);
            this.Controls.Add(this.tb_login);
            this.Controls.Add(this.bt_GetDataAD);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ksc)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox tb_instruction;
        private System.Windows.Forms.TextBox tb_pass;
        private System.Windows.Forms.TextBox tb_domain;
        private System.Windows.Forms.TextBox tb_login;
        private System.Windows.Forms.Button bt_GetDataAD;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tb_PathMPReport;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button bt_BrowseMPReport;
        private System.Windows.Forms.Button bt_ReadMPReport;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tb_PathKSCReport;
        private System.Windows.Forms.Button bt_BrowseKSCReport;
        private System.Windows.Forms.Button bt_ReadKSCReport;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tb_PathSEPReport;
        private System.Windows.Forms.Button bt_BrowseSEPReport;
        private System.Windows.Forms.Button bt_ReadSEPReport;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox tb_PathSCCMReport;
        private System.Windows.Forms.Button bt_BrowseSCCMReport;
        private System.Windows.Forms.Button bt_ReadSCCMReport;
        private System.Windows.Forms.Button bt_RemoveTrashFromMP;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button bt_RemoveTrashFromAD;
        private System.Windows.Forms.Button bt_RemoveTrashFromKSC;
        private System.Windows.Forms.DataGridView dgv_ksc;
        private System.Windows.Forms.Button bt_DisplayKSC;
    }
}


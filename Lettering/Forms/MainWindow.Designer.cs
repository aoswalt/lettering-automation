namespace Lettering {
    partial class MainWindow {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.chkEndDate = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.datePickerStart = new System.Windows.Forms.DateTimePicker();
            this.datePickerEnd = new System.Windows.Forms.DateTimePicker();
            this.btnCutAutomation = new System.Windows.Forms.Button();
            this.btnCutReport = new System.Windows.Forms.Button();
            this.btnStoneReport = new System.Windows.Forms.Button();
            this.btnSewReport = new System.Windows.Forms.Button();
            this.btnCsvAutomation = new System.Windows.Forms.Button();
            this.btnCheckSetup = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editConfigsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkEndDate
            // 
            this.chkEndDate.AutoSize = true;
            this.chkEndDate.Location = new System.Drawing.Point(193, 30);
            this.chkEndDate.Name = "chkEndDate";
            this.chkEndDate.Size = new System.Drawing.Size(74, 17);
            this.chkEndDate.TabIndex = 2;
            this.chkEndDate.Text = "End Date:";
            this.chkEndDate.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Start Date:";
            // 
            // datePickerStart
            // 
            this.datePickerStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.datePickerStart.Location = new System.Drawing.Point(85, 27);
            this.datePickerStart.Name = "datePickerStart";
            this.datePickerStart.Size = new System.Drawing.Size(83, 20);
            this.datePickerStart.TabIndex = 4;
            // 
            // datePickerEnd
            // 
            this.datePickerEnd.Enabled = false;
            this.datePickerEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.datePickerEnd.Location = new System.Drawing.Point(273, 27);
            this.datePickerEnd.Name = "datePickerEnd";
            this.datePickerEnd.Size = new System.Drawing.Size(82, 20);
            this.datePickerEnd.TabIndex = 5;
            // 
            // btnCutAutomation
            // 
            this.btnCutAutomation.Location = new System.Drawing.Point(41, 59);
            this.btnCutAutomation.Name = "btnCutAutomation";
            this.btnCutAutomation.Size = new System.Drawing.Size(134, 29);
            this.btnCutAutomation.TabIndex = 6;
            this.btnCutAutomation.Text = "Cut Lettering Automation";
            this.btnCutAutomation.UseVisualStyleBackColor = true;
            this.btnCutAutomation.Click += new System.EventHandler(this.runButton_Click);
            // 
            // btnCutReport
            // 
            this.btnCutReport.Location = new System.Drawing.Point(193, 59);
            this.btnCutReport.Name = "btnCutReport";
            this.btnCutReport.Size = new System.Drawing.Size(134, 29);
            this.btnCutReport.TabIndex = 7;
            this.btnCutReport.Text = "Cut Lettering Report";
            this.btnCutReport.UseVisualStyleBackColor = true;
            // 
            // btnStoneReport
            // 
            this.btnStoneReport.Location = new System.Drawing.Point(193, 129);
            this.btnStoneReport.Name = "btnStoneReport";
            this.btnStoneReport.Size = new System.Drawing.Size(134, 29);
            this.btnStoneReport.TabIndex = 9;
            this.btnStoneReport.Text = "Stone/Sequin Report";
            this.btnStoneReport.UseVisualStyleBackColor = true;
            // 
            // btnSewReport
            // 
            this.btnSewReport.Location = new System.Drawing.Point(193, 94);
            this.btnSewReport.Name = "btnSewReport";
            this.btnSewReport.Size = new System.Drawing.Size(134, 29);
            this.btnSewReport.TabIndex = 8;
            this.btnSewReport.Text = "Sew Report";
            this.btnSewReport.UseVisualStyleBackColor = true;
            // 
            // btnCsvAutomation
            // 
            this.btnCsvAutomation.Location = new System.Drawing.Point(41, 94);
            this.btnCsvAutomation.Name = "btnCsvAutomation";
            this.btnCsvAutomation.Size = new System.Drawing.Size(134, 29);
            this.btnCsvAutomation.TabIndex = 10;
            this.btnCsvAutomation.Text = "CSV Automation";
            this.btnCsvAutomation.UseVisualStyleBackColor = true;
            this.btnCsvAutomation.Click += new System.EventHandler(this.openButton_Click);
            // 
            // btnCheckSetup
            // 
            this.btnCheckSetup.Location = new System.Drawing.Point(61, 132);
            this.btnCheckSetup.Name = "btnCheckSetup";
            this.btnCheckSetup.Size = new System.Drawing.Size(87, 23);
            this.btnCheckSetup.TabIndex = 11;
            this.btnCheckSetup.Text = "Check Setup";
            this.btnCheckSetup.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(385, 24);
            this.menuStrip1.TabIndex = 12;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editConfigsToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // editConfigsToolStripMenuItem
            // 
            this.editConfigsToolStripMenuItem.Name = "editConfigsToolStripMenuItem";
            this.editConfigsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.editConfigsToolStripMenuItem.Text = "Edit &Configs";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(385, 171);
            this.Controls.Add(this.btnCheckSetup);
            this.Controls.Add(this.btnCsvAutomation);
            this.Controls.Add(this.btnStoneReport);
            this.Controls.Add(this.btnSewReport);
            this.Controls.Add(this.btnCutReport);
            this.Controls.Add(this.btnCutAutomation);
            this.Controls.Add(this.datePickerEnd);
            this.Controls.Add(this.datePickerStart);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkEndDate);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lettering";
            this.Load += new System.EventHandler(this.LauncherWindow_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckBox chkEndDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker datePickerStart;
        private System.Windows.Forms.DateTimePicker datePickerEnd;
        private System.Windows.Forms.Button btnCutAutomation;
        private System.Windows.Forms.Button btnCutReport;
        private System.Windows.Forms.Button btnStoneReport;
        private System.Windows.Forms.Button btnSewReport;
        private System.Windows.Forms.Button btnCsvAutomation;
        private System.Windows.Forms.Button btnCheckSetup;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editConfigsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    }
}
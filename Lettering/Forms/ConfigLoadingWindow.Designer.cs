namespace Lettering.Forms {
    partial class ConfigLoadingWindow {
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
            this.label1 = new System.Windows.Forms.Label();
            this.labelFileNumber = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labelLineNumber = new System.Windows.Forms.Label();
            this.progressFiles = new System.Windows.Forms.ProgressBar();
            this.progressLines = new System.Windows.Forms.ProgressBar();
            this.labelFilename = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "File:";
            // 
            // labelFileNumber
            // 
            this.labelFileNumber.AutoSize = true;
            this.labelFileNumber.Location = new System.Drawing.Point(61, 8);
            this.labelFileNumber.Name = "labelFileNumber";
            this.labelFileNumber.Size = new System.Drawing.Size(30, 13);
            this.labelFileNumber.TabIndex = 1;
            this.labelFileNumber.Text = "0 / 0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Line:";
            // 
            // labelLineNumber
            // 
            this.labelLineNumber.AutoSize = true;
            this.labelLineNumber.Location = new System.Drawing.Point(61, 73);
            this.labelLineNumber.Name = "labelLineNumber";
            this.labelLineNumber.Size = new System.Drawing.Size(30, 13);
            this.labelLineNumber.TabIndex = 3;
            this.labelLineNumber.Text = "0 / 0";
            // 
            // progressFiles
            // 
            this.progressFiles.Location = new System.Drawing.Point(12, 43);
            this.progressFiles.Name = "progressFiles";
            this.progressFiles.Size = new System.Drawing.Size(100, 16);
            this.progressFiles.TabIndex = 4;
            // 
            // progressLines
            // 
            this.progressLines.Location = new System.Drawing.Point(12, 89);
            this.progressLines.Name = "progressLines";
            this.progressLines.Size = new System.Drawing.Size(100, 16);
            this.progressLines.TabIndex = 5;
            // 
            // labelFilename
            // 
            this.labelFilename.Location = new System.Drawing.Point(2, 21);
            this.labelFilename.Name = "labelFilename";
            this.labelFilename.Size = new System.Drawing.Size(119, 13);
            this.labelFilename.TabIndex = 6;
            this.labelFilename.Text = "cutLetter.cfg";
            this.labelFilename.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ConfigLoadingWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(124, 117);
            this.ControlBox = false;
            this.Controls.Add(this.labelFilename);
            this.Controls.Add(this.progressLines);
            this.Controls.Add(this.progressFiles);
            this.Controls.Add(this.labelLineNumber);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.labelFileNumber);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigLoadingWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Loading Configs";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelFileNumber;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelLineNumber;
        private System.Windows.Forms.ProgressBar progressFiles;
        private System.Windows.Forms.ProgressBar progressLines;
        private System.Windows.Forms.Label labelFilename;
    }
}
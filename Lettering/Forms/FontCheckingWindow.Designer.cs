namespace Lettering.Forms {
    partial class FontCheckingWindow {
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
            this.progressFonts = new System.Windows.Forms.ProgressBar();
            this.labelFontNumber = new System.Windows.Forms.Label();
            this.labelFontFile = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Font:";
            // 
            // progressFonts
            // 
            this.progressFonts.Location = new System.Drawing.Point(10, 60);
            this.progressFonts.Name = "progressFonts";
            this.progressFonts.Size = new System.Drawing.Size(100, 17);
            this.progressFonts.TabIndex = 0;
            // 
            // labelFontNumber
            // 
            this.labelFontNumber.AutoSize = true;
            this.labelFontNumber.Location = new System.Drawing.Point(59, 16);
            this.labelFontNumber.Name = "labelFontNumber";
            this.labelFontNumber.Size = new System.Drawing.Size(30, 13);
            this.labelFontNumber.TabIndex = 1;
            this.labelFontNumber.Text = "0 / 0";
            // 
            // labelFontFile
            // 
            this.labelFontFile.Location = new System.Drawing.Point(2, 38);
            this.labelFontFile.Name = "labelFontFile";
            this.labelFontFile.Size = new System.Drawing.Size(117, 13);
            this.labelFontFile.TabIndex = 3;
            this.labelFontFile.Text = "fontFile.otf";
            this.labelFontFile.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FontCheckingWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(122, 89);
            this.ControlBox = false;
            this.Controls.Add(this.labelFontFile);
            this.Controls.Add(this.labelFontNumber);
            this.Controls.Add(this.progressFonts);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FontCheckingWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Checking Fonts";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar progressFonts;
        private System.Windows.Forms.Label labelFontNumber;
        private System.Windows.Forms.Label labelFontFile;
    }
}
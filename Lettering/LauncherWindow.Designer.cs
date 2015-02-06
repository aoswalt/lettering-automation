namespace Lettering {
    partial class LauncherWindow {
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
            this.runButton = new System.Windows.Forms.Button();
            this.openButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // runButton
            // 
            this.runButton.Enabled = false;
            this.runButton.Location = new System.Drawing.Point(12, 10);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(75, 23);
            this.runButton.TabIndex = 0;
            this.runButton.Text = "Run Report";
            this.runButton.UseVisualStyleBackColor = true;
            this.runButton.Click += new System.EventHandler(this.runButton_Click);
            // 
            // openButton
            // 
            this.openButton.Location = new System.Drawing.Point(93, 10);
            this.openButton.Name = "openButton";
            this.openButton.Size = new System.Drawing.Size(77, 23);
            this.openButton.TabIndex = 1;
            this.openButton.Text = "Open Report";
            this.openButton.UseVisualStyleBackColor = true;
            this.openButton.Click += new System.EventHandler(this.openButton_Click);
            // 
            // LauncherWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(182, 41);
            this.Controls.Add(this.openButton);
            this.Controls.Add(this.runButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "LauncherWindow";
            this.Text = "Lettering";
            this.Load += new System.EventHandler(this.LauncherWindow_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button runButton;
        private System.Windows.Forms.Button openButton;
    }
}
namespace Lettering.Forms {
    partial class EditorWindow {
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
            this.configTabControl = new System.Windows.Forms.TabControl();
            this.setupTab = new System.Windows.Forms.TabPage();
            this.setupTabControl = new System.Windows.Forms.TabControl();
            this.filePathsTab = new System.Windows.Forms.TabPage();
            this.prefixesTab = new System.Windows.Forms.TabPage();
            this.trimsTab = new System.Windows.Forms.TabPage();
            this.exportsTab = new System.Windows.Forms.TabPage();
            this.styleTypesTab = new System.Windows.Forms.TabPage();
            this.pathRulesTab = new System.Windows.Forms.TabPage();
            this.stylesTab = new System.Windows.Forms.TabPage();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.prefixesDataGrid = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.trimsDataGrid = new System.Windows.Forms.DataGridView();
            this.exportsDataGrid = new System.Windows.Forms.DataGridView();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.pathRulesDataGrid = new System.Windows.Forms.DataGridView();
            this.configTabControl.SuspendLayout();
            this.setupTab.SuspendLayout();
            this.setupTabControl.SuspendLayout();
            this.filePathsTab.SuspendLayout();
            this.prefixesTab.SuspendLayout();
            this.trimsTab.SuspendLayout();
            this.exportsTab.SuspendLayout();
            this.styleTypesTab.SuspendLayout();
            this.pathRulesTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.prefixesDataGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trimsDataGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.exportsDataGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pathRulesDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // configTabControl
            // 
            this.configTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.configTabControl.Controls.Add(this.setupTab);
            this.configTabControl.Controls.Add(this.stylesTab);
            this.configTabControl.Location = new System.Drawing.Point(0, 0);
            this.configTabControl.Multiline = true;
            this.configTabControl.Name = "configTabControl";
            this.configTabControl.SelectedIndex = 0;
            this.configTabControl.Size = new System.Drawing.Size(717, 543);
            this.configTabControl.TabIndex = 0;
            // 
            // setupTab
            // 
            this.setupTab.Controls.Add(this.setupTabControl);
            this.setupTab.Location = new System.Drawing.Point(4, 22);
            this.setupTab.Name = "setupTab";
            this.setupTab.Padding = new System.Windows.Forms.Padding(3);
            this.setupTab.Size = new System.Drawing.Size(709, 517);
            this.setupTab.TabIndex = 0;
            this.setupTab.Text = "Setup";
            this.setupTab.UseVisualStyleBackColor = true;
            // 
            // setupTabControl
            // 
            this.setupTabControl.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.setupTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.setupTabControl.Controls.Add(this.filePathsTab);
            this.setupTabControl.Controls.Add(this.prefixesTab);
            this.setupTabControl.Controls.Add(this.trimsTab);
            this.setupTabControl.Controls.Add(this.exportsTab);
            this.setupTabControl.Controls.Add(this.styleTypesTab);
            this.setupTabControl.Controls.Add(this.pathRulesTab);
            this.setupTabControl.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.setupTabControl.HotTrack = true;
            this.setupTabControl.ItemSize = new System.Drawing.Size(25, 80);
            this.setupTabControl.Location = new System.Drawing.Point(0, 0);
            this.setupTabControl.Multiline = true;
            this.setupTabControl.Name = "setupTabControl";
            this.setupTabControl.SelectedIndex = 0;
            this.setupTabControl.Size = new System.Drawing.Size(709, 517);
            this.setupTabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.setupTabControl.TabIndex = 0;
            this.setupTabControl.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.setupTabControl_DrawItem);
            // 
            // filePathsTab
            // 
            this.filePathsTab.Controls.Add(this.textBox3);
            this.filePathsTab.Controls.Add(this.label3);
            this.filePathsTab.Controls.Add(this.textBox2);
            this.filePathsTab.Controls.Add(this.label2);
            this.filePathsTab.Controls.Add(this.textBox1);
            this.filePathsTab.Controls.Add(this.label1);
            this.filePathsTab.Location = new System.Drawing.Point(84, 4);
            this.filePathsTab.Name = "filePathsTab";
            this.filePathsTab.Padding = new System.Windows.Forms.Padding(3);
            this.filePathsTab.Size = new System.Drawing.Size(621, 509);
            this.filePathsTab.TabIndex = 0;
            this.filePathsTab.Text = "File Paths";
            this.filePathsTab.UseVisualStyleBackColor = true;
            // 
            // prefixesTab
            // 
            this.prefixesTab.Controls.Add(this.prefixesDataGrid);
            this.prefixesTab.Location = new System.Drawing.Point(84, 4);
            this.prefixesTab.Name = "prefixesTab";
            this.prefixesTab.Padding = new System.Windows.Forms.Padding(3);
            this.prefixesTab.Size = new System.Drawing.Size(621, 509);
            this.prefixesTab.TabIndex = 1;
            this.prefixesTab.Text = "Prefixes";
            this.prefixesTab.UseVisualStyleBackColor = true;
            // 
            // trimsTab
            // 
            this.trimsTab.Controls.Add(this.trimsDataGrid);
            this.trimsTab.Location = new System.Drawing.Point(84, 4);
            this.trimsTab.Name = "trimsTab";
            this.trimsTab.Padding = new System.Windows.Forms.Padding(3);
            this.trimsTab.Size = new System.Drawing.Size(621, 509);
            this.trimsTab.TabIndex = 2;
            this.trimsTab.Text = "Trims";
            this.trimsTab.UseVisualStyleBackColor = true;
            // 
            // exportsTab
            // 
            this.exportsTab.Controls.Add(this.exportsDataGrid);
            this.exportsTab.Location = new System.Drawing.Point(84, 4);
            this.exportsTab.Name = "exportsTab";
            this.exportsTab.Padding = new System.Windows.Forms.Padding(3);
            this.exportsTab.Size = new System.Drawing.Size(621, 509);
            this.exportsTab.TabIndex = 3;
            this.exportsTab.Text = "Exports";
            this.exportsTab.UseVisualStyleBackColor = true;
            // 
            // styleTypesTab
            // 
            this.styleTypesTab.Controls.Add(this.label10);
            this.styleTypesTab.Controls.Add(this.label11);
            this.styleTypesTab.Controls.Add(this.label12);
            this.styleTypesTab.Controls.Add(this.textBox7);
            this.styleTypesTab.Controls.Add(this.label7);
            this.styleTypesTab.Controls.Add(this.textBox8);
            this.styleTypesTab.Controls.Add(this.label8);
            this.styleTypesTab.Controls.Add(this.textBox9);
            this.styleTypesTab.Controls.Add(this.label9);
            this.styleTypesTab.Controls.Add(this.textBox4);
            this.styleTypesTab.Controls.Add(this.label4);
            this.styleTypesTab.Controls.Add(this.textBox5);
            this.styleTypesTab.Controls.Add(this.label5);
            this.styleTypesTab.Controls.Add(this.textBox6);
            this.styleTypesTab.Controls.Add(this.label6);
            this.styleTypesTab.Location = new System.Drawing.Point(84, 4);
            this.styleTypesTab.Name = "styleTypesTab";
            this.styleTypesTab.Padding = new System.Windows.Forms.Padding(3);
            this.styleTypesTab.Size = new System.Drawing.Size(621, 509);
            this.styleTypesTab.TabIndex = 4;
            this.styleTypesTab.Text = "Style Types";
            this.styleTypesTab.UseVisualStyleBackColor = true;
            // 
            // pathRulesTab
            // 
            this.pathRulesTab.Controls.Add(this.pathRulesDataGrid);
            this.pathRulesTab.Location = new System.Drawing.Point(84, 4);
            this.pathRulesTab.Name = "pathRulesTab";
            this.pathRulesTab.Padding = new System.Windows.Forms.Padding(3);
            this.pathRulesTab.Size = new System.Drawing.Size(621, 509);
            this.pathRulesTab.TabIndex = 5;
            this.pathRulesTab.Text = "Path Rules";
            this.pathRulesTab.UseVisualStyleBackColor = true;
            // 
            // stylesTab
            // 
            this.stylesTab.Location = new System.Drawing.Point(4, 22);
            this.stylesTab.Name = "stylesTab";
            this.stylesTab.Padding = new System.Windows.Forms.Padding(3);
            this.stylesTab.Size = new System.Drawing.Size(709, 517);
            this.stylesTab.TabIndex = 1;
            this.stylesTab.Text = "Styles";
            this.stylesTab.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 546);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(720, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // prefixesDataGrid
            // 
            this.prefixesDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.prefixesDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.prefixesDataGrid.Location = new System.Drawing.Point(3, 3);
            this.prefixesDataGrid.Name = "prefixesDataGrid";
            this.prefixesDataGrid.Size = new System.Drawing.Size(615, 503);
            this.prefixesDataGrid.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(51, 10);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 1;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(51, 36);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "label2";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(51, 62);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(100, 20);
            this.textBox3.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "label3";
            // 
            // trimsDataGrid
            // 
            this.trimsDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.trimsDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trimsDataGrid.Location = new System.Drawing.Point(3, 3);
            this.trimsDataGrid.Name = "trimsDataGrid";
            this.trimsDataGrid.Size = new System.Drawing.Size(615, 503);
            this.trimsDataGrid.TabIndex = 0;
            // 
            // exportsDataGrid
            // 
            this.exportsDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.exportsDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.exportsDataGrid.Location = new System.Drawing.Point(3, 3);
            this.exportsDataGrid.Name = "exportsDataGrid";
            this.exportsDataGrid.Size = new System.Drawing.Size(615, 503);
            this.exportsDataGrid.TabIndex = 0;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(151, 138);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(100, 20);
            this.textBox4.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(110, 141);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "label4";
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(151, 100);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(100, 20);
            this.textBox5.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(110, 103);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "label5";
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(151, 74);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(100, 20);
            this.textBox6.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(110, 77);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "label6";
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(151, 230);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(100, 20);
            this.textBox7.TabIndex = 17;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(110, 233);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "label7";
            // 
            // textBox8
            // 
            this.textBox8.Location = new System.Drawing.Point(151, 204);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(100, 20);
            this.textBox8.TabIndex = 15;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(110, 207);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "label8";
            // 
            // textBox9
            // 
            this.textBox9.Location = new System.Drawing.Point(151, 164);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(100, 20);
            this.textBox9.TabIndex = 13;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(110, 167);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 13);
            this.label9.TabIndex = 12;
            this.label9.Text = "label9";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(48, 204);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 13);
            this.label10.TabIndex = 20;
            this.label10.Text = "label10";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(48, 138);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(41, 13);
            this.label11.TabIndex = 19;
            this.label11.Text = "label11";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(48, 74);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(41, 13);
            this.label12.TabIndex = 18;
            this.label12.Text = "label12";
            // 
            // pathRulesDataGrid
            // 
            this.pathRulesDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.pathRulesDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pathRulesDataGrid.Location = new System.Drawing.Point(3, 3);
            this.pathRulesDataGrid.Name = "pathRulesDataGrid";
            this.pathRulesDataGrid.Size = new System.Drawing.Size(615, 503);
            this.pathRulesDataGrid.TabIndex = 0;
            // 
            // EditorWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(720, 568);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.configTabControl);
            this.Name = "EditorWindow";
            this.Text = "Editor";
            this.configTabControl.ResumeLayout(false);
            this.setupTab.ResumeLayout(false);
            this.setupTabControl.ResumeLayout(false);
            this.filePathsTab.ResumeLayout(false);
            this.filePathsTab.PerformLayout();
            this.prefixesTab.ResumeLayout(false);
            this.trimsTab.ResumeLayout(false);
            this.exportsTab.ResumeLayout(false);
            this.styleTypesTab.ResumeLayout(false);
            this.styleTypesTab.PerformLayout();
            this.pathRulesTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.prefixesDataGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trimsDataGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.exportsDataGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pathRulesDataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl configTabControl;
        private System.Windows.Forms.TabPage setupTab;
        private System.Windows.Forms.TabControl setupTabControl;
        private System.Windows.Forms.TabPage filePathsTab;
        private System.Windows.Forms.TabPage prefixesTab;
        private System.Windows.Forms.TabPage trimsTab;
        private System.Windows.Forms.TabPage exportsTab;
        private System.Windows.Forms.TabPage styleTypesTab;
        private System.Windows.Forms.TabPage pathRulesTab;
        private System.Windows.Forms.TabPage stylesTab;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.DataGridView prefixesDataGrid;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView trimsDataGrid;
        private System.Windows.Forms.DataGridView exportsDataGrid;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView pathRulesDataGrid;
    }
}
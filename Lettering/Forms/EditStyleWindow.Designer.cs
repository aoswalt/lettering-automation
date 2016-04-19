namespace Lettering.Forms {
    partial class EditStyleWindow {
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
            this.textBoxStyle = new System.Windows.Forms.TextBox();
            this.groupBoxCut = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonCutResetWords = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonCutResetMirror = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxCutMirror = new System.Windows.Forms.TextBox();
            this.cboxCutRule = new System.Windows.Forms.ComboBox();
            this.cboxCutWordOrder1 = new System.Windows.Forms.ComboBox();
            this.cboxCutWordOrder2 = new System.Windows.Forms.ComboBox();
            this.cboxCutWordOrder3 = new System.Windows.Forms.ComboBox();
            this.cboxCutWordOrder4 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dataGridCutExceptions = new System.Windows.Forms.DataGridView();
            this.groupBoxSew = new System.Windows.Forms.GroupBox();
            this.dataGridSewExceptions = new System.Windows.Forms.DataGridView();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cboxSewWordOrder4 = new System.Windows.Forms.ComboBox();
            this.cboxSewWordOrder3 = new System.Windows.Forms.ComboBox();
            this.cboxSewWordOrder2 = new System.Windows.Forms.ComboBox();
            this.cboxSewWordOrder1 = new System.Windows.Forms.ComboBox();
            this.cboxSewRule = new System.Windows.Forms.ComboBox();
            this.textBoxSewMirror = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.buttonSewResetMirror = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.buttonSewResetWords = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBoxStone = new System.Windows.Forms.GroupBox();
            this.dataGridStoneExceptions = new System.Windows.Forms.DataGridView();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.cboxStoneWordOrder4 = new System.Windows.Forms.ComboBox();
            this.cboxStoneWordOrder3 = new System.Windows.Forms.ComboBox();
            this.cboxStoneWordOrder2 = new System.Windows.Forms.ComboBox();
            this.cboxStoneWordOrder1 = new System.Windows.Forms.ComboBox();
            this.cboxStoneRule = new System.Windows.Forms.ComboBox();
            this.textBoxStoneMirror = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.buttonStoneResetMirror = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.buttonStoneResetWords = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.buttonAccept = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBoxCut.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridCutExceptions)).BeginInit();
            this.groupBoxSew.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridSewExceptions)).BeginInit();
            this.groupBoxStone.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridStoneExceptions)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Style";
            // 
            // textBoxStyle
            // 
            this.textBoxStyle.Location = new System.Drawing.Point(51, 6);
            this.textBoxStyle.Name = "textBoxStyle";
            this.textBoxStyle.Size = new System.Drawing.Size(273, 20);
            this.textBoxStyle.TabIndex = 1;
            // 
            // groupBoxCut
            // 
            this.groupBoxCut.Controls.Add(this.dataGridCutExceptions);
            this.groupBoxCut.Controls.Add(this.label6);
            this.groupBoxCut.Controls.Add(this.label5);
            this.groupBoxCut.Controls.Add(this.cboxCutWordOrder4);
            this.groupBoxCut.Controls.Add(this.cboxCutWordOrder3);
            this.groupBoxCut.Controls.Add(this.cboxCutWordOrder2);
            this.groupBoxCut.Controls.Add(this.cboxCutWordOrder1);
            this.groupBoxCut.Controls.Add(this.cboxCutRule);
            this.groupBoxCut.Controls.Add(this.textBoxCutMirror);
            this.groupBoxCut.Controls.Add(this.label4);
            this.groupBoxCut.Controls.Add(this.buttonCutResetMirror);
            this.groupBoxCut.Controls.Add(this.label3);
            this.groupBoxCut.Controls.Add(this.buttonCutResetWords);
            this.groupBoxCut.Controls.Add(this.label2);
            this.groupBoxCut.Location = new System.Drawing.Point(12, 32);
            this.groupBoxCut.Name = "groupBoxCut";
            this.groupBoxCut.Size = new System.Drawing.Size(514, 113);
            this.groupBoxCut.TabIndex = 2;
            this.groupBoxCut.TabStop = false;
            this.groupBoxCut.Text = "Cut";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Rule";
            // 
            // buttonCutResetWords
            // 
            this.buttonCutResetWords.Location = new System.Drawing.Point(6, 41);
            this.buttonCutResetWords.Name = "buttonCutResetWords";
            this.buttonCutResetWords.Size = new System.Drawing.Size(19, 23);
            this.buttonCutResetWords.TabIndex = 1;
            this.buttonCutResetWords.Text = "X";
            this.buttonCutResetWords.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Word Order";
            // 
            // buttonCutResetMirror
            // 
            this.buttonCutResetMirror.Location = new System.Drawing.Point(6, 70);
            this.buttonCutResetMirror.Name = "buttonCutResetMirror";
            this.buttonCutResetMirror.Size = new System.Drawing.Size(19, 23);
            this.buttonCutResetMirror.TabIndex = 4;
            this.buttonCutResetMirror.Text = "X";
            this.buttonCutResetMirror.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(31, 75);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Mirror";
            // 
            // textBoxCutMirror
            // 
            this.textBoxCutMirror.Location = new System.Drawing.Point(66, 72);
            this.textBoxCutMirror.Name = "textBoxCutMirror";
            this.textBoxCutMirror.Size = new System.Drawing.Size(156, 20);
            this.textBoxCutMirror.TabIndex = 6;
            // 
            // cboxCutRule
            // 
            this.cboxCutRule.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxCutRule.FormattingEnabled = true;
            this.cboxCutRule.Location = new System.Drawing.Point(66, 17);
            this.cboxCutRule.Name = "cboxCutRule";
            this.cboxCutRule.Size = new System.Drawing.Size(156, 21);
            this.cboxCutRule.TabIndex = 7;
            // 
            // cboxCutWordOrder1
            // 
            this.cboxCutWordOrder1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxCutWordOrder1.FormattingEnabled = true;
            this.cboxCutWordOrder1.Location = new System.Drawing.Point(96, 44);
            this.cboxCutWordOrder1.Name = "cboxCutWordOrder1";
            this.cboxCutWordOrder1.Size = new System.Drawing.Size(27, 21);
            this.cboxCutWordOrder1.TabIndex = 8;
            // 
            // cboxCutWordOrder2
            // 
            this.cboxCutWordOrder2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxCutWordOrder2.FormattingEnabled = true;
            this.cboxCutWordOrder2.Location = new System.Drawing.Point(129, 44);
            this.cboxCutWordOrder2.Name = "cboxCutWordOrder2";
            this.cboxCutWordOrder2.Size = new System.Drawing.Size(27, 21);
            this.cboxCutWordOrder2.TabIndex = 9;
            // 
            // cboxCutWordOrder3
            // 
            this.cboxCutWordOrder3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxCutWordOrder3.FormattingEnabled = true;
            this.cboxCutWordOrder3.Location = new System.Drawing.Point(162, 44);
            this.cboxCutWordOrder3.Name = "cboxCutWordOrder3";
            this.cboxCutWordOrder3.Size = new System.Drawing.Size(27, 21);
            this.cboxCutWordOrder3.TabIndex = 10;
            // 
            // cboxCutWordOrder4
            // 
            this.cboxCutWordOrder4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxCutWordOrder4.FormattingEnabled = true;
            this.cboxCutWordOrder4.Location = new System.Drawing.Point(195, 44);
            this.cboxCutWordOrder4.Name = "cboxCutWordOrder4";
            this.cboxCutWordOrder4.Size = new System.Drawing.Size(27, 21);
            this.cboxCutWordOrder4.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(253, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Exceptions";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(253, 32);
            this.label6.Name = "label6";
            this.label6.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label6.Size = new System.Drawing.Size(24, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "info";
            // 
            // dataGridCutExceptions
            // 
            this.dataGridCutExceptions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridCutExceptions.Location = new System.Drawing.Point(314, 19);
            this.dataGridCutExceptions.Name = "dataGridCutExceptions";
            this.dataGridCutExceptions.Size = new System.Drawing.Size(192, 86);
            this.dataGridCutExceptions.TabIndex = 14;
            // 
            // groupBoxSew
            // 
            this.groupBoxSew.Controls.Add(this.dataGridSewExceptions);
            this.groupBoxSew.Controls.Add(this.label7);
            this.groupBoxSew.Controls.Add(this.label8);
            this.groupBoxSew.Controls.Add(this.cboxSewWordOrder4);
            this.groupBoxSew.Controls.Add(this.cboxSewWordOrder3);
            this.groupBoxSew.Controls.Add(this.cboxSewWordOrder2);
            this.groupBoxSew.Controls.Add(this.cboxSewWordOrder1);
            this.groupBoxSew.Controls.Add(this.cboxSewRule);
            this.groupBoxSew.Controls.Add(this.textBoxSewMirror);
            this.groupBoxSew.Controls.Add(this.label9);
            this.groupBoxSew.Controls.Add(this.buttonSewResetMirror);
            this.groupBoxSew.Controls.Add(this.label10);
            this.groupBoxSew.Controls.Add(this.buttonSewResetWords);
            this.groupBoxSew.Controls.Add(this.label11);
            this.groupBoxSew.Location = new System.Drawing.Point(12, 151);
            this.groupBoxSew.Name = "groupBoxSew";
            this.groupBoxSew.Size = new System.Drawing.Size(514, 113);
            this.groupBoxSew.TabIndex = 15;
            this.groupBoxSew.TabStop = false;
            this.groupBoxSew.Text = "Sew";
            // 
            // dataGridSewExceptions
            // 
            this.dataGridSewExceptions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridSewExceptions.Location = new System.Drawing.Point(314, 19);
            this.dataGridSewExceptions.Name = "dataGridSewExceptions";
            this.dataGridSewExceptions.Size = new System.Drawing.Size(192, 86);
            this.dataGridSewExceptions.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(253, 32);
            this.label7.Name = "label7";
            this.label7.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label7.Size = new System.Drawing.Size(24, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "info";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(253, 19);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 13);
            this.label8.TabIndex = 12;
            this.label8.Text = "Exceptions";
            // 
            // cboxSewWordOrder4
            // 
            this.cboxSewWordOrder4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxSewWordOrder4.FormattingEnabled = true;
            this.cboxSewWordOrder4.Location = new System.Drawing.Point(195, 44);
            this.cboxSewWordOrder4.Name = "cboxSewWordOrder4";
            this.cboxSewWordOrder4.Size = new System.Drawing.Size(27, 21);
            this.cboxSewWordOrder4.TabIndex = 11;
            // 
            // cboxSewWordOrder3
            // 
            this.cboxSewWordOrder3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxSewWordOrder3.FormattingEnabled = true;
            this.cboxSewWordOrder3.Location = new System.Drawing.Point(162, 44);
            this.cboxSewWordOrder3.Name = "cboxSewWordOrder3";
            this.cboxSewWordOrder3.Size = new System.Drawing.Size(27, 21);
            this.cboxSewWordOrder3.TabIndex = 10;
            // 
            // cboxSewWordOrder2
            // 
            this.cboxSewWordOrder2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxSewWordOrder2.FormattingEnabled = true;
            this.cboxSewWordOrder2.Location = new System.Drawing.Point(129, 44);
            this.cboxSewWordOrder2.Name = "cboxSewWordOrder2";
            this.cboxSewWordOrder2.Size = new System.Drawing.Size(27, 21);
            this.cboxSewWordOrder2.TabIndex = 9;
            // 
            // cboxSewWordOrder1
            // 
            this.cboxSewWordOrder1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxSewWordOrder1.FormattingEnabled = true;
            this.cboxSewWordOrder1.Location = new System.Drawing.Point(96, 44);
            this.cboxSewWordOrder1.Name = "cboxSewWordOrder1";
            this.cboxSewWordOrder1.Size = new System.Drawing.Size(27, 21);
            this.cboxSewWordOrder1.TabIndex = 8;
            // 
            // cboxSewRule
            // 
            this.cboxSewRule.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxSewRule.FormattingEnabled = true;
            this.cboxSewRule.Location = new System.Drawing.Point(66, 17);
            this.cboxSewRule.Name = "cboxSewRule";
            this.cboxSewRule.Size = new System.Drawing.Size(156, 21);
            this.cboxSewRule.TabIndex = 7;
            // 
            // textBoxSewMirror
            // 
            this.textBoxSewMirror.Location = new System.Drawing.Point(66, 72);
            this.textBoxSewMirror.Name = "textBoxSewMirror";
            this.textBoxSewMirror.Size = new System.Drawing.Size(156, 20);
            this.textBoxSewMirror.TabIndex = 6;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(31, 75);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(33, 13);
            this.label9.TabIndex = 5;
            this.label9.Text = "Mirror";
            // 
            // buttonSewResetMirror
            // 
            this.buttonSewResetMirror.Location = new System.Drawing.Point(6, 70);
            this.buttonSewResetMirror.Name = "buttonSewResetMirror";
            this.buttonSewResetMirror.Size = new System.Drawing.Size(19, 23);
            this.buttonSewResetMirror.TabIndex = 4;
            this.buttonSewResetMirror.Text = "X";
            this.buttonSewResetMirror.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(28, 46);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(62, 13);
            this.label10.TabIndex = 3;
            this.label10.Text = "Word Order";
            // 
            // buttonSewResetWords
            // 
            this.buttonSewResetWords.Location = new System.Drawing.Point(6, 41);
            this.buttonSewResetWords.Name = "buttonSewResetWords";
            this.buttonSewResetWords.Size = new System.Drawing.Size(19, 23);
            this.buttonSewResetWords.TabIndex = 1;
            this.buttonSewResetWords.Text = "X";
            this.buttonSewResetWords.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(31, 20);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(29, 13);
            this.label11.TabIndex = 0;
            this.label11.Text = "Rule";
            // 
            // groupBoxStone
            // 
            this.groupBoxStone.Controls.Add(this.dataGridStoneExceptions);
            this.groupBoxStone.Controls.Add(this.label12);
            this.groupBoxStone.Controls.Add(this.label13);
            this.groupBoxStone.Controls.Add(this.cboxStoneWordOrder4);
            this.groupBoxStone.Controls.Add(this.cboxStoneWordOrder3);
            this.groupBoxStone.Controls.Add(this.cboxStoneWordOrder2);
            this.groupBoxStone.Controls.Add(this.cboxStoneWordOrder1);
            this.groupBoxStone.Controls.Add(this.cboxStoneRule);
            this.groupBoxStone.Controls.Add(this.textBoxStoneMirror);
            this.groupBoxStone.Controls.Add(this.label14);
            this.groupBoxStone.Controls.Add(this.buttonStoneResetMirror);
            this.groupBoxStone.Controls.Add(this.label15);
            this.groupBoxStone.Controls.Add(this.buttonStoneResetWords);
            this.groupBoxStone.Controls.Add(this.label16);
            this.groupBoxStone.Location = new System.Drawing.Point(12, 270);
            this.groupBoxStone.Name = "groupBoxStone";
            this.groupBoxStone.Size = new System.Drawing.Size(514, 113);
            this.groupBoxStone.TabIndex = 15;
            this.groupBoxStone.TabStop = false;
            this.groupBoxStone.Text = "Stone";
            // 
            // dataGridStoneExceptions
            // 
            this.dataGridStoneExceptions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridStoneExceptions.Location = new System.Drawing.Point(314, 19);
            this.dataGridStoneExceptions.Name = "dataGridStoneExceptions";
            this.dataGridStoneExceptions.Size = new System.Drawing.Size(192, 86);
            this.dataGridStoneExceptions.TabIndex = 14;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(253, 32);
            this.label12.Name = "label12";
            this.label12.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label12.Size = new System.Drawing.Size(24, 13);
            this.label12.TabIndex = 13;
            this.label12.Text = "info";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(253, 19);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(59, 13);
            this.label13.TabIndex = 12;
            this.label13.Text = "Exceptions";
            // 
            // cboxStoneWordOrder4
            // 
            this.cboxStoneWordOrder4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxStoneWordOrder4.FormattingEnabled = true;
            this.cboxStoneWordOrder4.Location = new System.Drawing.Point(195, 44);
            this.cboxStoneWordOrder4.Name = "cboxStoneWordOrder4";
            this.cboxStoneWordOrder4.Size = new System.Drawing.Size(27, 21);
            this.cboxStoneWordOrder4.TabIndex = 11;
            // 
            // cboxStoneWordOrder3
            // 
            this.cboxStoneWordOrder3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxStoneWordOrder3.FormattingEnabled = true;
            this.cboxStoneWordOrder3.Location = new System.Drawing.Point(162, 44);
            this.cboxStoneWordOrder3.Name = "cboxStoneWordOrder3";
            this.cboxStoneWordOrder3.Size = new System.Drawing.Size(27, 21);
            this.cboxStoneWordOrder3.TabIndex = 10;
            // 
            // cboxStoneWordOrder2
            // 
            this.cboxStoneWordOrder2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxStoneWordOrder2.FormattingEnabled = true;
            this.cboxStoneWordOrder2.Location = new System.Drawing.Point(129, 44);
            this.cboxStoneWordOrder2.Name = "cboxStoneWordOrder2";
            this.cboxStoneWordOrder2.Size = new System.Drawing.Size(27, 21);
            this.cboxStoneWordOrder2.TabIndex = 9;
            // 
            // cboxStoneWordOrder1
            // 
            this.cboxStoneWordOrder1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxStoneWordOrder1.FormattingEnabled = true;
            this.cboxStoneWordOrder1.Location = new System.Drawing.Point(96, 44);
            this.cboxStoneWordOrder1.Name = "cboxStoneWordOrder1";
            this.cboxStoneWordOrder1.Size = new System.Drawing.Size(27, 21);
            this.cboxStoneWordOrder1.TabIndex = 8;
            // 
            // cboxStoneRule
            // 
            this.cboxStoneRule.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxStoneRule.FormattingEnabled = true;
            this.cboxStoneRule.Location = new System.Drawing.Point(66, 17);
            this.cboxStoneRule.Name = "cboxStoneRule";
            this.cboxStoneRule.Size = new System.Drawing.Size(156, 21);
            this.cboxStoneRule.TabIndex = 7;
            // 
            // textBoxStoneMirror
            // 
            this.textBoxStoneMirror.Location = new System.Drawing.Point(66, 72);
            this.textBoxStoneMirror.Name = "textBoxStoneMirror";
            this.textBoxStoneMirror.Size = new System.Drawing.Size(156, 20);
            this.textBoxStoneMirror.TabIndex = 6;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(31, 75);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(33, 13);
            this.label14.TabIndex = 5;
            this.label14.Text = "Mirror";
            // 
            // buttonStoneResetMirror
            // 
            this.buttonStoneResetMirror.Location = new System.Drawing.Point(6, 70);
            this.buttonStoneResetMirror.Name = "buttonStoneResetMirror";
            this.buttonStoneResetMirror.Size = new System.Drawing.Size(19, 23);
            this.buttonStoneResetMirror.TabIndex = 4;
            this.buttonStoneResetMirror.Text = "X";
            this.buttonStoneResetMirror.UseVisualStyleBackColor = true;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(28, 46);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(62, 13);
            this.label15.TabIndex = 3;
            this.label15.Text = "Word Order";
            // 
            // buttonStoneResetWords
            // 
            this.buttonStoneResetWords.Location = new System.Drawing.Point(6, 41);
            this.buttonStoneResetWords.Name = "buttonStoneResetWords";
            this.buttonStoneResetWords.Size = new System.Drawing.Size(19, 23);
            this.buttonStoneResetWords.TabIndex = 1;
            this.buttonStoneResetWords.Text = "X";
            this.buttonStoneResetWords.UseVisualStyleBackColor = true;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(31, 20);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(29, 13);
            this.label16.TabIndex = 0;
            this.label16.Text = "Rule";
            // 
            // buttonAccept
            // 
            this.buttonAccept.Location = new System.Drawing.Point(18, 389);
            this.buttonAccept.Name = "buttonAccept";
            this.buttonAccept.Size = new System.Drawing.Size(75, 23);
            this.buttonAccept.TabIndex = 16;
            this.buttonAccept.Text = "Accept";
            this.buttonAccept.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(99, 389);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 17;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // EditStyleWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(539, 421);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonAccept);
            this.Controls.Add(this.groupBoxStone);
            this.Controls.Add(this.groupBoxSew);
            this.Controls.Add(this.groupBoxCut);
            this.Controls.Add(this.textBoxStyle);
            this.Controls.Add(this.label1);
            this.Name = "EditStyleWindow";
            this.Text = "Edit Style";
            this.groupBoxCut.ResumeLayout(false);
            this.groupBoxCut.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridCutExceptions)).EndInit();
            this.groupBoxSew.ResumeLayout(false);
            this.groupBoxSew.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridSewExceptions)).EndInit();
            this.groupBoxStone.ResumeLayout(false);
            this.groupBoxStone.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridStoneExceptions)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxStyle;
        private System.Windows.Forms.GroupBox groupBoxCut;
        private System.Windows.Forms.DataGridView dataGridCutExceptions;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboxCutWordOrder4;
        private System.Windows.Forms.ComboBox cboxCutWordOrder3;
        private System.Windows.Forms.ComboBox cboxCutWordOrder2;
        private System.Windows.Forms.ComboBox cboxCutWordOrder1;
        private System.Windows.Forms.ComboBox cboxCutRule;
        private System.Windows.Forms.TextBox textBoxCutMirror;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonCutResetMirror;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonCutResetWords;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBoxSew;
        private System.Windows.Forms.DataGridView dataGridSewExceptions;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cboxSewWordOrder4;
        private System.Windows.Forms.ComboBox cboxSewWordOrder3;
        private System.Windows.Forms.ComboBox cboxSewWordOrder2;
        private System.Windows.Forms.ComboBox cboxSewWordOrder1;
        private System.Windows.Forms.ComboBox cboxSewRule;
        private System.Windows.Forms.TextBox textBoxSewMirror;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button buttonSewResetMirror;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button buttonSewResetWords;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox groupBoxStone;
        private System.Windows.Forms.DataGridView dataGridStoneExceptions;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cboxStoneWordOrder4;
        private System.Windows.Forms.ComboBox cboxStoneWordOrder3;
        private System.Windows.Forms.ComboBox cboxStoneWordOrder2;
        private System.Windows.Forms.ComboBox cboxStoneWordOrder1;
        private System.Windows.Forms.ComboBox cboxStoneRule;
        private System.Windows.Forms.TextBox textBoxStoneMirror;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button buttonStoneResetMirror;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button buttonStoneResetWords;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button buttonAccept;
        private System.Windows.Forms.Button buttonCancel;
    }
}
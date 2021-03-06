﻿namespace Lettering.Forms {
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
            this.tabControlConfig = new System.Windows.Forms.TabControl();
            this.tabSetup = new System.Windows.Forms.TabPage();
            this.tabControlSetup = new System.Windows.Forms.TabControl();
            this.tabFilePaths = new System.Windows.Forms.TabPage();
            this.buttonFilePathsHelp = new System.Windows.Forms.Button();
            this.textBoxInstalledLibraryFile = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxNetworkLibraryFile = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxNetworkFontsFolder = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPrefixes = new System.Windows.Forms.TabPage();
            this.buttonPrefixesHelp = new System.Windows.Forms.Button();
            this.buttonPrefixesDown = new System.Windows.Forms.Button();
            this.buttonPrefixesUp = new System.Windows.Forms.Button();
            this.buttonPrefixesRemove = new System.Windows.Forms.Button();
            this.buttonPrefixesAdd = new System.Windows.Forms.Button();
            this.dataGridPrefixes = new System.Windows.Forms.DataGridView();
            this.tabTrims = new System.Windows.Forms.TabPage();
            this.buttonTrimsDown = new System.Windows.Forms.Button();
            this.buttonTrimsUp = new System.Windows.Forms.Button();
            this.buttonTrimsHelp = new System.Windows.Forms.Button();
            this.buttonTrimsRemove = new System.Windows.Forms.Button();
            this.buttonTrimsAdd = new System.Windows.Forms.Button();
            this.dataGridTrims = new System.Windows.Forms.DataGridView();
            this.tabExports = new System.Windows.Forms.TabPage();
            this.buttonExportsHelp = new System.Windows.Forms.Button();
            this.buttonExportsRemove = new System.Windows.Forms.Button();
            this.buttonExportsAdd = new System.Windows.Forms.Button();
            this.dataGridExports = new System.Windows.Forms.DataGridView();
            this.tabStyleTypes = new System.Windows.Forms.TabPage();
            this.buttonSetupHelp = new System.Windows.Forms.Button();
            this.textBoxStoneExtension = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxStoneRoot = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxSewExtension = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxSewRoot = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.textBoxCutExtension = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxCutRoot = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tabPathRules = new System.Windows.Forms.TabPage();
            this.buttonPathRulesHelp = new System.Windows.Forms.Button();
            this.buttonPathRulesRemove = new System.Windows.Forms.Button();
            this.buttonPathRulesAdd = new System.Windows.Forms.Button();
            this.dataGridPathRules = new System.Windows.Forms.DataGridView();
            this.tabStyles = new System.Windows.Forms.TabPage();
            this.buttonStylesHelp = new System.Windows.Forms.Button();
            this.buttonStylesRemove = new System.Windows.Forms.Button();
            this.buttonStylesAdd = new System.Windows.Forms.Button();
            this.treeViewStyles = new System.Windows.Forms.TreeView();
            this.statusStripEditor = new System.Windows.Forms.StatusStrip();
            this.buttonDone = new System.Windows.Forms.Button();
            this.buttonInfo = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.tabControlConfig.SuspendLayout();
            this.tabSetup.SuspendLayout();
            this.tabControlSetup.SuspendLayout();
            this.tabFilePaths.SuspendLayout();
            this.tabPrefixes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridPrefixes)).BeginInit();
            this.tabTrims.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridTrims)).BeginInit();
            this.tabExports.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridExports)).BeginInit();
            this.tabStyleTypes.SuspendLayout();
            this.tabPathRules.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridPathRules)).BeginInit();
            this.tabStyles.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlConfig
            // 
            this.tabControlConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlConfig.Controls.Add(this.tabSetup);
            this.tabControlConfig.Controls.Add(this.tabStyles);
            this.tabControlConfig.Location = new System.Drawing.Point(0, 0);
            this.tabControlConfig.Multiline = true;
            this.tabControlConfig.Name = "tabControlConfig";
            this.tabControlConfig.SelectedIndex = 0;
            this.tabControlConfig.Size = new System.Drawing.Size(717, 510);
            this.tabControlConfig.TabIndex = 0;
            // 
            // tabSetup
            // 
            this.tabSetup.Controls.Add(this.tabControlSetup);
            this.tabSetup.Location = new System.Drawing.Point(4, 22);
            this.tabSetup.Name = "tabSetup";
            this.tabSetup.Padding = new System.Windows.Forms.Padding(3);
            this.tabSetup.Size = new System.Drawing.Size(709, 484);
            this.tabSetup.TabIndex = 0;
            this.tabSetup.Text = "Setup";
            this.tabSetup.UseVisualStyleBackColor = true;
            // 
            // tabControlSetup
            // 
            this.tabControlSetup.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.tabControlSetup.Controls.Add(this.tabFilePaths);
            this.tabControlSetup.Controls.Add(this.tabPrefixes);
            this.tabControlSetup.Controls.Add(this.tabTrims);
            this.tabControlSetup.Controls.Add(this.tabExports);
            this.tabControlSetup.Controls.Add(this.tabStyleTypes);
            this.tabControlSetup.Controls.Add(this.tabPathRules);
            this.tabControlSetup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlSetup.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabControlSetup.HotTrack = true;
            this.tabControlSetup.ItemSize = new System.Drawing.Size(25, 80);
            this.tabControlSetup.Location = new System.Drawing.Point(3, 3);
            this.tabControlSetup.Multiline = true;
            this.tabControlSetup.Name = "tabControlSetup";
            this.tabControlSetup.SelectedIndex = 0;
            this.tabControlSetup.Size = new System.Drawing.Size(703, 478);
            this.tabControlSetup.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControlSetup.TabIndex = 0;
            this.tabControlSetup.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.setupTabControl_DrawItem);
            // 
            // tabFilePaths
            // 
            this.tabFilePaths.Controls.Add(this.buttonFilePathsHelp);
            this.tabFilePaths.Controls.Add(this.textBoxInstalledLibraryFile);
            this.tabFilePaths.Controls.Add(this.label3);
            this.tabFilePaths.Controls.Add(this.textBoxNetworkLibraryFile);
            this.tabFilePaths.Controls.Add(this.label2);
            this.tabFilePaths.Controls.Add(this.textBoxNetworkFontsFolder);
            this.tabFilePaths.Controls.Add(this.label1);
            this.tabFilePaths.Location = new System.Drawing.Point(84, 4);
            this.tabFilePaths.Name = "tabFilePaths";
            this.tabFilePaths.Padding = new System.Windows.Forms.Padding(3);
            this.tabFilePaths.Size = new System.Drawing.Size(615, 470);
            this.tabFilePaths.TabIndex = 0;
            this.tabFilePaths.Text = "File Paths";
            this.tabFilePaths.UseVisualStyleBackColor = true;
            // 
            // buttonFilePathsHelp
            // 
            this.buttonFilePathsHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonFilePathsHelp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonFilePathsHelp.Location = new System.Drawing.Point(6, 441);
            this.buttonFilePathsHelp.Name = "buttonFilePathsHelp";
            this.buttonFilePathsHelp.Size = new System.Drawing.Size(28, 23);
            this.buttonFilePathsHelp.TabIndex = 13;
            this.buttonFilePathsHelp.Text = "?";
            this.buttonFilePathsHelp.UseVisualStyleBackColor = true;
            this.buttonFilePathsHelp.Click += new System.EventHandler(this.buttonFilePathsHelp_Click);
            // 
            // textBoxInstalledLibraryFile
            // 
            this.textBoxInstalledLibraryFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxInstalledLibraryFile.Location = new System.Drawing.Point(123, 62);
            this.textBoxInstalledLibraryFile.Name = "textBoxInstalledLibraryFile";
            this.textBoxInstalledLibraryFile.Size = new System.Drawing.Size(482, 20);
            this.textBoxInstalledLibraryFile.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Installed Library File:";
            // 
            // textBoxNetworkLibraryFile
            // 
            this.textBoxNetworkLibraryFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxNetworkLibraryFile.Location = new System.Drawing.Point(123, 36);
            this.textBoxNetworkLibraryFile.Name = "textBoxNetworkLibraryFile";
            this.textBoxNetworkLibraryFile.Size = new System.Drawing.Size(482, 20);
            this.textBoxNetworkLibraryFile.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Network Library File:";
            // 
            // textBoxNetworkFontsFolder
            // 
            this.textBoxNetworkFontsFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxNetworkFontsFolder.Location = new System.Drawing.Point(123, 10);
            this.textBoxNetworkFontsFolder.Name = "textBoxNetworkFontsFolder";
            this.textBoxNetworkFontsFolder.Size = new System.Drawing.Size(482, 20);
            this.textBoxNetworkFontsFolder.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Network Fonts Folder:";
            // 
            // tabPrefixes
            // 
            this.tabPrefixes.Controls.Add(this.buttonPrefixesHelp);
            this.tabPrefixes.Controls.Add(this.buttonPrefixesDown);
            this.tabPrefixes.Controls.Add(this.buttonPrefixesUp);
            this.tabPrefixes.Controls.Add(this.buttonPrefixesRemove);
            this.tabPrefixes.Controls.Add(this.buttonPrefixesAdd);
            this.tabPrefixes.Controls.Add(this.dataGridPrefixes);
            this.tabPrefixes.Location = new System.Drawing.Point(84, 4);
            this.tabPrefixes.Name = "tabPrefixes";
            this.tabPrefixes.Padding = new System.Windows.Forms.Padding(3);
            this.tabPrefixes.Size = new System.Drawing.Size(615, 470);
            this.tabPrefixes.TabIndex = 1;
            this.tabPrefixes.Text = "Prefixes";
            this.tabPrefixes.UseVisualStyleBackColor = true;
            // 
            // buttonPrefixesHelp
            // 
            this.buttonPrefixesHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonPrefixesHelp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPrefixesHelp.Location = new System.Drawing.Point(6, 441);
            this.buttonPrefixesHelp.Name = "buttonPrefixesHelp";
            this.buttonPrefixesHelp.Size = new System.Drawing.Size(28, 23);
            this.buttonPrefixesHelp.TabIndex = 13;
            this.buttonPrefixesHelp.Text = "?";
            this.buttonPrefixesHelp.UseVisualStyleBackColor = true;
            this.buttonPrefixesHelp.Click += new System.EventHandler(this.buttonPrefixesHelp_Click);
            // 
            // buttonPrefixesDown
            // 
            this.buttonPrefixesDown.Location = new System.Drawing.Point(6, 93);
            this.buttonPrefixesDown.Name = "buttonPrefixesDown";
            this.buttonPrefixesDown.Size = new System.Drawing.Size(28, 23);
            this.buttonPrefixesDown.TabIndex = 7;
            this.buttonPrefixesDown.Text = "▼";
            this.buttonPrefixesDown.UseVisualStyleBackColor = true;
            this.buttonPrefixesDown.Click += new System.EventHandler(this.buttonPrefixesDown_Click);
            // 
            // buttonPrefixesUp
            // 
            this.buttonPrefixesUp.Location = new System.Drawing.Point(6, 64);
            this.buttonPrefixesUp.Name = "buttonPrefixesUp";
            this.buttonPrefixesUp.Size = new System.Drawing.Size(28, 23);
            this.buttonPrefixesUp.TabIndex = 6;
            this.buttonPrefixesUp.Text = "▲";
            this.buttonPrefixesUp.UseVisualStyleBackColor = true;
            this.buttonPrefixesUp.Click += new System.EventHandler(this.buttonPrefixesUp_Click);
            // 
            // buttonPrefixesRemove
            // 
            this.buttonPrefixesRemove.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPrefixesRemove.Location = new System.Drawing.Point(6, 35);
            this.buttonPrefixesRemove.Name = "buttonPrefixesRemove";
            this.buttonPrefixesRemove.Size = new System.Drawing.Size(28, 23);
            this.buttonPrefixesRemove.TabIndex = 5;
            this.buttonPrefixesRemove.Text = "-";
            this.buttonPrefixesRemove.UseVisualStyleBackColor = true;
            this.buttonPrefixesRemove.Click += new System.EventHandler(this.buttonPrefixesRemove_Click);
            // 
            // buttonPrefixesAdd
            // 
            this.buttonPrefixesAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPrefixesAdd.Location = new System.Drawing.Point(6, 6);
            this.buttonPrefixesAdd.Name = "buttonPrefixesAdd";
            this.buttonPrefixesAdd.Size = new System.Drawing.Size(28, 23);
            this.buttonPrefixesAdd.TabIndex = 4;
            this.buttonPrefixesAdd.Text = "+";
            this.buttonPrefixesAdd.UseVisualStyleBackColor = true;
            this.buttonPrefixesAdd.Click += new System.EventHandler(this.buttonPrefixesAdd_Click);
            // 
            // dataGridPrefixes
            // 
            this.dataGridPrefixes.AllowUserToAddRows = false;
            this.dataGridPrefixes.AllowUserToDeleteRows = false;
            this.dataGridPrefixes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridPrefixes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridPrefixes.Location = new System.Drawing.Point(40, 3);
            this.dataGridPrefixes.Name = "dataGridPrefixes";
            this.dataGridPrefixes.Size = new System.Drawing.Size(572, 464);
            this.dataGridPrefixes.TabIndex = 1;
            // 
            // tabTrims
            // 
            this.tabTrims.Controls.Add(this.buttonTrimsDown);
            this.tabTrims.Controls.Add(this.buttonTrimsUp);
            this.tabTrims.Controls.Add(this.buttonTrimsHelp);
            this.tabTrims.Controls.Add(this.buttonTrimsRemove);
            this.tabTrims.Controls.Add(this.buttonTrimsAdd);
            this.tabTrims.Controls.Add(this.dataGridTrims);
            this.tabTrims.Location = new System.Drawing.Point(84, 4);
            this.tabTrims.Name = "tabTrims";
            this.tabTrims.Padding = new System.Windows.Forms.Padding(3);
            this.tabTrims.Size = new System.Drawing.Size(615, 470);
            this.tabTrims.TabIndex = 2;
            this.tabTrims.Text = "Trims";
            this.tabTrims.UseVisualStyleBackColor = true;
            // 
            // buttonTrimsDown
            // 
            this.buttonTrimsDown.Location = new System.Drawing.Point(6, 93);
            this.buttonTrimsDown.Name = "buttonTrimsDown";
            this.buttonTrimsDown.Size = new System.Drawing.Size(28, 23);
            this.buttonTrimsDown.TabIndex = 15;
            this.buttonTrimsDown.Text = "▼";
            this.buttonTrimsDown.UseVisualStyleBackColor = true;
            this.buttonTrimsDown.Click += new System.EventHandler(this.buttonTrimsDown_Click);
            // 
            // buttonTrimsUp
            // 
            this.buttonTrimsUp.Location = new System.Drawing.Point(6, 64);
            this.buttonTrimsUp.Name = "buttonTrimsUp";
            this.buttonTrimsUp.Size = new System.Drawing.Size(28, 23);
            this.buttonTrimsUp.TabIndex = 14;
            this.buttonTrimsUp.Text = "▲";
            this.buttonTrimsUp.UseVisualStyleBackColor = true;
            this.buttonTrimsUp.Click += new System.EventHandler(this.buttonTrimsUp_Click);
            // 
            // buttonTrimsHelp
            // 
            this.buttonTrimsHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonTrimsHelp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonTrimsHelp.Location = new System.Drawing.Point(6, 441);
            this.buttonTrimsHelp.Name = "buttonTrimsHelp";
            this.buttonTrimsHelp.Size = new System.Drawing.Size(28, 23);
            this.buttonTrimsHelp.TabIndex = 13;
            this.buttonTrimsHelp.Text = "?";
            this.buttonTrimsHelp.UseVisualStyleBackColor = true;
            this.buttonTrimsHelp.Click += new System.EventHandler(this.buttonTrimsHelp_Click);
            // 
            // buttonTrimsRemove
            // 
            this.buttonTrimsRemove.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonTrimsRemove.Location = new System.Drawing.Point(6, 35);
            this.buttonTrimsRemove.Name = "buttonTrimsRemove";
            this.buttonTrimsRemove.Size = new System.Drawing.Size(28, 23);
            this.buttonTrimsRemove.TabIndex = 7;
            this.buttonTrimsRemove.Text = "-";
            this.buttonTrimsRemove.UseVisualStyleBackColor = true;
            this.buttonTrimsRemove.Click += new System.EventHandler(this.buttonTrimsRemove_Click);
            // 
            // buttonTrimsAdd
            // 
            this.buttonTrimsAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonTrimsAdd.Location = new System.Drawing.Point(6, 6);
            this.buttonTrimsAdd.Name = "buttonTrimsAdd";
            this.buttonTrimsAdd.Size = new System.Drawing.Size(28, 23);
            this.buttonTrimsAdd.TabIndex = 6;
            this.buttonTrimsAdd.Text = "+";
            this.buttonTrimsAdd.UseVisualStyleBackColor = true;
            this.buttonTrimsAdd.Click += new System.EventHandler(this.buttonTrimsAdd_Click);
            // 
            // dataGridTrims
            // 
            this.dataGridTrims.AllowUserToAddRows = false;
            this.dataGridTrims.AllowUserToDeleteRows = false;
            this.dataGridTrims.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridTrims.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridTrims.Location = new System.Drawing.Point(40, 3);
            this.dataGridTrims.Name = "dataGridTrims";
            this.dataGridTrims.Size = new System.Drawing.Size(572, 464);
            this.dataGridTrims.TabIndex = 0;
            // 
            // tabExports
            // 
            this.tabExports.Controls.Add(this.buttonExportsHelp);
            this.tabExports.Controls.Add(this.buttonExportsRemove);
            this.tabExports.Controls.Add(this.buttonExportsAdd);
            this.tabExports.Controls.Add(this.dataGridExports);
            this.tabExports.Location = new System.Drawing.Point(84, 4);
            this.tabExports.Name = "tabExports";
            this.tabExports.Padding = new System.Windows.Forms.Padding(3);
            this.tabExports.Size = new System.Drawing.Size(615, 470);
            this.tabExports.TabIndex = 3;
            this.tabExports.Text = "Exports";
            this.tabExports.UseVisualStyleBackColor = true;
            // 
            // buttonExportsHelp
            // 
            this.buttonExportsHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonExportsHelp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonExportsHelp.Location = new System.Drawing.Point(6, 441);
            this.buttonExportsHelp.Name = "buttonExportsHelp";
            this.buttonExportsHelp.Size = new System.Drawing.Size(28, 23);
            this.buttonExportsHelp.TabIndex = 13;
            this.buttonExportsHelp.Text = "?";
            this.buttonExportsHelp.UseVisualStyleBackColor = true;
            this.buttonExportsHelp.Click += new System.EventHandler(this.buttonExportsHelp_Click);
            // 
            // buttonExportsRemove
            // 
            this.buttonExportsRemove.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonExportsRemove.Location = new System.Drawing.Point(6, 35);
            this.buttonExportsRemove.Name = "buttonExportsRemove";
            this.buttonExportsRemove.Size = new System.Drawing.Size(28, 23);
            this.buttonExportsRemove.TabIndex = 9;
            this.buttonExportsRemove.Text = "-";
            this.buttonExportsRemove.UseVisualStyleBackColor = true;
            this.buttonExportsRemove.Click += new System.EventHandler(this.buttonExportsRemove_Click);
            // 
            // buttonExportsAdd
            // 
            this.buttonExportsAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonExportsAdd.Location = new System.Drawing.Point(6, 6);
            this.buttonExportsAdd.Name = "buttonExportsAdd";
            this.buttonExportsAdd.Size = new System.Drawing.Size(28, 23);
            this.buttonExportsAdd.TabIndex = 8;
            this.buttonExportsAdd.Text = "+";
            this.buttonExportsAdd.UseVisualStyleBackColor = true;
            this.buttonExportsAdd.Click += new System.EventHandler(this.buttonExportsAdd_Click);
            // 
            // dataGridExports
            // 
            this.dataGridExports.AllowUserToAddRows = false;
            this.dataGridExports.AllowUserToDeleteRows = false;
            this.dataGridExports.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridExports.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridExports.Location = new System.Drawing.Point(40, 3);
            this.dataGridExports.Name = "dataGridExports";
            this.dataGridExports.Size = new System.Drawing.Size(572, 464);
            this.dataGridExports.TabIndex = 0;
            // 
            // tabStyleTypes
            // 
            this.tabStyleTypes.Controls.Add(this.buttonSetupHelp);
            this.tabStyleTypes.Controls.Add(this.textBoxStoneExtension);
            this.tabStyleTypes.Controls.Add(this.label8);
            this.tabStyleTypes.Controls.Add(this.textBoxStoneRoot);
            this.tabStyleTypes.Controls.Add(this.label9);
            this.tabStyleTypes.Controls.Add(this.textBoxSewExtension);
            this.tabStyleTypes.Controls.Add(this.label4);
            this.tabStyleTypes.Controls.Add(this.textBoxSewRoot);
            this.tabStyleTypes.Controls.Add(this.label7);
            this.tabStyleTypes.Controls.Add(this.label10);
            this.tabStyleTypes.Controls.Add(this.label11);
            this.tabStyleTypes.Controls.Add(this.label12);
            this.tabStyleTypes.Controls.Add(this.textBoxCutExtension);
            this.tabStyleTypes.Controls.Add(this.label5);
            this.tabStyleTypes.Controls.Add(this.textBoxCutRoot);
            this.tabStyleTypes.Controls.Add(this.label6);
            this.tabStyleTypes.Location = new System.Drawing.Point(84, 4);
            this.tabStyleTypes.Name = "tabStyleTypes";
            this.tabStyleTypes.Padding = new System.Windows.Forms.Padding(3);
            this.tabStyleTypes.Size = new System.Drawing.Size(615, 470);
            this.tabStyleTypes.TabIndex = 4;
            this.tabStyleTypes.Text = "Style Types";
            this.tabStyleTypes.UseVisualStyleBackColor = true;
            // 
            // buttonSetupHelp
            // 
            this.buttonSetupHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSetupHelp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSetupHelp.Location = new System.Drawing.Point(6, 441);
            this.buttonSetupHelp.Name = "buttonSetupHelp";
            this.buttonSetupHelp.Size = new System.Drawing.Size(28, 23);
            this.buttonSetupHelp.TabIndex = 29;
            this.buttonSetupHelp.Text = "?";
            this.buttonSetupHelp.UseVisualStyleBackColor = true;
            this.buttonSetupHelp.Click += new System.EventHandler(this.buttonSetupHelp_Click);
            // 
            // textBoxStoneExtension
            // 
            this.textBoxStoneExtension.Location = new System.Drawing.Point(118, 171);
            this.textBoxStoneExtension.Name = "textBoxStoneExtension";
            this.textBoxStoneExtension.Size = new System.Drawing.Size(67, 20);
            this.textBoxStoneExtension.TabIndex = 28;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(56, 174);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 13);
            this.label8.TabIndex = 27;
            this.label8.Text = "Extension:";
            // 
            // textBoxStoneRoot
            // 
            this.textBoxStoneRoot.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxStoneRoot.Location = new System.Drawing.Point(118, 145);
            this.textBoxStoneRoot.Name = "textBoxStoneRoot";
            this.textBoxStoneRoot.Size = new System.Drawing.Size(491, 20);
            this.textBoxStoneRoot.TabIndex = 26;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(79, 148);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(33, 13);
            this.label9.TabIndex = 25;
            this.label9.Text = "Root:";
            // 
            // textBoxSewExtension
            // 
            this.textBoxSewExtension.Location = new System.Drawing.Point(118, 100);
            this.textBoxSewExtension.Name = "textBoxSewExtension";
            this.textBoxSewExtension.Size = new System.Drawing.Size(67, 20);
            this.textBoxSewExtension.TabIndex = 24;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(56, 103);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 23;
            this.label4.Text = "Extension:";
            // 
            // textBoxSewRoot
            // 
            this.textBoxSewRoot.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSewRoot.Location = new System.Drawing.Point(118, 74);
            this.textBoxSewRoot.Name = "textBoxSewRoot";
            this.textBoxSewRoot.Size = new System.Drawing.Size(491, 20);
            this.textBoxSewRoot.TabIndex = 22;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(79, 77);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(33, 13);
            this.label7.TabIndex = 21;
            this.label7.Text = "Root:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(11, 148);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(40, 13);
            this.label10.TabIndex = 20;
            this.label10.Text = "Stone";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(18, 77);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(31, 13);
            this.label11.TabIndex = 19;
            this.label11.Text = "Sew";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(23, 9);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(26, 13);
            this.label12.TabIndex = 18;
            this.label12.Text = "Cut";
            // 
            // textBoxCutExtension
            // 
            this.textBoxCutExtension.Location = new System.Drawing.Point(118, 32);
            this.textBoxCutExtension.Name = "textBoxCutExtension";
            this.textBoxCutExtension.Size = new System.Drawing.Size(67, 20);
            this.textBoxCutExtension.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(56, 35);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Extension:";
            // 
            // textBoxCutRoot
            // 
            this.textBoxCutRoot.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxCutRoot.Location = new System.Drawing.Point(118, 6);
            this.textBoxCutRoot.Name = "textBoxCutRoot";
            this.textBoxCutRoot.Size = new System.Drawing.Size(491, 20);
            this.textBoxCutRoot.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(79, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Root:";
            // 
            // tabPathRules
            // 
            this.tabPathRules.Controls.Add(this.buttonPathRulesHelp);
            this.tabPathRules.Controls.Add(this.buttonPathRulesRemove);
            this.tabPathRules.Controls.Add(this.buttonPathRulesAdd);
            this.tabPathRules.Controls.Add(this.dataGridPathRules);
            this.tabPathRules.Location = new System.Drawing.Point(84, 4);
            this.tabPathRules.Name = "tabPathRules";
            this.tabPathRules.Padding = new System.Windows.Forms.Padding(3);
            this.tabPathRules.Size = new System.Drawing.Size(615, 470);
            this.tabPathRules.TabIndex = 5;
            this.tabPathRules.Text = "Path Rules";
            this.tabPathRules.UseVisualStyleBackColor = true;
            // 
            // buttonPathRulesHelp
            // 
            this.buttonPathRulesHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonPathRulesHelp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPathRulesHelp.Location = new System.Drawing.Point(6, 441);
            this.buttonPathRulesHelp.Name = "buttonPathRulesHelp";
            this.buttonPathRulesHelp.Size = new System.Drawing.Size(28, 23);
            this.buttonPathRulesHelp.TabIndex = 12;
            this.buttonPathRulesHelp.Text = "?";
            this.buttonPathRulesHelp.UseVisualStyleBackColor = true;
            this.buttonPathRulesHelp.Click += new System.EventHandler(this.buttonPathRulesHelp_Click);
            // 
            // buttonPathRulesRemove
            // 
            this.buttonPathRulesRemove.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPathRulesRemove.Location = new System.Drawing.Point(6, 35);
            this.buttonPathRulesRemove.Name = "buttonPathRulesRemove";
            this.buttonPathRulesRemove.Size = new System.Drawing.Size(28, 23);
            this.buttonPathRulesRemove.TabIndex = 11;
            this.buttonPathRulesRemove.Text = "-";
            this.buttonPathRulesRemove.UseVisualStyleBackColor = true;
            this.buttonPathRulesRemove.Click += new System.EventHandler(this.buttonPathRulesRemove_Click);
            // 
            // buttonPathRulesAdd
            // 
            this.buttonPathRulesAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPathRulesAdd.Location = new System.Drawing.Point(6, 6);
            this.buttonPathRulesAdd.Name = "buttonPathRulesAdd";
            this.buttonPathRulesAdd.Size = new System.Drawing.Size(28, 23);
            this.buttonPathRulesAdd.TabIndex = 10;
            this.buttonPathRulesAdd.Text = "+";
            this.buttonPathRulesAdd.UseVisualStyleBackColor = true;
            this.buttonPathRulesAdd.Click += new System.EventHandler(this.buttonPathRulesAdd_Click);
            // 
            // dataGridPathRules
            // 
            this.dataGridPathRules.AllowUserToAddRows = false;
            this.dataGridPathRules.AllowUserToDeleteRows = false;
            this.dataGridPathRules.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridPathRules.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridPathRules.Location = new System.Drawing.Point(40, 3);
            this.dataGridPathRules.Name = "dataGridPathRules";
            this.dataGridPathRules.Size = new System.Drawing.Size(572, 464);
            this.dataGridPathRules.TabIndex = 0;
            // 
            // tabStyles
            // 
            this.tabStyles.Controls.Add(this.buttonStylesHelp);
            this.tabStyles.Controls.Add(this.buttonStylesRemove);
            this.tabStyles.Controls.Add(this.buttonStylesAdd);
            this.tabStyles.Controls.Add(this.treeViewStyles);
            this.tabStyles.Location = new System.Drawing.Point(4, 22);
            this.tabStyles.Name = "tabStyles";
            this.tabStyles.Padding = new System.Windows.Forms.Padding(3);
            this.tabStyles.Size = new System.Drawing.Size(709, 484);
            this.tabStyles.TabIndex = 1;
            this.tabStyles.Text = "Styles";
            this.tabStyles.UseVisualStyleBackColor = true;
            // 
            // buttonStylesHelp
            // 
            this.buttonStylesHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonStylesHelp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonStylesHelp.Location = new System.Drawing.Point(8, 455);
            this.buttonStylesHelp.Name = "buttonStylesHelp";
            this.buttonStylesHelp.Size = new System.Drawing.Size(28, 23);
            this.buttonStylesHelp.TabIndex = 14;
            this.buttonStylesHelp.Text = "?";
            this.buttonStylesHelp.UseVisualStyleBackColor = true;
            this.buttonStylesHelp.Click += new System.EventHandler(this.buttonStylesHelp_Click);
            // 
            // buttonStylesRemove
            // 
            this.buttonStylesRemove.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonStylesRemove.Location = new System.Drawing.Point(8, 35);
            this.buttonStylesRemove.Name = "buttonStylesRemove";
            this.buttonStylesRemove.Size = new System.Drawing.Size(28, 23);
            this.buttonStylesRemove.TabIndex = 7;
            this.buttonStylesRemove.Text = "-";
            this.buttonStylesRemove.UseVisualStyleBackColor = true;
            this.buttonStylesRemove.Click += new System.EventHandler(this.buttonStylesRemove_Click);
            // 
            // buttonStylesAdd
            // 
            this.buttonStylesAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonStylesAdd.Location = new System.Drawing.Point(8, 6);
            this.buttonStylesAdd.Name = "buttonStylesAdd";
            this.buttonStylesAdd.Size = new System.Drawing.Size(28, 23);
            this.buttonStylesAdd.TabIndex = 6;
            this.buttonStylesAdd.Text = "+";
            this.buttonStylesAdd.UseVisualStyleBackColor = true;
            this.buttonStylesAdd.Click += new System.EventHandler(this.buttonStylesAdd_Click);
            // 
            // treeViewStyles
            // 
            this.treeViewStyles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeViewStyles.Location = new System.Drawing.Point(42, 3);
            this.treeViewStyles.Name = "treeViewStyles";
            this.treeViewStyles.Size = new System.Drawing.Size(664, 478);
            this.treeViewStyles.TabIndex = 0;
            this.treeViewStyles.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewStyles_NodeMouseDoubleClick);
            // 
            // statusStripEditor
            // 
            this.statusStripEditor.Location = new System.Drawing.Point(0, 546);
            this.statusStripEditor.Name = "statusStripEditor";
            this.statusStripEditor.Size = new System.Drawing.Size(720, 22);
            this.statusStripEditor.TabIndex = 1;
            this.statusStripEditor.Text = "statusStrip1";
            // 
            // buttonDone
            // 
            this.buttonDone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDone.Location = new System.Drawing.Point(12, 516);
            this.buttonDone.Name = "buttonDone";
            this.buttonDone.Size = new System.Drawing.Size(75, 23);
            this.buttonDone.TabIndex = 2;
            this.buttonDone.Text = "Done";
            this.buttonDone.UseVisualStyleBackColor = true;
            this.buttonDone.Click += new System.EventHandler(this.buttonDone_Click);
            // 
            // buttonInfo
            // 
            this.buttonInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonInfo.Location = new System.Drawing.Point(633, 516);
            this.buttonInfo.Name = "buttonInfo";
            this.buttonInfo.Size = new System.Drawing.Size(75, 23);
            this.buttonInfo.TabIndex = 3;
            this.buttonInfo.Text = "Info";
            this.buttonInfo.UseVisualStyleBackColor = true;
            this.buttonInfo.Click += new System.EventHandler(this.buttonInfo_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonCancel.Location = new System.Drawing.Point(93, 516);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 4;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // EditorWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(720, 568);
            this.ControlBox = false;
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonInfo);
            this.Controls.Add(this.buttonDone);
            this.Controls.Add(this.statusStripEditor);
            this.Controls.Add(this.tabControlConfig);
            this.Name = "EditorWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Editor";
            this.tabControlConfig.ResumeLayout(false);
            this.tabSetup.ResumeLayout(false);
            this.tabControlSetup.ResumeLayout(false);
            this.tabFilePaths.ResumeLayout(false);
            this.tabFilePaths.PerformLayout();
            this.tabPrefixes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridPrefixes)).EndInit();
            this.tabTrims.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridTrims)).EndInit();
            this.tabExports.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridExports)).EndInit();
            this.tabStyleTypes.ResumeLayout(false);
            this.tabStyleTypes.PerformLayout();
            this.tabPathRules.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridPathRules)).EndInit();
            this.tabStyles.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlConfig;
        private System.Windows.Forms.TabPage tabSetup;
        private System.Windows.Forms.TabControl tabControlSetup;
        private System.Windows.Forms.TabPage tabFilePaths;
        private System.Windows.Forms.TabPage tabPrefixes;
        private System.Windows.Forms.TabPage tabTrims;
        private System.Windows.Forms.TabPage tabExports;
        private System.Windows.Forms.TabPage tabStyleTypes;
        private System.Windows.Forms.TabPage tabPathRules;
        private System.Windows.Forms.TabPage tabStyles;
        private System.Windows.Forms.StatusStrip statusStripEditor;
        private System.Windows.Forms.DataGridView dataGridPrefixes;
        private System.Windows.Forms.TextBox textBoxInstalledLibraryFile;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxNetworkLibraryFile;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxNetworkFontsFolder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridTrims;
        private System.Windows.Forms.DataGridView dataGridExports;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBoxCutExtension;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxCutRoot;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dataGridPathRules;
        private System.Windows.Forms.TextBox textBoxStoneExtension;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxStoneRoot;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxSewExtension;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxSewRoot;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TreeView treeViewStyles;
        private System.Windows.Forms.Button buttonDone;
        private System.Windows.Forms.Button buttonInfo;
        private System.Windows.Forms.Button buttonPrefixesDown;
        private System.Windows.Forms.Button buttonPrefixesUp;
        private System.Windows.Forms.Button buttonPrefixesRemove;
        private System.Windows.Forms.Button buttonPrefixesAdd;
        private System.Windows.Forms.Button buttonTrimsRemove;
        private System.Windows.Forms.Button buttonTrimsAdd;
        private System.Windows.Forms.Button buttonExportsRemove;
        private System.Windows.Forms.Button buttonExportsAdd;
        private System.Windows.Forms.Button buttonPathRulesRemove;
        private System.Windows.Forms.Button buttonPathRulesAdd;
        private System.Windows.Forms.Button buttonFilePathsHelp;
        private System.Windows.Forms.Button buttonPrefixesHelp;
        private System.Windows.Forms.Button buttonTrimsHelp;
        private System.Windows.Forms.Button buttonExportsHelp;
        private System.Windows.Forms.Button buttonSetupHelp;
        private System.Windows.Forms.Button buttonPathRulesHelp;
        private System.Windows.Forms.Button buttonTrimsDown;
        private System.Windows.Forms.Button buttonTrimsUp;
        private System.Windows.Forms.Button buttonStylesHelp;
        private System.Windows.Forms.Button buttonStylesRemove;
        private System.Windows.Forms.Button buttonStylesAdd;
        private System.Windows.Forms.Button buttonCancel;
    }
}
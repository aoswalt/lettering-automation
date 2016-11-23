using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Lettering.Data;
using Newtonsoft.Json;

namespace Lettering.Forms {
    public partial class EditorWindow : Form {
        public JsonConfigData Config;
        private JsonConfigData editedConfig;

        public EditorWindow(JsonConfigData config) {
            //NOTE(adam): simple but probably inefficient deep copy of config
            editedConfig = JsonConvert.DeserializeObject<JsonConfigData>(JsonConvert.SerializeObject(config));
            InitializeComponent();
            BindSetupData();
            BuildStylesTree();
        }

        private void BindSetupData() {
            textBoxNetworkFontsFolder.DataBindings.Add("Text", editedConfig.Setup.FilePaths, "NetworkFontsFolderPath", true, DataSourceUpdateMode.OnPropertyChanged);
            textBoxNetworkLibraryFile.DataBindings.Add("Text", editedConfig.Setup.FilePaths, "NetworkLibraryFilePath", true, DataSourceUpdateMode.OnPropertyChanged);
            textBoxInstalledLibraryFile.DataBindings.Add("Text", editedConfig.Setup.FilePaths, "InstalledLibraryFilePath", true, DataSourceUpdateMode.OnPropertyChanged);

            dataGridPrefixes.DataSource = new BindingList<StringData>(editedConfig.Setup.StylePrefixes);
            dataGridPrefixes.Columns[0].HeaderText = "Prefix";

            dataGridTrims.DataSource = new BindingList<Data_Trim>(editedConfig.Setup.Trims);

            dataGridExports.DataSource = new BindingList<Data_Export>(editedConfig.Setup.Exports);
            dataGridExports.Columns[0].HeaderText = "Style Regex";
            dataGridExports.Columns[1].HeaderText = "File Type";

            textBoxCutRoot.DataBindings.Add("Text", editedConfig.Setup.TypeData["Cut"], "Root");
            textBoxCutExtension.DataBindings.Add("Text", editedConfig.Setup.TypeData["Cut"], "Extension");
            textBoxSewRoot.DataBindings.Add("Text", editedConfig.Setup.TypeData["Sew"], "Root");
            textBoxSewExtension.DataBindings.Add("Text", editedConfig.Setup.TypeData["Sew"], "Extension");
            textBoxStoneRoot.DataBindings.Add("Text", editedConfig.Setup.TypeData["Stone"], "Root");
            textBoxStoneExtension.DataBindings.Add("Text", editedConfig.Setup.TypeData["Stone"], "Extension");

            //TODO(adam): define path rules info in better way
            dataGridPathRules.DataSource = new BindingList<Data_PathRule>(editedConfig.Setup.PathRules);
            dataGridPathRules.Columns[1].Width *= 2;
        }

        private void BuildStylesTree() {
            foreach(KeyValuePair<string, Data_Style> styleKV in editedConfig.Styles) {
                treeViewStyles.Nodes.Add(CreateTreeNode(styleKV.Key, styleKV.Value));
            }
            treeViewStyles.Sort();
        }

        private TreeNode CreateTreeNode(string style, Data_Style data) {
            TreeNode styleNode = new TreeNode(style) { Name = style, Tag = data };

            foreach(LetteringType type in Enum.GetValues(typeof(LetteringType))) {
                Data_StyleData styleData = null;

                switch(type) {
                    case LetteringType.Cut:
                        styleData = data.Cut;
                        break;
                    case LetteringType.Sew:
                        styleData = data.Sew;
                        break;
                    case LetteringType.Stone:
                        styleData = data.Stone;
                        break;
                }

                if(styleData != null) {
                    TreeNode typeNode = new TreeNode(type.ToString());
                    styleNode.Nodes.Add(typeNode);

                    if(styleData.Rule != null) {
                        typeNode.Nodes.Add(new TreeNode("Rule: " + styleData.Rule));
                    }

                    if(styleData.CustomWordOrder != null) {
                        typeNode.Nodes.Add(new TreeNode("Custom Word Order: " + string.Join(",", styleData.CustomWordOrder)));
                    }

                    if(styleData.MirroredStyle != null) {
                        typeNode.Nodes.Add(new TreeNode("Mirror: " + styleData.MirroredStyle));
                    }

                    if(styleData.Exceptions != null) {
                        TreeNode exsNode = new TreeNode("Exceptions");
                        typeNode.Nodes.Add(exsNode);

                        foreach(Data_Exception ex in styleData.Exceptions) {
                            TreeNode exNode = new TreeNode(ex.Path);
                            exsNode.Nodes.Add(exNode);

                            if(ex.Conditions != null) {
                                foreach(string condition in ex.Conditions) {
                                    exNode.Nodes.Add(new TreeNode("Condition: " + condition));
                                }
                            }
                        }
                    }
                }
            }

            return styleNode;
        }

        //NOTE(adam): based on MSDN, draws tab text horizontal
        private void setupTabControl_DrawItem(object sender, DrawItemEventArgs e) {
            Graphics g = e.Graphics;

            // Get the item from the collection.
            TabPage _tabPage = tabControlSetup.TabPages[e.Index];

            // Get the real bounds for the tab rectangle.
            Rectangle _tabBounds = tabControlSetup.GetTabRect(e.Index);
            int offset = 8;
            _tabBounds.Width -= offset;
            _tabBounds.X += offset;
            
            Brush _textBrush = new System.Drawing.SolidBrush(e.ForeColor);
            e.DrawBackground();

            // Draw string. Left Align the text.
            StringFormat _stringFlags = new StringFormat();
            _stringFlags.Alignment = StringAlignment.Near;
            _stringFlags.LineAlignment = StringAlignment.Center;
            g.DrawString(_tabPage.Text, e.Font, _textBrush, _tabBounds, new StringFormat(_stringFlags));
        }

        private void treeViewStyles_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e) {
            TreeNode root = e.Node;
            while(root.Parent != null) {
                root = root.Parent;
            }
            
            EditStyleWindow editStyleWindow = new EditStyleWindow(editedConfig, root.Text, (Data_Style)root.Tag);
            if(editStyleWindow.ShowDialog(this) == DialogResult.OK) {
                treeViewStyles.Nodes.RemoveByKey(root.Text);
                treeViewStyles.Nodes.RemoveByKey(editStyleWindow.StyleCode);

                TreeNode newNode = CreateTreeNode(editStyleWindow.StyleCode, editStyleWindow.EditedStyle);
                treeViewStyles.Nodes.Add(newNode);
                treeViewStyles.Sort();
                treeViewStyles.SelectedNode = newNode;

                if(root.Text != editStyleWindow.StyleCode) {
                    editedConfig.Styles.Remove(root.Text);
                }

                if(editedConfig.Styles.ContainsKey(editStyleWindow.StyleCode)) {
                    editedConfig.Styles[editStyleWindow.StyleCode] = editStyleWindow.EditedStyle;
                } else {
                    editedConfig.Styles.Add(editStyleWindow.StyleCode, editStyleWindow.EditedStyle);
                }
            }
        }

        private void buttonDone_Click(object sender, EventArgs e) {
            Config = editedConfig;
            this.Close();
        }

        private void buttonInfo_Click(object sender, EventArgs e) {
            string infoText = "Special options available to use in paths\n\n";
            infoText += "!type\t";
            infoText += "type of lettering \t ex: TT Styles\n";
            infoText += "!style\t";
            infoText += "style code \t ex: TT FBL\n";
            infoText += "!size\t";
            infoText += "lettering size \t ex: 3INCH\n";
            infoText += "!spec\t";
            infoText += "spec width \t ex: 10.5\n";
            infoText += "!ya\t";
            infoText += "Youth/Adult\n";
            infoText += "!cd\t";
            infoText += "Cheer/Dance\n";
            infoText += "!word1\t";
            infoText += "1st word (or name)\n";
            infoText += "!word2\t";
            infoText += "2nd word\n";
            infoText += "!word3\t";
            infoText += "3rd word\n";
            infoText += "!word4\t";
            infoText += "4th word\n";
            infoText += "!ecm1\t";
            infoText += "1st word as ECM\n";
            infoText += "!ecm2\t";
            infoText += "2nd word as ECM\n";
            infoText += "!ecm3\t";
            infoText += "3rd word as ECM\n";
            infoText += "!ecm4\t";
            infoText += "4th word as ECM\n";
            infoText += "!name\t";
            infoText += "name\n";
            Messenger.Show(infoText, "Info");
        }

        private void buttonFilePathsHelp_Click(object sender, EventArgs e) {
            Messenger.Show("Global paths used across the program.", "File Paths Help");
        }

        private void buttonPrefixesHelp_Click(object sender, EventArgs e) {
            Messenger.Show("Prefixes used for parsing styles.\n\nProcessed in order!", "Prefixes Help");
        }

        private void buttonPrefixesAdd_Click(object sender, EventArgs e) {
            string prefix = Messenger.Prompt("Enter new prefix:", "Add Prefix").ToUpper();
            if(prefix.Length > 0) {
                //NOTE(adam): convert to string list for check because contains not working for StringData
                if(!editedConfig.Setup.StylePrefixes.ConvertAll(x => x.Value).Contains(prefix)) {
                    editedConfig.Setup.StylePrefixes.Add(prefix);
                    //TODO(adam): look into better method of updaing data
                    dataGridPrefixes.DataSource = new BindingList<StringData>(editedConfig.Setup.StylePrefixes);
                } else {
                    Messenger.Show("Prefix already exists.", "Add Prefix Error");
                }
            }
        }

        private void buttonPrefixesRemove_Click(object sender, EventArgs e) {
            DataGridViewCell selected = dataGridPrefixes.SelectedCells[0];
            bool confirm = Messenger.Show($"Remove '{selected.Value}'?", "Confirm Remove", MessageButtons.YesNo);
            if(confirm) {
                int selectedIndex = selected.RowIndex;
                editedConfig.Setup.StylePrefixes.RemoveAt(selectedIndex);
                //TODO(adam): look into better method of updaing data
                dataGridPrefixes.DataSource = new BindingList<StringData>(editedConfig.Setup.StylePrefixes);
            }
        }

        private void buttonPrefixesUp_Click(object sender, EventArgs e) {
            int selectedIndex = dataGridPrefixes.SelectedCells[0].RowIndex;
            if(selectedIndex != 0) {
                StringData prefix = editedConfig.Setup.StylePrefixes[selectedIndex];
                editedConfig.Setup.StylePrefixes.RemoveAt(selectedIndex);
                editedConfig.Setup.StylePrefixes.Insert(selectedIndex - 1, prefix);
                //TODO(adam): look into better method of updaing data
                dataGridPrefixes.DataSource = new BindingList<StringData>(editedConfig.Setup.StylePrefixes);
                dataGridPrefixes.ClearSelection();
                dataGridPrefixes.Rows[selectedIndex - 1].Selected = true;
            }
        }

        private void buttonPrefixesDown_Click(object sender, EventArgs e) {
            int selectedIndex = dataGridPrefixes.SelectedCells[0].RowIndex;
            if(selectedIndex != dataGridPrefixes.RowCount - 1) {
                StringData prefix = editedConfig.Setup.StylePrefixes[selectedIndex];
                editedConfig.Setup.StylePrefixes.RemoveAt(selectedIndex);
                editedConfig.Setup.StylePrefixes.Insert(selectedIndex + 1, prefix);
                //TODO(adam): look into better method of updaing data
                dataGridPrefixes.DataSource = new BindingList<StringData>(editedConfig.Setup.StylePrefixes);
                dataGridPrefixes.ClearSelection();
                dataGridPrefixes.Rows[selectedIndex + 1].Selected = true;
            }
        }

        private void buttonTrimsHelp_Click(object sender, EventArgs e) {
            Messenger.Show("Regular expressions to try to remove extraneous information from style codes.\n\nProcessed in order!", "Trims Help");
        }

        private void buttonTrimsAdd_Click(object sender, EventArgs e) {
            Messenger.Show("TrimsAdd");
        }

        private void buttonTrimsRemove_Click(object sender, EventArgs e) {
            Messenger.Show("TrimsRemove");
        }

        private void buttonTrimsUp_Click(object sender, EventArgs e) {
            Messenger.Show("TrimsUp");
        }

        private void buttonTrimsDown_Click(object sender, EventArgs e) {
            Messenger.Show("TrimsDown");
        }

        private void buttonExportsHelp_Click(object sender, EventArgs e) {
            Messenger.Show("Regular expressions for which styles to export with the file type to export.", "Exports Help");
        }

        private void buttonExportsAdd_Click(object sender, EventArgs e) {
            Messenger.Show("ExportsAdd");
        }

        private void buttonExportsRemove_Click(object sender, EventArgs e) {
            Messenger.Show("ExportsRemove");
        }

        private void buttonSetupHelp_Click(object sender, EventArgs e) {
            Messenger.Show("Per-type root path and file extension.", "Setup Help");
        }

        private void buttonPathRulesHelp_Click(object sender, EventArgs e) {
            Messenger.Show("The path rules for styles to follow.", "Path Rules Help");
        }

        private void buttonPathRulesAdd_Click(object sender, EventArgs e) {
            Messenger.Show("PathRulesAdd");
        }

        private void buttonPathRulesRemove_Click(object sender, EventArgs e) {
            Messenger.Show("PathRulesRemove");
        }
    }
}

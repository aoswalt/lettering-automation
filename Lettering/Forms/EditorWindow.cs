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
            labelPathRulesInfo.Text = "\n";
            labelPathRulesInfo.Text += "!style\n    Style Folder/Style Code\n";
            labelPathRulesInfo.Text += "!size\n    #INCH\n";
            labelPathRulesInfo.Text += "!spec\n    #\n";
            labelPathRulesInfo.Text += "!ya\n    Youth/Adult\n";
            labelPathRulesInfo.Text += "!cd\n    Cheer/Dance\n";
            labelPathRulesInfo.Text += "\n";
            labelPathRulesInfo.Text += "ignore\n    Ignore Style\n";
            labelPathRulesInfo.Text += "mirror\n    Same as Other Style\n";
            labelPathRulesInfo.Text += "names\n    Names for Automation\n";
            labelPathRulesInfo.Text += "\n";
            labelPathRulesInfo.Text += "cut-sew_files\n    Sew Files in Cut Files\n";
            labelPathRulesInfo.Text += "cut-specific\n    specific Sew Files\n";
        }

        private void BuildStylesTree() {
            foreach(KeyValuePair<string, Data_Style> styleKV in editedConfig.Styles) {
                treeViewStyles.Nodes.Add(CreateTreeNode(styleKV.Key, styleKV.Value));
            }
            treeViewStyles.Sort();
        }

        private TreeNode CreateTreeNode(string style, Data_Style data) {
            TreeNode styleNode = new TreeNode(style) { Name = style, Tag = data };

            foreach(ReportType type in Enum.GetValues(typeof(ReportType))) {
                Data_StyleData styleData = null;

                switch(type) {
                    case ReportType.Cut:
                        styleData = data.Cut;
                        break;
                    case ReportType.Sew:
                        styleData = data.Sew;
                        break;
                    case ReportType.Stone:
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

        private void buttonDone_Click(object sender, EventArgs e) {
            Config = editedConfig;
            this.Close();
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
    }
}

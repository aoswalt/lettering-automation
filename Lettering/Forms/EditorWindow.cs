using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Lettering.Data;
using Newtonsoft.Json;

namespace Lettering.Forms {
    public partial class EditorWindow : Form {
        private JsonConfigData config;
        private JsonConfigData editedConfig;

        public EditorWindow(JsonConfigData config) {
            this.config = config;
            //NOTE(adam): simple but probably inefficient deep copy of config
            editedConfig = JsonConvert.DeserializeObject<JsonConfigData>(JsonConvert.SerializeObject(config));
            InitializeComponent();
            bindData();
        }

        private void bindData() {
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

        private void buttonDone_Click(object sender, System.EventArgs e) {
            MessageBox.Show(editedConfig.Setup.StylePrefixes[0]);
        }
    }
}

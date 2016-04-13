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

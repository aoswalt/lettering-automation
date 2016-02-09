using System.Windows.Forms;

namespace Lettering.Forms {
    internal partial class FontCheckingWindow : Form {
        internal FontCheckingWindow() {
            InitializeComponent();
        }

        internal void SetFontsProgress(string fontFile, int fontNumber, int totalFonts) {
            this.labelFontFile.Text = fontFile;
            this.labelFontNumber.Text = $"{fontNumber}/{totalFonts}";
            this.progressFonts.Maximum = totalFonts;
            this.progressFonts.Value = fontNumber;
            this.Refresh();
        }
    }
}

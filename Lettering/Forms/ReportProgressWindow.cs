using System.Windows.Forms;

namespace Lettering.Forms {
    internal partial class ReportProgressWindow : Form {
        internal ReportProgressWindow() {
            InitializeComponent();
        }

        internal void SetReportProgress(LetteringType type, string styleCode, int recordNumber, int totalRecords) {
            this.labelReportType.Text = type.ToString();
            this.labelStyleCode.Text = styleCode;
            this.labelRecordNumber.Text = $"{recordNumber}/{totalRecords}";
            this.progressFonts.Maximum = totalRecords;
            this.progressFonts.Value = recordNumber;
            this.Refresh();
        }
    }
}

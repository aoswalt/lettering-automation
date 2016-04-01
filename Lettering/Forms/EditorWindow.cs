using System.Drawing;
using System.Windows.Forms;

namespace Lettering.Forms {
    public partial class EditorWindow : Form {
        public EditorWindow() {
            InitializeComponent();
        }

        //NOTE(adam): based on MSDN, draws text horizontal
        private void setupTabControl_DrawItem(object sender, DrawItemEventArgs e) {
            Graphics g = e.Graphics;

            // Get the item from the collection.
            TabPage _tabPage = setupTabControl.TabPages[e.Index];

            // Get the real bounds for the tab rectangle.
            Rectangle _tabBounds = setupTabControl.GetTabRect(e.Index);
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
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lettering.Data;

namespace Lettering.Forms {
    public partial class EditStyleWindow : Form {
        public string StyleCode { get; set; }
        public Data_Style EditedStyle { get; set; }

        private JsonConfigData configData;
        private object[] wordNums = new object[] { "", 1, 2, 3, 4 };

        public EditStyleWindow(JsonConfigData configData, string styleCode, Data_Style editedStyle) {
            this.configData = configData;
            InitializeComponent();
            FillLists();

            if(styleCode != null && editedStyle != null) {
                this.StyleCode = styleCode;
                this.EditedStyle = editedStyle;
                PopulateControls();
            }
        }

        private void FillLists() {
            cboxCutRule.DataSource = configData.Setup.PathRules;
            cboxCutRule.DisplayMember = "Id";
            cboxCutRule.ValueMember = "Id";
            cboxCutRule.SelectedIndex = -1;
            
            cboxCutWordOrder1.Items.AddRange(wordNums);
            cboxCutWordOrder2.Items.AddRange(wordNums);
            cboxCutWordOrder3.Items.AddRange(wordNums);
            cboxCutWordOrder4.Items.AddRange(wordNums);

            cboxCutMirror.DataSource = configData.Styles.Keys.ToList();
            cboxCutMirror.SelectedIndex = -1;
        }

        private void PopulateControls() {
            textBoxStyle.Text = StyleCode;

            if(EditedStyle.Cut != null) {
                cboxCutRule.SelectedIndex = cboxCutRule.FindStringExact(EditedStyle.Cut.Rule);

                if(EditedStyle.Cut.CustomWordOrder != null) {
                    if(EditedStyle.Cut.CustomWordOrder.Count >= 1) {
                        cboxCutWordOrder1.SelectedIndex = cboxCutWordOrder1.FindString(EditedStyle.Cut.CustomWordOrder[0].ToString());
                    }
                    if(EditedStyle.Cut.CustomWordOrder.Count >= 2) {
                        cboxCutWordOrder2.SelectedIndex = cboxCutWordOrder2.FindString(EditedStyle.Cut.CustomWordOrder[1].ToString());
                    }
                    if(EditedStyle.Cut.CustomWordOrder.Count >= 3) {
                        cboxCutWordOrder3.SelectedIndex = cboxCutWordOrder3.FindString(EditedStyle.Cut.CustomWordOrder[2].ToString());
                    }
                    if(EditedStyle.Cut.CustomWordOrder.Count >= 4) {
                        cboxCutWordOrder4.SelectedIndex = cboxCutWordOrder4.FindString(EditedStyle.Cut.CustomWordOrder[3].ToString());
                    }
                }

                if(EditedStyle.Cut.MirroredStyle != null) {
                    cboxCutMirror.Text = EditedStyle.Cut.MirroredStyle;
                }

                if(EditedStyle.Cut.Exceptions != null) {
                    listBoxCutExPaths.DataSource = new BindingList<Data_Exception>(EditedStyle.Cut.Exceptions);
                    listBoxCutExPaths.DisplayMember = "Path";

                    if((Data_Exception)listBoxCutExPaths.SelectedItem != null) {
                        listBoxCutExConditions.DataSource = new BindingList<string>(((Data_Exception)listBoxCutExPaths.SelectedItem).Conditions);
                    }
                }
            }
        }
    }
}

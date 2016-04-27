using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
            //CUT
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

            //SEW
            cboxSewRule.DataSource = new List<Data_PathRule>(configData.Setup.PathRules);
            cboxSewRule.DisplayMember = "Id";
            cboxSewRule.ValueMember = "Id";
            cboxSewRule.SelectedIndex = -1;

            cboxSewWordOrder1.Items.AddRange(wordNums);
            cboxSewWordOrder2.Items.AddRange(wordNums);
            cboxSewWordOrder3.Items.AddRange(wordNums);
            cboxSewWordOrder4.Items.AddRange(wordNums);

            cboxSewMirror.DataSource = configData.Styles.Keys.ToList();
            cboxSewMirror.SelectedIndex = -1;

            //STONE
            cboxStoneRule.DataSource = new List<Data_PathRule>(configData.Setup.PathRules);
            cboxStoneRule.DisplayMember = "Id";
            cboxStoneRule.ValueMember = "Id";
            cboxStoneRule.SelectedIndex = -1;

            cboxStoneWordOrder1.Items.AddRange(wordNums);
            cboxStoneWordOrder2.Items.AddRange(wordNums);
            cboxStoneWordOrder3.Items.AddRange(wordNums);
            cboxStoneWordOrder4.Items.AddRange(wordNums);

            cboxStoneMirror.DataSource = configData.Styles.Keys.ToList();
            cboxStoneMirror.SelectedIndex = -1;
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

            if(EditedStyle.Sew != null) {
                cboxSewRule.SelectedIndex = cboxSewRule.FindStringExact(EditedStyle.Sew.Rule);

                if(EditedStyle.Sew.CustomWordOrder != null) {
                    if(EditedStyle.Sew.CustomWordOrder.Count >= 1) {
                        cboxSewWordOrder1.SelectedIndex = cboxSewWordOrder1.FindString(EditedStyle.Sew.CustomWordOrder[0].ToString());
                    }
                    if(EditedStyle.Sew.CustomWordOrder.Count >= 2) {
                        cboxSewWordOrder2.SelectedIndex = cboxSewWordOrder2.FindString(EditedStyle.Sew.CustomWordOrder[1].ToString());
                    }
                    if(EditedStyle.Sew.CustomWordOrder.Count >= 3) {
                        cboxSewWordOrder3.SelectedIndex = cboxSewWordOrder3.FindString(EditedStyle.Sew.CustomWordOrder[2].ToString());
                    }
                    if(EditedStyle.Sew.CustomWordOrder.Count >= 4) {
                        cboxSewWordOrder4.SelectedIndex = cboxSewWordOrder4.FindString(EditedStyle.Sew.CustomWordOrder[3].ToString());
                    }
                }

                if(EditedStyle.Sew.MirroredStyle != null) {
                    cboxSewMirror.Text = EditedStyle.Sew.MirroredStyle;
                }

                if(EditedStyle.Sew.Exceptions != null) {
                    listBoxSewExPaths.DataSource = new BindingList<Data_Exception>(EditedStyle.Sew.Exceptions);
                    listBoxSewExPaths.DisplayMember = "Path";

                    if((Data_Exception)listBoxSewExPaths.SelectedItem != null) {
                        listBoxSewExConditions.DataSource = new BindingList<string>(((Data_Exception)listBoxSewExPaths.SelectedItem).Conditions);
                    }
                }
            }

            if(EditedStyle.Stone != null) {
                cboxStoneRule.SelectedIndex = cboxStoneRule.FindStringExact(EditedStyle.Stone.Rule);

                if(EditedStyle.Stone.CustomWordOrder != null) {
                    if(EditedStyle.Stone.CustomWordOrder.Count >= 1) {
                        cboxStoneWordOrder1.SelectedIndex = cboxStoneWordOrder1.FindString(EditedStyle.Stone.CustomWordOrder[0].ToString());
                    }
                    if(EditedStyle.Stone.CustomWordOrder.Count >= 2) {
                        cboxStoneWordOrder2.SelectedIndex = cboxStoneWordOrder2.FindString(EditedStyle.Stone.CustomWordOrder[1].ToString());
                    }
                    if(EditedStyle.Stone.CustomWordOrder.Count >= 3) {
                        cboxStoneWordOrder3.SelectedIndex = cboxStoneWordOrder3.FindString(EditedStyle.Stone.CustomWordOrder[2].ToString());
                    }
                    if(EditedStyle.Stone.CustomWordOrder.Count >= 4) {
                        cboxStoneWordOrder4.SelectedIndex = cboxStoneWordOrder4.FindString(EditedStyle.Stone.CustomWordOrder[3].ToString());
                    }
                }

                if(EditedStyle.Stone.MirroredStyle != null) {
                    cboxStoneMirror.Text = EditedStyle.Stone.MirroredStyle;
                }

                if(EditedStyle.Stone.Exceptions != null) {
                    listBoxStoneExPaths.DataSource = new BindingList<Data_Exception>(EditedStyle.Stone.Exceptions);
                    listBoxStoneExPaths.DisplayMember = "Path";

                    if((Data_Exception)listBoxStoneExPaths.SelectedItem != null) {
                        listBoxStoneExConditions.DataSource = new BindingList<string>(((Data_Exception)listBoxStoneExPaths.SelectedItem).Conditions);
                    }
                }
            }
        }
    }
}

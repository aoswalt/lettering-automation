using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Lettering.Data;
using Newtonsoft.Json;

namespace Lettering.Forms {
    public partial class EditStyleWindow : Form {
        public string StyleCode { get; set; }
        public Data_Style EditedStyle { get; set; }

        private JsonConfigData configData;
        private Data_Style workingStyle;
        private object[] wordNums = new object[] { "", 1, 2, 3, 4 };

        public EditStyleWindow(JsonConfigData configData, string styleCode, Data_Style style) {
            this.configData = configData;
            InitializeComponent();
            FillLists();

            if(styleCode != null && style != null) {
                this.StyleCode = styleCode;
                //NOTE(adam): simple but probably inefficient deep copy of config
                this.workingStyle = JsonConvert.DeserializeObject<Data_Style>(JsonConvert.SerializeObject(style));

                //NOTE(adam): potentially temporary StyleData for ability to add properties through controls
                if(workingStyle.Cut == null) workingStyle.Cut = new Data_StyleData();
                if(workingStyle.Sew == null) workingStyle.Sew = new Data_StyleData();
                if(workingStyle.Stone == null) workingStyle.Stone = new Data_StyleData();

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

            if(workingStyle.Cut != null) {
                cboxCutRule.SelectedIndex = cboxCutRule.FindStringExact(workingStyle.Cut.Rule);

                if(workingStyle.Cut.CustomWordOrder != null) {
                    if(workingStyle.Cut.CustomWordOrder.Count >= 1) {
                        cboxCutWordOrder1.SelectedIndex = cboxCutWordOrder1.FindString(workingStyle.Cut.CustomWordOrder[0].ToString());
                    }
                    if(workingStyle.Cut.CustomWordOrder.Count >= 2) {
                        cboxCutWordOrder2.SelectedIndex = cboxCutWordOrder2.FindString(workingStyle.Cut.CustomWordOrder[1].ToString());
                    }
                    if(workingStyle.Cut.CustomWordOrder.Count >= 3) {
                        cboxCutWordOrder3.SelectedIndex = cboxCutWordOrder3.FindString(workingStyle.Cut.CustomWordOrder[2].ToString());
                    }
                    if(workingStyle.Cut.CustomWordOrder.Count >= 4) {
                        cboxCutWordOrder4.SelectedIndex = cboxCutWordOrder4.FindString(workingStyle.Cut.CustomWordOrder[3].ToString());
                    }
                }

                if(workingStyle.Cut.MirroredStyle != null) {
                    cboxCutMirror.Text = workingStyle.Cut.MirroredStyle;
                }

                if(workingStyle.Cut.Exceptions != null) {
                    listBoxCutExPaths.DataSource = new BindingList<Data_Exception>(workingStyle.Cut.Exceptions);
                    listBoxCutExPaths.DisplayMember = "Path";

                    if((Data_Exception)listBoxCutExPaths.SelectedItem != null) {
                        listBoxCutExConditions.DataSource = new BindingList<string>(((Data_Exception)listBoxCutExPaths.SelectedItem).Conditions);
                    }
                }
            }

            if(workingStyle.Sew != null) {
                cboxSewRule.SelectedIndex = cboxSewRule.FindStringExact(workingStyle.Sew.Rule);

                if(workingStyle.Sew.CustomWordOrder != null) {
                    if(workingStyle.Sew.CustomWordOrder.Count >= 1) {
                        cboxSewWordOrder1.SelectedIndex = cboxSewWordOrder1.FindString(workingStyle.Sew.CustomWordOrder[0].ToString());
                    }
                    if(workingStyle.Sew.CustomWordOrder.Count >= 2) {
                        cboxSewWordOrder2.SelectedIndex = cboxSewWordOrder2.FindString(workingStyle.Sew.CustomWordOrder[1].ToString());
                    }
                    if(workingStyle.Sew.CustomWordOrder.Count >= 3) {
                        cboxSewWordOrder3.SelectedIndex = cboxSewWordOrder3.FindString(workingStyle.Sew.CustomWordOrder[2].ToString());
                    }
                    if(workingStyle.Sew.CustomWordOrder.Count >= 4) {
                        cboxSewWordOrder4.SelectedIndex = cboxSewWordOrder4.FindString(workingStyle.Sew.CustomWordOrder[3].ToString());
                    }
                }

                if(workingStyle.Sew.MirroredStyle != null) {
                    cboxSewMirror.Text = workingStyle.Sew.MirroredStyle;
                }

                if(workingStyle.Sew.Exceptions != null) {
                    listBoxSewExPaths.DataSource = new BindingList<Data_Exception>(workingStyle.Sew.Exceptions);
                    listBoxSewExPaths.DisplayMember = "Path";

                    if((Data_Exception)listBoxSewExPaths.SelectedItem != null) {
                        listBoxSewExConditions.DataSource = new BindingList<string>(((Data_Exception)listBoxSewExPaths.SelectedItem).Conditions);
                    }
                }
            }

            if(workingStyle.Stone != null) {
                cboxStoneRule.SelectedIndex = cboxStoneRule.FindStringExact(workingStyle.Stone.Rule);

                if(workingStyle.Stone.CustomWordOrder != null) {
                    if(workingStyle.Stone.CustomWordOrder.Count >= 1) {
                        cboxStoneWordOrder1.SelectedIndex = cboxStoneWordOrder1.FindString(workingStyle.Stone.CustomWordOrder[0].ToString());
                    }
                    if(workingStyle.Stone.CustomWordOrder.Count >= 2) {
                        cboxStoneWordOrder2.SelectedIndex = cboxStoneWordOrder2.FindString(workingStyle.Stone.CustomWordOrder[1].ToString());
                    }
                    if(workingStyle.Stone.CustomWordOrder.Count >= 3) {
                        cboxStoneWordOrder3.SelectedIndex = cboxStoneWordOrder3.FindString(workingStyle.Stone.CustomWordOrder[2].ToString());
                    }
                    if(workingStyle.Stone.CustomWordOrder.Count >= 4) {
                        cboxStoneWordOrder4.SelectedIndex = cboxStoneWordOrder4.FindString(workingStyle.Stone.CustomWordOrder[3].ToString());
                    }
                }

                if(workingStyle.Stone.MirroredStyle != null) {
                    cboxStoneMirror.Text = workingStyle.Stone.MirroredStyle;
                }

                if(workingStyle.Stone.Exceptions != null) {
                    listBoxStoneExPaths.DataSource = new BindingList<Data_Exception>(workingStyle.Stone.Exceptions);
                    listBoxStoneExPaths.DisplayMember = "Path";

                    if((Data_Exception)listBoxStoneExPaths.SelectedItem != null) {
                        listBoxStoneExConditions.DataSource = new BindingList<string>(((Data_Exception)listBoxStoneExPaths.SelectedItem).Conditions);
                    }
                }
            }
        }

        private List<int> BuildWordOrder(ComboBox w1, ComboBox w2, ComboBox w3, ComboBox w4) {
            List<int> words = new List<int>();
            if(w1.SelectedIndex > 0) words.Add((int)w1.SelectedItem);
            if(w2.SelectedIndex > 0) words.Add((int)w2.SelectedItem);
            if(w3.SelectedIndex > 0) words.Add((int)w3.SelectedItem);
            if(w4.SelectedIndex > 0) words.Add((int)w4.SelectedItem);
            return words;
        }

        //CUT SECTION
        private void buttonCutClearRule_Click(object sender, EventArgs e) {
            cboxCutRule.SelectedIndex = -1;
            workingStyle.Cut.Rule = null;
        }

        private void cboxCutRule_SelectedIndexChanged(object sender, EventArgs e) {}

        private void buttonCutResetWords_Click(object sender, EventArgs e) {
            cboxCutWordOrder1.SelectedIndex = -1;
            cboxCutWordOrder2.SelectedIndex = -1;
            cboxCutWordOrder2.Enabled = false;
            cboxCutWordOrder3.SelectedIndex = -1;
            cboxCutWordOrder3.Enabled = false;
            cboxCutWordOrder4.SelectedIndex = -1;
            cboxCutWordOrder4.Enabled = false;
        }

        private void cboxCutWordOrder1_SelectedIndexChanged(object sender, EventArgs e) {
            cboxCutWordOrder2.Items.Clear();
            cboxCutWordOrder2.Items.AddRange(wordNums);
            if(cboxCutWordOrder1.SelectedIndex > 0) {
                cboxCutWordOrder2.Items.Remove(cboxCutWordOrder1.SelectedItem);
            }
            cboxCutWordOrder2.SelectedIndex = -1;
            cboxCutWordOrder3.SelectedIndex = -1;
            cboxCutWordOrder4.SelectedIndex = -1;
            cboxCutWordOrder2.Enabled = (cboxCutWordOrder1.SelectedIndex > 0);
            cboxCutWordOrder3.Enabled = (cboxCutWordOrder2.SelectedIndex > 0);
            cboxCutWordOrder4.Enabled = (cboxCutWordOrder3.SelectedIndex > 0);
        }

        private void cboxCutWordOrder2_SelectedIndexChanged(object sender, EventArgs e) {
            cboxCutWordOrder3.Items.Clear();
            cboxCutWordOrder3.Items.AddRange(wordNums);
            if(cboxCutWordOrder2.SelectedIndex > 0) {
                cboxCutWordOrder3.Items.Remove(cboxCutWordOrder1.SelectedItem);
                cboxCutWordOrder3.Items.Remove(cboxCutWordOrder2.SelectedItem);
            }
            cboxCutWordOrder3.SelectedIndex = -1;
            cboxCutWordOrder4.SelectedIndex = -1;
            cboxCutWordOrder3.Enabled = (cboxCutWordOrder2.SelectedIndex > 0);
            cboxCutWordOrder4.Enabled = (cboxCutWordOrder3.SelectedIndex > 0);
        }

        private void cboxCutWordOrder3_SelectedIndexChanged(object sender, EventArgs e) {
            cboxCutWordOrder4.Items.Clear();
            cboxCutWordOrder4.Items.AddRange(wordNums);
            if(cboxCutWordOrder3.SelectedIndex > 0) {
                cboxCutWordOrder4.Items.Remove(cboxCutWordOrder1.SelectedItem);
                cboxCutWordOrder4.Items.Remove(cboxCutWordOrder2.SelectedItem);
                cboxCutWordOrder4.Items.Remove(cboxCutWordOrder3.SelectedItem);
            }
            cboxCutWordOrder4.SelectedIndex = -1;
            cboxCutWordOrder4.Enabled = (cboxCutWordOrder3.SelectedIndex > 0);
        }

        private void cboxCutWordOrder4_SelectedIndexChanged(object sender, EventArgs e) {}

        private void buttonCutResetMirror_Click(object sender, EventArgs e) {
            cboxCutMirror.SelectedIndex = -1;
        }

        private void cboxCutMirror_SelectedIndexChanged(object sender, EventArgs e) {}

        private void listBoxCutExPaths_SelectedIndexChanged(object sender, EventArgs e) {
            Data_Exception ex = (Data_Exception)listBoxCutExPaths.SelectedItem;
            if(ex.Conditions != null) {
                listBoxCutExConditions.DataSource = new BindingList<string>(ex.Conditions);
            } else {
                listBoxCutExConditions.DataSource = null;
            }
        }

        private void buttonCutExPathsAdd_Click(object sender, EventArgs e) {
            string input = Messenger.Prompt("Enter a new path", "Add Path");
            if(input.Length > 0) {
                if(listBoxCutExPaths.DataSource == null) {
                    workingStyle.Cut.Exceptions = new List<Data_Exception>();
                    listBoxCutExPaths.DataSource = new BindingList<Data_Exception>(workingStyle.Cut.Exceptions);
                    listBoxCutExPaths.DisplayMember = "Path";
                }

                ((BindingList<Data_Exception>)listBoxCutExPaths.DataSource).Add(new Data_Exception() {
                    Path = input
                });
            }
        }

        private void buttonCutExPathsDelete_Click(object sender, EventArgs e) {
            Data_Exception ex = (Data_Exception)listBoxCutExPaths.SelectedItem;
            if(ex != null) {
                if(!Messenger.Show($"Are you sure you want to remove {ex.Path} and any conditions?",
                                   "Confirm Removal",
                                   MessageButtons.YesNo)) {
                    return;
                }

                ((BindingList<Data_Exception>)listBoxCutExPaths.DataSource).Remove(ex);
                if(((BindingList<Data_Exception>)listBoxCutExPaths.DataSource).Count > 0) {
                    //NOTE(adam): next item is automatically selected
                    ex = (Data_Exception)listBoxCutExPaths.SelectedItem;
                    listBoxCutExConditions.DataSource = new BindingList<string>(ex.Conditions);
                } else {
                    listBoxCutExPaths.DataSource = null;
                }
            }
        }

        private void listBoxCutExConditions_SelectedIndexChanged(object sender, EventArgs e) {}

        private void buttonCutExConditionsAdd_Click(object sender, EventArgs e) {
            string input = Messenger.Prompt("Enter a new condition", "Add Condition");
            if(input.Length > 0) {
                if(listBoxCutExConditions.DataSource == null) {
                    ((Data_Exception)listBoxCutExPaths.SelectedItem).Conditions = new List<string>();
                    listBoxCutExConditions.DataSource = new BindingList<string>(((Data_Exception)listBoxCutExPaths.SelectedItem).Conditions);
                }

                ((BindingList<string>)listBoxCutExConditions.DataSource).Add(input);
            }
        }

        private void buttonCutExConditionsDelete_Click(object sender, EventArgs e) {
            string cond = (string)listBoxCutExConditions.SelectedItem;
            if(cond != null) {
                if(!Messenger.Show($"Are you sure you want to remove the {cond} conditions?",
                                   "Confirm Removal",
                                   MessageButtons.YesNo)) {
                    return;
                }
                ((BindingList<string>)listBoxCutExConditions.DataSource).Remove(cond);

                if(((BindingList<string>)listBoxCutExConditions.DataSource).Count == 0) {
                    ((Data_Exception)listBoxCutExPaths.SelectedItem).Conditions = null;
                    listBoxCutExConditions.DataSource = null;
                }
            }
        }


        //SEW SECTION
        private void buttonSewClearRule_Click(object sender, EventArgs e) {
            cboxSewRule.SelectedIndex = -1;
            workingStyle.Sew.Rule = null;
        }

        private void cboxSewRule_SelectedIndexChanged(object sender, EventArgs e) { }

        private void buttonSewResetWords_Click(object sender, EventArgs e) {
            cboxSewWordOrder1.SelectedIndex = -1;
            cboxSewWordOrder2.SelectedIndex = -1;
            cboxSewWordOrder2.Enabled = false;
            cboxSewWordOrder3.SelectedIndex = -1;
            cboxSewWordOrder3.Enabled = false;
            cboxSewWordOrder4.SelectedIndex = -1;
            cboxSewWordOrder4.Enabled = false;
        }

        private void cboxSewWordOrder1_SelectedIndexChanged(object sender, EventArgs e) {
            cboxSewWordOrder2.Items.Clear();
            cboxSewWordOrder2.Items.AddRange(wordNums);
            if(cboxSewWordOrder1.SelectedIndex > 0) {
                cboxSewWordOrder2.Items.Remove(cboxSewWordOrder1.SelectedItem);
            }
            cboxSewWordOrder2.SelectedIndex = -1;
            cboxSewWordOrder3.SelectedIndex = -1;
            cboxSewWordOrder4.SelectedIndex = -1;
            cboxSewWordOrder2.Enabled = (cboxSewWordOrder1.SelectedIndex > 0);
            cboxSewWordOrder3.Enabled = (cboxSewWordOrder2.SelectedIndex > 0);
            cboxSewWordOrder4.Enabled = (cboxSewWordOrder3.SelectedIndex > 0);
        }

        private void cboxSewWordOrder2_SelectedIndexChanged(object sender, EventArgs e) {
            cboxSewWordOrder3.Items.Clear();
            cboxSewWordOrder3.Items.AddRange(wordNums);
            if(cboxSewWordOrder2.SelectedIndex > 0) {
                cboxSewWordOrder3.Items.Remove(cboxSewWordOrder1.SelectedItem);
                cboxSewWordOrder3.Items.Remove(cboxSewWordOrder2.SelectedItem);
            }
            cboxSewWordOrder3.SelectedIndex = -1;
            cboxSewWordOrder4.SelectedIndex = -1;
            cboxSewWordOrder3.Enabled = (cboxSewWordOrder2.SelectedIndex > 0);
            cboxSewWordOrder4.Enabled = (cboxSewWordOrder3.SelectedIndex > 0);
        }

        private void cboxSewWordOrder3_SelectedIndexChanged(object sender, EventArgs e) {
            cboxSewWordOrder4.Items.Clear();
            cboxSewWordOrder4.Items.AddRange(wordNums);
            if(cboxSewWordOrder3.SelectedIndex > 0) {
                cboxSewWordOrder4.Items.Remove(cboxSewWordOrder1.SelectedItem);
                cboxSewWordOrder4.Items.Remove(cboxSewWordOrder2.SelectedItem);
                cboxSewWordOrder4.Items.Remove(cboxSewWordOrder3.SelectedItem);
            }
            cboxSewWordOrder4.SelectedIndex = -1;
            cboxSewWordOrder4.Enabled = (cboxSewWordOrder3.SelectedIndex > 0);
        }

        private void cboxSewWordOrder4_SelectedIndexChanged(object sender, EventArgs e) { }

        private void buttonSewResetMirror_Click(object sender, EventArgs e) {
            cboxSewMirror.SelectedIndex = -1;
        }

        private void cboxSewMirror_SelectedIndexChanged(object sender, EventArgs e) { }

        private void listBoxSewExPaths_SelectedIndexChanged(object sender, EventArgs e) {
            Data_Exception ex = (Data_Exception)listBoxSewExPaths.SelectedItem;
            if(ex.Conditions != null) {
                listBoxSewExConditions.DataSource = new BindingList<string>(ex.Conditions);
            } else {
                listBoxSewExConditions.DataSource = null;
            }
        }

        private void buttonSewExPathsAdd_Click(object sender, EventArgs e) {
            string input = Messenger.Prompt("Enter a new path", "Add Path");
            if(input.Length > 0) {
                if(listBoxSewExPaths.DataSource == null) {
                    workingStyle.Sew.Exceptions = new List<Data_Exception>();
                    listBoxSewExPaths.DataSource = new BindingList<Data_Exception>(workingStyle.Sew.Exceptions);
                    listBoxSewExPaths.DisplayMember = "Path";
                }

                ((BindingList<Data_Exception>)listBoxSewExPaths.DataSource).Add(new Data_Exception() {
                    Path = input
                });
            }
        }

        private void buttonSewExPathsDelete_Click(object sender, EventArgs e) {
            Data_Exception ex = (Data_Exception)listBoxSewExPaths.SelectedItem;
            if(ex != null) {
                if(!Messenger.Show($"Are you sure you want to remove {ex.Path} and any conditions?",
                                   "Confirm Removal",
                                   MessageButtons.YesNo)) {
                    return;
                }

                ((BindingList<Data_Exception>)listBoxSewExPaths.DataSource).Remove(ex);
                if(((BindingList<Data_Exception>)listBoxSewExPaths.DataSource).Count > 0) {
                    //NOTE(adam): next item is automatically selected
                    ex = (Data_Exception)listBoxSewExPaths.SelectedItem;
                    listBoxSewExConditions.DataSource = new BindingList<string>(ex.Conditions);
                } else {
                    listBoxSewExPaths.DataSource = null;
                }
            }
        }

        private void listBoxSewExConditions_SelectedIndexChanged(object sender, EventArgs e) { }

        private void buttonSewExConditionsAdd_Click(object sender, EventArgs e) {
            string input = Messenger.Prompt("Enter a new condition", "Add Condition");
            if(input.Length > 0) {
                if(listBoxSewExConditions.DataSource == null) {
                    ((Data_Exception)listBoxSewExPaths.SelectedItem).Conditions = new List<string>();
                    listBoxSewExConditions.DataSource = new BindingList<string>(((Data_Exception)listBoxSewExPaths.SelectedItem).Conditions);
                }

                ((BindingList<string>)listBoxSewExConditions.DataSource).Add(input);
            }
        }

        private void buttonSewExConditionsDelete_Click(object sender, EventArgs e) {
            string cond = (string)listBoxSewExConditions.SelectedItem;
            if(cond != null) {
                if(!Messenger.Show($"Are you sure you want to remove the {cond} conditions?",
                                   "Confirm Removal",
                                   MessageButtons.YesNo)) {
                    return;
                }
                ((BindingList<string>)listBoxSewExConditions.DataSource).Remove(cond);

                if(((BindingList<string>)listBoxSewExConditions.DataSource).Count == 0) {
                    ((Data_Exception)listBoxSewExPaths.SelectedItem).Conditions = null;
                    listBoxSewExConditions.DataSource = null;
                }
            }
        }


        //STONE SECTION
        private void buttonStoneClearRule_Click(object sender, EventArgs e) {
            cboxStoneRule.SelectedIndex = -1;
            workingStyle.Stone.Rule = null;
        }

        private void cboxStoneRule_SelectedIndexChanged(object sender, EventArgs e) { }

        private void buttonStoneResetWords_Click(object sender, EventArgs e) {
            cboxStoneWordOrder1.SelectedIndex = -1;
            cboxStoneWordOrder2.SelectedIndex = -1;
            cboxStoneWordOrder2.Enabled = false;
            cboxStoneWordOrder3.SelectedIndex = -1;
            cboxStoneWordOrder3.Enabled = false;
            cboxStoneWordOrder4.SelectedIndex = -1;
            cboxStoneWordOrder4.Enabled = false;
        }

        private void cboxStoneWordOrder1_SelectedIndexChanged(object sender, EventArgs e) {
            cboxStoneWordOrder2.Items.Clear();
            cboxStoneWordOrder2.Items.AddRange(wordNums);
            if(cboxStoneWordOrder1.SelectedIndex > 0) {
                cboxStoneWordOrder2.Items.Remove(cboxStoneWordOrder1.SelectedItem);
            }
            cboxStoneWordOrder2.SelectedIndex = -1;
            cboxStoneWordOrder3.SelectedIndex = -1;
            cboxStoneWordOrder4.SelectedIndex = -1;
            cboxStoneWordOrder2.Enabled = (cboxStoneWordOrder1.SelectedIndex > 0);
            cboxStoneWordOrder3.Enabled = (cboxStoneWordOrder2.SelectedIndex > 0);
            cboxStoneWordOrder4.Enabled = (cboxStoneWordOrder3.SelectedIndex > 0);
        }

        private void cboxStoneWordOrder2_SelectedIndexChanged(object sender, EventArgs e) {
            cboxStoneWordOrder3.Items.Clear();
            cboxStoneWordOrder3.Items.AddRange(wordNums);
            if(cboxStoneWordOrder2.SelectedIndex > 0) {
                cboxStoneWordOrder3.Items.Remove(cboxStoneWordOrder1.SelectedItem);
                cboxStoneWordOrder3.Items.Remove(cboxStoneWordOrder2.SelectedItem);
            }
            cboxStoneWordOrder3.SelectedIndex = -1;
            cboxStoneWordOrder4.SelectedIndex = -1;
            cboxStoneWordOrder3.Enabled = (cboxStoneWordOrder2.SelectedIndex > 0);
            cboxStoneWordOrder4.Enabled = (cboxStoneWordOrder3.SelectedIndex > 0);
        }

        private void cboxStoneWordOrder3_SelectedIndexChanged(object sender, EventArgs e) {
            cboxStoneWordOrder4.Items.Clear();
            cboxStoneWordOrder4.Items.AddRange(wordNums);
            if(cboxStoneWordOrder3.SelectedIndex > 0) {
                cboxStoneWordOrder4.Items.Remove(cboxStoneWordOrder1.SelectedItem);
                cboxStoneWordOrder4.Items.Remove(cboxStoneWordOrder2.SelectedItem);
                cboxStoneWordOrder4.Items.Remove(cboxStoneWordOrder3.SelectedItem);
            }
            cboxStoneWordOrder4.SelectedIndex = -1;
            cboxStoneWordOrder4.Enabled = (cboxStoneWordOrder3.SelectedIndex > 0);
        }

        private void cboxStoneWordOrder4_SelectedIndexChanged(object sender, EventArgs e) { }

        private void buttonStoneResetMirror_Click(object sender, EventArgs e) {
            cboxStoneMirror.SelectedIndex = -1;
        }

        private void cboxStoneMirror_SelectedIndexChanged(object sender, EventArgs e) { }

        private void listBoxStoneExPaths_SelectedIndexChanged(object sender, EventArgs e) {
            Data_Exception ex = (Data_Exception)listBoxStoneExPaths.SelectedItem;
            if(ex.Conditions != null) {
                listBoxStoneExConditions.DataSource = new BindingList<string>(ex.Conditions);
            } else {
                listBoxStoneExConditions.DataSource = null;
            }
        }

        private void buttonStoneExPathsAdd_Click(object sender, EventArgs e) {
            string input = Messenger.Prompt("Enter a new path", "Add Path");
            if(input.Length > 0) {
                if(listBoxStoneExPaths.DataSource == null) {
                    workingStyle.Stone.Exceptions = new List<Data_Exception>();
                    listBoxStoneExPaths.DataSource = new BindingList<Data_Exception>(workingStyle.Stone.Exceptions);
                    listBoxStoneExPaths.DisplayMember = "Path";
                }

                ((BindingList<Data_Exception>)listBoxStoneExPaths.DataSource).Add(new Data_Exception() {
                    Path = input
                });
            }
        }

        private void buttonStoneExPathsDelete_Click(object sender, EventArgs e) {
            Data_Exception ex = (Data_Exception)listBoxStoneExPaths.SelectedItem;
            if(ex != null) {
                if(!Messenger.Show($"Are you sure you want to remove {ex.Path} and any conditions?",
                                   "Confirm Removal",
                                   MessageButtons.YesNo)) {
                    return;
                }

                ((BindingList<Data_Exception>)listBoxStoneExPaths.DataSource).Remove(ex);
                if(((BindingList<Data_Exception>)listBoxStoneExPaths.DataSource).Count > 0) {
                    //NOTE(adam): next item is automatically selected
                    ex = (Data_Exception)listBoxStoneExPaths.SelectedItem;
                    listBoxStoneExConditions.DataSource = new BindingList<string>(ex.Conditions);
                } else {
                    listBoxStoneExPaths.DataSource = null;
                }
            }
        }

        private void listBoxStoneExConditions_SelectedIndexChanged(object sender, EventArgs e) { }

        private void buttonStoneExConditionsAdd_Click(object sender, EventArgs e) {
            string input = Messenger.Prompt("Enter a new condition", "Add Condition");
            if(input.Length > 0) {
                if(listBoxStoneExConditions.DataSource == null) {
                    ((Data_Exception)listBoxStoneExPaths.SelectedItem).Conditions = new List<string>();
                    listBoxStoneExConditions.DataSource = new BindingList<string>(((Data_Exception)listBoxStoneExPaths.SelectedItem).Conditions);
                }

                ((BindingList<string>)listBoxStoneExConditions.DataSource).Add(input);
            }
        }

        private void buttonStoneExConditionsDelete_Click(object sender, EventArgs e) {
            string cond = (string)listBoxStoneExConditions.SelectedItem;
            if(cond != null) {
                if(!Messenger.Show($"Are you sure you want to remove the {cond} conditions?",
                                   "Confirm Removal",
                                   MessageButtons.YesNo)) {
                    return;
                }
                ((BindingList<string>)listBoxStoneExConditions.DataSource).Remove(cond);

                if(((BindingList<string>)listBoxStoneExConditions.DataSource).Count == 0) {
                    ((Data_Exception)listBoxStoneExPaths.SelectedItem).Conditions = null;
                    listBoxStoneExConditions.DataSource = null;
                }
            }
        }

        private void buttonAccept_Click(object sender, EventArgs e) {
            //NOTE(adam): copying non-binding elements to working style object
            //CUT
            workingStyle.Cut.Rule = (cboxCutRule.SelectedIndex > -1) ? ((Data_PathRule)cboxCutRule.SelectedItem).Id : null;

            List<int> cutWords = BuildWordOrder(cboxCutWordOrder1, cboxCutWordOrder2, cboxCutWordOrder3, cboxCutWordOrder4);
            workingStyle.Cut.CustomWordOrder = (cutWords.Count > 0) ? cutWords : null;
            
            workingStyle.Cut.MirroredStyle = (cboxCutMirror.SelectedIndex > -1) ? (string)cboxCutMirror.SelectedItem : null;


            //SEW
            workingStyle.Sew.Rule = (cboxSewRule.SelectedIndex > -1) ? ((Data_PathRule)cboxSewRule.SelectedItem).Id : null;

            List<int> sewWords = BuildWordOrder(cboxCutWordOrder1, cboxCutWordOrder2, cboxCutWordOrder3, cboxCutWordOrder4);
            workingStyle.Sew.CustomWordOrder = (sewWords.Count > 0) ? sewWords : null;

            workingStyle.Sew.MirroredStyle = (cboxSewMirror.SelectedIndex > -1) ? (string)cboxSewMirror.SelectedItem : null;


            //STONE
            workingStyle.Stone.Rule = (cboxStoneRule.SelectedIndex > -1) ? ((Data_PathRule)cboxStoneRule.SelectedItem).Id : null;

            List<int> stoneWords = BuildWordOrder(cboxStoneWordOrder1, cboxStoneWordOrder2, cboxStoneWordOrder3, cboxStoneWordOrder4);
            workingStyle.Stone.CustomWordOrder = (stoneWords.Count > 0) ? stoneWords : null;

            workingStyle.Stone.MirroredStyle = (cboxStoneMirror.SelectedIndex > -1) ? (string)cboxStoneMirror.SelectedItem : null;


            //NOTE(adam): removing unused temporary StyleData
            if(workingStyle.Cut.Rule == null &&
               (workingStyle.Cut.CustomWordOrder == null || workingStyle.Cut.CustomWordOrder.Count == 0) &&
               workingStyle.Cut.MirroredStyle == null && 
               workingStyle.Cut.Exceptions == null) workingStyle.Cut = null;
            if(workingStyle.Sew.Rule == null &&
               (workingStyle.Sew.CustomWordOrder == null || workingStyle.Sew.CustomWordOrder.Count == 0) &&
               workingStyle.Sew.MirroredStyle == null &&
               workingStyle.Sew.Exceptions == null) workingStyle.Sew = null;
            if(workingStyle.Stone.Rule == null &&
               (workingStyle.Stone.CustomWordOrder == null || workingStyle.Stone.CustomWordOrder.Count == 0) &&
               workingStyle.Stone.MirroredStyle == null &&
               workingStyle.Stone.Exceptions == null) workingStyle.Stone = null;

            if(workingStyle.Cut == null && workingStyle.Sew == null && workingStyle.Stone == null) {
                Messenger.Show("Nothing to save.", "No Data");

                //NOTE(adam): reinitialize StyleData from being removed
                if(workingStyle.Cut == null) workingStyle.Cut = new Data_StyleData();
                if(workingStyle.Sew == null) workingStyle.Sew = new Data_StyleData();
                if(workingStyle.Stone == null) workingStyle.Stone = new Data_StyleData();

                this.DialogResult = DialogResult.None;

                return;
            }

            this.EditedStyle = this.workingStyle;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e) {
            workingStyle = null;
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}

using prjGIUnimage.bus;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prjGIUnimage
{
    public partial class frmCollections : Form
    {
        int current = 0;
        bool Modify = false;
        clsListData stCol = new clsListData();
        clsListData stFil = new clsListData();
        clsListCollections lstCol = new clsListCollections();
        clsListElements eleCol = new clsListElements();

        public frmCollections()
        {
            InitializeComponent();
        }

        private void clsCollections_Load(object sender, EventArgs e)
        {
            //Update the Collection and Division tables of SILEX in "DataTables".
            this.ActiveControl = txtSearch;
            clsSilex.UpDateCollection();
            clsSilex.UpDateDivision();

            try
            {
                //Update the Collection tables
                UpdateTableCollections();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            DeactivateTexts();
            try
            {
                stCol.DataByGroup(103);
                stCol.RemoveLastItem();
                stFil.DataByGroup(103);

                cboStatus.DisplayMember = "DataDesc_fra";
                cboStatus.ValueMember = "DataValue";
                cboStatus.DataSource = stCol.Elements;

                UploadComboFilter();

                eleCol.GetElementsCollection(cboFilter.SelectedIndex);
                lstCol.AllCollections();
                LinkListCollections();
                CollectionTotext(lstCol.CollectionByID(current));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UploadComboFilter()
        {
            foreach (clsData ele in stFil.Elements)
            {
                cboFilter.Items.Add(ele.DataDesc_fra);
            }
            cboFilter.SelectedIndex = 3;
        }

        private void CollectionTotext(clsCollection Col)
        {
            try
            {
                txtStatusSX.Text = clsSilex.CollectionStatus(Col.CollectionID);
                txtCode.Text = Col.GetCollectionCode();
                txtComment.Text = Col.GICollectionComment;
                txtModifiedD.Text = Convert.ToString(Col.ModifiedDate);
                txtModifiedU.Text = clsUser.GetUserName(Col.ModifiedByUserID);
                cboStatus.SelectedIndex = Col.GICollectionStatus < 2 ? Col.GICollectionStatus : -1;
                txtName.Text = Col.GetCollectionName();
                txtDesc.Text = Col.GetCollectionDesc();
                txtDivision.Text = Col.GetDivisionCode();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DeactivateTexts()
        {
            txtCode.ReadOnly = true;
            txtComment.ReadOnly = true;
            txtModifiedD.ReadOnly = true;
            txtModifiedU.ReadOnly = true;
            txtDivision.ReadOnly = true;
            txtName.ReadOnly = true;
            txtDesc.ReadOnly = true;
            txtStatusSX.ReadOnly = true;
            cboStatus.Enabled = false;
            btnSave.Enabled = false;
            btnModify.Enabled = true;
        }

        private void UpdateTableCollections()
        {
            clsListCollections giCol = new clsListCollections();
            clsListCollections sxCol = new clsListCollections();
            int counter = 0;

            giCol.AllCollections();
            sxCol.AllSXCollections();
            foreach (clsCollection ele in sxCol.Elements)
            {
                if (!giCol.Exists(ele))
                {
                    counter++;
                    ele.InsertGICollection();
                }
            }
            if (counter > 0)
            {
                MessageBox.Show(counter + " collections ont été mises à jour");
            }
        }

        private void LinkListCollections()
        {
            lstCollections.DisplayMember = "Code";
            lstCollections.ValueMember = "ElementID";
            lstCollections.DataSource = eleCol.Elements;
        }

        private void lstCollections_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Modify)
                {
                    MessageBox.Show("Enregistrez les modifications ou cliquez sur le bouton \"Retour\" pour annuler .....", "Exclamation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    CleanControls();
                    if (lstCollections.SelectedIndex >= 0 && lstCol.Quantity > 0)
                    {
                        current = Convert.ToInt32(lstCollections.SelectedValue);
                        CollectionTotext(lstCol.CollectionByID(current));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (clsFrmGlobals.frMP == null)
                {
                    clsFrmGlobals.frMP = new frmMenuPpal();
                    clsFrmGlobals.frMP.MdiParent = this.MdiParent;
                    clsFrmGlobals.frMP.FormClosed += new FormClosedEventHandler(frMPFromClosed);
                    clsFrmGlobals.frMP.Show();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void frMPFromClosed(object sender, FormClosedEventArgs e)
        {
            clsFrmGlobals.frMP = null;
            clsGlobals.GIPar.UpdateIDVariables();
            if (Application.OpenForms.Count == 1)
            {
                Application.Exit();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                if (clsFrmGlobals.frTP == null)
                {
                    clsFrmGlobals.frTP = new frmTablesPermanentes();
                    clsFrmGlobals.frTP.MdiParent = this.MdiParent;
                    clsFrmGlobals.frTP.FormClosed += new FormClosedEventHandler(frTPFromClosed);
                    clsFrmGlobals.frTP.Show();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void frTPFromClosed(object sender, FormClosedEventArgs e)
        {
            clsFrmGlobals.frTP = null;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Modify)
                {
                    MessageBox.Show("Enregistrez les modifications ou cliquez sur le bouton \"Retour\" pour annuler .....", "Exclamation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    current = 0;
                    if (!String.IsNullOrEmpty(txtSearch.Text))
                    {
                        eleCol.GetElementsCollection(cboFilter.SelectedIndex);
                        eleCol.FilterElements(txtSearch.Text.Trim().ToUpper());
                        if (eleCol.Quantity > 0)
                        {
                            CollectionTotext(lstCol.CollectionByID(current));
                            lstCollections.DataSource = null;
                            LinkListCollections();
                        }
                        else
                        {
                            LinkListCollections();
                            CleanControls();
                        }
                    }
                    else
                    {
                        eleCol.GetElementsCollection(cboFilter.SelectedIndex);
                        LinkListCollections();
                        CollectionTotext(lstCol.CollectionByID(current));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CleanControls()
        {
            txtCode.Clear();
            txtComment.Clear();
            txtDesc.Clear();
            txtDivision.Clear();
            txtModifiedD.Clear();
            txtModifiedU.Clear();
            txtName.Clear();
            txtStatusSX.Clear();
            cboStatus.SelectedIndex = -1;
        }

        private void cboFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Modify)
                {
                    MessageBox.Show("Enregistrez les modifications ou cliquez sur le bouton \"Retour\" pour annuler .....", "Exclamation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    eleCol.GetElementsCollection(cboFilter.SelectedIndex);
                    LinkListCollections();
                    if (eleCol.Quantity > 0 && lstCol.Quantity > 0)
                    {
                        CollectionTotext(lstCol.CollectionByID(current));
                    }
                    else
                    {
                        CleanControls();
                    }
                    txtSearch.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Modify = true;
                ActivateTexts();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ActivateTexts()
        {
            txtComment.ReadOnly = false;
            cboStatus.Enabled = true;
            btnModify.Enabled = false;
            btnSave.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateChildren(ValidationConstraints.Enabled))
                {
                    Modify = false;
                    clsCollection aCol = new clsCollection();
                    var tmpSelected = lstCollections.SelectedValue;
                    aCol = TextToCollection(Convert.ToInt32(tmpSelected));
                    aCol.UpdateGICollection();
                    DeactivateTexts();
                    eleCol.GetElementsCollection(cboFilter.SelectedIndex);
                    eleCol.FilterElements(txtSearch.Text.Trim().ToUpper());
                    lstCol.AllCollections();
                    LinkListCollections();
                    lstCollections.SelectedValue = tmpSelected;
                    MessageBox.Show("La collection a été mise à jour correctement", "Exclamation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private clsCollection TextToCollection(int SelectedValue)
        {
            clsCollection aTem = new clsCollection();
            aTem.GICollectionID = SelectedValue;
            aTem.GICollectionStatus = Convert.ToInt32(cboStatus.SelectedValue);
            aTem.GICollectionComment = txtComment.Text;
            
            return aTem;
        }

        private bool IsNumber(string text)
        {
            try
            {
                double x = Convert.ToInt32(text);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

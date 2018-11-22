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
            //cboFilter.Items.Add("Tout");
            cboFilter.SelectedIndex = 3;
        }

        private void CollectionTotext(clsCollection Col)
        {
            try
            {
                txtStatusSX.Text = clsSilex.CollectionStatus(Col.CollectionID);
                txtCode.Text = Col.GetCollectionCode();
                txtComment.Text = Col.GICollectionComment;
                //txtCommon.Text = Col.SurplusRateCommon.ToString();
                //txtIdentified.Text = Col.SurplusRateIdentified.ToString();
                txtModifiedD.Text = Convert.ToString(Col.ModifiedDate);
                txtModifiedU.Text = clsUser.GetUserName(Col.ModifiedByUserID);
                //txtOS.Text = Col.SurplusRateOS.ToString();
                //txtUnique.Text = Col.SurplusRateUnique.ToString();
                cboStatus.SelectedIndex = Col.GICollectionStatus < 2 ? Col.GICollectionStatus : -1;
                txtName.Text = Col.GetCollectionName();
                txtDesc.Text = Col.GetCollectionDesc();
                txtDivision.Text = Col.GetDivisionCode();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        private void DeactivateTexts()
        {
            txtCode.ReadOnly = true;
            txtComment.ReadOnly = true;
            //txtCommon.ReadOnly = true;
            //txtIdentified.ReadOnly = true;
            txtModifiedD.ReadOnly = true;
            txtModifiedU.ReadOnly = true;
            //txtOS.ReadOnly = true;
            //txtUnique.ReadOnly = true;
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
            if (Modify)
            {
                MessageBox.Show("Enregistrez les modifications ou cliquez sur le bouton \"Retour\" pour annuler .....", "Exclamation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                CleanControls();
                if (lstCollections.SelectedIndex >= 0)
                {
                    current = Convert.ToInt32(lstCollections.SelectedValue);
                    CollectionTotext(lstCol.CollectionByID(current));
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
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
            if (clsFrmGlobals.frTP == null)
            {
                clsFrmGlobals.frTP = new frmTablesPermanentes();
                clsFrmGlobals.frTP.MdiParent = this.MdiParent;
                clsFrmGlobals.frTP.FormClosed += new FormClosedEventHandler(frTPFromClosed);
                clsFrmGlobals.frTP.Show();
                this.Close();
            }
        }

        private void frTPFromClosed(object sender, FormClosedEventArgs e)
        {
            clsFrmGlobals.frTP = null;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
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
                    //eleCol.FilterListBy(txtSearch.Text, cboFilter.SelectedIndex);
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

        private void CleanControls()
        {
            txtCode.Clear();
            txtComment.Clear();
            //txtCommon.Clear();
            txtDesc.Clear();
            txtDivision.Clear();
            //txtIdentified.Clear();
            txtModifiedD.Clear();
            txtModifiedU.Clear();
            txtName.Clear();
            txtStatusSX.Clear();
            //txtOS.Clear();
            //txtUnique.Clear();
            cboStatus.SelectedIndex = -1;
        }

        private void cboFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Modify)
            {
                MessageBox.Show("Enregistrez les modifications ou cliquez sur le bouton \"Retour\" pour annuler .....", "Exclamation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                eleCol.GetElementsCollection(cboFilter.SelectedIndex);
                LinkListCollections();
                if (eleCol.Quantity > 0)
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

        private void button2_Click(object sender, EventArgs e)
        {
            Modify = true;
            ActivateTexts();
        }

        private void ActivateTexts()
        {
            txtComment.ReadOnly = false;
            //txtCommon.ReadOnly = false;
            //txtIdentified.ReadOnly = false;
            //txtOS.ReadOnly = false;
            //txtUnique.ReadOnly = false;
            cboStatus.Enabled = true;
            btnModify.Enabled = false;
            btnSave.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (ValidateChildren(ValidationConstraints.Enabled))
            {
                Modify = false;
                clsCollection aCol = new clsCollection();
                var tmpSelected = lstCollections.SelectedValue;
                aCol = TextToCollection(Convert.ToInt32(tmpSelected));
                aCol.UpdateGICollection();
                DeactivateTexts();
                //eleCol.GetElementsCollection(cboFilter.SelectedIndex);
                //eleCol.FilterListBy(txtSearch.Text, cboFilter.SelectedIndex);
                eleCol.GetElementsCollection(cboFilter.SelectedIndex);
                eleCol.FilterElements(txtSearch.Text.Trim().ToUpper());
                lstCol.AllCollections();
                LinkListCollections();
                //txtSearch.Text = "";
                lstCollections.SelectedValue = tmpSelected;
                MessageBox.Show("La collection a été mise à jour correctement", "Exclamation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private clsCollection TextToCollection(int SelectedValue)
        {
            clsCollection aTem = new clsCollection();
            aTem.GICollectionID = SelectedValue;
            aTem.GICollectionStatus = Convert.ToInt32(cboStatus.SelectedValue);
            aTem.GICollectionComment = txtComment.Text;
            //aTem.SurplusRateCommon = Convert.ToDouble(txtCommon.Text);
            //aTem.SurplusRateIdentified = Convert.ToDouble(txtIdentified.Text);
            //aTem.SurplusRateOS = Convert.ToDouble(txtOS.Text);
            //aTem.SurplusRateUnique = Convert.ToDouble(txtUnique.Text);

            return aTem;
        }

        //private void txtUnique_Validating(object sender, CancelEventArgs e)
        //{
        //    if (!IsNumber(txtUnique.Text))
        //    {
        //        e.Cancel = true;
        //        txtUnique.Focus();
        //        errorProvider1.SetError(txtUnique, "Entrez une valeur numérique");
        //    }
        //    else
        //    {
        //        e.Cancel = false;
        //        errorProvider1.SetError(txtUnique, null);
        //    }
        //}

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

        //private void txtCommon_Validating(object sender, CancelEventArgs e)
        //{
        //    if (!IsNumber(txtCommon.Text))
        //    {
        //        e.Cancel = true;
        //        txtCommon.Focus();
        //        errorProvider1.SetError(txtCommon, "Entrez une valeur numérique");
        //    }
        //    else
        //    {
        //        e.Cancel = false;
        //        errorProvider1.SetError(txtCommon, null);
        //    }
        //}

        //private void txtIdentified_Validating(object sender, CancelEventArgs e)
        //{
        //    if (!IsNumber(txtIdentified.Text))
        //    {
        //        e.Cancel = true;
        //        txtIdentified.Focus();
        //        errorProvider1.SetError(txtIdentified, "Entrez une valeur numérique");
        //    }
        //    else
        //    {
        //        e.Cancel = false;
        //        errorProvider1.SetError(txtIdentified, null);
        //    }
        //}

        //private void txtOS_Validating(object sender, CancelEventArgs e)
        //{
        //    if (!IsNumber(txtOS.Text))
        //    {
        //        e.Cancel = true;
        //        txtOS.Focus();
        //        errorProvider1.SetError(txtOS, "Entrez une valeur numérique");
        //    }
        //    else
        //    {
        //        e.Cancel = false;
        //        errorProvider1.SetError(txtOS, null);
        //    }
        //}
    }
}

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
    public partial class frmProductColor : Form
    {
        clsListProduct lstPro = new clsListProduct();
        clsListData stPro = new clsListData();
        clsListData stFil = new clsListData();
        bool Modify = false;

        public frmProductColor()
        {
            InitializeComponent();
        }

        private void frmProductColor_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txtSearch;
            clsSilex.UpDateProduct();
            try
            {
                UpdateTableProducts();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            DeactivateTexts();
            stPro.DataByGroup(101);
            stPro.RemoveLastItem();
            stPro.RemoveLastItem();
            stPro.RemoveLastItem();
            stFil.DataByGroup(101);

            cboStatus.DisplayMember = "DataDesc_fra";
            cboStatus.ValueMember = "DataValue";
            cboStatus.DataSource = stPro.Elements;

            cboFilter.DisplayMember = "DataDesc_fra";
            cboFilter.ValueMember = "DataValue";
            cboFilter.DataSource = stFil.Elements;

            lstPro.AllProducts(txtSearch.Text, cboFilter.SelectedValue);
            LinkListProducts();

            cboFilter.SelectedIndex = 5;
        }

        private void UpdateTableProducts()
        {
            clsListProduct giPro = new clsListProduct();
            clsListProduct sxPro = new clsListProduct();
            int counter = 0;

            giPro.AllProducts();
            sxPro.AllSXProducts();
            foreach (clsProduct ele in sxPro.Elements)
            {
                if (!giPro.Exists(ele))
                {
                    counter++;
                    ele.InsertGIProduct();
                }
            }
            if (counter > 0)
            {
                MessageBox.Show(counter + " products ont été mises à jour");
            }
        }

        private void LinkListProducts()
        {
            dgvProductColor.DataSource = lstPro.Elements;
            dgvProductColor.Columns[0].Visible = false;
            dgvProductColor.Columns[1].Visible = false;
            dgvProductColor.Columns[2].Visible = false;
            dgvProductColor.Columns[3].Visible = false;
            dgvProductColor.Columns[4].Visible = false;
            dgvProductColor.Columns[5].Visible = false;
            dgvProductColor.Columns[6].Visible = false;
            dgvProductColor.Columns[7].Visible = false;
            dgvProductColor.Columns[8].Visible = false;
            dgvProductColor.Columns[9].Visible = false;
            dgvProductColor.Columns[10].Visible = false;
            dgvProductColor.Columns[11].Visible = false;
            dgvProductColor.Columns[12].Visible = false;
            dgvProductColor.Columns[14].Visible = false;
            dgvProductColor.Columns[16].Visible = false;
            dgvProductColor.Columns[17].Visible = false;
            dgvProductColor.Columns[18].Visible = false;
            dgvProductColor.Columns[19].Visible = false;
            dgvProductColor.Columns[20].Visible = false;
            dgvProductColor.Columns[15].Width = 243;

            if (dgvProductColor.Rows.Count > 0)
            {
                dgvProductColor.Rows[0].Selected = true;
            }
        }

        private void CleanControls()
        {
            txtDesc.Clear();
            txtCode.Clear();
            txtCollection.Clear();
            txtColor.Clear();
            txtComment.Clear();
            txtDesc.Clear();
            txtGroup.Clear();
            txtModifiedD.Clear();
            txtModifiedU.Clear();
            txtShort.Clear();
            txtSurplus.Clear();
            txtSxStatus.Clear();
            cboStatus.SelectedIndex = -1;
        }

        private void dgvProductColor_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dgvProductColor_SelectionChanged(object sender, EventArgs e)
        {
            if (Modify)
            {
                MessageBox.Show("Enregistrez les modifications ou cliquez sur le bouton \"Retour\" pour annuler .....", "Exclamation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if (dgvProductColor.CurrentRow != null)
                {
                    ProductToText(lstPro.ElementByID(Convert.ToInt32(dgvProductColor.CurrentRow.Cells[0].Value)));
                }
            }
        }

        private void ProductToText(clsProduct Pro)
        {
            txtCode.Text = Convert.ToString(Pro.ProductCode);
            txtCollection.Text = Convert.ToString(Pro.CollectionName);
            txtColor.Text = Convert.ToString(Pro.ColorName);
            txtComment.Text = Convert.ToString(Pro.ProductComment);
            txtDesc.Text = Convert.ToString(Pro.ProductDesc);
            txtGroup.Text = Convert.ToString(Pro.GroupName);
            txtModifiedD.Text = Convert.ToString(Pro.ModifiedDate);
            txtModifiedU.Text = clsUser.GetUserName(Pro.ModifiedByUserID);
            txtShort.Text = Convert.ToString(Pro.ShortProductCode);
            txtSurplus.Text = Convert.ToString(Pro.SurplusRate);
            txtSxStatus.Text = Convert.ToString(Pro.ProductStatusDesc);
            cboStatus.SelectedIndex = Pro.GIProductStatus < 3 ? Pro.GIProductStatus : -1;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (Modify)
            {
                MessageBox.Show("Enregistrez les modifications ou cliquez sur le bouton \"Retour\" pour annuler .....", "Exclamation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                lstPro.AllProducts(txtSearch.Text, cboFilter.SelectedValue);
                LinkListProducts();
            }
        }

        private void cboFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Modify)
            {
                MessageBox.Show("Enregistrez les modifications ou cliquez sur le bouton \"Retour\" pour annuler .....", "Exclamation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                lstPro.AllProducts(txtSearch.Text, cboFilter.SelectedValue);
                LinkListProducts();
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

        private void button2_Click(object sender, EventArgs e)
        {
            ActivateTexts();
            Modify = true;
        }

        private void ActivateTexts()
        {
            txtComment.ReadOnly = false;
            txtSurplus.ReadOnly = false;
            cboStatus.Enabled = true;
            btnModify.Enabled = false;
            btnSave.Enabled = true;
        }

        private void DeactivateTexts()
        {
            txtCode.ReadOnly = true;
            txtCollection.ReadOnly = true;
            txtComment.ReadOnly = true;
            txtDesc.ReadOnly = true;
            txtGroup.ReadOnly = true;
            txtModifiedD.ReadOnly = true;
            txtModifiedU.ReadOnly = true;
            txtShort.ReadOnly = true;
            txtSurplus.ReadOnly = true;
            txtSxStatus.ReadOnly = true;
            txtColor.ReadOnly = true;
            cboStatus.Enabled = false;
            btnModify.Enabled = true;
            btnSave.Enabled = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (ValidateChildren(ValidationConstraints.Enabled))
            {
                if (Convert.ToInt32(txtSurplus.Text) > 100)
                {
                    if(MessageBox.Show("Vous êtes sûr d'utiliser un surplus supérieur à 100%", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        RegisterChanges();
                    }
                    else
                    {
                        txtSurplus.Focus();
                    }
                }
                else
                {
                    RegisterChanges();
                }
            }
        }

        private void RegisterChanges()
        {
            //Guardar focus
            int dgvIndex = dgvProductColor.CurrentRow.Index;

            Modify = false;
            clsProduct aDiv = new clsProduct();
            aDiv = TextToDivision(Convert.ToInt32(dgvProductColor.CurrentRow.Cells[0].Value));
            aDiv.UpdateGIProduct();
            DeactivateTexts();
            ProductToText(aDiv);
            lstPro.AllProducts(txtSearch.Text, cboFilter.SelectedValue);
            dgvProductColor.Rows[dgvIndex].Selected = true;
            MessageBox.Show("Le produit a été mise à jour correctement", "Exclamation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private clsProduct TextToDivision(int GIProductID)
        {
            clsProduct aTemp = new clsProduct();
            aTemp.CollectionName = Convert.ToString(txtCollection.Text);
            aTemp.ColorName = Convert.ToString(txtColor.Text);
            aTemp.GIProductID = GIProductID;
            aTemp.GIProductStatus = Convert.ToInt32(cboStatus.SelectedValue);
            aTemp.GroupName = Convert.ToString(txtGroup.Text);
            aTemp.ProductCode = Convert.ToString(txtCode.Text);
            aTemp.ProductComment = Convert.ToString(txtComment.Text);
            aTemp.ProductDesc = Convert.ToString(txtDesc.Text);
            aTemp.ProductStatusDesc = Convert.ToString(txtSxStatus.Text);
            aTemp.ShortProductCode = Convert.ToString(txtShort.Text);
            aTemp.SurplusRate = Convert.ToDouble(txtSurplus.Text);
            return aTemp;
        }

        private void txtSurplus_Validating(object sender, CancelEventArgs e)
        {
            if (!IsNumber(txtSurplus.Text))
            {
                e.Cancel = true;
                txtSurplus.Focus();
                errorProvider1.SetError(txtSurplus, "Entrez une valeur numérique");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtSurplus, null);
            }
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

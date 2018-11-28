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
    public partial class frmEquivalentProduct : Form
    {
        clsListElements Ele = new clsListElements();
        clsListSeason lstSea = new clsListSeason();
        clsListProductEqui lstPeq = new clsListProductEqui();
        int NewID = 0, EquID = 0;
        public frmEquivalentProduct()
        {
            InitializeComponent();
        }

        private void frmEquivalentProduct_Load(object sender, EventArgs e)
        {
            try
            {
                this.ActiveControl = txtSearchProduct;
                radNew.Checked = true;
                txtEquivalentProduct.ReadOnly = true;
                txtNewProduct.ReadOnly = true;
                Ele.GetProducts();
                lstSea.GetAllSeasons();

                cboSeason.DisplayMember = "SeasonName";
                cboSeason.ValueMember = "GISeasonID";
                cboSeason.DataSource = lstSea.Elements;

                cboSeason.SelectedIndex = -1;
                LinkListCollections();

                LinkListEquivalentProducts();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LinkListEquivalentProducts()
        {
            lstPeq.GetAllEquivalentProducts();
            dgvResult.DataSource = lstPeq.Elements;
            dgvResult.Columns[0].Visible = false;
            dgvResult.Columns[2].Visible = false;
            dgvResult.Columns[3].Visible = false;
            dgvResult.Columns[6].Visible = false;
            dgvResult.AutoResizeColumns();
        }

        private void txtSearchProduct_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string myText = txtSearchProduct.Text.Trim().ToUpper();

                Ele.GetProducts();
                Ele.FilterElements(myText);
                LinkListCollections();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LinkListCollections()
        {
            lstProducts.DisplayMember = "Full";
            lstProducts.ValueMember = "ElementID";
            lstProducts.DataSource = Ele.Elements;
        }

        private void lstProducts_SelectedIndexChanged(object sender, EventArgs e)
        {
            
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

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Ele.GetProducts();
                LinkListCollections();
                cboSeason.SelectedIndex = -1;
                txtNewProduct.Clear();
                txtEquivalentProduct.Clear();
                txtSearchProduct.Clear();
                radNew.Checked = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboSeason.SelectedIndex < 0)
                {
                    MessageBox.Show("Sélectionnez une saison...");
                }
                else
                {
                    if (string.IsNullOrEmpty(txtNewProduct.Text))
                    {
                        MessageBox.Show("Sélectionnez le nouveau produit...");
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(txtEquivalentProduct.Text))
                        {
                            MessageBox.Show("Sélectionnez le produit équivalent...");
                        }
                        else
                        {
                            if (MessageBox.Show("Etes-vous sûr de créer cette relation?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                            {
                                clsProductEqui Pequ = new clsProductEqui(NewID, EquID, Convert.ToInt32(cboSeason.SelectedValue));
                                Pequ.InsertProductEqui();
                                Ele.GetProducts();
                                LinkListCollections();
                                cboSeason.SelectedIndex = -1;
                                txtNewProduct.Clear();
                                txtEquivalentProduct.Clear();
                                txtSearchProduct.Clear();
                                radNew.Checked = true;
                                LinkListEquivalentProducts();
                            }
                            else
                            {
                                Ele.GetProducts();
                                LinkListCollections();
                                cboSeason.SelectedIndex = -1;
                                txtNewProduct.Clear();
                                txtEquivalentProduct.Clear();
                                txtSearchProduct.Clear();
                                radNew.Checked = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvResult.Rows.Count > 0)
                {
                    if (MessageBox.Show("Êtes-vous sûr de supprimer cette équivalence?", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        int vEleID = Convert.ToInt32(dgvResult.CurrentRow.Cells[0].Value.ToString());
                        clsProductEqui myPeq = lstPeq.GetEquiProductByID(vEleID);
                        myPeq.DeleteProductEqui();
                        LinkListEquivalentProducts();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lstProducts_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (radNew.Checked)
                {
                    txtNewProduct.Text = Ele.GetProductCode(lstProducts.SelectedValue);
                    NewID = Convert.ToInt32(lstProducts.SelectedValue);
                }
                else
                {
                    if (radEquivalent.Checked)
                    {
                        txtEquivalentProduct.Text = Ele.GetProductCode(lstProducts.SelectedValue);
                        EquID = Convert.ToInt32(lstProducts.SelectedValue);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

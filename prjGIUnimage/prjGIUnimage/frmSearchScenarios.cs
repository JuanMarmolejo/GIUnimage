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
    public partial class frmSearchScenarios : Form
    {
        clsListElements lstProVO = new clsListElements();
        clsListScenario lstScePr = new clsListScenario();
        clsListSeason lstSea = new clsListSeason();
        clsListData lstStatus = new clsListData();
        frmOrderProducts frOP = null;
        int Counter = 0;

        public frmSearchScenarios()
        {
            InitializeComponent();
        }

        private void frmSearchScenarios_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txtSearch;

            DeactivateTexts();

            lstSea.GetAllSeasons();
            lstProVO.GetListProductsWithVO();
            lstStatus.DataByGroup(102);

            dgvResult.DataSource = lstProVO.Elements;
            dgvResult.Columns[0].Visible = false;
            dgvResult.Columns[3].Visible = false;
            dgvResult.AutoResizeColumns();

            cboCurrentSaison.DisplayMember = "SeasonName";
            cboCurrentSaison.ValueMember = "GISeasonID";
            cboCurrentSaison.DataSource = lstSea.Elements;
            cboCurrentSaison.SelectedIndex = -1;

            cboStatus.DisplayMember = "DataDesc_fra";
            cboStatus.ValueMember = "DataValue";
            cboStatus.DataSource = lstStatus.Elements;
        }

        private void dgvResult_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvResult.CurrentRow != null)
            {
                int productID = Convert.ToInt32(dgvResult.CurrentRow.Cells[0].Value);
                lstScePr.GetListScenariosByProduct(productID);
                lbxScenarios.DisplayMember = "ScenarioCode";
                lbxScenarios.ValueMember = "GIScenarioID";
                lbxScenarios.DataSource = lstScePr.Elements;
            }
        }

        private void lbxScenarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            CleanControls();
            Counter++;
            if (lbxScenarios.SelectedIndex >= 0 && Counter > 3)
            {
                clsGlobals.GIPar.ScenarioID = Convert.ToInt32(lbxScenarios.SelectedValue);
                ScenarioTotext(lstScePr.ElementByID(clsGlobals.GIPar.ScenarioID));
            }
        }

        private void CleanControls()
        {
            txtCode.Clear();
            txtCreatedD.Clear();
            txtCreatedU.Clear();
            txtDesc.Clear();
            txtModifiedD.Clear();
            txtModifiedU.Clear();
            txtPreviousSeason.Clear();
            cboCurrentSaison.SelectedIndex = -1;
        }

        private void ScenarioTotext(clsScenario Sce)
        {
            clsSeason mySea = lstSea.GetSeasonByID(Sce.GISeasonID);
            txtCode.Text = Convert.ToString(Sce.ScenarioCode);
            txtCreatedD.Text = Convert.ToString(Sce.CreatedDate);
            txtCreatedU.Text = Convert.ToString(Sce.CreatedByUserID);
            txtDesc.Text = Convert.ToString(Sce.ScenarioDesc);
            txtModifiedD.Text = Convert.ToString(Sce.ModifiedDate);
            txtModifiedU.Text = Convert.ToString(Sce.ModifiedByUserID);
            cboStatus.SelectedIndex = Sce.ScenarioStatus;
            cboCurrentSaison.SelectedValue = mySea.GISeasonID;
            txtPreviousSeason.Text = mySea.SeasonPrecName;
            txtModifiedU.Text = clsUser.GetUserName(Sce.ModifiedByUserID);
            txtCreatedU.Text = clsUser.GetUserName(Sce.CreatedByUserID);
        }

        private void DeactivateTexts()
        {
            txtCode.ReadOnly = true;
            txtCreatedD.ReadOnly = true;
            txtCreatedU.ReadOnly = true;
            txtDesc.ReadOnly = true;
            txtModifiedD.ReadOnly = true;
            txtModifiedU.ReadOnly = true;
            txtPreviousSeason.ReadOnly = true;
            cboStatus.Enabled = false;
            cboCurrentSaison.Enabled = false;
        }

        private void lbxScenarios_DoubleClick(object sender, EventArgs e)
        {
            clsGlobals.GIPar.ProductColorID = Convert.ToInt32(dgvResult.CurrentRow.Cells[0].Value);
            clsGlobals.ParentProductID = clsGlobals.GIPar.ProductColorID;
            if (clsProductEqui.ExistsEquivalence(clsGlobals.GIPar.ProductColorID))
            {
                clsProductEqui myPequ = new clsProductEqui();
                myPequ.GetProductEquiByPColorID(clsGlobals.GIPar.ProductColorID);
                clsGlobals.ProductEquiID = myPequ.ProductEquiID;
                MessageBox.Show("Exite equivalencia avec " + myPequ.CodeEqui + " en " + myPequ.NameSeason);
            }
            if (frOP == null)
            {
                frOP = new frmOrderProducts();
                frOP.MdiParent = this.MdiParent;
                frOP.FormClosed += new FormClosedEventHandler(frOPFromClosed);
                frOP.Show();
            }
        }

        private void frOPFromClosed(object sender, FormClosedEventArgs e)
        {
            frOP = null;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string myText = txtSearch.Text.Trim().ToUpper();

            lstProVO.GetListProductsWithVO();
            lstProVO.FilterElements(myText);

            dgvResult.DataSource = lstProVO.Elements;
            dgvResult.Columns[0].Visible = false;
            dgvResult.Columns[3].Visible = false;
            dgvResult.AutoResizeColumns();
        }

        private void btnMenu_Click(object sender, EventArgs e)
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

        private void btnReturn_Click(object sender, EventArgs e)
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
    }
}

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
    public partial class frmNewScenario : Form
    {
        clsListData lstStatus = new clsListData();
        clsListScenario lstScn = new clsListScenario();
        clsListSeason lstSea = new clsListSeason();
        clsListSeason lstSecSea = new clsListSeason();
        clsScenario aDiv = new clsScenario();
        clsListProductEqui lstPequ = new clsListProductEqui();
        bool flagNew = false;

        public frmNewScenario()
        {
            InitializeComponent();
            cboSecondSeason.Enabled = false;
        }

        private void frmNewScenario_Load(object sender, EventArgs e)
        {
            try
            {
                DeactivateTexts();

                lstStatus.DataByGroup(102);
                lstSea.GetAllSeasons();
                lstSecSea.GetAllSeasons();

                cboCurrentSaison.DisplayMember = "SeasonName";
                cboCurrentSaison.ValueMember = "GISeasonID";
                cboCurrentSaison.DataSource = lstSea.Elements;
                cboCurrentSaison.SelectedIndex = -1;

                cboSecondSeason.DisplayMember = "SeasonName";
                cboSecondSeason.ValueMember = "GISeasonID";
                cboSecondSeason.DataSource = lstSecSea.Elements;
                cboSecondSeason.SelectedIndex = -1;

                cboStatus.DisplayMember = "DataDesc_fra";
                cboStatus.ValueMember = "DataValue";
                cboStatus.DataSource = lstStatus.Elements;

                lstScn.AllScenarios();
                LinkListScenarios();
                clsGlobals.GIPar.ScenarioID = Convert.ToInt32(lstScenarios.SelectedValue);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
            btnRecord.Enabled = false;
            btnModify.Enabled = true;
            btnNew.Enabled = true;
            cboSecondSeason.Enabled = false;
            chkSecondSeason.Enabled = false;
        }

        private void LinkListScenarios()
        {
            lstScenarios.DisplayMember = "ScenarioCode";
            lstScenarios.ValueMember = "GIScenarioID";
            lstScenarios.DataSource = lstScn.Elements;
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

        private void button8_Click(object sender, EventArgs e)
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

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                ActivateTexts();
                CleanControls();
                flagNew = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ActivateTexts()
        {
            txtCode.ReadOnly = false;
            txtDesc.ReadOnly = false;
            cboCurrentSaison.Enabled = true;
            btnRecord.Enabled = true;
            btnModify.Enabled = false;
            btnNew.Enabled = false;
            chkSecondSeason.Enabled = true;
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
            cboSecondSeason.SelectedIndex = -1;
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            try
            {
                if (clsGlobals.GIPar.ScenarioID == 0)
                {
                    MessageBox.Show("Il n'y a pas de scénarios.");
                }
                else
                {
                    if (clsFrmGlobals.frAS == null)
                    {
                        clsFrmGlobals.frAS = new frmAnalysisScenarioCopy();
                        clsFrmGlobals.frAS.MdiParent = this.MdiParent;
                        clsFrmGlobals.frAS.FormClosed += new FormClosedEventHandler(frASFromClosed);
                        clsFrmGlobals.frAS.Show();
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void frASFromClosed(object sender, FormClosedEventArgs e)
        {
            clsFrmGlobals.frAS = null;
        }

        private void btnRecord_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateChildren(ValidationConstraints.Enabled))
                {
                    clsGlobals.GIPar.GetVOParameters();
                    aDiv = TextToScenario();
                    clsGlobals.NextScenarioID = clsScenario.NextScenarioID();
                    clsGlobals.GISeasonID = aDiv.GISeasonID;
                    clsGlobals.OriginOfStoredProc = 1;

                    //abre formulario de espera
                    if (clsFrmGlobals.frES == null)
                    {
                        clsFrmGlobals.frES = new frmExecutionStoredProcedure();
                        clsFrmGlobals.frES.MdiParent = this.MdiParent;
                        clsFrmGlobals.frES.FormClosed += new FormClosedEventHandler(frESFromClosed);
                        clsFrmGlobals.frES.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void frESFromClosed(object sender, FormClosedEventArgs e)
        {
            if (clsGlobals.Flag)
            {
                //InsertVirtualProducts();
                aDiv.InsertScenario();
                GenerateCollectionTable();
                GenerateProductTable();
                UpdateEquivalentProducts();
                MessageBox.Show("Le scénario a été créé correctement", "Exclamation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                MessageBox.Show("Le scénario n'a pas été créé.", "Exclamation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            ActiveApplication();
            flagNew = false;
            DeactivateTexts();
            lstScn.AllScenarios();
            LinkListScenarios();
            clsFrmGlobals.frES = null;
        }

        private void UpdateEquivalentProducts()
        {
            int numberSeasons = 0;
            lstPequ.GetAllEquivalentProducts();
            foreach (clsProductEqui myPeq in lstPequ.Elements)
            {
                clsSeason seasonScenario = new clsSeason();
                clsSeason seasonPeque = new clsSeason();

                seasonScenario.GetSeasonByID(clsGlobals.GISeasonID);
                seasonPeque.GetSeasonByID(myPeq.SeasonID);

                numberSeasons = seasonScenario.NumberSeasons(seasonPeque);
                if (numberSeasons > 0)
                {
                    UpdateItems(myPeq, numberSeasons);
                }
            }
        }

        private void UpdateItems(clsProductEqui myPeq, int numberSeasons)
        {
            clsListScSalesHistory myProductBase = new clsListScSalesHistory();
            
            myProductBase.GetSalesHistoryByID(clsGlobals.GIPar.ScenarioID, clsProduct.GetProductColorIDFromGIProductID(myPeq.ProductBaseID));
            foreach(clsScSalesHistory ele in myProductBase.Elements)
            {
                if (numberSeasons == 2)
                {
                    ele.UpdateTwoItems(myPeq.ProductEquiID);
                }
                else
                {
                    ele.UpdateOneItems(myPeq.ProductEquiID);
                }
            }
        }

        private void ActiveApplication()
        {
            groupBox1.Visible = true;
            btnModify.Visible = true;
            btnNew.Visible = true;
            btnRecord.Visible = true;
            btnMenu.Visible = true;
            btnReturn.Visible = true;
            lblScenarios.Visible = true;
            lstScenarios.Visible = true;
        }

        private void DeactivateApplication()
        {
            groupBox1.Visible = false;
            btnModify.Visible = false;
            btnNew.Visible = false;
            btnRecord.Visible = false;
            btnMenu.Visible = false;
            btnReturn.Visible = false;
            lblScenarios.Visible = false;
            lstScenarios.Visible = false;
        }

        private void GenerateProductTable()
        {
            clsListProduct lstPro = new clsListProduct();
            lstPro.GetActiveElements();
            int vScenarioID = lstScn.IDLastRecord();
            foreach (clsProduct ele in lstPro.Elements)
            {
                clsScProduct scPro = new clsScProduct();
                scPro.ScenarioID = vScenarioID;
                scPro.InsertScProduct(ele);
            }
        }

        private void GenerateCollectionTable()
        {
            clsListCollections lstCol = new clsListCollections();
            lstCol.GetActiveElements();
            int vScenarioID = lstScn.IDLastRecord();
            foreach (clsCollection ele in lstCol.Elements)
            {
                clsScCollection scCol = new clsScCollection();
                scCol.ScenarioID = vScenarioID;
                scCol.InsertScCollection(ele);
            }
        }

        private clsScenario TextToScenario()
        {
            clsScenario aTem = new clsScenario();
            aTem.ScenarioCode = Convert.ToString(txtCode.Text);
            aTem.ScenarioDesc = Convert.ToString(txtDesc.Text);
            aTem.ScenarioStatus = Convert.ToInt32(cboStatus.SelectedValue);
            aTem.GISeasonID = Convert.ToInt32(cboCurrentSaison.SelectedValue);
            clsGlobals.SecondSeasonID = chkSecondSeason.Checked ? Convert.ToInt32(cboSecondSeason.SelectedValue) : -1;

            return aTem;
        }

        private void cboCurrentSaison_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtPreviousSeason.Text = lstSea.PreviousSeason(Convert.ToInt32(cboCurrentSaison.SelectedValue));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lstScenarios_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (clsFrmGlobals.frAS == null)
                {
                    clsFrmGlobals.frAS = new frmAnalysisScenarioCopy();
                    clsFrmGlobals.frAS.MdiParent = this.MdiParent;
                    clsFrmGlobals.frAS.FormClosed += new FormClosedEventHandler(frASFromClosed);
                    clsFrmGlobals.frAS.Show();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lstScenarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (flagNew)
                {
                    MessageBox.Show("Vous devez enregistrer les modifications", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    CleanControls();
                    if (lstScenarios.SelectedIndex >= 0)
                    {
                        clsGlobals.GIPar.ScenarioID = Convert.ToInt32(lstScenarios.SelectedValue);
                        ScenarioTotext(lstScn.ElementByID(clsGlobals.GIPar.ScenarioID));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ScenarioTotext(clsScenario Sce)
        {
            clsGlobals.mySea = lstSea.GetSeasonByID(Sce.GISeasonID);
            txtCode.Text = Convert.ToString(Sce.ScenarioCode);
            txtCreatedD.Text = Convert.ToString(Sce.CreatedDate);
            txtCreatedU.Text = Convert.ToString(Sce.CreatedByUserID);
            txtDesc.Text = Convert.ToString(Sce.ScenarioDesc);
            txtModifiedD.Text = Convert.ToString(Sce.ModifiedDate);
            txtModifiedU.Text = Convert.ToString(Sce.ModifiedByUserID);
            cboStatus.SelectedIndex = Sce.ScenarioStatus;
            cboCurrentSaison.SelectedValue = clsGlobals.mySea.GISeasonID;
            txtPreviousSeason.Text = clsGlobals.mySea.SeasonPrecName;
            txtModifiedU.Text = clsUser.GetUserName(Sce.ModifiedByUserID);
            txtCreatedU.Text = clsUser.GetUserName(Sce.CreatedByUserID);
        }

        private void cboCurrentSaison_Validating(object sender, CancelEventArgs e)
        {
            if (cboCurrentSaison.SelectedIndex < 0)
            {
                e.Cancel = true;
                cboCurrentSaison.Focus();
                errorProvider1.SetError(cboCurrentSaison, "Sélectionnez une saison");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(cboCurrentSaison, null);
            }
        }

        private void txtCode_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtCode.Text))
            {
                e.Cancel = true;
                txtCode.Focus();
                errorProvider1.SetError(txtCode, "Entrez un nom pour le scénario");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtCode, null);
            }
        }

        private void chkSecondSeason_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkSecondSeason.Checked)
                {
                    cboSecondSeason.Enabled = true;
                }
                else
                {
                    cboSecondSeason.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

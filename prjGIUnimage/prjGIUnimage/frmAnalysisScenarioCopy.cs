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
    public partial class frmAnalysisScenarioCopy : Form
    {
        int qtyGroups = 0;
        int qtyStyles = 0;
        int qtyColors = 0;
        int qtyCollections = 0;
        int qtySpecCollection = 0;
        int qtyCommon = 0;
        int qtyIdentify = 0;
        int qtyVOSpecCollection = 0;
        int qtyVOCommon = 0;
        int qtyVOIdentify = 0;
        clsListData lstStatus = new clsListData();
        clsScenario mySce = new clsScenario();
        clsListSeason lstSea = new clsListSeason();
        clsSeason mySea = new clsSeason();
        clsElement eleShip = new clsElement();
        clsListElements lstBillFrom = new clsListElements();
        clsListElements lstPurchaseType = new clsListElements();
        clsListElements lstDivisions = new clsListElements();
        clsListElements lstWhs = new clsListElements();
        clsListSeason lstSeasons = new clsListSeason();
        clsListElements lstCollections = new clsListElements();
        public frmAnalysisScenarioCopy()
        {
            InitializeComponent();
            clsGlobals.AvaibleFlag = false;
        }

        private void frmAnalysisScenarioCopy_Load(object sender, EventArgs e)
        {
            LoadComboBoxes();
            DeactivateControls();
            LoadScenarioInformation();
            InitializeGlobalLists();
            StatisticsWithoutSelection();
            DisplayStats();
        }

        private void LoadComboBoxes()
        {
            lstStatus.DataByGroup(102);
            cboStatus.DisplayMember = "DataDesc_fra";
            cboStatus.ValueMember = "DataValue";
            cboStatus.DataSource = lstStatus.Elements;

            lstBillFrom.GetBillFrom();
            cboBillFrom.DisplayMember = "Name";
            cboBillFrom.ValueMember = "ElementID";
            cboBillFrom.DataSource = lstBillFrom.Elements;

            lstPurchaseType.GetPurchaseType();
            cboPurchaseType.DisplayMember = "Name";
            cboPurchaseType.ValueMember = "ElementID";
            cboPurchaseType.DataSource = lstPurchaseType.Elements;

            lstWhs.GetWhs();
            cboWhs.DisplayMember = "Full";
            cboWhs.ValueMember = "ElementID";
            cboWhs.DataSource = lstWhs.Elements;

            lstDivisions.GetDivisions();
            cboDivision.DisplayMember = "Code";
            cboDivision.ValueMember = "ElementID";
            cboDivision.DataSource = lstDivisions.Elements;

            lstSeasons.GetAllSeasons();
            cboSeason.DisplayMember = "SeasonName";
            cboSeason.ValueMember = "GISeasonID";
            cboSeason.DataSource = lstSeasons.Elements;
        }

        private void DeactivateControls()
        {
            txtCode.ReadOnly = true;
            txtCurrentSeasons.ReadOnly = true;
            txtSeasonsPrevious.ReadOnly = true;
            txtDesc.ReadOnly = true;
            txtCollection.ReadOnly = true;
            txtColor.ReadOnly = true;
            txtGroup.ReadOnly = true;
            txtOrderNumber.ReadOnly = true;
            txtRef1.ReadOnly = true;
            txtRef2.ReadOnly = true;
            txtShipFrom.ReadOnly = true;
            txtStyle.ReadOnly = true;
            txtVOMessage.ReadOnly = true;
            txtVONote.ReadOnly = true;
            cboStatus.Enabled = false;
            cboBillFrom.Enabled = false;
            cboCollection.Enabled = false;
            cboDivision.Enabled = false;
            cboPurchaseType.Enabled = false;
            cboSeason.Enabled = false;
            cboWhs.Enabled = false;
            dtpArrivalDate.Enabled = false;
            dtpShipDate.Enabled = false;
            btnModify.Enabled = true;
            btnRes1.Enabled = true;
            btnRes2.Enabled = true;
            btnRes3.Enabled = true;
            btnRes4.Enabled = true;
            btnSave.Enabled = false;
            btnCommand1.Enabled = true;
            btnCommand2.Enabled = true;
            btnCommand3.Enabled = true;
            btnMenu.Enabled = true;
            btnNew.Enabled = true;
            btnReturn.Enabled = true;

            lblCollection.Visible = false;
            lblCouleur.Visible = false;
            lblGroupe.Visible = false;
            lblStyle.Visible = false;
        }

        private void LoadScenarioInformation()
        {
            mySce.GetScenarioByID(clsGlobals.GIPar.ScenarioID);
            lstSea.GetAllSeasons();
            mySea = lstSea.GetSeasonByID(mySce.GISeasonID);
            txtRef1.Text = mySce.ReferenceNo1;
            txtRef2.Text = mySce.ReferenceNo2;
            txtVOMessage.Text = mySce.VOMessage;
            txtVONote.Text = mySce.VONote;
            txtCode.Text = mySce.ScenarioCode;
            txtDesc.Text = mySce.ScenarioDesc;
            txtCurrentSeasons.Text = mySea.SeasonName;
            txtSeasonsPrevious.Text = mySea.SeasonPrecName;
            cboStatus.SelectedValue = mySce.ScenarioStatus;
            cboBillFrom.SelectedValue = mySce.VendorID.ToString();
            cboCollection.SelectedValue = mySce.CollectionID.ToString();
            cboDivision.SelectedValue = mySce.DivisionID.ToString();
            cboPurchaseType.SelectedValue = mySce.PurchaseTypeID.ToString();
            cboSeason.SelectedValue = mySce.GISeasonID;
            cboWhs.SelectedValue = mySce.DefaultWarehouseID.ToString();
            dtpShipDate.Value = mySce.ExpShippingDate;
            dtpArrivalDate.Value = mySce.ExpArrivalDate;
        }

        private void StatisticsWithoutSelection()
        {
            qtyGroups = clsGlobals.ListGroups.GetNumberGroups();
            qtyStyles = clsGlobals.ListStyles.GetNumberStyles();
            qtyColors = clsGlobals.ListColors.GetNumberColors();
            qtyCollections = clsGlobals.ListCollections.GetNumberCollections();
            qtySpecCollection = clsGlobals.ListCollections.GetNumberSpecCollection();
            qtyCommon = clsGlobals.ListCollections.GetNumberCommon();
            qtyIdentify = clsGlobals.ListCollections.GetNumberIdentify();
            qtyVOSpecCollection = clsGlobals.ListCollections.GetNumberVOSpecCollection();
            qtyVOCommon = clsGlobals.ListCollections.GetNumberVOCommon();
            qtyVOIdentify = clsGlobals.ListCollections.GetNumberVOIdentify();
        }

        private void InitializeGlobalLists()
        {
            clsGlobals.ListGroups = new clsListElements();
            clsGlobals.ListStyles = new clsListElements();
            clsGlobals.ListColors = new clsListElements();
            clsGlobals.ListCollections = new clsListElements();
        }

        private void DisplayStats()
        {
            txtGroup.Text = Convert.ToString(qtyGroups);
            txtStyle.Text = Convert.ToString(qtyStyles);
            txtColor.Text = Convert.ToString(qtyColors);
            txtCollection.Text = Convert.ToString(qtyCollections);
            infoCollection.Text = qtyVOSpecCollection + "/" + qtySpecCollection;
            infoCommon.Text = qtyVOCommon + "/" + qtyCommon;
            infoIdentify.Text = qtyVOIdentify + "/" + qtyIdentify;
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
            if (clsFrmGlobals.frNS == null)
            {
                clsFrmGlobals.frNS = new frmNewScenario();
                clsFrmGlobals.frNS.MdiParent = this.MdiParent;
                clsFrmGlobals.frNS.FormClosed += new FormClosedEventHandler(frNSFromClosed);
                clsFrmGlobals.frNS.Show();
                this.Close();
            }
        }

        private void frNSFromClosed(object sender, FormClosedEventArgs e)
        {
            clsFrmGlobals.frNS = null;
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            ActivateControls();
        }

        private void ActivateControls()
        {
            txtDesc.ReadOnly = false;
            txtRef1.ReadOnly = false;
            txtRef2.ReadOnly = false;
            txtVOMessage.ReadOnly = false;
            txtVONote.ReadOnly = false;
            cboStatus.Enabled = true;
            cboBillFrom.Enabled = true;
            cboPurchaseType.Enabled = true;
            cboSeason.Enabled = true;
            dtpArrivalDate.Enabled = true;
            dtpShipDate.Enabled = true;
            btnSave.Enabled = true;
            btnModify.Enabled = false;
            btnRes1.Enabled = false;
            btnRes2.Enabled = false;
            btnRes3.Enabled = false;
            btnRes4.Enabled = false;
            btnCommand1.Enabled = false;
            btnCommand2.Enabled = false;
            btnCommand3.Enabled = false;
            btnNew.Enabled = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            clsScenario mySce = new clsScenario();
            mySce = TextToScenario();
            mySce.UpdateScenario();
            MessageBox.Show("Les modifications ont été enregistrées");
            LoadScenarioInformation();
            DeactivateControls();
        }

        private clsScenario TextToScenario()
        {
            clsScenario mySce = new clsScenario();

            mySce.GIScenarioID = clsGlobals.GIPar.ScenarioID;
            mySce.ScenarioDesc = txtDesc.Text;
            mySce.ScenarioStatus = Convert.ToInt32(cboStatus.SelectedValue);

            mySce.CollectionID = Convert.ToInt32(cboCollection.SelectedValue);
            mySce.DefaultWarehouseID = Convert.ToInt32(cboWhs.SelectedValue); ;
            mySce.DivisionID = Convert.ToInt32(cboDivision.SelectedValue);
            mySce.ExpArrivalDate = dtpArrivalDate.Value;
            mySce.ExpShippingDate = dtpShipDate.Value;
            mySce.GIScenarioID = clsGlobals.GIPar.ScenarioID;
            mySce.PurchaseTypeID = Convert.ToInt32(cboPurchaseType.SelectedValue); ;
            mySce.ReferenceNo1 = txtRef1.Text;
            mySce.ReferenceNo2 = txtRef2.Text;
            mySce.ScenarioDesc = txtDesc.Text;
            mySce.ScenarioStatus = Convert.ToInt32(cboStatus.SelectedValue);
            mySce.VendorID = Convert.ToInt32(cboBillFrom.SelectedValue);
            mySce.VendorSiteID = eleShip.GetVendorSiteID(mySce.VendorID);
            mySce.VOMessage = txtVOMessage.Text;
            mySce.VONote = txtVONote.Text;
            mySce.VOSeasonID = Convert.ToInt32(cboSeason.SelectedValue);
            return mySce;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            NewSelection();
        }

        private void NewSelection()
        {
            dgvRes1.DataSource = null;
            clsGlobals.ListGroups = new clsListElements();
            dgvRes2.DataSource = null;
            clsGlobals.ListStyles = new clsListElements();
            dgvRes3.DataSource = null;
            clsGlobals.ListColors = new clsListElements();
            dgvRes4.DataSource = null;
            clsGlobals.ListCollections = new clsListElements();
            StatisticsWithoutSelection();
            DisplayStats();
        }

        private void btnRes1_Click(object sender, EventArgs e)
        {
            if (dgvRes1.Rows.Count > 0)
            {
                clsGlobals.lstBackUp = DataGridToList(dgvRes1);
            }
            else
            {
                clsGlobals.lstBackUp = new clsListElements();
            }
            dgvRes1.DataSource = null;
            clsGlobals.ListGroups = new clsListElements();
            dgvRes2.DataSource = null;
            clsGlobals.ListStyles = new clsListElements();
            dgvRes3.DataSource = null;
            clsGlobals.ListColors = new clsListElements();
            dgvRes4.DataSource = null;
            clsGlobals.ListCollections = new clsListElements();
            clsGlobals.ListGroups.SelectGroups();
            AddGroups();
        }

        private clsListElements DataGridToList(DataGridView dgvResjcm)
        {
            clsListElements lstElements = new clsListElements();
            foreach (DataGridViewRow myRow in dgvResjcm.Rows)
            {
                clsElement myEle = new clsElement();
                myEle.ElementID = Convert.ToString(myRow.Cells[0].Value);
                myEle.Code = Convert.ToString(myRow.Cells[1].Value);
                myEle.Name = Convert.ToString(myRow.Cells[2].Value);
                lstElements.Add(myEle);
            }
            return lstElements;
        }

        private void AddGroups()
        {
            if (clsFrmGlobals.frGW == null)
            {
                clsFrmGlobals.frGW = new frmGroupsToWork();
                clsFrmGlobals.frGW.MdiParent = this.MdiParent;
                clsFrmGlobals.frGW.FormClosed += new FormClosedEventHandler(AddGroupsClosed);
                clsFrmGlobals.frGW.Show();
            }
        }

        private void AddGroupsClosed(object sender, FormClosedEventArgs e)
        {
            clsFrmGlobals.frGW = null;
            dgvRes1.DataSource = null;
            clsGlobals.ListGroups.Elements = clsGlobals.ListTemp.Elements;
            dgvRes1.DataSource = clsGlobals.ListGroups.Elements;
            dgvRes1.AutoResizeColumns();
            dgvRes1.Columns[0].Visible = false;
            dgvRes1.Columns[3].Visible = false;
            FilterQuery();
            DisplayStats();
        }

        private void FilterQuery()
        {
            if (dgvRes1.Rows.Count > 0)
            {
                qtyGroups = dgvRes1.Rows.Count;
                if (dgvRes2.Rows.Count > 0)
                {
                    qtyStyles = dgvRes2.Rows.Count;
                    if (dgvRes3.Rows.Count > 0)
                    {
                        qtyColors = dgvRes3.Rows.Count;
                        if (dgvRes4.Rows.Count > 0)
                        {
                            qtyCollections = dgvRes4.Rows.Count;

                            qtySpecCollection = clsGlobals.ListCollections.GetNumberSpecCollection();
                            qtyCommon = clsGlobals.ListCollections.GetNumberCommon();
                            qtyIdentify = clsGlobals.ListCollections.GetNumberIdentify();
                            qtyVOSpecCollection = clsGlobals.ListCollections.GetNumberVOSpecCollection();
                            qtyVOCommon = clsGlobals.ListCollections.GetNumberVOCommon();
                            qtyVOIdentify = clsGlobals.ListCollections.GetNumberVOIdentify();
                        }
                        else
                        {
                            qtyCollections = clsGlobals.ListCollections.GetNumberCollections();
                            qtySpecCollection = clsGlobals.ListCollections.GetNumberSpecCollection();
                            qtyCommon = clsGlobals.ListCollections.GetNumberCommon();
                            qtyIdentify = clsGlobals.ListCollections.GetNumberIdentify();
                            qtyVOSpecCollection = clsGlobals.ListCollections.GetNumberVOSpecCollection();
                            qtyVOCommon = clsGlobals.ListCollections.GetNumberVOCommon();
                            qtyVOIdentify = clsGlobals.ListCollections.GetNumberVOIdentify();
                        }
                    }
                    else
                    {
                        qtyColors = clsGlobals.ListCollections.GetNumberColors();
                        qtyCollections = clsGlobals.ListCollections.GetNumberCollections();
                        qtySpecCollection = clsGlobals.ListCollections.GetNumberSpecCollection();
                        qtyCommon = clsGlobals.ListCollections.GetNumberCommon();
                        qtyIdentify = clsGlobals.ListCollections.GetNumberIdentify();
                        qtyVOSpecCollection = clsGlobals.ListCollections.GetNumberVOSpecCollection();
                        qtyVOCommon = clsGlobals.ListCollections.GetNumberVOCommon();
                        qtyVOIdentify = clsGlobals.ListCollections.GetNumberVOIdentify();
                    }
                }
                else
                {
                    qtyStyles = clsGlobals.ListGroups.GetNumberStyles();
                    qtyColors = clsGlobals.ListCollections.GetNumberColors();
                    qtyCollections = clsGlobals.ListCollections.GetNumberCollections();
                    qtySpecCollection = clsGlobals.ListCollections.GetNumberSpecCollection();
                    qtyCommon = clsGlobals.ListCollections.GetNumberCommon();
                    qtyIdentify = clsGlobals.ListCollections.GetNumberIdentify();
                    qtyVOSpecCollection = clsGlobals.ListCollections.GetNumberVOSpecCollection();
                    qtyVOCommon = clsGlobals.ListCollections.GetNumberVOCommon();
                    qtyVOIdentify = clsGlobals.ListCollections.GetNumberVOIdentify();
                }
            }
            else
            {
                NewSelection();
            }
        }

        private void btnRes2_Click(object sender, EventArgs e)
        {
            if (dgvRes2.Rows.Count > 0)
            {
                clsGlobals.lstBackUp = DataGridToList(dgvRes2);
            }
            else
            {
                clsGlobals.lstBackUp = new clsListElements();
            }
            dgvRes2.DataSource = null;
            clsGlobals.ListStyles = new clsListElements();
            dgvRes3.DataSource = null;
            clsGlobals.ListColors = new clsListElements();
            dgvRes4.DataSource = null;
            clsGlobals.ListCollections = new clsListElements();
            if (dgvRes1.Rows.Count > 0)
            {
                clsGlobals.ListStyles.SelectStyles();
                AddStyles();
            }
            else
            {
                MessageBox.Show("Un groupe doit être sélectionné.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void AddStyles()
        {
            if (clsFrmGlobals.frGW == null)
            {
                clsFrmGlobals.frGW = new frmGroupsToWork();
                clsFrmGlobals.frGW.MdiParent = this.MdiParent;
                clsFrmGlobals.frGW.FormClosed += new FormClosedEventHandler(AddStylesClosed);
                clsFrmGlobals.frGW.Show();
            }
        }

        private void AddStylesClosed(object sender, FormClosedEventArgs e)
        {
            clsFrmGlobals.frGW = null;
            dgvRes2.DataSource = null;
            clsGlobals.ListStyles.Elements = clsGlobals.ListTemp.Elements;
            dgvRes2.DataSource = clsGlobals.ListStyles.Elements;
            dgvRes2.AutoResizeColumns();
            dgvRes2.Columns[0].Visible = false;
            dgvRes2.Columns[3].Visible = false;
            FilterQuery();
            DisplayStats();
        }

        private void btnRes3_Click(object sender, EventArgs e)
        {
            if (dgvRes3.Rows.Count > 0)
            {
                clsGlobals.lstBackUp = DataGridToList(dgvRes3);
            }
            else
            {
                clsGlobals.lstBackUp = new clsListElements();
            }
            dgvRes3.DataSource = null;
            clsGlobals.ListColors = new clsListElements();
            dgvRes4.DataSource = null;
            clsGlobals.ListCollections = new clsListElements();
            if (dgvRes2.Rows.Count > 0)
            {
                clsGlobals.ListColors.SelectColors();
                AddColors();
            }
            else
            {
                MessageBox.Show("Un style doit être sélectionné.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void AddColors()
        {
            if (clsFrmGlobals.frGW == null)
            {
                clsFrmGlobals.frGW = new frmGroupsToWork();
                clsFrmGlobals.frGW.MdiParent = this.MdiParent;
                clsFrmGlobals.frGW.FormClosed += new FormClosedEventHandler(AddColorsClosed);
                clsFrmGlobals.frGW.Show();
            }
        }

        private void AddColorsClosed(object sender, FormClosedEventArgs e)
        {
            clsFrmGlobals.frGW = null;
            dgvRes3.DataSource = null;
            clsGlobals.ListColors.Elements = clsGlobals.ListTemp.Elements;
            dgvRes3.DataSource = clsGlobals.ListColors.Elements;
            dgvRes3.AutoResizeColumns();
            dgvRes3.Columns[0].Visible = false;
            dgvRes3.Columns[3].Visible = false;
            FilterQuery();
            DisplayStats();
        }

        private void btnRes4_Click(object sender, EventArgs e)
        {
            if (dgvRes4.Rows.Count > 0)
            {
                clsGlobals.lstBackUp = DataGridToList(dgvRes4);
            }
            else
            {
                clsGlobals.lstBackUp = new clsListElements();
            }
            dgvRes4.DataSource = null;
            clsGlobals.ListCollections = new clsListElements();
            if (dgvRes3.Rows.Count > 0)
            {
                clsGlobals.ListCollections.SelectCollections();
                AddCollections();
            }
            else
            {
                MessageBox.Show("Une couleur doit être sélectionnée.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void AddCollections()
        {
            if (clsFrmGlobals.frGW == null)
            {
                clsFrmGlobals.frGW = new frmGroupsToWork();
                clsFrmGlobals.frGW.MdiParent = this.MdiParent;
                clsFrmGlobals.frGW.FormClosed += new FormClosedEventHandler(AddCollectionsClosed);
                clsFrmGlobals.frGW.Show();
            }
        }

        private void AddCollectionsClosed(object sender, FormClosedEventArgs e)
        {
            clsFrmGlobals.frGW = null;
            dgvRes4.DataSource = null;
            clsGlobals.ListCollections.Elements = clsGlobals.ListTemp.Elements;
            dgvRes4.DataSource = clsGlobals.ListCollections.Elements;
            dgvRes4.AutoResizeColumns();
            dgvRes4.Columns[0].Visible = false;
            dgvRes4.Columns[3].Visible = false;
            FilterQuery();
            DisplayStats();
        }

        private void btnCommand1_Click(object sender, EventArgs e)
        {
            clsGlobals.ListCollections.GetListCollections();
            clsGlobals.ActiveRatio = mySce.SurplusRateUnique;
            clsGlobals.BkRatio = mySce.SurplusRateUnique;
            if (clsFrmGlobals.frSS == null)
            {
                clsFrmGlobals.frSS = new frmProductsSelectedScenario();
                clsFrmGlobals.frSS.MdiParent = this.MdiParent;
                clsFrmGlobals.frSS.FormClosed += new FormClosedEventHandler(frSSFormClosed);
                clsFrmGlobals.frSS.Show();
            }
        }

        private void frSSFormClosed(object sender, FormClosedEventArgs e)
        {
            clsFrmGlobals.frSS = null;
            FilterQuery();
            DisplayStats();
        }

        private void btnCommand2_Click(object sender, EventArgs e)
        {
            clsGlobals.ListCollections.GetListCommons();
            clsGlobals.ActiveRatio = mySce.SurplusRateCommon;
            clsGlobals.BkRatio = mySce.SurplusRateCommon;
            if (clsFrmGlobals.frSS == null)
            {
                clsFrmGlobals.frSS = new frmProductsSelectedScenario();
                clsFrmGlobals.frSS.MdiParent = this.MdiParent;
                clsFrmGlobals.frSS.FormClosed += new FormClosedEventHandler(frSSFormClosed);
                clsFrmGlobals.frSS.Show();
            }
        }

        private void btnCommand3_Click(object sender, EventArgs e)
        {
            clsGlobals.ListCollections.GetListUnidentified();
            clsGlobals.ActiveRatio = mySce.SurplusRateOS;
            clsGlobals.BkRatio = mySce.SurplusRateOS;
            clsGlobals.AvaibleFlag = true;
            if (clsFrmGlobals.frSS == null)
            {
                clsFrmGlobals.frSS = new frmProductsSelectedScenario();
                clsFrmGlobals.frSS.MdiParent = this.MdiParent;
                clsFrmGlobals.frSS.FormClosed += new FormClosedEventHandler(frSSFormClosed);
                clsFrmGlobals.frSS.Show();
            }
        }

        private void cboDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboDivision.SelectedIndex >= 0)
            {
                lstCollections.GetCollections(Convert.ToInt32(cboDivision.SelectedValue));
                cboCollection.DisplayMember = "Full";
                cboCollection.ValueMember = "ElementID";
                cboCollection.DataSource = lstCollections.Elements;
            }
        }

        private void cboBillFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboBillFrom.SelectedIndex > 0)
            {
                eleShip.GetShipFrom(Convert.ToInt32(cboBillFrom.SelectedValue));
                txtShipFrom.Text = eleShip.Full;
            }
        }
    }
}

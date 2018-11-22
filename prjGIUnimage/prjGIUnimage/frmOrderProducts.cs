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
    public partial class frmOrderProducts : Form
    {
        clsElement eleShip = new clsElement();
        clsListElements lstBillFrom = new clsListElements();
        clsListElements lstPurchaseType = new clsListElements();
        clsListElements lstDivisions = new clsListElements();
        clsListElements lstWhs = new clsListElements();
        clsListSeason lstSeasons = new clsListSeason();
        clsListElements lstCollections = new clsListElements(); 
        clsListScSalesHistory lstSCSales = new clsListScSalesHistory();
        clsListSeason lstSea = new clsListSeason();
        clsListData lstStatus = new clsListData();
        clsScProduct myScP = new clsScProduct();
        clsListProductOrdered lstProOrd = new clsListProductOrdered();
        clsListProductOrdered lstTotals = new clsListProductOrdered();
        clsScenario mySce = new clsScenario();
        
        public frmOrderProducts()
        {
            InitializeComponent();
            clsSilex.UpDateProductAvailableOS();
            chkNotGenerateVO.Checked = false;
        }

        private void frmOrderProducts_Load(object sender, EventArgs e)
        {
            //Obtain information about the scenario by ID
            mySce.GetScenarioByID(clsGlobals.GIPar.ScenarioID);
            clsGlobals.GIPar.SeasonID = mySce.GISeasonID;

            //Obtain size information by product and scenario
            lstSCSales.GetSalesHistoryByID(clsGlobals.GIPar.ScenarioID, clsGlobals.GIPar.ProductColorID);

            //Get Product Information SCProduct
            myScP.GetScProduct(clsGlobals.GIPar.ScenarioID, clsGlobals.GIPar.ProductColorID);
            clsGlobals.ActiveRatio = clsGlobals.BkRatio;
            
            //Rate update
            if (myScP.SurplusRate == 0)
            {
                myScP.SurplusRate = clsGlobals.ActiveRatio;
            }
            else
            {
                clsGlobals.ActiveRatio = myScP.SurplusRate;
            }
            try
            {
                //Filling combobox
                LoadComboBoxes();

                //Generates and calculates product information from 4017
                GenerateOrderedProductsList();

                //Copy general parameters to text
                GIParametersToText();

                //Desactiva controles
                DeactivateControls();

                //Configura datagridview
                ConfigureDataView();

                if (myScP.VOStatus == 2)
                {
                    chkNotGenerateVO.Checked = true;
                }

                //if (clsGlobals.AvaibleFlag && clsGlobals.CollectionsFlag)
                //{
                //    foreach (clsProductOrdered myPro in lstProOrd.Elements)
                //    {
                //        myPro.Available = clsProductAvailableOS.GetAvailableQuantity(clsGlobals.GIPar.ScenarioID, clsGlobals.ParentProductID, myPro.DimID, myPro.SizeOrder);
                //    }
                //    ConfigureResults();
                //    ConfigureTotals();
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadComboBoxes()
        {
            lstBillFrom.GetBillFrom();
            cboBillFrom.DisplayMember = "Name";
            cboBillFrom.ValueMember = "ElementID";
            cboBillFrom.DataSource = lstBillFrom.Elements;

            lstPurchaseType.GetPurchaseType();
            cboPurchaseType.DisplayMember = "Name";
            cboPurchaseType.ValueMember = "ElementID";
            cboPurchaseType.DataSource = lstPurchaseType.Elements;

            lstWhs.GetWhs(clsGlobals.GIPar.ProductColorID);
            cboWhs.DisplayMember = "Full";
            cboWhs.ValueMember = "ElementID";
            cboWhs.DataSource = lstWhs.Elements;

            //lstDivisions.GetDivisions();
            lstDivisions.GetDivisions(clsGlobals.GIPar.ProductColorID);
            cboDivision.DisplayMember = "Code";
            cboDivision.ValueMember = "ElementID";
            cboDivision.DataSource = lstDivisions.Elements;

            //Seleccionar primer elemento
            if (lstDivisions.Quantity > 0)
            {
                cboDivision.SelectedIndex = 0;
            }

            lstSeasons.GetAllSeasons();
            cboSeason.DisplayMember = "SeasonName";
            cboSeason.ValueMember = "GISeasonID";
            cboSeason.DataSource = lstSeasons.Elements;

            lstSea.GetAllSeasons();
            cboCurrentSaison.DisplayMember = "SeasonName";
            cboCurrentSaison.ValueMember = "GISeasonID";
            cboCurrentSaison.DataSource = lstSea.Elements;

            lstStatus.DataByGroup(102);
            cboStatus.DisplayMember = "DataDesc_fra";
            cboStatus.ValueMember = "DataValue";
            cboStatus.DataSource = lstStatus.Elements;
        }

        private void GenerateOrderedProductsList()
        {
            if (mySce.ExistsBeforeFiscal())
            {
                if (mySce.ExistsAfterFiscal())
                {
                    GenerateListOrderedProducts(1);
                }
                else
                {
                    MessageBox.Show("Aucune donnée n'a été trouvée après fiscal");
                    GenerateListOrderedProducts(2);
                }
            }
            else
            {
                if (mySce.ExistsAfterFiscal())
                {
                    MessageBox.Show("Aucune donnée trouvée avant fiscal");
                    GenerateListOrderedProducts(3);
                }
                else
                {
                    MessageBox.Show("Aucune donnée d'inventaire fiscal trouvée");
                    GenerateListOrderedProducts(4);
                }
            }
        }

        private void GenerateListOrderedProducts(int v)
        {
            //lstProOrd.GetParametersVO(myPar);
            //lstProOrd.GetParametersID(lstSCSales.Elements[0]);
            lstProOrd = new clsListProductOrdered();
            int cont = 0;

            foreach (clsScSalesHistory ele in lstSCSales.Elements)
            {
                clsProductOrdered myEle = new clsProductOrdered();
                if (myScP.PurchaseTypeID == 104)
                {
                    myEle.CalculateStatisticsRepeat(ele, v);
                }
                else
                {
                    myEle.CalculateStatisticsBooking(ele, v);
                }

                clsProductAvailableOS myPAva = new clsProductAvailableOS(ele, myEle.Available);
                if (!clsSilex.Exists(myPAva) && clsGlobals.AvaibleFlag)
                {
                    myPAva.InsertProductAvailableOS();
                }
                lstProOrd.Add(myEle);
                cont++;
            }
        }

        private void GIParametersToText()
        {
            if (myScP.HasBeenModified())
            {
                //Vendor Order
                txtRef1.Text = myScP.ReferenceNo1;
                txtRef2.Text = myScP.ReferenceNo2;
                txtVOMessage.Text = myScP.VOMessage;
                txtVONote.Text = myScP.VONote;
                cboBillFrom.SelectedValue = myScP.VendorID.ToString();
                //cboDivision.SelectedValue = mySce.DivisionID.ToString();
                //cboCollection.SelectedValue = mySce.CollectionID.ToString();
                cboPurchaseType.SelectedValue = myScP.PurchaseTypeID.ToString();
                cboSeason.SelectedValue = myScP.VOSeasonID;
                cboWhs.SelectedValue = myScP.DefaultWarehouseID.ToString();
                dtpArrivalDate.Value = myScP.ExpArrivalDate;
                dtpShipDate.Value = myScP.ExpShippingDate;

                //Informations du scénario
                cboCurrentSaison.SelectedValue = mySce.GISeasonID;
                txtCode.Text = mySce.ScenarioCode;
                cboStatus.SelectedValue = mySce.ScenarioStatus;
                txtDesc.Text = mySce.ScenarioDesc;
                txtCreatedD.Text = Convert.ToString(mySce.CreatedDate);
                txtCreatedU.Text = clsUser.GetUserName(Convert.ToInt32(mySce.CreatedByUserID));
                txtModifiedD.Text = Convert.ToString(mySce.ModifiedDate);
                txtModifiedU.Text = clsUser.GetUserName(Convert.ToInt32(mySce.ModifiedByUserID));

                //Informations sur le produit
                txtOrderNumber.Text = myScP.VOCode;
                txtProductCode.Text = myScP.GetProductCode();
                txtProductDescription.Text = myScP.GetProductDesc();
                txtColorCode.Text = myScP.GetColorCode();
                txtColorName.Text = myScP.GetColorName();
                txtSxStatus.Text = myScP.SXGetStatus();
                txtGIStatus.Text = myScP.GetGIProducStatus();
                txtSurplus.Text = myScP.SurplusRate.ToString();
                txtProductComment.Text = myScP.ProductComment;

                if (myScP.VoPdfStatus == 1)
                {
                    chkPrinted.Checked = true;
                    lblPDFPrintingDate.Text = "le " + myScP.DeletedDate.ToLongDateString() + " à " + myScP.DeletedDate.ToShortTimeString();
                }
                else
                {
                    chkPrinted.Checked = false;
                    lblPDFPrintingDate.Visible = false;
                }
            }
            else
            {
                //Vendor Order
                txtRef1.Text = mySce.ReferenceNo1;
                txtRef2.Text = mySce.ReferenceNo2;
                txtVOMessage.Text = mySce.VOMessage;
                txtVONote.Text = mySce.VONote;
                cboBillFrom.SelectedValue = mySce.VendorID.ToString();
                //cboDivision.SelectedValue = mySce.DivisionID.ToString();
                //cboCollection.SelectedValue = mySce.CollectionID.ToString();
                cboPurchaseType.SelectedValue = mySce.PurchaseTypeID.ToString();
                cboSeason.SelectedValue = mySce.GISeasonID;
                cboWhs.SelectedValue = mySce.DefaultWarehouseID.ToString();
                if (cboWhs.SelectedValue == null)
                {
                    cboWhs.SelectedIndex = 0;
                }
                dtpArrivalDate.Value = mySce.ExpArrivalDate;
                dtpShipDate.Value = mySce.ExpShippingDate;

                //Informations du scénario
                cboCurrentSaison.SelectedValue = mySce.GISeasonID;
                txtCode.Text = mySce.ScenarioCode;
                cboStatus.SelectedValue = mySce.ScenarioStatus;
                txtDesc.Text = mySce.ScenarioDesc;
                txtCreatedD.Text = Convert.ToString(mySce.CreatedDate);
                txtCreatedU.Text = clsUser.GetUserName(Convert.ToInt32(mySce.CreatedByUserID));
                txtModifiedD.Text = Convert.ToString(mySce.ModifiedDate);
                txtModifiedU.Text = clsUser.GetUserName(Convert.ToInt32(mySce.ModifiedByUserID));

                //Informations sur le produit
                txtOrderNumber.Text = myScP.VOCode;
                txtProductCode.Text = myScP.GetProductCode();
                txtProductDescription.Text = myScP.GetProductDesc();
                txtColorCode.Text = myScP.GetColorCode();
                txtColorName.Text = myScP.GetColorName();
                txtSxStatus.Text = myScP.SXGetStatus();
                txtGIStatus.Text = myScP.GetGIProducStatus();
                txtSurplus.Text = myScP.SurplusRate.ToString();
                txtProductComment.Text = myScP.ProductComment;

                if (myScP.VoPdfStatus == 1)
                {
                    chkPrinted.Checked = true;
                    lblPDFPrintingDate.Text = "le " + myScP.DeletedDate.ToLongDateString() + " à " + myScP.DeletedDate.ToShortTimeString();
                }
                else
                {
                    chkPrinted.Checked = false;
                    lblPDFPrintingDate.Visible = false;
                }
            }
        }

        private void DeactivateControls()
        {
            txtOrderNumber.ReadOnly = true;
            txtRef1.ReadOnly = true;
            txtRef2.ReadOnly = true;
            txtShipFrom.ReadOnly = true;
            txtVONote.ReadOnly = true;
            txtVOMessage.ReadOnly = true;
            txtProductCode.ReadOnly = true;
            txtProductDescription.ReadOnly = true;
            txtColorCode.ReadOnly = true;
            txtColorName.ReadOnly = true;
            txtSxStatus.ReadOnly = true;
            txtGIStatus.ReadOnly = true;
            txtSurplus.ReadOnly = true;
            txtProductComment.ReadOnly = true;
            dtpArrivalDate.Enabled = false;
            dtpShipDate.Enabled = false;
            cboBillFrom.Enabled = false;
            cboCollection.Enabled = false;
            cboDivision.Enabled = false;
            cboPurchaseType.Enabled = false;
            cboSeason.Enabled = false;
            cboWhs.Enabled = false;
            btnSave.Enabled = false;
            btnModify.Enabled = true;
            chkPrinted.AutoCheck = false;

            //Scenario
            cboCurrentSaison.Enabled = false;
            cboStatus.Enabled = false;
            txtPreviousSeason.ReadOnly = true;
            txtCode.ReadOnly = true;
            txtDesc.ReadOnly = true;
            txtCreatedD.ReadOnly = true;
            txtCreatedU.ReadOnly = true;
            txtModifiedD.ReadOnly = true;
            txtModifiedU.ReadOnly = true;

            btnHelp.Enabled = true;
            btnPreview.Enabled = true;
            btnCollection.Enabled = true;
        }

        private void ConfigureDataView()
        {
            ConfigureResults();
            ConfigureTotals();
        }

        private void ConfigureResults()
        {
            dgvResult.DataSource = null;
            dgvResult.DataSource = lstProOrd.Elements;


            dgvResult.Columns["ProductID"].Visible = false;
            dgvResult.Columns["ProductColorID"].Visible = false;

            dgvResult.Columns["ProductDimID"].Visible = false;
            dgvResult.Columns["ProductCatID"].Visible = false;
            dgvResult.Columns["SizeOrder"].Visible = false;
            dgvResult.Columns["ColorID"].Visible = false;
            dgvResult.Columns["DimID"].Visible = false;
            dgvResult.Columns["CatID"].Visible = false;
            dgvResult.Columns["ProductGroupID"].Visible = false;
            dgvResult.Columns["ProductSubGroupID"].Visible = false;
            dgvResult.Columns["Available"].Visible = false;
            //if (clsGlobals.AvaibleFlag && clsGlobals.CollectionsFlag)
            //{
            //    dgvResult.Columns["Available"].Visible = true;
            //}
            //else
            //{
            //    dgvResult.Columns["Available"].Visible = false;
            //}
            SetDataGridViewResult();
            dgvResult.AutoResizeColumns();
        }

        private void SetDataGridViewResult()
        {
            // Set the column header style.
            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();

            columnHeaderStyle.BackColor = Color.Beige;
            columnHeaderStyle.Font = new Font("Verdana", 10, FontStyle.Bold);
            dgvResult.ColumnHeadersDefaultCellStyle = columnHeaderStyle;
        }

        private void ConfigureTotals()
        {
            clsProductOrdered nPord = new clsProductOrdered();
            //Totales
            dgvTotals.DataSource = null;
            dgvTotals.DataSource = lstTotals.Elements;
            dgvTotals.Columns["ProductID"].Visible = false;
            dgvTotals.Columns["ProductColorID"].Visible = false;

            dgvTotals.Columns["ProductDimID"].Visible = false;
            dgvTotals.Columns["ProductCatID"].Visible = false;
            dgvTotals.Columns["SizeOrder"].Visible = false;
            dgvTotals.Columns["ColorID"].Visible = false;
            dgvTotals.Columns["DimID"].Visible = false;
            dgvTotals.Columns["CatID"].Visible = false;
            dgvTotals.Columns["ProductGroupID"].Visible = false;
            dgvTotals.Columns["ProductSubGroupID"].Visible = false;
            dgvTotals.Columns["Available"].Visible = false;
            //if (clsGlobals.AvaibleFlag && clsGlobals.CollectionsFlag)
            //{
            //    dgvTotals.Columns["Available"].Visible = true;
            //    nPord.Available = SumOfColumnsAva("Available");
            //}
            //else
            //{
            //    dgvTotals.Columns["Available"].Visible = false;
            //}

            nPord.ACH = SumOfColumns("ACH");
            nPord.Besoin = SumOfColumns("Besoin");
            nPord.CMD = SumOfColumns("CMD");
            nPord.CSS = SumOfColumns("CSS");
            nPord.IBF = SumOfColumns("IBF");
            nPord.IC = SumOfColumns("IC");
            nPord.IAF = SumOfColumns("IAF");
            nPord.QVO = SumOfColumns("QVO");
            nPord.VTEM = SumOfColumns("VTEM");
            nPord.RP1 = SumOfColumns("RP1");
            nPord.RP2 = SumOfColumns("RP2");
            //nPord.SizeOrder = SumOfColumns("SizeOrder");
            nPord.SizeOrder = 0;
            nPord.VP1 = SumOfColumns("VP1");
            nPord.VP2 = SumOfColumns("VP2");
            nPord.VTOT = SumOfColumns("VTOT");
            nPord.Var = SumOfColumns("Var");
            
            //nPord.Dim = "TOTAUX PAR ";
            //nPord.Size = "COLONNES";
            lblInfo.Text = SumOfColumnsBesoin("Besoin");
            if (lstTotals.Quantity > 0)
            {
                lstTotals = new clsListProductOrdered();
            }
            lstTotals.Insert(0, nPord);
            dgvTotals.DataSource = lstTotals.Elements;
            SetDataGridViewTotals();
            dgvTotals.AutoResizeColumns();
        }

        private void SetDataGridViewTotals()
        {
            // Set the column header style.
            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();

            columnHeaderStyle.BackColor = Color.Beige;
            columnHeaderStyle.Font = new Font("Verdana", 10, FontStyle.Bold);
            dgvTotals.ColumnHeadersDefaultCellStyle = columnHeaderStyle;
        }

        private string SumOfColumnsBesoin(string v)
        {
            int sumPos = 0;
            int sumNeg = 0;
            foreach (DataGridViewRow row in dgvResult.Rows)
            {
                int val = Convert.ToInt32(row.Cells[v].Value);
                if (val > 0)
                {
                    sumPos += val;
                }
                else
                {
                    sumNeg += val;
                }
            }
            return "Montants des besoins: " + sumNeg + " / +" + sumPos;
        }

        private int SumOfColumns(string v)
        {
            int sum = 0;
            foreach (DataGridViewRow row in dgvResult.Rows)
            {
                int val = Convert.ToInt32(row.Cells[v].Value);
                if (val > 0)
                {
                    sum += val;
                }
            }
            return sum;
        }

        //private int SumOfColumnsAva(string v)
        //{
        //    int sum = 0;
        //    foreach (DataGridViewRow row in dgvResult.Rows)
        //    {
        //        int val = Convert.ToInt32(row.Cells[v].Value);
        //        sum += val;
        //    }
        //    return sum;
        //}

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

        private void button1_Click(object sender, EventArgs e)
        {
            ActivateControls();
        }

        private void ActivateControls()
        {
            //txtOrderNumber.ReadOnly = true;
            txtRef1.ReadOnly = false;
            txtRef2.ReadOnly = false;
            //txtShipFrom.ReadOnly = true;
            txtVONote.ReadOnly = false;
            txtVOMessage.ReadOnly = false;
            //txtProductCode.ReadOnly = true;
            //txtProductDescription.ReadOnly = true;
            //txtColorCode.ReadOnly = true;
            //txtColorName.ReadOnly = true;
            //txtSxStatus.ReadOnly = true;
            //txtGIStatus.ReadOnly = true;
            txtSurplus.ReadOnly = false;
            txtProductComment.ReadOnly = false;
            dtpArrivalDate.Enabled = true;
            dtpShipDate.Enabled = true;
            cboBillFrom.Enabled = true;
            cboCollection.Enabled = true;
            cboDivision.Enabled = true;
            cboPurchaseType.Enabled = true;
            cboSeason.Enabled = true;
            cboWhs.Enabled = true;
            btnSave.Enabled = true;
            btnModify.Enabled = false;
            chkPrinted.AutoCheck = true;
            btnHelp.Enabled = false;
            btnPreview.Enabled = false;
            btnCollection.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Validate input variables
            if (ValidateChildren(ValidationConstraints.Enabled))
            {
                //Evaluate percentage greater than 100
                if (Convert.ToInt32(txtSurplus.Text) > 100)
                {
                    if (MessageBox.Show("Vous êtes sûr d'utiliser un surplus supérieur à 100%", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
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

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void RegisterChanges()
        {
            DeactivateControls();

            //cAPTURAR DATOS DE CONTROLES
            TextToScProduct();
            //ACTUALIZAR INFLORMACION EN TBLSCPRODUCT
            myScP.UpdateScProduct();

            //Actualiza las modificaciones en el Scenario
            clsScenario.UpdateModifiedByUser(clsGlobals.GIPar.ScenarioID);
            //GenerateOrderedProductsList();
            DeactivateControls();

            ConfigureDataView();

            MessageBox.Show("Les données ont été enregistrées correctement.");
        }

        private void TextToScProduct()
        {
            myScP.ScenarioID = clsGlobals.GIPar.ScenarioID;
            myScP.ProductColorID = clsGlobals.GIPar.ProductColorID;
            myScP.ReferenceNo1 = txtRef1.Text;
            myScP.ReferenceNo2 = txtRef2.Text;
            myScP.ExpArrivalDate = dtpArrivalDate.Value;
            myScP.ExpShippingDate = dtpShipDate.Value;
            myScP.VendorID = Convert.ToInt32(cboBillFrom.SelectedValue);
            myScP.VendorSiteID = eleShip.GetVendorSiteID(myScP.VendorID);
            myScP.PurchaseTypeID = Convert.ToInt32(cboPurchaseType.SelectedValue);
            myScP.DefaultWarehouseID = Convert.ToInt32(cboWhs.SelectedValue);
            myScP.DivisionID = Convert.ToInt32(cboDivision.SelectedValue);
            myScP.CollectionID = Convert.ToInt32(cboCollection.SelectedValue);
            myScP.VOSeasonID = Convert.ToInt32(cboSeason.SelectedValue);
            myScP.VONote = txtVONote.Text.Trim();
            myScP.VOMessage = txtVOMessage.Text.Trim();
            myScP.SurplusRate = Convert.ToDouble(txtSurplus.Text);
            myScP.ProductComment = txtProductComment.Text;
            clsGlobals.ActiveRatio = myScP.SurplusRate;
        }

        private void dgvResult_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            dgv.Columns["DIM"].ReadOnly = true;
            dgv.Columns["SIZE"].ReadOnly = true;
            dgv.Columns["RP1"].ReadOnly = true;
            dgv.Columns["VP1"].ReadOnly = true;
            dgv.Columns["RP2"].ReadOnly = true;
            dgv.Columns["VP2"].ReadOnly = true;
            dgv.Columns["ACH"].ReadOnly = true;
            dgv.Columns["CMD"].ReadOnly = true;
            dgv.Columns["VTOT"].ReadOnly = true;
            dgv.Columns["CSS"].ReadOnly = true;
            dgv.Columns["Var"].ReadOnly = true;
            dgv.Columns["VTEM"].ReadOnly = true;
            dgv.Columns["IBF"].ReadOnly = true;
            dgv.Columns["IAF"].ReadOnly = true;
            dgv.Columns["IC"].ReadOnly = true;
            dgv.Columns["Besoin"].ReadOnly = true;
            dgv.Columns["Available"].ReadOnly = true;
            //dgv.Columns["Qty"].DefaultCellStyle.BackColor = Color.LightYellow;

            dgv.Columns["RP1"].DefaultCellStyle.BackColor = Color.LightBlue;
            dgv.Columns["VP1"].DefaultCellStyle.BackColor = Color.LightBlue;
            dgv.Columns["RP2"].DefaultCellStyle.BackColor = Color.LightGreen;
            dgv.Columns["VP2"].DefaultCellStyle.BackColor = Color.LightGreen;
            dgv.Columns["ACH"].DefaultCellStyle.BackColor = Color.Orange;
            dgv.Columns["CMD"].DefaultCellStyle.BackColor = Color.LightCoral;
            dgv.Columns["VTOT"].DefaultCellStyle.BackColor = Color.LightCyan;
            dgv.Columns["CSS"].DefaultCellStyle.BackColor = Color.LightGoldenrodYellow;
            dgv.Columns["Var"].DefaultCellStyle.BackColor = Color.LightGray;
            dgv.Columns["VTEM"].DefaultCellStyle.BackColor = Color.LightPink;
            dgv.Columns["IBF"].DefaultCellStyle.BackColor = Color.LightSalmon;
            dgv.Columns["IAF"].DefaultCellStyle.BackColor = Color.LightSeaGreen;
            dgv.Columns["IC"].DefaultCellStyle.BackColor = Color.LightSkyBlue;
            dgv.Columns["Available"].DefaultCellStyle.BackColor = Color.LightSlateGray;
            //dgv.Columns["Besoin"].DefaultCellStyle.BackColor = Color.LightSteelBlue;
            //dgv.Columns["Qty"].DefaultCellStyle.BackColor = Color.LightYellow;

            //Texto en negrita - Bold Text
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            style.Font = new Font("Verdana", 10, FontStyle.Bold);
            dgvTotals.DefaultCellStyle = style;
            //dgvResult.Rows[0].DefaultCellStyle = style;

            if (dgv.Columns[e.ColumnIndex].Name == "Besoin")  
            {
                if (Convert.ToInt32(e.Value) > 0)   
                {
                    e.CellStyle.BackColor = Color.Red;
                }
            }
        }

        private void cboCurrentSaison_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtPreviousSeason.Text = lstSea.PreviousSeason(Convert.ToInt32(cboCurrentSaison.SelectedValue));
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            if (myScP.VorderExists())
            {
                MessageBox.Show("Le VO a déjà été générée");
            }
            else
            {
                //Leer producto
                TextToScProduct();

                //Crear encabezado VO
                clsScVorder myVO = new clsScVorder();

                myVO.CollectionID = myScP.CollectionID;
                myVO.DefaultWarehouseID = myScP.DefaultWarehouseID;
                myVO.DivisionID = myScP.DivisionID;
                myVO.ExpArrivalDate = myScP.ExpArrivalDate;
                myVO.ExpShippingDate = myScP.ExpShippingDate;
                myVO.OrderTotalQty = 0;
                myVO.PDFPrinted = 0;
                myVO.PurchaseTypeID = myScP.PurchaseTypeID;
                myVO.ReferenceNo1 = myScP.ReferenceNo1;
                myVO.ReferenceNo2 = myScP.ReferenceNo2;
                myVO.SeasonID = myScP.VOSeasonID;
                myVO.VendorID = myScP.VendorID;
                myVO.VendorSiteID = myScP.VendorSiteID;
                myVO.VOMessage = myScP.VOMessage;
                myVO.VONote = myScP.VONote;

                clsListScVorderDetail lstVoDet = new clsListScVorderDetail();
                //bool flag = true;

                foreach (DataGridViewRow dgRow in dgvResult.Rows)
                {
                    clsScVorderDetail myVODetails = new clsScVorderDetail();

                    myVODetails.CatID = Convert.ToInt32(dgRow.Cells["CatID"].Value);
                    myVODetails.ColorID = Convert.ToInt32(dgRow.Cells["ColorID"].Value);
                    myVODetails.DimID = Convert.ToInt32(dgRow.Cells["DimID"].Value);
                    //myVODetails.GIVODetailID = Convert.ToInt32(dgRow.Cells["GIVODetailID"].Value);
                    //myVODetails.GIVOID = Convert.ToInt32(dgRow.Cells["GIVOID"].Value);
                    myVODetails.OrderQty = Convert.ToInt32(dgRow.Cells["QVO"].Value);
                    myVO.OrderTotalQty += myVODetails.OrderQty;
                    myVODetails.ProductCatID = Convert.ToInt32(dgRow.Cells["ProductCatID"].Value);
                    myVODetails.ProductColorID = Convert.ToInt32(dgRow.Cells["ProductColorID"].Value);
                    myVODetails.ProductDimID = Convert.ToInt32(dgRow.Cells["ProductDimID"].Value);
                    myVODetails.ProductGroupID = Convert.ToInt32(dgRow.Cells["ProductGroupID"].Value);
                    myVODetails.ProductID = Convert.ToInt32(dgRow.Cells["ProductID"].Value);
                    myVODetails.ProductSubGroupID = Convert.ToInt32(dgRow.Cells["ProductSubGroupID"].Value);
                    myVODetails.ProductWarehouseID = clsScVorderDetail.GetProductWarehouseID(myVODetails.ProductID, myVO.DefaultWarehouseID);
                    //myVODetails.SeasonID = Convert.ToInt32(dgRow.Cells["SeasonID"].Value);
                    myVODetails.SizeOrder = Convert.ToInt32(dgRow.Cells["SizeOrder"].Value);
                    myVODetails.Dim = Convert.ToString(dgRow.Cells["Dim"].Value);
                    myVODetails.Size = Convert.ToString(dgRow.Cells["Size"].Value);
                    myVODetails.VODetailDesc = myScP.GetProductDesc();
                    //myVODetails.VODetailNote = Convert.ToString(dgRow.Cells["VODetailNote"].Value);
                    //myVODetails.VODetailStatus = Convert.ToInt32(dgRow.Cells["VODetailStatus"].Value);

                    lstVoDet.AddScVorderDetail(myVODetails);
                }

                clsGlobals.Vorder = myVO;
                clsGlobals.VorderDetail = lstVoDet;

                if (clsFrmGlobals.frPV == null)
                {
                    clsFrmGlobals.frPV = new frmPreviewVO();
                    clsFrmGlobals.frPV.MdiParent = this.MdiParent;
                    clsFrmGlobals.frPV.FormClosed += new FormClosedEventHandler(frPVFromClosed);
                    clsFrmGlobals.frPV.Show();
                    //this.Close();
                }
            }
        }

        private void frPVFromClosed(object sender, FormClosedEventArgs e)
        {
            clsFrmGlobals.frPV = null;
            txtOrderNumber.Text = clsGlobals.Vorder.VOCode;
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

        private void button1_Click_1(object sender, EventArgs e)
        {
            string info = 
                "		P1		Saison précédente\n" +
                "		P2		Saison courante\n" +
                "				\n" +
                "DimCde		Dim		Famille de tailles\n" +
                "SizeDesc		Size		Code de taille\n" +
                "RecP1		RP1		Quantité reçues Période 1\n" +
                "VteP1		VP1		Ventes Période 1 : Facturées et à facturer\n" +
                "RecP2		RP2		Quantité reçues Période 2\n" +
                "VteP2		VP2		Ventes Période 2 : Facturées et à facturer\n" +
                "Achats		ACH		Achats en cours P1 + P2\n" +
                "Cmds		CMD		CMD en cours = CMD P1 + CMD P2\n" +
                "VtePlusCmd	VTOT		Qtés facturées avant pivot 2 + Vtes non-postées + CMD en cours\n" +
                "CmdSS		CSS		Commande client sans sac\n" +
                "VteVs		Var		Variation VTE = VP2 - VP1\n" +
                "QtyMoy		VTEM		Quantité moyenne vendue : (VP1+VP2)/2\n" +
                "InvBefFis		IBF		Inventaire Before Fiscal\n" +
                "InvFis		IAF		Inventaire After Fiscal\n" +
                "InvCourant	IC		Inventaire courant\n" +
                "Besoin		Besoin		\"Besoin = ( CMD + VTE non-postées + Facturées avant P2) * RATIO - (facturées avant P2 +VTE à facturer +Achats en cours +Inv courant)\"\n" +
                "Qty 		QVO		Quantité à commander\n";
            MessageBox.Show(info, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void chkPrinted_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPrinted.Checked)
            {
                myScP.VoPdfStatus = 1;
            }
            else
            {
                myScP.VoPdfStatus = 0;
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (!clsElement.CompCollectionExists(myScP.ProductColorID))
                {
                    MessageBox.Show("Il n'y a pas de collections avec ce produit");
                }
                else
                {
                    clsGlobals.ListCollections.GetListCompCollection(myScP.ProductColorID);
                    clsGlobals.ActiveRatio = mySce.SurplusRateIdentified;
                    clsGlobals.BkRatio = mySce.SurplusRateIdentified;
                    if (clsFrmGlobals.frSC == null)
                    {
                        clsFrmGlobals.frSC = new frmProductsSelectedCollection();
                        clsFrmGlobals.frSC.MdiParent = this.MdiParent;
                        clsFrmGlobals.frSC.FormClosed += new FormClosedEventHandler(frSCFormClosed);
                        clsFrmGlobals.frSC.Show();
                    }
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void frSCFormClosed(object sender, FormClosedEventArgs e)
        {
            clsFrmGlobals.frSC = null;
            clsGlobals.CollectionsFlag = false;
        }

        private void dgvResult_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            ConfigureTotals();
        }

        private void chkNotGenerateVO_CheckedChanged(object sender, EventArgs e)
        {
            if (myScP.VOStatus == 1)
            {
                if (chkNotGenerateVO.Checked)
                {
                    MessageBox.Show("Le VO pour ce produit a déjà été généré.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    chkNotGenerateVO.Checked = false;
                }

            }
            else
            {
                if (chkNotGenerateVO.Checked)
                {
                    if (myScP.VOStatus == 2)
                    {
                        btnCollection.Enabled = false;
                        btnModify.Enabled = false;
                        btnPreview.Enabled = false;
                        btnSave.Enabled = false;
                        btnCollection.Enabled = false;
                    }
                    else
                    {
                        if (MessageBox.Show("Êtes-vous sûr de ne pas générer de VO pour ce produit?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            btnCollection.Enabled = false;
                            btnModify.Enabled = false;
                            btnPreview.Enabled = false;
                            btnSave.Enabled = false;
                            btnCollection.Enabled = false;
                            myScP.UpdateVOStatus(2);
                        }
                        else
                        {
                            chkNotGenerateVO.Checked = false;
                            btnCollection.Enabled = true;
                            btnModify.Enabled = true;
                            btnPreview.Enabled = true;
                            btnSave.Enabled = true;
                            btnCollection.Enabled = true;
                        }
                    }
                }
                else
                {
                    btnCollection.Enabled = true;
                    btnModify.Enabled = true;
                    btnPreview.Enabled = true;
                    btnSave.Enabled = true;
                    btnCollection.Enabled = true;
                    myScP.UpdateVOStatus(0);
                }
            }
        }
    }
}


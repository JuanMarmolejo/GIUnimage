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
    public partial class frmPreviewVO : Form
    {
        clsElement eleShip = new clsElement();
        clsListElements lstBillFrom = new clsListElements();
        clsListElements lstPurchaseType = new clsListElements();
        clsListElements lstDivisions = new clsListElements();
        clsListElements lstWhs = new clsListElements();
        clsListSeason lstSeasons = new clsListSeason();
        clsListElements lstCollections = new clsListElements();
        public frmPreviewVO()
        {
            InitializeComponent();
        }

        private void frmPreviewVO_Load(object sender, EventArgs e)
        {
            try
            {
                LoadComboBoxes();
                DeactivateControls();

                txtColorCode.Text = clsSilex.GetColorCode(clsGlobals.VorderDetail.Elements[0].ColorID);
                txtColorName.Text = clsSilex.GetColorName(clsGlobals.VorderDetail.Elements[0].ColorID);
                txtOrderNumber.Text = "VO-????";
                txtProductCode.Text = clsSilex.GetProductCode(clsGlobals.VorderDetail.Elements[0].ProductID);
                txtProductDescription.Text = clsSilex.GetProductDesc(clsGlobals.VorderDetail.Elements[0].ProductID);
                txtRef1.Text = clsGlobals.Vorder.ReferenceNo1;
                txtRef2.Text = clsGlobals.Vorder.ReferenceNo2;
                txtVOMessage.Text = clsGlobals.Vorder.VOMessage;
                txtVONote.Text = clsGlobals.Vorder.VONote;

                dtpArrivalDate.Value = clsGlobals.Vorder.ExpArrivalDate;
                dtpShipDate.Value = clsGlobals.Vorder.ExpShippingDate;

                cboBillFrom.SelectedValue = clsGlobals.Vorder.VendorID.ToString();
                cboCollection.SelectedValue = clsGlobals.Vorder.CollectionID.ToString();
                cboDivision.SelectedValue = clsGlobals.Vorder.DivisionID.ToString();
                cboPurchaseType.SelectedValue = clsGlobals.Vorder.PurchaseTypeID.ToString();
                cboSeason.SelectedValue = clsGlobals.Vorder.SeasonID;
                cboWhs.SelectedValue = clsGlobals.Vorder.DefaultWarehouseID.ToString();

                dgvResult.DataSource = DebugList(clsGlobals.VorderDetail.Elements);
                dgvResult.Columns["ElementID"].HeaderText = "Dim";
                dgvResult.Columns["Code"].HeaderText = "Size";
                dgvResult.Columns["Name"].HeaderText = "QVO";
                dgvResult.Columns["Full"].Visible = false;
                dgvResult.AutoResizeColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DeactivateControls()
        {
            txtColorCode.ReadOnly = true;
            txtColorName.ReadOnly = true;
            txtOrderNumber.ReadOnly = true;
            txtProductCode.ReadOnly = true;
            txtProductDescription.ReadOnly = true;
            txtRef1.ReadOnly = true;
            txtRef2.ReadOnly = true;
            txtShipFrom.ReadOnly = true;
            txtVOMessage.ReadOnly = true;
            txtVONote.ReadOnly = true;
            cboBillFrom.Enabled = false;
            cboCollection.Enabled = false;
            cboDivision.Enabled = false;
            cboPurchaseType.Enabled = false;
            cboSeason.Enabled = false;
            cboWhs.Enabled = false;
            
            dtpArrivalDate.Enabled = false;
            dtpShipDate.Enabled = false;
        }

        private object DebugList(List<clsScVorderDetail> elements)
        {
            clsListElements myList = new clsListElements();
            int cont = 0;
            foreach(clsScVorderDetail ele in elements)
            {
                clsElement myEle = new clsElement();
                cont++;
                myEle.ElementID = ele.Dim;
                myEle.Code = ele.Size;
                myEle.Name = ele.OrderQty.ToString();
                myList.Add(myEle);
            }
            return myList.Elements;
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

            lstDivisions.GetDivisions();
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
        }

        private void cboDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboDivision.SelectedIndex >= 0)
                {
                    lstCollections.GetCollections(Convert.ToInt32(cboDivision.SelectedValue));
                    cboCollection.DisplayMember = "Full";
                    cboCollection.ValueMember = "ElementID";
                    cboCollection.DataSource = lstCollections.Elements;
                    cboCollection.SelectedValue = clsGlobals.Vorder.CollectionID.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cboBillFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboBillFrom.SelectedIndex > 0)
                {
                    eleShip.GetShipFrom(Convert.ToInt32(cboBillFrom.SelectedValue));
                    txtShipFrom.Text = eleShip.Full;
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
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            clsGlobals.giVOID = clsGlobals.Vorder.SaveVorder();
            if (clsGlobals.Vorder.IsUnimage())
            {
                clsGlobals.VorderDetail.SaveVorderDetailUnimage(clsGlobals.giVOID);
            }
            else
            {
                clsGlobals.VorderDetail.SaveVorderDetail(clsGlobals.giVOID);
            }
            clsScenario.UpdateModifiedByUser(clsGlobals.GIPar.ScenarioID);
            try
            {
                clsSilex.RunStoredProcedure(clsGlobals.giVOID);
                clsScSalesHistory.SavePurchasedQuantities(clsGlobals.Vorder, clsGlobals.VorderDetail);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            clsGlobals.Vorder.GetVoCodeBy(clsGlobals.giVOID);
            clsScProduct.UpdateVoCode(clsGlobals.Vorder.VOCode, clsGlobals.ScProductID);
            MessageBox.Show("La VO a été générée avec succès");
            this.Close();
        }
    }
}

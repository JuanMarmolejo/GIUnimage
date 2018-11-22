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
    public partial class frmVOParameters : Form
    {
        clsListElements lstBillFrom = new clsListElements();
        clsListElements lstPurchaseType = new clsListElements();
        clsListElements lstWhs = new clsListElements();
        clsListElements lstDivisions = new clsListElements();
        clsListElements lstCollections = new clsListElements();
        clsListSeason lstSeasons = new clsListSeason();
        clsElement eleShip = new clsElement();
        public frmVOParameters()
        {
            InitializeComponent();
        }

        private void frmVOParameters_Load(object sender, EventArgs e)
        {
            clsGIParameter myPar = new clsGIParameter();
            LoadComboBoxes();
            myPar.GetVOParameters();
            GIParametersToText(myPar);
            DeactivateControls();
        }

        private void GIParametersToText(clsGIParameter myPar)
        {
            txtRef1.Text = myPar.ReferenceNo1;
            txtRef2.Text = myPar.ReferenceNo2;
            txtVOMessage.Text = myPar.VOMessage;
            txtVONote.Text = myPar.VONote;
            cboBillFrom.SelectedValue = myPar.VendorID.ToString();
            cboCollection.SelectedValue = myPar.CollectionID.ToString();
            cboDivision.SelectedValue = myPar.DivisionID.ToString();
            cboPurchaseType.SelectedValue = myPar.PurchaseTypeID.ToString();
            cboSeason.SelectedValue = myPar.SeasonID.ToString();
            cboWhs.SelectedValue = myPar.DefaultWarehouseID.ToString();
            dtpArrivalDate.Value = myPar.ExpArrivalDate;
            dtpShipDate.Value = myPar.ExpShippingDate;
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
            txtRef1.ReadOnly = true;
            txtRef2.ReadOnly = true;
            txtShipFrom.ReadOnly = true;
            txtVONote.ReadOnly = true;
            txtVOMessage.ReadOnly = true;
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
        }

        private void ActivateControls()
        {
            txtRef1.ReadOnly = false;
            txtRef2.ReadOnly = false;
            txtVONote.ReadOnly = false;
            txtVOMessage.ReadOnly = false;
            dtpArrivalDate.Enabled = true;
            dtpShipDate.Enabled = true;
            cboBillFrom.Enabled = true;
            cboCollection.Enabled = true;
            cboDivision.Enabled = true;
            cboPurchaseType.Enabled = true;
            cboSeason.Enabled = true;
            //cboWhs.Enabled = true;
            btnSave.Enabled = true;
            btnModify.Enabled = false;
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            ActivateControls();
            txtRef1.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            TextToGIParameter().UpdateVOParameter();
            DeactivateControls();
        }

        private clsGIParameter TextToGIParameter()
        {
            clsGIParameter myPar = new clsGIParameter();

            myPar.CollectionID = Convert.ToInt32(cboCollection.SelectedValue);
            myPar.DefaultWarehouseID = Convert.ToInt32(cboWhs.SelectedValue);
            myPar.DivisionID = Convert.ToInt32(cboDivision.SelectedValue);
            myPar.ExpArrivalDate = Convert.ToDateTime(dtpArrivalDate.Value);
            myPar.ExpShippingDate = Convert.ToDateTime(dtpShipDate.Value);
            //myPar.GIParameterID = Convert.ToInt32("100");
            myPar.PurchaseTypeID = Convert.ToInt32(cboPurchaseType.SelectedValue);
            myPar.ReferenceNo1 = Convert.ToString(txtRef1.Text.Trim());
            myPar.ReferenceNo2 = Convert.ToString(txtRef2.Text.Trim());
            myPar.SeasonID = Convert.ToInt32(cboSeason.SelectedValue);
            myPar.VendorID = Convert.ToInt32(cboBillFrom.SelectedValue);
            myPar.VendorSiteID = Convert.ToInt32(eleShip.ElementID);
            myPar.VOMessage = Convert.ToString(txtVOMessage.Text.Trim());
            myPar.VONote = Convert.ToString(txtVONote.Text.Trim());

            return myPar;
        }

        private void cboBillFrom_SelectedIndexChanged(object sender, EventArgs e)
        {   
            if (cboBillFrom.SelectedIndex > 0)
            {
                eleShip.GetShipFrom(Convert.ToInt32(cboBillFrom.SelectedValue));
                txtShipFrom.Text = eleShip.Full;
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
            if (clsFrmGlobals.frGP == null)
            {
                clsFrmGlobals.frGP = new frmGeneralParameters();
                clsFrmGlobals.frGP.MdiParent = this.MdiParent;
                clsFrmGlobals.frGP.FormClosed += new FormClosedEventHandler(frGPFromClosed);
                clsFrmGlobals.frGP.Show();
                this.Close();
            }
        }

        private void frGPFromClosed(object sender, FormClosedEventArgs e)
        {
            clsFrmGlobals.frGP = null;
        }
    }
}

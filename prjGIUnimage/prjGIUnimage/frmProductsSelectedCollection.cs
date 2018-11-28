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
    public partial class frmProductsSelectedCollection : Form
    {
        frmOrderProductsCollections frOP = null;
        clsListElements AllElements = new clsListElements();
        public frmProductsSelectedCollection()
        {
            InitializeComponent();
            clsGlobals.CollectionsFlag = false;
        }

        private void frmProductsSelectedCollection_Load(object sender, EventArgs e)
        {
            try
            {
                btnMenu.Enabled = false;
                AllElements.GetElementsGlobalRequest();
                dgvResult.DataSource = AllElements.Elements;
                dgvResult.Columns[0].Visible = false;
                dgvResult.Columns[3].Visible = false;
                dgvResult.AutoResizeColumns();
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
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvResult_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                clsScenario mySce = new clsScenario();
                mySce.GetScenarioByID(clsGlobals.GIPar.ScenarioID);
                clsGlobals.GIPar.ProductColorID = Convert.ToInt32(dgvResult.CurrentRow.Cells[0].Value);
                clsGlobals.ActiveRatio = mySce.SurplusRateIdentified;
                clsGlobals.BkRatio = mySce.SurplusRateIdentified;
                clsGlobals.CollectionsFlag = true;
                if (frOP == null)
                {
                    frOP = new frmOrderProductsCollections();
                    frOP.MdiParent = this.MdiParent;
                    frOP.FormClosed += new FormClosedEventHandler(frOPFromClosed);
                    frOP.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void frOPFromClosed(object sender, FormClosedEventArgs e)
        {
            frOP = null;
        }
    }
}

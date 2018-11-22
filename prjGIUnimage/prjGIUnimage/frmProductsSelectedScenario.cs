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
    public partial class frmProductsSelectedScenario : Form
    {
        clsListElements AllElements = new clsListElements();
        frmOrderProducts frOP = null;
        public frmProductsSelectedScenario()
        {
            InitializeComponent();
        }

        private void frmProductsSelectedScenario_Load(object sender, EventArgs e)
        {
            btnMenu.Enabled = false;
            this.ActiveControl = txtSearch;
            try
            {
                AllElements.GetElementsGlobalRequest();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            dgvResult.DataSource = AllElements.Elements;
            dgvResult.Columns[0].Visible = false;
            dgvResult.Columns[3].Visible = false;
            dgvResult.AutoResizeColumns();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void dgvResult_DoubleClick(object sender, EventArgs e)
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

        private void dgvResult_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string myText = txtSearch.Text.Trim().ToUpper();

            AllElements.GetElementsGlobalRequest();
            AllElements.FilterElements(myText);
            dgvResult.DataSource = AllElements.Elements;
            dgvResult.Columns[0].Visible = false;
            dgvResult.Columns[3].Visible = false;
            dgvResult.AutoResizeColumns();
        }
    }
}

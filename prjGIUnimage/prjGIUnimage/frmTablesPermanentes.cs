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
    public partial class frmTablesPermanentes : Form
    {
        public frmTablesPermanentes()
        {
            InitializeComponent();
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

        private void frmTablesPermanentes_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (clsFrmGlobals.frCL == null)
                {
                    clsFrmGlobals.frCL = new frmCollections();
                    clsFrmGlobals.frCL.MdiParent = this.MdiParent;
                    clsFrmGlobals.frCL.FormClosed += new FormClosedEventHandler(frCLFromClosed);
                    clsFrmGlobals.frCL.Show();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void frCLFromClosed(object sender, FormClosedEventArgs e)
        {
            clsFrmGlobals.frCL = null;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (clsFrmGlobals.frPC == null)
                {
                    clsFrmGlobals.frPC = new frmProductColor();
                    clsFrmGlobals.frPC.MdiParent = this.MdiParent;
                    clsFrmGlobals.frPC.FormClosed += new FormClosedEventHandler(frPCFromClosed);
                    clsFrmGlobals.frPC.Show();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void frPCFromClosed(object sender, FormClosedEventArgs e)
        {
            clsFrmGlobals.frPC = null;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (clsFrmGlobals.frEP == null)
                {
                    clsFrmGlobals.frEP = new frmEquivalentProduct();
                    clsFrmGlobals.frEP.MdiParent = this.MdiParent;
                    clsFrmGlobals.frEP.FormClosed += new FormClosedEventHandler(frEPFromClosed);
                    clsFrmGlobals.frEP.Show();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void frEPFromClosed(object sender, FormClosedEventArgs e)
        {
            clsFrmGlobals.frEP = null;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void frGPFromClosed(object sender, FormClosedEventArgs e)
        {
            clsFrmGlobals.frGP = null;
        }
    }
}

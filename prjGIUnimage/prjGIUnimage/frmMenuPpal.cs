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
    public partial class frmMenuPpal : Form
    {
        public frmMenuPpal()
        {
            InitializeComponent();
        }

        private void frmMenuPpal_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (clsFrmGlobals.frTP == null)
                {
                    clsFrmGlobals.frTP = new frmTablesPermanentes();
                    clsFrmGlobals.frTP.MdiParent = this.MdiParent;
                    clsFrmGlobals.frTP.FormClosed += new FormClosedEventHandler(frTPClosed);
                    clsFrmGlobals.frTP.Show();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void frTPClosed(object sender, FormClosedEventArgs e)
        {
            clsFrmGlobals.frTP = null;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (clsFrmGlobals.frPS == null)
                {
                    clsFrmGlobals.frPS = new frmPreparationSession();
                    clsFrmGlobals.frPS.MdiParent = this.MdiParent;
                    clsFrmGlobals.frPS.FormClosed += new FormClosedEventHandler(frPSFromClosed);
                    clsFrmGlobals.frPS.Show();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void frPSFromClosed(object sender, FormClosedEventArgs e)
        {
            clsFrmGlobals.frPS = null;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void frNSFromClosed(object sender, FormClosedEventArgs e)
        {
            clsFrmGlobals.frNS = null;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (clsFrmGlobals.frPDF == null)
                {
                    clsFrmGlobals.frPDF = new frmPDFPrinting();
                    clsFrmGlobals.frPDF.MdiParent = this.MdiParent;
                    clsFrmGlobals.frPDF.FormClosed += new FormClosedEventHandler(frPDFFromClosed);
                    clsFrmGlobals.frPDF.Show();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void frPDFFromClosed(object sender, FormClosedEventArgs e)
        {
            clsFrmGlobals.frPDF = null;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (clsFrmGlobals.frSSc == null)
                {
                    clsFrmGlobals.frSSc = new frmSearchScenarios();
                    clsFrmGlobals.frSSc.MdiParent = this.MdiParent;
                    clsFrmGlobals.frSSc.FormClosed += new FormClosedEventHandler(frSScFromClosed);
                    clsFrmGlobals.frSSc.Show();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void frSScFromClosed(object sender, FormClosedEventArgs e)
        {
            clsFrmGlobals.frSSc = null;
        }
    }
}

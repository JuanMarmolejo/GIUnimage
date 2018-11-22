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
    public partial class frmGeneralParameters : Form
    {
        public frmGeneralParameters()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (clsFrmGlobals.frVP == null)
            {
                clsFrmGlobals.frVP = new frmVOParameters();
                clsFrmGlobals.frVP.MdiParent = this.MdiParent;
                clsFrmGlobals.frVP.FormClosed += new FormClosedEventHandler(frVPFromClosed);
                clsFrmGlobals.frVP.Show();
                this.Close();
            }
        }

        private void frVPFromClosed(object sender, FormClosedEventArgs e)
        {
            clsFrmGlobals.frVP = null;
        }

        private void button3_Click(object sender, EventArgs e)
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

        private void button8_Click(object sender, EventArgs e)
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

        private void frTPClosed(object sender, FormClosedEventArgs e)
        {
            clsFrmGlobals.frTP = null;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (clsFrmGlobals.frSP == null)
            {
                clsFrmGlobals.frSP = new frmSurplusParameters();
                clsFrmGlobals.frSP.MdiParent = this.MdiParent;
                clsFrmGlobals.frSP.FormClosed += new FormClosedEventHandler(frSPClosed);
                clsFrmGlobals.frSP.Show();
                this.Close();
            }
        }

        private void frSPClosed(object sender, FormClosedEventArgs e)
        {
            clsFrmGlobals.frSP = null;
        }

        private void frmGeneralParameters_Load(object sender, EventArgs e)
        {

        }
    }
}

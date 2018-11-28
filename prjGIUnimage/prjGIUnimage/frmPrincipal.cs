using prjGIUnimage.bus;
using prjGIUnimage.data;
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
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            try
            {
                if (clsFrmGlobals.frMP == null)
                {
                    clsFrmGlobals.frMP = new frmMenuPpal();
                    clsFrmGlobals.frMP.MdiParent = this;
                    clsFrmGlobals.frMP.FormClosed += new FormClosedEventHandler(frMPClosed);
                    clsFrmGlobals.frMP.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void frMPClosed(object sender, FormClosedEventArgs e)
        {
            clsFrmGlobals.frMP = null;
            clsGlobals.GIPar.UpdateIDVariables();
            clsGlobals.GIPar.SetUserID();
            if (Application.OpenForms.Count == 1)
            {
                Conexion.EndSession();
                Application.Exit();
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void menuPrincipalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (clsFrmGlobals.frMP == null)
                {
                    clsFrmGlobals.frMP = new frmMenuPpal();
                    clsFrmGlobals.frMP.MdiParent = this;
                    clsFrmGlobals.frMP.FormClosed += new FormClosedEventHandler(frMPClosed);
                    clsFrmGlobals.frMP.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

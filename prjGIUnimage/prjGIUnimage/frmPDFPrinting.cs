using prjGIUnimage.bus;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prjGIUnimage
{
    public partial class frmPDFPrinting : Form
    {
        clsListScenario lstScn = new clsListScenario();
        clsScenario mySce = new clsScenario();
        clsSeason mySea = new clsSeason();
        string HeaderInformation = "";
        int scenarioID = 0;
        int cont = 0;

        public frmPDFPrinting()
        {
            InitializeComponent();
        }

        private void frmPDFPrinting_Load(object sender, EventArgs e)
        {
            try
            {
                lstScn.AllScenarios();

                cboScenario.DisplayMember = "ScenarioCode";
                cboScenario.ValueMember = "GIScenarioID";
                cboScenario.DataSource = lstScn.Elements;
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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                HeaderInformation = "";
                HeaderInformation += "Nom du scénario: " + mySce.ScenarioCode + "\n";
                HeaderInformation += "Season: " + clsSilex.GetNameSeason(mySea.SXSeasonID) + "\n";
                HeaderInformation += "Date: " + DateTime.Now.ToLongDateString() + "\n";
                clsPrint myPrint = new clsPrint(dgvResult, HeaderInformation);
                myPrint.PrintForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cboScenario_SelectedIndexChanged(object sender, EventArgs e)
        {
            scenarioID = Convert.ToInt32(cboScenario.SelectedValue);
            cont++;
            mySce.GetScenarioByID(scenarioID);
            dgvResult.DataSource = clsListScProduct.GetListVOToPrint(scenarioID);
            try
            {
                mySea.GetSeasonByID(mySce.VOSeasonID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnPrinted_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Êtes-vous sûr de marquer cette liste de VO comme déjà imprimé?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    clsScProduct.UpdateVoPdfStatus(scenarioID);
                    dgvResult.DataSource = clsListScProduct.GetListVOToPrint(scenarioID);
                    MessageBox.Show("La liste a été mise à jour.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

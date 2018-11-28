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
    public partial class frmGeneratesFiscal : Form
    {
        clsListSeason lstSea = new clsListSeason();
        clsScenario mySce = new clsScenario();
        bool Flag = clsGlobals.Flag;
        public frmGeneratesFiscal()
        {
            InitializeComponent();
        }

        private void frmGeneratesFiscal_Load(object sender, EventArgs e)
        {
            try
            {
                if (Flag == true)
                {
                    lblAvant.Visible = true;
                    lblBefore.Visible = false;
                    txtCode.Text = "Avant Fiscal";
                    txtCode.ReadOnly = true;
                    txtPreviousSeason.ReadOnly = true;
                    cboCurrentSaison.SelectedIndex = -1;
                    lstSea.GetNotGeneratedBefiscal();
                }

                if (Flag == false)
                {
                    lblAvant.Visible = false;
                    lblBefore.Visible = true;
                    txtCode.Text = "Après Fiscal";
                    txtCode.ReadOnly = true;
                    txtPreviousSeason.ReadOnly = true;
                    cboCurrentSaison.SelectedIndex = -1;
                    lstSea.GetNotGeneratedAfiscal();
                }
                cboCurrentSaison.DisplayMember = "SeasonName";
                cboCurrentSaison.ValueMember = "GISeasonID";
                cboCurrentSaison.DataSource = lstSea.Elements;
                btnRun.Enabled = lstSea.Quantity > 0 ? true : false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cboCurrentSaison_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtPreviousSeason.Text = lstSea.PreviousSeason(Convert.ToInt32(cboCurrentSaison.SelectedValue));
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

        private void btnRun_Click(object sender, EventArgs e)
        {
            try
            {
                mySce.ScenarioCode = txtCode.Text;
                mySce.GISeasonID = Convert.ToInt32(cboCurrentSaison.SelectedValue);
                mySce.ScenarioStatus = 2;
                if (mySce.Exists())
                {
                    MessageBox.Show("Attention, l'inventaire a déjà été généré", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    clsGlobals.NextScenarioID = clsScenario.NextScenarioID();
                    GenerateFiscal();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Close();
            }
        }

        private void frESFromClosed(object sender, FormClosedEventArgs e)
        {
            if (clsGlobals.Flag)
            {
                mySce.InsertScenarioFiscal();
                clsSeason.UpdateGeneratedStatus(Flag, mySce.GISeasonID);
                CopyTableFiscal(Flag);
                MessageBox.Show("Le inventaire a été créé correctement", "Exclamation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                MessageBox.Show("Le inventaire n'a pas été créé.", "Exclamation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            clsFrmGlobals.frES = null;
        }

        private void GenerateFiscal()
        {
            
            clsGlobals.GISeasonID = mySce.GISeasonID;
            clsGlobals.OriginOfStoredProc = 2;

            //abre formulario de espera
            if (clsFrmGlobals.frES == null)
            {
                clsFrmGlobals.frES = new frmExecutionStoredProcedure();
                clsFrmGlobals.frES.MdiParent = this.MdiParent;
                clsFrmGlobals.frES.FormClosed += new FormClosedEventHandler(frESFromClosed);
                clsFrmGlobals.frES.Show();
            }
        }

        private void CopyTableFiscal(bool flag)
        {
            clsListScSalesHistory lstSalH = new clsListScSalesHistory();
            clsListInvFiscal lstInv = new clsListInvFiscal();
            lstSalH.GetSalesHistory(clsGlobals.NextScenarioID);
            foreach(clsScSalesHistory ele in lstSalH.Elements)
            {
                clsInvFiscal myInv = new clsInvFiscal();
                myInv.CopyElement(ele);
                lstInv.AddInvFiscal(myInv);
            }
            lstInv.SaveListInvFiscal(flag);
        }
    }
}

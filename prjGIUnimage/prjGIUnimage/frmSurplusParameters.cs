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
    public partial class frmSurplusParameters : Form
    {
        public frmSurplusParameters()
        {
            InitializeComponent();
        }

        private void frmSurplusParameters_Load(object sender, EventArgs e)
        {
            try
            {
                clsGIParameter myPar = new clsGIParameter();
                myPar.GetSurplusParameters();
                GIParameterToText(myPar);
                DeactivateControls();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void GIParameterToText(clsGIParameter myPar)
        {
            txtCommon.Text = myPar.SurplusRateCommon.ToString();
            txtIdentified.Text = myPar.SurplusRateIdentified.ToString();
            txtOS.Text = myPar.SurplusRateOS.ToString();
            txtUnique.Text = myPar.SurplusRateUnique.ToString();
        }

        private void DeactivateControls()
        {
            txtCommon.ReadOnly = true;
            txtIdentified.ReadOnly = true;
            txtOS.ReadOnly = true;
            txtUnique.ReadOnly = true;
            btnModify.Enabled = true;
            btnSave.Enabled = false;
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            try
            {
                ActivateControls();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ActivateControls()
        {
            txtCommon.ReadOnly = false;
            txtIdentified.ReadOnly = false;
            txtOS.ReadOnly = false;
            txtUnique.ReadOnly = false;
            btnModify.Enabled = false;
            btnSave.Enabled = true;
            txtUnique.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateChildren(ValidationConstraints.Enabled))
                {
                    if (Convert.ToInt32(txtCommon.Text) > 100 || Convert.ToInt32(txtIdentified.Text) > 100 || Convert.ToInt32(txtOS.Text) > 100 || Convert.ToInt32(txtUnique.Text) > 100)
                    {
                        if (MessageBox.Show("Vous êtes sûr d'utiliser un surplus supérieur à 100%", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            TextToGIParameter().UpDateSurplusParameters();
                            DeactivateControls();
                        }
                    }
                    else
                    {
                        TextToGIParameter().UpDateSurplusParameters();
                        DeactivateControls();
                    }
                    clsGlobals.GIPar.GetSurplusParameters();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private clsGIParameter TextToGIParameter()
        {
            clsGIParameter myPar = new clsGIParameter();

            myPar.SurplusRateCommon = Convert.ToInt32(txtCommon.Text);
            myPar.SurplusRateIdentified = Convert.ToInt32(txtIdentified.Text);
            myPar.SurplusRateOS = Convert.ToInt32(txtOS.Text);
            myPar.SurplusRateUnique = Convert.ToInt32(txtUnique.Text);

            return myPar;
        }

        private void btnMenu_Click(object sender, EventArgs e)
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

        private void btnReturn_Click(object sender, EventArgs e)
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

        private void txtUnique_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (!IsNumber(txtUnique.Text))
                {
                    e.Cancel = true;
                    txtUnique.Focus();
                    errorProvider1.SetError(txtUnique, "Entrez une valeur numérique");
                }
                else
                {
                    e.Cancel = false;
                    errorProvider1.SetError(txtUnique, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool IsNumber(string text)
        {
            try
            {
                double x = Convert.ToInt32(text);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void txtCommon_Validating(object sender, CancelEventArgs e)
        {
            if (!IsNumber(txtCommon.Text))
            {
                e.Cancel = true;
                txtCommon.Focus();
                errorProvider1.SetError(txtCommon, "Entrez une valeur numérique");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtCommon, null);
            }
        }

        private void txtIdentified_Validating(object sender, CancelEventArgs e)
        {
            if (!IsNumber(txtIdentified.Text))
            {
                e.Cancel = true;
                txtIdentified.Focus();
                errorProvider1.SetError(txtIdentified, "Entrez une valeur numérique");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtIdentified, null);
            }
        }

        private void txtOS_Validating(object sender, CancelEventArgs e)
        {
            if (!IsNumber(txtOS.Text))
            {
                e.Cancel = true;
                txtOS.Focus();
                errorProvider1.SetError(txtOS, "Entrez une valeur numérique");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtOS, null);
            }
        }
    }
}

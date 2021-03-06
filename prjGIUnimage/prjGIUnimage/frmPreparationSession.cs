﻿using prjGIUnimage.bus;
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
    public partial class frmPreparationSession : Form
    {
        public frmPreparationSession()
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

        private void frmPreparationSession_Load(object sender, EventArgs e)
        {
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (clsFrmGlobals.frGF == null)
                {
                    clsGlobals.Flag = true;
                    clsFrmGlobals.frGF = new frmGeneratesFiscal();
                    clsFrmGlobals.frGF.MdiParent = this.MdiParent;
                    clsFrmGlobals.frGF.FormClosed += new FormClosedEventHandler(frGFAFromClosed);
                    clsFrmGlobals.frGF.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (clsFrmGlobals.frGF == null)
                {
                    clsGlobals.Flag = false;
                    clsFrmGlobals.frGF = new frmGeneratesFiscal();
                    clsFrmGlobals.frGF.MdiParent = this.MdiParent;
                    clsFrmGlobals.frGF.FormClosed += new FormClosedEventHandler(frGFBFromClosed);
                    clsFrmGlobals.frGF.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void frGFBFromClosed(object sender, FormClosedEventArgs e)
        {
            clsFrmGlobals.frGF = null;
        }

        private void frGFAFromClosed(object sender, FormClosedEventArgs e)
        {
            clsFrmGlobals.frGF = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}

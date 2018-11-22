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
    public partial class frmExecutionStoredProcedure : Form
    {
        public frmExecutionStoredProcedure()
        {
            InitializeComponent();
        }

        private void frmExecutionStoredProcedure_Load(object sender, EventArgs e)
        {
            if (clsGlobals.OriginOfStoredProc == 1)
            {
                lblInventory.Visible = false;
                lblScenarios.Visible = true;
            }
            if (clsGlobals.OriginOfStoredProc == 2)
            {
                lblInventory.Visible = true;
                lblScenarios.Visible = false;
            }
        }

        private bool LoadTabletblGIScSalesHistory(int nextScenarioID, int gISeasonID)
        {
            //clsListSeason lstSea = new clsListSeason();
            clsSeason mySea = new clsSeason();
            //lstSea.GetAllSeasons();
            mySea.GetSeasonByID(gISeasonID);
            try
            {
                if(clsGlobals.SecondSeasonID > 99 && clsGlobals.OriginOfStoredProc == 1)
                {
                    clsSeason mySSea = new clsSeason();
                    mySSea.GetSeasonByID(clsGlobals.SecondSeasonID);
                    clsGlobals.SecondSeasonID = mySSea.SXSeasonID;
                }
                else
                {
                    clsGlobals.SecondSeasonID = -1;
                }
                clsSilex.RunStoredProcedure(nextScenarioID, mySea.SXSeasonID, clsGlobals.SecondSeasonID, mySea.SXSeasonPrecID);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private void frmExecutionStoredProcedure_Shown(object sender, EventArgs e)
        {
            MessageBox.Show("La création du scénario peut prendre quelques minutes...");
            if (LoadTabletblGIScSalesHistory(clsGlobals.NextScenarioID, clsGlobals.GISeasonID))
            {
                clsGlobals.Flag = true;
            }
            else
            {
                clsGlobals.Flag = false;
            }
            this.Close();
        }
    }
}

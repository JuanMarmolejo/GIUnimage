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
    public partial class delEquivProd : Form
    {
        clsListSeason lstSea = new clsListSeason();
        clsScenario aDiv = new clsScenario();
        clsListData lstStatus = new clsListData();
        clsListSeason lstSecSea = new clsListSeason();
        clsListScenario lstScn = new clsListScenario();
        DataSet mySet = new DataSet();
        //bool flagNew = false;

        public delEquivProd()
        {
            InitializeComponent();
        }

        private void delEquivProd_Load(object sender, EventArgs e)
        {
            DeactivateTexts();

            lstStatus.DataByGroup(102);
            lstSea.GetAllSeasons();
            lstSecSea.GetAllSeasons();

            cboCurrentSaison.DisplayMember = "SeasonName";
            cboCurrentSaison.ValueMember = "GISeasonID";
            cboCurrentSaison.DataSource = lstSea.Elements;
            cboCurrentSaison.SelectedIndex = -1;

            lstScn.AllScenarios();
            LinkListScenarios();
        }

        private void LinkListScenarios()
        {
            
        }

        private void DeactivateTexts()
        {
            txtCode.ReadOnly = true;
            
            cboCurrentSaison.Enabled = false;
            btnRecord.Enabled = false;
            btnModify.Enabled = true;
            btnNew.Enabled = true;   
        }

        private bool LoadTabletblGIScSalesHistory(int nextScenarioID, int gISeasonID)
        {
            clsSeason mySea = new clsSeason();
            
            mySea = lstSea.GetSeasonByID(gISeasonID);
            try
            {
                RunStoredProcedure(nextScenarioID, mySea.SXSeasonID, clsGlobals.SecondSeasonID, mySea.SXSeasonPrecID);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private void RunStoredProcedure(int activeScenario, int sXSeasonID, int secondSeasonID, int sXSeasonPrecID)
        {
            DataTable myTb = new DataTable();
            DataSet mySet = new DataSet();
            Object[] args = new Object[] { activeScenario, sXSeasonID, secondSeasonID, sXSeasonPrecID };
            //Conexion.StartSession();
            mySet = Conexion.GDatos.GetDataSet(clsGlobals.Silex + "spCustomGenerateReqVirtual", args);
            //Conexion.EndSession();
            myTb = mySet.Tables[0];
            dgvResult.DataSource = myTb;
            txtCode.Text = myTb.Rows.Count.ToString();
        }

        private void btnRecord_Click(object sender, EventArgs e)
        {
            if (ValidateChildren(ValidationConstraints.Enabled))
            {
                clsGlobals.GIPar.GetVOParameters();
                aDiv = TextToScenario();
                clsGlobals.NextScenarioID = clsScenario.NextScenarioID();
                clsGlobals.GISeasonID = aDiv.GISeasonID;
                clsGlobals.OriginOfStoredProc = 1;

                //abre formulario de espera
                MessageBox.Show("La création du scénario peut prendre quelques minutes...");
                if (LoadTabletblGIScSalesHistory(clsGlobals.NextScenarioID, clsGlobals.GISeasonID))
                {
                    clsGlobals.Flag = true;
                }
                else
                {
                    clsGlobals.Flag = false;
                }
            }
        }

        private void frESFromClosed(object sender, FormClosedEventArgs e)
        {
            if (clsGlobals.Flag)
            {
                MessageBox.Show("Le scénario a été créé correctement", "Exclamation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                MessageBox.Show("Le scénario n'a pas été créé.", "Exclamation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            clsFrmGlobals.frES = null;
        }

        private clsScenario TextToScenario()
        {
            clsScenario aTem = new clsScenario();
            aTem.ScenarioCode = Convert.ToString(txtCode.Text);
            aTem.GISeasonID = Convert.ToInt32(cboCurrentSaison.SelectedValue);
            
            return aTem;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            ActivateTexts();
            CleanControls();
            //flagNew = true;
        }

        private void ActivateTexts()
        {
            txtCode.ReadOnly = false;
            cboCurrentSaison.Enabled = true;
            btnRecord.Enabled = true;
            btnModify.Enabled = false;
            btnNew.Enabled = false;
        }

        private void CleanControls()
        {
            txtCode.Clear();
            cboCurrentSaison.SelectedIndex = -1;
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            copyAlltoClipboard();
            Microsoft.Office.Interop.Excel.Application xlexcel;
            Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;
            xlexcel = new Microsoft.Office.Interop.Excel.Application();
            xlexcel.Visible = true;
            xlWorkBook = xlexcel.Workbooks.Add(misValue);
            xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            Microsoft.Office.Interop.Excel.Range CR = (Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[1, 1];
            CR.Select();
            xlWorkSheet.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);
        }

        private void copyAlltoClipboard()
        {
            dgvResult.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            dgvResult.MultiSelect = true;
            dgvResult.SelectAll();
            DataObject dataObj = dgvResult.GetClipboardContent();
            if (dataObj != null)
                Clipboard.SetDataObject(dataObj);
        }
    }
}

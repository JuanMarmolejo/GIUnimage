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
    public partial class frmGroupsToWork : Form
    {
        clsListElements AllElements = new clsListElements();
        public frmGroupsToWork()
        {
            InitializeComponent();
        }

        private void frmGroupsToWork_Load(object sender, EventArgs e)
        {
            clsGlobals.ListTemp = new clsListElements();
            clsGlobals.ListTemp = clsGlobals.lstBackUp;
            this.ActiveControl = txtSearch;
            try
            {
                AllElements.GetElementsGlobalRequest();
                if (clsGlobals.lstBackUp.Quantity > 0){
                    foreach(clsElement ele in clsGlobals.lstBackUp.Elements)
                    {
                        AllElements.RemoveItem(ele);
                    }
                }
                LinkLists();
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
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                clsElement eTemp = new clsElement();

                if (lstAllEle.SelectedValue == null || String.IsNullOrEmpty(lstAllEle.SelectedValue.ToString()))
                {
                    MessageBox.Show("Sélectionnez un élément de la liste");
                }
                else
                {
                    eTemp = AllElements.ElementByID(lstAllEle.SelectedValue.ToString());
                    clsGlobals.ListTemp.AddNotExist(eTemp);
                    AllElements.RemoveItem(eTemp);
                    LinkLists();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LinkLists()
        {
            lstSelectedEle.DataSource = null;
            lstSelectedEle.DataSource = clsGlobals.ListTemp.Elements;
            lstSelectedEle.DisplayMember = "Full";
            lstSelectedEle.ValueMember = "ElementID";
            
            lstAllEle.DataSource = null;
            lstAllEle.DataSource = AllElements.Elements;
            lstAllEle.DisplayMember = "Full";
            lstAllEle.ValueMember = "ElementID";
            
            lstSelectedEle.Focus();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (AllElements.Quantity > 0)
                {
                    clsGlobals.ListTemp = AllElements;
                    AllElements = new clsListElements();
                    LinkLists();
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
                AllElements.GetElementsGlobalRequest();
                clsGlobals.ListTemp = new clsListElements();
                LinkLists();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                clsElement eTemp = new clsElement();
                string index = "";

                if (lstSelectedEle.SelectedValue == null || String.IsNullOrEmpty(lstSelectedEle.SelectedValue.ToString()))
                {
                    MessageBox.Show("Sélectionnez un élément de la liste");
                }
                else
                {
                    index = lstSelectedEle.SelectedValue.ToString();
                    eTemp = clsGlobals.ListTemp.ElementByID(index);
                    AllElements.AddNotExist(eTemp);
                    clsGlobals.ListTemp.RemoveItem(eTemp);
                    LinkLists();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string myText = txtSearch.Text.Trim().ToUpper();

                AllElements.GetElementsGlobalRequest();
                AllElements.FilterElements(myText);
                lstAllEle.DataSource = AllElements.Elements;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lstAllEle_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void lstAllEle_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                clsElement eTemp = new clsElement();
                if (lstAllEle.SelectedValue == null || String.IsNullOrEmpty(lstAllEle.SelectedValue.ToString()))
                {
                    MessageBox.Show("Sélectionnez un élément de la liste");
                }
                else
                {
                    eTemp = AllElements.ElementByID(lstAllEle.SelectedValue.ToString());
                    clsGlobals.ListTemp.AddNotExist(eTemp);
                    AllElements.RemoveItem(eTemp);
                    LinkLists();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lstSelectedEle_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                clsElement eTemp = new clsElement();
                string index = "";

                if (lstSelectedEle.SelectedValue == null || String.IsNullOrEmpty(lstSelectedEle.SelectedValue.ToString()))
                {
                    MessageBox.Show("Sélectionnez un élément de la liste");
                }
                else
                {
                    index = lstSelectedEle.SelectedValue.ToString();
                    eTemp = clsGlobals.ListTemp.ElementByID(index);
                    AllElements.AddNotExist(eTemp);
                    clsGlobals.ListTemp.RemoveItem(eTemp);
                    LinkLists();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}

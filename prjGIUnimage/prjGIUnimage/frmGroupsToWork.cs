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
                //dgvAllEle.DataSource = AllElements.Elements;
                //dgvAllEle.AutoResizeColumns();
                //dgvAllEle.Columns[0].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            clsElement eTemp = new clsElement();
            //if (String.IsNullOrEmpty(dgvAllEle.CurrentRow.Cells[0].Value.ToString()))
            if (lstAllEle.SelectedValue == null || String.IsNullOrEmpty(lstAllEle.SelectedValue.ToString()))
            {
                MessageBox.Show("Sélectionnez un élément de la liste");
            }
            else
            {
                //eTemp = AllElements.ElementByID(dgvAllEle.CurrentRow.Cells[0].Value.ToString());
                eTemp = AllElements.ElementByID(lstAllEle.SelectedValue.ToString());
                clsGlobals.ListTemp.AddNotExist(eTemp);
                AllElements.RemoveItem(eTemp);
                LinkLists();
            }
        }

        private void LinkLists()
        {
            lstSelectedEle.DataSource = null;
            lstSelectedEle.DataSource = clsGlobals.ListTemp.Elements;
            lstSelectedEle.DisplayMember = "Full";
            lstSelectedEle.ValueMember = "ElementID";
            //dgvSelectedEle.Columns[0].Visible = false;

            lstAllEle.DataSource = null;
            lstAllEle.DataSource = AllElements.Elements;
            lstAllEle.DisplayMember = "Full";
            lstAllEle.ValueMember = "ElementID";
            //dgvAllEle.Columns[0].Visible = false;

            lstSelectedEle.Focus();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (AllElements.Quantity > 0)
            {
                clsGlobals.ListTemp = AllElements;
                AllElements = new clsListElements();
                LinkLists();
            }
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AllElements.GetElementsGlobalRequest();
            clsGlobals.ListTemp = new clsListElements();
            LinkLists();
        }

        private void button2_Click(object sender, EventArgs e)
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

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string myText = txtSearch.Text.Trim().ToUpper();

            AllElements.GetElementsGlobalRequest();
            //AllElements.FilterGroupsToWork(myText);
            AllElements.FilterElements(myText);
            lstAllEle.DataSource = AllElements.Elements;

            //clsListElements tmp = new clsListElements();
            //if (string.IsNullOrEmpty(txtSearch.Text))
            //{
            //    AllElements.GetElementsGlobalRequest();
            //    lstAllEle.DataSource = AllElements.Elements;
            //}
            //else
            //{
            //    foreach (clsElement ele in AllElements.Elements)
            //    {
            //        if (ele.Code.ToUpper().Contains(txtSearch.Text.ToUpper()))
            //        {
            //            tmp.Elements.Add(ele);
            //        }
            //    }
            //    lstAllEle.DataSource = tmp.Elements;
            //    AllElements = tmp;
            //}
        }

        private void lstAllEle_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void lstAllEle_DoubleClick(object sender, EventArgs e)
        {
            clsElement eTemp = new clsElement();
            //if (String.IsNullOrEmpty(dgvAllEle.CurrentRow.Cells[0].Value.ToString()))
            if (lstAllEle.SelectedValue == null || String.IsNullOrEmpty(lstAllEle.SelectedValue.ToString()))
            {
                MessageBox.Show("Sélectionnez un élément de la liste");
            }
            else
            {
                //eTemp = AllElements.ElementByID(dgvAllEle.CurrentRow.Cells[0].Value.ToString());
                eTemp = AllElements.ElementByID(lstAllEle.SelectedValue.ToString());
                clsGlobals.ListTemp.AddNotExist(eTemp);
                AllElements.RemoveItem(eTemp);
                LinkLists();
            }
        }

        private void lstSelectedEle_DoubleClick(object sender, EventArgs e)
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

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}

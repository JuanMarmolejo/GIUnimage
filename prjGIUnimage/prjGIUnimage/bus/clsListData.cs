using prjGIUnimage.data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjGIUnimage.bus
{
    class clsListData
    {
        List<clsData> myList;

        public clsListData()
        {
            this.myList = new List<clsData>();
        }

        public int Quantity
        {
            get => myList.Count;
        }

        public List<clsData> Elements
        {
            get => myList;
            set => myList = value;
        }

        private List<clsData> CopyDataTable(DataTable myTb)
        {
            List<clsData> lstTemp = new List<clsData>();
            foreach (DataRow rw in myTb.Rows)
            {
                clsData myDat = new clsData();
                myDat.CopyDatarow(rw);
                lstTemp.Add(myDat);
            }
            return lstTemp;
        }

        public void DataByGroup(int datagroupID)
        {
            string sql = "SELECT * FROM " + clsGlobals.Gesin + "tblGIData " +
                "WHERE datagroupID = "+ datagroupID + " " +
                "ORDER BY[DataSort]";
            
            //Conexion.StartSession();
            DataTable myTb = Conexion.GDatos.GetDataTableSql(sql);
            Conexion.EndSession();
            Elements = CopyDataTable(myTb);
        }

        internal void RemoveLastItem()
        {
            Elements.RemoveAt(Elements.Count - 1);
        }
    }
}

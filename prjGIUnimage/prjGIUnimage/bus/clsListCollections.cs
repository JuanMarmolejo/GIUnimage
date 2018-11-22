using prjGIUnimage.data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjGIUnimage.bus
{
    class clsListCollections
    {
        List<clsCollection> myList;

        public clsListCollections()
        {
            this.myList = new List<clsCollection>();
        }

        public int Quantity
        {
            get => myList.Count;
        }

        public List<clsCollection> Elements
        {
            get => myList;
            set => myList = value;
        }

        public void AllCollections()
        {
            DataTable myTb = new DataTable();
            Conexion.StartSession();
            myTb = Conexion.GDatos.BringDataTableSql("select * from " + clsGlobals.Gesin + "tblGICollection where GICollectionStatus != 9 order by [CollectionID]");
            Elements = CopyDataTable(myTb);
            Conexion.EndSession();
        }

        private List<clsCollection> CopyDataTable(DataTable myTb)
        {
            List<clsCollection> lstTem = new List<clsCollection>();
            foreach (DataRow rw in myTb.Rows)
            {
                clsCollection myCol = new clsCollection();
                myCol.CopyDatarow(rw);
                lstTem.Add(myCol);
            }
            return lstTem;
        }

        internal void AllSXCollections()
        {
            string sql = "SELECT [CollectionID], col.[DivisionID], [CollectionCode], [CollectionStatus], div.[DivisionCode], [CollectionName], " +
                "[CollectionDesc]FROM " + clsGlobals.Silex + "[tblSXCollection] AS col INNER JOIN " + clsGlobals.Silex + "[tblSXDivision] AS div ON col.DivisionID = div.DivisionID " +
                "WHERE[CollectionStatus] != 9";
            Conexion.StartSession();
            DataTable myTb = Conexion.GDatos.BringDataTableSql(sql);
            Elements = CopySXDataTable(myTb);
            Conexion.EndSession();
        }

        private List<clsCollection> CopySXDataTable(DataTable myTb)
        {
            List<clsCollection> lstTem = new List<clsCollection>();
            foreach (DataRow rw in myTb.Rows)
            {
                clsCollection myCol = new clsCollection();
                myCol.CopySXDatarow(rw);
                lstTem.Add(myCol);
            }
            return lstTem;
        }

        internal bool Exists(clsCollection ele)
        {
            return Elements.Contains(ele);
        }

        public clsCollection CollectionByID(int current)
        {
            foreach (clsCollection it in myList)
            {
                if (it.GICollectionID == current)
                {
                    return it;
                }
            }
            return new clsCollection();
        }

        internal void GetActiveElements()
        {
            string sql = "SELECT * FROM " + clsGlobals.Gesin + "[tblGICollection] WHERE [GICollectionStatus]!=1";
            Conexion.StartSession();
            DataTable myTb = Conexion.GDatos.BringDataTableSql(sql);
            Conexion.EndSession();
            Elements = CopyDataTable(myTb);
        }
    }
}

using prjGIUnimage.data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjGIUnimage.bus
{
    class clsListProductEqui
    {
        List<clsProductEqui> myList;

        public clsListProductEqui()
        {
            this.myList = new List<clsProductEqui>();
        }

        public int Quantity
        {
            get => myList.Count;
        }

        public List<clsProductEqui> Elements
        {
            get => myList;
            set => myList = value;
        }

        internal void GetAllEquivalentProducts()
        {
            string sql = "SELECT * FROM " + clsGlobals.Gesin + "[tblGIProductEqui] WHERE [ProductEquiStatus]!=9";
            DataTable myTb = new DataTable();
            Conexion.StartSession();
            myTb = Conexion.GDatos.BringDataTableSql(sql);
            Elements = CopyDataTable(myTb);
            Conexion.EndSession();
        }

        private List<clsProductEqui> CopyDataTable(DataTable myTb)
        {
            List<clsProductEqui> lstTem = new List<clsProductEqui>();
            foreach (DataRow rw in myTb.Rows)
            {
                clsProductEqui myEle = new clsProductEqui();
                myEle.CopyDataRow(rw);
                lstTem.Add(myEle);
            }
            return lstTem;
        }

        internal clsProductEqui GetEquiProductByID(int vEleID)
        {
            foreach (clsProductEqui ele in Elements)
            {
                if (ele.GIProductEquiID == vEleID)
                {
                    return ele;
                }
            }
            return new clsProductEqui();
        }
    }
}

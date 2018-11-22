using prjGIUnimage.data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjGIUnimage.bus
{
    class clsListUser
    {
        List<clsUser> myList;

        public clsListUser()
        {
            this.myList = new List<clsUser>();
        }

        public int Quantity
        {
            get => myList.Count;
        }

        public List<clsUser> Elements
        {
            get => myList;
            set => myList = value;
        }

        public void AllUsers()
        {
            DataTable myTb = new DataTable();
            Conexion.StartSession();
            myTb = Conexion.GDatos.BringDataTableSql("SELECT [UserID],[FirstName],[LastName],[Username],[Password],[UserStatus] " +
                "FROM " + clsGlobals.Silex + "[tblSXUser] WHERE [UserStatus]=0 AND [Password] IS NOT NULL ORDER BY [Username]");
            Elements = CopyDataTable(myTb);
            Conexion.EndSession();
        }

        internal clsUser UserByID(int UserID)
        {
            foreach(clsUser usr in Elements)
            {
                if (usr.UserID == UserID)
                {
                    return usr;
                }
            }
            return new clsUser();
        }

        private List<clsUser> CopyDataTable(DataTable myTb)
        {
            List<clsUser> lstTem = new List<clsUser>();
            foreach (DataRow rw in myTb.Rows)
            {
                clsUser myDiv = new clsUser();
                myDiv.CopyDatarow(rw);
                lstTem.Add(myDiv);
            }
            return lstTem;
        }
    }
}

using prjGIUnimage.data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjGIUnimage
{
    class clsUser
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int UserStatus { get; set; }

        public void CopyDatarow(DataRow rw)
        {
            this.FirstName = Convert.ToString(rw["FirstName"]);
            this.LastName = Convert.ToString(rw["LastName"]);
            this.Password = Convert.ToString(rw["Password"]);
            this.UserID = Convert.ToInt32(rw["UserID"]);
            this.Username = Convert.ToString(rw["Username"]);
            this.UserStatus = Convert.ToInt32(rw["UserStatus"]);
        }

        internal static string GetUserName(int UserID)
        {
            if (UserID == 0)
            {
                return "Admin";
            }
            else
            {
                string sql = "SELECT[FirstName]FROM " + clsGlobals.Silex + "[tblSXUser]WHERE[UserID]=" + UserID;
                //Conexion.StartSession();
                string UserName = (string)Conexion.GDatos.GetScalarValueSql(sql);
                Conexion.EndSession();
                return UserName;
            }
        }
    }
}

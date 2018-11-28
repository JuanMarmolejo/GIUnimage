using prjGIUnimage.data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjGIUnimage.bus
{
    class clsSeason
    {
        public int GISeasonID { get; set; }
        public int SXSeasonID { get; set; }
        public int SXSeasonPrecID { get; set; }
        public string SeasonName { get; set; }
        public string SeasonPrecName { get; set; }
        public int CreatedByUserID { get; set; }
        public int ModifiedByUserID { get; set; }
        public int DeletedByUserID { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime DeletedDate { get; set; }

        internal void CopyDatarow(DataRow rw)
        {
            this.GISeasonID = Convert.ToInt32(rw["GISeasonID"]);
            this.SXSeasonID = Convert.ToInt32(rw["SXSeasonID"]);
            this.SXSeasonPrecID = Convert.ToInt32(rw["SXSeasonPrecID"]);
            this.CreatedByUserID = String.IsNullOrEmpty(rw["CreatedByUserID"].ToString()) ? 0 : Convert.ToInt32(rw["CreatedByUserID"]);
            this.ModifiedByUserID = String.IsNullOrEmpty(rw["ModifiedByUserID"].ToString()) ? 0 : Convert.ToInt32(rw["ModifiedByUserID"]);
            this.DeletedByUserID = String.IsNullOrEmpty(rw["DeletedByUserID"].ToString()) ? 0 : Convert.ToInt32(rw["DeletedByUserID"]);
            this.CreatedDate = String.IsNullOrEmpty(rw["CreatedDate"].ToString()) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(rw["CreatedDate"]);
            this.ModifiedDate = String.IsNullOrEmpty(rw["ModifiedDate"].ToString()) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(rw["ModifiedDate"]);
            this.DeletedDate = String.IsNullOrEmpty(rw["DeletedDate"].ToString()) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(rw["DeletedDate"]);
        }

        internal void GetSeasonByID(int gISeasonID)
        {
            string sql = "SELECT * FROM " + clsGlobals.Gesin + "[tblGISeason]WHERE[GISeasonID]=" + gISeasonID;
            //Conexion.StartSession();
            DataTable myTb = Conexion.GDatos.GetDataTableSql(sql);
            Conexion.EndSession();
            try
            {
                CopyDatarow(myTb.Rows[0]);
            }
            catch
            {
                throw new Exception("Aucune donnée disponible");
            }
        }

        internal static void UpdateGeneratedStatus(bool flag, int gISeasonID)
        {
            string sql = "";
            if (flag)
            {
                //tblGIInvBeforeFiscal
                sql = "UPDATE " + clsGlobals.Gesin + "[tblGISeason] " +
                    "SET[StatusBeforeFiscal] = 1, [ModifiedByUserID] = " + clsGlobals.GIPar.UserID + ",[ModifiedDate]=GETDATE() " +
                    "WHERE[GISeasonID]=" + gISeasonID;
            }
            else
            {
                //tblGIInvAfterFiscal
                sql = "UPDATE " + clsGlobals.Gesin + "[tblGISeason] " +
                    "SET[StatusAfterFiscal] = 1, [ModifiedByUserID] = " + clsGlobals.GIPar.UserID + ",[ModifiedDate]=GETDATE() " +
                    "WHERE[GISeasonID]=" + gISeasonID;
            }
            //Conexion.StartSession();
            Conexion.GDatos.RunSql(sql);
            Conexion.EndSession();
        }

        internal int NumberSeasons(clsSeason seasonPeque)
        {
            if (this.SXSeasonID == seasonPeque.SXSeasonID)
            {
                return 2;
            }
            if (this.SXSeasonPrecID == seasonPeque.SXSeasonID)
            {
                return 1;
            }
            return 0;
        }
    }
}

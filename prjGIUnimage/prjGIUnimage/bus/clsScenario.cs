using prjGIUnimage.data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjGIUnimage.bus
{
    class clsScenario
    {
        public int GIScenarioID { get; set; }
        public string ScenarioCode { get; set; }
        public string ScenarioDesc { get; set; }
        public int GISeasonID { get; set; }
        public int SalesEstimateID { get; set; }
        public int ScenarioStatus { get; set; }
        public double SurplusRateUnique { get; set; }
        public double SurplusRateCommon { get; set; }
        public double SurplusRateIdentified { get; set; }
        public double SurplusRateOS { get; set; }
        public string ReferenceNo1 { get; set; }
        public string ReferenceNo2 { get; set; }
        public DateTime ExpShippingDate { get; set; }
        public DateTime ExpArrivalDate { get; set; }
        public int VendorID { get; set; }
        public int VendorSiteID { get; set; }
        public int PurchaseTypeID { get; set; }
        public int DefaultWarehouseID { get; set; }
        public int DivisionID { get; set; }
        public int CollectionID { get; set; }
        public int VOSeasonID { get; set; }
        public string VONote { get; set; }
        public string VOMessage { get; set; }
        public int CreatedByUserID { get; set; }
        public int ModifiedByUserID { get; set; }
        public int DeletedByUserID { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime DeletedDate { get; set; }

        public clsScenario()
        {
        }

        internal void CopyDatarow(DataRow rw)
        {
            this.GIScenarioID = Convert.ToInt32(rw["GIScenarioID"]);
            this.SalesEstimateID = String.IsNullOrEmpty(rw["SalesEstimateID"].ToString()) ? 0 : Convert.ToInt32(rw["SalesEstimateID"]);
            this.ScenarioCode = rw["ScenarioCode"].ToString();
            this.ScenarioDesc = Convert.ToString(rw["ScenarioDesc"]);
            this.GISeasonID = String.IsNullOrEmpty(rw["GISeasonID"].ToString()) ? 0 : Convert.ToInt32(rw["GISeasonID"]);
            this.ScenarioStatus = Convert.ToInt32(rw["ScenarioStatus"]);
            //this.SurplusRate = Convert.ToInt32(rw["SurplusRate"]);
            this.SurplusRateUnique = Convert.ToInt32(rw["SurplusRateUnique"]);
            this.SurplusRateCommon = Convert.ToInt32(rw["SurplusRateCommon"]);
            this.SurplusRateIdentified = Convert.ToInt32(rw["SurplusRateIdentified"]);
            this.SurplusRateOS = Convert.ToInt32(rw["SurplusRateOS"]);
            this.ReferenceNo1 = Convert.ToString(rw["ReferenceNo1"]);
            this.ReferenceNo2 = Convert.ToString(rw["ReferenceNo2"]);
            this.ExpShippingDate = Convert.ToDateTime(rw["ExpShippingDate"]);
            this.ExpArrivalDate = Convert.ToDateTime(rw["ExpArrivalDate"]);
            this.VendorID = Convert.ToInt32(rw["VendorID"]);
            this.VendorSiteID = Convert.ToInt32(rw["VendorSiteID"]);
            this.PurchaseTypeID = Convert.ToInt32(rw["PurchaseTypeID"]);
            this.DefaultWarehouseID = Convert.ToInt32(rw["DefaultWarehouseID"]);
            this.DivisionID = Convert.ToInt32(rw["DivisionID"]);
            this.CollectionID = Convert.ToInt32(rw["CollectionID"]);
            this.VOSeasonID = Convert.ToInt32(rw["VOSeasonID"]);
            this.VONote = Convert.ToString(rw["VONote"]);
            this.VOMessage = Convert.ToString(rw["VOMessage"]);
            this.CreatedByUserID = String.IsNullOrEmpty(rw["CreatedByUserID"].ToString()) ? 0 : Convert.ToInt32(rw["CreatedByUserID"]);
            this.ModifiedByUserID = String.IsNullOrEmpty(rw["ModifiedByUserID"].ToString()) ? 0 : Convert.ToInt32(rw["ModifiedByUserID"]);
            this.DeletedByUserID = String.IsNullOrEmpty(rw["DeletedByUserID"].ToString()) ? 0 : Convert.ToInt32(rw["DeletedByUserID"]);
            this.CreatedDate = String.IsNullOrEmpty(rw["CreatedDate"].ToString()) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(rw["CreatedDate"]);
            this.ModifiedDate = String.IsNullOrEmpty(rw["ModifiedDate"].ToString()) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(rw["ModifiedDate"]);
            this.DeletedDate = String.IsNullOrEmpty(rw["DeletedDate"].ToString()) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(rw["DeletedDate"]);
        }

        internal void GetScenarioByID(int activeScenario)
        {
            string sql = "SELECT * FROM " + clsGlobals.Gesin + "[tblGIScenario] WHERE [GIScenarioID]=" + activeScenario;
            Conexion.StartSession();
            DataTable myTb = Conexion.GDatos.BringDataTableSql(sql);
            Conexion.EndSession();
            CopyDatarow(myTb.Rows[0]);
        }

        internal void InsertScenario()
        {
            Conexion.StartSession();
            string sql = "INSERT INTO " + clsGlobals.Gesin + "[tblGIScenario] ( " +
                "[ScenarioCode] " +
                ",[ScenarioDesc] " +
                ",[GISeasonID] " +
                ",[ScenarioStatus] " +
                ",[SurplusRateUnique] " +
                ",[SurplusRateCommon] " +
                ",[SurplusRateIdentified] " +
                ",[SurplusRateOS] " +
                ",[ReferenceNo1] " +
                ",[ReferenceNo2] " +
                ",[ExpShippingDate] " +
                ",[ExpArrivalDate] " +
                ",[VendorID] " +
                ",[VendorSiteID] " +
                ",[PurchaseTypeID] " +
                ",[DefaultWarehouseID] " +
                ",[DivisionID] " +
                ",[CollectionID] " +
                ",[VOSeasonID] " +
                ",[VONote] " +
                ",[VOMessage] " +
                ",[CreatedByUserID] " +
                ",[CreatedDate] " +
                ") " +
                "VALUES( '" +
                this.ScenarioCode + "', '" +
                this.ScenarioDesc + "', " +
                this.GISeasonID + ", " +
                this.ScenarioStatus + ", " +
                clsGlobals.GIPar.SurplusRateUnique + ", " +
                clsGlobals.GIPar.SurplusRateCommon + ", " +
                clsGlobals.GIPar.SurplusRateIdentified + ", " +
                clsGlobals.GIPar.SurplusRateOS + ", '" +
                clsGlobals.GIPar.ReferenceNo1 + "', '" +
                clsGlobals.GIPar.ReferenceNo2 + "', '" +
                clsGlobals.GIPar.ExpShippingDate.ToString("yyyy-MM-dd") + "', '" +
                clsGlobals.GIPar.ExpArrivalDate.ToString("yyyy-MM-dd") + "', " +
                clsGlobals.GIPar.VendorID + ", " +
                clsGlobals.GIPar.VendorSiteID + ", " +
                clsGlobals.GIPar.PurchaseTypeID + ", " +
                clsGlobals.GIPar.DefaultWarehouseID + ", " +
                clsGlobals.GIPar.DivisionID + ", " +
                clsGlobals.GIPar.CollectionID + ", " +
                clsGlobals.GIPar.SeasonID + ", '" +
                clsGlobals.GIPar.VONote + "', '" +
                clsGlobals.GIPar.VOMessage + "', " +
                "" + clsGlobals.GIPar.UserID + ", GETDATE()" +
                ")";
            Conexion.GDatos.RunSql(sql);
            Conexion.EndSession();
        }

        internal static void UpdateModifiedByUser(int scenarioID)
        {
            string sql = "UPDATE " + clsGlobals.Gesin + "[tblGIScenario] SET [ModifiedByUserID] = " + clsGlobals.GIPar.UserID + ", [ModifiedDate] = GETDATE() " +
                "WHERE [GIScenarioID]=" + scenarioID;
            Conexion.StartSession();
            Conexion.GDatos.RunSql(sql);
            Conexion.EndSession();
        }

        internal void UpdateScenario()
        {
            //2018-07-19 MODIFIE REQUET
            //string sql = "UPDATE [tblGIScenario] SET [ScenarioStatus] = " + this.ScenarioStatus + ", [ScenarioDesc] = '" + this.ScenarioDesc 
            //    + "', [ModifiedByUserID] = " + clsGlobals.ActiveUser + ", [ModifiedDate] = GETDATE() WHERE [GIScenarioID]=" + clsGlobals.ActiveScenario;
            string sql = "UPDATE " + clsGlobals.Gesin + "[tblGIScenario] SET [ScenarioDesc] = '" + ScenarioDesc + "', [ScenarioStatus] = " + ScenarioStatus +
                ", [ReferenceNo1] = '" + ReferenceNo1 + "', [ReferenceNo2] = '" + ReferenceNo2 + "', [ExpShippingDate] = '" + 
                ExpShippingDate.ToString("yyyy-MM-dd") + "', [ExpArrivalDate] = '" + ExpArrivalDate.ToString("yyyy-MM-dd") + "', [VendorID] = " + 
                VendorID + ", [VendorSiteID] = " + VendorSiteID + ", [PurchaseTypeID] = " + PurchaseTypeID + ", [DivisionID] = " + DivisionID + 
                ", [CollectionID] = " + CollectionID + ", [VOSeasonID] = " + VOSeasonID + ", [VONote] = '" + VONote + "', [VOMessage] = '" + VOMessage +
                "', [ModifiedByUserID] = " + clsGlobals.GIPar.UserID + ", [ModifiedDate] = GETDATE() WHERE [GIScenarioID]=" + clsGlobals.GIPar.ScenarioID;
            Conexion.StartSession();
            Conexion.GDatos.RunSql(sql);
            Conexion.EndSession();
        }

        internal static int NextScenarioID()
        {
            int next = new int();
            string sql = "SELECT MAX([GIScenarioID]) FROM " + clsGlobals.Gesin + "[tblGIScenario]";
            Conexion.StartSession();
            try
            {
                next = Convert.ToInt32(Conexion.GDatos.BringScalarValueSql(sql)) + 1;
            }
            catch
            {
                next = 100;
            }
            Conexion.EndSession();
            return next;
        }

        internal bool Exists()
        {
            string sql = clsGlobals.useSilex + " SELECT * FROM " + clsGlobals.Gesin + "[tblGIScenario] WHERE[GISeasonID] = " + this.GISeasonID +
                " AND[ScenarioStatus] = 2 AND[ScenarioCode] = '" + this.ScenarioCode + "'";
            Conexion.StartSession();
            DataTable myTb = Conexion.GDatos.BringDataTableSql(sql);
            Conexion.EndSession();
            if (myTb.Rows.Count > 0)
            {
                DataRow rw = myTb.Rows[0];
                this.GIScenarioID = Convert.ToInt32(rw["GIScenarioID"]);
                return true;
            }
            else
            {
                return false;
            }
        }

        internal void InsertScenarioFiscal()
        {
            string sql = "INSERT INTO " + clsGlobals.Gesin + "[tblGIScenario] ([ScenarioCode],[GISeasonID],[ScenarioStatus],[CreatedByUserID],[CreatedDate])VALUES('" + 
                this.ScenarioCode + "'," + this.GISeasonID + "," + this.ScenarioStatus + "," + clsGlobals.GIPar.UserID + ",GETDATE())";
            Conexion.StartSession();
            Conexion.GDatos.RunSql(sql);
            Conexion.EndSession();
        }

        internal int GetIDBeforeFiscal()
        {
            string sql = "SELECT MAX([GIScenarioID]) FROM " + clsGlobals.Gesin + "[tblGIScenario] WHERE[ScenarioCode] = '" + this.ScenarioCode + 
                "' AND[GISeasonID] = " + this.GISeasonID + " AND[ScenarioStatus] = 2";
            Conexion.StartSession();
            int scenarioID = (int)Conexion.GDatos.BringScalarValueSql(sql);
            Conexion.EndSession();
            return scenarioID;
        }

        internal bool ExistsBeforeFiscal()
        {
            string sql = "SELECT * FROM " + clsGlobals.Gesin + "[tblGIScenario] " +
                "WHERE[ScenarioCode] = 'Avant Fiscal' " +
                "AND [GISeasonID] = " + this.GISeasonID + " " +
                "AND [ScenarioStatus] = 2";
            Conexion.StartSession();
            DataTable myTb = Conexion.GDatos.BringDataTableSql(sql);
            Conexion.EndSession();
            if (myTb.Rows.Count > 0)
            {
                DataRow rw = myTb.Rows[0];
                this.GIScenarioID = Convert.ToInt32(rw["GIScenarioID"]);
                return true;
            }
            else
            {
                return false;
            }
        }

        internal bool ExistsAfterFiscal()
        {
            string sql = "SELECT * FROM " + clsGlobals.Gesin + "[tblGIScenario] " +
                "WHERE[ScenarioCode] = 'Après Fiscal' " +
                "AND [GISeasonID] = " + this.GISeasonID + " " +
                "AND [ScenarioStatus] = 2";
            Conexion.StartSession();
            DataTable myTb = Conexion.GDatos.BringDataTableSql(sql);
            Conexion.EndSession();
            if (myTb.Rows.Count > 0)
            {
                DataRow rw = myTb.Rows[0];
                this.GIScenarioID = Convert.ToInt32(rw["GIScenarioID"]);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

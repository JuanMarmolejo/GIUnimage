using prjGIUnimage.data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjGIUnimage.bus
{
    class clsScProduct
    {
        public int ScProductID { get; set; }
        public int ScenarioID { get; set; }
        public int GIProductID { get; set; }
        public int ScProductStatus { get; set; }
        public string ScProductComment { get; set; }
        public int ProductID { get; set; }
        public int ColorID { get; set; }
        public int GIProductStatus { get; set; }
        public string ProductComment { get; set; }
        public int CreatedByUserID { get; set; }
        public int ModifiedByUserID { get; set; }
        public int DeletedByUserID { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime DeletedDate { get; set; }
        public int ProductColorID { get; set; }
        public double SurplusRate { get; set; }
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
        public int VOStatus { get; set; }
        public int VOID { get; set; }
        public string VOCode { get; set; }
        public int VoPdfStatus { get; set; }

        internal void InsertScProduct(clsProduct ele)
        {
            string sql = "INSERT INTO " + clsGlobals.Gesin + "[tblGIScProduct]([ScenarioID],[GIProductID],[ProductColorID],[ProductID],[ColorID],[GIProductStatus]," +
                "[ProductComment],[SurplusRate],[ReferenceNo1],[ReferenceNo2],[ExpShippingDate],[ExpArrivalDate],[VendorID],[VendorSiteID]," +
                "[PurchaseTypeID],[DefaultWarehouseID],[DivisionID],[CollectionID],[VOSeasonID],[VONote],[VOMessage],[VOStatus],[VoPdfStatus]," +
                "[CreatedByUserID],[CreatedDate])VALUES(" + ScenarioID + "," + ele.GIProductID + "," + ele.ProductColorID + "," + ele.ProductID +
                "," + ele.ColorID + "," + ele.GIProductStatus + ",'" + ele.ProductComment + "'," + ele.SurplusRate + ",'" + clsGlobals.GIPar.ReferenceNo1 +
                "','" + clsGlobals.GIPar.ReferenceNo2 + "','" + clsGlobals.GIPar.ExpShippingDate.ToString("yyyy-MM-dd") + "','" +
                clsGlobals.GIPar.ExpArrivalDate.ToString("yyyy-MM-dd") + "'," + clsGlobals.GIPar.VendorID + "," + clsGlobals.GIPar.VendorSiteID +
                "," + clsGlobals.GIPar.PurchaseTypeID + "," + clsGlobals.GIPar.DefaultWarehouseID + "," + clsGlobals.GIPar.DivisionID + ","
                + clsGlobals.GIPar.CollectionID + "," + clsGlobals.GIPar.SeasonID + ",'" + clsGlobals.GIPar.VONote + "','" + clsGlobals.GIPar.VOMessage +
                "',0,0," + clsGlobals.GIPar.UserID + ",GETDATE())";
            //Conexion.StartSession();
            Conexion.GDatos.RunSql(sql);
            Conexion.EndSession();
        }

        internal void GetScProduct(int activeScenario, int activeProduct)
        {
            string sql = "SELECT [ScProductID],[ScenarioID],[GIProductID],[ProductColorID],[ScProductStatus],[ScProductComment],[ProductID]," +
                "[ColorID],[GIProductStatus],[ProductComment],[SurplusRate],[ReferenceNo1],[ReferenceNo2],[ExpShippingDate],[ExpArrivalDate]," +
                "[VendorID],[VendorSiteID],[PurchaseTypeID],[DefaultWarehouseID],[DivisionID],[CollectionID],[VOSeasonID],[VONote],[VOMessage]," +
                "[VOStatus],[VOID],[VOCode],[VoPdfStatus],[CreatedByUserID],[ModifiedByUserID],[DeletedByUserID],[CreatedDate],[ModifiedDate]," +
                "[DeletedDate]FROM " + clsGlobals.Gesin + "[tblGIScProduct]WHERE [ProductColorID]=" + activeProduct + " AND [ScenarioID]=" + 
                activeScenario;
            
            //Conexion.StartSession();
            DataTable myTb = Conexion.GDatos.GetDataTableSql(sql);
            Conexion.EndSession();
            DataRow rw = myTb.Rows[0];

            this.CollectionID = Convert.ToInt32(rw["CollectionID"]);
            this.ColorID = Convert.ToInt32(rw["ColorID"]);
            this.CreatedByUserID = Convert.ToInt32(rw["CreatedByUserID"]);
            this.CreatedDate = String.IsNullOrEmpty(rw["CreatedDate"].ToString()) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(rw["CreatedDate"]);
            this.DefaultWarehouseID = Convert.ToInt32(rw["DefaultWarehouseID"]);
            this.DeletedByUserID = String.IsNullOrEmpty(rw["DeletedByUserID"].ToString()) ? 0 : Convert.ToInt32(rw["DeletedByUserID"]);
            this.DeletedDate = String.IsNullOrEmpty(rw["DeletedDate"].ToString()) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(rw["DeletedDate"]);
            this.DivisionID = Convert.ToInt32(rw["DivisionID"]);
            this.ExpArrivalDate = String.IsNullOrEmpty(rw["ExpArrivalDate"].ToString()) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(rw["ExpArrivalDate"]);
            this.ExpShippingDate = String.IsNullOrEmpty(rw["ExpShippingDate"].ToString()) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(rw["ExpShippingDate"]);
            this.GIProductID = Convert.ToInt32(rw["GIProductID"]);
            this.GIProductStatus = Convert.ToInt32(rw["GIProductStatus"]);
            this.ModifiedByUserID = String.IsNullOrEmpty(rw["ModifiedByUserID"].ToString()) ? 0 : Convert.ToInt32(rw["ModifiedByUserID"]);
            this.ModifiedDate = String.IsNullOrEmpty(rw["ModifiedDate"].ToString()) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(rw["ModifiedDate"]);
            this.ProductColorID = Convert.ToInt32(rw["ProductColorID"]);
            this.ProductComment = Convert.ToString(rw["ProductComment"]);
            this.ProductID = Convert.ToInt32(rw["ProductID"]);
            this.PurchaseTypeID = Convert.ToInt32(rw["PurchaseTypeID"]);
            this.ReferenceNo1 = Convert.ToString(rw["ReferenceNo1"]);
            this.ReferenceNo2 = Convert.ToString(rw["ReferenceNo2"]);
            this.ScenarioID = Convert.ToInt32(rw["ScenarioID"]);
            this.ScProductComment = Convert.ToString(rw["ScProductComment"]);
            this.ScProductID = Convert.ToInt32(rw["ScProductID"]);
            this.ScProductStatus = String.IsNullOrEmpty(rw["ScProductStatus"].ToString()) ? 0 : Convert.ToInt32(rw["ScProductStatus"]);
            this.SurplusRate = Convert.ToInt32(rw["SurplusRate"]);
            this.VendorID = Convert.ToInt32(rw["VendorID"]);
            this.VendorSiteID = Convert.ToInt32(rw["VendorSiteID"]);
            this.VOCode = Convert.ToString(rw["VOCode"]);
            this.VOID = String.IsNullOrEmpty(rw["VOID"].ToString()) ? 0 : Convert.ToInt32(rw["VOID"]);
            this.VOMessage = Convert.ToString(rw["VOMessage"]);
            this.VONote = Convert.ToString(rw["VONote"]);
            this.VoPdfStatus = String.IsNullOrEmpty(rw["VoPdfStatus"].ToString()) ? 0 : Convert.ToInt32(rw["VoPdfStatus"]);
            this.VOSeasonID = String.IsNullOrEmpty(rw["VOSeasonID"].ToString()) ? 0 : Convert.ToInt32(rw["VOSeasonID"]);
            this.VOStatus = String.IsNullOrEmpty(rw["VOStatus"].ToString()) ? 0 : Convert.ToInt32(rw["VOStatus"]);
        }

        internal string GetGIProducStatus()
        {
            string sql = "SELECT [DataDesc_fra]FROM " + clsGlobals.Gesin + "[tblGIData]WHERE DataGroupID=101 AND[DataValue]=" + GIProductStatus;
            //Conexion.StartSession();
            string vRes = Conexion.GDatos.GetScalarValueSql(sql).ToString();
            Conexion.EndSession();
            return vRes;
        }

        internal string SXGetStatus()
        {
            clsSilex.UpDateProductColor();
            return clsSilex.GetStatus(ProductColorID);
        }

        internal string GetColorName()
        {
            clsSilex.UpDateColor();
            return clsSilex.GetColorName(ColorID);
        }

        internal string GetColorCode()
        {
            clsSilex.UpDateColor();
            return clsSilex.GetColorCode(ColorID);
        }

        internal string GetProductCode()
        {
            clsSilex.UpDateProduct();
            return clsSilex.GetProductCode(ProductID);
        }

        internal string GetProductDesc()
        {
            clsSilex.UpDateProduct();
            return clsSilex.GetProductDesc(ProductID);
        }

        internal void UpdateScProduct()
        {
            string sql = "UPDATE " + clsGlobals.Gesin + "[tblGIScProduct] SET " +
                "[GIProductID] = " + GIProductID +
                ",[ScProductStatus]= " + ScProductStatus +
                ",[ScProductComment]= '" + ScProductComment +
                "',[ProductID]= " + ProductID +
                ",[ColorID]= " + ColorID +
                ",[GIProductStatus]= " + GIProductStatus +
                ",[ProductComment]= '" + ProductComment +
                "',[SurplusRate]= " + SurplusRate +
                ",[ReferenceNo1]= '" + ReferenceNo1 +
                "',[ReferenceNo2]= '" + ReferenceNo2 +
                "',[ExpShippingDate]= '" + ExpShippingDate.ToString("yyyy-MM-dd") +
                "',[ExpArrivalDate]= '" + ExpArrivalDate.ToString("yyyy-MM-dd") +
                "',[VendorID]= " + VendorID +
                ",[VendorSiteID]= " + VendorSiteID +
                ",[PurchaseTypeID]= " + PurchaseTypeID +
                ",[DefaultWarehouseID]= " + DefaultWarehouseID +
                ",[DivisionID]= " + DivisionID +
                ",[CollectionID]= " + CollectionID +
                ",[VOSeasonID]= " + VOSeasonID +
                ",[VONote]= '" + VONote +
                "',[VOMessage]= '" + VOMessage +
                "',[VOStatus]= " + VOStatus +
                ",[VOID]= " + VOID +
                ",[VOCode]= '" + VOCode +
                "',[VoPdfStatus]= " + VoPdfStatus +
                ",[ModifiedByUserID]= " + clsGlobals.GIPar.UserID +
                ",[ModifiedDate]= GETDATE() WHERE[ScenarioID]= " + ScenarioID + " AND[ProductColorID]= " + ProductColorID;
            //Conexion.StartSession();
            Conexion.GDatos.RunSql(sql);
            Conexion.EndSession();
        }

        internal static int UpdateVOStatus(clsScVorderDetail tmp)
        {
            string sql = "UPDATE " + clsGlobals.Gesin + "[tblGIScProduct] " +
                "SET [VOStatus] = 1 " +
                "WHERE [ScenarioID]=" + clsGlobals.GIPar.ScenarioID + " " +
                "AND [ProductID]=" + tmp.ProductID + " " +
                "AND [ProductColorID]=" + tmp.ProductColorID + " " +
                "AND [ColorID]=" + tmp.ColorID;
            //Conexion.StartSession();
            Conexion.GDatos.RunSql(sql);
            sql = "SELECT [ScProductID] " +
                "FROM " + clsGlobals.Gesin + "[tblGIScProduct]" +
                "WHERE [ScenarioID]=" + clsGlobals.GIPar.ScenarioID + " " +
                "AND [ProductID]=" + tmp.ProductID + " " +
                "AND [ProductColorID]=" + tmp.ProductColorID + " " +
                "AND [ColorID]=" + tmp.ColorID;
            int pdtID = (int)Conexion.GDatos.GetScalarValueSql(sql);
            Conexion.EndSession();
            return pdtID;
        }

        internal static void UpdateVoCode(string vOCode, int scProductID)
        {
            string sql = "UPDATE " + clsGlobals.Gesin + "[tblGIScProduct] " +
                "SET[VOCode] = '" + vOCode + "' " +
                "WHERE[ScProductID] = " + scProductID;
            //Conexion.StartSession();
            Conexion.GDatos.RunSql(sql);
            Conexion.EndSession();
        }

        internal bool VorderExists()
        {
            string sql = "SELECT * FROM " + clsGlobals.Gesin + "[tblGIScProduct] " +
                "WHERE[ProductColorID] = " + this.ProductColorID + " " +
                "AND [ScenarioID] = " + this.ScenarioID + " " +
                "AND [VOStatus] = 1";
            //Conexion.StartSession();
            DataTable myTb = Conexion.GDatos.GetDataTableSql(sql);
            Conexion.EndSession();
            if (myTb.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        internal void UpdateVOStatus(int vOStatus)
        {
            string sql = "UPDATE " + clsGlobals.Gesin + "[tblGIScProduct] " +
                "SET[VOStatus] = " + vOStatus + ", [ModifiedByUserID]=" + clsGlobals.GIPar.UserID + ", [ModifiedDate]=GETDATE() " +
                "WHERE[ScProductID]=" + this.ScProductID;
            //Conexion.StartSession();
            Conexion.GDatos.RunSql(sql);
            Conexion.EndSession();
            VOStatus = vOStatus;
        }

        internal static void UpdateVoPdfStatus(int scenarioID)
        {
            string sql = "UPDATE " + clsGlobals.Gesin + "[tblGIScProduct] " +
                "SET[VoPdfStatus] = 1 " +
                "WHERE[VOStatus] = 1 AND[VoPdfStatus] = 0 AND[ScenarioID] = " + scenarioID;
            //Conexion.StartSession();
            Conexion.GDatos.RunSql(sql);
            Conexion.EndSession();
        }

        internal bool HasBeenModified()
        {
            string sql = "SELECT [ModifiedDate] FROM " + clsGlobals.Gesin + "[tblGIScProduct] WHERE ScProductID = " + this.ScProductID;
            //Conexion.StartSession();
            object temp = Conexion.GDatos.GetScalarValueSql(sql);
            Conexion.EndSession();
            try
            {
                DateTime Mod = Convert.ToDateTime(temp);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

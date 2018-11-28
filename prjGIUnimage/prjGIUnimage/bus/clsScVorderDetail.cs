using prjGIUnimage.data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjGIUnimage.bus
{
    class clsScVorderDetail
    {
        public int GIVODetailID { get; set; }
        public int GIVOID { get; set; }
        public int ProductID { get; set; }
        public int ProductColorID { get; set; }
        public int ProductDimID { get; set; }
        public int ProductCatID { get; set; }
        public int SeasonID { get; set; }
        public string Dim { get; set; }
        public string Size { get; set; }
        public int SizeOrder { get; set; }
        public int ColorID { get; set; }
        public int DimID { get; set; }
        public int CatID { get; set; }
        public int ProductGroupID { get; set; }
        public int ProductSubGroupID { get; set; }
        public string VODetailDesc { get; set; }
        public string VODetailNote { get; set; }
        public double OrderQty { get; set; }
        public int VODetailStatus { get; set; }
        public int CreatedByUserID { get; set; }
        public int ModifiedByUserID { get; set; }
        public int DeletedByUserID { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime DeletedDate { get; set; }
        public int ProductWarehouseID { get; set; }

        internal void SaveScVorderDetail(int giVOID)
        {
            string sql = "INSERT INTO " + clsGlobals.Gesin + "[tblGIScVorderDetail] ([GIVOID],[ProductID],[ProductColorID],[ProductDimID],[ProductCatID]," +
                "[VODetailDesc],[VODetailNote],[DimID],[CatID],[ProductGroupID],[ProductSubGroupID],[ColorID],[SeasonID],[DimCode],[SizeDesc]," +
                "[SizeOrder],[OrderQty],[VODetailStatus],[DetailOrderTotalQty],[CreatedByUserID],[CreatedDate],[ProductWarehouseID]) VALUES(" +
                giVOID + "," + this.ProductID + "," + this.ProductColorID + "," + this.ProductDimID + "," + this.ProductCatID + ",'" + this.VODetailDesc +
                "','" + this.VODetailNote + "'," + this.DimID + "," + this.CatID + "," + this.ProductGroupID + "," + this.ProductSubGroupID + "," +
                this.ColorID + "," + this.SeasonID + ",'" + this.Dim + "','" + this.Size + "'," + this.SizeOrder + "," + this.OrderQty + "," +
                this.VODetailStatus + "," + this.OrderQty + "," + clsGlobals.GIPar.UserID + ",GETDATE()," + this.ProductWarehouseID + ")";
            //Conexion.StartSession();
            Conexion.GDatos.RunSql(sql);
            Conexion.EndSession();
        }

        internal static int GetProductWarehouseID(int productID, int defaultWarehouseID)
        {
            string sql = "SELECT [ProductWarehouseID] " +
                "FROM " + clsGlobals.Silex + "[tblSXProductWarehouse] " +
                "WHERE[ProductID] = " + productID + " " +
                "AND [WarehouseID] = " + defaultWarehouseID;
            //Conexion.StartSession();
            int PwID = (int)Conexion.GDatos.GetScalarValueSql(sql);
            Conexion.EndSession();
            return PwID;
        }

        private static int GetProductWarehouseID(int productID)
        {
            string sql = "SELECT [ProductWarehouseID] " +
                "FROM " + clsGlobals.Silex + "[tblSXProductWarehouse] " +
                "WHERE[ProductID] = " + productID + " AND [WarehouseID] IN (" +
                "SELECT DISTINCT[DefaultWarehouseID] " +
                "FROM " + clsGlobals.Gesin + "[tblGIScProduct] " +
                "WHERE[ProductID] = " + productID + ") ";
            //Conexion.StartSession();
            int PwID = (int)Conexion.GDatos.GetScalarValueSql(sql);
            Conexion.EndSession();
            return PwID;
        }

        internal void SaveScVorderDetailUnimage(int giVOID)
        {
            this.GetParametersVorderDetail(ProductColorID);

            string sql = "INSERT INTO " + clsGlobals.Gesin + "[tblGIScVorderDetail] ([GIVOID],[ProductID],[ProductColorID],[ProductDimID],[ProductCatID]," +
                "[VODetailDesc],[VODetailNote],[DimID],[CatID],[ProductGroupID],[ProductSubGroupID],[ColorID],[SeasonID],[DimCode],[SizeDesc]," +
                "[SizeOrder],[OrderQty],[VODetailStatus],[DetailOrderTotalQty],[CreatedByUserID],[CreatedDate],[ProductWarehouseID]) VALUES(" +
                giVOID + "," + this.ProductID + "," + this.ProductColorID + "," + this.ProductDimID + "," + this.ProductCatID + ",'" + this.VODetailDesc +
                "','" + this.VODetailNote + "'," + this.DimID + "," + this.CatID + "," + this.ProductGroupID + "," + this.ProductSubGroupID + "," +
                this.ColorID + "," + this.SeasonID + ",'" + this.Dim + "','" + this.Size + "'," + this.SizeOrder + "," + this.OrderQty + "," +
                this.VODetailStatus + "," + this.OrderQty + "," + clsGlobals.GIPar.UserID + ",GETDATE()," + this.ProductWarehouseID + ")";
            //Conexion.StartSession();
            Conexion.GDatos.RunSql(sql);
            Conexion.EndSession();
        }

        private void GetParametersVorderDetail(int productColorID)
        {
            DataTable myTb = new DataTable();
            string sql = "SELECT DISTINCT[CompProductColorID] " +
                "FROM " + clsGlobals.Silex + "[tblSXCostCardComp] AS COST " +
                "INNER JOIN " + clsGlobals.Silex + "[tblSXCostCardDetail] AS DET " +
                "ON COST.CostCardDetailID = DET.CostCardDetailID " +
                "WHERE [ProductColorID]= " + productColorID + " " +
                "AND [CompProductColorID] IS NOT NULL ";
            //Conexion.StartSession();
            productColorID = (int)Conexion.GDatos.GetScalarValueSql(sql);
            sql = "SELECT * FROM " + clsGlobals.Gesin + "[tblGIScSalesHistory] " +
                "WHERE[CatCode] = 'REG' " +
                "AND[ScenarioID] = " + clsGlobals.GIPar.ScenarioID + " " +
                "AND[ProductColorID] = " + productColorID + " ";
            myTb = Conexion.GDatos.GetDataTableSql(sql);
            Conexion.EndSession();
            DataRow myRow = myTb.Rows[0];

            this.CatID = Convert.ToInt32(myRow["CatID"]);
            this.ColorID = Convert.ToInt32(myRow["ColorID"]);
            this.OrderQty = -1 * this.OrderQty;
            this.ProductCatID = Convert.ToInt32(myRow["ProductCatID"]);
            this.ProductColorID = productColorID;
            this.ProductGroupID = Convert.ToInt32(myRow["ProductGroupID"]);
            this.ProductID = Convert.ToInt32(myRow["ProductID"]);
            this.ProductSubGroupID = Convert.ToInt32(myRow["ProductSubGroupID"]);
            this.SeasonID = Convert.ToInt32(myRow["SeasonID"]);
            this.ProductWarehouseID = clsScVorderDetail.GetProductWarehouseID(ProductID);
        }
    }
}

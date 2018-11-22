using prjGIUnimage.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjGIUnimage.bus
{
    class clsProductAvailableOS
    {
        public int ProductAvailableID { get; set; }
        public int ScenarioID { get; set; }
        public int CollectionID { get; set; }
        public int ProductID { get; set; }
        public int ProductColorID { get; set; }
        public int ProductDimID { get; set; }
        public int ProductCatID { get; set; }
        public int SizeOrder { get; set; }
        public string SizeDesc { get; set; }
        public int ColorID { get; set; }
        public int DimID { get; set; }
        public int CatID { get; set; }
        public int ProductGroupID { get; set; }
        public int ProductSubGroupID { get; set; }
        public double QtyAvailable { get; set; }
        public double QtyOrdered { get; set; }
        public int CreatedByUserID { get; set; }
        public int ModifiedByUserID { get; set; }
        public int DeletedByUserID { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime DeletedDate { get; set; }

        internal void UpdateQtyOrdered(int parentProductID)
        {
            string sql = "UPDATE " + clsGlobals.Gesin + "[tblGIProductAvailableOS] " +
                "SET[QtyOrdered] =[QtyOrdered] + " + QtyOrdered + ", [ModifiedByUserID]=" +
                clsGlobals.GIPar.UserID + ", [ModifiedDate]=GETDATE() " +
                "WHERE[ScenarioID] = " + ScenarioID + " AND[ProductColorID] = " + parentProductID + " " +
                "AND[DimID] = " + DimID + " AND[SizeDesc] = '" + SizeDesc + "'";
            Conexion.StartSession();
            Conexion.GDatos.RunSql(sql);
            Conexion.EndSession();
        }

        public clsProductAvailableOS(clsScSalesHistory ele, double available)
        {
            this.CatID = ele.CatID;
            this.CollectionID = ele.CollectionID;
            this.ColorID = ele.ColorID;
            this.DimID = ele.DimID;
            this.ProductCatID = ele.ProductCatID;
            this.ProductColorID = ele.ProductColorID;
            this.ProductDimID = ele.ProductDimID;
            this.ProductGroupID = ele.ProductGroupID;
            this.ProductID = ele.ProductID;
            this.ProductSubGroupID = ele.ProductSubGroupID;
            this.ScenarioID = clsGlobals.GIPar.ScenarioID;
            this.SizeOrder = ele.SizeOrder;
            this.SizeDesc = ele.SizeDesc;
            this.QtyAvailable = available;
        }

        public clsProductAvailableOS(clsScVorderDetail ele, double orderQty)
        {
            this.CatID = ele.CatID;
            this.ColorID = ele.ColorID;
            this.DimID = ele.DimID;
            this.ProductCatID = ele.ProductCatID;
            this.ProductColorID = ele.ProductColorID;
            this.ProductDimID = ele.ProductDimID;
            this.ProductGroupID = ele.ProductGroupID;
            this.ProductID = ele.ProductID;
            this.ProductSubGroupID = ele.ProductSubGroupID;
            this.ScenarioID = clsGlobals.GIPar.ScenarioID;
            this.SizeOrder = ele.SizeOrder;
            this.SizeDesc = ele.Size;
            this.QtyOrdered = orderQty;
        }

        internal void InsertProductAvailableOS()
        {
            string sql = "INSERT INTO " + clsGlobals.Gesin + "[tblGIProductAvailableOS] ([ScenarioID],[CollectionID]," +
                "[ProductID],[ProductColorID],[ProductDimID],[ProductCatID],[SizeOrder],[SizeDesc],[ColorID],[DimID],[CatID]," +
                "[ProductGroupID],[ProductSubGroupID],[QtyAvailable],[QtyOrdered],[CreatedByUserID],[CreatedDate])" +
                "VALUES(" + this.ScenarioID + "," + this.CollectionID + "," + this.ProductID + "," + this.ProductColorID +
                "," + this.ProductDimID + "," + this.ProductCatID + "," + this.SizeOrder + ",'" + this.SizeDesc + "'," + this.ColorID + "," +
                this.DimID + "," + this.CatID + "," + this.ProductGroupID + "," + this.ProductSubGroupID + "," +
                this.QtyAvailable + ", 0," + clsGlobals.GIPar.UserID + ",GETDATE())";
            Conexion.StartSession();
            Conexion.GDatos.RunSql(sql);
            Conexion.EndSession();
        }

        internal static double GetAvailableQuantity(int scenarioID, int parentProductID, int dimID, string sizeDesc)
        {
            string sql = "SELECT ([QtyAvailable]-[QtyOrdered]) " +
                "FROM " + clsGlobals.Gesin + "[tblGIProductAvailableOS] " +
                "WHERE[ScenarioID] = " + scenarioID + " AND [ProductColorID] = " + parentProductID + " " +
                "AND [DimID] = " + dimID + " AND [SizeDesc] = '" + sizeDesc + "'";
            Conexion.StartSession();
            double Qty = (double)Conexion.GDatos.BringScalarValueSql(sql);
            Conexion.EndSession();
            return Qty;
        }
    }
}

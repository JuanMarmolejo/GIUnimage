using prjGIUnimage.data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjGIUnimage.bus
{
    class clsProductEqui
    {
        public int GIProductEquiID { get; set; }
        public string NameSeason { get; set; }
        public int ProductBaseID { get; set; }
        public int ProductEquiID { get; set; }
        public string CodeBase { get; set; }
        public string CodeEqui { get; set; }
        public int SeasonID { get; set; }
        
        public clsProductEqui()
        {
        }

        public clsProductEqui(int productBaseID, int productEquiID, int seasonID)
        {
            ProductBaseID = productBaseID;
            ProductEquiID = productEquiID;
            SeasonID = seasonID;
        }

        internal void InsertProductEqui()
        {
            Conexion.StartSession();
            string sql = "INSERT INTO " + clsGlobals.Gesin + "[tblGIProductEqui]([ProductBaseID],[ProductEquiID],[SeasonID],[ProductEquiStatus],[CreatedByUserID],[CreatedDate])VALUES (" + 
                ProductBaseID + ", " + ProductEquiID + ", " + SeasonID + ", 0, " + clsGlobals.GIPar.UserID + ", GETDATE())";
            Conexion.GDatos.RunSql(sql);
            Conexion.EndSession();
        }

        internal void CopyDataRow(DataRow rw)
        {
            this.GIProductEquiID = Convert.ToInt32(rw["GIProductEquiID"]);
            this.ProductBaseID = Convert.ToInt32(rw["ProductBaseID"]);
            this.ProductEquiID = Convert.ToInt32(rw["ProductEquiID"]);
            this.SeasonID = Convert.ToInt32(rw["SeasonID"]);
            this.CodeBase = GetGIProductCode(ProductBaseID);
            this.CodeEqui = GetGIProductCode(ProductEquiID);
            this.NameSeason = GetNameSeason();
            Conexion.EndSession();
        }

        private string GetNameSeason()
        {
            string sql = "SELECT [SeasonName_fra]FROM " + clsGlobals.Silex + "[tblSXSeason] AS SX INNER JOIN " + clsGlobals.Gesin + "[tblGISeason] AS GI " +
                "ON SX.SeasonID=GI.SXSeasonID WHERE [GISeasonID]=" + SeasonID;
            Conexion.StartSession();
            string vRes = (string)Conexion.GDatos.BringScalarValueSql(sql);

            return vRes;
        }

        private string GetGIProductCode(int productID)
        {
            string sql = "SELECT SX.ProductCode + ' '+ [ColorName_fra] " +
                "FROM " + clsGlobals.Gesin + "[tblGIProduct] AS GI " +
                "INNER JOIN " + clsGlobals.Silex + "[tblSXProduct] AS SX " +
                "ON GI.ProductID=SX.ProductID " +
                "INNER JOIN " + clsGlobals.Silex + "[tblSXColor] AS CL " +
                "ON GI.ColorID= CL.ColorID " +
                "WHERE GI.GIProductID= " + productID;
            
            Conexion.StartSession();
            string vRes = (string)Conexion.GDatos.BringScalarValueSql(sql);

            return vRes;
        }

        internal void DeleteProductEqui()
        {
            string sql = "UPDATE " + clsGlobals.Gesin + "[tblGIProductEqui]SET[ProductEquiStatus]=9,[DeletedByUserID]=" + clsGlobals.GIPar.UserID + 
                ",[DeletedDate]=GETDATE()WHERE [GIProductEquiID]=" + GIProductEquiID;
            Conexion.StartSession();
            Conexion.GDatos.RunSql(sql);
            Conexion.EndSession();
        }

        internal static bool ExistsEquivalence(int productColorID)
        {
            string sql = "SELECT [GIProductEquiID] " +
                "FROM " + clsGlobals.Gesin + "[tblGIProductEqui] AS PE " +
                "INNER JOIN " + clsGlobals.Gesin + "[tblGIProduct] AS GIP " +
                "ON PE.[ProductBaseID]= GIProductID " +
                "WHERE[ProductColorID]= " + productColorID + " AND [ProductEquiStatus]!=9";
            Conexion.StartSession();
            DataTable myTb = Conexion.GDatos.BringDataTableSql(sql);
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

        internal void GetProductEquiByPColorID(int productColorID)
        {
            string sql = "SELECT [GIProductEquiID],[ProductBaseID],[ProductEquiID],[SeasonID] " +
                "FROM " + clsGlobals.Gesin + "[tblGIProductEqui] AS PE " +
                "INNER JOIN " + clsGlobals.Gesin + "[tblGIProduct] AS GIP ON PE.[ProductBaseID]= GIProductID " +
                "WHERE[ProductColorID]= " + productColorID;
            Conexion.StartSession();
            DataTable myTb = Conexion.GDatos.BringDataTableSql(sql);
            Conexion.EndSession();
            this.CopyDataRow(myTb.Rows[0]);
        }
    }
}

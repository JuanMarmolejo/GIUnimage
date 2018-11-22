using prjGIUnimage.data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjGIUnimage.bus
{
    class clsSilex
    {
        public static DataTable tblSXCollection;
        public static DataTable tblSXDivision;
        public static DataTable tblSXProduct;
        public static DataTable tblGIScSalesHistory;
        public static DataTable tblSXSeason;
        public static DataTable tblSXColor;
        public static DataTable tblSXProductColor;
        public static DataTable tblGIProductAvailableOS;

        public static void UpDateCollection()
        {
            Conexion.StartSession();
            string sql = "SELECT * FROM " + clsGlobals.Silex + "[tblSXCollection] WHERE [CollectionStatus]!=9";
            tblSXCollection = Conexion.GDatos.BringDataTableSql(sql);
            Conexion.EndSession();
        }

        public static void UpDateDivision()
        {
            Conexion.StartSession();
            string sql = "SELECT * FROM " + clsGlobals.Silex + "[tblSXDivision] WHERE [DivStatus]!=9";
            tblSXDivision = Conexion.GDatos.BringDataTableSql(sql);
            Conexion.EndSession();
        }

        internal static void UpDateProduct()
        {
            Conexion.StartSession();
            string sql = "SELECT * FROM " + clsGlobals.Silex + "[tblSXProduct] WHERE [ProductStatus]!=9 AND [VirtualStatus]=0";
            tblSXProduct = Conexion.GDatos.BringDataTableSql(sql);
            Conexion.EndSession();
        }

        internal static void RunStoredProcedure(int activeScenario, int sXSeasonID, int secondSeasonID, int sXSeasonPrecID)
        {
            DataSet mySet = new DataSet();
            Object[] args = new Object[] { activeScenario, sXSeasonID, secondSeasonID, sXSeasonPrecID };
            Conexion.StartSession();
            Conexion.GDatos.ExecuteStoredProcedure(clsGlobals.Silex + "spCustomGenerateReq", args);
            Conexion.GDatos.ExecuteStoredProcedure(clsGlobals.Silex + "spCustomGenerateReqVirtual", args);
            Conexion.EndSession();
        }

        internal static void UpDateScSalesHistory()
        {
            Conexion.StartSession();
            string sql = "SELECT * FROM " + clsGlobals.Gesin + "[tblGIScSalesHistory]";
            tblGIScSalesHistory = Conexion.GDatos.BringDataTableSql(sql);
            Conexion.EndSession();
        }

        internal static void UpDateProductColor()
        {
            Conexion.StartSession();
            string sql = "SELECT * FROM " + clsGlobals.Silex + "[tblSXProductColor] WHERE[ProductColorStatus]!=9";
            tblSXProductColor = Conexion.GDatos.BringDataTableSql(sql);
            Conexion.EndSession();
        }

        internal static void RunStoredProcedure(int gIVOID)
        {
            Object[] args = new Object[] { gIVOID };
            Conexion.StartSession();
            Conexion.GDatos.ExecuteStoredProcedure(clsGlobals.Gesin + "spGICreateVO", args);
            Conexion.EndSession();
        }

        internal static string CollectionStatus(int collectionID)
        {
            var query = from ele in clsSilex.tblSXCollection.AsEnumerable()
                        where ele.Field<int>("CollectionID") == collectionID
                        select ele.Field<byte>("CollectionStatus");
            string op = query.FirstOrDefault().ToString();
            switch (op)
            {
                case "0": return "Actif";
                case "1": return "Inactif";
                default: return query.FirstOrDefault().ToString();
            }
        }

        internal static int GetDivisionID(int collectionID)
        {
            var query = from ele in clsSilex.tblSXCollection.AsEnumerable()
                        where ele.Field<int>("CollectionID") == collectionID
                        select ele.Field<int>("DivisionID");
            return query.FirstOrDefault();
        }

        public static void UpDateSeasons()
        {
            Conexion.StartSession();
            string sql = "SELECT * FROM " + clsGlobals.Silex + "[tblSXSeason]";
            tblSXSeason = Conexion.GDatos.BringDataTableSql(sql);
            Conexion.EndSession();
        }

        internal static void UpDateColor()
        {
            Conexion.StartSession();
            string sql = "SELECT * FROM " + clsGlobals.Silex + "[tblSXColor] WHERE [ColorStatus]!=9";
            tblSXColor = Conexion.GDatos.BringDataTableSql(sql);
            Conexion.EndSession();
        }

        internal static string GetCollectionName(int collectionID)
        {
            var query = from ele in clsSilex.tblSXCollection.AsEnumerable()
                        where ele.Field<int>("CollectionID") == collectionID
                        select ele.Field<string>("CollectionName");
            return query.FirstOrDefault();
        }

        internal static string GetProductCode(int productID)
        {
            var query = from ele in clsSilex.tblSXProduct.AsEnumerable()
                        where ele.Field<int>("ProductID") == productID
                        select ele.Field<string>("ProductCode");
            return query.FirstOrDefault();
        }

        internal static string GetProductDesc(int productID)
        {
            var query = from ele in clsSilex.tblSXProduct.AsEnumerable()
                        where ele.Field<int>("ProductID") == productID
                        select ele.Field<string>("ProductDesc_eng");
            return query.FirstOrDefault();
        }

        internal static string GetColorName(int colorID)
        {
            var query = from ele in clsSilex.tblSXColor.AsEnumerable()
                        where ele.Field<int>("ColorID") == colorID
                        select ele.Field<string>("ColorName_eng");
            return query.FirstOrDefault();
        }

        internal static string GetColorCode(int colorID)
        {
            var query = from ele in clsSilex.tblSXColor.AsEnumerable()
                        where ele.Field<int>("ColorID") == colorID
                        select ele.Field<string>("ColorCode");
            return query.FirstOrDefault();
        }

        internal static string GetStatus(int productColorID)
        {
            string sql = "SELECT [DataDesc_fra]FROM " + clsGlobals.Silex + "[tblSXData] AS DT INNER JOIN " + clsGlobals.Silex + "[tblSXProductColor]ON [ProductColorStatus]=[DataValue]" +
                "WHERE DataGroupID=119 AND [ProductColorID]=" + productColorID;
            Conexion.StartSession();
            string status = Conexion.GDatos.BringScalarValueSql(sql).ToString();
            Conexion.EndSession();

            return status;
        }

        internal static int GetProductColorIDByGIProductID(int productBaseID)
        {
            string sql = "SELECT [ProductColorID] " +
                "FROM " + clsGlobals.Gesin + "[tblGIProduct] " +
                "WHERE[GIProductID] = " + productBaseID;
            Conexion.StartSession();
            var productColorID = Conexion.GDatos.BringScalarValueSql(sql);
            Conexion.EndSession();
            return Convert.ToInt32(productColorID);
        }

        public static void UpDateProductAvailableOS()
        {
            Conexion.StartSession();
            string sql = "SELECT * FROM " + clsGlobals.Gesin + "[tblGIProductAvailableOS]";
            tblGIProductAvailableOS = Conexion.GDatos.BringDataTableSql(sql);
            Conexion.EndSession();
        }

        internal static bool Exists(clsProductAvailableOS myPAva)
        {
            var query = from ele in clsSilex.tblGIProductAvailableOS.AsEnumerable()
                        where ele.Field<short>("ScenarioID") == myPAva.ScenarioID
                        && ele.Field<int>("ProductColorID") == myPAva.ProductColorID
                        && ele.Field<int>("DimID") == myPAva.DimID
                        && ele.Field<short>("SizeOrder") == myPAva.SizeOrder
                        select ele;

            if (query.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        internal static string GetNameSeason(int sXSeasonID)
        {
            UpDateSeasons();
            var query = from ele in clsSilex.tblSXSeason.AsEnumerable()
                        where ele.Field<int>("SeasonID") == sXSeasonID
                        select ele.Field<string>("SeasonName_eng");
            return query.FirstOrDefault();
        }
    }
}

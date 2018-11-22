using prjGIUnimage.data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjGIUnimage.bus
{
    class clsListElements
    {
        List<clsElement> myList;

        public clsListElements()
        {
            myList = new List<clsElement>();
        }

        public int Quantity
        {
            get => myList.Count;
        } 

        public List<clsElement> Elements
        {
            get => myList;
            set => myList = value;
        }

        private bool Exist(clsElement ele)
        {
            foreach (clsElement item in myList)
            {
                if (item.ElementID == ele.ElementID)
                {
                    return true;
                }
            }
            return false;
        }

        private List<clsElement> CopyDataTable(DataTable myTb)
        {
            List<clsElement> lstTem = new List<clsElement>();
            foreach (DataRow rw in myTb.Rows)
            {
                clsElement myEle = new clsElement();
                myEle.CopyDataRow(rw);
                lstTem.Add(myEle);
            }
            return lstTem;
        }

        public void Add(clsElement Ele)
        {
            myList.Add(Ele);
        }

        public void AddNotExist(clsElement Ele)
        {
            if (!this.Exist(Ele))
            {
                myList.Add(Ele);
            }
            Elements.Sort();
        }

        internal clsElement ElementByID(string ElementID)
        {
            foreach (clsElement ele in Elements)
            {
                if (ele.ElementID == ElementID)
                {
                    return ele;
                }
            }
            return new clsElement();
        }

        internal int GetNumberGroups()
        {
            string sql = "SELECT COUNT(DISTINCT PG.ProductGroupID)FROM " + clsGlobals.Gesin + "[tblGIScProduct] AS SC INNER JOIN " + clsGlobals.Silex + "[tblSXProduct] AS SX " +
                "ON SC.ProductID = SX.ProductID INNER JOIN " + clsGlobals.Silex + "[tblSXProductGroup] AS PG ON SX.ProductGroupID = PG.ProductGroupID " +
                "INNER JOIN " + clsGlobals.Gesin + "[tblGIScCollection] AS GI ON (GI.CollectionID = SX.CollectionID AND SC.ScenarioID = GI.ScenarioID) WHERE GI.ScenarioID = " + clsGlobals.GIPar.ScenarioID;
            //string sql = "SELECT COUNT(DISTINCT PG.ProductGroupID)FROM [tblGIScProduct] AS SC INNER JOIN [tblSXProduct] AS SX " +
            //    "ON SC.ProductID = SX.ProductID INNER JOIN [tblSXProductGroup] AS PG ON SX.ProductGroupID=PG.ProductGroupID WHERE SC.ScenarioID="
            //    + clsGlobals.GIPar.ScenarioID;
            Conexion.StartSession();
            int Num = Convert.ToInt32(Conexion.GDatos.BringScalarValueSql(sql));
            Conexion.EndSession();
            return Num;
        }

        internal int GetNumberStyles()
        {
            bool flag = true;

            string sql = "SELECT COUNT(DISTINCT SX.ShortProductCode) FROM " + clsGlobals.Gesin + "[tblGIScProduct] AS SC INNER JOIN " + clsGlobals.Silex + "[tblSXProduct] AS SX " +
                "ON SC.ProductID = SX.ProductID INNER JOIN " + clsGlobals.Silex + "[tblSXProductGroup] AS PG ON SX.ProductGroupID = PG.ProductGroupID " +
                "INNER JOIN " + clsGlobals.Gesin + "[tblGIScCollection] AS GI ON (GI.CollectionID = SX.CollectionID AND SC.ScenarioID = GI.ScenarioID) WHERE GI.ScenarioID = " +
                clsGlobals.GIPar.ScenarioID;
            //string sql = "SELECT COUNT(DISTINCT SX.ShortProductCode) FROM[tblGIScProduct] AS SC INNER JOIN[tblSXProduct] AS SX " +
            //    "ON SC.ProductID = SX.ProductID INNER JOIN[tblSXProductGroup] AS PG ON SX.ProductGroupID = PG.ProductGroupID WHERE SC.ScenarioID = "
            //    + clsGlobals.GIPar.ScenarioID + " ";

            foreach (clsElement ele in clsGlobals.ListGroups.Elements)
            {
                sql += flag ? "AND (SX.ProductGroupID=" + ele.ElementID + " " : "OR SX.ProductGroupID=" + ele.ElementID + " ";
                flag = false;
            }
            sql += flag ? "" : ")";

            Conexion.StartSession();
            return Convert.ToInt32(Conexion.GDatos.BringScalarValueSql(sql));
        }

        internal int GetNumberColors()
        {
            bool flag = true;

            string sql = "SELECT COUNT(DISTINCT[ColorID]) FROM " + clsGlobals.Gesin + "[tblGIScProduct] AS SC INNER JOIN " + clsGlobals.Silex + "[tblSXProduct] AS SX " +
                "ON SC.ProductID = SX.ProductID INNER JOIN " + clsGlobals.Gesin + "[tblGIScCollection] AS GI ON (GI.CollectionID = SX.CollectionID AND SC.ScenarioID = GI.ScenarioID) " +
                "WHERE GI.ScenarioID = " + clsGlobals.GIPar.ScenarioID;
            //string sql = "SELECT COUNT(DISTINCT[ColorID])FROM [tblGIScProduct] AS SC INNER JOIN [tblSXProduct] AS SX ON SC.ProductID=SX.ProductID " +
            //    "WHERE SC.ScenarioID=" + clsGlobals.GIPar.ScenarioID + " ";

            foreach (clsElement ele in clsGlobals.ListGroups.Elements)
            {
                sql += flag ? "AND(SX.ProductGroupID=" + ele.ElementID + " " : "OR SX.ProductGroupID=" + ele.ElementID + " ";
                flag = false;
            }
            sql += flag ? "" : ")";

            flag = true;
            foreach (clsElement ele in clsGlobals.ListStyles.Elements)
            {
                sql += flag ? "AND (SX.ShortProductCode='" + ele.ElementID + "' " : "OR SX.ShortProductCode='" + ele.ElementID + "' ";
                flag = false;
            }
            sql += flag ? "" : ")";

            Conexion.StartSession();
            return Convert.ToInt32(Conexion.GDatos.BringScalarValueSql(sql));
        }

        internal int GetNumberCollections()
        {
            bool flag = true;

            string sql = "SELECT COUNT(DISTINCT SX.CollectionID) FROM " + clsGlobals.Gesin + "[tblGIScProduct] AS SC INNER JOIN " + clsGlobals.Silex + "[tblSXProduct] AS SX " +
                "ON SC.ProductID = SX.ProductID INNER JOIN " + clsGlobals.Silex + "[tblSXProductColor] AS PC ON SC.ProductColorID = PC.ProductColorID " +
                "INNER JOIN " + clsGlobals.Gesin + "[tblGIScCollection] AS GI ON (GI.CollectionID = SX.CollectionID AND SC.ScenarioID = GI.ScenarioID) WHERE GI.ScenarioID = " +
                clsGlobals.GIPar.ScenarioID;
            //string sql = "SELECT COUNT(DISTINCT SX.CollectionID)FROM[tblGIScProduct] AS SC INNER JOIN[tblSXProduct] AS SX " +
            //    "ON SC.ProductID=SX.ProductID INNER JOIN [tblSXProductColor] AS PC ON SC.ProductColorID=PC.ProductColorID WHERE SC.ScenarioID = "
            //    + clsGlobals.GIPar.ScenarioID + " ";

            foreach (clsElement ele in clsGlobals.ListGroups.Elements)
            {
                sql += flag ? "AND(SX.ProductGroupID=" + ele.ElementID + " " : "OR SX.ProductGroupID=" + ele.ElementID + " ";
                flag = false;
            }
            sql += flag ? "" : ")";

            flag = true;
            foreach (clsElement ele in clsGlobals.ListStyles.Elements)
            {
                sql += flag ? "AND (SX.ShortProductCode='" + ele.ElementID + "' " : "OR SX.ShortProductCode='" + ele.ElementID + "' ";
                flag = false;
            }
            sql += flag ? "" : ")";

            flag = true;
            foreach (clsElement ele in clsGlobals.ListColors.Elements)
            {
                sql += flag ? "AND (PC.ColorID=" + ele.ElementID + " " : "OR PC.ColorID=" + ele.ElementID + " ";
                flag = false;
            }
            sql += flag ? "" : ")";

            Conexion.StartSession();
            return Convert.ToInt32(Conexion.GDatos.BringScalarValueSql(sql));
        }

        internal int GetNumberSpecCollection()
        {
            bool flag = true;

            string sql = "SELECT COUNT(DISTINCT[ScProductID]) " +
                "FROM " + clsGlobals.Gesin + "[tblGIScProduct] AS SC " +
                "INNER JOIN " + clsGlobals.Silex + "[tblSXProduct] AS SX ON SC.ProductID = SX.ProductID " +
                "INNER JOIN " + clsGlobals.Silex + "[tblSXProductColor] AS PC ON SC.ProductColorID = PC.ProductColorID " +
                "INNER JOIN " + clsGlobals.Gesin + "[tblGIScCollection] AS GI ON (GI.CollectionID = SX.CollectionID AND SC.ScenarioID = GI.ScenarioID)" +
                "WHERE(GI.[CollectionID] != 101 AND GI.[CollectionID] != 112) " +
                "AND PC.ProductColorID NOT IN(SELECT DISTINCT [ProductColorID] FROM " + clsGlobals.Silex + "[tblSXCostCardDetail])" +
                "AND SC.ScenarioID = " + clsGlobals.GIPar.ScenarioID;
            
            foreach (clsElement ele in clsGlobals.ListGroups.Elements)
            {
                sql += flag ? "AND(SX.ProductGroupID=" + ele.ElementID + " " : "OR SX.ProductGroupID=" + ele.ElementID + " ";
                flag = false;
            }
            sql += flag ? "" : ")";

            flag = true;
            foreach (clsElement ele in clsGlobals.ListStyles.Elements)
            {
                sql += flag ? "AND (SX.ShortProductCode='" + ele.ElementID + "' " : "OR SX.ShortProductCode='" + ele.ElementID + "' ";
                flag = false;
            }
            sql += flag ? "" : ")";

            flag = true;
            foreach (clsElement ele in clsGlobals.ListColors.Elements)
            {
                sql += flag ? "AND (PC.ColorID=" + ele.ElementID + " " : "OR PC.ColorID=" + ele.ElementID + " ";
                flag = false;
            }
            sql += flag ? "" : ")";

            flag = true;
            foreach (clsElement ele in clsGlobals.ListCollections.Elements)
            {
                sql += flag ? "AND(SX.CollectionID=" + ele.ElementID + " " : "OR SX.CollectionID=" + ele.ElementID + " ";
                flag = false;
            }
            sql += flag ? "" : ")";

            Conexion.StartSession();
            return Convert.ToInt32(Conexion.GDatos.BringScalarValueSql(sql));
        }

        internal int GetNumberVOSpecCollection()
        {
            bool flag = true;

            string sql = "SELECT COUNT(DISTINCT[ScProductID]) " +
                "FROM " + clsGlobals.Gesin + "[tblGIScProduct] AS SC " +
                "INNER JOIN " + clsGlobals.Silex + "[tblSXProduct] AS SX ON SC.ProductID = SX.ProductID " +
                "INNER JOIN " + clsGlobals.Silex + "[tblSXProductColor] AS PC ON SC.ProductColorID = PC.ProductColorID " +
                "INNER JOIN " + clsGlobals.Gesin + "[tblGIScCollection] AS GI ON (GI.CollectionID = SX.CollectionID AND SC.ScenarioID = GI.ScenarioID) " +
                "WHERE(GI.[CollectionID] != 101 AND GI.[CollectionID] != 112) " +
                "AND PC.ProductColorID NOT IN(SELECT DISTINCT [ProductColorID] FROM " + clsGlobals.Silex + "[tblSXCostCardDetail])" +
                "AND SC.ScenarioID = " + clsGlobals.GIPar.ScenarioID;
            
            foreach (clsElement ele in clsGlobals.ListGroups.Elements)
            {
                sql += flag ? "AND(SX.ProductGroupID=" + ele.ElementID + " " : "OR SX.ProductGroupID=" + ele.ElementID + " ";
                flag = false;
            }
            sql += flag ? "" : ")";

            flag = true;
            foreach (clsElement ele in clsGlobals.ListStyles.Elements)
            {
                sql += flag ? "AND (SX.ShortProductCode='" + ele.ElementID + "' " : "OR SX.ShortProductCode='" + ele.ElementID + "' ";
                flag = false;
            }
            sql += flag ? "" : ")";

            flag = true;
            foreach (clsElement ele in clsGlobals.ListColors.Elements)
            {
                sql += flag ? "AND (PC.ColorID=" + ele.ElementID + " " : "OR PC.ColorID=" + ele.ElementID + " ";
                flag = false;
            }
            sql += flag ? "" : ")";

            flag = true;
            foreach (clsElement ele in clsGlobals.ListCollections.Elements)
            {
                sql += flag ? "AND(SX.CollectionID=" + ele.ElementID + " " : "OR SX.CollectionID=" + ele.ElementID + " ";
                flag = false;
            }
            sql += flag ? "" : ")";

            sql += " AND [VOStatus]=1";

            Conexion.StartSession();
            return Convert.ToInt32(Conexion.GDatos.BringScalarValueSql(sql));
        }

        internal int GetNumberCommon()
        {
            bool flag = true;

            string sql = "SELECT COUNT(DISTINCT[ScProductID]) FROM " + clsGlobals.Gesin + "[tblGIScProduct] AS SC INNER JOIN " + clsGlobals.Silex + "[tblSXProduct] AS SX " +
                "ON SC.ProductID = SX.ProductID INNER JOIN " + clsGlobals.Silex + "[tblSXProductColor] AS PC ON SC.ProductColorID = PC.ProductColorID " +
                "INNER JOIN" + clsGlobals.Gesin + "[tblGIScCollection] AS GI ON (GI.CollectionID = SX.CollectionID AND SC.ScenarioID = GI.ScenarioID) WHERE(GI.[CollectionID] = 101) " +
                "AND SC.ScenarioID = " + clsGlobals.GIPar.ScenarioID;
            //string sql = "SELECT COUNT(DISTINCT[ScProductID])FROM[tblGIScProduct] AS SC INNER JOIN[tblSXProduct] AS SX ON SC.ProductID = SX.ProductID " +
            //    "INNER JOIN [tblSXProductColor] AS PC ON SC.ProductColorID=PC.ProductColorID WHERE(SX.[CollectionID] = 101) AND SC.ScenarioID = "
            //    + clsGlobals.GIPar.ScenarioID + " ";

            foreach (clsElement ele in clsGlobals.ListGroups.Elements)
            {
                sql += flag ? "AND(SX.ProductGroupID=" + ele.ElementID + " " : "OR SX.ProductGroupID=" + ele.ElementID + " ";
                flag = false;
            }
            sql += flag ? "" : ")";

            flag = true;
            foreach (clsElement ele in clsGlobals.ListStyles.Elements)
            {
                sql += flag ? "AND (SX.ShortProductCode='" + ele.ElementID + "' " : "OR SX.ShortProductCode='" + ele.ElementID + "' ";
                flag = false;
            }
            sql += flag ? "" : ")";

            flag = true;
            foreach (clsElement ele in clsGlobals.ListColors.Elements)
            {
                sql += flag ? "AND (PC.ColorID=" + ele.ElementID + " " : "OR PC.ColorID=" + ele.ElementID + " ";
                flag = false;
            }
            sql += flag ? "" : ")";

            flag = true;
            foreach (clsElement ele in clsGlobals.ListCollections.Elements)
            {
                sql += flag ? "AND(SX.CollectionID=" + ele.ElementID + " " : "OR SX.CollectionID=" + ele.ElementID + " ";
                flag = false;
            }
            sql += flag ? "" : ")";

            Conexion.StartSession();
            return Convert.ToInt32(Conexion.GDatos.BringScalarValueSql(sql));
        }

        internal int GetNumberVOCommon()
        {
            bool flag = true;

            string sql = "SELECT COUNT(DISTINCT[ScProductID]) FROM " + clsGlobals.Gesin + "[tblGIScProduct] AS SC INNER JOIN " + clsGlobals.Silex + "[tblSXProduct] AS SX " +
                "ON SC.ProductID = SX.ProductID INNER JOIN " + clsGlobals.Silex + "[tblSXProductColor] AS PC ON SC.ProductColorID = PC.ProductColorID " +
                "INNER JOIN" + clsGlobals.Gesin + "[tblGIScCollection] AS GI ON (GI.CollectionID = SX.CollectionID AND SC.ScenarioID = GI.ScenarioID) WHERE(GI.[CollectionID] = 101) " +
                "AND SC.ScenarioID = " + clsGlobals.GIPar.ScenarioID;
            //string sql = "SELECT COUNT(DISTINCT[ScProductID])FROM[tblGIScProduct] AS SC INNER JOIN[tblSXProduct] AS SX ON SC.ProductID = SX.ProductID " +
            //    "INNER JOIN [tblSXProductColor] AS PC ON SC.ProductColorID=PC.ProductColorID WHERE(SX.[CollectionID] = 101) AND SC.ScenarioID = "
            //    + clsGlobals.GIPar.ScenarioID + " ";

            foreach (clsElement ele in clsGlobals.ListGroups.Elements)
            {
                sql += flag ? "AND(SX.ProductGroupID=" + ele.ElementID + " " : "OR SX.ProductGroupID=" + ele.ElementID + " ";
                flag = false;
            }
            sql += flag ? "" : ")";

            flag = true;
            foreach (clsElement ele in clsGlobals.ListStyles.Elements)
            {
                sql += flag ? "AND (SX.ShortProductCode='" + ele.ElementID + "' " : "OR SX.ShortProductCode='" + ele.ElementID + "' ";
                flag = false;
            }
            sql += flag ? "" : ")";

            flag = true;
            foreach (clsElement ele in clsGlobals.ListColors.Elements)
            {
                sql += flag ? "AND (PC.ColorID=" + ele.ElementID + " " : "OR PC.ColorID=" + ele.ElementID + " ";
                flag = false;
            }
            sql += flag ? "" : ")";

            flag = true;
            foreach (clsElement ele in clsGlobals.ListCollections.Elements)
            {
                sql += flag ? "AND(SX.CollectionID=" + ele.ElementID + " " : "OR SX.CollectionID=" + ele.ElementID + " ";
                flag = false;
            }
            sql += flag ? "" : ")";

            sql += " AND [VOStatus]=1";

            Conexion.StartSession();
            return Convert.ToInt32(Conexion.GDatos.BringScalarValueSql(sql));
        }

        internal int GetNumberIdentify()
        {
            bool flag = true;

            string sql = "SELECT COUNT(DISTINCT[ScProductID]) FROM " + clsGlobals.Gesin + "[tblGIScProduct] AS SC INNER JOIN " + clsGlobals.Silex + "[tblSXProduct] AS SX " +
                "ON SC.ProductID = SX.ProductID INNER JOIN " + clsGlobals.Silex + "[tblSXProductColor] AS PC ON SC.ProductColorID = PC.ProductColorID " +
                "INNER JOIN " + clsGlobals.Gesin + "[tblGIScCollection] AS GI ON (GI.CollectionID = SX.CollectionID AND SC.ScenarioID = GI.ScenarioID) WHERE(GI.[CollectionID] = 112) " +
                "AND SC.ScenarioID = " + clsGlobals.GIPar.ScenarioID;
            //string sql = "SELECT COUNT(DISTINCT[ScProductID])FROM[tblGIScProduct] AS SC INNER JOIN[tblSXProduct] AS SX " +
            //    "ON SC.ProductID = SX.ProductID INNER JOIN [tblSXProductColor] AS PC ON SC.ProductColorID=PC.ProductColorID WHERE(SX.[CollectionID] = 112) " +
            //    "AND SC.ScenarioID = " + clsGlobals.GIPar.ScenarioID + " ";

            foreach (clsElement ele in clsGlobals.ListGroups.Elements)
            {
                sql += flag ? "AND(SX.ProductGroupID=" + ele.ElementID + " " : "OR SX.ProductGroupID=" + ele.ElementID + " ";
                flag = false;
            }
            sql += flag ? "" : ")";

            flag = true;
            foreach (clsElement ele in clsGlobals.ListStyles.Elements)
            {
                sql += flag ? "AND (SX.ShortProductCode='" + ele.ElementID + "' " : "OR SX.ShortProductCode='" + ele.ElementID + "' ";
                flag = false;
            }
            sql += flag ? "" : ")";

            flag = true;
            foreach (clsElement ele in clsGlobals.ListColors.Elements)
            {
                sql += flag ? "AND (PC.ColorID=" + ele.ElementID + " " : "OR PC.ColorID=" + ele.ElementID + " ";
                flag = false;
            }
            sql += flag ? "" : ")";

            flag = true;
            foreach (clsElement ele in clsGlobals.ListCollections.Elements)
            {
                sql += flag ? "AND(SX.CollectionID=" + ele.ElementID + " " : "OR SX.CollectionID=" + ele.ElementID + " ";
                flag = false;
            }
            sql += flag ? "" : ")";

            Conexion.StartSession();
            return Convert.ToInt32(Conexion.GDatos.BringScalarValueSql(sql));
        }

        internal int GetNumberVOIdentify()
        {
            bool flag = true;

            string sql = "SELECT COUNT(DISTINCT[ScProductID]) FROM " + clsGlobals.Gesin + "[tblGIScProduct] AS SC INNER JOIN " + clsGlobals.Silex + "[tblSXProduct] AS SX " +
                "ON SC.ProductID = SX.ProductID INNER JOIN " + clsGlobals.Silex + "[tblSXProductColor] AS PC ON SC.ProductColorID = PC.ProductColorID " +
                "INNER JOIN " + clsGlobals.Gesin + "[tblGIScCollection] AS GI ON (GI.CollectionID = SX.CollectionID AND SC.ScenarioID = GI.ScenarioID) WHERE(GI.[CollectionID] = 112) " +
                "AND SC.ScenarioID = " + clsGlobals.GIPar.ScenarioID;
            //string sql = "SELECT COUNT(DISTINCT[ScProductID])FROM[tblGIScProduct] AS SC INNER JOIN[tblSXProduct] AS SX " +
            //    "ON SC.ProductID = SX.ProductID INNER JOIN [tblSXProductColor] AS PC ON SC.ProductColorID=PC.ProductColorID WHERE(SX.[CollectionID] = 112) " +
            //    "AND SC.ScenarioID = " + clsGlobals.GIPar.ScenarioID + " ";

            foreach (clsElement ele in clsGlobals.ListGroups.Elements)
            {
                sql += flag ? "AND(SX.ProductGroupID=" + ele.ElementID + " " : "OR SX.ProductGroupID=" + ele.ElementID + " ";
                flag = false;
            }
            sql += flag ? "" : ")";

            flag = true;
            foreach (clsElement ele in clsGlobals.ListStyles.Elements)
            {
                sql += flag ? "AND (SX.ShortProductCode='" + ele.ElementID + "' " : "OR SX.ShortProductCode='" + ele.ElementID + "' ";
                flag = false;
            }
            sql += flag ? "" : ")";

            flag = true;
            foreach (clsElement ele in clsGlobals.ListColors.Elements)
            {
                sql += flag ? "AND (PC.ColorID=" + ele.ElementID + " " : "OR PC.ColorID=" + ele.ElementID + " ";
                flag = false;
            }
            sql += flag ? "" : ")";

            flag = true;
            foreach (clsElement ele in clsGlobals.ListCollections.Elements)
            {
                sql += flag ? "AND(SX.CollectionID=" + ele.ElementID + " " : "OR SX.CollectionID=" + ele.ElementID + " ";
                flag = false;
            }
            sql += flag ? "" : ")";

            sql += " AND [VOStatus]=1";

            Conexion.StartSession();
            return Convert.ToInt32(Conexion.GDatos.BringScalarValueSql(sql));
        }

        internal string GetProductCode(object selectedValue)
        {
            foreach (clsElement ele in myList)
            {
                if (ele.ElementID.ToString() == selectedValue.ToString())
                {
                    return ele.Full;
                }
            }
            return "";
        }

        //internal void FilterListBy(string text, object selectedValue)
        //{
        //    string sql = "SELECT [GICollectionID], [CollectionCode],[CollectionName] FROM " + clsGlobals.Gesin + "[tblGICollection] AS GI " +
        //        "INNER JOIN " + clsGlobals.Silex + "[tblSXCollection] AS SX ON GI.CollectionID=SX.CollectionID where [CollectionCode] like '%" + text + "%' ";
        //    if (Convert.ToInt32(selectedValue) != 2)
        //    {
        //        sql += "and[GICollectionStatus] = " + selectedValue + " ";
        //    }
        //    sql += "AND GICollectionStatus != 9 order by [CollectionCode]";
        //    Conexion.StartSession();
        //    DataTable myTb = Conexion.GDatos.BringDataTableSql(sql);
        //    Conexion.EndSession();
        //    Elements = CopyDataTable(myTb);
        //}

        internal void FilterElements(string text)
        {
            List<clsElement> newList = Elements.Where(m => m.Code.ToUpper().Contains(text) || m.Name.ToUpper().Contains(text)).ToList();
            Elements = newList;
        }

        //internal void FilterProduct(string text)
        //{
        //    List<clsElement> newList = Elements.Where(m => m.Code.Contains(text) || m.Name.ToUpper().Contains(text)).ToList();
        //    Elements = newList;
        //    //string sql = "SELECT [GIProductID], SX.ProductCode, CL.ColorName_fra FROM " + clsGlobals.Gesin + "[tblGIProduct] AS GI INNER JOIN " + clsGlobals.Silex + "[tblSXProduct] AS SX " +
        //    //    "ON GI.[ProductID]=SX.[ProductID] INNER JOIN " + clsGlobals.Silex + "[tblSXColor] AS CL ON GI.ColorID=CL.ColorID WHERE [ProductStatus] != 9 " +
        //    //    "AND [ProductCode] LIKE '%" + text + "%' ORDER BY [ProductCode]";
        //    //Conexion.StartSession();
        //    //DataTable myTb = Conexion.GDatos.BringDataTableSql(sql);
        //    //Conexion.EndSession();
        //    //Elements = CopyDataTable(myTb);
        //}

        internal void GetBillFrom()
        {
            string sql = "SELECT [VendorID],[DefaultShipFromID],[VendorCode] FROM " + clsGlobals.Silex + "[tblSXVendor] WHERE[VendorStatus] != 9 ORDER BY [VendorCode]";
            DataTable myTb = new DataTable();
            Conexion.StartSession();
            myTb = Conexion.GDatos.BringDataTableSql(sql);
            Conexion.EndSession();
            Elements = CopyDataTable(myTb);
        }

        internal void GetCollections(int DivisionID)
        {
            //String sql = "SELECT SX.[CollectionID],[CollectionCode],[CollectionName] FROM[tblGIScCollection] AS GI INNER JOIN[tblSXCollection] AS SX " +
            //    "ON GI.CollectionID = SX.CollectionID WHERE GI.ScenarioID = " + clsGlobals.GIPar.ScenarioID + " AND[CollectionStatus] != 9 " +
            //    "AND([DivisionID] = " + DivisionID + " OR SX.[CollectionID] = 1 OR SX.[CollectionID] = 50) ORDER BY[CollectionCode]";
            string sql = "SELECT [CollectionID],[CollectionCode],[CollectionName] FROM " + clsGlobals.Silex + "[tblSXCollection] WHERE[CollectionStatus] != 9 AND([DivisionID] = "
                + DivisionID + " OR[CollectionID] = 1 OR[CollectionID] = 50) ORDER BY[CollectionCode]";
            Conexion.StartSession();
            DataTable myTb = Conexion.GDatos.BringDataTableSql(sql);
            Conexion.EndSession();
            Elements = CopyDataTable(myTb);
        }

        internal void GetElementsCollection(object selectedValue)
        {
            string sql = "SELECT [GICollectionID], [CollectionCode],[CollectionName] FROM " + clsGlobals.Gesin + "[tblGICollection] AS GI " +
                "INNER JOIN " + clsGlobals.Silex + "[tblSXCollection] AS SX ON GI.CollectionID=SX.CollectionID WHERE CollectionStatus != 9 ";
            if (Convert.ToInt32(selectedValue) != 3)
            {
                sql += "AND GICollectionStatus = " + selectedValue + " ";
            }
            sql += "ORDER BY [CollectionCode]";
            Conexion.StartSession();
            DataTable myTb = Conexion.GDatos.BringDataTableSql(sql);
            Conexion.EndSession();
            Elements = CopyDataTable(myTb);
        }

        internal void GetDivisions()
        {
            string sql = "SELECT [DivisionID],[DivisionCode],[DivisionName] FROM " + clsGlobals.Silex + "[tblSXDivision] WHERE[DivStatus] != 9 ORDER BY [DivisionCode]";
            Conexion.StartSession();
            DataTable myTb = Conexion.GDatos.BringDataTableSql(sql);
            Conexion.EndSession();
            Elements = CopyDataTable(myTb);
        }

        internal void GetListCompCollection(int productColorID)
        {
            clsGlobals.sql = "SELECT [ProductColorID],[ProductCode],[ColorName_fra] FROM " + clsGlobals.Gesin +
                    "[tblGIScProduct] AS SC INNER JOIN " + clsGlobals.Silex + "[tblSXProduct] AS SX ON SC.ProductID=SX.ProductID " +
                    "INNER JOIN " + clsGlobals.Silex + "[tblSXColor] AS CL ON SC.ColorID= CL.ColorID WHERE[ProductColorID] IN " +
                    "(SELECT DISTINCT[ProductColorID] FROM " + clsGlobals.Silex + "[tblSXCostCardComp] AS COST  INNER JOIN " +
                    clsGlobals.Silex + "[tblSXCostCardDetail] AS DET ON COST.CostCardDetailID= DET.CostCardDetailID  WHERE[CompProductColorID]= " +
                    productColorID + ") AND ScenarioID = " + clsGlobals.GIPar.ScenarioID + " ORDER BY[ProductCode], [ColorName_eng]";
        }

        internal void GetDivisions(int productColorID)
        {
            string sql = "SELECT D.[DivisionID],[DivisionCode],[DivisionName] FROM " + clsGlobals.Silex + "[tblSXDivision] AS D " +
                "INNER JOIN " + clsGlobals.Silex + "[tblSXCollection] AS C ON D.[DivisionID]=C.[DivisionID] INNER JOIN " + clsGlobals.Silex + "[tblSXProduct] AS P " +
                "ON C.CollectionID=P.CollectionID INNER JOIN " + clsGlobals.Silex + "[tblSXProductColor] AS PC ON P.ProductID=PC.ProductID " +
                "WHERE[DivStatus] != 9 AND PC.ProductColorID=" + productColorID + " ORDER BY [DivisionCode]";
            Conexion.StartSession();
            DataTable myTb = Conexion.GDatos.BringDataTableSql(sql);
            Conexion.EndSession();
            Elements = CopyDataTable(myTb);
        }

        internal void GetElementsGlobalRequest()
        {
            Conexion.StartSession();
            DataTable myTb = Conexion.GDatos.BringDataTableSql(clsGlobals.sql);
            Conexion.EndSession();
            Elements = CopyDataTable(myTb);
        }

        //internal void FilterSelectedProduct(string text)
        //{
        //    List<clsElement> newList = Elements.Where(m => m.Code.Contains(text) || m.Name.ToUpper().Contains(text)).ToList();
        //    Elements = newList;
        //}

        //internal void FilterGroupsToWork(string text)
        //{
        //    List<clsElement> newList = Elements.Where(m => m.Code.Contains(text) || m.Name.ToUpper().Contains(text)).ToList();
        //    Elements = newList;
        //}

        internal void GetListCollections()
        {
            bool flag = true;
            //2018-07-13 14:43 CAMBIO REQUET
            string sql = "SELECT [ProductColorID],[ProductCode],[ColorName_fra]" +
                "FROM " + clsGlobals.Gesin + "[tblGIScProduct] AS SC " +
                "INNER JOIN " + clsGlobals.Silex + "[tblSXProduct] AS SX ON SC.ProductID=SX.ProductID " +
                "INNER JOIN " + clsGlobals.Silex + "[tblSXColor] AS CL ON SC.ColorID=CL.ColorID " +
                "INNER JOIN " + clsGlobals.Silex + "[tblSXCollection] AS CT ON SX.CollectionID=CT.CollectionID " +
                "INNER JOIN " + clsGlobals.Gesin + "[tblGIScCollection] AS giCL ON(CT.CollectionID = giCL.CollectionID AND SC.ScenarioID = giCL.ScenarioID) " +
                "WHERE SC.ScenarioID=" + clsGlobals.GIPar.ScenarioID +
                " AND SX.CollectionID!=112 AND SX.CollectionID!=101 AND CT.CollectionStatus=0 " +
                "AND [ProductColorID] NOT IN (SELECT DISTINCT [ProductColorID] FROM " + clsGlobals.Silex + "[tblSXCostCardDetail])";
            //string sql = "SELECT DISTINCT scSH.[ProductColorID],[ProductCode],[ColorName_eng] FROM[tblGIScSalesHistory] AS scSH " +
            //    "INNER JOIN[tblGIScCollection] AS giCL ON(scSH.CollectionID = giCL.CollectionID AND scSH.ScenarioID = giCL.ScenarioID) " +
            //    "INNER JOIN[tblGIScProduct] AS giPD ON(scSH.ProductID = GIPD.ProductID AND scSH.ScenarioID = giCL.ScenarioID) WHERE scSH.ScenarioID = " +
            //    clsGlobals.GIPar.ScenarioID + " AND scSH.CollectionID!=112 AND scSH.CollectionID!=101 ";
            foreach (clsElement ele in clsGlobals.ListGroups.Elements)
            {
                sql += flag ? "AND (SX.ProductGroupID=" + ele.ElementID + " " : "OR SX.ProductGroupID=" + ele.ElementID + " ";
                flag = false;
            }
            sql += flag ? "" : ")";

            flag = true;
            foreach (clsElement ele in clsGlobals.ListStyles.Elements)
            {
                sql += flag ? "AND (SX.ShortProductCode='" + ele.ElementID + "' " : "OR SX.ShortProductCode='" + ele.ElementID + "' ";
                flag = false;
            }
            sql += flag ? "" : ")";

            flag = true;
            foreach (clsElement ele in clsGlobals.ListColors.Elements)
            {
                sql += flag ? "AND (SC.ColorID=" + ele.ElementID + " " : "OR SC.ColorID=" + ele.ElementID + " ";
                flag = false;
            }
            sql += flag ? "" : ")";

            flag = true;
            foreach (clsElement ele in clsGlobals.ListCollections.Elements)
            {
                sql += flag ? "AND(SX.CollectionID=" + ele.ElementID + " " : "OR SX.CollectionID=" + ele.ElementID + " ";
                flag = false;
            }
            sql += flag ? "" : ")";

            sql += " ORDER BY [ProductCode], [ColorName_eng]";
            clsGlobals.sql = sql;
        }

        internal void GetListCommons()
        {
            bool flag = true;
            //2018-07-13 14:48 CAMBIO REQUET
            string sql = "SELECT [ProductColorID],[ProductCode],[ColorName_fra]FROM " + clsGlobals.Gesin + "[tblGIScProduct] AS SC " +
                "INNER JOIN " + clsGlobals.Silex + "[tblSXProduct] AS SX ON SC.ProductID=SX.ProductID " +
                "INNER JOIN " + clsGlobals.Silex + "[tblSXColor] AS CL ON SC.ColorID=CL.ColorID " +
                "INNER JOIN " + clsGlobals.Silex + "[tblSXCollection] AS CT ON SX.CollectionID=CT.CollectionID " +
                "INNER JOIN " + clsGlobals.Gesin + "[tblGIScCollection] AS giCL ON(CT.CollectionID = giCL.CollectionID AND SC.ScenarioID = giCL.ScenarioID) " +
                "WHERE SC.ScenarioID=" + clsGlobals.GIPar.ScenarioID
                + " AND SX.CollectionID=101 ";
            //string sql = "SELECT DISTINCT scSH.[ProductColorID],[ProductCode],[ColorName_eng] FROM[tblGIScSalesHistory] AS scSH " +
            //    "INNER JOIN[tblGIScCollection] AS giCL ON(scSH.CollectionID = giCL.CollectionID AND scSH.ScenarioID = giCL.ScenarioID) " +
            //    "INNER JOIN[tblGIScProduct] AS giPD ON(scSH.ProductID = GIPD.ProductID AND scSH.ScenarioID = giCL.ScenarioID) WHERE scSH.ScenarioID = " +
            //    clsGlobals.GIPar.ScenarioID + " AND scSH.CollectionID=101 ";
            foreach (clsElement ele in clsGlobals.ListGroups.Elements)
            {
                sql += flag ? "AND (SX.ProductGroupID=" + ele.ElementID + " " : "OR SX.ProductGroupID=" + ele.ElementID + " ";
                flag = false;
            }
            sql += flag ? "" : ")";

            flag = true;
            foreach (clsElement ele in clsGlobals.ListStyles.Elements)
            {
                sql += flag ? "AND (SX.ShortProductCode='" + ele.ElementID + "' " : "OR SX.ShortProductCode='" + ele.ElementID + "' ";
                flag = false;
            }
            sql += flag ? "" : ")";

            flag = true;
            foreach (clsElement ele in clsGlobals.ListColors.Elements)
            {
                sql += flag ? "AND (SC.ColorID=" + ele.ElementID + " " : "OR SC.ColorID=" + ele.ElementID + " ";
                flag = false;
            }
            sql += flag ? "" : ")";

            flag = true;
            foreach (clsElement ele in clsGlobals.ListCollections.Elements)
            {
                sql += flag ? "AND(SX.CollectionID=" + ele.ElementID + " " : "OR SX.CollectionID=" + ele.ElementID + " ";
                flag = false;
            }
            sql += flag ? "" : ")";

            sql += " ORDER BY [ProductCode], [ColorName_eng]";
            clsGlobals.sql = sql;
        }

        internal void GetListUnidentified()
        {
            bool flag = true;
            //2018-07-13 14:49 CAMBIO REQUET
            string sql = "SELECT [ProductColorID],[ProductCode],[ColorName_fra]FROM " + clsGlobals.Gesin + "[tblGIScProduct] AS SC " +
                "INNER JOIN " + clsGlobals.Silex + "[tblSXProduct] AS SX ON SC.ProductID=SX.ProductID " +
                "INNER JOIN " + clsGlobals.Silex + "[tblSXColor] AS CL ON SC.ColorID=CL.ColorID " +
                "INNER JOIN " + clsGlobals.Silex + "[tblSXCollection] AS CT ON SX.CollectionID=CT.CollectionID " +
                "INNER JOIN " + clsGlobals.Gesin + "[tblGIScCollection] AS giCL ON(CT.CollectionID = giCL.CollectionID AND SC.ScenarioID = giCL.ScenarioID) " +
                "WHERE SC.ScenarioID=" + clsGlobals.GIPar.ScenarioID + 
                " AND SX.CollectionID=112 ";
            //string sql = "SELECT DISTINCT scSH.[ProductColorID],[ProductCode],[ColorName_eng] FROM[tblGIScSalesHistory] AS scSH " +
            //    "INNER JOIN[tblGIScCollection] AS giCL ON(scSH.CollectionID = giCL.CollectionID AND scSH.ScenarioID = giCL.ScenarioID) " +
            //    "INNER JOIN[tblGIScProduct] AS giPD ON(scSH.ProductID = GIPD.ProductID AND scSH.ScenarioID = giCL.ScenarioID) WHERE scSH.ScenarioID = " +
            //    clsGlobals.GIPar.ScenarioID + " AND scSH.CollectionID=112 ";
            foreach (clsElement ele in clsGlobals.ListGroups.Elements)
            {
                sql += flag ? "AND (SX.ProductGroupID=" + ele.ElementID + " " : "OR SX.ProductGroupID=" + ele.ElementID + " ";
                flag = false;
            }
            sql += flag ? "" : ")";

            flag = true;
            foreach (clsElement ele in clsGlobals.ListStyles.Elements)
            {
                sql += flag ? "AND (SX.ShortProductCode='" + ele.ElementID + "' " : "OR SX.ShortProductCode='" + ele.ElementID + "' ";
                flag = false;
            }
            sql += flag ? "" : ")";

            flag = true;
            foreach (clsElement ele in clsGlobals.ListColors.Elements)
            {
                sql += flag ? "AND (SC.ColorID=" + ele.ElementID + " " : "OR SC.ColorID=" + ele.ElementID + " ";
                flag = false;
            }
            sql += flag ? "" : ")";

            flag = true;
            foreach (clsElement ele in clsGlobals.ListCollections.Elements)
            {
                sql += flag ? "AND(SX.CollectionID=" + ele.ElementID + " " : "OR SX.CollectionID=" + ele.ElementID + " ";
                flag = false;
            }
            sql += flag ? "" : ")";

            sql += " ORDER BY [ProductCode], [ColorName_eng]";
            clsGlobals.sql = sql;
        }

        internal void GetProducts()
        {
            string sql = "SELECT [GIProductID], SX.ProductCode, CL.ColorName_fra FROM " + clsGlobals.Gesin + "[tblGIProduct] AS GI INNER JOIN " + clsGlobals.Silex + "[tblSXProduct] AS SX " +
                "ON GI.[ProductID]=SX.[ProductID] INNER JOIN " + clsGlobals.Silex + "[tblSXColor] AS CL ON GI.ColorID=CL.ColorID WHERE [ProductStatus] != 9 ORDER BY [ProductCode]";
            Conexion.StartSession();
            DataTable myTb = Conexion.GDatos.BringDataTableSql(sql);
            Conexion.EndSession();
            Elements = CopyDataTable(myTb);
        }

        //internal void GetProductColor()
        //{
        //    string sql = "SELECT [ProductColorID], SX.ProductCode, CL.ColorName_fra FROM " + clsGlobals.Gesin + "[tblGIProduct] AS GI INNER JOIN " + clsGlobals.Silex + "[tblSXProduct] AS SX " +
        //        "ON GI.[ProductID]=SX.[ProductID] INNER JOIN " + clsGlobals.Silex + "[tblSXColor] AS CL ON GI.ColorID=CL.ColorID WHERE [ProductStatus] != 9 ORDER BY [ProductCode]";
        //    Conexion.StartSession();
        //    DataTable myTb = Conexion.GDatos.BringDataTableSql(sql);
        //    Conexion.EndSession();
        //    Elements = CopyDataTable(myTb);
        //}

        internal void GetPurchaseType()
        {
            string sql = "SELECT [PurchaseTypeID],[PurchaseTypeName_fra],[PurchaseTypeDesc_fra] FROM " + clsGlobals.Silex + "[tblSXPurchaseType] WHERE[PurchaseTypeStatus] != 9 " +
                "ORDER BY [PurchaseTypeName_fra]";
            Conexion.StartSession();
            DataTable myTb = Conexion.GDatos.BringDataTableSql(sql);
            Conexion.EndSession();
            Elements = CopyDataTable(myTb);
        }

        internal void GetWhs()
        {
            string sql = "SELECT [WarehouseID],[WarehouseCode],[WarehouseDesc] FROM " + clsGlobals.Silex + "[tblSXWarehouse] WHERE[WarehouseStatus] != 9 ORDER BY [WarehouseCode]";
            Conexion.StartSession();
            DataTable myTb = Conexion.GDatos.BringDataTableSql(sql);
            Conexion.EndSession();
            Elements = CopyDataTable(myTb);
        }

        internal void GetWhs(int activeProduct)
        {
            string sql = "SELECT DISTINCT WH.[WarehouseID],[WarehouseCode],[WarehouseDesc] FROM " + clsGlobals.Silex + "[tblSXWarehouse] AS WH " +
                "INNER JOIN " + clsGlobals.Silex + "[tblSXProductWarehouse] AS PW ON WH.WarehouseID=PW.WarehouseID INNER JOIN " + clsGlobals.Silex + "[tblSXProductColor] AS PC " +
                "ON PC.ProductID=PW.ProductID WHERE[WarehouseStatus] != 9 AND PC.ProductColorID=" + activeProduct + " ORDER BY [WarehouseCode]";
            Conexion.StartSession();
            DataTable myTb = Conexion.GDatos.BringDataTableSql(sql);
            Conexion.EndSession();
            Elements = CopyDataTable(myTb);
        }

        internal void RemoveItem(clsElement eTemp)
        {
            Elements.Remove(eTemp);
            Elements.Sort();
        }

        internal void SelectGroups()
        {
            //2018-07-12 12:07 Cambio de Requet
            clsGlobals.sql = "SELECT DISTINCT SX.[ProductGroupID] ,[GroupName_fra] ,[GroupDesc_fra] FROM " + clsGlobals.Gesin + "[tblGIScProduct] AS SC " +
                "INNER JOIN " + clsGlobals.Silex + "[tblSXProduct] AS SX ON SC.ProductID = SX.ProductID INNER JOIN " + clsGlobals.Silex + "[tblSXProductGroup] AS PG " +
                "ON SX.ProductGroupID = PG.ProductGroupID INNER JOIN " + clsGlobals.Gesin + "[tblGIScCollection] AS GI ON GI.CollectionID=SX.CollectionID " +
                "WHERE SC.ScenarioID=" + clsGlobals.GIPar.ScenarioID + " ORDER BY[GroupName_fra]";
            //clsGlobals.sql = "SELECT DISTINCT scSH.[ProductGroupID] ,scSH.GroupName, sxPG.GroupDesc_fra FROM[tblGIScSalesHistory] AS scSH " +
            //    "INNER JOIN[tblSXProductGroup] AS sxPG ON scSH.ProductGroupID=sxPG.ProductGroupID INNER JOIN[tblGIScCollection] AS giCL " +
            //    "ON (scSH.CollectionID=giCL.CollectionID AND scSH.ScenarioID=giCL.ScenarioID)INNER JOIN[tblGIScProduct] AS giPD " +
            //    "ON (scSH.ProductID=giPD.ProductID AND scSH.ScenarioID=giPD.ScenarioID)WHERE scSH.ScenarioID=" + clsGlobals.GIPar.ScenarioID +
            //    " ORDER BY scSH.GroupName";
        }

        internal void SelectStyles()
        {
            bool flag = true;
            //2018-07-13 9:15 cambio requet
            String sql = "SELECT DISTINCT [ShortProductCode] AS SPC, [ShortProductCode] ,[DefaultCostCardID] FROM " + clsGlobals.Gesin + "[tblGIScProduct] AS SC " +
                "INNER JOIN " + clsGlobals.Silex + "[tblSXProduct] AS SX ON SC.ProductID=SX.ProductID INNER JOIN " + clsGlobals.Gesin + "[tblGIScCollection] AS GI ON GI.CollectionID=SX.CollectionID " +
                "WHERE SC.ScenarioID=" + clsGlobals.GIPar.ScenarioID + " ";
            //String sql = "SELECT DISTINCT [ShortProductCode] AS SPC, [ShortProductCode], [ShortProductCode]FROM[tblGIScSalesHistory] AS scHS " +
            //    "INNER JOIN[tblGIScCollection] AS giCL ON (scHS.CollectionID = giCL.CollectionID AND scHS.ScenarioID=giCL.ScenarioID) " +
            //    "INNER JOIN[tblGIScProduct] AS giPD ON(scHS.ProductID = GIPD.ProductID AND scHS.ScenarioID = giPD.ScenarioID)WHERE scHS.ScenarioID=" 
            //    + clsGlobals.GIPar.ScenarioID + " ";

            foreach (clsElement ele in clsGlobals.ListGroups.Elements)
            {
                sql += flag ? "AND (SX.ProductGroupID=" + ele.ElementID + " " : "OR SX.ProductGroupID=" + ele.ElementID + " ";
                flag = false;
            }
            sql += flag ? "" : ")";
            sql += " ORDER BY SPC";
            clsGlobals.sql = sql;
        }

        internal void SelectColors()
        {
            bool flag = true;
            //2018-07-13 9:28 cambio requet
            string sql = "SELECT DISTINCT [ColorID], [ColorCode], [ColorName_fra]FROM " + clsGlobals.Silex + "[tblSXColor]" +
                "WHERE [ColorID] IN (SELECT DISTINCT SC.[ColorID] FROM " + clsGlobals.Gesin + "[tblGIScProduct] AS SC " +
                "INNER JOIN " + clsGlobals.Silex + "[tblSXProduct] AS SX ON SC.ProductID=SX.ProductID INNER JOIN " + clsGlobals.Gesin + "[tblGIScCollection] AS GI " +
                "ON GI.CollectionID=SX.CollectionID WHERE GI.ScenarioID= " + clsGlobals.GIPar.ScenarioID;
            //string sql = "SELECT DISTINCT scHS.[ColorID], [ColorCode], [ColorName_eng]FROM[tblGIScSalesHistory] AS scHS " +
            //    "INNER JOIN[tblGIScCollection] AS giCL ON (scHS.CollectionID = giCL.CollectionID AND scHS.ScenarioID=giCL.ScenarioID)" +
            //    "INNER JOIN[tblGIScProduct] AS giPD ON(scHS.ProductID = GIPD.ProductID AND scHS.ScenarioID = GICL.ScenarioID)WHERE scHS.ScenarioID = " + 
            //    clsGlobals.GIPar.ScenarioID + " ";
            foreach (clsElement ele in clsGlobals.ListGroups.Elements)
            {
                sql += flag ? "AND (SX.ProductGroupID=" + ele.ElementID + " " : "OR SX.ProductGroupID=" + ele.ElementID + " ";
                flag = false;
            }
            sql += flag ? "" : ")";

            flag = true;
            foreach (clsElement ele in clsGlobals.ListStyles.Elements)
            {
                sql += flag ? "AND (SX.ShortProductCode='" + ele.ElementID + "' " : "OR SX.ShortProductCode='" + ele.ElementID + "' ";
                flag = false;
            }
            sql += flag ? "" : ")";

            sql += ") ORDER BY [ColorCode]";
            clsGlobals.sql = sql;
        }

        internal void SelectCollections()
        {
            bool flag = true;
            //2018-08-14 14:05
            string sql = "SELECT DISTINCT GI.[CollectionID], [CollectionCode], [CollectionDesc] FROM " + clsGlobals.Gesin + "[tblGIScCollection] AS GI " +
                "INNER JOIN " + clsGlobals.Silex + "[tblSXCollection] AS CT ON GI.CollectionID = CT.CollectionID INNER JOIN " + clsGlobals.Silex + "[tblSXProduct] AS SX " +
                "ON CT.CollectionID = SX.CollectionID INNER JOIN " + clsGlobals.Gesin + "[tblGIScProduct] AS SC ON SC.ProductID = SX.ProductID " +
                "INNER JOIN " + clsGlobals.Silex + "[tblSXProductColor] AS PC ON SX.ProductID = PC.ProductID WHERE GI.ScenarioID = " + clsGlobals.GIPar.ScenarioID;
            //2018-07-13 9:44 cambio requet
            //string sql = "SELECT DISTINCT SX.[CollectionID], [CollectionCode], [CollectionDesc] FROM[tblGIScProduct] AS SC " +
            //    "INNER JOIN[tblSXProduct] AS SX ON SC.ProductID = SX.ProductID INNER JOIN[tblSXCollection] AS CT ON SX.CollectionID = CT.CollectionID " +
            //    "INNER JOIN[tblSXProductColor] PC ON SX.ProductID = PC.ProductID WHERE SC.ScenarioID = " + clsGlobals.GIPar.ScenarioID + " ";
            //string sql = "SELECT DISTINCT scHS.[CollectionID], scHS.[CollectionCode], [CollectionDesc] FROM[tblGIScSalesHistory] AS scHS " +
            //    "INNER JOIN[tblGIScCollection] AS giCL ON (scHS.CollectionID = giCL.CollectionID AND scHS.ScenarioID=giCL.ScenarioID)" +
            //    "INNER JOIN[tblGIScProduct] AS giPD ON(scHS.ProductID = GIPD.ProductID AND scHS.ScenarioID = giPD.ScenarioID)" +
            //    "INNER JOIN[tblSXCollection] AS CT ON scHS.CollectionID = CT.CollectionID WHERE scHS.ScenarioID=" + clsGlobals.GIPar.ScenarioID + " ";
            foreach (clsElement ele in clsGlobals.ListGroups.Elements)
            {
                sql += flag ? "AND (SX.ProductGroupID=" + ele.ElementID + " " : "OR SX.ProductGroupID=" + ele.ElementID + " ";
                flag = false;
            }
            sql += flag ? "" : ")";

            flag = true;
            foreach (clsElement ele in clsGlobals.ListStyles.Elements)
            {
                sql += flag ? "AND (SX.ShortProductCode='" + ele.ElementID + "' " : "OR SX.ShortProductCode='" + ele.ElementID + "' ";
                flag = false;
            }
            sql += flag ? "" : ")";

            flag = true;
            foreach (clsElement ele in clsGlobals.ListColors.Elements)
            {
                sql += flag ? "AND (PC.ColorID=" + ele.ElementID + " " : "OR PC.ColorID=" + ele.ElementID + " ";
                flag = false;
            }
            sql += flag ? "" : ")";

            sql += " ORDER BY [CollectionCode]";
            clsGlobals.sql = sql;
        }

        internal void GetListProductsWithVO()
        {
            string sql = "SELECT DISTINCT SC.[ProductColorID],[ProductCode],[ColorName_fra] " +
                "FROM " + clsGlobals.Gesin + "[tblGIScProduct] AS SC " +
                "INNER JOIN " + clsGlobals.Silex + "[tblSXProductColor] AS PC ON SC.ProductColorID=PC.ProductColorID " +
                "INNER JOIN " + clsGlobals.Silex + "[tblSXColor] AS CL ON PC.ColorID= CL.ColorID " +
                "INNER JOIN " + clsGlobals.Silex + "[tblSXProduct] AS PD ON PC.ProductID= PD.ProductID " +
                "WHERE [VOStatus]!=0 " +
                "ORDER BY [ProductCode], [ColorName_fra]";
            Conexion.StartSession();
            DataTable myTb = Conexion.GDatos.BringDataTableSql(sql);
            Conexion.EndSession();
            Elements = CopyDataTable(myTb);
        }
    }
}

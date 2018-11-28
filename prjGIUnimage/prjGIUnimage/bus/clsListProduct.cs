using prjGIUnimage.data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjGIUnimage.bus
{
    class clsListProduct
    {
        List<clsProduct> myList;

        public clsListProduct()
        {
            this.myList = new List<clsProduct>();
        }

        public int Quantity
        {
            get => myList.Count;
        }

        public List<clsProduct> Elements
        {
            get => myList;
            set => myList = value;
        }

        public void AllProducts()
        {
            string sql = "SELECT [ProductColorStatus],[GIProductID],SXPclr.[ProductColorID],GIPro.[ProductID],GIPro.[ColorID],SXPro.[ProductCode]," +
                "[ColorName_fra],SXCol.[CollectionName],SXPgr.[GroupName_fra],SXPro.[ShortProductCode],SXPro.[ProductDesc_fra],[DataDesc_fra]," +
                "[GIProductStatus],[ProductComment],[SurplusRate],GIPro.[CreatedByUserID],GIPro.[ModifiedByUserID],GIPro.[DeletedByUserID]," +
                "GIPro.[CreatedDate],GIPro.[ModifiedDate],GIPro.[DeletedDate] FROM " + clsGlobals.Gesin + "[tblGIProduct] AS GIPro INNER JOIN " + clsGlobals.Silex + "[tblSXProduct] AS SXPro " +
                "ON GIPro.[ProductID]=SXPro.[ProductID] INNER JOIN " + clsGlobals.Silex + "[tblSXColor] AS SXClr ON GIPro.[ColorID]=SXClr.[ColorID] " +
                "INNER JOIN " + clsGlobals.Silex + "[tblSXProductColor] AS SXPclr ON (GIPro.ProductID=SXPclr.ProductID AND GIPro.ColorID=SXPclr.ColorID) " +
                "INNER JOIN " + clsGlobals.Silex + "[tblSXCollection] AS SXCol ON SXPro.[CollectionID]=SXCol.[CollectionID] INNER JOIN " + clsGlobals.Silex + "[tblSXProductGroup] AS SXPgr " +
                "ON SXPro.ProductGroupID=SXPgr.ProductGroupID INNER JOIN " + clsGlobals.Silex + "[tblSXData] AS SXDat ON SXDat.[DataValue]=SXPclr.[ProductColorStatus] " +
                "AND [DataGroupID]=119 AND SXPclr.[ProductColorStatus] !=9 ORDER BY [ProductCode]";
            //Conexion.StartSession();
            DataTable myTb = Conexion.GDatos.GetDataTableSql(sql);
            Conexion.EndSession();
            Elements = CopyDataTable(myTb);
        }

        internal void AllProducts(string text, object selectedValue)
        {
            string sql = "SELECT [ProductColorStatus],[GIProductID],SXPclr.[ProductColorID],GIPro.[ProductID],GIPro.[ColorID],SXPro.[ProductCode],[ColorName_fra]," +
                "SXCol.[CollectionName],SXPgr.[GroupName_fra],SXPro.[ShortProductCode],SXPro.[ProductDesc_fra],[DataDesc_fra],[GIProductStatus]," +
                "[ProductComment],[SurplusRate],GIPro.[CreatedByUserID],GIPro.[ModifiedByUserID],GIPro.[DeletedByUserID],GIPro.[CreatedDate]," +
                "GIPro.[ModifiedDate],GIPro.[DeletedDate] FROM " + clsGlobals.Gesin + "[tblGIProduct] AS GIPro INNER JOIN " + clsGlobals.Silex + "[tblSXProduct] AS SXPro " +
                "ON GIPro.[ProductID]=SXPro.[ProductID] INNER JOIN " + clsGlobals.Silex + "[tblSXColor] AS SXClr ON GIPro.[ColorID]=SXClr.[ColorID] " +
                "INNER JOIN " + clsGlobals.Silex + "[tblSXProductColor] AS SXPclr ON (GIPro.ProductID=SXPclr.ProductID AND GIPro.ColorID=SXPclr.ColorID) " +
                "INNER JOIN " + clsGlobals.Silex + "[tblSXCollection] AS SXCol ON SXPro.[CollectionID]=SXCol.[CollectionID] INNER JOIN " + clsGlobals.Silex + "[tblSXProductGroup] AS SXPgr " +
                "ON SXPro.ProductGroupID=SXPgr.ProductGroupID INNER JOIN " + clsGlobals.Silex + "[tblSXData] AS SXDat ON SXDat.[DataValue]=SXPclr.[ProductColorStatus] " +
                "AND [DataGroupID]=119 ";
            if (Convert.ToInt32(selectedValue) != 5)
            {
                sql += "AND GIProductStatus=" + selectedValue + " ";
            }
            sql += "AND SXPclr.[ProductColorStatus] !=9 AND SXPro.[ProductCode] LIKE '%" +
                text + "%' ORDER BY [ProductCode]";
            //Conexion.StartSession();
            DataTable myTb = Conexion.GDatos.GetDataTableSql(sql);
            Conexion.EndSession();
            Elements = CopyDataTable(myTb);
        }

        private List<clsProduct> CopyDataTable(DataTable myTb)
        {
            List<clsProduct> lstTem = new List<clsProduct>();
            foreach (DataRow rw in myTb.Rows)
            {
                clsProduct myPro = new clsProduct();
                myPro.CopyDatarow(rw);
                lstTem.Add(myPro);
            }
            return lstTem;
        }

        public void FilterListBy(string text, object selectedValue)
        {
            string sql = "SELECT [GIProductID], GIPro.[ProductID], GIPro.[ColorID], SXPro.[ProductCode], SXCol.[CollectionName], " +
                "SXPgr.[GroupName_fra], SXPro.[ShortProductCode], SXPro.[ProductDesc_fra], [DataDesc_fra], [GIProductStatus],[ProductComment],[SurplusRate],GIPro.[CreatedByUserID]," +
                "GIPro.[ModifiedByUserID],GIPro.[DeletedByUserID],GIPro.[CreatedDate],GIPro.[ModifiedDate],GIPro.[DeletedDate] " +
                "FROM " + clsGlobals.Gesin + "[tblGIProduct] AS GIPro INNER JOIN " + clsGlobals.Silex + "[tblSXProduct] as SXPro ON GIPro.[ProductID]=SXPro.[ProductID] INNER JOIN " + clsGlobals.Silex + "[tblSXCollection] AS SXCol " +
                "ON SXPro.[CollectionID]=SXCol.[CollectionID] INNER JOIN " + clsGlobals.Silex + "[tblSXProductGroup] AS SXPgr ON SXPro.ProductGroupID=SXPgr.ProductGroupID " +
                "INNER JOIN " + clsGlobals.Silex + "[tblSXData] AS SXDat ON SXDat.[DataValue]=SXPro.ProductStatus AND [DataGroupID]=168 AND GIProductStatus=" + selectedValue +
                " AND ProductCode like '%" + text + "%' ORDER BY [ProductCode]";
            //Conexion.StartSession();
            DataTable myTb = Conexion.GDatos.GetDataTableSql(sql);
            Conexion.EndSession();
            Elements = CopyDataTable(myTb);
        }

        internal void GetActiveElements()
        {
            string sql = "SELECT * FROM " + clsGlobals.Gesin + "[tblGIProduct]WHERE[GIProductStatus]!=1";
            //Conexion.StartSession();
            DataTable myTb = Conexion.GDatos.GetDataTableSql(sql);
            Conexion.EndSession();
            Elements = CopyDataTableSimple(myTb);
        }

        private List<clsProduct> CopyDataTableSimple(DataTable myTb)
        {
            List<clsProduct> lstTem = new List<clsProduct>();
            foreach (DataRow rw in myTb.Rows)
            {
                clsProduct myPro = new clsProduct();
                myPro.CopyDataRowSimple(rw);
                lstTem.Add(myPro);
            }
            return lstTem;
        }

        internal void AllSXProducts()
        {
            string sql = "SELECT P.[ProductID],C.[ColorID], PC.ProductColorID, [ProductColorStatus] " +
                "FROM " + clsGlobals.Silex + "[tblSXProduct] AS P " +
                "INNER JOIN " + clsGlobals.Silex + "[tblSXProductColor] AS PC ON P.ProductID=PC.ProductID " +
                "INNER JOIN " + clsGlobals.Silex + "[tblSXColor] AS C ON PC.[ColorID]= C.[ColorID] " +
                "WHERE[ProductStatus]!=9 AND[VirtualStatus]= 0 AND[ProductColorStatus]!=9";
            
            //Conexion.StartSession();
            DataTable myTb = Conexion.GDatos.GetDataTableSql(sql);
            Conexion.EndSession();
            Elements = CopySXDataTable(myTb);
        }

        internal clsProduct ElementByID(int current)
        {
            foreach (clsProduct it in myList)
            {
                if (it.GIProductID == current)
                {
                    return it;
                }
            }
            return new clsProduct();
        }

        private List<clsProduct> CopySXDataTable(DataTable myTb)
        {
            List<clsProduct> lstTem = new List<clsProduct>();
            foreach (DataRow rw in myTb.Rows)
            {
                clsProduct myPro = new clsProduct();
                myPro.CopySXDatarow(rw);
                lstTem.Add(myPro);
            }
            return lstTem;
        }

        internal bool Exists(clsProduct ele)
        {
            return Elements.Contains(ele);
        }
    }
}

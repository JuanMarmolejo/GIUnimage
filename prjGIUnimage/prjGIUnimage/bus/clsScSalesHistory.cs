using prjGIUnimage.data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjGIUnimage.bus
{
    class clsScSalesHistory
    {
        public int ScSalesHistoryID { get; set; }
        public int SeasonID { get; set; }
        public int CollectionID { get; set; }
        public int ProductID { get; set; }
        public int ProductColorID { get; set; }
        public int ProductDimID { get; set; }
        public int ProductCatID { get; set; }
        public int SizeOrder { get; set; }
        public int ColorID { get; set; }
        public int DimID { get; set; }
        public int CatID { get; set; }
        public int ProductGroupID { get; set; }
        public int ProductSubGroupID { get; set; }
        public string CollectionName { get; set; }
        public string CollectionCode { get; set; }
        public string ProductCode { get; set; }
        public string ShortProductCode { get; set; }
        public string ProductDesc_eng { get; set; }
        public string GroupName { get; set; }
        public string SubGroupName { get; set; }
        public string ColorCode { get; set; }
        public string ColorName_eng { get; set; }
        public string DimCode { get; set; }
        public string SizeDesc { get; set; }
        public string CatCode { get; set; }
        public string ProductStatusDesc { get; set; }
        public string ProductColorStatusDesc { get; set; }
        public string BarcodeSKU { get; set; }
        public double QtyVI1 { get; set; }
        public double QtyCI1a { get; set; }
        public double QtyCI1b { get; set; }
        public double QtyCI1ToInvoice { get; set; }
        public double QtyVOOpen1 { get; set; }
        public double QtyCO1 { get; set; }
        public double QtyVI2 { get; set; }
        public double QtyCI2a { get; set; }
        public double QtyCI2b { get; set; }
        public double QtyCI2ToInvoice { get; set; }
        public double QtyVOOpen2 { get; set; }
        public double QtyCO2 { get; set; }
        public double QtyStock { get; set; }
        public double QtyStock2 { get; set; }
        public double QtyPick { get; set; }
        public double QtyPack { get; set; }
        public double DefEstPrice { get; set; }
        public double DefFOBPrice { get; set; }
        public double SalesPrice { get; set; }
        public double RetailPrice { get; set; }
        public double AmountCI1 { get; set; }
        public double AmountCI2 { get; set; }
        public double QtyInvBF { get; set; }
        public double QtyInvAF { get; set; }
        public double QtyAchat { get; set; }
        public double VOStatus { get; set; }

        internal void CopyDataRow(DataRow rw)
        {
            this.AmountCI1 = Convert.ToDouble(rw["AmountCI1"]);
            this.AmountCI2 = Convert.ToDouble(rw["AmountCI2"]);
            this.BarcodeSKU = Convert.ToString(rw["BarcodeSKU"]);
            this.CatCode = Convert.ToString(rw["CatCode"]);
            this.CatID = Convert.ToInt32(rw["CatID"]);
            this.CollectionCode = Convert.ToString(rw["CollectionCode"]);
            this.CollectionID = Convert.ToInt32(rw["CollectionID"]);
            this.CollectionName = Convert.ToString(rw["CollectionName"]);
            this.ColorCode = Convert.ToString(rw["ColorCode"]);
            this.ColorID = Convert.ToInt32(rw["ColorID"]);
            this.ColorName_eng = Convert.ToString(rw["ColorName_eng"]);
            this.DefEstPrice = string.IsNullOrEmpty(rw["DefEstPrice"].ToString()) ? 0 : Convert.ToDouble(rw["DefEstPrice"]);
            this.DefFOBPrice = string.IsNullOrEmpty(rw["DefFOBPrice"].ToString()) ? 0 : Convert.ToDouble(rw["DefFOBPrice"]);
            this.DimCode = Convert.ToString(rw["DimCode"]);
            this.DimID = Convert.ToInt32(rw["DimID"]);
            this.GroupName = Convert.ToString(rw["GroupName"]);
            this.ProductCatID = Convert.ToInt32(rw["ProductCatID"]);
            this.ProductCode = Convert.ToString(rw["ProductCode"]);
            this.ProductColorID = Convert.ToInt32(rw["ProductColorID"]);
            this.ProductColorStatusDesc = Convert.ToString(rw["ProductColorStatusDesc"]);
            this.ProductDesc_eng = Convert.ToString(rw["ProductDesc_eng"]);
            this.ProductDimID = Convert.ToInt32(rw["ProductDimID"]);
            this.ProductGroupID = Convert.ToInt32(rw["ProductGroupID"]);
            this.ProductID = Convert.ToInt32(rw["ProductID"]);
            this.ProductStatusDesc = Convert.ToString(rw["ProductStatusDesc"]);
            this.ProductSubGroupID = Convert.ToInt32(rw["ProductSubGroupID"]);
            this.QtyAchat = string.IsNullOrEmpty(rw["QtyAchat"].ToString()) ? 0 : Convert.ToDouble(rw["QtyAchat"]);
            this.QtyCI1a = Convert.ToDouble(rw["QtyCI1a"]);
            this.QtyCI1b = Convert.ToDouble(rw["QtyCI1b"]);
            this.QtyCI1ToInvoice = Convert.ToDouble(rw["QtyCI1ToInvoice"]);
            this.QtyCI2a = Convert.ToDouble(rw["QtyCI2a"]);
            this.QtyCI2b = Convert.ToDouble(rw["QtyCI2b"]);
            this.QtyCI2ToInvoice = Convert.ToDouble(rw["QtyCI2ToInvoice"]);
            this.QtyCO1 = Convert.ToDouble(rw["QtyCO1"]);
            this.QtyCO2 = Convert.ToDouble(rw["QtyCO2"]);
            this.QtyInvAF = string.IsNullOrEmpty(rw["QtyInvAF"].ToString()) ? 0 : Convert.ToDouble(rw["QtyInvAF"]);
            this.QtyInvBF = string.IsNullOrEmpty(rw["QtyInvBF"].ToString()) ? 0 : Convert.ToDouble(rw["QtyInvBF"]);
            this.QtyPack = Convert.ToDouble(rw["QtyPack"]);
            this.QtyPick = Convert.ToDouble(rw["QtyPick"]);
            this.QtyStock = Convert.ToDouble(rw["QtyStock"]);
            this.QtyStock2 = Convert.ToDouble(rw["QtyStock2"]);
            this.QtyVI1 = Convert.ToDouble(rw["QtyVI1"]);
            this.QtyVI2 = Convert.ToDouble(rw["QtyVI2"]);
            this.QtyVOOpen1 = Convert.ToDouble(rw["QtyVOOpen1"]);
            this.QtyVOOpen2 = Convert.ToDouble(rw["QtyVOOpen2"]);
            this.RetailPrice = string.IsNullOrEmpty(rw["RetailPrice"].ToString()) ? 0 : Convert.ToDouble(rw["RetailPrice"]);
            this.SalesPrice = string.IsNullOrEmpty(rw["SalesPrice"].ToString()) ? 0 : Convert.ToDouble(rw["SalesPrice"]);
            this.ScSalesHistoryID = Convert.ToInt32(rw["ScSalesHistoryID"]);
            this.SeasonID = Convert.ToInt32(rw["SeasonID"]);
            this.ShortProductCode = Convert.ToString(rw["ShortProductCode"]);
            this.SizeDesc = Convert.ToString(rw["SizeDesc"]);
            this.SizeOrder = Convert.ToInt32(rw["SizeOrder"]);
            this.SubGroupName = Convert.ToString(rw["SubGroupName"]);
            this.VOStatus = string.IsNullOrEmpty(rw["VOStatus"].ToString()) ? 0 : Convert.ToDouble(rw["VOStatus"]);
        }

        internal double GetInvBF()
        {
            string sql = "SELECT [QtyStockBF] FROM " + clsGlobals.Gesin + "[tblGIInvBeforeFiscal] WHERE[SeasonID]=" + this.SeasonID +
                " AND [CollectionID]=" + this.CollectionID + " AND [ProductID]=" + this.ProductID + " AND [ProductColorID]=" +
                this.ProductColorID + " AND [ProductDimID]=" + this.ProductDimID + " AND [ProductCatID]=" + this.ProductCatID +
                " AND [SizeDesc]='" + this.SizeDesc + "' AND [ColorID]=" + this.ColorID + " AND [DimID]=" + this.DimID + " AND [CatID]=" +
                this.CatID + " AND [ProductGroupID]=" + this.ProductGroupID + " AND [ProductSubGroupID]=" + this.ProductSubGroupID + "";
            Conexion.StartSession();
            double Qty = (double)Conexion.GDatos.BringScalarValueSql(sql);
            Conexion.EndSession();
            return Qty;
        }

        internal double GetInvAF()
        {
            string sql = "SELECT [QtyStockAF] FROM " + clsGlobals.Gesin + "[tblGIInvAfterFiscal] WHERE[SeasonID]=" + this.SeasonID +
                " AND [CollectionID]=" + this.CollectionID + " AND [ProductID]=" + this.ProductID + " AND [ProductColorID]=" +
                this.ProductColorID + " AND [ProductDimID]=" + this.ProductDimID + " AND [ProductCatID]=" + this.ProductCatID +
                " AND [SizeDesc]='" + this.SizeDesc + "' AND [ColorID]=" + this.ColorID + " AND [DimID]=" + this.DimID + " AND [CatID]=" +
                this.CatID + " AND [ProductGroupID]=" + this.ProductGroupID + " AND [ProductSubGroupID]=" + this.ProductSubGroupID + "";
            Conexion.StartSession();
            double Qty = (double)Conexion.GDatos.BringScalarValueSql(sql);
            Conexion.EndSession();
            return Qty;
        }

        internal static DataTable GetVirtualProducts(int scenarioID)
        {
            string sql = "SELECT * FROM[sxUnimageDevGI].[dbo].[tblGIreqVirtual] WHERE[CatCode] = 'REG' AND[ScenarioID] = " + scenarioID;
            Conexion.StartSession();
            DataTable myTb = Conexion.GDatos.BringDataTableSql(sql);
            Conexion.EndSession();
            return myTb;
        }

        internal void CopyVirtualDataRow(DataRow rw)
        {
            this.AmountCI1 = Convert.ToDouble(rw["AmountCI1"]);
            this.AmountCI2 = Convert.ToDouble(rw["AmountCI2"]);
            this.BarcodeSKU = Convert.ToString(rw["BarcodeSKU"]);
            this.CatCode = Convert.ToString(rw["CatCode"]);
            this.CatID = Convert.ToInt32(rw["CatID"]);
            this.CollectionCode = Convert.ToString(rw["VirtualCollectionCode"]);
            this.CollectionID = Convert.ToInt32(rw["VirtualCollectionID"]);
            this.CollectionName = Convert.ToString(rw["VirtualCollectionName"]);
            this.ColorCode = Convert.ToString(rw["ColorCode"]);
            this.ColorID = Convert.ToInt32(rw["ColorID"]);
            this.ColorName_eng = Convert.ToString(rw["ColorName_eng"]);
            this.DefEstPrice = string.IsNullOrEmpty(rw["DefEstPrice"].ToString()) ? 0 : Convert.ToDouble(rw["DefEstPrice"]);
            this.DefFOBPrice = string.IsNullOrEmpty(rw["DefFOBPrice"].ToString()) ? 0 : Convert.ToDouble(rw["DefFOBPrice"]);
            this.DimCode = Convert.ToString(rw["DimCode"]);
            this.DimID = Convert.ToInt32(rw["DimID"]);
            this.GroupName = Convert.ToString(rw["GroupName"]);
            this.ProductCatID = Convert.ToInt32(rw["VirtualProductCatID"]);
            this.ProductCode = Convert.ToString(rw["VirtualProductCode"]);
            this.ProductColorID = Convert.ToInt32(rw["VirtualProductColorID"]);
            this.ProductColorStatusDesc = Convert.ToString(rw["ProductColorStatusDesc"]);
            this.ProductDesc_eng = Convert.ToString(rw["ProductDesc_eng"]);
            this.ProductDimID = Convert.ToInt32(rw["VirtualProductDimID"]);
            this.ProductGroupID = string.IsNullOrEmpty(rw["ProductGroupID"].ToString()) ? 0 : Convert.ToInt32(rw["ProductGroupID"]);
            this.ProductID = Convert.ToInt32(rw["VirtualProductID"]);
            this.ProductStatusDesc = Convert.ToString(rw["ProductStatusDesc"]);
            this.ProductSubGroupID = string.IsNullOrEmpty(rw["ProductSubGroupID"].ToString()) ? 0 : Convert.ToInt32(rw["ProductSubGroupID"]);
            this.QtyAchat = 0;
            this.QtyCI1a = Convert.ToDouble(rw["QtyCI1a"]);
            this.QtyCI1b = Convert.ToDouble(rw["QtyCI1b"]);
            this.QtyCI1ToInvoice = Convert.ToDouble(rw["QtyCI1ToInvoice"]);
            this.QtyCI2a = Convert.ToDouble(rw["QtyCI2a"]);
            this.QtyCI2b = Convert.ToDouble(rw["QtyCI2b"]);
            this.QtyCI2ToInvoice = Convert.ToDouble(rw["QtyCI2ToInvoice"]);
            this.QtyCO1 = Convert.ToDouble(rw["QtyCO1"]);
            this.QtyCO2 = Convert.ToDouble(rw["QtyCO2"]);
            this.QtyInvAF = 0;
            this.QtyInvBF = 0;
            this.QtyPack = Convert.ToDouble(rw["QtyPack"]);
            this.QtyPick = Convert.ToDouble(rw["QtyPick"]);
            this.QtyStock = 0;
            this.QtyStock2 = 0;
            this.QtyVI1 = 0;
            this.QtyVI2 = 0;
            this.QtyVOOpen1 = 0;
            this.QtyVOOpen2 = 0;
            this.RetailPrice = string.IsNullOrEmpty(rw["RetailPrice"].ToString()) ? 0 : Convert.ToDouble(rw["RetailPrice"]);
            this.SalesPrice = string.IsNullOrEmpty(rw["SalesPrice"].ToString()) ? 0 : Convert.ToDouble(rw["SalesPrice"]);
            //this.ScSalesHistoryID = Convert.ToInt32(rw["ScSalesHistoryID"]);
            //this.SeasonID = Convert.ToInt32(rw["SeasonID"]);
            this.ShortProductCode = Convert.ToString(rw["VirtualShortProductCode"]);
            this.SizeDesc = Convert.ToString(rw["SizeDesc"]);
            this.SizeOrder = Convert.ToInt32(rw["VirtualSizeOrder"]);
            this.SubGroupName = Convert.ToString(rw["SubGroupName"]);
            this.VOStatus = 0;
        }

        internal static void SavePurchasedQuantities(clsScVorder vorder, clsListScVorderDetail vorderDetail)
        {
            foreach (clsScVorderDetail ele in vorderDetail.Elements)
            {
                int ScSalesHistoryID = GetIdByVendorOrder(clsGlobals.GIPar.ScenarioID, ele);
                UpdateQtyAchhat(ScSalesHistoryID, ele.OrderQty);
            }
        }

        private static void UpdateQtyAchhat(int scSalesHistoryID, double orderQty)
        {
            string sql = "UPDATE " + clsGlobals.Gesin + "[tblGIScSalesHistory] " +
                "SET[QtyAchat] = " + orderQty + " " +
                "WHERE[ScSalesHistoryID] = " + scSalesHistoryID;
            Conexion.StartSession();
            Conexion.GDatos.RunSql(sql);
            Conexion.EndSession();
        }

        internal void UpdateOneItems(int productEquiID)
        {
            clsScSalesHistory myItemEqui = new clsScSalesHistory();
            if (myItemEqui.GetEquivalentItem(this, productEquiID))
            {

                this.QtyVI1 = myItemEqui.QtyVI1;
                this.QtyCI1a = myItemEqui.QtyCI1a;
                this.QtyCI1b = myItemEqui.QtyCI1b;
                this.QtyCI1ToInvoice = myItemEqui.QtyCI1ToInvoice;
                this.QtyVOOpen1 = myItemEqui.QtyVOOpen1;
                this.QtyCO1 = myItemEqui.QtyCO1;

                this.UpdateQuantities();
            }
        }

        internal void UpdateTwoItems(int productEquiID)
        {
            clsScSalesHistory myItemEqui = new clsScSalesHistory();
            if(myItemEqui.GetEquivalentItem(this, productEquiID))
            {

                this.QtyVI1 = myItemEqui.QtyVI1;
                this.QtyCI1a = myItemEqui.QtyCI1a;
                this.QtyCI1b = myItemEqui.QtyCI1b;
                this.QtyCI1ToInvoice = myItemEqui.QtyCI1ToInvoice;
                this.QtyVOOpen1 = myItemEqui.QtyVOOpen1;
                this.QtyCO1 = myItemEqui.QtyCO1;

                this.QtyVI2 = myItemEqui.QtyVI2;
                this.QtyCI2a = myItemEqui.QtyCI2a;
                this.QtyCI2b = myItemEqui.QtyCI2b;
                this.QtyCI2ToInvoice = myItemEqui.QtyCI2ToInvoice;
                this.QtyVOOpen2 = myItemEqui.QtyVOOpen2;
                this.QtyCO2 = myItemEqui.QtyCO2;
                this.UpdateQuantities();
            }
        }

        private void UpdateQuantities()
        {
            string sql = "UPDATE [sxUnimageDevGI].[dbo].[tblGIScSalesHistory] " +
                "SET [QtyVI1]=" + this.QtyVI1 + ",[QtyCI1a]=" + this.QtyCI1a + ",[QtyCI1b]=" + this.QtyCI1b + ",[QtyCI1ToInvoice]=" + this.QtyCI1ToInvoice + 
                ",[QtyVOOpen1]=" + this.QtyVOOpen1 + ",[QtyCO1]=" + this.QtyCO1 + ",[QtyVI2]=" + this.QtyVI2 + ",[QtyCI2a]=" + this.QtyCI2a + 
                ",[QtyCI2b]=" + this.QtyCI2b + ",[QtyCI2ToInvoice]=" + this.QtyCI2ToInvoice + ",[QtyVOOpen2]=" + this.QtyVOOpen2 + ",[QtyCO2]=" + this.QtyCO2 + " " +
                "WHERE [ScSalesHistoryID]= " + this.ScSalesHistoryID;
            Conexion.StartSession();
            Conexion.GDatos.RunSql(sql);
            Conexion.EndSession();
        }

        private bool GetEquivalentItem(clsScSalesHistory proBase, int productEquiID)
        {
            string sql = "SELECT [ScSalesHistoryID],[ScenarioID],[SeasonID],[CollectionID],SH.[ProductID],SH.[ProductColorID],[ProductDimID]," +
                "[ProductCatID],[SizeOrder],SH.[ColorID],[DimID],[CatID],[ProductGroupID],[ProductSubGroupID],[CollectionName],[CollectionCode]," +
                "[ProductCode],[ShortProductCode],[ProductDesc_eng],[GroupName],[SubGroupName],[ColorCode],[ColorName_eng],[DimCode],[SizeDesc]," +
                "[CatCode],[ProductStatusDesc],[ProductColorStatusDesc],[BarcodeSKU],[QtyVI1],[QtyCI1a],[QtyCI1b],[QtyCI1ToInvoice],[QtyVOOpen1]," +
                "[QtyCO1],[QtyVI2],[QtyCI2a],[QtyCI2b],[QtyCI2ToInvoice],[QtyVOOpen2],[QtyCO2],[QtyStock],[QtyStock2],[QtyPick],[QtyPack]," +
                "[DefEstPrice],[DefFOBPrice],[SalesPrice],[RetailPrice],[AmountCI1],[AmountCI2],[QtyInvBF],[QtyInvAF],[QtyAchat],[VOStatus] " +
                "FROM [sxUnimageDevGI].[dbo].[tblGIScSalesHistory] AS SH " +
                "INNER JOIN [sxUnimageDevGI].[dbo].[tblGIProduct] AS GIP " +
                "ON GIP.ProductColorID=SH.ProductColorID " +
                "WHERE[GIProductID]= " + productEquiID + " " +
                "AND [CatCode]= '" + proBase.CatCode + "' " +
                "AND [ScenarioID]= " + clsGlobals.GIPar.ScenarioID + " " +
                "AND [DimID]= " + proBase.DimID + " " +
                "AND [SizeDesc]= '" + proBase.SizeDesc + "'";
            Conexion.StartSession();
            DataTable myTb = Conexion.GDatos.BringDataTableSql(sql);
            Conexion.EndSession();
            if (myTb.Rows.Count > 0)
            {
                this.CopyDataRow(myTb.Rows[0]);
                return true;
            }
            else
            {
                return false;
            }
        }

        private static int GetIdByVendorOrder(int scenarioID, clsScVorderDetail ele)
        {
            string sql = "SELECT [ScSalesHistoryID] " +
                "FROM " + clsGlobals.Gesin + "[tblGIScSalesHistory] " +
                "WHERE[ScenarioID] = " + scenarioID + " " +
                "AND[ProductColorID] = " + ele.ProductColorID + " " +
                "AND[ProductCatID] = " + ele.ProductCatID + " " +
                "AND[SizeDesc] = '" + ele.Size + "'";
            //string sql = "SELECT [ScSalesHistoryID] FROM " + clsGlobals.Gesin + "[tblGIScSalesHistory] " +
            //    "WHERE[ScenarioID] = " + scenarioID + " AND[ProductID] = " + ele.ProductID + " AND[ProductColorID] = " +
            //    ele.ProductColorID + " AND[ProductDimID] = " + ele.ProductDimID + " AND[ProductCatID] = " + ele.ProductCatID +
            //    " AND[DimID] = " + ele.DimID + " AND[CatID] = " + ele.CatID + " AND[ProductGroupID] = " + ele.ProductGroupID +
            //    " AND[ProductSubGroupID] = " + ele.ProductSubGroupID + " AND[ColorID] = " + ele.ColorID + " AND[SizeDesc] = '" + ele.Size + "'";
            Conexion.StartSession();
            int tmpID = (int)Conexion.GDatos.BringScalarValueSql(sql);
            Conexion.EndSession();
            return tmpID;
        }

        internal void InsertTotblGIScSalesHistory(int sXSeasonID)
        {
            string productDesc_eng = this.ProductDesc_eng.Replace('\'',' ');
            string sql = "INSERT INTO [sxUnimageDevGI].[dbo].[tblGIScSalesHistory] ([ScenarioID],[SeasonID],[CollectionID],[ProductID],[ProductColorID]," +
                "[ProductDimID],[ProductCatID],[SizeOrder],[ColorID],[DimID],[CatID],[ProductGroupID],[ProductSubGroupID],[CollectionName],[CollectionCode]," +
                "[ProductCode],[ShortProductCode],[ProductDesc_eng],[GroupName],[SubGroupName],[ColorCode],[ColorName_eng],[DimCode],[SizeDesc],[CatCode]," +
                "[ProductStatusDesc],[ProductColorStatusDesc],[BarcodeSKU],[QtyVI1],[QtyCI1a],[QtyCI1b],[QtyCI1ToInvoice],[QtyVOOpen1],[QtyCO1],[QtyVI2]," +
                "[QtyCI2a],[QtyCI2b],[QtyCI2ToInvoice],[QtyVOOpen2],[QtyCO2],[QtyStock],[QtyStock2],[QtyPick],[QtyPack],[DefEstPrice],[DefFOBPrice]," +
                "[SalesPrice],[RetailPrice],[AmountCI1],[AmountCI2],[QtyInvBF],[QtyInvAF],[QtyAchat],[VOStatus])" +
                "VALUES(" + clsGlobals.NextScenarioID + "," + sXSeasonID + "," + this.CollectionID + "," + this.ProductID + "," + 
                this.ProductColorID + "," + this.ProductDimID + "," + this.ProductCatID + "," + this.SizeOrder + "," + this.ColorID + "," + 
                this.DimID + "," + this.CatID + "," + this.ProductGroupID + "," + this.ProductSubGroupID + ",'" + this.CollectionName + "','" + 
                this.CollectionCode + "','" + this.ProductCode + "','" + this.ShortProductCode + "','" + productDesc_eng + "','" + 
                this.GroupName + "','" + this.SubGroupName + "','" + this.ColorCode + "','" + this.ColorName_eng + "','" + this.DimCode + "','" + 
                this.SizeDesc + "','" + this.CatCode + "','" + this.ProductStatusDesc + "','" + this.ProductColorStatusDesc + "','" + 
                this.BarcodeSKU + "'," + this.QtyVI1 + "," + this.QtyCI1a + "," + this.QtyCI1b + "," + this.QtyCI1ToInvoice + "," + this.QtyVOOpen1 + "," + 
                this.QtyCO1 + "," + this.QtyVI2 + "," + this.QtyCI2a + "," + this.QtyCI2b + "," + this.QtyCI2ToInvoice + "," + this.QtyVOOpen2 + "," + 
                this.QtyCO2 + "," + this.QtyStock + "," + this.QtyStock2 + "," + this.QtyPick + "," + this.QtyPack + "," + 
                this.DefEstPrice.ToString("R", System.Globalization.CultureInfo.CreateSpecificCulture("en-US")) + "," + 
                this.DefFOBPrice.ToString("R", System.Globalization.CultureInfo.CreateSpecificCulture("en-US")) + "," + 
                this.SalesPrice.ToString("R", System.Globalization.CultureInfo.CreateSpecificCulture("en-US")) + "," + 
                this.RetailPrice.ToString("R", System.Globalization.CultureInfo.CreateSpecificCulture("en-US")) + "," + 
                this.AmountCI1.ToString("R", System.Globalization.CultureInfo.CreateSpecificCulture("en-US")) + "," + 
                this.AmountCI2.ToString("R", System.Globalization.CultureInfo.CreateSpecificCulture("en-US")) + "," + 
                this.QtyInvBF + "," + this.QtyInvAF + "," + this.QtyAchat + "," + this.VOStatus + ")";

            Conexion.StartSession();
            Conexion.GDatos.RunSql(sql);
            Conexion.EndSession();
        }
    }
}

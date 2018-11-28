using prjGIUnimage.data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjGIUnimage.bus
{
    class clsListScSalesHistory
    {
        List<clsScSalesHistory> myList;

        public clsListScSalesHistory()
        {
            this.myList = new List<clsScSalesHistory>();
        }

        public int Quantity
        {
            get => myList.Count;
        }

        public List<clsScSalesHistory> Elements
        {
            get => myList;
            set => myList = value;
        }

        internal void GetSalesHistoryByID(int activeScenario, int activeProduct)
        {
            string sql = "SELECT [ScSalesHistoryID], SH.[ScenarioID],[SeasonID],[CollectionID],SH.[ProductID],[ProductColorID],[ProductDimID],[ProductCatID],[SizeOrder]," +
                "SH.[ColorID],[DimID],[CatID],[ProductGroupID],[ProductSubGroupID],[CollectionName],[CollectionCode],[ProductCode],[ShortProductCode]," +
                "[ProductDesc_eng],[GroupName],[SubGroupName],[ColorCode],[ColorName_eng],[DimCode],[SizeDesc],[CatCode],[ProductStatusDesc]," +
                "[ProductColorStatusDesc],[BarcodeSKU],[QtyVI1],[QtyCI1a],[QtyCI1b],[QtyCI1ToInvoice],[QtyVOOpen1],[QtyCO1],[QtyVI2],[QtyCI2a]," +
                "[QtyCI2b],[QtyCI2ToInvoice],[QtyVOOpen2],[QtyCO2],[QtyStock],[QtyStock2],[QtyPick],[QtyPack],[DefEstPrice],[DefFOBPrice],[SalesPrice]," +
                "[RetailPrice],[AmountCI1],[AmountCI2],[QtyInvBF],[QtyInvAF],[QtyAchat],[VOStatus]" +
                "FROM " + clsGlobals.Gesin + "[tblGIScSalesHistory] AS SH " +
                "WHERE[ProductColorID]=" + activeProduct + " AND SH.ScenarioID=" + activeScenario + " AND [CatID]=100 ORDER BY [DimCode],[SizeOrder]";
            //Conexion.StartSession();
            DataTable myTb = Conexion.GDatos.GetDataTableSql(sql);
            Conexion.EndSession();
            Elements = CopyDataTable(myTb);
        }

        private List<clsScSalesHistory> CopyDataTable(DataTable myTb)
        {
            List<clsScSalesHistory> lstTem = new List<clsScSalesHistory>();
            foreach (DataRow rw in myTb.Rows)
            {
                clsScSalesHistory myEle = new clsScSalesHistory();
                myEle.CopyDataRow(rw);
                lstTem.Add(myEle);
            }
            return lstTem;
        }

        internal void GetSalesHistory(int scenarioID)
        {
            string sql = "SELECT * FROM " + clsGlobals.Gesin + "[tblGIScSalesHistory] WHERE[CatCode]='REG' AND[ScenarioID]=" + scenarioID;
            //Conexion.StartSession();
            DataTable myTb = Conexion.GDatos.GetDataTableSql(sql);
            Conexion.EndSession();
            Elements = CopyDataTable(myTb);
        }
    }
}

using prjGIUnimage.data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjGIUnimage.bus
{
    class clsListScenario
    {
        List<clsScenario> myList;

        public clsListScenario()
        {
            this.myList = new List<clsScenario>();
        }

        public int Quantity
        {
            get => myList.Count;
        }

        public List<clsScenario> Elements
        {
            get => myList;
            set => myList = value;
        }

        public void AllScenarios()
        {
            string sql = "SELECT * FROM " + clsGlobals.Gesin + "[tblGIScenario] WHERE [ScenarioStatus]!=2 ORDER BY [CreatedDate] DESC";
            DataTable myTb = new DataTable();
            Conexion.StartSession();
            myTb = Conexion.GDatos.BringDataTableSql(sql);
            Conexion.EndSession();
            Elements = CopyDataTable(myTb);
        }

        private List<clsScenario> CopyDataTable(DataTable myTb)
        {
            List<clsScenario> lstTem = new List<clsScenario>();
            foreach (DataRow rw in myTb.Rows)
            {
                clsScenario myDiv = new clsScenario();
                myDiv.CopyDatarow(rw);
                lstTem.Add(myDiv);
            }
            return lstTem;
        }

        internal clsScenario ElementByID(int current)
        {
            foreach (clsScenario it in Elements)
            {
                if (it.GIScenarioID == current)
                {
                    return it;
                }
            }
            return new clsScenario();
        }

        internal int IDLastRecord()
        {
            string sql = "SELECT MAX([GIScenarioID])FROM " + clsGlobals.Gesin + "[tblGIScenario]";
            Conexion.StartSession();
            int LastID = Convert.ToInt32(Conexion.GDatos.BringScalarValueSql(sql));
            Conexion.EndSession();
            return LastID;
        }

        internal void GetListScenariosByProduct(int productID)
        {
            string sql = "SELECT [GIScenarioID],[ScenarioCode],[ScenarioDesc],[GISeasonID],[SalesEstimateID],[ScenarioStatus]," +
                "[SurplusRateUnique],[SurplusRateCommon],[SurplusRateIdentified],[SurplusRateOS], GI.[ReferenceNo1],GI.[ReferenceNo2]," +
                "GI.[ExpShippingDate],GI.[ExpArrivalDate],GI.[VendorID],GI.[VendorSiteID],GI.[PurchaseTypeID],GI.[DefaultWarehouseID]," +
                "GI.[DivisionID],GI.[CollectionID],GI.[VOSeasonID],GI.[VONote],GI.[VOMessage],GI.[CreatedByUserID],GI.[ModifiedByUserID]," +
                "GI.[DeletedByUserID],GI.[CreatedDate],GI.[ModifiedDate],GI.[DeletedDate] " +
                "FROM " + clsGlobals.Gesin + "[tblGIScenario] AS GI " +
                "INNER JOIN " + clsGlobals.Gesin + "[tblGIScProduct] AS SC ON GI.GIScenarioID=SC.ScenarioID " +
                "WHERE[ProductColorID]= " + productID + " AND [VOStatus]!=0 " +
                "ORDER BY CreatedDate DESC";
            DataTable myTb = new DataTable();
            Conexion.StartSession();
            myTb = Conexion.GDatos.BringDataTableSql(sql);
            Conexion.EndSession();
            Elements = CopyDataTable(myTb);
        }
    }
}

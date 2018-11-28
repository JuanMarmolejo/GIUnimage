using prjGIUnimage.data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjGIUnimage.bus
{
    class clsListScProduct
    {
        internal static DataTable GetListVOToPrint(int scenarioID)
        {
            string sql = "SELECT [ColorName_fra] AS Color,[ProductCode] AS Product, [VOCode] AS Code " +
                "FROM " + clsGlobals.Gesin + "[tblGIScProduct] AS SCP " +
                "INNER JOIN " + clsGlobals.Silex + "[tblSXProductColor] AS PC ON SCP.ProductColorID=PC.ProductColorID " +
                "INNER JOIN " + clsGlobals.Silex + "[tblSXProduct] AS PD ON PC.ProductID= PD.ProductID " +
                "INNER JOIN " + clsGlobals.Silex + "[tblSXcOLOR] AS CL ON PC.ColorID= CL.ColorID " +
                "WHERE[VOStatus]= 1 AND[VoPdfStatus]= 0 AND[ScenarioID]= " + scenarioID + " " +
                "ORDER BY [VOCode]";
            //Conexion.StartSession();
            DataTable myTb = Conexion.GDatos.GetDataTableSql(sql);
            Conexion.EndSession();
            return myTb;
        }
    }
}

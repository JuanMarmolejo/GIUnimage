using prjGIUnimage.data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjGIUnimage.bus
{
    class clsElement : IEquatable<clsElement>, IComparable<clsElement>
    {
        string vElementID;
        string vCode;
        string vName;

        public clsElement()
        {
        }

        public clsElement(string elementID, string code, string name)
        {
            ElementID = elementID;
            Code = code;
            Name = name;
        }

        public string ElementID { get => vElementID; set => vElementID = value; }
        public string Code { get => vCode; set => vCode = value; }
        public string Name { get => vName; set => vName = value; }
        public string Full { get => this.Code + " " + this.Name;}

        public int CompareTo(clsElement compareElement)
        {
            // A null value means that this object is greater.
            if (compareElement == null)
                return 1;

            else
                return this.Code.CompareTo(compareElement.Code);
        }

        public bool Equals(clsElement other)
        {
            if (other == null) return false;
            return (this.Code.Equals(other.Code));
        }

        internal void CopyDataRow(DataRow dr)
        {
            this.ElementID = dr[0].ToString();
            this.Code = dr[1].ToString();
            this.Name = dr[2].ToString();
        }

        internal void GetShipFrom(int VendorID)
        {
            string sql = "SELECT [VendorSiteID],[SiteName],[SiteAddress1] FROM " + clsGlobals.Silex + "[tblSXVendorSite] WHERE[VendorID] = " + VendorID;
            Conexion.StartSession();
            DataTable MyTb = Conexion.GDatos.BringDataTableSql(sql);
            Conexion.EndSession();
            CopyDataRow(MyTb.Rows[0]);
        }

        internal int GetVendorSiteID(int vendorID)
        {
            string sql = "SELECT [VendorSiteID] FROM " + clsGlobals.Silex + "[tblSXVendorSite] WHERE [VendorID]=" + vendorID;
            Conexion.StartSession();
            var Id = Conexion.GDatos.BringScalarValueSql(sql);
            Conexion.EndSession();
            return Convert.ToInt32(Id);
        }

        internal static bool CompCollectionExists(int productColorID)
        {
            string sql = "SELECT DISTINCT[ProductColorID] FROM " + clsGlobals.Silex + "[tblSXCostCardComp] AS COST " +
                "INNER JOIN " + clsGlobals.Silex + "[tblSXCostCardDetail] AS DET ON COST.CostCardDetailID=DET.CostCardDetailID " +
                "WHERE [CompProductColorID]= " + productColorID;
            Conexion.StartSession();
            try
            {
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
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

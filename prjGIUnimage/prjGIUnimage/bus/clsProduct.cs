using prjGIUnimage.data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjGIUnimage.bus
{
    class clsProduct
    {
        //Fields in the table GIProducts
        public int GIProductID { get; set; }
        public int ProductID { get; set; }
        public int ColorID { get; set; }
        public int ProductColorID { get; set; }
        public int GIProductStatus { get; set; }
        public string ProductComment { get; set; }
        public double SurplusRate { get; set; }
        public int CreatedByUserID { get; set; }
        public int ModifiedByUserID { get; set; }
        public int DeletedByUserID { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime DeletedDate { get; set; }

        //Additional product information
        public string ProductCode { get; set; }
        public string ColorCode { get; set; }
        public string ColorName { get; set; }
        public string CollectionName { get; set; }
        public string GroupName { get; set; }
        public string ShortProductCode { get; set; }
        public string ProductStatusDesc { get; set; }
        public string ProductDesc { get; set; }

        public clsProduct()
        {
        }

        public override bool Equals(Object obj)
        {
            clsProduct vObj = obj as clsProduct;
            if (vObj == null)
                return false;
            else
                return (ProductID.Equals(vObj.ProductID) && ColorID.Equals(vObj.ColorID));
        }

        public override int GetHashCode()
        {
            return ProductID;
        }

        internal void CopyDatarow(DataRow rw)
        {
            this.GIProductID = Convert.ToInt32(rw["GIProductID"]);
            this.ProductID = Convert.ToInt32(rw["ProductID"]);
            this.ProductColorID = Convert.ToInt32(rw["ProductColorID"]);
            this.ColorID = Convert.ToInt32(rw["ColorID"]);
            this.ColorName = String.IsNullOrEmpty(rw["ColorName_fra"].ToString()) ? "" : Convert.ToString(rw["ColorName_fra"]);
            this.GIProductStatus = Convert.ToInt32(rw["GIProductStatus"]);
            this.ProductComment = Convert.ToString(rw["ProductComment"]);
            this.SurplusRate = String.IsNullOrEmpty(rw["SurplusRate"].ToString()) ? 0 : Convert.ToDouble(rw["SurplusRate"]);
            this.CreatedByUserID = String.IsNullOrEmpty(rw["CreatedByUserID"].ToString()) ? 0 : Convert.ToInt32(rw["CreatedByUserID"]);
            this.ModifiedByUserID = String.IsNullOrEmpty(rw["ModifiedByUserID"].ToString()) ? 0 : Convert.ToInt32(rw["ModifiedByUserID"]);
            this.DeletedByUserID = String.IsNullOrEmpty(rw["DeletedByUserID"].ToString()) ? 0 : Convert.ToInt32(rw["DeletedByUserID"]);
            this.CreatedDate = String.IsNullOrEmpty(rw["CreatedDate"].ToString()) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(rw["CreatedDate"]);
            this.ModifiedDate = String.IsNullOrEmpty(rw["ModifiedDate"].ToString()) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(rw["ModifiedDate"]);
            this.DeletedDate = String.IsNullOrEmpty(rw["DeletedDate"].ToString()) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(rw["DeletedDate"]);

            this.ProductCode = Convert.ToString(rw["ProductCode"]);
            this.CollectionName = Convert.ToString(rw["CollectionName"]);
            this.GroupName = Convert.ToString(rw["GroupName_fra"]);
            this.ShortProductCode = Convert.ToString(rw["ShortProductCode"]);
            this.ProductStatusDesc = Convert.ToString(rw["DataDesc_fra"]);
            this.ProductDesc = Convert.ToString(rw["ProductDesc_fra"]);
        }

        internal void CopySXDatarow(DataRow rw)
        {
            this.GIProductID = 0;
            this.ProductID = Convert.ToInt32(rw["ProductID"]);
            this.ColorID = Convert.ToInt32(rw["ColorID"]);
            this.ProductColorID = Convert.ToInt32(rw["ProductColorID"]);
            this.GIProductStatus = Convert.ToInt32(rw["ProductColorStatus"]);
            this.ProductComment = "";
            this.SurplusRate = 5;
            this.CreatedByUserID = clsGlobals.GIPar.UserID;
            this.ModifiedByUserID = 0;
            this.DeletedByUserID = 0;
            this.CreatedDate = DateTime.Now;
            this.ModifiedDate = Convert.ToDateTime("01/01/1900");
            this.DeletedDate = Convert.ToDateTime("01/01/1900");
        }

        internal void CopyDataRowSimple(DataRow rw)
        {
            try
            {
                this.GIProductID = Convert.ToInt32(rw["GIProductID"]);
                this.ProductColorID = Convert.ToInt32(rw["ProductColorID"]);
                this.ProductID = Convert.ToInt32(rw["ProductID"]);
                this.ColorID = Convert.ToInt32(rw["ColorID"]);
                this.GIProductStatus = Convert.ToInt32(rw["GIProductStatus"]);
                this.ProductComment = Convert.ToString(rw["ProductComment"]);
                this.SurplusRate = String.IsNullOrEmpty(rw["SurplusRate"].ToString()) ? 0 : Convert.ToDouble(rw["SurplusRate"]);
                this.CreatedByUserID = String.IsNullOrEmpty(rw["CreatedByUserID"].ToString()) ? 0 : Convert.ToInt32(rw["CreatedByUserID"]);
                this.ModifiedByUserID = String.IsNullOrEmpty(rw["ModifiedByUserID"].ToString()) ? 0 : Convert.ToInt32(rw["ModifiedByUserID"]);
                this.DeletedByUserID = String.IsNullOrEmpty(rw["DeletedByUserID"].ToString()) ? 0 : Convert.ToInt32(rw["DeletedByUserID"]);
                this.CreatedDate = String.IsNullOrEmpty(rw["CreatedDate"].ToString()) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(rw["CreatedDate"]);
                this.ModifiedDate = String.IsNullOrEmpty(rw["ModifiedDate"].ToString()) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(rw["ModifiedDate"]);
                this.DeletedDate = String.IsNullOrEmpty(rw["DeletedDate"].ToString()) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(rw["DeletedDate"]);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal void UpdateGIProduct()
        {
            Conexion.StartSession();
            string sql = "UPDATE " + clsGlobals.Gesin + "[tblGIProduct] SET   " +
                "[ProductComment] = '" + this.ProductComment + "' " +
                ",[SurplusRate] = " + this.SurplusRate + " " +
                ",[GIProductStatus] = " + this.GIProductStatus + " " +
                ",[ModifiedByUserID] = " + clsGlobals.GIPar.UserID + " " +
                ",[ModifiedDate] = GETDATE() " +
                "WHERE[GIProductID] = " + this.GIProductID;
            Conexion.GDatos.RunSql(sql);
            Conexion.EndSession();
        }

        public void InsertGIProduct()
        {
            Conexion.StartSession();
            string sql = "INSERT INTO " + clsGlobals.Gesin + "[tblGIProduct] ( " +
                "[ProductID]" +
                ",[ColorID] " +
                ",[ProductColorID] " +
                ",[GIProductStatus] " +
                ",[ProductComment] " +
                ",[SurplusRate] " +
                ",[CreatedByUserID] " +
                ",[CreatedDate] " +
                ") VALUES( " +
                this.ProductID + ", " +
                this.ColorID + ", " +
                this.ProductColorID + ", " +
                "3" + ", '" +
                this.ProductComment + "', " +
                this.SurplusRate + ", " + clsGlobals.GIPar.UserID + ", GETDATE()" +
                ")";
            Conexion.GDatos.RunSql(sql);
            Conexion.EndSession();
        }

        internal static int GetProductColorIDFromGIProductID(int gIProductID)
        {
            string sql = "SELECT [ProductColorID] " +
                "FROM " + clsGlobals.Gesin + "[tblGIProduct] " +
                "WHERE [GIProductID] = " + gIProductID;
            Conexion.StartSession();
            int productColorID = Convert.ToInt32(Conexion.GDatos.BringScalarValueSql(sql));
            Conexion.EndSession();

            return productColorID;
        }
    }
}

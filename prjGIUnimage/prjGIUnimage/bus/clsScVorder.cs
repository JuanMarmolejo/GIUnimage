using prjGIUnimage.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjGIUnimage.bus
{
    class clsScVorder
    {
        public int GIVOID { get; set; }
        public string VOCode { get; set; }
        public int DivisionID { get; set; }
        public int VendorID { get; set; }
        public int VendorSiteID { get; set; }
        public int DefaultWarehouseID { get; set; }
        public int CollectionID { get; set; }
        public int SeasonID { get; set; }
        public int PurchaseTypeID { get; set; }
        public string ReferenceNo1 { get; set; }
        public string ReferenceNo2 { get; set; }
        public DateTime ExpShippingDate { get; set; }
        public DateTime ExpArrivalDate { get; set; }
        public double OrderTotalQty { get; set; }
        public int VOStatus { get; set; }
        public int CreatedByUserID { get; set; }
        public int ModifiedByUserID { get; set; }
        public int DeletedByUserID { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime DeletedDate { get; set; }
        public string VONote { get; set; }
        public string VOMessage { get; set; }
        public int PDFPrinted { get; set; }

        public clsScVorder()
        {
        }

        internal int SaveVorder()
        {
            clsSeason mySea = new clsSeason();
            mySea.GetSeasonByID(this.SeasonID);

            string sql = "INSERT INTO " + clsGlobals.Gesin + "[tblGIScVorder] ([DivisionID],[VendorID],[VendorSiteID],[DefaultWarehouseID],[CollectionID]," +
                "[SeasonID],[PurchaseTypeID],[ReferenceNo1],[ReferenceNo2],[ExpShippingDate],[ExpArrivalDate],[OrderTotalQty]," +
                "[VOStatus],[CreatedByUserID],[CreatedDate],[VONote],[VOMessage],[PDFPrinted])VALUES (" + this.DivisionID + "," + 
                this.VendorID + "," + this.VendorSiteID + "," + this.DefaultWarehouseID + "," + this.CollectionID + "," + mySea.SXSeasonID + 
                "," + this.PurchaseTypeID + ",'" + this.ReferenceNo1 + "','" + this.ReferenceNo2 + "','" + this.ExpShippingDate + "','" + 
                this.ExpArrivalDate + "'," + this.OrderTotalQty + "," + this.VOStatus + "," + clsGlobals.GIPar.UserID + ",GETDATE(),'" + 
                this.VONote + "','" + this.VOMessage + "',0)";
            Conexion.StartSession();
            Conexion.GDatos.RunSql(sql);
            sql = "SELECT MAX([GIVOID])FROM " + clsGlobals.Gesin + "[tblGIScVorder]";
            int giVOID = Convert.ToInt32(Conexion.GDatos.BringScalarValueSql(sql));
            Conexion.EndSession();
            return giVOID;
        }

        internal void GetVoCodeBy(int giVOID)
        {
            string sql = "SELECT [VOCode] FROM " + clsGlobals.Gesin + "[tblGIScVorder] WHERE[GIVOID] = " + giVOID;
            Conexion.StartSession();
            this.VOCode = (string)Conexion.GDatos.BringScalarValueSql(sql);
            Conexion.EndSession();
            this.GIVOID = giVOID;
        }

        internal bool IsUnimage()
        {
            if (this.VendorID == 119)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

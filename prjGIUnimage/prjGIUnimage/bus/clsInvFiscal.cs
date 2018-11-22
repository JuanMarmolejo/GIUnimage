using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prjGIUnimage.bus;
using prjGIUnimage.data;

namespace prjGIUnimage
{
    class clsInvFiscal
    {
        public int InvFiscalID { get; set; }
        public int SeasonID { get; set; }
        public int CollectionID { get; set; }
        public int ProductID { get; set; }
        public int ProductColorID { get; set; }
        public int ProductDimID { get; set; }
        public int ProductCatID { get; set; }
        public int SizeOrder { get; set; }
        public string SizeDesc { get; set; }
        public int ColorID { get; set; }
        public int DimID { get; set; }
        public int CatID { get; set; }
        public int ProductGroupID { get; set; }
        public int ProductSubGroupID { get; set; }
        public string GIInvFComment { get; set; }
        public double QtyStockF { get; set; }
        public int CreatedByUserID { get; set; }
        public int ModifiedByUserID { get; set; }
        public int DeletedByUserID { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public System.DateTime DeletedDate { get; set; }

        internal void CopyElement(clsScSalesHistory ele)
        {
            this.CatID = ele.CatID;
            this.CollectionID = ele.CollectionID;
            this.ColorID = ele.ColorID;
            this.DimID = ele.DimID;
            this.ProductCatID = ele.ProductCatID;
            this.ProductColorID = ele.ProductColorID;
            this.ProductDimID = ele.ProductDimID;
            this.ProductGroupID = ele.ProductGroupID;
            this.ProductID = ele.ProductID;
            this.ProductSubGroupID = ele.ProductSubGroupID;
            this.QtyStockF = ele.QtyStock;
            this.SeasonID = ele.SeasonID;
            this.SizeOrder = ele.SizeOrder;
            this.SizeDesc = ele.SizeDesc;
        }

        internal void InsertInvFiscal(bool flag)
        {
            string sql = "";
            if (flag)
            {
                sql = "INSERT INTO " + clsGlobals.Gesin + "[tblGIInvBeforeFiscal] ([SeasonID],[CollectionID],[ProductID],[ProductColorID],[ProductDimID],[ProductCatID]," +
                    "[SizeOrder],[SizeDesc],[ColorID],[DimID],[CatID],[ProductGroupID],[ProductSubGroupID],[GIInvBFComment],[QtyStockBF],[CreatedByUserID]," +
                    "[CreatedDate]) VALUES (" + this.SeasonID + "," + this.CollectionID + "," + this.ProductID + "," + this.ProductColorID + "," +
                    this.ProductDimID + "," + this.ProductCatID + "," + this.SizeOrder + ",'" + this.SizeDesc + "'," + this.ColorID + "," + this.DimID + "," + this.CatID +
                    "," + this.ProductGroupID + "," + this.ProductSubGroupID + ",'" + this.GIInvFComment + "'," + this.QtyStockF.ToString("R", System.Globalization.CultureInfo.CreateSpecificCulture("en-US")) + "," +
                    clsGlobals.GIPar.UserID + ",GETDATE())";
            }
            else
            {
                sql = "INSERT INTO " + clsGlobals.Gesin + "[tblGIInvAfterFiscal] ([SeasonID],[CollectionID],[ProductID],[ProductColorID],[ProductDimID],[ProductCatID], " +
                    "[SizeOrder],[SizeDesc],[ColorID],[DimID],[CatID],[ProductGroupID],[ProductSubGroupID],[GIInvAFComment],[QtyStockAF],[CreatedByUserID]," +
                    "[CreatedDate]) VALUES (" + this.SeasonID + "," + this.CollectionID + "," + this.ProductID + "," + this.ProductColorID + "," +
                    this.ProductDimID + "," + this.ProductCatID + "," + this.SizeOrder + ",'" + this.SizeDesc + "'," + this.ColorID + "," + this.DimID + "," + this.CatID +
                    "," + this.ProductGroupID + "," + this.ProductSubGroupID + ",'" + this.GIInvFComment + "'," + this.QtyStockF.ToString("R", System.Globalization.CultureInfo.CreateSpecificCulture("en-US")) + "," +
                    clsGlobals.GIPar.UserID + ",GETDATE())";
            }
            Conexion.GDatos.RunSql(sql);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjGIUnimage.bus
{
    class clsProductOrdered
    {
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
        public string Dim { get; set; }
        public string Size { get; set; }
        public double RP1 { get; set; }
        public double VP1 { get; set; }
        public double RP2 { get; set; }
        public double VP2 { get; set; }
        public double ACH { get; set; }
        public double CMD { get; set; }
        public double VTOT { get; set; }
        public double CSS { get; set; }
        public double Var { get; set; }
        public double VTEM { get; set; }
        public double IBF { get; set; }
        public double IAF { get; set; }
        public double IC { get; set; }
        public double Besoin { get; set; }
        public double Available { get; set; }
        public double QVO { get; set; }

        public clsProductOrdered()
        {
        }

        internal void CalculateStatisticsBooking(clsScSalesHistory ele, int v)
        {
            clsGIParameter myPa = new clsGIParameter();

            //General parameters
            myPa = clsGlobals.GIPar;

            //Product Information
            ProductID = ele.ProductID;
            ProductColorID = ele.ProductColorID;
            ProductDimID = ele.ProductDimID;
            ProductCatID = ele.ProductCatID;
            SizeOrder = ele.SizeOrder;
            ColorID = ele.ColorID;
            DimID = ele.DimID;
            CatID = ele.CatID;
            ProductGroupID = ele.ProductGroupID;
            ProductSubGroupID = ele.ProductSubGroupID;
            this.Dim = ele.DimCode;
            this.Size = ele.SizeDesc;
            this.RP1 = ele.QtyVI1;
            this.VP1 = ele.QtyCI1a + ele.QtyCI1ToInvoice;
            this.RP2 = ele.QtyVI2;
            this.VP2 = ele.QtyCI2a + ele.QtyCI2ToInvoice;
            this.ACH = ele.QtyVOOpen1 + ele.QtyVOOpen2;
            this.CMD = ele.QtyCO1 + ele.QtyCO2;
            this.VTOT = ele.QtyCI2a + ele.QtyCI2ToInvoice + ele.QtyCO2;
            this.CSS = ele.QtyCO2 - ele.QtyPack;
            this.Var = this.VTOT - this.VP1;
            this.VTEM = (this.VTOT + this.VP1) / 2;
            if (v == 1)
            {
                this.IBF = ele.GetInvBF();
                this.IAF = ele.GetInvAF();
            }
            if (v == 2)
            {
                this.IBF = ele.GetInvBF();
                this.IAF = 0;
            }
            if (v == 3)
            {
                this.IBF = 0;
                this.IAF = ele.GetInvAF();
            }
            if (v == 4)
            {
                this.IBF = 0;
                this.IAF = 0;
            }
            this.IC = ele.QtyStock;

            //Calcul des besoins.
            Besoin = Math.Floor(((ele.QtyCO1 + ele.QtyCI2a + ele.QtyCI2ToInvoice + ele.QtyCO2) * (1 + clsGlobals.ActiveRatio / 100))
                - ele.QtyVOOpen1 - ele.QtyCI2a - ele.QtyCI2ToInvoice - ele.QtyVOOpen2 - ele.QtyStock);
            this.QVO = ele.QtyAchat;

            //compras + IC - cmd
            this.Available = this.ACH + this.IC - this.CMD;
        }

        internal void CalculateStatisticsRepeat(clsScSalesHistory ele, int v)
        {
            clsGIParameter myPa = new clsGIParameter();
            myPa = clsGlobals.GIPar;
            ProductID = ele.ProductID;
            ProductColorID = ele.ProductColorID;
            ProductDimID = ele.ProductDimID;
            ProductCatID = ele.ProductCatID;
            SizeOrder = ele.SizeOrder;
            ColorID = ele.ColorID;
            DimID = ele.DimID;
            CatID = ele.CatID;
            ProductGroupID = ele.ProductGroupID;
            ProductSubGroupID = ele.ProductSubGroupID;
            this.Dim = ele.DimCode;
            this.Size = ele.SizeDesc;
            this.RP1 = ele.QtyVI1;
            this.VP1 = ele.QtyCI1a + ele.QtyCI1ToInvoice;
            this.RP2 = ele.QtyVI2;
            this.VP2 = ele.QtyCI2a + ele.QtyCI2ToInvoice;
            this.ACH = ele.QtyVOOpen1 + ele.QtyVOOpen2;
            this.CMD = ele.QtyCO1 + ele.QtyCO2;
            this.VTOT = ele.QtyCI2a + ele.QtyCI2ToInvoice + ele.QtyCO2;
            this.CSS = ele.QtyCO2 - ele.QtyPack;
            this.Var = this.VTOT - this.VP1;
            this.VTEM = (this.VTOT + this.VP1) / 2;
            if (v == 1)
            {
                this.IBF = ele.GetInvBF();
                this.IAF = ele.GetInvAF();
            }
            if (v == 2)
            {
                this.IBF = ele.GetInvBF();
                this.IAF = 0;
            }
            if (v == 3)
            {
                this.IBF = 0;
                this.IAF = ele.GetInvAF();
            }
            if (v == 4)
            {
                this.IBF = 0;
                this.IAF = 0;
            }
            this.IC = ele.QtyStock;
            Besoin = Math.Floor(((ele.QtyCO1 + ele.QtyCI2a + ele.QtyCI2ToInvoice + ele.QtyCO2) * (1 + clsGlobals.ActiveRatio / 100)) 
                - ele.QtyVOOpen1 - ele.QtyCI2a - ele.QtyCI2ToInvoice - ele.QtyVOOpen2 - ele.QtyStock 
                - ele.QtyCI2a - ele.QtyCI2ToInvoice);
            this.QVO = ele.QtyAchat;

            //compras + IC - cmd
            this.Available = this.ACH + this.IC - this.CMD;
        }
    }
}

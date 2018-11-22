using prjGIUnimage.data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace prjGIUnimage.bus
{
    class clsGIParameter
    {
        public int GIParameterID { get; set; }

        //Surplus Parameters
        //public double SurplusRate { get; set; }
        public double SurplusRateUnique { get; set; }
        public double SurplusRateCommon { get; set; }
        public double SurplusRateIdentified { get; set; }
        public double SurplusRateOS { get; set; }

        //VO Parameters
        public string ReferenceNo1 { get; set; }
        public string ReferenceNo2 { get; set; }
        public DateTime ExpShippingDate { get; set; }
        public DateTime ExpArrivalDate { get; set; }
        public int VendorID { get; set; }
        public int VendorSiteID { get; set; }
        public int PurchaseTypeID { get; set; }
        public int DefaultWarehouseID { get; set; }
        public int DivisionID { get; set; }
        public int CollectionID { get; set; }
        public int SeasonID { get; set; }
        public string VONote { get; set; }
        public string VOMessage { get; set; }

        //IDVariables
        public int UserID { get; set; }
        public int ScenarioID { get; set; }
        public int ProductColorID { get; set; }

        //Registry information
        public int CreatedByUserID { get; set; }
        public int ModifiedByUserID { get; set; }
        public int DeletedByUserID { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime DeletedDate { get; set; }

        public clsGIParameter()
        {
        }

        internal void UpDateSurplusParameters()
        {
            string sql = "UPDATE " + clsGlobals.Gesin + "[tblGIParameter] SET " +
                "[SurplusRateUnique] = " + SurplusRateUnique +
                ",[SurplusRateCommon] = " + SurplusRateCommon +
                ",[SurplusRateIdentified] = " + SurplusRateIdentified +
                ",[SurplusRateOS] = " + SurplusRateOS +
                ",[ModifiedByUserID] = " + clsGlobals.GIPar.UserID +
                ",[ModifiedDate] = GETDATE()" +
                "WHERE[GIParameterID]=100";
            Conexion.StartSession();
            Conexion.GDatos.RunSql(sql);
            Conexion.EndSession();
        }

        internal void UpdateVOParameter()
        {
            string sql = "UPDATE " + clsGlobals.Gesin + "[tblGIParameter] SET " +
                "[ReferenceNo1] ='" + ReferenceNo1 +
                "',[ReferenceNo2] = '" + ReferenceNo2 +
                "',[ExpShippingDate] = '" + ExpShippingDate.ToString("yyyy-MM-dd") +
                "',[ExpArrivalDate] = '" + ExpArrivalDate.ToString("yyyy-MM-dd") +
                "',[VendorID] = " + VendorID +
                ",[VendorSiteID] = " + VendorSiteID +
                ",[PurchaseTypeID] = " + PurchaseTypeID +
                ",[DefaultWarehouseID] = " + DefaultWarehouseID +
                ",[DivisionID] = " + DivisionID +
                ",[CollectionID] = " + CollectionID +
                ",[SeasonID] = " + SeasonID +
                ",[VONote] = '" + VONote +
                "',[VOMessage] = '" + VOMessage +
                "',[ModifiedByUserID] = " + clsGlobals.GIPar.UserID +
                ",[ModifiedDate] = GETDATE()" +
                "WHERE[GIParameterID]=100";
            Conexion.StartSession();
            Conexion.GDatos.RunSql(sql);
            Conexion.EndSession();
        }

        public void UpdateIDVariables()
        {
            string sql = "UPDATE " + clsGlobals.Gesin + "[tblGIParameter] SET " +
                "[ActiveUserID] = " + UserID +
                ",[ActiveScenarioID] = " + ScenarioID +
                ",[ActiveProductID] = " + ProductColorID +
                "WHERE[GIParameterID] = 100 ";
            Conexion.StartSession();
            Conexion.GDatos.RunSql(sql);
            Conexion.EndSession();
        }

        internal void GetSurplusParameters()
        {
            string sql = "SELECT [SurplusRateUnique],[SurplusRateCommon],[SurplusRateIdentified],[SurplusRateOS]FROM " + clsGlobals.Gesin + "[tblGIParameter] " +
                "WHERE[GIParameterID]=100";
            Conexion.StartSession();
            DataTable myTb = Conexion.GDatos.BringDataTableSql(sql);
            DataRow rw = myTb.Rows[0];
            Conexion.EndSession();

            SurplusRateCommon = Convert.ToInt32(rw["SurplusRateCommon"]);
            SurplusRateIdentified = Convert.ToInt32(rw["SurplusRateIdentified"]);
            SurplusRateOS = Convert.ToInt32(rw["SurplusRateOS"]);
            SurplusRateUnique = Convert.ToInt32(rw["SurplusRateUnique"]);
        }

        internal void GetVOParameters()
        {
            string sql = "SELECT [ReferenceNo1],[ReferenceNo2],[ExpShippingDate],[ExpArrivalDate],[VendorID],[VendorSiteID],[PurchaseTypeID]," +
                "[DefaultWarehouseID],[DivisionID],[CollectionID],[SeasonID],[VONote],[VOMessage] FROM " + clsGlobals.Gesin + "[tblGIParameter] WHERE[GIParameterID]=100";
            Conexion.StartSession();
            DataTable myTb = Conexion.GDatos.BringDataTableSql(sql);
            DataRow rw = myTb.Rows[0];
            Conexion.EndSession();
            ReferenceNo1 = Convert.ToString(rw["ReferenceNo1"]);
            ReferenceNo2 = Convert.ToString(rw["ReferenceNo2"]);
            ExpShippingDate = Convert.ToDateTime(rw["ExpShippingDate"]);
            ExpArrivalDate = Convert.ToDateTime(rw["ExpArrivalDate"]);
            VendorID = Convert.ToInt32(rw["VendorID"]);
            VendorSiteID = Convert.ToInt32(rw["VendorSiteID"]);
            PurchaseTypeID = Convert.ToInt32(rw["PurchaseTypeID"]);
            DefaultWarehouseID = Convert.ToInt32(rw["DefaultWarehouseID"]);
            DivisionID = Convert.ToInt32(rw["DivisionID"]);
            CollectionID = Convert.ToInt32(rw["CollectionID"]);
            SeasonID = Convert.ToInt32(rw["SeasonID"]);
            VONote = Convert.ToString(rw["VONote"]);
            VOMessage = Convert.ToString(rw["VOMessage"]);
        }

        internal void GetIDVariables()
        {
            string sql = "SELECT [ActiveUserID],[ActiveScenarioID],[ActiveProductID]FROM " + clsGlobals.Gesin + "[tblGIParameter]WHERE [GIParameterID]=100";
            Conexion.StartSession();
            DataTable myTb = Conexion.GDatos.BringDataTableSql(sql);
            Conexion.EndSession();
            DataRow rw = myTb.Rows[0];
            UserID = Convert.ToInt32(rw["ActiveUserID"]);
            ScenarioID = Convert.ToInt32(rw["ActiveScenarioID"]);
            ProductColorID = Convert.ToInt32(rw["ActiveProductID"]);
        }

        internal void GetUserID()
        {
            // create the path variable
            string path = @".\Parameters.xml";
            // create the XmlReaderSettings object
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreWhitespace = true;
            settings.IgnoreComments = true;
            // create the XmlReader object
            XmlReader xmlIn = XmlReader.Create(path, settings);
            // read past all nodes to the first Product node
            if (xmlIn.ReadToDescendant("Parameter"))
            { // create one Product object for each Product node
                do
                {
                    UserID = Convert.ToInt32(xmlIn["ActiveUser"]);
                    xmlIn.ReadStartElement("Parameter");
                }
                while (xmlIn.ReadToNextSibling("Parameter"));
            }
            // close the XmlReader object
            xmlIn.Close();
        }

        internal void SetUserID()
        {
            // create the path variable
            string path = @".\Parameters.xml";

            // create the XmlWriterSettings object
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = (" ");

            // create the XmlWriter object
            XmlWriter xmlOut = XmlWriter.Create(path, settings);

            // write the start of the document
            xmlOut.WriteStartDocument();
            xmlOut.WriteStartElement("Parameters");

            // write each Product object to the xml file
            xmlOut.WriteStartElement("Parameter");
            xmlOut.WriteAttributeString("ActiveUser", Convert.ToString(UserID));
            xmlOut.WriteEndElement();

            // write the end tag for the root element
            xmlOut.WriteEndElement();

            // close the XmlWriter object
            xmlOut.Close();
        }
    }
}

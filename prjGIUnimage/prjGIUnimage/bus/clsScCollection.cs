using prjGIUnimage.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjGIUnimage.bus
{
    class clsScCollection
    {
        public int ScCollectionID { get; set; }
        public int ScenarioID { get; set; }
        public int GICollectionID { get; set; }
        public byte ScCollectionStatus { get; set; }
        public string ScCollectionComment { get; set; }
        public int CollectionID { get; set; }
        public bool GICollectionStatus { get; set; }
        public string GICollectionComment { get; set; }
        public double SurplusRateUnique { get; set; }
        public double SurplusRateCommon { get; set; }
        public double SurplusRateIdentified { get; set; }
        public double SurplusRateOS { get; set; }
        public int CreatedByUserID { get; set; }
        public int ModifiedByUserID { get; set; }
        public int DeletedByUserID { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime DeletedDate { get; set; }

        public clsScCollection()
        {
        }

        internal void InsertScCollection(clsCollection ele)
        {
            Conexion.StartSession();
            string sql = "INSERT INTO " + clsGlobals.Gesin + "[tblGIScCollection]([ScenarioID],[GICollectionID],[ScCollectionStatus],[ScCollectionComment],[CollectionID]" +
                ",[GICollectionStatus],[GICollectionComment],[CreatedByUserID],[CreatedDate])VALUES(" + this.ScenarioID + "," + ele.GICollectionID +
                "," + this.ScCollectionStatus + ",'" + this.ScCollectionComment + "'," + ele.CollectionID + "," + ele.GICollectionStatus + ",'" + 
                ele.GICollectionComment + "'," + clsGlobals.GIPar.UserID + ",GETDATE())";
            Conexion.GDatos.RunSql(sql);
            Conexion.EndSession();
        }
    }
}

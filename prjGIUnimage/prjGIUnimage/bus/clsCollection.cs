using prjGIUnimage.data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjGIUnimage.bus
{
    class clsCollection
    {
        public int GICollectionID { get; set; }
        public int CollectionID { get; set; }
        public int GICollectionStatus { get; set; }
        public string GICollectionComment { get; set; }
        public int CreatedByUserID { get; set; }
        public int ModifiedByUserID { get; set; }
        public int DeletedByUserID { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime DeletedDate { get; set; }

        public clsCollection()
        {
        }

        public override bool Equals(Object obj)
        {
            clsCollection vObj = obj as clsCollection;
            if (vObj == null)
                return false;
            else
                return CollectionID.Equals(vObj.CollectionID);
        }

        public override int GetHashCode()
        {
            return CollectionID;
        }

        internal void CopyDatarow(DataRow rw)
        {
            this.GICollectionID = Convert.ToInt32(rw["GICollectionID"]);
            this.CollectionID = Convert.ToInt32(rw["CollectionID"]);
            this.GICollectionStatus = Convert.ToInt32(rw["GICollectionStatus"]);
            this.GICollectionComment = Convert.ToString(rw["GICollectionComment"]);
            this.CreatedByUserID = String.IsNullOrEmpty(rw["CreatedByUserID"].ToString()) ? 0 : Convert.ToInt32(rw["CreatedByUserID"]);
            this.ModifiedByUserID = String.IsNullOrEmpty(rw["ModifiedByUserID"].ToString()) ? 0 : Convert.ToInt32(rw["ModifiedByUserID"]);
            this.DeletedByUserID = String.IsNullOrEmpty(rw["DeletedByUserID"].ToString()) ? 0 : Convert.ToInt32(rw["DeletedByUserID"]);
            this.CreatedDate = String.IsNullOrEmpty(rw["CreatedDate"].ToString()) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(rw["CreatedDate"]);
            this.ModifiedDate = String.IsNullOrEmpty(rw["ModifiedDate"].ToString()) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(rw["ModifiedDate"]);
            this.DeletedDate = String.IsNullOrEmpty(rw["DeletedDate"].ToString()) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(rw["DeletedDate"]);
        }

        internal string GetCollectionStatus()
        {
            var query = from col in clsSilex.tblSXCollection.AsEnumerable()
                        where col.Field<int>("CollectionID") == CollectionID
                        select col.Field<string>("CollectionName");
            return query.FirstOrDefault().ToString();
        }

        public string GetCollectionDesc()
        {
            string qry;
            var query = from col in clsSilex.tblSXCollection.AsEnumerable()
                        where col.Field<int>("CollectionID") == CollectionID
                        select col.Field<string>("CollectionDesc");
            qry = query.FirstOrDefault() == null ? "" : query.FirstOrDefault().ToString();
            return qry;
        }

        public string GetCollectionName()
        {
            var query = from col in clsSilex.tblSXCollection.AsEnumerable()
                        where col.Field<int>("CollectionID") == CollectionID
                        select col.Field<string>("CollectionName");
            return query.FirstOrDefault().ToString();
        }

        public string GetDivisionCode()
        {
            string qry = "";
            if (CollectionID > 99)
            {
                var query = from div in clsSilex.tblSXDivision.AsEnumerable()
                            from col in clsSilex.tblSXCollection.AsEnumerable()
                            where col.Field<int>("CollectionID") == CollectionID
                            && col.Field<int>("DivisionID") == div.Field<int>("DivisionID")
                            select div.Field<string>("DivisionCode");
                qry = query.FirstOrDefault().ToString();
            }
            return qry;
        }

        public string GetCollectionCode()
        {
            var query = from col in clsSilex.tblSXCollection.AsEnumerable()
                        where col.Field<int>("CollectionID") == CollectionID
                        select col.Field<string>("CollectionCode");
            return query.FirstOrDefault().ToString();
        }

        internal void InsertGICollection()
        {
            //Conexion.StartSession();
            string sql = "INSERT INTO " + clsGlobals.Gesin + "tblGICollection ([CollectionID], [GICollectionStatus], [GICollectionComment] " +
                ",[CreatedByUserID], [CreatedDate]) VALUES( " + this.CollectionID + ", " + "2" + ", '" + this.GICollectionComment + "', " +
                +clsGlobals.GIPar.UserID + ", GETDATE()" + ")";
            Conexion.GDatos.RunSql(sql);
            Conexion.EndSession();
        }

        internal void CopySXDatarow(DataRow rw)
        {
            this.CollectionID = Convert.ToInt32(rw["CollectionID"]);
            this.GICollectionComment = "";
            this.GICollectionID = 0;
            this.GICollectionStatus = Convert.ToInt32(rw["CollectionStatus"]);
            this.CreatedByUserID = clsGlobals.GIPar.UserID;
            this.ModifiedByUserID = 0;
            this.DeletedByUserID = 0;
            this.CreatedDate = DateTime.Now;
            this.ModifiedDate = Convert.ToDateTime("01/01/1900");
            this.DeletedDate = Convert.ToDateTime("01/01/1900");
        }

        internal void UpdateGICollection()
        {
            //Conexion.StartSession();
            string sql = "UPDATE " + clsGlobals.Gesin + "[tblGICollection] SET" +
                "[GICollectionStatus] = " + this.GICollectionStatus + ", " +
                "[GICollectionComment] = '" + this.GICollectionComment + "', " +
                "[ModifiedByUserID] = " + clsGlobals.GIPar.UserID + ", " +
                "[ModifiedDate] = GETDATE() " +
                "WHERE [GICollectionID] = " + this.GICollectionID;
            Conexion.GDatos.RunSql(sql);
            Conexion.EndSession();
        }
    }
}

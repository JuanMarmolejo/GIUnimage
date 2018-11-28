using prjGIUnimage.data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjGIUnimage.bus
{
    class clsListSeason
    {
        List<clsSeason> myList;

        public clsListSeason()
        {
            myList = new List<clsSeason>();
        }

        public int Quantity
        {
            get => myList.Count;
        }

        public List<clsSeason> Elements
        {
            get => myList;
            set => myList = value;
        }

        internal void GetAllSeasons()
        {
            string sql = "SELECT * FROM " + clsGlobals.Gesin + "[tblGISeason] ORDER BY [SXSeasonID] DESC";
            //Conexion.StartSession();
            DataTable myTb = Conexion.GDatos.GetDataTableSql(sql);
            Conexion.EndSession();
            Elements = CopyDataTable(myTb);
            UpdateSeasonsNames();
        }

        private void UpdateSeasonsNames()
        {
            clsSilex.UpDateSeasons();
            foreach(clsSeason ele in Elements)
            {
                var results = from myRow in clsSilex.tblSXSeason.AsEnumerable()
                              where myRow.Field<int>("SeasonID") == ele.SXSeasonID
                              select myRow.Field<string>("SeasonName_fra");
                ele.SeasonName = results.FirstOrDefault();

                results = from myRow in clsSilex.tblSXSeason.AsEnumerable()
                              where myRow.Field<int>("SeasonID") == ele.SXSeasonPrecID
                              select myRow.Field<string>("SeasonName_fra");
                ele.SeasonPrecName = results.FirstOrDefault();
            }
        }

        private List<clsSeason> CopyDataTable(DataTable myTb)
        {
            List<clsSeason> lstTem = new List<clsSeason>();
            foreach (DataRow rw in myTb.Rows)
            {
                clsSeason myCol = new clsSeason();
                myCol.CopyDatarow(rw);
                lstTem.Add(myCol);
            }
            return lstTem;
        }

        internal string PreviousSeason(int selectedValue)
        {
            var results = from clsSeason in Elements
                          where clsSeason.GISeasonID == selectedValue
                          select clsSeason.SeasonPrecName;
            return results.FirstOrDefault();
        }

        internal clsSeason GetSeasonByID(int gISeasonID)
        {
            foreach (clsSeason it in Elements)
            {
                if (it.GISeasonID == gISeasonID)
                {
                    return it;
                }
            }
            return new clsSeason();
        }

        internal void GetNotGeneratedAfiscal()
        {
            string sql = "SELECT * FROM " + clsGlobals.Gesin + "[tblGISeason] WHERE[StatusAfterFiscal] = 0 ORDER BY[SXSeasonID] DESC";
            //Conexion.StartSession();
            DataTable myTb = Conexion.GDatos.GetDataTableSql(sql);
            Conexion.EndSession();
            Elements = CopyDataTable(myTb);
            UpdateSeasonsNames();
        }

        internal void GetNotGeneratedBefiscal()
        {
            string sql = "SELECT * FROM " + clsGlobals.Gesin + "[tblGISeason] WHERE[StatusBeforeFiscal] = 0 ORDER BY[SXSeasonID] DESC";
            //Conexion.StartSession();
            DataTable myTb = Conexion.GDatos.GetDataTableSql(sql);
            Conexion.EndSession();
            Elements = CopyDataTable(myTb);
            UpdateSeasonsNames();
        }
    }
}

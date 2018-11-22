using prjGIUnimage.bus;
using prjGIUnimage.data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace prjGIUnimage
{
    class clsGlobals
    {
        //Bases de datos Dev JuanK
        public static string Silex = "[sxUnimageDev].[dbo].";
        public static string useSilex = "USE [sxUnimageDev]";
        public static string Gesin = "[sxUnimageDevGI].[dbo].";

        //Bases de datos Dev Stef
        //public static string Silex = "[sxUnimageDevStef].[dbo].";
        //public static string useSilex = "USE [sxUnimageDevStef]";
        //public static string Gesin = "[sxUnimageDevGIStef].[dbo].";

        //Variables
        internal static double ActiveRatio;
        internal static double BkRatio;
        public static string sql;
        public static int NextScenarioID;
        public static int GISeasonID;
        public static bool Flag;
        public static bool AvaibleFlag;
        public static bool CollectionsFlag;
        public static int OriginOfStoredProc;
        public static int giVOID;
        public static int ScProductID;
        public static int ProductEquiID;
        //public static int SeasonsProductEqui;
        public static int SecondSeasonID;
        public static int ParentProductID;

        //Classes
        public static clsGIParameter GIPar;
        public static clsScVorder Vorder;

        //Lists
        public static clsListElements ListStyles;
        public static clsListElements ListGroups;
        public static clsListElements ListColors;
        public static clsListElements ListCollections;
        public static clsListElements ListTemp;
        public static clsListElements lstBackUp;
        public static clsListScVorderDetail VorderDetail;
        internal static clsSeason mySea;

        //public static DataTable reqVirtuals;
    }
}

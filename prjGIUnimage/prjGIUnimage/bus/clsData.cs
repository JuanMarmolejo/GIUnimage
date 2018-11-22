using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjGIUnimage.bus
{
    class clsData
    {
        public int DataID { get; set; }
        public int DataGroupID { get; set; }
        public int DataValue { get; set; }
        public bool IsHidden { get; set; }
        public bool IsSystem { get; set; }
        public int DataSort { get; set; }
        public string DataDesc_eng { get; set; }
        public string DataDesc_fra { get; set; }

        internal void CopyDatarow(DataRow rw)
        {
            this.DataID = Convert.ToInt32(rw["DataID"]);
            this.DataGroupID = Convert.ToInt32(rw["DataGroupID"]);
            this.DataValue = Convert.ToInt16(rw["DataValue"]);
            this.IsHidden = Convert.ToBoolean(rw["IsHidden"]);
            this.IsSystem = Convert.ToBoolean(rw["IsSystem"]);
            this.DataSort = Convert.ToInt16(rw["DataSort"]);
            this.DataDesc_eng = Convert.ToString(rw["DataDesc_eng"]);
            this.DataDesc_fra = Convert.ToString(rw["DataDesc_fra"]);
            
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjGIUnimage.bus
{
    class clsListSCCollection
    {
        List<clsScCollection> myList;

        public clsListSCCollection()
        {
            this.myList = new List<clsScCollection>();
        }

        public int Quantity
        {
            get => myList.Count;
        }

        public List<clsScCollection> Elements
        {
            get => myList;
            set => myList = value;
        }
    }
}

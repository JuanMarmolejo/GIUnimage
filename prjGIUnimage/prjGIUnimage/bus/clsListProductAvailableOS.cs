using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjGIUnimage.bus
{
    class clsListProductAvailableOS
    {
        List<clsProductAvailableOS> myList;

        public clsListProductAvailableOS()
        {
            this.myList = new List<clsProductAvailableOS>();
        }

        public int Quantity
        {
            get => myList.Count;
        }

        public List<clsProductAvailableOS> Elements
        {
            get => myList;
            set => myList = value;
        }

        internal void Add(clsProductAvailableOS myEle)
        {
            Elements.Add(myEle);
        }
    }
}

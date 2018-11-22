using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjGIUnimage.bus
{
    class clsListProductOrdered
    {
        List<clsProductOrdered> myList;

        public clsListProductOrdered()
        {
            this.myList = new List<clsProductOrdered>();
        }

        public int Quantity
        {
            get => myList.Count;
        }

        public List<clsProductOrdered> Elements
        {
            get => myList;
            set => myList = value;
        }

        internal void Add(clsProductOrdered myEle)
        {
            Elements.Add(myEle);
        }

        internal void Insert(int v, clsProductOrdered nPord)
        {
            Elements.Insert(v, nPord);
        }
    }
}

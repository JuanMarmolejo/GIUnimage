using prjGIUnimage.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjGIUnimage
{
    class clsListInvFiscal
    {
        List<clsInvFiscal> myList;

        public clsListInvFiscal()
        {
            this.myList = new List<clsInvFiscal>();
        }

        public int Quantity
        {
            get => myList.Count;
        }

        public List<clsInvFiscal> Elements
        {
            get => myList;
            set => myList = value;
        }

        internal void AddInvFiscal(clsInvFiscal myInv)
        {
            Elements.Add(myInv);
        }

        internal void SaveListInvFiscal(bool flag)
        {
            //Conexion.StartSession();
            foreach(clsInvFiscal ele in Elements)
            {
                ele.InsertInvFiscal(flag);
            }
            Conexion.EndSession();
        }
    }
}

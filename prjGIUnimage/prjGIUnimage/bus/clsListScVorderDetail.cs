using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjGIUnimage.bus
{
    class clsListScVorderDetail
    {
        List<clsScVorderDetail> myList;

        public clsListScVorderDetail()
        {
            myList = new List<clsScVorderDetail>();
        }

        public int Quantity
        {
            get => myList.Count;
        }

        public List<clsScVorderDetail> Elements
        {
            get => myList;
            set => myList = value;
        }

        internal void AddScVorderDetail(clsScVorderDetail myVODetails)
        {
            Elements.Add(myVODetails);
        }

        internal void SaveVorderDetail(int giVOID)
        {
            clsScVorderDetail tmp = new clsScVorderDetail();
            foreach (clsScVorderDetail ele in Elements)
            {
                clsProductAvailableOS myPava = new clsProductAvailableOS(ele, ele.OrderQty);
                if (ele.OrderQty != 0)
                {
                    ele.SaveScVorderDetail(giVOID);
                }
                if (clsGlobals.AvaibleFlag && clsGlobals.CollectionsFlag)
                {
                    try
                    {
                        myPava.UpdateQtyOrdered(clsGlobals.ParentProductID);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                
                tmp = ele;
            }
            clsGlobals.ScProductID = clsScProduct.UpdateVOStatus(tmp);
        }

        internal void SaveVorderDetailUnimage(int giVOID)
        {
            clsScVorderDetail tmp = new clsScVorderDetail();
            foreach (clsScVorderDetail ele in Elements)
            {
                clsProductAvailableOS myPava = new clsProductAvailableOS(ele, ele.OrderQty);
                if (ele.OrderQty != 0)
                {
                    ele.SaveScVorderDetail(giVOID);
                    ele.SaveScVorderDetailUnimage(giVOID);
                }
                if (clsGlobals.AvaibleFlag && clsGlobals.CollectionsFlag)
                {
                    try
                    {
                        myPava.UpdateQtyOrdered(clsGlobals.ParentProductID);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

                tmp = ele;
            }
            clsGlobals.ScProductID = clsScProduct.UpdateVOStatus(tmp);
        }
    }
}

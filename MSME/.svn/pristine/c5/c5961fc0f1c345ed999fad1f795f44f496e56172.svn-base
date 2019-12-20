using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Portal.Core.ViewModel;
using Portal.DAL;
using Portal.Common;
using System.Reflection;
using System.Data;

namespace Portal.Core
{
    public class GstBL
    {
        DBInterface dbInterface;
        public GstBL()
        {
            dbInterface = new DBInterface();
        }
        public List<GSTViewModel> GetGSTList()
        {
            List<GSTViewModel> gsts = new List<GSTViewModel>();
            try
            {
                List<GST> gstList = dbInterface.GetGSTList();
                if (gstList != null && gstList.Count > 0)
                {
                    foreach (GST gst in gstList)
                    {
                        gsts.Add(new GSTViewModel { GSTID = gst.GSTID, GSTName = gst.GSTName });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return gsts;
        }

    }
}

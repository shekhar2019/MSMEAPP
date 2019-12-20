using Portal.Common;
using Portal.Core.ViewModel;
using Portal.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Core
{
   public  class GSTPercentBL
    {
        DBInterface dbInterface;
        public GSTPercentBL()
        {
            dbInterface = new DBInterface();
        }

        public ResponseOut AddEditGSTPercent(GSTPercentViewModel gstPercentViewModel)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                GSTPercent gstPercent = new GSTPercent
                {
                    GSTPercentageID = gstPercentViewModel.GSTPercentageID,
                    GSTPercentageName = gstPercentViewModel.GSTPercentageName,
                    GSTPercentageStatus = gstPercentViewModel.GSTPercentageStatus
                };
                responseOut = dbInterface.AddEditGSTPercent(gstPercent);
            }
            catch (Exception ex)
            {
                responseOut.status = ActionStatus.Fail;
                responseOut.message = ActionMessage.ApplicationException;
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return responseOut;
        }
        public List<GSTPercentViewModel> GetGSTPercentList()
        {
            List<GSTPercentViewModel> gsts = new List<GSTPercentViewModel>();
            try
            {
                List<GSTPercent> gstList = dbInterface.GetGSTPercentList();
                if (gstList != null && gstList.Count > 0)
                {
                    foreach (GSTPercent gst in gstList)
                    {
                        gsts.Add(new GSTPercentViewModel { GSTPercentageID = gst.GSTPercentageID, GSTPercentageName = gst.GSTPercentageName,GSTPercentageStatus=gst.GSTPercentageStatus });
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


        public ResponseOut RemoveGSTPercent(int gstPercentageID)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                responseOut = dbInterface.RemoveGSTPercent(gstPercentageID);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return responseOut;
        }
    }
}

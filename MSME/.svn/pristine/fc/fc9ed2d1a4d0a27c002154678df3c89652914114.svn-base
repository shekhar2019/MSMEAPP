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
    public class DeliveryStatusBL
    {
        DBInterface dbInterface;
        public DeliveryStatusBL()
        {
            dbInterface = new DBInterface();
        }
        public List<DeliveryStatusViewModel> GetDeliveryStatusList()
        {
            List<DeliveryStatusViewModel> gsts = new List<DeliveryStatusViewModel>();
            try
            {
                List<DeliveryStatu> gstList = dbInterface.GetDeliveryStatusList();
                if (gstList != null && gstList.Count > 0)
                {
                    foreach (DeliveryStatu ds in gstList)
                    {
                        gsts.Add(new DeliveryStatusViewModel { DelivryStatusID = ds.DelivryStatusID, DeliveryName = ds.DeliveryName });
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

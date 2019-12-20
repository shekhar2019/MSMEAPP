using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Portal.Core;
using Portal.Core.ViewModel;
using Portal.Common;
using System.Reflection;
using Microsoft.Reporting.WebForms;
using Microsoft.ReportingServices;
using System.IO;


namespace Portal.Controllers
{
    public class DeliveryChallanRegisterController : BaseController
    {
      

        [ValidateRequest(true, UserInterfaceHelper.Add_DeliveryChallanRegister, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult ListDeliveryChallanRegister()
        {
            try
            {
                FinYearViewModel finYear = Session[SessionKey.CurrentFinYear] != null ? (FinYearViewModel)Session[SessionKey.CurrentFinYear] : new FinYearViewModel();

                ViewData["fromDate"] = finYear.StartDate;
                ViewData["toDate"] = finYear.EndDate;

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return View();
        }


        [HttpGet]
        public PartialViewResult GetDeliveryChallanRegisterList(int customerId = 0, int stateId = 0, int shippingstateId = 0, string fromDate = "", string toDate = "", int createdBy = 0, string sortBy = "", string sortOrder = "")
        {           
            DeliveryChallanRegisterBL deliveryChallanRegisterBL = new DeliveryChallanRegisterBL();
            List<DeliveryChallanViewModel> deliveryChallanViewModel = new List<DeliveryChallanViewModel>();
            try
            {
                deliveryChallanViewModel = deliveryChallanRegisterBL.GetDeliveryChallanRegisterList(customerId, stateId, shippingstateId, fromDate, toDate, createdBy, ContextUser.CompanyId, sortBy, sortOrder);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(deliveryChallanViewModel);
        }




    }
}

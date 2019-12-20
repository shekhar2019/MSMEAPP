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
    public class SORegisterController : BaseController
    {
        //
        // GET: /SORegister/
          

        [ValidateRequest(true, UserInterfaceHelper.Add_SORegister, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult ListSORegister()
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
        public PartialViewResult GetSORegisterList(int customerId = 0, int stateId = 0, int shippingstateId = 0, string fromDate = "", string toDate = "", int createdBy = 0, string sortBy = "", string sortOrder = "")
        {
            List<SOViewModel> sos = new List<SOViewModel>();
            SORegisterBL soregisterBL = new SORegisterBL();
            try
            {
                sos = soregisterBL.GetSORegisterList(customerId, stateId, shippingstateId, fromDate, toDate, createdBy, ContextUser.CompanyId, sortBy, sortOrder);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(sos);
        }




    }
}

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
    public class QuotationRegisterController : BaseController
    {
        //
        // GET: /QuotationRegister/
         

        [ValidateRequest(true, UserInterfaceHelper.Add_QuotationRegister, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult ListQuotationRegister()
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
        public PartialViewResult GetQuotationRegisterList( int customerId = 0, int stateId = 0, string fromDate = "", string toDate = "", int createdBy = 0 ,string sortBy="", string sortOrder = "")
        {
            List<QuotationViewModel> quotations = new List<QuotationViewModel>();
            QuotationRegisterBL quotationregisterBL = new QuotationRegisterBL();
            try
             {
                
                quotations = quotationregisterBL.GetQuotationRegisterList(customerId, stateId, fromDate, toDate, ContextUser.CompanyId, createdBy, sortBy, sortOrder);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(quotations);
        }



    }
}

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
    public class CustomerFormRegisterController : BaseController
    {
        //
        // GET: /CustomerFromRegister/
        [ValidateRequest(true, UserInterfaceHelper.AddCustomer_Form_Register, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult ListCFormRegister()
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
        public PartialViewResult GetCustomerFormRegisterList(string formStatus="", int customerId =0, string invoiceNo="", string refNo="", string fromDate="", string toDate="", int createdBy=0, string sortBy="", string sortOrder="")
        {
            List<CustomerFormViewModel> customerFormList = new List<CustomerFormViewModel>();
            
            CustomerFormRegisterBL customerFormRegisterBL = new CustomerFormRegisterBL();
            try
            {
                customerFormList = customerFormRegisterBL.GetCustomerFormRegisterList(formStatus, customerId, invoiceNo,refNo,fromDate,toDate,ContextUser.CompanyId,createdBy,sortBy,sortOrder);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(customerFormList);
        }

    }
}

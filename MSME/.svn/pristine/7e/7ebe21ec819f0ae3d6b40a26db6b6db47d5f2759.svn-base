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
using System.Data;
namespace Portal.Controllers
{
    public class SaleInvoiceRegisterController : BaseController
    {
        #region SaleInvoiceRegister

        //
        // GET: /SaleInvoiceRegister/ 


        [ValidateRequest(true, UserInterfaceHelper.Add_SaleInvoiceRegister, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult ListSaleInvoiceRegister()
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
        public PartialViewResult GetSaleInvoiceRegisterList(int customerId = 0, int stateId = 0, int shippingstateId = 0, string fromDate = "", string toDate = "", int createdBy = 0, string sortBy = "", string sortOrder = "")
        {
            List<SaleInvoiceViewModel> saleinvoices = new List<SaleInvoiceViewModel>();
              SaleInvoiceRegisterBL saleinvoiceregisterBL = new SaleInvoiceRegisterBL();
            try
            {

                saleinvoices = saleinvoiceregisterBL.GetSaleInvoiceRegisterList(customerId, stateId, shippingstateId, fromDate, toDate, ContextUser.CompanyId, createdBy, sortBy, sortOrder);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(saleinvoices);
        }

         

        #endregion
    }
}

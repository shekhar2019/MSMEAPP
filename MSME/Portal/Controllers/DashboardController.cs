using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Portal.Core;
using Portal.Core.ViewModel;
using Portal.Common;
using System.Reflection;
namespace Portal.Controllers
{
    public class DashboardController : BaseController
    {
        //
        // GET: /ModuleDashboard/

        public ActionResult ModuleDashboard()
        {
            return View();
        }
        public ActionResult AdminDashboard()
        {
            return View();
        }
        public ActionResult CRMDashboard()
        {
           return View();
        }
        public ActionResult SaleDashboard()
        {
            try
            {
                FinYearViewModel currentFinYear = (FinYearViewModel)Session[SessionKey.CurrentFinYear];
                ViewData["currentFinyearId"] = currentFinYear.FinYearId;
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return View();
        }
        [HttpPost]
        public void SetFinancialYear(int finYearId)
        {
            try
            {
                SessionWrapper session = new SessionWrapper();
                FinYearBL finYearBL = new FinYearBL();
                FinYearViewModel currentFinYear = finYearBL.GetCurrentFinancialYear(finYearId);
                session.SetInSession(SessionKey.CurrentFinYear, currentFinYear);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return;
        }
        public ActionResult PurchaseDashboard()
        {
            try
            {
                FinYearViewModel currentFinYear = (FinYearViewModel)Session[SessionKey.CurrentFinYear];
                ViewData["currentFinyearId"] = currentFinYear.FinYearId;
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return View();
        }
        public ActionResult InventoryDashboard()
        {
            try
            {
                FinYearViewModel currentFinYear = (FinYearViewModel)Session[SessionKey.CurrentFinYear];
                ViewData["currentFinyearId"] = currentFinYear.FinYearId;
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return View();
        }
        public ActionResult AccountDashboard()
        {
            try
            {
                FinYearViewModel currentFinYear = (FinYearViewModel)Session[SessionKey.CurrentFinYear];
                ViewData["currentFinyearId"] = currentFinYear.FinYearId;
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return View();
        }
        public ActionResult HRDashboard()
        {
           return View();
        }
        public ActionResult PayrollDashboard()
        {
            try
            {
                FinYearViewModel currentFinYear = (FinYearViewModel)Session[SessionKey.CurrentFinYear];
                ViewData["currentFinyearId"] = currentFinYear.FinYearId;
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return View();
        }

        public ActionResult BuyerDashboard()
        {
            try
            {
                FinYearViewModel currentFinYear = (FinYearViewModel)Session[SessionKey.CurrentFinYear];
                ViewData["currentFinyearId"] = currentFinYear.FinYearId;
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return View();
        }
        public ActionResult SalerDashboard()
        {
            return View();
        }


    }
}

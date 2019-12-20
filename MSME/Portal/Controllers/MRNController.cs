using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Portal.Core;
using Portal.Core.ViewModel;
using Portal.Common;
using System.Reflection;
using System.Data;
using Microsoft.Reporting.WebForms;
using System.IO;

namespace Portal.Controllers
{
    public class MRNController : BaseController
    {
        //
        // GET: /MRN/
        #region MRN
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_MRN, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult AddEditMRN(int mrnId = 0, int accessMode = 3)
        {
            try
            {
                FinYearViewModel finYear = Session[SessionKey.CurrentFinYear] != null ? (FinYearViewModel)Session[SessionKey.CurrentFinYear] : new FinYearViewModel();
                ViewData["fromDate"] = finYear.StartDate;
                ViewData["toDate"] = finYear.EndDate;
                ViewData["currentDate"] = DateTime.Now.ToString("dd-MMM-yyyy");
                if (mrnId != 0)
                {
                    ViewData["mrnId"] = mrnId;
                    ViewData["accessMode"] = accessMode;
                }
                else
                {
                    ViewData["mrnId"] = 0;
                    ViewData["accessMode"] = 0;
                }

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return View();
        }

        [HttpPost]
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_MRN, (int)AccessMode.AddAccess, (int)RequestMode.Ajax)]
        public ActionResult AddEditMRN(MRNViewModel mrnViewModel, List<MRNProductDetailViewModel> mrnProducts)
        {
            ResponseOut responseOut = new ResponseOut();
            MRNBL mrnBL = new MRNBL();
            try
            {
                if (mrnViewModel != null)
                {
                    mrnViewModel.CreatedBy = ContextUser.UserId;
                    mrnViewModel.CompanyId = ContextUser.CompanyId;
                    mrnViewModel.FinYearId = Session[SessionKey.CurrentFinYear] != null ? ((FinYearViewModel)Session[SessionKey.CurrentFinYear]).FinYearId : DateTime.Now.Year;
                    responseOut = mrnBL.AddEditMRN(mrnViewModel, mrnProducts);
                }
                else
                {
                    responseOut.message = ActionMessage.ProbleminData;
                    responseOut.status = ActionStatus.Fail;
                    responseOut.trnId = 0;
                }

            }
            catch (Exception ex)
            {
                responseOut.message = ActionMessage.ApplicationException;
                responseOut.status = ActionStatus.Fail;
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(responseOut, JsonRequestBehavior.AllowGet);
        }
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_MRN, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult ListMRN()
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
        public PartialViewResult GetMRNList(string mrnNo = "", string vendorName = "", string dispatchrefNo = "", string fromDate = "", string toDate = "", string approvalStatus = "")
        {
            List<MRNViewModel> mrns = new List<MRNViewModel>();
            MRNBL mrnBL = new MRNBL();
            try
            {
                mrns = mrnBL.GetMRNList(mrnNo, vendorName, dispatchrefNo, fromDate, toDate, ContextUser.CompanyId, approvalStatus);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(mrns);
        }
        [HttpGet]
        public PartialViewResult GetPurchaseInvoiceList(string piNo="", string vendorName="", string refNo="", string fromDate="", string toDate="", string approvalStatus = "", string displayType = "")
        {
            List<PurchaseInvoiceViewModel> invoices = new List<PurchaseInvoiceViewModel>();
            
            PurchaseInvoiceBL purchaseInvoiceBL = new PurchaseInvoiceBL();
            try
            {
                
               invoices = purchaseInvoiceBL.GetPIList(piNo,vendorName, refNo, fromDate, toDate, ContextUser.CompanyId,approvalStatus, displayType);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(invoices);
        }

        [HttpPost]
        public PartialViewResult GetMRNProductList(List<MRNProductDetailViewModel> mrnProducts, long mrnId)
        {
            MRNBL mrnBL = new MRNBL();
            try
            {
                if (mrnProducts == null)
                {
                    mrnProducts = mrnBL.GetMRNProductList(mrnId);
                }

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(mrnProducts);
        }

        [HttpGet]
        public JsonResult GetMRNDetail(long mrnId)
        {
            MRNBL mrnBL = new MRNBL();
            MRNViewModel mrn = new MRNViewModel();
            try
            {
                mrn = mrnBL.GetMRNDetail(mrnId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(mrn, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Report(long mrnId, string reportType = "PDF")
        {
            LocalReport lr = new LocalReport();
            MRNBL mrnBL = new MRNBL();
            PurchaseInvoiceBL purchaseInvoiceBL = new PurchaseInvoiceBL();
            string path = Path.Combine(Server.MapPath("~/RDLC"), "MRNPrint.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("Index");
            }

            DataTable dt = new DataTable();
            dt = mrnBL.GetMRNDetailDataTable(mrnId);

            //decimal totalInvoiceAmount = 0;
            //totalInvoiceAmount = Convert.ToDecimal(dt.Rows[0]["TotalValue"].ToString());
            //string totalWords = CommonHelper.changeToWords(totalInvoiceAmount.ToString());

            ReportDataSource rd = new ReportDataSource("DataSet1", dt);
            ReportDataSource rdProduct = new ReportDataSource("DataSetProduct",mrnBL.GetMRNProductListDataTable(mrnId));
            
            lr.DataSources.Add(rd);
            lr.DataSources.Add(rdProduct);
            

            //ReportParameter rp1 = new ReportParameter("AmountInWords", totalWords);
            //ReportParameter rp2 = new ReportParameter("ReportOption", reportOption);
            //lr.SetParameters(rp1);
            //lr.SetParameters(rp2);

            string mimeType;
            string encoding;
            string fileNameExtension;



            string deviceInfo = "<DeviceInfo>" +
            "  <OutputFormat>" + reportType + "</OutputFormat>" +
            "  <PageWidth>8.5in</PageWidth>" +
            "  <PageHeight>11in</PageHeight>" +
            "  <MarginTop>0.50in</MarginTop>" +
            "  <MarginLeft>.2in</MarginLeft>" +
            "  <MarginRight>.2in</MarginRight>" +
            "  <MarginBottom>0.5in</MarginBottom>" +
            "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = lr.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);


            return File(renderedBytes, mimeType);
        }
        #endregion
    }
}

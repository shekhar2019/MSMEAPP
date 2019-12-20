﻿using System;
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
    public class STRController : BaseController
    {
        //
        // GET: /STR/

        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_STR, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult AddEditSTR(int strId = 0, int accessMode = 3)
        {
            try
            {
                FinYearViewModel finYear = Session[SessionKey.CurrentFinYear] != null ? (FinYearViewModel)Session[SessionKey.CurrentFinYear] : new FinYearViewModel();
                ViewData["fromDate"] = finYear.StartDate;
                ViewData["toDate"] = finYear.EndDate;
                ViewData["currentDate"] = DateTime.Now.ToString("dd-MMM-yyyy");
                if (strId != 0)
                {
                    ViewData["strId"] = strId;
                    ViewData["accessMode"] = accessMode;
                }
                else
                {
                    ViewData["strId"] = 0;
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
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_STR, (int)AccessMode.AddAccess, (int)RequestMode.Ajax)]
        public ActionResult AddEditSTR(STRViewModel strViewModel, List<STRProductDetailViewModel> strProductDetailViewModel)
        {
            ResponseOut responseOut = new ResponseOut();

            STRBL stnBL = new STRBL();
            try
            {
                if (strViewModel != null)
                {
                    strViewModel.CreatedBy = ContextUser.UserId;
                    strViewModel.CompanyId = ContextUser.CompanyId;
                    strViewModel.FinYearId = Session[SessionKey.CurrentFinYear] != null ? ((FinYearViewModel)Session[SessionKey.CurrentFinYear]).FinYearId : DateTime.Now.Year;
                    responseOut = stnBL.AddEditSTR(strViewModel, strProductDetailViewModel);

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

        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_STR, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult ListSTR()
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

        [HttpPost]
        public PartialViewResult GetSTRProductList(List<STRProductDetailViewModel> strProducts, long strId)
        {
            STRBL strBL = new STRBL();

            try
            {
                if (strProducts == null)
                {
                    strProducts = strBL.GetSTRProductList(strId);
                }

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(strProducts);
        }

        [HttpGet]
        public PartialViewResult GetSTNList(string stnNo = "", string glNo = "", int fromLocation = 0, int toLocation = 0, string fromDate = "", string toDate = "", string displayType = "", string approvalStatus = "")
        {
            List<STNViewModel> stns = new List<STNViewModel>();

            STNBL stnBL = new STNBL();
            try
            {
                stns = stnBL.GetSTNList(stnNo, glNo, fromLocation, toLocation, fromDate, toDate, ContextUser.CompanyId, displayType,approvalStatus);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(stns);
        }

        public JsonResult GetSTRDetail(long strId)
        {
            
            STRBL strBL = new STRBL();

            STRViewModel str = new STRViewModel();
            try
            {
                str = strBL.GetSTRDetail(strId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(str, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult GetSTRList(string strNo = "", string grNo = "", int fromLocation = 0, int toLocation = 0, string fromDate = "", string toDate = "",string approvalStatus="")
        {
            List<STRViewModel> strs = new List<STRViewModel>();

            STRBL strBL = new STRBL();
            try
            {
                strs = strBL.GetSTRList(strNo, grNo, fromLocation, toLocation, fromDate, toDate, ContextUser.CompanyId, approvalStatus);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(strs);
        }

        public ActionResult Report(long strId, string reportType = "PDF")
        {
            LocalReport lr = new LocalReport();
            STRBL strBL = new STRBL();

            //PurchaseInvoiceBL purchaseInvoiceBL = new PurchaseInvoiceBL();
            string path = Path.Combine(Server.MapPath("~/RDLC"), "STRPrint.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("Index");
            }

            DataTable dt = new DataTable();
            dt = strBL.GetSTRDetailDataTable(strId);

            //decimal totalInvoiceAmount = 0;
            //totalInvoiceAmount = Convert.ToDecimal(dt.Rows[0]["TotalValue"].ToString());
            //string totalWords = CommonHelper.changeToWords(totalInvoiceAmount.ToString());

            ReportDataSource rd = new ReportDataSource("DataSet1", dt);
            ReportDataSource rdProduct = new ReportDataSource("DataSetProduct", strBL.GetSTRProductListDataTable(strId));

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
    }
}

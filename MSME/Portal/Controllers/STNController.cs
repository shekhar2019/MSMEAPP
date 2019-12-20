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
    public class STNController : BaseController
    {
     
        // GET: /STN/
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_STN, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult AddEditSTN(int stnId = 0, int accessMode = 3)
        {
            try
            {
                FinYearViewModel finYear = Session[SessionKey.CurrentFinYear] != null ? (FinYearViewModel)Session[SessionKey.CurrentFinYear] : new FinYearViewModel();
                ViewData["fromDate"] = finYear.StartDate;
                ViewData["toDate"] = finYear.EndDate;
                ViewData["currentDate"] = DateTime.Now.ToString("dd-MMM-yyyy");
                if (stnId != 0)
                {
                    ViewData["stnId"] = stnId;
                    ViewData["accessMode"] = accessMode;
                }
                else
                {
                    ViewData["stnId"] = 0;
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
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_STN, (int)AccessMode.AddAccess, (int)RequestMode.Ajax)]
        public ActionResult AddEditSTN(STNViewModel stnViewModel, List<STNProductDetailViewModel> stnProductDetailViewModel)
        {
            ResponseOut responseOut = new ResponseOut();
            
            STNBL stnBL = new STNBL();
            try
            {
                if (stnViewModel != null)
                {
                    stnViewModel.CreatedBy = ContextUser.UserId;
                    stnViewModel.CompanyId = ContextUser.CompanyId;
                    stnViewModel.FinYearId = Session[SessionKey.CurrentFinYear] != null ? ((FinYearViewModel)Session[SessionKey.CurrentFinYear]).FinYearId : DateTime.Now.Year;
                    responseOut = stnBL.AddEditSTN(stnViewModel,stnProductDetailViewModel);

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

        [HttpGet]
        public JsonResult GetCompanyBranchList(int companyId)
        {
            CompanyBL companyBL = new CompanyBL();
            List<CompanyBranchViewModel> companyBranchList = new List<CompanyBranchViewModel>();
            try
            {
                companyBranchList = companyBL.GetCompanyBranchList(ContextUser.CompanyId);
                
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(companyBranchList, JsonRequestBehavior.AllowGet);
        }

        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_STN, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult ListSTN()
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
        public PartialViewResult GetSTNList(string stnNo = "", string glNo = "", int fromLocation=0,int toLocation=0,string fromDate = "", string toDate = "", string displayType = "",string approvalStatus="")
        {
            List<STNViewModel> stns = new List<STNViewModel>();
            
            STNBL stnBL = new STNBL();
            try
            {
                stns = stnBL.GetSTNList(stnNo,glNo, fromLocation, toLocation, fromDate, toDate, ContextUser.CompanyId, displayType,approvalStatus);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(stns);
        }

        [HttpPost]
        public PartialViewResult GetSTNProductList(List<STNProductDetailViewModel> stnProducts, long stnId)
        {
            STNBL stnBL = new STNBL();

            try
            {
                if (stnProducts == null)
                {
                    stnProducts = stnBL.GetSTNProductList(stnId);
                }

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(stnProducts);
        }
        public JsonResult GetSTNDetail(long stnId)
        {
            STNBL stnBL = new STNBL();
            
            STNViewModel stn = new STNViewModel();
            try
            {
                stn = stnBL.GetSTNDetail(stnId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(stn, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Report(long stnId, string reportType = "PDF")
        {
            LocalReport lr = new LocalReport();
            STNBL stnBL = new STNBL();
            
            //PurchaseInvoiceBL purchaseInvoiceBL = new PurchaseInvoiceBL();
            string path = Path.Combine(Server.MapPath("~/RDLC"), "STNPrint.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("Index");
            }

            DataTable dt = new DataTable();
            dt = stnBL.GetSTNDetailDataTable(stnId);

            //decimal totalInvoiceAmount = 0;
            //totalInvoiceAmount = Convert.ToDecimal(dt.Rows[0]["TotalValue"].ToString());
            //string totalWords = CommonHelper.changeToWords(totalInvoiceAmount.ToString());

            ReportDataSource rd = new ReportDataSource("DataSet1", dt);
            ReportDataSource rdProduct = new ReportDataSource("DataSetProduct", stnBL.GetSTNProductListDataTable(stnId));

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

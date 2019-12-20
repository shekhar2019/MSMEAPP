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
    public class StockLedgerController : BaseController
    {
        //
        // GET: /Product/
        //
        // GET: /User/
        
        [ValidateRequest(true, UserInterfaceHelper.Print_Stock_Ledger, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult PrintStockLedger(int productId = 0, int accessMode = 3)
        {

            try
            {
                FinYearViewModel finYear = Session[SessionKey.CurrentFinYear] != null ? (FinYearViewModel)Session[SessionKey.CurrentFinYear] : new FinYearViewModel();

                ViewData["fromDate"] = finYear.StartDate;
                ViewData["toDate"] = finYear.EndDate;

                if (productId != 0)
                {
                    ViewData["productId"] = productId;
                    ViewData["accessMode"] = accessMode;
                }
                else
                {
                    ViewData["productId"] = 0;
                    ViewData["accessMode"] = 0;
                }

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return View();
        }

        public ActionResult Report(int productTypeId,string assemblyType,int productMainGroupId, int productSubGroupId,long productId,int customerBranchId,string fromDate, string toDate, string reportType = "PDF")
        {
            LocalReport lr = new LocalReport();
            StockLedgerBL stockLedgerBL = new StockLedgerBL();
            string path = Path.Combine(Server.MapPath("~/RDLC"), "StockLedger.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("PrintStockLedger");
            }

            DataTable dt = new DataTable();
            dt = stockLedgerBL.GetStockLedgerDataTable(productTypeId, assemblyType, productMainGroupId, productSubGroupId, productId, customerBranchId,Convert.ToDateTime(fromDate),Convert.ToDateTime(toDate), ContextUser.CompanyId);

            
            ReportDataSource rd = new ReportDataSource("DataSet1", dt);
            lr.DataSources.Add(rd);
            
            string mimeType;
            string encoding;
            string fileNameExtension;



            string deviceInfo =

            "<DeviceInfo>" +
            "  <OutputFormat>" + reportType + "</OutputFormat>" +
            "  <PageWidth>11in</PageWidth>" +
            "  <PageHeight>8.5in</PageHeight>" +
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
        public ActionResult SummaryReport(int productTypeId, string assemblyType, int productMainGroupId, int productSubGroupId, long productId, int customerBranchId, string fromDate, string toDate, string reportType = "PDF")
        {
            LocalReport lr = new LocalReport();
            StockLedgerBL stockLedgerBL = new StockLedgerBL();
            string path = Path.Combine(Server.MapPath("~/RDLC"), "StockSummary.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("PrintStockLedger");
            }

            DataTable dt = new DataTable();
            dt = stockLedgerBL.GetStockSummaryDataTable(productTypeId, assemblyType, productMainGroupId, productSubGroupId, productId, customerBranchId, Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate), ContextUser.CompanyId);


            ReportDataSource rd = new ReportDataSource("DataSet1", dt);
            lr.DataSources.Add(rd);

            string mimeType;
            string encoding;
            string fileNameExtension;



            string deviceInfo =

            "<DeviceInfo>" +
            "  <OutputFormat>" + reportType + "</OutputFormat>" +
            "  <PageWidth>11in</PageWidth>" +
            "  <PageHeight>8.5in</PageHeight>" +
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

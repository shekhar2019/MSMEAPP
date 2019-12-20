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
using System.Text;
namespace Portal.Controllers
{
    public class SaleInvoiceController : BaseController
    {
        #region SaleInvoice

        //
        // GET: /Quotation/

        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_SaleInvoice, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult AddEditSaleInvoice(int siId = 0, int accessMode = 3)
        {
            try
            {
                FinYearViewModel finYear = Session[SessionKey.CurrentFinYear] != null ? (FinYearViewModel)Session[SessionKey.CurrentFinYear] : new FinYearViewModel();

                ViewData["fromDate"] = finYear.StartDate;
                ViewData["toDate"] = finYear.EndDate;
                ViewData["currentDate"] = DateTime.Now.ToString("dd-MMM-yyyy");

                if (siId != 0)
                {
                    ViewData["siId"] = siId;
                    ViewData["accessMode"] = accessMode;
                    
                }
                else
                {
                    ViewData["siId"] = 0;
                    ViewData["accessMode"] = 3;
                    
                }

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return View();
        }


        [HttpPost]
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_SaleInvoice, (int)AccessMode.AddAccess, (int)RequestMode.Ajax)]
        public ActionResult AddEditSaleInvoice(SaleInvoiceViewModel saleinvoiceViewModel, List<SaleInvoiceProductViewModel> saleinvoiceProducts, List<SaleInvoiceTaxViewModel> saleinvoiceTaxes, List<SaleInvoiceTermViewModel> saleinvoiceTerms)
        {
            ResponseOut responseOut = new ResponseOut();
            SaleInvoiceBL saleinvoiceBL = new SaleInvoiceBL();
            try
            {
                if (saleinvoiceViewModel != null)
                {
                    saleinvoiceViewModel.CreatedBy = ContextUser.UserId;
                    saleinvoiceViewModel.CompanyId = ContextUser.CompanyId;
                    saleinvoiceViewModel.FinYearId = Session[SessionKey.CurrentFinYear] != null ? ((FinYearViewModel)Session[SessionKey.CurrentFinYear]).FinYearId : DateTime.Now.Year;
                    responseOut = saleinvoiceBL.AddEditSaleInvoice(saleinvoiceViewModel, saleinvoiceProducts, saleinvoiceTaxes, saleinvoiceTerms);

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

        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_SaleInvoice, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult ListSaleInvoice()
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
        public PartialViewResult GetSaleInvoiceList(string saleinvoiceNo = "", string customerName = "", string refNo = "", string fromDate = "", string toDate = "",string invoiceType="",string approvalStatus="",int companyBranchId=0)
        {
            List<SaleInvoiceViewModel> saleinvoices = new List<SaleInvoiceViewModel>();
            SaleInvoiceBL saleinvoiceBL = new SaleInvoiceBL();
            try
            {
                saleinvoices = saleinvoiceBL.GetSaleInvoiceList(saleinvoiceNo, customerName, refNo, fromDate, toDate, ContextUser.CompanyId,invoiceType,"", approvalStatus,companyBranchId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(saleinvoices);
        }

        [HttpGet]
        public JsonResult GetSaleInvoiceDetail(long saleinvoiceId)
        {
            SaleInvoiceBL saleinvoiceBL = new SaleInvoiceBL();
            SaleInvoiceViewModel saleinvoice = new SaleInvoiceViewModel();
            try
            {
                saleinvoice = saleinvoiceBL.GetSaleInvoiceDetail(saleinvoiceId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(saleinvoice, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public PartialViewResult GetSaleInvoiceProductList(List<SaleInvoiceProductViewModel> saleinvoiceProducts, long saleinvoiceId)
        {
            SaleInvoiceBL siBL = new SaleInvoiceBL();
            try
            {
                if (saleinvoiceProducts == null)
                {
                    saleinvoiceProducts = siBL.GetSaleInvoiceProductList(saleinvoiceId);
                }

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(saleinvoiceProducts);
        }
        [HttpPost]
        public PartialViewResult GetSaleInvoiceTaxList(List<SaleInvoiceTaxViewModel> saleinvoiceTaxes, long saleinvoiceId)
        {
            SaleInvoiceBL saleinvoiceBL = new SaleInvoiceBL();
            try
            {
                if (saleinvoiceTaxes == null)
                {
                    saleinvoiceTaxes = saleinvoiceBL.GetSaleInvoiceTaxList(saleinvoiceId);
                }

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(saleinvoiceTaxes);
        }
        [HttpPost]
        public PartialViewResult GetSaleInvoiceTermList(List<SaleInvoiceTermViewModel> saleinvoiceTerms, long saleinvoiceId)
        {
            SaleInvoiceBL saleinvoiceBL = new SaleInvoiceBL();
            try
            {
                if (saleinvoiceTerms == null)
                {
                    saleinvoiceTerms = saleinvoiceBL.GetSaleInvoiceTermList(saleinvoiceId);
                }

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(saleinvoiceTerms);
        }


        [HttpGet]
        public JsonResult GetCustomerAutoCompleteList(string term)
        {
            CustomerBL customerBL = new CustomerBL();

            List<CustomerViewModel> customerList = new List<CustomerViewModel>();
            try
            {
                customerList = customerBL.GetCustomerAutoCompleteList(term, ContextUser.CompanyId);

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(customerList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetTaxAutoCompleteList(string term)
        {
            TaxBL taxBL = new TaxBL();

            List<TaxViewModel> taxList = new List<TaxViewModel>();
            try
            {
                taxList = taxBL.GetTaxAutoCompleteList(term, "SALE", ContextUser.CompanyId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(taxList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetTermTemplateList()
        {
            TermTemplateBL termBL = new TermTemplateBL();
            List<TermTemplateViewModel> termList = new List<TermTemplateViewModel>();
            try
            {
                termList = termBL.GetTermTemplateList(ContextUser.CompanyId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(termList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetTermTemplateDetailList(int termTemplateId)
        {
            TermTemplateBL termBL = new TermTemplateBL();
            List<TermTemplateDetailViewModel> termList = new List<TermTemplateDetailViewModel>();
            try
            {
                termList = termBL.GetTermTemplateDetailList(termTemplateId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(termList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public PartialViewResult GetSaleOrderList(string soNo = "", string customerName = "", string refNo = "", string fromDate = "", string toDate = "",string approvalStatus="", string displayType = "")
        {
            List<SOViewModel> soList = new List<SOViewModel>();
            SOBL soBL = new SOBL();
            try
            {
                soList = soBL.GetSOList(soNo, customerName, refNo, fromDate, toDate, ContextUser.CompanyId, approvalStatus, displayType);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(soList);
        }
        public ActionResult Report(long siId, string reportType = "PDF",string reportOption="Original")
        {
            LocalReport lr = new LocalReport();
            SaleInvoiceBL siBL = new SaleInvoiceBL();
            string path = Path.Combine(Server.MapPath("~/RDLC"), "SaleInvoicePrint.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("Index");
            }

            DataTable dt = new DataTable();
            dt = siBL.GetSaleInvoiceDetailDataTable(siId);

            decimal totalInvoiceAmount = 0;
            totalInvoiceAmount = Convert.ToDecimal(dt.Rows[0]["TotalValue"].ToString());
            string totalWords = CommonHelper.changeToWords(totalInvoiceAmount.ToString());

            ReportDataSource rd = new ReportDataSource("DataSet1", dt);
            ReportDataSource rdProduct = new ReportDataSource("DataSetProduct", siBL.GetSaleInvoiceProductListDataTable(siId));
            ReportDataSource rdTax = new ReportDataSource("DataSetTax", siBL.GetSaleInvoiceTaxListDataTable(siId));
            ReportDataSource rdTerms = new ReportDataSource("DataSetTerms", siBL.GetSaleInvoiceTermListDataTable(siId));


            lr.DataSources.Add(rd);
            lr.DataSources.Add(rdProduct);
            lr.DataSources.Add(rdTax);
            lr.DataSources.Add(rdTerms);

            ReportParameter rp1 = new ReportParameter("AmountInWords", totalWords);
            ReportParameter rp2 = new ReportParameter("ReportOption", reportOption);
            lr.SetParameters(rp1);
            lr.SetParameters(rp2);

            string mimeType;
            string encoding;
            string fileNameExtension;



            string deviceInfo =

            "<DeviceInfo>" +
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
        public ActionResult SaleInvoiceMail(long siId, string reportType = "PDF", string reportOption = "Original")
        {
            ResponseOut responseOut = new ResponseOut();
            LocalReport lr = new LocalReport();
            SaleInvoiceBL siBL = new SaleInvoiceBL();

            try
            { 


                string path = Path.Combine(Server.MapPath("~/RDLC"), "SaleInvoicePrint.rdlc");
                if (System.IO.File.Exists(path))
                {
                    lr.ReportPath = path;
                }
                else
                {
                    responseOut.message = "Report Path not Found!!!";
                    responseOut.status = ActionStatus.Fail;
                    return Json(responseOut, JsonRequestBehavior.AllowGet);
                }

                DataTable dt = new DataTable();
                dt = siBL.GetSaleInvoiceDetailDataTable(siId);

                if (dt.Rows.Count > 0)
                {
                    StringBuilder mailBody = new StringBuilder(" ");
                    SendMail sendMail = new SendMail();
                    mailBody.Append("<html><head></head><body>");
                    //mailBody.Append("<img src='" + Convert.ToString(ConfigurationManager.AppSettings["Logo_Path"]) + "' alt='ICS Logo' />");
                    //mailBody.Append("<hr/><br/>");
                    mailBody.Append("<p style='font-family:Cambria;font-size:12px;margin: 0px 0px 0px 0px;'>Dear " + dt.Rows[0]["ContactPerson"].ToString() + " </p><br/>");
                    mailBody.Append("<p style='font-family:Cambria;font-size:12px;margin: 0px 0px 0px 0px;'>Please find attached Invoice for your reference</p><br/>");
                    mailBody.Append("<p style='font-family:Cambria;font-size:12px;margin: 0px 0px 0px 0px;'>Regards,</p><br/>");
                    mailBody.Append("<p style='font-family:Cambria;font-size:12px;margin: 0px 0px 0px 0px;'>Sale Team</p><br/>");
                    mailBody.Append("</body></html>");

                    decimal totalInvoiceAmount = 0;
                    totalInvoiceAmount = Convert.ToDecimal(dt.Rows[0]["TotalValue"].ToString());
                    string totalWords = CommonHelper.changeToWords(totalInvoiceAmount.ToString());



                    ReportDataSource rd = new ReportDataSource("DataSet1", dt);
                    ReportDataSource rdProduct = new ReportDataSource("DataSetProduct", siBL.GetSaleInvoiceProductListDataTable(siId));
                    ReportDataSource rdTax = new ReportDataSource("DataSetTax", siBL.GetSaleInvoiceTaxListDataTable(siId));
                    ReportDataSource rdTerms = new ReportDataSource("DataSetTerms", siBL.GetSaleInvoiceTermListDataTable(siId));

                    lr.DataSources.Add(rd);
                    lr.DataSources.Add(rdProduct);
                    lr.DataSources.Add(rdTax);
                    lr.DataSources.Add(rdTerms);

                    ReportParameter rp1 = new ReportParameter("AmountInWords", totalWords);
                    ReportParameter rp2 = new ReportParameter("ReportOption", reportOption);
                    lr.SetParameters(rp1);
                    lr.SetParameters(rp2);
                    string mimeType;
                    string encoding;
                    string fileNameExtension;



                    string deviceInfo =

                    "<DeviceInfo>" +
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

                 
                    UserEmailSettingBL userEmailSettingBL = new UserEmailSettingBL();
                    DataTable userEmailSetting = userEmailSettingBL.GetUserEmailSettingDetailDataTable(ContextUser.UserId);
                    bool sendMailStatus = false;
                    if (userEmailSetting.Rows.Count>0)
                    {
                        sendMailStatus = sendMail.SendEmail(userEmailSetting.Rows[0]["SmtpUser"].ToString(), dt.Rows[0]["Email"].ToString(), "Purchase Order", mailBody.ToString(), renderedBytes, "PO.pdf", userEmailSetting.Rows[0]["SmtpPass"].ToString(), userEmailSetting.Rows[0]["SmtpDisplayName"].ToString(), userEmailSetting.Rows[0]["SmtpServer"].ToString(),Convert.ToInt32(userEmailSetting.Rows[0]["SmtpPort"]),Convert.ToBoolean(userEmailSetting.Rows[0]["EnableSsl"]));
                    }
                    else
                    {
                        sendMailStatus = sendMail.SendEmail("", dt.Rows[0]["Email"].ToString(), "Purchase Order", mailBody.ToString(), renderedBytes, "PO.pdf");
                    }
                    //bool sendMailStatus = sendMail.SendEmail("", dt.Rows[0]["Email"].ToString(), "Invoice", mailBody.ToString(), renderedBytes, "Invoice.pdf");
                    if (sendMailStatus)
                    {
                        responseOut.message = "Mail Sent Successfully";
                        responseOut.status = ActionStatus.Success;


                    }
                    else
                    {
                        responseOut.message = "Problem in Sending Mail!!!";
                        responseOut.status = ActionStatus.Fail;

                    }
                }
                else
                {
                    responseOut.message = "Invoice Detail not found!!!";
                    responseOut.status = ActionStatus.Fail;

                }

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(responseOut, JsonRequestBehavior.AllowGet);
        }


        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_SaleInvoice, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult CancelSaleInvoice(int siId = 0, int accessMode = 3)
        {
            try
            {
                FinYearViewModel finYear = Session[SessionKey.CurrentFinYear] != null ? (FinYearViewModel)Session[SessionKey.CurrentFinYear] : new FinYearViewModel();

                ViewData["fromDate"] = finYear.StartDate;
                ViewData["toDate"] = finYear.EndDate;
                ViewData["currentDate"] = DateTime.Now.ToString("dd-MMM-yyyy");

                if (siId != 0)
                {
                    ViewData["siId"] = siId;
                    ViewData["accessMode"] = accessMode;

                }
                else
                {
                    ViewData["siId"] = 0;
                    ViewData["accessMode"] = 3;

                }

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return View();
        }


        [HttpPost]
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_SaleInvoice, (int)AccessMode.AddAccess, (int)RequestMode.Ajax)]
        public ActionResult CancelSaleInvoice(long invoiceId,string cancelReason)
        {
            ResponseOut responseOut = new ResponseOut();
            SaleInvoiceBL saleinvoiceBL = new SaleInvoiceBL();
            SaleInvoiceViewModel saleinvoiceViewModel = new SaleInvoiceViewModel();
            try
            {
                if (saleinvoiceViewModel != null)
                {
                    saleinvoiceViewModel.InvoiceId = invoiceId;
                    saleinvoiceViewModel.CancelReason = cancelReason;
                    saleinvoiceViewModel.CreatedBy = ContextUser.UserId;
                    responseOut = saleinvoiceBL.CancelSaleInvoice(saleinvoiceViewModel);
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
        public JsonResult GetCurrencyList()
        {
            CurrencyBL currencyBL = new CurrencyBL();
            List<CurrencyViewModel> currencyList = new List<CurrencyViewModel>();
            try
            {
                currencyList = currencyBL.GetCurrencyList();
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(currencyList, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}

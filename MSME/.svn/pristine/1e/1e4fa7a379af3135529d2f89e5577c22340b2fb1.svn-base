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
using System.IO;
using System.Text;
using System.Data;

namespace Portal.Controllers
{
    public class POController : BaseController
    {
        #region PO

        //
        // GET: /PO/

        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_PO, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult AddEditPO(int poId = 0, int accessMode = 3)
        {
            try
            {
                if (poId != 0)
                {
                    ViewData["poId"] = poId;
                    ViewData["accessMode"] = accessMode;
                    ViewData["currentDate"] = DateTime.Now.ToString("dd-MMM-yyyy");
                }
                else
                {
                    ViewData["poId"] = 0;
                    ViewData["accessMode"] = 3;
                    ViewData["currentDate"] = DateTime.Now.ToString("dd-MMM-yyyy");
                }

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return View();
        }
        [HttpPost]
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_PO, (int)AccessMode.AddAccess, (int)RequestMode.Ajax)]
        public ActionResult AddEditPO(POViewModel poViewModel, List<POProductViewModel> poProducts, List<POTaxViewModel> poTaxes, List<POTermViewModel> poTerms)
        {
            ResponseOut responseOut = new ResponseOut();
            POBL poBL = new POBL();
            try
            {
                if (poViewModel != null)
                {
                    poViewModel.CreatedBy = ContextUser.UserId;
                    poViewModel.CompanyId = ContextUser.CompanyId;
                    poViewModel.FinYearId = Session[SessionKey.CurrentFinYear] != null ? ((FinYearViewModel)Session[SessionKey.CurrentFinYear]).FinYearId : DateTime.Now.Year;
                    responseOut = poBL.AddEditPO(poViewModel, poProducts, poTaxes, poTerms);
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

        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_PO, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult ListPO()
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
        public PartialViewResult GetPOList(string poNo = "", string vendorName = "", string refNo = "", string fromDate = "", string toDate = "",string approvalStatus="", string displayType = "")
        {
            List<POViewModel> pos = new List<POViewModel>();

            POBL poBL = new POBL();
            try
            {
                pos = poBL.GetPOList(poNo, vendorName, refNo, fromDate, toDate,approvalStatus, ContextUser.CompanyId, displayType);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(pos);
        }

        [HttpPost]
        public PartialViewResult GetPOProductList(List<POProductViewModel> poProducts, long poId)
        {
            POBL poBL = new POBL();
            try
            {
                if (poProducts == null)
                {
                    poProducts = poBL.GetPOProductList(poId);
                }

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(poProducts);
        }

        [HttpPost]
        public PartialViewResult GetPOTaxList(List<POTaxViewModel> poTaxes, long poId)
        {

            POBL poBL = new POBL();
            try
            {
                if (poTaxes == null)
                {
                    poTaxes = poBL.GetPOTaxList(poId);
                }

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(poTaxes);
        }
        [HttpPost]
        public PartialViewResult GetPOTermList(List<POTermViewModel> poTerms, long poId)
        {
            POBL poBL = new POBL();
            try
            {
                if (poTerms == null)
                {
                    poTerms = poBL.GetPoTermList(poId);
                }

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(poTerms);
        }

        [HttpGet]
        public JsonResult GetVendorAutoCompleteList(string term)
        {
            CustomerBL customerBL = new CustomerBL();
            VendorBL vendorBL = new VendorBL();
            List<VendorViewModel> vendorList = new List<VendorViewModel>();
            try
            {
                vendorList = vendorBL.GetVendorAutoCompleteList(term, ContextUser.CompanyId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(vendorList, JsonRequestBehavior.AllowGet);
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
        public JsonResult GetPODetail(long poId)
        {

            POBL poBL = new POBL();
            POViewModel po = new POViewModel();
            try
            {
                po = poBL.GetPODetail(poId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(po, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Report(long poId, string reportType = "PDF")
        {
            LocalReport lr = new LocalReport();
            POBL poBL = new POBL();
            string path = Path.Combine(Server.MapPath("~/RDLC"), "POPrint.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("Index");
            }
            ReportDataSource rd = new ReportDataSource("DataSet2", poBL.GetPODetailDataTable(poId));
            ReportDataSource rdProduct = new ReportDataSource("DataSetPOProduct", poBL.GetPOProductListDataTable(poId));
            ReportDataSource rdTax = new ReportDataSource("DataSetPOTax", poBL.GetPOTaxList(poId));
            ReportDataSource rdTerms = new ReportDataSource("DataSetPOTerms", poBL.GetPOTermListDataTable(poId));

            lr.DataSources.Add(rd);
            lr.DataSources.Add(rdProduct);
            lr.DataSources.Add(rdTax);
            lr.DataSources.Add(rdTerms);
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


        [HttpGet]
        public ActionResult POMail(long poId, string reportType = "PDF")
        {
            ResponseOut responseOut = new ResponseOut();
            LocalReport lr = new LocalReport();
            POBL poBL = new POBL();

            try
            {  
                string path = Path.Combine(Server.MapPath("~/RDLC"), "POPrint.rdlc");
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
                dt = poBL.GetPODetailDataTable(poId);

                if (dt.Rows.Count > 0)
                {
                    StringBuilder mailBody = new StringBuilder(" ");
                    SendMail sendMail = new SendMail();
                    mailBody.Append("<html><head></head><body>");
                    //mailBody.Append("<img src='" + Convert.ToString(ConfigurationManager.AppSettings["Logo_Path"]) + "' alt='ICS Logo' />");
                    //mailBody.Append("<hr/><br/>");
                    mailBody.Append("<p style='font-family:Cambria;font-size:12px;margin: 0px 0px 0px 0px;'>Dear " + dt.Rows[0]["ContactPersonName"].ToString() + " </p><br/>");
                    mailBody.Append("<p style='font-family:Cambria;font-size:12px;margin: 0px 0px 0px 0px;'>Please find attached Purchase Order for your reference</p><br/>");
                    mailBody.Append("<p style='font-family:Cambria;font-size:12px;margin: 0px 0px 0px 0px;'>Regards,</p><br/>");
                    mailBody.Append("<p style='font-family:Cambria;font-size:12px;margin: 0px 0px 0px 0px;'>Sale Team</p><br/>");
                    mailBody.Append("</body></html>");



                    ReportDataSource rd = new ReportDataSource("DataSet2", dt);
                    ReportDataSource rdProduct = new ReportDataSource("DataSetPOProduct", poBL.GetPOProductListDataTable(poId));
                    ReportDataSource rdTax = new ReportDataSource("DataSetPOTax", poBL.GetPOTaxList(poId));
                    ReportDataSource rdTerms = new ReportDataSource("DataSetPOTerms", poBL.GetPOTermListDataTable(poId));
                    lr.DataSources.Add(rd);
                    lr.DataSources.Add(rdProduct);
                    lr.DataSources.Add(rdTax);
                    lr.DataSources.Add(rdTerms);
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
                    responseOut.message = "Purchase Order Detail not found!!!";
                    responseOut.status = ActionStatus.Fail;

                }

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(responseOut, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDocumentTypeList()
        {
            DocumentTypeBL docBL = new DocumentTypeBL();
            List<DocumentTypeViewModel> documentList = new List<DocumentTypeViewModel>();
            try
            {
                int companyId = 1;
                documentList = docBL.GetDocumentTypeList("",companyId,"");
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(documentList, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Revised PO
        [ValidateRequest(true, UserInterfaceHelper.Add_RevisedPO, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult AddRevisedPO(int poId = 0, int accessMode = 3)
        {
            try
            {
                if (poId != 0)
                {
                    ViewData["poId"] = poId;
                    ViewData["accessMode"] = accessMode;
                    ViewData["currentDate"] = DateTime.Now.ToString("dd-MMM-yyyy");
                }
                else
                {
                    ViewData["poId"] = 0;
                    ViewData["accessMode"] = 3;
                    ViewData["currentDate"] = DateTime.Now.ToString("dd-MMM-yyyy");
                }

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return View();
        }
        [HttpPost]
        [ValidateRequest(true, UserInterfaceHelper.Add_RevisedPO, (int)AccessMode.AddAccess, (int)RequestMode.Ajax)]
        public ActionResult AddRevisedPO(POViewModel poViewModel, List<POProductViewModel> poProducts, List<POTaxViewModel> poTaxes, List<POTermViewModel> poTerms)
        {
            ResponseOut responseOut = new ResponseOut();
            POBL poBL = new POBL();
            try
            {
                if (poViewModel != null)
                {
                    poViewModel.CreatedBy = ContextUser.UserId;
                    poViewModel.CompanyId = ContextUser.CompanyId;
                    poViewModel.FinYearId = Session[SessionKey.CurrentFinYear] != null ? ((FinYearViewModel)Session[SessionKey.CurrentFinYear]).FinYearId : DateTime.Now.Year;
                    responseOut = poBL.AddRevisedPO(poViewModel, poProducts, poTaxes, poTerms);
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
        #endregion
}
}

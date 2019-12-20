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
    public class VoucherController : BaseController
    {
        //
        // GET: /Voucher/
        #region Bank Voucher Entry
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_BankVoucher, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult AddEditBankVoucher(int voucherId = 0, int accessMode = 3)
        {
            try
            {
                FinYearViewModel finYear = Session[SessionKey.CurrentFinYear] != null ? (FinYearViewModel)Session[SessionKey.CurrentFinYear] : new FinYearViewModel();
                ViewData["fromDate"] = finYear.StartDate;
                ViewData["toDate"] = finYear.EndDate;
                ViewData["currentDate"] = DateTime.Now.ToString("dd-MMM-yyyy");

                if (voucherId != 0)
                {
                    ViewData["voucherId"] = voucherId;
                    ViewData["accessMode"] = accessMode;
                }
                else
                {
                    ViewData["voucherId"] = 0;
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
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_BankVoucher, (int)AccessMode.AddAccess, (int)RequestMode.Ajax)]
        public ActionResult AddEditBankVoucher(VoucherViewModel voucherViewModel, List<VoucherDetailViewModel> voucherEntryList)
        {
            ResponseOut responseOut = new ResponseOut();
            VoucherBL voucherBL = new VoucherBL();
            try
            {
                if (voucherViewModel != null)
                {
                    voucherViewModel.CreatedBy = ContextUser.UserId;
                    voucherViewModel.CompanyId = ContextUser.CompanyId;
                    voucherViewModel.FinYearId = Session[SessionKey.CurrentFinYear] != null ? ((FinYearViewModel)Session[SessionKey.CurrentFinYear]).FinYearId : DateTime.Now.Year;
                    responseOut = voucherBL.AddEditBankVoucher(voucherViewModel, voucherEntryList);
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



        [HttpPost]
        public PartialViewResult GetBankVoucherEntryList(List<VoucherDetailViewModel> voucherEntryList, long voucherId)
        {
            VoucherBL voucherBL = new VoucherBL();
            try
            {
                if (voucherEntryList == null)
                {
                    voucherEntryList = voucherBL.GetBankVoucherEntryList(voucherId);
                }

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(voucherEntryList);
        }

        [HttpGet]
        public JsonResult GetBankVoucherDetail(long voucherId)
        {
            VoucherBL voucherBL = new VoucherBL();
            VoucherViewModel voucher = new VoucherViewModel();
            try
            {
                voucher = voucherBL.GetBankVoucherDetail(voucherId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(voucher, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Helper Method
        [HttpGet]
        public JsonResult GetBookList(string bookType)
        {
            BookBL bookBL = new BookBL();
            List<BookViewModel> bookList = new List<BookViewModel>();
            try
            {
                bookList = bookBL.GetBookList(bookType,ContextUser.CompanyId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(bookList, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetSLTypeList()
        {
            SLTypeBL slTypeBL = new SLTypeBL();
            List<SLTypeViewModel> slTypeList = new List<SLTypeViewModel>();
            try
            {
                slTypeList = slTypeBL.GetSLTypeList();
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(slTypeList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetSLAutoCompleteList(string term,int slTypeId)
        {
            SLBL slBL = new SLBL();
            List<SLViewModel> slList = new List<SLViewModel>();
            try
            {
                slList = slBL.GetSLAutoCompleteList(term,slTypeId, ContextUser.CompanyId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(slList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetGLAutoCompleteList(string term)
        {
            GLBL glBL = new GLBL();
            List<GLViewModel> glList = new List<GLViewModel>();
            try
            {
                glList = glBL.GetGLAutoCompleteList(term, ContextUser.CompanyId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(glList, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetPaymentModeList()
        {
            PaymentModeBL paymentModeBL = new PaymentModeBL();
            List<PaymentModeViewModel> paymentList = new List<PaymentModeViewModel>();
            try
            {
                paymentList = paymentModeBL.GetPaymentModeList();
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(paymentList, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetCostCenterList()
        {
            CostCenterBL costCenterBL = new CostCenterBL();
            List<CostCenterViewModel> costCenterList = new List<CostCenterViewModel>();
            try
            {
                costCenterList = costCenterBL.GetCostCenterList(ContextUser.CompanyId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(costCenterList, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}

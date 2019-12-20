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
    public class CustomerController : BaseController
    {
        //
        // GET: /Customer/
        #region Customer
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_Customer, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult AddEditCustomer(int customerId = 0, int accessMode = 3)
        {
            try
            {
                if (customerId != 0)
                {
                    ViewData["customerId"] = customerId;
                    ViewData["accessMode"] = accessMode;
                }
                else
                {
                    ViewData["customerId"] = 0;
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
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_Customer, (int)AccessMode.AddAccess, (int)RequestMode.Ajax)]
        public ActionResult AddEditCustomer(CustomerViewModel customerViewModel, List<CustomerBranchViewModel> customerBranchs, List<CustomerProductViewModel> customerProducts)
        {
            ResponseOut responseOut = new ResponseOut();
            CustomerBL customerBL = new CustomerBL();
            try
            {
                if (customerViewModel != null)
                {
                    customerViewModel.CreatedBy = ContextUser.UserId;
                    customerViewModel.CompanyId = ContextUser.CompanyId;
                    responseOut = customerBL.AddEditCustomer(customerViewModel, customerBranchs, customerProducts);
                }
                else
                {
                    responseOut.message = ActionMessage.ProbleminData;
                    responseOut.status = ActionStatus.Fail;
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
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_Customer, (int)AccessMode.EditAccess, (int)RequestMode.Ajax)]
        public ActionResult RemoveCustomerBranch(long customerBranchId = 0)
        {
            ResponseOut responseOut = new ResponseOut();
            CustomerBL customerBL = new CustomerBL();
            try
            {
                responseOut = customerBL.RemoveCustomerBranch(customerBranchId);
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
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_Customer, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult ListCustomer()
        {
            try
            {

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return View();
        }
        [HttpGet]
        public PartialViewResult GetCustomerList(string customerName = "", string customerCode = "", string mobileNo = "", int customerTypeid = 0, string customerStatus = "")
        {
            List<CustomerViewModel> customers = new List<CustomerViewModel>();
            CustomerBL customerBL = new CustomerBL();
            try
            {
                customers = customerBL.GetCustomerList(customerName, customerCode, mobileNo, customerTypeid, ContextUser.CompanyId, customerStatus);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(customers);
        }

        [HttpPost]
        public PartialViewResult GetCustomerBranchList(List<CustomerBranchViewModel> customerBranchs, int customerId)
        {
            CustomerBL customerBL = new CustomerBL();
            try
            {
                if (customerBranchs == null)
                {
                    customerBranchs = customerBL.GetCustomerBranchList(customerId);
                }

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(customerBranchs);
        }

        [HttpPost]
        public PartialViewResult GetCustomerProductList(List<CustomerProductViewModel> customerProducts, int customerId)
        {
            CustomerBL customerBL = new CustomerBL();
            try
            {
                if (customerProducts == null)
                {
                    customerProducts = customerBL.GetCustomerProductList(customerId);
                }

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(customerProducts);
        }

        [HttpPost]
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_Customer, (int)AccessMode.EditAccess, (int)RequestMode.Ajax)]
        public ActionResult RemoveCustomerProduct(long mappingId = 0)
        {
            ResponseOut responseOut = new ResponseOut();
            CustomerBL customerBL = new CustomerBL();
            try
            {
                responseOut = customerBL.RemoveCustomerProduct(mappingId);
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
        public JsonResult GetCustomerDetail(int customerId)
        {
            CustomerBL customerBL = new CustomerBL();
            CustomerViewModel customer = new CustomerViewModel();
            try
            {
                customer = customerBL.GetCustomerDetail(customerId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(customer, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetCustomerTypeList()
        {
            CustomerTypeBL customerTypeBL = new CustomerTypeBL();
            List<CustomerTypeViewModel> customerTypes = new List<CustomerTypeViewModel>();
            try
            {

                customerTypes = customerTypeBL.GetCustomerTypeList();
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(customerTypes, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}

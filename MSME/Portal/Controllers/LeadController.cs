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
    public class LeadController : BaseController
    {
        //
        // GET: /User/
        #region Lead
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_Lead_CRM, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult AddEditLead(int leadId = 0, int accessMode = 3)
        {
          try
            {
                if (leadId != 0)
                {
                    ViewData["leadId"] = leadId;
                    ViewData["accessMode"] = accessMode;
                    ViewData["UserId"] = ContextUser.UserId;
                    ViewData["FollowUpByUserName"] = ContextUser.FullName;
                    ViewData["UserId"] = ContextUser.UserId;
                }
                else
                {
                    ViewData["leadId"] = 0;
                    ViewData["accessMode"] = 0;
                    ViewData["CreatedDate"] = DateTime.Now;
                    ViewData["ModifyDate"] = DateTime.Now;
                    ViewData["FollowUpByUserName"] = ContextUser.FullName;
                    ViewData["UserId"] = ContextUser.UserId;
                }

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return View();
        }
        [HttpPost]
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_Lead_CRM, (int)AccessMode.AddAccess, (int)RequestMode.Ajax)]
        public ActionResult AddEditLead(LeadViewModel leadViewModel, List<LeadFollowUpViewModel> leadFollowUps)
        {
            ResponseOut responseOut = new ResponseOut();
            LeadBL leadBL = new LeadBL();
            try
            {
                if (leadViewModel != null)
                {
                    leadViewModel.CreatedBy = ContextUser.UserId;
                    leadViewModel.CompanyId = ContextUser.CompanyId;
                    responseOut = leadBL.AddEditLead(leadViewModel, leadFollowUps);
                    
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


        [HttpGet]
        public JsonResult GetLeadSourceList()
        {
            LeadSourceBL leadsourceBL = new LeadSourceBL();
            List<LeadSourceViewModel> leadsourceList = new List<LeadSourceViewModel>();
            try
            {
                leadsourceList = leadsourceBL.GetLeadSourceList();
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(leadsourceList, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetFollowUpActivityTypeList()
        {
            LeadBL leadBL = new LeadBL();
            List<FollowUpActivityTypeViewModel> followupList = new List<FollowUpActivityTypeViewModel>();
            try
            {
                followupList = leadBL.GetFollowUpActivityTypeList();
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(followupList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetLeadStatusList()
        {
            LeadStatusBL leadstatusBL = new LeadStatusBL();
            List<LeadStatusViewModel> leadstatusList = new List<LeadStatusViewModel>();
            try
            {
                leadstatusList = leadstatusBL.GetLeadStatusList();
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(leadstatusList, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetUserAutoCompleteList(string term)
        {
            LeadBL leadBL = new LeadBL();
            List<UserViewModel> userList = new List<UserViewModel>();
            try
            {
                userList = leadBL.GetUserAutoCompleteList(term);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(userList, JsonRequestBehavior.AllowGet);
        }

        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_Lead_CRM, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult ListLead()
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

        public PartialViewResult GetLeadList(string leadCode = "", string companyName = "",  string contactPersonName = "", string email = "", string contactNo = "", string companyAddress = "", string companyCity = "", int companyStateId = 0, int leadSourceId = 0, int leadStatusId = 0,int createdid=0 ,int userId=0,string status = "")
        { 
            List<LeadViewModel> leads = new List<LeadViewModel>();
            LeadBL leadBL = new LeadBL();
            int createdLeadByUserId =(createdid==1)?ContextUser.UserId:0;
            try
            {
                string Employeeid = leadBL.GetUserIdByEmail(ContextUser.UserName.ToString());
                userId = ContextUser.UserId;
                leads = leadBL.GetLeadList(leadCode, companyName, contactPersonName,  email,  contactNo,  companyAddress, companyCity, companyStateId, leadStatusId, leadSourceId, createdLeadByUserId, userId,status );
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(leads);
        }


        public JsonResult GetLeadDetail(int leadId)
        {
            LeadBL leadBL = new LeadBL();
            LeadViewModel lead = new LeadViewModel();
            try
            {
                lead = leadBL.GetLeadDetail(leadId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(lead, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetCountryList()
        {
            CountryBL countryBL = new CountryBL();
            List<CountryViewModel> countryList = new List<CountryViewModel>();
            try
            {
                countryList = countryBL.GetCountryList();
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(countryList, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetStateList(int countryId)
        {
            StateBL stateBL = new StateBL();
            List<StateViewModel> stateList = new List<StateViewModel>();
            LeadBL leadBL = new LeadBL();
            try
            {
                stateList = leadBL.GetStateList(ContextUser.UserName,countryId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(stateList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public PartialViewResult GetLeadFollowUpList(List<LeadFollowUpViewModel> leadFollowUps, int leadid)
        {
            LeadBL leadBL = new LeadBL();
            try
            {
                if (leadFollowUps == null)
                {
                  leadFollowUps = leadBL.GetLeadFollowUpList(leadid);
                }

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(leadFollowUps);
        }
        #endregion
    }
}

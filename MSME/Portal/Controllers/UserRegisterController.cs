﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Portal.Core;
using Portal.Core.ViewModel;
using Portal.Common;
using System.Reflection;
using System.IO;
using Portal.DAL;

namespace Portal.Controllers
{
    public class UserRegisterController : BaseController
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(UserRegistrationViewModel userRegistrationVM, UserSupportingDocumentViewModel userSupportingDocumentVM)
        {
            ResponseOut responseOut = new ResponseOut();
            UserRegistrationBL userRegistrationBL = new UserRegistrationBL();
            UserRegistrationBL userRegistrationBL1 = new UserRegistrationBL();
            try
            {
                var UserCode = userRegistrationBL1.GetUserMaxCount();

                if (userRegistrationVM.KYCCode == "B")
                {
                    userRegistrationVM.UserCode = "BY" + UserCode;
                }
                else if (userRegistrationVM.KYCCode == "S")
                {
                    userRegistrationVM.UserCode = "SL" + UserCode;
                }

                if (userRegistrationVM != null)
                {
                    userRegistrationVM.ExpireDate =Convert.ToString(DateTime.Now.AddYears(1));
                    userRegistrationVM.UserStatus = true;
                    responseOut = userRegistrationBL.AddEditUserRegistration(userRegistrationVM, userSupportingDocumentVM);
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
        public ActionResult UpdateUserPic()
        {
            ResponseOut responseOut = new ResponseOut();
            HttpFileCollectionBase files = Request.Files;
            UserSupportingDocumentViewModel UserSupportingDocumentVM = new UserSupportingDocumentViewModel();
            UserRegistrationBL userRegistrationBL = new UserRegistrationBL();
            
            try
            {

                UserSupportingDocumentVM.UserId = Convert.ToInt32(Request["UserId"]);
                //  Get all files from Request object  
                if (files != null && files.Count > 0 && Request.Files[0] != null && Request.Files[0].ContentLength > 0)
                {
                    HttpPostedFileBase file = files[0];
                    string fname;
                    // Checking for Internet Explorer  
                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1];
                    }
                    else
                    {
                        fname = file.FileName;
                    }

                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var logoFilename = UserSupportingDocumentVM.UserId.ToString() + "_" + fileName;
                        var path = Path.Combine(Server.MapPath("~/Images/CompanyLogo"), logoFilename);
                        file.SaveAs(path);
                        UserSupportingDocumentVM.DocumentPath = logoFilename;
                    }
                }

                if (UserSupportingDocumentVM != null && !string.IsNullOrEmpty(UserSupportingDocumentVM.DocumentPath))
                {
                    responseOut = userRegistrationBL.UpdateUserPic(UserSupportingDocumentVM);
                }
                else
                {
                    responseOut.message = "";
                    responseOut.status = ActionStatus.Success;
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
        public ActionResult EditProfile()
        {
            UserRegistrationBL bl = new UserRegistrationBL();
            try
            {
                var user= bl.GetRegistration(BSContextUser.UserId);
                return View(user);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return View(new UserRegistration());

        }

        [HttpPost]
        public ActionResult UpdateProfile(UserRegistration userRegistration)
        {
            ResponseOut responseOut = new ResponseOut();
            UserRegistrationBL userRegistrationBL = new UserRegistrationBL();

            try
            {
                userRegistration.UserId = BSContextUser.UserId;
                userRegistrationBL.UpdateProfile(userRegistration);
                responseOut.status = ActionStatus.Success;
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
        public JsonResult GetUserMaxCount()
        {
            UserRegistrationBL userRegistrationBL = new UserRegistrationBL();
            int USerCodeMax = 0;
            try
            {
                USerCodeMax = userRegistrationBL.GetUserMaxCount();
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(USerCodeMax, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [ValidateRequest(true, UserInterfaceHelper.AddEditUOM__INVENTORY, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult ListUserRegistration()
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
        public PartialViewResult GetUserRegistrationList(string firstName="", string email="", string contactNo="", string kycCode="")
        {
            List<UserRegistrationViewModel> userRegistrations = new List<UserRegistrationViewModel>();
            UserRegistrationBL userRegistrationBL = new UserRegistrationBL();
            try
            {
                userRegistrations = userRegistrationBL.GetUserRegistrationList(firstName, email, contactNo, kycCode);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(userRegistrations);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Portal.Core;
using Portal.Core.ViewModel;
using Portal.Common;
using System.Reflection;
using System.Text;
using System.Data;

namespace Portal.Controllers
{
    public class HomeController : BaseController
    {
        [AllowAnonymous]
        public ActionResult Login()
        {
            try
            {
                return View();
            }
            catch(Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                return View();
            }
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(string userName,string password)
        {
            UserBL userBL = new UserBL();
            UserViewModel userViewModel = new UserViewModel();
            try
            {
                userViewModel = userBL.AuthenticateUser(userName, password);
            }
            catch (Exception ex)
            {
                userViewModel.status = ActionStatus.Fail;
                userViewModel.message = ActionMessage.ApplicationException;
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(userViewModel, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult SendOTPMail(string UserName ,string UserEmail, string OTP)
        {
            ResponseOut responseOut = new ResponseOut();
            UserBL useBL = new UserBL();
            try
            {

                if (!string.IsNullOrEmpty(UserEmail))
                {
                    StringBuilder mailBody = new StringBuilder(" ");
                    SendMail sendMail = new SendMail();
                    mailBody.Append("<html><head></head><body>");
                    //mailBody.Append("<img src='" + Convert.ToString(ConfigurationManager.AppSettings["Logo_Path"]) + "' alt='ICS Logo' />");
                    //mailBody.Append("<hr/><br/>");
                    mailBody.Append("<p style='font-family:Cambria;font-size:12px;margin: 0px 0px 0px 0px;'>Dear " + UserName + " </p><br/>");
                    mailBody.Append("<p style='font-family:Cambria;font-size:12px;margin: 0px 0px 0px 0px;'>Please find below OTP</p><br/>");
                    mailBody.Append("<p style='font-family:Cambria;font-size:12px;margin: 0px 0px 0px 0px;'>OTP: <b>" + OTP + "</b></p><br/>");
                    mailBody.Append("<p style='font-family:Cambria;font-size:12px;margin: 0px 0px 0px 0px;'>Regards,</p><br/>");
                    mailBody.Append("<p style='font-family:Cambria;font-size:12px;margin: 0px 0px 0px 0px;'>Support Team</p><br/>");
                    mailBody.Append("</body></html>");

                    bool sendMailStatus = sendMail.SendEmail("", UserEmail, "OTP Mail", mailBody.ToString());
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
                    responseOut.message = "Entered Email not exists!!!";
                    responseOut.status = ActionStatus.Fail;
                }

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(responseOut, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult SendConfirmationMail(string UserName, string UserEmail, string UserCode)
        {
            ResponseOut responseOut = new ResponseOut();
            UserBL useBL = new UserBL();
            try
            {

                if (!string.IsNullOrEmpty(UserEmail))
                {
                    StringBuilder mailBody = new StringBuilder(" ");
                    SendMail sendMail = new SendMail();
                    mailBody.Append("<html><head></head><body>");
                    //mailBody.Append("<img src='" + Convert.ToString(ConfigurationManager.AppSettings["Logo_Path"]) + "' alt='ICS Logo' />");
                    //mailBody.Append("<hr/><br/>");
                    mailBody.Append("<p style='font-family:Cambria;font-size:12px;margin: 0px 0px 0px 0px;'>Dear " + UserName + " </p><br/>");
                    mailBody.Append("<p style='font-family:Cambria;font-size:12px;margin: 0px 0px 0px 0px;'>Please find below USer Code for login</p><br/>");
                    mailBody.Append("<p style='font-family:Cambria;font-size:12px;margin: 0px 0px 0px 0px;'>OTP: <b>" + UserCode + "</b></p><br/>");
                    mailBody.Append("<p style='font-family:Cambria;font-size:12px;margin: 0px 0px 0px 0px;'>Regards,</p><br/>");
                    mailBody.Append("<p style='font-family:Cambria;font-size:12px;margin: 0px 0px 0px 0px;'>Support Team</p><br/>");
                    mailBody.Append("</body></html>");

                    bool sendMailStatus = sendMail.SendEmail("", UserEmail, "User Code Mail", mailBody.ToString());
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
                    responseOut.message = "Entered Email not exists!!!";
                    responseOut.status = ActionStatus.Fail;
                }

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(responseOut, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [AllowAnonymous]
        public ActionResult SendPasswordMail(string userEmail)
        {
            ResponseOut responseOut = new ResponseOut(); 
            UserBL useBL = new UserBL(); 
            try
            {
                UserViewModel userViewModel = new UserViewModel();
                userViewModel = useBL.GetUserFromEmail(userEmail); 

                if (!string.IsNullOrEmpty(userViewModel.FullName))
                {
                    StringBuilder mailBody = new StringBuilder(" ");
                    SendMail sendMail = new SendMail();
                    mailBody.Append("<html><head></head><body>");
                    //mailBody.Append("<img src='" + Convert.ToString(ConfigurationManager.AppSettings["Logo_Path"]) + "' alt='ICS Logo' />");
                    //mailBody.Append("<hr/><br/>");
                    mailBody.Append("<p style='font-family:Cambria;font-size:12px;margin: 0px 0px 0px 0px;'>Dear " + userViewModel.FullName + " </p><br/>");
                    mailBody.Append("<p style='font-family:Cambria;font-size:12px;margin: 0px 0px 0px 0px;'>Please find below password</p><br/>");
                    mailBody.Append("<p style='font-family:Cambria;font-size:12px;margin: 0px 0px 0px 0px;'>Password: <b>"+ userViewModel.Password +"</b></p><br/>");
                    mailBody.Append("<p style='font-family:Cambria;font-size:12px;margin: 0px 0px 0px 0px;'>Regards,</p><br/>");
                    mailBody.Append("<p style='font-family:Cambria;font-size:12px;margin: 0px 0px 0px 0px;'>Support Team</p><br/>");
                    mailBody.Append("</body></html>"); 

                    bool sendMailStatus = sendMail.SendEmail("", userEmail, "Password Recovery Mail", mailBody.ToString());
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
                    responseOut.message = "Entered Email not exists!!!";
                    responseOut.status = ActionStatus.Fail;

                }

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(responseOut, JsonRequestBehavior.AllowGet);
        }
        
        [AllowAnonymous]
        public ActionResult ChangePassword()
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


        [HttpPost] 
        public ActionResult ChangePassword(UserViewModel userViewModel)
        {
            ResponseOut responseOut = new ResponseOut();
            UserBL userBL = new UserBL();
            try
            {
                if (userViewModel != null)
                {
                    userViewModel.CreatedBy = ContextUser.UserId;
                    userViewModel.UserId = ContextUser.UserId;
                    responseOut = userBL.ChangePassword(userViewModel);
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

        [AllowAnonymous]
        public ActionResult SignOut()
        {
            try
            {
                Session.Abandon();
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return RedirectToAction("Login", "Home");
        }

        [AllowAnonymous]
        public ActionResult AccessDenied(string redirectURL="")
        {
            try
            {
                ViewBag.RedirectURL = redirectURL;
                if (string.IsNullOrEmpty(redirectURL))
                { 
                ViewBag.ErrorMessage = ActionMessage.AccessDenied;
                }
                else if (redirectURL == "SA")
                {
                    string errMessage = "<p style='font-weight:bold;font-size:15px;text-align:center;line-height:25px;margin-top:28px;'>Access to the requested page has been denied</p>";
                    //errMessage += "<p style='font-size:15px;text-align:center;'>Please contact to administrator.<a href='../Company/SuperAdminDashboard\")' class='btn btn-success'>Click Here</a> to go back to Dashboard</p>";
                    ViewBag.ErrorMessage = errMessage;
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return View();
        }

       // For webSite purpose Code And All Website Views here
       // Start Code Date 25-Sep-2018
       //-------------------------------------------------------
        public ActionResult Index()
        {
            SessionWrapper sessionWrapper = new Common.SessionWrapper();
            WebBL webBL = new WebBL();
            LogoBL logoBL = new LogoBL();
            sessionWrapper.SetInSession("HomeMenuList", webBL.GetMenuList());
            SessionWrapper.SetInSession("Logo", logoBL.GetLogo());
            HomeUpdateBL homeUpdateBL = new HomeUpdateBL();
            WelcomeHomeBL welcomeHomeBL = new WelcomeHomeBL();
            
            List<HomeSliderViewModel> homeSliderList = new List<HomeSliderViewModel>();
            ContactInfoBL contactInfoBL = new ContactInfoBL();
            List<ContactInfoViewModel> contactInfo = new List<ContactInfoViewModel>();
            CopyRightBL copyRightBL = new CopyRightBL();
            HomeAboutBL homeAboutBL = new HomeAboutBL();
           
            try
            {
                homeSliderList = webBL.GetHomeSliderList();
                ViewBag.homeContent =welcomeHomeBL.GetWelcomeHomeList();
                Session["homeSlider"] = homeSliderList;
                //ViewBag.products = productBL.GetProductList("",1,"","","",0,0,"","");
                ViewBag.homeUpdates = homeUpdateBL.GetHomeUpdateList("associates");
                Session["copyright"] = copyRightBL.GetCopyRighList();
                Session["contactInfo"]= contactInfoBL.GetContactInfoList();
                Session["mission"] = webBL.GetOurMissionList();
                Session["homeAbout"] = homeAboutBL.GetHomeAboutList();
                Session["welcomeHome"] = welcomeHomeBL.GetWelcomeHomeList();
            }
            catch (Exception ex)
            {
                 Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }


            return View();
        }
       
        [HttpGet]
        public PartialViewResult GetHomeLetestUpdateList()
        {
            HomeUpdateBL homeUpdateBL = new HomeUpdateBL();
            List<HomeUpdateViewModel> homeUpdates = new List<HomeUpdateViewModel>();
            try
            {
                homeUpdates = homeUpdateBL.GetHomeUpdateList("latest");
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(homeUpdates);
        }

        [HttpGet]
        public PartialViewResult GetHomeNewUpdateList()
        {
            HomeUpdateBL homeUpdateBL = new HomeUpdateBL();
            List<HomeUpdateViewModel> homeUpdates = new List<HomeUpdateViewModel>();
            try
            {
                homeUpdates = homeUpdateBL.GetHomeUpdateList("new");
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(homeUpdates);
        }
        [HttpGet]
        public PartialViewResult GetHomeAssociatesList()
        {
            HomeUpdateBL homeUpdateBL = new HomeUpdateBL();
            List<HomeUpdateViewModel> homeUpdates = new List<HomeUpdateViewModel>();
            try
            {
                
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(homeUpdates);
        }


      
        public ActionResult AboutUs()
        {
            WebBL webBL = new WebBL(); 
            ViewData["About"] = webBL.GetAboutUSList();
            return View();
        }

     
       
    }
}

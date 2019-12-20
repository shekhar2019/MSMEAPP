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
    public class UserLoginController : BaseController
    {
        //
        // GET: /UserLogin/
        [AllowAnonymous]
        public ActionResult Index()
        {
            try
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

                homeSliderList = webBL.GetHomeSliderList();
                ViewBag.homeContent = welcomeHomeBL.GetWelcomeHomeList();
                Session["homeSlider"] = homeSliderList;
                //ViewBag.products = productBL.GetProductList("",1,"","","",0,0,"","");
                ViewBag.homeUpdates = homeUpdateBL.GetHomeUpdateList("associates");
                Session["copyright"] = copyRightBL.GetCopyRighList();
                Session["contactInfo"] = contactInfoBL.GetContactInfoList();
                Session["mission"] = webBL.GetOurMissionList();
                Session["homeAbout"] = homeAboutBL.GetHomeAboutList();
                Session["welcomeHome"] = welcomeHomeBL.GetWelcomeHomeList();
                return View();
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                return View();
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Index(string userName, string password)
        {
            UserBL userBL = new UserBL();
            UserRegistrationViewModel userRegistrationViewModel = new UserRegistrationViewModel();
            try
            {
                userRegistrationViewModel = userBL.AuthenticateBSUser(userName, password);
                Session.Add("KYCCode", userRegistrationViewModel.KYCCode);
            }
            catch (Exception ex)
            {
                userRegistrationViewModel.status = ActionStatus.Fail;
                userRegistrationViewModel.message = ActionMessage.ApplicationException;
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(userRegistrationViewModel, JsonRequestBehavior.AllowGet);
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
            return RedirectToAction("Index", "UserLogin");
        }

    }
}

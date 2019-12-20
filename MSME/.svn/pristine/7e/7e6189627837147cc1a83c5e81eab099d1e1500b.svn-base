using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Portal.Core;
using Portal.Core.ViewModel;
using Portal.Common;
using System.Reflection;
using System.IO;
namespace Portal.Controllers
{
    public class WelcomeHomeController : BaseController
    {
        //
        // GET: /WelcomeHome/
        #region WelcomeHome
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_WelcomeHome, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult AddEditWelcomeHome(int welcomeHomeId = 0, int accessMode = 3)
        {
            try
            {
                if (welcomeHomeId != 0)
                {
                    ViewData["welcomeHomeId"] = welcomeHomeId;
                    ViewData["accessMode"] = accessMode;
                }
                else
                {
                    ViewData["welcomeHomeId"] = 0;
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
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_WelcomeHome, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult AddEditWelcomeHome(WelcomeHomeViewModel welcomeHomeViewModel)
        {
            ResponseOut responseOut = new ResponseOut();
            WelcomeHomeBL welcomeHomeBL = new WelcomeHomeBL();
            try
            {
                if (welcomeHomeViewModel != null)
                {
                    welcomeHomeViewModel.CreatedBy = ContextUser.UserId;
                    responseOut = welcomeHomeBL.AddEditWelcomeHome(welcomeHomeViewModel);
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
        public PartialViewResult GetWelcomeHomeList()
        {
            WelcomeHomeBL welcomeHomeBL = new WelcomeHomeBL();
            
            List<WelcomeHomeViewModel> welcomeHomes = new List<WelcomeHomeViewModel>();
            try
            {
                welcomeHomes = welcomeHomeBL.GetWelcomeHomeList();
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(welcomeHomes);
        }

        public ActionResult RemoveWelcomeHome(int welcomeId)
        {
            ResponseOut responseOut = new ResponseOut();
            WelcomeHomeBL welcomeHomeBL = new WelcomeHomeBL();
            try
            {
                responseOut = welcomeHomeBL.RemoveWelcomeHome(welcomeId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return Json(responseOut, JsonRequestBehavior.AllowGet);
        }
        #endregion WelcomeHome


    }
}

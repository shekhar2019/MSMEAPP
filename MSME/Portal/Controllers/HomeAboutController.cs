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
    public class HomeAboutController : BaseController
    {

        #region HomeAbout
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_CopyRight, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult AddEditHomeAbout(int Id = 0, int accessMode = 3)
        {
            try
            {
                if (Id != 0)
                {
                    ViewData["Id"] = Id;
                    ViewData["accessMode"] = accessMode;
                }
                else
                {
                    ViewData["Id"] = 0;
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
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_CopyRight, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult AddEditHomeAbout(HomeAboutViewModel homeAboutViewModel)
        {
            ResponseOut responseOut = new ResponseOut();
            HomeAboutBL homeAboutBL = new HomeAboutBL();
            try
            {
                if (homeAboutViewModel != null)
                {
                    homeAboutViewModel.CreatedBy = ContextUser.UserId;
                    responseOut = homeAboutBL.AddEditHomeAbout(homeAboutViewModel);
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
        public PartialViewResult GetHomeAboutList()
        {
            HomeAboutBL homeAboutBL = new HomeAboutBL();
            List<HomeAboutViewModel> homeAboutViewModel = new List<HomeAboutViewModel>();
            try
            {
                homeAboutViewModel = homeAboutBL.GetHomeAboutList();
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(homeAboutViewModel);
        }
        #endregion
    }
}

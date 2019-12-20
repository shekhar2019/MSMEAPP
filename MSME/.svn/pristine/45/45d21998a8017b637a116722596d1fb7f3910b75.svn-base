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
    public class ButtonTitleController : BaseController
    {
        #region ButtonTitle
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_ButtonTitle, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult AddEditButtonTitle(int Id = 0, int accessMode = 3)
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
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_ButtonTitle, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult AddEditButtonTitle(ButtonTitleViewModel buttonTitleViewModel)
        {
            ResponseOut responseOut = new ResponseOut();
            ButtonTitleBL buttonTitleBL = new ButtonTitleBL();
            try
            {
                if (buttonTitleViewModel != null)
                {
                    buttonTitleViewModel.CreatedBy = ContextUser.UserId;
                    responseOut = buttonTitleBL.AddEditButtonTitle(buttonTitleViewModel);
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
        public PartialViewResult GetButtonTitleList()
        {
            ButtonTitleBL buttonTitleBL = new ButtonTitleBL();
            List<ButtonTitleViewModel> buttonTitleViewModel = new List<ButtonTitleViewModel>();
            try
            {
                buttonTitleViewModel = buttonTitleBL.GetButtonTitleList();
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(buttonTitleViewModel);
        }
        #endregion
    }
}

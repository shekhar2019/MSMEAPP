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
    public class CopyRightController  : BaseController
    {

        #region CopyRight
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_CopyRight, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult AddEditCopyRight(int Id = 0, int accessMode = 3)
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
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_LOGO, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult AddEditCopyRight(CopyRightViewModel copyRightViewModel)
        {
            ResponseOut responseOut = new ResponseOut();

            CopyRightBL copyRightBL = new CopyRightBL();
            try
            {
                if (copyRightViewModel != null)
                {
                    copyRightViewModel.CreatedBy = ContextUser.UserId;
                    responseOut = copyRightBL.AddEditCopyRight(copyRightViewModel);
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
        public PartialViewResult GetCopyRightList()
        {
            CopyRightBL copyRightBL = new CopyRightBL();
            List<CopyRightViewModel> copyRightViewModel = new List<CopyRightViewModel>();
            try
            {
                copyRightViewModel = copyRightBL.GetCopyRighList();
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(copyRightViewModel);
        }
        #endregion
    }
}

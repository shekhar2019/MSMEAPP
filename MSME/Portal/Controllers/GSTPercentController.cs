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
    public class GSTPercentController : BaseController
    {
        //
        // GET: /GSTPercent/

        #region GST
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_LOGO, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult AddEditGSTPercent(int gstPercentageID = 0, int accessMode = 3)
        {
            try
            {
                if (gstPercentageID != 0)
                {
                    ViewData["gstPercentageID"] = gstPercentageID;
                    ViewData["accessMode"] = accessMode;
                }
                else
                {
                    ViewData["gstPercentageID"] = 0;
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
        public ActionResult AddEditGSTPercent(GSTPercentViewModel gSTPercentViewModel)
        {
            ResponseOut responseOut = new ResponseOut();
            GSTPercentBL gSTPercentBL = new GSTPercentBL();
            try
            {
                if (gSTPercentViewModel != null)
                {
                    responseOut = gSTPercentBL.AddEditGSTPercent(gSTPercentViewModel);
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
        public PartialViewResult GetGSTPercentageList()
        {
            GSTPercentBL gstPercentBL = new GSTPercentBL();
            List<GSTPercentViewModel> gstList = new List<GSTPercentViewModel>();
            try
            {
                gstList = gstPercentBL.GetGSTPercentList();
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(gstList);
        }
        public ActionResult RemoveGSTPercentage(int gSTPercentId)
        {
            ResponseOut responseOut = new ResponseOut();
            GSTPercentBL gSTPercentBL = new GSTPercentBL();
            try
            {

                responseOut = gSTPercentBL.RemoveGSTPercent(gSTPercentId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return Json(responseOut, JsonRequestBehavior.AllowGet);
        }
        #endregion


    }
}

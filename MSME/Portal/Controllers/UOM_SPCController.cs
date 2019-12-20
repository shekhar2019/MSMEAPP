using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Portal.Core;
using Portal.Core.ViewModel;
using Portal.Common;
using System.Reflection;
using Microsoft.Reporting.WebForms;
using Microsoft.ReportingServices;
using System.IO;
using System.Data;
using System.Text;


namespace Portal.Controllers
{
    public class UOM_SPCController : BaseController
    {
        //
        // GET: /AddEditUOM_SPC/

        [ValidateRequest(true, UserInterfaceHelper.AddEditUOM__INVENTORY, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult AddEditUOM_SPC(int UOMId = 0, int accessMode = 3)
        {
            
            try
            {
                if (UOMId != 0)
                {
                    ViewData["UOMId"] = UOMId;
                    ViewData["accessMode"] = accessMode;
                }
                else
                {
                    ViewData["UOMId"] = 0;
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
        [ValidateRequest(true, UserInterfaceHelper.AddEditUOM__INVENTORY, (int)AccessMode.AddAccess, (int)RequestMode.Ajax)]
        public ActionResult AddEditUOM_SPC(AddEditUOM_SPCViewModel uomViewModel)
        {
            ResponseOut responseOut = new ResponseOut();
            ProductBL uomBL = new ProductBL();
            try
            {
                if (uomViewModel != null)
                {
                    responseOut = uomBL.AddEditUOM_SPC(uomViewModel);
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

    }
}

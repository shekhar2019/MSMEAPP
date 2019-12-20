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
    public class SellerController : BaseController
    {
        //
        // GET: /Seller/

        public ActionResult Index()
        {
            MSMEBL msmeBL = new MSMEBL();
            try
            {
                ViewBag.MSMEList = msmeBL.GetMSMEList();
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return View();
        }

    }
}

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
using AttributeRouting.Web.Mvc;

namespace Portal.Controllers
{
    public class ProductViewController : Controller
    {
        //
        // GET: /ProductView/

       
        [Route("ProductView/{productName?}")]
        public ActionResult Index(string productName)
        {
            WebBL webBL = new WebBL();
            try
            {
                ViewBag.productImages = webBL.GetProdcutImagesList(productName.Replace('-',' '));
                ViewBag.productImagesList = webBL.GetProdcutImagesList1(productName.Replace('-', ' '));

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return View();
        }

    }
}

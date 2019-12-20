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
using System.Web.Script.Serialization;

namespace Portal.Controllers
{
    public class KnowledgeCenterController : BaseController
    {
        //
        // GET: /KnowledgeCenter/

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public PartialViewResult PraentClass(string category)
        {
            ProductMainGroupBL productMainGroupBL = new ProductMainGroupBL();
            try
            {

                ViewBag.ProductMainGroup = productMainGroupBL.GetProductMainGroupList(category, "", "1");
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView("PartailViewParentClass");
        }
        public ActionResult Category(int productMainGroupId)
        {
            ViewData["productMainGroupId"] = productMainGroupId.ToString();
            ProductMainGroupViewModel productmaingroup = new ProductMainGroupViewModel();
            ProductMainGroupBL productmaingroupBL = new ProductMainGroupBL();
            try
            {
                productmaingroup = productmaingroupBL.GetProductMainGroupDetail(productMainGroupId);
                ViewData["productMainGroupDESC"] = productmaingroup.ProductMainGroupDesc;
                ViewData["productMainGroupName"] = productmaingroup.ProductMainGroupName;
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }

            return View();
        }
        [HttpPost]
        public PartialViewResult CategoryGroup(string category, int productMainGroupId)
        {
            ProductSubGroupBL productSubGroupBL = new ProductSubGroupBL();
            try
            {
                ViewBag.ProductSubGroup = productSubGroupBL.GetProductSubGroupList(category, "", productMainGroupId, "1");


            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView("PartailViewSubGroup");
        }

        public ActionResult SubCategoryCategory(int productMainGroupId, int productSubGroupId)
        {
            ProductSubChildGroupBL productSubChildGroupBL = new ProductSubChildGroupBL();
            try
            {
                ViewBag.ProductSubChildGroup = productSubChildGroupBL.GetProductSubChildGroupList(productMainGroupId, productSubGroupId);
                ViewBag.ProductMainGroupId = productMainGroupId;
                ViewBag.ProductSubGroupId = productSubGroupId;
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return View();
        }


        public ActionResult GetProductList(int productSubGroupId)
        {

            ViewData["productSubGroupId"] = productSubGroupId.ToString();
            ProductSubGroupViewModel productmaingroup = new ProductSubGroupViewModel();
            ProductSubGroupBL productmaingroupBL = new ProductSubGroupBL();
            try
            {
                productmaingroup = productmaingroupBL.GetProductSubGroupDetail(productSubGroupId);
                ViewData["productSubGroupDESC"] = productmaingroup.ProductSubGroupDesc;
                ViewData["productSubGroupName"] = productmaingroup.ProductSubGroupName;
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }

            return View();

            //ProductBL productBL = new ProductBL();

            //try
            //{
            //    ViewBag.Products = productBL.GetProductsByGroupList(0, productSubGroupId, 0);
            //}
            //catch (Exception ex)
            //{
            //    Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            //}
            //return View();
        }


        [HttpPost]
        public PartialViewResult GetProductListData(string category, int productSubGroupId)
        {
            ProductBL productBL = new ProductBL();

            try
            {
                ViewData["currentDate"] = DateTime.Now.ToString("dd-MMM-yyyy");
                ViewBag.Products = productBL.GetProductsByGroupListKnowledgeCenter(0, productSubGroupId, 0, category);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView("PartailViewProductDetails");
        }

        [HttpPost]
        public string GetGraphData(string productID, string FromDate, string ToDate, string GraphType, string Year, string Fmonth, string Tmonth, string FYear, string TYear, string BSType)
        {
            ProductBL productBL = new ProductBL();
            List<GrpahViewModel> RateGrpah = new List<GrpahViewModel>();
            try
            {

                RateGrpah = productBL.GetProductsRateGroupbyKnowledgeCenter(productID, FromDate, ToDate, GraphType, Year, Fmonth, Tmonth, FYear, TYear, BSType);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(RateGrpah);
            return json.ToString();
            // return PartialView("PartailViewProductDetails");
        }
    }
}

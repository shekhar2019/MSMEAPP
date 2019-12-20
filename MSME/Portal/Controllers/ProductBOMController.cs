using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Portal.Core;
using Portal.Core.ViewModel;
using Portal.Common;
using System.Reflection;

namespace Portal.Controllers
{
    public class ProductBOMController : BaseController
    {
        //
        // GET: /ProductOpeningStock/

        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_ProductBOM, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult AddEditProductBOM(int bomId = 0, int accessMode = 3)
        {

            try
            {
                if (bomId != 0)
                {
                    ViewData["bomId"] = bomId;
                    ViewData["accessMode"] = accessMode;
                }
                else
                {
                    ViewData["bomId"] = 0;
                    ViewData["accessMode"] = 3;
                }

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return View();
        }
        [HttpPost]
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_ProductBOM, (int)AccessMode.AddAccess, (int)RequestMode.Ajax)]
        public ActionResult AddEditProductBOM(ProductBOMViewModel productBOMViewModel)
        {
            ResponseOut responseOut = new ResponseOut();
            ProductBOMBL productBOMBL = new ProductBOMBL();
            try
            {
                if (productBOMViewModel != null)
                {
                    productBOMViewModel.CompanyId = ContextUser.CompanyId;
                    productBOMViewModel.CreatedBy = ContextUser.UserId;
                    responseOut = productBOMBL.AddEditProductBOM(productBOMViewModel);
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

        [HttpPost]
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_ProductBOM, (int)AccessMode.AddAccess, (int)RequestMode.Ajax)]
        public ActionResult CopyProductBOM(long copyFromAssemblyId, long copyToAssemblyId)
        {
            ResponseOut responseOut = new ResponseOut();
            ProductBOMBL productBOMBL = new ProductBOMBL();
            try
            {
                if (copyFromAssemblyId != 0 && copyToAssemblyId != 0)
                {
                    responseOut = productBOMBL.CopyProductBOM(copyFromAssemblyId, copyToAssemblyId, ContextUser.UserId);
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


        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_ProductBOM, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult ListProductBOM()
        {
            try
            {

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return View();
        }

        [HttpGet]
        public PartialViewResult GetAssemblyList(string assemblyName, string assemblyType)
        {
            List<ProductBOMViewModel> productBOMs = new List<ProductBOMViewModel>();
            ProductBOMBL productBOMBL = new ProductBOMBL();
            try
            {

                productBOMs = productBOMBL.GetAssemblyList(assemblyName, assemblyType,ContextUser.CompanyId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(productBOMs);
        }
        [HttpGet]
        public PartialViewResult GetAssemblyBOMList(long assemblyID)
        {
            List<ProductBOMViewModel> productBOMs = new List<ProductBOMViewModel>();
            ProductBOMBL productBOMBL = new ProductBOMBL();
            try
            {

                productBOMs = productBOMBL.GetAssemblyBOMList(assemblyID);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(productBOMs);
        }

        [HttpGet]
        public JsonResult GetProductBOMDetail(long bomId)
        {
            ProductBOMBL productBOMBL = new ProductBOMBL();
            ProductBOMViewModel productBOM = new ProductBOMViewModel();
            try
            {
                productBOM = productBOMBL.GetProductBOMDetail(bomId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(productBOM, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetProductAutoCompleteList(string term,string assemblyType)
        {
            ProductBL productBL = new ProductBL();
            List<ProductViewModel> productList = new List<ProductViewModel>();
            try
            {
                productList = productBL.GetProductAutoCompleteList(term, ContextUser.CompanyId, assemblyType);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(productList, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetSubAssemblyAutoCompleteList(string term)
        {
            ProductBL productBL = new ProductBL();
            List<ProductViewModel> productList = new List<ProductViewModel>();
            try
            {
                productList = productBL.GetSubAssemblyAutoCompleteList(term, ContextUser.CompanyId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(productList, JsonRequestBehavior.AllowGet);
        }

    }
}

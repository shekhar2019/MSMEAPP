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
    public class ProductController : BaseController
    {
        //
        // GET: /Product/
        //
        // GET: /User/

        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_Product, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult AddEditProduct(int productId = 0, int accessMode = 3)
        {

            try
            {
                if (productId != 0)
                {
                    ViewData["productId"] = productId;
                    ViewData["accessMode"] = accessMode;
                }
                else
                {
                    ViewData["productId"] = 0;
                    ViewData["accessMode"] = 0;
                }

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return View();
        }

        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_Product, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult AddEditBuyerSellerProduct(int productId = 0, int accessMode = 3)
        {
            try
            {
                var model = new SellerProductViewModel();
                return View(model);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return View();
        }

        [HttpPost]
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_Product, (int)AccessMode.AddAccess, (int)RequestMode.Ajax)]
        public ActionResult AddEditProduct(ProductViewModel productViewModel, List<ProductImageDetailViewModel> productImageDetails, List<ProductTechSpecificationViewModel> productTechSpecifications)
        {
            ResponseOut responseOut = new ResponseOut();
            ProductBL productBL = new ProductBL();
            try
            {
                if (productViewModel != null)
                {
                    productViewModel.CompanyId = ContextUser.CompanyId;
                    productViewModel.CreatedBy = ContextUser.UserId;
                    responseOut = productBL.AddEditProduct(productViewModel, productImageDetails, productTechSpecifications);
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

        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_Product, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult ListProduct()
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
        public PartialViewResult GetProductList(string productName, string productCode, string productShortDesc, string productFullDesc, int productTypeId, int productMainGroupId, string assemblyType, string brandName)
        {
            List<ProductViewModel> products = new List<ProductViewModel>();
            ProductBL productBL = new ProductBL();
            try
            {

                products = productBL.GetProductList(productName, ContextUser.CompanyId, productCode, productShortDesc, productFullDesc, productTypeId, productMainGroupId, assemblyType, brandName);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(products);
        }

        [HttpGet]
        public JsonResult GetProductDetail(long productid)
        {
            ProductBL productBL = new ProductBL();
            ProductViewModel product = new ProductViewModel();
            try
            {
                product = productBL.GetProductDetail(productid);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(product, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetProductAutoCompleteList(string term)
        {
            ProductBL productBL = new ProductBL();
            List<ProductViewModel> productList = new List<ProductViewModel>();
            try
            {
                //productList = productBL.GetProductAutoCompleteList(term, ContextUser.CompanyId);
                productList = productBL.GetProductAutoCompleteList(term, 1);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(productList, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult GetProductAutoCompleteListFiltered(string searchTerm, int productMainGroupId, int productSubGroupId, int productSubChildGroupId)
        {
            ProductBL productBL = new ProductBL();
            List<ProductViewModel> productList = new List<ProductViewModel>();
            try
            {
                productList = productBL.GetProductAutoCompleteList(searchTerm, productMainGroupId, productSubGroupId, productSubChildGroupId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(productList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetProductTypeList()
        {
            ProductTypeBL productTypeBL = new ProductTypeBL();
            List<ProductTypeViewModel> productTypeList = new List<ProductTypeViewModel>();
            try
            {
                productTypeList = productTypeBL.GetProductTypeList();
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(productTypeList, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetProductMainGroupList()
        {
            ProductMainGroupBL productMainGroupBL = new ProductMainGroupBL();
            List<ProductMainGroupViewModel> productMainGroupList = new List<ProductMainGroupViewModel>();
            try
            {
                productMainGroupList = productMainGroupBL.GetProductMainGroupList();
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(productMainGroupList, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetProductSubGroupList(int productMainGroupId)
        {
            ProductSubGroupBL productSubGroupBL = new ProductSubGroupBL();
            List<ProductSubGroupViewModel> productSubGroupList = new List<ProductSubGroupViewModel>();
            try
            {
                productSubGroupList = productSubGroupBL.GetProductSubGroupList(productMainGroupId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(productSubGroupList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetProductSubChildGroupList(int productMainGroupId, int productSubGroupId)
        {
            ProductSubChildGroupBL productSubChildGroupBL = new ProductSubChildGroupBL();
            List<ProductSubChildGroupViewModel> productSubChildGroupList = new List<ProductSubChildGroupViewModel>();
            try
            {
                productSubChildGroupList = productSubChildGroupBL.GetProductSubChildGroupList(productMainGroupId, productSubGroupId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(productSubChildGroupList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetUOMList()
        {
            UOMBL uomBL = new UOMBL();
            List<UOMViewModel> uomList = new List<UOMViewModel>();
            try
            {
                uomList = uomBL.GetUOMList();
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(uomList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetFinYearList()
        {
            FinYearBL finYearBL = new FinYearBL();
            List<FinYearViewModel> finYearList = new List<FinYearViewModel>();
            try
            {
                finYearList = finYearBL.GetFinancialYearList();
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(finYearList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetProductAvailableStock(long productid, int companyBranchId, int trnId, string trnType)
        {
            StockLedgerBL stockBL = new StockLedgerBL();
            decimal availableStock = 0;
            try
            {
                int companyId = ContextUser.CompanyId;
                int finYearId = Session[SessionKey.CurrentFinYear] != null ? ((FinYearViewModel)Session[SessionKey.CurrentFinYear]).FinYearId : DateTime.Now.Year;

                availableStock = stockBL.GetProductAvailableStock(productid, finYearId, companyId, companyBranchId, trnId, trnType);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(availableStock, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public PartialViewResult GetProductImageDetailList(List<ProductImageDetailViewModel> productImageList, int productId)
        {
            ProductBL productBL = new ProductBL();
            try
            {
                if (productImageList == null)
                {
                    productImageList = productBL.GetProductImageDetailList(productId);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(productImageList);
        }

        [HttpPost]
        public PartialViewResult GetProductTechSpecificationList(List<ProductTechSpecificationViewModel> productTechSpecificationList, int productId)
        {
            ProductBL productBL = new ProductBL();

            try
            {
                if (productTechSpecificationList == null)
                {
                    productTechSpecificationList = productBL.GetProductTechSpecificationList(productId);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(productTechSpecificationList);
        }

        [HttpPost]
        public PartialViewResult GetTechSpecificationList(List<ProductTechSpecificationViewModel> productTechSpecificationList, int productId)
        {
            ProductBL productBL = new ProductBL();

            try
            {
                if (productTechSpecificationList == null)
                {
                    productTechSpecificationList = productBL.GetProductTechSpecificationList(productId);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(productTechSpecificationList);
        }


        [HttpPost]
        public ActionResult UpdateHomeSliderImage()
        {
            ResponseOut responseOut = new ResponseOut();
            HttpFileCollectionBase files = Request.Files;
            HomeSliderViewModel homeSliderViewModel = new HomeSliderViewModel();
            HomeSliderBL homeSliderBL = new HomeSliderBL();
            try
            {

                homeSliderViewModel.Sliderid = Convert.ToInt32(Request["sliderId"]);
                //  Get all files from Request object  
                if (files != null && files.Count > 0 && Request.Files[0] != null && Request.Files[0].ContentLength > 0)
                {
                    HttpPostedFileBase file = files[0];
                    string fname;
                    // Checking for Internet Explorer  
                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1];
                    }
                    else
                    {
                        fname = file.FileName;
                    }

                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var sliderFilename = homeSliderViewModel.Sliderid.ToString() + "_" + fileName;
                        var path = Path.Combine(Server.MapPath("~/Images/ProductImage"), sliderFilename);
                        file.SaveAs(path);
                        homeSliderViewModel.Image = sliderFilename;

                    }
                }

                if (homeSliderViewModel != null && !string.IsNullOrEmpty(homeSliderViewModel.Image))
                {
                    responseOut = homeSliderBL.UpdateHomeSliderPic(homeSliderViewModel);
                }
                else
                {
                    responseOut.message = "";
                    responseOut.status = ActionStatus.Success;
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
        public ActionResult SaveProductImage()
        {
            ResponseOut responseOut = new ResponseOut();
            HttpFileCollectionBase files = Request.Files;
            Random rnd = new Random();
            try
            {
                //  Get all files from Request object  
                if (files != null && files.Count > 0 && Request.Files[0] != null && Request.Files[0].ContentLength > 0)
                {
                    HttpPostedFileBase file = files[0];
                    string fname;
                    // Checking for Internet Explorer  
                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1];
                    }
                    else
                    {
                        fname = file.FileName;
                    }

                    if (file != null && file.ContentLength > 0)
                    {
                        string newFileName = "";
                        var fileName = Path.GetFileName(file.FileName);
                        newFileName = Convert.ToString(rnd.Next(10000, 99999)) + "_" + fileName;
                        var path = Path.Combine(Server.MapPath("~/Images/ProductImage"), newFileName);
                        file.SaveAs(path);
                        responseOut.status = ActionStatus.Success;
                        responseOut.message = newFileName;
                    }
                    else
                    {
                        responseOut.status = ActionStatus.Fail;
                        responseOut.message = "";
                    }
                }
                else
                {
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = "";
                }


            }
            catch (Exception ex)
            {
                responseOut.message = "";
                responseOut.status = ActionStatus.Fail;
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(responseOut, JsonRequestBehavior.AllowGet);
        }

    }
}

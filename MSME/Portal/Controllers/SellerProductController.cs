using Portal.Common;
using Portal.Core;
using Portal.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Portal.Controllers
{
    public class SellerProductController : BaseController
    {
        //
        // GET: /SellerProduct/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddEditSellerProduct()
        {
            UserRegistrationViewModel userRegistrationViewModel = Session[SessionKey.CurrentBSUser] != null ? (UserRegistrationViewModel)Session[SessionKey.CurrentBSUser] : new UserRegistrationViewModel();
            ViewData["userid"] = userRegistrationViewModel.UserId;
            var model = new SellerProductViewModel();
            return View(model);
        }
        [HttpGet]
        public ActionResult GetSellerProducts(long productMainGroupId, long productSubGroupId, long productChildSubGroupId)
        {
            SellerProductBL SellerProductBL = new SellerProductBL ();
            
            IEnumerable< SellerProductViewModel> products = SellerProductBL.GetSellerProductDetails(productMainGroupId, productSubGroupId, productChildSubGroupId, BSContextUser.UserId);
            return PartialView("_SellerProductList", products);
        }

        [HttpGet]
        public ActionResult GetAllSellerProducts()
        {
            SellerProductBL buyerProductBL = new SellerProductBL();
            IEnumerable<SellerProductViewModel> products = buyerProductBL.GetProductBySellerId(BSContextUser.UserId);
            return PartialView("_SellerProductList", products);
        }

        [HttpGet]
        public ActionResult GetDashboardSellerProducts(string sortBy = SortDashboardBy.LatestDate, string category = "", string city = "")
        {
            SellerProductBL sellerBL = new SellerProductBL();
            IEnumerable<SellerProductDashboardViewModel> products = sellerBL.GetSellerDashboardProducts(BSContextUser.UserId, category, city);

            foreach (var item in products)
            {
                if (sortBy == SortDashboardBy.LatestDate)
                {
                    item.Buyers = item.Buyers.OrderByDescending(x => x.ProductDate).ToList();
                }
            }
            if (sortBy == SortDashboardBy.LatestDate)
            {
                products = products.OrderByDescending(x => x.MaxBuyerDate).ToList();
            }

            return PartialView("_SellerDashboardProducts", products);
        }
        [HttpPost]
        public ActionResult DeleteSellerProductDetail(long sellerProductDetailId)
        {
            ResponseOut responseOut = new ResponseOut();
            ProductBL productBL = new ProductBL();
            try
            {
                if (sellerProductDetailId != 0)
                {
                    SellerProductBL sellerProductBL = new SellerProductBL();
                    responseOut = sellerProductBL.DeleteUpdateSellerProduct(sellerProductDetailId);
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
        public ActionResult AddEditSellerProduct(SellerProductViewModel productViewModel, bool isAdmin = false)
        {
            ResponseOut responseOut = new ResponseOut();
            ProductBL productBL = new ProductBL();
            try
            {
                if (productViewModel != null)
                {
                    SellerProductBL SellerProductBL = new SellerProductBL();
                    if (isAdmin)
                    {
                        productViewModel.SellerId = ContextUser.UserId;
                    }
                    else
                    {
                        productViewModel.SellerId = BSContextUser.UserId;
                    }
                    responseOut = SellerProductBL.AddUpdateSellerProduct(productViewModel, BSContextUser.CompanyId);
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
        public JsonResult GetGSTList()
        {
             GstBL gstBL = new GstBL();
            List<GSTViewModel> gstList = new List<GSTViewModel>();
            try
            {
                gstList = gstBL.GetGSTList();
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(gstList, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetGSTPercentageList()
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
            return Json(gstList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetDeliveryStatusList()
        {
            DeliveryStatusBL gstPercentBL = new DeliveryStatusBL();
            List<DeliveryStatusViewModel> deliveryStatusList = new List<DeliveryStatusViewModel>();
            try
            {
                deliveryStatusList = gstPercentBL.GetDeliveryStatusList();
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(deliveryStatusList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetBuyerAsProductId(int categoryId, int subCategoryId, int productId, int sellerId)
        {
            SellerProductBL sellerProductBL = new SellerProductBL();
            IEnumerable<BuyerProductViewModel> products = sellerProductBL.GetBuyerAsProductId(categoryId, subCategoryId, productId, sellerId);
            return Json(products, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddEditProduct(ProductViewModel productViewModel, List<ProductImageDetailViewModel> productImageDetails, List<ProductTechSpecificationViewModel> productTechSpecifications)
        {
            ResponseOut responseOut = new ResponseOut();
            ProductBL productBL = new ProductBL();
            try
            {
                if (productViewModel != null)
                {
                    productViewModel.CompanyId = 0;
                    productViewModel.CreatedBy = (int)BSContextUser.UserId;
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


        [HttpPost]
        public ActionResult UpdateSellerProductPrice(string sellerproductdetailsID, string price, string MinimumOrderQuantity)
        {
            ResponseOut responseOut = new ResponseOut();
            ProductBL productBL = new ProductBL();
            try
            {
                responseOut= productBL.UpdateSellerProductPrice(sellerproductdetailsID, price, MinimumOrderQuantity);

            }
            catch (Exception ex)
            {
                responseOut.message = ActionMessage.ApplicationException;
                responseOut.status = ActionStatus.Fail;
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(responseOut, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetSellerProductDetail(long salerProductDetailId)
        {
            try
            {
                SellerProductBL sellerProductBL = new SellerProductBL();
                var sellerProduct = sellerProductBL.GetSellerProductDetail(salerProductDetailId);
                return Json(sellerProduct, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(new BuyerProductSeller(), JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public PartialViewResult GetDashboardSellerProductsPricedetails(long salerProductDetailId)
        {
            SellerProductBL sellerBL = new SellerProductBL();
            IEnumerable<SellerProductPriceDashboardViewModel> products = sellerBL.GetDashboardSellerProductsPricedetails(salerProductDetailId);



            return PartialView("_SellerDashboardProductspricedetails", products);
          
        }

    }
}


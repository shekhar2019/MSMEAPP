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
            
            IEnumerable< SellerProductViewModel> products = SellerProductBL.GetSellerProductDetails(productMainGroupId, productSubGroupId, productChildSubGroupId, BuyerContextUser.UserId);
            return PartialView("_SellerProductList", products);
        }

        [HttpGet]
        public ActionResult GetAllSellerProducts()
        {
            SellerProductBL buyerProductBL = new SellerProductBL();
            IEnumerable<SellerProductViewModel> products = buyerProductBL.GetProductBySellerId(BuyerContextUser.UserId);
            return PartialView("_SellerProductList", products);
        }

        [HttpGet]
        public ActionResult GetDashboardSellerProducts()
        {
            SellerProductBL sellerBL = new SellerProductBL();
            IEnumerable<SellerProductDashboardViewModel> products = sellerBL.GetProductBySellerId(BuyerContextUser.UserId);
            return PartialView("_SellerProductDetail", products);
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
                        productViewModel.SellerId = BuyerContextUser.UserId;
                    }
                    responseOut = SellerProductBL.AddUpdateSellerProduct(productViewModel);
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
                    productViewModel.CreatedBy = (int)BuyerContextUser.UserId;
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

    }
}


using System.Web.Mvc;
using Portal.Core;
using Portal.Core.ViewModel;
using Portal.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;
using System.Reflection;
using System.Linq;

namespace Portal.Controllers
{
    public class BuyerProductController : BaseController
    {

        public ActionResult AddEditBuyerProduct()
        {
            UserRegistrationViewModel userRegistrationViewModel = Session[SessionKey.CurrentBSUser] != null ? (UserRegistrationViewModel)Session[SessionKey.CurrentBSUser] : new UserRegistrationViewModel();
            ViewData["userid"] = userRegistrationViewModel.UserId;
            var model = new BuyerProductViewModel();
            return View(model);
        }

        [HttpPost]
        //[ValidateRequest(true, UserInterfaceHelper.Add_Edit_Buyer_Product, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult AddEditBuyerProduct(BuyerProductViewModel productViewModel, bool isAdmin = false)
        {
            ResponseOut responseOut = new ResponseOut();
            ProductBL productBL = new ProductBL();
            try
            {
                if (productViewModel != null)
                {
                    BuyerProductBL buyerProductBL = new BuyerProductBL();
                    if (isAdmin)
                    {
                        productViewModel.BuyerId = ContextUser.UserId;
                    }
                    else
                    {
                        productViewModel.BuyerId = BSContextUser.UserId;
                    }

                    responseOut = buyerProductBL.AddUpdateBuyerProduct(productViewModel, 0);
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
        public ActionResult AddEditProductSubGroup(ProductSubGroupViewModel productsubgroupViewModel)
        {
            ResponseOut responseOut = new ResponseOut();
            ProductSubGroupBL productsubgroupBL = new ProductSubGroupBL();
            try
            {
                if (productsubgroupViewModel != null)
                {
                    responseOut = productsubgroupBL.AddEditProductSubGroup(productsubgroupViewModel);

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
        public ActionResult AddEditProductSubChildGroup(ProductSubChildGroupViewModel ProductSubChildGroupVM)
        {
            ResponseOut responseOut = new ResponseOut();
            ProductSubChildGroupBL productSubChildBL = new ProductSubChildGroupBL();
            try
            {
                if (ProductSubChildGroupVM != null)
                {
                    responseOut = productSubChildBL.AddEditProductSubChildGroup(ProductSubChildGroupVM);
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
        public ActionResult GetBuyerProducts(long productMainGroupId, long productSubGroupId, long productChildSubGroupId)
        {
            BuyerProductBL buyerProductBL = new BuyerProductBL();
            IEnumerable<BuyerProductViewModel> products = buyerProductBL.GetBuyerProductDetails(productMainGroupId, productSubGroupId, productChildSubGroupId, BSContextUser.UserId);
            return PartialView("_ByerProductList", products);
        }

        [HttpGet]
        public ActionResult GetAllBuyerProducts()
        {
            BuyerProductBL buyerProductBL = new BuyerProductBL();
            IEnumerable<BuyerProductViewModel> products = buyerProductBL.GetProductByBuyerId(BSContextUser.UserId);
            return PartialView("_ByerProductList", products);
        }

        [HttpGet]
        public ActionResult GetDashboardBuyerProducts(string sortBy = SortDashboardBy.LowestPrice, string category = "", string city = "")
        {
            BuyerProductBL buyerProductBL = new BuyerProductBL();
            IEnumerable<BuyerProductDashboardViewModel> products = buyerProductBL.GetBuyerDashBoardProducts(BSContextUser.UserId, category, city);
            foreach (var item in products)
            {
                if (sortBy == SortDashboardBy.LatestDate)
                {
                    item.Sellers = item.Sellers.OrderByDescending(x => x.ProductDate).ToList();
                }
                else if (sortBy == SortDashboardBy.LowestPrice)
                {
                    item.Sellers = item.Sellers.OrderBy(x => x.SalePrice).ToList();
                }
                else if (sortBy == SortDashboardBy.LargestPrice)
                {
                    item.Sellers = item.Sellers.OrderByDescending(x => x.SalePrice).ToList();
                }
            }
            if (sortBy == SortDashboardBy.LatestDate)
            {
                products = products.OrderByDescending(x => x.MaxSellerDate).ToList();
            }
            else if (sortBy == SortDashboardBy.LowestPrice)
            {
                products = products.OrderBy(x => x.MaxSellerPrice).ToList();
            }
            else if (sortBy == SortDashboardBy.LargestPrice)
            {
                products = products.OrderByDescending(x => x.MaxSellerPrice).ToList();
            }

            return PartialView("_BuyerDashboardProducts", products);
        }
        [HttpGet]
        public ActionResult GetSellerAsProductId(int categoryId, int subCategoryId, int productId, int buyerId)
        {
            BuyerProductBL buyerProductBL = new BuyerProductBL();
            IEnumerable<SellerProductViewModel> products = buyerProductBL.GetSellerAsProductId(categoryId, subCategoryId, productId, buyerId);
            return Json(products, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult DeleteBuyerProductDetail(long buyerProductDetailId)
        {
            ResponseOut responseOut = new ResponseOut();
            ProductBL productBL = new ProductBL();
            try
            {
                if (buyerProductDetailId != 0)
                {
                    BuyerProductBL buyerProductBL = new BuyerProductBL();
                    responseOut = buyerProductBL.DeleteUpdateBuyerProduct(buyerProductDetailId);
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

        public ActionResult BuyerFavouriteProduct()
        {
            return View();
        }

        public ActionResult GetBuyerProductDetail(long buyerProductDetailId)
        {
            try
            {
                BuyerProductBL buyerProductBL = new BuyerProductBL();
                var sellerProduct = buyerProductBL.GetBuyerProductDetail(buyerProductDetailId);
                return Json(sellerProduct, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(new BuyerProductSeller(), JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public PartialViewResult GetDashboardBuyerProductsQuantitydetails(long buyerproductdetailsID)
        {
         BuyerProductBL    BuyerBL = new BuyerProductBL();
            IEnumerable<BuyerProductQtyDashboardViewModel> products = BuyerBL.GetDashboardBuyerProductsQtydetails(buyerproductdetailsID);



            return PartialView("_BuyerDashboardProductsQtydetails", products);

        }

        [HttpPost]
        public ActionResult UpdateBuyerProductQTY(string buyerproductdetailsID, string DemandOrderQTY)
        {
            ResponseOut responseOut = new ResponseOut();
            ProductBL productBL = new ProductBL();
            try
            {
                responseOut = productBL.UpdateBuyerProductQTY(buyerproductdetailsID, DemandOrderQTY);

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

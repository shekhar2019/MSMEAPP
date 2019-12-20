using System.Web.Mvc;
using Portal.Core;
using Portal.Core.ViewModel;
using Portal.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;
using System.Reflection;

namespace Portal.Controllers
{
    public class BuyerProductController : BaseController
    {
        //
        // GET: /BuyerProduct/

        public ActionResult AddEditBuyerProduct()
        {
            UserRegistrationViewModel userRegistrationViewModel = Session[SessionKey.CurrentBSUser] != null ? (UserRegistrationViewModel)Session[SessionKey.CurrentBSUser] : new UserRegistrationViewModel();
            ViewData["userid"] = userRegistrationViewModel.UserId;
            var model = new BuyerProductViewModel();
            return View(model);
        }

        [HttpPost]
        //[ValidateRequest(true, UserInterfaceHelper.Add_Edit_Buyer_Product, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult AddEditBuyerProduct(BuyerProductViewModel productViewModel,bool isAdmin=false)
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
                        productViewModel.BuyerId = BuyerContextUser.UserId;
                    }
                    
                    responseOut = buyerProductBL.AddUpdateBuyerProduct(productViewModel);
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
       // [ValidateRequest(true, UserInterfaceHelper.Add_Edit_ProductSubGroup_INVENTORY, (int)AccessMode.AddAccess, (int)RequestMode.Ajax)]
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

        [HttpGet]
        public ActionResult GetBuyerProducts(long productMainGroupId, long productSubGroupId, long productChildSubGroupId)
        {
            BuyerProductBL buyerProductBL = new BuyerProductBL();
            IEnumerable<BuyerProductViewModel> products = buyerProductBL.GetBuyerProductDetails(productMainGroupId, productSubGroupId, productChildSubGroupId, BuyerContextUser.UserId);
            return PartialView("_ByerProductList", products);
        }

        [HttpGet]
        public ActionResult GetAllBuyerProducts()
        {
            BuyerProductBL buyerProductBL = new BuyerProductBL();
            IEnumerable<BuyerProductViewModel> products = buyerProductBL.GetProductByBuyerId( BuyerContextUser.UserId);
            return PartialView("_ByerProductList", products);
        }

        [HttpGet]
        public ActionResult GetDashboardBuyerProducts()
        {
            BuyerProductBL buyerProductBL = new BuyerProductBL();
            IEnumerable<BuyerProductDashboardViewModel> products = buyerProductBL.GetProductByBuyerId(BuyerContextUser.UserId);
             return PartialView("_ByerProductDetail", products);
        }
        [HttpGet]
        public ActionResult GetSellerAsProductId(int categoryId,int subCategoryId,int productId,int buyerId)
        {
            BuyerProductBL buyerProductBL = new BuyerProductBL();
            IEnumerable<SellerProductViewModel> products = buyerProductBL.GetSellerAsProductId(categoryId,subCategoryId,productId,buyerId);
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
    }
}

﻿using System;
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
    public class ProductTypeController : BaseController
    {
        //
        // GET: /Company/
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_ProductType_ADMIN, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult AddEditProductType(int producttypeId = 0, int accessMode = 3)
        {

            try
            {
                if (producttypeId != 0)
                {
                    ViewData["producttypeId"] = producttypeId;
                    ViewData["accessMode"] = accessMode;
                }
                else
                {
                    ViewData["producttypeId"] = 0;
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
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_ProductType_ADMIN, (int)AccessMode.AddAccess, (int)RequestMode.Ajax)]
        public ActionResult AddEditProductType(ProductTypeViewModel producttypeViewModel)
        {
            ResponseOut responseOut = new ResponseOut();
            ProductTypeBL producttypeBL = new ProductTypeBL();
            try
            {
                if (producttypeViewModel != null)
                {
                    responseOut = producttypeBL.AddEditProductType(producttypeViewModel);
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


        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_ProductType_ADMIN, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult ListProductType()
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
        public PartialViewResult GetProductTypeList(string producttypeName = "", string producttypeCode = "", string producttypeStatus = "")
        {
            List<ProductTypeViewModel> producttypes = new List<ProductTypeViewModel>();
            ProductTypeBL producttypeBL = new ProductTypeBL();
            try
            {
                producttypes = producttypeBL.GetProductTypeList(producttypeName, producttypeCode, producttypeStatus);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(producttypes);
        }

       


        [HttpGet]
        public JsonResult GetProductTypeDetail(int producttypeId)
        {
            ProductTypeBL producttypeBL = new ProductTypeBL();
            ProductTypeViewModel producttype = new ProductTypeViewModel();
            try
            {
                producttype = producttypeBL.GetProductTypeDetail(producttypeId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(producttype, JsonRequestBehavior.AllowGet);
        }

    }
}

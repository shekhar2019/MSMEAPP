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
    public class ProductSubGroupController : BaseController
    {
        //
        // GET: /Company/
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_ProductSubGroup_INVENTORY, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult AddEditProductSubGroup(int productsubgroupId = 0, int accessMode = 3)
        {

            try
            {
                if (productsubgroupId != 0)
                {
                    ViewData["productsubgroupId"] = productsubgroupId;
                    ViewData["accessMode"] = accessMode;
                }
                else
                {
                    ViewData["productsubgroupId"] = 0;
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
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_ProductSubGroup_INVENTORY, (int)AccessMode.AddAccess, (int)RequestMode.Ajax)]
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


        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_ProductSubGroup_INVENTORY, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult ListProductSubGroup()
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
        public PartialViewResult GetProductSubGroupList(string productsubgroupName = "", string productsubgroupCode = "", int productmaingroupId = 0, string Status = "")
        {
            List<ProductSubGroupViewModel> productsubgroup = new List<ProductSubGroupViewModel>();
            ProductSubGroupBL productsubgroupBL = new ProductSubGroupBL();
            try
            {
                productsubgroup = productsubgroupBL.GetProductSubGroupList(productsubgroupName, productsubgroupCode, productmaingroupId, Status);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(productsubgroup);
        }

         
        [HttpGet]
        public JsonResult GetProductSubGroupDetail(int productsubgroupId)
        {
            ProductSubGroupBL productsubgroupBL = new ProductSubGroupBL();
            ProductSubGroupViewModel productsubgroup = new ProductSubGroupViewModel();
            try
            {
                productsubgroup = productsubgroupBL.GetProductSubGroupDetail(productsubgroupId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(productsubgroup, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateProductSubGroupImage()
        {
            ResponseOut responseOut = new ResponseOut();
            HttpFileCollectionBase files = Request.Files;
            ProductSubGroupViewModel productSubGroupViewModel = new ProductSubGroupViewModel();
            ProductSubGroupBL productSubGroupBL = new ProductSubGroupBL();
            try
            {

                productSubGroupViewModel.ProductSubGroupId = Convert.ToInt32(Request["productSubGroupId"]);
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
                        var aboutFilename = productSubGroupViewModel.ProductSubGroupId.ToString() + "_" + fileName;
                        var path = Path.Combine(Server.MapPath("~/Images/ProductSubGroupImages"), aboutFilename);
                        file.SaveAs(path);
                        productSubGroupViewModel.ProductSubGroupImageUrl = aboutFilename;

                    }
                }

                if (productSubGroupViewModel != null && !string.IsNullOrEmpty(productSubGroupViewModel.ProductSubGroupImageUrl))
                {
                    responseOut = productSubGroupBL.UpdateProductSubGroupPic(productSubGroupViewModel);
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

    }
}
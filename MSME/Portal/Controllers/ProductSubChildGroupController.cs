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
using Portal.DAL;

namespace Portal.Controllers
{
    public class ProductSubChildGroupController : BaseController
    {
        //
        // GET: /ProductSubChildGroup/
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_ProductSubChildGroup, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult AddEditProductSubChildGroup(int productSubChildGroupId = 0, int accessMode = 3)
        {
            try
            {
                if (productSubChildGroupId != 0)
                {
                    ViewData["productSubChildGroupId"] = productSubChildGroupId;
                    ViewData["accessMode"] = accessMode;
                }
                else
                {
                    ViewData["productSubChildGroupId"] = 0;
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
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_ProductSubChildGroup, (int)AccessMode.AddAccess, (int)RequestMode.Ajax)]
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

        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_ProductSubChildGroup, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult ListProductSubChildGroup()
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
        public PartialViewResult GetProductSubChildGroupList(string productSubChildGroupName="",string productSubChildGroupCode="",string status="")
        {
            ProductSubChildGroupBL productSubChildGroupBL = new ProductSubChildGroupBL();
            List<ProductSubChildGroupViewModel> productSubChildGroup = new List<ProductSubChildGroupViewModel>();
            try
            {
                productSubChildGroup = productSubChildGroupBL.GetProductSubChildGroupList(productSubChildGroupName, productSubChildGroupCode, status);
            }
            catch(Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(productSubChildGroup);
        }

        [HttpGet]
        public JsonResult ProductSubChildGroupDetail(int productSubChildGroupId)
        {
            ProductSubChildGroupBL productSubChildGroupBL = new ProductSubChildGroupBL();
            ProductSubChildGroupViewModel productSubChildGroupVM = new ProductSubChildGroupViewModel();
            try
            {
                productSubChildGroupVM = productSubChildGroupBL.ProductSubChildGroupDetail(productSubChildGroupId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(productSubChildGroupVM, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateProductSubChildGroupImage()
        {
            ResponseOut responseOut = new ResponseOut();
            HttpFileCollectionBase files = Request.Files;
            ProductSubChildGroupViewModel productSubChildGroupViewModel = new ProductSubChildGroupViewModel();
            ProductSubChildGroupBL productSubChildGroupBL = new ProductSubChildGroupBL();
            try
            {

                productSubChildGroupViewModel.ProductSubGroupId = Convert.ToInt32(Request["productSubChildGroupId"]);
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
                        var aboutFilename = productSubChildGroupViewModel.ProductSubChildGroupId.ToString() + "_" + fileName;
                        var path = Path.Combine(Server.MapPath("~/Images/ProductSubChildGroupImages"), aboutFilename);
                        file.SaveAs(path);
                        productSubChildGroupViewModel.ProductSubChildGroupImageUrl = aboutFilename;

                    }
                }

                if (productSubChildGroupViewModel != null && !string.IsNullOrEmpty(productSubChildGroupViewModel.ProductSubChildGroupImageUrl))
                {
                    responseOut = productSubChildGroupBL.UpdateProductSubChildGroupPic(productSubChildGroupViewModel);
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

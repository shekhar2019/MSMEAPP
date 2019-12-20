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
    public class MSMEController : BaseController
    {
        //
        // GET: /MSME/

        public ActionResult Index()
        {
            MSMEBL msmeBL = new MSMEBL();
            try
            {
                ViewBag.MSMEList = msmeBL.GetMSMEList();
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return View();    
        }

        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_MSME, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult AddEditMSME(int MSMEId = 0, int accessMode = 3)
        {
            try
            {
                if (MSMEId != 0)
                {
                    ViewData["MSMEId"] = MSMEId;
                    ViewData["accessMode"] = accessMode;
                }
                else
                {
                    ViewData["MSMEId"] = 0;
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
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_MSME, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult AddEditMSME(MSMEViewModel msmeViewModel)
        {
            ResponseOut responseOut = new ResponseOut();
            MSMEBL msmeBL = new MSMEBL();
            try
            {
                if (msmeViewModel != null)
                {
                    msmeViewModel.CreatedBy = ContextUser.UserId;
                    responseOut = msmeBL.AddEditMSME(msmeViewModel);
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
        public ActionResult UpdateMSMEImage()
        {
            ResponseOut responseOut = new ResponseOut();
            HttpFileCollectionBase files = Request.Files;
            MSMEViewModel msmeViewModel = new MSMEViewModel();
            MSMEBL msmeBL = new MSMEBL();
            try
            {
                msmeViewModel.MSMEId = Convert.ToInt32(Request["MSMEId"]);
                
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
                        var msmefilename = msmeViewModel.MSMEId.ToString() + "_" + fileName;
                        var path = Path.Combine(Server.MapPath("~/Images/MSME"), msmefilename);
                        file.SaveAs(path);
                        msmeViewModel.MSMEImageUrl = msmefilename;

                    }
                }

                if (msmeViewModel != null && !string.IsNullOrEmpty(msmeViewModel.MSMEImageUrl))
                {
                    responseOut = msmeBL.UpdateMSMEPic(msmeViewModel);
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

        [HttpGet]
        public PartialViewResult GetMSMEList()
        {
            MSMEBL msmeBL = new MSMEBL();
            List<MSMEViewModel> msmes = new List<MSMEViewModel>();
            try
            {
                msmes = msmeBL.GetMSMEList();
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(msmes);
        }

        public ActionResult RemoveMSME(int msmeID)
        {
            ResponseOut responseOut = new ResponseOut();
            MSMEBL msmeBL = new MSMEBL();
             try
            {

                responseOut = msmeBL.RemoveMSME(msmeID);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return Json(responseOut, JsonRequestBehavior.AllowGet);
        }
    }
}

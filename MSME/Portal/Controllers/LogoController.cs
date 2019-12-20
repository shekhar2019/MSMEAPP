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
    public class LogoController : BaseController
    {
        //
        // GET: /Logo/

        #region Logo
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_LOGO, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult AddEditLogo(int logoId = 0, int accessMode = 3)
        {
            try
            {
                if (logoId != 0)
                {
                    ViewData["logoId"] = logoId;
                    ViewData["accessMode"] = accessMode;
                }
                else
                {
                    ViewData["logoId"] = 0;
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
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_LOGO, (int)AccessMode.AddAccess, (int)RequestMode.Ajax)]
        public ActionResult AddEditLogo(LogoViewModel logoViewModel)
        {
            ResponseOut responseOut = new ResponseOut();
            LogoBL logoBL = new LogoBL();
            try
            {
                if (logoViewModel != null)
                {
                    logoViewModel.CreatedBy = ContextUser.UserId;
                    responseOut = logoBL.AddEditLogo(logoViewModel);
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
        public PartialViewResult GetLogoList()
        {
            LogoBL logoBL = new LogoBL();
            List<LogoViewModel> logoes = new List<LogoViewModel>();
            try
            {
                logoes= logoBL.GetLogoList();
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(logoes);
        }

        [HttpPost]
        public ActionResult UpdateLogo()
        {
            ResponseOut responseOut = new ResponseOut();
            HttpFileCollectionBase files = Request.Files;
            ProductViewModel productViewModel = new ProductViewModel();
            LogoViewModel logoViewModel = new LogoViewModel();
            LogoBL logoBL = new LogoBL();
            try
            {

                logoViewModel.LogoId = Convert.ToInt32(Request["logoId"]);
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
                        var logoFilename = logoViewModel.LogoId.ToString() + "_" + fileName;
                        var path = Path.Combine(Server.MapPath("~/Images/CompanyLogo"), logoFilename);
                        file.SaveAs(path);
                        logoViewModel.LogoPath = logoFilename;

                        //queryDetail.QueryAttachment = new byte[file.ContentLength];
                        //file.InputStream.Read(queryDetail.QueryAttachment, 0, file.ContentLength);
                    }
                }

                if (logoViewModel != null && !string.IsNullOrEmpty(logoViewModel.LogoPath))
                {
                    responseOut = logoBL.UpdateLogoPic(logoViewModel);
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

        public ActionResult RemoveLogo(int logoId)
        {
            ResponseOut responseOut = new ResponseOut();
            LogoBL logoBL = new LogoBL();
            try
            {

                responseOut = logoBL.RemoveLogo(logoId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return Json(responseOut, JsonRequestBehavior.AllowGet);
        }
        #endregion Logo

    }
}

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
    public class AboutusController : BaseController
    {
        //
        // GET: /Aboutus/

        #region Aboutus

        public ActionResult Index()
        {
            WebBL webBL = new WebBL();
            try
            {
                ViewBag.AboutList = webBL.GetAboutList();
            }
            catch(Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return View();
        }

        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_LOGO, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult AddEditAboutus(int aboutusId = 0, int accessMode = 3)
        {
            try
            {
                if (aboutusId != 0)
                {
                    ViewData["aboutusId"] = aboutusId;
                    ViewData["accessMode"] = accessMode;
                }
                else
                {
                    ViewData["aboutusId"] = 0;
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
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_LOGO, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult AddEditAboutus(AboutUsViewModel aboutUsViewModel)
        {
            ResponseOut responseOut = new ResponseOut();

            AboutusBL aboutusBL = new AboutusBL();
            try
            {
                if (aboutUsViewModel != null)
                {
                    aboutUsViewModel.CreatedBy = ContextUser.UserId;
                    responseOut = aboutusBL.AddEditAboutus(aboutUsViewModel);
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
        public ActionResult UpdateAboutUsImage()
        {
            ResponseOut responseOut = new ResponseOut();
            HttpFileCollectionBase files = Request.Files;
            AboutUsViewModel aboutUsViewModel = new AboutUsViewModel();
            AboutusBL aboutusBL = new AboutusBL();
            try
            {

                aboutUsViewModel.AboutusId = Convert.ToInt32(Request["AboutusId"]);
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
                        var aboutFilename = aboutUsViewModel.AboutusId.ToString() + "_" + fileName;
                        var path = Path.Combine(Server.MapPath("~/Images/AboutUs"), aboutFilename);
                        file.SaveAs(path);
                        aboutUsViewModel.ImageUrl = aboutFilename;

                    }
                }

                if (aboutUsViewModel != null && !string.IsNullOrEmpty(aboutUsViewModel.ImageUrl))
                {
                    responseOut = aboutusBL.UpdateAboutPic(aboutUsViewModel);
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
        public PartialViewResult GetAboutUsList()
        {
            AboutusBL aboutusBL = new AboutusBL();
            List<AboutUsViewModel> abouts = new List<AboutUsViewModel>();
            try
            {
                abouts = aboutusBL.GetAboutUsList();
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(abouts);
        }
        #endregion

    }
}

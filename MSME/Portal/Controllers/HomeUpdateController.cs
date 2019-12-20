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
    public class HomeUpdateController : BaseController
    {
        //
        // GET: /HomeUpdate/

        public ActionResult Index()
        {
            return View();
        }

        #region HomeAbout
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_HomeUpdate, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult AddEditHomeUpdate(int updateId = 0, int accessMode = 3)
        {
            try
            {
                ViewData["currentDate"] = DateTime.Now.ToString("dd-MMM-yyyy");
                if (updateId != 0)
                {
                    ViewData["updateId"] = updateId;
                    ViewData["accessMode"] = accessMode;
                }
                else
                {
                    ViewData["updateId"] = 0;
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
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_HomeUpdate, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult AddEditHomeUpdate(HomeUpdateViewModel homeUpdateViewModel)
        {
            ResponseOut responseOut = new ResponseOut();
          
            HomeUpdateBL homeUpdateBL = new HomeUpdateBL();
            try
            {
                if (homeUpdateViewModel != null)
                {
                    homeUpdateViewModel.CreatedBy = ContextUser.UserId;
                    responseOut = homeUpdateBL.AddEditHomeUpdate(homeUpdateViewModel);
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
        public ActionResult UpdateImage()
        {
            ResponseOut responseOut = new ResponseOut();
            HttpFileCollectionBase files = Request.Files;
            HomeUpdateViewModel homeUpdateViewModel = new HomeUpdateViewModel();
            HomeUpdateBL homeUpdateBL = new HomeUpdateBL();
            try
            {

                homeUpdateViewModel.UpdateId = Convert.ToInt32(Request["updateId"]);
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
                        var logoFilename = homeUpdateViewModel.UpdateId.ToString() + "_" + fileName;
                        var path = Path.Combine(Server.MapPath("~/Images/HomeUpdate"), logoFilename);
                        file.SaveAs(path);
                        homeUpdateViewModel.ImageUrl = logoFilename;

                        //queryDetail.QueryAttachment = new byte[file.ContentLength];
                        //file.InputStream.Read(queryDetail.QueryAttachment, 0, file.ContentLength);
                    }
                }

                if (homeUpdateViewModel != null && !string.IsNullOrEmpty(homeUpdateViewModel.ImageUrl))
                {
                    responseOut = homeUpdateBL.UpdateImage(homeUpdateViewModel);
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
        public PartialViewResult GetHomeUpdateList()
        {
            HomeUpdateBL homeUpdateBL = new HomeUpdateBL();
            List<HomeUpdateViewModel> homeUpdates = new List<HomeUpdateViewModel>();
            try
            {
                homeUpdates = homeUpdateBL.GetHomeUpdateList("");
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(homeUpdates);
        }

        public ActionResult RemoveHomeUpdate(int updateId)
        {
            ResponseOut responseOut = new ResponseOut();
            HomeUpdateBL homeUpdateBL = new HomeUpdateBL();
            try
            {

                responseOut = homeUpdateBL.RemoveHomeUpdate(updateId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return Json(responseOut, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}

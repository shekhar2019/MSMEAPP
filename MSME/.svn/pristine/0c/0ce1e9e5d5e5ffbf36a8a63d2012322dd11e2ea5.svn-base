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
    public class HomeSliderController : BaseController
    {
        //
        // GET: /HomeSlider/
        #region HomeSlider
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_HomeSlider, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult HomeSlider(int homeSliderId = 0, int accessMode = 3)
        {
            try
            {
                if (homeSliderId != 0)
                {
                    ViewData["homeSliderId"] = homeSliderId;
                    ViewData["accessMode"] = accessMode;
                }
                else
                {
                    ViewData["homeSliderId"] = 0;
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
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_HomeSlider, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult HomeSlider(HomeSliderViewModel homeSliderViewModel)
        {
            ResponseOut responseOut = new ResponseOut();

            HomeSliderBL homeSliderBL = new HomeSliderBL();
            try
            {
                if (homeSliderViewModel != null)
                {
                    homeSliderViewModel.CreatedBy = ContextUser.UserId;
                    responseOut = homeSliderBL.AddEditHomeSlider(homeSliderViewModel);
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
        public ActionResult UpdateHomeSliderImage()
        {
            ResponseOut responseOut = new ResponseOut();
            HttpFileCollectionBase files = Request.Files;
            HomeSliderViewModel homeSliderViewModel = new HomeSliderViewModel();
            HomeSliderBL homeSliderBL = new HomeSliderBL();
            try
            {

                homeSliderViewModel.Sliderid = Convert.ToInt32(Request["sliderId"]);
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
                        var sliderFilename = homeSliderViewModel.Sliderid.ToString() + "_" + fileName;
                        var path = Path.Combine(Server.MapPath("~/media/home_slider/"), sliderFilename);
                        file.SaveAs(path);
                        homeSliderViewModel.Image = "/media/home_slider/"+sliderFilename;

                    }
                }

                if (homeSliderViewModel != null && !string.IsNullOrEmpty(homeSliderViewModel.Image))
                {
                    responseOut = homeSliderBL.UpdateHomeSliderPic(homeSliderViewModel);
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
        public PartialViewResult GetHomeSliderList()
        {
            HomeSliderBL homeSliderBL = new HomeSliderBL();
            List<HomeSliderViewModel> homeSliders = new List<HomeSliderViewModel>();
            try
            {
                homeSliders = homeSliderBL.GetHomeSliderList();
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(homeSliders);
        }

        public ActionResult RemoveSlider(int sliderid)
        {
            ResponseOut responseOut = new ResponseOut();
            HomeSliderBL homeSliderBL = new HomeSliderBL();
            try
            {
                responseOut = homeSliderBL.RemoveSliderHome(sliderid);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return Json(responseOut, JsonRequestBehavior.AllowGet);
        }
        #endregion HomeSlider

    }
}

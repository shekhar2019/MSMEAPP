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
    public class ContactInfoController : BaseController
    {
        //
        // GET: /ContactInfo/

        #region ContactInfo
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_ContactInfo, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult AddEditContactInfo(int Id = 0, int accessMode = 3)
        {
            try
            {
                if (Id != 0)
                {
                    ViewData["Id"] = Id;
                    ViewData["accessMode"] = accessMode;
                }
                else
                {
                    ViewData["Id"] = 0;
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
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_ContactInfo, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]

        public ActionResult AddEditContactInfo(ContactInfoViewModel contactInfoViewModel)
        {
            ResponseOut responseOut = new ResponseOut();
            ContactInfoBL contactInfoBL = new ContactInfoBL();
            try
            {
                if (contactInfoViewModel != null)
                {
                    contactInfoViewModel.CreatedBy = ContextUser.UserId;
                    responseOut = contactInfoBL.AddEditContactInfo(contactInfoViewModel);
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


        public PartialViewResult GetContactInfoList()
        {
            ContactInfoBL contactInfoBL = new ContactInfoBL();
            List<ContactInfoViewModel> contactInfo = new List<ContactInfoViewModel>();
            try
            {
                contactInfo = contactInfoBL.GetContactInfoList();
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(contactInfo);
        }

        public ActionResult RemoveContactInfo(int contactInfoId)
        {
            ResponseOut responseOut = new ResponseOut();
            ContactInfoBL contactInfoBL = new ContactInfoBL();
            try
            {

                responseOut = contactInfoBL.RemoveContactInfo(contactInfoId);
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

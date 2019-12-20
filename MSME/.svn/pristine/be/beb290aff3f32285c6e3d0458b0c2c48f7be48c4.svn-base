using Portal.Common;
using Portal.Core;
using Portal.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Portal.Controllers
{
    public class ContactUsController : Controller
    {
        //
        // GET: /ContactUs/
        public ActionResult Index()
        {
            ContactInfoBL contactInfoBL = new ContactInfoBL();
            List<ContactInfoViewModel> contactInfo = new List<ContactInfoViewModel>();
            try
            {
               ViewBag.contactInfo = contactInfoBL.GetContactInfoList();
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }

            return View();
        }

        #region Contact Us
        [ValidateRequest(true,UserInterfaceHelper.Add_Edit_ContactUs,(int)AccessMode.ViewAccess,(int)RequestMode.GetPost)]
        public ActionResult AddEditContactUs(int contactInfoId=0,int accessMode=3)
        {
            try
            {
                if(contactInfoId==0)
                {
                    ViewData["contactInfoId"] = contactInfoId;
                    ViewData["accessMode"] = accessMode;
                }
            }
            catch(Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return View();
        }
        [HttpPost]
        public ActionResult SendEnquiry(string customerName,string customerMail,string customerMobile,string query)
        {
            bool mailStatus = false;
            ResponseOut responseOut = new ResponseOut();
            try
            {
                
                SendMail sendMail = new SendMail();
                string mailBody = "Hi Admin <br> We recived enquiry from contact us page<br>";
                mailBody += "Name " + customerName+"<br>";
                mailBody += "Email " + customerMail + "<br>";
                mailBody += "Mobile " + customerMobile + "<br>";
                mailBody += "Message " + query + "<br>";
                mailStatus = sendMail.SendEmail("anil.kumar@htssolutons.org", "Enquiry From MSME Contact Page", mailBody);
                if(mailStatus)
                {
                    responseOut.message = "Thank You Contacting Us will revert within 48 hours.";
                    responseOut.status = ActionStatus.Success;

                }
                else
                {
                    responseOut.message = "Problem in Sending Mail!!!";
                    responseOut.status = ActionStatus.Fail;
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(responseOut, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}

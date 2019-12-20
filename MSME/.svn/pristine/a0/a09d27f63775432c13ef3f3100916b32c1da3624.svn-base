using System;
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
    public class MenuController : BaseController
    {
        //
        // GET: /Menu/
        #region Home Menu Add Edit 
        [ValidateRequest(true,UserInterfaceHelper.Add_Edit_Menu,(int)AccessMode.ViewAccess,(int)RequestMode.GetPost)]
        public ActionResult AddEditMenu(int menuId=0, int accessMode = 3)
        {
            if(menuId!=0)
            {
                ViewData["menuId"] = menuId;
                ViewData["accessMode"] = accessMode;
            }
            else
            {
                ViewData["menuId"] = 0;
                ViewData["accessMode"] = 0;
            }
        
             return View();
        }
        [HttpPost]
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_Menu, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult AddEditMenu(MenuViewModel menuViewModel)
        {
            ResponseOut responseOut = new ResponseOut();
            MenuBL menuBL = new MenuBL();
            try
            {
                menuViewModel.CreatedBy = ContextUser.CreatedBy;
                menuViewModel.CreateDate = DateTime.Now.ToString();
                responseOut = menuBL.AddEditMenu(menuViewModel);
            }
            catch(Exception ex)
            {
                responseOut.message = ActionMessage.ApplicationException;
                responseOut.status = ActionStatus.Fail;
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }

            return Json(responseOut, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public PartialViewResult GetMenuList()
        {
            List<MenuViewModel> menuList = new List<MenuViewModel>();
            MenuBL menuBL = new MenuBL();
            try
            {
               menuList= menuBL.GetMenuList();
            }
            catch(Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }

            return PartialView(menuList);
        }

        [HttpPost]
        public ActionResult RemoveMenu(int menuId)
        {
            ResponseOut responseOut = new ResponseOut();
            MenuBL menuBL = new MenuBL();
            try
            {
                responseOut = menuBL.RemoveMenu(menuId);

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(responseOut, JsonRequestBehavior.AllowGet);
            
        }
        #endregion
        public ActionResult Index()
        {
            return View();
        }

    }
}

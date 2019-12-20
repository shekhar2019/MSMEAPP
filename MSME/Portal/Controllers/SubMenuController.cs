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
    public class SubMenuController : BaseController
    {
        //
        // GET: /SubMenu/
        #region SubMenu Add Edit 
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_SubMenu, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult AddEditSubMenu(int menuId = 0, int accessMode = 3)
        {
            if (menuId != 0)
            {
                ViewData["subMenuId"] = menuId;
                ViewData["accessMode"] = accessMode;
            }
            else
            {
                ViewData["subMenuId"] = 0;
                ViewData["accessMode"] = 0;
            }

            return View();
        }
        [HttpPost]
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_SubMenu, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult AddEditSubMenu(SubMenuViewModel subMenuViewModel)
        {
            ResponseOut responseOut = new ResponseOut();
            SubMenuBL menuBL = new SubMenuBL();
            try
            {
                responseOut = menuBL.AddEditSubMenu(subMenuViewModel);
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
        public JsonResult GetMenuList()
        {
            SubMenuBL subMenuBL = new SubMenuBL();
            List<MenuViewModel> menus = new List<MenuViewModel>();
            try
            {
                menus = subMenuBL.GetMenuList();
            }
            catch(Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(menus, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public PartialViewResult GetSubMenuList()
        {
            SubMenuBL subMenuBL = new SubMenuBL();
            List<SubMenuViewModel> menus = new List<SubMenuViewModel>();
            try
            {
                menus = subMenuBL.GetSubMenuList();
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(menus);
        }
        [HttpPost]
        public ActionResult RemoveSubMenu(int menuId)
        {
            ResponseOut responseOut = new ResponseOut();
            SubMenuBL subMenuBL = new SubMenuBL();
            try
            {
                responseOut = subMenuBL.RemoveSubMenu(menuId);
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

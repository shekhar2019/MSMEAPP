using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Portal.Core;
using Portal.Core.ViewModel;
using Portal.Common;
using System.Reflection;
using Portal.DAL;

namespace Portal.Controllers
{
    public class SubChildMenuController : BaseController { 
        //
        // GET: /SubChildMenu/
        #region SubMenu Add Edit 
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_SubChildMenu, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult AddEditSubChildMenu(int menuId = 0, int accessMode = 3)
      {
        if (menuId != 0)
        {
            ViewData["subChildMenuId"] = menuId;
            ViewData["accessMode"] = accessMode;
        }
        else
        {
            ViewData["subChildMenuId"] = 0;
            ViewData["accessMode"] = 0;
        }

        return View();
    }
        [HttpPost]
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_SubChildMenu, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult AddEditSubChildMenu(SubChildMenuViewModel subChildMenuViewModel)
        {
            ResponseOut responseOut = new ResponseOut();
            SubChildMenuBL subChildMenuBL = new SubChildMenuBL();
            try
            {
                responseOut = subChildMenuBL.AddEditSubChildMenu(subChildMenuViewModel);
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
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(menus, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetSubMenuList(int menuId)
        {

            SubChildMenuBL subChildMenuBL = new SubChildMenuBL();
            List<SubMenuViewModel> subMenus = new List<SubMenuViewModel>();
            try
            {
                subMenus = subChildMenuBL.GetSubMenuList(menuId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(subMenus, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public PartialViewResult GetSubChildMenuList(int subMenuId=0)
        {
            SubChildMenuBL subChildMenuBL = new SubChildMenuBL();
            List<SubChildMenuViewModel> menus = new List<SubChildMenuViewModel>();
            try
            {
                menus = subChildMenuBL.GetSubChildMenu(subMenuId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(menus);
        }
        [HttpPost]
        public ActionResult RemoveSubChildMenu(int menuId)
        {
            ResponseOut responseOut = new ResponseOut();
            
            SubChildMenuBL subChildMenuBL = new SubChildMenuBL();
            try
            {
                responseOut = subChildMenuBL.RemoveSubChildMenu(menuId);
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

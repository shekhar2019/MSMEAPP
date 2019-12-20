using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Portal.Core.ViewModel;
using Portal.DAL;
using Portal.Common;
using System.Reflection;
using System.Data;

namespace Portal.Core
{
 public  class SubMenuBL
    {
        DBInterface dbInterface;
        public SubMenuBL()
        {
            dbInterface = new DBInterface();
        }
        public List<MenuViewModel> GetMenuList()
        {
            List<MenuViewModel> menuList = new List<MenuViewModel>();
            try
            {
                List<Menu> menus = dbInterface.GetMenuList();
                foreach (Menu menu in menus)
                {
                    if (menu.MenuStatus == true)
                    {
                        menuList.Add(new MenuViewModel
                        {
                            MenuId = menu.MenuId,
                            MenuName = menu.MenuName,
                            MenuLink = menu.MenuLink,
                            MenuStatus = Convert.ToBoolean(menu.MenuStatus),
                            SequenceNo = Convert.ToInt32(menu.SequenceNo)
                        });
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return menuList;
        }

        public ResponseOut AddEditSubMenu(SubMenuViewModel subMenuViewModel)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                SubMenu menu = new SubMenu
                {
                    SubMenuId = subMenuViewModel.SubMenuId,
                    MenuId =subMenuViewModel.MenuId,
                    PageName=subMenuViewModel.PageName,
                    SubMenuName = subMenuViewModel.SubMenuName,
                    SubMenuLink = subMenuViewModel.SubMenuLink,
                    SubMenuStatus = subMenuViewModel.SubMenuStatus,
                    SequenceNo = subMenuViewModel.SequenceNo
                };
                responseOut = dbInterface.AddEditSubMenu(menu);

            }
            catch (Exception ex)
            {
                responseOut.status = ActionStatus.Fail;
                responseOut.message = ActionMessage.ApplicationException;
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return responseOut;
        }

        public List<SubMenuViewModel> GetSubMenuList()
        {
            List<SubMenuViewModel> subMenuList = new List<SubMenuViewModel>();
            try
            {
                List<SubMenu> menus = dbInterface.GetSubMenuList();
                foreach (SubMenu menu in menus)
                {
                    subMenuList.Add(new SubMenuViewModel
                    {
                        SubMenuId=menu.SubMenuId,
                        MenuName=menu.MenuName,
                        MenuId = menu.MenuId,
                        PageName=menu.PageName,
                        SubMenuName = menu.SubMenuName,
                        SubMenuLink = menu.SubMenuLink,
                        SubMenuStatus = Convert.ToBoolean(menu.SubMenuStatus),
                        SequenceNo = Convert.ToInt32(menu.SequenceNo)
                    });
                }

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return subMenuList;
        }

        public ResponseOut RemoveSubMenu(int subMenuId)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                responseOut = dbInterface.RemoveSubMenu(subMenuId);

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return responseOut;
        }
    }
}

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
   
    public class MenuBL
    {
        DBInterface dbInterface;
        public MenuBL()
        {
             dbInterface = new DBInterface();
        }

        public ResponseOut AddEditMenu(MenuViewModel menuViewModel)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                Menu menu = new Menu
                {
                    MenuId = menuViewModel.MenuId,
                    MenuName= menuViewModel.MenuName,
                    MenuLink= menuViewModel.MenuLink,
                    MenuStatus=menuViewModel.MenuStatus,
                    SequenceNo=menuViewModel.SequenceNo,
                    CreatedBy=menuViewModel.CreatedBy,
                    CreateDate=Convert.ToDateTime(menuViewModel.CreateDate),
                  
                    
                };
                responseOut = dbInterface.AddEditMenu(menu);

            }
            catch(Exception ex)
            {
                responseOut.status = ActionStatus.Fail;
                responseOut.message = ActionMessage.ApplicationException;
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return responseOut;
        }

        public List<MenuViewModel> GetMenuList()
        {
            List<MenuViewModel> menuList = new List<MenuViewModel>();
           try
            {
                List<Menu> menus = dbInterface.GetMenuList();
                foreach(Menu menu in menus)
                {
                    menuList.Add(new MenuViewModel {
                        MenuId=menu.MenuId,
                        MenuName=menu.MenuName,
                        MenuLink=menu.MenuLink,
                        MenuStatus=Convert.ToBoolean(menu.MenuStatus),
                        SequenceNo=Convert.ToInt32(menu.SequenceNo)
                    });
                }

            }
            catch(Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return menuList;
        }

        public ResponseOut RemoveMenu(int menuId)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                responseOut = dbInterface.RemoveMenu(menuId);

            }
            catch(Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return responseOut;
        }
    }
}

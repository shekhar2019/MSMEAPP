using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Portal.Common;
using Portal.Core.ViewModel;
using Portal.DAL;
using System.Reflection;


namespace Portal.Core
{
 public class WebBL
    {
        DBInterface dbInterface;
        public WebBL()
        {
            dbInterface = new DBInterface();

        }
        public string GetAboutUSList()
        {
            string aboutuList = "";
            try
            {
                aboutuList = dbInterface.GetAboutUSList();

            }
            catch (Exception ex)
            {
                  Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return aboutuList;
        }

        public List<AboutUsViewModel> GetAboutList()
        {
            List<AboutUsViewModel> aboutUsList = new List<AboutUsViewModel>();
            try
            {
                List<Aboutu> aboutList = dbInterface.GetAboutList();
                foreach(var items in aboutList)
                {
                    aboutUsList.Add(new AboutUsViewModel {

                        AboutusTitle=items.AboutusTitle,
                        Description=items.Description,
                        ImageUrl=items.ImageUrl
                    });
                }
                

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return aboutUsList;
        }
        // Home Slider Show In Index Page

        public List<HomeSliderViewModel> GetHomeSliderList()
        {
            List<HomeSliderViewModel> homeSliderList = new List<HomeSliderViewModel>();
            try
            {
                List<HomeSlider> homeSliders = dbInterface.GetHomeSlidersList();
                if (homeSliders != null && homeSliders.Count > 0)
                {
                    foreach (HomeSlider homeSlider in homeSliders)
                    {
                        homeSliderList.Add(new HomeSliderViewModel
                        { Sliderid = homeSlider.Sliderid, SliderTitle = homeSlider.SliderTitle, Image = homeSlider.Image, Description = homeSlider.Description,HomeSiderStatus=Convert.ToBoolean(homeSlider.HomeSiderStatus) });
                    }
                }
            }

            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return homeSliderList;
        }

       
        public List<ProductViewModel> GetProdcutImagesList(string productName)
        {
            List<ProductViewModel> productList = new List<ProductViewModel>();
            try
            {
                List<Product> prodcuts = dbInterface.GetProdcutImagesList(productName);
                if (prodcuts != null && prodcuts.Count > 0)
                {
                    foreach (Product product in prodcuts)
                    {
                        productList.Add(new ProductViewModel
                        {
                            Productid = product.Productid,
                            ProductName = product.ProductName,
                            ProductFullDesc = product.ProductFullDesc,
                            
                        });
                    }
                }
            }

            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return productList;
        }
        public List<ProductImageDetailViewModel> GetProdcutImagesList1(string productName)
        {
            List<ProductImageDetailViewModel> productList = new List<ProductImageDetailViewModel>();
            try
            {
                List<ProductImageDetail> prodcuts = dbInterface.GetProdcutImagesList1(productName);
                if (prodcuts != null && prodcuts.Count > 0)
                {
                    foreach (ProductImageDetail product in prodcuts)
                    {
                        productList.Add(new ProductImageDetailViewModel
                        {
                            ProductId=Convert.ToInt32(product.ProductId),
                            ImageTitle=product.ImageTitle,
                            ImageUrl=product.ImageUrl,
                            ImageNavigateUrl=product.ImageNavigateUrl,
                            ImageAlt=product.ImageAlt,
                            ImageSequence= Convert.ToInt32(product.ImageSequence)
                        });
                    }
                }
            }

            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return productList;
        }

        public List<AboutUsViewModel> GetOurMissionList()
        {
            List<AboutUsViewModel> aboutUsList = new List<AboutUsViewModel>();
            try
            {
                List<Aboutu> aboutList = dbInterface.GetOurMissionList();
                foreach (var items in aboutList)
                {
                    aboutUsList.Add(new AboutUsViewModel
                    {

                        AboutusTitle = items.AboutusTitle,
                        Description = items.Description,
                        ImageUrl = items.ImageUrl
                    });
                }


            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return aboutUsList;
        }

        public List<MenuViewModel> GetMenuList()
        {
            List<MenuViewModel> menuList = new List<MenuViewModel>();
            try
            {
                List<proc_GetHomeParentMenu_Result1> parentMenuList = new List<proc_GetHomeParentMenu_Result1>();
                parentMenuList = dbInterface.GetHomeParentMenu();
                List<SubMenuViewModel> subMenus;
                List<SubChildMenuViewModel> subChildMenu;
                foreach (proc_GetHomeParentMenu_Result1 parentMenu in parentMenuList)
                {
                    MenuViewModel menuViewModel = new MenuViewModel();
                    menuViewModel.MenuId = parentMenu.MenuId;
                    menuViewModel.MenuName = parentMenu.MenuName;
                    menuViewModel.MenuLink = parentMenu.MenuLink;
                    List<proc_GetHomeSubMenu_Result> subMenuList = dbInterface.GetHomeSubMenu(parentMenu.MenuId);
                    if(subMenuList!=null)
                    {
                        subMenus = new List<SubMenuViewModel>();
                        foreach (proc_GetHomeSubMenu_Result subMenuItem in subMenuList)
                        {
                            SubMenuViewModel subMenuvm = new SubMenuViewModel();
                            subMenuvm.SubMenuId = subMenuItem.SubMenuId;
                            subMenuvm.SubMenuName = subMenuItem.SubMenuName;
                            subMenuvm.SubMenuLink = subMenuItem.SubMenuLink;
                            subMenuvm.PageName = subMenuItem.PageName;
                            List<proc_GetHomeSubChildMenu_Result> subChildMenuList = dbInterface.GetHomeSubChildMenu(subMenuItem.SubMenuId);
                            if(subChildMenuList!=null)
                            {
                                subChildMenu = new List<SubChildMenuViewModel>();
                                foreach (proc_GetHomeSubChildMenu_Result subChildMenuItem in subChildMenuList)
                                {
                                    subChildMenu.Add(new SubChildMenuViewModel { SubChildMenuName= subChildMenuItem .SubChildMenuName,SubChildMenuLink= subChildMenuItem .SubChildMenuLink});
                                }
                            }
                            else
                            {
                                subChildMenu = new List<SubChildMenuViewModel>();
                            }

                            subMenus.Add(
                                new SubMenuViewModel {
                                    SubMenuId = subMenuItem.SubMenuId,
                                    SubMenuName = subMenuItem.SubMenuName,
                                    SubMenuLink = subMenuItem.SubMenuLink,
                                    PageName = subMenuItem.PageName,
                                    subChildMenuList = subChildMenu
                                });
                        }

                    }
                    else
                    {
                        subMenus = new List<SubMenuViewModel>();
                    }
                    menuViewModel.subMenuList = subMenus;
                    menuList.Add(menuViewModel);
                }
                


            }
            catch(Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return menuList;
        }
    }
}

using System;
using Portal.DAL;
using Portal.Common;
using System.Reflection;
using Portal.Core.ViewModel;
using System.Collections.Generic;
using System.Data;

namespace Portal.Core
{
  public  class HomeSliderBL
    {
        DBInterface dbInterface;
        public HomeSliderBL()
        {
            dbInterface = new DBInterface();
        }
        public ResponseOut AddEditHomeSlider(HomeSliderViewModel homeSliderVM)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                HomeSlider homeSlider = new HomeSlider
                {
                    Sliderid = homeSliderVM.Sliderid,
                    SliderTitle=homeSliderVM.SliderTitle,
                    Image=homeSliderVM.Image,
                    ImageAlt= homeSliderVM.ImageAlt,
                    ImageTitle = homeSliderVM.ImageTitle,
                    Description= homeSliderVM.Description,
                    LinkUrl = homeSliderVM.LinkUrl,
                    CreatedBy=homeSliderVM.CreatedBy,
                    HomeSiderStatus= homeSliderVM.HomeSiderStatus
                };
                
                responseOut = dbInterface.AddEditHomeSlider(homeSlider);

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

        public ResponseOut UpdateHomeSliderPic(HomeSliderViewModel homeSliderViewModel)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                HomeSlider homeSlider = new HomeSlider {
                    Sliderid=homeSliderViewModel.Sliderid,
                    Image=homeSliderViewModel.Image
                };
                

                responseOut = dbInterface.UpdateHomeSliderPic(homeSlider);
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

        public List<HomeSliderViewModel> GetHomeSliderList()
        {
            List<HomeSliderViewModel> homeSliderlist = new List<HomeSliderViewModel>();
            try
            {
                List<HomeSlider> homeSliders = dbInterface.GetHomeSliderList();

                if (homeSliders != null && homeSliders.Count > 0)
                {
                    foreach (var item in homeSliders)
                    {
                        homeSliderlist.Add(new HomeSliderViewModel
                        {
                            Sliderid=item.Sliderid,
                            SliderTitle=item.SliderTitle,
                            Image = item.Image,
                            ImageAlt = item.ImageAlt,
                            ImageTitle = item.ImageTitle,
                            Description = item.Description,
                            LinkUrl = item.LinkUrl,
                            HomeSiderStatus =Convert.ToBoolean(item.HomeSiderStatus),
                            CreatedByUser= item.UserName
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return homeSliderlist;
        }

        public ResponseOut RemoveSliderHome(int sliderid)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                responseOut = dbInterface.RemoveHomeSlider(sliderid);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return responseOut;
        }
    }
}

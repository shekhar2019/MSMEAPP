using Portal.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace Portal.DAL
{
    public partial class HomeSlider
    {
        public string UserName { get; set; }
    }
    public partial class DBInterface : IDisposable
    {
        public ResponseOut AddEditHomeSlider(HomeSlider homeSlider)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                if (entities.HomeSliders.Any(x => x.SliderTitle == homeSlider.SliderTitle && homeSlider.Sliderid==0))
                {
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.DuplicateWelcomeHome;
                }
                else
                {
                    if (homeSlider.Sliderid == 0)
                    {
                        homeSlider.CreateDate = DateTime.Now;
                        entities.HomeSliders.Add(homeSlider);
                        responseOut.message = ActionMessage.HomeSliderCreatedSuccess;
                    }
                    else
                    {
                        homeSlider.ModifyBy = homeSlider.ModifyBy;
                        homeSlider.ModifyDate = homeSlider.ModifyDate;
                        entities.HomeSliders.Where(x => x.Sliderid == homeSlider.Sliderid).ToList().ForEach(x =>
                        {
                            x.Sliderid = homeSlider.Sliderid;
                            x.SliderTitle = homeSlider.SliderTitle;
                            x.Image = homeSlider.Image;
                            x.ImageAlt = homeSlider.ImageAlt;
                            x.ImageTitle = homeSlider.ImageTitle;
                            x.Description = homeSlider.Description;
                            x.LinkUrl = homeSlider.LinkUrl;
                            x.ModifyBy = homeSlider.ModifyBy;
                            x.ModifyDate = homeSlider.ModifyDate;
                            x.HomeSiderStatus = homeSlider.HomeSiderStatus;
                        });
                        responseOut.message = ActionMessage.HomeSliderUpdatedSuccess;
                    }

                    entities.SaveChanges();
                    responseOut.trnId = homeSlider.Sliderid;
                    responseOut.status = ActionStatus.Success;
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                var errorList = new List<string>();

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        errorList.Add(String.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage));
                    }
                }

            }
            return responseOut;
        }
        public ResponseOut UpdateHomeSliderPic(HomeSlider homeSlider)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                entities.HomeSliders.Where(x => x.Sliderid == homeSlider.Sliderid).ToList().ForEach(x => {
                    x.Image = homeSlider.Image;
                });
                entities.SaveChanges();
                responseOut.status = ActionStatus.Success;
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
        public List<HomeSlider> GetHomeSliderList()
        {
            List<HomeSlider> homeSliderList = new List<HomeSlider>();
            try
            {
                var homeSlider = entities.HomeSliders.Join(entities.Users,
                    hs => hs.CreatedBy,
                    u => u.UserId, (hs, u) => new
                    {
                        Sliderid=hs.Sliderid,
                        SliderTitle=hs.SliderTitle,
                        Image=hs.Image,
                        ImageAlt=hs.ImageAlt,
                        ImageTitle=hs.ImageTitle,
                        Description=hs.Description,
                        LinkUrl=hs.LinkUrl,
                        HomeSiderStatus=hs.HomeSiderStatus,
                        UserName=u.UserName
                    }).ToList();
                
                if (homeSlider != null && homeSlider.Count > 0)
                {
                    foreach (var item in homeSlider)
                    {
                        homeSliderList.Add(
                            new HomeSlider {
                                Sliderid = item.Sliderid,
                                SliderTitle = item.SliderTitle,
                                Image=item.Image,
                                ImageAlt=item.ImageAlt,
                                ImageTitle=item.ImageTitle,
                                Description=item.Description,
                                LinkUrl=item.LinkUrl,
                                HomeSiderStatus=item.HomeSiderStatus,
                                UserName=item.UserName
                            });
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

        public ResponseOut RemoveHomeSlider(int sliderid)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                var homeSlider = entities.HomeSliders.FirstOrDefault(x => x.Sliderid == sliderid);
                entities.HomeSliders.Remove(homeSlider);
                entities.SaveChanges();
                responseOut.status = ActionStatus.Success;
                responseOut.message = ActionMessage.RemoveHomeSlider;
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



    }
}

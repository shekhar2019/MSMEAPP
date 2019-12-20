using System;
using Portal.DAL;
using Portal.Common;
using System.Reflection;
using Portal.Core.ViewModel;
using System.Collections.Generic;
using System.Data;

namespace Portal.Core
{
    public class HomeAboutBL
    {
        DBInterface dbInterface;
        public HomeAboutBL()
        {
            dbInterface = new DBInterface();
        }
        public ResponseOut AddEditHomeAbout(HomeAboutViewModel homeAboutViewModel)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                HomeAbout homeAbout = new HomeAbout
                {
                    Id = homeAboutViewModel.Id,
                    Title = homeAboutViewModel.Title,
                    Description = homeAboutViewModel.Description,
                    CreatedBy = homeAboutViewModel.CreatedBy,
                    HomeAboutStatus = homeAboutViewModel.HomeAboutStatus
                };
                responseOut = dbInterface.AddEditHomeAbout(homeAbout);

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

  


        public List<HomeAboutViewModel> GetHomeAboutList()
        {
            List<HomeAboutViewModel> homeAboutlist = new List<HomeAboutViewModel>();
            try
            {
                List<HomeAbout> homeAbout = dbInterface.GetHomeAboutList();

                if (homeAbout != null && homeAbout.Count > 0)
                {
                    foreach (var item in homeAbout)
                    {
                        homeAboutlist.Add(new HomeAboutViewModel
                        {
                            Id = item.Id,
                            Description = item.Description,
                            Title = item.Title,
                            HomeAboutStatus = Convert.ToBoolean(item.HomeAboutStatus),
                            CreatedByUserName = item.UserName
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return homeAboutlist;
        }

     
    }
}

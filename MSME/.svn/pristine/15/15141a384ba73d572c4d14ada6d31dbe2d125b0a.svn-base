using System;
using Portal.DAL;
using Portal.Common;
using System.Reflection;
using Portal.Core.ViewModel;
using System.Collections.Generic;
using System.Data;

namespace Portal.Core
{
    public class WelcomeHomeBL
    {
        DBInterface dbInterface;
        public WelcomeHomeBL()
        {
            dbInterface = new DBInterface();
        }
        public ResponseOut AddEditWelcomeHome(WelcomeHomeViewModel welcomeHomevm)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                WelcomeHome welcomeHome = new WelcomeHome
                {
                    WelcomeHomeId = welcomeHomevm.WelcomeHomeId,
                    Title = welcomeHomevm.Title,
                    WelcomeContent = welcomeHomevm.WelcomeContent,
                    CreatedBy= welcomeHomevm.CreatedBy,
                    WelcomeHomeStatus= welcomeHomevm.WelcomeHomeStatus
                };
               
                responseOut = dbInterface.AddEditWelcomeHome(welcomeHome);
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

        public List<WelcomeHomeViewModel> GetWelcomeHomeList()
        {
            List<WelcomeHomeViewModel> WelcomeHomeVM = new List<WelcomeHomeViewModel>();
            try
            {
                List<WelcomeHome> welcomeHomes = dbInterface.GetWelcomeHomeList();
                
                if (welcomeHomes != null && welcomeHomes.Count > 0)
                {
                    foreach (var item in welcomeHomes)
                    {
                        WelcomeHomeVM.Add(new WelcomeHomeViewModel
                        {
                            WelcomeHomeId=item.WelcomeHomeId,
                            Title=item.Title,
                            WelcomeContent= item.WelcomeContent,
                            WelcomeHomeStatus=Convert.ToBoolean(item.WelcomeHomeStatus),
                            CreatedByName=item.UserName
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return WelcomeHomeVM;
        }

        public ResponseOut UpdateLogoPic(LogoViewModel logoViewModel)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                Logo logo = new Logo
                {
                    LogoId= logoViewModel.LogoId,
                    LogoPath=logoViewModel.LogoPath
                };
                responseOut= dbInterface.UpdateLogoPic(logo);
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

        public ResponseOut RemoveWelcomeHome(int welcomeId)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                responseOut = dbInterface.RemoveWelcomeHome(welcomeId);
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

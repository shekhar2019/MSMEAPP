using System;
using Portal.DAL;
using Portal.Common;
using System.Reflection;
using Portal.Core.ViewModel;
using System.Collections.Generic;
using System.Data;

namespace Portal.Core
{
    public class LogoBL
    {
        DBInterface dbInterface;
        public LogoBL()
        {
            dbInterface = new DBInterface();
        }
        public ResponseOut AddEditLogo(LogoViewModel logoViewModel)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                Logo logo = new Logo {
                    LogoId=logoViewModel.LogoId,
                    Title = logoViewModel.Title,
                    LogoPath = logoViewModel.LogoPath,
                    Link= logoViewModel.Link,
                    CreatedBy=logoViewModel.CreatedBy,
                    LogoStatus=logoViewModel.LogoStatus
                };
                responseOut = dbInterface.AddEditLogo(logo);
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

        public List<LogoViewModel> GetLogoList()
        {
            List<LogoViewModel> logoes = new List<LogoViewModel>();
            try
            {
                List<Logo> listLogoes = dbInterface.GetLogoList();
                
                if (listLogoes != null && listLogoes.Count > 0)
                {
                    foreach (var item in listLogoes)
                    {
                        logoes.Add(new LogoViewModel
                        {
                            LogoId=item.LogoId,
                            Title=item.Title,
                            LogoPath= item.LogoPath,
                            Link=item.Link,
                            LogoStatus = Convert.ToBoolean(item.LogoStatus),
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
            return logoes;
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

        public ResponseOut RemoveLogo(int logoId)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                responseOut = dbInterface.RemoveLogo(logoId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return responseOut;
        }

        public LogoViewModel GetLogo()
        {
            LogoViewModel logoViewModel = new LogoViewModel();
            Logo logo = new Logo();
            try
            {
                logo = dbInterface.GetLogo();
                logoViewModel = new LogoViewModel
                {
                    LogoId=logo.LogoId,
                    Title=logo.Title,
                    LogoPath =logo.LogoPath,
                };
            }
            catch(Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return logoViewModel;
        }
    }
}

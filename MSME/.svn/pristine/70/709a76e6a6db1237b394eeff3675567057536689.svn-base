using System;
using Portal.DAL;
using Portal.Common;
using System.Reflection;
using Portal.Core.ViewModel;
using System.Collections.Generic;
using System.Data;

namespace Portal.Core
{
    public class ButtonTitleBL
    {
        DBInterface dbInterface;
        public ButtonTitleBL()
        {
            dbInterface = new DBInterface();
        }
        public ResponseOut AddEditButtonTitle(ButtonTitleViewModel buttonTitleViewModel)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                ButtonTitle buttonTitle = new ButtonTitle
                {
                    Id = buttonTitleViewModel.Id,
                    Title = buttonTitleViewModel.Title,
                    CreatedBy = buttonTitleViewModel.CreatedBy,
                    ButtonTitleStatus = buttonTitleViewModel.ButtonTitleStatus
                };
                responseOut = dbInterface.AddEditButtonTitle(buttonTitle);

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

  


        public List<ButtonTitleViewModel> GetButtonTitleList()
        {
            List<ButtonTitleViewModel> buttonTitlelist = new List<ButtonTitleViewModel>();
            try
            {
                List<ButtonTitle> buttonTitle = dbInterface.GetButtonTitleList();

                if (buttonTitle != null && buttonTitle.Count > 0)
                {
                    foreach (var item in buttonTitle)
                    {
                        buttonTitlelist.Add(new ButtonTitleViewModel
                        {
                            Id = item.Id,
                            Title = item.Title,
                            ButtonTitleStatus = Convert.ToBoolean(item.ButtonTitleStatus),
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
            return buttonTitlelist;
        }

     
    }
}

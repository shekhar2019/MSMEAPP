using System;
using Portal.DAL;
using Portal.Common;
using System.Reflection;
using Portal.Core.ViewModel;
using System.Collections.Generic;
using System.Data;

namespace Portal.Core
{
    public class CopyRightBL
    {
        DBInterface dbInterface;
        public CopyRightBL()
        {
            dbInterface = new DBInterface();
        }
        public ResponseOut AddEditCopyRight(CopyRightViewModel copyRightViewModel)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                Copyright copyright = new Copyright
                {
                    Id = copyRightViewModel.Id,                  
                    Description = copyRightViewModel.Description,
                    CreatedBy = copyRightViewModel.CreatedBy,
                    CopyrightStatus = copyRightViewModel.CopyrightStatus
                };
                responseOut = dbInterface.AddEditCopyRight(copyright);

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

  


        public List<CopyRightViewModel> GetCopyRighList()
        {
            List<CopyRightViewModel> copyRightlist = new List<CopyRightViewModel>();
            try
            {
                List<Copyright> copyright = dbInterface.GetCopyRightList();

                if (copyright != null && copyright.Count > 0)
                {
                    foreach (var item in copyright)
                    {
                        copyRightlist.Add(new CopyRightViewModel
                        {
                            Id = item.Id,
                            Description = item.Description,
                            CopyrightStatus = Convert.ToBoolean(item.CopyrightStatus),
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
            return copyRightlist;
        }

     
    }
}

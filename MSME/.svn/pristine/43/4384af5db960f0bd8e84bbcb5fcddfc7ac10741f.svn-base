using System;
using Portal.DAL;
using Portal.Common;
using System.Reflection;
using Portal.Core.ViewModel;
using System.Collections.Generic;
using System.Data;

namespace Portal.Core
{
    public class MSMEBL
    {
        DBInterface dbInterface;
        public MSMEBL()
        {
            dbInterface = new DBInterface();
        }
        public ResponseOut AddEditMSME(MSMEViewModel mSMEViewModel)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                MSME msme = new MSME
                {
                    MSMEId= mSMEViewModel.MSMEId,
                    MSMETitle=mSMEViewModel.MSMETitle,
                    MSMEDescription=mSMEViewModel.MSMEDescription,
                    MSMEContentArea=mSMEViewModel.MSMEContentArea,
                    MSMEImageUrl=mSMEViewModel.MSMEImageUrl,
                    MSMEStatus=mSMEViewModel.MSMEStatus
                };
               
                responseOut = dbInterface.AddEditMSME(msme);

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

        public ResponseOut UpdateMSMEPic(MSMEViewModel msmeViewModel)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                MSME msme = new MSME
                {
                    MSMEId = msmeViewModel.MSMEId,
                    MSMEImageUrl = msmeViewModel.MSMEImageUrl
                };
              
                responseOut = dbInterface.UpdateMSMEPic(msme);
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

        public List<MSMEViewModel> GetMSMEList()
        {
            List<MSMEViewModel> msmelist = new List<MSMEViewModel>();
            try
            {
                List<MSME> msmes = dbInterface.GetMSMEList();

                if (msmes != null && msmes.Count > 0)
                {
                    foreach (var item in msmes)
                    {
                        msmelist.Add(new MSMEViewModel
                        {
                            MSMEId=item.MSMEId,
                            MSMETitle=item.MSMETitle,
                            MSMEDescription=item.MSMEDescription,
                            MSMEImageUrl=item.MSMEImageUrl,
                            MSMEContentArea=item.MSMEContentArea,
                            MSMEStatus=Convert.ToBoolean(item.MSMEStatus)
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return msmelist;
        }
        public ResponseOut RemoveMSME(int msmeId)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                responseOut = dbInterface.RemoveMSME(msmeId);
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

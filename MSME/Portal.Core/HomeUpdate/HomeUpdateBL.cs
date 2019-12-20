using System;
using Portal.DAL;
using Portal.Common;
using System.Reflection;
using Portal.Core.ViewModel;
using System.Collections.Generic;
using System.Data;


namespace Portal.Core
{
 public class HomeUpdateBL
    {
        DBInterface dbInterface;
        public HomeUpdateBL()
        {
            dbInterface = new DBInterface();
        }

        public ResponseOut AddEditHomeUpdate(HomeUpdateViewModel homeUpdateViewModel)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                HomeUpdate homeUpdate = new HomeUpdate
                {
                    UpdateId= homeUpdateViewModel.UpdateId,
                    UpdateTitle=homeUpdateViewModel.UpdateTitle,
                    UpdateDate=Convert.ToDateTime(homeUpdateViewModel.UpdateDate),
                    UpdateDec=homeUpdateViewModel.UpdateDec,
                    UpdateUrl=homeUpdateViewModel.UpdateUrl,
                    ImageUrl=homeUpdateViewModel.ImageUrl,
                    ImageTitile=homeUpdateViewModel.ImageTitile,
                    ImageNavigateUrl=homeUpdateViewModel.ImageNavigateUrl,
                    ImageAlt=homeUpdateViewModel.ImageAlt,
                    UpdateStatus=homeUpdateViewModel.UpdateStatus,
                    CreatedBy=homeUpdateViewModel.CreatedBy,
                    UpdateArea=homeUpdateViewModel.UpdateArea
                   
                };
                responseOut = dbInterface.AddEditHomeUpdate(homeUpdate);
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
        public ResponseOut UpdateImage(HomeUpdateViewModel homeUpdateViewModel)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                HomeUpdate homeUpdate = new HomeUpdate
                {
                    UpdateId=homeUpdateViewModel.UpdateId,
                    ImageUrl=homeUpdateViewModel.ImageUrl
                };
               
                responseOut = dbInterface.UpdateImage(homeUpdate);
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

        public List<HomeUpdateViewModel> GetHomeUpdateList(string homeUpdatesArea)
        {
            SQLDbInterface sQLDbInterface = new SQLDbInterface();
            List<HomeUpdateViewModel> homeUpdates = new List<HomeUpdateViewModel>();
            try
            {
                DataTable dtHomeUpdates = sQLDbInterface.GetHomeUpdateList(homeUpdatesArea);
                foreach(DataRow dr in dtHomeUpdates.Rows)
                {
                    homeUpdates.Add(new HomeUpdateViewModel {
                        UpdateId=Convert.ToInt32(dr["UpdateId"]),
                        UpdateTitle=Convert.ToString(dr["UpdateTitle"]),
                        UpdateDate = Convert.ToString(dr["UpdateDate"]),
                        UpdateDec = Convert.ToString(dr["UpdateDec"]),
                        UpdateUrl = Convert.ToString(dr["UpdateUrl"]),
                        ImageUrl = Convert.ToString(dr["ImageUrl"]),
                        ImageTitile = Convert.ToString(dr["ImageTitile"]),
                        ImageNavigateUrl = Convert.ToString(dr["ImageNavigateUrl"]),
                        ImageAlt = Convert.ToString(dr["ImageAlt"]),
                        UpdateStatus = Convert.ToBoolean(Convert.ToString(dr["UpdateStatus"])),
                        CreatedByUserName=Convert.ToString(dr["CreatedByUser"]),
                        UpdateArea=Convert.ToString(dr["UpdateArea"])
                    });
                }

            }
            catch(Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return homeUpdates;
        }

        public ResponseOut RemoveHomeUpdate(int updateId)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                responseOut = dbInterface.RemoveHomeUpdate(updateId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return responseOut;
        }

     
        //public List<HomeUpdateViewModel> GetHomeNewUpdateList()
        //{
        //    SQLDbInterface sQLDbInterface = new SQLDbInterface();
        //    List<HomeUpdateViewModel> homeUpdates = new List<HomeUpdateViewModel>();
        //    try
        //    {
        //        DataTable dtHomeUpdates = sQLDbInterface.GetHomeUpdateList();
        //        foreach (DataRow dr in dtHomeUpdates.Rows)
        //        {
        //            if (dr["UpdateArea"].ToString().Trim().Equals("new"))
        //            {
        //                homeUpdates.Add(new HomeUpdateViewModel
        //                {
        //                    UpdateId = Convert.ToInt32(dr["UpdateId"]),
        //                    UpdateTitle = Convert.ToString(dr["UpdateTitle"]),
        //                    UpdateDate = Convert.ToString(dr["UpdateDate"]),
        //                    UpdateDec = Convert.ToString(dr["UpdateDec"]),
        //                    UpdateUrl = Convert.ToString(dr["UpdateUrl"]),
        //                    ImageUrl = Convert.ToString(dr["ImageUrl"]),
        //                    ImageTitile = Convert.ToString(dr["ImageTitile"]),
        //                    ImageNavigateUrl = Convert.ToString(dr["ImageNavigateUrl"]),
        //                    ImageAlt = Convert.ToString(dr["ImageAlt"]),
        //                    UpdateStatus = Convert.ToBoolean(Convert.ToString(dr["UpdateStatus"])),
        //                    CreatedByUserName = Convert.ToString(dr["CreatedByUser"]),
        //                    UpdateArea = Convert.ToString(dr["UpdateArea"])
        //                });
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
        //        throw ex;
        //    }
        //    return homeUpdates;
        //}

        //public List<HomeUpdateViewModel> GetHomeAssociatesList()
        //{
        //    SQLDbInterface sQLDbInterface = new SQLDbInterface();
        //    List<HomeUpdateViewModel> homeUpdates = new List<HomeUpdateViewModel>();
        //    try
        //    {
        //        DataTable dtHomeUpdates = sQLDbInterface.GetHomeUpdateList();
        //        foreach (DataRow dr in dtHomeUpdates.Rows)
        //        {
        //            if (dr["UpdateArea"].ToString().Trim().Equals("associates"))
        //            {
        //                homeUpdates.Add(new HomeUpdateViewModel
        //                {
        //                    UpdateId = Convert.ToInt32(dr["UpdateId"]),
        //                    UpdateTitle = Convert.ToString(dr["UpdateTitle"]),
        //                    UpdateDate = Convert.ToString(dr["UpdateDate"]),
        //                    UpdateDec = Convert.ToString(dr["UpdateDec"]),
        //                    UpdateUrl = Convert.ToString(dr["UpdateUrl"]),
        //                    ImageUrl = Convert.ToString(dr["ImageUrl"]),
        //                    ImageTitile = Convert.ToString(dr["ImageTitile"]),
        //                    ImageNavigateUrl = Convert.ToString(dr["ImageNavigateUrl"]),
        //                    ImageAlt = Convert.ToString(dr["ImageAlt"]),
        //                    UpdateStatus = Convert.ToBoolean(Convert.ToString(dr["UpdateStatus"])),
        //                    CreatedByUserName = Convert.ToString(dr["CreatedByUser"]),
        //                    UpdateArea = Convert.ToString(dr["UpdateArea"])
        //                });
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
        //        throw ex;
        //    }
        //    return homeUpdates;
        //}
    }
}
 
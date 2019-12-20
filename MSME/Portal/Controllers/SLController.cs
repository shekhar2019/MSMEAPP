﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Portal.Core;
using Portal.Core.ViewModel;
using Portal.Common;
using System.Reflection;

namespace Portal.Controllers
{
    public class SLController :BaseController
    {
        //
        // GET: /SL/2077
        #region SL
        [ValidateRequest(true,UserInterfaceHelper.Add_Edit_SL,(int)AccessMode.ViewAccess,(int)RequestMode.GetPost)]
        public ActionResult AddEditSL(int sLId = 0, int accessMode = 3)
        {

            try
            {
                if (sLId != 0)
                {

                    ViewData["sLId"] = sLId;
                    ViewData["accessMode"] = accessMode;
                }
                else
                {
                    ViewData["sLId"] = 0;
                    ViewData["accessMode"] = 0;
                }

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return View();
        }


        [HttpPost]
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_SL, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult AddEditSL(SLViewModel sLViewModel)
        {
            ResponseOut responseOut = new ResponseOut();
            SLBL sLBL = new SLBL();
            try
            {
                if (sLViewModel != null)
                {
                    sLViewModel.CompanyId = ContextUser.CompanyId;
                    sLViewModel.CreatedBy = ContextUser.UserId;
                    responseOut = sLBL.AddEditSL(sLViewModel);
                }
                else
                {
                    responseOut.message = ActionMessage.ProbleminData;
                    responseOut.status = ActionStatus.Fail;
                }

            }
            catch (Exception ex)
            {
                responseOut.message = ActionMessage.ApplicationException;
                responseOut.status = ActionStatus.Fail;
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(responseOut, JsonRequestBehavior.AllowGet);
        }

        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_SL, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult ListSL()
        {
            try
            {

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return View();
        }

        [HttpGet]
        public PartialViewResult GetSLList(string SLCode = "", int SLTypeId = 0, int CostCenterId = 0, string Status = "")
        {
            List<SLViewModel> sL = new List<SLViewModel>();
            SLBL sLBL = new SLBL();
            try
            {
                sL = sLBL.GetSLList(SLCode,SLTypeId,CostCenterId,ContextUser.CompanyId,Status);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(sL);
        }

        [HttpGet]
        public JsonResult GetSLDetail(int sLId)
        {
            SLBL sLBL = new SLBL();
            SLViewModel sLViewModel = new SLViewModel();
            try
            {
                sLViewModel = sLBL.GetSLDetail(sLId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(sLViewModel, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetCostCenterList()
        {
            SLBL sLBL = new SLBL();
            List<CostCenterViewModel> costCenterList = new List<CostCenterViewModel>();
            try
            {
                costCenterList = sLBL.GetCostCenterList();
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(costCenterList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetSLTypeList()
        {
            SLBL sLBL = new SLBL();
            List<SLTypeViewModel> slTypeList = new List<SLTypeViewModel>();
            try
            {
                slTypeList = sLBL.GetSLTypeList();
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(slTypeList, JsonRequestBehavior.AllowGet);
        }

        #endregion

    }
}

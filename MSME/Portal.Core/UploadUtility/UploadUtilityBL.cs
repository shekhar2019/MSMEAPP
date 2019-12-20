using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Portal.Core.ViewModel;
using Portal.DAL;
using Portal.Common;
using System.Reflection;
using System.Data;

namespace Portal.Core
{
    public class UploadUtilityBL
    {
        DBInterface dbInterface;
        public UploadUtilityBL()
        {
            dbInterface = new DBInterface();
        }
        #region Import Lead


        #endregion

        #region Common Method
        public int GetIdByStateName(string stateName)
        {
            int stateId = 0;
            try
            {

                stateId = dbInterface.GetIdByStateName(stateName);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return stateId;
        }
        public int GetIdByCountryName(string countryName)
        {
            int countryId = 0;
            try
            {
                countryId = dbInterface.GetIdByCountryName(countryName);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return countryId;
        }
        public int GetIdByLeadSourceName(string leadSourceName)
        {
            int leadSourceId = 0;
            try
            {
                leadSourceId = dbInterface.GetIdByLeadSourceName(leadSourceName);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return leadSourceId;
        }
        public int GetIdByLeadStatusName(string leadStatusName)
        {
            int leadStatusId = 0;
            try
            {
                leadStatusId = dbInterface.GetIdByLeadStatusName(leadStatusName);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return leadStatusId;
        }
        public int GetIdByFollowUpActivityName(string followUpActivityTypeName)
        {
            int followUpActivityTypeId = 0;
            try
            {
                followUpActivityTypeId = dbInterface.GetIdByFollowUpActivityTypeName(followUpActivityTypeName);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return followUpActivityTypeId;
        }
        public int GetIdByPriorityName(string priorityName)
        {
            int priorityId = 0;
            try
            {
                switch (priorityName.Trim().ToUpper())
                {
                    case "URGENT":
                        priorityId = 1;
                        break;
                    case "HIGH":
                        priorityId = 2;
                        break;
                    case "MEDIUM":
                        priorityId = 3;
                        break;
                    case "LOW":
                        priorityId = 4;
                        break;
                    default:
                        priorityId = 4;
                        break;

                }

                
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return priorityId;
        }
        public int GetIdByGlMainGroupName(string glMainGroupName)
        {
            int glMainGroupId = 0;
            try
            {

                glMainGroupId = dbInterface.GetIdByGLMainGroupName(glMainGroupName);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return glMainGroupId;
        }
        public int GetIdByScheduleName(string scheduleName)
        {
            int scheduleID = 0;
            try
            {

                scheduleID = dbInterface.GetIdByScheduleName(scheduleName);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return scheduleID;
        }

        public int GetIdBySLTypeName(string sLTypeName)
        {
            int sLTypeId = 0;
            try
            {

                sLTypeId = dbInterface.GetIdBySLTypeName(sLTypeName);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return sLTypeId;
        }
        public int GetIdByCostCenterName(string costCenterName)
        {
            int costCenterId = 0;
            try
            {

                costCenterId = dbInterface.GetIdByCostCenterName(costCenterName);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return costCenterId;
        }
        public int GetIdBySubCostCenterName(string subCostCenterName)
        {
            int subCostCenterId = 0;
            try
            {

                subCostCenterId = dbInterface.GetIdBySubCostCenterName(subCostCenterName);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return subCostCenterId;
        }
        public int GetIdByGLHead(string gLHead)
        {
            int postingGLId = 0;
            try
            {

                postingGLId = dbInterface.GetIdByGLHead(gLHead);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return postingGLId;
        }

        public int GetIdByEmployeeName(string employeeName)
        {
            int employeeId = 0;
            try
            {

                employeeId = dbInterface.GetIdByEmployeeName(employeeName);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return employeeId;
        }
        public int GetIdByCustomerTypeDesc(string customerTypeDesc)
        {
            int customerTypeId = 0;
            try
            {

                customerTypeId = dbInterface.GetIdByCustomerTypeDesc(customerTypeDesc);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return customerTypeId;
        }
        public int GetIdByDepartmentName(string departmentName)
        {
            int departmentId = 0;
            try
            {

                departmentId = dbInterface.GetIdByDepartmentName(departmentName);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return departmentId;
        }
        public int GetIdByDesignationName(string designationName)
        {
            int designationId = 0;
            try
            {

                designationId = dbInterface.GetIdByDesignationName(designationName);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return designationId;
        }

        public int GetIdByProductMainGroupName(string mainGroupName)
        {
            int productMainGroupID = 0;
            try
            {

                productMainGroupID = dbInterface.GetIdByProductMainGroupName(mainGroupName);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return productMainGroupID;
        }
        public int GetIdByProductSubGroupName(string mainGroupName)
        {
            int productSubGroupID = 0;
            try
            {

                productSubGroupID = dbInterface.GetIdByProductSubGroupName(mainGroupName);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return productSubGroupID;
        }
        public int GetIdByProductTypeName(string productTypeName)
        {
            int productTypeId = 0;
            try
            {

                productTypeId = dbInterface.GetIdByProductTypeName(productTypeName);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return productTypeId;
        }
        public int GetIdByUOMName(string uOMName)
        {
            int UOMId = 0;
            try
            {

                UOMId = dbInterface.GetIdByUOMName(uOMName);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return UOMId;
        }

        public int GetIdByCustomerName(string customerName)
        {
            int customerId = 0;
            try
            {

                customerId = dbInterface.GetIdByCustomerName(customerName);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return customerId;
        }

            
        #endregion




    }
}

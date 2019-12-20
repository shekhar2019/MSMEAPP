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
    public class GLSubGroupBL
    {
        DBInterface dbInterface;
        public GLSubGroupBL()
        {
            dbInterface = new DBInterface();
        }


        public ResponseOut AddEditGLSubGroup(GLSubGroupViewModel glsubgroupViewModel)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                GLSubGroup glsubgroup = new GLSubGroup
                {
                   GLSubGroupId = glsubgroupViewModel.GLSubGroupId,
                   GLSubGroupName = glsubgroupViewModel.GLSubGroupName,
                    CompanyId = glsubgroupViewModel.CompanyId,
                    GLMainGroupId  = glsubgroupViewModel.GLMainGroupId,
                   SequenceNo = glsubgroupViewModel.SequenceNo,
                    CreatedBy = glsubgroupViewModel.CreatedBy,
                    Status = glsubgroupViewModel.GLSubGroup_Status
                };
                responseOut = dbInterface.AddEditGLSubGroup(glsubgroup);
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



        public List<GLSubGroupViewModel> GetGLSubGroupList(string glsubgroupName = "", string glmaingroupId = "", int sequenceNo = 0, int companyId = 0, string status = "")
        {
            List<GLSubGroupViewModel> glsubgroups = new List<GLSubGroupViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtGLSubGroups = sqlDbInterface.GetGLSubGroupList(glsubgroupName, glmaingroupId, sequenceNo, companyId, status);
                if (dtGLSubGroups != null && dtGLSubGroups.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtGLSubGroups.Rows)
                    {
                        glsubgroups.Add(new GLSubGroupViewModel
                        {
                            GLSubGroupId = Convert.ToInt32(dr["GLSubGroupId"]),
                            GLSubGroupName = Convert.ToString(dr["GLSubGroupName"]),
                            GLMainGroupId = Convert.ToInt32(dr["GLMainGroupId"]),
                            GLMainGroupName = Convert.ToString(dr["GLMainGroupName"]),
                            SequenceNo = Convert.ToInt32(dr["SequenceNo"].ToString() == "" ? "0" : dr["SequenceNo"].ToString()),
                            GLSubGroup_Status = Convert.ToBoolean(dr["Status"]),
                            CreatedBy = Convert.ToInt32(dr["CreatedBy"].ToString() == "" ? "0" : dr["CreatedBy"].ToString()),
                            CreatedName = Convert.ToString(dr["CreatedByName"]),
                            CreatedDate = Convert.ToString(dr["CreatedDate"]),
                            ModifiedBy = Convert.ToInt32(dr["ModifiedBy"].ToString() == "" ? "0" : dr["ModifiedBy"].ToString()),
                            ModifiedName = Convert.ToString(dr["ModifiedByName"]),
                            ModifiedDate = Convert.ToString(dr["ModifiedDate"]),
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return glsubgroups;
        }

        public GLSubGroupViewModel GetGLSubGroupDetail(int glsubgroupId = 0)
        {
            GLSubGroupViewModel glsubgroup = new GLSubGroupViewModel();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtGLSubGroups = sqlDbInterface.GetGLSubGroupDetail(glsubgroupId);
                if (dtGLSubGroups != null && dtGLSubGroups.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtGLSubGroups.Rows)
                    {
                        glsubgroup = new GLSubGroupViewModel
                        {
                            GLSubGroupId = Convert.ToInt32(dr["GLSubGroupId"]),
                            GLSubGroupName = Convert.ToString(dr["GLSubGroupName"]),
                            GLMainGroupId = Convert.ToInt32(dr["GLMainGroupId"]),
                            SequenceNo = Convert.ToInt32(dr["SequenceNo"]),
                            GLSubGroup_Status = Convert.ToBoolean(dr["Status"]),
                            CreatedBy = Convert.ToInt32(dr["CreatedBy"].ToString() == "" ? "0" : dr["CreatedBy"].ToString()),
                            CreatedName = Convert.ToString(dr["CreatedByName"]),
                            CreatedDate = Convert.ToString(dr["CreatedDate"]),
                            ModifiedBy = Convert.ToInt32(dr["ModifiedBy"].ToString() == "" ? "0" : dr["ModifiedBy"].ToString()),
                            ModifiedName = Convert.ToString(dr["ModifiedByName"]),
                            ModifiedDate = Convert.ToString(dr["ModifiedDate"]),
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return glsubgroup;
        }




        public List<GLMainGroupViewModel> GetGLMainGroupList()
        {
            List<GLMainGroupViewModel> glMainGroups = new List<GLMainGroupViewModel>();
            try
            {
                List<GLMainGroup> glMainGroupList = dbInterface.GetGLMainGroupList();
                if (glMainGroupList != null && glMainGroupList.Count > 0)
                {
                    foreach (GLMainGroup glMainGroup in glMainGroupList)
                    {
                        glMainGroups.Add(new GLMainGroupViewModel { GLMainGroupId = glMainGroup.GLMainGroupId, GLMainGroupName = glMainGroup.GLMainGroupName });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return glMainGroups;
        }

    }
}

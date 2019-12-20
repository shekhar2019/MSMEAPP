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
   public class ProductSubChildGroupBL
    {
        DBInterface dbInterface;
        public ProductSubChildGroupBL()
        {
            dbInterface = new DBInterface();

        }
        public ResponseOut AddEditProductSubChildGroup(ProductSubChildGroupViewModel productSubChildGroupVM)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                ProductSubChildGroup productSubChildGroup = new ProductSubChildGroup
                {
                    ProductSubChildGroupId = productSubChildGroupVM.ProductSubChildGroupId,
                    ProductSubChildGroupName = productSubChildGroupVM.ProductSubChildGroupName,
                    ProductSubChildGroupCode = productSubChildGroupVM.ProductSubChildGroupCode,
                    ProductMainGroupId = productSubChildGroupVM.ProductMainGroupId,
                    ProductSubGroupId = productSubChildGroupVM.ProductSubGroupId,
                    ProductSubChildGroupDesc= productSubChildGroupVM.ProductSubChildGroupDesc,
                    ProductSubChildGroupImageUrl= productSubChildGroupVM.ProductSubChildGroupImageUrl,
                    Status = productSubChildGroupVM.ProductSubGroupStatus
                };
                responseOut = dbInterface.AddEditProductSubChildGroup(productSubChildGroup);
            }
            catch(Exception ex)
            {

                responseOut.status = ActionStatus.Fail;
                responseOut.message = ActionMessage.ApplicationException;
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return responseOut;
        }

        public List<ProductSubChildGroupViewModel> GetProductSubChildGroupList(string productSubChildGroupName = "", string productSubChildGroupCode = "", string Status = "")
        {
            List<ProductSubChildGroupViewModel> productSubChildGroups = new List<ProductSubChildGroupViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtProductSubChildGroups = sqlDbInterface.GetProductSubChildGroupList(productSubChildGroupName, productSubChildGroupCode, Status);
                if (dtProductSubChildGroups != null && dtProductSubChildGroups.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtProductSubChildGroups.Rows)
                    {
                        productSubChildGroups.Add(new ProductSubChildGroupViewModel
                        {
                            ProductSubChildGroupId = Convert.ToInt32(dr["ProductSubChildGroupId"]),
                            ProductSubChildGroupName = Convert.ToString(dr["ProductSubChildGroupName"]),
                            ProductSubChildGroupCode = Convert.ToString(dr["ProductSubChildGroupCode"]),
                            ProductSubGroupId=Convert.ToInt32(dr["ProductSubGroupId"]),
                            ProductSubGroupName=Convert.ToString(dr["ProductSubGroupName"]),
                            ProductSubChildGroupDesc = Convert.ToString(dr["ProductSubChildGroupDesc"]),
                            ProductSubChildGroupImageUrl = Convert.ToString(dr["ProductSubChildGroupImageUrl"]),
                            ProductSubGroupStatus = Convert.ToBoolean(dr["Status"]),
                            ProductMainGroupName = Convert.ToString(dr["ProductMainGroupName"]),
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return productSubChildGroups;
        }

        public ProductSubChildGroupViewModel ProductSubChildGroupDetail(int productSubChildGroupId = 0)
        {
            ProductSubChildGroupViewModel productMainGroupVM = new ProductSubChildGroupViewModel();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtProductSubGroups = sqlDbInterface.GetProductSubChildGroupDetail(productSubChildGroupId);
                if (dtProductSubGroups != null && dtProductSubGroups.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtProductSubGroups.Rows)
                    {
                        productMainGroupVM = new ProductSubChildGroupViewModel
                        {
                            ProductSubChildGroupId=Convert.ToInt32(dr["ProductSubChildGroupId"]),
                            ProductSubChildGroupName=Convert.ToString(dr["ProductSubChildGroupName"]),
                            ProductSubChildGroupCode=Convert.ToString(dr["ProductSubChildGroupCode"]),
                            ProductMainGroupId=Convert.ToInt32(dr["ProductMainGroupId"]),
                            ProductSubGroupId=Convert.ToInt32(dr["ProductSubGroupId"]),
                            ProductSubChildGroupDesc=Convert.ToString(dr["ProductSubChildGroupDesc"]),
                            ProductSubChildGroupImageUrl=Convert.ToString(dr["ProductSubChildGroupImageUrl"]),
                            ProductSubGroupStatus = Convert.ToBoolean(dr["Status"])
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return productMainGroupVM;
        }

        public List<ProductSubChildGroupViewModel> GetProductSubChildGroupList(int productMainGroupId, int productSubGroupId)
        {
            List<ProductSubChildGroupViewModel> productSubGroups = new List<ProductSubChildGroupViewModel>();
            try
            {
                List<ProductSubChildGroup> productSubChildGroupList = dbInterface.GetProductSubChildGroupList(productMainGroupId, productSubGroupId);
                if (productSubChildGroupList != null && productSubChildGroupList.Count > 0)
                {
                    foreach (ProductSubChildGroup productSubChildGroup in productSubChildGroupList)
                    {
                        productSubGroups.Add(new ProductSubChildGroupViewModel
                        {
                            ProductSubGroupId = Convert.ToInt32(productSubChildGroup.ProductSubChildGroupId),
                            ProductSubGroupName = productSubChildGroup.ProductSubChildGroupName,
                            ProductSubChildGroupCode= productSubChildGroup.ProductSubChildGroupCode,
                            ProductMainGroupId= Convert.ToInt64(productSubChildGroup.ProductMainGroupId),
                            ProductSubChildGroupId=Convert.ToInt64(productSubChildGroup.ProductSubChildGroupId),
                            ProductSubChildGroupDesc=productSubChildGroup.ProductSubChildGroupDesc,
                            ProductSubChildGroupImageUrl=productSubChildGroup.ProductSubChildGroupImageUrl

                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return productSubGroups;
        }

        public ResponseOut UpdateProductSubChildGroupPic(ProductSubChildGroupViewModel productSubChildGroupViewModel)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                ProductSubChildGroup productSubChildGroup = new ProductSubChildGroup
                {
                    ProductSubChildGroupId = productSubChildGroupViewModel.ProductSubGroupId,
                    ProductSubChildGroupImageUrl = productSubChildGroupViewModel.ProductSubChildGroupImageUrl
                };
                responseOut = dbInterface.UpdateProductSubChildGroupPic(productSubChildGroup);
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
    }
}

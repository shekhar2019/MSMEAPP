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
    public class DocumentTypeBL
    {
        DBInterface dbInterface;
        public DocumentTypeBL()
        {
            dbInterface = new DBInterface();
        }

        //public List<CustomerTypeViewModel> GetCustomerTypeList()
        //{
        //    List<CustomerTypeViewModel> customerTypes = new List<CustomerTypeViewModel>();
        //    try
        //    {
        //        List<CustomerType> customerTypeList = dbInterface.GetCustomerTypeList();
        //        if (customerTypeList != null && customerTypeList.Count > 0)
        //        {
        //            foreach (CustomerType customerType in customerTypeList)
        //            {
        //                customerTypes.Add(new CustomerTypeViewModel { CustomerTypeId = customerType.CustomerTypeId, CustomerTypeDesc = customerType.CustomerTypeDesc });
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
        //        throw ex;
        //    }
        //    return customerTypes;
        //}

        public ResponseOut AddEditDocumentType(DocumentTypeViewModel documenttypeViewModel)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                DocumentType documenttype = new DocumentType
                {
                    DocumentTypeId = documenttypeViewModel.DocumentTypeId,
                    DocumentTypeDesc = documenttypeViewModel.DocumentTypeDesc,
                    CompanyId = documenttypeViewModel.CompanyId,
                    Status = documenttypeViewModel.DocumentType_Status,

                };
                responseOut = dbInterface.AddEditDocumentType(documenttype);
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


        public List<DocumentTypeViewModel> GetDocumentTypeList(string documenttypeDesc = "", int companyId = 0, string Status = "")
        {
            List<DocumentTypeViewModel> documenttypes = new List<DocumentTypeViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtDocumentTypes = sqlDbInterface.GetDocumentTypeList(documenttypeDesc, companyId, Status);
                if (dtDocumentTypes != null && dtDocumentTypes.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtDocumentTypes.Rows)
                    {
                        documenttypes.Add(new DocumentTypeViewModel
                        {
                            DocumentTypeId = Convert.ToInt32(dr["DocumentTypeId"]),
                            CompanyId = Convert.ToInt32(dr["CompanyId"]),
                            DocumentTypeDesc = Convert.ToString(dr["DocumentTypeDesc"]),
                            DocumentType_Status = Convert.ToBoolean(dr["Status"]),

                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return documenttypes;
        }

        public DocumentTypeViewModel GetDocumentTypeDetail(int documenttypeId = 0)
        {
            DocumentTypeViewModel documenttype = new DocumentTypeViewModel();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtDocumentTypes = sqlDbInterface.GetDocumentTypeDetail(documenttypeId);
                if (dtDocumentTypes != null && dtDocumentTypes.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtDocumentTypes.Rows)
                    {
                        documenttype = new DocumentTypeViewModel
                        {
                            DocumentTypeId = Convert.ToInt32(dr["DocumentTypeId"]),
                            DocumentTypeDesc = Convert.ToString(dr["DocumentTypeDesc"]),
                            DocumentType_Status = Convert.ToBoolean(dr["Status"]),
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return documenttype;
        }





    }
}

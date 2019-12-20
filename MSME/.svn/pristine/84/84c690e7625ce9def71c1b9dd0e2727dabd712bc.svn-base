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
using System.Transactions;
namespace Portal.Core
{
    public class QuotationBL
    {
        DBInterface dbInterface;
        public QuotationBL()
        {
            dbInterface = new DBInterface();
        }
        public ResponseOut AddEditQuotation(QuotationViewModel quotationViewModel,List<QuotationProductViewModel> quotationProducts, List<QuotationTaxViewModel> quotationTaxes, List<QuotationTermViewModel> quotationTerms)
        {
            ResponseOut responseOut = new ResponseOut();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                Quotation quotation = new Quotation
                {
                    QuotationId = quotationViewModel.QuotationId,
                    QuotationDate = Convert.ToDateTime(quotationViewModel.QuotationDate),
                    CustomerId = quotationViewModel.CustomerId,
                    CustomerName = quotationViewModel.CustomerName,
                    BillingAddress = quotationViewModel.BillingAddress,
                    City = quotationViewModel.City,
                    StateId = quotationViewModel.StateId,
                    CountryId = quotationViewModel.CountryId,
                    PinCode = quotationViewModel.PinCode,
                    CSTNo = quotationViewModel.CSTNo,
                    TINNo = quotationViewModel.TINNo,
                    PANNo = quotationViewModel.PANNo,
                    GSTNo = quotationViewModel.GSTNo,
                    ExciseNo = quotationViewModel.ExciseNo,
                    RefNo = quotationViewModel.RefNo,
                    RefDate = string.IsNullOrEmpty(quotationViewModel.RefDate) ? Convert.ToDateTime("01-01-1900") : Convert.ToDateTime(quotationViewModel.RefDate),
                    BasicValue = quotationViewModel.BasicValue,
                    TotalValue = quotationViewModel.TotalValue,
                    FinYearId = quotationViewModel.FinYearId,
                    CompanyId = quotationViewModel.CompanyId,
                    CreatedBy = quotationViewModel.CreatedBy,
                    ApprovalStatus= quotationViewModel.ApprovalStatus


                };
                List<QuotationProductDetail> quotationProductList = new List<QuotationProductDetail>();
                if(quotationProducts!=null && quotationProducts.Count>0)
                {
                    foreach(QuotationProductViewModel item in quotationProducts )
                    {
                        quotationProductList.Add(new QuotationProductDetail
                        {
                            ProductId=item.ProductId,
                            ProductShortDesc=item.ProductShortDesc,
                            Price=item.Price,
                            Quantity=item.Quantity,
                            DiscountPercentage=0,
                            DiscountAmount=0,
                            TaxPercentage=0,
                            TaxAmount=0,
                            TotalPrice =item.TotalPrice
                        });
                    }
                }

                List<QuotationTaxDetail> quotationTaxList = new List<QuotationTaxDetail>();
                if (quotationTaxes != null && quotationTaxes.Count > 0)
                {
                    foreach (QuotationTaxViewModel item in quotationTaxes)
                    {
                        quotationTaxList.Add(new QuotationTaxDetail
                        {
                            TaxId = item.TaxId,
                            TaxName = item.TaxName,
                            TaxPercentage = item.TaxPercentage,
                            TaxAmount = item.TaxAmount
                        });
                    }
                }
                List<QuotationTermsDetail> quotationTermList = new List<QuotationTermsDetail>();
                if (quotationTerms != null && quotationTerms.Count > 0)
                {
                    foreach (QuotationTermViewModel item in quotationTerms)
                    {
                        quotationTermList.Add(new QuotationTermsDetail
                        {
                            TermDesc = item.TermDesc,
                            TermSequence = item.TermSequence
                        });
                    }
                }

                responseOut = sqlDbInterface.AddEditQuotation(quotation, quotationProductList, quotationTaxList, quotationTermList);
             

             
             
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
        public ResponseOut RemoveCustomerBranch(long customerBranchId)
        {
            
            ResponseOut responseOut = new ResponseOut();
            try
                {
                    responseOut = dbInterface.RemoveCustomerBranch(customerBranchId);
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

        public ResponseOut RemoveCustomerProduct(long mappingId)
        {

            ResponseOut responseOut = new ResponseOut();
            try
            {
                responseOut = dbInterface.RemoveCustomerProduct(mappingId);
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

        public List<QuotationViewModel> GetQuotationList(string quotationNo, string customerName, string refNo, string fromDate, string toDate, int companyId, string displayType = "",string approvalStatus="")
        {
            List<QuotationViewModel> quotations = new List<QuotationViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtQuotations = sqlDbInterface.GetQuotationList(quotationNo, customerName, refNo, Convert.ToDateTime(fromDate),Convert.ToDateTime(toDate),companyId, displayType, approvalStatus);
                if (dtQuotations != null && dtQuotations.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtQuotations.Rows)
                    {
                        quotations.Add(new QuotationViewModel
                        {
                            QuotationId = Convert.ToInt32(dr["QuotationId"]),
                            QuotationNo = Convert.ToString(dr["QuotationNo"]),
                            QuotationDate = Convert.ToString(dr["QuotationDate"]),
                            CustomerName = Convert.ToString(dr["CustomerName"]),
                            CustomerId = Convert.ToInt32(dr["CustomerID"]),
                            CustomerCode= Convert.ToString(dr["CustomerCode"]),
                            City = Convert.ToString(dr["City"]),
                            StateName = Convert.ToString(dr["StateName"]),
                            RefNo = Convert.ToString(dr["RefNo"]),
                            RefDate = Convert.ToString(dr["RefDate"]),
                            BasicValue = Convert.ToDecimal(dr["BasicValue"]),
                            TotalValue = Convert.ToDecimal(dr["TotalValue"]),
                            QuotationRevisedStatus = Convert.ToBoolean(dr["QuotationRevisedStatus"]),
                            CreatedByUserName = Convert.ToString(dr["CreatedByName"]),
                            CreatedDate = Convert.ToString(dr["CreatedDate"]),
                            ApprovalStatus=Convert.ToString(dr["ApprovalStatus"]),
                            ModifiedByUserName = Convert.ToString(dr["ModifiedByName"]),
                            ModifiedDate = Convert.ToString(dr["ModifiedDate"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return quotations;
        }
        public QuotationViewModel GetQuotationDetail(long quotationId = 0)
        {
            QuotationViewModel quotation = new QuotationViewModel();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtCompanies = sqlDbInterface.GetQuotationDetail(quotationId);
                if (dtCompanies != null && dtCompanies.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtCompanies.Rows)
                    {
                        quotation = new QuotationViewModel
                        {
                            QuotationId = Convert.ToInt32(dr["QuotationId"]),
                            QuotationNo = Convert.ToString(dr["QuotationNo"]),
                            QuotationDate = Convert.ToString(dr["QuotationDate"]),
                            CustomerId = Convert.ToInt32(dr["CustomerId"]),
                            CustomerCode = Convert.ToString(dr["CustomerCode"]),
                            CustomerName = Convert.ToString(dr["CustomerName"]),
                            BillingAddress = Convert.ToString(dr["BillingAddress"]),
                            City = Convert.ToString(dr["City"]),
                            StateId = Convert.ToInt32(dr["StateId"]),
                            CountryId = Convert.ToInt32(dr["CountryId"]),
                            PinCode = Convert.ToString(dr["PinCode"]),
                            CSTNo = Convert.ToString(dr["CSTNo"]),
                            TINNo = Convert.ToString(dr["TINNo"]),
                            PANNo = Convert.ToString(dr["PANNo"]),
                            GSTNo = Convert.ToString(dr["GSTNo"]),
                            RefNo = Convert.ToString(dr["RefNo"]),
                            RefDate = Convert.ToString(dr["RefDate"]),
                            ApprovalStatus = Convert.ToString(dr["ApprovalStatus"]),
                            BasicValue = Convert.ToDecimal(dr["BasicValue"]),
                            TotalValue = Convert.ToDecimal(dr["TotalValue"]),
                            CreatedByUserName = Convert.ToString(dr["CreatedByName"]),
                            CreatedDate = Convert.ToString(dr["CreatedDate"]),
                            ModifiedByUserName = Convert.ToString(dr["ModifiedByName"]),
                            ModifiedDate = Convert.ToString(dr["ModifiedDate"])
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return quotation;
        }
        public DataTable GetQuotationDetailDataTable(long quotationId = 0)
        {
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            DataTable dtQuotation = new DataTable();
            try
            {
                 dtQuotation = sqlDbInterface.GetQuotationDetail(quotationId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dtQuotation;
        }
        public List<CustomerBranchViewModel> GetCustomerBranchList(int customerId)
        {
            List<CustomerBranchViewModel> customerBranchs = new List<CustomerBranchViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtCustomers = sqlDbInterface.GetCustomerBranchList(customerId);
                if (dtCustomers != null && dtCustomers.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtCustomers.Rows)
                    {
                        customerBranchs.Add(new CustomerBranchViewModel
                        {
                            CustomerBranchId = Convert.ToInt32(dr["CustomerBranchId"]),
                            BranchName = Convert.ToString(dr["BranchName"]),
                            PrimaryAddress = Convert.ToString(dr["PrimaryAddress"]),
                            City = Convert.ToString(dr["City"]),
                            StateId = Convert.ToInt32(dr["StateId"]),
                            StateName = Convert.ToString(dr["StateName"]),
                            CountryId = Convert.ToInt32(dr["CountryId"]),
                            PinCode = Convert.ToString(dr["PinCode"]),
                            CSTNo = Convert.ToString(dr["CSTNo"]),
                            TINNo = Convert.ToString(dr["TINNo"]),
                            PANNo = Convert.ToString(dr["PANNo"]),
                            GSTNo = Convert.ToString(dr["GSTNo"]),
                            ContactPersonName = Convert.ToString(dr["ContactPersonName"]),
                            Designation = Convert.ToString(dr["Designation"]),
                            Email = Convert.ToString(dr["Email"]),
                            MobileNo = Convert.ToString(dr["MobileNo"]),
                            ContactNo = Convert.ToString(dr["ContactNo"]),
                            Fax = Convert.ToString(dr["Fax"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return customerBranchs;
        }

        public List<QuotationProductViewModel> GetQuotationProductList(long quotationId)
        {
            List<QuotationProductViewModel> quotationProducts = new List<QuotationProductViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtCustomers = sqlDbInterface.GetQuotationProductList(quotationId);
                if (dtCustomers != null && dtCustomers.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtCustomers.Rows)
                    {
                        quotationProducts.Add(new QuotationProductViewModel
                        {
                            QuotationProductDetailId = Convert.ToInt32(dr["QuotationProductDetailId"]),
                            SequenceNo = Convert.ToInt32(dr["SNo"]),
                            ProductId = Convert.ToInt32(dr["ProductId"]),
                            ProductName = Convert.ToString(dr["ProductName"]),
                            ProductCode = Convert.ToString(dr["ProductCode"]),
                            ProductShortDesc = Convert.ToString(dr["ProductShortDesc"]),
                            UOMName = Convert.ToString(dr["UOMName"]),
                            Price = Convert.ToDecimal(dr["Price"]),
                            Quantity = Convert.ToDecimal(dr["Quantity"]),
                            DiscountPercentage = Convert.ToDecimal(dr["DiscountPercentage"]),
                            DiscountAmount = Convert.ToDecimal(dr["DiscountAmount"]),
                            TaxPercentage = Convert.ToDecimal(dr["TaxPercentage"]),
                            TaxAmount = Convert.ToDecimal(dr["TaxAmount"]),
                            TotalPrice = Convert.ToDecimal(dr["TotalPrice"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return quotationProducts;
        }

        
         public List<QuotationProductViewModel> GetQuotationSOProductList(long quotationId)
        {
            List<QuotationProductViewModel> quotationProducts = new List<QuotationProductViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtCustomers = sqlDbInterface.GetQuotationSOProductList(quotationId);
                if (dtCustomers != null && dtCustomers.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtCustomers.Rows)
                    {
                        quotationProducts.Add(new QuotationProductViewModel
                        {
                            QuotationProductDetailId = Convert.ToInt32(dr["QuotationProductDetailId"]),
                            SequenceNo = Convert.ToInt32(dr["SNo"]),
                            ProductId = Convert.ToInt32(dr["ProductId"]),
                            ProductName = Convert.ToString(dr["ProductName"]),
                            ProductCode = Convert.ToString(dr["ProductCode"]),
                            ProductShortDesc = Convert.ToString(dr["ProductShortDesc"]),
                            UOMName = Convert.ToString(dr["UOMName"]),
                            Price = Convert.ToDecimal(dr["Price"]),
                            Quantity = Convert.ToDecimal(dr["Quantity"]),
                            DiscountPercentage = Convert.ToDecimal(dr["DiscountPercentage"]),
                            DiscountAmount = Convert.ToDecimal(dr["DiscountAmount"]),
                            TaxPercentage = Convert.ToDecimal(dr["TaxPercentage"]),
                            TaxAmount = Convert.ToDecimal(dr["TaxAmount"]),
                            TotalPrice = Convert.ToDecimal(dr["TotalPrice"]),
                            TaxId = 0,
                            TaxName="",
                            SurchargeName_1 = Convert.ToString(dr["SurchargeName_1"]),
                            SurchargePercentage_1 = Convert.ToDecimal(dr["SurchargePercentage_1"]),
                            SurchargeAmount_1 = Convert.ToDecimal(dr["SurchargeAmount_1"]),
                            SurchargeName_2 = Convert.ToString(dr["SurchargeName_2"]),
                            SurchargePercentage_2 = Convert.ToDecimal(dr["SurchargePercentage_2"]),
                            SurchargeAmount_2 = Convert.ToDecimal(dr["SurchargeAmount_2"]),
                            SurchargeName_3 = Convert.ToString(dr["SurchargeName_3"]),
                            SurchargePercentage_3 = Convert.ToDecimal(dr["SurchargePercentage_3"]),
                            SurchargeAmount_3 = Convert.ToDecimal(dr["SurchargeAmount_3"]),

                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return quotationProducts;
        }

        public List<QuotationTaxViewModel> GetQuotationSOTaxList(long quotationId)
        {
            List<QuotationTaxViewModel> quotationTaxes = new List<QuotationTaxViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtCustomers = sqlDbInterface.GetQuotationTaxList(quotationId);
                if (dtCustomers != null && dtCustomers.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtCustomers.Rows)
                    {
                        quotationTaxes.Add(new QuotationTaxViewModel
                        {
                            QuotationTaxDetailId = Convert.ToInt32(dr["QuotationTaxDetailId"]),
                            TaxSequenceNo = Convert.ToInt32(dr["SNo"]),
                            TaxId = Convert.ToInt32(dr["TaxId"]),
                            TaxName = Convert.ToString(dr["TaxName"]),
                            TaxPercentage = Convert.ToDecimal(dr["TaxPercentage"]),
                            TaxAmount = Convert.ToDecimal(dr["TaxAmount"]),
                            SurchargeName_1 = Convert.ToString(dr["SurchargeName_1"]),
                            SurchargePercentage_1 = Convert.ToDecimal(dr["SurchargePercentage_1"]),
                            SurchargeAmount_1 = Convert.ToDecimal(dr["SurchargeAmount_1"]),
                            SurchargeName_2 = Convert.ToString(dr["SurchargeName_2"]),
                            SurchargePercentage_2 = Convert.ToDecimal(dr["SurchargePercentage_2"]),
                            SurchargeAmount_2 = Convert.ToDecimal(dr["SurchargeAmount_2"]),
                            SurchargeName_3 = Convert.ToString(dr["SurchargeName_3"]),
                            SurchargePercentage_3 = Convert.ToDecimal(dr["SurchargePercentage_3"]),
                            SurchargeAmount_3 = Convert.ToDecimal(dr["SurchargeAmount_3"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return quotationTaxes;
        }
        public DataTable GetQuotationProductListDataTable(long quotationId)
        {
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            DataTable dtProducts = new DataTable();
            try
            {
                 dtProducts = sqlDbInterface.GetQuotationProductList(quotationId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dtProducts;
        }
        public DataTable GetQuotationTermListDataTable(long quotationId)
        {
            
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            DataTable dtTerms = new DataTable();
            try
            {
                 dtTerms = sqlDbInterface.GetQuotationTermList(quotationId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dtTerms;
        }

        public List<QuotationTaxViewModel> GetQuotationTaxList(long quotationId)
        {
            List<QuotationTaxViewModel> quotationTaxes = new List<QuotationTaxViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtCustomers = sqlDbInterface.GetQuotationTaxList(quotationId);
                if (dtCustomers != null && dtCustomers.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtCustomers.Rows)
                    {
                        quotationTaxes.Add(new QuotationTaxViewModel
                        {
                            QuotationTaxDetailId = Convert.ToInt32(dr["QuotationTaxDetailId"]),
                            TaxSequenceNo=Convert.ToInt32(dr["SNo"]),
                            TaxId = Convert.ToInt32(dr["TaxId"]),
                            TaxName = Convert.ToString(dr["TaxName"]),
                            TaxPercentage = Convert.ToDecimal(dr["TaxPercentage"]),
                            TaxAmount = Convert.ToDecimal(dr["TaxAmount"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return quotationTaxes;
        }
        public List<QuotationTermViewModel> GetQuotationTermList(long quotationId)
        {
            List<QuotationTermViewModel> quotationTerms = new List<QuotationTermViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtCustomers = sqlDbInterface.GetQuotationTermList(quotationId);
                if (dtCustomers != null && dtCustomers.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtCustomers.Rows)
                    {
                        quotationTerms.Add(new QuotationTermViewModel
                        {
                            QuotationTermDetailId = Convert.ToInt32(dr["QuotationTermDetailId"]),
                            TermDesc = Convert.ToString(dr["TermDesc"]),
                            TermSequence = Convert.ToInt16(dr["TermSequence"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return quotationTerms;
        }

        public List<CustomerViewModel> GetCustomerAutoCompleteList(string searchTerm, int companyId)
        {
            List<CustomerViewModel> customers = new List<CustomerViewModel>();
            try
            {
                List<Customer> customerList = dbInterface.GetCustomerAutoCompleteList(searchTerm, companyId);

                if (customerList != null && customerList.Count > 0)
                {
                    foreach (Customer customer in customerList)
                    {
                        customers.Add(new CustomerViewModel { CustomerId = customer.CustomerId, CustomerName = customer.CustomerName, CustomerCode = customer.CustomerCode, PrimaryAddress = customer.PrimaryAddress });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return customers;
        }



        #region Revised Quotation 
        public ResponseOut AddRevisedQuotation(QuotationViewModel quotationViewModel, List<QuotationProductViewModel> quotationProducts, List<QuotationTaxViewModel> quotationTaxes, List<QuotationTermViewModel> quotationTerms)
        {
            ResponseOut responseOut = new ResponseOut();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                Quotation quotation = new Quotation
                {
                    QuotationId = quotationViewModel.QuotationId,
                    QuotationNo = quotationViewModel.QuotationNo,
                    QuotationDate = Convert.ToDateTime(quotationViewModel.QuotationDate),
                    CustomerId = quotationViewModel.CustomerId,
                    CustomerName = quotationViewModel.CustomerName,
                    BillingAddress = quotationViewModel.BillingAddress,
                    City = quotationViewModel.City,
                    StateId = quotationViewModel.StateId,
                    CountryId = quotationViewModel.CountryId,
                    PinCode = quotationViewModel.PinCode,
                    CSTNo = quotationViewModel.CSTNo,
                    TINNo = quotationViewModel.TINNo,
                    PANNo = quotationViewModel.PANNo,
                    GSTNo = quotationViewModel.GSTNo,
                    ExciseNo = quotationViewModel.ExciseNo,
                    RefNo = quotationViewModel.RefNo,
                    RefDate = string.IsNullOrEmpty(quotationViewModel.RefDate) ? Convert.ToDateTime("01-01-1900") : Convert.ToDateTime(quotationViewModel.RefDate),
                    BasicValue = quotationViewModel.BasicValue,
                    TotalValue = quotationViewModel.TotalValue,
                    FinYearId = quotationViewModel.FinYearId,
                    CompanyId = quotationViewModel.CompanyId,
                    CreatedBy = quotationViewModel.CreatedBy,
                    ApprovalStatus = quotationViewModel.ApprovalStatus

                };
                List<QuotationProductDetail> quotationProductList = new List<QuotationProductDetail>();
                if (quotationProducts != null && quotationProducts.Count > 0)
                {
                    foreach (QuotationProductViewModel item in quotationProducts)
                    {
                        quotationProductList.Add(new QuotationProductDetail
                        {
                            ProductId = item.ProductId,
                            ProductShortDesc = item.ProductShortDesc,
                            Price = item.Price,
                            Quantity = item.Quantity,
                            DiscountPercentage = 0,
                            DiscountAmount = 0,
                            TaxPercentage = 0,
                            TaxAmount = 0,
                            TotalPrice = item.TotalPrice
                        });
                    }
                }

                List<QuotationTaxDetail> quotationTaxList = new List<QuotationTaxDetail>();
                if (quotationTaxes != null && quotationTaxes.Count > 0)
                {
                    foreach (QuotationTaxViewModel item in quotationTaxes)
                    {
                        quotationTaxList.Add(new QuotationTaxDetail
                        {
                            TaxId = item.TaxId,
                            TaxName = item.TaxName,
                            TaxPercentage = item.TaxPercentage,
                            TaxAmount = item.TaxAmount
                        });
                    }
                }
                List<QuotationTermsDetail> quotationTermList = new List<QuotationTermsDetail>();
                if (quotationTerms != null && quotationTerms.Count > 0)
                {
                    foreach (QuotationTermViewModel item in quotationTerms)
                    {
                        quotationTermList.Add(new QuotationTermsDetail
                        {
                            TermDesc = item.TermDesc,
                            TermSequence = item.TermSequence
                        });
                    }
                }

                responseOut = sqlDbInterface.AddRevisedQuotation(quotation, quotationProductList, quotationTaxList, quotationTermList);
                 

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
        #endregion 
    }
}

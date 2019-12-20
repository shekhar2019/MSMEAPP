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
    public class SOBL
    {
        DBInterface dbInterface;
        public SOBL()
        {
            dbInterface = new DBInterface();
        }
        public ResponseOut AddEditSO(SOViewModel soViewModel, List<SOProductViewModel> soProducts, List<SOTaxViewModel> soTaxes, List<SOTermViewModel> soTerms)
        {
            ResponseOut responseOut = new ResponseOut();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                SO so = new SO
                {
                    SOId = soViewModel.SOId,
                    SODate = Convert.ToDateTime(soViewModel.SODate),
                    QuotationNo = soViewModel.QuotationNo,
                    QuotationId = soViewModel.QuotationId,
                    CustomerId = soViewModel.CustomerId,
                    CustomerName = soViewModel.CustomerName,
                    ContactPerson=soViewModel.ContactPerson,
                    BillingAddress = soViewModel.BillingAddress,
                    City = soViewModel.City,
                    StateId = soViewModel.StateId,
                    CountryId = soViewModel.CountryId,
                    PinCode = soViewModel.PinCode,
                    CSTNo = soViewModel.CSTNo,
                    TINNo = soViewModel.TINNo,
                    PANNo = soViewModel.PANNo,
                    GSTNo = soViewModel.GSTNo,
                    ExciseNo = soViewModel.ExciseNo,
                    Email = soViewModel.Email,
                    MobileNo = soViewModel.MobileNo,
                    ContactNo = soViewModel.ContactNo,
                    Fax = soViewModel.Fax,
                    ShippingContactPerson = soViewModel.ShippingContactPerson,
                    ShippingBillingAddress = soViewModel.ShippingBillingAddress,
                    ShippingCity = soViewModel.ShippingCity,
                    ShippingStateId = soViewModel.ShippingStateId,
                    ShippingCountryId = soViewModel.ShippingCountryId,
                    ShippingPinCode = soViewModel.ShippingPinCode,
                    ShippingTINNo = soViewModel.ShippingTINNo,
                    ShippingEmail = soViewModel.ShippingEmail,
                    ShippingMobileNo = soViewModel.ShippingMobileNo,
                    ShippingContactNo = soViewModel.ShippingContactNo,
                    ShippingFax = soViewModel.ShippingFax,
                    RefNo = soViewModel.RefNo,
                    RefDate = string.IsNullOrEmpty(soViewModel.RefDate) ? Convert.ToDateTime("01-01-1900") : Convert.ToDateTime(soViewModel.RefDate),
                    BasicValue = soViewModel.BasicValue,
                    TotalValue = soViewModel.TotalValue,
                    FreightValue = soViewModel.FreightValue,
                    LoadingValue = soViewModel.LoadingValue,
                    PayToBookId = soViewModel.PayToBookId,
                    Remarks1 = soViewModel.Remarks1,
                    Remarks2 = soViewModel.Remarks2,
                    FinYearId = soViewModel.FinYearId,
                    CompanyId = soViewModel.CompanyId,
                    ApprovalStatus=soViewModel.ApprovalStatus,
                    CreatedBy = soViewModel.CreatedBy

                };
                List<SOProductDetail> soProductList = new List<SOProductDetail>();
                if (soProducts != null && soProducts.Count > 0)
                {
                    foreach (SOProductViewModel item in soProducts)
                    {
                        soProductList.Add(new SOProductDetail
                        {
                            ProductId = item.ProductId,
                            ProductShortDesc = item.ProductShortDesc,
                            Price = item.Price,
                            Quantity = item.Quantity,
                            DiscountPercentage = item.DiscountPercentage,
                            DiscountAmount = item.DiscountAmount,
                            TaxId=item.TaxId,
                            TaxName=item.TaxName,
                            TaxPercentage = item.TaxPercentage,
                            TaxAmount = item.TaxAmount,
                            SurchargeName_1 = item.SurchargeName_1,
                            SurchargePercentage_1 = item.SurchargePercentage_1,
                            SurchargeAmount_1 = item.SurchargeAmount_1,
                            SurchargeName_2 = item.SurchargeName_2,
                            SurchargePercentage_2 = item.SurchargePercentage_2,
                            SurchargeAmount_2 = item.SurchargeAmount_2,
                            SurchargeName_3 = item.SurchargeName_3,
                            SurchargePercentage_3 = item.SurchargePercentage_3,
                            SurchargeAmount_3 = item.SurchargeAmount_3,
                            TotalPrice = item.TotalPrice
                        });
                    }
                }

                List<SOTaxDetail> soTaxList = new List<SOTaxDetail>();
                if (soTaxes != null && soTaxes.Count > 0)
                {
                    foreach (SOTaxViewModel item in soTaxes)
                    {
                        soTaxList.Add(new SOTaxDetail
                        {
                            TaxId = item.TaxId,
                            TaxName = item.TaxName,
                            TaxPercentage = item.TaxPercentage,
                            TaxAmount = item.TaxAmount,
                            SurchargeName_1 = item.SurchargeName_1,
                            SurchargePercentage_1 = item.SurchargePercentage_1,
                            SurchargeAmount_1 = item.SurchargeAmount_1,
                            SurchargeName_2 = item.SurchargeName_2,
                            SurchargePercentage_2 = item.SurchargePercentage_2,
                            SurchargeAmount_2 = item.SurchargeAmount_2,
                            SurchargeName_3 = item.SurchargeName_3,
                            SurchargePercentage_3 = item.SurchargePercentage_3,
                            SurchargeAmount_3 = item.SurchargeAmount_3
                        });
                    }
                }
                List<SOTermsDetail> soTermList = new List<SOTermsDetail>();
                if (soTerms != null && soTerms.Count > 0)
                {
                    foreach (SOTermViewModel item in soTerms)
                    {
                        soTermList.Add(new SOTermsDetail
                        {
                            TermDesc = item.TermDesc,
                            TermSequence = item.TermSequence
                        });
                    }
                }

                responseOut = sqlDbInterface.AddEditSO(so, soProductList, soTaxList, soTermList);




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

        public List<SOViewModel> GetSOList(string soNo, string customerName, string refNo, string fromDate, string toDate, int companyId, string approvalStatus = "",string displayType="")
        {
            List<SOViewModel> sos = new List<SOViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtSOs = sqlDbInterface.GetSOList(soNo, customerName, refNo, Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate), companyId, approvalStatus , displayType);
                if (dtSOs != null && dtSOs.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtSOs.Rows)
                    {
                        sos.Add(new SOViewModel
                        {
                            SOId = Convert.ToInt32(dr["SOId"]),
                            SONo = Convert.ToString(dr["SONo"]),
                            SODate = Convert.ToString(dr["SODate"]),
                            CustomerId= Convert.ToInt32(dr["CustomerId"]),
                            CustomerCode= Convert.ToString(dr["CustomerCode"]),
                            CustomerName = Convert.ToString(dr["CustomerName"]),
                            QuotationNo= Convert.ToString(dr["QuotationNo"]),
                            RefNo = Convert.ToString(dr["RefNo"]),
                            RefDate = Convert.ToString(dr["RefDate"]),
                            BasicValue = Convert.ToDecimal(dr["BasicValue"]),
                            TotalValue = Convert.ToDecimal(dr["TotalValue"]),
                            ApprovalStatus=Convert.ToString(dr["ApprovalStatus"]),
                            CreatedByUserName = Convert.ToString(dr["CreatedByName"]),
                            CreatedDate = Convert.ToString(dr["CreatedDate"]),
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
            return sos;
        }
        public SOViewModel GetSODetail(long soId = 0)
        {
            SOViewModel so = new SOViewModel();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtCompanies = sqlDbInterface.GetSODetail(soId);
                if (dtCompanies != null && dtCompanies.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtCompanies.Rows)
                    {
                        so = new SOViewModel
                        {
                            SOId = Convert.ToInt32(dr["SOId"]),
                            SONo = Convert.ToString(dr["SONo"]),
                            SODate = Convert.ToString(dr["SODate"]),
                            QuotationId = Convert.ToInt32(dr["QuotationId"]),
                            QuotationNo = Convert.ToString(dr["QuotationNo"]),
                            QuotationDate = Convert.ToString(dr["QuotationDate"]),
                            
                            CustomerId = Convert.ToInt32(dr["CustomerId"]),
                            CustomerCode = Convert.ToString(dr["CustomerCode"]),
                            CustomerName = Convert.ToString(dr["CustomerName"]),

                            ContactPerson = Convert.ToString(dr["ContactPerson"]),
                            BillingAddress = Convert.ToString(dr["BillingAddress"]),
                            City = Convert.ToString(dr["City"]),
                            StateId = Convert.ToInt32(dr["StateId"]),
                            CountryId = Convert.ToInt32(dr["CountryId"]),
                            PinCode = Convert.ToString(dr["PinCode"]),
                            TINNo = Convert.ToString(dr["TINNo"]),

                            Email = Convert.ToString(dr["Email"]),
                            MobileNo = Convert.ToString(dr["MobileNo"]),
                            ContactNo = Convert.ToString(dr["ContactNo"]),
                            Fax = Convert.ToString(dr["Fax"]),


                            ShippingContactPerson = Convert.ToString(dr["ShippingContactPerson"]),
                            ShippingBillingAddress = Convert.ToString(dr["ShippingBillingAddress"]),
                            ShippingCity = Convert.ToString(dr["ShippingCity"]),
                            ShippingStateId = Convert.ToInt32(dr["ShippingStateId"]),
                            ShippingCountryId = Convert.ToInt32(dr["ShippingCountryId"]),
                            ShippingPinCode = Convert.ToString(dr["ShippingPinCode"]),
                            ShippingTINNo = Convert.ToString(dr["ShippingTINNo"]),

                            ShippingEmail = Convert.ToString(dr["ShippingEmail"]),
                            ShippingMobileNo = Convert.ToString(dr["ShippingMobileNo"]),
                            ShippingContactNo = Convert.ToString(dr["ShippingContactNo"]),
                            ShippingFax = Convert.ToString(dr["ShippingFax"]),


                            RefNo = Convert.ToString(dr["RefNo"]),
                            RefDate = Convert.ToString(dr["RefDate"]),

                            BasicValue = Convert.ToDecimal(dr["BasicValue"]),
                            TotalValue = Convert.ToDecimal(dr["TotalValue"]),

                            FreightValue = Convert.ToDecimal(dr["FreightValue"]),
                            LoadingValue = Convert.ToDecimal(dr["LoadingValue"]),
                            PayToBookId = Convert.ToInt32(dr["PayToBookId"]),
                            PayToBookName= Convert.ToString(dr["PayToBookName"]),
                            PayToBookBranch= Convert.ToString(dr["PayToBookBranch"]),
                            
                            Remarks1 = Convert.ToString(dr["Remarks1"]),
                            Remarks2 = Convert.ToString(dr["Remarks2"]),
                            ApprovalStatus = Convert.ToString(dr["ApprovalStatus"]),
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
            return so;
        }
        public DataTable GetSODetailDataTable(long soId = 0)
        {
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            DataTable dtQuotation = new DataTable();
            try
            {
                dtQuotation = sqlDbInterface.GetSODetail(soId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dtQuotation;
        }
        public DataTable GetSOProductListDataTable(long soId)
        {
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            DataTable dtProducts = new DataTable();
            try
            {
                dtProducts = sqlDbInterface.GetSOProductList(soId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dtProducts;
        }
        public DataTable GetSOTermListDataTable(long soId)
        {

            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            DataTable dtTerms = new DataTable();
            try
            {
                dtTerms = sqlDbInterface.GetSOTermList(soId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dtTerms;
        }
        public DataTable GetSOTaxListDataTable(long soId)
        {

            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            DataTable dtTerms = new DataTable();
            try
            {
                dtTerms = sqlDbInterface.GetSOTaxList(soId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dtTerms;
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

        public List<SOProductViewModel> GetSOProductList(long soId)
        {
            List<SOProductViewModel> soProducts = new List<SOProductViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtCustomers = sqlDbInterface.GetSOProductList(soId);
                if (dtCustomers != null && dtCustomers.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtCustomers.Rows)
                    {
                        soProducts.Add(new SOProductViewModel
                        {
                            SOProductDetailId = Convert.ToInt32(dr["SOProductDetailId"]),
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
                            SurchargeAmount_3 = Convert.ToDecimal(dr["SurchargeAmount_3"]),
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
            return soProducts;
        }

        public List<SOTaxViewModel> GetSOTaxList(long soId)
        {
            List<SOTaxViewModel> soTaxes = new List<SOTaxViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtCustomers = sqlDbInterface.GetSOTaxList(soId);
                if (dtCustomers != null && dtCustomers.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtCustomers.Rows)
                    {
                        soTaxes.Add(new SOTaxViewModel
                        {
                            TaxSequenceNo = Convert.ToInt32(dr["SNo"]),
                            SOTaxDetailId = Convert.ToInt32(dr["SOTaxDetailId"]),
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
            return soTaxes;
        }
        public List<SOTermViewModel> GetSOTermList(long soId)
        {
            List<SOTermViewModel> soTerms = new List<SOTermViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtCustomers = sqlDbInterface.GetSOTermList(soId);
                if (dtCustomers != null && dtCustomers.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtCustomers.Rows)
                    {
                        soTerms.Add(new SOTermViewModel
                        {
                            SOTermDetailId = Convert.ToInt32(dr["SOTermDetailId"]),
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
            return soTerms;
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


    }
}

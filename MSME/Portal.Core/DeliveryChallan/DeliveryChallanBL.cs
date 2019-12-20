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
    public class DeliveryChallanBL
    {
        DBInterface dbInterface;
        public DeliveryChallanBL()
        {
            dbInterface = new DBInterface();
        }
        public ResponseOut AddEditDeliveryChallan(DeliveryChallanViewModel challanViewModel, List<ChallanProductViewModel> challanProducts, List<ChallanTaxViewModel> challanTaxes, List<ChallanTermsViewModel> challanTerms)
        {
            ResponseOut responseOut = new ResponseOut();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DeliveryChallan deliverychallan = new DeliveryChallan
                {
                    ChallanId = challanViewModel.ChallanId,
                    ChallanDate = Convert.ToDateTime(challanViewModel.ChallanDate),
                    InvoiceNo = challanViewModel.InvoiceNo,
                    InvoiceId = challanViewModel.InvoiceId,
                    CustomerId = challanViewModel.CustomerId,
                    CustomerName = challanViewModel.CustomerName,
                    ContactPerson = challanViewModel.ContactPerson,  
                    ShippingContactPerson = challanViewModel.ShippingContactPerson,
                    ShippingBillingAddress = challanViewModel.ShippingBillingAddress,
                    ShippingCity = challanViewModel.ShippingCity,
                    ShippingStateId = challanViewModel.ShippingStateId,
                    ShippingCountryId = challanViewModel.ShippingCountryId,
                    ShippingPinCode = challanViewModel.ShippingPinCode,
                    ShippingTINNo = challanViewModel.ShippingTINNo,
                    ShippingEmail = challanViewModel.ShippingEmail,
                    ShippingMobileNo = challanViewModel.ShippingMobileNo,
                    ShippingContactNo = challanViewModel.ShippingContactNo,
                    ShippingFax = challanViewModel.ShippingFax,
                    CompanyBranchId=challanViewModel.CompanyBranchId,
                    DispatchRefNo = challanViewModel.DispatchRefNo,
                    LRNo = challanViewModel.LRNo, 
                    LRDate = string.IsNullOrEmpty(challanViewModel.LRDate) ? Convert.ToDateTime("01-01-1900") : Convert.ToDateTime(challanViewModel.LRDate),
                    TransportVia = challanViewModel.TransportVia,
                    NoOfPackets = challanViewModel.NoOfPackets, 
                    DispatchRefDate = string.IsNullOrEmpty(challanViewModel.DispatchRefDate) ? Convert.ToDateTime("01-01-1900") : Convert.ToDateTime(challanViewModel.DispatchRefDate),
                    BasicValue = challanViewModel.BasicValue,
                    TotalValue = challanViewModel.TotalValue,
                    FreightValue = challanViewModel.FreightValue,
                    LoadingValue = challanViewModel.LoadingValue, 
                    Remarks1 = challanViewModel.Remarks1,
                    Remarks2 = challanViewModel.Remarks2,
                    ApprovalStatus= challanViewModel.ApprovalStatus,
                    FinYearId = challanViewModel.FinYearId,
                    CompanyId = challanViewModel.CompanyId,
                    CreatedBy = challanViewModel.CreatedBy

                };
                List<ChallanProductDetail> challanProductList = new List<ChallanProductDetail>();
                if (challanProducts != null && challanProducts.Count > 0)
                {
                    foreach (ChallanProductViewModel item in challanProducts)
                    {
                        challanProductList.Add(new ChallanProductDetail
                        {
                            ProductId = item.ProductId,
                            ProductShortDesc = item.ProductShortDesc,
                            Price = item.Price,
                            Quantity = item.Quantity,
                            DiscountPercentage = item.DiscountPercentage,
                            DiscountAmount = item.DiscountAmount,
                            TaxId = item.TaxId,
                            TaxName = item.TaxName,
                            TaxPercentage = item.TaxPercentage,
                            TaxAmount = item.TaxAmount,
                            TotalPrice = item.TotalPrice
                        });
                    }
                }

                List<ChallanTaxDetail> challanTaxList = new List<ChallanTaxDetail>();
                if (challanTaxes != null && challanTaxes.Count > 0)
                {
                    foreach (ChallanTaxViewModel item in challanTaxes)
                    {
                        challanTaxList.Add(new ChallanTaxDetail
                        {
                            TaxId = item.TaxId,
                            TaxName = item.TaxName,
                            TaxPercentage = item.TaxPercentage,
                            TaxAmount = item.TaxAmount
                        });
                    }
                }
                List<ChallanTermsDetail> challanTermList = new List<ChallanTermsDetail>();
                if (challanTerms != null && challanTerms.Count > 0)
                {
                    foreach (ChallanTermsViewModel item in challanTerms)
                    {
                        challanTermList.Add(new ChallanTermsDetail
                        {
                            TermDesc = item.TermDesc,
                            TermSequence = item.TermSequence
                        });
                    }
                }

                responseOut = sqlDbInterface.AddEditDeliveryChallan(deliverychallan, challanProductList, challanTaxList, challanTermList); 

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

        public List<DeliveryChallanViewModel> GetChallanList(string deliverychallanNo, string customerName, string dispatchrefNo, string fromDate, string toDate, string approvalStatus, int companyId)
        {
            List<DeliveryChallanViewModel> deliverychallans = new List<DeliveryChallanViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtDeliveryChallans = sqlDbInterface.GetChallanList(deliverychallanNo, customerName, dispatchrefNo, Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate), approvalStatus, companyId);
                if (dtDeliveryChallans != null && dtDeliveryChallans.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtDeliveryChallans.Rows)
                    {
                        deliverychallans.Add(new DeliveryChallanViewModel
                        {
                            ChallanId = Convert.ToInt32(dr["ChallanId"]),
                            ChallanNo = Convert.ToString(dr["ChallanNo"]),
                            ChallanDate = Convert.ToString(dr["ChallanDate"]),
                            CustomerId = Convert.ToInt32(dr["CustomerId"]),
                            CustomerCode = Convert.ToString(dr["CustomerCode"]),
                            CustomerName = Convert.ToString(dr["CustomerName"]),
                            ShippingCity = Convert.ToString(dr["ShippingCity"]),
                            ShippingStateName = Convert.ToString(dr["StateName"]),
                            DispatchRefNo = Convert.ToString(dr["DispatchRefNo"]),
                            DispatchRefDate = Convert.ToString(dr["DispatchRefDate"]),
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
            return deliverychallans;
        }
        public DeliveryChallanViewModel GetChallanDetail(long challanId = 0)
        {
            DeliveryChallanViewModel deliverychallan = new DeliveryChallanViewModel();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtCompanies = sqlDbInterface.GetChallanDetail(challanId);
                if (dtCompanies != null && dtCompanies.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtCompanies.Rows)
                    {
                        deliverychallan = new DeliveryChallanViewModel
                        {
                            ChallanId = Convert.ToInt32(dr["ChallanId"]),
                            ChallanNo = Convert.ToString(dr["ChallanNo"]),
                            ChallanDate = Convert.ToString(dr["ChallanDate"]),
                            InvoiceId = Convert.ToInt32(dr["InvoiceId"]),
                            InvoiceNo = Convert.ToString(dr["InvoiceNo"]),
                            InvoiceDate = Convert.ToString(dr["InvoiceDate"]),

                            CustomerId = Convert.ToInt32(dr["CustomerId"]),
                            CustomerCode = Convert.ToString(dr["CustomerCode"]),
                            CustomerName = Convert.ToString(dr["CustomerName"]), 

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

                            CompanyBranchId = Convert.ToInt32(string.IsNullOrEmpty(dr["CompanyBranchId"].ToString())? "0": dr["CompanyBranchId"]),
                            CompanyBranchName = Convert.ToString(dr["CompanyBranchName"]),
                            CompanyBranchAddress = Convert.ToString(dr["CompanyBranchAddress"]),
                            CompanyBranchCity = Convert.ToString(dr["CompanyBranchCity"]),
                            CompanyBranchStateName = Convert.ToString(dr["CompanyBranchStateName"]),
                            CompanyBranchPinCode = Convert.ToString(dr["CompanyBranchPinCode"]),
                            CompanyBranchCSTNo = Convert.ToString(dr["CompanyBranchCSTNo"]),
                            CompanyBranchTINNo = Convert.ToString(dr["CompanyBranchTINNo"]),
                            DispatchRefNo = Convert.ToString(dr["DispatchRefNo"]),
                            DispatchRefDate = Convert.ToString(dr["DispatchRefDate"]),
                             
                            LRNo = Convert.ToString(dr["LRNo"]),
                            LRDate = Convert.ToString(dr["LRDate"]), 

                            TransportVia = Convert.ToString(dr["TransportVia"]),
                            NoOfPackets = Convert.ToInt32(dr["NoOfPackets"]), 

                            BasicValue = Convert.ToDecimal(dr["BasicValue"]),
                            TotalValue = Convert.ToDecimal(dr["TotalValue"]),

                            FreightValue = Convert.ToDecimal(dr["FreightValue"]),
                            LoadingValue = Convert.ToDecimal(dr["LoadingValue"]),
                          
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
            return deliverychallan;
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

        public List<ChallanProductViewModel> GetChallanProductList(long challanId)
        {
            List<ChallanProductViewModel> challanProducts = new List<ChallanProductViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtCustomers = sqlDbInterface.GetChallanProductList(challanId);
                if (dtCustomers != null && dtCustomers.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtCustomers.Rows)
                    {
                        challanProducts.Add(new ChallanProductViewModel
                        {
                            SequenceNo = Convert.ToInt32(dr["SNo"]),
                            ChallanProductDetailId = Convert.ToInt32(dr["ChallanProductDetailId"]),
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
            return challanProducts;
        }

        public List<ChallanTaxViewModel> GetChallanTaxList(long challanId)
        {
            List<ChallanTaxViewModel> challanTaxes = new List<ChallanTaxViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtCustomers = sqlDbInterface.GetChallanTaxList(challanId);
                if (dtCustomers != null && dtCustomers.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtCustomers.Rows)
                    {
                        challanTaxes.Add(new ChallanTaxViewModel
                        {
                            TaxSequenceNo = Convert.ToInt32(dr["SNo"]),
                            ChallanTaxDetailId = Convert.ToInt32(dr["ChallanTaxDetailId"]),
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
            return challanTaxes;
        }
        public List<ChallanTermsViewModel> GetChallanTermList(long challanId)
        {
            List<ChallanTermsViewModel> challanTerms = new List<ChallanTermsViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtCustomers = sqlDbInterface.GetChallanTermList(challanId);
                if (dtCustomers != null && dtCustomers.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtCustomers.Rows)
                    {
                        challanTerms.Add(new ChallanTermsViewModel
                        {
                            ChallanTermDetailId = Convert.ToInt32(dr["ChallanTermDetailId"]),
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
            return challanTerms;
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

        public DataTable GetChallanDetailTable(long challanId=0)
        {
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            DataTable dtChallan = new DataTable();
            try
            {
                dtChallan = sqlDbInterface.GetChallanDetail(challanId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dtChallan;
        }
        public DataTable GetChallanProductListDataTable(long challanId)
        {

            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            DataTable dtProducts = new DataTable();
            try
            {
                dtProducts = sqlDbInterface.GetChallanProductList(challanId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dtProducts;
        }
        public DataTable GetChallanTaxListDataTable(long challanId)
        {

            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            DataTable dtTerms = new DataTable();
            try
            {
                dtTerms = sqlDbInterface.GetChallanTaxList(challanId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dtTerms;
        }
        public DataTable GetChallanTermListDataTable(long challanId)
        {

            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            DataTable dtTerms = new DataTable();
            try
            {
                dtTerms = sqlDbInterface.GetChallanTermList(challanId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dtTerms;
        }
        

    }
}

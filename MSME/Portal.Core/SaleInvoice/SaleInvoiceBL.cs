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
    public class SaleInvoiceBL
    {
        DBInterface dbInterface;
        public SaleInvoiceBL()
        {
            dbInterface = new DBInterface();
        }
        public ResponseOut AddEditSaleInvoice(SaleInvoiceViewModel saleinvoiceViewModel, List<SaleInvoiceProductViewModel> saleinvoiceProducts, List<SaleInvoiceTaxViewModel> saleinvoiceTaxes, List<SaleInvoiceTermViewModel> saleinvoiceTerms)
        {
            ResponseOut responseOut = new ResponseOut();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                SaleInvoice saleinvoice = new SaleInvoice
                {
                    InvoiceId = saleinvoiceViewModel.InvoiceId,
                    InvoiceDate = Convert.ToDateTime(saleinvoiceViewModel.InvoiceDate),
                    SONo = saleinvoiceViewModel.SONo,
                    SOId = saleinvoiceViewModel.SOId,
                    CustomerId = saleinvoiceViewModel.CustomerId,
                    CustomerName = saleinvoiceViewModel.CustomerName,
                    InvoiceType = saleinvoiceViewModel.InvoiceType,
                    CompanyBranchId= saleinvoiceViewModel.CompanyBranchId,
                    CurrencyCode = saleinvoiceViewModel.CurrencyCode,
                    ContactPerson = saleinvoiceViewModel.ContactPerson,
                    BillingAddress = saleinvoiceViewModel.BillingAddress,
                    City = saleinvoiceViewModel.City,
                    StateId = saleinvoiceViewModel.StateId,
                    CountryId = saleinvoiceViewModel.CountryId,
                    PinCode = saleinvoiceViewModel.PinCode,
                    CSTNo = saleinvoiceViewModel.CSTNo,
                    TINNo = saleinvoiceViewModel.TINNo,
                    PANNo = saleinvoiceViewModel.PANNo,
                    GSTNo = saleinvoiceViewModel.GSTNo,
                    ExciseNo = saleinvoiceViewModel.ExciseNo,
                    Email = saleinvoiceViewModel.Email,
                    MobileNo = saleinvoiceViewModel.MobileNo,
                    ContactNo = saleinvoiceViewModel.ContactNo,
                    Fax = saleinvoiceViewModel.Fax,
                    ApprovalStatus= saleinvoiceViewModel.ApprovalStatus,

                    ShippingContactPerson = saleinvoiceViewModel.ShippingContactPerson,
                    ShippingBillingAddress = saleinvoiceViewModel.ShippingBillingAddress,
                    ShippingCity = saleinvoiceViewModel.ShippingCity,
                    ShippingStateId = saleinvoiceViewModel.ShippingStateId,
                    ShippingCountryId = saleinvoiceViewModel.ShippingCountryId,
                    ShippingPinCode = saleinvoiceViewModel.ShippingPinCode,
                    ShippingTINNo = saleinvoiceViewModel.ShippingTINNo,
                    ShippingEmail = saleinvoiceViewModel.ShippingEmail,
                    ShippingMobileNo = saleinvoiceViewModel.ShippingMobileNo,
                    ShippingContactNo = saleinvoiceViewModel.ShippingContactNo,
                    ShippingFax = saleinvoiceViewModel.ShippingFax,
                    RefNo = saleinvoiceViewModel.RefNo,
                    RefDate = string.IsNullOrEmpty(saleinvoiceViewModel.RefDate) ? Convert.ToDateTime("01-01-1900") : Convert.ToDateTime(saleinvoiceViewModel.RefDate),
                    BasicValue = saleinvoiceViewModel.BasicValue,
                    TotalValue = saleinvoiceViewModel.TotalValue,
                    FreightValue = saleinvoiceViewModel.FreightValue,
                    LoadingValue = saleinvoiceViewModel.LoadingValue,
                    PayToBookId = saleinvoiceViewModel.PayToBookId,
                    Remarks = saleinvoiceViewModel.Remarks,
                    FinYearId = saleinvoiceViewModel.FinYearId,
                    CompanyId = saleinvoiceViewModel.CompanyId,
                    CreatedBy = saleinvoiceViewModel.CreatedBy

                };
                List<SaleInvoiceProductDetail> saleinvoiceProductList = new List<SaleInvoiceProductDetail>();
                if (saleinvoiceProducts != null && saleinvoiceProducts.Count > 0)
                {
                    foreach (SaleInvoiceProductViewModel item in saleinvoiceProducts)
                    {
                        saleinvoiceProductList.Add(new SaleInvoiceProductDetail
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
                            SurchargeName_1=item.SurchargeName_1,
                            SurchargePercentage_1=item.SurchargePercentage_1,
                            SurchargeAmount_1=item.SurchargeAmount_1,
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

                List<SaleInvoiceTaxDetail> saleinvoiceTaxList = new List<SaleInvoiceTaxDetail>();
                if (saleinvoiceTaxes != null && saleinvoiceTaxes.Count > 0)
                {
                    foreach (SaleInvoiceTaxViewModel item in saleinvoiceTaxes)
                    {
                        saleinvoiceTaxList.Add(new SaleInvoiceTaxDetail
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
                List<SaleInvoiceTermsDetail> saleinvoiceTermList = new List<SaleInvoiceTermsDetail>();
                if (saleinvoiceTerms != null && saleinvoiceTerms.Count > 0)
                {
                    foreach (SaleInvoiceTermViewModel item in saleinvoiceTerms)
                    {
                        saleinvoiceTermList.Add(new SaleInvoiceTermsDetail
                        {
                            TermDesc = item.TermDesc,
                            TermSequence = item.TermSequence
                        });
                    }
                }

                responseOut = sqlDbInterface.AddEditSaleInvoice(saleinvoice, saleinvoiceProductList, saleinvoiceTaxList, saleinvoiceTermList);

                 
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

        public List<SaleInvoiceViewModel> GetSaleInvoiceList(string saleinvoiceNo, string customerName, string refNo, string fromDate, string toDate, int companyId,string invoiceType="",string displayType="" ,string approvalStatus="",int companyBranchId=0)
        {
            List<SaleInvoiceViewModel> saleinvoices = new List<SaleInvoiceViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtSaleInvoices = sqlDbInterface.GetSaleInvoiceList(saleinvoiceNo, customerName, refNo, Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate), companyId,invoiceType,displayType, approvalStatus, companyBranchId);
                if (dtSaleInvoices != null && dtSaleInvoices.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtSaleInvoices.Rows)
                    {
                        saleinvoices.Add(new SaleInvoiceViewModel
                        {
                            InvoiceId = Convert.ToInt32(dr["InvoiceId"]),
                            InvoiceNo = Convert.ToString(dr["InvoiceNo"]),
                            InvoiceDate = Convert.ToString(dr["InvoiceDate"]),
                            SONo = Convert.ToString(dr["SONo"]),
                            InvoiceType = Convert.ToString(dr["InvoiceType"]),
                            CompanyBranchName = Convert.ToString(dr["BranchName"]),
                            CustomerId = Convert.ToInt32(dr["CustomerId"]),
                            CustomerCode = Convert.ToString(dr["CustomerCode"]),
                            CustomerName = Convert.ToString(dr["CustomerName"]),
                            City = Convert.ToString(dr["City"]),
                            StateName = Convert.ToString(dr["StateName"]),
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
            return saleinvoices;
        }
        public SaleInvoiceViewModel GetSaleInvoiceDetail(long saleinvoiceId = 0)
        {
            SaleInvoiceViewModel saleinvoice = new SaleInvoiceViewModel();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtCompanies = sqlDbInterface.GetSaleInvoiceDetail(saleinvoiceId);
                if (dtCompanies != null && dtCompanies.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtCompanies.Rows)
                    {
                        saleinvoice = new SaleInvoiceViewModel
                        {
                            InvoiceId = Convert.ToInt32(dr["InvoiceId"]),
                            InvoiceNo = Convert.ToString(dr["InvoiceNo"]),
                            InvoiceType= Convert.ToString(dr["InvoiceType"]),
                            CompanyBranchId= Convert.ToInt32(dr["CompanyBranchId"]),
                            CurrencyCode= Convert.ToString(dr["CurrencyCode"]),
                            InvoiceDate = Convert.ToString(dr["InvoiceDate"]),
                            SOId = Convert.ToInt32(dr["SOId"]),
                            SONo = Convert.ToString(dr["SONo"]),
                            SODate = Convert.ToString(dr["SODate"]),

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
                            ApprovalStatus = Convert.ToString(dr["ApprovalStatus"]),

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
                            PayToBookName = Convert.ToString(dr["PayToBookName"]),
                            PayToBookBranch = Convert.ToString(dr["PayToBookBranch"]),

                            Remarks = Convert.ToString(dr["Remarks"]),
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
            return saleinvoice;
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

        public List<SaleInvoiceProductViewModel> GetSaleInvoiceProductList(long saleinvoiceId)
        {
            List<SaleInvoiceProductViewModel> saleinvoiceProducts = new List<SaleInvoiceProductViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtCustomers = sqlDbInterface.GetSaleInvoiceProductList(saleinvoiceId);
                if (dtCustomers != null && dtCustomers.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtCustomers.Rows)
                    {
                        saleinvoiceProducts.Add(new SaleInvoiceProductViewModel
                        {
                            SequenceNo = Convert.ToInt32(dr["SNo"]),
                            InvoiceProductDetailId = Convert.ToInt32(dr["InvoiceProductDetailId"]),
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
            return saleinvoiceProducts;
        }

        public List<SaleInvoiceTaxViewModel> GetSaleInvoiceTaxList(long saleinvoiceId)
        {
            List<SaleInvoiceTaxViewModel> saleinvoiceTaxes = new List<SaleInvoiceTaxViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtCustomers = sqlDbInterface.GetSaleInvoiceTaxList(saleinvoiceId);
                if (dtCustomers != null && dtCustomers.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtCustomers.Rows)
                    {
                        saleinvoiceTaxes.Add(new SaleInvoiceTaxViewModel
                        {
                            TaxSequenceNo = Convert.ToInt32(dr["SNo"]),
                            InvoiceTaxDetailId = Convert.ToInt32(dr["InvoiceTaxDetailId"]),
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
            return saleinvoiceTaxes;
        }
        public List<SaleInvoiceTermViewModel> GetSaleInvoiceTermList(long saleinvoiceId)
        {
            List<SaleInvoiceTermViewModel> saleinvoiceTerms = new List<SaleInvoiceTermViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtCustomers = sqlDbInterface.GetSaleInvoiceTermList(saleinvoiceId);
                if (dtCustomers != null && dtCustomers.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtCustomers.Rows)
                    {
                        saleinvoiceTerms.Add(new SaleInvoiceTermViewModel
                        {
                            InvoiceTermDetailId = Convert.ToInt32(dr["InvoiceTermDetailId"]),
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
            return saleinvoiceTerms;
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
        public DataTable GetSaleInvoiceDetailDataTable(long siId = 0)
        {
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            DataTable dtQuotation = new DataTable();
            try
            {
                dtQuotation = sqlDbInterface.GetSaleInvoiceDetail(siId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dtQuotation;
        }
        public DataTable GetSaleInvoiceProductListDataTable(long siId)
        {
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            DataTable dtProducts = new DataTable();
            try
            {
                dtProducts = sqlDbInterface.GetSaleInvoiceProductList(siId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dtProducts;
        }
        public DataTable GetSaleInvoiceTermListDataTable(long siId)
        {

            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            DataTable dtTerms = new DataTable();
            try
            {
                dtTerms = sqlDbInterface.GetSaleInvoiceTermList(siId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dtTerms;
        }
        public DataTable GetSaleInvoiceTaxListDataTable(long siId)
        {

            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            DataTable dtTerms = new DataTable();
            try
            {
                dtTerms = sqlDbInterface.GetSaleInvoiceTaxList(siId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dtTerms;
        }



         
        public ResponseOut CancelSaleInvoice(SaleInvoiceViewModel saleinvoiceViewModel)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                SaleInvoice saleinvoice = new SaleInvoice
                {
                    InvoiceId = saleinvoiceViewModel.InvoiceId,
                    CancelStatus = "Cancel",
                    ApprovalStatus = "Cancelled",
                    CreatedBy = saleinvoiceViewModel.CreatedBy, 
                    CancelReason=saleinvoiceViewModel.CancelReason
                };
                responseOut = dbInterface.CancelSaleInvoice(saleinvoice);
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

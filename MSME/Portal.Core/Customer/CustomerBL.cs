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
    public class CustomerBL
    {
        DBInterface dbInterface;
        public CustomerBL()
        {
            dbInterface = new DBInterface();
        }
        public ResponseOut AddEditCustomer(CustomerViewModel customerViewModel, List<CustomerBranchViewModel> customerBranchs, List<CustomerProductViewModel> customerProducts)
        {
            ResponseOut responseOutBranch = new ResponseOut();
            ResponseOut responseOutProduct = new ResponseOut();
            ResponseOut responseOut = new ResponseOut();
            using (TransactionScope transactionscope = new TransactionScope())
            {
                try
                {

                    Customer customer = new Customer
                    {
                        CustomerId = customerViewModel.CustomerId,
                        CustomerCode = customerViewModel.CustomerCode,
                        CustomerName = customerViewModel.CustomerName,
                        ContactPersonName = customerViewModel.ContactPersonName,
                        Designation = customerViewModel.Designation,
                        Email = customerViewModel.Email,
                        MobileNo = customerViewModel.MobileNo,
                        ContactNo = customerViewModel.ContactNo,
                        Fax = customerViewModel.Fax,
                        PrimaryAddress = customerViewModel.PrimaryAddress,
                        City = customerViewModel.City,
                        StateId = customerViewModel.StateId,
                        CountryId = customerViewModel.CountryId,
                        PinCode = customerViewModel.PinCode,
                        CSTNo = customerViewModel.CSTNo,
                        TINNo = customerViewModel.TINNo,
                        PANNo = customerViewModel.PANNo,
                        GSTNo = customerViewModel.GSTNo,
                        ExciseNo = customerViewModel.ExciseNo,
                        EmployeeId = customerViewModel.EmployeeId,
                        CustomerTypeId = customerViewModel.CustomerTypeId,
                        CompanyId = customerViewModel.CompanyId,
                        CreatedBy = customerViewModel.CreatedBy,
                        CreditLimit=customerViewModel.CreditLimit,
                        CreditDays=customerViewModel.CreditDays,
                        Status = customerViewModel.Customer_Status
                    };


                    int customerId = 0;
                    responseOut = dbInterface.AddEditCustomer(customer, out customerId);
                    if (responseOut.status == ActionStatus.Success)
                    {
                        if (customerBranchs != null && customerBranchs.Count > 0)
                        {
                            foreach (CustomerBranchViewModel customerBranchViewModel in customerBranchs)
                            {
                                CustomerBranch customerBranch = new CustomerBranch
                                {
                                    CustomerId = customerId,
                                    CustomerBranchId= customerBranchViewModel.CustomerBranchId,
                                    BranchName = customerBranchViewModel.BranchName,
                                    ContactPersonName = customerBranchViewModel.ContactPersonName,
                                    Designation = customerBranchViewModel.Designation,
                                    Email = customerBranchViewModel.Email,
                                    MobileNo = customerBranchViewModel.MobileNo,
                                    ContactNo = customerBranchViewModel.ContactNo,
                                    Fax = customerBranchViewModel.Fax,
                                    PrimaryAddress = customerBranchViewModel.PrimaryAddress,
                                    City = customerBranchViewModel.City,
                                    StateId = customerBranchViewModel.StateId,
                                    CountryId = customerBranchViewModel.CountryId,
                                    PinCode = customerBranchViewModel.PinCode,
                                    CSTNo = customerBranchViewModel.CSTNo,
                                    TINNo = customerBranchViewModel.TINNo,
                                    PANNo = customerBranchViewModel.PANNo,
                                    GSTNo = customerBranchViewModel.GSTNo, 
                                    Status = true
                                };
                                responseOutBranch = dbInterface.AddEditCustomerBranch(customerBranch);
                            }
                        }

                        if (customerProducts != null && customerProducts.Count > 0)
                        {
                            foreach (CustomerProductViewModel customerProductViewModel in customerProducts)
                            {
                                CustomerProductMapping customerProduct = new CustomerProductMapping
                                {
                                    CustomerId = customerId,
                                    MappingId = customerProductViewModel.MappingId,
                                    ProductId = customerProductViewModel.ProductId,
                                    Status = true
                                };
                                responseOutProduct = dbInterface.AddEditCustomerProduct(customerProduct);
                            }
                        }

                    }
                    transactionscope.Complete();
                }
                catch (TransactionException ex1)
                {
                    transactionscope.Dispose();
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.ApplicationException;
                    Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex1);
                    throw ex1;
                }
                catch (Exception ex)
                {
                    transactionscope.Dispose();
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.ApplicationException;
                    Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                    throw ex;
                }
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

        public List<CustomerViewModel> GetCustomerList(string customerName, string customerCode, string mobileNo, int customerTypeid, int companyId, string customerStatus)
        {
            List<CustomerViewModel> customers = new List<CustomerViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtCustomers = sqlDbInterface.GetCustomerList(customerName, customerCode, mobileNo, customerTypeid, companyId, customerStatus);
                if (dtCustomers != null && dtCustomers.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtCustomers.Rows)
                    {
                        customers.Add(new CustomerViewModel
                        {
                            CustomerId = Convert.ToInt32(dr["CustomerId"]),
                            CustomerCode = Convert.ToString(dr["CustomerCode"]),
                            CustomerName = Convert.ToString(dr["CustomerName"]),
                            ContactPersonName = Convert.ToString(dr["ContactPersonName"]),
                            Email = Convert.ToString(dr["Email"]),
                            MobileNo = Convert.ToString(dr["MobileNo"]),
                            PrimaryAddress = Convert.ToString(dr["PrimaryAddress"]),
                            City = Convert.ToString(dr["City"]),
                            ExciseNo = Convert.ToString(dr["ExciseNo"]),
                            StateName = Convert.ToString(dr["StateName"]),
                            CustomerTypeDesc = Convert.ToString(dr["CustomerTypeDesc"]),
                            CreatedByUserName = Convert.ToString(dr["CreatedByName"]),
                            CreatedDate = Convert.ToString(dr["CreatedDate"]),
                            Customer_Status = Convert.ToBoolean(dr["Status"])
                        });
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
        public CustomerViewModel GetCustomerDetail(int customerId = 0)
        {
            CustomerViewModel customer = new CustomerViewModel();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtCompanies = sqlDbInterface.GetCustomerDetail(customerId);
                if (dtCompanies != null && dtCompanies.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtCompanies.Rows)
                    {
                        customer = new CustomerViewModel
                        {
                            CustomerId = Convert.ToInt32(dr["CustomerId"]),
                            CustomerCode = Convert.ToString(dr["CustomerCode"]),
                            CustomerName = Convert.ToString(dr["CustomerName"]),
                            ContactPersonName = Convert.ToString(dr["ContactPersonName"]),
                            Designation = Convert.ToString(dr["Designation"]),
                            Email = Convert.ToString(dr["Email"]),
                            MobileNo = Convert.ToString(dr["MobileNo"]),
                            ContactNo = Convert.ToString(dr["ContactNo"]),
                            Fax = Convert.ToString(dr["Fax"]),
                            PrimaryAddress = Convert.ToString(dr["PrimaryAddress"]),
                            City = Convert.ToString(dr["City"]),
                            StateId = Convert.ToInt32(dr["StateId"]),
                            CountryId = Convert.ToInt32(dr["CountryId"]),
                            PinCode = Convert.ToString(dr["PinCode"]),
                            CSTNo = Convert.ToString(dr["CSTNo"]),
                            TINNo = Convert.ToString(dr["TINNo"]),
                            PANNo = Convert.ToString(dr["PANNo"]),
                            GSTNo = Convert.ToString(dr["GSTNo"]),
                            ExciseNo = Convert.ToString(dr["ExciseNo"]),
                            EmployeeId = Convert.ToInt32(dr["EmployeeId"].ToString() == "" ? "0" : dr["EmployeeId"]),
                            EmployeeName = Convert.ToString(dr["EmployeeName"]),
                            CreditLimit = Convert.ToDecimal(dr["CreditLimit"].ToString() == "" ? "0" : dr["CreditLimit"]),
                            CreditDays= Convert.ToInt16(dr["CreditDays"].ToString() == "" ? "0" : dr["CreditDays"]),
                            CustomerTypeId = Convert.ToInt32(dr["CustomerTypeId"]),
                            CreatedByUserName = Convert.ToString(dr["CreatedByName"]),
                            CreatedDate = Convert.ToString(dr["CreatedDate"]),
                            Customer_Status = Convert.ToBoolean(dr["Status"])
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return customer;
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

        public List<CustomerProductViewModel> GetCustomerProductList(int customerId)
        {
            List<CustomerProductViewModel> customerProducts = new List<CustomerProductViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtCustomers = sqlDbInterface.GetCustomerProductList(customerId);
                if (dtCustomers != null && dtCustomers.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtCustomers.Rows)
                    {
                        customerProducts.Add(new CustomerProductViewModel
                        {
                            MappingId = Convert.ToInt32(dr["MappingId"]),
                            ProductId = Convert.ToInt32(dr["ProductId"]),
                            ProductName = Convert.ToString(dr["ProductName"]),
                            ProductCode = Convert.ToString(dr["ProductCode"]),
                            ProductShortDesc = Convert.ToString(dr["ProductShortDesc"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return customerProducts;
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
        public List<CustomerBranchViewModel> GetCustomerBranches( int customerId)
        {
            List<CustomerBranchViewModel> customers = new List<CustomerBranchViewModel>();
            try
            {
                List<CustomerBranch> customerList = dbInterface.GetCustomerBranchList(customerId);

                if (customerList != null && customerList.Count > 0)
                {
                    foreach (CustomerBranch customer in customerList)
                    {
                        customers.Add(new CustomerBranchViewModel { CustomerBranchId = customer.CustomerBranchId, BranchName = customer.BranchName });
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
        public CustomerBranchViewModel GetCustomerBranchDetail(long customerBranchId)
        {
            CustomerBranchViewModel customers = new CustomerBranchViewModel();
            try
            {
                CustomerBranch customerList = dbInterface.GetCustomerBranchDetail(customerBranchId);

                if (customerList != null)
                {
                    customers = new CustomerBranchViewModel {
                        PrimaryAddress = customerList.PrimaryAddress,
                        City = customerList.City,
                        StateId =Convert.ToInt32(customerList.StateId),
                        CountryId =Convert.ToInt32(customerList.CountryId),
                        PinCode = customerList.PinCode,
                        TINNo = customerList.TINNo,
                        ContactPersonName = customerList.ContactPersonName,
                        Email = customerList.Email,
                        MobileNo = customerList.MobileNo,
                        ContactNo = customerList.ContactNo,
                        Fax = customerList.Fax
                    };
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return customers;
        }

       public List<CustomerViewModel> GetCustomerAutoCompleteForPaymentModeList(string searchTerm, int companyId)
        {
            List<CustomerViewModel> customers = new List<CustomerViewModel>();
            try
            {
                List<Customer> customerList = dbInterface.GetCustomerAutoCompleteForPaymentModeList(searchTerm, companyId);

                if (customerList != null && customerList.Count > 0)
                {
                    foreach (Customer customer in customerList)
                    {
                        customers.Add(new CustomerViewModel { CustomerId = customer.CustomerId, CustomerName = customer.CustomerName });
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


        public ResponseOut ImportCustomer(CustomerViewModel customerViewModel)
        {

            ResponseOut responseOut = new ResponseOut();
            using (TransactionScope transactionscope = new TransactionScope())
            {
                try
                {

                    Customer customer = new Customer
                    {
                        CustomerId = customerViewModel.CustomerId,
                        LeadId = customerViewModel.LeadId,
                        CustomerCode = customerViewModel.CustomerCode,
                        CustomerName = customerViewModel.CustomerName,
                        ContactPersonName = customerViewModel.ContactPersonName,
                        Designation = customerViewModel.Designation,
                        Email = customerViewModel.Email,
                        MobileNo = customerViewModel.MobileNo,
                        ContactNo = customerViewModel.ContactNo,
                        Fax = customerViewModel.Fax,
                        PrimaryAddress = customerViewModel.PrimaryAddress,
                        City = customerViewModel.City,
                        StateId = customerViewModel.StateId,
                        CountryId = customerViewModel.CountryId,
                        PinCode = customerViewModel.PinCode,
                        CSTNo = customerViewModel.CSTNo,
                        TINNo = customerViewModel.TINNo,
                        PANNo = customerViewModel.PANNo,
                        GSTNo = customerViewModel.GSTNo,
                        ExciseNo = customerViewModel.ExciseNo,
                        EmployeeId = customerViewModel.EmployeeId,
                        CustomerTypeId = customerViewModel.CustomerTypeId,
                        CompanyId = customerViewModel.CompanyId,
                        CreatedBy = customerViewModel.CreatedBy,
                        CreditLimit = customerViewModel.CreditLimit,
                        CreditDays = customerViewModel.CreditDays,
                        Status = customerViewModel.Customer_Status
                    };


                    int customerId = 0;
                    responseOut = dbInterface.AddEditCustomer(customer, out customerId);
                    transactionscope.Complete();

                }
                catch (TransactionException ex1)
                {
                    transactionscope.Dispose();
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.ApplicationException;
                    Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex1);
                    throw ex1;
                }
                catch (Exception ex)
                {
                    transactionscope.Dispose();
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.ApplicationException;
                    Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                    throw ex;
                }
            }
            return responseOut;
        }


        public ResponseOut ImportCustomerBranch(CustomerBranchViewModel customerbranchViewModel)
        {

            ResponseOut responseOut = new ResponseOut();
            using (TransactionScope transactionscope = new TransactionScope())
            {
                try
                {

                    CustomerBranch customerbranch = new CustomerBranch
                    {
                        CustomerBranchId = customerbranchViewModel.CustomerBranchId,
                        BranchName = customerbranchViewModel.BranchName,
                        ContactPersonName = customerbranchViewModel.ContactPersonName,
                        Designation = customerbranchViewModel.Designation,
                        Email = customerbranchViewModel.Email,
                        MobileNo = customerbranchViewModel.MobileNo,
                        ContactNo = customerbranchViewModel.ContactNo,
                        Fax = customerbranchViewModel.Fax,
                        PrimaryAddress = customerbranchViewModel.PrimaryAddress,
                        City = customerbranchViewModel.City,
                        StateId = customerbranchViewModel.StateId,
                        CustomerId = customerbranchViewModel.CustomerId,
                        CountryId = customerbranchViewModel.CountryId,
                        PinCode = customerbranchViewModel.PinCode,
                        CSTNo = customerbranchViewModel.CSTNo,
                        TINNo = customerbranchViewModel.TINNo,
                        PANNo = customerbranchViewModel.PANNo,
                        GSTNo = customerbranchViewModel.GSTNo,
                        Status = customerbranchViewModel.CustomerBranch_Status,


                    };

                    responseOut = dbInterface.AddEditCustomerBranch(customerbranch);
                    transactionscope.Complete();
                }
                catch (TransactionException ex1)
                {
                    transactionscope.Dispose();
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.ApplicationException;
                    Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex1);
                    throw ex1;
                }
                catch (Exception ex)
                {
                    transactionscope.Dispose();
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.ApplicationException;
                    Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                    throw ex;
                }
            }
            return responseOut;
        }

    }
}

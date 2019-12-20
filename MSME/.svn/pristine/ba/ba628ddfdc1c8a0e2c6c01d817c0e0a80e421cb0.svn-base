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
 public class PurchaseInvoiceBL
    {
        DBInterface dbInterface;
        public PurchaseInvoiceBL()
        {
            dbInterface = new DBInterface();
        }
        public List<PurchaseInvoiceProductDetailViewModel> GetPIProductList(long InvoiceId)
        {
            List<PurchaseInvoiceProductDetailViewModel> piProducts = new List<PurchaseInvoiceProductDetailViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtProducts = sqlDbInterface.GetPIProductList(InvoiceId);
                if (dtProducts != null && dtProducts.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtProducts.Rows)
                    {
                        piProducts.Add(new PurchaseInvoiceProductDetailViewModel
                        {
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
            return piProducts;
        }
        public List<PurchaseInvoiceTaxDetailViewModel> GetPITaxList(long InvoiceId)
        {
            List<PurchaseInvoiceTaxDetailViewModel> poTaxes = new List<PurchaseInvoiceTaxDetailViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtTaxs = sqlDbInterface.GetPITaxList(InvoiceId);
                if (dtTaxs != null && dtTaxs.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtTaxs.Rows)
                    {
                        poTaxes.Add(new PurchaseInvoiceTaxDetailViewModel
                        {
                            InvoiceTaxDetailId = Convert.ToInt32(dr["InvoiceTaxDetailId"]),
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
            return poTaxes;
        }
        public List<PurchaseInvoiceTermsDetailViewModel> GetPITermList(long InvoiceId)
        {
            List<PurchaseInvoiceTermsDetailViewModel> piTerms = new List<PurchaseInvoiceTermsDetailViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtTerms = sqlDbInterface.GetPITermList(InvoiceId);
                if (dtTerms != null && dtTerms.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtTerms.Rows)
                    {
                        piTerms.Add(new PurchaseInvoiceTermsDetailViewModel
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
            return piTerms;
        }
       
        public ResponseOut AddEditPI(PurchaseInvoiceViewModel piViewModel, List<PurchaseInvoiceProductDetailViewModel> piProducts, List<PurchaseInvoiceTaxDetailViewModel> piTaxes, List<PurchaseInvoiceTermsDetailViewModel> piTerms)
        {
            ResponseOut responseOut = new ResponseOut();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                PurchaseInvoice pi = new PurchaseInvoice {
                    InvoiceId = piViewModel.InvoiceId,
                    InvoiceDate = Convert.ToDateTime(piViewModel.InvoiceDate),
                    POId= piViewModel.POId,
                    PONo = piViewModel.PONo,
                    VendorId = piViewModel.VendorId,
                    VendorName = piViewModel.VendorName,
                    BillingAddress = piViewModel.BillingAddress,
                    City = piViewModel.City,
                    StateId = piViewModel.StateId,
                    CountryId = piViewModel.CountryId,
                    PinCode = piViewModel.PinCode,
                    CSTNo = piViewModel.CSTNo,
                    TINNo = piViewModel.TINNo,
                    PANNo = piViewModel.PANNo,
                    GSTNo = piViewModel.GSTNo,
                    ExciseNo = piViewModel.ExciseNo,
                    ApprovalStatus=piViewModel.ApprovalStatus,
                    RefNo = string.IsNullOrEmpty(piViewModel.RefNo) ? "" : piViewModel.RefNo,
                    RefDate = string.IsNullOrEmpty(piViewModel.RefDate) ? Convert.ToDateTime("01-01-1900") : Convert.ToDateTime(piViewModel.RefDate),
                    BasicValue = piViewModel.BasicValue,
                    TotalValue = piViewModel.TotalValue,
                    Remarks= string.IsNullOrEmpty(piViewModel.Remarks)?"":piViewModel.Remarks,
                    FinYearId = piViewModel.FinYearId,
                    CompanyId = piViewModel.CompanyId,
                    CreatedBy = piViewModel.CreatedBy
                };
                
                List<PurchaseInvoiceProductDetail> piProductList = new List<PurchaseInvoiceProductDetail>();
                if (piProducts != null && piProducts.Count > 0)
                {
                    foreach (PurchaseInvoiceProductDetailViewModel item in piProducts)
                    {
                        piProductList.Add(new PurchaseInvoiceProductDetail
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
               
                List<PurchaseInvoiceTaxDetail> piTaxList = new List<PurchaseInvoiceTaxDetail>();
                if (piTaxes != null && piTaxes.Count > 0)
                {
                    foreach (PurchaseInvoiceTaxDetailViewModel item in piTaxes)
                    {
                        piTaxList.Add(new PurchaseInvoiceTaxDetail
                        {
                            TaxId = item.TaxId,
                            TaxName = item.TaxName,
                            TaxPercentage = item.TaxPercentage,
                            TaxAmount = item.TaxAmount
                        });
                    }
                }
                List<PurchaseInvoiceTermsDetail> piTermList = new List<PurchaseInvoiceTermsDetail>();
                if (piTerms != null && piTerms.Count > 0)
                {
                    foreach (PurchaseInvoiceTermsDetailViewModel item in piTerms)
                    {
                        piTermList.Add(new PurchaseInvoiceTermsDetail
                        {
                            TermDesc = item.TermDesc,
                            TermSequence = item.TermSequence
                        });
                    }
                }

                responseOut = sqlDbInterface.AddEditPI(pi,piProductList,piTaxList, piTermList);
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

        public List<PurchaseInvoiceViewModel> GetPIList(string piNo, string vendorName, string refNo, string fromDate, string toDate, int companyId, string approvalStatus="", string displayType="")
        {
            List<PurchaseInvoiceViewModel> pos = new List<PurchaseInvoiceViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtPIs = sqlDbInterface.GetPIList(piNo, vendorName, refNo, fromDate, toDate, companyId, approvalStatus, displayType);
                if (dtPIs != null && dtPIs.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtPIs.Rows)
                    {
                        pos.Add(new PurchaseInvoiceViewModel
                        {
                            InvoiceId = Convert.ToInt32(dr["InvoiceId"]),
                            InvoiceNo = Convert.ToString(dr["InvoiceNo"]),
                            InvoiceDate = Convert.ToString(dr["InvoiceDate"]),
                            VendorId = Convert.ToInt32(dr["VendorId"]),
                            VendorName = Convert.ToString(dr["VendorName"]),
                            VendorCode = Convert.ToString(dr["VendorCode"]),
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
            return pos;
        }
        public PurchaseInvoiceViewModel GetPIDetail(long invoiceId = 0)
        {
            PurchaseInvoiceViewModel pi = new PurchaseInvoiceViewModel();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtpos = sqlDbInterface.GetPIDetail(invoiceId);
                if (dtpos != null && dtpos.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtpos.Rows)
                    {
                        pi = new PurchaseInvoiceViewModel
                        {
                          InvoiceId = Convert.ToInt32(dr["InvoiceId"]),
                          InvoiceNo =Convert.ToString(dr["InvoiceNo"]),
                          InvoiceDate = Convert.ToString(dr["InvoiceDate"]),
                          POId =Convert.ToInt32(dr["POId"]),
                          PONo = Convert.ToString(dr["PONo"]),
                          VendorId = Convert.ToInt32(dr["VendorId"]),
                          VendorCode = Convert.ToString(dr["VendorCode"]),
                          VendorName = Convert.ToString(dr["VendorName"]),
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
                          BasicValue = Convert.ToDecimal(dr["BasicValue"]),
                          TotalValue = Convert.ToDecimal(dr["TotalValue"]),
                          Remarks = Convert.ToString(dr["Remarks"]),
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
            return pi;
        }

        public DataTable GetPIDetailDataTable(long piId = 0)
        {
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            DataTable dtPI = new DataTable();
            try
            {
                dtPI = sqlDbInterface.GetPIDetail(piId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dtPI;
        }
        public DataTable GetPIProductListDataTable(long piId)
        {
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            DataTable dtProducts = new DataTable();
            try
            {
                dtProducts = sqlDbInterface.GetPIProductList(piId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dtProducts;
        }
        public DataTable GetPITermListDataTable(long piId)
        {
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            DataTable dtTerms = new DataTable();
            try
            {
                dtTerms = sqlDbInterface.GetPITermList(piId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dtTerms;
        }
        public DataTable GetPITaxDataTable(long piId)
        {

            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            DataTable dtTaxs = new DataTable();
            try
            {
                dtTaxs = sqlDbInterface.GetPITaxList(piId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dtTaxs;
        }


        public ResponseOut CancelPI(PurchaseInvoiceViewModel purchaseinvoiceViewModel)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            { 
                PurchaseInvoice pi = new PurchaseInvoice
                {
                    InvoiceId = purchaseinvoiceViewModel.InvoiceId,
                    CancelStatus = "Cancel",
                    ApprovalStatus = "Cancelled",
                    CreatedBy = purchaseinvoiceViewModel.CreatedBy,
                    CancelReason = purchaseinvoiceViewModel.CancelReason
                };
                responseOut = dbInterface.CancelPI(pi);
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

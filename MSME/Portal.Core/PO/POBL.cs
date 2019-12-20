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
 public class POBL
    {
        DBInterface dbInterface;
        public POBL()
        {
            dbInterface = new DBInterface();
        }
        public List<POProductViewModel> GetPOProductList(long poId)
        {
            List<POProductViewModel> quotationProducts = new List<POProductViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtProducts = sqlDbInterface.GetPOProductList(poId);
                if (dtProducts != null && dtProducts.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtProducts.Rows)
                    {
                        quotationProducts.Add(new POProductViewModel
                        {
                            POProductDetailId = Convert.ToInt32(dr["POProductDetailId"]),
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
        public List<POTaxViewModel> GetPOTaxList(long poId)
        {
            List<POTaxViewModel> poTaxes = new List<POTaxViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtTaxs = sqlDbInterface.GetPOTaxList(poId);
                if (dtTaxs != null && dtTaxs.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtTaxs.Rows)
                    {
                        poTaxes.Add(new POTaxViewModel
                        {
                            POTaxDetailId = Convert.ToInt32(dr["POTaxDetailId"]),
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
        public List<POTermViewModel> GetPoTermList(long poId)
        {
            List<POTermViewModel> poTerms = new List<POTermViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtTerms = sqlDbInterface.GetPOTermList(poId);
                if (dtTerms != null && dtTerms.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtTerms.Rows)
                    {
                        poTerms.Add(new POTermViewModel
                        {
                            POTermDetailId = Convert.ToInt32(dr["POTermDetailId"]),
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
            return poTerms;
        }

        public List<POTermViewModel> GetPOTermsList(long poId)
        {
            List<POTermViewModel> poTerms = new List<POTermViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtTerms = sqlDbInterface.GetPOTermList(poId);
                if (dtTerms != null && dtTerms.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtTerms.Rows)
                    {
                        poTerms.Add(new POTermViewModel
                        {
                            POTermDetailId = Convert.ToInt32(dr["POTermDetailId"]),
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
            return poTerms;
        }

        public ResponseOut AddEditPO(POViewModel poViewModel, List<POProductViewModel> poProducts, List<POTaxViewModel> poTaxes, List<POTermViewModel> poTerms)
        {
            ResponseOut responseOut = new ResponseOut();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                PO po = new PO
                {
                    POId = poViewModel.POId,
                    PODate = Convert.ToDateTime(poViewModel.PODate),
                    VendorId = poViewModel.VendorId,
                    VendorName = poViewModel.VendorName,
                    BillingAddress = poViewModel.BillingAddress,
                    ShippingAddress= poViewModel.ShippingAddress,
                    City = poViewModel.City,
                    StateId = poViewModel.StateId,
                    CountryId = poViewModel.CountryId,
                    PinCode = poViewModel.PinCode,
                    CSTNo = poViewModel.CSTNo,
                    TINNo = poViewModel.TINNo,
                    PANNo = poViewModel.PANNo,
                    GSTNo = poViewModel.GSTNo,
                    ExciseNo = poViewModel.ExciseNo,
                    ApprovalStatus=poViewModel.ApprovalStatus,
                    RefNo = string.IsNullOrEmpty(poViewModel.RefNo) ? "" : poViewModel.RefNo,
                    RefDate = string.IsNullOrEmpty(poViewModel.RefDate) ? Convert.ToDateTime("01-01-1900") : Convert.ToDateTime(poViewModel.RefDate),
                    BasicValue = poViewModel.BasicValue,
                    TotalValue = poViewModel.TotalValue,
                    FreightValue= poViewModel.FreightValue,
                    LoadingValue= poViewModel.LoadingValue,
                    Remarks1 = string.IsNullOrEmpty(poViewModel.Remarks1)?null:poViewModel.Remarks1,
                    Remarks2 = string.IsNullOrEmpty(poViewModel.Remarks2)?null:poViewModel.Remarks2,
                    FinYearId = poViewModel.FinYearId,
                    CompanyId = poViewModel.CompanyId,
                    CreatedBy = poViewModel.CreatedBy

                };
                List<POProductDetail> poProductList = new List<POProductDetail>();
                if (poProducts != null && poProducts.Count > 0)
                {
                    foreach (POProductViewModel item in poProducts)
                    {
                        poProductList.Add(new POProductDetail
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

                List<POTaxDetail> poTaxList = new List<POTaxDetail>();
                if (poTaxes != null && poTaxes.Count > 0)
                {
                    foreach (POTaxViewModel item in poTaxes)
                    {
                        poTaxList.Add(new POTaxDetail
                        {
                            TaxId = item.TaxId,
                            TaxName = item.TaxName,
                            TaxPercentage = item.TaxPercentage,
                            TaxAmount = item.TaxAmount
                        });
                    }
                }
                List<POTermsDetail> poTermList = new List<POTermsDetail>();
                if (poTerms != null && poTerms.Count > 0)
                {
                    foreach (POTermViewModel item in poTerms)
                    {
                        poTermList.Add(new POTermsDetail
                        {
                            TermDesc = item.TermDesc,
                            TermSequence = item.TermSequence
                        });
                    }
                }

                responseOut = sqlDbInterface.AddEditPO(po, poProductList,poTaxList,poTermList);
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

        public List<POViewModel> GetPOList(string poNo, string vendorName, string refNo, string fromDate, string toDate,string approvalStatus, int companyId, string displayType = "")
        {
            List<POViewModel> pos = new List<POViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtPOs = sqlDbInterface.GetPOList(poNo, vendorName, refNo, Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate), approvalStatus, companyId, displayType);
                if (dtPOs != null && dtPOs.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtPOs.Rows)
                    {
                        pos.Add(new POViewModel
                        {
                            POId = Convert.ToInt32(dr["POId"]),
                            PONo = Convert.ToString(dr["PONo"]),
                            PODate = Convert.ToString(dr["PODate"]),
                            VendorName = Convert.ToString(dr["VendorName"]),
                            VendorId = Convert.ToInt32(dr["VendorId"]),
                            VendorCode = Convert.ToString(dr["VendorCode"]),
                            City = Convert.ToString(dr["City"]),
                            StateName = Convert.ToString(dr["StateName"]),
                            RefNo = Convert.ToString(dr["RefNo"]),
                            RefDate = Convert.ToString(dr["RefDate"]),
                            ApprovalStatus = Convert.ToString(dr["ApprovalStatus"]),
                            BasicValue = Convert.ToDecimal(dr["BasicValue"]),
                            TotalValue = Convert.ToDecimal(dr["TotalValue"]),
                            PORevisedStatus = Convert.ToBoolean(dr["PORevisedStatus"]),
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
        public POViewModel GetPODetail(long poId = 0)
        {
            
            POViewModel po = new POViewModel();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtpos = sqlDbInterface.GetPODetail(poId);
                if (dtpos != null && dtpos.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtpos.Rows)
                    {
                        po = new POViewModel
                        {
                            POId = Convert.ToInt32(dr["POId"]),
                            PONo = Convert.ToString(dr["PONo"]),
                            PODate = Convert.ToString(dr["PODate"]),
                            VendorId = Convert.ToInt32(dr["VendorId"]),
                            VendorCode = Convert.ToString(dr["VendorCode"]),
                            VendorName = Convert.ToString(dr["VendorName"]),
                            BillingAddress = Convert.ToString(dr["BillingAddress"]),
                            ShippingAddress=Convert.ToString(dr["ShippingAddress"]),
                            City = Convert.ToString(dr["City"]),
                            StateId = Convert.ToInt32(dr["StateId"]),
                            CountryId = Convert.ToInt32(dr["CountryId"]),
                            PinCode = Convert.ToString(dr["PinCode"]),
                            CSTNo = Convert.ToString(dr["CSTNo"]),
                            TINNo = Convert.ToString(dr["TINNo"]),
                            PANNo = Convert.ToString(dr["PANNo"]),
                            GSTNo = Convert.ToString(dr["GSTNo"]),
                            RefNo = Convert.ToString(dr["RefNo"]),
                            ApprovalStatus=Convert.ToString(dr["ApprovalStatus"]),
                            RefDate = Convert.ToString(dr["RefDate"]),
                            BasicValue = Convert.ToDecimal(dr["BasicValue"]),
                            TotalValue = Convert.ToDecimal(dr["TotalValue"]),
                            FreightValue=Convert.ToDecimal(dr["FreightValue"]),
                            LoadingValue=Convert.ToDecimal(dr["LoadingValue"]),
                            Remarks1=Convert.ToString(dr["Remarks1"]),
                            Remarks2 = Convert.ToString(dr["Remarks2"]),
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
            return po;
        }
        public DataTable GetPODetailDataTable(long poId = 0)
        {
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            DataTable dtPO = new DataTable();
            try
            {
                dtPO = sqlDbInterface.GetPODetail(poId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dtPO;
        }


        public DataTable GetPOProductListDataTable(long poId)
        {
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            DataTable dtProducts = new DataTable();
            try
            {
                dtProducts = sqlDbInterface.GetPOProductList(poId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dtProducts;
        }
        public DataTable GetPOTermListDataTable(long poId)
        {

            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            DataTable dtTerms = new DataTable();
            try
            {
                dtTerms = sqlDbInterface.GetPOTermList(poId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dtTerms;
        }

        public DataTable GetPOTaxDataTable(long poId)
        {

            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            DataTable dtTaxs = new DataTable();
            try
            {
                dtTaxs = sqlDbInterface.GetPOTaxList(poId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dtTaxs;
        }

        public ResponseOut AddRevisedPO(POViewModel poViewModel, List<POProductViewModel> poProducts, List<POTaxViewModel> poTaxes, List<POTermViewModel> poTerms)
        {
            ResponseOut responseOut = new ResponseOut();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                PO po = new PO
                {
                    POId = poViewModel.POId,
                    PONo = poViewModel.PONo,
                    PODate = Convert.ToDateTime(poViewModel.PODate),
                    VendorId = poViewModel.VendorId,
                    VendorName = poViewModel.VendorName,
                    BillingAddress = poViewModel.BillingAddress,
                    ShippingAddress = poViewModel.ShippingAddress,
                    City = poViewModel.City,
                    StateId = poViewModel.StateId,
                    CountryId = poViewModel.CountryId,
                    PinCode = poViewModel.PinCode,
                    CSTNo = poViewModel.CSTNo,
                    TINNo = poViewModel.TINNo,
                    PANNo = poViewModel.PANNo,
                    GSTNo = poViewModel.GSTNo,
                    ExciseNo = poViewModel.ExciseNo,
                    ApprovalStatus=poViewModel.ApprovalStatus,
                    RefNo = string.IsNullOrEmpty(poViewModel.RefNo) ? "" : poViewModel.RefNo,
                    RefDate = string.IsNullOrEmpty(poViewModel.RefDate) ? Convert.ToDateTime("01-01-1900") : Convert.ToDateTime(poViewModel.RefDate),
                    BasicValue = poViewModel.BasicValue,
                    TotalValue = poViewModel.TotalValue,
                    FreightValue = poViewModel.FreightValue,
                    LoadingValue = poViewModel.LoadingValue,
                    Remarks1 = string.IsNullOrEmpty(poViewModel.Remarks1) ? null : poViewModel.Remarks1,
                    Remarks2 = string.IsNullOrEmpty(poViewModel.Remarks2) ? null : poViewModel.Remarks2,
                    FinYearId = poViewModel.FinYearId,
                    CompanyId = poViewModel.CompanyId,
                    CreatedBy = poViewModel.CreatedBy

                };
                List<POProductDetail> poProductList = new List<POProductDetail>();
                if (poProducts != null && poProducts.Count > 0)
                {
                    foreach (POProductViewModel item in poProducts)
                    {
                        poProductList.Add(new POProductDetail
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

                List<POTaxDetail> poTaxList = new List<POTaxDetail>();
                if (poTaxes != null && poTaxes.Count > 0)
                {
                    foreach (POTaxViewModel item in poTaxes)
                    {
                        poTaxList.Add(new POTaxDetail
                        {
                            TaxId = item.TaxId,
                            TaxName = item.TaxName,
                            TaxPercentage = item.TaxPercentage,
                            TaxAmount = item.TaxAmount
                        });
                    }
                }
                List<POTermsDetail> poTermList = new List<POTermsDetail>();
                if (poTerms != null && poTerms.Count > 0)
                {
                    foreach (POTermViewModel item in poTerms)
                    {
                        poTermList.Add(new POTermsDetail
                        {
                            TermDesc = item.TermDesc,
                            TermSequence = item.TermSequence
                        });
                    }
                }

                responseOut = sqlDbInterface.AddRevisedPO(po, poProductList, poTaxList, poTermList);
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

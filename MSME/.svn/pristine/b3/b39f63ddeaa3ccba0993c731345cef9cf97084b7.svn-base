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
    public class TaxBL
    {
        DBInterface dbInterface;
        public TaxBL()
        {
            dbInterface = new DBInterface();
        }
        public ResponseOut AddEditTax(TaxViewModel taxViewModel)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                Tax tax = new Tax
                {
                    TaxId = taxViewModel.TaxId,
                    TaxName = taxViewModel.TaxName,
                    CompanyId = taxViewModel.CompanyId,
                    TaxType = taxViewModel.TaxType,
                    TaxSubType = taxViewModel.TaxSubType,
                    FormTypeId = taxViewModel.FormTypeId,
                    TaxPercentage = taxViewModel.TaxPercentage,
                    CFormApplicable = taxViewModel.CFormApplicable,
                    WithOutCFormTaxPercentae = taxViewModel.WithOutCFormTaxPercentae,
                    TaxGLCode = taxViewModel.TaxGLCode,                    
                    TaxSLCode = taxViewModel.TaxSLCode,
                    SurchargeName_1= Convert.ToString(taxViewModel.SurchargeName_1==""?" ": taxViewModel.SurchargeName_1),
                    SurchargeName_2 = Convert.ToString(taxViewModel.SurchargeName_2 == "" ? " " : taxViewModel.SurchargeName_2),
                    SurchargeName_3 = Convert.ToString(taxViewModel.SurchargeName_3 == "" ? " " : taxViewModel.SurchargeName_3),
                    SurchargePercentage_1 = string.IsNullOrEmpty(taxViewModel.SurchargePercentage_1.ToString())?0: Convert.ToDecimal(taxViewModel.SurchargePercentage_1),
                    SurchargePercentage_2 = string.IsNullOrEmpty(taxViewModel.SurchargePercentage_2.ToString()) ? 0 : Convert.ToDecimal(taxViewModel.SurchargePercentage_2),
                    SurchargePercentage_3 = string.IsNullOrEmpty(taxViewModel.SurchargePercentage_3.ToString()) ? 0 : Convert.ToDecimal(taxViewModel.SurchargePercentage_3),
                    CreatedBy = taxViewModel.CreatedBy,
                    Status = taxViewModel.Tax_Status
                };
                responseOut = dbInterface.AddEditTax(tax);
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



        public List<TaxViewModel> GetTaxList(string taxName = "",  string taxType = "0", string taxSubType = "0",  int companyId = 0, string status = "")
        {
            List<TaxViewModel> taxes = new List<TaxViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtTaxes = sqlDbInterface.GetTaxList(taxName, taxType, taxSubType, companyId, status);
                if (dtTaxes != null && dtTaxes.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtTaxes.Rows)
                    {
                        taxes.Add(new TaxViewModel
                        {
                            TaxId = Convert.ToInt32(dr["TaxId"]),
                            TaxName = Convert.ToString(dr["TaxName"]),
                            TaxPercentage = Convert.ToInt32(dr["TaxPercentage"]),
                            TaxType = Convert.ToString(dr["TaxType"]),
                            TaxSubType = Convert.ToString(dr["TaxSubType"]),
                            FormTypeDesc = Convert.ToString(dr["FormTypeDesc"]),
                            CompanyId = Convert.ToInt32(dr["CompanyId"]), 
                            CreatedBy = Convert.ToInt32(dr["CreatedBy"].ToString() == "" ? "0" : dr["CreatedBy"].ToString()),
                            CreatedName = Convert.ToString(dr["CreatedByName"]),
                            CreatedDate = Convert.ToString(dr["CreatedDate"]),
                            ModifiedBy = Convert.ToInt32(dr["ModifiedBy"].ToString() == "" ? "0" : dr["ModifiedBy"].ToString()),
                            ModifiedName = Convert.ToString(dr["ModifiedByName"]),
                            ModifiedDate = Convert.ToString(dr["ModifiedDate"]),
                            Tax_Status = Convert.ToBoolean(dr["Status"]),
                            SurchargeName_1 = Convert.ToString(dr["SurchargeName_1"]),
                            SurchargeName_2 = Convert.ToString(dr["SurchargeName_2"]),
                            SurchargeName_3 = Convert.ToString(dr["SurchargeName_3"]),
                            SurchargePercentage_1 = dr["SurchargePercentage_1"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["SurchargePercentage_1"]),
                            SurchargePercentage_2 = dr["SurchargePercentage_2"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["SurchargePercentage_2"]),
                            SurchargePercentage_3 = dr["SurchargePercentage_3"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["SurchargePercentage_3"]),

                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return taxes;
        }

        public TaxViewModel GetTaxDetail(int taxId = 0)
        {
            TaxViewModel tax = new TaxViewModel();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtTaxes = sqlDbInterface.GetTaxDetail(taxId);
                if (dtTaxes != null && dtTaxes.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtTaxes.Rows)
                    {
                        tax = new TaxViewModel
                        {
                            TaxId = Convert.ToInt32(dr["TaxId"]),
                            TaxName = Convert.ToString(dr["TaxName"]),
                            TaxPercentage = Convert.ToDecimal(dr["TaxPercentage"]),
                            TaxType = Convert.ToString(dr["TaxType"]),
                            TaxSubType = Convert.ToString(dr["TaxSubType"]),
                            FormTypeId = Convert.ToInt32(dr["FormTypeId"].ToString() == "" ? "0" : dr["FormTypeId"].ToString()),
                            CFormApplicable = Convert.ToBoolean(dr["CFormApplicable"]),
                            WithOutCFormTaxPercentae = Convert.ToDecimal(dr["WithOutCFormTaxPercentae"].ToString() == "" ? "0" : dr["WithOutCFormTaxPercentae"].ToString()), 
                            TaxGLCode = Convert.ToString(dr["TaxGLCode"]),
                            TaxSLCode = Convert.ToString(dr["TaxSLCode"]),
                            CreatedBy = Convert.ToInt32(dr["CreatedBy"].ToString() == "" ? "0" : dr["CreatedBy"].ToString()),
                            CreatedName = Convert.ToString(dr["CreatedByName"]),
                            CreatedDate = Convert.ToString(dr["CreatedDate"]),
                            ModifiedBy = Convert.ToInt32(dr["ModifiedBy"].ToString() == "" ? "0" : dr["ModifiedBy"].ToString()),
                            ModifiedName = Convert.ToString(dr["ModifiedByName"]),
                            ModifiedDate = Convert.ToString(dr["ModifiedDate"]),
                            Tax_Status = Convert.ToBoolean(dr["Status"]),
                            SurchargeName_1 = Convert.ToString(dr["SurchargeName_1"]),
                            SurchargeName_2 = Convert.ToString(dr["SurchargeName_2"]),
                            SurchargeName_3 = Convert.ToString(dr["SurchargeName_3"]),
                            SurchargePercentage_1 = dr["SurchargePercentage_1"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["SurchargePercentage_1"]),
                            SurchargePercentage_2 = dr["SurchargePercentage_2"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["SurchargePercentage_2"]),
                            SurchargePercentage_3 = dr["SurchargePercentage_3"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["SurchargePercentage_3"]),
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return tax;
        }
        public List<TaxViewModel> GetTaxAutoCompleteList(string searchTerm,string taxtaxSubType, int companyId)
        {
            List<TaxViewModel> taxes = new List<TaxViewModel>();
            try
            {
                List<Tax> taxList = dbInterface.GetTaxAutoCompleteList(searchTerm, taxtaxSubType, companyId);

                if (taxList != null && taxList.Count > 0)
                {
                    foreach (Tax tax in taxList)
                    {
                        taxes.Add(new TaxViewModel { TaxId = tax.TaxId, TaxName = tax.TaxName, TaxPercentage = tax.TaxPercentage, SurchargeName_1=tax.SurchargeName_1,SurchargePercentage_1=tax.SurchargePercentage_1, SurchargeName_2 = tax.SurchargeName_2, SurchargePercentage_2 = tax.SurchargePercentage_2, SurchargeName_3 = tax.SurchargeName_3, SurchargePercentage_3 = tax.SurchargePercentage_3 });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return taxes;
        }


    }
}









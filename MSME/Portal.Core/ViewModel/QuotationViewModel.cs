using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Core.ViewModel
{
   public class QuotationViewModel
    {


        public long QuotationId { get; set; }
        public string QuotationNo { get; set; }
        public string QuotationDate { get; set; }
        public int CustomerId { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string ContactPersonName { get; set; }
        public string MobileNo { get; set; }
        public string ContactNo { get; set; }
        public string Fax { get; set; }
        public string BillingAddress { get; set; }
        public string City { get; set; }
        public int StateId { get; set; }
        public string StateName { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public string PinCode { get; set; }
        public string CSTNo { get; set; }
        public string TINNo { get; set; }
        public string PANNo { get; set; }
        public string GSTNo { get; set; }
        public string ExciseNo { get; set; }
        public string RefNo { get; set; }
        public string RefDate { get; set; }
        public decimal BasicValue { get; set; }
        public decimal TotalValue { get; set; }
        public int FinYearId { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }

        public string CompanyPhone { get; set; }
        public string CompanyEmail { get; set; }
        public string CompanyFax { get; set; }
        public string CompanyWebsite { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyCity { get; set; }
        public string CompanyZipCode { get; set; }
        public string CompanyPANNo { get; set; }
        public string CompanyTINNo { get; set; }
        public string CompanyTanNo { get; set; }
        public string CompanyServiceTaxNo { get; set; }
        
        public string CompanyCountryName { get; set; }
        public string CompanyStateName { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedByUserName { get; set; }
        public string CreatedDate { get; set; }
        public int Modifiedby { get; set; }
        public string ModifiedByUserName { get; set; }
        public string ModifiedDate { get; set; }
        public bool QuotationRevisedStatus { get; set; }
        public string ApprovalStatus { get; set; }
        public int ApprovedBy { get; set; }
        public string ApprovedByUserName { get; set; }
        public string ApprovedDate { get; set; }
        public string RejectionStatus { get; set; }
        public int RejectedBy { get; set; }
        public string RejectedByUserName { get; set; }
        public string RejectedDate { get; set; }
        public string RejectedReason { get; set; }

        public List<QuotationProductViewModel> QuotationProductList { get; set; }
        public List<QuotationTaxViewModel> QuotationTaxList{ get; set; }
        public List<QuotationTermViewModel> QuotationTermList { get; set; }
        public string message { get; set; }
        public string status { get; set; }

        
    }
    public class QuotationProductViewModel
    {
        public long QuotationProductDetailId { get; set; }
        public int SequenceNo { get; set; }
        public int QuotationId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string ProductShortDesc { get; set; }
        public string UOMName { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TaxPercentage { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalPrice { get; set; }
        public long TaxId { get; set; }
        public string TaxName { get; set; }
        public string message { get; set; }
        public string status { get; set; }

        public string SurchargeName_1 { get; set; }
        public decimal SurchargePercentage_1 { get; set; }
        public decimal SurchargeAmount_1 { get; set; }
        public string SurchargeName_2 { get; set; }
        public decimal SurchargePercentage_2 { get; set; }
        public decimal SurchargeAmount_2 { get; set; }
        public string SurchargeName_3 { get; set; }
        public decimal SurchargePercentage_3 { get; set; }
        public decimal SurchargeAmount_3 { get; set; }

    }
    public class QuotationTaxViewModel
    {
        public long QuotationTaxDetailId { get; set; }
        public int TaxSequenceNo { get; set; }
        public int QuotationId { get; set; }
        public long TaxId { get; set; }
        public string TaxName { get; set; }
        public decimal TaxPercentage { get; set; }
        public decimal TaxAmount { get; set; }
        public string message { get; set; }
        public string status { get; set; }


        public string SurchargeName_1 { get; set; }
        public decimal SurchargePercentage_1 { get; set; }
        public decimal SurchargeAmount_1 { get; set; }
        public string SurchargeName_2 { get; set; }
        public decimal SurchargePercentage_2 { get; set; }
        public decimal SurchargeAmount_2 { get; set; }
        public string SurchargeName_3 { get; set; }
        public decimal SurchargePercentage_3 { get; set; }
        public decimal SurchargeAmount_3 { get; set; }
    }
    public class QuotationTermViewModel
    {
        public long QuotationTermDetailId { get; set; }
        public int QuotationId { get; set; }
        public string TermDesc { get; set; }
        public int TermSequence { get; set; }
        public string message { get; set; }
        public string status { get; set; }

    }
}

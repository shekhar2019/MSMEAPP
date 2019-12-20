//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Portal.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class SO
    {
        public long SOId { get; set; }
        public string SONo { get; set; }
        public Nullable<System.DateTime> SODate { get; set; }
        public Nullable<long> QuotationId { get; set; }
        public string QuotationNo { get; set; }
        public Nullable<int> CompanyBranchId { get; set; }
        public string CurrencyCode { get; set; }
        public Nullable<int> CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string ContactPerson { get; set; }
        public string BillingAddress { get; set; }
        public string City { get; set; }
        public Nullable<int> StateId { get; set; }
        public Nullable<int> CountryId { get; set; }
        public string PinCode { get; set; }
        public string CSTNo { get; set; }
        public string TINNo { get; set; }
        public string PANNo { get; set; }
        public string GSTNo { get; set; }
        public string ExciseNo { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string ContactNo { get; set; }
        public string Fax { get; set; }
        public Nullable<int> ConsigneeId { get; set; }
        public string ConsigneeName { get; set; }
        public string ShippingContactPerson { get; set; }
        public string ShippingBillingAddress { get; set; }
        public string ShippingCity { get; set; }
        public Nullable<int> ShippingStateId { get; set; }
        public Nullable<int> ShippingCountryId { get; set; }
        public string ShippingPinCode { get; set; }
        public string ShippingTINNo { get; set; }
        public string ShippingEmail { get; set; }
        public string ShippingMobileNo { get; set; }
        public string ShippingContactNo { get; set; }
        public string ShippingFax { get; set; }
        public string RefNo { get; set; }
        public Nullable<System.DateTime> RefDate { get; set; }
        public Nullable<decimal> BasicValue { get; set; }
        public Nullable<decimal> TotalValue { get; set; }
        public Nullable<decimal> FreightValue { get; set; }
        public Nullable<decimal> LoadingValue { get; set; }
        public Nullable<int> PayToBookId { get; set; }
        public string Remarks1 { get; set; }
        public string Remarks2 { get; set; }
        public Nullable<int> FinYearId { get; set; }
        public Nullable<int> CompanyId { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<bool> SORevisedStatus { get; set; }
        public string SOStatus { get; set; }
        public string ApprovalStatus { get; set; }
        public Nullable<int> ApprovedBy { get; set; }
        public Nullable<System.DateTime> ApprovedDate { get; set; }
        public string RejectionStatus { get; set; }
        public Nullable<int> RejectedBy { get; set; }
        public Nullable<System.DateTime> RejectedDate { get; set; }
        public string RejectedReason { get; set; }
        public Nullable<int> SOSequence { get; set; }
        public Nullable<bool> ReverseChargeApplicable { get; set; }
        public Nullable<decimal> ReverseChargeAmount { get; set; }
        public Nullable<decimal> InsuranceValue { get; set; }
        public Nullable<decimal> FreightCGST_Amt { get; set; }
        public Nullable<decimal> FreightSGST_Amt { get; set; }
        public Nullable<decimal> FreightIGST_Amt { get; set; }
        public Nullable<decimal> LoadingCGST_Amt { get; set; }
        public Nullable<decimal> LoadingSGST_Amt { get; set; }
        public Nullable<decimal> LoadingIGST_Amt { get; set; }
        public Nullable<decimal> InsuranceCGST_Amt { get; set; }
        public Nullable<decimal> InsuranceSGST_Amt { get; set; }
        public Nullable<decimal> InsuranceIGST_Amt { get; set; }
        public Nullable<decimal> FreightCGST_Perc { get; set; }
        public Nullable<decimal> FreightSGST_Perc { get; set; }
        public Nullable<decimal> FreightIGST_Perc { get; set; }
        public Nullable<decimal> LoadingCGST_Perc { get; set; }
        public Nullable<decimal> LoadingSGST_Perc { get; set; }
        public Nullable<decimal> LoadingIGST_Perc { get; set; }
        public Nullable<decimal> InsuranceCGST_Perc { get; set; }
        public Nullable<decimal> InsuranceSGST_Perc { get; set; }
        public Nullable<decimal> InsuranceIGST_Perc { get; set; }
    }
}

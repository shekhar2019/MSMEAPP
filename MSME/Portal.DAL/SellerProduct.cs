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
    
    public partial class SellerProduct
    {
        public long SellerProductDetailId { get; set; }
        public Nullable<long> ParentClassId { get; set; }
        public Nullable<long> CategoryId { get; set; }
        public Nullable<long> SubCategoryId { get; set; }
        public Nullable<long> ProductId { get; set; }
        public string ProductName { get; set; }
        public string SpecificationName { get; set; }
        public string SpecificationValue { get; set; }
        public Nullable<int> UomId { get; set; }
        public Nullable<int> BrandId { get; set; }
        public string BrandName { get; set; }
        public string ProductCode { get; set; }
        public Nullable<long> SellerId { get; set; }
        public string SellerCode { get; set; }
        public Nullable<decimal> SalePrice { get; set; }
        public Nullable<byte> GSTID { get; set; }
        public Nullable<byte> GSTPercentageID { get; set; }
        public Nullable<byte> MOQ { get; set; }
        public Nullable<byte> DeliveryStatusID { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<decimal> MinimumorderQty { get; set; }
    }
}

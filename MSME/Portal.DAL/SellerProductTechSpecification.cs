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
    
    public partial class SellerProductTechSpecification
    {
        public long Id { get; set; }
        public long ProductTechSpecId { get; set; }
        public long SellerProductId { get; set; }
        public string ProductTechSpecValue { get; set; }
        public Nullable<int> UomId { get; set; }
    }
}
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
    
    public partial class Book
    {
        public int BookId { get; set; }
        public string BookName { get; set; }
        public string BookCode { get; set; }
        public string BookType { get; set; }
        public string GLCode { get; set; }
        public Nullable<int> CompanyId { get; set; }
        public string BankBranch { get; set; }
        public string BankAccountNo { get; set; }
        public string IFSC { get; set; }
        public Nullable<decimal> OverDraftLimit { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> Modifiedby { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<bool> Status { get; set; }
    }
}

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
    
    public partial class UserEmailSetting
    {
        public long SettingId { get; set; }
        public Nullable<int> UserId { get; set; }
        public string SmtpUser { get; set; }
        public string SmtpPass { get; set; }
        public string SmtpServer { get; set; }
        public Nullable<bool> EnableSsl { get; set; }
        public Nullable<int> SmtpPort { get; set; }
        public string SmtpDisplayName { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
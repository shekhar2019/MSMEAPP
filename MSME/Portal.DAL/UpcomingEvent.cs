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
    
    public partial class UpcomingEvent
    {
        public int Id { get; set; }
        public string UpcomingEvents { get; set; }
        public string Image { get; set; }
        public string Dated { get; set; }
        public string UpcomingDescription { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<int> ModifyBy { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public Nullable<bool> UpcomingEventStatus { get; set; }
    }
}

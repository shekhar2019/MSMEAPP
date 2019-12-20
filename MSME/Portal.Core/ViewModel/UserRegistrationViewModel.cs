using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Core.ViewModel
{
  public class UserRegistrationViewModel
    {
        public long UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ContactNo { get; set; }
        public string AlternateContactno { get; set; }
        public string WhatsaapPhone { get; set; }
        public string Fax { get; set; }
        public int StateId { get; set; }
        public string City { get; set; }
        public string SubCity { get; set; }
        public string Landmark { get; set; }
        public string BuildingNo { get; set; }
        public string PINCode { get; set; }
        public string GSTNo { get; set; }
        public string PANNo { get; set; }
        public string UdyogAadhaarNo { get; set; }
        public string KYCCode { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string CompanyName { get; set; }
        public int CompanyId { get; set; }
        public string RegistrationDate { get; set; }
        public string ExpireDate { get; set; }
        public bool UserStatus { get; set; }

        public string status { get; set; }
        public string message { get; set; }
        public string UserCode { get; set; }

        public string FullName { get; set; }

        public string StateName { get; set; }
    }
    public class UserSupportingDocumentViewModel
    {
        public int UserDocId { get; set; }
        public long UserId { get; set; }
        public int DocumentTypeId { get; set; }
        public string DocumentName { get; set; }
        public string DocumentPath { get; set; }
    }
}

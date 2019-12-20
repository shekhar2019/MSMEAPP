using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Core
{
    public class ContactInfoViewModel
    {
        public int ContactInfoId { get; set; }
        public string ContactTitle { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public string Website { get; set; }

        public string Address { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedByUserName { get; set; }
        public string CreateDate { get; set; }
        public int ModifyBy { get; set; }
        public string ModifyDate { get; set; }
        public bool ContactInfoStatus { get; set; }
    }
}

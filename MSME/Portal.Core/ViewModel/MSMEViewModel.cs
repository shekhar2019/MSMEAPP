using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Core.ViewModel
{
   public class MSMEViewModel
    {
        public long MSMEId { get; set; }
        public string MSMETitle { get; set; }
        public string MSMEDescription { get; set; }
        public string MSMEImageUrl { get; set; }
        public string MSMEContentArea { get; set; }
        public int CreatedBy { get; set; }
        public string CreateDate { get; set; }
        public int ModifyBy { get; set; }
        public string ModifyDate { get; set; }
        public bool MSMEStatus { get; set; }
    }
}

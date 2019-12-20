using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Core.ViewModel
{
    public class ProductSubChildGroupViewModel
    {
        public long ProductSubChildGroupId { get; set; }
        public string ProductSubChildGroupName { get; set; }
        public string ProductSubChildGroupCode { get; set; }
        public long ProductMainGroupId { get; set; }
        public long ProductSubGroupId { get; set; }
        public string ProductSubGroupName { get; set; }
        public string ProductMainGroupName { get; set; }
        public bool ProductSubGroupStatus { get; set; }

        public string ProductSubChildGroupDesc { get; set; }
        public string ProductSubChildGroupImageUrl { get; set; }
    }
}

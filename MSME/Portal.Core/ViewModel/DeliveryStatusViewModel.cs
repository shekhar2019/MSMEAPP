using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Core.ViewModel
{
    public class DeliveryStatusViewModel
    {
        public int DelivryStatusID { get; set; }
        public string DeliveryName { get; set; }
        public Nullable<bool> DeliveryStatus { get; set; }
    }
}

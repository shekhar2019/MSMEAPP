using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Core
{
  public class ProductArhViewModel
    {
        public long ProductId { get; set; }
        public int ProductMainGroupId { get; set; }
        public int ProductSubGroupId { get; set; }
        public string ProductName { get; set; }
        public string ProductDesc { get; set; }
        public bool Status { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }
    }
    public  class ProductDetailArhViewModel
    {
        public long ProductDetailId { get; set; }
        public long ProductId { get; set; }
        public string Image { get; set; }
    }
}

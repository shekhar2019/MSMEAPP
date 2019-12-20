using Portal.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Portal.Common;
namespace Portal.Core.ViewModel
{
    public class ProductItemViewModel : IModel
    {
        public long ProductCodeID { get; set; }
        public string ProductCodeValue { get; set; }
        public long ProductMainGroupId { get; set; }
        public long ProductSubGroupId { get; set; }
        public long ProductSubChildGroupId { get; set; }
    }
    
}

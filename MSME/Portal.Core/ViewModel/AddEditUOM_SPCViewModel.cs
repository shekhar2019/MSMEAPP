using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Core.ViewModel
{
    public partial class AddEditUOM_SPCViewModel
    {
       
        public long ParentClassId { get; set; }
        public long CategoryId { get; set; }
        public long SubCategoryId { get; set; }
        public long ProductTechSpecId { get; set; }
       
        public int UomId { get; set; }
        public int UOM_sps_Id { get; set; }


    }
}

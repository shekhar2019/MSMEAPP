using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.DAL
{
    public partial class AddEditUOM_SPC
    {
        public long ParentClassId { get; set; }
        public long CategoryId { get; set; }
        public long SubCategoryId { get; set; }
        public long ProductTechSpecId { get; set; }

        public int UomId { get; set; }
        public int UOM_sps_Id { get; set; }
    }
}

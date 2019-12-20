using Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Core.ViewModel
{
    public class GSTViewModel: IModel
    {
        public int GSTID { get; set; }
        public string GSTName { get; set; }
        public Nullable<bool> GSTStatus { get; set; }
    }
}

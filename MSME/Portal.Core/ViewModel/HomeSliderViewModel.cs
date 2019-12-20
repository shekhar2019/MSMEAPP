using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Core
{
   public class HomeSliderViewModel
    {
        public int Sliderid { get; set; }
        public string SliderTitle { get; set; }
        public string Image { get; set; }
        public string ImageAlt { get; set; }
        public string ImageTitle { get; set; }
        public string Description { get; set; }
        public string LinkUrl { get; set; }
        public int CreatedBy { get; set; }

        public string CreatedByUser { get; set; }
        public string CreateDate { get; set; }
        public int ModifyBy { get; set; }
        public string ModifyDate { get; set; }
        public bool HomeSiderStatus { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Core.ViewModel
{
   public  class CopyRightViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedByUserName { get; set; }
        public string CreateDate { get; set; }
        public int ModifyBy { get; set; }
        public string ModifyDate { get; set; }
        public bool CopyrightStatus { get; set; }
    }
}

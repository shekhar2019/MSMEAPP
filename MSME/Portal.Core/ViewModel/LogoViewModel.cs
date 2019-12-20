﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Core.ViewModel
{
    public  class LogoViewModel
    {
        public int LogoId { get; set; }
        public string Title { get; set; }
        public string LogoPath { get; set; }
        public string Link { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public string CreateDate { get; set; }
        public int ModifyBy { get; set; }
        public string ModifyDate { get; set; }
        public bool LogoStatus { get; set; }
    }
}

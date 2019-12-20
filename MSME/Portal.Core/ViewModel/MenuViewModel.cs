using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Core.ViewModel
{
    public class HomeMenuViewModel
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public string MenuLink { get; set; }
        public int CreatedBy { get; set; }
        public string CreateDate { get; set; }
        
        public bool MenuStatus { get; set; }
        public int SequenceNo { get; set; }

     
    }
    public class MenuViewModel
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public string MenuLink { get; set; }
        public int CreatedBy { get; set; }
        public string CreateDate { get; set; }
        public int ModifyBy { get; set; }
        public string ModifyDate { get; set; }
        public bool MenuStatus { get; set; }
        public int SequenceNo { get; set; }

        public List<SubMenuViewModel> subMenuList { get; set; }
    }
    public class SubMenuViewModel
    {
        public int SubMenuId { get; set; }
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public string PageName { get; set; }
        public string SubMenuName { get; set; }
        public string SubMenuLink { get; set; }
        public bool SubMenuStatus { get; set; }
        public int  SequenceNo { get; set; }

        public List<SubChildMenuViewModel> subChildMenuList { get; set; }
    }

    public class SubChildMenuViewModel
    {
        public int SubChildMenuId { get; set; }
        public int SubMenuId { get; set; }
        public int MenuId { get; set; }
        public string SubMenuName { get; set; }
        public string SubChildMenuName { get; set; }
        public string SubChildPageName { get; set; }
        public string SubChildMenuLink { get; set; }
        public bool SubChildMenuStatus { get; set; }
        public int SequenceNo { get; set; }
    }
}

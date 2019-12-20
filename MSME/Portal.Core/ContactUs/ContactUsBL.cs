using System;
using Portal.DAL;
using Portal.Common;
using System.Reflection;
using Portal.Core.ViewModel;
using System.Collections.Generic;
using System.Data;

namespace Portal.Core.ContactUs
{
    public class ContactUsBL
    {
        DBInterface dbInterface;

        public ContactUsBL()
        {
            dbInterface = new DBInterface();

        }
    }
}

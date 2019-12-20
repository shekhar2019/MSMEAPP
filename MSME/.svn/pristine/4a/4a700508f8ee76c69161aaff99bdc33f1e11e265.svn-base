using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Portal.Common;
using Portal.DAL;
using System.Reflection;

namespace Portal.Core
{
  public class ContactInfoBL
    {
        DBInterface dbInterface;
        public ContactInfoBL()
        {
            dbInterface = new DBInterface();
        }

        public ResponseOut AddEditContactInfo(ContactInfoViewModel contactInfoViewModel)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                
                ContactInfo contactInfo = new ContactInfo
                {
                        ContactInfoId = contactInfoViewModel.ContactInfoId,
                        ContactTitle = contactInfoViewModel.ContactTitle,
                        Phone = contactInfoViewModel.Phone,
                        Email = contactInfoViewModel.Email,
                        Website = contactInfoViewModel.Website,
                        Address = contactInfoViewModel.Address,
                        Status=contactInfoViewModel.ContactInfoStatus,
                        CreatedBy = contactInfoViewModel.CreatedBy,
                        
                };
                responseOut = dbInterface.AddEditContactInfo(contactInfo);

            }

            catch (Exception ex)
            {
                responseOut.status = ActionStatus.Fail;
                responseOut.message = ActionMessage.ApplicationException;
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return responseOut;
        }
        
        public List<ContactInfoViewModel> GetContactInfoList()
        {
            List<ContactInfoViewModel> contactInfolist = new List<ContactInfoViewModel>();
            try
            {
                List<ContactInfo> contactInfo = dbInterface.GetContactInfoList();

                if (contactInfo != null && contactInfo.Count > 0)
                {
                    foreach (var item in contactInfo)
                    {
                        contactInfolist.Add(new ContactInfoViewModel
                        {
                            ContactInfoId = item.ContactInfoId,
                            ContactTitle = item.ContactTitle,
                            Phone = item.Phone,
                            Email = item.Email,
                            Website = item.Website,
                            Address = item.Address,
                            CreatedByUserName = item.UserName,
                            ContactInfoStatus =Convert.ToBoolean(item.Status),

                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return contactInfolist;
        }

        public ResponseOut RemoveContactInfo(int contactInfoId)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                responseOut = dbInterface.RemoveContactInfo(contactInfoId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return responseOut;
        }
    }
}

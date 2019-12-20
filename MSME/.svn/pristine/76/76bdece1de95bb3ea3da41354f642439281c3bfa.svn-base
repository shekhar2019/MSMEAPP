using System;
using Portal.DAL;
using Portal.Common;
using System.Reflection;
using Portal.Core.ViewModel;
using System.Collections.Generic;
using System.Data;

namespace Portal.Core
{
    public class AboutusBL
    {
        DBInterface dbInterface;
        public AboutusBL()
        {
            dbInterface = new DBInterface();
        }
        public ResponseOut AddEditAboutus(AboutUsViewModel aboutUsViewModel)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                Aboutu aboutus = new Aboutu
                {
                    AboutusId = aboutUsViewModel.AboutusId,
                    AboutusTitle = aboutUsViewModel.AboutusTitle,
                    Description = aboutUsViewModel.Description,
                    ImageUrl = aboutUsViewModel.ImageUrl,
                    CreatedBy = aboutUsViewModel.CreatedBy,
                    AboutStatus = aboutUsViewModel.AboutStatus
                };
                responseOut = dbInterface.AddEditAboutus(aboutus);

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
                            ContactInfoId =item.ContactInfoId,
                            ContactTitle = item.ContactTitle,
                            Phone = item.Phone,
                            Email = item.Email,
                            Website = item.Website,
                            Address = item.Address,
                            CreatedByUserName = item.UserName
                            //Status=Convert.ToBoolean(item.Status),

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


        public List<AboutUsViewModel> GetAboutUsList()
        {
            List<AboutUsViewModel> aboutuslist = new List<AboutUsViewModel>();
            try
            {
                List<Aboutu> abouts = dbInterface.GetAboutusList();

                if (abouts != null && abouts.Count > 0)
                {
                    foreach (var item in abouts)
                    {
                        aboutuslist.Add(new AboutUsViewModel
                        {
                            AboutusId = item.AboutusId,
                            AboutusTitle = item.AboutusTitle,
                            Description = item.Description,
                            ImageUrl = item.ImageUrl,
                            AboutStatus = Convert.ToBoolean(item.AboutStatus),
                            CreatedByUserName = item.UserName
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return aboutuslist;
        }

        public ResponseOut UpdateAboutPic(AboutUsViewModel aboutUsViewModel)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                Aboutu aboutu = new Aboutu
                {
                    AboutusId=aboutUsViewModel.AboutusId,
                    ImageUrl=aboutUsViewModel.ImageUrl
                };
                
                responseOut= dbInterface.UpdateAboutusPic(aboutu);
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

        public ResponseOut RemoveLogo(int logoId)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                responseOut = dbInterface.RemoveLogo(logoId);
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

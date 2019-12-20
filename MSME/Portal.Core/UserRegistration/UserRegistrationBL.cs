using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Portal.Core.ViewModel;
using Portal.DAL;
using Portal.Common;
using System.Reflection;
using System.Data;
using System.Web.Mvc;

namespace Portal.Core
{
  public class UserRegistrationBL
    {
        DBInterface dbInterface;
        public UserRegistrationBL()
        {
            dbInterface = new DBInterface();
        }
        public ResponseOut AddEditUserRegistration(UserRegistrationViewModel userRegistrationVM,UserSupportingDocumentViewModel userSupportingDocumentVM)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                UserRegistration userRegistration = new UserRegistration
                {

                    FirstName = userRegistrationVM.FirstName,
                    LastName = userRegistrationVM.LastName,
                    Email = userRegistrationVM.Email,
                    ContactNo = userRegistrationVM.ContactNo,
                    AlternateContactno = userRegistrationVM.AlternateContactno,
                    WhatsaapPhone = userRegistrationVM.WhatsaapPhone,
                    Fax = userRegistrationVM.Fax,
                    StateId = userRegistrationVM.StateId,
                    City = userRegistrationVM.City,
                    SubCity = userRegistrationVM.SubCity,
                    Landmark = userRegistrationVM.Landmark,
                    BuildingNo = userRegistrationVM.BuildingNo,
                    PINCode = userRegistrationVM.PINCode,
                    GSTNo = userRegistrationVM.GSTNo,
                    PANNo = userRegistrationVM.PANNo,
                    UdyogAadhaarNo = userRegistrationVM.UdyogAadhaarNo,
                    KYCCode= userRegistrationVM.KYCCode,
                    UserName= userRegistrationVM.UserName,
                    Password= userRegistrationVM.Password,
                    CompanyName = userRegistrationVM.CompanyName,
                    ExpireDate = DateTime.Now.AddMonths(1),
                    UserStatus= userRegistrationVM.UserStatus,
                    UserCode = userRegistrationVM.UserCode


                };
                int userId = 0;
                responseOut = dbInterface.UserRegistration(userRegistration, out userId);
                //UserSupportingDocument userSupportingDocument = new UserSupportingDocument
                //{
                //    UserDocId=0,
                //    UserId= userSupportingDocumentVM.UserId,
                //    DocumentName = userSupportingDocumentVM.DocumentName,
                //    DocumentPath= userSupportingDocumentVM.DocumentPath,
                //    DocumentTypeId= userSupportingDocumentVM.DocumentTypeId
                //};
            }
            catch(Exception ex)
            {
               
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);

            }
            return responseOut;

        }

        public ResponseOut UpdateUserPic(UserSupportingDocumentViewModel userSupportingDocumentVM)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                UserSupportingDocument userSupportingDocument = new UserSupportingDocument {
                     UserId = userSupportingDocumentVM.UserId,
                     DocumentPath= userSupportingDocumentVM.DocumentPath
                   };
                
                responseOut = dbInterface.UpdateUserPic(userSupportingDocument);
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

        public UserRegistration GetRegistration(long userId)
        {
            return dbInterface.GetRegistration(userId);
        }

        public void UpdateProfile(UserRegistration userRegistration)
        {
            dbInterface.UpdateProfile(userRegistration);
        }

        public int GetUserMaxCount()
        {
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            int UserCodeMax = 0;
            try
            {
                UserCodeMax = sqlDbInterface.GetUserMaxCount();
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return UserCodeMax;
        }

        public List<UserRegistrationViewModel> GetUserRegistrationList(string firstName, string email, string contactNo, string kycCode)
        {
            List<UserRegistrationViewModel> userRegistrations = new List<UserRegistrationViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtUserRegistrations = sqlDbInterface.GetUserRegistrationList(firstName, email, contactNo, kycCode);
                if (dtUserRegistrations != null && dtUserRegistrations.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtUserRegistrations.Rows)
                    {
                        userRegistrations.Add(new UserRegistrationViewModel
                        {
                            UserId = Convert.ToInt32(dr["UserId"]),
                            FullName = Convert.ToString(dr["FullName"]),
                            Email = Convert.ToString(dr["Email"]),
                            ContactNo = Convert.ToString(dr["ContactNo"]),
                            AlternateContactno = Convert.ToString(dr["AlternateContactno"]),
                            WhatsaapPhone = Convert.ToString(dr["WhatsaapPhone"]),
                            Fax = Convert.ToString(dr["Fax"]),
                            StateName = Convert.ToString(dr["StateName"]),
                            City = Convert.ToString(dr["City"]),
                            SubCity = Convert.ToString(dr["SubCity"]),
                            Landmark = Convert.ToString(dr["Landmark"]),
                            BuildingNo = Convert.ToString(dr["BuildingNo"]),
                            PINCode = Convert.ToString(dr["PINCode"]),
                            GSTNo= Convert.ToString(dr["GSTNo"]),
                            PANNo=Convert.ToString(dr["PANNo"]),
                            UdyogAadhaarNo=Convert.ToString(dr["UdyogAadhaarNo"]),
                            KYCCode =Convert.ToString(dr["KYCCode"]),
                            UserName=Convert.ToString(dr["UserName"]),
                            Password=Convert.ToString(dr["Password"]),
                            CompanyName = Convert.ToString(dr["CompanyName"]),
                            RegistrationDate=Convert.ToString(dr["RegistrationDate"]),
                            ExpireDate= Convert.ToString(dr["ExpireDate"]),
                            UserStatus = Convert.ToBoolean(dr["UserStatus"]),
                            UserCode =Convert.ToString(dr["UserCode"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return userRegistrations;
        }
    }
}
 
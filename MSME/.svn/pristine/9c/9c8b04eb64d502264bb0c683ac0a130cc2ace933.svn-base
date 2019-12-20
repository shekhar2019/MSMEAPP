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

namespace Portal.Core
{
    public class UserBL
    {
        DBInterface dbInterface;
        public UserBL()
        {
            dbInterface = new DBInterface();
        }
        public ResponseOut AddEditUser(UserViewModel userViewModel)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                User user = new User
                {
                    UserId = userViewModel.UserId,
                    UserName = userViewModel.UserName,
                    FullName = userViewModel.FullName,
                    MobileNo = userViewModel.MobileNo,
                    Email = userViewModel.Email,
                    Password = userViewModel.Password,
                    RoleId = userViewModel.RoleId,
                    CompanyId = userViewModel.CompanyId,
                    CreatedBy = userViewModel.CreatedBy,
                    CreatedDate = DateTime.Now,
                    ExpiryDate = Convert.ToDateTime(userViewModel.ExpiryDate),
                    Status = userViewModel.UserStatus
                };
                responseOut = dbInterface.AddEditUser(user);
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
        public UserViewModel AuthenticateUser(string userName, string password)
        {
            UserViewModel userViewModel = new UserViewModel();
            try
            {
                User user = dbInterface.AuthenticateUser(userName, password);
                userViewModel = new UserViewModel
                {
                    UserId = user.UserId,
                    UserName = user.UserName,
                    FullName = user.FullName,
                    MobileNo = user.MobileNo,
                    Email = user.Email,
                    Password = user.Password,
                    RoleId = user.RoleId,
                    CompanyId = user.CompanyId,
                    ExpiryDate = user.ExpiryDate == null ? "" : Convert.ToDateTime(user.ExpiryDate).ToString("dd-MMM-yyyy"),
                    UserStatus = user.Status
                };

                if (userViewModel.UserId != 0)
                {
                    if (userViewModel.RoleId == (int)Roles.SuperAdmin)
                    {
                        userViewModel.status = ActionStatus.Success;
                        userViewModel.message = "";
                    }
                    else
                    {
                        if (!userViewModel.UserStatus)
                        {
                            userViewModel.status = ActionStatus.Fail;
                            userViewModel.message = ActionMessage.UserNotActive;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(userViewModel.ExpiryDate) && Convert.ToDateTime(userViewModel.ExpiryDate).Date < DateTime.Now.Date)
                            {
                                userViewModel.status = ActionStatus.Fail;
                                userViewModel.message = ActionMessage.UserLoginExpired;
                            }
                            else
                            {
                                userViewModel.status = ActionStatus.Success;
                                userViewModel.message = "";
                            }
                        }
                    }

                }
                else
                {
                    userViewModel.status = ActionStatus.Fail;
                    userViewModel.message = ActionMessage.InvalidCredential;
                }
                if (userViewModel.status == ActionStatus.Success)
                {
                    SessionWrapper session = new SessionWrapper();
                    FinYearBL finYearBL = new FinYearBL();
                    FinYearViewModel currentFinYear = finYearBL.GetCurrentFinancialYear(0);

                    session.SetInSession(SessionKey.CurrentUser, userViewModel);
                    session.SetInSession(SessionKey.IsAuthenticated, true);
                    session.SetInSession(SessionKey.UserFullName, userViewModel.FullName);
                    session.SetInSession(SessionKey.CurrentFinYear, currentFinYear);

                    List<ParentMenu> parentMenuList = new List<ParentMenu>();
                    List<proc_GetRoleWiseParentUI_Result> parentUIList = dbInterface.GetRoleWiseParentUI(userViewModel.RoleId);

                    ParentMenu parentmenu;
                    List<ChildMenu> childMenuList;
                    foreach (proc_GetRoleWiseParentUI_Result UIItem in parentUIList)
                    {
                        parentmenu = new ParentMenu();
                        parentmenu.InterfaceId = Convert.ToInt32(UIItem.InterfaceId);
                        parentmenu.InterfaceName = UIItem.InterfaceName;
                        parentmenu.InterfaceURL = UIItem.InterfaceURL;
                        parentmenu.InterfaceType = UIItem.InterfaceType;
                        parentmenu.InterfaceSubType = UIItem.InterfaceSubType;
                        if (string.IsNullOrEmpty(UIItem.InterfaceURL))
                        {
                            childMenuList = new List<ChildMenu>();
                            List<proc_GetRoleWiseChildUI_Result> childUIList = dbInterface.GetRoleWiseChildUI(userViewModel.RoleId, Convert.ToInt32(UIItem.InterfaceId));
                            foreach (proc_GetRoleWiseChildUI_Result childUIItem in childUIList)
                            {
                                childMenuList.Add(new ChildMenu { InterfaceId = Convert.ToInt32(childUIItem.InterfaceId), InterfaceName = childUIItem.InterfaceName, InterfaceURL = childUIItem.InterfaceURL, InterfaceType = childUIItem.InterfaceType, InterfaceSubType = childUIItem.InterfaceSubType });
                            }

                        }
                        else
                        {
                            childMenuList = new List<ChildMenu>();
                        }
                        parentmenu.childMenuList = childMenuList;
                        parentMenuList.Add(parentmenu);
                    }

                    session.SetInSession(SessionKey.RoleWiseUIMatrix, parentMenuList);

                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return userViewModel;
        }
        public List<UserViewModel> GetUserList(string userName = "", int companyId = 0, int roleId = 0, string fullName = "", string phoneNo = "", string email = "", int userRole = 0)
        {
            List<UserViewModel> users = new List<UserViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtusers = sqlDbInterface.GetUserList(userName, companyId, roleId, fullName, phoneNo, email, userRole);
                if (dtusers != null && dtusers.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtusers.Rows)
                    {
                        users.Add(new UserViewModel
                        {
                            UserId = Convert.ToInt32(dr["UserId"]),
                            UserName = Convert.ToString(dr["UserName"]),
                            FullName = Convert.ToString(dr["FullName"]),
                            MobileNo = Convert.ToString(dr["MobileNo"]),
                            Email = Convert.ToString(dr["Email"]),
                            RoleId = Convert.ToInt32(dr["RoleId"]),
                            RoleName = Convert.ToString(dr["RoleName"]),
                            CompanyId = Convert.ToInt32(dr["CompanyId"]),
                            CompanyName = Convert.ToString(dr["CompanyName"]),
                            CreatedBy = Convert.ToInt32(dr["CreatedBy"]),
                            CreatedByName = Convert.ToString(dr["CreatedByName"]),
                            CreatedDate = Convert.ToString(dr["CreatedDate"]),
                            ModifiedBy = Convert.ToInt32(dr["ModifiedBy"].ToString() == "" ? "0" : dr["ModifiedBy"].ToString()),
                            ModifiedByName = Convert.ToString(dr["ModifiedByName"]),
                            ModifiedDate = Convert.ToString(dr["ModifiedDate"]),
                            ExpiryDate = Convert.ToString(dr["ExpiryDate"]),
                            UserStatus = Convert.ToBoolean(dr["Status"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return users;
        }
        public UserViewModel GetUserDetail(int userId = 0)
        {
            UserViewModel user = new UserViewModel();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtUsers = sqlDbInterface.GetUserDetail(userId);
                if (dtUsers != null && dtUsers.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtUsers.Rows)
                    {
                        user = new UserViewModel
                        {
                            UserId = Convert.ToInt32(dr["UserId"]),
                            UserName = Convert.ToString(dr["UserName"]),
                            FullName = Convert.ToString(dr["FullName"]),
                            MobileNo = Convert.ToString(dr["MobileNo"]),
                            Email = Convert.ToString(dr["Email"]),
                            RoleId = Convert.ToInt32(dr["RoleId"]),
                            RoleName = Convert.ToString(dr["RoleName"]),
                            CompanyId = Convert.ToInt32(dr["CompanyId"]),
                            CompanyName = Convert.ToString(dr["CompanyName"]),
                            CreatedBy = Convert.ToInt32(dr["CreatedBy"]),
                            CreatedByName = Convert.ToString(dr["CreatedByName"]),
                            CreatedDate = Convert.ToString(dr["CreatedDate"]),
                            ModifiedBy = Convert.ToInt32(dr["ModifiedBy"].ToString() == "" ? "0" : dr["ModifiedBy"].ToString()),
                            ModifiedByName = Convert.ToString(dr["ModifiedByName"]),
                            ModifiedDate = Convert.ToString(dr["ModifiedDate"]),
                            ExpiryDate = Convert.ToString(dr["ExpiryDate"]),
                            UserStatus = Convert.ToBoolean(dr["Status"])
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return user;
        }

        public UserViewModel GetUserDetailWithProduct(int ProductId = 0, int userId = 0, int userType=0)
        {
            UserViewModel user = new UserViewModel();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtUsers = sqlDbInterface.GetUserDetailWithProduct(ProductId,userId, userType);
                if (dtUsers != null && dtUsers.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtUsers.Rows)
                    {
                        user = new UserViewModel
                        {
                            FullAddress = Convert.ToString(dr["Address"]),
                            UserName = Convert.ToString(dr["UserName"]),
                            //FullName = Convert.ToString(dr["FullName"]),
                            MobileNo = Convert.ToString(dr["ContactNo"]),
                            Email = Convert.ToString(dr["Email"]),
                            //RoleId = Convert.ToInt32(dr["RoleId"]),
                            GSTStatus = Convert.ToString(dr["GSTStatus"]),
                            MOQ = Convert.ToString(dr["MOQ"]),
                            RatePerUnit = Convert.ToString(dr["RatePerUnit"]),
                            //RoleName = Convert.ToString(dr["RoleName"]),
                            //CompanyId = Convert.ToInt32(dr["CompanyId"]),
                            //CompanyName = Convert.ToString(dr["CompanyName"]),
                            //CreatedBy = Convert.ToInt32(dr["CreatedBy"]),
                            //CreatedByName = Convert.ToString(dr["CreatedByName"]),
                            //CreatedDate = Convert.ToString(dr["CreatedDate"]),
                            //ModifiedBy = Convert.ToInt32(dr["ModifiedBy"].ToString() == "" ? "0" : dr["ModifiedBy"].ToString()),
                            //ModifiedByName = Convert.ToString(dr["ModifiedByName"]),
                            //ModifiedDate = Convert.ToString(dr["ModifiedDate"]),
                            //ExpiryDate = Convert.ToString(dr["ExpiryDate"]),
                            //UserStatus = Convert.ToBoolean(dr["Status"])
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return user;
        }
        public UserViewModel GetUserFromEmail(string userEmail)
        {
            UserViewModel userViewModel = new UserViewModel();
            User user = new User();
            try
            {
                user = dbInterface.GetUserFromEmail(userEmail);
                userViewModel.FullName = user.FullName;
                userViewModel.Password = user.Password;
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return userViewModel;
        }

        public ResponseOut ChangePassword(UserViewModel userViewModel)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                responseOut = dbInterface.VerifyOldPassword(userViewModel.UserId, userViewModel.OldPassword);
                if (responseOut.status == ActionStatus.Success)
                {
                    User user = new User
                    {
                        UserId = userViewModel.UserId,
                        Password = userViewModel.Password,
                        CreatedBy = userViewModel.CreatedBy,
                        CreatedDate = DateTime.Now,
                    };
                    responseOut = dbInterface.ChangePassword(user);
                }
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

        public UserRegistrationViewModel AuthenticateBSUser(string userName, string password)
        {
            UserRegistrationViewModel userRegistrationViewModel = new UserRegistrationViewModel();
            try
            {
                UserRegistration userRegistration = dbInterface.AuthenticateBSUser(userName, password);

                userRegistrationViewModel = new UserRegistrationViewModel
                {
                    UserId= userRegistration.UserId,
                    FirstName=userRegistration.FirstName,
                    LastName=userRegistration.LastName,
                    Email= userRegistration.Email,
                    ContactNo= userRegistration.ContactNo,
                    AlternateContactno= userRegistration.AlternateContactno,
                    WhatsaapPhone= userRegistration.WhatsaapPhone,
                    Fax= userRegistration.Fax,
                    StateId=Convert.ToInt32(userRegistration.StateId),
                    City=userRegistration.City,
                    SubCity=userRegistration.SubCity,
                    Landmark=userRegistration.Landmark,
                    BuildingNo=userRegistration.BuildingNo,
                    PINCode = userRegistration.PINCode,
                    GSTNo=userRegistration.GSTNo,
                    PANNo=userRegistration.PANNo,
                    UdyogAadhaarNo=userRegistration.UdyogAadhaarNo,
                    KYCCode = userRegistration.KYCCode,
                    CompanyName=userRegistration.CompanyName,
                    RegistrationDate=Convert.ToString(userRegistration.RegistrationDate),
                    ExpireDate = userRegistration.ExpireDate == null ? "" : Convert.ToDateTime(userRegistration.ExpireDate).ToString("dd-MMM-yyyy"),
                    UserStatus = Convert.ToBoolean(userRegistration.UserStatus)

                };
                if(userRegistrationViewModel.UserId!=0)
                {
                    if(!userRegistrationViewModel.UserStatus)
                    {
                        userRegistrationViewModel.status = ActionStatus.Fail;
                        userRegistrationViewModel.message = ActionMessage.UserNotActive;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(userRegistrationViewModel.ExpireDate) && Convert.ToDateTime(userRegistrationViewModel.ExpireDate).Date < DateTime.Now.Date)
                        {
                            userRegistrationViewModel.status = ActionStatus.Fail;
                            userRegistrationViewModel.message = ActionMessage.UserLoginExpired;
                        }
                        else
                        {
                            userRegistrationViewModel.status = ActionStatus.Success;
                            userRegistrationViewModel.message = "";
                        }
                    }
                }
                else
                {
                    userRegistrationViewModel.status = ActionStatus.Fail;
                    userRegistrationViewModel.message = ActionMessage.InvalidCredential;
                }

                if(userRegistrationViewModel.status==ActionStatus.Success)
                {
                    SessionWrapper session = new SessionWrapper();
                    session.SetInSession(SessionKey.CurrentBSUser, userRegistrationViewModel);
                    session.SetInSession(SessionKey.UserFullName, userRegistrationViewModel.FirstName+" "+ userRegistrationViewModel.LastName);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }

            return userRegistrationViewModel;
        }


    }
}

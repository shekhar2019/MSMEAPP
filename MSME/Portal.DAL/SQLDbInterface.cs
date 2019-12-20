﻿using Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Configuration;

namespace Portal.DAL
{
    public class SQLDbInterface : IDisposable
    {
        private readonly string connectionString = "";
        public SQLDbInterface()
        {
            connectionString = ConfigurationManager.ConnectionStrings["SQLConnStr"].ConnectionString;
        }
        #region Dispose Methods
        public void Dispose()
        {
            try
            {

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
        }

        #endregion
        #region Company
        public DataTable GetCompanyList(string companyName, string cityName, string panNo, string phoneNo, string tinNo)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetCompanies", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CompanyName", companyName);
                    da.SelectCommand.Parameters.AddWithValue("@City", cityName);
                    da.SelectCommand.Parameters.AddWithValue("@PanNo", panNo);
                    da.SelectCommand.Parameters.AddWithValue("@PhoneNo", phoneNo);
                    da.SelectCommand.Parameters.AddWithValue("@TinNo", tinNo);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetCompanyDetail(int companyId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetCompanyDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetCountryList(string countryName, string countryCode, string Status)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetCountries", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CountryName", countryName);
                    da.SelectCommand.Parameters.AddWithValue("@CountryCode", countryCode);
                    da.SelectCommand.Parameters.AddWithValue("@Status", Status);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetCountryDetail(int countryId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetCountryDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CountryId", countryId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion

        #region Currency
        public DataTable GetCurrencyList(string currencyName, string currencyCode, string Status)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetCurrencies", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CurrencyName", currencyName);
                    da.SelectCommand.Parameters.AddWithValue("@CurrencyCode", currencyCode);
                    da.SelectCommand.Parameters.AddWithValue("@Status", Status);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetCurrencyDetail(int currencyId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetCurrencyDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CurrencyId", currencyId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion

        #region User
        public DataTable GetUserList(string userName, int companyid, int roleId, string fullName, string phoneNo, string email, int userRole)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    if (userRole == (int)Roles.SuperAdmin)
                    {
                        da = new SqlDataAdapter("proc_GetPrimaryUsers", con);
                    }
                    else
                    {
                        da = new SqlDataAdapter("proc_GetUsers", con);
                    }
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@UserName", userName);
                    da.SelectCommand.Parameters.AddWithValue("@Companyid", companyid);
                    da.SelectCommand.Parameters.AddWithValue("@RoleId", roleId);
                    da.SelectCommand.Parameters.AddWithValue("@FullName", fullName);
                    da.SelectCommand.Parameters.AddWithValue("@PhoneNo", phoneNo);
                    da.SelectCommand.Parameters.AddWithValue("@Email", email);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetUserDetail(int userId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetUserDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@UserId", userId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetUserDetailWithProduct(int productId, int userId, int userType)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("sp_GetProdInfo_Sel", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@UserType", userType);
                    da.SelectCommand.Parameters.AddWithValue("@UserId", userId);
                    da.SelectCommand.Parameters.AddWithValue("@ProductId", productId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetBuyerProductDetail(long buyerProductDetailId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetBuyerProduct", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@BuyerProductDetailId", buyerProductDetailId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }

        public DataTable GetSellerProductDetail(long sellerrProductDetailId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSellerProduct", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@SellerProductDetailId", sellerrProductDetailId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }



        #endregion

        #region  UserRegistration
        public DataTable GetUserRegistrationList(string firstName, string email, string contactNo, string KYCCode)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetUserRegistrations", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@FirstName", firstName);
                    da.SelectCommand.Parameters.AddWithValue("@Email", email);
                    da.SelectCommand.Parameters.AddWithValue("@ContactNo", contactNo);
                    da.SelectCommand.Parameters.AddWithValue("@KYCCode", KYCCode);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion


        #region Fin Year
        public DataTable GetFinYearList()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetFinYear", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion
        #region CompanyBranch
        public DataTable GetCompanyBranchList(string BranchName, string cityName, string panNo, string phoneNo, string tinNo)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetCompaniesBranch", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CompanyBranchName", BranchName);
                    da.SelectCommand.Parameters.AddWithValue("@City", cityName);
                    da.SelectCommand.Parameters.AddWithValue("@PanNo", panNo);
                    da.SelectCommand.Parameters.AddWithValue("@PhoneNo", phoneNo);
                    da.SelectCommand.Parameters.AddWithValue("@TinNo", tinNo);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetCompanyBranchDetail(int companyBranchId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetCompanyBranchDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CompanyBranchId", companyBranchId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion

        #region State 
        public DataTable GetStateList(string stateName, string stateCode, int countryId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetStates", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@StateName", stateName);
                    da.SelectCommand.Parameters.AddWithValue("@StateCode", stateCode);
                    da.SelectCommand.Parameters.AddWithValue("@CountryId", countryId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetStateDetail(int stateId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetStateDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@StateId", stateId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion

        #region City 
        public DataTable GetCityList(string cityName, int stateId, int countryId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetCities", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CityName", cityName);
                    da.SelectCommand.Parameters.AddWithValue("@StateId", stateId);
                    da.SelectCommand.Parameters.AddWithValue("@CountryId", countryId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetCityDetail(int CityId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetCityDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CityId", CityId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion 

        #region Role 
        public DataTable GetRoleList(string roleName, string roleDesc, string isAdmin, string Status, int companyId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetRoles", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@RoleName", roleName);
                    da.SelectCommand.Parameters.AddWithValue("@RoleDesc", roleDesc);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@IsAdmin", isAdmin);
                    da.SelectCommand.Parameters.AddWithValue("@Status", Status);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetRoleDetail(int roleId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetRoleDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@RoleId", roleId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion

        #region RoleUIMapping
        public DataTable GetRoleUIActionMappingDetail(string interfaceType, string interfaceSubType, int roleId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetRoleUIActionMappingDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@InterfaceType", interfaceType);
                    da.SelectCommand.Parameters.AddWithValue("@InterfaceSubType", interfaceSubType);
                    da.SelectCommand.Parameters.AddWithValue("@RoleId", roleId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        #endregion


        #region LeadStatus
        public DataTable GetLeadStatusList(string leadstatusName, string Status, int companyId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetLeadStatuses", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@LeadStatusName", leadstatusName);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@Status", Status);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetLeadStatusDetail(int leadstatusId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetLeadStatusDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@LeadStatusId", leadstatusId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion

        #region LeadSource
        public DataTable GetLeadSourceList(string leadsourceName, string Status, int companyId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetLeadSources", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@leadsourceName", leadsourceName);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@Status", Status);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetLeadSourceDetail(int leadsourceId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetLeadSourceDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@LeadSourceId", leadsourceId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion



        #region ProductType
        public DataTable GetProductTypeList(string producttypeName, string producttypeCode, string Status)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetProductTypes", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ProductTypeName", producttypeName);
                    da.SelectCommand.Parameters.AddWithValue("@ProductTypeCode", producttypeCode);
                    da.SelectCommand.Parameters.AddWithValue("@Status", Status);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetProductTypeDetail(int producttypeId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetProductTypeDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ProductTypeId", producttypeId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion


        #region Product
        public DataTable GetBuyerSellerProductList(string productName, int companyid, string productCode, string productShortDesc, string productFullDesc, int productTypeId, int productMainGroupId, string assemblyType, string brandName)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetBuyerProducts", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ProductName", productName);
                    da.SelectCommand.Parameters.AddWithValue("@Companyid", companyid);
                    da.SelectCommand.Parameters.AddWithValue("@ProductCode", productCode);
                    da.SelectCommand.Parameters.AddWithValue("@ProductShortDesc", productShortDesc);
                    da.SelectCommand.Parameters.AddWithValue("@ProductFullDesc", productFullDesc);
                    da.SelectCommand.Parameters.AddWithValue("@ProductTypeId", productTypeId);
                    da.SelectCommand.Parameters.AddWithValue("@ProductMainGroupId", productMainGroupId);
                    da.SelectCommand.Parameters.AddWithValue("@AssemblyType", assemblyType);
                    da.SelectCommand.Parameters.AddWithValue("@BrandName", brandName);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetProductList(string productName, int companyid, string productCode, string productShortDesc, string productFullDesc, int productTypeId, int productMainGroupId, string assemblyType, string brandName)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetProducts", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ProductName", productName);
                    da.SelectCommand.Parameters.AddWithValue("@Companyid", companyid);
                    da.SelectCommand.Parameters.AddWithValue("@ProductCode", productCode);
                    da.SelectCommand.Parameters.AddWithValue("@ProductShortDesc", productShortDesc);
                    da.SelectCommand.Parameters.AddWithValue("@ProductFullDesc", productFullDesc);
                    da.SelectCommand.Parameters.AddWithValue("@ProductTypeId", productTypeId);
                    da.SelectCommand.Parameters.AddWithValue("@ProductMainGroupId", productMainGroupId);
                    da.SelectCommand.Parameters.AddWithValue("@AssemblyType", assemblyType);
                    da.SelectCommand.Parameters.AddWithValue("@BrandName", brandName);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetProductDetail(long productId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetProductDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ProductId", productId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetProductTechSpecificationList(int productId)
        {
            DataTable dtProductTechSpecification = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetProductTechSpecifications", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ProductId", productId);
                    da.Fill(dtProductTechSpecification);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dtProductTechSpecification;

        }

        public DataTable GetProductsByGroupList(int productMainGroupId, int productSubGroupId, int productSubChildGroupId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetProductList", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ProductMainGroupId", productMainGroupId);
                    da.SelectCommand.Parameters.AddWithValue("@ProductSubGroupId", productSubGroupId);
                    da.SelectCommand.Parameters.AddWithValue("@ProductSubChildGroupId", productSubChildGroupId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }

        public DataTable GetProductsByGroupListKnowledgeCenter(int productMainGroupId, int productSubGroupId, int productSubChildGroupId,string searchtext)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetProductListNew", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ProductMainGroupId", productMainGroupId);
                    da.SelectCommand.Parameters.AddWithValue("@ProductSubGroupId", productSubGroupId);
                    da.SelectCommand.Parameters.AddWithValue("@ProductSubChildGroupId", productSubChildGroupId);
                    da.SelectCommand.Parameters.AddWithValue("@searchtext", searchtext);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }


        public DataTable GetProductsRateGroupbyKnowledgeCenter(string productID, string FromDate, string ToDate, string GraphType,string Year,string Fmonth,string Tmonth,string FYear,string TYear,string BSType)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("Rate_Graph_Data", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ProductID", productID);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", FromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", ToDate);
                    da.SelectCommand.Parameters.AddWithValue("@GraphType", GraphType);
                    da.SelectCommand.Parameters.AddWithValue("@Year", Year);
                    da.SelectCommand.Parameters.AddWithValue("@Fmonth", Fmonth);
                    da.SelectCommand.Parameters.AddWithValue("@Tmonth", Tmonth);
                    da.SelectCommand.Parameters.AddWithValue("@FYear", FYear);
                    da.SelectCommand.Parameters.AddWithValue("@TYear", TYear);
                    da.SelectCommand.Parameters.AddWithValue("@BSType", BSType);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }


        #endregion

        #region PaymentTerm  
        public DataTable GetPaymentTermList(string paymenttermDesc, string Status, int companyId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetPaymentTerms", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@PaymentTermDesc", paymenttermDesc);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@Status", Status);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetPaymentTermDetail(int paymenttermId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetPaymentTermDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@PaymentTermId", paymenttermId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion 


        #region Department 
        public DataTable GetDepartmentList(string departmentName, string departmentCode, string Status, int companyId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetDepartments", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@DepartmentName", departmentName);
                    da.SelectCommand.Parameters.AddWithValue("@DepartmentCode", departmentCode);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@Status", Status);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetDepartmentDetail(int departmentId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetDepartmentDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@departmentId", departmentId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion




        #region Product Opening Stock
        public DataTable GetProductOpeningList(string productName, int companyid, int finYearId, int companyBranchId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetProductOpenings", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ProductName", productName);
                    da.SelectCommand.Parameters.AddWithValue("@Companyid", companyid);
                    da.SelectCommand.Parameters.AddWithValue("@FinYearId", finYearId);
                    da.SelectCommand.Parameters.AddWithValue("@companyBranchId", companyBranchId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetProductOpeningDetail(long openingTrnId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetProductOpeningDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@OpeningTrnId", openingTrnId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion

        #region Product BOM
        public DataTable GetAssemblyList(string assemblyName, string assemblyType, int companyid)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetAssemblyList", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@AssemblyName", assemblyName);
                    da.SelectCommand.Parameters.AddWithValue("@AssemblyType", assemblyType);
                    da.SelectCommand.Parameters.AddWithValue("@Companyid", companyid);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetAssemblyBOMList(long assemblyId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetAssemblyBOMList", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@AssemblyId", assemblyId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetProductBOMDetail(long bomId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetProductBOMDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@BOMId", bomId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion


        #region Product Main Group 
        public DataTable GetProductMainGroupList(string productmaingroupName, string productmaingroupCode, string Status)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetProductMainGroupDetails", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ProductMainGroupName", productmaingroupName);
                    da.SelectCommand.Parameters.AddWithValue("@ProductMainGroupCode", productmaingroupCode);
                    da.SelectCommand.Parameters.AddWithValue("@Status", Status);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetProductMainGroupDetail(int productmaingroupId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetProductMainGroups", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ProductMainGroupId", productmaingroupId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion


        #region Product Sub Group 
        public DataTable GetProductSubGroupList(string productsubgroupName, string productsubgroupCode, int productmaingroupId, string Status)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetProductSubGroups", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ProductSubGroupName", productsubgroupName);
                    da.SelectCommand.Parameters.AddWithValue("@ProductSubGroupCode", productsubgroupCode);
                    da.SelectCommand.Parameters.AddWithValue("@ProductMainGroupId", productmaingroupId);
                    da.SelectCommand.Parameters.AddWithValue("@Status", Status);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetProductSubGroupDetail(int productsubgroupId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetProductSubGroupDetails", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ProductSubGroupId", productsubgroupId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion

        #region Product Sub Child Group
        public DataTable GetProductSubChildGroupList(string productmaingroupName, string productmaingroupCode, string Status)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetProductSubChildGroupDetails", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ProductSubChildGroupName", productmaingroupName);
                    da.SelectCommand.Parameters.AddWithValue("@ProductSubChildGroupCode", productmaingroupCode);
                    da.SelectCommand.Parameters.AddWithValue("@Status", Status);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetProductSubChildGroupDetail(int productsubChildGroupId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetProductSubChildGroups", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ProductSubChildGroupId", productsubChildGroupId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion

        #region UOM 
        public DataTable GetUOMList(string uomName, string uomDesc, string Status)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetUOMs", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@UOMName", uomName);
                    da.SelectCommand.Parameters.AddWithValue("@UOMDesc", uomDesc);
                    da.SelectCommand.Parameters.AddWithValue("@Status", Status);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetUOMDetail(int uomId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetUOMDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@UOMId", uomId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion

        #region Designation 
        public DataTable GetDesignationList(string designationName, string designationCode, int departmentId, string Status)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetDesignations", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@DesignationName", designationName);
                    da.SelectCommand.Parameters.AddWithValue("@DesignationCode", designationCode);
                    da.SelectCommand.Parameters.AddWithValue("@DepartmentId", departmentId);
                    da.SelectCommand.Parameters.AddWithValue("@Status", Status);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetDesignationDetail(int designationId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetDesignationDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@designationId", designationId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }
        #endregion 

        #region PaymentMode 
        public DataTable GetPaymentModeList(string paymentModeName, string Status)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetPaymentMode", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@PaymentModeName", paymentModeName);
                    da.SelectCommand.Parameters.AddWithValue("@Status", Status);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }
        public DataTable GetPaymentModeDetail(int paymentModeId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetPaymentModeDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@PaymentModeId", paymentModeId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }
        #endregion

        #region SLType 
        public DataTable GetSLTypeList(string sLTypeName, string Status)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSLType", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@SLTypeName", sLTypeName);
                    da.SelectCommand.Parameters.AddWithValue("@Status", Status);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }
        public DataTable GetSLTypeDetail(int sLTypeId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSLTypeDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@SLTypeId", sLTypeId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }
        #endregion


        #region Services 
        public DataTable GetServicesList(string servicesName, string Status)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetServices", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ServicesName", servicesName);
                    da.SelectCommand.Parameters.AddWithValue("@Status", Status);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }
        public DataTable GetServicesDetail(int servicesId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetServicesDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ServicesId", servicesId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }
        #endregion

        #region EmployeeStateMapping
        public DataTable GetStateMap(int employeeId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetStates_EmpStateMap", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@EmployeeId", employeeId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion


        #region Lead 
        public DataTable GetEmployeeId(string userEmail)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetEmployeeIdByEmail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@UserEmail", userEmail);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetLeadList(string leadCode, string companyName, string contactPersonName, string Email, string contactNo, string companyAddress, string companyCity, int companyStateId, int leadStatusId, int leadSourceId, int createdUserId, int userId, string Status)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetLeads", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@LeadCode", leadCode);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyName", companyName);
                    da.SelectCommand.Parameters.AddWithValue("@ContactPersonName", contactPersonName);
                    da.SelectCommand.Parameters.AddWithValue("@Email", Email);
                    da.SelectCommand.Parameters.AddWithValue("@ContactNo", contactNo);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyAddress", companyAddress);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyCity", companyCity);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyStateId", companyStateId);
                    da.SelectCommand.Parameters.AddWithValue("@LeadSourceId", leadSourceId);
                    da.SelectCommand.Parameters.AddWithValue("@LeadStatusId", leadStatusId);
                    da.SelectCommand.Parameters.AddWithValue("@CreateUserId", createdUserId);
                    da.SelectCommand.Parameters.AddWithValue("@UserId", userId);
                    da.SelectCommand.Parameters.AddWithValue("@Status", Status);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetLeadDetail(int leadId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetLeadDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@leadId", leadId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }

        public DataTable GetLeadStateMap(string EmployeeUserName, int countryId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetStateLead", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@UserName", EmployeeUserName);
                    da.SelectCommand.Parameters.AddWithValue("@CountryId", countryId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion


        #region Tax 
        public DataTable GetTaxList(string taxName = "", string taxType = "0", string taxSubType = "0", int companyId = 0, string status = "")
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetTaxes", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@TaxName", taxName);
                    da.SelectCommand.Parameters.AddWithValue("@TaxType", taxType);
                    da.SelectCommand.Parameters.AddWithValue("@TaxSubType", taxSubType);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@Status", status);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetTaxDetail(int taxId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetTaxDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@taxId", taxId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }
        #endregion 




        #region Employee
        public DataTable GetEmployeeList(string employeeName, string employeeCode, string mobileNo, string email, string panNo, int departmentId, string employeeType, string currentStatus, int companyId, string employeeStatus)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetEmployees", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@EmployeeName", employeeName);
                    da.SelectCommand.Parameters.AddWithValue("@EmployeeCode", employeeCode);
                    da.SelectCommand.Parameters.AddWithValue("@MobileNo", mobileNo);
                    da.SelectCommand.Parameters.AddWithValue("@Email", email);
                    da.SelectCommand.Parameters.AddWithValue("@PANNo", panNo);
                    da.SelectCommand.Parameters.AddWithValue("@DepartmentId", departmentId);
                    da.SelectCommand.Parameters.AddWithValue("@EmployeeType", employeeType);
                    da.SelectCommand.Parameters.AddWithValue("@CurrentStatus", currentStatus);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@EmployeeStatus", employeeStatus);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetEmployeeDetail(int employeeId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetEmployeeDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@EmployeeId", employeeId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }
        #endregion


        #region Book
        public DataTable GetBookList(string bookName = "", string bookType = "0", string bookCode = "", int companyId = 0, string status = "")
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetBooks", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@BookName", bookName);
                    da.SelectCommand.Parameters.AddWithValue("@BookType", bookType);
                    da.SelectCommand.Parameters.AddWithValue("@BookCode", bookCode);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@Status", status);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetBookDetail(int bookId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetBookDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@bookId", bookId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }
        #endregion 


        #region CustomerType
        public DataTable GetCustomerTypeList(string customertypeDesc, string Status)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetCustomerTypes", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CustomerTypeDesc", customertypeDesc);
                    da.SelectCommand.Parameters.AddWithValue("@Status", Status);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetCustomerTypeDetail(int customertypeId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetCustomerTypeDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CustomerTypeId", customertypeId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion






        #region Customer
        public DataTable GetCustomerList(string customerName, string customerCode, string mobileNo, int customerTypeid, int companyId, string customerStatus)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetCustomers", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CustomerName", customerName);
                    da.SelectCommand.Parameters.AddWithValue("@CustomerCode", customerCode);
                    da.SelectCommand.Parameters.AddWithValue("@MobileNo", mobileNo);
                    da.SelectCommand.Parameters.AddWithValue("@CustomerTypeId", customerTypeid);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@CustomerStatus", customerStatus);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetCustomerDetail(int customerId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetCustomerDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CustomerId", customerId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }
        public DataTable GetNewCustomerCount(int companyId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_Dashboard_GetNewCustomersCount", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }

        public DataTable GetTotalCustomerCount(int companyId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_Dashboard_GetTotalCustomersCount", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }

        public DataTable GetTodayNewCustomer(int companyId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_Dashboard_GetNewCustomersToday", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }
        #endregion


        #region Vendor
        public DataTable GetVendorList(string vendorName, string vendorCode, string mobileNo, int companyId, string vendorStatus)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetVendors", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@VendorName", vendorName);
                    da.SelectCommand.Parameters.AddWithValue("@VendorCode", vendorCode);
                    da.SelectCommand.Parameters.AddWithValue("@MobileNo", mobileNo);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@VendorStatus", vendorStatus);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetVendorDetail(int vendorId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetVendorDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@VendorId", vendorId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }

        #endregion




        #region Customer Branch
        public DataTable GetCustomerBranchList(int customerId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetCustomerBranchs", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CustomerId", customerId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion



        #region GL Main Group 
        public DataTable GetGLMainGroupList(string glmaingroupName, string glType, int sequenceNo, int companyId, string status)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetGLMainGroups", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@GLMainGroupName", glmaingroupName);
                    da.SelectCommand.Parameters.AddWithValue("@GLType", glType);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@SequenceNo", sequenceNo);
                    da.SelectCommand.Parameters.AddWithValue("@Status", status);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetGLMainGroupDetail(int glmaingroupId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetGLMainGroupDetails", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@GLMainGroupId", glmaingroupId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion
        #region Schedule 
        public DataTable GetScheduleList(string scheduleName, int companyId, string status)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSchedule", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ScheduleName", scheduleName);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@Status", status);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetScheduleDetail(int scheduleId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetScheduleDetails", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@scheduleId", scheduleId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion
        #region FormType 
        public DataTable GetFormTypeList(string formTypeDesc, int companyId, string status)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_FormType", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@FormTypeDesc", formTypeDesc);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@Status", status);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetFormTypeDetail(int formTypeId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetFormTypeDetails", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@FormTypeId", formTypeId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }




        #endregion

        #region Customer Product
        public DataTable GetCustomerProductList(int customerId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetCustomerProducts", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CustomerId", customerId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion

        #region Vendor Product
        public DataTable GetVendorProductList(int vendorId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetVendorProducts", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@VendorId", vendorId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion

        #region GL Sub Group 
        public DataTable GetGLSubGroupList(string glsubgroupName, string glmaingroupId, int sequenceNo, int companyId, string status)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetGLSubGroups", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@GLSubGroupName", glsubgroupName);
                    da.SelectCommand.Parameters.AddWithValue("@glmaingroupId", glmaingroupId);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@SequenceNo", sequenceNo);
                    da.SelectCommand.Parameters.AddWithValue("@Status", status);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetGLSubGroupDetail(int glsubgroupId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetGLSubGroupDetails", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@GLSubGroupId", glsubgroupId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion


        #region QuotationSetting
        public DataTable GetQuotationSettingList(int companyId, string status)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetQuotationSettings", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@Status", status);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetQuotationSettingDetail(int quotationsettingId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetQuotationSettingDetails", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@QuotationSettingId", quotationsettingId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion


        #region Term Template Details 

        public DataTable GetTermTemplateList(string termtemplateName, int companyId, string status)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetTermTemplates", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@TermTempalteName", termtemplateName);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@Status", status);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetTermTemplateDetail(int termtemplateId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetTermTemplateDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@TermTemplateId", termtemplateId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }



        public DataTable GetTermTemplateDetailList(int termtemplateId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetTermProductDetails", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@TermTemplateId", termtemplateId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion 


        #region Quotation


        public ResponseOut AddEditQuotation(Quotation quotation, List<QuotationProductDetail> quotationProductList, List<QuotationTaxDetail> quotationTaxList, List<QuotationTermsDetail> quotationTermList)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                DataTable dtQuotationProduct = new DataTable();
                dtQuotationProduct.Columns.Add("QuotationProductDetailId", typeof(Int64));
                dtQuotationProduct.Columns.Add("QuotationId", typeof(Int64));
                dtQuotationProduct.Columns.Add("ProductId", typeof(Int64));
                dtQuotationProduct.Columns.Add("ProductShortDesc", typeof(string));
                dtQuotationProduct.Columns.Add("Price", typeof(decimal));
                dtQuotationProduct.Columns.Add("Quantity", typeof(decimal));
                dtQuotationProduct.Columns.Add("DiscountPercentage", typeof(decimal));
                dtQuotationProduct.Columns.Add("DiscountAmount", typeof(decimal));
                dtQuotationProduct.Columns.Add("TaxPercentage", typeof(decimal));
                dtQuotationProduct.Columns.Add("TaxAmount", typeof(decimal));
                dtQuotationProduct.Columns.Add("TotalPrice", typeof(decimal));

                if (quotationProductList != null && quotationProductList.Count > 0)
                {
                    foreach (QuotationProductDetail item in quotationProductList)
                    {
                        DataRow dtrow = dtQuotationProduct.NewRow();
                        dtrow["QuotationProductDetailId"] = 0;
                        dtrow["QuotationId"] = 0;
                        dtrow["ProductId"] = item.ProductId;
                        dtrow["ProductShortDesc"] = item.ProductShortDesc;
                        dtrow["Price"] = item.Price;
                        dtrow["Quantity"] = item.Quantity;
                        dtrow["DiscountPercentage"] = item.DiscountPercentage;
                        dtrow["DiscountAmount"] = item.DiscountAmount;
                        dtrow["TaxPercentage"] = item.TaxPercentage;
                        dtrow["TaxAmount"] = item.TaxAmount;
                        dtrow["TotalPrice"] = item.TotalPrice;
                        dtQuotationProduct.Rows.Add(dtrow);
                    }
                    dtQuotationProduct.AcceptChanges();

                }

                DataTable dtQuotationTax = new DataTable();
                dtQuotationTax.Columns.Add("TaxId", typeof(Int64));
                dtQuotationTax.Columns.Add("TaxName", typeof(string));
                dtQuotationTax.Columns.Add("TaxPercentage", typeof(decimal));
                dtQuotationTax.Columns.Add("TaxAmount", typeof(decimal));

                if (quotationTaxList != null && quotationTaxList.Count > 0)
                {
                    foreach (QuotationTaxDetail item in quotationTaxList)
                    {
                        DataRow dtrow = dtQuotationTax.NewRow();
                        dtrow["TaxId"] = item.TaxId;
                        dtrow["TaxName"] = item.TaxName;
                        dtrow["TaxPercentage"] = item.TaxPercentage;
                        dtrow["TaxAmount"] = item.TaxAmount;

                        dtQuotationTax.Rows.Add(dtrow);
                    }
                    dtQuotationTax.AcceptChanges();
                }

                DataTable dtQuotationTerm = new DataTable();
                dtQuotationTerm.Columns.Add("TermDesc", typeof(string));
                dtQuotationTerm.Columns.Add("TermSequence", typeof(int));

                if (quotationTermList != null && quotationTermList.Count > 0)
                {
                    foreach (QuotationTermsDetail item in quotationTermList)
                    {
                        DataRow dtrow = dtQuotationTerm.NewRow();
                        dtrow["TermDesc"] = item.TermDesc;
                        dtrow["TermSequence"] = item.TermSequence;
                        dtQuotationTerm.Rows.Add(dtrow);
                    }
                    dtQuotationTerm.AcceptChanges();
                }

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("proc_AddEditQuotation", con))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@QuotationId", quotation.QuotationId);
                        sqlCommand.Parameters.AddWithValue("@QuotationDate", quotation.QuotationDate);
                        sqlCommand.Parameters.AddWithValue("@CustomerId", quotation.CustomerId);
                        sqlCommand.Parameters.AddWithValue("@CustomerName", quotation.CustomerName);
                        sqlCommand.Parameters.AddWithValue("@BillingAddress", quotation.BillingAddress);
                        sqlCommand.Parameters.AddWithValue("@City", quotation.City);
                        sqlCommand.Parameters.AddWithValue("@StateId", quotation.StateId);
                        sqlCommand.Parameters.AddWithValue("@CountryId", quotation.CountryId);
                        sqlCommand.Parameters.AddWithValue("@PinCode", quotation.PinCode);
                        sqlCommand.Parameters.AddWithValue("@CSTNo", quotation.CSTNo);
                        sqlCommand.Parameters.AddWithValue("@TINNo", quotation.TINNo);
                        sqlCommand.Parameters.AddWithValue("@PANNo", quotation.PANNo);
                        sqlCommand.Parameters.AddWithValue("@GSTNo", quotation.GSTNo);
                        sqlCommand.Parameters.AddWithValue("@ExciseNo", quotation.ExciseNo);
                        sqlCommand.Parameters.AddWithValue("@RefNo", quotation.RefNo);
                        sqlCommand.Parameters.AddWithValue("@RefDate", quotation.RefDate);
                        sqlCommand.Parameters.AddWithValue("@BasicValue", quotation.BasicValue);
                        sqlCommand.Parameters.AddWithValue("@TotalValue", quotation.TotalValue);
                        sqlCommand.Parameters.AddWithValue("@FinYearId", quotation.FinYearId);
                        sqlCommand.Parameters.AddWithValue("@CompanyId", quotation.CompanyId);
                        sqlCommand.Parameters.AddWithValue("@CreatedBy", quotation.CreatedBy);
                        sqlCommand.Parameters.AddWithValue("@ApprovalStatus", quotation.ApprovalStatus);
                        sqlCommand.Parameters.AddWithValue("@QuotationProductDetail", dtQuotationProduct);
                        sqlCommand.Parameters.AddWithValue("@QuotationTaxDetail", dtQuotationTax);
                        sqlCommand.Parameters.AddWithValue("@QuotationTermDetail", dtQuotationTerm);
                        sqlCommand.Parameters.Add("@status", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@RetQuotationId", SqlDbType.BigInt).Direction = ParameterDirection.Output;

                        con.Open();
                        sqlCommand.ExecuteNonQuery();
                        if (sqlCommand.Parameters["@status"].Value != null)
                        {
                            responseOut.status = Convert.ToString(sqlCommand.Parameters["@status"].Value);
                        }
                        if (responseOut.status != ActionStatus.Success)
                        {
                            if (sqlCommand.Parameters["@message"].Value != null)
                            {
                                responseOut.message = Convert.ToString(sqlCommand.Parameters["@message"].Value);
                            }

                        }
                        else
                        {
                            if (quotation.QuotationId == 0)
                            {
                                responseOut.message = ActionMessage.QuotationCreatedSuccess;
                            }
                            else
                            {
                                responseOut.message = ActionMessage.QuotationUpdatedSuccess;
                            }
                            if (sqlCommand.Parameters["@RetQuotationId"].Value != null)
                            {
                                responseOut.trnId = Convert.ToInt64(sqlCommand.Parameters["@RetQuotationId"].Value);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return responseOut;

        }
        public DataTable GetQuotationProductList(long quotationId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetQuotationProducts", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@QuotationId", quotationId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetQuotationSOProductList(long quotationId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetQuotationProducts", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@QuotationId", quotationId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetQuotationTaxList(long quotationId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetQuotationTaxes", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@QuotationId", quotationId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetQuotationTermList(long quotationId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetQuotationTerms", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@QuotationId", quotationId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetQuotationList(string quotationNo, string customerName, string refNo, DateTime fromDate, DateTime toDate, int companyId, string displayType = "", string approvalStatus = "")
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetQuotations", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@QuotationNo", quotationNo);
                    da.SelectCommand.Parameters.AddWithValue("@CustomerName", customerName);
                    da.SelectCommand.Parameters.AddWithValue("@RefNo", refNo);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@DisplayType", displayType);
                    da.SelectCommand.Parameters.AddWithValue("@ApprovalStatus", approvalStatus);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetQuotationDetail(long quotationId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetQuotationDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@QuotationId", quotationId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }
        #endregion

        #region PO
        public ResponseOut AddEditPO(PO po, List<POProductDetail> poProductList, List<POTaxDetail> poTaxList, List<POTermsDetail> poTermList)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                DataTable dtPOProduct = new DataTable();
                dtPOProduct.Columns.Add("POProductDetailId", typeof(Int64));
                dtPOProduct.Columns.Add("POId", typeof(Int64));
                dtPOProduct.Columns.Add("ProductId", typeof(Int64));
                dtPOProduct.Columns.Add("ProductShortDesc", typeof(string));
                dtPOProduct.Columns.Add("Price", typeof(decimal));
                dtPOProduct.Columns.Add("Quantity", typeof(decimal));
                dtPOProduct.Columns.Add("DiscountPercentage", typeof(decimal));
                dtPOProduct.Columns.Add("DiscountAmount", typeof(decimal));
                dtPOProduct.Columns.Add("TaxPercentage", typeof(decimal));
                dtPOProduct.Columns.Add("TaxAmount", typeof(decimal));
                dtPOProduct.Columns.Add("TotalPrice", typeof(decimal));
                dtPOProduct.Columns.Add("CGST_Perc", typeof(decimal));
                dtPOProduct.Columns.Add("CGST_Amount", typeof(decimal));
                dtPOProduct.Columns.Add("SGST_Perc", typeof(decimal));
                dtPOProduct.Columns.Add("SGST_Amount", typeof(decimal));
                dtPOProduct.Columns.Add("IGST_Perc", typeof(decimal));
                dtPOProduct.Columns.Add("IGST_Amount", typeof(decimal));
                dtPOProduct.Columns.Add("HSN_Code", typeof(string));


                if (poProductList != null && poProductList.Count > 0)
                {
                    foreach (POProductDetail item in poProductList)
                    {
                        DataRow dtrow = dtPOProduct.NewRow();
                        dtrow["POProductDetailId"] = 0;
                        dtrow["POId"] = 0;
                        dtrow["ProductId"] = item.ProductId;
                        dtrow["ProductShortDesc"] = item.ProductShortDesc;
                        dtrow["Price"] = item.Price;
                        dtrow["Quantity"] = item.Quantity;
                        dtrow["DiscountPercentage"] = item.DiscountPercentage;
                        dtrow["DiscountAmount"] = item.DiscountAmount;
                        dtrow["TaxPercentage"] = item.TaxPercentage;
                        dtrow["TaxAmount"] = item.TaxAmount;
                        dtrow["TotalPrice"] = item.TotalPrice;
                        dtrow["CGST_Perc"] = item.CGST_Perc;
                        dtrow["CGST_Amount"] = item.CGST_Amount;
                        dtrow["SGST_Perc"] = item.SGST_Perc;
                        dtrow["SGST_Amount"] = item.SGST_Amount;
                        dtrow["IGST_Perc"] = item.IGST_Perc;
                        dtrow["IGST_Amount"] = item.IGST_Amount;
                        dtrow["HSN_Code"] = item.HSN_Code;
                        dtPOProduct.Rows.Add(dtrow);
                    }
                    dtPOProduct.AcceptChanges();

                }
                DataTable dtPOTax = new DataTable();
                dtPOTax.Columns.Add("TaxId", typeof(Int64));
                dtPOTax.Columns.Add("TaxName", typeof(string));
                dtPOTax.Columns.Add("TaxPercentage", typeof(decimal));
                dtPOTax.Columns.Add("TaxAmount", typeof(decimal));

                if (poTaxList != null && poTaxList.Count > 0)
                {
                    foreach (POTaxDetail item in poTaxList)
                    {
                        DataRow dtrow = dtPOTax.NewRow();
                        dtrow["TaxId"] = item.TaxId;
                        dtrow["TaxName"] = item.TaxName;
                        dtrow["TaxPercentage"] = item.TaxPercentage;
                        dtrow["TaxAmount"] = item.TaxAmount;

                        dtPOTax.Rows.Add(dtrow);
                    }
                    dtPOTax.AcceptChanges();
                }

                DataTable dtPOTerm = new DataTable();
                dtPOTerm.Columns.Add("TermDesc", typeof(string));
                dtPOTerm.Columns.Add("TermSequence", typeof(int));

                if (poTermList != null && poTermList.Count > 0)
                {
                    foreach (POTermsDetail item in poTermList)
                    {
                        DataRow dtrow = dtPOTerm.NewRow();
                        dtrow["TermDesc"] = item.TermDesc;
                        dtrow["TermSequence"] = item.TermSequence;
                        dtPOTerm.Rows.Add(dtrow);
                    }
                    dtPOTerm.AcceptChanges();
                }
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("proc_AddEditPO", con))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@POId", po.POId);
                        sqlCommand.Parameters.AddWithValue("@PODate", po.PODate);
                        sqlCommand.Parameters.AddWithValue("@VendorId", po.VendorId);
                        sqlCommand.Parameters.AddWithValue("@VendorName", po.VendorName);
                        sqlCommand.Parameters.AddWithValue("@BillingAddress", po.BillingAddress);
                        sqlCommand.Parameters.AddWithValue("@ShippingAddress", po.ShippingAddress);
                        sqlCommand.Parameters.AddWithValue("@City", po.City);
                        sqlCommand.Parameters.AddWithValue("@StateId", po.StateId);
                        sqlCommand.Parameters.AddWithValue("@CountryId", po.CountryId);
                        sqlCommand.Parameters.AddWithValue("@PinCode", po.PinCode);
                        sqlCommand.Parameters.AddWithValue("@CSTNo", po.CSTNo);
                        sqlCommand.Parameters.AddWithValue("@TINNo", po.TINNo);
                        sqlCommand.Parameters.AddWithValue("@PANNo", po.PANNo);
                        sqlCommand.Parameters.AddWithValue("@GSTNo", po.GSTNo);
                        sqlCommand.Parameters.AddWithValue("@ExciseNo", po.ExciseNo);

                        sqlCommand.Parameters.AddWithValue("@ApprovalStatus", po.ApprovalStatus);
                        sqlCommand.Parameters.AddWithValue("@RefNo", po.RefNo);
                        sqlCommand.Parameters.AddWithValue("@RefDate", po.RefDate);
                        sqlCommand.Parameters.AddWithValue("@BasicValue", po.BasicValue);
                        sqlCommand.Parameters.AddWithValue("@TotalValue", po.TotalValue);

                        sqlCommand.Parameters.AddWithValue("@ShippingCity", po.ShippingCity);
                        sqlCommand.Parameters.AddWithValue("@ShippingStateId", po.ShippingStateId);
                        sqlCommand.Parameters.AddWithValue("@ShippingCountryId", po.ShippingCountryId);
                        sqlCommand.Parameters.AddWithValue("@ShippingPinCode", po.ShippingPinCode);
                        sqlCommand.Parameters.AddWithValue("@ConsigneeGSTNo", po.ConsigneeGSTNo);


                        sqlCommand.Parameters.AddWithValue("@FreightValue", po.FreightValue);

                        sqlCommand.Parameters.AddWithValue("@ConsigneeId", po.ConsigneeId);
                        sqlCommand.Parameters.AddWithValue("@ConsigneeName", po.ConsigneeName);
                        sqlCommand.Parameters.AddWithValue("@FreightCGST_Perc", po.FreightCGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@FreightCGST_Amt", po.FreightCGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@FreightSGST_Perc", po.FreightSGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@FreightSGST_Amt", po.FreightSGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@FreightIGST_Perc", po.FreightIGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@FreightIGST_Amt", po.FreightIGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@LoadingValue", po.LoadingValue);
                        sqlCommand.Parameters.AddWithValue("@LoadingCGST_Perc", po.LoadingCGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@LoadingCGST_Amt", po.LoadingCGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@LoadingSGST_Perc", po.LoadingSGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@LoadingSGST_Amt", po.LoadingSGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@LoadingIGST_Perc", po.LoadingIGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@LoadingIGST_Amt", po.LoadingIGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@InsuranceValue", po.InsuranceValue);
                        sqlCommand.Parameters.AddWithValue("@InsuranceCGST_Perc", po.InsuranceCGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@InsuranceCGST_Amt", po.InsuranceCGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@InsuranceSGST_Perc", po.InsuranceSGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@InsuranceSGST_Amt", po.InsuranceSGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@InsuranceIGST_Perc", po.InsuranceIGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@InsuranceIGST_Amt", po.InsuranceIGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@ReverseChargeApplicable", po.ReverseChargeApplicable);
                        sqlCommand.Parameters.AddWithValue("@ReverseChargeAmount", po.RevserseChargeAmount);

                        sqlCommand.Parameters.AddWithValue("@Remarks1", po.Remarks1);
                        sqlCommand.Parameters.AddWithValue("@Remarks2", po.Remarks2);
                        sqlCommand.Parameters.AddWithValue("@CurrencyCode", po.CurrencyCode);
                        sqlCommand.Parameters.AddWithValue("@FinYearId", po.FinYearId);
                        sqlCommand.Parameters.AddWithValue("@CompanyId", po.CompanyId);
                        sqlCommand.Parameters.AddWithValue("@CreatedBy", po.CreatedBy);
                        sqlCommand.Parameters.AddWithValue("@POProductDetail", dtPOProduct);
                        sqlCommand.Parameters.AddWithValue("@POTaxDetail", dtPOTax);
                        sqlCommand.Parameters.AddWithValue("@POTermDetail", dtPOTerm);
                        sqlCommand.Parameters.Add("@status", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@RetPOId", SqlDbType.BigInt).Direction = ParameterDirection.Output;

                        con.Open();
                        sqlCommand.ExecuteNonQuery();
                        if (sqlCommand.Parameters["@status"].Value != null)
                        {
                            responseOut.status = Convert.ToString(sqlCommand.Parameters["@status"].Value);
                        }
                        if (responseOut.status != ActionStatus.Success)
                        {
                            if (sqlCommand.Parameters["@message"].Value != null)
                            {
                                responseOut.message = Convert.ToString(sqlCommand.Parameters["@message"].Value);
                            }

                        }
                        else
                        {
                            if (po.POId == 0)
                            {
                                responseOut.message = ActionMessage.POCreatedSuccess;
                            }
                            else
                            {
                                responseOut.message = ActionMessage.POUpdatedSuccess;
                            }
                            if (sqlCommand.Parameters["@RetPOId"].Value != null)
                            {
                                responseOut.trnId = Convert.ToInt64(sqlCommand.Parameters["@RetPOId"].Value);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return responseOut;

        }
        public DataTable GetPOProductList(long poId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetPOProducts", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@POId", poId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetPOTaxList(long poId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetPOTaxes", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@POId", poId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetPOTermList(long poId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetPOTerms", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@POId", poId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetPOList(string poNo, string vendorName, string refNo, DateTime fromDate, DateTime toDate, string approvalStatus, int companyId, string displayType = "")
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetPOs", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@PONo", poNo);
                    da.SelectCommand.Parameters.AddWithValue("@VendorName", vendorName);
                    da.SelectCommand.Parameters.AddWithValue("@RefNo", refNo);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@ApprovalStatus", approvalStatus);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@DisplayType", displayType);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetPODetail(long poId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetPODetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@POId", poId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }
        #endregion

        #region  Import PO
        public ResponseOut AddEditImportPO(PO po, List<POProductDetail> poProductList, List<POTaxDetail> poTaxList, List<POTermsDetail> poTermList)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                DataTable dtPOProduct = new DataTable();
                dtPOProduct.Columns.Add("POProductDetailId", typeof(Int64));
                dtPOProduct.Columns.Add("POId", typeof(Int64));
                dtPOProduct.Columns.Add("ProductId", typeof(Int64));
                dtPOProduct.Columns.Add("ProductShortDesc", typeof(string));
                dtPOProduct.Columns.Add("Price", typeof(decimal));
                dtPOProduct.Columns.Add("Quantity", typeof(decimal));
                dtPOProduct.Columns.Add("DiscountPercentage", typeof(decimal));
                dtPOProduct.Columns.Add("DiscountAmount", typeof(decimal));
                dtPOProduct.Columns.Add("TaxPercentage", typeof(decimal));
                dtPOProduct.Columns.Add("TaxAmount", typeof(decimal));
                dtPOProduct.Columns.Add("TotalPrice", typeof(decimal));
                dtPOProduct.Columns.Add("CGST_Perc", typeof(decimal));
                dtPOProduct.Columns.Add("CGST_Amount", typeof(decimal));
                dtPOProduct.Columns.Add("SGST_Perc", typeof(decimal));
                dtPOProduct.Columns.Add("SGST_Amount", typeof(decimal));
                dtPOProduct.Columns.Add("IGST_Perc", typeof(decimal));
                dtPOProduct.Columns.Add("IGST_Amount", typeof(decimal));
                dtPOProduct.Columns.Add("HSN_Code", typeof(string));


                if (poProductList != null && poProductList.Count > 0)
                {
                    foreach (POProductDetail item in poProductList)
                    {
                        DataRow dtrow = dtPOProduct.NewRow();
                        dtrow["POProductDetailId"] = 0;
                        dtrow["POId"] = 0;
                        dtrow["ProductId"] = item.ProductId;
                        dtrow["ProductShortDesc"] = item.ProductShortDesc;
                        dtrow["Price"] = item.Price;
                        dtrow["Quantity"] = item.Quantity;
                        dtrow["DiscountPercentage"] = item.DiscountPercentage;
                        dtrow["DiscountAmount"] = item.DiscountAmount;
                        dtrow["TaxPercentage"] = item.TaxPercentage;
                        dtrow["TaxAmount"] = item.TaxAmount;
                        dtrow["TotalPrice"] = item.TotalPrice;
                        dtrow["CGST_Perc"] = 0;
                        dtrow["CGST_Amount"] = 0;
                        dtrow["SGST_Perc"] = 0;
                        dtrow["SGST_Amount"] = 0;
                        dtrow["IGST_Perc"] = 0;
                        dtrow["IGST_Amount"] = 0;
                        dtrow["HSN_Code"] = item.HSN_Code;
                        dtPOProduct.Rows.Add(dtrow);
                    }
                    dtPOProduct.AcceptChanges();

                }
                DataTable dtPOTax = new DataTable();
                dtPOTax.Columns.Add("TaxId", typeof(Int64));
                dtPOTax.Columns.Add("TaxName", typeof(string));
                dtPOTax.Columns.Add("TaxPercentage", typeof(decimal));
                dtPOTax.Columns.Add("TaxAmount", typeof(decimal));

                if (poTaxList != null && poTaxList.Count > 0)
                {
                    foreach (POTaxDetail item in poTaxList)
                    {
                        DataRow dtrow = dtPOTax.NewRow();
                        dtrow["TaxId"] = item.TaxId;
                        dtrow["TaxName"] = item.TaxName;
                        dtrow["TaxPercentage"] = item.TaxPercentage;
                        dtrow["TaxAmount"] = item.TaxAmount;

                        dtPOTax.Rows.Add(dtrow);
                    }
                    dtPOTax.AcceptChanges();
                }

                DataTable dtPOTerm = new DataTable();
                dtPOTerm.Columns.Add("TermDesc", typeof(string));
                dtPOTerm.Columns.Add("TermSequence", typeof(int));

                if (poTermList != null && poTermList.Count > 0)
                {
                    foreach (POTermsDetail item in poTermList)
                    {
                        DataRow dtrow = dtPOTerm.NewRow();
                        dtrow["TermDesc"] = item.TermDesc;
                        dtrow["TermSequence"] = item.TermSequence;
                        dtPOTerm.Rows.Add(dtrow);
                    }
                    dtPOTerm.AcceptChanges();
                }
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("proc_AddEditImportPO", con))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@POId", po.POId);
                        sqlCommand.Parameters.AddWithValue("@PODate", po.PODate);
                        sqlCommand.Parameters.AddWithValue("@VendorId", po.VendorId);
                        sqlCommand.Parameters.AddWithValue("@VendorName", po.VendorName);
                        sqlCommand.Parameters.AddWithValue("@BillingAddress", po.BillingAddress);
                        sqlCommand.Parameters.AddWithValue("@ShippingAddress", po.ShippingAddress);
                        sqlCommand.Parameters.AddWithValue("@City", po.City);
                        sqlCommand.Parameters.AddWithValue("@StateId", po.StateId);
                        sqlCommand.Parameters.AddWithValue("@CountryId", po.CountryId);
                        sqlCommand.Parameters.AddWithValue("@PinCode", po.PinCode);
                        sqlCommand.Parameters.AddWithValue("@CSTNo", po.CSTNo);
                        sqlCommand.Parameters.AddWithValue("@TINNo", po.TINNo);
                        sqlCommand.Parameters.AddWithValue("@PANNo", po.PANNo);
                        sqlCommand.Parameters.AddWithValue("@GSTNo", po.GSTNo);
                        sqlCommand.Parameters.AddWithValue("@ExciseNo", po.ExciseNo);

                        sqlCommand.Parameters.AddWithValue("@ApprovalStatus", po.ApprovalStatus);
                        sqlCommand.Parameters.AddWithValue("@RefNo", po.RefNo);
                        sqlCommand.Parameters.AddWithValue("@RefDate", po.RefDate);
                        sqlCommand.Parameters.AddWithValue("@BasicValue", po.BasicValue);
                        sqlCommand.Parameters.AddWithValue("@TotalValue", po.TotalValue);

                        sqlCommand.Parameters.AddWithValue("@ShippingCity", po.ShippingCity);
                        sqlCommand.Parameters.AddWithValue("@ShippingStateId", po.ShippingStateId);
                        sqlCommand.Parameters.AddWithValue("@ShippingCountryId", po.ShippingCountryId);
                        sqlCommand.Parameters.AddWithValue("@ShippingPinCode", po.ShippingPinCode);
                        sqlCommand.Parameters.AddWithValue("@ConsigneeGSTNo", po.ConsigneeGSTNo);


                        sqlCommand.Parameters.AddWithValue("@FreightValue", po.FreightValue);

                        sqlCommand.Parameters.AddWithValue("@ConsigneeId", po.ConsigneeId);
                        sqlCommand.Parameters.AddWithValue("@ConsigneeName", po.ConsigneeName);
                        sqlCommand.Parameters.AddWithValue("@FreightCGST_Perc", po.FreightCGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@FreightCGST_Amt", po.FreightCGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@FreightSGST_Perc", po.FreightSGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@FreightSGST_Amt", po.FreightSGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@FreightIGST_Perc", po.FreightIGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@FreightIGST_Amt", po.FreightIGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@LoadingValue", po.LoadingValue);
                        sqlCommand.Parameters.AddWithValue("@LoadingCGST_Perc", po.LoadingCGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@LoadingCGST_Amt", po.LoadingCGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@LoadingSGST_Perc", po.LoadingSGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@LoadingSGST_Amt", po.LoadingSGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@LoadingIGST_Perc", po.LoadingIGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@LoadingIGST_Amt", po.LoadingIGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@InsuranceValue", po.InsuranceValue);
                        sqlCommand.Parameters.AddWithValue("@InsuranceCGST_Perc", po.InsuranceCGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@InsuranceCGST_Amt", po.InsuranceCGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@InsuranceSGST_Perc", po.InsuranceSGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@InsuranceSGST_Amt", po.InsuranceSGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@InsuranceIGST_Perc", po.InsuranceIGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@InsuranceIGST_Amt", po.InsuranceIGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@ReverseChargeApplicable", po.ReverseChargeApplicable);
                        sqlCommand.Parameters.AddWithValue("@ReverseChargeAmount", po.RevserseChargeAmount);

                        sqlCommand.Parameters.AddWithValue("@Remarks1", po.Remarks1);
                        sqlCommand.Parameters.AddWithValue("@Remarks2", po.Remarks2);
                        sqlCommand.Parameters.AddWithValue("@CurrencyCode", po.CurrencyCode);

                        sqlCommand.Parameters.AddWithValue("@FreightTypeCode", po.FreightTypeCode);
                        sqlCommand.Parameters.AddWithValue("@ConversionRate", po.ConversionRate);
                        sqlCommand.Parameters.AddWithValue("@OtherPrice", po.OtherPrice);
                        sqlCommand.Parameters.AddWithValue("@POType", po.POType);


                        sqlCommand.Parameters.AddWithValue("@FinYearId", po.FinYearId);
                        sqlCommand.Parameters.AddWithValue("@CompanyId", po.CompanyId);
                        sqlCommand.Parameters.AddWithValue("@CreatedBy", po.CreatedBy);
                        sqlCommand.Parameters.AddWithValue("@POProductDetail", dtPOProduct);
                        sqlCommand.Parameters.AddWithValue("@POTaxDetail", dtPOTax);
                        sqlCommand.Parameters.AddWithValue("@POTermDetail", dtPOTerm);
                        sqlCommand.Parameters.Add("@status", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@RetPOId", SqlDbType.BigInt).Direction = ParameterDirection.Output;

                        con.Open();
                        sqlCommand.ExecuteNonQuery();
                        if (sqlCommand.Parameters["@status"].Value != null)
                        {
                            responseOut.status = Convert.ToString(sqlCommand.Parameters["@status"].Value);
                        }
                        if (responseOut.status != ActionStatus.Success)
                        {
                            if (sqlCommand.Parameters["@message"].Value != null)
                            {
                                responseOut.message = Convert.ToString(sqlCommand.Parameters["@message"].Value);
                            }

                        }
                        else
                        {
                            if (po.POId == 0)
                            {
                                // responseOut.message = ActionMessage.ImportPOCreatedSuccess;
                            }
                            else
                            {
                                //responseOut.message = ActionMessage.ImportPOUpdatedSuccess;
                            }
                            if (sqlCommand.Parameters["@RetPOId"].Value != null)
                            {
                                responseOut.trnId = Convert.ToInt64(sqlCommand.Parameters["@RetPOId"].Value);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return responseOut;

        }

        public DataTable GetImportPODetail(long poId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetImportPODetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@POId", poId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }

        public DataTable GetImportPOList(string poNo, string vendorName, string refNo, DateTime fromDate, DateTime toDate, string approvalStatus, int companyId, string displayType = "")
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetImportPOs", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@PONo", poNo);
                    da.SelectCommand.Parameters.AddWithValue("@VendorName", vendorName);
                    da.SelectCommand.Parameters.AddWithValue("@RefNo", refNo);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@ApprovalStatus", approvalStatus);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@DisplayType", displayType);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion

        #region PurchaseInvoice
        public ResponseOut AddEditPI(PurchaseInvoice pi, List<PurchaseInvoiceProductDetail> piProductList, List<PurchaseInvoiceTaxDetail> piTaxList, List<PurchaseInvoiceTermsDetail> piTermList)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                DataTable dtPIProduct = new DataTable();
                dtPIProduct.Columns.Add("InvoiceProductDetailId", typeof(Int64));
                dtPIProduct.Columns.Add("InvoiceId", typeof(Int64));
                dtPIProduct.Columns.Add("ProductId", typeof(Int64));
                dtPIProduct.Columns.Add("ProductShortDesc", typeof(string));
                dtPIProduct.Columns.Add("Price", typeof(decimal));
                dtPIProduct.Columns.Add("Quantity", typeof(decimal));
                dtPIProduct.Columns.Add("DiscountPercentage", typeof(decimal));
                dtPIProduct.Columns.Add("DiscountAmount", typeof(decimal));
                dtPIProduct.Columns.Add("TaxPercentage", typeof(decimal));
                dtPIProduct.Columns.Add("TaxAmount", typeof(decimal));
                dtPIProduct.Columns.Add("TotalPrice", typeof(decimal));
                dtPIProduct.Columns.Add("CGST_Perc", typeof(decimal));
                dtPIProduct.Columns.Add("CGST_Amount", typeof(decimal));
                dtPIProduct.Columns.Add("SGST_Perc", typeof(decimal));
                dtPIProduct.Columns.Add("SGST_Amount", typeof(decimal));
                dtPIProduct.Columns.Add("IGST_Perc", typeof(decimal));
                dtPIProduct.Columns.Add("IGST_Amount", typeof(decimal));
                dtPIProduct.Columns.Add("HSN_Code", typeof(string));

                if (piProductList != null && piProductList.Count > 0)
                {
                    foreach (PurchaseInvoiceProductDetail item in piProductList)
                    {
                        DataRow dtrow = dtPIProduct.NewRow();
                        dtrow["InvoiceProductDetailId"] = 0;
                        dtrow["InvoiceId"] = 0;
                        dtrow["ProductId"] = item.ProductId;
                        dtrow["ProductShortDesc"] = item.ProductShortDesc;
                        dtrow["Price"] = item.Price;
                        dtrow["Quantity"] = item.Quantity;
                        dtrow["DiscountPercentage"] = item.DiscountPercentage;
                        dtrow["DiscountAmount"] = item.DiscountAmount;
                        dtrow["TaxPercentage"] = item.TaxPercentage;
                        dtrow["TaxAmount"] = item.TaxAmount;
                        dtrow["TotalPrice"] = item.TotalPrice;
                        dtrow["CGST_Perc"] = item.CGST_Perc;
                        dtrow["CGST_Amount"] = item.CGST_Amount;
                        dtrow["SGST_Perc"] = item.SGST_Perc;
                        dtrow["SGST_Amount"] = item.SGST_Amount;
                        dtrow["IGST_Perc"] = item.IGST_Perc;
                        dtrow["IGST_Amount"] = item.IGST_Amount;
                        dtrow["HSN_Code"] = item.HSN_Code;
                        dtPIProduct.Rows.Add(dtrow);
                    }
                    dtPIProduct.AcceptChanges();

                }
                DataTable dtPITax = new DataTable();
                dtPITax.Columns.Add("TaxId", typeof(Int64));
                dtPITax.Columns.Add("TaxName", typeof(string));
                dtPITax.Columns.Add("TaxPercentage", typeof(decimal));
                dtPITax.Columns.Add("TaxAmount", typeof(decimal));

                if (piTaxList != null && piTaxList.Count > 0)
                {
                    foreach (PurchaseInvoiceTaxDetail item in piTaxList)
                    {
                        DataRow dtrow = dtPITax.NewRow();
                        dtrow["TaxId"] = item.TaxId;
                        dtrow["TaxName"] = item.TaxName;
                        dtrow["TaxPercentage"] = item.TaxPercentage;
                        dtrow["TaxAmount"] = item.TaxAmount;

                        dtPITax.Rows.Add(dtrow);
                    }
                    dtPITax.AcceptChanges();
                }

                DataTable dtPITerm = new DataTable();
                dtPITerm.Columns.Add("TermDesc", typeof(string));
                dtPITerm.Columns.Add("TermSequence", typeof(int));

                if (piTermList != null && piTermList.Count > 0)
                {
                    foreach (PurchaseInvoiceTermsDetail item in piTermList)
                    {
                        DataRow dtrow = dtPITerm.NewRow();
                        dtrow["TermDesc"] = item.TermDesc;
                        dtrow["TermSequence"] = item.TermSequence;
                        dtPITerm.Rows.Add(dtrow);
                    }
                    dtPITerm.AcceptChanges();
                }
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("proc_AddEditPurchaseInvoice", con))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@InvoiceId", pi.InvoiceId);
                        sqlCommand.Parameters.AddWithValue("@InvoiceDate", pi.InvoiceDate);
                        sqlCommand.Parameters.AddWithValue("@POId", pi.POId);
                        sqlCommand.Parameters.AddWithValue("@PONo", pi.PONo);
                        sqlCommand.Parameters.AddWithValue("@VendorId", pi.VendorId);
                        sqlCommand.Parameters.AddWithValue("@VendorName", pi.VendorName);
                        sqlCommand.Parameters.AddWithValue("@BillingAddress", pi.BillingAddress);
                        sqlCommand.Parameters.AddWithValue("@City", pi.City);
                        sqlCommand.Parameters.AddWithValue("@StateId", pi.StateId);
                        sqlCommand.Parameters.AddWithValue("@CountryId", pi.CountryId);
                        sqlCommand.Parameters.AddWithValue("@PinCode", pi.PinCode);
                        sqlCommand.Parameters.AddWithValue("@CSTNo", pi.CSTNo);
                        sqlCommand.Parameters.AddWithValue("@TINNo", pi.TINNo);
                        sqlCommand.Parameters.AddWithValue("@PANNo", pi.PANNo);
                        sqlCommand.Parameters.AddWithValue("@GSTNo", pi.GSTNo);
                        sqlCommand.Parameters.AddWithValue("@ExciseNo", pi.ExciseNo);
                        sqlCommand.Parameters.AddWithValue("@ApprovalStatus", pi.ApprovalStatus);

                        sqlCommand.Parameters.AddWithValue("@RefNo", pi.RefNo);
                        sqlCommand.Parameters.AddWithValue("@RefDate", pi.RefDate);
                        sqlCommand.Parameters.AddWithValue("@BasicValue", pi.BasicValue);
                        sqlCommand.Parameters.AddWithValue("@TotalValue", pi.TotalValue);

                        sqlCommand.Parameters.AddWithValue("@ShippingAddress", pi.ShippingAddress);
                        sqlCommand.Parameters.AddWithValue("@ShippingCity", pi.ShippingCity);
                        sqlCommand.Parameters.AddWithValue("@ShippingStateId", pi.ShippingStateId);
                        sqlCommand.Parameters.AddWithValue("@ShippingCountryId", pi.ShippingCountryId);
                        sqlCommand.Parameters.AddWithValue("@ShippingPinCode", pi.ShippingPinCode);
                        sqlCommand.Parameters.AddWithValue("@ConsigneeGSTNo", pi.ConsigneeGSTNo);


                        sqlCommand.Parameters.AddWithValue("@FreightValue", pi.FreightValue);

                        sqlCommand.Parameters.AddWithValue("@ConsigneeId", pi.ConsigneeId);
                        sqlCommand.Parameters.AddWithValue("@ConsigneeName", pi.ConsigneeName);
                        sqlCommand.Parameters.AddWithValue("@FreightCGST_Perc", pi.FreightCGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@FreightCGST_Amt", pi.FreightCGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@FreightSGST_Perc", pi.FreightSGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@FreightSGST_Amt", pi.FreightSGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@FreightIGST_Perc", pi.FreightIGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@FreightIGST_Amt", pi.FreightIGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@LoadingValue", pi.LoadingValue);
                        sqlCommand.Parameters.AddWithValue("@LoadingCGST_Perc", pi.LoadingCGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@LoadingCGST_Amt", pi.LoadingCGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@LoadingSGST_Perc", pi.LoadingSGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@LoadingSGST_Amt", pi.LoadingSGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@LoadingIGST_Perc", pi.LoadingIGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@LoadingIGST_Amt", pi.LoadingIGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@InsuranceValue", pi.InsuranceValue);
                        sqlCommand.Parameters.AddWithValue("@InsuranceCGST_Perc", pi.InsuranceCGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@InsuranceCGST_Amt", pi.InsuranceCGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@InsuranceSGST_Perc", pi.InsuranceSGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@InsuranceSGST_Amt", pi.InsuranceSGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@InsuranceIGST_Perc", pi.InsuranceIGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@InsuranceIGST_Amt", pi.InsuranceIGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@ReverseChargeApplicable", pi.ReverseChargeApplicable);
                        sqlCommand.Parameters.AddWithValue("@ReverseChargeAmount", pi.RevserseChargeAmount);



                        sqlCommand.Parameters.AddWithValue("@Remarks", pi.Remarks);
                        sqlCommand.Parameters.AddWithValue("@FinYearId", pi.FinYearId);
                        sqlCommand.Parameters.AddWithValue("@CompanyId", pi.CompanyId);
                        sqlCommand.Parameters.AddWithValue("@CreatedBy", pi.CreatedBy);
                        sqlCommand.Parameters.AddWithValue("@PurchaseInvoiceProductDetail", dtPIProduct);
                        sqlCommand.Parameters.AddWithValue("@PurchaseInvoiceTaxDetail", dtPITax);
                        sqlCommand.Parameters.AddWithValue("@PurchaseInvoiceTermDetail", dtPITerm);
                        sqlCommand.Parameters.Add("@status", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@RetInvoiceId", SqlDbType.BigInt).Direction = ParameterDirection.Output;

                        con.Open();
                        sqlCommand.ExecuteNonQuery();
                        if (sqlCommand.Parameters["@status"].Value != null)
                        {
                            responseOut.status = Convert.ToString(sqlCommand.Parameters["@status"].Value);
                        }
                        if (responseOut.status != ActionStatus.Success)
                        {
                            if (sqlCommand.Parameters["@message"].Value != null)
                            {
                                responseOut.message = Convert.ToString(sqlCommand.Parameters["@message"].Value);
                            }

                        }
                        else
                        {
                            if (pi.InvoiceId == 0)
                            {
                                responseOut.message = ActionMessage.PICreatedSuccess;
                            }
                            else
                            {
                                responseOut.message = ActionMessage.PIUpdatedSuccess;
                            }
                            if (sqlCommand.Parameters["@RetInvoiceId"].Value != null)
                            {
                                responseOut.trnId = Convert.ToInt64(sqlCommand.Parameters["@RetInvoiceId"].Value);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return responseOut;

        }
        public DataTable GetPIProductList(long invoiceId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetPurchaseInvoiceProducts", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@InvoiceId", invoiceId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetPITaxList(long invoiceId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetPurchaseInvoiceTaxDetailTaxes", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@InvoiceId", invoiceId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetPITermList(long invoiceId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {

                    da = new SqlDataAdapter("proc_GetPurchaseInvoiceTermsDetailTerms", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@InvoiceId", invoiceId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetPIList(string piNo, string vendorName, string refNo, string fromDate, string toDate, int companyId, string approvalStatus = "", string displayType = "")
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {

                    da = new SqlDataAdapter("proc_GetPIs", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@InvoiceNo", piNo);
                    da.SelectCommand.Parameters.AddWithValue("@VendorName", vendorName);
                    da.SelectCommand.Parameters.AddWithValue("@RefNo", refNo);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@ApprovalStatus", approvalStatus);
                    da.SelectCommand.Parameters.AddWithValue("@DisplayType", displayType);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetPIDetail(long invoiceId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetPIDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@InvoiceId", invoiceId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }
        #endregion

        #region Lead FollowUp
        public DataTable GetLeadFollowUpList(int leadId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetLeadFollowUp", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@leadid", leadId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion


        #region DocumentType
        public DataTable GetDocumentTypeList(string documenttypeDesc, int companyId, string Status)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetDocumentTypes", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@DocumentTypeDesc", documenttypeDesc);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@Status", Status);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }
        public DataTable GetDocumentTypeDetail(int documenttypeId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetDocumentTypeDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@DocumentTypeId", documenttypeId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion


        #region  SO

        public ResponseOut AddEditSO(SO so, List<SOProductDetail> soProductList, List<SOTaxDetail> soTaxList, List<SOTermsDetail> soTermList)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                DataTable dtSOProduct = new DataTable();
                dtSOProduct.Columns.Add("SOProductDetailId", typeof(Int64));
                dtSOProduct.Columns.Add("SOId", typeof(Int64));
                dtSOProduct.Columns.Add("ProductId", typeof(Int64));
                dtSOProduct.Columns.Add("ProductShortDesc", typeof(string));
                dtSOProduct.Columns.Add("Price", typeof(decimal));
                dtSOProduct.Columns.Add("Quantity", typeof(decimal));
                dtSOProduct.Columns.Add("DiscountPercentage", typeof(decimal));
                dtSOProduct.Columns.Add("DiscountAmount", typeof(decimal));
                dtSOProduct.Columns.Add("TaxId", typeof(Int64));
                dtSOProduct.Columns.Add("TaxName", typeof(string));
                dtSOProduct.Columns.Add("TaxPercentage", typeof(decimal));
                dtSOProduct.Columns.Add("TaxAmount", typeof(decimal));

                dtSOProduct.Columns.Add("SurchargeName_1", typeof(string));
                dtSOProduct.Columns.Add("SurchargePercentage_1", typeof(decimal));
                dtSOProduct.Columns.Add("SurchargeAmount_1", typeof(decimal));

                dtSOProduct.Columns.Add("SurchargeName_2", typeof(string));
                dtSOProduct.Columns.Add("SurchargePercentage_2", typeof(decimal));
                dtSOProduct.Columns.Add("SurchargeAmount_2", typeof(decimal));

                dtSOProduct.Columns.Add("SurchargeName_3", typeof(string));
                dtSOProduct.Columns.Add("SurchargePercentage_3", typeof(decimal));
                dtSOProduct.Columns.Add("SurchargeAmount_3", typeof(decimal));
                dtSOProduct.Columns.Add("TotalPrice", typeof(decimal));

                dtSOProduct.Columns.Add("CGST_Perc", typeof(decimal));
                dtSOProduct.Columns.Add("CGST_Amount", typeof(decimal));
                dtSOProduct.Columns.Add("SGST_Perc", typeof(decimal));
                dtSOProduct.Columns.Add("SGST_Amount", typeof(decimal));
                dtSOProduct.Columns.Add("IGST_Perc", typeof(decimal));
                dtSOProduct.Columns.Add("IGST_Amount", typeof(decimal));
                dtSOProduct.Columns.Add("HSN_Code", typeof(string));



                if (soProductList != null && soProductList.Count > 0)
                {
                    foreach (SOProductDetail item in soProductList)
                    {
                        DataRow dtrow = dtSOProduct.NewRow();
                        dtrow["SOProductDetailId"] = 0;
                        dtrow["SOId"] = 0;
                        dtrow["ProductId"] = item.ProductId;
                        dtrow["ProductShortDesc"] = item.ProductShortDesc;
                        dtrow["Price"] = item.Price;
                        dtrow["Quantity"] = item.Quantity;
                        dtrow["DiscountPercentage"] = item.DiscountPercentage;
                        dtrow["DiscountAmount"] = item.DiscountAmount;
                        dtrow["TaxId"] = item.TaxId;
                        dtrow["TaxName"] = item.TaxName;
                        dtrow["TaxPercentage"] = item.TaxPercentage;
                        dtrow["TaxAmount"] = item.TaxAmount;

                        dtrow["SurchargeName_1"] = item.SurchargeName_1;
                        dtrow["SurchargePercentage_1"] = item.SurchargePercentage_1;
                        dtrow["SurchargeAmount_1"] = item.SurchargeAmount_1;

                        dtrow["SurchargeName_2"] = item.SurchargeName_2;
                        dtrow["SurchargePercentage_2"] = item.SurchargePercentage_2;
                        dtrow["SurchargeAmount_2"] = item.SurchargeAmount_2;

                        dtrow["SurchargeName_3"] = item.SurchargeName_3;
                        dtrow["SurchargePercentage_3"] = item.SurchargePercentage_3;
                        dtrow["SurchargeAmount_3"] = item.SurchargeAmount_3;

                        dtrow["TotalPrice"] = item.TotalPrice;
                        dtrow["CGST_Perc"] = item.CGST_Perc;
                        dtrow["CGST_Amount"] = item.CGST_Amount;
                        dtrow["SGST_Perc"] = item.SGST_Perc;
                        dtrow["SGST_Amount"] = item.SGST_Amount;
                        dtrow["IGST_Perc"] = item.IGST_Perc;
                        dtrow["IGST_Amount"] = item.IGST_Amount;
                        dtrow["HSN_Code"] = item.HSN_Code;

                        dtSOProduct.Rows.Add(dtrow);
                    }
                    dtSOProduct.AcceptChanges();

                }

                DataTable dtSOTax = new DataTable();
                dtSOTax.Columns.Add("TaxId", typeof(Int64));
                dtSOTax.Columns.Add("TaxName", typeof(string));
                dtSOTax.Columns.Add("TaxPercentage", typeof(decimal));
                dtSOTax.Columns.Add("TaxAmount", typeof(decimal));

                dtSOTax.Columns.Add("SurchargeName_1", typeof(string));
                dtSOTax.Columns.Add("SurchargePercentage_1", typeof(decimal));
                dtSOTax.Columns.Add("SurchargeAmount_1", typeof(decimal));

                dtSOTax.Columns.Add("SurchargeName_2", typeof(string));
                dtSOTax.Columns.Add("SurchargePercentage_2", typeof(decimal));
                dtSOTax.Columns.Add("SurchargeAmount_2", typeof(decimal));

                dtSOTax.Columns.Add("SurchargeName_3", typeof(string));
                dtSOTax.Columns.Add("SurchargePercentage_3", typeof(decimal));
                dtSOTax.Columns.Add("SurchargeAmount_3", typeof(decimal));


                if (soTaxList != null && soTaxList.Count > 0)
                {
                    foreach (SOTaxDetail item in soTaxList)
                    {
                        DataRow dtrow = dtSOTax.NewRow();
                        dtrow["TaxId"] = item.TaxId;
                        dtrow["TaxName"] = item.TaxName;
                        dtrow["TaxPercentage"] = item.TaxPercentage;
                        dtrow["TaxAmount"] = item.TaxAmount;

                        dtrow["SurchargeName_1"] = item.SurchargeName_1;
                        dtrow["SurchargePercentage_1"] = item.SurchargePercentage_1;
                        dtrow["SurchargeAmount_1"] = item.SurchargeAmount_1;

                        dtrow["SurchargeName_2"] = item.SurchargeName_2;
                        dtrow["SurchargePercentage_2"] = item.SurchargePercentage_2;
                        dtrow["SurchargeAmount_2"] = item.SurchargeAmount_2;

                        dtrow["SurchargeName_3"] = item.SurchargeName_3;
                        dtrow["SurchargePercentage_3"] = item.SurchargePercentage_3;
                        dtrow["SurchargeAmount_3"] = item.SurchargeAmount_3;

                        dtSOTax.Rows.Add(dtrow);
                    }
                    dtSOTax.AcceptChanges();
                }

                DataTable dtSOTerm = new DataTable();
                dtSOTerm.Columns.Add("TermDesc", typeof(string));
                dtSOTerm.Columns.Add("TermSequence", typeof(int));

                if (soTermList != null && soTermList.Count > 0)
                {
                    foreach (SOTermsDetail item in soTermList)
                    {
                        DataRow dtrow = dtSOTerm.NewRow();
                        dtrow["TermDesc"] = item.TermDesc;
                        dtrow["TermSequence"] = item.TermSequence;
                        dtSOTerm.Rows.Add(dtrow);
                    }
                    dtSOTerm.AcceptChanges();
                }

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("proc_AddEditSO", con))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@SOId", so.SOId);
                        sqlCommand.Parameters.AddWithValue("@SODate", so.SODate);
                        sqlCommand.Parameters.AddWithValue("@QuotationId", so.QuotationId);
                        sqlCommand.Parameters.AddWithValue("@QuotationNo", so.QuotationNo);
                        sqlCommand.Parameters.AddWithValue("@CustomerId", so.CustomerId);
                        sqlCommand.Parameters.AddWithValue("@CustomerName", so.CustomerName);

                        sqlCommand.Parameters.AddWithValue("@ConsigneeId", so.ConsigneeId);
                        sqlCommand.Parameters.AddWithValue("@ConsigneeName", so.ConsigneeName);
                        sqlCommand.Parameters.AddWithValue("@CompanyBranchId", so.CompanyBranchId);

                        sqlCommand.Parameters.AddWithValue("@ContactPerson", so.ContactPerson);
                        sqlCommand.Parameters.AddWithValue("@BillingAddress", so.BillingAddress);
                        sqlCommand.Parameters.AddWithValue("@City", so.City);
                        sqlCommand.Parameters.AddWithValue("@StateId", so.StateId);
                        sqlCommand.Parameters.AddWithValue("@CountryId", so.CountryId);
                        sqlCommand.Parameters.AddWithValue("@PinCode", so.PinCode);
                        sqlCommand.Parameters.AddWithValue("@CSTNo", so.CSTNo);
                        sqlCommand.Parameters.AddWithValue("@TINNo", so.TINNo);
                        sqlCommand.Parameters.AddWithValue("@PANNo", so.PANNo);
                        sqlCommand.Parameters.AddWithValue("@GSTNo", so.GSTNo);
                        sqlCommand.Parameters.AddWithValue("@ExciseNo", so.ExciseNo);
                        sqlCommand.Parameters.AddWithValue("@Email", so.Email);
                        sqlCommand.Parameters.AddWithValue("@MobileNo", so.MobileNo);
                        sqlCommand.Parameters.AddWithValue("@ContactNo", so.ContactNo);
                        sqlCommand.Parameters.AddWithValue("@Fax", so.Fax);
                        sqlCommand.Parameters.AddWithValue("@ApprovalStatus", so.ApprovalStatus);
                        sqlCommand.Parameters.AddWithValue("@ShippingContactPerson", so.ShippingContactPerson);
                        sqlCommand.Parameters.AddWithValue("@ShippingBillingAddress", so.ShippingBillingAddress);
                        sqlCommand.Parameters.AddWithValue("@ShippingCity", so.ShippingCity);
                        sqlCommand.Parameters.AddWithValue("@ShippingStateId", so.ShippingStateId);
                        sqlCommand.Parameters.AddWithValue("@ShippingCountryId", so.ShippingCountryId);
                        sqlCommand.Parameters.AddWithValue("@ShippingPinCode", so.ShippingPinCode);
                        sqlCommand.Parameters.AddWithValue("@ShippingTINNo", so.ShippingTINNo);
                        sqlCommand.Parameters.AddWithValue("@ShippingEmail", so.ShippingEmail);
                        sqlCommand.Parameters.AddWithValue("@ShippingMobileNo", so.ShippingMobileNo);
                        sqlCommand.Parameters.AddWithValue("@ShippingContactNo", so.ShippingContactNo);
                        sqlCommand.Parameters.AddWithValue("@ShippingFax", so.ShippingFax);

                        sqlCommand.Parameters.AddWithValue("@RefNo", so.RefNo);
                        sqlCommand.Parameters.AddWithValue("@RefDate", so.RefDate);
                        sqlCommand.Parameters.AddWithValue("@BasicValue", so.BasicValue);
                        sqlCommand.Parameters.AddWithValue("@TotalValue", so.TotalValue);

                        sqlCommand.Parameters.AddWithValue("@FreightValue", so.FreightValue);
                        sqlCommand.Parameters.AddWithValue("@LoadingValue", so.LoadingValue);
                        sqlCommand.Parameters.AddWithValue("@PayToBookId", so.PayToBookId);


                        sqlCommand.Parameters.AddWithValue("@ReverseChargeApplicable", so.ReverseChargeApplicable);
                        sqlCommand.Parameters.AddWithValue("@RevserseChargeAmount", so.ReverseChargeAmount);
                        sqlCommand.Parameters.AddWithValue("@InsuranceValue", so.InsuranceValue);
                        sqlCommand.Parameters.AddWithValue("@FreightCGST_Amt", so.FreightCGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@FreightSGST_Amt", so.FreightSGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@FreightIGST_Amt", so.FreightIGST_Amt);

                        sqlCommand.Parameters.AddWithValue("@LoadingCGST_Amt", so.LoadingCGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@LoadingSGST_Amt", so.LoadingSGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@LoadingIGST_Amt", so.LoadingIGST_Amt);

                        sqlCommand.Parameters.AddWithValue("@InsuranceCGST_Amt", so.InsuranceCGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@InsuranceSGST_Amt", so.InsuranceSGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@InsuranceIGST_Amt", so.InsuranceIGST_Amt);

                        sqlCommand.Parameters.AddWithValue("@FreightCGST_Perc", so.FreightCGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@FreightSGST_Perc", so.FreightSGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@FreightIGST_Perc", so.FreightIGST_Perc);

                        sqlCommand.Parameters.AddWithValue("@LoadingCGST_Perc", so.LoadingCGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@LoadingSGST_Perc", so.LoadingSGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@LoadingIGST_Perc", so.LoadingIGST_Perc);

                        sqlCommand.Parameters.AddWithValue("@InsuranceCGST_Perc", so.InsuranceCGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@InsuranceSGST_Perc", so.InsuranceSGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@InsuranceIGST_Perc", so.InsuranceIGST_Perc);


                        sqlCommand.Parameters.AddWithValue("@Remarks1", so.Remarks1);
                        sqlCommand.Parameters.AddWithValue("@Remarks2", so.Remarks2);
                        sqlCommand.Parameters.AddWithValue("@FinYearId", so.FinYearId);
                        sqlCommand.Parameters.AddWithValue("@CompanyId", so.CompanyId);
                        sqlCommand.Parameters.AddWithValue("@CurrencyCode", so.CurrencyCode);
                        sqlCommand.Parameters.AddWithValue("@CreatedBy", so.CreatedBy);
                        sqlCommand.Parameters.AddWithValue("@SOProductDetail", dtSOProduct);
                        sqlCommand.Parameters.AddWithValue("@SOTaxDetail", dtSOTax);
                        sqlCommand.Parameters.AddWithValue("@SOTermDetail", dtSOTerm);
                        sqlCommand.Parameters.Add("@status", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@RetSOId", SqlDbType.BigInt).Direction = ParameterDirection.Output;

                        con.Open();
                        sqlCommand.ExecuteNonQuery();
                        if (sqlCommand.Parameters["@status"].Value != null)
                        {
                            responseOut.status = Convert.ToString(sqlCommand.Parameters["@status"].Value);
                        }
                        if (responseOut.status != ActionStatus.Success)
                        {
                            if (sqlCommand.Parameters["@message"].Value != null)
                            {
                                responseOut.message = Convert.ToString(sqlCommand.Parameters["@message"].Value);
                            }

                        }
                        else
                        {
                            if (so.SOId == 0)
                            {
                                responseOut.message = ActionMessage.SOCreatedSuccess;
                            }
                            else
                            {
                                responseOut.message = ActionMessage.SOUpdatedSuccess;
                            }
                            if (sqlCommand.Parameters["@RetSOId"].Value != null)
                            {
                                responseOut.trnId = Convert.ToInt64(sqlCommand.Parameters["@RetSOId"].Value);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return responseOut;

        }
        public DataTable GetSOProductList(long soId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSOProducts", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@SOId", soId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetSOTaxList(long soId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSOTaxes", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@SOId", soId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetSOTermList(long soId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSOTerms", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@SOId", soId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetSOList(string soNo, string customerName, string refNo, DateTime fromDate, DateTime toDate, int companyId, string approvalStatus = "", string displayType = "")
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSOs", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@SONo", soNo);
                    da.SelectCommand.Parameters.AddWithValue("@CustomerName", customerName);
                    da.SelectCommand.Parameters.AddWithValue("@RefNo", refNo);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@ApprovalStatus", approvalStatus);
                    da.SelectCommand.Parameters.AddWithValue("@DisplayType", displayType);

                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetSODetail(long soId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSODetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@SOId", soId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }

        public DataTable GetDashboard_TotalSaleCount(int companyId, int finYearId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_Dashboard_GetSOTotalCount", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@FinYearId", finYearId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }

        public DataTable GetSITotalAmountSumByUser(int companyId, int FinYearId, int userId, int reportingUserId, int reportingRoleId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_Dashboard_GetUserIDBySITotalAmountSum", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@FinYearId", FinYearId);
                    da.SelectCommand.Parameters.AddWithValue("@UserId", userId);
                    da.SelectCommand.Parameters.AddWithValue("@ReportingUserId", reportingUserId);
                    da.SelectCommand.Parameters.AddWithValue("@ReportingRoleId", reportingRoleId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }



        #endregion


        #region  SaleInvoice

        public ResponseOut AddEditSaleInvoice(SaleInvoice saleinvoice, List<SaleInvoiceProductDetail> saleinvoiceProductList, List<SaleInvoiceTaxDetail> saleinvoiceTaxList, List<SaleInvoiceTermsDetail> saleinvoiceTermList)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                DataTable dtSaleInvoiceProduct = new DataTable();
                dtSaleInvoiceProduct.Columns.Add("InvoiceProductDetailId", typeof(Int64));
                dtSaleInvoiceProduct.Columns.Add("InvoiceId", typeof(Int64));
                dtSaleInvoiceProduct.Columns.Add("ProductId", typeof(Int64));
                dtSaleInvoiceProduct.Columns.Add("ProductShortDesc", typeof(string));
                dtSaleInvoiceProduct.Columns.Add("Price", typeof(decimal));
                dtSaleInvoiceProduct.Columns.Add("Quantity", typeof(decimal));
                dtSaleInvoiceProduct.Columns.Add("DiscountPercentage", typeof(decimal));
                dtSaleInvoiceProduct.Columns.Add("DiscountAmount", typeof(decimal));
                dtSaleInvoiceProduct.Columns.Add("TaxId", typeof(Int64));
                dtSaleInvoiceProduct.Columns.Add("TaxName", typeof(string));
                dtSaleInvoiceProduct.Columns.Add("TaxPercentage", typeof(decimal));
                dtSaleInvoiceProduct.Columns.Add("TaxAmount", typeof(decimal));
                dtSaleInvoiceProduct.Columns.Add("SurchargeName_1", typeof(string));
                dtSaleInvoiceProduct.Columns.Add("SurchargePercentage_1", typeof(decimal));
                dtSaleInvoiceProduct.Columns.Add("SurchargeAmount_1", typeof(decimal));
                dtSaleInvoiceProduct.Columns.Add("SurchargeName_2", typeof(string));
                dtSaleInvoiceProduct.Columns.Add("SurchargePercentage_2", typeof(decimal));
                dtSaleInvoiceProduct.Columns.Add("SurchargeAmount_2", typeof(decimal));
                dtSaleInvoiceProduct.Columns.Add("SurchargeName_3", typeof(string));
                dtSaleInvoiceProduct.Columns.Add("SurchargePercentage_3", typeof(decimal));
                dtSaleInvoiceProduct.Columns.Add("SurchargeAmount_3", typeof(decimal));
                dtSaleInvoiceProduct.Columns.Add("TotalPrice", typeof(decimal));

                dtSaleInvoiceProduct.Columns.Add("CGST_Perc", typeof(decimal));
                dtSaleInvoiceProduct.Columns.Add("CGST_Amount", typeof(decimal));
                dtSaleInvoiceProduct.Columns.Add("SGST_Perc", typeof(decimal));
                dtSaleInvoiceProduct.Columns.Add("SGST_Amount", typeof(decimal));
                dtSaleInvoiceProduct.Columns.Add("IGST_Perc", typeof(decimal));
                dtSaleInvoiceProduct.Columns.Add("IGST_Amount", typeof(decimal));
                dtSaleInvoiceProduct.Columns.Add("HSN_Code", typeof(string));





                if (saleinvoiceProductList != null && saleinvoiceProductList.Count > 0)
                {
                    foreach (SaleInvoiceProductDetail item in saleinvoiceProductList)
                    {
                        DataRow dtrow = dtSaleInvoiceProduct.NewRow();
                        dtrow["InvoiceProductDetailId"] = 0;
                        dtrow["InvoiceId"] = 0;
                        dtrow["ProductId"] = item.ProductId;
                        dtrow["ProductShortDesc"] = item.ProductShortDesc;
                        dtrow["Price"] = item.Price;
                        dtrow["Quantity"] = item.Quantity;
                        dtrow["DiscountPercentage"] = item.DiscountPercentage;
                        dtrow["DiscountAmount"] = item.DiscountAmount;
                        dtrow["TaxId"] = item.TaxId;
                        dtrow["TaxName"] = item.TaxName;
                        dtrow["TaxPercentage"] = item.TaxPercentage;
                        dtrow["TaxAmount"] = item.TaxAmount;

                        dtrow["SurchargeName_1"] = item.SurchargeName_1;
                        dtrow["SurchargePercentage_1"] = item.SurchargePercentage_1;
                        dtrow["SurchargeAmount_1"] = item.SurchargeAmount_1;

                        dtrow["SurchargeName_2"] = item.SurchargeName_2;
                        dtrow["SurchargePercentage_2"] = item.SurchargePercentage_2;
                        dtrow["SurchargeAmount_2"] = item.SurchargeAmount_2;

                        dtrow["SurchargeName_3"] = item.SurchargeName_3;
                        dtrow["SurchargePercentage_3"] = item.SurchargePercentage_3;
                        dtrow["SurchargeAmount_3"] = item.SurchargeAmount_3;
                        dtrow["TotalPrice"] = item.TotalPrice;

                        dtrow["CGST_Perc"] = item.CGST_Perc;
                        dtrow["CGST_Amount"] = item.CGST_Amount;
                        dtrow["SGST_Perc"] = item.SGST_Perc;
                        dtrow["SGST_Amount"] = item.SGST_Amount;
                        dtrow["IGST_Perc"] = item.IGST_Perc;
                        dtrow["IGST_Amount"] = item.IGST_Amount;
                        dtrow["HSN_Code"] = item.HSN_Code;


                        dtSaleInvoiceProduct.Rows.Add(dtrow);
                    }
                    dtSaleInvoiceProduct.AcceptChanges();

                }

                DataTable dtSaleInvoiceTax = new DataTable();
                dtSaleInvoiceTax.Columns.Add("TaxId", typeof(Int64));
                dtSaleInvoiceTax.Columns.Add("TaxName", typeof(string));
                dtSaleInvoiceTax.Columns.Add("TaxPercentage", typeof(decimal));
                dtSaleInvoiceTax.Columns.Add("TaxAmount", typeof(decimal));
                dtSaleInvoiceTax.Columns.Add("SurchargeName_1", typeof(string));
                dtSaleInvoiceTax.Columns.Add("SurchargePercentage_1", typeof(decimal));
                dtSaleInvoiceTax.Columns.Add("SurchargeAmount_1", typeof(decimal));
                dtSaleInvoiceTax.Columns.Add("SurchargeName_2", typeof(string));
                dtSaleInvoiceTax.Columns.Add("SurchargePercentage_2", typeof(decimal));
                dtSaleInvoiceTax.Columns.Add("SurchargeAmount_2", typeof(decimal));
                dtSaleInvoiceTax.Columns.Add("SurchargeName_3", typeof(string));
                dtSaleInvoiceTax.Columns.Add("SurchargePercentage_3", typeof(decimal));
                dtSaleInvoiceTax.Columns.Add("SurchargeAmount_3", typeof(decimal));



                if (saleinvoiceTaxList != null && saleinvoiceTaxList.Count > 0)
                {
                    foreach (SaleInvoiceTaxDetail item in saleinvoiceTaxList)
                    {
                        DataRow dtrow = dtSaleInvoiceTax.NewRow();
                        dtrow["TaxId"] = item.TaxId;
                        dtrow["TaxName"] = item.TaxName;
                        dtrow["TaxPercentage"] = item.TaxPercentage;
                        dtrow["TaxAmount"] = item.TaxAmount;
                        dtrow["SurchargeName_1"] = item.SurchargeName_1;
                        dtrow["SurchargePercentage_1"] = item.SurchargePercentage_1;
                        dtrow["SurchargeAmount_1"] = item.SurchargeAmount_1;

                        dtrow["SurchargeName_2"] = item.SurchargeName_2;
                        dtrow["SurchargePercentage_2"] = item.SurchargePercentage_2;
                        dtrow["SurchargeAmount_2"] = item.SurchargeAmount_2;

                        dtrow["SurchargeName_3"] = item.SurchargeName_3;
                        dtrow["SurchargePercentage_3"] = item.SurchargePercentage_3;
                        dtrow["SurchargeAmount_3"] = item.SurchargeAmount_3;



                        dtSaleInvoiceTax.Rows.Add(dtrow);
                    }
                    dtSaleInvoiceTax.AcceptChanges();
                }

                DataTable dtSaleInvoiceTerm = new DataTable();
                dtSaleInvoiceTerm.Columns.Add("TermDesc", typeof(string));
                dtSaleInvoiceTerm.Columns.Add("TermSequence", typeof(int));

                if (saleinvoiceTermList != null && saleinvoiceTermList.Count > 0)
                {
                    foreach (SaleInvoiceTermsDetail item in saleinvoiceTermList)
                    {
                        DataRow dtrow = dtSaleInvoiceTerm.NewRow();
                        dtrow["TermDesc"] = item.TermDesc;
                        dtrow["TermSequence"] = item.TermSequence;
                        dtSaleInvoiceTerm.Rows.Add(dtrow);
                    }
                    dtSaleInvoiceTerm.AcceptChanges();
                }

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("proc_AddEditSaleInvoice", con))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@InvoiceId", saleinvoice.InvoiceId);
                        sqlCommand.Parameters.AddWithValue("@InvoiceDate", saleinvoice.InvoiceDate);
                        sqlCommand.Parameters.AddWithValue("@InvoiceType", saleinvoice.InvoiceType);
                        sqlCommand.Parameters.AddWithValue("@CompanyBranchId", saleinvoice.CompanyBranchId);
                        sqlCommand.Parameters.AddWithValue("@CurrencyCode", saleinvoice.CurrencyCode);
                        sqlCommand.Parameters.AddWithValue("@SOId", saleinvoice.SOId);
                        sqlCommand.Parameters.AddWithValue("@SONo", saleinvoice.SONo);
                        sqlCommand.Parameters.AddWithValue("@CustomerId", saleinvoice.CustomerId);
                        sqlCommand.Parameters.AddWithValue("@CustomerName", saleinvoice.CustomerName);
                        sqlCommand.Parameters.AddWithValue("@ContactPerson", saleinvoice.ContactPerson);
                        sqlCommand.Parameters.AddWithValue("@BillingAddress", saleinvoice.BillingAddress);
                        sqlCommand.Parameters.AddWithValue("@City", saleinvoice.City);
                        sqlCommand.Parameters.AddWithValue("@StateId", saleinvoice.StateId);
                        sqlCommand.Parameters.AddWithValue("@CountryId", saleinvoice.CountryId);
                        sqlCommand.Parameters.AddWithValue("@PinCode", saleinvoice.PinCode);
                        sqlCommand.Parameters.AddWithValue("@CSTNo", saleinvoice.CSTNo);
                        sqlCommand.Parameters.AddWithValue("@TINNo", saleinvoice.TINNo);
                        sqlCommand.Parameters.AddWithValue("@PANNo", saleinvoice.PANNo);
                        sqlCommand.Parameters.AddWithValue("@GSTNo", saleinvoice.GSTNo);
                        sqlCommand.Parameters.AddWithValue("@ExciseNo", saleinvoice.ExciseNo);
                        sqlCommand.Parameters.AddWithValue("@Email", saleinvoice.Email);
                        sqlCommand.Parameters.AddWithValue("@MobileNo", saleinvoice.MobileNo);
                        sqlCommand.Parameters.AddWithValue("@ContactNo", saleinvoice.ContactNo);
                        sqlCommand.Parameters.AddWithValue("@Fax", saleinvoice.Fax);
                        sqlCommand.Parameters.AddWithValue("@ConsigneeId", saleinvoice.ConsigneeId);
                        sqlCommand.Parameters.AddWithValue("@ConsigneeName", saleinvoice.ConsigneeName);
                        sqlCommand.Parameters.AddWithValue("@ApprovalStatus", saleinvoice.ApprovalStatus);

                        sqlCommand.Parameters.AddWithValue("@ShippingContactPerson", saleinvoice.ShippingContactPerson);
                        sqlCommand.Parameters.AddWithValue("@ShippingBillingAddress", saleinvoice.ShippingBillingAddress);
                        sqlCommand.Parameters.AddWithValue("@ShippingCity", saleinvoice.ShippingCity);
                        sqlCommand.Parameters.AddWithValue("@ShippingStateId", saleinvoice.ShippingStateId);
                        sqlCommand.Parameters.AddWithValue("@ShippingCountryId", saleinvoice.ShippingCountryId);
                        sqlCommand.Parameters.AddWithValue("@ShippingPinCode", saleinvoice.ShippingPinCode);
                        sqlCommand.Parameters.AddWithValue("@ShippingTINNo", saleinvoice.ShippingTINNo);
                        sqlCommand.Parameters.AddWithValue("@ShippingEmail", saleinvoice.ShippingEmail);
                        sqlCommand.Parameters.AddWithValue("@ShippingMobileNo", saleinvoice.ShippingMobileNo);
                        sqlCommand.Parameters.AddWithValue("@ShippingContactNo", saleinvoice.ShippingContactNo);
                        sqlCommand.Parameters.AddWithValue("@ShippingFax", saleinvoice.ShippingFax);

                        sqlCommand.Parameters.AddWithValue("@RefNo", saleinvoice.RefNo);
                        sqlCommand.Parameters.AddWithValue("@RefDate", saleinvoice.RefDate);
                        sqlCommand.Parameters.AddWithValue("@BasicValue", saleinvoice.BasicValue);
                        sqlCommand.Parameters.AddWithValue("@TotalValue", saleinvoice.TotalValue);

                        sqlCommand.Parameters.AddWithValue("@FreightValue", saleinvoice.FreightValue);
                        sqlCommand.Parameters.AddWithValue("@LoadingValue", saleinvoice.LoadingValue);
                        sqlCommand.Parameters.AddWithValue("@PayToBookId", saleinvoice.PayToBookId);


                        sqlCommand.Parameters.AddWithValue("@Remarks", saleinvoice.Remarks);
                        sqlCommand.Parameters.AddWithValue("@FinYearId", saleinvoice.FinYearId);
                        sqlCommand.Parameters.AddWithValue("@CompanyId", saleinvoice.CompanyId);
                        sqlCommand.Parameters.AddWithValue("@CreatedBy", saleinvoice.CreatedBy);

                        sqlCommand.Parameters.AddWithValue("@ReverseChargeApplicable", saleinvoice.ReverseChargeApplicable);
                        sqlCommand.Parameters.AddWithValue("@RevserseChargeAmount", saleinvoice.RevserseChargeAmount);
                        sqlCommand.Parameters.AddWithValue("@InsuranceValue", saleinvoice.InsuranceValue);
                        sqlCommand.Parameters.AddWithValue("@FreightCGST_Amt", saleinvoice.FreightCGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@FreightSGST_Amt", saleinvoice.FreightSGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@FreightIGST_Amt", saleinvoice.FreightIGST_Amt);

                        sqlCommand.Parameters.AddWithValue("@LoadingCGST_Amt", saleinvoice.LoadingCGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@LoadingSGST_Amt", saleinvoice.LoadingSGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@LoadingIGST_Amt", saleinvoice.LoadingIGST_Amt);

                        sqlCommand.Parameters.AddWithValue("@InsuranceCGST_Amt", saleinvoice.InsuranceCGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@InsuranceSGST_Amt", saleinvoice.InsuranceSGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@InsuranceIGST_Amt", saleinvoice.InsuranceIGST_Amt);

                        sqlCommand.Parameters.AddWithValue("@FreightCGST_Perc", saleinvoice.FreightCGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@FreightSGST_Perc", saleinvoice.FreightSGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@FreightIGST_Perc", saleinvoice.FreightIGST_Perc);

                        sqlCommand.Parameters.AddWithValue("@LoadingCGST_Perc", saleinvoice.LoadingCGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@LoadingSGST_Perc", saleinvoice.LoadingSGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@LoadingIGST_Perc", saleinvoice.LoadingIGST_Perc);

                        sqlCommand.Parameters.AddWithValue("@InsuranceCGST_Perc", saleinvoice.InsuranceCGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@InsuranceSGST_Perc", saleinvoice.InsuranceSGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@InsuranceIGST_Perc", saleinvoice.InsuranceIGST_Perc);

                        sqlCommand.Parameters.AddWithValue("@SaleInvoiceProductDetail", dtSaleInvoiceProduct);
                        sqlCommand.Parameters.AddWithValue("@SaleInvoiceTaxDetail", dtSaleInvoiceTax);
                        sqlCommand.Parameters.AddWithValue("@SaleInvoiceTermDetail", dtSaleInvoiceTerm);
                        sqlCommand.Parameters.Add("@status", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@RetInvoiceId", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                        con.Open();
                        sqlCommand.ExecuteNonQuery();
                        if (sqlCommand.Parameters["@status"].Value != null)
                        {
                            responseOut.status = Convert.ToString(sqlCommand.Parameters["@status"].Value);
                        }
                        if (responseOut.status != ActionStatus.Success)
                        {
                            if (sqlCommand.Parameters["@message"].Value != null)
                            {
                                responseOut.message = Convert.ToString(sqlCommand.Parameters["@message"].Value);
                            }

                        }
                        else
                        {
                            if (saleinvoice.InvoiceId == 0)
                            {
                                responseOut.message = ActionMessage.SaleInvoiceCreatedSuccess;
                            }
                            else
                            {
                                responseOut.message = ActionMessage.SaleInvoiceUpdatedSuccess;
                            }
                            if (sqlCommand.Parameters["@RetInvoiceId"].Value != null)
                            {
                                responseOut.trnId = Convert.ToInt64(sqlCommand.Parameters["@RetInvoiceId"].Value);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return responseOut;

        }
        public DataTable GetSaleInvoiceProductList(long invoiceId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSaleInvoiceProducts", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@InvoiceId", invoiceId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetSaleInvoiceTaxList(long invoiceId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSaleInvoiceTaxes", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@InvoiceId", invoiceId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetSaleInvoiceTermList(long saleinvoiceId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSaleInvoiceTerms", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@InvoiceId", saleinvoiceId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetSaleInvoiceList(string invoiceNo, string customerName, string refNo, DateTime fromDate, DateTime toDate, int companyId, string invoiceType = "", string displayType = "", string approvalStatus = "", int companyBranchId = 0)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSaleInvoices", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@InvoiceNo", invoiceNo);
                    da.SelectCommand.Parameters.AddWithValue("@CustomerName", customerName);
                    da.SelectCommand.Parameters.AddWithValue("@RefNo", refNo);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@invoiceType", invoiceType);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyBranchId", companyBranchId);
                    da.SelectCommand.Parameters.AddWithValue("@DisplayType", displayType);
                    da.SelectCommand.Parameters.AddWithValue("@ApprovalStatus", approvalStatus);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetSaleInvoiceDetail(long saleinvoiceId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSaleInvoiceDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@InvoiceId", saleinvoiceId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }
        #endregion

        #region Product State Tax Mapping 
        public DataTable GetProductTaxMappingList(int productSubGroupId, int stateId, int taxId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetProductStateTaxMapping", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@Productsubgroupid", productSubGroupId);
                    da.SelectCommand.Parameters.AddWithValue("@StateId", stateId);
                    da.SelectCommand.Parameters.AddWithValue("@TaxId", taxId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetProductStateTaxDetail(int MappingId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetProductStateTaxDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@MappingId", MappingId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }
        #endregion
        #region SOSetting
        public DataTable GetSOSettingList(int companyId, string status)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSOSettings", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@Status", status);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetSOSettingDetail(int sosettingId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSOSettingDetails", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@SOSettingId", sosettingId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion



        #region  Customer Payment

        public ResponseOut AddEditCustomerPayment(CustomerPayment customerpayment)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("proc_AddEditCustomerPayment", con))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@PaymentTrnId", customerpayment.PaymentTrnId);
                        sqlCommand.Parameters.AddWithValue("@PaymentDate", customerpayment.PaymentDate);
                        sqlCommand.Parameters.AddWithValue("@CustomerId", customerpayment.CustomerId);
                        sqlCommand.Parameters.AddWithValue("@InvoiceId", customerpayment.InvoiceId);
                        sqlCommand.Parameters.AddWithValue("@BookId", customerpayment.BookId);
                        sqlCommand.Parameters.AddWithValue("@PaymentModeId", customerpayment.PaymentModeId);
                        sqlCommand.Parameters.AddWithValue("@RefNo", customerpayment.RefNo);
                        sqlCommand.Parameters.AddWithValue("@RefDate", customerpayment.RefDate);
                        sqlCommand.Parameters.AddWithValue("@Remarks", customerpayment.Remarks);
                        sqlCommand.Parameters.AddWithValue("@ValueDate", customerpayment.ValueDate);
                        sqlCommand.Parameters.AddWithValue("@Amount", customerpayment.Amount);
                        sqlCommand.Parameters.AddWithValue("@CompanyId", customerpayment.CompanyId);
                        sqlCommand.Parameters.AddWithValue("@FinYearId", customerpayment.FinYearId);
                        sqlCommand.Parameters.AddWithValue("@CustomerPaymentStatus", customerpayment.Status);
                        sqlCommand.Parameters.AddWithValue("@CreatedBy", customerpayment.CreatedBy);
                        sqlCommand.Parameters.Add("@status", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@RetPaymentTrnId", SqlDbType.BigInt).Direction = ParameterDirection.Output;

                        con.Open();
                        sqlCommand.ExecuteNonQuery();
                        if (sqlCommand.Parameters["@status"].Value != null)
                        {
                            responseOut.status = Convert.ToString(sqlCommand.Parameters["@status"].Value);
                        }
                        if (responseOut.status != ActionStatus.Success)
                        {
                            if (sqlCommand.Parameters["@message"].Value != null)
                            {
                                responseOut.message = Convert.ToString(sqlCommand.Parameters["@message"].Value);
                            }

                        }
                        else
                        {
                            if (customerpayment.PaymentTrnId == 0)
                            {
                                responseOut.message = ActionMessage.CustomerPaymentCreatedSuccess;
                            }
                            else
                            {
                                responseOut.message = ActionMessage.CustomerPaymentUpdatedSuccess;
                            }
                            if (sqlCommand.Parameters["@RetPaymentTrnId"].Value != null)
                            {
                                responseOut.trnId = Convert.ToInt64(sqlCommand.Parameters["@RetPaymentTrnId"].Value);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return responseOut;

        }


        public DataTable GetCustomerPaymentList(string paymentNo, string customerName, string invoiceNo, string refNo, DateTime fromDate, DateTime toDate, int companyId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetCustomerPayments", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@PaymentNo", paymentNo);
                    da.SelectCommand.Parameters.AddWithValue("@CustomerName", customerName);
                    da.SelectCommand.Parameters.AddWithValue("@InvoiceNo", invoiceNo);
                    da.SelectCommand.Parameters.AddWithValue("@RefNo", refNo);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetCustomerPaymentDetail(long paymenttrnId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetCustomerPaymentDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@PaymentTrnId", paymenttrnId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }





        #endregion


        #region  Customer Form     
        public DataTable GetCustomerFormList(string FormStatus, string customerName, string invoiceNo, string refNo, DateTime fromDate, DateTime toDate, int companyId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetCustomerForm", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@FormStatus", FormStatus);
                    da.SelectCommand.Parameters.AddWithValue("@CustomerName", customerName);
                    da.SelectCommand.Parameters.AddWithValue("@InvoiceNo", invoiceNo);
                    da.SelectCommand.Parameters.AddWithValue("@RefNo", refNo);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetCustomerFormDetail(long customerFormTrnId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetCustomerFormDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CustomerFormTrnId", customerFormTrnId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }


        #endregion

        #region  Vendor Payment

        public ResponseOut AddEditVendorPayment(VendorPayment vendorpayment)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("proc_AddEditVendorPayment", con))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@PaymentTrnId", vendorpayment.PaymentTrnId);
                        sqlCommand.Parameters.AddWithValue("@PaymentDate", vendorpayment.PaymentDate);
                        sqlCommand.Parameters.AddWithValue("@VendorId", vendorpayment.VendorId);
                        sqlCommand.Parameters.AddWithValue("@InvoiceId", vendorpayment.InvoiceId);
                        sqlCommand.Parameters.AddWithValue("@BookId", vendorpayment.BookId);
                        sqlCommand.Parameters.AddWithValue("@PaymentModeId", vendorpayment.PaymentModeId);
                        sqlCommand.Parameters.AddWithValue("@RefNo", vendorpayment.RefNo);
                        sqlCommand.Parameters.AddWithValue("@RefDate", vendorpayment.RefDate);
                        sqlCommand.Parameters.AddWithValue("@Remarks", vendorpayment.Remarks);
                        sqlCommand.Parameters.AddWithValue("@ValueDate", vendorpayment.ValueDate);
                        sqlCommand.Parameters.AddWithValue("@Amount", vendorpayment.Amount);
                        sqlCommand.Parameters.AddWithValue("@CompanyId", vendorpayment.CompanyId);
                        sqlCommand.Parameters.AddWithValue("@FinYearId", vendorpayment.FinYearId);
                        sqlCommand.Parameters.AddWithValue("@VendorPaymentStatus", vendorpayment.Status);
                        sqlCommand.Parameters.AddWithValue("@CreatedBy", vendorpayment.CreatedBy);
                        sqlCommand.Parameters.Add("@status", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@RetPaymentTrnId", SqlDbType.BigInt).Direction = ParameterDirection.Output;

                        con.Open();
                        sqlCommand.ExecuteNonQuery();
                        if (sqlCommand.Parameters["@status"].Value != null)
                        {
                            responseOut.status = Convert.ToString(sqlCommand.Parameters["@status"].Value);
                        }
                        if (responseOut.status != ActionStatus.Success)
                        {
                            if (sqlCommand.Parameters["@message"].Value != null)
                            {
                                responseOut.message = Convert.ToString(sqlCommand.Parameters["@message"].Value);
                            }

                        }
                        else
                        {
                            if (vendorpayment.PaymentTrnId == 0)
                            {
                                responseOut.message = ActionMessage.VendorPaymentCreatedSuccess;
                            }
                            else
                            {
                                responseOut.message = ActionMessage.VendorPaymentUpdatedSuccess;
                            }
                            if (sqlCommand.Parameters["@RetPaymentTrnId"].Value != null)
                            {
                                responseOut.trnId = Convert.ToInt64(sqlCommand.Parameters["@RetPaymentTrnId"].Value);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return responseOut;

        }


        public DataTable GetVendorPaymentList(string paymentNo, string vendorName, string invoiceNo, string refNo, DateTime fromDate, DateTime toDate, int companyId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetVendorPayments", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@PaymentNo", paymentNo);
                    da.SelectCommand.Parameters.AddWithValue("@VendorName", vendorName);
                    da.SelectCommand.Parameters.AddWithValue("@InvoiceNo", invoiceNo);
                    da.SelectCommand.Parameters.AddWithValue("@RefNo", refNo);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetVendorPaymentDetail(long paymenttrnId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetVendorPaymentDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@PaymentTrnId", paymenttrnId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }

        #endregion


        #region  Delivery Challan

        public ResponseOut AddEditDeliveryChallan(DeliveryChallan deliverychallan, List<ChallanProductDetail> challanProductList, List<ChallanTaxDetail> challanTaxList, List<ChallanTermsDetail> challanTermList)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                DataTable dtChallanProduct = new DataTable();
                dtChallanProduct.Columns.Add("ChallanProductDetailId", typeof(Int64));
                dtChallanProduct.Columns.Add("ChallanId", typeof(Int64));
                dtChallanProduct.Columns.Add("ProductId", typeof(Int64));
                dtChallanProduct.Columns.Add("ProductShortDesc", typeof(string));
                dtChallanProduct.Columns.Add("Price", typeof(decimal));
                dtChallanProduct.Columns.Add("Quantity", typeof(decimal));
                dtChallanProduct.Columns.Add("DiscountPercentage", typeof(decimal));
                dtChallanProduct.Columns.Add("DiscountAmount", typeof(decimal));
                dtChallanProduct.Columns.Add("TaxId", typeof(Int64));
                dtChallanProduct.Columns.Add("TaxName", typeof(string));
                dtChallanProduct.Columns.Add("TaxPercentage", typeof(decimal));
                dtChallanProduct.Columns.Add("TaxAmount", typeof(decimal));
                dtChallanProduct.Columns.Add("TotalPrice", typeof(decimal));

                if (challanProductList != null && challanProductList.Count > 0)
                {
                    foreach (ChallanProductDetail item in challanProductList)
                    {
                        DataRow dtrow = dtChallanProduct.NewRow();
                        dtrow["ChallanProductDetailId"] = 0;
                        dtrow["ChallanId"] = 0;
                        dtrow["ProductId"] = item.ProductId;
                        dtrow["ProductShortDesc"] = item.ProductShortDesc;
                        dtrow["Price"] = item.Price;
                        dtrow["Quantity"] = item.Quantity;
                        dtrow["DiscountPercentage"] = item.DiscountPercentage;
                        dtrow["DiscountAmount"] = item.DiscountAmount;
                        dtrow["TaxId"] = item.TaxId;
                        dtrow["TaxName"] = item.TaxName;
                        dtrow["TaxPercentage"] = item.TaxPercentage;
                        dtrow["TaxAmount"] = item.TaxAmount;
                        dtrow["TotalPrice"] = item.TotalPrice;
                        dtChallanProduct.Rows.Add(dtrow);
                    }
                    dtChallanProduct.AcceptChanges();

                }

                DataTable dtChallanTax = new DataTable();
                dtChallanTax.Columns.Add("TaxId", typeof(Int64));
                dtChallanTax.Columns.Add("TaxName", typeof(string));
                dtChallanTax.Columns.Add("TaxPercentage", typeof(decimal));
                dtChallanTax.Columns.Add("TaxAmount", typeof(decimal));

                if (challanTaxList != null && challanTaxList.Count > 0)
                {
                    foreach (ChallanTaxDetail item in challanTaxList)
                    {
                        DataRow dtrow = dtChallanTax.NewRow();
                        dtrow["TaxId"] = item.TaxId;
                        dtrow["TaxName"] = item.TaxName;
                        dtrow["TaxPercentage"] = item.TaxPercentage;
                        dtrow["TaxAmount"] = item.TaxAmount;

                        dtChallanTax.Rows.Add(dtrow);
                    }
                    dtChallanTax.AcceptChanges();
                }

                DataTable dtChallanTerm = new DataTable();
                dtChallanTerm.Columns.Add("TermDesc", typeof(string));
                dtChallanTerm.Columns.Add("TermSequence", typeof(int));

                if (challanTermList != null && challanTermList.Count > 0)
                {
                    foreach (ChallanTermsDetail item in challanTermList)
                    {
                        DataRow dtrow = dtChallanTerm.NewRow();
                        dtrow["TermDesc"] = item.TermDesc;
                        dtrow["TermSequence"] = item.TermSequence;
                        dtChallanTerm.Rows.Add(dtrow);
                    }
                    dtChallanTerm.AcceptChanges();
                }

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("proc_AddEditDeliveryChallan", con))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@ChallanId", deliverychallan.ChallanId);
                        sqlCommand.Parameters.AddWithValue("@ChallanDate", deliverychallan.ChallanDate);
                        sqlCommand.Parameters.AddWithValue("@InvoiceId", deliverychallan.InvoiceId);
                        sqlCommand.Parameters.AddWithValue("@InvoiceNo", deliverychallan.InvoiceNo);
                        sqlCommand.Parameters.AddWithValue("@CustomerId", deliverychallan.CustomerId);
                        sqlCommand.Parameters.AddWithValue("@CustomerName", deliverychallan.CustomerName);
                        sqlCommand.Parameters.AddWithValue("@ContactPerson", deliverychallan.ContactPerson);

                        sqlCommand.Parameters.AddWithValue("@ShippingContactPerson", deliverychallan.ShippingContactPerson);
                        sqlCommand.Parameters.AddWithValue("@ShippingBillingAddress", deliverychallan.ShippingBillingAddress);
                        sqlCommand.Parameters.AddWithValue("@ShippingCity", deliverychallan.ShippingCity);
                        sqlCommand.Parameters.AddWithValue("@ShippingStateId", deliverychallan.ShippingStateId);
                        sqlCommand.Parameters.AddWithValue("@ShippingCountryId", deliverychallan.ShippingCountryId);
                        sqlCommand.Parameters.AddWithValue("@ShippingPinCode", deliverychallan.ShippingPinCode);
                        sqlCommand.Parameters.AddWithValue("@ShippingTINNo", deliverychallan.ShippingTINNo);
                        sqlCommand.Parameters.AddWithValue("@ShippingEmail", deliverychallan.ShippingEmail);
                        sqlCommand.Parameters.AddWithValue("@ShippingMobileNo", deliverychallan.ShippingMobileNo);
                        sqlCommand.Parameters.AddWithValue("@ShippingContactNo", deliverychallan.ShippingContactNo);
                        sqlCommand.Parameters.AddWithValue("@ShippingFax", deliverychallan.ShippingFax);
                        sqlCommand.Parameters.AddWithValue("@CompanyBranchId", deliverychallan.CompanyBranchId);
                        sqlCommand.Parameters.AddWithValue("@DispatchRefNo", deliverychallan.DispatchRefNo);
                        sqlCommand.Parameters.AddWithValue("@DispatchRefDate", deliverychallan.DispatchRefDate);

                        sqlCommand.Parameters.AddWithValue("@LRNo", deliverychallan.LRNo);
                        sqlCommand.Parameters.AddWithValue("@LRDate", deliverychallan.LRDate);
                        sqlCommand.Parameters.AddWithValue("@TransportVia", deliverychallan.TransportVia);
                        sqlCommand.Parameters.AddWithValue("@NoOfPackets", deliverychallan.NoOfPackets);
                        sqlCommand.Parameters.AddWithValue("@ApprovalStatus", deliverychallan.ApprovalStatus);

                        sqlCommand.Parameters.AddWithValue("@BasicValue", deliverychallan.BasicValue);
                        sqlCommand.Parameters.AddWithValue("@TotalValue", deliverychallan.TotalValue);

                        sqlCommand.Parameters.AddWithValue("@FreightValue", deliverychallan.FreightValue);
                        sqlCommand.Parameters.AddWithValue("@LoadingValue", deliverychallan.LoadingValue);
                        sqlCommand.Parameters.AddWithValue("@Remarks1", deliverychallan.Remarks1);
                        sqlCommand.Parameters.AddWithValue("@Remarks2", deliverychallan.Remarks2);
                        sqlCommand.Parameters.AddWithValue("@FinYearId", deliverychallan.FinYearId);
                        sqlCommand.Parameters.AddWithValue("@CompanyId", deliverychallan.CompanyId);

                        sqlCommand.Parameters.AddWithValue("@CreatedBy", deliverychallan.CreatedBy);
                        sqlCommand.Parameters.AddWithValue("@ChallanProductDetail", dtChallanProduct);
                        sqlCommand.Parameters.AddWithValue("@ChallanTaxDetail", dtChallanTax);
                        sqlCommand.Parameters.AddWithValue("@ChallanTermDetail", dtChallanTerm);
                        sqlCommand.Parameters.Add("@status", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@RetChallanId", SqlDbType.BigInt).Direction = ParameterDirection.Output;

                        con.Open();
                        sqlCommand.ExecuteNonQuery();
                        if (sqlCommand.Parameters["@status"].Value != null)
                        {
                            responseOut.status = Convert.ToString(sqlCommand.Parameters["@status"].Value);
                        }
                        if (responseOut.status != ActionStatus.Success)
                        {
                            if (sqlCommand.Parameters["@message"].Value != null)
                            {
                                responseOut.message = Convert.ToString(sqlCommand.Parameters["@message"].Value);
                            }

                        }
                        else
                        {
                            if (deliverychallan.ChallanId == 0)
                            {
                                responseOut.message = ActionMessage.ChallanCreatedSuccess;
                            }
                            else
                            {
                                responseOut.message = ActionMessage.ChallanUpdatedSuccess;
                            }
                            if (sqlCommand.Parameters["@RetChallanId"].Value != null)
                            {
                                responseOut.trnId = Convert.ToInt64(sqlCommand.Parameters["@RetChallanId"].Value);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return responseOut;

        }
        public DataTable GetChallanProductList(long challanId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetChallanProducts", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ChallanId", challanId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetChallanTaxList(long challanId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetChallanTaxes", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ChallanId", challanId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetChallanTermList(long challanId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetChallanTerms", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ChallanId", challanId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetChallanList(string challanNo, string customerName, string dispatchrefNo, DateTime fromDate, DateTime toDate, string approvalStatus, int companyId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetChallans", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ChallanNo", challanNo);
                    da.SelectCommand.Parameters.AddWithValue("@CustomerName", customerName);
                    da.SelectCommand.Parameters.AddWithValue("@DispatchRefNo", dispatchrefNo);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@ApprovalStatus", approvalStatus);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetChallanDetail(long challanId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetChallanDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ChallanId", challanId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }
        #endregion

        #region FollowUpActivityType 
        public DataTable GetFollowUpActivityTypeList(string followUpActivityTypeName, string Status)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetFollowUpActivityType", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@FollowUpActivityTypeName", followUpActivityTypeName);
                    da.SelectCommand.Parameters.AddWithValue("@Status", Status);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }
        public DataTable GetFollowUpActivityTypeDetail(int followUpActivityTypeId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetFollowUpActivityTypeDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@FollowUpActivityTypeId", followUpActivityTypeId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }
        #endregion

        #region Revised Quotation 

        public ResponseOut AddRevisedQuotation(Quotation quotation, List<QuotationProductDetail> quotationProductList, List<QuotationTaxDetail> quotationTaxList, List<QuotationTermsDetail> quotationTermList)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                DataTable dtQuotationProduct = new DataTable();
                dtQuotationProduct.Columns.Add("QuotationProductDetailId", typeof(Int64));
                dtQuotationProduct.Columns.Add("QuotationId", typeof(Int64));
                dtQuotationProduct.Columns.Add("ProductId", typeof(Int64));
                dtQuotationProduct.Columns.Add("ProductShortDesc", typeof(string));
                dtQuotationProduct.Columns.Add("Price", typeof(decimal));
                dtQuotationProduct.Columns.Add("Quantity", typeof(decimal));
                dtQuotationProduct.Columns.Add("DiscountPercentage", typeof(decimal));
                dtQuotationProduct.Columns.Add("DiscountAmount", typeof(decimal));
                dtQuotationProduct.Columns.Add("TaxPercentage", typeof(decimal));
                dtQuotationProduct.Columns.Add("TaxAmount", typeof(decimal));
                dtQuotationProduct.Columns.Add("TotalPrice", typeof(decimal));

                if (quotationProductList != null && quotationProductList.Count > 0)
                {
                    foreach (QuotationProductDetail item in quotationProductList)
                    {
                        DataRow dtrow = dtQuotationProduct.NewRow();
                        dtrow["QuotationProductDetailId"] = 0;
                        dtrow["QuotationId"] = 0;
                        dtrow["ProductId"] = item.ProductId;
                        dtrow["ProductShortDesc"] = item.ProductShortDesc;
                        dtrow["Price"] = item.Price;
                        dtrow["Quantity"] = item.Quantity;
                        dtrow["DiscountPercentage"] = item.DiscountPercentage;
                        dtrow["DiscountAmount"] = item.DiscountAmount;
                        dtrow["TaxPercentage"] = item.TaxPercentage;
                        dtrow["TaxAmount"] = item.TaxAmount;
                        dtrow["TotalPrice"] = item.TotalPrice;
                        dtQuotationProduct.Rows.Add(dtrow);
                    }
                    dtQuotationProduct.AcceptChanges();

                }

                DataTable dtQuotationTax = new DataTable();
                dtQuotationTax.Columns.Add("TaxId", typeof(Int64));
                dtQuotationTax.Columns.Add("TaxName", typeof(string));
                dtQuotationTax.Columns.Add("TaxPercentage", typeof(decimal));
                dtQuotationTax.Columns.Add("TaxAmount", typeof(decimal));

                if (quotationTaxList != null && quotationTaxList.Count > 0)
                {
                    foreach (QuotationTaxDetail item in quotationTaxList)
                    {
                        DataRow dtrow = dtQuotationTax.NewRow();
                        dtrow["TaxId"] = item.TaxId;
                        dtrow["TaxName"] = item.TaxName;
                        dtrow["TaxPercentage"] = item.TaxPercentage;
                        dtrow["TaxAmount"] = item.TaxAmount;

                        dtQuotationTax.Rows.Add(dtrow);
                    }
                    dtQuotationTax.AcceptChanges();
                }

                DataTable dtQuotationTerm = new DataTable();
                dtQuotationTerm.Columns.Add("TermDesc", typeof(string));
                dtQuotationTerm.Columns.Add("TermSequence", typeof(int));

                if (quotationTermList != null && quotationTermList.Count > 0)
                {
                    foreach (QuotationTermsDetail item in quotationTermList)
                    {
                        DataRow dtrow = dtQuotationTerm.NewRow();
                        dtrow["TermDesc"] = item.TermDesc;
                        dtrow["TermSequence"] = item.TermSequence;
                        dtQuotationTerm.Rows.Add(dtrow);
                    }
                    dtQuotationTerm.AcceptChanges();
                }

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("proc_AddRevisedQuotation", con))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@QuotationId", quotation.QuotationId);
                        sqlCommand.Parameters.AddWithValue("@QuotationNo", quotation.QuotationNo);
                        sqlCommand.Parameters.AddWithValue("@QuotationDate", quotation.QuotationDate);
                        sqlCommand.Parameters.AddWithValue("@CustomerId", quotation.CustomerId);
                        sqlCommand.Parameters.AddWithValue("@CustomerName", quotation.CustomerName);
                        sqlCommand.Parameters.AddWithValue("@BillingAddress", quotation.BillingAddress);
                        sqlCommand.Parameters.AddWithValue("@City", quotation.City);
                        sqlCommand.Parameters.AddWithValue("@StateId", quotation.StateId);
                        sqlCommand.Parameters.AddWithValue("@CountryId", quotation.CountryId);
                        sqlCommand.Parameters.AddWithValue("@PinCode", quotation.PinCode);
                        sqlCommand.Parameters.AddWithValue("@CSTNo", quotation.CSTNo);
                        sqlCommand.Parameters.AddWithValue("@TINNo", quotation.TINNo);
                        sqlCommand.Parameters.AddWithValue("@PANNo", quotation.PANNo);
                        sqlCommand.Parameters.AddWithValue("@GSTNo", quotation.GSTNo);
                        sqlCommand.Parameters.AddWithValue("@ExciseNo", quotation.ExciseNo);
                        sqlCommand.Parameters.AddWithValue("@RefNo", quotation.RefNo);
                        sqlCommand.Parameters.AddWithValue("@RefDate", quotation.RefDate);
                        sqlCommand.Parameters.AddWithValue("@BasicValue", quotation.BasicValue);
                        sqlCommand.Parameters.AddWithValue("@TotalValue", quotation.TotalValue);
                        sqlCommand.Parameters.AddWithValue("@FinYearId", quotation.FinYearId);
                        sqlCommand.Parameters.AddWithValue("@CompanyId", quotation.CompanyId);
                        sqlCommand.Parameters.AddWithValue("@ApprovalStatus", quotation.ApprovalStatus);
                        sqlCommand.Parameters.AddWithValue("@CreatedBy", quotation.CreatedBy);
                        sqlCommand.Parameters.AddWithValue("@QuotationProductDetail", dtQuotationProduct);
                        sqlCommand.Parameters.AddWithValue("@QuotationTaxDetail", dtQuotationTax);
                        sqlCommand.Parameters.AddWithValue("@QuotationTermDetail", dtQuotationTerm);
                        sqlCommand.Parameters.Add("@status", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@RetQuotationId", SqlDbType.BigInt).Direction = ParameterDirection.Output;

                        con.Open();
                        sqlCommand.ExecuteNonQuery();
                        if (sqlCommand.Parameters["@status"].Value != null)
                        {
                            responseOut.status = Convert.ToString(sqlCommand.Parameters["@status"].Value);
                        }
                        if (responseOut.status != ActionStatus.Success)
                        {
                            if (sqlCommand.Parameters["@message"].Value != null)
                            {
                                responseOut.message = Convert.ToString(sqlCommand.Parameters["@message"].Value);
                            }

                        }
                        else
                        {

                            responseOut.message = ActionMessage.RevisedQuotationCreatedSuccess;


                            if (sqlCommand.Parameters["@RetQuotationId"].Value != null)
                            {
                                responseOut.trnId = Convert.ToInt64(sqlCommand.Parameters["@RetQuotationId"].Value);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return responseOut;

        }
        #endregion 


        #region Revised PO
        public ResponseOut AddRevisedPO(PO po, List<POProductDetail> poProductList, List<POTaxDetail> poTaxList, List<POTermsDetail> poTermList)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                DataTable dtPOProduct = new DataTable();
                dtPOProduct.Columns.Add("POProductDetailId", typeof(Int64));
                dtPOProduct.Columns.Add("POId", typeof(Int64));
                dtPOProduct.Columns.Add("ProductId", typeof(Int64));
                dtPOProduct.Columns.Add("ProductShortDesc", typeof(string));
                dtPOProduct.Columns.Add("Price", typeof(decimal));
                dtPOProduct.Columns.Add("Quantity", typeof(decimal));
                dtPOProduct.Columns.Add("DiscountPercentage", typeof(decimal));
                dtPOProduct.Columns.Add("DiscountAmount", typeof(decimal));
                dtPOProduct.Columns.Add("TaxPercentage", typeof(decimal));
                dtPOProduct.Columns.Add("TaxAmount", typeof(decimal));
                dtPOProduct.Columns.Add("TotalPrice", typeof(decimal));

                if (poProductList != null && poProductList.Count > 0)
                {
                    foreach (POProductDetail item in poProductList)
                    {
                        DataRow dtrow = dtPOProduct.NewRow();
                        dtrow["POProductDetailId"] = 0;
                        dtrow["POId"] = 0;
                        dtrow["ProductId"] = item.ProductId;
                        dtrow["ProductShortDesc"] = item.ProductShortDesc;
                        dtrow["Price"] = item.Price;
                        dtrow["Quantity"] = item.Quantity;
                        dtrow["DiscountPercentage"] = item.DiscountPercentage;
                        dtrow["DiscountAmount"] = item.DiscountAmount;
                        dtrow["TaxPercentage"] = item.TaxPercentage;
                        dtrow["TaxAmount"] = item.TaxAmount;
                        dtrow["TotalPrice"] = item.TotalPrice;
                        dtPOProduct.Rows.Add(dtrow);
                    }
                    dtPOProduct.AcceptChanges();

                }
                DataTable dtPOTax = new DataTable();
                dtPOTax.Columns.Add("TaxId", typeof(Int64));
                dtPOTax.Columns.Add("TaxName", typeof(string));
                dtPOTax.Columns.Add("TaxPercentage", typeof(decimal));
                dtPOTax.Columns.Add("TaxAmount", typeof(decimal));

                if (poTaxList != null && poTaxList.Count > 0)
                {
                    foreach (POTaxDetail item in poTaxList)
                    {
                        DataRow dtrow = dtPOTax.NewRow();
                        dtrow["TaxId"] = item.TaxId;
                        dtrow["TaxName"] = item.TaxName;
                        dtrow["TaxPercentage"] = item.TaxPercentage;
                        dtrow["TaxAmount"] = item.TaxAmount;

                        dtPOTax.Rows.Add(dtrow);
                    }
                    dtPOTax.AcceptChanges();
                }

                DataTable dtPOTerm = new DataTable();
                dtPOTerm.Columns.Add("TermDesc", typeof(string));
                dtPOTerm.Columns.Add("TermSequence", typeof(int));

                if (poTermList != null && poTermList.Count > 0)
                {
                    foreach (POTermsDetail item in poTermList)
                    {
                        DataRow dtrow = dtPOTerm.NewRow();
                        dtrow["TermDesc"] = item.TermDesc;
                        dtrow["TermSequence"] = item.TermSequence;
                        dtPOTerm.Rows.Add(dtrow);
                    }
                    dtPOTerm.AcceptChanges();
                }
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("proc_AddRevisedPO", con))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@POId", po.POId);
                        sqlCommand.Parameters.AddWithValue("@PONo", po.PONo);
                        sqlCommand.Parameters.AddWithValue("@PODate", po.PODate);
                        sqlCommand.Parameters.AddWithValue("@VendorId", po.VendorId);
                        sqlCommand.Parameters.AddWithValue("@VendorName", po.VendorName);
                        sqlCommand.Parameters.AddWithValue("@BillingAddress", po.BillingAddress);
                        sqlCommand.Parameters.AddWithValue("@ShippingAddress", po.ShippingAddress);
                        sqlCommand.Parameters.AddWithValue("@City", po.City);
                        sqlCommand.Parameters.AddWithValue("@StateId", po.StateId);
                        sqlCommand.Parameters.AddWithValue("@CountryId", po.CountryId);
                        sqlCommand.Parameters.AddWithValue("@PinCode", po.PinCode);
                        sqlCommand.Parameters.AddWithValue("@CSTNo", po.CSTNo);
                        sqlCommand.Parameters.AddWithValue("@TINNo", po.TINNo);
                        sqlCommand.Parameters.AddWithValue("@PANNo", po.PANNo);
                        sqlCommand.Parameters.AddWithValue("@GSTNo", po.GSTNo);
                        sqlCommand.Parameters.AddWithValue("@ExciseNo", po.ExciseNo);
                        sqlCommand.Parameters.AddWithValue("@ApprovalStatus", po.ApprovalStatus);
                        sqlCommand.Parameters.AddWithValue("@RefNo", po.RefNo);
                        sqlCommand.Parameters.AddWithValue("@RefDate", po.RefDate);
                        sqlCommand.Parameters.AddWithValue("@BasicValue", po.BasicValue);
                        sqlCommand.Parameters.AddWithValue("@TotalValue", po.TotalValue);
                        sqlCommand.Parameters.AddWithValue("@FreightValue", po.FreightValue);
                        sqlCommand.Parameters.AddWithValue("@LoadingValue", po.LoadingValue);
                        sqlCommand.Parameters.AddWithValue("@Remarks1", po.Remarks1);
                        sqlCommand.Parameters.AddWithValue("@Remarks2", po.Remarks2);
                        sqlCommand.Parameters.AddWithValue("@FinYearId", po.FinYearId);
                        sqlCommand.Parameters.AddWithValue("@CompanyId", po.CompanyId);
                        sqlCommand.Parameters.AddWithValue("@CreatedBy", po.CreatedBy);
                        sqlCommand.Parameters.AddWithValue("@POProductDetail", dtPOProduct);
                        sqlCommand.Parameters.AddWithValue("@POTaxDetail", dtPOTax);
                        sqlCommand.Parameters.AddWithValue("@POTermDetail", dtPOTerm);
                        sqlCommand.Parameters.Add("@status", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@RetPOId", SqlDbType.BigInt).Direction = ParameterDirection.Output;

                        con.Open();
                        sqlCommand.ExecuteNonQuery();
                        if (sqlCommand.Parameters["@status"].Value != null)
                        {
                            responseOut.status = Convert.ToString(sqlCommand.Parameters["@status"].Value);
                        }
                        if (responseOut.status != ActionStatus.Success)
                        {
                            if (sqlCommand.Parameters["@message"].Value != null)
                            {
                                responseOut.message = Convert.ToString(sqlCommand.Parameters["@message"].Value);
                            }

                        }
                        else
                        {

                            responseOut.message = ActionMessage.RevisedPOCreatedSuccess;

                            if (sqlCommand.Parameters["@RetPOId"].Value != null)
                            {
                                responseOut.trnId = Convert.ToInt64(sqlCommand.Parameters["@RetPOId"].Value);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return responseOut;

        }

        #endregion

        #region MRN
        public ResponseOut AddEditMRN(MRN mrn, List<MRNProductDetail> mrnProductList)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                DataTable dtmrnProduct = new DataTable();
                dtmrnProduct.Columns.Add("MRNProductDetailId", typeof(Int64));
                dtmrnProduct.Columns.Add("MRNId", typeof(Int64));
                dtmrnProduct.Columns.Add("ProductId", typeof(Int64));
                dtmrnProduct.Columns.Add("ProductShortDesc", typeof(string));
                dtmrnProduct.Columns.Add("Price", typeof(decimal));
                dtmrnProduct.Columns.Add("Quantity", typeof(decimal));


                if (mrnProductList != null && mrnProductList.Count > 0)
                {
                    foreach (MRNProductDetail item in mrnProductList)
                    {
                        DataRow dtrow = dtmrnProduct.NewRow();
                        dtrow["MRNProductDetailId"] = 0;
                        dtrow["MRNId"] = 0;
                        dtrow["ProductId"] = item.ProductId;
                        dtrow["ProductShortDesc"] = item.ProductShortDesc;
                        dtrow["Price"] = item.Price;
                        dtrow["Quantity"] = item.Quantity;

                        dtmrnProduct.Rows.Add(dtrow);
                    }
                    dtmrnProduct.AcceptChanges();

                }



                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("proc_AddEditMRN", con))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@MRNId", mrn.MRNId);
                        sqlCommand.Parameters.AddWithValue("@MRNDate", mrn.MRNDate);
                        sqlCommand.Parameters.AddWithValue("@GRNo", mrn.GRNo);
                        sqlCommand.Parameters.AddWithValue("@GRDate", mrn.GRDate);
                        sqlCommand.Parameters.AddWithValue("@InvoiceId", mrn.InvoiceId);
                        sqlCommand.Parameters.AddWithValue("@InvoiceNo", mrn.InvoiceNo);
                        sqlCommand.Parameters.AddWithValue("@VendorId", mrn.VendorId);
                        sqlCommand.Parameters.AddWithValue("@VendorName", mrn.VendorName);
                        sqlCommand.Parameters.AddWithValue("@ContactPerson", mrn.ContactPerson);

                        sqlCommand.Parameters.AddWithValue("@ShippingContactPerson", mrn.ShippingContactPerson);
                        sqlCommand.Parameters.AddWithValue("@ShippingBillingAddress", mrn.ShippingBillingAddress);
                        sqlCommand.Parameters.AddWithValue("@ShippingCity", mrn.ShippingCity);
                        sqlCommand.Parameters.AddWithValue("@ShippingStateId", mrn.ShippingStateId);
                        sqlCommand.Parameters.AddWithValue("@ShippingCountryId", mrn.ShippingCountryId);
                        sqlCommand.Parameters.AddWithValue("@ShippingPinCode", mrn.ShippingPinCode);
                        sqlCommand.Parameters.AddWithValue("@ShippingTINNo", mrn.ShippingTINNo);
                        sqlCommand.Parameters.AddWithValue("@ShippingEmail", mrn.ShippingEmail);
                        sqlCommand.Parameters.AddWithValue("@ShippingMobileNo", mrn.ShippingMobileNo);
                        sqlCommand.Parameters.AddWithValue("@ShippingContactNo", mrn.ShippingContactNo);
                        sqlCommand.Parameters.AddWithValue("@ShippingFax", mrn.ShippingFax);
                        sqlCommand.Parameters.AddWithValue("@CompanyBranchId", mrn.CompanyBranchId);
                        sqlCommand.Parameters.AddWithValue("@DispatchRefNo", mrn.DispatchRefNo);
                        sqlCommand.Parameters.AddWithValue("@DispatchRefDate", mrn.DispatchRefDate);

                        sqlCommand.Parameters.AddWithValue("@LRNo", mrn.LRNo);
                        sqlCommand.Parameters.AddWithValue("@LRDate", mrn.LRDate);
                        sqlCommand.Parameters.AddWithValue("@TransportVia", mrn.TransportVia);
                        sqlCommand.Parameters.AddWithValue("@NoOfPackets", mrn.NoOfPackets);
                        sqlCommand.Parameters.AddWithValue("@Remarks1", mrn.Remarks1);
                        sqlCommand.Parameters.AddWithValue("@Remarks2", mrn.Remarks2);
                        sqlCommand.Parameters.AddWithValue("@FinYearId", mrn.FinYearId);
                        sqlCommand.Parameters.AddWithValue("@CompanyId", mrn.CompanyId);

                        sqlCommand.Parameters.AddWithValue("@CreatedBy", mrn.CreatedBy);
                        sqlCommand.Parameters.AddWithValue("@MRNProductDetail", dtmrnProduct);

                        sqlCommand.Parameters.Add("@status", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@RetMRNId", SqlDbType.BigInt).Direction = ParameterDirection.Output;

                        sqlCommand.Parameters.AddWithValue("@ApprovalStatus", mrn.ApprovalStatus);

                        con.Open();
                        sqlCommand.ExecuteNonQuery();
                        if (sqlCommand.Parameters["@status"].Value != null)
                        {
                            responseOut.status = Convert.ToString(sqlCommand.Parameters["@status"].Value);
                        }
                        if (responseOut.status != ActionStatus.Success)
                        {
                            if (sqlCommand.Parameters["@message"].Value != null)
                            {
                                responseOut.message = Convert.ToString(sqlCommand.Parameters["@message"].Value);
                            }

                        }
                        else
                        {
                            if (mrn.MRNId == 0)
                            {
                                responseOut.message = ActionMessage.MRNCreatedSuccess;
                            }
                            else
                            {
                                responseOut.message = ActionMessage.MRNUpdatedSuccess;
                            }
                            if (sqlCommand.Parameters["@RetMRNId"].Value != null)
                            {
                                responseOut.trnId = Convert.ToInt64(sqlCommand.Parameters["@RetMRNId"].Value);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return responseOut;

        }
        public DataTable GetMRNProductList(long mrnId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetMRNProducts", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@MRNId", mrnId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetMRNDetail(long mrnId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetMRNDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@MRNId", mrnId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }

        public DataTable GetMRNList(string mrnNo, string vendorName, string dispatchrefNo, DateTime fromDate, DateTime toDate, int companyId, string approvalStatus = "")
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetMRNs", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@MRNNo", mrnNo);
                    da.SelectCommand.Parameters.AddWithValue("@VendorName", vendorName);
                    da.SelectCommand.Parameters.AddWithValue("@DispatchRefNo", dispatchrefNo);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@ApprovalStatus", approvalStatus);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion



        #region  Quotation Register
        public DataTable GetQuotationRegisterList(int customerId, int stateId, DateTime fromDate, DateTime toDate, int companyId, int createdBy, string sortBy, string sortOrder)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetQuotationRegister", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CustomerId", customerId);
                    da.SelectCommand.Parameters.AddWithValue("@StateId", stateId);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@CreatedBy", createdBy);
                    da.SelectCommand.Parameters.AddWithValue("@SortBy", sortBy);
                    da.SelectCommand.Parameters.AddWithValue("@SortOrder", sortOrder);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion


        #region  PO Register
        public DataTable GetPORegisterList(string vendorId, int stateId, DateTime fromDate, DateTime toDate, int companyId, int createdBy, string sortBy, string sortOrder)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetPORegister", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@VendorId", vendorId);
                    da.SelectCommand.Parameters.AddWithValue("@StateId", stateId);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@CreatedBy", createdBy);
                    da.SelectCommand.Parameters.AddWithValue("@SortBy", sortBy);
                    da.SelectCommand.Parameters.AddWithValue("@SortOrder", sortOrder);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion

        #region  PurchaseInvoiceRegister
        public DataTable GetPurchaseInvoiceRegisterList(string vendorId, int stateId, DateTime fromDate, DateTime toDate, int companyId, int createdBy, string sortBy, string sortOrder)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetPurchaseInvoiceRegister", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@VendorId", vendorId);
                    da.SelectCommand.Parameters.AddWithValue("@StateId", stateId);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@CreatedBy", createdBy);
                    da.SelectCommand.Parameters.AddWithValue("@SortBy", sortBy);
                    da.SelectCommand.Parameters.AddWithValue("@SortOrder", sortOrder);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion

        #region  SO Register
        public DataTable GetSORegisterList(int customerId, int stateId, int shippingstateId, DateTime fromDate, DateTime toDate, int createdBy, int companyId, string sortBy, string sortOrder)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSORegister", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CustomerId", customerId);
                    da.SelectCommand.Parameters.AddWithValue("@StateId", stateId);
                    da.SelectCommand.Parameters.AddWithValue("@ShippingStateId", shippingstateId);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@CreatedBy", createdBy);
                    da.SelectCommand.Parameters.AddWithValue("@SortBy", sortBy);
                    da.SelectCommand.Parameters.AddWithValue("@SortOrder", sortOrder);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion



        #region  Sale Invoice Register
        public DataTable GetSaleInvoiceRegisterList(int customerId, int stateId, int shippingstateId, DateTime fromDate, DateTime toDate, int companyId, int createdBy, string sortBy, string sortOrder)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSaleInvoiceRegister", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CustomerId", customerId);
                    da.SelectCommand.Parameters.AddWithValue("@StateId", stateId);
                    da.SelectCommand.Parameters.AddWithValue("@ShippingStateId", shippingstateId);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@CreatedBy", createdBy);
                    da.SelectCommand.Parameters.AddWithValue("@SortBy", sortBy);
                    da.SelectCommand.Parameters.AddWithValue("@SortOrder", sortOrder);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion

        #region  MRN Register
        public DataTable GetMRNRegisterList(int vendorId, int shippingstateId, DateTime fromDate, DateTime toDate, int companyId, int createdBy, string sortBy, string sortOrder)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetMRNRegister", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@VendorId", vendorId);
                    da.SelectCommand.Parameters.AddWithValue("@ShippingStateId", shippingstateId);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@CreatedBy", createdBy);
                    da.SelectCommand.Parameters.AddWithValue("@SortBy", sortBy);
                    da.SelectCommand.Parameters.AddWithValue("@SortOrder", sortOrder);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }


        #endregion 


        #region STN
        public ResponseOut AddEditSTN(STN stn, List<STNProductDetail> stnProductList)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                DataTable dtstnProduct = new DataTable();
                dtstnProduct.Columns.Add("STNProductDetailId", typeof(Int64));
                dtstnProduct.Columns.Add("STNId", typeof(Int64));
                dtstnProduct.Columns.Add("ProductId", typeof(Int64));
                dtstnProduct.Columns.Add("ProductShortDesc", typeof(string));
                dtstnProduct.Columns.Add("Price", typeof(decimal));
                dtstnProduct.Columns.Add("Quantity", typeof(decimal));
                dtstnProduct.Columns.Add("TotalPrice", typeof(decimal));

                if (stnProductList != null && stnProductList.Count > 0)
                {
                    foreach (STNProductDetail item in stnProductList)
                    {
                        DataRow dtrow = dtstnProduct.NewRow();
                        dtrow["STNProductDetailId"] = 0;
                        dtrow["STNId"] = 0;
                        dtrow["ProductId"] = item.ProductId;
                        dtrow["ProductShortDesc"] = item.ProductShortDesc;
                        dtrow["Price"] = item.Price;
                        dtrow["Quantity"] = item.Quantity;
                        dtrow["TotalPrice"] = item.TotalPrice;

                        dtstnProduct.Rows.Add(dtrow);
                    }
                    dtstnProduct.AcceptChanges();

                }



                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("proc_AddEditSTN", con))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@STNId", stn.STNId);
                        sqlCommand.Parameters.AddWithValue("@STNDate", stn.STNDate);
                        sqlCommand.Parameters.AddWithValue("@GRNo", stn.GRNo);
                        sqlCommand.Parameters.AddWithValue("@GRDate", stn.GRDate);
                        // sqlCommand.Parameters.AddWithValue("@InvoiceId", stn.InvoiceId);
                        //  sqlCommand.Parameters.AddWithValue("@InvoiceNo", stn.InvoiceNo);
                        sqlCommand.Parameters.AddWithValue("@ContactPerson", stn.ContactPerson);

                        sqlCommand.Parameters.AddWithValue("@FromWarehouseId", stn.FromWarehouseId);
                        sqlCommand.Parameters.AddWithValue("@ToWarehouseId", stn.ToWarehouseId);

                        sqlCommand.Parameters.AddWithValue("@DispatchRefNo", stn.DispatchRefNo);
                        sqlCommand.Parameters.AddWithValue("@DispatchRefDate", stn.DispatchRefDate);

                        sqlCommand.Parameters.AddWithValue("@LRNo", stn.LRNo);
                        sqlCommand.Parameters.AddWithValue("@LRDate", stn.LRDate);
                        sqlCommand.Parameters.AddWithValue("@TransportVia", stn.TransportVia);
                        sqlCommand.Parameters.AddWithValue("@NoOfPackets", stn.NoOfPackets);

                        sqlCommand.Parameters.AddWithValue("@BasicValue", stn.BasicValue);
                        sqlCommand.Parameters.AddWithValue("@TotalValue", stn.TotalValue);
                        sqlCommand.Parameters.AddWithValue("@FreightValue", stn.FreightValue);
                        sqlCommand.Parameters.AddWithValue("@LoadingValue", stn.LoadingValue);

                        sqlCommand.Parameters.AddWithValue("@Remarks1", stn.Remarks1);
                        sqlCommand.Parameters.AddWithValue("@Remarks2", stn.Remarks2);
                        sqlCommand.Parameters.AddWithValue("@FinYearId", stn.FinYearId);
                        sqlCommand.Parameters.AddWithValue("@CompanyId", stn.CompanyId);


                        sqlCommand.Parameters.AddWithValue("@CreatedBy", stn.CreatedBy);
                        sqlCommand.Parameters.AddWithValue("@STNProductDetail", dtstnProduct);

                        sqlCommand.Parameters.Add("@status", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@RetSTNId", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.AddWithValue("@ApprovalStatus", stn.ApprovalStatus);
                        con.Open();
                        sqlCommand.ExecuteNonQuery();
                        if (sqlCommand.Parameters["@status"].Value != null)
                        {
                            responseOut.status = Convert.ToString(sqlCommand.Parameters["@status"].Value);
                        }
                        if (responseOut.status != ActionStatus.Success)
                        {
                            if (sqlCommand.Parameters["@message"].Value != null)
                            {
                                responseOut.message = Convert.ToString(sqlCommand.Parameters["@message"].Value);
                            }

                        }
                        else
                        {
                            if (stn.STNId == 0)
                            {
                                responseOut.message = ActionMessage.STNCreatedSuccess;
                            }
                            else
                            {
                                responseOut.message = ActionMessage.STNUpdatedSuccess;
                            }
                            if (sqlCommand.Parameters["@RetSTNId"].Value != null)
                            {
                                responseOut.trnId = Convert.ToInt64(sqlCommand.Parameters["@RetSTNId"].Value);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return responseOut;

        }
        public DataTable GetSTNProductList(long stnId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSTNProducts", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@STNId", stnId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetSTNList(string stnNo, string glNo, int fromLocation, int toLocation, DateTime fromDate, DateTime toDate, int companyId, string displayType, string approvalStatus)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSTNs", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@STNNo", stnNo);
                    da.SelectCommand.Parameters.AddWithValue("@GRNo", glNo);
                    da.SelectCommand.Parameters.AddWithValue("@FromLocation", fromLocation);
                    da.SelectCommand.Parameters.AddWithValue("@ToLocation", toLocation);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@DisplayType", displayType);
                    da.SelectCommand.Parameters.AddWithValue("@ApprovalStatus", approvalStatus);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetSTNDetail(long stnId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSTNDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@STNId", stnId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }
        #endregion

        #region STR
        public ResponseOut AddEditSTR(STR str, List<STRProductDetail> strProductList)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                DataTable dtstrProduct = new DataTable();
                dtstrProduct.Columns.Add("STRProductDetailId", typeof(Int64));
                dtstrProduct.Columns.Add("STRId", typeof(Int64));
                dtstrProduct.Columns.Add("ProductId", typeof(Int64));
                dtstrProduct.Columns.Add("ProductShortDesc", typeof(string));
                dtstrProduct.Columns.Add("Price", typeof(decimal));
                dtstrProduct.Columns.Add("Quantity", typeof(decimal));
                dtstrProduct.Columns.Add("TotalPrice", typeof(decimal));

                if (strProductList != null && strProductList.Count > 0)
                {
                    foreach (STRProductDetail item in strProductList)
                    {
                        DataRow dtrow = dtstrProduct.NewRow();
                        dtrow["STRProductDetailId"] = 0;
                        dtrow["STRId"] = 0;
                        dtrow["ProductId"] = item.ProductId;
                        dtrow["ProductShortDesc"] = item.ProductShortDesc;
                        dtrow["Price"] = item.Price;
                        dtrow["Quantity"] = item.Quantity;
                        dtrow["TotalPrice"] = item.TotalPrice;

                        dtstrProduct.Rows.Add(dtrow);
                    }
                    dtstrProduct.AcceptChanges();

                }



                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("proc_AddEditSTR", con))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@STRId", str.STRId);
                        sqlCommand.Parameters.AddWithValue("@STRDate", str.STRDate);

                        sqlCommand.Parameters.AddWithValue("@STNId", str.STNId);
                        sqlCommand.Parameters.AddWithValue("@STNNo", str.STNNo);


                        sqlCommand.Parameters.AddWithValue("@GRNo", str.GRNo);
                        sqlCommand.Parameters.AddWithValue("@GRDate", str.GRDate);

                        sqlCommand.Parameters.AddWithValue("@ContactPerson", str.ContactPerson);

                        sqlCommand.Parameters.AddWithValue("@FromWarehouseId", str.FromWarehouseId);
                        sqlCommand.Parameters.AddWithValue("@ToWarehouseId", str.ToWarehouseId);

                        sqlCommand.Parameters.AddWithValue("@DispatchRefNo", str.DispatchRefNo);
                        sqlCommand.Parameters.AddWithValue("@DispatchRefDate", str.DispatchRefDate);


                        sqlCommand.Parameters.AddWithValue("@ApprovalStatus", str.ApprovalStatus);

                        sqlCommand.Parameters.AddWithValue("@LRNo", str.LRNo);
                        sqlCommand.Parameters.AddWithValue("@LRDate", str.LRDate);
                        sqlCommand.Parameters.AddWithValue("@TransportVia", str.TransportVia);
                        sqlCommand.Parameters.AddWithValue("@NoOfPackets", str.NoOfPackets);

                        sqlCommand.Parameters.AddWithValue("@BasicValue", str.BasicValue);
                        sqlCommand.Parameters.AddWithValue("@TotalValue", str.TotalValue);
                        sqlCommand.Parameters.AddWithValue("@FreightValue", str.FreightValue);
                        sqlCommand.Parameters.AddWithValue("@LoadingValue", str.LoadingValue);

                        sqlCommand.Parameters.AddWithValue("@Remarks1", str.Remarks1);
                        sqlCommand.Parameters.AddWithValue("@Remarks2", str.Remarks2);
                        sqlCommand.Parameters.AddWithValue("@FinYearId", str.FinYearId);
                        sqlCommand.Parameters.AddWithValue("@CompanyId", str.CompanyId);

                        sqlCommand.Parameters.AddWithValue("@CreatedBy", str.CreatedBy);
                        sqlCommand.Parameters.AddWithValue("@STRProductDetail", dtstrProduct);

                        sqlCommand.Parameters.Add("@status", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@RetSTRId", SqlDbType.BigInt).Direction = ParameterDirection.Output;

                        con.Open();
                        sqlCommand.ExecuteNonQuery();
                        if (sqlCommand.Parameters["@status"].Value != null)
                        {
                            responseOut.status = Convert.ToString(sqlCommand.Parameters["@status"].Value);
                        }
                        if (responseOut.status != ActionStatus.Success)
                        {
                            if (sqlCommand.Parameters["@message"].Value != null)
                            {
                                responseOut.message = Convert.ToString(sqlCommand.Parameters["@message"].Value);
                            }

                        }
                        else
                        {
                            if (str.STRId == 0)
                            {
                                responseOut.message = ActionMessage.STRCreatedSuccess;
                            }
                            else
                            {
                                responseOut.message = ActionMessage.STRUpdatedSuccess;
                            }
                            if (sqlCommand.Parameters["@RetSTRId"].Value != null)
                            {
                                responseOut.trnId = Convert.ToInt64(sqlCommand.Parameters["@RetSTRId"].Value);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return responseOut;

        }
        public DataTable GetSTRProductList(long stnId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSTRProducts", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@STRId", stnId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetSTRList(string stnNo, string grNo, int fromLocation, int toLocation, DateTime fromDate, DateTime toDate, int companyId, string approvalStatus)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSTRs", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@STRNo", stnNo);
                    da.SelectCommand.Parameters.AddWithValue("@GRNo", grNo);
                    da.SelectCommand.Parameters.AddWithValue("@FromLocation", fromLocation);
                    da.SelectCommand.Parameters.AddWithValue("@ToLocation", toLocation);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@ApprovalStatus", approvalStatus);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetSTRDetail(long strId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSTRDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@STRId", strId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }
        #endregion

        #region Stock Ledger
        public DataTable GetStockLedgerDetail(int productTypeId, string assemblyType, int productMainGroupId, int productSubGroupId, long productId, int customerBranchId, DateTime fromDate, DateTime toDate, int companyId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_PrintStockLedger", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ProductTypeId", productTypeId);
                    da.SelectCommand.Parameters.AddWithValue("@AssemblyType", assemblyType);
                    da.SelectCommand.Parameters.AddWithValue("@ProductMainGroupId", productMainGroupId);
                    da.SelectCommand.Parameters.AddWithValue("@ProductSubGroupId", productSubGroupId);
                    da.SelectCommand.Parameters.AddWithValue("@ProductId", productId);
                    da.SelectCommand.Parameters.AddWithValue("@CustomerBranchId", customerBranchId);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);

                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }
        public DataTable GetStockLedgerSummary(int productTypeId, string assemblyType, int productMainGroupId, int productSubGroupId, long productId, int customerBranchId, DateTime fromDate, DateTime toDate, int companyId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_PrintStockSummary", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ProductTypeId", productTypeId);
                    da.SelectCommand.Parameters.AddWithValue("@AssemblyType", assemblyType);
                    da.SelectCommand.Parameters.AddWithValue("@ProductMainGroupId", productMainGroupId);
                    da.SelectCommand.Parameters.AddWithValue("@ProductSubGroupId", productSubGroupId);
                    da.SelectCommand.Parameters.AddWithValue("@ProductId", productId);
                    da.SelectCommand.Parameters.AddWithValue("@CustomerBranchId", customerBranchId);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);

                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }

        public decimal GetProductAvailableStock(long productId, int finYearId, int companyId, int companyBranchId, int trnId, string trnType)
        {
            decimal availableStock = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("proc_GetProductAvailableStock", con))
                    {
                        con.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ProductId", productId);
                        cmd.Parameters.AddWithValue("@FinYearId", finYearId);
                        cmd.Parameters.AddWithValue("@CompanyId", companyId);
                        cmd.Parameters.AddWithValue("@CompanyBranchId", companyBranchId);
                        cmd.Parameters.AddWithValue("@TrnId", trnId);
                        cmd.Parameters.AddWithValue("@TrnType", trnType);
                        object qty = cmd.ExecuteScalar();
                        if (qty != null)
                        {
                            availableStock = Convert.ToDecimal(qty);
                        }
                        else
                        {
                            availableStock = 0;
                        }

                    }


                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return availableStock;
        }


        #endregion

        #region  DeliveryChallanRegister
        public DataTable GetDeliveryChallanRegisterList(int customerId, int stateId, int shippingstateId, DateTime fromDate, DateTime toDate, int createdBy, int companyId, string sortBy, string sortOrder)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetDeliveryChallanRegister", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CustomerId", customerId);
                    da.SelectCommand.Parameters.AddWithValue("@StateId", stateId);
                    da.SelectCommand.Parameters.AddWithValue("@ShippingStateId", shippingstateId);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@CreatedBy", createdBy);
                    da.SelectCommand.Parameters.AddWithValue("@SortBy", sortBy);
                    da.SelectCommand.Parameters.AddWithValue("@SortOrder", sortOrder);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion

        #region CustomerFormRegister
        public DataTable GetCFormRegisterList(string formStatus, int customerId, string invoiceNo, string refNo, DateTime fromDate, DateTime toDate, int companyId, int createdBy, string sortBy, string sortOrder)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetCustomerFormRegister", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@FormStatus", formStatus);
                    da.SelectCommand.Parameters.AddWithValue("@CustomerId", customerId);
                    da.SelectCommand.Parameters.AddWithValue("@InvoiceNo", invoiceNo);
                    da.SelectCommand.Parameters.AddWithValue("@RefNo", refNo);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@CreatedBy", createdBy);
                    da.SelectCommand.Parameters.AddWithValue("@SortBy", sortBy);
                    da.SelectCommand.Parameters.AddWithValue("@SortOrder", sortOrder);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion

        #region STN Register

        public DataTable GetSTNRegisterList(string stnNo, string glNo, int fromLocation, int toLocation, DateTime fromDate, DateTime toDate, int companyId, int createdBy, string sortBy, string sortOrder)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSTNRegister", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@STNNo", stnNo);
                    da.SelectCommand.Parameters.AddWithValue("@GRNo", glNo);
                    da.SelectCommand.Parameters.AddWithValue("@FromLocation", fromLocation);
                    da.SelectCommand.Parameters.AddWithValue("@ToLocation", toLocation);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@CreatedBy", createdBy);
                    da.SelectCommand.Parameters.AddWithValue("@SortBy", sortBy);
                    da.SelectCommand.Parameters.AddWithValue("@SortOrder", sortOrder);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion

        #region STR Register
        public DataTable GetSTRRegisterList(string strNo, string glNo, int fromLocation, int toLocation, DateTime fromDate, DateTime toDate, int companyId, int createdBy, string sortBy, string sortOrder)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSTRRegister", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@STRNo", strNo);
                    da.SelectCommand.Parameters.AddWithValue("@GRNo", glNo);
                    da.SelectCommand.Parameters.AddWithValue("@FromLocation", fromLocation);
                    da.SelectCommand.Parameters.AddWithValue("@ToLocation", toLocation);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@CreatedBy", createdBy);
                    da.SelectCommand.Parameters.AddWithValue("@SortBy", sortBy);
                    da.SelectCommand.Parameters.AddWithValue("@SortOrder", sortOrder);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion



        #region CustomerPaymentRegister
        public DataTable GetCustomerPaymentRegisterList(int customerId, int paymentModeId, string sortBy, string sortOrder, DateTime fromDate, DateTime toDate, int companyId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetCustomerPaymentRegister", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CustomerId", customerId);
                    da.SelectCommand.Parameters.AddWithValue("@PaymentModeId", paymentModeId);
                    da.SelectCommand.Parameters.AddWithValue("@SortBy", sortBy);
                    da.SelectCommand.Parameters.AddWithValue("@SortOrder", sortOrder);
                    da.SelectCommand.Parameters.AddWithValue("@FormPaymentDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToPaymentDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion

        #region VendorPaymentRegister
        public DataTable GetVendorPaymentRegisterList(int vendorId, int paymentModeId, string sortBy, string sortOrder, DateTime fromDate, DateTime toDate, int companyId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetVendorPaymentRegister", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@VendorId", vendorId);
                    da.SelectCommand.Parameters.AddWithValue("@PaymentModeId", paymentModeId);
                    da.SelectCommand.Parameters.AddWithValue("@SortBy", sortBy);
                    da.SelectCommand.Parameters.AddWithValue("@SortOrder", sortOrder);
                    da.SelectCommand.Parameters.AddWithValue("@FormPaymentDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToPaymentDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId); ;
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion

        #region UserEmailSetting 
        public DataTable GetUserEmailSettingList(string fullName, string userName)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetUsereMailsettings", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@FullName", fullName);
                    da.SelectCommand.Parameters.AddWithValue("@UserName", userName);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetUserEmailSettingDetail(int settingId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetUserEmailSettingDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@SettingId", settingId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetUserEmailSettingDetailByUserId(int userId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetUserEmailSettingByUserId", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@UserId", userId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion

        #region Sale Summary Register
        public DataTable GetSaleSummaryRegister(int customerId, int userId, int stateId, int companyId, DateTime fromDate, DateTime toDate)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_PrintSaleSummaryRegister", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CustomerId", customerId);
                    da.SelectCommand.Parameters.AddWithValue("@Userid", userId);
                    da.SelectCommand.Parameters.AddWithValue("@StateId", stateId);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);

                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion

        #region VendorForm
        public DataTable GetVendorFormList(string FormStatus, string vendorName, string invoiceNo, string refNo, DateTime fromDate, DateTime toDate, int companyId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetVendorForm", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@FormStatus", FormStatus);
                    da.SelectCommand.Parameters.AddWithValue("@VendorName", vendorName);
                    da.SelectCommand.Parameters.AddWithValue("@InvoiceNo", invoiceNo);
                    da.SelectCommand.Parameters.AddWithValue("@RefNo", refNo);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetVendorFormDetail(long vendorFormTrnId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetVendorFormDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@VendorFormTrnId", vendorFormTrnId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }
        #endregion



        #region Purchase Summary Register
        public DataTable GetPurchaseSummaryRegister(int vendorId, int userId, int stateId, int companyId, DateTime fromDate, DateTime toDate)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_PrintPurchaseSummaryRegister", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@VendorId", vendorId);
                    da.SelectCommand.Parameters.AddWithValue("@Userid", userId);
                    da.SelectCommand.Parameters.AddWithValue("@StateId", stateId);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);

                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion

        #region GL
        public ResponseOut AddEditGL(GL gl, GLDetail glDetail)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("proc_AddEditGL", con))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@GLId", gl.GLId);
                        sqlCommand.Parameters.AddWithValue("@GLCode", gl.GLCode);
                        sqlCommand.Parameters.AddWithValue("@GLHead", gl.GLHead);
                        sqlCommand.Parameters.AddWithValue("@GLType", gl.GLType);
                        sqlCommand.Parameters.AddWithValue("@GLSubGroupId", gl.GLSubGroupId);
                        sqlCommand.Parameters.AddWithValue("@SLTypeId", gl.SLTypeId);
                        sqlCommand.Parameters.AddWithValue("@IsBookGL", gl.IsBookGL);
                        sqlCommand.Parameters.AddWithValue("@IsBranchGL", gl.IsBranchGL);
                        sqlCommand.Parameters.AddWithValue("@IsDebtorGL", gl.IsDebtorGL);

                        sqlCommand.Parameters.AddWithValue("@IsCreditorGL", gl.IsCreditorGL);
                        sqlCommand.Parameters.AddWithValue("@IsTaxGL", gl.IsTaxGL);
                        sqlCommand.Parameters.AddWithValue("@IsPostGL", gl.IsPostGL);
                        sqlCommand.Parameters.AddWithValue("@CompanyId", gl.CompanyId);
                        sqlCommand.Parameters.AddWithValue("@CreatedBy", gl.CreatedBy);
                        sqlCommand.Parameters.AddWithValue("@GLStatus", gl.Status);

                        sqlCommand.Parameters.AddWithValue("@FinYearId", glDetail.FinYearId);
                        sqlCommand.Parameters.AddWithValue("@OpeningBalanceDebit", glDetail.OpeningBalanceDebit);
                        sqlCommand.Parameters.AddWithValue("@OpeningBalanceCredit", glDetail.OpeningBalanceCredit);
                        sqlCommand.Parameters.AddWithValue("@OpeningBalance", glDetail.OpeningBalance);
                        sqlCommand.Parameters.AddWithValue("@GLDetailStatus", gl.Status);


                        sqlCommand.Parameters.Add("@status", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;


                        con.Open();
                        sqlCommand.ExecuteNonQuery();
                        if (sqlCommand.Parameters["@status"].Value != null)
                        {
                            responseOut.status = Convert.ToString(sqlCommand.Parameters["@status"].Value);
                        }

                        if (responseOut.status != ActionStatus.Success)
                        {
                            if (sqlCommand.Parameters["@message"].Value != null)
                            {
                                responseOut.message = Convert.ToString(sqlCommand.Parameters["@message"].Value);
                            }

                        }
                        else
                        {
                            if (gl.GLId == 0)
                            {
                                responseOut.message = ActionMessage.GLCreatedSuccess;
                            }
                            else
                            {
                                responseOut.message = ActionMessage.GLUpdatedSuccess;
                            }

                        }





                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return responseOut;

        }

        public DataTable GetGLList(string GLCode = "", string GLType = "", int GLMainGroupId = 0, int GLSubGroupId = 0, int SLTypeId = 0, int companyId = 0, int finYear = 0)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetGLs", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@GLCode", GLCode);
                    da.SelectCommand.Parameters.AddWithValue("@GLType", GLType);
                    da.SelectCommand.Parameters.AddWithValue("@GLMainGroupId", GLMainGroupId);
                    da.SelectCommand.Parameters.AddWithValue("@GLSubGroupId", GLSubGroupId);
                    da.SelectCommand.Parameters.AddWithValue("@SLTypeId", SLTypeId);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@FinYear", finYear);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetGLDetail(int GLId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetGLDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@GLId", GLId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }
        #endregion
        #region Bank Voucher

        public DataTable GetBankVoucherEntryDetail(long voucherId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetBankVoucherEntryDetails", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@VoucherId", voucherId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public ResponseOut AddEditBankVoucher(Voucher voucher, List<VoucherDetail> voucherEntryList)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                DataTable dtVoucherDetail = new DataTable();
                dtVoucherDetail.Columns.Add("SequenceNo", typeof(Int16));
                dtVoucherDetail.Columns.Add("EntryMode", typeof(string));
                dtVoucherDetail.Columns.Add("GLId", typeof(Int32));
                dtVoucherDetail.Columns.Add("GLCode", typeof(string));
                dtVoucherDetail.Columns.Add("SLId", typeof(Int32));
                dtVoucherDetail.Columns.Add("SLCode", typeof(string));
                dtVoucherDetail.Columns.Add("Narration", typeof(string));
                dtVoucherDetail.Columns.Add("PaymentModeId", typeof(Int16));
                dtVoucherDetail.Columns.Add("ChequeRefNo", typeof(string));
                dtVoucherDetail.Columns.Add("ChequeRefDate", typeof(DateTime));
                dtVoucherDetail.Columns.Add("Amount", typeof(decimal));
                dtVoucherDetail.Columns.Add("CostCenterId", typeof(Int32));
                dtVoucherDetail.Columns.Add("SubCostCenterId", typeof(Int32));
                dtVoucherDetail.Columns.Add("ValueDate", typeof(DateTime));
                dtVoucherDetail.Columns.Add("DrawnOnBank", typeof(string));
                dtVoucherDetail.Columns.Add("PO_SONo", typeof(string));
                dtVoucherDetail.Columns.Add("BillNo", typeof(string));
                dtVoucherDetail.Columns.Add("BillDate", typeof(DateTime));
                dtVoucherDetail.Columns.Add("PayeeId", typeof(Int32));
                dtVoucherDetail.Columns.Add("PayeeName", typeof(string));

                if (voucherEntryList != null && voucherEntryList.Count > 0)
                {
                    foreach (VoucherDetail item in voucherEntryList)
                    {
                        DataRow dtrow = dtVoucherDetail.NewRow();
                        dtrow["SequenceNo"] = item.SequenceNo;
                        dtrow["EntryMode"] = item.EntryMode;
                        dtrow["GLId"] = item.GLId;
                        dtrow["GLCode"] = item.GLCode;
                        dtrow["SLId"] = item.SLId;
                        dtrow["SLCode"] = item.SLCode;
                        dtrow["Narration"] = item.Narration;
                        dtrow["PaymentModeId"] = item.PaymentModeId;
                        dtrow["ChequeRefNo"] = item.ChequeRefNo;
                        dtrow["ChequeRefDate"] = item.ChequeRefDate;
                        dtrow["Amount"] = item.Amount;
                        dtrow["CostCenterId"] = item.CostCenterId;
                        dtrow["SubCostCenterId"] = item.SubCostCenterId;
                        dtrow["ValueDate"] = item.ValueDate;
                        dtrow["DrawnOnBank"] = item.DrawnOnBank;
                        dtrow["PO_SONo"] = item.PO_SONo;
                        dtrow["BillNo"] = item.BillNo;
                        dtrow["BillDate"] = item.BillDate;
                        dtrow["PayeeId"] = item.PayeeId;
                        dtrow["PayeeName"] = item.PayeeName;
                        dtVoucherDetail.Rows.Add(dtrow);
                    }
                    dtVoucherDetail.AcceptChanges();

                }

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("proc_AddEditBankVoucher", con))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@VoucherId", voucher.VoucherId);
                        sqlCommand.Parameters.AddWithValue("@VoucherDate", voucher.VoucherDate);
                        sqlCommand.Parameters.AddWithValue("@VoucherMode", voucher.VoucherMode);
                        sqlCommand.Parameters.AddWithValue("@VoucherAmount", voucher.VoucherAmount);
                        sqlCommand.Parameters.AddWithValue("@PayeeSLTypeId", voucher.PayeeSLTypeId);
                        sqlCommand.Parameters.AddWithValue("@BookId", voucher.BookId);
                        sqlCommand.Parameters.AddWithValue("@CompanyId", voucher.CompanyId);
                        sqlCommand.Parameters.AddWithValue("@FinYearId", voucher.FinYearId);
                        sqlCommand.Parameters.AddWithValue("@VoucherStatus", voucher.VoucherStatus);

                        sqlCommand.Parameters.AddWithValue("@CreatedBy", voucher.CreatedBy);
                        sqlCommand.Parameters.AddWithValue("@VoucherDetail", dtVoucherDetail);

                        sqlCommand.Parameters.Add("@status", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@RetVoucherId", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                        con.Open();
                        sqlCommand.ExecuteNonQuery();
                        if (sqlCommand.Parameters["@status"].Value != null)
                        {
                            responseOut.status = Convert.ToString(sqlCommand.Parameters["@status"].Value);
                        }
                        if (responseOut.status != ActionStatus.Success)
                        {
                            if (sqlCommand.Parameters["@message"].Value != null)
                            {
                                responseOut.message = Convert.ToString(sqlCommand.Parameters["@message"].Value);
                            }

                        }
                        else
                        {
                            if (voucher.VoucherId == 0)
                            {
                                responseOut.message = ActionMessage.BankVoucherCreatedSuccess;
                            }
                            else
                            {
                                responseOut.message = ActionMessage.BankVoucherUpdatedSuccess;
                            }
                            if (sqlCommand.Parameters["@RetVoucherId"].Value != null)
                            {
                                responseOut.trnId = Convert.ToInt64(sqlCommand.Parameters["@RetVoucherId"].Value);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return responseOut;

        }
        public DataTable GetBankVoucherDetail(long voucherId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetBankVoucherDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@VoucherId", voucherId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }

        #endregion


        #region Cost Center
        public DataTable GetCostCenterList(string costcenterName, int companyId, string status)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetCostCenters", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CostCenterName", costcenterName);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@Status", status);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetCostCenterDetail(int costcenterId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetCostCenterDetails", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CostCenterId", costcenterId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion


        #region SL
        public DataTable GetSLList(string SLCode, int SLTypeId, int CostCenterId, int CompanyId, string Status)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSLs", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@SLCode", SLCode);
                    da.SelectCommand.Parameters.AddWithValue("@SLTypeId", SLTypeId);
                    da.SelectCommand.Parameters.AddWithValue("@CostCenterId", CostCenterId);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", CompanyId);
                    da.SelectCommand.Parameters.AddWithValue("@Status", Status);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }
        public DataTable GetSLDetail(int sLId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSLDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@SLId", sLId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }
        #endregion

        #region SL Detail
        public DataTable GetSLDetailList(int SLTypeId, int GLId, int SLId, int FinYearId, int CompanyId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSLDetailList", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@SLTypeId", SLTypeId);
                    da.SelectCommand.Parameters.AddWithValue("@GLId", GLId);
                    da.SelectCommand.Parameters.AddWithValue("@SLId", SLId);
                    da.SelectCommand.Parameters.AddWithValue("@FinYearId", FinYearId);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", CompanyId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }
        #endregion

        #region Inventory Dashboard Data
        public DataTable GetDashboard_QuatationCount(int companyId, int userId, int reportingUserId, int reportingRoleId, int finYearId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_Dashboard_GetQuatationCount", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@FinYearId", finYearId);
                    da.SelectCommand.Parameters.AddWithValue("@UserId", userId);
                    da.SelectCommand.Parameters.AddWithValue("@ReportingUserId", reportingUserId);
                    da.SelectCommand.Parameters.AddWithValue("@ReportingRoleId", reportingRoleId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }
        public DataTable GetDashboard_SaleOrderCount(int companyId, int userId, int reportingUserId, int reportingRoleId, int finYearId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_Dashboard_GetSaleOrderCount", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@FinYearId", finYearId);
                    da.SelectCommand.Parameters.AddWithValue("@UserId", userId);
                    da.SelectCommand.Parameters.AddWithValue("@ReportingUserId", reportingUserId);
                    da.SelectCommand.Parameters.AddWithValue("@ReportingRoleId", reportingRoleId);

                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }
        public DataTable GetDashboard_SICount(int companyId, int userId, int reportingUserId, int reportingRoleId, int finYearId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_Dashboard_GetSICount", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@FinYearId", finYearId);
                    da.SelectCommand.Parameters.AddWithValue("@UserId", userId);
                    da.SelectCommand.Parameters.AddWithValue("@ReportingUserId", reportingUserId);
                    da.SelectCommand.Parameters.AddWithValue("@ReportingRoleId", reportingRoleId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }
        public DataTable GetDashboard_TodayPOSumAmount(int companyId, int userId, int reportingUserId, int reportingRoleId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_Dashboard_GetTodayPOSumAmount", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@UserId", userId);
                    da.SelectCommand.Parameters.AddWithValue("@ReportingUserId", reportingUserId);
                    da.SelectCommand.Parameters.AddWithValue("@ReportingRoleId", reportingRoleId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }

        public DataTable GetDashboard_TodayPISumAmount(int companyId, int userId, int reportingUserId, int reportingRoleId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_Dashboard_GetTodayPISumAmount", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@UserId", userId);
                    da.SelectCommand.Parameters.AddWithValue("@ReportingUserId", reportingUserId);
                    da.SelectCommand.Parameters.AddWithValue("@ReportingRoleId", reportingRoleId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }

        #endregion

        #region HomeUpdate
        public DataTable GetHomeUpdateList(string homeUpdatesArea = "")
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetHomeUpdates", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("HomeUpdatesArea", homeUpdatesArea);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }
        #endregion

        public ResponseOut UpdateSellerProductPrice(string sellerproductdetailsID, string price, string MinimumOrderQuantity)
        {
            ResponseOut responseOut = new ResponseOut();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("proc_AddEditSellerPrice", con))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@sellerproductdetailsID", Convert.ToInt64(sellerproductdetailsID));
                    sqlCommand.Parameters.AddWithValue("@price", Convert.ToDecimal(price));
                    sqlCommand.Parameters.AddWithValue("@MinimumOrderQuantity", Convert.ToDecimal(MinimumOrderQuantity));
                    sqlCommand.Parameters.Add("@status", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                    sqlCommand.Parameters.Add("@message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    sqlCommand.Parameters.Add("@RetSOId", SqlDbType.BigInt).Direction = ParameterDirection.Output;

                    con.Open();
                    sqlCommand.ExecuteNonQuery();
                    if (sqlCommand.Parameters["@status"].Value != null)
                    {
                        responseOut.status = Convert.ToString(sqlCommand.Parameters["@status"].Value);
                    }
                    if (responseOut.status != ActionStatus.Success)
                    {
                        if (sqlCommand.Parameters["@message"].Value != null)
                        {
                            responseOut.message = Convert.ToString(sqlCommand.Parameters["@message"].Value);
                        }

                    }

                }
            }

            return responseOut;
        }

        public ResponseOut UpdateBuyerProductQTY(string buyerproductdetailsID, string OrderQuantity)
        {
            ResponseOut responseOut = new ResponseOut();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("proc_AddEditBuyerDemandQuantity", con))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@buyerproductdetailsID", Convert.ToInt64(buyerproductdetailsID));
                    sqlCommand.Parameters.AddWithValue("@OrderQuantity", Convert.ToDecimal(OrderQuantity));
                    sqlCommand.Parameters.Add("@status", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                    sqlCommand.Parameters.Add("@message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    sqlCommand.Parameters.Add("@RetSOId", SqlDbType.BigInt).Direction = ParameterDirection.Output;

                    con.Open();
                    sqlCommand.ExecuteNonQuery();
                    if (sqlCommand.Parameters["@status"].Value != null)
                    {
                        responseOut.status = Convert.ToString(sqlCommand.Parameters["@status"].Value);
                    }
                    if (responseOut.status != ActionStatus.Success)
                    {
                        if (sqlCommand.Parameters["@message"].Value != null)
                        {
                            responseOut.message = Convert.ToString(sqlCommand.Parameters["@message"].Value);
                        }

                    }

                }
            }

            return responseOut;
        }

        public DataTable GetProductpricedetailsBySellerProductId(long SellerProductId)
        {

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_Dashboard_sellerProductPricedetails", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@sellerproductdetailsID", SellerProductId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

          
        }
        public DataTable GetProductQtydetailsByBuyerrProductId(long BuyerProductDetailId)
        {

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_Dashboard_Buyer_demand_quantity_details", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@BuyerProductDetailId", BuyerProductDetailId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

          
        }

        public int GetUserMaxCount()
        {
            int UserCodeMax = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_getUserCode_Sel", con))
                    {
                        con.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        object number = cmd.ExecuteScalar();
                        if (number != null)
                        {
                            UserCodeMax = Convert.ToInt16(number);
                        }
                        else
                        {
                            UserCodeMax = 0;
                        }

                    }


                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return UserCodeMax;
        }

        public int GetUOMId(long ParentClassId,long CategoryId, long SubCategoryId)
        {
            int UOMId = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_getUOMId_Sel", con))
                    {
                        con.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ParentClassId", ParentClassId);
                        cmd.Parameters.AddWithValue("@CategoryId", CategoryId);
                        cmd.Parameters.AddWithValue("@SubCategoryId", SubCategoryId);
                        object number = cmd.ExecuteScalar();
                        if (number != null)
                        {
                            UOMId = Convert.ToInt16(number);
                        }
                        else
                        {
                            UOMId = 0;
                        }

                    }


                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return UOMId;
        }

     
        public DataTable GetUOMListforSpecification(long ParentClassId, long CategoryId, long SubCategoryId,long ProductTechSpecId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("sp_getUOMListforSpecification_Sel", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ParentClassId", ParentClassId);
                    da.SelectCommand.Parameters.AddWithValue("@CategoryId", CategoryId);
                    da.SelectCommand.Parameters.AddWithValue("@SubCategoryId", SubCategoryId);
                    da.SelectCommand.Parameters.AddWithValue("@ProductTechSpecId", ProductTechSpecId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;


            
        }

        public ResponseOut AddEditUOM_SPC(AddEditUOM_SPC UOMLIst)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("proc_Edit_UOM", con))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@ParentClassId", UOMLIst.ParentClassId);
                        sqlCommand.Parameters.AddWithValue("@CategoryId", UOMLIst.CategoryId);
                        sqlCommand.Parameters.AddWithValue("@SubCategoryId", UOMLIst.SubCategoryId);
                        sqlCommand.Parameters.AddWithValue("@ProductTechSpecId", UOMLIst.ProductTechSpecId);
                        sqlCommand.Parameters.AddWithValue("@UOMId", UOMLIst.UomId);
                        sqlCommand.Parameters.AddWithValue("@UOM_sps_Id", UOMLIst.UOM_sps_Id);


                        sqlCommand.Parameters.Add("@status", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                        con.Open();
                        sqlCommand.ExecuteNonQuery();
                        if (sqlCommand.Parameters["@status"].Value != null)
                        {
                            responseOut.status = Convert.ToString(sqlCommand.Parameters["@status"].Value);
                        }
                        if (responseOut.status != ActionStatus.Success)
                        {
                            if (sqlCommand.Parameters["@message"].Value != null)
                            {
                                responseOut.message = Convert.ToString(sqlCommand.Parameters["@message"].Value);
                            }

                        }
                        else
                        {
                            responseOut.message = ActionMessage.UOMUpdatedSuccess;


                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return responseOut;

        }

        public string GetPRoductCodeName(string prduoctCodeID)
        {
            string UOMId = "0";
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_GetPRoductCodeName", con))
                    {
                        con.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@prduoctCodeID", prduoctCodeID);
                       
                        object number = cmd.ExecuteScalar();
                        if (number != null)
                        {
                            UOMId = number.ToString();
                        }
                        else
                        {
                            UOMId = "";
                        }

                    }


                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return UOMId;
        }
    }
}

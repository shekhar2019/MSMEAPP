using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Portal.Core;
using Portal.Core.ViewModel;
using Portal.Common;
using System.Reflection;
using System.IO;
using System.Data;
using System.Text;
namespace Portal.Controllers
{
    public class UploadUtilityController : BaseController
    {
        //
        // GET: /UploadUtility/

        #region Import Customer
        [ValidateRequest(true, UserInterfaceHelper.ImportCustomer, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        [HttpGet]
        public ActionResult ImportCustomer()
        {
            try
            {


            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return View();
        }
        [ValidateRequest(true, UserInterfaceHelper.ImportCustomer, (int)AccessMode.AddAccess, (int)RequestMode.GetPost)]
        [ActionName("ImportCustomer")]
        [HttpPost]
        public ActionResult ImportCustomerData()
        {
            UploadUtilityBL utilityBL = new UploadUtilityBL();
            CustomerBL customerBL = new CustomerBL();
            try
            {
                if (Request.Files["FileUpload1"].ContentLength > 0)
                {
                    string extension = System.IO.Path.GetExtension(Request.Files["FileUpload1"].FileName).ToLower();
                    string connString = "";
                    string[] validFileTypes = { ".xls",".xlsx", ".csv" };

                    string path1 = string.Format("{0}/{1}", Server.MapPath("~/Content/Uploads"), Request.Files["FileUpload1"].FileName);
                    if (!Directory.Exists(path1))
                    {
                        Directory.CreateDirectory(Server.MapPath("~/Content/Uploads"));
                    }
                    if (validFileTypes.Contains(extension))
                    {
                        DataTable dt = new DataTable();
                        if (System.IO.File.Exists(path1))
                        { System.IO.File.Delete(path1); }
                        Request.Files["FileUpload1"].SaveAs(path1);
                        if (extension == ".csv")
                        {
                            dt = CommonHelper.ConvertCSVtoDataTable(path1);
                            ViewBag.Data = dt;
                        }
                        //Connection String to Excel Workbook  
                        else if (extension.Trim() == ".xls")
                        {
                            connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path1 + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                            dt = CommonHelper.ConvertXSLXtoDataTable(path1, connString);
                            ViewBag.Data = dt;
                        }
                        else if (extension.Trim() == ".xlsx")
                        {
                            connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path1 + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                            dt = CommonHelper.ConvertXSLXtoDataTable(path1, connString);
                            ViewBag.Data = dt;
                        }

                        // Code to Update ID based on Name of field
                        StringBuilder strErrMsg = new StringBuilder(" ");
                        if (dt.Rows.Count > 0)
                        {
                            int stateId = 0;
                            int countryId = 0;
                            int employeeID = 0;
                            int customerTypeId = 0;
                            int creditDays = 0;
                            int creditLimit = 0;
                            int rowCounter = 1;
                            int leadID = 0;
                            CustomerViewModel customerViewModel;
                            dt.Columns.Add("UploadStatus", typeof(bool));
                            bool rowVerifyStatus = true;
                            Random rnd = new Random(50000);
                            foreach (DataRow dr in dt.Rows)
                            {
                                customerViewModel = new CustomerViewModel();
                                //code to validate data in excel//
                                if (string.IsNullOrEmpty(Convert.ToString(dr["CustomerName"])))
                                {
                                    strErrMsg.Append("Customer Name Column has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                    rowVerifyStatus = false;
                                }
                                else if (string.IsNullOrEmpty(Convert.ToString(dr["CustomerCode"])))
                                {
                                    strErrMsg.Append("Customer Code Column has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                    rowVerifyStatus = false;
                                }

                                else if (string.IsNullOrEmpty(Convert.ToString(dr["ContactPersonName"])))
                                {
                                    strErrMsg.Append("Contact Person Name has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                    rowVerifyStatus = false;
                                }
                                else if (string.IsNullOrEmpty(Convert.ToString(dr["Designation"])))
                                {
                                    strErrMsg.Append("Designation Column has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                    rowVerifyStatus = false;
                                }
                                else if (string.IsNullOrEmpty(Convert.ToString(dr["Email"])))
                                {
                                    strErrMsg.Append("Email Column has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                    rowVerifyStatus = false;
                                }
                                else if (string.IsNullOrEmpty(Convert.ToString(dr["MobileNo"])))
                                {
                                    strErrMsg.Append("Mobile No. Column has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                    rowVerifyStatus = false;
                                }
                                else if (string.IsNullOrEmpty(Convert.ToString(dr["ContactNo"])))
                                {
                                    strErrMsg.Append("Contact No. Column has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                    rowVerifyStatus = false;
                                }
                                else if (string.IsNullOrEmpty(Convert.ToString(dr["Fax"])))
                                {
                                    strErrMsg.Append("Fax Column has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                    rowVerifyStatus = false;
                                }
                                else if (string.IsNullOrEmpty(Convert.ToString(dr["PrimaryAddress"])))
                                {
                                    strErrMsg.Append("Primary Address Column has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                    rowVerifyStatus = false;
                                }
                                else if (string.IsNullOrEmpty(Convert.ToString(dr["City"])))
                                {
                                    strErrMsg.Append("City Name Column has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                    rowVerifyStatus = false;

                                }
                                else if (string.IsNullOrEmpty(Convert.ToString(dr["PinCode"])))
                                {
                                    strErrMsg.Append("Pin Code Column has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                    rowVerifyStatus = false;
                                }
                                else if (string.IsNullOrEmpty(Convert.ToString(dr["CSTNo"])))
                                {
                                    strErrMsg.Append("CST No has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                    rowVerifyStatus = false;
                                }
                                else if (string.IsNullOrEmpty(Convert.ToString(dr["TINNo"])))
                                {
                                    strErrMsg.Append("TIN No. has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                    rowVerifyStatus = false;
                                }
                                else if (string.IsNullOrEmpty(Convert.ToString(dr["PANNo"])))
                                {
                                    strErrMsg.Append("PAN No. has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                    rowVerifyStatus = false;
                                }
                                else if (string.IsNullOrEmpty(Convert.ToString(dr["GSTNo"])))
                                {
                                    strErrMsg.Append("GST No. Column has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                    rowVerifyStatus = false;
                                }
                                else if (string.IsNullOrEmpty(Convert.ToString(dr["ExciseNo"])))
                                {
                                    strErrMsg.Append("Excise No. Column has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                    rowVerifyStatus = false;
                                }
                                else if (string.IsNullOrEmpty(Convert.ToString(dr["CreditLimit"])) || int.TryParse(Convert.ToString(dr["CreditLimit"]), out creditLimit) == false)
                                {
                                    strErrMsg.Append("Credit Limit Column has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                    rowVerifyStatus = false;
                                }
                                else if (string.IsNullOrEmpty(Convert.ToString(dr["CreditDays"])) || int.TryParse(Convert.ToString(dr["CreditDays"]), out creditDays) == false)
                                {
                                    strErrMsg.Append("Credit Days Column has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                    rowVerifyStatus = false;
                                }

                                //end of code to validate data in excel//

                                //code to get Id from Name data in excel//

                                if (!string.IsNullOrEmpty(Convert.ToString(dr["StateName"])))
                                {
                                    stateId = utilityBL.GetIdByStateName(Convert.ToString(dr["StateName"]));
                                    if (stateId == 0)
                                    {
                                        strErrMsg.Append("Invalid State Name data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                        rowVerifyStatus = false;
                                    }
                                }

                                if (!string.IsNullOrEmpty(Convert.ToString(dr["CountryName"])))
                                {
                                    countryId = utilityBL.GetIdByCountryName(Convert.ToString(dr["CountryName"]));
                                    if (countryId == 0)
                                    {
                                        strErrMsg.Append("Invalid Country Name data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                        rowVerifyStatus = false;
                                    }
                                }

                                if (!string.IsNullOrEmpty(Convert.ToString(dr["EmployeeName"])))
                                {
                                    employeeID = utilityBL.GetIdByEmployeeName(Convert.ToString(dr["EmployeeName"]));
                                    if (employeeID == 0)
                                    {
                                        strErrMsg.Append("Invalid Employee Name data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                        rowVerifyStatus = false;
                                    }
                                }
                                if (!string.IsNullOrEmpty(Convert.ToString(dr["CustomerTypeDesc"])))
                                {
                                    customerTypeId = utilityBL.GetIdByCustomerTypeDesc(Convert.ToString(dr["CustomerTypeDesc"]));
                                    if (customerTypeId == 0)
                                    {
                                        strErrMsg.Append("Invalid Customer Type Desc data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                        rowVerifyStatus = false;
                                    }
                                }
                                if (rowVerifyStatus == true)
                                {
                                    //End of code to get Id from Name data in excel//


                                    customerViewModel.LeadId = leadID;
                                    customerViewModel.CustomerCode = Convert.ToString(dr["CustomerCode"]);
                                    customerViewModel.CustomerName = Convert.ToString(dr["CustomerName"]);
                                    customerViewModel.ContactPersonName = Convert.ToString(dr["ContactPersonName"]);
                                    customerViewModel.Designation = Convert.ToString(dr["Designation"]);
                                    customerViewModel.Email = Convert.ToString(dr["Email"]);
                                    customerViewModel.MobileNo = Convert.ToString(dr["MobileNo"]);
                                    customerViewModel.ContactNo = Convert.ToString(dr["ContactNo"]);
                                    customerViewModel.Fax = Convert.ToString(dr["Fax"]);
                                    customerViewModel.PrimaryAddress = Convert.ToString(dr["PrimaryAddress"]);
                                    customerViewModel.City = Convert.ToString(dr["City"]);
                                    customerViewModel.StateId = stateId;
                                    customerViewModel.CountryId = countryId;
                                    customerViewModel.PinCode = Convert.ToString(dr["PinCode"]);
                                    customerViewModel.CSTNo = Convert.ToString(dr["CSTNo"]);
                                    customerViewModel.TINNo = Convert.ToString(dr["TINNo"]);
                                    customerViewModel.PANNo = Convert.ToString(dr["PANNo"]);
                                    customerViewModel.ExciseNo = Convert.ToString(dr["ExciseNo"]);
                                    customerViewModel.EmployeeId = employeeID;
                                    customerViewModel.CustomerTypeId = customerTypeId;
                                    customerViewModel.CreditLimit = creditLimit;
                                    customerViewModel.CreditDays = creditDays;
                                    customerViewModel.CompanyId = ContextUser.CompanyId;
                                    customerViewModel.CreatedBy = ContextUser.UserId;
                                    customerViewModel.Customer_Status = true;

                                    customerViewModel.CreatedBy = ContextUser.UserId;
                                    ResponseOut responseOut = customerBL.ImportCustomer(customerViewModel);
                                    if (responseOut.status == ActionStatus.Fail)
                                    {
                                        strErrMsg.Append(responseOut.message + ": Error in Inserting in Row #" + rowCounter.ToString() + Environment.NewLine);
                                        dr["UploadStatus"] = false;
                                    }
                                    else
                                    {
                                        dr["UploadStatus"] = true;
                                    }
                                }
                                else
                                {
                                    dr["UploadStatus"] = false;
                                }
                                rowCounter += 1;

                            }
                            dt.AcceptChanges();
                        }
                        else
                        {
                            strErrMsg.Append("Import not found");
                        }

                        ViewBag.Error = strErrMsg.ToString();
                        // End of Code to Update ID based on Name of field

                    }
                    else
                    {
                        ViewBag.Error = "Please Upload Files in .xls, .xlsx or .csv format";
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return View();
        }

        #endregion

        #region Import Employee
        [ValidateRequest(true, UserInterfaceHelper.ImportEmployee, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        [HttpGet]
        public ActionResult ImportEmployee()
        {
            try
            {


            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return View();
        }
        [ValidateRequest(true, UserInterfaceHelper.ImportEmployee, (int)AccessMode.AddAccess, (int)RequestMode.GetPost)]
        [ActionName("ImportEmployee")]
        [HttpPost]
        public ActionResult ImportEmployeeData()
        {
            UploadUtilityBL utilityBL = new UploadUtilityBL();
            EmployeeBL employeeBL = new EmployeeBL();
            try
            {
                if (Request.Files["FileUpload1"].ContentLength > 0)
                {
                    string extension = System.IO.Path.GetExtension(Request.Files["FileUpload1"].FileName).ToLower();
                    string connString = "";
                    string[] validFileTypes = { ".xls", ".xlsx", ".csv" };

                    string path1 = string.Format("{0}/{1}", Server.MapPath("~/Content/Uploads"), Request.Files["FileUpload1"].FileName);
                    if (!Directory.Exists(path1))
                    {
                        Directory.CreateDirectory(Server.MapPath("~/Content/Uploads"));
                    }
                    if (validFileTypes.Contains(extension))
                    {
                        DataTable dt = new DataTable();
                        if (System.IO.File.Exists(path1))
                        { System.IO.File.Delete(path1); }
                        Request.Files["FileUpload1"].SaveAs(path1);
                        if (extension == ".csv")
                        {
                            dt = CommonHelper.ConvertCSVtoDataTable(path1);
                            ViewBag.Data = dt;
                        }
                        //Connection String to Excel Workbook  
                        else if (extension.Trim() == ".xls")
                        {
                            connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path1 + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                            dt = CommonHelper.ConvertXSLXtoDataTable(path1, connString);
                            ViewBag.Data = dt;
                        }
                        else if (extension.Trim() == ".xlsx")
                        {
                            connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path1 + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                            dt = CommonHelper.ConvertXSLXtoDataTable(path1, connString);
                            ViewBag.Data = dt;
                        }

                        // Code to Update ID based on Name of field
                        StringBuilder strErrMsg = new StringBuilder(" ");
                        if (dt.Rows.Count > 0)
                        {
                            int cstateId = 0;
                            int ccountryId = 0;
                            int pstateId = 0;
                            int pcountryId = 0;
                            int designationId = 0;
                            int customerTypeId = 0;
                            int creditDays = 0;
                            int creditLimit = 0;
                            int rowCounter = 1;
                            int departmentId = 0;

                            EmployeeViewModel employeeViewModel;
                            dt.Columns.Add("UploadStatus", typeof(bool));
                            bool rowVerifyStatus = true;
                            Random rnd = new Random(50000);
                            foreach (DataRow dr in dt.Rows)
                            {
                                employeeViewModel = new EmployeeViewModel();
                                //code to validate data in excel//
                                if (string.IsNullOrEmpty(Convert.ToString(dr["EmployeeCode"])))
                                {
                                    strErrMsg.Append("Employee Code Column has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                    rowVerifyStatus = false;
                                }
                                else if (string.IsNullOrEmpty(Convert.ToString(dr["FirstName"])))
                                {
                                    strErrMsg.Append("FirstName Column has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                    rowVerifyStatus = false;
                                }

                                else if (string.IsNullOrEmpty(Convert.ToString(dr["LastName"])))
                                {
                                    strErrMsg.Append("LastName has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                    rowVerifyStatus = false;
                                }
                                else if (string.IsNullOrEmpty(Convert.ToString(dr["FatherSpouseName"])))
                                {
                                    strErrMsg.Append("Father Spouse Name Column has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                    rowVerifyStatus = false;
                                }
                                else if (string.IsNullOrEmpty(Convert.ToString(dr["Gender"])))
                                {
                                    strErrMsg.Append("Gender Column has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                    rowVerifyStatus = false;
                                }
                                else if (string.IsNullOrEmpty(Convert.ToString(dr["MaritalStatus"])))
                                {
                                    strErrMsg.Append("Marital Status Column has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                    rowVerifyStatus = false;
                                }
                                else if (string.IsNullOrEmpty(Convert.ToString(dr["BloodGroup"])))
                                {
                                    strErrMsg.Append("Blood Group Column has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                    rowVerifyStatus = false;
                                }
                                else if (string.IsNullOrEmpty(Convert.ToString(dr["Email"])))
                                {
                                    strErrMsg.Append("Email Column has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                    rowVerifyStatus = false;
                                }
                                else if (string.IsNullOrEmpty(Convert.ToString(dr["AlternateEmail"])))
                                {
                                    strErrMsg.Append("Alternate Email Column has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                    rowVerifyStatus = false;
                                }
                                else if (string.IsNullOrEmpty(Convert.ToString(dr["ContactNo"])))
                                {
                                    strErrMsg.Append("Contact No. Column has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                    rowVerifyStatus = false;
                                }
                                else if (string.IsNullOrEmpty(Convert.ToString(dr["AlternateContactno"])))
                                {
                                    strErrMsg.Append("Alternate Contact No. Column has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                    rowVerifyStatus = false;
                                }
                                else if (string.IsNullOrEmpty(Convert.ToString(dr["MobileNo"])))
                                {
                                    strErrMsg.Append("Mobile No. Column has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                    rowVerifyStatus = false;
                                }
                                else if (string.IsNullOrEmpty(Convert.ToString(dr["CAddress"])))
                                {
                                    strErrMsg.Append("Current Address Column has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                    rowVerifyStatus = false;
                                }
                                else if (string.IsNullOrEmpty(Convert.ToString(dr["CCity"])))
                                {
                                    strErrMsg.Append("Current City Column has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                    rowVerifyStatus = false;
                                }
                                else if (string.IsNullOrEmpty(Convert.ToString(dr["CPinCode"])))
                                {
                                    strErrMsg.Append("Current PinCode Column has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                    rowVerifyStatus = false;
                                }
                                else if (string.IsNullOrEmpty(Convert.ToString(dr["PAddress"])))
                                {
                                    strErrMsg.Append("PAddress Column has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                    rowVerifyStatus = false;
                                }
                                else if (string.IsNullOrEmpty(Convert.ToString(dr["PCity"])))
                                {
                                    strErrMsg.Append("PCity Column has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                    rowVerifyStatus = false;
                                }
                                else if (string.IsNullOrEmpty(Convert.ToString(dr["PPinCode"])))
                                {
                                    strErrMsg.Append("PPinCode Column has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                    rowVerifyStatus = false;
                                }
                                else if (string.IsNullOrEmpty(Convert.ToString(dr["DateOfJoin"])))
                                {
                                    strErrMsg.Append("Date Of Join Column has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                    rowVerifyStatus = false;
                                }
                                else if (string.IsNullOrEmpty(Convert.ToString(dr["DateOfLeave"])))
                                {
                                    strErrMsg.Append("Date Of Leave Column has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                    rowVerifyStatus = false;

                                }
                                else if (string.IsNullOrEmpty(Convert.ToString(dr["PANNo"])))
                                {
                                    strErrMsg.Append("PAN No. Column has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                    rowVerifyStatus = false;
                                }

                                else if (string.IsNullOrEmpty(Convert.ToString(dr["AadharNo"])))
                                {
                                    strErrMsg.Append("Aadhar No. Column has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                    rowVerifyStatus = false;
                                }
                                else if (string.IsNullOrEmpty(Convert.ToString(dr["BankAccountNo"])))
                                {
                                    strErrMsg.Append("Bank Account No. Column has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                    rowVerifyStatus = false;
                                }
                                else if (string.IsNullOrEmpty(Convert.ToString(dr["PFApplicable"])))
                                {
                                    strErrMsg.Append("PFApplicable Column has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                    rowVerifyStatus = false;
                                }
                                else if (string.IsNullOrEmpty(Convert.ToString(dr["PFNo"])))
                                {
                                    strErrMsg.Append("PFNo Column has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                    rowVerifyStatus = false;

                                }
                                else if (string.IsNullOrEmpty(Convert.ToString(dr["ESIApplicable"])))
                                {
                                    strErrMsg.Append("ESIApplicable has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                    rowVerifyStatus = false;

                                }
                                else if (string.IsNullOrEmpty(Convert.ToString(dr["ESINo"])))
                                {
                                    strErrMsg.Append("ESINo Column has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                    rowVerifyStatus = false;
                                }
                                else if (string.IsNullOrEmpty(Convert.ToString(dr["Division"])))
                                {
                                    strErrMsg.Append("Division No has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                    rowVerifyStatus = false;
                                }
                                else if (string.IsNullOrEmpty(Convert.ToString(dr["EmploymentType"])))
                                {
                                    strErrMsg.Append("Employment Type has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                    rowVerifyStatus = false;
                                }
                                else if (string.IsNullOrEmpty(Convert.ToString(dr["EmployeeCurrentStatus"])))
                                {
                                    strErrMsg.Append("Employee Current Status. has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                    rowVerifyStatus = false;
                                }
                                else if (string.IsNullOrEmpty(Convert.ToString(dr["EmployeeStatusPeriod"])))
                                {
                                    strErrMsg.Append("Employee Status Period Column has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                    rowVerifyStatus = false;
                                }
                                else if (string.IsNullOrEmpty(Convert.ToString(dr["EmployeeStatusStartDate"])))
                                {
                                    strErrMsg.Append("Employee Status StartDate Column has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                    rowVerifyStatus = false;
                                }
                                //else if (string.IsNullOrEmpty(Convert.ToString(dr["CreditLimit"])) || int.TryParse(Convert.ToString(dr["CreditLimit"]), out creditLimit) == false)
                                //{
                                //    strErrMsg.Append("Credit Limit Column has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                //    rowVerifyStatus = false;
                                //}
                                //else if (string.IsNullOrEmpty(Convert.ToString(dr["CreditDays"])) || int.TryParse(Convert.ToString(dr["CreditDays"]), out creditDays) == false)
                                //{
                                //    strErrMsg.Append("Credit Days Column has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                //    rowVerifyStatus = false;
                                //}

                                //end of code to validate data in excel//

                                //code to get Id from Name data in excel//

                                if (!string.IsNullOrEmpty(Convert.ToString(dr["CStateName"])))
                                {
                                    cstateId = utilityBL.GetIdByStateName(Convert.ToString(dr["CStateName"]));
                                    if (cstateId == 0)
                                    {
                                        strErrMsg.Append("Invalid State Name data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                        rowVerifyStatus = false;
                                    }
                                }
                                if (!string.IsNullOrEmpty(Convert.ToString(dr["PStateName"])))
                                {
                                    pstateId = utilityBL.GetIdByStateName(Convert.ToString(dr["PStateName"]));
                                    if (pstateId == 0)
                                    {
                                        strErrMsg.Append("Invalid State Name data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                        rowVerifyStatus = false;
                                    }
                                }

                                if (!string.IsNullOrEmpty(Convert.ToString(dr["CCountryName"])))
                                {
                                    ccountryId = utilityBL.GetIdByCountryName(Convert.ToString(dr["CCountryName"]));
                                    if (ccountryId == 0)
                                    {
                                        strErrMsg.Append("Invalid Country Name data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                        rowVerifyStatus = false;
                                    }
                                }
                                if (!string.IsNullOrEmpty(Convert.ToString(dr["PCountryName"])))
                                {
                                    pcountryId = utilityBL.GetIdByCountryName(Convert.ToString(dr["PCountryName"]));
                                    if (pcountryId == 0)
                                    {
                                        strErrMsg.Append("Invalid Country Name data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                        rowVerifyStatus = false;
                                    }
                                }
                                if (!string.IsNullOrEmpty(Convert.ToString(dr["DepartmentName"])))
                                {
                                    departmentId = utilityBL.GetIdByDepartmentName(Convert.ToString(dr["DepartmentName"]));
                                    if (departmentId == 0)
                                    {
                                        strErrMsg.Append("Invalid Department Name data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                        rowVerifyStatus = false;
                                    }
                                }
                                if (!string.IsNullOrEmpty(Convert.ToString(dr["DesignationName"])))
                                {
                                    designationId = utilityBL.GetIdByDesignationName(Convert.ToString(dr["DesignationName"]));
                                    if (designationId == 0)
                                    {
                                        strErrMsg.Append("Invalid Designation Name data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                        rowVerifyStatus = false;
                                    }
                                }

                                if (rowVerifyStatus == true)
                                {
                                    //End of code to get Id from Name data in excel//

                                    employeeViewModel.EmployeeCode = Convert.ToString(dr["EmployeeCode"]);
                                    employeeViewModel.FirstName = Convert.ToString(dr["FirstName"]);
                                    employeeViewModel.LastName = Convert.ToString(dr["LastName"]);
                                    employeeViewModel.FatherSpouseName = Convert.ToString(dr["FatherSpouseName"]);

                                    employeeViewModel.Gender = Convert.ToString(dr["Gender"]);
                                    employeeViewModel.DateOfBirth = Convert.ToString(dr["DateOfBirth"]);
                                    employeeViewModel.MaritalStatus = Convert.ToString(dr["MaritalStatus"]);
                                    employeeViewModel.BloodGroup = Convert.ToString(dr["BloodGroup"]);


                                    employeeViewModel.Email = Convert.ToString(dr["Email"]);
                                    employeeViewModel.AlternateEmail = Convert.ToString(dr["AlternateEmail"]);

                                    employeeViewModel.ContactNo = Convert.ToString(dr["ContactNo"]);
                                    employeeViewModel.AlternateContactno = Convert.ToString(dr["AlternateContactno"]);
                                    employeeViewModel.MobileNo = Convert.ToString(dr["MobileNo"]);

                                    employeeViewModel.CAddress = Convert.ToString(dr["CAddress"]);
                                    employeeViewModel.CCity = Convert.ToString(dr["CCity"]);
                                    employeeViewModel.CStateId = cstateId;
                                    employeeViewModel.CCountryId = ccountryId;
                                    employeeViewModel.CPinCode = Convert.ToString(dr["CPinCode"]);

                                    employeeViewModel.PAddress = Convert.ToString(dr["PAddress"]);
                                    employeeViewModel.CCity = Convert.ToString(dr["PCity"]);
                                    employeeViewModel.PStateId = pstateId;
                                    employeeViewModel.PCountryId = pcountryId;
                                    employeeViewModel.PPinCode = Convert.ToString(dr["PPinCode"]);


                                    employeeViewModel.DateOfJoin = Convert.ToString(dr["DateOfJoin"]);
                                    employeeViewModel.DateOfLeave = Convert.ToString(dr["DateOfLeave"]);
                                    employeeViewModel.PANNo = Convert.ToString(dr["PANNo"]);

                                    employeeViewModel.AadharNo = Convert.ToString(dr["AadharNo"]);
                                    employeeViewModel.BankDetail = Convert.ToString(dr["BankDetail"]);
                                    employeeViewModel.BankAccountNo = Convert.ToString(dr["BankAccountNo"]);
                                    employeeViewModel.PANNo = Convert.ToString(dr["PANNo"]);

                                    employeeViewModel.PFApplicable = Convert.ToBoolean(dr["PFApplicable"]);
                                    employeeViewModel.PFNo = Convert.ToString(dr["PFNo"]);
                                    employeeViewModel.ESIApplicable = Convert.ToBoolean(dr["ESIApplicable"]);
                                    employeeViewModel.ESINo = Convert.ToString(dr["ESINo"]);

                                    employeeViewModel.CompanyId = ContextUser.CompanyId;
                                    employeeViewModel.CreatedBy = ContextUser.UserId;
                                    employeeViewModel.Emp_Status = true;

                                    employeeViewModel.CreatedBy = ContextUser.UserId;
                                    ResponseOut responseOut = employeeBL.ImportEmployee(employeeViewModel);
                                    if (responseOut.status == ActionStatus.Fail)
                                    {
                                        strErrMsg.Append(responseOut.message + ": Error in Inserting in Row #" + rowCounter.ToString() + Environment.NewLine);
                                        dr["UploadStatus"] = false;
                                    }
                                    else
                                    {
                                        dr["UploadStatus"] = true;
                                    }
                                }
                                else
                                {
                                    dr["UploadStatus"] = false;
                                }
                                rowCounter += 1;

                            }
                            dt.AcceptChanges();
                        }
                        else
                        {
                            strErrMsg.Append("Import not found");
                        }

                        ViewBag.Error = strErrMsg.ToString();
                        // End of Code to Update ID based on Name of field

                    }
                    else
                    {
                        ViewBag.Error = "Please Upload Files in .xls, .xlsx or .csv format";
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return View();
        }

        #endregion

        #region Import Product SubGroup
        [ValidateRequest(true, UserInterfaceHelper.ImportProductSubGroup, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        [HttpGet]
        public ActionResult ImportProductSubGroup()
        {
            try
            {


            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return View();
        }
        [ValidateRequest(true, UserInterfaceHelper.ImportProductSubGroup, (int)AccessMode.AddAccess, (int)RequestMode.GetPost)]
        [ActionName("ImportProductSubGroup")]
        [HttpPost]
        public ActionResult ImportProductSubGroupData()
        {
            UploadUtilityBL utilityBL = new UploadUtilityBL();
            ProductSubGroupBL productSubGroupBL = new ProductSubGroupBL();
            try
            {
                if (Request.Files["FileUpload1"].ContentLength > 0)
                {
                    string extension = System.IO.Path.GetExtension(Request.Files["FileUpload1"].FileName).ToLower();
                    string query = null;
                    string connString = "";
                    string[] validFileTypes = { ".xls", ".xlsx", ".csv" };

                    string path1 = string.Format("{0}/{1}", Server.MapPath("~/Content/Uploads"), Request.Files["FileUpload1"].FileName);
                    if (!Directory.Exists(path1))
                    {
                        Directory.CreateDirectory(Server.MapPath("~/Content/Uploads"));
                    }
                    if (validFileTypes.Contains(extension))
                    {
                        DataTable dt = new DataTable();
                        if (System.IO.File.Exists(path1))
                        { System.IO.File.Delete(path1); }
                        Request.Files["FileUpload1"].SaveAs(path1);
                        if (extension == ".csv")
                        {
                            dt = CommonHelper.ConvertCSVtoDataTable(path1);
                            ViewBag.Data = dt;
                        }
                        //Connection String to Excel Workbook  
                        else if (extension.Trim() == ".xls")
                        {
                            connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path1 + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                            dt = CommonHelper.ConvertXSLXtoDataTable(path1, connString);
                            ViewBag.Data = dt;
                        }
                        else if (extension.Trim() == ".xlsx")
                        {
                            connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path1 + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                            dt = CommonHelper.ConvertXSLXtoDataTable(path1, connString);
                            ViewBag.Data = dt;
                        }

                        // Code to Update ID based on Name of field
                        StringBuilder strErrMsg = new StringBuilder(" ");
                        if (dt.Rows.Count > 0)
                        {


                            int rowCounter = 1;
                            int productMainGroupID = 0;

                            ProductSubGroupViewModel productSubGroupViewModel;
                            dt.Columns.Add("UploadStatus", typeof(bool));
                            bool rowVerifyStatus = true;

                            foreach (DataRow dr in dt.Rows)
                            {
                                productSubGroupViewModel = new ProductSubGroupViewModel();

                                //code to validate data in excel//
                                if (string.IsNullOrEmpty(Convert.ToString(dr["ProductSubGroupName"])))
                                {
                                    strErrMsg.Append("Product SubGroup Name Column has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                    rowVerifyStatus = false;
                                }


                                else if (string.IsNullOrEmpty(Convert.ToString(dr["ProductSubGroupCode"])))
                                {
                                    strErrMsg.Append("Product SubGroup Code Column has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                    rowVerifyStatus = false;
                                }
                                else if (string.IsNullOrEmpty(Convert.ToString(dr["ProductSubGroupCode"])))
                                {
                                    strErrMsg.Append("Product SubGroup Code Column has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                    rowVerifyStatus = false;
                                }
                                //end of code to validate data in excel//

                                //code to get Id from Name data in excel//
                                if (!string.IsNullOrEmpty(Convert.ToString(dr["ProductMainGroupName"])))
                                {
                                    productMainGroupID = utilityBL.GetIdByProductMainGroupName(Convert.ToString(dr["ProductMainGroupName"]));
                                    if (productMainGroupID == 0)
                                    {
                                        strErrMsg.Append("Invalid Product MainGroup Name data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                        rowVerifyStatus = false;
                                    }
                                }

                                if (rowVerifyStatus == true)
                                {
                                    //End of code to get Id from Name data in excel//
                                    productSubGroupViewModel.ProductSubGroupName = Convert.ToString(dr["ProductSubGroupName"]);
                                    productSubGroupViewModel.ProductSubGroupCode = Convert.ToString(dr["ProductSubGroupCode"]);
                                    productSubGroupViewModel.ProductMainGroupId = productMainGroupID;
                                    productSubGroupViewModel.ProductSubGroup_Status = true;
                                    ResponseOut responseOut = productSubGroupBL.ImportProductSubGroup(productSubGroupViewModel);
                                    if (responseOut.status == ActionStatus.Fail)
                                    {
                                        strErrMsg.Append(responseOut.message + ": Error in Inserting in Row #" + rowCounter.ToString() + Environment.NewLine);
                                        dr["UploadStatus"] = false;
                                    }
                                    else
                                    {
                                        dr["UploadStatus"] = true;
                                    }
                                }
                                else
                                {
                                    dr["UploadStatus"] = false;
                                }
                                rowCounter += 1;

                            }
                            dt.AcceptChanges();
                        }
                        else
                        {
                            strErrMsg.Append("Import not found");
                        }

                        ViewBag.Error = strErrMsg.ToString();
                        // End of Code to Update ID based on Name of field

                    }
                    else
                    {
                        ViewBag.Error = "Please Upload Files in .xls, .xlsx or .csv format";
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return View();
        }

        #endregion

        #region Import Product
        [ValidateRequest(true, UserInterfaceHelper.ImportProduct, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        [HttpGet]
        public ActionResult ImportProduct()
        {
            try
            {


            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return View();
        }
        [ValidateRequest(true, UserInterfaceHelper.ImportProduct, (int)AccessMode.AddAccess, (int)RequestMode.GetPost)]
        [ActionName("ImportProduct")]
        [HttpPost]
        public ActionResult ImportProductData()
        {
            UploadUtilityBL utilityBL = new UploadUtilityBL();
            ProductBL productBL = new ProductBL();
            try
            {
                if (Request.Files["FileUpload1"].ContentLength > 0)
                {
                    string extension = System.IO.Path.GetExtension(Request.Files["FileUpload1"].FileName).ToLower();
                    string connString = "";
                    string[] validFileTypes = { ".xls", ".xlsx", ".csv" };

                    string path1 = string.Format("{0}/{1}", Server.MapPath("~/Content/Uploads"), Request.Files["FileUpload1"].FileName);
                    if (!Directory.Exists(path1))
                    {
                        Directory.CreateDirectory(Server.MapPath("~/Content/Uploads"));
                    }
                    if (validFileTypes.Contains(extension))
                    {
                        DataTable dt = new DataTable();
                        if (System.IO.File.Exists(path1))
                        { System.IO.File.Delete(path1); }
                        Request.Files["FileUpload1"].SaveAs(path1);
                        if (extension == ".csv")
                        {
                            dt = CommonHelper.ConvertCSVtoDataTable(path1);
                            ViewBag.Data = dt;
                        }
                        //Connection String to Excel Workbook  
                        else if (extension.Trim() == ".xls")
                        {
                            connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path1 + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                            dt = CommonHelper.ConvertXSLXtoDataTable(path1, connString);
                            ViewBag.Data = dt;
                        }
                        else if (extension.Trim() == ".xlsx")
                        {
                            connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path1 + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                            dt = CommonHelper.ConvertXSLXtoDataTable(path1, connString);
                            ViewBag.Data = dt;
                        }

                        // Code to Update ID based on Name of field
                        StringBuilder strErrMsg = new StringBuilder(" ");
                        if (dt.Rows.Count > 0)
                        {

                            int UOMId = 0;
                            int saleprice = 0;
                            int productTypeId = 0;
                            int rowCounter = 1;
                            int productMainGroupID = 0;
                            int productSubGroupID = 0;
                            ProductViewModel productViewModel;
                            dt.Columns.Add("UploadStatus", typeof(bool));
                            bool rowVerifyStatus = true;
                            Random rnd = new Random(50000);
                            foreach (DataRow dr in dt.Rows)
                            {
                                productViewModel = new ProductViewModel();
                                //code to validate data in excel//
                                if (string.IsNullOrEmpty(Convert.ToString(dr["ProductName"])))
                                {
                                    strErrMsg.Append("Product Name Column has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                    rowVerifyStatus = false;
                                }
                                else if (string.IsNullOrEmpty(Convert.ToString(dr["ProductCode"])))
                                {
                                    strErrMsg.Append("Product Code Column has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                    rowVerifyStatus = false;
                                }

                                else if (string.IsNullOrEmpty(Convert.ToString(dr["ProductShortDesc"])))
                                {
                                    strErrMsg.Append("Product Short Desc has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                    rowVerifyStatus = false;
                                }
                                else if (string.IsNullOrEmpty(Convert.ToString(dr["ProductFullDesc"])))
                                {
                                    strErrMsg.Append("ProductFullDesc Column has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                    rowVerifyStatus = false;
                                }
                                else if (string.IsNullOrEmpty(Convert.ToString(dr["AssemblyType"])))
                                {
                                    strErrMsg.Append("AssemblyType Column has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                    rowVerifyStatus = false;
                                }
                                else if (string.IsNullOrEmpty(Convert.ToString(dr["PurchasePrice"])))
                                {
                                    strErrMsg.Append("Purchase Price Column has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                    rowVerifyStatus = false;
                                }
                                else if (string.IsNullOrEmpty(Convert.ToString(dr["SalePrice"])))
                                {
                                    strErrMsg.Append("Sale Price Column has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                    rowVerifyStatus = false;
                                }
                                else if (string.IsNullOrEmpty(Convert.ToString(dr["LocalTaxRate"])))
                                {
                                    strErrMsg.Append("Local Tax Rate has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                    rowVerifyStatus = false;
                                }
                                else if (string.IsNullOrEmpty(Convert.ToString(dr["CentralTaxRate"])))
                                {
                                    strErrMsg.Append("Central Tax Rate Column has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                    rowVerifyStatus = false;
                                }
                                else if (string.IsNullOrEmpty(Convert.ToString(dr["OtherTaxRate"])))
                                {
                                    strErrMsg.Append("Other Tax Rate Column has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                    rowVerifyStatus = false;

                                }
                                else if (string.IsNullOrEmpty(Convert.ToString(dr["IsSerializedProduct"])))
                                {
                                    strErrMsg.Append("IsSerialized Product Column has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                    rowVerifyStatus = false;
                                }
                                else if (string.IsNullOrEmpty(Convert.ToString(dr["BrandName"])))
                                {
                                    strErrMsg.Append("Brand Name has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                    rowVerifyStatus = false;
                                }
                                else if (string.IsNullOrEmpty(Convert.ToString(dr["ReOrderQty"])))
                                {
                                    strErrMsg.Append("Re-Order Qty has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                    rowVerifyStatus = false;
                                }
                                else if (string.IsNullOrEmpty(Convert.ToString(dr["MinOrderQty"])))
                                {
                                    strErrMsg.Append("Min Order Qty has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                    rowVerifyStatus = false;
                                }

                                //end of code to validate data in excel//

                                //code to get Id from Name data in excel//            

                                if (!string.IsNullOrEmpty(Convert.ToString(dr["ProductTypeName"])))
                                {
                                    productTypeId = utilityBL.GetIdByProductTypeName(Convert.ToString(dr["ProductTypeName"]));
                                    if (productTypeId == 0)
                                    {
                                        strErrMsg.Append("Invalid Product Type Name data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                        rowVerifyStatus = false;
                                    }
                                }
                                if (!string.IsNullOrEmpty(Convert.ToString(dr["ProductMainGroupName"])))
                                {
                                    productMainGroupID = utilityBL.GetIdByProductMainGroupName(Convert.ToString(dr["ProductMainGroupName"]));
                                    if (productMainGroupID == 0)
                                    {
                                        strErrMsg.Append("Invalid Product MainGroup Name data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                        rowVerifyStatus = false;
                                    }
                                }
                                if (!string.IsNullOrEmpty(Convert.ToString(dr["ProductSubGroupName"])))
                                {
                                    productSubGroupID = utilityBL.GetIdByProductSubGroupName(Convert.ToString(dr["ProductSubGroupName"]));
                                    if (productSubGroupID == 0)
                                    {
                                        strErrMsg.Append("Invalid Product SubGroup Name data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                        rowVerifyStatus = false;
                                    }
                                }
                                if (!string.IsNullOrEmpty(Convert.ToString(dr["UOMName"])))
                                {
                                    UOMId = utilityBL.GetIdByUOMName(Convert.ToString(dr["UOMName"]));
                                    if (UOMId == 0)
                                    {
                                        strErrMsg.Append("Invalid UOM Name data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                        rowVerifyStatus = false;
                                    }
                                }
                                if (rowVerifyStatus == true)
                                {
                                    //End of code to get Id from Name data in excel//

                                    productViewModel.ProductName = Convert.ToString(dr["ProductName"]);
                                    productViewModel.ProductCode = Convert.ToString(dr["ProductCode"]);
                                    productViewModel.ProductShortDesc = Convert.ToString(dr["ProductShortDesc"]);
                                    productViewModel.ProductFullDesc = Convert.ToString(dr["ProductFullDesc"]);
                                    productViewModel.CompanyId = ContextUser.CompanyId;
                                    productViewModel.ProductTypeId = productTypeId;
                                    productViewModel.ProductMainGroupId = productMainGroupID;
                                    productViewModel.ProductSubGroupId = productSubGroupID;
                                    productViewModel.AssemblyType = Convert.ToString(dr["AssemblyType"]);
                                    productViewModel.UOMId = UOMId;
                                    productViewModel.PurchasePrice = Convert.ToDecimal(dr["PurchasePrice"]);
                                    productViewModel.SalePrice = Convert.ToDecimal(dr["SalePrice"]);
                                    productViewModel.LocalTaxRate = Convert.ToDecimal(dr["LocalTaxRate"]);
                                    productViewModel.CentralTaxRate = Convert.ToDecimal(dr["CentralTaxRate"]);
                                    productViewModel.OtherTaxRate = Convert.ToDecimal(dr["OtherTaxRate"]);
                                    productViewModel.IsSerializedProduct = Convert.ToBoolean(dr["IsSerializedProduct"]);
                                    productViewModel.BrandName = Convert.ToString(dr["BrandName"]);
                                    productViewModel.ReOrderQty = Convert.ToDecimal(dr["ReOrderQty"]);
                                    productViewModel.MinOrderQty = Convert.ToDecimal(dr["MinOrderQty"]);
                                    productViewModel.CreatedBy = ContextUser.UserId;

                                    ResponseOut responseOut = productBL.ImportProduct(productViewModel);
                                    if (responseOut.status == ActionStatus.Fail)
                                    {
                                        strErrMsg.Append(responseOut.message + ": Error in Inserting in Row #" + rowCounter.ToString() + Environment.NewLine);
                                        dr["UploadStatus"] = false;
                                    }
                                    else
                                    {
                                        dr["UploadStatus"] = true;
                                    }
                                }
                                else
                                {
                                    dr["UploadStatus"] = false;
                                }
                                rowCounter += 1;

                            }
                            dt.AcceptChanges();
                        }
                        else
                        {
                            strErrMsg.Append("Import not found");
                        }

                        ViewBag.Error = strErrMsg.ToString();
                        // End of Code to Update ID based on Name of field

                    }
                    else
                    {
                        ViewBag.Error = "Please Upload Files in .xls, .xlsx or .csv format";
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return View();
        }

        #endregion

        #region Import Vendor   
        [ValidateRequest(true, UserInterfaceHelper.ImportVendor, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        [HttpGet]
        public ActionResult ImportVendor()
        {
            try
            {


            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return View();
        }
        [ValidateRequest(true, UserInterfaceHelper.ImportVendor, (int)AccessMode.AddAccess, (int)RequestMode.GetPost)]
        [ActionName("ImportVendor")]
        [HttpPost]
        public ActionResult ImportVendorData()
        {
            UploadUtilityBL utilityBL = new UploadUtilityBL();
            VendorBL vendorBL = new VendorBL();
            try
            {
                if (Request.Files["FileUpload1"].ContentLength > 0)
                {
                    string extension = System.IO.Path.GetExtension(Request.Files["FileUpload1"].FileName).ToLower();
                    string connString = "";
                    string[] validFileTypes = { ".xls", ".xlsx", ".csv" };

                    string path1 = string.Format("{0}/{1}", Server.MapPath("~/Content/Uploads"), Request.Files["FileUpload1"].FileName);
                    if (!Directory.Exists(path1))
                    {
                        Directory.CreateDirectory(Server.MapPath("~/Content/Uploads"));
                    }
                    if (validFileTypes.Contains(extension))
                    {
                        DataTable dt = new DataTable();
                        if (System.IO.File.Exists(path1))
                        { System.IO.File.Delete(path1); }
                        Request.Files["FileUpload1"].SaveAs(path1);
                        if (extension == ".csv")
                        {
                            dt = CommonHelper.ConvertCSVtoDataTable(path1);
                            ViewBag.Data = dt;
                        }
                        //Connection String to Excel Workbook  
                        else if (extension.Trim() == ".xls")
                        {
                            connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path1 + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                            dt = CommonHelper.ConvertXSLXtoDataTable(path1, connString);
                            ViewBag.Data = dt;
                        }
                        else if (extension.Trim() == ".xlsx")
                        {
                            connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path1 + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                            dt = CommonHelper.ConvertXSLXtoDataTable(path1, connString);
                            ViewBag.Data = dt;
                        }

                        // Code to Update ID based on Name of field
                        StringBuilder strErrMsg = new StringBuilder(" ");
                        if (dt.Rows.Count > 0)
                        {
                            int stateId = 0;
                            int countryId = 0;
                            int creditDays = 0;
                            int creditLimit = 0;
                            int rowCounter = 1;
                           
                            VendorViewModel vendorViewModel;
                            dt.Columns.Add("UploadStatus", typeof(bool));
                            bool rowVerifyStatus = true;
                            Random rnd = new Random(50000);
                            foreach (DataRow dr in dt.Rows)
                            {
                                vendorViewModel = new VendorViewModel();
                                //code to validate data in excel//
                                if (string.IsNullOrEmpty(Convert.ToString(dr["VendorName"])))
                                {
                                    strErrMsg.Append("Vendor Name Column has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                    rowVerifyStatus = false;
                                }
                                else if (string.IsNullOrEmpty(Convert.ToString(dr["VendorCode"])))
                                {
                                    strErrMsg.Append("Vendor Code Column has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                    rowVerifyStatus = false;
                                }

                                else if (string.IsNullOrEmpty(Convert.ToString(dr["ContactPersonName"])))
                                {
                                    strErrMsg.Append("Contact Person Name has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                    rowVerifyStatus = false;
                                }
                                //else if (string.IsNullOrEmpty(Convert.ToString(dr["Designation"])))
                                //{
                                //    strErrMsg.Append("Designation Column has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                //    rowVerifyStatus = false;
                                //}
                                else if (string.IsNullOrEmpty(Convert.ToString(dr["Email"])))
                                {
                                    strErrMsg.Append("Email Column has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                    rowVerifyStatus = false;
                                }
                                else if (string.IsNullOrEmpty(Convert.ToString(dr["MobileNo"])))
                                {
                                    strErrMsg.Append("Mobile No. Column has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                    rowVerifyStatus = false;
                                }
                                else if (string.IsNullOrEmpty(Convert.ToString(dr["ContactNo"])))
                                {
                                    strErrMsg.Append("Contact No. Column has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                    rowVerifyStatus = false;
                                }
                                else if (string.IsNullOrEmpty(Convert.ToString(dr["Fax"])))
                                {
                                    strErrMsg.Append("Fax Column has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                    rowVerifyStatus = false;
                                }
                                else if (string.IsNullOrEmpty(Convert.ToString(dr["Address"])))
                                {
                                    strErrMsg.Append("Primary Address Column has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                    rowVerifyStatus = false;
                                }
                                else if (string.IsNullOrEmpty(Convert.ToString(dr["City"])))
                                {
                                    strErrMsg.Append("City Name Column has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                    rowVerifyStatus = false;

                                }
                                else if (string.IsNullOrEmpty(Convert.ToString(dr["PinCode"])))
                                {
                                    strErrMsg.Append("Pin Code Column has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                    rowVerifyStatus = false;
                                }
                                else if (string.IsNullOrEmpty(Convert.ToString(dr["CSTNo"])))
                                {
                                    strErrMsg.Append("CST No has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                    rowVerifyStatus = false;
                                }
                                else if (string.IsNullOrEmpty(Convert.ToString(dr["TINNo"])))
                                {
                                    strErrMsg.Append("TIN No. has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                    rowVerifyStatus = false;
                                }
                                else if (string.IsNullOrEmpty(Convert.ToString(dr["PANNo"])))
                                {
                                    strErrMsg.Append("PAN No. has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                    rowVerifyStatus = false;
                                }
                                else if (string.IsNullOrEmpty(Convert.ToString(dr["GSTNo"])))
                                {
                                    strErrMsg.Append("GST No. Column has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                    rowVerifyStatus = false;
                                }
                                else if (string.IsNullOrEmpty(Convert.ToString(dr["ExciseNo"])))
                                {
                                    strErrMsg.Append("Excise No. Column has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                    rowVerifyStatus = false;
                                }
                                else if (string.IsNullOrEmpty(Convert.ToString(dr["CreditLimit"])) || int.TryParse(Convert.ToString(dr["CreditLimit"]), out creditLimit) == false)
                                {
                                    strErrMsg.Append("Credit Limit Column has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                    rowVerifyStatus = false;
                                }
                                else if (string.IsNullOrEmpty(Convert.ToString(dr["CreditDays"])) || int.TryParse(Convert.ToString(dr["CreditDays"]), out creditDays) == false)
                                {
                                    strErrMsg.Append("Credit Days Column has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                    rowVerifyStatus = false;
                                }

                                //end of code to validate data in excel//

                                //code to get Id from Name data in excel//

                                if (!string.IsNullOrEmpty(Convert.ToString(dr["StateName"])))
                                {
                                    stateId = utilityBL.GetIdByStateName(Convert.ToString(dr["StateName"]));
                                    if (stateId == 0)
                                    {
                                        strErrMsg.Append("Invalid State Name data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                        rowVerifyStatus = false;
                                    }
                                }

                                if (!string.IsNullOrEmpty(Convert.ToString(dr["CountryName"])))
                                {
                                    countryId = utilityBL.GetIdByCountryName(Convert.ToString(dr["CountryName"]));
                                    if (countryId == 0)
                                    {
                                        strErrMsg.Append("Invalid Country Name data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                        rowVerifyStatus = false;
                                    }
                                }

                                if (rowVerifyStatus == true)
                                {
                                    //End of code to get Id from Name data in excel//

                                    vendorViewModel.VendorCode = Convert.ToString(dr["VendorCode"]);
                                    vendorViewModel.VendorName = Convert.ToString(dr["VendorName"]);
                                    vendorViewModel.ContactPersonName = Convert.ToString(dr["ContactPersonName"]);

                                    vendorViewModel.Email = Convert.ToString(dr["Email"]);
                                    vendorViewModel.MobileNo = Convert.ToString(dr["MobileNo"]);
                                    vendorViewModel.ContactNo = Convert.ToString(dr["ContactNo"]);
                                    vendorViewModel.Fax = Convert.ToString(dr["Fax"]);
                                    vendorViewModel.Address = Convert.ToString(dr["Address"]);
                                    vendorViewModel.City = Convert.ToString(dr["City"]);
                                    vendorViewModel.StateId = stateId;
                                    vendorViewModel.CountryId = countryId;
                                    vendorViewModel.PinCode = Convert.ToString(dr["PinCode"]);
                                    vendorViewModel.CSTNo = Convert.ToString(dr["CSTNo"]);
                                    vendorViewModel.TINNo = Convert.ToString(dr["TINNo"]);
                                    vendorViewModel.PANNo = Convert.ToString(dr["PANNo"]);
                                    vendorViewModel.ExciseNo = Convert.ToString(dr["ExciseNo"]);
                                    vendorViewModel.CreditLimit = creditLimit;
                                    vendorViewModel.CreditDays = creditDays;
                                    vendorViewModel.CompanyId = ContextUser.CompanyId;
                                    vendorViewModel.CreatedBy = ContextUser.UserId;
                                    vendorViewModel.Vendor_Status = true;

                                    vendorViewModel.CreatedBy = ContextUser.UserId;
                                    ResponseOut responseOut = vendorBL.ImportVendor(vendorViewModel);
                                    if (responseOut.status == ActionStatus.Fail)
                                    {
                                        strErrMsg.Append(responseOut.message + ": Error in Inserting in Row #" + rowCounter.ToString() + Environment.NewLine);
                                        dr["UploadStatus"] = false;
                                    }
                                    else
                                    {
                                        dr["UploadStatus"] = true;
                                    }
                                }
                                else
                                {
                                    dr["UploadStatus"] = false;
                                }
                                rowCounter += 1;

                            }
                            dt.AcceptChanges();
                        }
                        else
                        {
                            strErrMsg.Append("Import not found");
                        }

                        ViewBag.Error = strErrMsg.ToString();
                        // End of Code to Update ID based on Name of field

                    }
                    else
                    {
                        ViewBag.Error = "Please Upload Files in .xls, .xlsx or .csv format";
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return View();
        }

        #endregion

    }
}

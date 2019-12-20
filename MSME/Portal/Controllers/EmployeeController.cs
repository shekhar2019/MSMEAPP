using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Portal.Core;
using Portal.Core.ViewModel;
using Portal.Common;
using System.Reflection;


namespace Portal.Controllers
{
    public class EmployeeController : BaseController
    {
        //
        // GET: /Employee/
        #region Employee
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_Employee, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult AddEditEmployee(int employeeId = 0, int accessMode = 3)
        {
            try
            {
                if (employeeId != 0)
                {
                    ViewData["employeeId"] = employeeId;
                    ViewData["accessMode"] = accessMode;
                }
                else
                {
                    ViewData["employeeId"] = 0;
                    ViewData["accessMode"] = 3;
                }

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return View();
        }
        [HttpPost]
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_Employee, (int)AccessMode.AddAccess, (int)RequestMode.Ajax)]
        public ActionResult AddEditEmployee(EmployeeViewModel employeeViewModel)
        {
            ResponseOut responseOut = new ResponseOut();
            EmployeeBL employeeBL = new EmployeeBL();
            try
            {
                if (employeeViewModel != null)
                {
                    employeeViewModel.CreatedBy = ContextUser.UserId;
                    employeeViewModel.CompanyId = ContextUser.CompanyId;
                    responseOut = employeeBL.AddEditEmployee(employeeViewModel);
                }
                else
                {
                    responseOut.message = ActionMessage.ProbleminData;
                    responseOut.status = ActionStatus.Fail;
                }

            }
            catch (Exception ex)
            {
                responseOut.message = ActionMessage.ApplicationException;
                responseOut.status = ActionStatus.Fail;
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(responseOut, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_Employee, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult ListEmployee()
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
        [HttpGet]
        public PartialViewResult GetEmployeeList(string employeeName = "", string employeeCode = "", string mobileNo="",string email="",string panNo="",int departmentId=0,string employeeType="0",string currentStatus="0",  string employeeStatus = "")
        {
            List<EmployeeViewModel> employees = new List<EmployeeViewModel>();
            EmployeeBL employeeBL = new EmployeeBL();
            try
            {
                employees = employeeBL.GetEmployeeList(employeeName, employeeCode, mobileNo, email, panNo, departmentId, employeeType, currentStatus, ContextUser.CompanyId, employeeStatus);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(employees);
        }

        [HttpGet]
        public JsonResult GetEmployeeDetail(int employeeId)
        {
            EmployeeBL employeeBL = new EmployeeBL();
            EmployeeViewModel employee = new EmployeeViewModel();
            try
            {
                employee = employeeBL.GetEmployeeDetail(employeeId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(employee, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetDepartmentList()
        {
            DepartmentBL departmentBL = new DepartmentBL();
            List<DepartmentViewModel> department = new List<DepartmentViewModel>();
            try
            {

                department = departmentBL.GetDepartmentList(ContextUser.CompanyId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(department, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetDesignationList(int departmentId)
        {
            DesignationBL designationBL = new DesignationBL();
            List<DesignationViewModel> designations = new List<DesignationViewModel>();
            try
            {

                designations = designationBL.GetDesignationList(departmentId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(designations, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetEmployeeAutoCompleteList(string term,int departmentId=0,int designationId=0)
        {
            EmployeeBL employeeBL = new EmployeeBL();

            List<EmployeeViewModel> employeeList = new List<EmployeeViewModel>();
            try
            {
                employeeList = employeeBL.GetEmployeeAutoCompleteList(term, departmentId, designationId);

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(employeeList, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}

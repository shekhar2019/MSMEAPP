﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Core.ViewModel
{
   public class EmployeeViewModel
    {

        public int EmployeeId { get; set; }
        public string EmployeeCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherSpouseName { get; set; }
        public string Gender { get; set; }
        public string DateOfBirth { get; set; }
        public string MaritalStatus { get; set; }
        public string BloodGroup { get; set; }
        public string Email { get; set; }
        public string AlternateEmail { get; set; }
        public string ContactNo { get; set; }
        public string AlternateContactno { get; set; }
        public string MobileNo { get; set; }
        public string CAddress { get; set; }
        public string CCity { get; set; }
        public int CStateId { get; set; }
        public string CStateName { get; set; }
        public int CCountryId { get; set; }
        public string CCountryName { get; set; }
        public string CPinCode { get; set; }
        public string PAddress { get; set; }
        public string PCity { get; set; }
        public int PStateId { get; set; }
        public string PStateName { get; set; }
        public int PCountryId { get; set; }
        public string PCountryName { get; set; }
        public string PPinCode { get; set; }
        public string DateOfJoin { get; set; }
        public string DateOfLeave { get; set; }
        public string PANNo { get; set; }
        public string AadharNo { get; set; }
        public string BankDetail { get; set; }
        public string BankAccountNo { get; set; }
        public bool PFApplicable { get; set; }
        public string PFNo { get; set; }
        public bool ESIApplicable { get; set; }
        public string ESINo { get; set; }
        public int CompanyId { get; set; }
        public string Division { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int DesignationId { get; set; }
        public string DesignationName { get; set; }
        public string EmploymentType { get; set; }
        public string EmployeeCurrentStatus { get; set; }
        public int EmployeeStatusPeriod { get; set; }
        public string EmployeeStatusStartDate { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedByUserName { get; set; }
        public string CreatedDate { get; set; }
        public int Modifiedby { get; set; }
        public string ModifiedByUserName { get; set; }
        public string ModifiedDate { get; set; }
        public bool Emp_Status { get; set; }
        public long PayInfoId { get; set; }
        public decimal OTRate { get; set; }
        public decimal BasicPay { get; set; }
        public decimal HRA { get; set; }
        public decimal ConveyanceAllow { get; set; }
        public decimal SpecialAllow { get; set; }
        public decimal OtherAllow { get; set; }
        public decimal OtherDeduction { get; set; }
        public long EmployeeReportingId { get; set; }
        public int ReportingDepartmentId { get; set; }
        public string ReportingDepartmentName { get; set; }
        public int ReportingDesignationId { get; set; }
        public string ReportingDesignationName { get; set; }
        public int ReportingEmployeeId { get; set; }
        public string ReportingEmployeeName { get; set; }
        public string message { get; set; }
        public string status { get; set; }
        
    }
}

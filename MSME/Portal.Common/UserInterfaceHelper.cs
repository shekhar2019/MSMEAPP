using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Common
{
    public class UserInterfaceHelper
    {
        public const int Add_Edit_Company_CP = 1;
        public const int Add_Edit_User_CP = 2;
        public const int Add_Edit_User_ADMIN = 3;
        public const int Add_Edit_Country_CP = 4;
        public const int Add_Edit_FinYear_CP = 5;
        public const int Add_Edit_State_CP = 6;
        public const int Add_Edit_Role_CP = 7;
        public const int Add_Edit_LeadStatus_CP = 8;
        public const int Add_Edit_LeadSource_CP = 10;
        public const int Add_Edit_ProductType_ADMIN = 12;
        public const int Add_Edit_Product = 13;
        public const int Add_Edit_PaymentTerm_ACCOUNT = 14;
        public const int Add_Edit_Department_HR = 15;
        public const int Add_Edit_ProductMainGroup_INVENTORY = 16;
        public const int Add_Edit_ProductSubGroup_INVENTORY = 18;
        public const int Add_Edit_UOM_CP = 20;
        public const int Add_Edit_Designation_HR = 21;
        public const int Add_Edit_Lead_CRM = 23;
        public const int Add_Edit_Employee_State_Mapping_CRM = 29;
        public const int Add_Edit_City_CP =27;
        public const int Add_Edit_Tax_ACCOUNT = 30; 
        public const int Add_Edit_Book_ACCOUNT = 31;
        public const int Add_Edit_CustomerType_CP = 34;
        public const int Add_Edit_Vendor_SALE = 35;
        public const int Add_Edit_GLMainGroup_ACCOUNT = 36;
        public const int Add_Edit_GLSubGroup_ACCOUNT = 37;
        public const int Add_Edit_QuotationSetting_SALE = 41;
        public const int Add_Edit_TermTemplate_Admin = 42;
        public const int Add_Edit_DocumentType_Admin = 44;
        public const int Add_Edit_SO = 46;
        public const int Add_Edit_SaleInvoice = 47;
        public const int Add_Edit_CustomerPayment= 53;
        public const int Add_Edit_VendorPayment = 54;
        public const int Add_Edit_SOSetting_SALE = 51;
        public const int Add_Edit_DeliveryChallan = 1054;
        public const int Add_RevisedQuotation = 1057;
        public const int Add_RevisedPO = 1061;
        public const int Add_QuotationRegister = 1062;
        public const int Add_PORegister = 1065;
        public const int Add_SORegister = 1066;
        public const int Add_SaleInvoiceRegister = 2062;
        public const int Add_MRNRegister = 2065;
        public const int Add_STNRegister = 2067;
        public const int Add_STRRegister = 2068;
        public const int AddVendor_Payment_Register = 2070;
        public const int Add_SaleInvoiceSummary = 2071;
        public const int Add_PurchaseSummary = 2076;

        public const int Add_DeliveryChallanRegister = 2064;
        public const int Add_PurchaseInvoiceRegister = 2063;
        public const int Add_Edit_Role_UI_Mapping_Admin = 9;
        public const int Add_Edit_Product_Opening_Stock = 17;
        public const int Add_Edit_ProductBOM = 19;
        public const int Add_Edit_Employee = 28;
        public const int Add_Edit_PaymentMode_CP =22;
        public const int Add_Edit_SLType_CP = 24;
        public const int Add_Edit_Services_CP = 25;
        public const int Add_Edit_Schedule_ACCOUNT = 1055;
        public const int Add_Edit_CompanyBranch_ADMIN = 1059;
        public const int Add_Edit_FormType_ADMIN = 1063;
        public const int Add_Edit_CustomerForm = 1067;
        public const int Add_Edit_CostCenter_ACCOUNT = 2079;
        public const int Add_Edit_SL = 2077;

        public const int Add_Edit_Customer = 32;
        public const int Add_Edit_Quotation = 39;
        public const int Add_Edit_PO = 43;
        public const int Add_Edit_PI = 49;
        public const int Add_Edit_ProductTaxMapping = 50;

        public const int Add_Edit_MRN = 1056;

        public const int Add_Edit_STN = 1064;

        public const int Add_Edit_STR = 1068;

        public const int Add_Edit_FollowUpActivityType_CP = 33;

        public const int Print_Stock_Ledger = 1069;

        public const int AddCustomer_Form_Register = 2066;

        public const int AddCustomer_Payment_Register = 2069;

        public const int AddEdit_UserEmailSetting = 2072;

        public const int AddEdit_GL = 2073;

        public const int Add_Edit_VendorForm = 2075;
        public const int Add_Edit_BankVoucher = 2074;
        public const int Add_Edit_SLDetail = 2078;

        public const int ImportCustomer = 2084;
        public const int ImportVendor = 2085;
        public const int ImportCustomerBranch = 2087;
        public const int ImportDepartment = 2088;
        public const int ImportDesignation = 2089;
        public const int ImportEmployee = 2086;

        public const int ImportProduct = 2091;
        public const int ImportProductSubGroup = 2090;
        public const int Add_Edit_LOGO = 2095;
        public const int Add_Edit_WelcomeHome = 2097;
        public const int Add_Edit_HomeSlider = 2096;
        public const int Add_Edit_ContactInfo = 2098;
        public const int Add_Edit_CopyRight = 2100;
        public const int Add_Edit_ButtonTitle = 2102;

        public const int Add_Edit_ContactUs = 2103;
        public const int Add_Edit_HomeUpdate = 2104;

        public const int Add_Edit_Menu = 2105;
        public const int Add_Edit_SubMenu = 2106;
        public const int Add_Edit_SubChildMenu = 2107;

        public const int Add_Edit_ProductSubChildGroup = 2108;

        public const int Add_Edit_MSME = 2110;

        public const int Add_Edit_Buyer_Product = 2111;

        public const int AddEditUOM__INVENTORY = 2114;
    }
}


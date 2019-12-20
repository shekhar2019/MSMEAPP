using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Common
{
    public class ActionMessage
    {
        #region Exception
        public const String ApplicationException = "Error occured in application. Please contact administrator";
        public const String SessionException = "Session Expired!!!";
        public const String AuthenticationException = "User not authenticated";
        public const String AccessDenied = "Access Denied";
        public const String ProbleminData = "Problem in Data";

        #endregion
        #region Login
        public const String LoginSuccess = "Login Success";
        public const String UserNotActive = "User is not active.";
        public const String UserLoginExpired = "User login expired.";
        public const String InvalidCredential = "Invalid User Name/Password";
        #endregion
        #region Company
        public const String CompanyCreatedSuccess = "Company Created Successfully";
        public const String CompanyUpdatedSuccess = "Company Updated Successfully";
        public const String DuplicateCompanyCode = "Company Code already opt out";
        public const String DuplicateCompanyName= "Company already exist with Same Name & Email";
        #endregion
        #region CompanyBranch
        public const String CompanyBranchCreatedSuccess = "Company Branch Created Successfully";
        public const String CompanyBranchUpdatedSuccess = "Company Branch Updated Successfully";

        #endregion


        #region CompanyForm
        public const String CompanyFormCreatedSuccess = "Company Form Created Successfully";
        public const String CompanyFormUpdatedSuccess = "Company Form Updated Successfully";

        #endregion

        #region User
        public const String UserCreatedSuccess = "User Created Successfully";
        public const String UserUpdatedSuccess = "User Updated Successfully";
        public const String DuplicateUsername = "Login Id already opt out";
        public const String PasswordDoesNotMatch = "Password does not match ";
        public const String PasswordChangeSuccessfully = "Password Change Successfully ";
        
        #endregion
        #region FinYear
        public const String FinYearCreatedSuccess = "Financial Year Created Successfully";
        public const String FinYearUpdatedSuccess = "Financial Year Updated Successfully";
        public const String DuplicateFinYear = "Financial Year already exists";
        #endregion


        #region Country
        public const String CountryCreatedSuccess = "Country Created Successfully";
        public const String CountryUpdatedSuccess = "Country Updated Successfully";
        public const String DuplicateCountryCode = "Country Code already opt out";
        public const String DuplicateCountryName = "Country already exist with Same Name";
        #endregion

        #region State
        public const String StateCreatedSuccess = "State Created Successfully";
        public const String StateUpdatedSuccess = "State Updated Successfully";
        public const String DuplicateStateCode = "State Code already opt out";
        public const String DuplicateStateName = "State already exist with Same Name";
        #endregion

        #region City
        public const String CityCreatedSuccess = "City Created Successfully";
        public const String CityUpdatedSuccess = "City Updated Successfully";
        public const String DuplicateCityName = "City already exist with same name";
        #endregion

        #region Role
        public const String RoleCreatedSuccess = "Role Created Successfully";
        public const String RoleUpdatedSuccess = "Role Updated Successfully";
        public const String DuplicateRoleDesc = "Role Code already opt out";
        public const String DuplicateRoleName = "Role already exist with Same Name";
        #endregion

        #region LeadStatus
        public const String LeadStatusCreatedSuccess = "LeadStatus Created Successfully";
        public const String LeadStatusUpdatedSuccess = "LeadStatus Updated Successfully"; 
        public const String DuplicateLeadStatusName = "LeadStatusName already exist with Same Name";
        #endregion

        #region LeadSource
        public const String LeadSourceCreatedSuccess = "LeadSource Created Successfully";
        public const String LeadSourceUpdatedSuccess = "LeadSource Updated Successfully";
        public const String DuplicateLeadSourceName = "LeadSource Name already exist with Same Name";
        #endregion

        #region Role UI Mapping
        public const String RoleUIMappingSuccessful = "Role UI Mapping Updated Successfully";
        #endregion

        #region ProductType
        public const String ProductTypeCreatedSuccess = "ProductType Created Successfully";
        public const String ProductTypeUpdatedSuccess = "ProductType Updated Successfully";
        public const String DuplicateProductTypeName = "ProductType Name already exist with Same Name";
        public const String DuplicateProductTypeCode = "ProductType Code already exist with Same Name";
        #endregion
       

        #region PaymentTerm
        public const String PaymentTermCreatedSuccess = "PaymentTerm Created Successfully";
        public const String PaymentTermUpdatedSuccess = "PaymentTerm Updated Successfully";
        public const String DuplicatePaymentTermDesc = "PaymentTerm Name already exist with Same Name";
        #endregion

        #region Department
        public const String DepartmentCreatedSuccess = "Department Created Successfully";
        public const String DepartmentUpdatedSuccess = "Department Updated Successfully";
        public const String DuplicateDepartmentName= "Department Name already exist with Same Name";
        public const String DuplicateDepartmentCode = "Department Code already exist with Same Name";
        #endregion

        #region Product
        public const String ProductCreatedSuccess = "Product Created Successfully";
        public const String ProductUpdatedSuccess = "Product Updated Successfully";
        public const String DuplicateProductCode = "Product Code already exist";
        #endregion

        #region ProductMainGroup
        public const String ProductMainGroupCreatedSuccess = "Parent Class Created Successfully";
        public const String ProductMainGroupUpdatedSuccess = "Parent Class Updated Successfully";
        public const String DuplicateProductMainGroupName = "Parent Class Code already exist";
        public const String DuplicateProductMainGroupCode = "Parent Class Code already exist";
        #endregion 

        #region ProductSubGroup
        public const String ProductSubGroupCreatedSuccess = "Category Created Successfully";
        public const String ProductSubGroupUpdatedSuccess = "Category Updated Successfully";
        public const String DuplicateProductSubGroupName = "Category Code already exist";
        public const String DuplicateProductSubGroupCode = "Category Code already exist";
        #endregion

        #region UOM
        public const String UOMCreatedSuccess = "UOM Created Successfully";
        public const String UOMUpdatedSuccess = "UOM Updated Successfully";
        public const String DuplicateUOMName = "UOM Name already exist";
        public const String DuplicateUOMDesc = "UOM Desc already exist";
        #endregion

        #region GSTPerc
        public const String GSTCreatedSuccess = "GST Created Successfully";
        public const String GSTUpdatedSuccess = "GST Updated Successfully";
        public const String DuplicateGSTName = "GST Name already exist";
        public const String DuplicateGSTDesc = "GST Desc already exist";
        public const String RemoveGSTPerc = "GST (%) Deleted Successfully";
        #endregion

        #region Designation
        public const String DesignationCreatedSuccess = "Designation Created Successfully";
        public const String DesignationUpdatedSuccess = "Designation Updated Successfully";
        public const String DuplicateDesignationName = "Designation Name already exist with Same Name";
        public const String DuplicateDesignationCode = "Designation Code already exist with Same Name";
        #endregion

        #region Lead
        public const String LeadCreatedSuccess = "Lead Created Successfully";
        public const String LeadUpdatedSuccess = "Lead Updated Successfully";
        public const String DuplicateLeadCode = "Lead Code already opt out";
        public const String DuplicateLead = "Lead Detail already exist"; 
        #endregion
        #region Product Opening Stock
        public const String ProductOpeningCreatedSuccess = "Product Opening Stock Created Successfully";
        public const String ProductOpeningUpdatedSuccess = "Product Opening Stock Updated Successfully";
        public const String DuplicateProductOpening = "Product Opening Stock already exist for selected Financial year";
        #endregion

        #region Product BOM
        public const String ProductBOMCreatedSuccess = "Assembly BOM Created Successfully";
        public const String ProductBOMUpdatedSuccess = "Assembly BOM Updated Successfully";
        public const String DuplicateBOM = "Assembly BOM Already exist with this Assembly and Component";
        public const String ProductBOMCopySuccess = "Assembly BOM Copied Successfully";
        public const String ProductBOMCopyFail = "Assembly BOM Copied Fail";
        #endregion

        #region PaymentMode
        public const String PaymentModeCreatedSuccess = "Payment Mode Created Successfully";
        public const String PaymentModeUpdatedSuccess = "Payment Mode Updated Successfully";
        public const String DuplicatePaymentModeName = "Payment Mode Name already exist with Same Name";
        #endregion

        #region SLType
        public const String SLTypeCreatedSuccess = "SL Type Created Successfully";
        public const String SLTypeUpdatedSuccess = "SL Type Updated Successfully";
        public const String DuplicateSLTypeName = "SL Type Name already exist with Same Name";
        #endregion

        #region Services
        public const String ServicesCreatedSuccess = "Services Created Successfully";
        public const String ServicesUpdatedSuccess = "Services Updated Successfully";
        public const String DuplicateServicesName = "Services Name already exist with Same Name";
        #endregion

        #region Employee State Mapping
        public const String EmployeeStateMappingSuccessful = "Employee State Mapping Updated Successfully";
        #endregion

        #region Employee
        public const String EmployeeCreatedSuccess = "Employee Created Successfully";
        public const String EmployeeUpdatedSuccess = "Employee Updated Successfully";
        public const String DuplicateEmployeeCode = "Employee Code already exist";
        public const String DuplicateEmployeeDetail = "Employee with Same Name, Mobile Number already exist";
        #endregion

        #region Tax
        public const String TaxCreatedSuccess = "Tax Created Successfully";
        public const String TaxUpdatedSuccess = "Tax Updated Successfully";
        public const String DuplicateTaxName = "Tax Name already exist";
        #endregion

        #region Book
        public const String BookCreatedSuccess = "Book Created Successfully";
        public const String BookUpdatedSuccess = "Book Updated Successfully";
        public const String DuplicateBookName = "Book Name already exist";
        public const String DuplicateBookCode = "Book Code already exist";
        public const String DuplicateBookGLCode = "GL Code already used for other Book";
        #endregion

        #region Customer
        public const String CustomerCreatedSuccess = "Party Created Successfully";
        public const String CustomerUpdatedSuccess = "Party Updated Successfully";
        public const String DuplicateCustomerCode = "Party Code already exist";
        public const String DuplicateCustomerDetail = "Party with Same Name, Mobile Number and Type already exist";
        public const String CustomerBranchRemovedSuccess = "Party Branch removed";
        public const String CustomerProductRemovedSuccess = "Party Product removed";
        #endregion

        #region CustomerType
        public const String CustomerTypeCreatedSuccess = "Customer Type Created Successfully";
        public const String CustomerTypeUpdatedSuccess = "Customer Type Updated Successfully";
        public const String DuplicateCustomerTypeDesc = "Customer Type Desc already exist";
        #endregion

        #region Vendor
        public const String VendorCreatedSuccess = "Vendor Created Successfully";
        public const String VendorUpdatedSuccess = "Vendor Updated Successfully";
        public const String DuplicateVendorCode = "Vendor Code already exist";
        public const String DuplicateVendorDetail = "Vendor with Same Name and Mobile Number already exist";
        #endregion


        #region GLMainGroup
        public const String GLMainGroupCreatedSuccess = "GL Main Group Created Successfully";
        public const String GLMainGroupUpdatedSuccess = "GL Main Group Updated Successfully";
        public const String DuplicateGLMainGroupName = "GL Main Group Name already exist";
        public const String DuplicateGLType = "GL Type with Same GLMainGroupName already exist";
        #endregion

        #region Schedule
        public const String ScheduleCreatedSuccess = "Schedule Created Successfully";
        public const String ScheduleUpdatedSuccess = "Schedule Updated Successfully";

        public const String DuplicateScheduleName = "Schedule Name already exist";

        #endregion
        #region Schedule
        public const String FormTypeCreatedSuccess = "Form Type Created Successfully";
        public const String FormTypeUpdatedSuccess = "Form Type Updated Successfully";
        public const String DuplicateFormType = "Form Type Desc already exist";
        public const String DuplicateFormTypeName = "Form Type Name already exist";

        #endregion
        #region GLSubGroup
        public const String GLSubGroupCreatedSuccess = "GL Sub Group Created Successfully";
        public const String GLSubGroupUpdatedSuccess = "GL Sub Group Updated Successfully";
        public const String DuplicateGLSubGroupName = "GL Sub Group Name already exist";
        public const String DuplicateGLMainGroupId = "GL Main Group with Same GL Sub Group Name already exist";
        #endregion


        #region QuotationSetting
        public const String QuotationSettingCreatedSuccess = "Quotation Setting Created Successfully";
        public const String QuotationSettingUpdatedSuccess = "Quotation Setting Updated Successfully";
        public const String QuotationSettingExist = "Active Quotation Setting already exist";
        #endregion

        #region TermTemplate
        public const String TermTemplateCreatedSuccess = "Term Template Created Successfully";
        public const String TermTemplateUpdatedSuccess = "Term Template Updated Successfully";
        public const String TermTemplateRemovedSuccess = "Term Template Created Successfully";
        public const String DuplicateTermTempalteName = "Term Template Name Already Exit"; 
        #endregion



        #region Quotation
        public const String QuotationCreatedSuccess = "Quotation Created Successfully";
        public const String QuotationUpdatedSuccess = "Quotation Updated Successfully";
        public const String RevisedQuotationCreatedSuccess = "Revised Quotation Created Successfully";
        #endregion

        #region PO
        public const String POCreatedSuccess = "Purchase Order Created Successfully";
        public const String POUpdatedSuccess = "Purchase Order Updated Successfully";
        public const String RevisedPOCreatedSuccess = "Revised Purchase Order Created Successfully";
        #endregion

        #region PI
        public const String PICreatedSuccess = "Purchase Invoice Created Successfully";
        public const String PIUpdatedSuccess = "Purchase Invoice Updated Successfully";
        public const String PurchaseInvoiceCancelSuccess = "Purchase Invoice Cancel Successfully";
        #endregion

        #region DocumentType
        public const String DocumentTypeCreatedSuccess = "Document Type Created Successfully";
        public const String DocumentTypeUpdatedSuccess = "Document Type Updated Successfully";
        public const String DuplicateDocumentTypeDesc = "Document Type Desc already exist";
        #endregion

        #region SO
        public const String SOCreatedSuccess = "SO Created Successfully";
        public const String SOUpdatedSuccess = "SO Updated Successfully";
        #endregion

        #region SaleInvoice
        public const String SaleInvoiceCreatedSuccess = "Invoice Created Successfully";
        public const String SaleInvoiceUpdatedSuccess = "Invoice Updated Successfully";
        public const String SaleInvoiceCancelSuccess = "Invoice Cancel Successfully";
        #endregion

        #region SoSetting
        public const String SOSettingCreatedSuccess = "Sale Order Setting Created Successfully";
        public const String SOSettingUpdatedSuccess = "Sale Order Setting Updated Successfully";
        public const String SOSettingExist = "Active Sale Order Setting already exist";
        #endregion


        #region Product State Tax Mapping
        public const String ProductStateTaxMappingCreatedSuccessful = "Product State Tax Mapping Created Successfully";
        public const String ProductStateTaxMappingUpdateSuccessful = "Product State Tax Mapping Updated Successfully";
        public const String DuplicateProductStateTaxMapping = "Product State Tax Mapping already exist with same Product";
        #endregion

        #region Customer Payment
        public const String CustomerPaymentCreatedSuccess = "Customer Payment Created Successfully";
        public const String CustomerPaymentUpdatedSuccess = "Customer Payment Updated Successfully"; 
        public const String DuplicateCustomerPaymentDetail = "Customer with Same Name, Invoice Id and Ref No already exist";
        #endregion
        #region Vendor Payment
        public const String VendorPaymentCreatedSuccess = "Vendor Payment Created Successfully";
        public const String VendorPaymentUpdatedSuccess = "Vendor Payment Updated Successfully";
        public const String DuplicateVendorPaymentDetail = "Vendor with Same Name, Invoice Id and Ref No already exist";
        #endregion

        #region Delivery Challan
        public const String ChallanCreatedSuccess = "Challan Created Successfully";
        public const String ChallanUpdatedSuccess = "Challan Updated Successfully";
        #endregion

        #region MRN
        public const String MRNCreatedSuccess = "MRN Created Successfully";
        public const String MRNUpdatedSuccess = "MRN Updated Successfully";
        #endregion

        #region STN
        public const String STNCreatedSuccess = "STN Created Successfully";
        public const String STNUpdatedSuccess = "STN Updated Successfully";
        #endregion

        #region STR
        public const String STRCreatedSuccess = "STR Created Successfully";
        public const String STRUpdatedSuccess = "STR Updated Successfully";
        #endregion

        #region FollowUp Activity Type
        public const String FollowUpActivityTypeCreatedSuccess = "Follow Up Activity Type Created Successfully";
        public const String FollowUpActivityTypeUpdatedSuccess = "Follow Up Activity Type Updated Successfully";
        public const String DuplicateFollowUpActivityTypeName = "Follow Up Activity Type already exist";
        #endregion

        #region UserEmailSettinh
        public const String UserEmailSettingCreatedSuccess = "User Email Setting Created Successfully";
        public const String UserEmailSettingUpdatedSuccess = "User Email Setting Updated Successfully";
        public const String DuplicateUserEmailSetting = "Email Setting already Created..";
        #endregion

        #region VendorForm
        public const String VendorFormCreatedSuccess = "Vednor Form Created Successfully";
        public const String VendorFormUpdatedSuccess = "Vednor Form Updated Successfully";
        #endregion

        #region CustomerForm
        public const String CustomerFormCreatedSuccess = "Customer Form Created Successfully";
        public const String CustomerFormUpdatedSuccess = "Customer Form Updated Successfully";

        #endregion

        #region GL
        public const String GLCreatedSuccess = "GL Created Successfully";
        public const String GLUpdatedSuccess = "GL Updated Successfully";
        public const String GLDuplicateCode = "GL Code already exist";
        #endregion

        #region CostCenter
        public const String CostCenterCreatedSuccess = "Cost Center Created Successfully";
        public const String CostCenterUpdatedSuccess = "Cost Center Updated Successfully";
        public const String DuplicateCostCenterName = "Cost Center Name already exist";
        public const String DuplicateCostCenter = "Cost Center with Same CostCenterName already exist";
        #endregion




        #region Bank Voucher
        public const String BankVoucherCreatedSuccess = "Bank Voucher Created Successfully";
        public const String BankVoucherUpdatedSuccess = "Bank Voucher Updated Successfully";
        #endregion

        #region SL
        public const String SLCreatedSuccess = "SL Created Successfully";
        public const String SLUpdatedSuccess = "SL Updated Successfully";
        public const String DuplicateSLName = "SL Code Name already exist with Same Name";
        #endregion

        #region SL Detail
        public const String SLDetailSuccessful = "SL Detail Updated Successfully";
        #endregion

        #region Logo
        public const String LogoCreatedSuccess = "Logo Saved Successfully";
        public const String LogoUpdatedSuccess = "Logo Updated Successfully";
        public const String DuplicateLogoTitle = "Logo Title already exist";
        public const String RemoveLogo = "Logo Deleted Successfully";
        #endregion

        #region About Us 
        public const String AboutusCreatedSuccess = "About Us Content Saved Successfully";
        public const String AboutusUpdatedSuccess = "About Us Content Updated Successfully";
        public const String DuplicateAboutus = "About Us Content Title already exist";
        public const String RemoveAboutus = "About Us Content Deleted Successfully";
        #endregion

        #region Welcome Home
        public const String WelcomeHomeCreatedSuccess = "Welcome Content Saved Successfully";
        public const String WelcomeHomeUpdatedSuccess = "Welcome Content Updated Successfully";
        public const String DuplicateWelcomeHome = "Welcome Content Title already exist";
        public const String RemoveWelcomeHome = "Welcome Content Deleted Successfully";
        #endregion

        #region HomeSlider
        public const String HomeSliderCreatedSuccess = "Home Slider Content Saved Successfully";
        public const String HomeSliderUpdatedSuccess = "Home Slider Welcome Content Updated Successfully";
        public const String DuplicateHomeSlider = "Home Slider Content Title already exist";
        public const String RemoveHomeSlider = "Home Slider Content Deleted Successfully";
        #endregion

        #region ContactInfo 
        public const String ContactInfoCreatedSuccess = "Contact Content Saved Successfully";
        public const String ContactInfoUpdatedSuccess = "Contact Content Updated Successfully";
        public const String ContactInfoTitle = "Content Title already exist";
        public const String ContactInfoRemove = "About Us Content Deleted Successfully";
        #endregion

        #region CopyRight 
        public const String CopyRightCreatedSuccess = "Copy Right Saved Successfully";
        public const String CopyRightUpdatedSuccess = "Copy Right Updated Successfully";
        public const String DuplicateCopyRight = "Copy Right Description already exist";
        public const String RemoveCopyRight = "Copy Right  Deleted Successfully";
        #endregion

        #region CopyRight 
        public const String HomeAboutCreatedSuccess = "Home About Saved Successfully";
        public const String HomeAboutUpdatedSuccess = "Home About Updated Successfully";
        public const String HomeAboutCopyRight = "Home About Title already exist";

        #endregion

        #region ButtonTitle 
        public const String ButtonTitleCreatedSuccess = "Button Title Saved Successfully";
        public const String ButtonTitleUpdatedSuccess = "Button Title Updated Successfully";
        public const String ButtonTitleDuplicate = "Button Title already exist";

        #endregion

        #region Home Udate
        public const String HomeUpdatesCreatedSuccess = "Home Updates Content Saved Successfully";
        public const String HomeUpdatesUpdatedSuccess = "Home Updates Content Updated Successfully";
        public const String DuplicateHomeUpdate = "Home Updates Title already exist";
        public const String RemoveHomeUpdate = "Home Updates Deleted Successfully";
        #endregion

        #region Admin Menu
        public const String MenuCreatedSuccess = "Menu Name Saved Successfully";
        public const String MenuUpdatedSuccess = "Menu Name Updated Successfully";
        public const String DuplicateMenu= "Menu Name already exist";
        public const String RemoveMenu = "Menu Deleted Successfully";
        #endregion
      
        #region Admin Sub  Menu
        public const String SubMenuCreatedSuccess = "Sub Menu Name Saved Successfully";
        public const String SubMenuUpdatedSuccess = "Sub Menu Name Updated Successfully";
        public const String SubMenuDuplicate = "Sub Menu Name already exist";
        public const String SubMenuRemove = "Sub Menu Deleted Successfully";
        #endregion

        #region Admin Sub Child Menu
        public const String SubChildMenuCreatedSuccess = "Sub Child Menu Name Saved Successfully";
        public const String SubChildMenuUpdatedSuccess = "Sub Child Menu Name Updated Successfully";
        public const String SubChildMenuDuplicate = "Sub Child Menu Name already exist";
        public const String SubChildMenuRemove = "Sub Child Menu Deleted Successfully";
        #endregion

        #region UserRegistration
        public const String UserRegistrationSuccess = "User Registered Successfully";
        public const String UserRegistrationSuccesswithCode = "Registered Successfully. The registed Code is ";
        public const String UserRegistrationUpdatedSuccess = "User Updated Successfully";
        public const String UserRegistrationDuplicate = "User Name already exist";
        public const String UserRegistrationRemove = "User Deleted Successfully";
        #endregion

        #region Product Sub Child Product
        public const String ProductSubChildGroupSuccess = "Sub Category Saved Successfully";
        public const String ProductSubChildGroupUpdatedSuccess = "Sub Category Updated Successfully";
        public const String ProductSubChildGroupDuplicate = "Sub Category Name already exist";
        public const String ProductSubChildGroupRemove = "Sub Category Deleted Successfully";
        #endregion

        #region About Us 
        public const String MSMECreatedSuccess = "MSME Content Saved Successfully";
        public const String MSMEUpdatedSuccess = "MSME Content Updated Successfully";
        public const String MSMEDuplicate = "MSME Content Title already exist";
        public const String MSMERemove = "MSME Content Deleted Successfully";
        #endregion

        #region Buyer Products

        public const String BuyerProductCreated = "Buyer product created Successfully";

        public const String BuyerProductDeleted = "Buyer product deleted Successfully";

        #endregion

        #region Seller Products

        public const String SellerProductCreated = "Seller product created Successfully";

        public const String SellerProductDeleted = "Seller product deleted Successfully";

        #endregion
    }
}

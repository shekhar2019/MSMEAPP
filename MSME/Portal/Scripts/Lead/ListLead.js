$(document).ready(function () {
    BindStateList();
    SearchLead();
    //$('#tblCompanyList').paging({ limit: 2 });  
    BindLeadSourceList();
    BindLeadStatusList(); 
   
});

$(".alpha-only").on("input", function () {
    var regexp = /[^a-zA-Z]/g;
    if ($(this).val().match(regexp)) {
        $(this).val($(this).val().replace(regexp, ''));
    }
});
$(".alpha-space-only").on("input", function () {
    var regexp = /[^a-zA-Z\s]+$/g;
    if ($(this).val().match(regexp)) {
        $(this).val($(this).val().replace(regexp, ''));
    }
});
$(".numeric-only").on("input", function () {
    var regexp = /\D/g;
    if ($(this).val().match(regexp)) {
        $(this).val($(this).val().replace(regexp, ''));
    }
});
$(".alpha-numeric-only").on("input", function () {
    var regexp = /[^a-zA-Z0-9]/g;
    if ($(this).val().match(regexp)) {
        $(this).val($(this).val().replace(regexp, ''));
    }
});

function ClearFields() {
    $("#hdnLeadId").val("0");
    $("#txtLeadCode").val("");
    $("#txtCompanyName").val("");
    $("#txtContactPersoName").val("");
    $("#txtEmail").val("");
    $("#txtAlternateEmail").val("");
    $("#txtContactNo").val("");
    $("#txtAlternateContactNo").val("");
    $("#txtFax").val("");
    $("#txtDesignation").val("");
    $("#txtAddress").val("");
    $("#txtCompanyCity").val("");
    $("#ddlCountry").val("0");
    $("#ddlState").val("0");
    $("#txtCompanyPinCode").val("");
    $("#ddlLeadSource").val("0");
    $("#txtBranchAddress").val("");
    $("#txtCity").val("");
    $("#ddlBranchState").val("0");
    $("#ddlBranchCountry").val("0");
    $("#txtPinCode").val("");
    $("#txtOtherLeadSourceDescription").val("");
    $("#txtPinCode").val("");
    $("#ddlLeadStatus").val("0");
    $("#ddlStatus").val(""); 
}
 


 
function BindStateList() {
    var data = { countryId: 1 };
   $.ajax({
            type: "GET",
            url: "../Lead/GetStateList",
            data: data,
            asnc: false,
            dataType: "json",
            success: function (data) {
                $("#ddlState").append($("<option></option>").val(0).html("-Select State-"));
                $.each(data, function (i, item) {
                    $("#ddlState").append($("<option></option>").val(item.StateId).html(item.StateName));
                });
                
            },
            error: function (Result) {
                $("#ddlState").append($("<option></option>").val(0).html("-Select State-"));
            }
        });
}
 
function BindLeadSourceList() {
    $.ajax({
        type: "GET",
        url: "../Lead/GetLeadSourceList",
        data: "{}",
        dataType: "json",
        asnc: false,
        success: function (data) {
            $("#ddlLeadSource").append($("<option></option>").val(0).html("-Select Lead Source-"));
            $.each(data, function (i, item) {

                $("#ddlLeadSource").append($("<option></option>").val(item.LeadSourceId).html(item.LeadSourceName));
            });
        },
        error: function (Result) {
            $("#ddlLeadSource").append($("<option></option>").val(0).html("-Lead Source-"));
        }
    });
}


function BindLeadStatusList() {
    $.ajax({
        type: "GET",
        url: "../Lead/GetLeadStatusList",
        data: "{}",
        dataType: "json",
        asnc: false,
        success: function (data) {
            $("#ddlLeadStatus").append($("<option></option>").val(0).html("-Select Lead Status-"));
            $.each(data, function (i, item) {

                $("#ddlLeadStatus").append($("<option></option>").val(item.LeadStatusId).html(item.LeadStatusName));
            });
        },
        error: function (Result) {
            $("#ddlLeadStatus").append($("<option></option>").val(0).html("-Lead Status-"));
        }
    });

}
 
function SearchLead() { 
    var txtLeadCode = $("#txtLeadCode");
    var txtCompanyName = $("#txtCompanyName");
    var txtContactPersoName = $("#txtContactPersoName");
    var txtEmail = $("#txtEmail");
    var txtContactNo = $("#txtContactNo"); 
    var txtAddress = $("#txtAddress");
    var txtCompanyCity = $("#txtCompanyCity");
    var ddlState = $("#ddlState");
    var ddlLeadSource = $("#ddlLeadSource");  
    var ddlLeadStatus = $("#ddlLeadStatus");
    var ddlStatus = $("#ddlStatus");
    var ddlleadtype = $("#ddlleadtype");
    var createid = 0;
    if (ddlleadtype.val() == undefined)
    {
        createid = 0;
    }
    else
    {
        createid = ddlleadtype.val();
    }
  
    var requestData = { 
        leadCode: txtLeadCode.val().trim(),
        companyName: txtCompanyName.val().trim(),
        contactPersonName: txtContactPersoName.val().trim(),
        email: txtEmail.val().trim(),
        contactNo: txtContactNo.val().trim(),
        companyAddress: txtAddress.val(),
        companyCity: txtCompanyCity.val().trim(),
        companyStateId: ddlState.val(),
        leadSourceId: ddlLeadSource.val(),
        leadStatusId: ddlLeadStatus.val(),
        createdid: createid,
        userId: userid,
        status: ddlStatus.val()
    }; 
    $.ajax({
        url: "../Lead/GetLeadList",
        data: requestData,
        dataType: "html",
        type: "GET",
        error: function (err) {
            $("#divList").html("");
            $("#divList").html(err);
        },
        success: function (data) {
            $("#divList").html("");
            $("#divList").html(data); 
        }
    });
}

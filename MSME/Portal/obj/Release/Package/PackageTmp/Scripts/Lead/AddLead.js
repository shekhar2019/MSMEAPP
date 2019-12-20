﻿$(document).ready(function () {
    BindFollowUpActivityTypeList();
    var leadFollowup=[];
    GetLeadFollowUpList(leadFollowup);
    $("#tabs").tabs({
        collapsible: true
    });

    $("#txtAssignlead").autocomplete({
        minLength: 0,
        source: function (request, response) {
            $.ajax({
                url: "../Lead/GetUserAutoCompleteList",
                type: "GET",
                dataType: "json",
                data: { term: request.term },
                success: function (data) {
                    response($.map(data, function (item) {
                        return { label: item.FullName, value: item.UserName, UserId: item.UserId, MobileNo: item.MobileNo };
                    }))
                }
            })
        },
        focus: function (event, ui) {
            $("#txtAssignlead").val(ui.item.label);
            return false;
        },
        select: function (event, ui) {
            $("#txtAssignlead").val(ui.item.label);
            $("#hdnUserId").val(ui.item.UserId);
            $("#hdnFollowUpByUserName").val(ui.item.label);
            return false;
        },
        change: function (event, ui) {
            if (ui.item == null) {
                $("#txtAssignlead").val("");
                $("#hdnUserId").val("");
                ShowModel("Alert", "Please select User from List")
            }
            return false;
        }

    })
.autocomplete("instance")._renderItem = function (ul, item) {
    return $("<li>")
      .append("<div><b>" + item.label + " || " + item.value + "</b><br>" + item.MobileNo + "</div>")
      .appendTo(ul);
};

    var x = Math.floor(Math.random() * 100000) + 1;
        if ($(this).val(x))
        { 
            $("#txtLeadCode").val(x);
        }
      
        $('#txtReminderDate').datetimepicker({
            format: 'd-M-Y h:i a',
            formatTime: 'h:i a',
          });
        $('#txtdueDate').datetimepicker({
            format: 'd-M-Y h:i a',
            formatTime:'h:i a',
        });
        
    BindCountryList();
    $("#ddlState").append($("<option></option>").val(0).html("-Select State-"));
    BindCountryTwoList(); 
    $("#ddlBranchState").append($("<option></option>").val(0).html("-Select State-"));
    BindLeadSourceList();
    BindLeadStatusList();
    var hdnAccessMode = $("#hdnAccessMode"); 
    var hdnLeadId = $("#hdnLeadId");
    if (hdnLeadId.val() != "" && hdnLeadId.val() != "0" && hdnAccessMode.val() != "" && hdnAccessMode.val() != "0") {
        setTimeout(
        function () {
            GetLeadDetail(hdnLeadId.val());
        }, 2500);

     
        if (hdnAccessMode.val() == "3") {
            $("#btnSave").hide();
            $("#btnUpdate").hide();
            $("#btnReset").hide();
            $("input").attr('readOnly', true);
            $("textarea").attr('readOnly', true);
            $("select").attr('disabled', true);
        }
        else {
            $("#btnSave").hide();
            $("#btnUpdate").show();
            $("#btnReset").show();
        }
    }
    else {
        $("#btnSave").show();
        $("#btnUpdate").hide();
        $("#btnReset").show();
    }
    $("#txtDesignationName").focus();

    
});

function CompareDate(date1, date2) {
    var fdate = date1.split(/[+=;:-]/);
    var d1=fdate[0];
    var m1=MonthValue(fdate[1]);
    var y1=fdate[2].split(' ')[0];
    var h1=fdate[2].split(' ')[1];
    var i1=fdate[3].split(' ')[0];
    var a1=fdate[3].split(' ')[1];
    var dt1 = new Date(parseInt(y1), parseInt(m1) - 1, parseInt(d1),parseInt(h1),parseInt(i1));

    var sdate = date2.split(/[+=;:-]/);
    var d2 = sdate[0];
    var m2 = MonthValue(sdate[1]);
    var y2 = sdate[2].split(' ')[0];
    var h2 = sdate[2].split(' ')[1];
    var i2 = sdate[3].split(' ')[0];
    var a2 = sdate[3].split(' ')[1];
    var dt2 = new Date(parseInt(y2), parseInt(m2) - 1, parseInt(d2), parseInt(h2), parseInt(i2));
    var firstDate = new Date(dt1);
    var secondDate = new Date(dt2);
    if (firstDate > secondDate) {
        ShowModel("Alert", "Due Date should be greater Reminder Date ")
        return false;
    }
    if (firstDate == secondDate && a1==a2) {
        ShowModel("Alert", "Due Date should not equal Reminder Date ")
         return false;
    }
    
}
function ValueToMonth(value) {
    switch (value) {
        case 1: return "Jan";
        case 2: return "Feb";
        case 3: return "Mar";
        case 4: return "Apr";
        case 5: return "May";
        case 6: return "Jun";
        case 7: return "Jul";
        case 8: return "Aug";
        case 9: return "Sep";
        case 10: return "Oct";
        case 11: return "Nov";
        case 12: return "Dec";
        default: return "Jan";
    }
}
function MonthValue(str) {
    switch (str) {
        case "Jan": return 1;
        case "Feb": return 2;
        case "Mar": return 3;
        case "Apr": return 4;
        case "May": return 5;
        case "Jun": return 6;
        case "Jul": return 7;
        case "Aug": return 8;
        case "Sep": return 9;
        case "Oct": return 10;
        case "Nov": return 11;
        case "Dec": return 12;
        default: return 1;

    }

}
function padLeft(str) {
    if (parseInt(str) < 10) {
        return '0' + str;
    }
    else { return str; }
}
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
 
function ValidEmailCheck(emailAddress) {
    var pattern = /^([a-z\d!#$%&'*+\-\/=?^_`{|}~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]+(\.[a-z\d!#$%&'*+\-\/=?^_`{|}~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]+)*|"((([ \t]*\r\n)?[ \t]+)?([\x01-\x08\x0b\x0c\x0e-\x1f\x7f\x21\x23-\x5b\x5d-\x7e\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|\\[\x01-\x09\x0b\x0c\x0d-\x7f\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))*(([ \t]*\r\n)?[ \t]+)?")@(([a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|[a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF][a-z\d\-._~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]*[a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])\.)+([a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|[a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF][a-z\d\-._~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]*[a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])\.?$/i;
    return pattern.test(emailAddress);
};

function BindCountryList()
{
    $.ajax({
        type: "GET",
        url: "../Lead/GetCountryList",
        data: "{}",
        dataType: "json",
        asnc: false,
        success: function (data) {
            $("#ddlCountry").append($("<option></option>").val(0).html("-Select Country-"));
            $.each(data, function (i, item) {

                $("#ddlCountry").append($("<option></option>").val(item.CountryId).html(item.CountryName));
            });
        },
        error: function (Result) {
            $("#ddlCountry").append($("<option></option>").val(0).html("-Select Country-"));
        }
    });
}
function BindStateList(stateId) {
    var countryId = $("#ddlCountry option:selected").val();
    $("#ddlState").val(0);
    $("#ddlState").html("");
    if (countryId != undefined && countryId != "" && countryId != "0") {
        var data = { countryId: countryId };
        $.ajax({
            type: "GET",
            url: "../Company/GetStateList",
            data: data,
            asnc: false,
            dataType: "json",
            success: function (data) {
                $("#ddlState").append($("<option></option>").val(0).html("-Select State-"));
                $.each(data, function (i, item) {
                    $("#ddlState").append($("<option></option>").val(item.StateId).html(item.StateName));
                });
                $("#ddlState").val(stateId);
            },
            error: function (Result) {
                $("#ddlState").append($("<option></option>").val(0).html("-Select State-"));
            }
        });
    }
    else
    {
        
        $("#ddlState").append($("<option></option>").val(0).html("-Select State-"));
    }
    
}

function BindCountryTwoList()
{
    $.ajax({
        type: "GET",
        url: "../Company/GetCountryList",
        data: "{}",
        dataType: "json",
        asnc: false,
        success: function (data) {
            $("#ddlBranchCountry").append($("<option></option>").val(0).html("-Select Country-"));
            $.each(data, function (i, item) {

                $("#ddlBranchCountry").append($("<option></option>").val(item.CountryId).html(item.CountryName));
            });
        },
        error: function (Result) {
            $("#ddlBranchCountry").append($("<option></option>").val(0).html("-Select Country-"));
        }
    });
}

function BindBranchStateList(stateId) {
    var countryId = $("#ddlBranchCountry option:selected").val();
    $("#ddlBranchState").val(0);
    $("#ddlBranchState").html("");
    if (countryId != undefined && countryId != "" && countryId != "0") {
        var data = { countryId: countryId };
        $.ajax({
            type: "GET",
            url: "../Company/GetStateList",
            data: data,
            asnc: false,
            dataType: "json",
            success: function (data) {
                $("#ddlBranchState").append($("<option></option>").val(0).html("-Select State-"));
                $.each(data, function (i, item) {
                    $("#ddlBranchState").append($("<option></option>").val(item.StateId).html(item.StateName));
                });
                $("#ddlBranchState").val(stateId);
            },
            error: function (Result) {
                $("#ddlBranchState").append($("<option></option>").val(0).html("-Select State-"));
            }
        });
    }
    else {

        $("#ddlBranchState").append($("<option></option>").val(0).html("-Select State-"));
    }

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

function AddLeadFollowUp(action) {

    var hdnLeadId = $("#hdnLeadId");
    var hdnLeadFollowUpId = $("#hdnLeadFollowUpId");
    var ddlActivityType = $("#ddlActivityType");
    var txtdueDate = $("#txtdueDate");
    var txtReminderDate = $("#txtReminderDate");
    var txtRemarks = $("#txtRemarks");
    var ddlPriority = $("#ddlPriority");
    var ddlLeadStatus = $("#ddlLeadStatus");
    var txtLeadStatusReason = $("#txtLeadStatusReason");
    var txtAssignlead = $("#txtAssignlead");
    var hdnUserId = $("#hdnUserId");
    var hdnFollowUpByUserName = $("#hdnFollowUpByUserName");
    var hdnCreatedDate = $("#hdnCreatedDate");
    var hdnModifyDate = $("#hdnModifyDate");
    var hdnLoginUserId = $("#hdnLoginUserId");
    

    if (ddlActivityType.val().trim() == "" ||ddlActivityType.val()=="0") {
        ShowModel("Alert", "Please Select Activity Type")
        ddlActivityType.focus();
        return false;
    }
    if (txtdueDate.val().trim()== "") {
        ShowModel("Alert", "Please select due date")
        txtdueDate.focus();
        return false;
    }
    
    if (txtReminderDate.val().trim() == "") {
        ShowModel("Alert", "Please select reminder date")
        txtReminderDate.focus();
        return false;
    }
    
    if (CompareDate(txtReminderDate.val(), txtdueDate.val())==false)
    {
        txtReminderDate.focus();
        return false;
    }

    if (ddlLeadStatus.val() == "" || ddlLeadStatus.val() == "0") {
        ShowModel("Alert", "Please select Lead Status")
        ddlLeadStatus.focus();
        return false;
    }
    if (ddlPriority.val() == "" || ddlPriority.val() == "0") {
        ShowModel("Alert", "Please select Priority")
        ddlPriority.focus();
        return false;
    }
    var leadFollowupList = [];
    $('#tblLeadList tr').each(function (i, row) {
        var $row = $(row);
        var LeadFollowUpId = $row.find("#hdnLeadFollowUpId").val();
        var LeadId = $row.find("#hdnLeadflId").val();
        var followUpActivityTypeId = $row.find("#hdnFollowUpActivityTypeId").val();
        var hdnFollowUpActivityTypeName =$row.find("#hdnFollowUpActivityTypeName").val();
        var followUpDueDateTime = $row.find("#hdnFollowUpDueDateTime").val();
        var followUpReminderDateTime = $row.find("#hdnFollowUpReminderDateTime").val();
        var followUpRemarks = $row.find("#hdnFollowUpRemarks").val();
        var priority = $row.find("#hdnPriority").val();
        var hdnPriorityName = $row.find("#hdnPriorityName").val();
        var leadStatusId = $row.find("#hdnLeadStatusId").val();
        var hdnLeadStatusName = $row.find("#hdnLeadStatusName").val();
        var leadStatusReason = $row.find("#hdnLeadStatusReason").val();
        var leadFollowUpId = $row.find("#hdnLeadFollowUpId").val();
        var leadCreatedBy = $row.find("#hdnLeadCreatedBy").val();
        var leadCreatedDate = $row.find("#hdnLeadCreatedDate").val();
        var leadModifiedBy = $row.find("#hdnLeadModifiedBy").val();
        var leadModifiedDate = $row.find("#hdnLeadModifiedDate").val();
        var followUpByUserName = $row.find("#hdnFollowUpByUserName").val();
        var hdnLeadCreatedBy = $row.find("#hdnLeadCreatedBy").val();
        
        if (LeadFollowUpId != undefined) {
            if (action == 1 || hdnLeadFollowUpId != LeadFollowUpId) {

                var leadFollowup = {
                    LeadFollowUpId: LeadFollowUpId,
                    LeadId: LeadId,
                    FollowUpActivityTypeId: followUpActivityTypeId,
                    FollowUpActivityTypeName:hdnFollowUpActivityTypeName, 
                    FollowUpDueDateTime: followUpDueDateTime,
                    FollowUpReminderDateTime: followUpReminderDateTime,
                    FollowUpRemarks: followUpRemarks,
                    Priority: priority,
                    PriorityName: hdnPriorityName,
                    LeadStatusId: leadStatusId,
                    LeadStatusName: hdnLeadStatusName,
                    LeadStatusReason: leadStatusReason,
                    FollowUpByUserId: leadFollowUpId,
                    FollowUpByUserName: followUpByUserName,
                    CreatedBy: hdnLeadCreatedBy 
                 };
                leadFollowupList.push(leadFollowup);
            }
        }

    });
    var leadFollowUpAddEdit = {
        LeadFollowUpId: hdnLeadFollowUpId.val(),
        LeadId:hdnLeadId.val(),
        FollowUpActivityTypeName:$("#ddlActivityType option:selected").text(),
        FollowUpActivityTypeId: $("#ddlActivityType").val(),
        FollowUpDueDateTime: txtdueDate.val(),
        FollowUpReminderDateTime: txtReminderDate.val(),
        FollowUpRemarks: txtRemarks.val(),
        PriorityName: $("#ddlPriority option:selected").text(),
        Priority:ddlPriority.val(),
        LeadStatusId:$("#ddlLeadStatus").val(),
        LeadStatusName: $("#ddlLeadStatus option:selected").text(),
        LeadStatusReason: txtLeadStatusReason.val(),
        FollowUpByUserId: hdnUserId.val(),
        FollowUpByUserName: hdnFollowUpByUserName.val(),
        CreatedBy: hdnLoginUserId.val()
    };
    leadFollowupList.push(leadFollowUpAddEdit);
    GetLeadFollowUpList(leadFollowupList);
   
}
function EditRow(obj) {
  
    var $row = $(obj).closest("tr");
    var LeadFollowUpId = $row.find("#hdnLeadFollowUpId").val();
    var LeadId = $row.find("#hdnLeadId").val();
    var followUpActivityTypeId = $row.find("#hdnFollowUpActivityTypeId").val();  
    var hdnFollowUpActivityTypeName = $row.find("#hdnFollowUpActivityTypeName").val();
    var followUpDueDateTime = $row.find("#hdnFollowUpDueDateTime").val();
    var followUpReminderDateTime = $row.find("#hdnFollowUpReminderDateTime").val();
    var followUpRemarks = $row.find("#hdnFollowUpRemarks").val();
    var priority = $row.find("#hdnPriority").val();
    var hdnPriorityName = $row.find("#hdnPriorityName").val();
    var leadStatusId = $row.find("#hdnLeadStatusId").val();
    var hdnLeadStatusName = $row.find("#hdnLeadStatusName").val();
    var leadStatusReason = $row.find("#hdnLeadStatusReason").val();
    var leadFollowUpId = $row.find("#hdnLeadFollowUpId").val();
    var followUpByUserName = $row.find("#hdnFollowUpByUserName").val();
 
    $("#hdnLeadFollowUpId").val(LeadFollowUpId);
    $("#hdnLeadId").val(LeadId);
    $("#ddlActivityType").val(followUpActivityTypeId);
    $("#txtReminderDate").val(followUpReminderDateTime);
    $("#ddlPriority").val(priority);
    $("#ddlLeadStatus").val(leadStatusId);
    $("#txtAssignlead").val(followUpByUserName);
    $("#txtdueDate").val(followUpDueDateTime);
    $("#txtRemarks").val(followUpRemarks);
    $("#txtLeadStatusReason").val(leadStatusReason);
    $("#btnAddFollowUp").hide();
    $("#btnUpdateFollowUp").show();
}

function ClearFollowUpFields() {
    $("#hdnLeadFollowUpId").val("0");
    $("#ddlActivityType").val("0");
    $("#txtdueDate").val("");
    $("#txtReminderDate").val("");
    $("#txtRemarks").val("");
    $("#ddlPriority").val("0");
    $("#ddlLeadStatus").val("0");
    $("#txtLeadStatusReason").val("");
    $("#txtAssignlead").val("");
    $("#btnAddFollowUp").show();
    $("#btnUpdateFollowUp").hide();
}

function GetLeadFollowUpList(leadFollowUps) {
    var hdnleadId = $("#hdnLeadId");
    var requestData = { leadFollowUps: leadFollowUps, leadid: hdnleadId.val() };
    $.ajax({
        url: "../Lead/GetLeadFollowUpList",
        cache: false,
        data: JSON.stringify(requestData),
        dataType: "html",
        contentType: "application/json; charset=utf-8",
        type: "POST",
        error: function (err) {
            $("#divLeadList").html("");
            $("#divLeadList").html(err);
        },
        success: function (data) {
            $("#divLeadList").html("");
            $("#divLeadList").html(data);
            ClearFollowUpFields();
        }
    });
}

function BindFollowUpActivityTypeList() {
    $.ajax({
        type: "GET",
        url: "../Lead/GetFollowUpActivityTypeList",
        data: "{}",
        dataType: "json",
        asnc: false,
        success: function (data) {
            $("#ddlActivityType").append($("<option></option>").val(0).html("-Select Activity Type-"));
            $.each(data, function (i, item) {

                $("#ddlActivityType").append($("<option></option>").val(item.FollowUpActivityTypeId).html(item.FollowUpActivityTypeName));
            });
        },
        error: function (Result) {
            $("#ddlActivityType").append($("<option></option>").val(0).html("-Select Activity Type-"));
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

function GenerateLeadCode() {
    var x = Math.floor(Math.random() * 100000) + 1;
    if ($(this).val(x)) {
        $("#txtLeadCode").val(x);
    } 
}

function GenerateSameAddress() {
    var chkstatus = $("#txtSameCompanyAddress").is(':checked') ? true : false;  
    if (chkstatus)
    {
        var txtAddress = $("#txtAddress").val();
        if (txtAddress != "")
            {
                $("#txtBranchAddress").val(txtAddress);
            }
        var txtCity = $("#txtCompanyCity").val();
        if (txtCity != "") {
            $("#txtCity").val(txtCity);
        }
        var ddlCountry = $("#ddlCountry").val();
        if (ddlCountry != "" && ddlCountry != "0") {

            $("#ddlBranchCountry").val(ddlCountry);
            
        }
        var ddlState = $("#ddlState").val();
        if (ddlState != "" && ddlState != "0") {

            BindBranchStateList(ddlState);

            $("#ddlBranchState").val(ddlBranchState);
        }
        var txtPincode = $("#txtCompanyPinCode").val();
        if (txtPincode != "") {
            $("#txtPinCode").val(txtPincode);
        }
    }

    else { 
        $("#txtBranchAddress").val(""); 
        $("#txtCity").val(""); 
        $("#ddlBranchCountry").val("0"); 
        $("#ddlBranchState").val("0"); 
        $("#txtPinCode").val("");
        } 
}



function GetLeadDetail(leadId) {
    $.ajax({
        type: "GET",
        asnc: false,
        url: "../Lead/GetLeadDetail",
        data: { leadId: leadId },
        dataType: "json",
        success: function (data) {
            $("#txtLeadCode").val(data.LeadCode);
            $("#txtCompanyName").val(data.CompanyName);
            $("#txtEmail").val(data.Email);
            $("#txtContactNo").val(data.ContactNo);
            $("#txtFax").val(data.Fax);
            $("#txtAddress").val(data.CompanyAddress);
            $("#txtCompanyCity").val(data.CompanyCity);
            $("#ddlCountry").val(data.CountryId);
            BindStateList(data.StateId);
            $("#ddlState").val(data.StateId);
            $("#txtCompanyPinCode").val(data.CompanyPinCode);
            $("#ddlLeadSource").val(data.LeadSourceId);
            $("#txtContactPersoName").val(data.ContactPersonName);
            $("#txtAlternateEmail").val(data.AlternateEmail);
            $("#txtAlternateContactNo").val(data.AlternateContactNo);
            $("#txtDesignation").val(data.Designation);
            $("#txtBranchAddress").val(data.BranchAddress);
            $("#txtCity").val(data.City); 
            $("#ddlBranchCountry").val(data.CompanyCountryId);
            BindBranchStateList(data.CompanyStateId); 
            $("#ddlBranchState").val(data.StateId);
            $("#txtPinCode").val(data.PinCode);
            $("#txtOtherLeadSourceDescription").val(data.OtherLeadSourceDescription);
            $("#ddlLeadStatus").val(data.LeadStatusId); 
            if (data.Lead_Status == true) {
                $("#chkstatus").attr("checked", true);
            }
            else {
                $("#chkstatus").attr("checked", false);
            }
        },
        error: function (Result) {
            ShowModel("Alert", "Problem in Request");
        }
    });
}

function SaveData() {
    var hdnLeadId = $("#hdnLeadId");
    var txtLeadCode = $("#txtLeadCode");
    var txtCompanyName = $("#txtCompanyName");
    var txtEmail = $("#txtEmail"); 
    var txtContactNo = $("#txtContactNo");
    var txtFax = $("#txtFax");
    var txtAddress = $("#txtAddress");
    var txtCompanyCity = $("#txtCompanyCity");
    var ddlCountry = $("#ddlCountry");
    var ddlState = $("#ddlState"); 
    var txtCompanyPinCode = $("#txtCompanyPinCode");
    var ddlLeadSource = $("#ddlLeadSource"); 
    var txtContactPersoName = $("#txtContactPersoName");
    var txtAlternateEmail = $("#txtAlternateEmail");
    var txtAlternateContactNo = $("#txtAlternateContactNo");
    var txtDesignation = $("#txtDesignation");
    var txtBranchAddress = $("#txtBranchAddress");
    var txtCity = $("#txtCity");
    var ddlBranchState = $("#ddlBranchState");
    var ddlBranchCountry = $("#ddlBranchCountry");
    var txtPinCode = $("#txtPinCode");
    var ddlLeadStatus = $("#ddlLeadStatus");
    var txtOtherLeadSourceDescription = $("#txtOtherLeadSourceDescription");  
   
    var chkstatus = $("#chkstatus").is(':checked') ? true : false;

    if (txtCompanyName.val().trim() == "") {
        ShowModel("Alert", "Please Enter Company Name")
        txtCompanyName.focus();
        return false;
    }
    if (txtEmail.val().trim() == "") {
        ShowModel("Alert", "Please Enter Email")
        txtEmail.focus();
        return false;
    }
  
    if (!ValidEmailCheck(txtEmail.val().trim())) {
        ShowModel("Alert", "Please Enter Valid Email Address")
        txtEmail.focus();
        return false;
    }
    if (txtAlternateEmail.val().trim() !== "") {
        if (!ValidEmailCheck(txtAlternateEmail.val().trim())) {
            ShowModel("Alert", "Please Enter Valid Alternate Email")
            txtAlternateEmail.focus();
            return false;
        } 
    } 
    if (txtContactPersoName.val() == 0) {
        ShowModel("Alert", "Please Enter Contact Person Name")
        txtContactPersoName.focus();
        return false;
    } 
    if (txtContactNo.val().trim() == "") {
        ShowModel("Alert", "Please Enter Contact No")
        txtContactNo.focus();
        return false;
    }
    if (txtDesignation.val().trim() == 0) {
        ShowModel("Alert", "Please Enter Designation")
        txtDesignation.focus();
        return false;
    }
    if (txtAddress.val().trim() == 0) {
        ShowModel("Alert", "Please Enter Address")
        txtAddress.focus();
        return false;
    }
    if (txtCompanyCity.val().trim() == "") {
        ShowModel("Alert", "Please Enter City")
        txtCompanyCity.focus();
        return false;
    }
    if (ddlState.val() == 0) {
        ShowModel("Alert", "Please Select State")
        ddlState.focus();
        return false;
    }
    if (ddlCountry.val() == 0) {
        ShowModel("Alert", "Please Select Country")
        ddlCountry.focus();
        return false;
    }
    
    if (ddlLeadSource.val() == 0) {
        ShowModel("Alert", "Please Select Lead Source")
        ddlLeadSource.focus();
        return false;
    }
 
    var leadViewModel = {
        LeadId: hdnLeadId.val(),
        LeadCode: txtLeadCode.val().trim(),
        CompanyName: txtCompanyName.val().trim(),
        ContactPersonName: txtContactPersoName.val().trim(),
        Fax: txtFax.val().trim(),
        Designation: txtDesignation.val().trim(),
        Email: txtEmail.val().trim(),
        AlternateEmail: txtAlternateEmail.val().trim(),
        ContactNo: txtContactNo.val().trim(),
        AlternateContactNo: txtAlternateContactNo.val().trim(),
        CompanyAddress: txtAddress.val().trim(),
        BranchAddress:txtBranchAddress.val().trim(),
        City:txtCity.val().trim(),
        StateId: ddlState.val(),
        CountryId: ddlCountry.val(),
        CompanyPinCode: txtPinCode.val().trim(),
        CompanyCity: txtCompanyCity.val().trim(),
        CompanyStateId: ddlBranchState.val(),
        CompanyCountryId:ddlBranchCountry.val(),
        PinCode: txtPinCode.val().trim(),
        LeadStatusId: ddlLeadStatus.val(),
        LeadSourceId: ddlLeadSource.val(),
        OtherLeadSourceDescription: txtOtherLeadSourceDescription.val().trim(),
        Lead_Status: chkstatus
        
    };
    var leadFollowUpList = [];
    $('#tblLeadList tr').each(function (i, row) {
        var $row = $(row);
        var leadFollowUpId = $row.find("#hdnLeadFollowUpId").val();
        var followUpActivityTypeId = $row.find("#hdnFollowUpActivityTypeId").val();
        var followUpDueDateTime = $row.find("#hdnFollowUpDueDateTime").val();
        var followUpReminderDateTime = $row.find("#hdnFollowUpReminderDateTime").val();
        var followUpRemarks = $row.find("#hdnFollowUpRemarks").val();
        var priority = $row.find("#hdnPriority").val();
        var leadStatusId = $row.find("#hdnLeadStatusId").val();
        var leadStatusReason = $row.find("#hdnLeadStatusReason").val();
        var followUpByUserId = $row.find("#hdnFollowUpByUserId").val() == "" ? $row.find("#hdnLeadCreatedBy").val() : $row.find("#hdnFollowUpByUserId").val();
        var followUpByUserName = $row.find("#hdnFollowUpByUserName").val();
        var leadCreatedBy = $row.find("#hdnLeadCreatedBy").val();

        if (leadFollowUpId != undefined) {

            var leadFollowUps = {
                LeadFollowUpId: leadFollowUpId,
                FollowUpActivityTypeId: followUpActivityTypeId,
                FollowUpDueDateTime: followUpDueDateTime,
                FollowUpReminderDateTime: followUpReminderDateTime,
                FollowUpRemarks: followUpRemarks,
                Priority: priority,
                LeadStatusId: leadStatusId,
                LeadStatusReason: leadStatusReason,
                FollowUpByUserId: followUpByUserId,
                FollowUpByUserName:followUpByUserName,
                CreatedBy: leadCreatedBy
              };
            leadFollowUpList.push(leadFollowUps);
        }

    });
    var requestData = { leadViewModel: leadViewModel, leadFollowUps:leadFollowUpList };
    $.ajax({
        url: "../Lead/AddEditLead",
        cache: false,
        type: "POST",
        dataType: "json",
        data: JSON.stringify(requestData),
        contentType: 'application/json',
        success: function (data) {
            if (data.status == "SUCCESS") {
                ShowModel("Alert", data.message);
                ClearFields();
                $("#btnSave").show();
                $("#btnUpdate").hide();
            }
            else {
                ShowModel("Error", data.message)
            }
        },
        error: function (err) {
            ShowModel("Error", err)
        }
    });

}
function ShowModel(headerText, bodyText) {
    $("#alertModel").modal();
    $("#modelHeader").html(headerText);
    $("#modelText").html(bodyText);
}
function ClearFields() { 
        $("#hdnLeadId").val("0"); 
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
        $("#chkstatus").prop("checked", true);

    }

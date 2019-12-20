﻿$(document).ready(function () {
   
    var hdnAccessMode = $("#hdnAccessMode"); 
    var hdnSLId = $("#hdnSLId");
    if (hdnSLId.val() != "" && hdnSLId.val() != "0" && hdnAccessMode.val() != "" && hdnAccessMode.val() != "0") {
        setTimeout(
          function () {
              GetSLDetail(hdnSLId.val());
          }, 1000);
        
        if (hdnAccessMode.val() == "3") {
            $("#btnSave").hide();
            $("#btnUpdate").hide();
            $("#btnReset").hide();
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
    
});


BindSLTypeList();
BindCostCenterList();


function BindSLTypeList() {
    $.ajax({
        type: "GET",
        url: "../SL/GetSLTypeList",
        data: "{}",
        dataType: "json",
        asnc: false,
        success: function (data) {
            $.each(data, function (i, item) {
                $("#ddlSLTypeId").append($("<option></option>").val(item.SLTypeId).html(item.SLTypeName));
            });
        },
        error: function (Result) {
            $("#ddlSLTypeId").append($("<option></option>").val(0).html("NA"));
        }
    });
}

function BindCostCenterList() {
    $.ajax({
        type: "GET",
        url: "../SL/GetCostCenterList",
        data: "{}",
        dataType: "json",
        asnc: false,
        success: function (data) {
            $.each(data, function (i, item) {
                $("#ddlCostCenterId").append($("<option></option>").val(item.CostCenterId).html(item.CostCenterName));
            });
        },
        error: function (Result) {
            $("#ddlCostCenterId").append($("<option></option>").val(0).html("Select Cost Center Name"));
        }
    });
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

function GetSLDetail(slId) {
    $.ajax({
        type: "GET",
        asnc: false,
        url: "../SL/GetSLDetail",
        data: { sLId: slId },
        dataType: "json",
        success: function (data) {
            $("#hdnSLId").val(slId);
            $("#txtSLCode").val(data.SLCode);
            $("#txtSLHead").val(data.SLHead);
            $("#txtRefCode").val(data.RefCode);
            $("#ddlSLTypeId").val(data.SLTypeId);
            $("#ddlCostCenterId").val(data.CostCenterId);
            if (data.SL_Status == true) {
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

function SaveData(action) {
    var hdnSLId = $("#hdnSLId");
    var txtSLCode = $("#txtSLCode");
    var txtSLHead = $("#txtSLHead");
    var txtRefCode = $("#txtRefCode");
    var ddlSLTypeId = $("#ddlSLTypeId");
    var ddlCostCenterId = $("#ddlCostCenterId");
    var chkstatus = $("#chkstatus").is(":checked") ? true : false;


    if (txtSLCode.val().trim() == "") {
        ShowModel("Alert", "Please enter SL Code")
        txtSLCode.focus();
        return false;
   }

    if (txtSLHead.val().trim() == "") {
       ShowModel("Alert", "Please enter SL Head")
       txtSLHead.focus();
       return false;
   }

    if (ddlSLTypeId.val().trim() == "0") {
       ShowModel("Alert", "Please Select SL Type")
       ddlSLTypeId.focus();
       return false;
   }

    if (ddlCostCenterId.val().trim() == "0") {
        ShowModel("Alert", "Please Select Cost Center")
        ddlCostCenterId.focus();
        return false;
   }

    var slViewModel = {
        
        SLId:hdnSLId.val(),
        SLCode:txtSLCode.val(),
        SLHead:txtSLHead.val(),
        RefCode:txtRefCode.val(),
        SLTypeId:ddlSLTypeId.val(),
        CostCenterId:ddlCostCenterId.val(),
        SL_Status: chkstatus,
    };

    
    var requestData = { sLViewModel: slViewModel };
    $.ajax({
        url: "../SL/AddEditSL",
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
    $("#hdnSLId").val("0");
    $("#txtSLCode").val("");
    $("#txtSLHead").val("");
    $("#txtRefCode").val("");
    $("#ddlSLTypeId").val("0");
    $("#ddlCostCenterId").val("0");
   $("#chkstatus").prop("checked", true);

}

$(document).ready(function () {
    $("#ddlFormType").attr('disabled', true);
    $("#txtWithCFormTaxPercentage").attr('disabled', true);
    $("#DivSurcharge").hide();
    $("#DivSurcharge2").hide();
    $("#DivSurcharge3").hide();
    $("#DivChkSurcharge1").hide();
    $("#DivChkSurcharge2").hide();
    var hdnAccessMode = $("#hdnAccessMode"); 
    var hdnTaxId = $("#hdnTaxId");
    BindFormType();
    if (hdnTaxId.val() != "" && hdnTaxId.val() != "0" && hdnAccessMode.val() != "" && hdnAccessMode.val() != "0") {
      GetTaxDetail(hdnTaxId.val());
    
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
    $("#txtTaxName").focus();
    
    
});
//$(".alpha-only").on("keydown", function (event) {
//    // Allow controls such as backspace
//    var arr = [8, 16, 17, 20, 35, 36, 37, 38, 39, 40, 45, 46];

//    // Allow letters
//    for (var i = 65; i <= 90; i++) {
//        arr.push(i);
//    }

//    // Prevent default if not in array
//    if (jQuery.inArray(event.which, arr) === -1) {
//        event.preventDefault();
//    }
//});

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
function BindFormType() {
    $.ajax({
        type: "GET",
        url: "../CustomerForm/GetFormTypeList",
        data: "{}",
        dataType: "json",
        asnc: false,
        success: function (data) {
            $("#ddlFormType").append($("<option></option>").val(0).html("-Select Form Type Desc-"));
            $.each(data, function (i, item) {
                $("#ddlFormType").append($("<option></option>").val(item.FormTypeId).html(item.FormTypeDesc));
            });
        },
        error: function (Result) {
            $("#ddlFormType").append($("<option></option>").val(0).html("-Select Form Type Desc-"));
        }
    });
}

function ValidEmailCheck(emailAddress) {
    var pattern = /^([a-z\d!#$%&'*+\-\/=?^_`{|}~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]+(\.[a-z\d!#$%&'*+\-\/=?^_`{|}~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]+)*|"((([ \t]*\r\n)?[ \t]+)?([\x01-\x08\x0b\x0c\x0e-\x1f\x7f\x21\x23-\x5b\x5d-\x7e\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|\\[\x01-\x09\x0b\x0c\x0d-\x7f\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))*(([ \t]*\r\n)?[ \t]+)?")@(([a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|[a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF][a-z\d\-._~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]*[a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])\.)+([a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|[a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF][a-z\d\-._~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]*[a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])\.?$/i;
    return pattern.test(emailAddress);
};
  

function GetTaxDetail(taxId) {
    $.ajax({
        type: "GET",
        asnc: false,
        url: "../Tax/GetTaxDetail",
        data: { taxId: taxId },
        dataType: "json",
        success: function (data) {
            $("#txtLeadCode").val(data.LeadCode);
            $("#txtTaxName").val(data.TaxName);
            $("#txtTaxPercentage").val(data.TaxPercentage);
            $("#ddlTaxType").val(data.TaxType);
            $("#ddlTaxSubType").val(data.TaxSubType);
            $("#ddlFormType").val(data.FormTypeId);
            
            if (data.CFormApplicable  == true) {
                $("#chkCFormApplicable").attr("checked", true);
            }
            else {
                $("#chkCFormApplicable").attr("checked", false);
            }

            $("#txtWithCFormTaxPercentage").val(data.WithOutCFormTaxPercentae);
            $("#txtGLCode").val(data.TaxGLCode);
            $("#txtTaxSLCode").val(data.TaxSLCode);
            if (data.Tax_Status == true) {
                $("#chkstatus").attr("checked", true);
            }
            else {
                $("#chkstatus").attr("checked", false);
            }

            $("#txtSurchargeName1").val(data.SurchargeName_1);
            $("#txtSurchargeName2").val(data.SurchargeName_2);
            $("#txtSurchargeName3").val(data.SurchargeName_3);
            $("#txtSurchargePercentage_1").val(data.SurchargePercentage_1);
            $("#txtSurchargePercentage_2").val(data.SurchargePercentage_2);
            $("#txtSurchargePercentage3").val(data.SurchargePercentage_3);

            if(data.SurchargeName_1 !="" || data.SurchargePercentage_1 !="")
            {
                $("#chkSurcharge").attr("checked", true);
                $("#DivSurcharge").show();
                $("#DivChkSurcharge1").show();
                
            }
            else
            {
                $("#DivSurcharge").hide();
                $("#chkSurcharge").attr("checked", false);             

            }
            if (data.SurchargeName_2 != "" || data.SurchargePercentage_2 != "") {
                $("#chkSurcharge1").attr("checked", true);
                $("#DivSurcharge2").show();
                $("#DivChkSurcharge2").show();
              

            }
            else {
                $("#DivSurcharge2").hide();             
                $("#chkSurcharge1").attr("checked", false);
            }
            if (data.SurchargeName_3 != "" || data.SurchargePercentage_3 != "") {
                $("#chkSurcharge2").attr("checked", true);
                $("#DivSurcharge3").show();
              


            }
            else {
                $("#DivSurcharge3").hide();
                $("#chkSurcharge2").attr("checked", false);
            }

        },
        error: function (Result) {
            ShowModel("Alert", "Problem in Request");
        }
    });
}
function changeESINoStatus() {
    if ($("#chkCFormApplicable").is(':checked')) {
        $("#txtWithCFormTaxPercentage").attr("disabled", false);
        $("#ddlFormType").attr("disabled", false);
    }
    else {
        $("#txtWithCFormTaxPercentage").attr("disabled", true);
        $("#txtWithCFormTaxPercentage").val("");
        $("#ddlFormType").attr("disabled", true);
        $("#ddlFormType").val("0");
    }
}


function ShowSurcharge() {
    if ($("#chkSurcharge").is(':checked')) {
        $("#DivSurcharge").show();


        $("#DivChkSurcharge1").show();
    }
    else {
        $("#DivSurcharge").hide();
        $("#txtSurchargeName1").val("");
        $("#txtSurchargePercentage_1").val("");
        $("#DivChkSurcharge1").hide();
    }
}

function ShowSurcharge1() {
    if ($("#chkSurcharge1").is(':checked')) {
        $("#DivSurcharge2").show();
        $("#DivChkSurcharge2").show();
    }
    else {
        $("#DivSurcharge2").hide();
        $("#txtSurchargeName2").val("");
        $("#txtSurchargePercentage_2").val("");
        $("#DivChkSurcharge2").hide();
    }
}
function ShowSurcharge2() {
    if ($("#chkSurcharge2").is(':checked')) {
        $("#DivSurcharge3").show();
    }
    else {

        $("#DivSurcharge3").hide();
        $("#txtSurchargeName3").val("");
        $("#txtSurchargePercentage3").val("");
    }
}
function checkDec(el) {
    var ex = /^[0-9]+\.?[0-9]*$/;
    if (ex.test(el.value) == false) {
        el.value = el.value.substring(0, el.value.length - 1);
    }

}
function SaveData() {
    var txtTaxName = $("#txtTaxName");
    var txtTaxPercentage = $("#txtTaxPercentage");
    var ddlTaxType = $("#ddlTaxType");
    var ddlTaxSubType = $("#ddlTaxSubType");
    var chkCFormApplicable = $("#chkCFormApplicable").is(':checked') ? true : false;
    var txtWithCFormTaxPercentage = $("#txtWithCFormTaxPercentage");
    var ddlFormType = $("#ddlFormType");
    var txtGLCode = $("#txtGLCode");
    var txtTaxSLCode = $("#txtTaxSLCode"); 
    var hdnTaxId = $("#hdnTaxId");

    var txtSurchargeName1 = $("#txtSurchargeName1");
    var txtSurchargeName2 = $("#txtSurchargeName2");
    var txtSurchargeName3 = $("#txtSurchargeName3");
    var txtSurchargePercentage_1 = $("#txtSurchargePercentage_1");
    var txtSurchargePercentage_2 = $("#txtSurchargePercentage_2");
    var txtSurchargePercentage3 = $("#txtSurchargePercentage3");

    var chkstatus = $("#chkstatus").is(':checked') ? true : false;


    if (txtTaxName.val().trim() == "") {
        ShowModel("Alert", "Please Enter Tax Name")
        txtTaxName.focus();
        return false;
    }
    if (txtTaxPercentage.val().trim() == "") {
        ShowModel("Alert", "Please Enter Tax Percentage")
        txtTaxPercentage.focus();
        return false;
    } 
  
    if (ddlTaxType.val().trim() == 0) {
        ShowModel("Alert", "Please Select Tax Type")
        ddlTaxType.focus();
        return false;
    } 
    if (ddlTaxSubType.val().trim() == 0) {
        ShowModel("Alert", "Please Select Tax Sub Type")
        ddlTaxSubType.focus();
        return false;
    } 
 
    if (chkCFormApplicable == true) { 
        if (txtWithCFormTaxPercentage.val().trim() == 0) {
            ShowModel("Alert", "Please Enter C From Tax Form Tax Percentage Type")
            txtWithCFormTaxPercentage.focus();
            return false;
        }
        if (ddlFormType.val() == 0) {
            ShowModel("Alert", "Please Select Tax Form Type")
            ddlFormType.focus();
            return false;
        }

        
    }
 
    var taxViewModel = {
        TaxId: hdnTaxId.val(),
        TaxName: txtTaxName.val().trim(),
        TaxType: ddlTaxType.val().trim(),
        TaxSubType: ddlTaxSubType.val().trim(),
        TaxPercentage: txtTaxPercentage.val().trim(),
        FormTypeId: ddlFormType.val(),
        CFormApplicable: chkCFormApplicable,
        WithOutCFormTaxPercentae: txtWithCFormTaxPercentage.val().trim(),
        TaxGLCode: txtGLCode.val().trim(),
        TaxSLCode: txtTaxSLCode.val().trim(),
        SurchargeName_1: txtSurchargeName1.val().trim(),
        SurchargeName_2: txtSurchargeName2.val().trim(),
        SurchargeName_3: txtSurchargeName3.val().trim(),
        SurchargePercentage_1: txtSurchargePercentage_1.val().trim(),
        SurchargePercentage_2: txtSurchargePercentage_2.val().trim(),
        SurchargePercentage_3: txtSurchargePercentage3.val().trim(),
        Tax_Status: chkstatus 
    };
    var requestData = { taxViewModel: taxViewModel };
    $.ajax({
        url: "../Tax/AddEditTax",
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
        $("#hdnTaxId").val("0"); 
        $("#txtTaxName").val("");
        $("#txtTaxPercentage").val("");
        $("#ddlTaxType").val("0");
        $("#ddlTaxType").val("0");
        $("#ddlFormType").val("0");
        $("#chkCFormApplicable").prop("checked", false);
        $("#txtWithCFormTaxPercentage").attr("disabled", true);
        $("#txtWithCFormTaxPercentage").val();
        $("#txtGLCode").val("");
        $("#txtTaxSLCode").val("");
        $("#txtSurchargeName1").val("");
        $("#txtSurchargePercentage_1").val("");
        $("#txtSurchargeName2").val("");
        $("#txtSurchargePercentage_2").val("");
        $("#txtSurchargeName3").val("");
        $("#txtSurchargePercentage3").val("");
        $("#chkSurcharge").prop("checked", false);
        $("#chkSurcharge1").prop("checked", false);
        $("#DivSurcharge").hide();
        $("#DivSurcharge2").hide();
        $("#DivSurcharge3").hide();
        $("#chkstatus").prop("checked", true); 
    }

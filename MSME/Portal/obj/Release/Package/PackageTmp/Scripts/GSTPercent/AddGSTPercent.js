$(document).ready(function () {
    var hdnAccessMode = $("#hdnAccessMode");
    var hdnGSTPercentageID = $("#hdnGSTPercentageID");
    if (hdnGSTPercentageID.val() != "" && hdnGSTPercentageID.val() != "0" && hdnAccessMode.val() != "" && hdnAccessMode.val() != "0") {

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
    $("#txtGSTPercentageName").focus();
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

function ValidEmailCheck(emailAddress) {
    var pattern = /^([a-z\d!#$%&'*+\-\/=?^_`{|}~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]+(\.[a-z\d!#$%&'*+\-\/=?^_`{|}~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]+)*|"((([ \t]*\r\n)?[ \t]+)?([\x01-\x08\x0b\x0c\x0e-\x1f\x7f\x21\x23-\x5b\x5d-\x7e\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|\\[\x01-\x09\x0b\x0c\x0d-\x7f\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))*(([ \t]*\r\n)?[ \t]+)?")@(([a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|[a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF][a-z\d\-._~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]*[a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])\.)+([a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|[a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF][a-z\d\-._~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]*[a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])\.?$/i;
    return pattern.test(emailAddress);
};

function RemoveGSTPercent(gSTPercentId) {
    if (confirm("Do you want to remove selected GSTPercent?")) {
        $.ajax({
            type: "GET",
            asnc: false,
            url: "../GSTPercent/RemoveGSTPercentage",
            data: { gSTPercentId: gSTPercentId },
            dataType: "json",
            success: function (data) {
                if (data.status == "SUCCESS") {
                    // ShowModel("Alert", data.message);
                    SearchGSTPercentage();
                }
            },
            error: function (Result) {
                ShowModel("Alert", "Problem in Request");
            }
        });
    }
}
function changeESINoStatus() {
    if ($("#chkCFormApplicable").is(':checked')) {
        $("#txtWithCFormTaxPercentage").attr("disabled", false);
    }
    else {
        $("#txtWithCFormTaxPercentage").attr("disabled", true);
        $("#txtWithCFormTaxPercentage").val("");
    }
}
function checkDec(el) {
    var ex = /^[0-9]+\.?[0-9]*$/;
    if (ex.test(el.value) == false) {
        el.value = el.value.substring(0, el.value.length - 1);
    }

}
function SaveData() {
    var hdnGSTPercentageID = $("#hdnGSTPercentageID");
    var txtGSTPercentageName = $("#txtGSTPercentageName");
    var chkStatus = $("#chkstatus");


    if (txtGSTPercentageName.val() == "") {
        ShowModel("Alert", "Please GST Percentage Name")
        txtGSTPercentageName.focus();
        return false;
    }

    var gstStatus = true;
    if (chkStatus.prop("checked") == true) { gstStatus = true; }
    else { gstStatus = false; }

    var gSTPercentViewModel = {
        GSTPercentageID: hdnGSTPercentageID.val(),
        GSTPercentageName: txtGSTPercentageName.val(),
        GSTPercentageStatus: gstStatus
    };

    var requestData = { gSTPercentViewModel: gSTPercentViewModel };
    $.ajax({
        url: "../GSTPercent/AddEditGSTPercent",
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
                SearchGSTPercentage();
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
    $("#hdnGSTPercentageID").val("0");
    $("#txtGSTPercentageName").val("");
    $("#chkstatus").prop("checked", true);
}

function SearchGSTPercentage() {
    var requestData = {};
    $.ajax({
        url: "../GSTPercent/GetGSTPercentageList",
        data: requestData,
        dataType: "html",
        type: "GET",
        error: function (err) {
            $("#divGSTPercentList").html("");
            $("#divGSTPercentList").html(err);
        },
        success: function (data) {
            $("#divGSTPercentList").html("");
            $("#divGSTPercentList").html(data);

        }
    });
}

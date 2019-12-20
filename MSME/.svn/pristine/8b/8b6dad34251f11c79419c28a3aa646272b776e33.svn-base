$(document).ready(function () {
    //$('#tblCompanyList').paging({ limit: 2 });
    SearchProductSubChildGroup();
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
    $("#txtProductSubChildGroupName").val("");
    $("#txtProductSubChildGroupCode").val("");
    $("#ddlStatus").val("");
}

function SearchProductSubChildGroup() {
   
    var txtProductSubChildGroupName = $("#txtProductSubChildGroupName");
    var txtProductSubChildGroupCode = $("#txtProductSubChildGroupCode");
   // var hdnProductMainGroupId = $("#hdnProductMainGroupId");
    var ddlStatus = $("#ddlStatus");
    var requestData = {
        productSubChildGroupName: txtProductSubChildGroupName.val().trim(),
        productSubChildGroupCode: txtProductSubChildGroupCode.val().trim(),
        Status: ddlStatus.val()
    };

    $.ajax({ 
        url: "../ProductSubChildGroup/GetProductSubChildGroupList",
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

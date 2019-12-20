$(document).ready(function () {
    //$('#tblCompanyList').paging({ limit: 2 });
    SearchGLSubGroup();
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
    $("#txtGLSubGroupName").val("");
    $("#txtSequenceNo").val("");
    $("#ddlGLMainGroupId").val("0");
    $("#ddlStatus").val("");
}


BindGLMainGroupList();

function BindGLMainGroupList() {
    $.ajax({
        type: "GET",
        url: "../GLSubGroup/GetGLMainGroupList",
        data: "{}",
        dataType: "json",
        asnc: false,
        success: function (data) {
            $("#ddlGLMainGroupId").append($("<option></option>").val(0).html("-Select GLMainGroup-"));
            $.each(data, function (i, item) {

                $("#ddlGLMainGroupId").append($("<option></option>").val(item.GLMainGroupId).html(item.GLMainGroupName));
            });
        },
        error: function (Result) {
            $("#ddlGLMainGroupId").append($("<option></option>").val(0).html("-Select GLMainGroup-"));
        }
    });
}


function SearchGLSubGroup() {
    var txtGLSubGroupName = $("#txtGLSubGroupName");
    var txtSequenceNo = $("#txtSequenceNo");
    var ddlGLMainGroupId = $("#ddlGLMainGroupId");
    var hdnGLSubGroupId = $("#hdnGLSubGroupId");
    var ddlStatus = $("#ddlStatus");
    var requestData = {
        GLSubGroupName: txtGLSubGroupName.val().trim(),
        SequenceNo: txtSequenceNo.val().trim(),
        GLMainGroupId: ddlGLMainGroupId.val(),
        Status: ddlStatus.val()
    };

    $.ajax({
        url: "../GLSubGroup/GetGLSubGroupList",
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

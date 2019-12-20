(document).ready(function () {
    SearchUserRegistration();
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
    $("#txtFirstNameName").val("");
    $("#txtEmail").val("");
    $("#txtMobileNo").val("");
    $("#ddlKYCCode").val("");
}
function SearchUserRegistration() {
    var txtFirstNameName = $("#txtFirstNameName");
    var ddlKYCCode = $("#ddlKYCCode");
    var txtMobileNo = $("#txtMobileNo");
    var txtEmail = $("#txtEmail");
    var requestData = { firstName: txtFirstNameName.val().trim(), email: txtEmail.val().trim(), contactNo: txtMobileNo.val().trim(), kycCode: ddlKYCCode.val(), };
    $.ajax({
        url: "../UserRegister/GetUserRegistrationList",
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

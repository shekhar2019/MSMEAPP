﻿$(document).ready(function () {
    BindStateList(0);
    BindDocumentTypeList();


   
   
   
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

function checkPhone(el) {
    var ex = /^[0-9]+\-?[0-9]*$/;
    if (ex.test(el.value) == false) {
        el.value = el.value.substring(0, el.value.length - 1);
    }

}

function SendMail() {

     var txtmailid = $("#txtEmail");
    var txtFirstName = $("#txtFirstName");
    var txtLastName = $("#txtFirstName");
 
     var otp = generateOTP();
    $("#hdnOtp").val(otp);
    var requestData = { UserName: txtFirstName.val().trim() + "  " + txtLastName.val().trim(), UserEmail: txtmailid.val().trim(), OTP:otp };
    $.ajax({
        url: "../Home/SendOTPMail",
        type: "POST",
        dataType: "json",
        data: requestData,
        success: function (data) {
            if (data.status == "SUCCESS") {
              alert("OTP mail has been send please check your inbox & spam .");
            }
            else {
                alert(data.message)
            }
        },
        error: function (err) {
            alert(err)
        }
    });
}

function SendConfirmationMail(Email, Name, UserCode ) {


    var requestData = { UserName: Name, UserEmail: Email, UserCode: UserCode };
    $.ajax({
        url: "../Home/SendConfirmationMail",
        type: "POST",
        dataType: "json",
        data: requestData,
        success: function (data) {
            if (data.status == "SUCCESS") {
                // alert(data.message);
            }
            else {
                alert(data.message)
            }
        },
        error: function (err) {
            alert(err)
        }
    });
}

function generateOTP() {

    var digits = '0123456789abcdefghijklmnopqrstuvwxyz';
    var otpLength = 6;

    var otp = '';

    for (let i = 1; i <= otpLength; i++) {

        var index = Math.floor(Math.random() * (digits.length));

        otp = otp + digits[index];
    }

    return otp
}

function SaveData() {
    var txtFirstName = $("#txtFirstName");
    var txtLastName = $("#txtLastName");
    var txtEmail = $("#txtEmail");
    var txtContactNo = $("#txtContactNo");
    var txtAlternateContactNo = $("#txtAlternateContactNo");
    var txtWhatsaapPhone = $("#txtWhatsaapPhone");
    var txtFaxNo = $("#txtFaxNo");
    var ddlState = $("#ddlState");
    var txtCity = $("#txtCity");
    var txtSubCity = $("#txtSubCity");
    var txtLandmark = $("#txtLandmark");
    var txtBuildingNo = $("#txtBuildingNo");
    var txtPINCode = $("#txtPINCode");
    var txtGSTNo = $("#txtGSTNo");
    var txtPANNo = $("#txtPANNo");
    var txtUdyogAadhaarNo = $("#txtUdyogAadhaarNo");
    var txtUserName = $("#txtUserName");
    var txtPassword = $("#txtPassword");
    var txtConfirmPassword = $("#txtConfirmPassword");
    var txtpasswordotp = $("#txtpasswordotp");
    var txtCompanyName = $("#txtCompanyName");
    var ddlUserType = $("#ddlUserType");
    var ddlDocumentType = $("#ddlDocumentType");
    var txtUserTypeCode = $("#txtUserTypeCode");
    var hdnOtp = $("#hdnOtp");

    if (ddlUserType.val() == "0") {
        ShowModel("Alert", "Select User Type");
        ddlUserType.focus();
        return false;
    }

    if (txtFirstName.val() == "") {
        ShowModel("Alert", "Enter Your Name");
        txtFirstName.focus();
        return false;
    }
    //if (txtLastName.val() == "") {
    //    ShowModel("Alert","Enter Your Last Name");
    //    txtLastName.focus();
    //    return false;
    //}
    if (txtEmail.val() == "") {
        ShowModel("Alert","Enter Your Email");
        txtEmail.focus();
        return false;
    }
    if (txtContactNo.val() == "") {
        ShowModel("Alert","Enter Your Contact No");
        txtContactNo.focus();
        return false;
    }
    //if (txtAlternateContactNo.val() == "") {
    //    ShowModel("Alert","Enter your Alternate Contact No");
    //    txtAlternateContactNo.focus();
    //    return false;
    //}
    //if (txtWhatsaapPhone.val() == "") {
    //    ShowModel("Alert","Enter your What'saap No");
    //    txtWhatsaapPhone.focus();
    //    return false;
    //}
    
    if (ddlState.val() == "0") {
        ShowModel("Alert", "Select Your State");
        ddlState.focus();
        return false;
    }
    if (txtCity.val() == "") {
        ShowModel("Alert", "Enter your City");
        txtCity.focus();
        return false;
    }
    //if (txtSubCity.val() == "") {
    //    ShowModel("Alert", "Enter your Sub City");
    //    txtSubCity.focus();
    //    return false;
    //}
    //if (txtLandmark.val() == "") {
    //    ShowModel("Alert", "Enter your Landmark");
    //    txtLandmark.focus();
    //    return false;
    //}

    if (txtPassword.val() == "") {
        ShowModel("Alert", "Enter your Password");
        txtPassword.focus();
        return false;
    }

    if (txtConfirmPassword.val() == "") {
        ShowModel("Alert", "Enter confirm Password");
        txtConfirmPassword.focus();
        return false;
    }

    if (txtpasswordotp.val() == "") {
        ShowModel("Alert", "Enter OTP ");
        txtpasswordotp.focus();
        return false;
    }
    if (txtPassword.val() != txtConfirmPassword.val() ) {
        ShowModel("Alert", "Password and confirm Password didn't match");
        txtPassword.focus();
        return false;
    }
    if (txtpasswordotp.val().trim() != hdnOtp.val().trim()) {
        ShowModel("Alert", "OTP not valid");
        txtpasswordotp.focus();
        return false;
    }
    if (txtPINCode.val() == "")
    {
        ShowModel("Alert", "Enter Pin Code");
        txtPINCode.focus();
        return false;
    }
    //var logoStatus = true;
    //if (chkStatus.prop("checked") == true)
    //{ logoStatus = true; }
    //else
    //{ logoStatus = false; }

    var userRegistration = {
        FirstName: txtFirstName.val().trim(),
        LastName: txtLastName.val().trim(),
        Email: txtEmail.val().trim(),
        ContactNo: txtContactNo.val().trim(),
        AlternateContactno: txtAlternateContactNo.val().trim(),
        WhatsaapPhone: txtWhatsaapPhone.val().trim(),
        Fax: txtFaxNo.val().trim(),
        StateId: ddlState.val(),
        City: txtCity.val().trim(),
        SubCity: txtSubCity.val().trim(),
        Landmark: txtLandmark.val().trim(),
        BuildingNo: txtBuildingNo.val().trim(),
        PINCode: txtPINCode.val(),
        GSTNo: txtGSTNo.val(),
        PANNo: txtPANNo.val(),
        UdyogAadhaarNo: txtUdyogAadhaarNo.val(),
        KYCCode: ddlUserType.val(),
        UserName: txtEmail.val(),
        Password: txtPassword.val(),
        CompanyName: txtCompanyName.val(),
        UserCode: txtUserTypeCode.val()
   };
    var userSupportingDocument = {
        DocumentName: ddlDocumentType.text(),
        DocumentTypeId:ddlDocumentType.val()
    };
    var requestData = { userRegistrationVM: userRegistration, userSupportingDocumentVM: userSupportingDocument };
    $.ajax({
        url: "../UserRegister/Index",
        cache: false,
        type: "POST",
        dataType: "json",
        data: JSON.stringify(requestData),
        contentType: 'application/json',
        success: function (data) {
            if (data.status == "SUCCESS") {
                ShowModel("Alert", data.message);

                SendConfirmationMail(data.Email, data.Name, data.UserCode);
                ClearFields();
                setTimeout(
                                function () {
                                    window.location = "../UserRegister";
                                }, 2000);


                if ($("#hdnRemoveImage").val() == 1) {
                    RemoveImage();
                }
              

                $("#btnSave").show();
                $("#btnUpdate").hide();
            }
            else {
                // ShowModel("Error", data.message)
                alert(data.message);
            }
        },
        error: function (err) {
            //ShowModel("Error", err)
            alert(err);
        }
    });

}

function checkDec(el) {
    var ex = /^[0-9]+\.?[0-9]*$/;
    if (ex.test(el.value) == false) {
        el.value = el.value.substring(0, el.value.length - 1);
    }

}
function checkDec(el) {
    var ex = /^[0-9]+\.?[0-9]*$/;
    if (ex.test(el.value) == false) {
        el.value = el.value.substring(0, el.value.length - 1);
    }

}
function BindStateList(stateId) {
    var countryId = 1;//$("#ddlCCountry option:selected").val();
    $("#ddlCState").val(0);
    $("#ddlCState").html("");
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

function ShowModel(headerText, bodyText) {
    $("#alertModel").modal();
    $("#modelHeader").html(headerText);
    $("#modelText").html(bodyText);
}
function ClearFields() {
    $("#ddlUserType").val("0");
    $("#txtFirstName").val("");
    $("#txtLastName").val("");
    $("#txtEmail").val("");
    $("#txtContactNo").val("");
    $("#txtAlternateContactNo").val("");
    $("#txtWhatsaapPhone").val("");
    $("#txtFaxNo").val("");
    $("#ddlState").val("0");
    $("#txtCity").val("");
    $("#txtSubCity").val("");

    $("#txtLandmark").val("");
    $("#txtBuildingNo").val("");
    $("#txtGSTNo").val("");
    $("#txtPANNo").val("");
    $("#txtUdyogAadhaarNo").val("");
    $("#hdnOtp").val("0");
    $("#txtpasswordotp").val("");
    $("#txtUserTypeCode").val("");

}




function ConfirmRemoveImage() {
    if (confirm("Do you want to remove selected Image?")) {
        $("#hdnRemoveImage").val(1);
        document.getElementById('FileUpload1').value = "";
        document.getElementById('imgUserPic').src = "";
        $("#btnRemoveImg").hide();
    }
}

function SearchAboutus()
{
   var requestData = {};
    $.ajax({
        url: "../Aboutus/GetAboutUsList",
        data: requestData,
        dataType: "html",
        type: "GET",
        error: function (err) {
            $("#divAboutusList").html("");
            $("#divAboutusList").html(err);
        },
        success: function (data) {
            $("#divAboutusList").html("");
            $("#divAboutusList").html(data);

        }
    });
}

function EditRow(obj) {

    var row = $(obj).closest("tr");
    var aboutusId = $(row).find("#hdnAboutusId").val();
    var aboutusTitle = $(row).find("#hdnAboutusTitle").val();
    var description = $(row).find("#hdnDescription").val();
    var imageUrl = $(row).find("#hdnImageUrl").val();
    var aboutStatus = $(row).find("#hdnAboutStatus").val();
  

    $("#hdnAboutusId").val(aboutusId);
    $("#txtAboutusTitle").val(aboutusTitle);
    $("#txtEditor").Editor("setText", description);
    $("#imgUserPic").prop('src', "../Images/AboutUs/" + imageUrl)
                .width(70)
                .height(70);

    if (aboutStatus == 'true') {
        $("#chkstatus").attr("checked", true);
    }
    else {
        $("#chkstatus").attr("checked", false);
    }

    $("#btnSave").hide();
    $("#btnUpdate").show();
}

function BindDocumentTypeList() {
    $.ajax({
        type: "GET",
        url: "../PO/GetDocumentTypeList",
        data: "{}",
        dataType: "json",
        asnc: false,
        success: function (data) {
            $("#ddlDocumentType").append($("<option></option>").val(0).html("-Select Document Type-"));
            $.each(data, function (i, item) {

                $("#ddlDocumentType").append($("<option></option>").val(item.DocumentTypeId).html(item.DocumentTypeDesc));
            });
        },
        error: function (Result) {
            $("#ddlDocumentType").append($("<option></option>").val(0).html("-Select Document Type-"));
        }
    });
}

function ShowImagePreview(input)
{
    var fname = input.value;
    var ext = fname.split(".");
    var x = ext.length;
    var extstr = ext[x - 1].toLowerCase();
    if (extstr == 'jpg' || extstr == 'jpeg' || extstr == 'png' || extstr == 'gif') {

    }
    else {
        alert("File doesnt match png, jpg or gif");
        input.focus();
        input.value = "";
        return false;
    }
    if (typeof (FileReader) != "undefined") {
        if (input.files && input.files[0]) {
            if (input.files[0].name.length < 1) {
            }
            else if (input.files[0].size > 50000000) {
                input.files[0].name.length = 0;
                alert("File is too big");
                input.value = "";
                return false;
            }
            else if (input.files[0].type != 'image/png' && input.files[0].type != 'image/jpg' && !input.files[0].type != 'image/gif' && input.files[0].type != 'image/jpeg') {
                input.files[0].name.length = 0;
                alert("File doesnt match png, jpg or gif");
                input.value = "";
                return false;
            }
            var reader = new FileReader();
            reader.onload = function (e) {
                $("#imgUserPic").prop('src', e.target.result)
                    .width(70)
                    .height(70);

            };
            reader.readAsDataURL(input.files[0]);

            if ($("#FileUpload1").val() != '') {
                $("#btnRemoveImg").show();
            }
            else {
                $("#btnRemoveImg").hide();
            }
        }
    }
    else {

        alert("This browser does not support FileReader.");
        input.value = "";
        //return false;
    }
}

function ConfirmRemoveImage() {
    if (confirm("Do you want to remove selected Image?")) {
        $("#hdnRemoveImage").val(1);
        document.getElementById('FileUpload1').value = "";
        document.getElementById('imgUserPic').src = "";
        $("#btnRemoveImg").hide();
    }
}

function SaveUserImage(userId) {
    if (parseInt(userId) <= 0) {
        ShowModel("Alert", "User Id not available")
        return false;
    }
    if (window.FormData !== undefined) {
        var uploadfile = document.getElementById('FileUpload1');
        var fileData = new FormData();
        if (uploadfile.value != '') {

            var fileUpload = $("#FileUpload1").get(0);
            var files = fileUpload.files;

            if (uploadfile.files[0].size > 50000000) {
                uploadfile.files[0].name.length = 0;
                ShowModel("Alert", "File is too big")
                uploadfile.value = "";
                return false;
            }

            for (var i = 0; i < files.length; i++) {
                fileData.append(files[i].name, files[i]);
            }
        }
        fileData.append("UserId", userId);

    } else {

        ShowModel("Alert", "FormData is not supported.")
    }

    $.ajax({
        url: "../UserRegister/UpdateUserPic",
        type: "POST",
        contentType: false, // Not to set any content header  
        processData: false, // Not to process data  
        data: fileData,
        error: function () {
            ShowModel("Alert", "An error occured")
            return;
        },
        success: function (result) {
            if (result.status == "SUCCESS") {

                document.getElementById('FileUpload1').value = "";
                document.getElementById('imgUserPic').src = "";
            }
            else {
                ShowModel("Alert", result.message);
            }
        }
    });
}
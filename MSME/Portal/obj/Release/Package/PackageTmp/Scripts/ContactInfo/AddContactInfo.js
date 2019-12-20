$(document).ready(function () {
    var hdnAccessMode = $("#hdnAccessMode");
    var hdnAboutusId = $("#hdnId");
    if (hdnAboutusId.val() != "" && hdnAboutusId.val() != "0" && hdnAccessMode.val() != "" && hdnAccessMode.val() != "0") {
       
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
    $("#txtContactTitle").focus();
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


function checkDec(el) {
    var ex = /^[0-9]+\.?[0-9]*$/;
    if (ex.test(el.value) == false) {
        el.value = el.value.substring(0, el.value.length - 1);
    }

}

function SaveData() {
    var hdnId = $("#hdnId");
    var txtContactTitle = $("#txtContactTitle");
    var txtPhone = $("#txtPhone");
    var txtEmail = $("#txtEmail");
    var txtWebsite = $("#txtWebsite");
    var txtAddress = $("#txtAddress");
    
   
    var chkStatus = $("#chkStatus");
   
    if (txtContactTitle.val() == "") {
        ShowModel("Alert", "Please Enter Contact Title.")
        txtContactTitle.focus();
        return false;
    }
    if (txtPhone.val() == "") {
        ShowModel("Alert", "Please Enter Phone No.")
        txtPhone.focus();
        return false;
    }
    if (txtEmail.val() == "") {
        ShowModel("Alert", "Please Enter Email.")
        txtEmail.focus();
        return false;
    }
    if (txtWebsite.val() == "") {
        ShowModel("Alert", "Please Enter Website.")
        txtWebsite.focus();
        return false;
    }
    if (txtAddress.val() == "") {
        ShowModel("Alert", "Please Enter Address.")
        txtAddress.focus();
        return false;
    }

    var contactusStatus = true;
    if (chkStatus.prop("checked") == true)
    { contactusStatus = true; }
    else
    { contactusStatus = false; }

    var contactInfoViewModel = {
        ContactInfoId: hdnId.val(),
        ContactTitle: txtContactTitle.val().trim(),
        Phone: txtPhone.val().trim(),
        Email: txtEmail.val().trim(),
        Website: txtWebsite.val().trim(),
        Address:txtAddress.val().trim(),
        ContactInfoStatus: contactusStatus
    };
    var accessMode = 1;//Add Mode
    if (hdnId.val() != null && hdnId.val() != 0) {
        accessMode = 2;//Edit Mode
    }
    var requestData = { contactInfoViewModel: contactInfoViewModel };
    $.ajax({
        url: "../ContactInfo/AddEditContactInfo?AccessMode=" + accessMode + "",
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


    GetContactInfoList();
}
function ShowModel(headerText, bodyText) {
    $("#alertModel").modal();
    $("#modelHeader").html(headerText);
    $("#modelText").html(bodyText);
}
function ClearFields() {
    $("#hdnId").val("0");
    $("#txtContactTitle").val("");
    $("#txtPhone").val("");
    $("#txtEmail").val("");
    $("#txtWebsite").val("");
    $("#txtAddress").val("");
  
    $("#chkstatus").prop("checked", true);


 
   
}
function ShowImagePreview(input) {
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

function SaveAboutusImage(aboutusId) {
    if (parseInt(aboutusId) <= 0) {
        ShowModel("Alert", "Logo ID not available")
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
        fileData.append("aboutusId", aboutusId);

    } else {

        ShowModel("Alert", "FormData is not supported.")
    }

    $.ajax({
        url: "../Aboutus/UpdateAboutUsImage",
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

function GetContactInfoList() {

    var requestData = {};
    $.ajax({
        url: "../ContactInfo/GetContactInfoList",
        data: requestData,
        dataType: "html",
        type: "GET",
        error: function (err) {
            $("#divContactInfoList").html("");
            $("#divContactInfoList").html(err);
        },
        success: function (data) {
            $("#divContactInfoList").html("");
            $("#divContactInfoList").html(data);

        }
    });
}

function EditRowAboutus(obj) {

    var row = $(obj).closest("tr");
    var aboutusId = $(row).find("#hdnAboutusId").val();
    var aboutusTitle = $(row).find("#hdnAboutusTitle").val();
    var description = $(row).find("#hdnDescription").val();
    var imageUrl = $(row).find("#hdnImageUrl").val();
    var aboutStatus = $(row).find("#hdnAboutStatus").val();
  

    var hdnId = $("#hdnId");
    var txtContactTitle = $("#txtContactTitle");
    var txtPhone = $("#txtPhone");
    var txtEmail = $("#txtEmail");
    var txtWebsite = $("#txtWebsite");
    var txtAddress = $("#txtAddress");



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

function EditRow(obj) {

    var row = $(obj).closest("tr");
    var hdnId = $(row).find("#hdnId").val();
    var ContactTitle = $(row).find("#hdnContactTitle").val();
    var Phone = $(row).find("#hdnPhone").val();
    var Email = $(row).find("#hdnEmail").val();
    var Website = $(row).find("#hdnWebsite").val();
    var Address = $(row).find("#hdnAddress").val();
    var contactInfoStatus = $(row).find("#hdnContactInfoStatus").val();
    $("#hdnId").val(hdnId);
    $("#txtContactTitle").val(ContactTitle);
    $("#txtPhone").val(Phone);
    $("#txtEmail").val(Email);
    $("#txtWebsite").val(Website);
    $("#txtAddress").val(Address);
    if (contactInfoStatus == 'true') {
        $("#chkstatus").attr("checked", true);
    }
    else {
        $("#chkstatus").attr("checked", false);
    }

    $("#btnSave").hide();
    $("#btnUpdate").show();
}

function RemoveContactInfo(contactInfoId) {
    if (confirm("Do you want to remove selected Contact Information?")) {
        $.ajax({
            type: "GET",
            asnc: false,
            url: "../ContactInfo/RemoveContactInfo",
            data: { contactInfoId: contactInfoId },
            dataType: "json",
            success: function (data) {
                if (data.status == "SUCCESS") {
                    // ShowModel("Alert", data.message);
                    GetContactInfoList();
                }
            },
            error: function (Result) {
                ShowModel("Alert", "Problem in Request");
            }


        });
    }
}

function SendData() {
    var txtName = $("#txtName");
    var txtEmail = $("#txtEmail");
    var txtMobile = $("#txtMobile");
    var txtMessage = $("#txtMessage");
   
    if (txtName.val() == "") {
        ShowModel("Alert", "Please Enter Name")
        txtName.focus();
        return false;
    }
    if (txtEmail.val() == "") {
        ShowModel("Alert", "Please Enter Email")
        txtEmail.focus();
        return false;
    }
    if (txtMobile.val() == "")
    {
        ShowModel("Alert", "Please Enter Mobile.")
        txtMobile.focus();
        return false;
    }
    if (txtMessage.val() == "") {
        ShowModel("Alert", "Please Enter Message.")
        txtMessage.focus();
        return false;
    }

   
    var requestData = { customerName: txtName.val(), customerMail: txtEmail.val(), customerMobile: txtMobile.val(), query: txtMessage.val() };
    $.ajax({
        url: "../ContactUs/SendEnquiry",
        cache: false,
        type: "POST",
        dataType: "json",
        data: JSON.stringify(requestData),
        contentType: 'application/json',
        success: function (data) {
            if (data.status == "SUCCESS") {
                ShowModel("Alert", data.message);
                $("#txtName").val("");
                $("#txtEmail").val("");
                $("#txtMobile").val("");
                $("#txtMessage").val("");
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
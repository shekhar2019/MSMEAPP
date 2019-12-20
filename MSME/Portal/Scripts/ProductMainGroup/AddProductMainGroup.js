$(document).ready(function () {
    var hdnAccessMode = $("#hdnAccessMode");
    var hdnProductMainGroupId = $("#hdnProductMainGroupId");
    if (hdnProductMainGroupId.val() != "" && hdnProductMainGroupId.val() != "0" && hdnAccessMode.val() != "" && hdnAccessMode.val() != "0") {
        GetProductMainGroupDetail(hdnProductMainGroupId.val());
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
    $("#txtProductMainGroupName").focus();
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


function GetProductMainGroupDetail(productmaingroupId) {
    $.ajax({
        type: "GET",
        asnc: false,
        url: "../ProductMainGroup/GetProductMainGroupDetail",
        data: { productmaingroupId: productmaingroupId },
        dataType: "json",
        success: function (data) {
            $("#txtProductMainGroupName").val(data.ProductMainGroupName);
            $("#txtProductMainGroupCode").val(data.ProductMainGroupCode);
            $("#txtProductMainGroupDesc").val(data.ProductMainGroupDesc);
            document.getElementById('imgUserPic').src = "../Images/ProductMainGroupImages/" + data.ProductMainGroupImageUrl;
            if (data.ProductMainGroup_Status == true) {
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
    var txtProductMainGroupName = $("#txtProductMainGroupName");
    var txtProductMainGroupCode = $("#txtProductMainGroupCode");
    var hdnProductMainGroupId = $("#hdnProductMainGroupId");
    var chkstatus = $("#chkstatus").is(':checked') ? true : false;
    var txtProductMainGroupDesc = $("#txtProductMainGroupDesc");
    var imagePath = "";

    if (txtProductMainGroupName.val().trim() == "") {
        ShowModel("Alert", "Please enter Product Name")
        txtProductMainGroupName.focus();
        return false;
    }
    if (txtProductMainGroupCode.val().trim() == "") {
        ShowModel("Alert", "Please enter Product Code")
        txtProductMainGroupCode.focus();
        return false;
    }

    var productmaingroupViewModel = {
        ProductMainGroupId: hdnProductMainGroupId.val(),
        productMainGroupName: txtProductMainGroupName.val().trim(),
        productMainGroupCode: txtProductMainGroupCode.val().trim(),
        ProductMainGroupDesc: txtProductMainGroupDesc.val().trim(),
        ProductMainGroupImageUrl: imagePath,
        ProductMainGroup_Status: chkstatus
    };
    var requestData = { productmaingroupViewModel: productmaingroupViewModel };
    $.ajax({
        url: "../ProductMainGroup/AddEditProductMainGroup",
        cache: false,
        type: "POST",
        dataType: "json",
        data: JSON.stringify(requestData),
        contentType: 'application/json',
        success: function (data) {
            if (data.status == "SUCCESS") {
                ShowModel("Alert", data.message);
                setTimeout(
                 function () {
                     SaveProductMainGroupImage(data.trnId);
                 }, 2000);

                if ($("#hdnRemoveImage").val() == 1) {
                    RemoveImage();
                }
                ClearFields(true);
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
function ClearFields(preventReload) {
    if (preventReload) {
        $("#hdnProductMainGroupId").val("0");
        $("#txtProductMainGroupName").val("");
        $("#txtProductMainGroupCode").val("");
        $("#txtProductMainGroupDesc").val("");
        $("#chkstatus").prop("checked", true);
        $("#btnRemoveImg").hide();
    }
    else {
        window.location.href = window.location.href;
    }
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

function SaveProductMainGroupImage(productMainGroupId) {
    if (parseInt(productMainGroupId) <= 0) {
        ShowModel("Alert", "Parent Class Id not available")
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
        fileData.append("productMainGroupId", productMainGroupId);

    } else {

        ShowModel("Alert", "FormData is not supported.")
    }

    $.ajax({
        url: "../ProductMainGroup/UpdateProductMainGroupImage",
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

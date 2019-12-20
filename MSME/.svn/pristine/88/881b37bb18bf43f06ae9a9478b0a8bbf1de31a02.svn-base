
$(document).ready(function () {
    BindProductMainGroupList();
    var hdnAccessMode = $("#hdnAccessMode");
    var hdnProductSubGroupId = $("#hdnProductSubGroupId");
    if (hdnProductSubGroupId.val() != "" && hdnProductSubGroupId.val() != "0" && hdnAccessMode.val() != "" && hdnAccessMode.val() != "0") {
        GetProductSubGroupDetail(hdnProductSubGroupId.val());
        if (hdnAccessMode.val() == "3") {
            $("#btnSave").hide();
            $("#btnUpdate").hide();
            $("#btnReset").hide();
            $("input").attr('readOnly', true);
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
    $("#txtProductName").focus();



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


function BindProductMainGroupList() {
    $.ajax({
        type: "GET",
        url: "../Product/GetProductMainGroupList",
        data: "{}",
        dataType: "json",
        asnc: false,
        success: function (data) {
            $("#ddlProductMainGroup").append($("<option></option>").val(0).html("-Select Parent Class-"));
            $.each(data, function (i, item) {
                $("#ddlProductMainGroup").append($("<option></option>").val(item.ProductMainGroupId).html(item.ProductMainGroupName));
            });
        },
        error: function (Result) {
            $("#ddlProductMainGroup").append($("<option></option>").val(0).html("-Select Parent Class-"));
        }
    });
}



function GetProductSubGroupDetail(productsubgroupId) {
    $.ajax({
        type: "GET",
        asnc: false,
        url: "../ProductSubGroup/GetProductSubGroupDetail",
        data: { productsubgroupId: productsubgroupId },
        dataType: "json",
        success: function (data) {
            $("#txtProductSubGroupName").val(data.ProductSubGroupName);
            $("#txtProductSubGroupCode").val(data.ProductSubGroupCode);
            $("#ddlProductMainGroup").val(data.ProductMainGroupId);
            $("#txtProductSubGroupDesc").val(data.ProductSubGroupDesc);
            document.getElementById('imgUserPic').src = "../Images/ProductSubGroupImages/" + data.ProductSubGroupImageUrl;
            if (data.ProductSubGroup_Status == true) {
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
    var txtProductSubGroupName = $("#txtProductSubGroupName");
    var txtProductSubGroupCode = $("#txtProductSubGroupCode");
    var ddlProductMainGroup = $("#ddlProductMainGroup");
    var hdnProductSubGroupId = $("#hdnProductSubGroupId");
    var chkstatus = $("#chkstatus").is(':checked') ? true : false;
    var txtProductSubGroupDesc = $("#txtProductSubGroupDesc");
    var imageUrl = "";

    if (txtProductSubGroupName.val().trim() == "") {
        ShowModel("Alert", "Please Enter Category Name");
        txtProductSubGroupName.focus();
        return false;
    }
    if (txtProductSubGroupCode.val().trim() == "") {
        ShowModel("Alert", "Please Enter Sub Category Code");
        txtProductSubGroupCode.focus();
        return false;
    }

    var productsubgroupViewModel = {
        ProductSubGroupId: hdnProductSubGroupId.val(),
        productSubGroupName: txtProductSubGroupName.val().trim(),
        productSubGroupCode: txtProductSubGroupCode.val().trim(),
        productMainGroupId: ddlProductMainGroup.val(),
        ProductSubGroupDesc: txtProductSubGroupDesc.val().trim(),
        ProductSubGroupImageUrl: imageUrl,
        ProductSubGroup_Status: chkstatus
    };
    var requestData = { productsubgroupViewModel: productsubgroupViewModel };
    $.ajax({
        url: "../ProductSubGroup/AddEditProductSubGroup",
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
                   SaveProductSubGroupImage(data.trnId);
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
        $("#hdnProductSubGroupId").val("0");
        $("#txtProductSubGroupName").val("");
        $("#txtProductSubGroupCode").val("");
        $("#ddlProductMainGroup").val("");
        $("#chkstatus").prop("checked", true);
        $("#txtProductSubGroupDesc").val("");
        $("#btnRemoveImg").hide();
    }
    else
        window.location.href = window.location.href;

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

function SaveProductSubGroupImage(productSubGroupId) {
    if (parseInt(productSubGroupId) <= 0) {
        ShowModel("Alert", "Parent Category Id not available")
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
        fileData.append("productSubGroupId", productSubGroupId);

    } else {

        ShowModel("Alert", "FormData is not supported.")
    }

    $.ajax({
        url: "../ProductSubGroup/UpdateProductSubGroupImage",
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
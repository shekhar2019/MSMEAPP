
$(document).ready(function () {

    BindProductMainGroupList();

    $("#ddlProductSubGroup").append($("<option></option>").val(0).html("-Select Sub Group-"));
    var hdnAccessMode = $("#hdnAccessMode");
    var hdnProductSubChildGroupId = $("#hdnProductSubChildGroupId");
    if (hdnProductSubChildGroupId.val() != "" && hdnProductSubChildGroupId.val() != "0" && hdnAccessMode.val() != "" && hdnAccessMode.val() != "0") {
        GetProductSubChildGroupDetail(hdnProductSubChildGroupId.val());
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
    // $("#txtProductMainGroupName").focus();

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

function checkDec(el) {
    var ex = /^[0-9]+\.?[0-9]*$/;
    if (ex.test(el.value) == false) {
        el.value = el.value.substring(0, el.value.length - 1);
    }

}
function ValidEmailCheck(emailAddress) {
    var pattern = /^([a-z\d!#$%&'*+\-\/=?^_`{|}~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]+(\.[a-z\d!#$%&'*+\-\/=?^_`{|}~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]+)*|"((([ \t]*\r\n)?[ \t]+)?([\x01-\x08\x0b\x0c\x0e-\x1f\x7f\x21\x23-\x5b\x5d-\x7e\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|\\[\x01-\x09\x0b\x0c\x0d-\x7f\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))*(([ \t]*\r\n)?[ \t]+)?")@(([a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|[a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF][a-z\d\-._~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]*[a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])\.)+([a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|[a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF][a-z\d\-._~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]*[a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])\.?$/i;
    return pattern.test(emailAddress);
};


function BindProductMainGroupList() {
    $.ajax({
        type: "GET",
        url: "../Product/GetProductMainGroupList",
        data: "{}",
        dataType: "json",
        asnc: false,
        success: function (data) {
            $("#ddlProductMainGroup").append($("<option></option>").val(0).html("-Select Main Group-"));
            $.each(data, function (i, item) {
                $("#ddlProductMainGroup").append($("<option></option>").val(item.ProductMainGroupId).html(item.ProductMainGroupName));
            });
        },
        error: function (Result) {
            $("#ddlProductMainGroup").append($("<option></option>").val(0).html("-Select Main Group-"));
        }
    });
}
function BindProductSubGroupList(productSubGroupId) {
    var productMainGroupId = $("#ddlProductMainGroup option:selected").val();
    $("#ddlProductSubGroup").val(0);
    $("#ddlProductSubGroup").html("");
    if (productMainGroupId != undefined && productMainGroupId != "" && productMainGroupId != "0") {
        var data = { productMainGroupId: productMainGroupId };
        $.ajax({
            type: "GET",
            url: "../Product/GetProductSubGroupList",
            data: data,
            asnc: false,
            dataType: "json",
            success: function (data) {
                $("#ddlProductSubGroup").append($("<option></option>").val(0).html("-Select Sub Group-"));
                $.each(data, function (i, item) {
                    $("#ddlProductSubGroup").append($("<option></option>").val(item.ProductSubGroupId).html(item.ProductSubGroupName));
                });
                $("#ddlProductSubGroup").val(productSubGroupId);
            },
            error: function (Result) {
                $("#ddlProductSubGroup").append($("<option></option>").val(0).html("-Select Sub Group-"));
            }
        });
    }
    else {
        $("#ddlProductSubGroup").append($("<option></option>").val(0).html("-Select Sub Group-"));
    }

}



function SaveData() {
    var hdnProductSubChildGroupId = $("#hdnProductSubChildGroupId");
    var txtProductSubChildGroupName = $("#txtProductSubChildGroupName");
    var txtProductSubChildGroupCode = $("#txtProductSubChildGroupCode");
    var ddlProductSubGroup = $("#ddlProductSubGroup");
    var ddlProductMainGroup = $("#ddlProductMainGroup");
    var chkstatus = $("#chkstatus").is(':checked') ? true : false;
    var txtProductSubChildGroupDesc = $("#txtProductSubChildGroupDesc");
    var image = "";
    if (txtProductSubChildGroupName.val().trim() == "") {
        ShowModel("Alert", "Please enter Product Sub Category Name")
        txtProductSubChildGroupName.focus();
        return false;
    }
    if (txtProductSubChildGroupCode.val().trim() == "") {
        ShowModel("Alert", "Please enter Product Sub Category Code")
        txtProductSubChildGroupCode.focus();
        return false;
    }

    var productSubChildGroupViewModel = {
        ProductSubChildGroupId: hdnProductSubChildGroupId.val(),
        ProductSubChildGroupName: txtProductSubChildGroupName.val().trim(),
        ProductSubChildGroupCode: txtProductSubChildGroupCode.val().trim(),
        ProductMainGroupId: ddlProductMainGroup.val(),
        ProductSubGroupId: ddlProductSubGroup.val(),
        ProductSubChildGroupDesc: txtProductSubChildGroupDesc.val().trim(),
        ProductSubChildGroupImageUrl: image,
        ProductSubGroupStatus: chkstatus
    };
    var requestData = { ProductSubChildGroupVM: productSubChildGroupViewModel };
    $.ajax({
        url: "../ProductSubChildGroup/AddEditProductSubChildGroup",
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
                     SaveProductSubChildGroupImage(data.trnId);
                 }, 2000);
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
        $("#hdnProductSubChildGroupId").val("0");
        $("#txtProductSubChildGroupName").val("");
        $("#txtProductSubChildGroupCode").val("");
        $("#ddlProductSubGroup").val("0");
        $("#ddlProductMainGroup").val("0");
        $("#chkStatus").attr("checked", true);
        $("#txtProductSubChildGroupDesc").val("");
        $("#btnRemoveImg").hide();
    }
    else {
        window.location.href = window.location.href;
    }

}

function GetProductSubChildGroupDetail(productSubGroupId) {
    $.ajax({
        type: "GET",
        asnc: false,
        url: "../ProductSubChildGroup/ProductSubChildGroupDetail",
        data: { productSubChildGroupId: productSubGroupId },
        dataType: "json",
        success: function (data) {
            $("#txtProductSubChildGroupName").val(data.ProductSubChildGroupName);
            $("#txtProductSubChildGroupCode").val(data.ProductSubChildGroupCode);
            $("#ddlProductMainGroup").val(data.ProductMainGroupId);
            BindProductSubGroupList(data.ProductSubGroupId);
            $("#ddlProductSubGroup").val(data.ProductSubGroupId)
            $("#txtProductSubChildGroupDesc").val(data.ProductSubChildGroupDesc);
            document.getElementById('imgUserPic').src = "../Images/ProductSubChildGroupImages/" + data.ProductSubChildGroupImageUrl;

            if (data.ProductSubGroupStatus == true) {
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

function ClearText() {
    $("#txtImageTitle").val("");
    $("#txtImageAlt").val("");
    $("#txtImageNavigateUrl").val("");
    $("#FileUpload2").val("");
    $("#txtImageSequence").val("");
    document.getElementById('FileUpload2').value = "";
    document.getElementById('imgUserPic2').src = "";
    $("#btnRemoveImg").hide();
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

function SaveProductSubChildGroupImage(productSubChildGroupId) {
    if (parseInt(productSubChildGroupId) <= 0) {
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
        fileData.append("productSubChildGroupId", productSubChildGroupId);

    } else {

        ShowModel("Alert", "FormData is not supported.")
    }

    $.ajax({
        url: "../ProductSubChildGroup/UpdateProductSubChildGroupImage",
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
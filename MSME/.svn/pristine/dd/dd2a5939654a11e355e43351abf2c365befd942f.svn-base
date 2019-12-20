$(document).ready(function () {
    $("#txtUpdateDate").attr('readOnly', true);
    var hdnAccessMode = $("#hdnId");
    var hdnUpdateId = $("#hdnUpdateId");
   
    if (hdnUpdateId.val() != "" && hdnUpdateId.val() != "0" && hdnAccessMode.val() != "" && hdnAccessMode.val() != "0") {
       
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
   
});
$("#txtUpdateDate").datepicker({
    changeMonth: true,
    changeYear: true,
    dateFormat: 'dd-M-yy',
    yearRange: '-10:+100',
    onSelect: function (selected) {

    }
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
    var hdnUpdateId = $("#hdnUpdateId");
    var txtUpdateTitle = $("#txtUpdateTitle");
    var txtUpdateDate = $("#txtUpdateDate");
    var txtUpdateUrl = $("#txtUpdateUrl");
    var txtEditor = $("#txtEditor");
    var txtImageTitle = $("#txtImageTitle");
    var txtImageNavigateUrl = $("#txtImageNavigateUrl");
    var txtImageUrl = $("#txtImageUrl");
    var txtImageAlt = $("#txtImageAlt");
    var chkStatus = $("#chkstatus");
    var ddlUpdateArea = $("#ddlUpdateArea");

    if (txtUpdateTitle.val().trim() == "") {
        ShowModel("Alert", "Please Enter Title.")
        txtUpdateTitle.focus();
        return false;
    }
    if (txtUpdateDate.val().trim() == "")
    {
        ShowModel("Alert", "Please Select Date");
        txtUpdateDate.focus();
        return false;
    }
    if (txtUpdateUrl.val().trim() == "")
    {
        ShowModel("Alert", "Please Enter Update Url.");
        txtUpdateUrl.focus();
        return false;
    }
    if (txtEditor.Editor("getText").trim() == "") {
        ShowModel("Alert", "Please Enter Description.")
        txtEditor.focus();
        return false;
    }
    if (txtImageTitle.val().trim() == "")
    {
        ShowModel("Alert", "Please Enter Image Title")
        txtImageTitle.focus();
        return false;
    }

    if (ddlUpdateArea.val() == "" || ddlUpdateArea.val() == "0")
    {
        ShowModel("Alert", "Please Select Update Area");
        ddlUpdateArea.focus();
        return false;
    }
    var homeUpdateStatus = true;
    if (chkStatus.prop("checked") == true)
    { homeUpdateStatus = true; }
    else
    { homeUpdateStatus = false; }

    
    var homeUpdateViewModel = {
        UpdateId:hdnUpdateId.val(),
        UpdateTitle: txtUpdateTitle.val(),
        UpdateDate:txtUpdateDate.val(),
        UpdateDec: txtEditor.Editor("getText").trim(),
        UpdateUrl: txtUpdateUrl.val(),
        ImageUrl: "",
        ImageTitile: txtImageTitle.val(),
        ImageNavigateUrl: txtImageNavigateUrl.val(),
        ImageAlt: txtImageAlt.val(),
        UpdateStatus: homeUpdateStatus,
        UpdateArea:ddlUpdateArea.val()
    };
    var accessMode = 1;//Add Mode
    if (hdnUpdateId.val() != null && hdnUpdateId.val() != 0) {
        accessMode = 2;//Edit Mode
    }
    var requestData = { homeUpdateViewModel: homeUpdateViewModel };
    $.ajax({
        url: "../HomeUpdate/AddEditHomeUpdate?AccessMode=" + accessMode + "",
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
                                SaveImage(data.trnId);
                            }, 2000);
                ClearFields();
                setTimeout(
                           function () {
                               SearchHomeUpdate();
                           }, 3000);

                
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
    $("#hdnUpdateId").val("0");
    $("#txtUpdateTitle").val("");
    $("#txtUpdateDate").val($("#hdnCurrentDate").val());

    $("#txtUpdateUrl").val("#");
    $("#txtEditor").Editor("setText", "");
    $("#txtImageTitle").val("");
    $("#txtImageNavigateUrl").val("#");
    $("#txtImageUrl").val("");
    $("#txtImageAlt").val("");
    $("#chkstatus").prop("checked", true);
   
}


function SearchHomeUpdate()
{
   var requestData = {};
    $.ajax({
        url: "../HomeUpdate/GetHomeUpdateList",
        data: requestData,
        dataType: "html",
        type: "GET",
        error: function (err) {
            $("#divHomeUpdateList").html("");
            $("#divHomeUpdateList").html(err);
        },
        success: function (data) {
            $("#divHomeUpdateList").html("");
            $("#divHomeUpdateList").html(data);

        }
    });
}
function SelectDetails(id,title, description, copyrightStatus) {
    $("#hdnId").val(id);
    $("#txtDescription").val(description);
    $("#txtTitle").val(title);
    if (copyrightStatus == "true")
    {
        $("#chkstatus").prop("checked", true);
    }
    else {
        $("#chkstatus").prop("checked", false);
    }

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


function SaveImage(updateId) {
    if (parseInt(updateId) <= 0) {
        ShowModel("Alert", "Upadte Id not available")
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
        fileData.append("updateId", updateId);

    } else {

        ShowModel("Alert", "FormData is not supported.")
    }

    $.ajax({
        url: "../HomeUpdate/UpdateImage",
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

function EditHomeUpdateRow(obj) {

    var row = $(obj).closest("tr");
    var updateId = $(row).find("#hdnUpdateId").val();
    var updateTitle = $(row).find("#hdnUpdateTitle").val();
    var updateDate = $(row).find("#hdnUpdateDate").val();
    var updateDec = $(row).find("#hdnUpdateDec").val();
    var updateUrl = $(row).find("#hdnUpdateUrl").val();

    var imageTitile = $(row).find("#hdnImageTitile").val();
    var imageNavigateUrl = $(row).find("#hdnImageNavigateUrl").val();
    var imageUrl = $(row).find("#hdnImageUrl").val();
    var imageAlt = $(row).find("#hdnImageAlt").val();
    var imageTitle = $(row).find("#hdnImageTitile").val();
    var updateArea = $(row).find("#hdnUpdateArea").val();

    $("#hdnUpdateId").val(updateId);
    $("#txtUpdateTitle").val(updateTitle);
    $("#txtUpdateDate").val(updateDate);
    $("#txtUpdateUrl").val(updateUrl);
    $("#txtEditor").Editor("setText", updateDec);
    $("#ddlUpdateArea").val(updateArea);
    $("#txtImageTitle").val(imageTitile);
    $("#txtImageNavigateUrl").val(imageNavigateUrl);
    $("#txtImageAlt").val(imageAlt);
    document.getElementById('imgUserPic').src ="../Images/HomeUpdate/" + imageUrl;
    $("#btnSave").hide();
    $("#btnUpdate").show();
}

function RemoveHomeUpdate(updateId) {
    if (confirm("Do you want to remove selected Home Update?")) {
        $.ajax({
            type: "GET",
            asnc: false,
            url: "../HomeUpdate/RemoveHomeUpdate",
            data: { updateId: updateId },
            dataType: "json",
            success: function (data) {
                if (data.status == "SUCCESS") {
                    // ShowModel("Alert", data.message);
                    SearchHomeUpdate();
                }
            },
            error: function (Result) {
                ShowModel("Alert", "Problem in Request");
            }


        });
    }
}
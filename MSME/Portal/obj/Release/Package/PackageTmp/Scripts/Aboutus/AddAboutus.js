$(document).ready(function () {
    var hdnAccessMode = $("#hdnAccessMode");
    var hdnAboutusId = $("#hdnAboutusId");
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
    $("#txtLogoTitle").focus();
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


function RemoveLogo(logoId) {
    if (confirm("Do you want to remove selected logo?")) {
        $.ajax({
            type: "GET",
            asnc: false,
            url: "../Logo/RemoveLogo",
            data: { logoId: logoId },
            dataType: "json",
            success: function (data) {
                if (data.status == "SUCCESS") {
                   // ShowModel("Alert", data.message);
                    SearchLogoStatus();
                }
            },
            error: function (Result) {
                ShowModel("Alert", "Problem in Request");
            }


        });
    }
}

function checkDec(el) {
    var ex = /^[0-9]+\.?[0-9]*$/;
    if (ex.test(el.value) == false) {
        el.value = el.value.substring(0, el.value.length - 1);
    }

}

function SaveData() {
    var hdnAboutusId = $("#hdnAboutusId");
    var txtAboutusTitle = $("#txtAboutusTitle");
    var txtEditor = $("#txtEditor");
  
    var chkStatus = $("#chkstatus");
    var imagePath = "";
   

    if (txtAboutusTitle.val().trim() == "") {
        ShowModel("Alert", "Please About us Title")
        txtLogoTitle.focus();
        return false;
    }

    var aboutusStatus = true;
    if (chkStatus.prop("checked") == true)
    { aboutusStatus = true; }
    else
    { aboutusStatus = false; }

    
    var aboutViewModel = {
        AboutusId: hdnAboutusId.val(),
        AboutusTitle: txtAboutusTitle.val().trim(),
        Description:txtEditor.Editor("getText").trim(),
        ImageUrl: imagePath,
        AboutStatus: aboutusStatus
    };
    var accessMode = 1;//Add Mode
    if (hdnAboutusId.val() != null && hdnAboutusId.val() != 0) {
        accessMode = 2;//Edit Mode
    }
    var requestData = { aboutUsViewModel: aboutViewModel };
    $.ajax({
        url: "../Aboutus/AddEditAboutus?AccessMode=" + accessMode + "",
        cache: false,
        type: "POST",
        dataType: "json",
        data: JSON.stringify(requestData),
        contentType: 'application/json',
        success: function (data) {
            if (data.status == "SUCCESS") {
                
                setTimeout(
                                 function () {
                                     SaveAboutusImage(data.trnId);
                                 }, 2000);
                
                
                if ($("#hdnRemoveImage").val() == 1) {
                    RemoveImage();
                }
                ClearFields();
                ShowModel("Alert", data.message);
                $("#btnSave").show();
                $("#btnUpdate").hide();
                SearchAboutus();
                $("html, body").animate({ scrollTop: 600 }, 600);
                return false;
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
    $("#hdnAboutusId").val("0");
    $("#txtAboutusTitle").val("");
    $("#txtEditor").Editor("setText", "");
    $("#chkstatus").prop("checked", true);
    //document.getElementById('FileUpload1').value = "";
    //document.getElementById('imgUserPic').src = "";
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
        ShowModel("Alert", "Aboutus Id not available")
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
    $("html, body").animate({ scrollTop: 0 }, 600);
    return false;
}
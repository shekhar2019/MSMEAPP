$(document).ready(function () {
    var hdnAccessMode = $("#hdnId");
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

    var txtTitle = $("#txtTitle");
    var txtDescription = $("#txtDescription");
  
    var chkStatus = $("#chkstatus");
    
    if (txtTitle.val().trim() == "") {
        ShowModel("Alert", "Please Enter Title.")
        txtTitle.focus();
        return false;
    }

    if (txtDescription.val().trim() == "") {
        ShowModel("Alert", "Please Enter Description.")
        txtDescription.focus();
        return false;
    }

    var copyrightStatus = true;
    if (chkStatus.prop("checked") == true)
    { copyrightStatus = true; }
    else
    { copyrightStatus = false; }

    
    var homeAboutViewModel = {
        Id: hdnId.val(),
        Title: txtTitle.val(),
        Description: txtDescription.val(),
        HomeAboutStatus: copyrightStatus
    };
    var accessMode = 1;//Add Mode
    if (hdnId.val() != null && hdnId.val() != 0) {
        accessMode = 2;//Edit Mode
    }
    var requestData = { homeAboutViewModel: homeAboutViewModel };
    $.ajax({
        url: "../HomeAbout/AddEditHomeAbout?AccessMode=" + accessMode + "",
        cache: false,
        type: "POST",
        dataType: "json",
        data: JSON.stringify(requestData),
        contentType: 'application/json',
        success: function (data) {
            if (data.status == "SUCCESS") {
                ShowModel("Alert", data.message);
               
                ClearFields();
                SearchAboutus();
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
    $("#hdnId").val("0");
    $("#txtTitle").val("");
    $("#txtDescription").val("");
 
    $("#chkstatus").prop("checked", true);
   
}






function SearchAboutus()
{
   var requestData = {};
    $.ajax({
        url: "../HomeAbout/GetHomeAboutList",
        data: requestData,
        dataType: "html",
        type: "GET",
        error: function (err) {
            $("#divHomeAboutList").html("");
            $("#divHomeAboutList").html(err);
        },
        success: function (data) {
            $("#divHomeAboutList").html("");
            $("#divHomeAboutList").html(data);

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
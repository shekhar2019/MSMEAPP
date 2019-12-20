$(document).ready(function () {
    var hdnAccessMode = $("#hdnAccessMode"); 
    var hdnGLMainGroupId = $("#hdnGLMainGroupId");
    if (hdnGLMainGroupId.val() != "" && hdnGLMainGroupId.val() != "0" && hdnAccessMode.val() != "" && hdnAccessMode.val() != "0") {
        GetScheduleDetail(hdnGLMainGroupId.val());
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
    $("#txtGLMainGroupName").focus();
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


function GetScheduleDetail(scheduleId) {
    $.ajax({
        type: "GET",
        asnc: false,
        url: "../Schedule/GetScheduleDetail",
        data: { scheduleId: scheduleId },
        dataType: "json",
        success: function (data) {
            $("#txtGLMainGroupName").val(data.ScheduleName);          
            if (data.Schedule_Status == true) {
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
    var txtScheduleName = $("#txtGLMainGroupName");   
    var hdnScheduleId = $("#hdnGLMainGroupId");
    var chkstatus = $("#chkstatus").is(':checked') ? true : false;

    if (txtScheduleName.val().trim() == "") {
        ShowModel("Alert", "Please enter Schedule Name")
        txtScheduleName.focus();
        return false;
    }
    

    var ScheduleViewModel = {
        
        ScheduleId: hdnScheduleId.val(),
        ScheduleName: txtScheduleName.val().trim(),
       
        Schedule_Status: chkstatus
    };
    var requestData = { ScheduleViewModel: ScheduleViewModel };
    $.ajax({
        url: "../Schedule/AddEditSchedule",
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
}
function ShowModel(headerText, bodyText) {
    $("#alertModel").modal();
    $("#modelHeader").html(headerText);
    $("#modelText").html(bodyText);
}
function ClearFields() {
    $("#hdnGLMainGroupId").val("0");
    $("#txtGLMainGroupName").val("");
    //$("#txtSequenceNo").val("");
   // $("#ddlGLType").val("0");
    $("#chkstatus").prop("checked", true);

}

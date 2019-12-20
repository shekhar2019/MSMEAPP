
$(document).ready(function () { 
    


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

function SearchMenu() {
    var requestData = {};
    $.ajax({
        url: "../Menu/GetMenuList",
        data: requestData,
        dataType: "html",
        type: "GET",
        error: function (err) {
            $("#divMenuList").html("");
            $("#divMenuList").html(err);
        },
        success: function (data) {
            $("#divMenuList").html("");
            $("#divMenuList").html(data);
        }
    });
}

function EditMenuDetail(obj)
{
    var row = $(obj).closest("tr");
    var menuId = $(row).find("#hdnMainMenuId").val();
    var menuName = $(row).find("#hdnMenuName").val();
    var menuLink = $(row).find("#hdnMenuLink").val();
    var menuStatus = $(row).find("#hdnMenuStatus").val();
    var menuSequenceNo = $(row).find("#hdnMenuSequenceNo").val();

    $("#hdnMenuId").val(menuId);
    $("#txtMenuName").val(menuName);
    $("#txtMenuLink").val(menuLink);
    $("#txtSequenceNo").val(menuSequenceNo);
    if (menuStatus == 'true') {
        $("#chkstatus").prop("checked",true);
    }
    else
    {
        $("#chkstatus").prop("checked",false);
    }
        
    $("#btnSave").hide();
    $("#btnUpdate").show();
}

function SaveData()
{
    var txtMenuName = $("#txtMenuName");
    var hdnMenuId = $("#hdnMenuId");
    var txtMenuLink = $("#txtMenuLink");
    var txtSequenceNo = $("#txtSequenceNo");
    var chkstatus = $("#chkstatus").is(':checked') ? true : false;
    if (txtMenuName.val().trim() == "")
    {
        ShowModel("Alert", "Please Enter Menu Name")
        txtMenuName.focus();
        return false;
    }
    if (txtMenuLink.val().trim() == "") {
        ShowModel("Alert", "Please Enter Menu Link")
        txtMenuLink.focus();
        return false;
    }

    var menuViewModel = {
        MenuId: hdnMenuId.val(),
        MenuName: txtMenuName.val().trim(),
        MenuLink: txtMenuLink.val().trim(),
        MenuStatus: chkstatus,
        SequenceNo:txtSequenceNo.val().trim()
        
    };
    var requestData = { menuViewModel: menuViewModel };
    $.ajax({
        url: "../Menu/AddEditMenu",
        cache: false,
        type: "POST",
        dataType: "json",
        data: JSON.stringify( requestData),
        contentType: 'application/json',
        success: function (data) {
            if (data.status=="SUCCESS")
            {
                ShowModel("Alert", data.message);
                ClearFields();
                SearchMenu();
                $("#btnSave").show();
                $("#btnUpdate").hide();
            }
            else
            {
                ShowModel("Error", data.message)
            }
        },
        error: function (err) {
            ShowModel("Error", err)
        }
    });

}
function ShowModel(headerText,bodyText)
{
    $("#alertModel").modal();
    $("#modelHeader").html(headerText);
    $("#modelText").html(bodyText);

}
function ClearFields()
{
 
    $("#txtMenuName").val("");
    $("#txtMenuLink").val("#");
    $("#txtSequenceNo").val("")
    $("#chkstatus").prop("checked", true);
    
}

function RemoveMenu(menuId) {
    var confirmation = confirm("Do You Want Delete Menu");
    if (confirmation) {
        var requestData = { menuId: menuId };
        $.ajax({
            url: "../Menu/RemoveMenu",
            cache: false,
            type: "POST",
            dataType: "json",
            data: JSON.stringify(requestData),
            contentType: 'application/json',
            success: function (data) {
                if (data.status == "SUCCESS") {
                    ShowModel("Alert", data.message);
                    SearchMenu();
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
}


$(document).ready(function () { 
    BindMenuList();


});
function BindMenuList() {
    $.ajax({
        type: "GET",
        url: "../SubMenu/GetMenuList",
        data: "{}",
        dataType: "json",
        asnc: false,
        success: function (data) {
            $("#ddlMenu").append($("<option></option>").val(0).html("-Select Menu-"));
            $.each(data, function (i, item) {

                $("#ddlMenu").append($("<option></option>").val(item.MenuId).html(item.MenuName));
            });
        },
        error: function (Result) {
            $("#ddlMenu").append($("<option></option>").val(0).html("-Select Menu-"));
        }
    });
}

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

function SearchSubMenu() {
    var requestData = {};
    $.ajax({
        url: "../SubMenu/GetSubMenuList",
        data: requestData,
        dataType: "html",
        type: "GET",
        error: function (err) {
            $("#divSubMenuList").html("");
            $("#divSubMenuList").html(err);
        },
        success: function (data) {
            $("#divSubMenuList").html("");
            $("#divSubMenuList").html(data);
        }
    });
}

function EditMenuDetail(obj)
{
    
    var row = $(obj).closest("tr");
    var menuId = $(row).find("#hdnMenuId").val();
    var pageName = $(row).find("#hdnPageName").val();
    var subMenuId = $(row).find("#hdnSubMenuId").val();
    var subMenuName = $(row).find("#hdnSubMenuName").val();
    var subMenuLink = $(row).find("#hdnSubMenuLink").val();
    var subMenuStatus = $(row).find("#hdnSubMenuStatus").val();
    var subMenuSequenceNo = $(row).find("#hdnSubMenuSequenceNo").val();

    $("#ddlMenu").val(menuId);
    $("#hdnSubMenuId").val(subMenuId);
    $("#txtPageName").val(pageName);
    $("#txtSubMenuName").val(subMenuName);
    $("#txtSubMenuLink").val(subMenuLink);
    $("#txtSequenceNo").val(subMenuSequenceNo);
    if (subMenuStatus == 'true') {
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
    var hdnSubMenuId = $("#hdnSubMenuId");
    var ddlMenu = $("#ddlMenu");
    var txtSubMenuName = $("#txtSubMenuName");
    var txtPageName = $("#txtPageName");
    var hdnSubMenuId = $("#hdnSubMenuId");
    var txtSubMenuLink = $("#txtSubMenuLink");
    var txtSequenceNo = $("#txtSequenceNo");

    var chkstatus = $("#chkstatus").is(':checked') ? true : false;
    if (ddlMenu.val() == "0" || ddlMenu.val() == "")
    {
        ShowModel("Alert", "Please Select Menu Name");
        ddlMenu.focus();
        return false;
    }
    if (txtSubMenuName.val().trim() == "")
    {
        ShowModel("Alert", "Please Enter Sub Menu Name")
        txtSubMenuName.focus();
        return false;
    }
    if (txtSubMenuLink.val().trim() == "") {
        ShowModel("Alert", "Please Enter Menu Link")
        txtSubMenuLink.focus();
        return false;
    }
    if (txtSequenceNo.val().trim() == "") {
        ShowModel("Alert", "Please Enter Sequence No")
        txtSequenceNo.focus();
        return false;
    }
    var subMenuViewModel = {
        SubMenuId: hdnSubMenuId.val(),
        MenuId: ddlMenu.val(),
        PageName:txtPageName.val().trim(),
        SubMenuName: txtSubMenuName.val().trim(),
        SubMenuLink: txtSubMenuLink.val().trim(),
        SubMenuStatus: chkstatus,
        SequenceNo:txtSequenceNo.val().trim()
        
    };
    var requestData = { subMenuViewModel: subMenuViewModel };
    $.ajax({
        url: "../SubMenu/AddEditSubMenu",
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
               SearchSubMenu();
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
    $("#ddlMenu").val("0");
    $("#txtPageName").val("");
    $("#txtSubMenuName").val("");
    $("#txtSubMenuLink").val("");
    $("#txtSequenceNo").val("")
    $("#chkstatus").prop("checked", true);
    
}

function RemoveSubMenu(menuId) {
    var confirmation = confirm("Do You Want Delete Sub Menu");
    if (confirmation) {
        var requestData = { menuId: menuId };
        $.ajax({
            url: "../SubMenu/RemoveSubMenu",
            cache: false,
            type: "POST",
            dataType: "json",
            data: JSON.stringify(requestData),
            contentType: 'application/json',
            success: function (data) {
                if (data.status == "SUCCESS") {
                    ShowModel("Alert", data.message);
                    SearchSubMenu();
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

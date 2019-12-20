
$(document).ready(function () { 
    BindMenuList();


});
function BindMenuList() {
    $("#ddlMenu").val(0);
    $("#ddlMenu").html("");
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

function BindSubMenuList(subMenuId) {

    var menuId = $("#ddlMenu option:selected").val();
    //alert(stateId);
    $("#ddlSubMenuName").val(0);
    $("#ddlSubMenuName").html("");
    if (menuId != undefined && menuId != "" && menuId != "0") {
        var data = { menuId: menuId };
        $.ajax({
            type: "GET",
            url: "../SubChildMenu/GetSubMenuList",
            data: data,
            asnc: false,
            dataType: "json",
            success: function (data) {

                $("#ddlSubMenuName").append($("<option></option>").val(0).html("-Select Sub Menu Name-"));
                $.each(data, function (i, item) {
                    $("#ddlSubMenuName").append($("<option></option>").val(item.SubMenuId).html(item.SubMenuName));
                });
                $("#ddlSubMenuName").val(subMenuId);

            },
            error: function (Result) {
                $("#ddlSubMenuName").append($("<option></option>").val(0).html("-Select Sub Menu Name-"));
            }
        });
    }
    else {

        $("#ddlSubMenuName").append($("<option></option>").val(0).html("-Select State-"));
    }

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

function SearchSubChildMenu() {
    var subMenuId = $("#ddlSubMenuName").val();
    var requestData = { subMenuId: subMenuId };
    $.ajax({
        url: "../SubChildMenu/GetSubChildMenuList",
        data: requestData,
        dataType: "html",
        type: "GET",
        error: function (err) {
            $("#divSubChildMenuList").html("");
            $("#divSubChildMenuList").html(err);
        },
        success: function (data) {
            $("#divSubChildMenuList").html("");
            $("#divSubChildMenuList").html(data);
        }
    });
}

function EditMenuDetail(obj)
{
    
    var row = $(obj).closest("tr");
    var menuId = $(row).find("#hdnMenuId").val();
    var subChildMenuId = $(row).find("#hdnSubChildMenuId").val();
    var subChildPageName = $(row).find("#hdnSubChildPageName").val();
    var subMenuId = $(row).find("#hdnSubMenuId").val();
    var subChildMenuName = $(row).find("#hdnSubChildMenuName").val();
    var subChildMenuLink = $(row).find("#hdnSubChildMenuLink").val();
    var subChildMenuStatus = $(row).find("#hdnSubChildMenuStatus").val();
    var sequenceNo = $(row).find("#hdnSequenceNo").val();
    //BindMenuList();
    $("#ddlMenu").val(menuId);
    BindSubMenuList(subMenuId);
    $("#hdnsubChildMenuId").val(subChildMenuId);
    //$("#ddlSubMenuName").val(subMenuId);
    $("#txtSubChildMenuName").val(subChildMenuName);
    $("#txtChildPageName").val(subChildPageName);
    $("#txtSubChildMenuLink").val(subChildMenuLink)
    $("#txtSequenceNo").val(sequenceNo);
    if (subChildMenuStatus == 'true') {
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
    
    var hdnsubChildMenuId = $("#hdnsubChildMenuId");
    var ddlMenu = $("#ddlMenu");
    var ddlSubMenuName = $("#ddlSubMenuName");
    var txtSubChildMenuName = $("#txtSubChildMenuName");
    var txtChildPageName = $("#txtChildPageName");
    var txtSubChildMenuLink = $("#txtSubChildMenuLink");
    var txtSequenceNo = $("#txtSequenceNo");
    var chkstatus = $("#chkstatus").is(':checked') ? true : false;

    if (ddlMenu.val() == "0" || ddlMenu.val() == ""){
        ShowModel("Alert", "Please Select Menu Name");
        ddlMenu.focus();
        return false;
    }

    if (ddlSubMenuName.val() == "0" || ddlSubMenuName.val() == "") {
        ShowModel("Alert", "Please Select Sub Menu Name");
        ddlSubMenuName.focus();
        return false;
    }

    if (txtSubChildMenuName.val().trim() == ""){
        ShowModel("Alert", "Please Enter Sub Child Menu Name")
        txtSubChildMenuName.focus();
        return false;
    }
    if (txtSubChildMenuLink.val().trim() == "") {
        ShowModel("Alert", "Please Enter Menu Link")
        txtSubChildMenuLink.focus();
        return false;
    }
    if (txtSequenceNo.val().trim() == "") {
        ShowModel("Alert", "Please Enter Sequence No")
        txtSequenceNo.focus();
        return false;
    }
    
    var subChildMenuViewModel = {
        MenuId:ddlMenu.val(),
        SubMenuId:ddlSubMenuName.val(),
        SubChildMenuId:hdnsubChildMenuId.val(),
        SubChildPageName:txtChildPageName.val(),
        SubChildMenuName:txtSubChildMenuName.val(),
        SubChildMenuLink:txtSubChildMenuLink.val(),
        SubChildMenuStatus:chkstatus,
        SequenceNo: txtSequenceNo.val()
    }
    var requestData = { subChildMenuViewModel: subChildMenuViewModel };
    $.ajax({
        url: "../SubChildMenu/AddEditSubChildMenu",
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
                SearchSubChildMenu();
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
    $("#ddlSubMenuName").val("0");
    $("#ddlMenu").val("0");
    $("#txtSubChildMenuName").val("");
    $("#txtChildPageName").val("");
    $("#txtSubChildMenuLink").val("");
    $("#txtSequenceNo").val("")
    $("#chkstatus").prop("checked", true);
    
}

function RemoveSubChildMenu(menuId) {
    var confirmation = confirm("Do You Want Delete Sub Menu");
    if (confirmation) {
        var requestData = { menuId: menuId };
        $.ajax({
            url: "../SubChildMenu/RemoveSubChildMenu",
            cache: false,
            type: "POST",
            dataType: "json",
            data: JSON.stringify(requestData),
            contentType: 'application/json',
            success: function (data) {
                if (data.status == "SUCCESS") {
                    ShowModel("Alert", data.message);
                    SearchSubChildMenu();
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

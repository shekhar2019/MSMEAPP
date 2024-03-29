﻿
$(document).ready(function () {
    var hdnAccessMode = $("#hdnAccessMode");
    var hdnBOMId = $("#hdnBOMId");
    $("#txtAssemblyCode").attr('readOnly', true);
    $("#txtAssemblyShortDesc").attr('readOnly', true);
    $("#txtProductCode").attr('readOnly', true);
    $("#txtProductShortDesc").attr('readOnly', true);

    if (hdnBOMId.val() != "" && hdnBOMId.val() != "0" && hdnAccessMode.val() != "" && hdnAccessMode.val() != "0")
    {
        GetProductBOMDetail(hdnBOMId.val());
        
        if (hdnAccessMode.val() == "3")
        {
            $("#btnSave").hide();
            $("#btnUpdate").hide();
            $("#btnReset").hide();
            $("input").attr('readOnly', true);
            $("select").attr('disabled', true);
        }
        else
        {
            $("#btnSave").hide();
            $("#btnUpdate").show();
            $("#btnReset").show();
        }
    }
    else
    {
        $("#btnSave").show();
        $("#btnUpdate").hide();
        $("#btnReset").show();
    }
    $("#ddlAssemblyType").focus();
        


    $("#txtProductName").autocomplete({
        minLength: 0,
        source:function(request,response){
            $.ajax({
                url: "../ProductBOM/GetSubAssemblyAutoCompleteList",
                type: "GET",
                dataType: "json",
                data: { term: request.term },
                success: function (data) {
                    response($.map(data, function (item) {
                        return { label: item.ProductName, value: item.Productid,desc:item.ProductShortDesc,code:item.ProductCode };
                    }))}
            })},
        focus: function (event, ui) {
            $("#txtProductName").val(ui.item.label);
            return false;
        },
        select: function (event, ui) {
            $("#txtProductName").val(ui.item.label);
            $("#hdnProductId").val(ui.item.value);
            $("#txtProductShortDesc").val(ui.item.desc);
            $("#txtProductCode").val(ui.item.code);
            return false;
        },
        change:function(event,ui)
        {
            if (ui.item == null)
            {
                $("#txtProductName").val("");
                $("#hdnProductId").val("0");
                $("#txtProductShortDesc").val("");
                $("#txtProductCode").val("");
                ShowModel("Alert", "Please select Sub Assembly/ Raw Component from List")
                
            }
            return false;
        }
    
    })
   .autocomplete("instance")._renderItem = function (ul, item) {
       return $("<li>")
         .append("<div><b>" + item.label + " || " + item.code + "</b><br>" + item.desc + "</div>")
         .appendTo(ul);
   };

    

    $("#txtAssemblyName").autocomplete({
        minLength: 0,
        source: function (request, response) {
            $.ajax({
                url: "../ProductBOM/GetProductAutoCompleteList",
                type: "GET",
                dataType: "json",
                data: { term: request.term, assemblyType: $("#ddlAssemblyType").val()},
                success: function (data) {
                    response($.map(data, function (item) {
                        return { label: item.ProductName, value: item.Productid, desc: item.ProductShortDesc, code: item.ProductCode };
                    }))
                }
            })
        },
        focus: function (event, ui) {
            $("#txtAssemblyName").val(ui.item.label);
            return false;
        },
        select: function (event, ui) {
            $("#txtAssemblyName").val(ui.item.label);
            $("#hdnAssemblyId").val(ui.item.value);
            $("#txtAssemblyShortDesc").val(ui.item.desc);
            $("#txtAssemblyCode").val(ui.item.code);
            return false;
        },
        change: function (event, ui) {
            if (ui.item == null) {
                $("#txtAssemblyName").val("");
                $("#hdnAssemblyId").val("0");
                $("#txtAssemblyShortDesc").val("");
                $("#txtAssemblyCode").val("");
                ShowModel("Alert", "Please select Main Assembly / Sub Assembly from List")

            }
            return false;
        }

    })
 .autocomplete("instance")._renderItem = function (ul, item) {
     return $("<li>")
       .append("<div><b>" + item.label + " || " + item.code + "</b><br>" + item.desc + "</div>")
       .appendTo(ul);
 };

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
function GetProductBOMDetail(bomId) {
    $.ajax({
        type: "GET",
        asnc:false,
        url: "../ProductBOM/GetProductBOMDetail",
        data: { bomId: bomId },
        dataType: "json",
        success: function (data) {
            $("#hdnAssemblyId").val(data.AssemblyId);
            $("#txtAssemblyName").val(data.AssemblyName);
            $("#txtAssemblyCode").val(data.AssemblyCode);
            $("#txtAssemblyShortDesc").val(data.AssemblyShortDesc);

            $("#hdnProductId").val(data.ProductId);
            $("#txtProductName").val(data.ProductName);
            $("#txtProductCode").val(data.ProductCode);
            $("#txtProductShortDesc").val(data.ProductShortDesc);
            $("#ddlAssemblyType").val(data.AssemblyType);
            $("#txtBOMQty").val(data.BOMQty);
            if (data.BOM_Status) {
                $("#chkStatus").attr("checked", true);
            }
            else {
                $("#chkStatus").attr("checked", false);
            }
        },
        error: function (Result) {
            ShowModel("Alert", "Problem in Request");
        }
    });
    
}
function SaveData()
{
    var hdnBOMId = $("#hdnBOMId");
    var hdnAssemblyId = $("#hdnAssemblyId");
    var txtAssemblyName = $("#txtAssemblyName");
    
    var hdnProductId = $("#hdnProductId");
    var txtProductName = $("#txtProductName");
    
    var txtBOMQty = $("#txtBOMQty");
    var bomStatus = true;
    var chkStatus = $("#chkStatus");
    if (chkStatus.prop("checked") == true)
    { bomStatus = true; }
    else
    { bomStatus = false; }
 
    if (txtAssemblyName.val().trim() == "") {
        ShowModel("Alert", "Please enter Main Assemby/ Sub Assembly Name")
        txtAssemblyName.focus();
        return false;
    }
    if (hdnAssemblyId.val() == "" || hdnAssemblyId.val() == "0") {
        ShowModel("Alert", "Please select Main Assemby/ Sub Assembly from list")
        txtAssemblyName.focus();
        return false;
    }
  
 
    if (txtProductName.val().trim() == "")
    {
        ShowModel("Alert","Please enter Sub Assembly/ Raw Component Name")
        txtProductName.focus();
        return false;
    }
    if (hdnProductId.val() == "" || hdnProductId.val() == "0") {
        ShowModel("Alert", "Please select Sub Assembly/ Raw Component from list")
        txtProductName.focus();
        return false;
    }

    if (hdnProductId.val() == hdnAssemblyId.val()) {
        ShowModel("Alert", "Assembly name and BOM Component Name cannot be same")
        txtProductName.focus();
        return false;
    }

    if (txtBOMQty.val() == "" || txtBOMQty.val() == "0") {
        ShowModel("Alert", "Please enter Assembly BOM Qty.")
        txtBOMQty.focus();
        return false;
    }
    var productBOMViewModel = {
        BOMId: hdnBOMId.val(),AssemblyId:hdnAssemblyId.val(), ProductId: hdnProductId.val(),
        BOMQty: txtBOMQty.val(), BOM_Status: bomStatus
        };
    var requestData = { productBOMViewModel: productBOMViewModel };
    $.ajax({
        url: "../ProductBOM/AddEditProductBOM",
        cache: false,
        type: "POST",
        dataType: "json",
        data: JSON.stringify(requestData),
        contentType: 'application/json',
        success: function (data) {
            if (data.status=="SUCCESS")
            {
                ShowModel("Alert", data.message);
                ClearFields();
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
    $("#hdnBOMId").val("0");
    $("#hdnAccessMode").val("3");
    //$("#hdnAssemblyId").val("0");
    //$("#txtAssemblyName").val("");
    //$("#txtAssemblyCode").val("");
    //$("#txtAssemblyShortDesc").val("");
    $("#hdnProductId").val("0");
    $("#txtProductName").val("");
    $("#txtProductCode").val("");
    $("#txtProductShortDesc").val("");
    $("#txtBOMQty").val("0");
    $("#chkStatus").prop("checked", true);
}

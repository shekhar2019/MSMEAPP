﻿$(document).ready(function () {
    $("#txtFromDate").attr('readOnly', true);
    $("#txtToDate").attr('readOnly', true);
    $("#txtFromDate,#txtToDate").datepicker({
        changeMonth: true,
        changeYear: true,
        dateFormat: 'dd-M-yy',
        yearRange: '-10:+100',
        onSelect: function (selected) { 
        }
    }); 
    $("#txtVendorName").autocomplete({
        minLength: 0,
        source: function (request, response) {
            $.ajax({
                url: "../PO/GetVendorAutoCompleteList",
                type: "GET",
                dataType: "json",
                data: { term: request.term },
                success: function (data) {
                    response($.map(data, function (item) {
                        return { label: item.VendorName, value: item.VendorId, Address: item.Address, code: item.VendorCode };
                    }))
                }
            })
        },
        focus: function (event, ui) {
            $("#txtVendorName").val(ui.item.label);
            return false;
        },
        select: function (event, ui) {
            $("#txtVendorName").val(ui.item.label);
            $("#hdnVendorId").val(ui.item.value);
            $("#txtVendorCode").val(ui.item.code);
            //GetVendorDetail(ui.item.value);
            return false;
        },
        change: function (event, ui) {
            if (ui.item == null) {
                $("#txtVendorName").val("");
                $("#hdnVendorId").val("0");
                $("#txtVendorCode").val("");
                ShowModel("Alert", "Please select Vendor from List")

            }
            return false;
        }

    })
  .autocomplete("instance")._renderItem = function (ul, item) {
      return $("<li>")
        .append("<div><b>" + item.label + " || " + item.code + "</b><br>" + item.Address + "</div>")
        .appendTo(ul);
  };


    $("#txtCreatedBy").autocomplete({
        minLength: 0,
        source: function (request, response) {
            $.ajax({
                url: "../Lead/GetUserAutoCompleteList",
                type: "GET",
                dataType: "json",
                data: { term: request.term },
                success: function (data) {
                    response($.map(data, function (item) {
                        return { label: item.FullName, value: item.UserName, UserId: item.UserId, MobileNo: item.MobileNo };
                    }))
                }
            })
        },
        focus: function (event, ui) {
            $("#txtCreatedBy").val(ui.item.label);
            return false;
        },
        select: function (event, ui) {
            $("#txtCreatedBy").val(ui.item.label);
            $("#hdnCreatedId").val(ui.item.UserId); 
            return false;
        },
        change: function (event, ui) {
            if (ui.item == null) {
                $("#txtCreatedBy").val("");
                $("#hdnCreatedId").val("0");
                ShowModel("Alert", "Please select User from List")
            }
            return false;
        }

    })
    .autocomplete("instance")._renderItem = function (ul, item) {
        return $("<li>")
          .append("<div><b>" + item.label + " || " + item.value + "</b><br>" + item.MobileNo + "</div>")
          .appendTo(ul);
    };
     

    BindStateList(); 
    SearchPurchaseInvoiceRegister();

});
 
function BindStateList() { 
    $("#ddlState").val(0);
    $("#ddlState").html("");
        var data = { countryId: 1 };
        $.ajax({
            type: "GET",
            url: "../Company/GetStateList",
            data: data,
            asnc: false,
            dataType: "json",
            success: function (data) {
                $("#ddlState").append($("<option></option>").val(0).html("-Select State-"));
                $.each(data, function (i, item) {
                    $("#ddlState").append($("<option></option>").val(item.StateId).html(item.StateName));
                });
                
            },
            error: function (Result) {
                $("#ddlState").append($("<option></option>").val(0).html("-Select State-"));
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

function ClearFields() { 
    $("#txtVendorName").val("");
    $("#txtCreatedBy").val("");
    $("#ddlSortBy").val("E.PONo");
    $("#ddlSortOrder").val("ASC");
    $("#txtFromDate").val($("#hdnFromDate").val());
    $("#txtToDate").val($("#hdnToDate").val());
    $("#ddlState").val(0);
    
}
function SearchPurchaseInvoiceRegister() {
    var hdnVendorId = $("#hdnVendorId");
    var ddlState = $("#ddlState"); 
    var ddlSortBy = $("#ddlSortBy");
    var hdnCreatedId = $("#hdnCreatedId");
    var ddlSortBy = $("#ddlSortBy");
    var ddlSortOrder = $("#ddlSortOrder");
    var txtFromDate = $("#txtFromDate");
    var txtToDate = $("#txtToDate"); 
    var requestData = { vendorId: hdnVendorId.val(), stateId: ddlState.val(), fromDate: txtFromDate.val(), toDate: txtToDate.val(), createdBy: hdnCreatedId.val(), sortBy: ddlSortBy.val(), SortOrder: ddlSortOrder.val() };
    $.ajax({
        url: "../PurchaseInvoiceRegister/GetPurchaseInvoiceRegisterList",
        data: requestData,
        dataType: "html",
        type: "GET",
        error: function (err) {
            $("#divList").html("");
            $("#divList").html(err);
        },
        success: function (data) {
            $("#divList").html("");
            $("#divList").html(data);
        }
    }); 
}
function Export()
{
    var divList = $("#divList");
    ExporttoExcel(divList);

}

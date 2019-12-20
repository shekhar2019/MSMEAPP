﻿$(document).ready(function () {
    $("#txtFromDate").attr('readOnly', true);
    $("#txtToDate").attr('readOnly', true);
    $("#txtCustomerCode").attr('readOnly', true);
    $("#txtFromDate,#txtToDate").datepicker({
        changeMonth: true,
        changeYear: true,
        dateFormat: 'dd-M-yy',
        yearRange: '-10:+100',
        onSelect: function (selected) { 
        }
    });
    BindStateList();
    $("#txtCustomerName").autocomplete({
        minLength: 0,
        source: function (request, response) {
            $.ajax({
                url: "../Quotation/GetCustomerAutoCompleteList",
                type: "GET",
                dataType: "json",
                data: { term: request.term },
                success: function (data) {
                    response($.map(data, function (item) {
                        return { label: item.CustomerName, value: item.CustomerId, primaryAddress: item.PrimaryAddress, code: item.CustomerCode };
                    }))
                }
            })
        },
        focus: function (event, ui) {
            $("#txtCustomerName").val(ui.item.label);
            return false;
        },
        select: function (event, ui) {
            $("#txtCustomerName").val(ui.item.label);
            $("#hdnCustomerId").val(ui.item.value);
            $("#txtCustomerCode").val(ui.item.code);
           // GetCustomerDetail(ui.item.value);
            return false;
        },
        change: function (event, ui) {
            if (ui.item == null) {
                $("#txtCustomerName").val("");
                $("#hdnCustomerId").val("0");
                $("#txtCustomerCode").val("");
                ShowModel("Alert", "Please select Customer from List") 
            }
            return false;
        }

         
    })
    .autocomplete("instance")._renderItem = function (ul, item) {
     return $("<li>")
       .append("<div><b>" + item.label + " || " + item.code + "</b><br>" + item.primaryAddress + "</div>")
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
                $("#hdnCreatedId").val("");
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
});
 
function BindStateList() { 
    var data = { countryId: 1 };
        $.ajax({
            type: "GET",
            url: "../Company/GetStateList",
            data: data,
            asnc: false,
            dataType: "json",
            success: function (data) {
                $("#ddlState").append($("<option></option>").val("0").html("-Select State-"));
                $.each(data, function (i, item) {
                    $("#ddlState").append($("<option></option>").val(item.StateId).html(item.StateName));
                });
                
            },
            error: function (Result) {
                $("#ddlState").append($("<option></option>").val("0").html("-Select State-"));
            }
        }); 
} 

function ClearFields() { 
    $("#txtCustomerName").val("");
    $("#hdnCustomerId").val("0"); 
    $("#hdnCreatedId").val("0");
    $("#txtCustomerCode").val("");
    $("#txtFromDate").val($("#hdnFromDate").val());
    $("#txtToDate").val($("#hdnToDate").val());
    $("#ddlState").val("0");
    $("#txtCreatedBy").val("");  
    
    
    
}
function SearchSaleInvocieSummary() {
    var hdnCustomerId = $("#hdnCustomerId"); 
    var ddlState = $("#ddlState");
    var hdnCreatedId = $("#hdnCreatedId");
    var txtFromDate = $("#txtFromDate");
    var txtToDate = $("#txtToDate"); 
    var requestData = { customerId: hdnCustomerId.val(), userId: hdnCreatedId.val(), stateId: ddlState.val(),fromDate: txtFromDate.val(), toDate: txtToDate.val() };
    $.ajax({
        url: "../SaleInvoiceSummary/GetSaleInvoiceSummaryList",
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

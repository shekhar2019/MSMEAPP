
$(document).ready(function () {
    $("#txtFromDate").attr('readOnly', true);
    $("#txtToDate").attr('readOnly', true);
    $("#txtFromDate,#txtToDate").datepicker({
        changeMonth: true,
        changeYear: true,
        dateFormat: 'dd-M-yy',
        yearRange: '-10:+100',
        onSelect: function (selected) {
            GenerateReportParameters();
        }
    });

    BindProductTypeList();
    BindProductMainGroupList();
    BindCompanyBranchList();
    $("#ddlProductSubGroup").append($("<option></option>").val(0).html("-Select Sub Group-"));
    var hdnAccessMode = $("#hdnAccessMode");
    var hdnProductId = $("#hdnProductId");

    $("#txtProductName").autocomplete({
        minLength: 0,
        source: function (request, response) {
            $.ajax({
                url: "../Product/GetProductAutoCompleteList",
                type: "GET",
                dataType: "json",
                data: { term: request.term },
                success: function (data) {
                    response($.map(data, function (item) {
                        return { label: item.ProductName, value: item.Productid, desc: item.ProductShortDesc, code: item.ProductCode };
                    }))
                }
            })
        },
        focus: function (event, ui) {
            $("#txtProductName").val(ui.item.label);
            return false;
        },
        select: function (event, ui) {
            $("#txtProductName").val(ui.item.label);
            $("#hdnProductId").val(ui.item.value);
            GenerateReportParameters();
            return false;
        },
        change: function (event, ui) {
            if (ui.item == null) {
                $("#txtProductName").val("");
                $("#hdnProductId").val("0");
                GenerateReportParameters();
                ShowModel("Alert", "Please select Product from List")

            }
            return false;
        }

    })
.autocomplete("instance")._renderItem = function (ul, item) {
    return $("<li>")
      .append("<div><b>" + item.label + " || " + item.code + "</b><br>" + item.desc + "</div>")
      .appendTo(ul);
};
    var url = "../StockLedger/Report?productTypeId=0&assemblyType=0&productMainGroupId=0&productSubGroupId=0&productId=0&customerBranchId=0&fromDate=" + $("#txtFromDate").val() + "&toDate=" + $("#txtToDate").val() + "&reportType=PDF";
    $('#lnkExport,#lnkExportSummary').attr('href', url);

    var urlSummary = "../StockLedger/SummaryReport?productTypeId=0&assemblyType=0&productMainGroupId=0&productSubGroupId=0&productId=0&customerBranchId=0&fromDate=" + $("#txtFromDate").val() + "&toDate=" + $("#txtToDate").val() + "&reportType=PDF";
    $('#lnkExportSummary').attr('href', urlSummary);
});

function BindCompanyBranchList() {
    $("#ddlCompanyBranch").val(0);
    $("#ddlCompanyBranch").html("");
    $.ajax({
        type: "GET",
        url: "../DeliveryChallan/GetCompanyBranchList",
        data: {},
        dataType: "json",
        asnc: false,
        success: function (data) {
            $("#ddlCompanyBranch").append($("<option></option>").val(0).html("-Select Location-"));
            $.each(data, function (i, item) {
                $("#ddlCompanyBranch").append($("<option></option>").val(item.CompanyBranchId).html(item.BranchName));
            });
        },
        error: function (Result) {
            $("#ddlCompanyBranch").append($("<option></option>").val(0).html("-Select Location-"));
        }
    });
}
function BindProductTypeList() {
    $.ajax({
        type: "GET",
        url: "../Product/GetProductTypeList",
        data: "{}",
        dataType: "json",
        asnc: false,
        success: function (data) {
            $("#ddlProductType").append($("<option></option>").val(0).html("-Select Type-"));
            $.each(data, function (i, item) {
                $("#ddlProductType").append($("<option></option>").val(item.ProductTypeId).html(item.ProductTypeName));
            });
        },
        error: function (Result) {
            $("#ddlProductType").append($("<option></option>").val(0).html("-Select Type-"));
        }
    });
}
function BindProductMainGroupList() {
    $.ajax({
        type: "GET",
        url: "../Product/GetProductMainGroupList",
        data: "{}",
        dataType: "json",
        asnc: false,
        success: function (data) {
            $("#ddlProductMainGroup").append($("<option></option>").val(0).html("-Select Main Group-"));
            $.each(data, function (i, item) {
                $("#ddlProductMainGroup").append($("<option></option>").val(item.ProductMainGroupId).html(item.ProductMainGroupName));
            });
        },
        error: function (Result) {
            $("#ddlProductMainGroup").append($("<option></option>").val(0).html("-Select Main Group-"));
        }
    });
}
function BindProductSubGroupList(productSubGroupId) {
    var productMainGroupId = $("#ddlProductMainGroup option:selected").val();
    $("#ddlProductSubGroup").val(0);
    $("#ddlProductSubGroup").html("");
    if (productMainGroupId != undefined && productMainGroupId != "" && productMainGroupId != "0") {
        var data = { productMainGroupId: productMainGroupId };
        $.ajax({
            type: "GET",
            url: "../Product/GetProductSubGroupList",
            data: data,
            asnc: false,
            dataType: "json",
            success: function (data) {
                $("#ddlProductSubGroup").append($("<option></option>").val(0).html("-Select Sub Group-"));
                $.each(data, function (i, item) {
                    $("#ddlProductSubGroup").append($("<option></option>").val(item.ProductSubGroupId).html(item.ProductSubGroupName));
                });
                $("#ddlProductSubGroup").val(productSubGroupId);
            },
            error: function (Result) {
                $("#ddlProductSubGroup").append($("<option></option>").val(0).html("-Select Sub Group-"));
            }
        });
    }
    else {

        $("#ddlProductSubGroup").append($("<option></option>").val(0).html("-Select Sub Group-"));
    }

}

function ClearFields() {

    $("#txtFromDate").val($("#hdnFromDate").val());
    $("#txtToDate").val($("#hdnToDate").val());
    $("#ddlCompanyBranch").val("0");
    $("#txtProductName").val("");
    $("#hdnProductId").val("0");
    $("#ddlProductMainGroup").val("0");
    $("#ddlProductSubGroup").val("0");
    $("#ddlProductType").val("0");
    $("#ddlAssemblyType").val("0");

    $("#divList").html("");
    
}
function GenerateReportParameters() {
    var url = "../StockLedger/Report?productTypeId=" + $("#ddlProductType").val() + "&assemblyType=" + $("#ddlAssemblyType").val() + "&productMainGroupId=" + $("#ddlProductMainGroup").val() + "&productSubGroupId=" + $("#ddlProductSubGroup").val() + "&productId=" + $("#hdnProductId").val() + "&customerBranchId=" + $("#ddlCompanyBranch").val() + "&fromDate=" + $("#txtFromDate").val() + "&toDate=" + $("#txtToDate").val() + "&reportType=PDF";
    $('#lnkExport').attr('href', url);
    var urlSummary= "../StockLedger/SummaryReport?productTypeId=" + $("#ddlProductType").val() + "&assemblyType=" + $("#ddlAssemblyType").val() + "&productMainGroupId=" + $("#ddlProductMainGroup").val() + "&productSubGroupId=" + $("#ddlProductSubGroup").val() + "&productId=" + $("#hdnProductId").val() + "&customerBranchId=" + $("#ddlCompanyBranch").val() + "&fromDate=" + $("#txtFromDate").val() + "&toDate=" + $("#txtToDate").val() + "&reportType=PDF";
    $('#lnkExportSummary').attr('href', urlSummary);
     
  
}



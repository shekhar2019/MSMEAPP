$(window).load(function () {
    
});
$(document).ready(function () {
    $("#tabs").tabs({
        collapsible: true
    });
    $("#txtQuotationNo").attr('readOnly', true);
    $("#txtQuotationDate").attr('readOnly', true);


    $("#txtCustomerCode").attr('readOnly', true);
    $("#txtRefDate").attr('readOnly', true);
    $("#txtCreatedBy").attr('readOnly', true);
    $("#txtCreatedDate").attr('readOnly', true);
    $("#txtModifiedBy").attr('readOnly', true);
    $("#txtModifiedDate").attr('readOnly', true);
    $("#txtRejectedDate").attr('readOnly', true);

    $("#txtProductCode").attr('readOnly', true);
    $("#txtUOMName").attr('readOnly', true);
    $("#txtAvailableStock").attr('readOnly', true);
    
    
    $("#txtTotalPrice").attr('readOnly', true);
    $("#txtBasicValue").attr('readOnly', true);

    $("#txtTaxPercentage").attr('readOnly', true);
    $("#txtTaxAmount").attr('readOnly', true);
    $("#txtTotalValue").attr('readOnly', true);

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
            GetCustomerDetail(ui.item.value);
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
            $("#txtProductShortDesc").val(ui.item.desc);
            $("#txtProductCode").val(ui.item.code);
            GetProductDetail(ui.item.value);
            return false;
        },
        change: function (event, ui) {
            if (ui.item == null) {
                $("#txtProductName").val("");
                $("#hdnProductId").val("0");
                $("#txtProductShortDesc").val("");
                $("#txtProductCode").val("");
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

    $("#txtTaxName").autocomplete({
        minLength: 0,
        source: function (request, response) {
            $.ajax({
                url: "../Quotation/GetTaxAutoCompleteList",
                type: "GET",
                dataType: "json",
                data: { term: request.term },
                success: function (data) {
                    response($.map(data, function (item) {
                        return { label: item.TaxName, value: item.TaxId, percentage: item.TaxPercentage };
                    }))
                }
            })
        },
        focus: function (event, ui) {
            $("#txtTaxName").val(ui.item.label);
            return false;
        },
        select: function (event, ui) {
            $("#txtTaxName").val(ui.item.label);
            $("#hdnTaxId").val(ui.item.value);
            $("#txtTaxPercentage").val(ui.item.percentage);

            if (parseFloat($("#txtBasicValue").val()) > 0) {
                var taxAmount = (parseFloat($("#txtBasicValue").val()) * (parseFloat($("#txtTaxPercentage").val()) / 100));
                $("#txtTaxAmount").val(taxAmount.toFixed(2));
            }
            else {
                $("#txtTaxAmount").val("0");
            }
            return false;
        },
        change: function (event, ui) {
            if (ui.item == null) {
                $("#txtTaxName").val("");
                $("#hdnTaxId").val("0");
                $("#txtTaxPercentage").val("0");
                $("#txtTaxAmount").val("0");
                ShowModel("Alert", "Please select Tax from List")

            }
            return false;
        }

    })
.autocomplete("instance")._renderItem = function (ul, item) {
    return $("<li>")
      .append("<div><b>" + item.label + "</b></div>")
      .appendTo(ul);
};


    $("#txtQuotationDate,#txtRefDate").datepicker({
        changeMonth: true,
        changeYear: true,
        dateFormat: 'dd-M-yy',
        yearRange: '-10:+100',
        onSelect: function (selected) {

        }
    });


    BindCountryList();
    BindTermTemplateList();
    $("#ddlState").append($("<option></option>").val(0).html("-Select State-"));

    var hdnAccessMode = $("#hdnAccessMode");
    var hdnQuotationId = $("#hdnQuotationId");
    if (hdnQuotationId.val() != "" && hdnQuotationId.val() != "0" && hdnAccessMode.val() != "" && hdnAccessMode.val() != "0") {
        setTimeout(
       function () {
           GetQuotationDetail(hdnQuotationId.val());
       }, 2000);



        if (hdnAccessMode.val() == "3") {
            $("#btnSave").hide();
            $("#btnUpdate").hide();
            $("#btnReset").hide();
            $("input").attr('readOnly', true);
            $("textarea").attr('readOnly', true);
            $("select").attr('disabled', true);
            $("#chkstatus").attr('disabled', true);
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

    var quotationProducts = [];
    GetQuotationProductList(quotationProducts);
    var quotationTaxes = [];
    GetQuotationTaxList(quotationTaxes);
    var quotationTerms = [];
    GetQuotationTermList(quotationTerms);

    //$("#txtCustomerName").focus();

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
function BindCountryList() {
    $.ajax({
        type: "GET",
        url: "../Company/GetCountryList",
        data: "{}",
        dataType: "json",
        asnc: false,
        success: function (data) {
            $("#ddlCountry").append($("<option></option>").val(0).html("-Select Country-"));
            $.each(data, function (i, item) {

                $("#ddlCountry").append($("<option></option>").val(item.CountryId).html(item.CountryName));
            });
        },
        error: function (Result) {
            $("#ddlCountry").append($("<option></option>").val(0).html("-Select Country-"));
        }
    });
}
function BindTermTemplateList() {
    $.ajax({
        type: "GET",
        url: "../Quotation/GetTermTemplateList",
        data: "{}",
        dataType: "json",
        asnc: false,
        success: function (data) {
            $("#ddlTermTemplate").append($("<option></option>").val(0).html("-Select Terms Template-"));
            $.each(data, function (i, item) {

                $("#ddlTermTemplate").append($("<option></option>").val(item.TermTemplateId).html(item.TermTempalteName));
            });
        },
        error: function (Result) {
            $("#ddlTermTemplate").append($("<option></option>").val(0).html("-Select Terms Template-"));
        }
    });
}
function BindTermsDescription() {
    var termTemplateId = $("#ddlTermTemplate option:selected").val();
    var quotationTermList = [];
    if (termTemplateId != undefined && termTemplateId != "" && termTemplateId != "0") {
        var data = { termTemplateId: termTemplateId };

        $.ajax({
            type: "GET",
            url: "../Quotation/GetTermTemplateDetailList",
            data: data,
            asnc: false,
            dataType: "json",
            success: function (data) {
                var termCounter = 1;

                $.each(data, function (i, item) {
                    var quotationTerm = {
                        QuotationTermDetailId: item.TrnId,
                        TermDesc: item.TermsDesc,
                        TermSequence: termCounter
                    };
                    quotationTermList.push(quotationTerm);
                    termCounter += 1;
                });
                GetQuotationTermList(quotationTermList);
            },
            error: function (Result) {
                GetQuotationTermList(quotationTermList);
            }
        });
    }
    else {
        GetQuotationTermList(quotationTermList);
    }
}
function GetQuotationTermList(quotationTerms) {
    var hdnQuotationId = $("#hdnQuotationId");
    var requestData = { quotationTerms: quotationTerms, quotationId: hdnQuotationId.val() };
    $.ajax({
        url: "../Quotation/GetQuotationTermList",
        cache: false,
        data: JSON.stringify(requestData),
        dataType: "html",
        contentType: "application/json; charset=utf-8",
        type: "POST",
        error: function (err) {
            $("#divTermList").html("");
            $("#divTermList").html(err);
        },
        success: function (data) {
            $("#divTermList").html("");
            $("#divTermList").html(data);
            ShowHideTermPanel(2);
        }
    });
}
function AddTerm(action) {

    var txtTermDesc = $("#txtTermDesc");
    var hdnQuotationTermDetailId = $("#hdnQuotationTermDetailId");
    var hdnTermSequence = $("#hdnTermSequence");


    if (txtTermDesc.val().trim() == "") {
        ShowModel("Alert", "Please Enter Terms")
        txtTermDesc.focus();
        return false;
    }

    var quotationTermList = [];
    var termCounter = 1;
    $('#tblTermList tr').each(function (i, row) {
        var $row = $(row);
        var quotationTermDetailId = $row.find("#hdnQuotationTermDetailId").val();
        var termDesc = $row.find("#hdnTermDesc").val();
        var termSequence = $row.find("#hdnTermSequence").val();

        if (quotationTermDetailId != undefined) {
            if (action == 1 || hdnQuotationTermDetailId.val() != quotationTermDetailId) {

                if (termSequence == 0)
                { termSequence = termCounter; }

                var quotationTerm = {
                    QuotationTermDetailId: quotationTermDetailId,
                    TermDesc: termDesc,
                    TermSequence: termSequence
                };
                quotationTermList.push(quotationTerm);
                termCounter += 1;
            }
        }

    });

    if (hdnTermSequence.val() == "" || hdnTermSequence.val() == "0") {
        hdnTermSequence.val(termCounter);
    }
    var quotationTermAddEdit = {
        QuotationTermDetailId: hdnQuotationTermDetailId.val(),
        TermDesc: txtTermDesc.val().trim(),
        TermSequence: hdnTermSequence.val()
    };

    quotationTermList.push(quotationTermAddEdit);
    GetQuotationTermList(quotationTermList);

}
function EditTermRow(obj) {

    var row = $(obj).closest("tr");
    var quotationTermDetailId = $(row).find("#hdnQuotationTermDetailId").val();
    var termDesc = $(row).find("#hdnTermDesc").val();
    var termSequence = $(row).find("#hdnTermSequence").val();


    $("#txtTermDesc").val(termDesc);
    $("#hdnQuotationTermDetailId").val(quotationTermDetailId);
    $("#hdnTermSequence").val(termSequence);

    $("#btnAddTerm").hide();
    $("#btnUpdateTerm").show();
    ShowHideTermPanel(1);
}

function RemoveTermRow(obj) {
    if (confirm("Do you want to remove selected Term?")) {
        var row = $(obj).closest("tr");
        var quotationTermDetailId = $(row).find("#hdnQuotationTermDetailId").val();
        ShowModel("Alert", "Term Removed from List.");
        row.remove();
    }
}

function GetQuotationTaxList(quotationTaxes) {
    var hdnQuotationId = $("#hdnQuotationId");
    var requestData = { quotationTaxes: quotationTaxes, quotationId: hdnQuotationId.val() };
    $.ajax({
        url: "../Quotation/GetQuotationTaxList",
        cache: false,
        data: JSON.stringify(requestData),
        dataType: "html",
        contentType: "application/json; charset=utf-8",
        type: "POST",
        error: function (err) {
            $("#divTaxList").html("");
            $("#divTaxList").html(err);
        },
        success: function (data) {
            $("#divTaxList").html("");
            $("#divTaxList").html(data);
            CalculateGrossandNetValues();
            ShowHideTaxPanel(2);
        }
    });
}
function AddTax(action) {
    var flag = true;
    var taxEntrySequence = 0;
    var hdnTaxSequenceNo = $("#hdnTaxSequenceNo");
    var txtBasicValue = $("#txtBasicValue");
    var txtTaxName = $("#txtTaxName");
    var hdnQuotationTaxDetailId = $("#hdnQuotationTaxDetailId");
    var hdnTaxId = $("#hdnTaxId");
    var txtTaxPercentage = $("#txtTaxPercentage");
    var txtTaxAmount = $("#txtTaxAmount");

    if (txtBasicValue.val().trim() == "" || txtBasicValue.val().trim() == "0" || parseFloat(txtBasicValue.val().trim()) <= 0) {
        ShowModel("Alert", "Please select at least single product")
        txtTaxName.focus();
        return false;
    }

    if (txtTaxName.val().trim() == "") {
        ShowModel("Alert", "Please Enter Tax Name")
        txtTaxName.focus();
        return false;
    }
    if (hdnTaxId.val().trim() == "" || hdnTaxId.val().trim() == "0") {
        ShowModel("Alert", "Please select Tax from list")
        txtTaxName.focus();
        return false;
    }

    var quotationTaxList = [];
    if (action == 1 && (hdnTaxSequenceNo.val() == "" || hdnTaxSequenceNo.val() == "0")) {
        taxEntrySequence = 1;
    }
    $('#tblTaxList tr').each(function (i, row) {
        var $row = $(row);
        var taxSequenceNo = $row.find("#hdnTaxSequenceNo").val();
        var quotationTaxDetailId = $row.find("#hdnQuotationTaxDetailId").val();
        var taxId = $row.find("#hdnTaxId").val();
        var taxName = $row.find("#hdnTaxName").val();
        var taxPercentage = $row.find("#hdnTaxPercentage").val();
        var taxAmount = $row.find("#hdnTaxAmount").val();

        if (taxName != undefined) {
            if (action == 1 || (hdnTaxSequenceNo.val() != taxSequenceNo)) {

                if (taxId == hdnTaxId.val()) {
                    ShowModel("Alert", "Tax already added!!!")
                    txtTaxName.focus();
                    flag = false;
                    return false;
                }

                var quotationTax = {
                    QuotationTaxDetailId: quotationTaxDetailId,
                    TaxSequenceNo: taxSequenceNo,
                    TaxId: taxId,
                    TaxName: taxName,
                    TaxPercentage: taxPercentage,
                    TaxAmount: taxAmount
                };
                quotationTaxList.push(quotationTax);
                taxEntrySequence = parseInt(taxEntrySequence) + 1;
            }
            else if (hdnQuotationTaxDetailId.val() == quotationTaxDetailId && hdnTaxSequenceNo.val() == taxSequenceNo)
            {
                var quotationTax = {
                    QuotationTaxDetailId: hdnQuotationTaxDetailId.val(),
                    TaxSequenceNo: hdnTaxSequenceNo.val(),
                    TaxId: hdnTaxId.val(),
                    TaxName: txtTaxName.val().trim(),
                    TaxPercentage: txtTaxPercentage.val().trim(),
                    TaxAmount: txtTaxAmount.val().trim()
                };

                quotationTaxList.push(quotationTax);
                hdnTaxSequenceNo.val("0");
            }
        }

    });
    if (action == 1 && (hdnTaxSequenceNo.val() == "" || hdnTaxSequenceNo.val() == "0")) {
        hdnTaxSequenceNo.val(taxEntrySequence);
    }
    if (action == 1) {
        var quotationTaxAddEdit = {
            QuotationTaxDetailId: hdnQuotationTaxDetailId.val(),
            TaxSequenceNo: hdnTaxSequenceNo.val(),
            TaxId: hdnTaxId.val(),
            TaxName: txtTaxName.val().trim(),
            TaxPercentage: txtTaxPercentage.val().trim(),
            TaxAmount: txtTaxAmount.val().trim()
        };
        quotationTaxList.push(quotationTaxAddEdit);
        hdnTaxSequenceNo.val("0");
    }
    if (flag == true) {
        GetQuotationTaxList(quotationTaxList);

    }

}
function EditTaxRow(obj) {

    var row = $(obj).closest("tr");
    var quotationTaxDetailId = $(row).find("#hdnQuotationTaxDetailId").val();
    var taxSequenceNo = $(row).find("#hdnTaxSequenceNo").val();
    var taxId = $(row).find("#hdnTaxId").val();
    var taxName = $(row).find("#hdnTaxName").val();
    var taxPercentage = $(row).find("#hdnTaxPercentage").val();
    var taxAmount = $(row).find("#hdnTaxAmount").val();

    $("#txtTaxName").val(taxName);
    $("#hdnQuotationTaxDetailId").val(quotationTaxDetailId);
    $("#hdnTaxSequenceNo").val(taxSequenceNo);

    $("#hdnTaxId").val(taxId);
    $("#txtTaxPercentage").val(taxPercentage);
    $("#txtTaxAmount").val(taxAmount);

    $("#btnAddTax").hide();
    $("#btnUpdateTax").show();
    ShowHideTaxPanel(1);
}
function RemoveTaxRow(obj) {
    if (confirm("Do you want to remove selected Tax?")) {
        var row = $(obj).closest("tr");
        var quotationTaxDetailId = $(row).find("#hdnQuotationTaxDetailId").val();
        ShowModel("Alert", "Tax Removed from List.");
        row.remove();
        CalculateGrossandNetValues();
    }
}

function GetQuotationProductList(quotationProducts) {
    var hdnQuotationId = $("#hdnQuotationId");
    var requestData = { quotationProducts: quotationProducts, quotationId: hdnQuotationId.val() };
    $.ajax({
        url: "../Quotation/GetQuotationProductList",
        cache: false,
        data: JSON.stringify(requestData),
        dataType: "html",
        contentType: "application/json; charset=utf-8",
        type: "POST",
        error: function (err) {
            $("#divProductList").html("");
            $("#divProductList").html(err);
        },
        success: function (data) {
            $("#divProductList").html("");
            $("#divProductList").html(data);


            CalculateGrossandNetValues();
            ShowHideProductPanel(2);
        }
    });
}
function CalculateTotalCharges() {
    var price = $("#txtPrice").val();
    var quantity = $("#txtQuantity").val();
    price = price == "" ? 0 : price;
    quantity = quantity == "" ? 0 : quantity;

    var totalPrice = parseFloat(price) * parseFloat(quantity);
    $("#txtTotalPrice").val(totalPrice);


}
function CalculateGrossandNetValues() {
    var basicValue = 0;
    var taxValue = 0;
    $('#tblProductList tr').each(function (i, row) {
        var $row = $(row);
        var quotationProductDetailId = $row.find("#hdnQuotationProductDetailId").val();
        var totalPrice = $row.find("#hdnTotalPrice").val();

        if (totalPrice != undefined) {
            basicValue += parseFloat(totalPrice);
        }

    });
    $('#tblTaxList tr').each(function (i, row) {
        var $row = $(row);
        var quotationTaxDetailId = $row.find("#hdnQuotationTaxDetailId").val();
        var taxPercentage = $row.find("#hdnTaxPercentage").val();
        //var taxAmount = $row.find("#hdnTaxAmount").val();


        var taxAmount = 0;
        if (taxPercentage != undefined) {

            if (parseFloat(basicValue) > 0) {
                taxAmount = (parseFloat(basicValue) * (parseFloat(taxPercentage) / 100));
                $row.find("#hdnTaxAmount").val(taxAmount.toFixed(2));
                $row.find("#tdTaxAmount").html(taxAmount.toFixed(2));
            }
            else {
                taxAmount = 0;
                $row.find("#hdnTaxAmount").val("0");
                $row.find("#tdTaxAmount").html("0");
            }



            taxValue += parseFloat(taxAmount);
        }

    });

    $("#txtBasicValue").val(basicValue);
    $("#txtTotalValue").val(basicValue + taxValue);
}
function AddProduct(action) {
    var productEntrySequence = 0;
    var flag = true;
    var hdnSequenceNo = $("#hdnSequenceNo");
    var txtProductName = $("#txtProductName");
    var hdnQuotationProductDetailId = $("#hdnQuotationProductDetailId");
    var hdnProductId = $("#hdnProductId");
    var txtProductCode = $("#txtProductCode");
    var txtProductShortDesc = $("#txtProductShortDesc");
    var txtPrice = $("#txtPrice");
    var txtUOMName = $("#txtUOMName");
    var txtQuantity = $("#txtQuantity");
    var txtTotalPrice = $("#txtTotalPrice");

    if (txtProductName.val().trim() == "") {
        ShowModel("Alert", "Please Enter Product Name")
        txtProductName.focus();
        return false;
    }
    if (hdnProductId.val().trim() == "" || hdnProductId.val().trim() == "0") {
        ShowModel("Alert", "Please select Product from list")
        hdnProductId.focus();
        return false;
    }
    if (txtPrice.val().trim() == "" || txtPrice.val().trim() == "0" || parseFloat(txtPrice.val().trim()) <= 0) {
        ShowModel("Alert", "Please enter Price")
        txtPrice.focus();
        return false;
    }
    if (txtQuantity.val().trim() == "" || txtQuantity.val().trim() == "0" || parseFloat(txtQuantity.val().trim()) <= 0) {
        ShowModel("Alert", "Please enter Quantity")
        txtQuantity.focus();
        return false;
    }
    if (txtTotalPrice.val().trim() == "" || txtTotalPrice.val().trim() == "0" || parseFloat(txtTotalPrice.val().trim()) <= 0) {
        ShowModel("Alert", "Please enter correct Price and Quantity")
        txtQuantity.focus();
        return false;
    }
    if (action == 1 && (hdnSequenceNo.val() == "" || hdnSequenceNo.val() == "0")) {
        productEntrySequence = 1;
    }
    var quotationProductList = [];
    $('#tblProductList tr').each(function (i, row) {
        var $row = $(row);
        var sequenceNo = $row.find("#hdnSequenceNo").val();
        var quotationProductDetailId = $row.find("#hdnQuotationProductDetailId").val();
        var productId = $row.find("#hdnProductId").val();
        var productName = $row.find("#hdnProductName").val();
        var productCode = $row.find("#hdnProductCode").val();
        var productShortDesc = $row.find("#hdnProductShortDesc").val();
        var uomName = $row.find("#hdnUOMName").val();
        var price = $row.find("#hdnPrice").val();
        var quantity = $row.find("#hdnQuantity").val();
        var totalPrice = $row.find("#hdnTotalPrice").val();

        if (productName != undefined) {
            if (action == 1 || (hdnSequenceNo.val() != sequenceNo)) {

                if (productId == hdnProductId.val()) {
                    ShowModel("Alert", "Product already added!!!")
                    txtProductName.focus();
                    flag = false;
                    return false;
                }

                var quotationProduct = {
                    SequenceNo: sequenceNo,
                    QuotationProductDetailId: quotationProductDetailId,
                    ProductId: productId,
                    ProductName: productName,
                    ProductCode: productCode,
                    ProductShortDesc: productShortDesc,
                    UOMName: uomName,
                    Price: price,
                    Quantity: quantity,
                    TotalPrice: totalPrice
                };
                quotationProductList.push(quotationProduct);
                productEntrySequence = parseInt(productEntrySequence) + 1;
            }
            else if (hdnQuotationProductDetailId.val() == quotationProductDetailId && hdnSequenceNo.val() == sequenceNo)
            {
                var quotationProductAddEdit = {
                    QuotationProductDetailId: hdnQuotationProductDetailId.val(),
                    SequenceNo: hdnSequenceNo.val(),
                    ProductId: hdnProductId.val(),
                    ProductName: txtProductName.val().trim(),
                    ProductCode: txtProductCode.val().trim(),
                    ProductShortDesc: txtProductShortDesc.val().trim(),
                    UOMName: txtUOMName.val().trim(),
                    Price: txtPrice.val().trim(),
                    Quantity: txtQuantity.val().trim(),
                    TotalPrice: txtTotalPrice.val().trim()
                };

                quotationProductList.push(quotationProductAddEdit);
                hdnSequenceNo.val("0");
            }
        }

    });
    if (action == 1 && (hdnSequenceNo.val() == "" || hdnSequenceNo.val() == "0")) {
        hdnSequenceNo.val(productEntrySequence);
    }
    if (action == 1) {
        var quotationProductAddEdit = {
            QuotationProductDetailId: hdnQuotationProductDetailId.val(),
            SequenceNo: hdnSequenceNo.val(),
            ProductId: hdnProductId.val(),
            ProductName: txtProductName.val().trim(),
            ProductCode: txtProductCode.val().trim(),
            ProductShortDesc: txtProductShortDesc.val().trim(),
            UOMName: txtUOMName.val().trim(),
            Price: txtPrice.val().trim(),
            Quantity: txtQuantity.val().trim(),
            TotalPrice: txtTotalPrice.val().trim()
        };

        quotationProductList.push(quotationProductAddEdit);
        hdnSequenceNo.val("0");
    }

    if (flag == true) {
        GetQuotationProductList(quotationProductList);
    }
    
}
function EditProductRow(obj) {

    var row = $(obj).closest("tr");
    var sequenceNo = $(row).find("#hdnSequenceNo").val();
    var quotationProductDetailId = $(row).find("#hdnQuotationProductDetailId").val();
    var productId = $(row).find("#hdnProductId").val();
    var productName = $(row).find("#hdnProductName").val();
    var productCode = $(row).find("#hdnProductCode").val();
    var productShortDesc = $(row).find("#hdnProductShortDesc").val();
    var uomName = $(row).find("#hdnUOMName").val();
    var price = $(row).find("#hdnPrice").val();
    var quantity = $(row).find("#hdnQuantity").val();
    var totalPrice = $(row).find("#hdnTotalPrice").val();

    $("#txtProductName").val(productName);
    $("#hdnQuotationProductDetailId").val(quotationProductDetailId);
    $("#hdnSequenceNo").val(sequenceNo);
    $("#hdnProductId").val(productId);
    $("#txtProductCode").val(productCode);
    $("#txtProductShortDesc").val(productShortDesc);
    $("#txtUOMName").val(uomName);
    $("#txtPrice").val(price);
    $("#txtQuantity").val(quantity);
    $("#txtTotalPrice").val(totalPrice);

    $("#btnAddProduct").hide();
    $("#btnUpdateProduct").show();
    GetProductAvailableStock(productId);
    ShowHideProductPanel(1);
}

function RemoveProductRow(obj) {
    if (confirm("Do you want to remove selected Product?")) {
        var row = $(obj).closest("tr");
        var quotationProductDetailId = $(row).find("#hdnQuotationProductDetailId").val();
        ShowModel("Alert", "Product Removed from List.");
        row.remove();
        CalculateGrossandNetValues();
    }
}

function BindStateList(stateId) {
    var countryId = $("#ddlCountry option:selected").val();
    $("#ddlState").val(0);
    $("#ddlState").html("");
    if (countryId != undefined && countryId != "" && countryId != "0") {
        var data = { countryId: countryId };
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
                $("#ddlState").val(stateId);
            },
            error: function (Result) {
                $("#ddlState").append($("<option></option>").val(0).html("-Select State-"));
            }
        });
    }
    else {

        $("#ddlState").append($("<option></option>").val(0).html("-Select State-"));
    }

}

function GetQuotationDetail(quotationId) {
    $.ajax({
        type: "GET",
        asnc: false,
        url: "../Quotation/GetQuotationDetail",
        data: { quotationId: quotationId },
        dataType: "json",
        success: function (data) {
            $("#txtQuotationNo").val(data.QuotationNo);
            $("#txtQuotationDate").val(data.QuotationDate);
            $("#hdnCustomerId").val(data.CustomerId);
            $("#txtCustomerCode").val(data.CustomerCode);
            $("#txtCustomerName").val(data.CustomerName);
            $("#txtAddress").val(data.BillingAddress);
            $("#txtCity").val(data.City);
            $("#ddlCountry").val(data.CountryId);
            BindStateList(data.StateId);
            $("#ddlState").val(data.StateId);
            $("#ddlApprovalStatus").val(data.ApprovalStatus);
            $("#txtPinCode").val(data.PinCode);
            $("#txtCSTNo").val(data.CSTNo);
            $("#txtTINNo").val(data.TINNo);
            $("#txtPANNo").val(data.PANNo);
            $("#txtRefNo").val(data.RefNo);
            $("#txtRefDate").val(data.RefDate);
            $("#divCreated").show();
            $("#txtCreatedBy").val(data.CreatedByUserName);
            $("#txtCreatedDate").val(data.CreatedDate);
            if (data.ModifiedByUserName != "") {
                $("#divModified").show();
                $("#txtModifiedBy").val(data.ModifiedByUserName);
                $("#txtModifiedDate").val(data.ModifiedDate);
            }

            $("#txtBasicValue").val(data.BasicValue);
            $("#txtTotalValue").val(data.TotalValue);
            $("#btnAddNew").show();
            $("#btnPrint").show();
            $("#btnEmail").show();


        },
        error: function (Result) {
            ShowModel("Alert", "Problem in Request");
        }
    });

}

function SaveData() {
    var txtQuotationNo = $("#txtQuotationNo");
    var hdnQuotationId = $("#hdnQuotationId");
    var txtQuotationDate = $("#txtQuotationDate");
    var hdnCustomerId = $("#hdnCustomerId");
    var txtCustomerName = $("#txtCustomerName");
    var txtAddress = $("#txtAddress");
    var txtCity = $("#txtCity");
    var ddlCountry = $("#ddlCountry");
    var ddlState = $("#ddlState");
    var txtPinCode = $("#txtPinCode");
    var ddlApprovalStatus = $("#ddlApprovalStatus");
    var txtCSTNo = $("#txtCSTNo");
    var txtTINNo = $("#txtTINNo");
    var txtPANNo = $("#txtPANNo");
    var txtRefNo = $("#txtRefNo");  
    var txtRefDate = $("#txtRefDate");
    var txtBasicValue = $("#txtBasicValue");
    var txtTotalValue = $("#txtTotalValue");

    if (txtCustomerName.val().trim() == "") {
        ShowModel("Alert", "Please Enter Party Name")
        txtCustomerName.focus();
        return false;
    }
    if (hdnCustomerId.val() == "" || hdnCustomerId.val() == "0") {
        ShowModel("Alert", "Please select Party from list")
        txtCustomerName.focus();
        return false;
    }
    if (txtAddress.val().trim() == "") {
        ShowModel("Alert", "Please Enter Party billing Address")
        txtAddress.focus();
        return false;
    }
    if (txtCity.val().trim() == "") {
        ShowModel("Alert", "Please enter billing city")
        txtCity.focus();
        return false;
    }
    if (ddlCountry.val() == "" || ddlCountry.val() == "0") {
        ShowModel("Alert", "Please select billing country")
        ddlCountry.focus();
        return false;
    }
    if (ddlState.val() == "" || ddlState.val() == "0") {
        ShowModel("Alert", "Please select billing State")
        ddlState.focus();
        return false;
    }

    if (txtBasicValue.val() == "" || parseFloat(txtBasicValue.val()) <=0) {
        ShowModel("Alert", "Please select at least one Product")
        return false;
    }


    var quotationViewModel = {
        QuotationId: hdnQuotationId.val(),
        QuotationNo: txtQuotationNo.val().trim(),
        QuotationDate: txtQuotationDate.val().trim(),
        CustomerId: hdnCustomerId.val().trim(),
        CustomerName: txtCustomerName.val().trim(),
        BillingAddress: txtAddress.val().trim(),
        City: txtCity.val().trim(),
        StateId: ddlState.val(),
        CountryId: ddlCountry.val(),
        ApprovalStatus: ddlApprovalStatus.val(),
        PinCode: txtPinCode.val().trim(),
        CSTNo: txtCSTNo.val().trim(),
        TINNo: txtTINNo.val().trim(),
        PANNo: txtPANNo.val().trim(),
        RefNo: txtRefNo.val().trim(),
        RefDate: txtRefDate.val(),
        BasicValue: txtBasicValue.val(),
        TotalValue: txtTotalValue.val()
    };

    var quotationProductList = [];
    $('#tblProductList tr').each(function (i, row) {
        var $row = $(row);
        var quotationProductDetailId = $row.find("#hdnQuotationProductDetailId").val();
        var productId = $row.find("#hdnProductId").val();
        var productName = $row.find("#hdnProductName").val();
        var productCode = $row.find("#hdnProductCode").val();
        var productShortDesc = $row.find("#hdnProductShortDesc").val();
        var uomName = $row.find("#hdnUOMName").val();
        var price = $row.find("#hdnPrice").val();
        var quantity = $row.find("#hdnQuantity").val();
        var totalPrice = $row.find("#hdnTotalPrice").val();

        if (productName != undefined) {

            var quotationProduct = {
                QuotationProductDetailId: quotationProductDetailId,
                ProductId: productId,
                ProductName: productName,
                ProductCode: productCode,
                ProductShortDesc: productShortDesc,
                UOMName: uomName,
                Price: price,
                Quantity: quantity,
                TotalPrice: totalPrice
            };
            quotationProductList.push(quotationProduct);
        }
    });


    var quotationTaxList = [];
    $('#tblTaxList tr').each(function (i, row) {
        var $row = $(row);
        var quotationTaxDetailId = $row.find("#hdnQuotationTaxDetailId").val();
        var taxId = $row.find("#hdnTaxId").val();
        var taxName = $row.find("#hdnTaxName").val();
        var taxPercentage = $row.find("#hdnTaxPercentage").val();
        var taxAmount = $row.find("#hdnTaxAmount").val();

        if (taxName != undefined) {
            var quotationTax = {
                QuotationTaxDetailId: quotationTaxDetailId,
                TaxId: taxId,
                TaxName: taxName,
                TaxPercentage: taxPercentage,
                TaxAmount: taxAmount
            };
            quotationTaxList.push(quotationTax);
        }

    });

    var quotationTermList = [];
    $('#tblTermList tr').each(function (i, row) {
        var $row = $(row);
        var quotationTermDetailId = $row.find("#hdnQuotationTermDetailId").val();
        var termDesc = $row.find("#hdnTermDesc").val();
        var termSequence = $row.find("#hdnTermSequence").val();

        if (termDesc != undefined) {
            var quotationTerm = {
                QuotationTermDetailId: quotationTermDetailId,
                TermDesc: termDesc,
                TermSequence: termSequence
            };
            quotationTermList.push(quotationTerm);
        }

    });



    var requestData = { quotationViewModel: quotationViewModel, quotationProducts: quotationProductList, quotationTaxes: quotationTaxList, quotationTerms: quotationTermList };
    $.ajax({
        url: "../Quotation/AddEditQuotation",
        cache: false,
        type: "POST",
        dataType: "json",
        data: JSON.stringify(requestData),
        contentType: 'application/json',
        success: function (data) {
            if (data.status == "SUCCESS") {
                ShowModel("Alert", data.message);
                ClearFields();
                setTimeout(
                   function () {
                       window.location.href = "../Quotation/AddEditQuotation?QuotationId=" + data.trnId + "&AccessMode=2";
                   }, 2000);

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

    $("#txtQuotationNo").val("");
    $("#hdnQuotationId").val("0");
    $("#txtQuotationDate").val($("#hdnCurrentDate").val());
    $("#hdnCustomerId").val("0");
    $("#txtCustomerName").val("");
    $("#txtCustomerCode").val("");
    $("#txtAddress").val("");
    $("#txtCity").val("");
    $("#ddlCountry").val("0");
    $("#ddlState").val("0");
    $("#txtPinCode").val("");
    $("#ddlApprovalStatus").val("Final");
    $("#txtCSTNo").val("");
    $("#txtTINNo").val("");
    $("#txtPANNo").val("");
    $("#txtRefNo").val("");
    $("#txtRefDate").val("");

    $("#btnSave").show();
    $("#btnUpdate").hide();


}
function GetCustomerDetail(customerId) {
    $.ajax({
        type: "GET",
        asnc: false,
        url: "../Customer/GetCustomerDetail",
        data: { customerId: customerId },
        dataType: "json",
        success: function (data) {
            $("#txtAddress").val(data.PrimaryAddress);
            $("#txtCity").val(data.City);
            $("#ddlCountry").val(data.CountryId);
            BindStateList(data.StateId);
            $("#ddlState").val(data.StateId);
            $("#txtPinCode").val(data.PinCode);
            $("#txtCSTNo").val(data.CSTNo);
            $("#txtTINNo").val(data.TINNo);
            $("#txtPANNo").val(data.PANNo);
        },
        error: function (Result) {
            ShowModel("Alert", "Problem in Request");
        }
    });

}
function GetProductDetail(productId) {
    $.ajax({
        type: "GET",
        asnc: false,
        url: "../Product/GetProductDetail",
        data: { productid: productId },
        dataType: "json",
        success: function (data) {
            $("#txtPrice").val(data.SalePrice);
            $("#txtUOMName").val(data.UOMName);
        },
        error: function (Result) {
            ShowModel("Alert", "Problem in Request");
        }
    });

    GetProductAvailableStock(productId);

}
function GetProductAvailableStock(productId)
{
    $.ajax({
        type: "GET",
        asnc: false,
        url: "../Product/GetProductAvailableStock",
        data: { productid: productId, companyBranchId: 0, trnId: 0, trnType: "QU" },
        dataType: "json",
        success: function (data) {
            $("#txtAvailableStock").val(data);
        },
        error: function (Result) {
            ShowModel("Alert", "Problem in Request");
        }
    });
}
function ShowHideProductPanel(action) {
    if (action == 1) {
        $(".productsection").show();
    }
    else {
        $(".productsection").hide();
        $("#txtProductName").val("");
        $("#hdnProductId").val("0");
        $("#hdnQuotationProductDetailId").val("0");
        $("#txtProductCode").val("");
        $("#txtProductShortDesc").val("");
        $("#txtPrice").val("");
        $("#txtAvailableStock").val("");
        $("#txtUOMName").val("");
        $("#txtQuantity").val("");
        $("#txtTotalPrice").val("");
        $("#btnAddProduct").show();
        $("#btnUpdateProduct").hide();




    }
}
function ShowHideTaxPanel(action) {
    if (action == 1) {
        $(".taxsection").show();
    }
    else {
        $(".taxsection").hide();
        $("#txtTaxName").val("");
        $("#hdnTaxId").val("0");
        $("#hdnQuotationTaxDetailId").val("0");
        $("#txtTaxPercentage").val("");
        $("#txtTaxAmount").val("");
        $("#btnAddTax").show();
        $("#btnUpdateTax").hide();
    }
}
function ShowHideTermPanel(action) {
    if (action == 1) {
        $(".termsection").show();
    }
    else {
        $(".termsection").hide();
        $("#txtTermDesc").val("");
        $("#hdnQuotationTermDetailId").val("0");
        $("#hdnTermSequence").val("0");
        $("#btnAddTerm").show();
        $("#btnUpdateTerm").hide();
    }
}

function SendMail()
{
    var hdnQuotationId = $("#hdnQuotationId");
    var requestData = { quotationId: hdnQuotationId.val(), reportType: "PDF" };
    $.ajax({
        url: "../Quotation/QuotationMail",
        cache: false,
        type: "GET",
        dataType: "json",
        data: requestData,
        contentType: 'application/json',
        success: function (data) {
            if (data.status == "SUCCESS") {
                ShowModel("Alert", data.message);
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
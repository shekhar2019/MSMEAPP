$(document).ready(function () {
    $("#tabs").tabs({
        collapsible: true
    });
    $("#txtPONo").attr('readOnly', true);
    $("#txtPODate").attr('readOnly', true);
    
    $("#txtVendorName").attr('readOnly', true);
    $("#txtVendorCode").attr('readOnly', true);
    $("#txtRefDate").attr('readOnly', true);
    $("#txtCreatedBy").attr('readOnly', true);
    $("#txtCreatedDate").attr('readOnly', true);
    $("#txtModifiedBy").attr('readOnly', true);
    $("#txtModifiedDate").attr('readOnly', true);
    $("#txtRejectedDate").attr('readOnly', true);
    $("#txtUOMName").attr('readOnly', true);
    $("#txtProductCode").attr('readOnly', true);
    $("#txtTotalPrice").attr('readOnly', true);
    $("#txtBasicValue").attr('readOnly', true);

    $("#txtTaxPercentage").attr('readOnly', true);
    $("#txtTaxAmount").attr('readOnly', true);
    $("#txtTotalValue").attr('readOnly', true);

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
            GetVendorDetail(ui.item.value);
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
                url: "../PO/GetTaxAutoCompleteList",
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
                $("#txtTaxAmount").val(Math.round(taxAmount).toFixed(2));
            }
            else
            {
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
      .append("<div><b>" + item.label + " || " + item.code + "</b></div>")
      .appendTo(ul);
};


    $("#txtPODate,#txtRefDate").datepicker({
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
    var hdnPOId = $("#hdnPOId");
    if (hdnPOId.val() != "" && hdnPOId.val() != "0" && hdnAccessMode.val() != "" && hdnAccessMode.val() != "0") {
        setTimeout(
       function () {
           GetPODetail(hdnPOId.val());
       }, 1000);



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
            $("#btnSave").show(); 
            $("#btnReset").show();
        }
    }
    else {
        $("#btnSave").show(); 
        $("#btnReset").show();
    }

    var poProducts = [];
    GetPOProductList(poProducts);
    var poTaxes = [];
    GetPOTaxList(poTaxes);


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
function RemoveRow(obj) {
    if (confirm("Do you want to remove selected Branch?")) {
        var row = $(obj).closest("tr");
        var customerBranchId = $(row).find("#hdnCustomerBranchId").val();

        $.ajax({
            type: "POST",
            url: "../Customer/RemoveCustomerBranch",
            data: { customerBranchId: customerBranchId }
        }).success(function (data) {
            if (data.status == "SUCCESS") {
                ShowModel("Alert", data.message);
                row.remove();
            }
            else {
                ShowModel("Error", data.message)
            }

        }).error(function (err) {

            ShowModel("Error", err)
        });
    }
}
function EditRow(obj) {

    var row = $(obj).closest("tr");
    var customerBranchId = $(row).find("#hdnCustomerBranchId").val();
    var branchName = $(row).find("#hdnBranchName").val();
    var bAddress = $(row).find("#hdnBAddress").val();
    var bCity = $(row).find("#hdnBCity").val();
    var bStateId = $(row).find("#hdnBStateId").val();
    var bStateName = $(row).find("#hdnBStateName").val();
    var bCountryId = $(row).find("#hdnBCountryId").val();
    var bPinCode = $(row).find("#hdnBPinCode").val();
    var bCSTNo = $(row).find("#hdnBCSTNo").val();
    var bTINNo = $(row).find("#hdnBTINNo").val();
    var bPANNo = $(row).find("#hdnBPANNo").val();
    var bGSTNo = $(row).find("#hdnBGSTNo").val();
    var bContactPersonName = $(row).find("#hdnBContactPersonName").val();
    var bDesignation = $(row).find("#hdnBDesignation").val();
    var bEmail = $(row).find("#hdnBEmail").val();
    var bMobileNo = $(row).find("#hdnBMobileNo").val();
    var bContactNo = $(row).find("#hdnBContactNo").val();
    var bFax = $(row).find("#hdnBFax").val();

    $("#hdnCustomerBranchId").val(customerBranchId);
    $("#txtBranchName").val(branchName);
    $("#txtBContactPersonName").val(bContactPersonName);
    $("#txtBDesignation").val(bDesignation);
    $("#txtBEmail").val(bEmail);
    $("#txtBMobileNo").val(bMobileNo);
    $("#txtBContactNo").val(bContactNo);
    $("#txtBFax").val(bFax);
    $("#txtBAddress").val(bAddress);
    $("#txtBCity").val(bCity);
    $("#ddlBCountry").val(bCountryId);
    BindBranchStateList(bStateId);
    $("#ddlBState").val(bStateId);
    $("#txtBPinCode").val(bPinCode);
    $("#txtBCSTNo").val(bCSTNo);
    $("#txtBTINNo").val(bTINNo);
    $("#txtBPANNo").val(bPANNo);
    $("#txtBGSTNo").val(bGSTNo);
    $("#btnAddBranch").hide();
    $("#btnUpdateBranch").show();
}
function AddBranch(action) {
    var txtBranchName = $("#txtBranchName");
    var hdnCustomerBranchId = $("#hdnCustomerBranchId");

    var txtBContactPersonName = $("#txtBContactPersonName");
    var txtBDesignation = $("#txtBDesignation");
    var txtBEmail = $("#txtBEmail");
    var txtBMobileNo = $("#txtBMobileNo");
    var txtBContactNo = $("#txtBContactNo");

    var txtBFax = $("#txtBFax");
    var txtBAddress = $("#txtBAddress");
    var txtBCity = $("#txtBCity");
    var ddlBCountry = $("#ddlBCountry");
    var ddlBState = $("#ddlBState");
    var txtBPinCode = $("#txtBPinCode");
    var txtBCSTNo = $("#txtBCSTNo");
    var txtBTINNo = $("#txtBTINNo");
    var txtBPANNo = $("#txtBPANNo");
    var txtBGSTNo = $("#txtBGSTNo");

    if (txtBranchName.val().trim() == "") {
        ShowModel("Alert", "Please Enter Branch Name")
        txtBranchName.focus();
        return false;
    }
    if (txtBEmail.val().trim() != "" && !ValidEmailCheck(txtBEmail.val().trim())) {
        ShowModel("Alert", "Please enter Valid Email Id")
        txtBEmail.focus();
        return false;
    }

    if (txtBMobileNo.val().trim() != "" && txtBMobileNo.val().trim().length < 10) {
        ShowModel("Alert", "Please enter valid Mobile No.")
        txtBMobileNo.focus();
        return false;
    } 

    if (txtBAddress.val().trim() == "") {
        ShowModel("Alert", "Please enter Branch Address")
        txtBAddress.focus();
        return false;
    }
    if (txtBCity.val().trim() == "") {
        ShowModel("Alert", "Please enter Branch City")
        txtBCity.focus();
        return false;
    }
    if (ddlBCountry.val() == "" || ddlBCountry.val() == "0") {
        ShowModel("Alert", "Please select Branch Country")
        ddlBCountry.focus();
        return false;
    }
    if (ddlBState.val() == "" || ddlBState.val() == "0") {
        ShowModel("Alert", "Please select Branch State")
        ddlBState.focus();
        return false;
    }


    var customerBranchList = [];
    $('#tblCustomerBranchList tr').each(function (i, row) {
        var $row = $(row);
        var customerBranchId = $row.find("#hdnCustomerBranchId").val();
        var branchName = $row.find("#hdnBranchName").val();
        var bAddress = $row.find("#hdnBAddress").val();
        var bCity = $row.find("#hdnBCity").val();
        var bStateId = $row.find("#hdnBStateId").val();
        var bStateName = $row.find("#hdnBStateName").val();
        var bCountryId = $row.find("#hdnBCountryId").val();
        var bPinCode = $row.find("#hdnBPinCode").val();
        var bCSTNo = $row.find("#hdnBCSTNo").val();
        var bTINNo = $row.find("#hdnBTINNo").val();
        var bPANNo = $row.find("#hdnBPANNo").val();
        var bGSTNo = $row.find("#hdnBGSTNo").val();
        var bContactPersonName = $row.find("#hdnBContactPersonName").val();
        var bDesignation = $row.find("#hdnBDesignation").val();
        var bEmail = $row.find("#hdnBEmail").val();
        var bMobileNo = $row.find("#hdnBMobileNo").val();
        var bContactNo = $row.find("#hdnBContactNo").val();
        var bFax = $row.find("#hdnBFax").val();

        if (customerBranchId != undefined) {
            if (action == 1 || hdnCustomerBranchId.val() != customerBranchId) {

                if (branchName == txtBranchName.val().trim() && bAddress == txtBAddress.val().trim()) {
                    ShowModel("Alert", "Branch Name with same Address already exists!!!")
                    ddlBCountry.focus();
                    return false;
                }

                var customerBranch = {
                    CustomerBranchId: customerBranchId,
                    BranchName: branchName,
                    ContactPersonName: bContactPersonName,
                    Designation: bDesignation,
                    Email: bEmail,
                    MobileNo: bMobileNo,
                    ContactNo: bContactNo,
                    Fax: bFax,
                    PrimaryAddress: bAddress,
                    City: bCity,
                    StateId: bStateId,
                    StateName: bStateName,
                    CountryId: bCountryId,
                    PinCode: bPinCode,
                    CSTNo: bCSTNo,
                    TINNo: bTINNo,
                    PANNo: bPANNo,
                    GSTNo: bGSTNo

                };
                customerBranchList.push(customerBranch);
            }
        }

    });

    var customerBranchAddEdit = {
        CustomerBranchId: hdnCustomerBranchId.val(),
        BranchName: txtBranchName.val().trim(),
        ContactPersonName: txtBContactPersonName.val().trim(),
        Designation: txtBDesignation.val().trim(),
        Email: txtBEmail.val().trim(),
        MobileNo: txtBMobileNo.val().trim(),
        ContactNo: txtBContactNo.val().trim(),
        Fax: txtBFax.val().trim(),
        PrimaryAddress: txtBAddress.val().trim(),
        City: txtBCity.val().trim(),
        StateId: ddlBState.val(),
        StateName: $("#ddlBState option:selected").text(),
        CountryId: ddlBCountry.val(),
        PinCode: txtBPinCode.val().trim(),
        CSTNo: txtBCSTNo.val().trim(),
        TINNo: txtBTINNo.val().trim(),
        PANNo: txtBPANNo.val().trim(),
        GSTNo: txtBGSTNo.val().trim()

    };
    customerBranchList.push(customerBranchAddEdit);
    GetCustomerBranchList(customerBranchList);

}
function ClearBranchFields() {
    $("#hdnCustomerBranchId").val("0");
    $("#txtBranchName").val("");
    $("#txtBContactPersonName").val("");
    $("#txtBDesignation").val("");
    $("#txtBEmail").val("");
    $("#txtBMobileNo").val("");
    $("#txtBContactNo").val("");
    $("#txtBFax").val("");
    $("#txtBAddress").val("");
    $("#txtBCity").val("");
    $("#ddlBCountry").val("0");
    $("#ddlBState").val("0");
    $("#txtBPinCode").val("");
    $("#txtBCSTNo").val("");
    $("#txtBTINNo").val("");
    $("#txtBPANNo").val("");
    $("#txtBGSTNo").val("");
    $("#btnAddBranch").show();
    $("#btnUpdateBranch").hide();
}
function GetCustomerTaxList(customerBranchs) {
    var hdnCustomerId = $("#hdnCustomerId");
    var requestData = { customerBranchs: customerBranchs, customerId: hdnCustomerId.val() };
    $.ajax({
        url: "../Customer/GetCustomerBranchList",
        cache: false,
        data: JSON.stringify(requestData),
        dataType: "html",
        contentType: "application/json; charset=utf-8",
        type: "POST",
        error: function (err) {
            $("#divBranchList").html("");
            $("#divBranchList").html(err);
        },
        success: function (data) {
            $("#divBranchList").html("");
            $("#divBranchList").html(data);
            ClearBranchFields();
        }
    });
}


function GetPOProductList(poProducts) {
    var hdnPOId = $("#hdnPOId");
    var requestData = { poProducts: poProducts, poId: hdnPOId.val() };
    $.ajax({
        url: "../PO/GetPOProductList",
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
    $('#tblPOProductList tr').each(function (i, row) {
        var $row = $(row);
        var poProductDetailId = $row.find("#hdnPOProductDetailId").val();
        var totalPrice = $row.find("#hdnTotalPrice").val();
        if (poProductDetailId != undefined) {
            basicValue += parseFloat(totalPrice);
        }

    });
    $('#tblPOTaxList tr').each(function (i, row) {
        var $row = $(row);
        var poTaxDetailId = $row.find("#hdnPOTaxDetailId").val();
        var taxPercentage = $row.find("#hdnTaxPercentage").val();
        //var taxAmount = $row.find("#hdnTaxAmount").val();
        var taxAmount = 0;
        if (poTaxDetailId != undefined) {
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

    var freightValue = $("#txtFreightValue").val() == "" ? "0" : $("#txtFreightValue").val();
    var loadingValue = $("#txtLoadingValue").val() == "" ? "0" : $("#txtLoadingValue").val();
    if (parseFloat(freightValue) <= 0) {
        freightValue = 0;
    }
    if (parseFloat(loadingValue) <= 0) {
        loadingValue = 0;
    }

    $("#txtBasicValue").val(basicValue.toFixed(2));

    $("#txtTotalValue").val(parseFloat(parseFloat(basicValue) + parseFloat(taxValue) + parseFloat(freightValue) + parseFloat(loadingValue)).toFixed(2));
}
function AddProduct(action) {
    var txtProductName = $("#txtProductName");
    var hdnPOProductDetailId = $("#hdnPOProductDetailId");
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

    var poProductList = [];
    $('#tblPOProductList tr').each(function (i, row) {
        var $row = $(row);
        var poProductDetailId = $row.find("#hdnPOProductDetailId").val();
        var productId = $row.find("#hdnProductId").val();
        var productName = $row.find("#hdnProductName").val();
        var productCode = $row.find("#hdnProductCode").val();
        var productShortDesc = $row.find("#hdnProductShortDesc").val();
        var uomName = $row.find("#hdnUOMName").val();
        var price = $row.find("#hdnPrice").val();
        var quantity = $row.find("#hdnQuantity").val();
        var totalPrice = $row.find("#hdnTotalPrice").val();

        if (poProductDetailId != undefined) {
            if (action == 1 || hdnPOProductDetailId.val() != poProductDetailId) {

                if (productId == hdnProductId.val()) {
                    ShowModel("Alert", "Product already added!!!")
                    txtProductName.focus();
                    return false;
                }

                var poProduct = {
                    POProductDetailId: poProductDetailId,
                    ProductId: productId,
                    ProductName: productName,
                    ProductCode: productCode,
                    ProductShortDesc: productShortDesc,
                    UOMName: uomName,
                    Price: price,
                    Quantity: quantity,
                    TotalPrice: totalPrice
                };
                poProductList.push(poProduct);
            }
        }

    });

    var poProductAddEdit = {
        POProductDetailId: hdnPOProductDetailId.val(),
        ProductId: hdnProductId.val(),
        ProductName: txtProductName.val().trim(),
        ProductCode: txtProductCode.val().trim(),
        ProductShortDesc: txtProductShortDesc.val().trim(),
        UOMName: txtUOMName.val().trim(),
        Price: txtPrice.val().trim(),
        Quantity: txtQuantity.val().trim(),
        TotalPrice: txtTotalPrice.val().trim()
    };

    poProductList.push(poProductAddEdit);
    GetPOProductList(poProductList);

}
function EditProductRow(obj) {

    var row = $(obj).closest("tr");
    var poProductDetailId = $(row).find("#hdnPOProductDetailId").val();
    var productId = $(row).find("#hdnProductId").val();
    var productName = $(row).find("#hdnProductName").val();
    var productCode = $(row).find("#hdnProductCode").val();
    var productShortDesc = $(row).find("#hdnProductShortDesc").val();
    var uomName = $(row).find("#hdnUOMName").val();
    var price = $(row).find("#hdnPrice").val();
    var quantity = $(row).find("#hdnQuantity").val();
    var totalPrice = $(row).find("#hdnTotalPrice").val();

    $("#txtProductName").val(productName);
    $("#hdnPOProductDetailId").val(poProductDetailId);
    $("#hdnProductId").val(productId);
    $("#txtProductCode").val(productCode);
    $("#txtProductShortDesc").val(productShortDesc);
    $("#txtUOMName").val(uomName);
    $("#txtPrice").val(price);
    $("#txtQuantity").val(quantity);
    $("#txtTotalPrice").val(totalPrice);

    $("#btnAddProduct").hide();
    $("#btnUpdateProduct").show();
    ShowHideProductPanel(1);
}
function RemoveProductRow(obj) {
    if (confirm("Do you want to remove selected Product?")) {
        var row = $(obj).closest("tr");
        var poProductDetailId = $(row).find("#hdnPOProductDetailId").val();
        ShowModel("Alert", "Product Removed from List.");
        row.remove();
        CalculateGrossandNetValues();
    }
}

function GetPOTermList(poTerms) {
    var hdnPOId = $("#hdnPOId");
    var requestData = { poTerms: poTerms, poId: hdnPOId.val() };
    $.ajax({
        url: "../PO/GetPOTermList",
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

function BindTermTemplateList() {
    $.ajax({
        type: "GET",
        url: "../PO/GetTermTemplateList",
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
    var poTermList = [];
    if (termTemplateId != undefined && termTemplateId != "" && termTemplateId != "0") {
        var data = { termTemplateId: termTemplateId };

        $.ajax({
            type: "GET",
            url: "../PO/GetTermTemplateDetailList",
            data: data,
            asnc: false,
            dataType: "json",
            success: function (data) {
                var termCounter = 1;

                $.each(data, function (i, item) {
                    var poTerm = {
                        POTermDetailId: item.TrnId,
                        TermDesc: item.TermsDesc,
                        TermSequence: termCounter
                    };
                   poTermList.push(poTerm);
                    termCounter += 1;
                });
                GetPOTermList(poTermList);
            },
            error: function (Result) {
                GetPOTermList(poTermList);
            }
        });
    }
    else {
        GetPOTermList(poTermList);
    }
}
function GetPOTermList(poTerms) {
    var hdnPOId = $("#hdnPOId");
    var requestData = { poTerms: poTerms, poId: hdnPOId.val() };
    $.ajax({
        url: "../PO/GetPOTermList",
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
    var hdnPOTermDetailId = $("#hdnPOTermDetailId");
    var hdnTermSequence = $("#hdnTermSequence");


    if (txtTermDesc.val().trim() == "") {
        ShowModel("Alert", "Please Enter Terms")
        txtTermDesc.focus();
        return false;
    }

    var poTermList = [];
    var termCounter = 1;
    $('#tblPOTermList tr').each(function (i, row) {
        var $row = $(row);
        var poTermDetailId = $row.find("#hdnPOTermDetailId").val();
        var termDesc = $row.find("#hdnTermDesc").val();
        var termSequence = $row.find("#hdnTermSequence").val();

        if (poTermDetailId != undefined) {
            if (action == 1 || hdnPOTermDetailId.val() != poTermDetailId) {

                if (termSequence == 0)
                { termSequence = termCounter; }

                var poTerm = {
                    POTermDetailId:poTermDetailId,
                    TermDesc: termDesc,
                    TermSequence: termSequence
                };
                poTermList.push(poTerm);
                termCounter += 1;
            }
        }

    });

    if (hdnTermSequence.val() == "" || hdnTermSequence.val() == "0") {
        hdnTermSequence.val(termCounter);
    }
    var poTermAddEdit = {
        POTermDetailId: hdnPOTermDetailId.val(),
        TermDesc: txtTermDesc.val().trim(),
        TermSequence: hdnTermSequence.val()
    };

    poTermList.push(poTermAddEdit);
    GetPOTermList(poTermList);

}
function EditTermRow(obj) {
    var row = $(obj).closest("tr");
    var poTermDetailId = $(row).find("#hdnPOTermDetailId").val();
    var termDesc = $(row).find("#hdnTermDesc").val();
    var termSequence = $(row).find("#hdnTermSequence").val();
    $("#txtTermDesc").val(termDesc);
    $("#hdnPOTermDetailId").val(poTermDetailId);
    $("#hdnTermSequence").val(termSequence);

    $("#btnAddTerm").hide();
    $("#btnUpdateTerm").show();
    ShowHideTermPanel(1);
}
function RemoveTermRow(obj) {
    if (confirm("Do you want to remove selected Term?")) {
        var row = $(obj).closest("tr");
        var poTermDetailId = $(row).find("#hdnPOTermDetailId").val();
        ShowModel("Alert", "Term Removed from List.");
        row.remove();
    }
}

function GetPOTaxList(poTaxes) {
    var hdnPOId = $("#hdnPOId");
    var requestData = { poTaxes: poTaxes, poId: hdnPOId.val() };
    $.ajax({
        url: "../PO/GetPOTaxList",
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

    var txtBasicValue = $("#txtBasicValue");
    var txtTaxName = $("#txtTaxName");
    var hdnPOTaxDetailId = $("#hdnPOTaxDetailId");
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

    var poTaxList = [];
    $('#tblPOTaxList tr').each(function (i, row) {
        var $row = $(row);
        var poTaxDetailId = $row.find("#hdnPOTaxDetailId").val();
        var taxId = $row.find("#hdnTaxId").val();
        var taxName = $row.find("#hdnTaxName").val();
        var taxPercentage = $row.find("#hdnTaxPercentage").val();
        var taxAmount = $row.find("#hdnTaxAmount").val();

        if (poTaxDetailId != undefined) {
            if (action == 1 || hdnPOTaxDetailId.val() != poTaxDetailId) {

                if (taxId == hdnTaxId.val()) {
                    ShowModel("Alert", "Tax already added!!!")
                    txtTaxName.focus();
                    return false;
                }

                var poTax = {
                    POTaxDetailId: poTaxDetailId,
                    TaxId: taxId,
                    TaxName: taxName,
                    TaxPercentage: taxPercentage,
                    TaxAmount: taxAmount
                };
                poTaxList.push(poTax);
            }
        }

    });

    var poTaxAddEdit = {
        POTaxDetailId: hdnPOTaxDetailId.val(),
        TaxId: hdnTaxId.val(),
        TaxName: txtTaxName.val().trim(),
        TaxPercentage: txtTaxPercentage.val().trim(),
        TaxAmount: txtTaxAmount.val().trim()
    };

    poTaxList.push(poTaxAddEdit);
    GetPOTaxList(poTaxList);

}
function EditTaxRow(obj) {

    var row = $(obj).closest("tr");
    var poTaxDetailId = $(row).find("#hdnPOTaxDetailId").val();
    var taxId = $(row).find("#hdnTaxId").val();
    var taxName = $(row).find("#hdnTaxName").val();
    var taxPercentage = $(row).find("#hdnTaxPercentage").val();
    var taxAmount = $(row).find("#hdnTaxAmount").val();

    $("#txtTaxName").val(taxName);
    $("#hdnPOTaxDetailId").val(poTaxDetailId);
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
        var poTaxDetailId = $(row).find("#hdnPOTaxDetailId").val();
        ShowModel("Alert", "Tax Removed from List.");
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
function GetPODetail(poId) {
    $.ajax({
        type: "GET",
        asnc: false,
        url: "../PO/GetPODetail",
        data: { poId: poId },
        dataType: "json",
        success: function (data) {
            $("#txtPONo").val(data.PONo);
            $("#txtPODate").val(data.PODate);
            $("#hdnVendorId").val(data.VendorId);
            $("#txtVendorCode").val(data.VendorCode);
            $("#txtVendorName").val(data.VendorName);
            $("#txtAddress").val(data.BillingAddress);
            $("#txtShippingAddress").val(data.ShippingAddress);
            $("#txtCity").val(data.City);
            $("#ddlCountry").val(data.CountryId);
            BindStateList(data.StateId);
            $("#ddlState").val(data.StateId);
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
            $("#txtFreightValue").val(data.FreightValue);
            $("#txtLoadingValue").val(data.LoadingValue);
            $("#txtRemarks1").val(data.Remarks1)
            $("#txtRemarks2").val(data.Remarks2)
            $("#btnAddNew").show();
            $("#btnPrint").show();
          

        },
        error: function (Result) {
            ShowModel("Alert", "Problem in Request");
        }
    });

}


function SaveData() {
    var txtPONo = $("#txtPONo");
    var hdnPOId = $("#hdnPOId");
    var txtPODate = $("#txtPODate");
    var hdnVendorId = $("#hdnVendorId");
    var txtVendorName = $("#txtVendorName");
    var txtAddress = $("#txtAddress");
    var txtShippingAddress = $("#txtShippingAddress");
    var txtCity = $("#txtCity");
    var ddlCountry = $("#ddlCountry");
    var ddlState = $("#ddlState");
    var txtPinCode = $("#txtPinCode");
    var txtCSTNo = $("#txtCSTNo");
    var txtTINNo = $("#txtTINNo");
    var txtPANNo = $("#txtPANNo");
    var txtRefNo = $("#txtRefNo");


    var ddlRevisedApprovalStatus = $("#ddlRevisedApprovalStatus");

    var txtRefDate = $("#txtRefDate");
    var txtBasicValue = $("#txtBasicValue");
    var txtTotalValue = $("#txtTotalValue");
    var txtFreightValue = $("#txtFreightValue");
    var txtLoadingValue = $("#txtLoadingValue");
    var txtRemarks1 = $("#txtRemarks1");
    var txtRemarks2 = $("#txtRemarks2");

    if (txtVendorName.val().trim() == "") {
        ShowModel("Alert", "Please Enter Vendor Name")
        txtVendorName.focus();
        return false;
    }
    if (hdnVendorId.val() == "" || hdnVendorId.val() == "0") {
        ShowModel("Alert", "Please select vendor from list")
        txtVendorName.focus();
        return false;
    }
    if (txtAddress.val().trim() == "") {
        ShowModel("Alert", "Please Enter Vendor Billing Address")
        txtAddress.focus();
        return false;
    }
    if (txtShippingAddress.val().trim() == "") {
        ShowModel("Alert", "Please Enter Vendor Shipping Address")
        txtShippingAddress.focus();
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
    if (txtBasicValue.val() == "" || parseFloat(txtBasicValue.val()) <= 0) {
        ShowModel("Alert", "Please select at least one Product")
        return false;
    }
    var poViewModel = {
        POId: hdnPOId.val(),
        PONo: txtPONo.val().trim(),
        PODate: txtPODate.val().trim(),
        VendorId: hdnVendorId.val().trim(),
        VendorName: txtVendorName.val().trim(),
        BillingAddress: txtAddress.val().trim(),
        ShippingAddress: txtShippingAddress.val().trim(),
         City: txtCity.val().trim(),
        StateId: ddlState.val(),
        CountryId: ddlCountry.val(),
        ApprovalStatus:ddlRevisedApprovalStatus.val(),
        PinCode: txtPinCode.val().trim(),
        CSTNo: txtCSTNo.val().trim(),
        TINNo: txtTINNo.val().trim(),
        PANNo: txtPANNo.val().trim(),
        RefNo: txtRefNo.val().trim(),
        RefDate: txtRefDate.val(),
        BasicValue: txtBasicValue.val(),
        TotalValue: txtTotalValue.val(),
        FreightValue: txtFreightValue.val(),
        LoadingValue: txtLoadingValue.val(),
        Remarks1:txtRemarks1.val(),
        Remarks2:txtRemarks2.val()
        
    };

    var poProductList = [];
    $('#tblPOProductList tr').each(function (i, row) {
        var $row = $(row);
        var poProductDetailId = $row.find("#hdnPOProductDetailId").val();
        var productId = $row.find("#hdnProductId").val();
        var productName = $row.find("#hdnProductName").val();
        var productCode = $row.find("#hdnProductCode").val();
        var productShortDesc = $row.find("#hdnProductShortDesc").val();
        var uomName = $row.find("#hdnUOMName").val();
        var price = $row.find("#hdnPrice").val();
        var quantity = $row.find("#hdnQuantity").val();
        var totalPrice = $row.find("#hdnTotalPrice").val();

        if (poProductDetailId != undefined) {

            var poProduct = {
                POProductDetailId: poProductDetailId,
                ProductId: productId,
                ProductName: productName,
                ProductCode: productCode,
                ProductShortDesc: productShortDesc,
                UOMName: uomName,
                Price: price,
                Quantity: quantity,
                TotalPrice: totalPrice
            };
            poProductList.push(poProduct);
        }
    });

    var poTaxList = [];
    $('#tblPOTaxList tr').each(function (i, row) {
        var $row = $(row);
        var poTaxDetailId = $row.find("#hdnPOTaxDetailId").val();
        var taxId = $row.find("#hdnTaxId").val();
        var taxName = $row.find("#hdnTaxName").val();
        var taxPercentage = $row.find("#hdnTaxPercentage").val();
        var taxAmount = $row.find("#hdnTaxAmount").val();

        if (poTaxDetailId != undefined) {
            var poTax = {
                poTaxDetailId: poTaxDetailId,
                TaxId: taxId,
                TaxName: taxName,
                TaxPercentage: taxPercentage,
                TaxAmount: taxAmount
            };
            poTaxList.push(poTax);
        }

    });

    var poTermList = [];
    $('#tblPOTermList tr').each(function (i, row) {
        var $row = $(row);
        var poTermDetailId = $row.find("#hdnPOTermDetailId").val();
        var termDesc = $row.find("#hdnTermDesc").val();
        var termSequence = $row.find("#hdnTermSequence").val();

        if (poTermDetailId != undefined) {
            var poTerm = {
                POTermDetailId: poTermDetailId,
                TermDesc: termDesc,
                TermSequence: termSequence
            };
            poTermList.push(poTerm);
        }

    });



    var requestData = { poViewModel: poViewModel, poProducts: poProductList, poTaxes: poTaxList, poTerms: poTermList };
    $.ajax({
        url: "../PO/AddRevisedPO",
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
                      window.location.href = "../PO/AddEditPO?POId=" + data.trnId + "&AccessMode=2";
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

    $("#txtPONo").val("");
    $("#hdnPOId").val("0");
    $("#txtPODate").val($("#hdnCurrentDate").val());
    $("#hdnVendorId").val("0");
    $("#txtVendorName").val("");
    $("#txtVendorCode").val("");
    $("#txtAddress").val("");
    $("#txtCity").val("");
    $("#ddlCountry").val("0");
    $("#ddlState").val("0");
    $("#txtPinCode").val("");
    $("#ddlRevisedApprovalStatus").val("Final");

    $("#txtCSTNo").val("");
    $("#txtTINNo").val("");
    $("#txtPANNo").val("");
    $("#txtRefNo").val("");
    $("#txtRefDate").val("");

    $("#btnSave").show();
    $("#btnUpdate").hide();


}
function GetVendorDetail(vendorId) {
    $.ajax({
        type: "GET",
        asnc: false,
        url: "../Vendor/GetVendorDetail",
        data: { vendorId: vendorId },
        dataType: "json",
        success: function (data) {
            $("#txtAddress").val(data.Address);
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

}
function ShowHideProductPanel(action) {
    if (action == 1) {
        $(".productsection").show();
    }
    else {
        $(".productsection").hide();
        $("#txtProductName").val("");
        $("#hdnProductId").val("0");
        $("#hdnPOProductDetailId").val("0");
        $("#txtProductCode").val("");
        $("#txtProductShortDesc").val("");
        $("#txtPrice").val("");
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
        $("#hdnPOTaxDetailId").val("0");
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
        $("#hdnPOTermDetailId").val("0");
        $("#hdnTermSequence").val("0");
        $("#btnAddTerm").show();
        $("#btnUpdateTerm").hide();
    }
}
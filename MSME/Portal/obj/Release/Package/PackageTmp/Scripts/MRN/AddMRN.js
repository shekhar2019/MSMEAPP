$(document).ready(function () {
    $("#tabs").tabs({
        collapsible: true
    });
    $("#txtMRNNo").attr('readOnly', true);
    $("#txtMRNDate").attr('readOnly', true);
    $("#txtInvoiceNo").attr('readOnly', true);
    $("#txtInvoiceDate").attr('readOnly', true);
    $("#txtSearchFromDate").attr('readOnly', true);
    $("#txtSearchToDate").attr('readOnly', true);
    $("#txtDispatchRefDate").attr('readOnly', true);
    $("#txtLRDate").attr('readOnly', true);
    $("#txtVendorCode").attr('readOnly', true);
    $("#txtRefDate").attr('readOnly', true);
    $("#txtCreatedBy").attr('readOnly', true);
    $("#txtCreatedDate").attr('readOnly', true); 
    $("#txtModifiedBy").attr('readOnly', true);
    $("#txtModifiedDate").attr('readOnly', true);
    $("#txtRejectedDate").attr('readOnly', true);

    $("#txtProductCode").attr('readOnly', true);
    $("#txtDiscountAmount").attr('readOnly', true);
    $("#txtTaxAmount").attr('readOnly', true);
    
    $("#txtTotalPrice").attr('readOnly', true);
    $("#txtBasicValue").attr('readOnly', true);

    $("#txtTaxPercentage").attr('readOnly', true);
    $("#txtTaxAmount").attr('readOnly', true);
    $("#txtTotalValue").attr('readOnly', true);

    BindCompanyBranchList();
    

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
                url: "../DeliveryChallan/GetTaxAutoCompleteList",
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


    $("#txtProductTaxName").autocomplete({
        minLength: 0,
        source: function (request, response) {
            $.ajax({
                url: "../DeliveryChallan/GetTaxAutoCompleteList",
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
            $("#txtProductTaxName").val(ui.item.label);
            return false;
        },
        select: function (event, ui) {
            $("#txtProductTaxName").val(ui.item.label);
            $("#hdnProductTaxId").val(ui.item.value);
            $("#hdnProductTaxPerc").val(ui.item.percentage);
           // CalculateTotalCharges();
            //if (parseFloat($("#txtBasicValue").val()) > 0) {
            //    var taxAmount = (parseFloat($("#txtBasicValue").val()) * (parseFloat($("#txtTaxPercentage").val()) / 100));
            //    $("#txtProductTaxAmount").val(taxAmount.toFixed(2));
            //}
            //else {
            //    $("#txtProductTaxAmount").val("0");
            //}
            return false;
        },
        change: function (event, ui) {
            if (ui.item == null) {
                $("#txtProductTaxName").val("");
                $("#hdnProductTaxId").val("0");
                $("#hdnProductTaxPerc").val("0");
                $("#txtProductTaxAmount").val("0");
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

    $("#txtChallanDate,#txtLRDate,#txtDispatchRefDate").datepicker({
        changeMonth: true,
        changeYear: true,
        dateFormat: 'dd-M-yy',
        yearRange: '-10:+100',
        onSelect: function (selected) {

        }
    });
    $("#txtSearchFromDate,#txtSearchToDate").datepicker({
        changeMonth: true,
        changeYear: true,
        dateFormat: 'dd-M-yy',
        yearRange: '-10:+100',
        onSelect: function (selected) {

        }
    });
 var hdnAccessMode = $("#hdnAccessMode");
    var hdnmrnId = $("#hdnmrnId");
    if (hdnmrnId.val() != "" && hdnmrnId.val() != "0" && hdnAccessMode.val() != "" && hdnAccessMode.val() != "0") {
        setTimeout(
       function () {
           GetMRNDetail(hdnmrnId.val());
           
       }, 2000);
        //var vendord = $("#hdnCustomerId").val();
    //    BindCustomerBranchList(customerId);
      

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

    var mrnProducts = [];
    GetMRNProductList(mrnProducts);
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
function GetProductDetail(productId) {
    $.ajax({
        type: "GET",
        asnc: false,
        url: "../Product/GetProductDetail",
        data: { productid: productId },
        dataType: "json",
        success: function (data) {
            $("#txtPrice").val(data.SalePrice);
           // $("#txtUOMName").val(data.UOMName);
        },
        error: function (Result) {
            ShowModel("Alert", "Problem in Request");
        }
    });

}
function AddProduct(action) {
    var txtProductName = $("#txtProductName");
    var hdnMRNProductDetailId = $("#hdnMRNProductDetailId");
    var hdnProductId = $("#hdnProductId");
    var txtProductCode = $("#txtProductCode");
    var txtProductShortDesc = $("#txtProductShortDesc");
    var txtPrice = $("#txtPrice");
    var txtQuantity = $("#txtQuantity");
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

    var mrnProductList = [];
    $('#tblMRNProductList tr').each(function (i, row) {
        var $row = $(row);
        var mrnProductDetailId = $row.find("#hdnMRNProductDetailId").val();
        var productId = $row.find("#hdnProductId").val();
        var productName = $row.find("#hdnProductName").val();
        var productCode = $row.find("#hdnProductCode").val();
        var productShortDesc = $row.find("#hdnProductShortDesc").val();
        var price = $row.find("#hdnPrice").val();
        var quantity = $row.find("#hdnQuantity").val();
        if (mrnProductDetailId != undefined) {
            if (action == 1 || hdnMRNProductDetailId.val() != mrnProductDetailId) {

                if (productId == hdnProductId.val()) {
                    ShowModel("Alert", "Product already added!!!")
                    txtProductName.focus();
                    return false;
                }

                var mrnProduct = {
                    MRNProductDetailId: mrnProductDetailId,
                    ProductId: productId,
                    ProductName: productName,
                    ProductCode: productCode,
                    ProductShortDesc: productShortDesc,
                    Price: price,
                    Quantity: quantity
                    
                };
                mrnProductList.push(mrnProduct);
            }
        }

    });

    var mrnProductAddEdit = {
        MRNProductDetailId: hdnMRNProductDetailId.val(),
        ProductId: hdnProductId.val(),
        ProductName: txtProductName.val().trim(),
        ProductCode: txtProductCode.val().trim(),
        ProductShortDesc: txtProductShortDesc.val().trim(),
        Price: txtPrice.val().trim(),
        Quantity: txtQuantity.val().trim()
    };

    mrnProductList.push(mrnProductAddEdit);
    GetMRNProductList(mrnProductList);

}
function GetMRNProductList(mrnProducts) {
    var hdnmrnId = $("#hdnmrnId");
    var requestData = { mrnProducts: mrnProducts, mrnId: hdnmrnId.val() };
    $.ajax({
        url: "../MRN/GetMRNProductList",
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


           // CalculateGrossandNetValues();
            ShowHideProductPanel(2);
        }
    });
}


function EditProductRow(obj) {

    var row = $(obj).closest("tr");
    var challanProductDetailId = $(row).find("#hdnMRNProductDetailId").val();
    var productId = $(row).find("#hdnProductId").val();
    var productName = $(row).find("#hdnProductName").val();
    var productCode = $(row).find("#hdnProductCode").val();
    var productShortDesc = $(row).find("#hdnProductShortDesc").val();
    var uomName = $(row).find("#hdnUOMName").val();
    var price = $(row).find("#hdnPrice").val();
    var quantity = $(row).find("#hdnQuantity").val();
    $("#txtProductName").val(productName);
    $("#hdnMRNProductDetailId").val(challanProductDetailId);
    $("#hdnProductId").val(productId);
    $("#txtProductCode").val(productCode);
    $("#txtProductShortDesc").val(productShortDesc);
    $("#txtPrice").val(price);
    $("#txtQuantity").val(quantity);

    $("#btnAddProduct").hide();
    $("#btnUpdateProduct").show();
    ShowHideProductPanel(1);
}

function RemoveProductRow(obj) {
    if (confirm("Do you want to remove selected Product?")) {
        var row = $(obj).closest("tr");
        var challanProductDetailId = $(row).find("#hdnChallanProductDetailId").val();
        ShowModel("Alert", "Product Removed from List.");
        row.remove();

    }
}


function SaveData() {
    var txtMRNNo = $("#txtMRNNo");
    var hdnmrnId = $("#hdnmrnId");
    var txtMRNDate = $("#txtMRNDate");
    var txtGRNo = $("#txtGRNo") ;
    var txtGRDate = $("#txtGRDate");
    var hdnInvoiceId = $("#hdnInvoiceId");
    var txtInvoiceNo = $("#txtInvoiceNo");
    var hdnVendorId = $("#hdnVendorId");
    var txtVendorName = $("#txtVendorName");

    var txtSAddress = $("#txtSAddress");
    var txtSCity = $("#txtSCity");
    var ddlSCountry = $("#ddlSCountry");
    var ddlSState = $("#ddlSState");
    var txtSPinCode = $("#txtSPinCode");
    var txtSTINNo = $("#txtSTINNo");
    var txtSEmail = $("#txtSEmail");
    var txtSMobileNo = $("#txtSMobileNo");
    var txtSContactNo = $("#txtSContactNo");
    var txtSFax = $("#txtSFax");
    var txtSContactPerson = $("#txtSContactPerson");

    var ddlCompanyBranch = $("#ddlCompanyBranch");

    var txtDispatchRefNo = $("#txtDispatchRefNo") == "" ? "0" : $("#txtDispatchRefNo");
    var txtDispatchRefDate = $("#txtDispatchRefDate") == "" ? "0" : $("#txtDispatchRefDate");
    var txtLRNo = $("#txtLRNo") == "" ? "0" : $("#txtLRNo");
    var txtLRDate = $("#txtLRDate") == "" ? "0" : $("#txtLRDate");
    var txtTransportVia = $("#txtTransportVia") == "" ? "0" : $("#txtTransportVia");
    var txtNoOfPackets = $("#txtNoOfPackets") == "" ? "0" : $("#txtNoOfPackets");
    var txtRemarks1 = $("#txtRemarks1") == "" ? "0" : $("#txtRemarks1");
    var txtRemarks2 = $("#txtRemarks2") == "" ? "0" : $("#txtRemarks2");


    var ddlApprovalStatus = $("#ddlApprovalStatus");

    if (txtVendorName.val().trim() == "") {
        ShowModel("Alert", "Please Enter Vendor Name")
        txtVendorName.focus();
        return false;
    }
    if (hdnVendorId.val() == "" || hdnVendorId.val() == "0") {
        ShowModel("Alert", "Please select Vendor from list")
        txtCustomerName.focus();
        return false;
    }
  
    if (ddlCompanyBranch.val() == "" || ddlCompanyBranch.val() == "0") {
        ShowModel("Alert", "Please select Received At Location")
        ddlCompanyBranch.focus();
        return false;
    }
     

    var MRNViewModel = {
        MRNId: hdnmrnId.val(),
        MRNNo: txtMRNNo.val().trim(),
        MRNDate: txtMRNDate.val().trim(),
        GrNo: txtGRNo.val().trim() == "" ? "0" : txtGRNo.val().trim(),
        GrDate:txtGRDate.val().trim(),
        InvoiceId: hdnInvoiceId.val().trim(),
        InvoiceNo: txtInvoiceNo.val().trim(),
        VendorId: hdnVendorId.val().trim(),
        VendorName: txtVendorName.val().trim(),
        ContactPerson: txtSContactPerson.val().trim(),
        ShippingContactPerson: txtSContactPerson.val().trim(),
        ShippingBillingAddress: txtSAddress.val().trim(),
        ShippingCity: txtSCity.val().trim(),
        ShippingStateId: ddlSState.val(),
        ShippingCountryId: ddlSCountry.val(),
        ShippingPinCode: txtSPinCode.val().trim(),
        ShippingTINNo: txtSTINNo.val().trim(),
        ShippingEmail: txtSEmail.val().trim(),
        ShippingMobileNo: txtSMobileNo.val().trim(),
        ShippingContactNo: txtSContactNo.val().trim(),
        ShippingFax: txtSFax.val().trim(),
        CompanyBranchId: ddlCompanyBranch.val(),
        DispatchRefNo: txtDispatchRefNo.val().trim(),
        DispatchRefDate: txtDispatchRefDate.val(),
        LRNo: txtLRNo.val().trim(),
        LRDate: txtLRDate.val(),
        TransportVia: txtTransportVia.val().trim(),
        NoOfPackets: txtNoOfPackets.val(),
        Remarks1: txtRemarks1.val() == "" ? "0" : txtRemarks1.val(),
        Remarks2: txtRemarks2.val() == "" ? "0" : txtRemarks2.val(),

        ApprovalStatus: ddlApprovalStatus.val()
    };

    var mrnProductList = [];
    var productSelected = false;
    $('#tblMRNProductList tr').each(function (i, row) {
        var $row = $(row);
        var mrnProductDetailId = $row.find("#hdnMRNProductDetailId").val();
        var productId = $row.find("#hdnProductId").val();
        var productName = $row.find("#hdnProductName").val();
        var productCode = $row.find("#hdnProductCode").val();
        var productShortDesc = $row.find("#hdnProductShortDesc").val();
        var uomName = $row.find("#hdnUOMName").val();
        var price = $row.find("#hdnPrice").val();
        var quantity = $row.find("#hdnQuantity").val();
        if (mrnProductDetailId != undefined) {
            productSelected = true;
            var mrnProduct = {
                MRNProductDetailId: mrnProductDetailId,
                ProductId: productId,
                ProductName: productName,
                ProductCode: productCode,
                ProductShortDesc: productShortDesc,
                UOMName: uomName,
                Price: price,
                Quantity: quantity
            };
            mrnProductList.push(mrnProduct);
        }
    });

    if (productSelected == false) {
        ShowModel("Alert", "Please select at least one Product")
        ddlCompanyBranch.focus();
        return false; 
    }
    var requestData = { mrnViewModel:MRNViewModel, mrnProducts: mrnProductList };
    $.ajax({
        url: "../MRN/AddEditMRN",
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
                       window.location.href = "../MRN/AddEditMRN?mrnId=" + data.trnId + "&AccessMode=2";
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

    $("#txtMRNNo").val("");
    $("#hdnmrnId").val("0");
    $("#txtMRNDate").val($("#hdnCurrentDate").val());
    $("#hdnVendorId").val("0");
    $("#txtVendorName").val("");
    $("#txtVendorCode").val("");
    $("#txtInvoiceNo").val("");
    $("#txtInvoiceDate").val("");
    $("#txtInvoiceId").val("0"); 

    $("#txtSContactPerson").val("");
    $("#txtSAddress").val("");
    $("#txtSCity").val("");
    $("#ddlSCountry").val("0");
    $("#ddlSState").val("0");
    $("#txtSPinCode").val("");
    $("#txtSTINNo").val("");
    $("#txtSEmail").val("");
    $("#txtSMobileNo").val("");
    $("#txtSEmail").val("");
    $("#txtSFax").val(""); 

    $("#ddlCompanyBranch").val("0");

    $("#ddlApprovalStatus").val("Final");


    $("#txtDispatchRefNo").val("");
    $("#txtDispatchRefDate").val(""); 
    $("#txtLRNo").val("");
    $("#txtLRDate").val("");
    $("#txtTransportVia").val("");
    $("#txtNoOfPackets").val("");
    $("#txtRemarks1").val("");
    $("#txtRemarks2").val("");

    $("#btnSave").show();
    $("#btnUpdate").hide();

    


}
function ShowHideProductPanel(action) {
    if (action == 1) {
        $(".productsection").show();
    }
    else {
        $(".productsection").hide();
        $("#txtProductName").val("");
        $("#hdnProductId").val("0");
        $("#hdnMRNProductDetailId").val("0");
        $("#txtProductCode").val("");
        $("#txtProductShortDesc").val("");
        $("#txtPrice").val("");
        $("#btnAddProduct").show();
        $("#btnUpdateProduct").hide();
    }
}


function OpenInvoiceSearchPopup() {
    $("#SearchInvoiceModel").modal();

}

function SearchInvoice() {
    var txtInvoiceNo = $("#txtSearchInvoiceNo");
    var txtVendorName = $("#txtSearchVendorName");
    var txtRefNo = $("#txtSearchRefNo");
    var txtFromDate = $("#txtSearchFromDate");
    var txtToDate = $("#txtSearchToDate");

    var requestData = { piNo: txtInvoiceNo.val().trim(), vendorName: txtVendorName.val().trim(), refNo: txtRefNo.val().trim(), fromDate: txtFromDate.val(), toDate: txtToDate.val(), approvalStatus: "Final", displayType: "Popup" };
    $.ajax({
        url: "../MRN/GetPurchaseInvoiceList",
        data: requestData,
        dataType: "html",
        type: "GET",
        error: function (err) {
            $("#divInvoiceList").html("");
            $("#divInvoiceList").html(err);
        },
        success: function (data) {
            $("#divInvoiceList").html("");
            $("#divInvoiceList").html(data);
        }
    });
}
function SelectInvoice(invoiceId, invoiceNo, invoiceDate, vendorId, vendorCode, vendorName) {
    $("#txtInvoiceNo").val(invoiceNo);
    $("#hdnInvoiceId").val(invoiceId);
    $("#txtInvoiceDate").val(invoiceDate);
    $("#hdnVendorId").val(vendorId);
    $("#txtVendorCode").val(vendorCode);
    $("#txtVendorName").val(vendorName);
    $("#SearchInvoiceModel").modal('hide');
    GetVendorDetail(vendorId);
}
function GetVendorDetail(vendorId) {
    $.ajax({
        type: "GET",
        asnc: false,
        url: "../Vendor/GetVendorDetail",
        data: { vendorId: vendorId },
        dataType: "json",
        success: function (data) {
            $("#txtSAddress").val(data.Address);
            $("#txtSCity").val(data.City);
            $("#ddlSCountry").val(data.CountryId);
            $("#ddlSState").val(data.StateId);
            $("#txtSPinCode").val(data.PinCode);
            $("#txtSTINNo").val(data.TINNo);
            $("#txtSEmail").val(data.Email);
            $("#txtSMobileNo").val(data.MobileNo);
            $("#txtSContactNo").val(data.ContactNo);
            $("#txtSFax").val(data.Fax);
            $("#txtSContactPerson").val(data.ContactPersonName);
            
        },
        error: function (Result) {
            ShowModel("Alert", "Problem in Request");
        }
    });

}
function GetMRNDetail(mrnId) {
    $.ajax({
        type: "GET",
        asnc: false,
        url: "../MRN/GetMRNDetail",
        data: { mrnId: mrnId },
        dataType: "json",
        success: function (data) {
            $("#txtMRNNo").val(data.MRNNo);
            $("#txtMRNDate").val(data.MRNDate);
            $("#txtGRNo").val(data.GRNo),
            $("#txtGRDate").val(data.GRDate),
            $("#txtInvoiceNo").val(data.InvoiceNo);
            $("#hdnInvoiceId").val(data.InvoiceId);
            $("#txtInvoiceDate").val(data.InvoiceDate);

            $("#hdnVendorId").val(data.VendorId);
            $("#txtVendorCode").val(data.VendorCode);
            $("#txtVendorName").val(data.VendorName);

            $("#ddlApprovalStatus").val(data.ApprovalStatus)

            $("#txtSContactPerson").val(data.ShippingContactPerson);
            $("#txtSAddress").val(data.ShippingBillingAddress);
            $("#txtSCity").val(data.ShippingCity);
            $("#ddlSCountry").val(data.ShippingCountryId);
            $("#ddlSState").val(data.ShippingStateId);
            $("#txtSPinCode").val(data.ShippingPinCode);
            $("#txtSTINNo").val(data.ShippingTINNo);
            $("#txtSEmail").val(data.ShippingEmail);
            $("#txtSMobileNo").val(data.ShippingMobileNo);
            $("#txtSContactNo").val(data.ShippingContactNo);
            $("#txtSFax").val(data.ShippingFax);

            $("#ddlCompanyBranch").val(data.CompanyBranchId);
            $("#txtDispatchRefNo").val(data.DispatchRefNo);
            $("#txtDispatchRefDate").val(data.DispatchRefDate);
            $("#txtLRNo").val(data.LRNo);
            $("#txtLRDate").val(data.LRDate);
            $("#txtTransportVia").val(data.TransportVia);
            $("#txtNoOfPackets").val(data.NoOfPackets);


            $("#txtRemarks1").val(data.Remarks1);
            $("#txtRemarks2").val(data.Remarks2);


            $("#divCreated").show();
            $("#txtCreatedBy").val(data.CreatedByUserName);
            $("#txtCreatedDate").val(data.CreatedDate);
            if (data.ModifiedByUserName != "") {
                $("#divModified").show();
                $("#txtModifiedBy").val(data.ModifiedByUserName);
                $("#txtModifiedDate").val(data.ModifiedDate);
            }

        
            $("#btnAddNew").show();
            $("#btnPrint").show();


        },
        error: function (Result) {
            ShowModel("Alert", "Problem in Request");
        }
    });


}
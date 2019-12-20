$(document).ready(function () {
    $("#tabs").tabs({
        collapsible: true
    });

    $("#txtProductCode").attr('readOnly', true);
    $("#txtProductShortDesc").attr('readOnly', true);

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

    BindCountryList();
    BindCustomerTypeList();
    $("#ddlState").append($("<option></option>").val(0).html("-Select State-"));
    $("#ddlBState").append($("<option></option>").val(0).html("-Select State-"));

    var hdnAccessMode = $("#hdnAccessMode");
    var hdnCustomerId = $("#hdnCustomerId");
    if (hdnCustomerId.val() != "" && hdnCustomerId.val() != "0" && hdnAccessMode.val() != "" && hdnAccessMode.val() != "0") {
        setTimeout(
       function () {
           GetCustomerDetail(hdnCustomerId.val());
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

    var customerBranchs = [];
    GetCustomerBranchList(customerBranchs);

    var customerProducts = [];
    GetCustomerProductList(customerProducts);

    $("#txtCustomerName").focus();


    $("#txtEmployeeName").autocomplete({
        minLength: 0,
        source: function (request, response) {
            $.ajax({
                url: "../Employee/GetEmployeeAutoCompleteList",
                type: "GET",
                dataType: "json",
                data: {
                    term: request.term, departmentId: 0, designationId: 0
                },
                success: function (data) {
                    response($.map(data, function (item) {
                        return {
                            label: item.FirstName, value: item.EmployeeId, EmployeeCode: item.EmployeeCode, MobileNo: item.MobileNo
                        };
                    }))
                }
            })
        },
        focus: function (event, ui) {
            $("#txtEmployeeName").val(ui.item.label);
            return false;
        },
        select: function (event, ui) {
            $("#txtEmployeeName").val(ui.item.label);
            $("#hdnEmployeeId").val(ui.item.value);
            return false;
        },
        change: function (event, ui) {
            if (ui.item == null) {
                $("#txtEmployeeName").val("");
                $("#hdnEmployeeId").val("0");
                ShowModel("Alert", "Please select Employee from List")

            }
            return false;
        }

    })
.autocomplete("instance")._renderItem = function (ul, item) {
    return $("<li>")
      .append("<div><b>" + item.label + " || " + item.EmployeeCode + "</b><br>" + item.MobileNo + "</div>")
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
function checkPhone(el) {
    var ex = /^[0-9]+\-?[0-9]*$/;
    if (ex.test(el.value) == false) {
        el.value = el.value.substring(0, el.value.length - 1);
    }

}

function BindCustomerTypeList() {
    $.ajax({
        type: "GET",
        url: "../Customer/GetCustomerTypeList",
        data: "{}",
        dataType: "json",
        asnc: false,
        success: function (data) {
            $("#ddlCustomerType").append($("<option></option>").val(0).html("-Select Customer Type-"));
            $.each(data, function (i, item) {

                $("#ddlCustomerType").append($("<option></option>").val(item.CustomerTypeId).html(item.CustomerTypeDesc));
            });
        },
        error: function (Result) {
            $("#ddlCustomerType").append($("<option></option>").val(0).html("-Select Customer Type-"));
        }
    });
}
function ValidEmailCheck(emailAddress) {
    var pattern = /^([a-z\d!#$%&'*+\-\/=?^_`{|}~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]+(\.[a-z\d!#$%&'*+\-\/=?^_`{|}~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]+)*|"((([ \t]*\r\n)?[ \t]+)?([\x01-\x08\x0b\x0c\x0e-\x1f\x7f\x21\x23-\x5b\x5d-\x7e\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|\\[\x01-\x09\x0b\x0c\x0d-\x7f\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))*(([ \t]*\r\n)?[ \t]+)?")@(([a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|[a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF][a-z\d\-._~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]*[a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])\.)+([a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|[a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF][a-z\d\-._~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]*[a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])\.?$/i;
    return pattern.test(emailAddress);
};
function BindCountryList() {
    $.ajax({
        type: "GET",
        url: "../Company/GetCountryList",
        data: "{}",
        dataType: "json",
        asnc: false,
        success: function (data) {
            $("#ddlCountry,#ddlBCountry").append($("<option></option>").val(0).html("-Select Country-"));
            $.each(data, function (i, item) {

                $("#ddlCountry,#ddlBCountry").append($("<option></option>").val(item.CountryId).html(item.CountryName));
            });
        },
        error: function (Result) {
            $("#ddlCountry,#ddlBCountry").append($("<option></option>").val(0).html("-Select Country-"));
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
    if (txtBContactPersonName.val().trim() == "") {
        ShowModel("Alert", "Please Enter Company/ Contact Person Name")
        txtBContactPersonName.focus();
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
function GetCustomerBranchList(customerBranchs) {
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


function GetCustomerProductList(customerProducts) {
    var hdnCustomerId = $("#hdnCustomerId");
    var requestData = { customerProducts: customerProducts, customerId: hdnCustomerId.val() };
    $.ajax({
        url: "../Customer/GetCustomerProductList",
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
            ClearProductFields();
        }
    });
}
function ClearProductFields() {
    $("#hdnMappingId").val("0");
    $("#txtProductName").val("");
    $("#hdnProductId").val("0");
    $("#txtProductCode").val("");
    $("#txtProductShortDesc").val("");
    $("#btnAddProduct").show();
    $("#btnUpdateProduct").hide();
}
function AddProduct(action) {
    var txtProductName = $("#txtProductName");
    var hdnMappingId = $("#hdnMappingId");
    var hdnProductId = $("#hdnProductId");
    var txtProductCode = $("#txtProductCode");
    var txtProductShortDesc = $("#txtProductShortDesc");

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

    var customerProductList = [];
    $('#tblCustomerProductList tr').each(function (i, row) {
        var $row = $(row);
        var mappingId = $row.find("#hdnMappingId").val();
        var productId = $row.find("#hdnProductId").val();
        var productName = $row.find("#hdnProductName").val();
        var productCode = $row.find("#hdnProductCode").val();
        var productShortDesc = $row.find("#hdnProductShortDesc").val();

        if (mappingId != undefined) {
            if (action == 1 || hdnMappingId.val() != mappingId) {

                if (productId == hdnProductId.val()) {
                    ShowModel("Alert", "Product already mapped!!!")
                    txtProductName.focus();
                    return false;
                }

                var customerProduct = {
                    MappingId: mappingId,
                    ProductId: productId,
                    ProductName: productName,
                    ProductCode: productCode,
                    ProductShortDesc: productShortDesc
                };
                customerProductList.push(customerProduct);
            }
        }

    });

    var customerProductAddEdit = {
        MappingId: hdnMappingId.val(),
        ProductId: hdnProductId.val(),
        ProductName: txtProductName.val().trim(),
        ProductCode: txtProductCode.val().trim(),
        ProductShortDesc: txtProductShortDesc.val().trim()
    };
    customerProductList.push(customerProductAddEdit);
    GetCustomerProductList(customerProductList);

}
function EditProductRow(obj) {

    var row = $(obj).closest("tr");
    var mappingId = $(row).find("#hdnMappingId").val();
    var productId = $(row).find("#hdnProductId").val();
    var productName = $(row).find("#hdnProductName").val();
    var productCode = $(row).find("#hdnProductCode").val();
    var productShortDesc = $(row).find("#hdnProductShortDesc").val();

    $("#txtProductName").val(productName);
    $("#hdnMappingId").val(mappingId);
    $("#hdnProductId").val(productId);
    $("#txtProductCode").val(productCode);
    $("#txtProductShortDesc").val(productShortDesc);

    $("#btnAddProduct").hide();
    $("#btnUpdateProduct").show();
}

function RemoveProductRow(obj) {
    if (confirm("Do you want to remove selected Product?")) {
        var row = $(obj).closest("tr");
        var mappingId = $(row).find("#hdnMappingId").val();

        $.ajax({
            type: "POST",
            url: "../Customer/RemoveCustomerProduct",
            data: { mappingId: mappingId }
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

function BindPrimaryStateList(stateId) {
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
function BindBranchStateList(stateId) {
    var countryId = $("#ddlBCountry option:selected").val();
    $("#ddlBState").val(0);
    $("#ddlBState").html("");
    if (countryId != undefined && countryId != "" && countryId != "0") {
        var data = { countryId: countryId };
        $.ajax({
            type: "GET",
            url: "../Company/GetStateList",
            data: data,
            asnc: false,
            dataType: "json",
            success: function (data) {
                $("#ddlBState").append($("<option></option>").val(0).html("-Select State-"));
                $.each(data, function (i, item) {
                    $("#ddlBState").append($("<option></option>").val(item.StateId).html(item.StateName));
                });
                $("#ddlBState").val(stateId);
            },
            error: function (Result) {
                $("#ddlBState").append($("<option></option>").val(0).html("-Select State-"));
            }
        });
    }
    else {

        $("#ddlBState").append($("<option></option>").val(0).html("-Select State-"));
    }

}

function GetCustomerDetail(customerId) {
    $.ajax({
        type: "GET",
        asnc: false,
        url: "../Customer/GetCustomerDetail",
        data: { customerId: customerId },
        dataType: "json",
        success: function (data) {
            $("#txtCustomerCode").val(data.CustomerCode);
            $("#txtCustomerName").val(data.CustomerName);
            $("#txtContactPersonName").val(data.ContactPersonName);
            $("#txtDesignation").val(data.Designation);
            $("#txtEmail").val(data.Email);
            $("#txtMobileNo").val(data.MobileNo);
            $("#txtContactNo").val(data.ContactNo);
            $("#txtFax").val(data.Fax);
            $("#txtPrimaryAddress").val(data.PrimaryAddress);
            $("#txtCity").val(data.City);
            $("#ddlCountry").val(data.CountryId);
            BindPrimaryStateList(data.StateId);
            $("#ddlState").val(data.StateId);
            $("#txtPinCode").val(data.PinCode);
            $("#txtCSTNo").val(data.CSTNo);
            $("#txtTINNo").val(data.TINNo);
            $("#txtPANNo").val(data.PANNo);
            $("#txtGSTNo").val(data.GSTNo);
            $("#txtExciseNo").val(data.ExciseNo);
            $("#ddlCustomerType").val(data.CustomerTypeId);
            $("#hdnEmployeeId").val(data.EmployeeId);
            $("#txtEmployeeName").val(data.EmployeeName);
            $("#txtCreditLimit").val(data.CreditLimit);
            $("#txtCreditDays").val(data.CreditDays);

            if (data.Customer_Status == true) {
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


function SaveData() {
    var txtCustomerName = $("#txtCustomerName");
    var hdnCustomerId = $("#hdnCustomerId");
    var txtCustomerCode = $("#txtCustomerCode");
    var txtContactPersonName = $("#txtContactPersonName");
    var txtDesignation = $("#txtDesignation");
    var txtEmail = $("#txtEmail");
    var txtMobileNo = $("#txtMobileNo");
    var txtContactNo = $("#txtContactNo");

    var txtFax = $("#txtFax");
    var txtPrimaryAddress = $("#txtPrimaryAddress");
    var txtCity = $("#txtCity");
    var ddlCountry = $("#ddlCountry");
    var ddlState = $("#ddlState");
    var txtPinCode = $("#txtPinCode");
    var txtCSTNo = $("#txtCSTNo");
    var txtTINNo = $("#txtTINNo");
    var txtPANNo = $("#txtPANNo");
    var txtGSTNo = $("#txtGSTNo");
    var txtExciseNo = $("#txtExciseNo");
    var ddlCustomerType = $("#ddlCustomerType");
    var hdnEmployeeId = $("#hdnEmployeeId");
    var txtCreditLimit = $("#txtCreditLimit");
    var txtCreditDays = $("#txtCreditDays");

    var chkStatus = $("#chkStatus").is(':checked') ? true : false; 
    if (txtCustomerName.val().trim() == "") {
        ShowModel("Alert", "Please Enter Customer Name")
        txtCustomerName.focus();
        return false;
    }
    if (txtCustomerCode.val().trim() == "") {
        ShowModel("Alert", "Please Enter Customer Code name")
        txtCustomerCode.focus();
        return false;
    }
   
    if (txtContactPersonName.val().trim() == "") {
        ShowModel("Alert", "Please Enter Contact Person name")
        txtContactPersonName.focus();
        return false;
    } 

   

    if (txtEmail.val().trim() != "" && !ValidEmailCheck(txtEmail.val().trim())) {
        ShowModel("Alert", "Please enter Valid Email Id")
        txtEmail.focus();
        return false;
    }
     

    if (txtMobileNo.val().trim() == "") {
        ShowModel("Alert", "Please enter Mobile No.")
        txtMobileNo.focus();
        return false;
    }
    if (txtMobileNo.val().trim().length < 10) {
        ShowModel("Alert", "Please enter valid Mobile No.")
        txtMobileNo.focus();
        return false;
    } 
    if (txtPrimaryAddress.val().trim() == "") {
        ShowModel("Alert", "Please enter Primary Address")
        txtPrimaryAddress.focus();
        return false;
    }
    if (txtCity.val().trim() == "") {
        ShowModel("Alert", "Please enter Primary City")
        txtCity.focus();
        return false;
    }
    if (ddlCountry.val() == "" || ddlCountry.val() == "0") {
        ShowModel("Alert", "Please select Primary Country")
        ddlCountry.focus();
        return false;
    }
    if (ddlState.val() == "" || ddlState.val() == "0") {
        ShowModel("Alert", "Please select Primary State")
        ddlState.focus();
        return false;
    }
    if (ddlCustomerType.val() == "" || ddlCustomerType.val() == "0") {
        ShowModel("Alert", "Please select Customer Type")
        ddlCustomerType.focus();
        return false;
    } 
    var customerViewModel = {
        CustomerId: hdnCustomerId.val(),
        CustomerCode: txtCustomerCode.val().trim(),
        CustomerName: txtCustomerName.val().trim(),
        ContactPersonName: txtContactPersonName.val().trim(),
        Designation: txtDesignation.val().trim(),
        Email: txtEmail.val().trim(),
        MobileNo: txtMobileNo.val().trim(),
        ContactNo: txtContactNo.val(),
        Fax: txtFax.val().trim(),
        PrimaryAddress: txtPrimaryAddress.val().trim(),
        City: txtCity.val().trim(),
        StateId: ddlState.val(),
        CountryId: ddlCountry.val(),
        PinCode: txtPinCode.val().trim(),
        CSTNo: txtCSTNo.val().trim(),
        TINNo: txtTINNo.val().trim(),
        PANNo: txtPANNo.val().trim(),
        GSTNo: txtGSTNo.val().trim(),
        ExciseNo: txtExciseNo.val().trim(),
        EmployeeId: hdnEmployeeId.val(),
        CustomerTypeId: ddlCustomerType.val(),
        CreditLimit: txtCreditLimit.val().trim(),
        CreditDays: txtCreditDays.val().trim(),
        Customer_Status: chkStatus

    };

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

    });

    var customerProductList = [];
    $('#tblCustomerProductList tr').each(function (i, row) {
        var $row = $(row);
        var mappingId = $row.find("#hdnMappingId").val();
        var productId = $row.find("#hdnProductId").val();
        var productName = $row.find("#hdnProductName").val();
        var productCode = $row.find("#hdnProductCode").val();
        var productShortDesc = $row.find("#hdnProductShortDesc").val();
        if (mappingId != undefined) {
            var customerProduct = {
                MappingId: mappingId,
                ProductId: productId,
                ProductName: productName,
                ProductCode: productCode,
                ProductShortDesc: productShortDesc
            };
            customerProductList.push(customerProduct);
        }

    });



    var requestData = { customerViewModel: customerViewModel, customerBranchs: customerBranchList, customerProducts: customerProductList };
    $.ajax({
        url: "../Customer/AddEditCustomer",
        cache: false,
        type: "POST",
        dataType: "json",
        data: JSON.stringify(requestData),
        contentType: 'application/json',
        success: function (data) {
            if (data.status == "SUCCESS") {
                ShowModel("Alert", data.message);
                ClearFields();
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

    $("#txtCustomerName").val("");
    $("#hdnCustomerId").val("0");
    $("#txtCustomerCode").val("");
    $("#txtContactPersonName").val("");
    $("#txtDesignation").val("");
    $("#txtEmail").val("");
    $("#txtMobileNo").val("");
    $("#txtContactNo").val("");
    $("#txtFax").val("");
    $("#txtPrimaryAddress").val("");
    $("#txtCity").val("");
    $("#ddlCountry").val("0");
    $("#ddlState").val("0");
    $("#txtPinCode").val("");
    $("#txtCSTNo").val("");
    $("#txtTINNo").val("");
    $("#txtPANNo").val("");
    $("#txtGSTNo").val("");
    $("#txtExciseNo").val(""); 
    $("#ddlCustomerType").val("0");
    $("#hdnEmployeeId").val("0");
    $("#txtEmployeeName").val("");
    $("#txtCreditLimit").val("0");
    $("#txtCreditDays").val("0");
    $("#chkStatus").prop("checked", true);
    $("#btnSave").show();
    $("#btnUpdate").hide();
    $("#divBranchList").html("");
    $("#divProductList").html("");

}

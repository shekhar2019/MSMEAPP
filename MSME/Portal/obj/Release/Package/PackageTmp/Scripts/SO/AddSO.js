$(document).ready(function () {
    $("#tabs").tabs({
        collapsible: true
    });
    $("#txtSONo").attr('readOnly', true);
    $("#txtSODate").attr('readOnly', true);
    $("#txtQuotationNo").attr('readOnly', true);
    $("#txtQuotationDate").attr('readOnly', true);
    $("#txtSearchFromDate").attr('readOnly', true);
    $("#txtSearchToDate").attr('readOnly', true);


    $("#txtCustomerCode").attr('readOnly', true);
    $("#txtRefDate").attr('readOnly', true);
    $("#txtPayToBookBranch").attr('readOnly', true);
    
    $("#txtCreatedBy").attr('readOnly', true);
    $("#txtCreatedDate").attr('readOnly', true);
    $("#txtModifiedBy").attr('readOnly', true);
    $("#txtModifiedDate").attr('readOnly', true);
    $("#txtRejectedDate").attr('readOnly', true);

    $("#txtProductCode").attr('readOnly', true);
    $("#txtDiscountAmount").attr('readOnly', true);
    $("#txtTaxAmount").attr('readOnly', true);
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
                url: "../SO/GetCustomerAutoCompleteList",
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

    $("#txtPayToBookName").autocomplete({
        minLength: 0,
        source: function (request, response) {
            $.ajax({
                url: "../SO/GetBookAutoCompleteList",
                type: "GET",
                dataType: "json",
                data: { term: request.term, bookType: "B" },
                success: function (data) {
                    response($.map(data, function (item) {
                        return { label: item.BookName, value: item.BookId, branch: item.BankBranch, ifsc: item.IFSC };
                    }))
                }
            })
        },
        focus: function (event, ui) {
            $("#txtPayToBookName").val(ui.item.label);
            return false;
        },
        select: function (event, ui) {
            $("#txtPayToBookName").val(ui.item.label);
            $("#hdnPayToBookId").val(ui.item.value);
            $("#txtPayToBookBranch").val(ui.item.branch);
            return false;
        },
        change: function (event, ui) {
            if (ui.item == null) {
                $("#txtPayToBookName").val("");
                $("#hdnPayToBookId").val("0");
                $("#txtPayToBookBranch").val("0");
                ShowModel("Alert", "Please select Bank from List")

            }
            return false;
        }

    })
.autocomplete("instance")._renderItem = function (ul, item) {
    return $("<li>")
      .append("<div><b>" + item.label + " || " + item.branch + "</b><br>" + item.ifsc + "</div>")
      .appendTo(ul);
};

    $("#txtTaxName").autocomplete({
        minLength: 0,
        source: function (request, response) {
            $.ajax({
                url: "../SO/GetTaxAutoCompleteList",
                type: "GET",
                dataType: "json",
                data: { term: request.term },
                success: function (data) {
                    response($.map(data, function (item) {
                        return { label: item.TaxName, value: item.TaxId, percentage: item.TaxPercentage, SurchargeName_1: item.SurchargeName_1, SurchargePercentage_1: item.SurchargePercentage_1, SurchargeName_2: item.SurchargeName_2, SurchargePercentage_2: item.SurchargePercentage_2, SurchargeName_3: item.SurchargeName_3, SurchargePercentage_3: item.SurchargePercentage_3 };
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

            $("#hdnSurchargeName_1").val(ui.item.SurchargeName_1);
            $("#hdnSurchargePercentage_1").val(ui.item.SurchargePercentage_1);
            $("#hdnSurchargeName_2").val(ui.item.SurchargeName_2);
            $("#hdnSurchargePercentage_2").val(ui.item.SurchargePercentage_2);
            $("#hdnSurchargeName_3").val(ui.item.SurchargeName_3);
            $("#hdnSurchargePercentage_3").val(ui.item.SurchargePercentage_3);
            if (parseFloat($("#txtBasicValue").val()) > 0) {
                var taxAmount = (parseFloat($("#txtBasicValue").val()) * (parseFloat($("#txtTaxPercentage").val()) / 100));
                $("#txtTaxAmount").val(taxAmount.toFixed(2));
                var surchargeAmount_1 = (parseFloat(taxAmount) * (parseFloat($("#hdnSurchargePercentage_1").val()) / 100));
                $("#txtSurchargeAmount_1").val(surchargeAmount_1.toFixed(2));
                var surchargeAmount_2 = (parseFloat(taxAmount) * (parseFloat($("#hdnSurchargePercentage_2").val()) / 100));
                $("#txtSurchargeAmount_2").val(surchargeAmount_2.toFixed(2));
                var surchargeAmount_3 = (parseFloat(taxAmount) * (parseFloat($("#hdnSurchargePercentage_3").val()) / 100));
                $("#txtSurchargeAmount_3").val(surchargeAmount_3.toFixed(2));
                var totalTaxAmount = parseFloat(taxAmount) + parseFloat(surchargeAmount_1) + +parseFloat(surchargeAmount_2) + +parseFloat(surchargeAmount_3);
                $("#txtTotalTaxAmount").val(totalTaxAmount.toFixed(2));
            }
            else {
                $("#txtTaxAmount").val("0");
                $("#txtSurchargeAmount_1").val("0");
                $("#txtSurchargeAmount_2").val("0");
                $("#txtSurchargeAmount_3").val("0");
                $("#txtTotalTaxAmount").val("0");
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
                url: "../SO/GetTaxAutoCompleteList",
                type: "GET",
                dataType: "json",
                data: { term: request.term },
                success: function (data) {
                    response($.map(data, function (item) {
                        return { label: item.TaxName, value: item.TaxId, percentage: item.TaxPercentage, SurchargeName_1: item.SurchargeName_1, SurchargePercentage_1: item.SurchargePercentage_1, SurchargeName_2: item.SurchargeName_2, SurchargePercentage_2: item.SurchargePercentage_2, SurchargeName_3: item.SurchargeName_3, SurchargePercentage_3: item.SurchargePercentage_3};
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

            $("#hdnProductSurchargeName_1").val(ui.item.SurchargeName_1);
            $("#hdnProductSurchargePercentage_1").val(ui.item.SurchargePercentage_1);
            $("#hdnProductSurchargeName_2").val(ui.item.SurchargeName_2);
            $("#hdnProductSurchargePercentage_2").val(ui.item.SurchargePercentage_2);
            $("#hdnProductSurchargeName_3").val(ui.item.SurchargeName_3);
            $("#hdnProductSurchargePercentage_3").val(ui.item.SurchargePercentage_3);
            CalculateTotalCharges();
            
            return false;
        },
        change: function (event, ui) {
            if (ui.item == null) {
                $("#txtProductTaxName").val("");
                $("#hdnProductTaxId").val("0");
                $("#hdnProductTaxPerc").val("0");
                $("#txtProductTaxAmount").val("0");

                $("#hdnProductSurchargeName_1").val("");
                $("#hdnProductSurchargePercentage_1").val("0");
                $("#hdnProductSurchargeName_2").val("");
                $("#hdnProductSurchargePercentage_2").val("0");
                $("#hdnProductSurchargeName_3").val("");
                $("#hdnProductSurchargePercentage_3").val("0");

                $("#txtProductSurchargeAmount_1").val("0");
                $("#txtProductSurchargeAmount_2").val("0");
                $("#txtProductSurchargeAmount_3").val("0");

                $("#txtProductTotalTaxAmount").val("0");


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

    $("#txtSODate,#txtRefDate").datepicker({
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


    BindTermTemplateList();
    
    var hdnAccessMode = $("#hdnAccessMode");
    var hdnSOId = $("#hdnSOId");
    if (hdnSOId.val() != "" && hdnSOId.val() != "0" && hdnAccessMode.val() != "" && hdnAccessMode.val() != "0") {
        setTimeout(
       function () {
           GetSODetail(hdnSOId.val());
           
       }, 2000);
        var customerId = $("#hdnCustomerId").val();
        BindCustomerBranchList(customerId);
      

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

    var soProducts = [];
    GetSOProductList(soProducts);
    var soTaxes = [];
    GetSOTaxList(soTaxes);
    var soTerms = [];
    GetSOTermList(soTerms);
    

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


function BindCustomerBranchList(customerId) {
    $.ajax({
        type: "GET",
        url: "../SO/GetCustomerBranchList",
        data: { customerId: customerId },
        dataType: "json",
        asnc: false,
        success: function (data) {
            $("#ddlBCustomerBranch,#ddlSCustomerBranch").append($("<option></option>").val(0).html("-Select Branch-"));
            $.each(data, function (i, item) {
                $("#ddlBCustomerBranch,#ddlSCustomerBranch").append($("<option></option>").val(item.CustomerBranchId).html(item.BranchName));
            });
        },
        error: function (Result) {
            $("#ddlBCustomerBranch,#ddlSCustomerBranch").append($("<option></option>").val(0).html("-Select Branch-"));
        }
    });
}
function BindTermTemplateList() {
    $.ajax({
        type: "GET",
        url: "../SO/GetTermTemplateList",
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
    var soTermList = [];
    if (termTemplateId != undefined && termTemplateId != "" && termTemplateId != "0") {
        var data = { termTemplateId: termTemplateId };

        $.ajax({
            type: "GET",
            url: "../SO/GetTermTemplateDetailList",
            data: data,
            asnc: false,
            dataType: "json",
            success: function (data) {
                var termCounter = 1;

                $.each(data, function (i, item) {
                    var soTerm = {
                        SOTermDetailId: item.TrnId,
                        TermDesc: item.TermsDesc,
                        TermSequence: termCounter
                    };
                    soTermList.push(soTerm);
                    termCounter += 1;
                });
                GetSOTermList(soTermList);
            },
            error: function (Result) {
                GetSOTermList(soTermList);
            }
        });
    }
    else {
        GetSOTermList(soTermList);
    }
}
function GetSOTermList(soTerms) {
    var hdnSOId = $("#hdnSOId");
    var requestData = { soTerms: soTerms, soId: hdnSOId.val() };
    $.ajax({
        url: "../SO/GetSOTermList",
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
    var hdnSOTermDetailId = $("#hdnSOTermDetailId");
    var hdnTermSequence = $("#hdnTermSequence");

    if (txtTermDesc.val().trim() == "") {
        ShowModel("Alert", "Please Enter Terms")
        txtTermDesc.focus();
        return false;
    }

    var soTermList = [];
    var termCounter = 1;
    $('#tblTermList tr').each(function (i, row) {
        var $row = $(row);
        var soTermDetailId = $row.find("#hdnSOTermDetailId").val();
        var termDesc = $row.find("#hdnTermDesc").val();
        var termSequence = $row.find("#hdnTermSequence").val();

        if (soTermDetailId != undefined) {
            if (action == 1 || hdnSOTermDetailId.val() != soTermDetailId) {

                if (termSequence == 0)
                { termSequence = termCounter; }

                var soTerm = {
                    SOTermDetailId: soTermDetailId,
                    TermDesc: termDesc,
                    TermSequence: termSequence
                };
                soTermList.push(soTerm);
                termCounter += 1;
            }
        }

    });

    if (hdnTermSequence.val() == "" || hdnTermSequence.val() == "0") {
        hdnTermSequence.val(termCounter);
    }
    var soTermAddEdit = {
        SOTermDetailId: hdnSOTermDetailId.val(),
        TermDesc: txtTermDesc.val().trim(),
        TermSequence: hdnTermSequence.val()
    };

    soTermList.push(soTermAddEdit);
    GetSOTermList(soTermList);

}
function EditTermRow(obj) {

    var row = $(obj).closest("tr");
    var soTermDetailId = $(row).find("#hdnSOTermDetailId").val();
    var termDesc = $(row).find("#hdnTermDesc").val();
    var termSequence = $(row).find("#hdnTermSequence").val();


    $("#txtTermDesc").val(termDesc);
    $("#hdnSOTermDetailId").val(soTermDetailId);
    $("#hdnTermSequence").val(termSequence);

    $("#btnAddTerm").hide();
    $("#btnUpdateTerm").show();
    ShowHideTermPanel(1);
}

function RemoveTermRow(obj) {
    if (confirm("Do you want to remove selected Term?")) {
        var row = $(obj).closest("tr");
        var soTermDetailId = $(row).find("#hdnSOTermDetailId").val();
        ShowModel("Alert", "Term Removed from List.");
        row.remove();
    }
}

function GetSOTaxList(soTaxes) {
    var hdnSOId = $("#hdnSOId");
    var requestData = { soTaxes: soTaxes, soId: hdnSOId.val() };
    $.ajax({
        url: "../SO/GetSOTaxList",
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
    var taxEntrySequence = 0;
    var flag = true;
    var hdnTaxSequenceNo = $("#hdnTaxSequenceNo");
    var txtBasicValue = $("#txtBasicValue");
    var txtTaxName = $("#txtTaxName");
    var hdnSOTaxDetailId = $("#hdnSOTaxDetailId");
    var hdnTaxId = $("#hdnTaxId");
    var txtTaxPercentage = $("#txtTaxPercentage");
    var txtTaxAmount = $("#txtTaxAmount");

    var txtTotalTaxAmount = $("#txtTotalTaxAmount");
    var txtSurchargeAmount_1 = $("#txtSurchargeAmount_1");
    var txtSurchargeAmount_2 = $("#txtSurchargeAmount_2");
    var txtSurchargeAmount_3 = $("#txtSurchargeAmount_3");
    var hdnSurchargeName_1 = $("#hdnSurchargeName_1");
    var hdnSurchargePercentage_1 = $("#hdnSurchargePercentage_1");
    var hdnSurchargeName_2 = $("#hdnSurchargeName_2");
    var hdnSurchargePercentage_2 = $("#hdnSurchargePercentage_2");
    var hdnSurchargeName_3 = $("#hdnSurchargeName_3");
    var hdnSurchargePercentage_3 = $("#hdnSurchargePercentage_3");

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

    var soTaxList = [];
    if (action == 1 && (hdnTaxSequenceNo.val() == "" || hdnTaxSequenceNo.val() == "0")) {
        taxEntrySequence = 1;
    }
    $('#tblTaxList tr').each(function (i, row) {
        var $row = $(row);
        var taxSequenceNo = $row.find("#hdnTaxSequenceNo").val();
        var soTaxDetailId = $row.find("#hdnSOTaxDetailId").val();
        var taxId = $row.find("#hdnTaxId").val();
        var taxName = $row.find("#hdnTaxName").val();
        var taxPercentage = $row.find("#hdnTaxPercentage").val();
        var taxAmount = $row.find("#hdnTaxAmount").val();

        var surchargeName_1 = $row.find("#hdnSurchargeName_1").val();
        var surchargePercentage_1 = $row.find("#hdnSurchargePercentage_1").val();
        var surchargeAmount_1 = $row.find("#hdnSurchargeAmount_1").val();

        var surchargeName_2 = $row.find("#hdnSurchargeName_2").val();
        var surchargePercentage_2 = $row.find("#hdnSurchargePercentage_2").val();
        var surchargeAmount_2 = $row.find("#hdnSurchargeAmount_2").val();

        var surchargeName_3 = $row.find("#hdnSurchargeName_3").val();
        var surchargePercentage_3 = $row.find("#hdnSurchargePercentage_3").val();
        var surchargeAmount_3 = $row.find("#hdnSurchargeAmount_3").val();

        if (taxName != undefined) {
            if (action == 1 || (hdnTaxSequenceNo.val() != taxSequenceNo)) {

                if (taxId == hdnTaxId.val()) {
                    ShowModel("Alert", "Tax already added!!!")
                    txtTaxName.focus();
                    flag = false;
                    return false;
                }

                var soTax = {
                    SOTaxDetailId: soTaxDetailId,
                    TaxSequenceNo: taxSequenceNo,
                    TaxId: taxId,
                    TaxName: taxName,
                    TaxPercentage: taxPercentage,
                    TaxAmount: taxAmount,
                    SurchargeName_1: surchargeName_1,
                    SurchargePercentage_1: surchargePercentage_1,
                    SurchargeAmount_1: surchargeAmount_1,
                    SurchargeName_2: surchargeName_2,
                    SurchargePercentage_2: surchargePercentage_2,
                    SurchargeAmount_2: surchargeAmount_2,
                    SurchargeName_3: surchargeName_3,
                    SurchargePercentage_3: surchargePercentage_3,
                    SurchargeAmount_3: surchargeAmount_3
                };
                soTaxList.push(soTax);
                taxEntrySequence = parseInt(taxEntrySequence) + 1;
            }
            else if (hdnTaxSequenceNo.val() == taxSequenceNo)
            {
                  var soTaxAddEdit = {
                                SOTaxDetailId: hdnSOTaxDetailId.val(),
                                TaxSequenceNo: hdnTaxSequenceNo.val(),
                                TaxId: hdnTaxId.val(),
                                TaxName: txtTaxName.val().trim(),
                                TaxPercentage: txtTaxPercentage.val().trim(),
                                TaxAmount: txtTaxAmount.val().trim(),
                                SurchargeName_1: hdnSurchargeName_1.val(),
                                SurchargePercentage_1: hdnSurchargePercentage_1.val(),
                                SurchargeAmount_1: txtSurchargeAmount_1.val(),
                                SurchargeName_2: hdnSurchargeName_2.val(),
                                SurchargePercentage_2: hdnSurchargePercentage_2.val(),
                                SurchargeAmount_2: txtSurchargeAmount_2.val(),
                                SurchargeName_3: hdnSurchargeName_3.val(),
                                SurchargePercentage_3: hdnSurchargePercentage_3.val(),
                                SurchargeAmount_3: txtSurchargeAmount_3.val()
                     };
                   soTaxList.push(soTaxAddEdit);
            }
        }

    });

    if (action == 1 && (hdnTaxSequenceNo.val() == "" || hdnTaxSequenceNo.val() == "0")) {
        hdnTaxSequenceNo.val(taxEntrySequence);
    }
    if (action == 1) {

        var soTaxAddEdit = {
            SOTaxDetailId: hdnSOTaxDetailId.val(),
            TaxSequenceNo: hdnTaxSequenceNo.val(),
            TaxId: hdnTaxId.val(),
            TaxName: txtTaxName.val().trim(),
            TaxPercentage: txtTaxPercentage.val().trim(),
            TaxAmount: txtTaxAmount.val().trim(),
            SurchargeName_1: hdnSurchargeName_1.val(),
            SurchargePercentage_1: hdnSurchargePercentage_1.val(),
            SurchargeAmount_1: txtSurchargeAmount_1.val(),
            SurchargeName_2: hdnSurchargeName_2.val(),
            SurchargePercentage_2: hdnSurchargePercentage_2.val(),
            SurchargeAmount_2: txtSurchargeAmount_2.val(),
            SurchargeName_3: hdnSurchargeName_3.val(),
            SurchargePercentage_3: hdnSurchargePercentage_3.val(),
            SurchargeAmount_3: txtSurchargeAmount_3.val()
        };

        soTaxList.push(soTaxAddEdit);
        hdnTaxSequenceNo.val("0");
    }
    if (flag == true) {
        GetSOTaxList(soTaxList);
    }

}
function EditTaxRow(obj) {

    var row = $(obj).closest("tr");
    var soTaxDetailId = $(row).find("#hdnSOTaxDetailId").val();
    var taxSequenceNo = $(row).find("#hdnTaxSequenceNo").val();
    var taxId = $(row).find("#hdnTaxId").val();
    var taxName = $(row).find("#hdnTaxName").val();
    var taxPercentage = $(row).find("#hdnTaxPercentage").val();
    var taxAmount = $(row).find("#hdnTaxAmount").val();

    $("#hdnTaxSequenceNo").val(taxSequenceNo);
    $("#txtTaxName").val(taxName);
    $("#hdnSOTaxDetailId").val(soTaxDetailId);
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
        var soTaxDetailId = $(row).find("#hdnSOTaxDetailId").val();
        ShowModel("Alert", "Tax Removed from List.");
        row.remove();
        CalculateGrossandNetValues();
    }
}


function AddProduct(action) {
    var productEntrySequence = 0;
    var flag = true;
    var hdnSequenceNo = $("#hdnSequenceNo");
    var txtProductName = $("#txtProductName");
    var hdnSOProductDetailId = $("#hdnSOProductDetailId");
    var hdnProductId = $("#hdnProductId");
    var txtProductCode = $("#txtProductCode");
    var txtProductShortDesc = $("#txtProductShortDesc");
    var txtPrice = $("#txtPrice");
    var txtUOMName = $("#txtUOMName");
    var txtQuantity = $("#txtQuantity");

    var txtDiscountPerc = $("#txtDiscountPerc");
    var txtDiscountAmount = $("#txtDiscountAmount");
    var txtProductTaxName = $("#txtProductTaxName");
    var hdnProductTaxId = $("#hdnProductTaxId");
    var hdnProductTaxPerc = $("#hdnProductTaxPerc");
    var txtProductTaxAmount = $("#txtProductTaxAmount");

    var txtProductSurchargeAmount_1 = $("#txtProductSurchargeAmount_1");
    var txtProductSurchargeAmount_2 = $("#txtProductSurchargeAmount_2");
    var txtProductSurchargeAmount_3 = $("#txtProductSurchargeAmount_3");
    var hdnProductSurchargeName_1 = $("#hdnProductSurchargeName_1");
    var hdnProductSurchargePercentage_1 = $("#hdnProductSurchargePercentage_1");
    var hdnProductSurchargeName_2 = $("#hdnProductSurchargeName_2");
    var hdnProductSurchargePercentage_2 = $("#hdnProductSurchargePercentage_2");
    var hdnProductSurchargeName_3 = $("#hdnProductSurchargeName_3");
    var hdnProductSurchargePercentage_3 = $("#hdnProductSurchargePercentage_3");
    var txtProductTotalTaxAmount = $("#txtProductTotalTaxAmount");

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

    if (txtDiscountPerc.val().trim() != "" && (parseFloat(txtDiscountPerc.val().trim()) < 0 || parseFloat(txtDiscountPerc.val().trim()) > 100)) {
        ShowModel("Alert", "Please enter correct discount %")
        txtDiscountPerc.focus();
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
    var soProductList = [];
    $('#tblProductList tr').each(function (i, row) {
        var $row = $(row);
        var sequenceNo = $row.find("#hdnSequenceNo").val();
        var soProductDetailId = $row.find("#hdnSOProductDetailId").val();
        var productId = $row.find("#hdnProductId").val();
        var productName = $row.find("#hdnProductName").val();
        var productCode = $row.find("#hdnProductCode").val();
        var productShortDesc = $row.find("#hdnProductShortDesc").val();
        var uomName = $row.find("#hdnUOMName").val();
        var price = $row.find("#hdnPrice").val();
        var quantity = $row.find("#hdnQuantity").val();
        var discountPerc = $row.find("#hdnDiscountPerc").val();
        var discountAmount = $row.find("#hdnDiscountAmount").val();
        var productTaxId = $row.find("#hdnProductTaxId").val();
        var productTaxPerc = $row.find("#hdnProductTaxPerc").val();
        var productTaxAmount = $row.find("#hdnProductTaxAmount").val();
        var productTaxName = $row.find("#hdnProductTaxName").val();

        var productSurchargeName_1 = $row.find("#hdnProductSurchargeName_1").val();
        var productSurchargePercentage_1 = $row.find("#hdnProductSurchargePercentage_1").val();
        var productSurchargeAmount_1 = $row.find("#hdnProductSurchargeAmount_1").val();

        var productSurchargeName_2 = $row.find("#hdnProductSurchargeName_2").val();
        var productSurchargePercentage_2 = $row.find("#hdnProductSurchargePercentage_2").val();
        var productSurchargeAmount_2 = $row.find("#hdnProductSurchargeAmount_2").val();

        var productSurchargeName_3 = $row.find("#hdnProductSurchargeName_3").val();
        var productSurchargePercentage_3 = $row.find("#hdnProductSurchargePercentage_3").val();
        var productSurchargeAmount_3 = $row.find("#hdnProductSurchargeAmount_3").val();


        var totalPrice = $row.find("#hdnTotalPrice").val();

        if (productName != undefined) {
            if (action == 1 || (hdnSequenceNo.val() != sequenceNo)) {

                if (productId == hdnProductId.val()) {
                    ShowModel("Alert", "Product already added!!!")
                    txtProductName.focus();
                    flag = false;
                    return false;
                }

                var soProduct = {
                    SequenceNo: sequenceNo,
                    SOProductDetailId: soProductDetailId,
                    ProductId: productId,
                    ProductName: productName,
                    ProductCode: productCode,
                    ProductShortDesc: productShortDesc,
                    UOMName: uomName,
                    Price: price,
                    Quantity: quantity,
                    DiscountPercentage: discountPerc,
                    DiscountAmount: discountAmount,
                    TaxId: productTaxId,
                    TaxName: productTaxName,
                    TaxPercentage: productTaxPerc,
                    TaxAmount: productTaxAmount,
                    TotalPrice: totalPrice,
                    SurchargeName_1: productSurchargeName_1,
                    SurchargePercentage_1: productSurchargePercentage_1,
                    SurchargeAmount_1: productSurchargeAmount_1,
                    SurchargeName_2: productSurchargeName_2,
                    SurchargePercentage_2: productSurchargePercentage_2,
                    SurchargeAmount_2: productSurchargeAmount_2,
                    SurchargeName_3: productSurchargeName_3,
                    SurchargePercentage_3: productSurchargePercentage_3,
                    SurchargeAmount_3: productSurchargeAmount_3
                };
                soProductList.push(soProduct);
                productEntrySequence = parseInt(productEntrySequence) + 1;
            }
            else if (hdnSequenceNo.val() == sequenceNo) {

                var soProduct = {
                    SequenceNo: hdnSequenceNo.val(),
                    SOProductDetailId: hdnSOProductDetailId.val(),  
                    ProductId: hdnProductId.val(),
                    ProductName: txtProductName.val().trim(),
                    ProductCode: txtProductCode.val().trim(),
                    ProductShortDesc: txtProductShortDesc.val().trim(),
                    UOMName: txtUOMName.val().trim(),
                    Price: txtPrice.val().trim(),
                    Quantity: txtQuantity.val().trim(),
                    DiscountPercentage: txtDiscountPerc.val().trim(),
                    DiscountAmount: txtDiscountAmount.val().trim(),
                    TaxId: hdnProductTaxId.val().trim(),
                    TaxName: txtProductTaxName.val().trim(),
                    TaxPercentage: hdnProductTaxPerc.val().trim(),
                    TaxAmount: txtProductTaxAmount.val().trim(),
                    TotalPrice: txtTotalPrice.val().trim(),
                    SurchargePercentage_1: hdnProductSurchargePercentage_1.val(),
                    SurchargeAmount_1: txtProductSurchargeAmount_1.val(),
                    SurchargeName_2: hdnProductSurchargeName_2.val(),
                    SurchargePercentage_2: hdnProductSurchargePercentage_2.val(),
                    SurchargeAmount_2: txtProductSurchargeAmount_2.val(),
                    SurchargeName_3: hdnProductSurchargeName_3.val(),
                    SurchargePercentage_3: hdnProductSurchargePercentage_3.val(),
                    SurchargeAmount_3: txtProductSurchargeAmount_3.val()
                };

                soProductList.push(soProduct);

            }
        }

    });

    if (action == 1 && (hdnSequenceNo.val() == "" || hdnSequenceNo.val() == "0")) {
        hdnSequenceNo.val(productEntrySequence);
    }
    if (action == 1) {
        var soProductAddEdit = {
            SequenceNo: hdnSequenceNo.val(),
            SOProductDetailId: hdnSOProductDetailId.val(),
            ProductId: hdnProductId.val(),
            ProductName: txtProductName.val().trim(),
            ProductCode: txtProductCode.val().trim(),
            ProductShortDesc: txtProductShortDesc.val().trim(),
            UOMName: txtUOMName.val().trim(),
            Price: txtPrice.val().trim(),
            Quantity: txtQuantity.val().trim(),
            DiscountPercentage: txtDiscountPerc.val().trim(),
            DiscountAmount: txtDiscountAmount.val().trim(),
            TaxId: hdnProductTaxId.val().trim(),
            TaxName: txtProductTaxName.val().trim(),
            TaxPercentage: hdnProductTaxPerc.val().trim(),
            TaxAmount: txtProductTaxAmount.val().trim(),
            TotalPrice: txtTotalPrice.val().trim(),
            SurchargeName_1: hdnProductSurchargeName_1.val(),
            SurchargePercentage_1: hdnProductSurchargePercentage_1.val(),
            SurchargeAmount_1: txtProductSurchargeAmount_1.val(),
            SurchargeName_2: hdnProductSurchargeName_2.val(),
            SurchargePercentage_2: hdnProductSurchargePercentage_2.val(),
            SurchargeAmount_2: txtProductSurchargeAmount_2.val(),
            SurchargeName_3: hdnProductSurchargeName_3.val(),
            SurchargePercentage_3: hdnProductSurchargePercentage_3.val(),
            SurchargeAmount_3: txtProductSurchargeAmount_3.val()

        };
        soProductList.push(soProductAddEdit);
        hdnSequenceNo.val("0");
    }
    if (flag == true) {
        GetSOProductList(soProductList);
    }

}
function GetSOProductList(soProducts) {
    var hdnSOId = $("#hdnSOId");
    var requestData = { soProducts: soProducts, soId: hdnSOId.val() };
    $.ajax({
        url: "../SO/GetSOProductList",
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
    var discountPerc = $("#txtDiscountPerc").val();
    var productTaxPerc = $("#hdnProductTaxPerc").val();
    var productSurchargePerc_1 = $("#hdnProductSurchargePercentage_1").val();
    var productSurchargePerc_2 = $("#hdnProductSurchargePercentage_2").val();
    var productSurchargePerc_3 = $("#hdnProductSurchargePercentage_3").val();
    var discountAmount = 0;
    var taxAmount = 0;
    var productSurchargeAmount_1 = 0;
    var productSurchargeAmount_2 = 0;
    var productSurchargeAmount_3 = 0;
    var totalTaxAmount = 0;
    price = price == "" ? 0 : price;
    quantity = quantity == "" ? 0 : quantity;
    var totalPrice = parseFloat(price) * parseFloat(quantity);
    if (parseFloat(discountPerc) > 0) {
        discountAmount = (parseFloat(totalPrice) * parseFloat(discountPerc)) / 100
    }
    $("#txtDiscountAmount").val(discountAmount.toFixed(2));
    if (parseFloat(productTaxPerc) > 0) {
        taxAmount = ((parseFloat(totalPrice) - parseFloat(discountAmount)) * parseFloat(productTaxPerc)) / 100;
    }
    $("#txtProductTaxAmount").val(taxAmount.toFixed(2));

    if (parseFloat(taxAmount) > 0 && parseFloat(productSurchargePerc_1) > 0) {
        productSurchargeAmount_1 = (parseFloat(taxAmount) * parseFloat(productSurchargePerc_1)) / 100;
    }
    $("#txtProductSurchargeAmount_1").val(productSurchargeAmount_1.toFixed(2));

    if (parseFloat(taxAmount) > 0 && parseFloat(productSurchargePerc_2) > 0) {
        productSurchargeAmount_2 = (parseFloat(taxAmount) * parseFloat(productSurchargePerc_2)) / 100;
    }
    $("#txtProductSurchargeAmount_2").val(productSurchargeAmount_2.toFixed(2));

    if (parseFloat(taxAmount) > 0 && parseFloat(productSurchargePerc_3) > 0) {
        productSurchargeAmount_3 = (parseFloat(taxAmount) * parseFloat(productSurchargePerc_3)) / 100;
    }
    $("#txtProductSurchargeAmount_3").val(productSurchargeAmount_3.toFixed(2));
    totalTaxAmount = parseFloat(taxAmount) + parseFloat(productSurchargeAmount_1) + parseFloat(productSurchargeAmount_2) + parseFloat(productSurchargeAmount_3);
    $("#txtProductTotalTaxAmount").val(totalTaxAmount.toFixed(2));

    $("#txtTotalPrice").val((totalPrice - discountAmount + taxAmount + productSurchargeAmount_1 + productSurchargeAmount_2 + productSurchargeAmount_3).toFixed(2));


}
function CalculateGrossandNetValues() {
    var basicValue = 0;
    var taxValue = 0;
    $('#tblProductList tr').each(function (i, row) {
        var $row = $(row);
        var soProductDetailId = $row.find("#hdnSOProductDetailId").val();
        var totalPrice = $row.find("#hdnTotalPrice").val();
        if (totalPrice != undefined) {
            basicValue += parseFloat(totalPrice);
        }

    });
    $('#tblTaxList tr').each(function (i, row) {
        var $row = $(row);
        var soTaxDetailId = $row.find("#hdnSOTaxDetailId").val();
        var taxPercentage = $row.find("#hdnTaxPercentage").val();
        var surchargePercentage_1 = $row.find("#hdnSurchargePercentage_1").val();
        var surchargePercentage_2 = $row.find("#hdnSurchargePercentage_2").val();
        var surchargePercentage_3 = $row.find("#hdnSurchargePercentage_3").val();

        var taxAmount = 0;
        var surchargeAmount_1 = 0;
        var surchargeAmount_2 = 0;
        var surchargeAmount_3 = 0;
        var totalTaxAmount = 0;

        if (taxPercentage != undefined) {

            if (parseFloat(basicValue) > 0) {
                taxAmount = (parseFloat(basicValue) * (parseFloat(taxPercentage) / 100));
                if (parseFloat(taxAmount) > 0 && parseFloat(surchargePercentage_1) > 0) {
                    surchargeAmount_1 = (parseFloat(taxAmount) * (parseFloat(surchargePercentage_1) / 100));
                }
                if (parseFloat(taxAmount) > 0 && parseFloat(surchargePercentage_2) > 0) {
                    surchargeAmount_2 = (parseFloat(taxAmount) * (parseFloat(surchargePercentage_2) / 100));
                }
                if (parseFloat(taxAmount) > 0 && parseFloat(surchargePercentage_3) > 0) {
                    surchargeAmount_3 = (parseFloat(taxAmount) * (parseFloat(surchargePercentage_3) / 100));
                }
                totalTaxAmount = parseFloat(taxAmount) + parseFloat(surchargeAmount_1) + parseFloat(surchargeAmount_2) + parseFloat(surchargeAmount_3);
                $row.find("#hdnTaxAmount").val(taxAmount.toFixed(2));
                $row.find("#hdnSurchargeAmount_1").val(surchargeAmount_1.toFixed(2));
                $row.find("#hdnSurchargeAmount_2").val(surchargeAmount_2.toFixed(2));
                $row.find("#hdnSurchargeAmount_3").val(surchargeAmount_3.toFixed(2));

                $row.find("#tdTaxAmount").html(totalTaxAmount.toFixed(2));
            }
            else {
                taxAmount = 0;
                $row.find("#hdnTaxAmount").val("0");
                $row.find("#hdnSurchargeAmount_1").val("0");
                $row.find("#hdnSurchargeAmount_2").val("0");
                $row.find("#hdnSurchargeAmount_3").val("0");

                $row.find("#tdTaxAmount").html("0");
            }
            taxValue += parseFloat(totalTaxAmount);
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

    $("#txtBasicValue").val(basicValue);
    $("#txtTotalValue").val(parseFloat(parseFloat(basicValue) + parseFloat(taxValue) + parseFloat(freightValue) + parseFloat(loadingValue)).toFixed(0));
}

function EditProductRow(obj) {

    var row = $(obj).closest("tr");
    var sequenceNo = $(row).find("#hdnSequenceNo").val();
    var soProductDetailId = $(row).find("#hdnSOProductDetailId").val();
    var productId = $(row).find("#hdnProductId").val();
    var productName = $(row).find("#hdnProductName").val();
    var productCode = $(row).find("#hdnProductCode").val();
    var productShortDesc = $(row).find("#hdnProductShortDesc").val();
    var uomName = $(row).find("#hdnUOMName").val();
    var price = $(row).find("#hdnPrice").val();
    var quantity = $(row).find("#hdnQuantity").val();

    var discountPerc = $(row).find("#hdnDiscountPerc").val();
    var discountAmount = $(row).find("#hdnDiscountAmount").val();
    var productTaxId = $(row).find("#hdnProductTaxId").val();
    var productTaxPerc = $(row).find("#hdnProductTaxPerc").val();
    var productTaxAmount = $(row).find("#hdnProductTaxAmount").val();
    var productTaxName = $(row).find("#hdnProductTaxName").val();

    var productSurchargeName_1 = $(row).find("#hdnProductSurchargeName_1").val();
    var productSurchargePercentage_1 = $(row).find("#hdnProductSurchargePercentage_1").val();
    var productSurchargeAmount_1 = $(row).find("#hdnProductSurchargeAmount_1").val();
    var productSurchargeName_2 = $(row).find("#hdnProductSurchargeName_2").val();
    var productSurchargePercentage_2 = $(row).find("#hdnProductSurchargePercentage_2").val();
    var productSurchargeAmount_2 = $(row).find("#hdnProductSurchargeAmount_2").val();
    var productSurchargeName_3 = $(row).find("#hdnProductSurchargeName_3").val();
    var productSurchargePercentage_3 = $(row).find("#hdnProductSurchargePercentage_3").val();
    var productSurchargeAmount_3 = $(row).find("#hdnProductSurchargeAmount_3").val();

    var totalPrice = $(row).find("#hdnTotalPrice").val();

    $("#txtProductName").val(productName);
    $("#hdnSOProductDetailId").val(soProductDetailId);
    $("#hdnSequenceNo").val(sequenceNo);
    $("#hdnProductId").val(productId);
    $("#txtProductCode").val(productCode);
    $("#txtProductShortDesc").val(productShortDesc);
    $("#txtUOMName").val(uomName);
    $("#txtPrice").val(price);
    $("#txtQuantity").val(quantity);
    $("#txtDiscountPerc").val(discountPerc);
    $("#txtDiscountAmount").val(discountAmount);
    $("#txtProductTaxName").val(productTaxName);
    $("#hdnProductTaxId").val(productTaxId);
    $("#hdnProductTaxPerc").val(productTaxPerc);
    $("#txtProductTaxAmount").val(productTaxAmount);
    $("#hdnProductSurchargeName_1").val(productSurchargeName_1);
    $("#hdnProductSurchargePercentage_1").val(productSurchargePercentage_1);
    $("#txtProductSurchargeAmount_1").val(productSurchargeAmount_1);

    $("#hdnProductSurchargeName_2").val(productSurchargeName_2);
    $("#hdnProductSurchargePercentage_2").val(productSurchargePercentage_2);
    $("#txtProductSurchargeAmount_2").val(productSurchargeAmount_2);

    $("#hdnProductSurchargeName_3").val(productSurchargeName_3);
    $("#hdnProductSurchargePercentage_3").val(productSurchargePercentage_3);
    $("#txtProductSurchargeAmount_3").val(productSurchargeAmount_3);

    $("#txtProductTotalTaxAmount").val(parseFloat(productTaxAmount) + parseFloat(productSurchargeAmount_1) + parseFloat(productSurchargeAmount_2) + parseFloat(productSurchargeAmount_3));

    $("#txtTotalPrice").val(totalPrice);
    $("#btnAddProduct").hide();
    $("#btnUpdateProduct").show();
    GetProductAvailableStock(productId);
    ShowHideProductPanel(1);
}

function RemoveProductRow(obj) {
    if (confirm("Do you want to remove selected Product?")) {
        var row = $(obj).closest("tr");
        var soProductDetailId = $(row).find("#hdnSOProductDetailId").val();
        ShowModel("Alert", "Product Removed from List.");
        row.remove();
        CalculateGrossandNetValues();
    }
}

function GetSODetail(soId) {
    $.ajax({
        type: "GET",
        asnc: false,
        url: "../SO/GetSODetail",
        data: { soId: soId },
        dataType: "json",
        success: function (data) {
            $("#txtSONo").val(data.SONo);
            $("#txtSODate").val(data.SODate);
            $("#txtQuotationNo").val(data.QuotationNo);
            $("#hdnQuotationId").val(data.QuotationId);
            $("#txtQuotationDate").val(data.QuotationDate);
            
            $("#hdnCustomerId").val(data.CustomerId);
            $("#txtCustomerCode").val(data.CustomerCode);
            $("#txtCustomerName").val(data.CustomerName);
            $("#ddlApprovalStatus").val(data.ApprovalStatus)

            $("#txtBContactPerson").val(data.ContactPerson);
            $("#txtBAddress").val(data.BillingAddress);
            $("#txtBCity").val(data.City);
            $("#ddlBCountry").val(data.CountryId);
            $("#ddlBState").val(data.StateId);
            $("#txtBPinCode").val(data.PinCode);
            $("#txtBTINNo").val(data.TINNo);

            $("#txtBEmail").val(data.Email);
            $("#txtBMobileNo").val(data.MobileNo);
            $("#txtBContactNo").val(data.ContactNo);
            $("#txtBFax").val(data.Fax);

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


            $("#txtRefNo").val(data.RefNo);
            $("#txtRefDate").val(data.RefDate);

            $("#txtPayToBookName").val(data.PayToBookName);
            $("#hdnPayToBookId").val(data.PayToBookId);
            $("#txtPayToBookBranch").val(data.PayToBookBranch);

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

            $("#txtBasicValue").val(data.BasicValue);
            $("#txtTotalValue").val(data.TotalValue);
            $("#txtFreightValue").val(data.FreightValue);
            $("#txtLoadingValue").val(data.LoadingValue);

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
    var txtSONo = $("#txtSONo");
    var hdnSOId = $("#hdnSOId");
    var txtSODate = $("#txtSODate");
    var hdnQuotationId = $("#hdnQuotationId");
    var txtQuotationNo = $("#txtQuotationNo");

    var hdnCustomerId = $("#hdnCustomerId");
    var txtCustomerName = $("#txtCustomerName");
    var txtBContactPerson = $("#txtBContactPerson");
    var txtBAddress = $("#txtBAddress");
    var txtBCity = $("#txtBCity");
    var ddlBCountry = $("#ddlBCountry");
    var ddlBState = $("#ddlBState");
    var txtBPinCode = $("#txtBPinCode");
    var ddlApprovalStatus = $("#ddlApprovalStatus");
    var txtBTINNo = $("#txtBTINNo");
    var txtBEmail = $("#txtBEmail");
    var txtBMobileNo = $("#txtBMobileNo");
    var txtBContactNo = $("#txtBContactNo");
    var txtBFax = $("#txtBFax");

    var txtSContactPerson = $("#txtSContactPerson");
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

    


    var txtRefNo = $("#txtRefNo");
    var txtRefDate = $("#txtRefDate");
    var txtBasicValue = $("#txtBasicValue");
    var txtFreightValue = $("#txtFreightValue");
    var txtLoadingValue = $("#txtLoadingValue");
    var txtTotalValue = $("#txtTotalValue");
    var hdnPayToBookId = $("#hdnPayToBookId");
    var txtRemarks1 = $("#txtRemarks1");
    var txtRemarks2 = $("#txtRemarks2");

    

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
    if (txtBAddress.val().trim() == "") {
        ShowModel("Alert", "Please Enter Party billing Address")
        txtBAddress.focus();
        return false;
    }
    if (txtBCity.val().trim() == "") {
        ShowModel("Alert", "Please enter billing city")
        txtBCity.focus();
        return false;
    }
    if (txtSAddress.val().trim() == "") {
        ShowModel("Alert", "Please Enter Party shipping Address")
        txtSAddress.focus();
        return false;
    }
    if (txtSCity.val().trim() == "") {
        ShowModel("Alert", "Please enter shipping city")
        txtSCity.focus();
        return false;
    }
    if (hdnPayToBookId.val() == "" || hdnPayToBookId.val() == "0") {
        ShowModel("Alert", "Please select Bank Name from list")
        return false;
    }

    if (txtBasicValue.val() == "" || parseFloat(txtBasicValue.val()) <= 0) {
        ShowModel("Alert", "Please select at least one Product")
        return false;
    }

    var soViewModel = {
        SOId: hdnSOId.val(),
        SONo: txtSONo.val().trim(),
        SODate: txtSODate.val().trim(),
        QuotationId: hdnQuotationId.val().trim(),
        QuotationNo: txtQuotationNo.val().trim(),
        CustomerId: hdnCustomerId.val().trim(),
        CustomerName: txtCustomerName.val().trim(),
        ContactPerson:txtBContactPerson.val().trim(),
        BillingAddress: txtBAddress.val().trim(),
        City: txtBCity.val().trim(),
        StateId: ddlBState.val(),
        CountryId: ddlBCountry.val(),
        PinCode: txtBPinCode.val().trim(),
        TINNo: txtBTINNo.val().trim(),
        Email: txtBEmail.val().trim(),
        MobileNo: txtBMobileNo.val().trim(),
        ContactNo: txtBContactNo.val().trim(),
        Fax: txtBFax.val().trim(),
        ApprovalStatus: ddlApprovalStatus.val(),
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
        RefNo: txtRefNo.val().trim(),
        RefDate: txtRefDate.val(),
        BasicValue: txtBasicValue.val(),
        FreightValue: txtFreightValue.val(),
        LoadingValue:txtLoadingValue.val(),
        TotalValue: txtTotalValue.val(),
        PayToBookId: hdnPayToBookId.val(),
        Remarks1: txtRemarks1.val(),
        Remarks2: txtRemarks2.val()
    };

    var soProductList = [];
    $('#tblProductList tr').each(function (i, row) {
        var $row = $(row);
        var soProductDetailId = $row.find("#hdnSOProductDetailId").val();
        var productId = $row.find("#hdnProductId").val();
        var productName = $row.find("#hdnProductName").val();
        var productCode = $row.find("#hdnProductCode").val();
        var productShortDesc = $row.find("#hdnProductShortDesc").val();
        var uomName = $row.find("#hdnUOMName").val();
        var price = $row.find("#hdnPrice").val();
        var quantity = $row.find("#hdnQuantity").val();
        var discountPerc = $row.find("#hdnDiscountPerc").val();
        var discountAmount = $row.find("#hdnDiscountAmount").val();
        var productTaxId = $row.find("#hdnProductTaxId").val();
        var productTaxPerc = $row.find("#hdnProductTaxPerc").val();
        var productTaxAmount = $row.find("#hdnProductTaxAmount").val();
        var productTaxName = $row.find("#hdnProductTaxName").val();

        var productSurchargeName_1 = $row.find("#hdnProductSurchargeName_1").val();
        var productSurchargePercentage_1 = $row.find("#hdnProductSurchargePercentage_1").val();
        var productSurchargeAmount_1 = $row.find("#hdnProductSurchargeAmount_1").val();

        var productSurchargeName_2 = $row.find("#hdnProductSurchargeName_2").val();
        var productSurchargePercentage_2 = $row.find("#hdnProductSurchargePercentage_2").val();
        var productSurchargeAmount_2 = $row.find("#hdnProductSurchargeAmount_2").val();

        var productSurchargeName_3 = $row.find("#hdnProductSurchargeName_3").val();
        var productSurchargePercentage_3 = $row.find("#hdnProductSurchargePercentage_3").val();
        var productSurchargeAmount_3 = $row.find("#hdnProductSurchargeAmount_3").val();


        var totalPrice = $row.find("#hdnTotalPrice").val();

        if (productName != undefined) {

            var soProduct = {
                SOProductDetailId: soProductDetailId,
                ProductId: productId,
                ProductName: productName,
                ProductCode: productCode,
                ProductShortDesc: productShortDesc,
                UOMName: uomName,
                Price: price,
                Quantity: quantity,
                DiscountPercentage: discountPerc,
                DiscountAmount: discountAmount,
                TaxId: productTaxId,
                TaxName: productTaxName,
                TaxPercentage: productTaxPerc,
                TaxAmount: productTaxAmount,
                TotalPrice: totalPrice,
                SurchargeName_1: productSurchargeName_1,
                SurchargePercentage_1: productSurchargePercentage_1,
                SurchargeAmount_1: productSurchargeAmount_1,
                SurchargeName_2: productSurchargeName_2,
                SurchargePercentage_2: productSurchargePercentage_2,
                SurchargeAmount_2: productSurchargeAmount_2,
                SurchargeName_3: productSurchargeName_3,
                SurchargePercentage_3: productSurchargePercentage_3,
                SurchargeAmount_3: productSurchargeAmount_3
            };
            soProductList.push(soProduct);
        }
    });


    var soTaxList = [];
    $('#tblTaxList tr').each(function (i, row) {
        var $row = $(row);
        var soTaxDetailId = $row.find("#hdnSOTaxDetailId").val();
        var taxId = $row.find("#hdnTaxId").val();
        var taxName = $row.find("#hdnTaxName").val();
        var taxPercentage = $row.find("#hdnTaxPercentage").val();
        var taxAmount = $row.find("#hdnTaxAmount").val();

        var surchargeName_1 = $row.find("#hdnSurchargeName_1").val();
        var surchargePercentage_1 = $row.find("#hdnSurchargePercentage_1").val();
        var surchargeAmount_1 = $row.find("#hdnSurchargeAmount_1").val();

        var surchargeName_2 = $row.find("#hdnSurchargeName_2").val();
        var surchargePercentage_2 = $row.find("#hdnSurchargePercentage_2").val();
        var surchargeAmount_2 = $row.find("#hdnSurchargeAmount_2").val();

        var surchargeName_3 = $row.find("#hdnSurchargeName_3").val();
        var surchargePercentage_3 = $row.find("#hdnSurchargePercentage_3").val();
        var surchargeAmount_3 = $row.find("#hdnSurchargeAmount_3").val();

        if (taxName != undefined) {
            var soTax = {
                SOTaxDetailId: soTaxDetailId,
                TaxId: taxId,
                TaxName: taxName,
                TaxPercentage: taxPercentage,
                TaxAmount: taxAmount,
                SurchargeName_1: surchargeName_1,
                SurchargePercentage_1: surchargePercentage_1,
                SurchargeAmount_1: surchargeAmount_1,
                SurchargeName_2: surchargeName_2,
                SurchargePercentage_2: surchargePercentage_2,
                SurchargeAmount_2: surchargeAmount_2,
                SurchargeName_3: surchargeName_3,
                SurchargePercentage_3: surchargePercentage_3,
                SurchargeAmount_3: surchargeAmount_3
            };
            soTaxList.push(soTax);
        }

    });

    var soTermList = [];
    $('#tblTermList tr').each(function (i, row) {
        var $row = $(row);
        var soTermDetailId = $row.find("#hdnSOTermDetailId").val();
        var termDesc = $row.find("#hdnTermDesc").val();
        var termSequence = $row.find("#hdnTermSequence").val();

        if (termDesc != undefined) {
            var soTerm = {
                SOTermDetailId: soTermDetailId,
                TermDesc: termDesc,
                TermSequence: termSequence
            };
            soTermList.push(soTerm);
        }

    });



    var requestData = { soViewModel: soViewModel, soProducts: soProductList, soTaxes: soTaxList, soTerms: soTermList };
    $.ajax({
        url: "../SO/AddEditSO",
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
                       window.location.href = "../SO/AddEditSO?SOId=" + data.trnId + "&AccessMode=2";
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

    $("#txtSONo").val("");
    $("#hdnSOId").val("0");
    $("#txtSODate").val($("#hdnCurrentDate").val());
    $("#hdnCustomerId").val("0");
    $("#txtCustomerName").val("");
    $("#txtCustomerCode").val("");
    $("#txtQuotationNo").val("");
    $("#txtQuotationDate").val("");
    $("#txtQuotationId").val("0");

    $("#ddlBCustomerBranch").val("0");
    $("#txtBContactPerson").val("");
    $("#txtBAddress").val("");
    $("#txtBCity").val("");
    $("#ddlBCountry").val("0");
    $("#ddlBState").val("0");
    $("#txtBPinCode").val("");
    $("#txtBTINNo").val("");
    $("#txtBEmail").val("");
    $("#txtBMobileNo").val("");
    $("#txtBEmail").val("");
    $("#txtBFax").val("");
    $("#ddlApprovalStatus").val("Final");
    $("#ddlSCustomerBranch").val("0");
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


    $("#txtRefNo").val("");
    $("#txtRefDate").val("");
    $("#hdnPayToBookId").val("0");
    $("#txtPayToBookName").val("");
    $("#txtPayToBookBranch").val("");
    $("#txtBasicValue").val("");
    $("#txtFreightValue").val("");
    $("#txtLoadingValue").val("");
    $("#txtTotalValue").val("");
    $("#txtRemarks1").val("");
    $("#txtRemarks2").val("");

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
            $("#txtBContactPerson").val(data.ContactPersonName);
            $("#txtBAddress").val(data.PrimaryAddress);
            $("#txtBCity").val(data.City);
            $("#ddlBCountry").val(data.CountryId);
            $("#ddlBState").val(data.StateId);
            $("#txtBPinCode").val(data.PinCode);
            $("#txtBTINNo").val(data.TINNo);
            $("#txtBEmail").val(data.Email);
            $("#txtBMobileNo").val(data.MobileNo);
            $("#txtBContactNo").val(data.ContactNo);
            $("#txtBFax").val(data.Fax);

        },
        error: function (Result) {
            ShowModel("Alert", "Problem in Request");
        }
    });
    BindCustomerBranchList(customerId)
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
function GetProductAvailableStock(productId) {
    $.ajax({
        type: "GET",
        asnc: false,
        url: "../Product/GetProductAvailableStock",
        data: { productid: productId, companyBranchId: 0, trnId: 0, trnType: "SO" },
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
        $("#hdnSOProductDetailId").val("0");
        $("#txtProductCode").val("");
        $("#txtProductShortDesc").val("");
        $("#txtPrice").val("");
        $("#txtAvailableStock").val("");
        $("#txtUOMName").val("");
        $("#txtQuantity").val("");
        $("#txtDiscountPerc").val("");
        $("#txtDiscountAmount").val("");
        $("#txtProductTaxName").val("");
        $("#hdnProductTaxId").val("");
        $("#hdnProductTaxPerc").val("");
        $("#txtProductTaxAmount").val("");
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
        $("#hdnSOTaxDetailId").val("0");
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
        $("#hdnSOTermDetailId").val("0");
        $("#hdnTermSequence").val("0");
        $("#btnAddTerm").show();
        $("#btnUpdateTerm").hide();
    }
}
function OpenQuotationSearchPopup() {
    $("#SearchQuotationModel").modal();

}
function SearchQuotation() {
    var txtQuotationNo = $("#txtSearchQuotationNo");
    var txtCustomerName = $("#txtSearchCustomerName");

    var txtRefNo = $("#txtSearchRefNo");
    var txtFromDate = $("#txtSearchFromDate");
    var txtToDate = $("#txtSearchToDate");

    var requestData = { quotationNo: txtQuotationNo.val().trim(), customerName: txtCustomerName.val().trim(), refNo: txtRefNo.val().trim(), fromDate: txtFromDate.val(), toDate: txtToDate.val(), displayType:"Popup", approvalStatus: "Final" };
    $.ajax({
        url: "../SO/GetQuotationList",
        data: requestData,
        dataType: "html",
        type: "GET",
        error: function (err) {
            $("#divQuotationList").html("");
            $("#divQuotationList").html(err);
        },
        success: function (data) {
            $("#divQuotationList").html("");
            $("#divQuotationList").html(data);
        }
    });
}
function SelectQuotation(quotationId, quotationNo, quotationDate, customerId, customerCode, customerName) {
    GetCustomerDetail(customerId);
    GetQuotationDetail(quotationId);
    
    $("#SearchQuotationModel").modal('hide');
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
            $("#hdnQuotationId").val(quotationId);
            $("#txtQuotationDate").val(data.QuotationDate);
            $("#hdnCustomerId").val(data.CustomerId);
            $("#txtCustomerCode").val(data.CustomerCode);
            $("#txtCustomerName").val(data.CustomerName);
            $("#txtAddress").val(data.BillingAddress);
            $("#ddlApprovalStatus").val(data.ApprovalStatus);
            $("#txtPinCode").val(data.PinCode);
            $("#txtCSTNo").val(data.CSTNo);
            $("#txtTINNo").val(data.TINNo);
            $("#txtPANNo").val(data.PANNo);
            $("#txtRefNo").val(data.RefNo);
            $("#txtRefDate").val(data.RefDate);
            $("#txtBasicValue").val(data.BasicValue);
            $("#txtTotalValue").val(data.TotalValue);
            $("#txtFreightValue").val(data.FreightValue);
            $("#txtLoadingValue").val(data.LoadingValue);
            $("#txtRemarks1").val(data.Remarks1);
            $("#txtRemarks2").val(data.Remarks2);

            if (data.ModifiedByUserName != "") {
                $("#divModified").show();
                $("#txtModifiedBy").val(data.ModifiedByUserName);
                $("#txtModifiedDate").val(data.ModifiedDate);
            }

        },
        error: function (Result) {
            ShowModel("Alert", "Problem in Request");
        }
    });

    var quotationProducts = [];
    GetQuotationProductList(quotationProducts, quotationId);
    var quotationTaxes = [];
    GetQuotationTaxList(quotationTaxes, quotationId);
    var quotationTerms = [];
    GetQuotationTermList(quotationTerms, quotationId);
    
}
function GetQuotationProductList(quotationProducts, quotationId) {
    var hdnQuotationId = $("#hdnQuotationId");
    var requestData = { quotationProducts: quotationProducts, quotationId: quotationId };
    $.ajax({
        url: "../Quotation/GetQuotationSOProductList",
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
function GetQuotationTaxList(quotationTaxes, quotationId) {

    var requestData = { quotationTaxes: quotationTaxes, quotationId: quotationId };
    $.ajax({
        url: "../Quotation/GetQuotationSOTaxList",
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
function GetQuotationTermList(quotationTerms, quotationId) {

    var requestData = { quotationTerms: quotationTerms, quotationId: quotationId };
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

function CopyCurrentAddress() {
    if ($("#chkSamePermanentAddress").is(':checked')) {
        if ($("#txtBContactPerson").val().trim() != "") {
            $("#txtSContactPerson").val($("#txtBContactPerson").val().trim());
        }
        if ($("#txtBAddress").val().trim() != "") {
            $("#txtSAddress").val($("#txtBAddress").val().trim());
        }
        if ($("#txtBCity").val().trim() != "") {
            $("#txtSCity").val($("#txtBCity").val().trim());
        }
        if ($("#ddlBCountry").val() != "" && $("#ddlBCountry").val() != "0") {
            $("#ddlSCountry").val($("#ddlBCountry").val());
        }
        if ($("#ddlBState").val() != "" && $("#ddlBState").val() != "0") {
            $("#ddlSState").val($("#ddlBState").val());
        }
        if ($("#txtBPinCode").val().trim() != "") {
            $("#txtSPinCode").val($("#txtBPinCode").val().trim());
        }
        if ($("#txtBTINNo").val().trim() != "") {
            $("#txtSTINNo").val($("#txtBTINNo").val().trim());
        }
        if ($("#txtBEmail").val().trim() != "") {
            $("#txtSEmail").val($("#txtSEmail").val().trim());
        }
        if ($("#txtBMobileNo").val().trim() != "") {
            $("#txtSMobileNo").val($("#txtBMobileNo").val().trim());
        }
        if ($("#txtBContactNo").val().trim() != "") {
            $("#txtSContactNo").val($("#txtBContactNo").val().trim());
        }
        if ($("#txtBFax").val().trim() != "") {
            $("#txtSFax").val($("#txtBFax").val().trim());
        }
    }
    else {
        $("#txtSContactPerson").val("");
        $("#txtSAddress").val("");
        $("#txtSCity").val("");
        $("#ddlSCountry").val("0");
        $("#ddlSState").val("0");
        $("#txtSPinCode").val("");
        $("#txtSTINNo").val("");
        $("#txtSEmail").val("");
        $("#txtSMobileNo").val("");
        $("#txtSContactNo").val("");
        $("#txtSFax").val("");
    }

}
function FillShippingAddress() {

    var customerBranchId = $("#ddlSCustomerBranch option:selected").val();
    $.ajax({
        type: "GET",
        asnc: false,
        url: "../SO/GetCustomerBranchDetail",
        data: {
            customerBranchId: customerBranchId
        },
        dataType: "json",
        success: function (data) {

            $("#txtSContactPerson").val(data.ContactPersonName);
            $("#txtSAddress").val(data.PrimaryAddress);
            $("#txtSCity").val(data.City);
            $("#ddlSCountry").val(data.CountryId);
            $("#ddlSState").val(data.StateId);
            $("#txtSPinCode").val(data.PinCode);
            $("#txtSTINNo").val(data.TINNo);
            $("#txtSEmail").val(data.Email);
            $("#txtSMobileNo").val(data.MobileNo);
            $("#txtSContactNo").val(data.ContactNo);
            $("#txtSFax").val(data.Fax);
        },
        error: function (Result) {
            ShowModel("Alert", "Problem in Request");
        }
    });
}
function FillBillingAddress() {

    var customerBranchId = $("#ddlBCustomerBranch option:selected").val();
    $.ajax({
        type: "GET",
        asnc: false,
        url: "../SO/GetCustomerBranchDetail",
        data: {
            customerBranchId: customerBranchId
        },
        dataType: "json",
        success: function (data) {

            $("#txtBContactPerson").val(data.ContactPersonName);
            $("#txtBAddress").val(data.PrimaryAddress);
            $("#txtBCity").val(data.City);
            $("#ddlBCountry").val(data.CountryId);
            $("#ddlBState").val(data.StateId);
            $("#txtBPinCode").val(data.PinCode);
            $("#txtBTINNo").val(data.TINNo);
            $("#txtBEmail").val(data.Email);
            $("#txtBMobileNo").val(data.MobileNo);
            $("#txtBContactNo").val(data.ContactNo);
            $("#txtBFax").val(data.Fax);
        },
        error: function (Result) {
            ShowModel("Alert", "Problem in Request");
        }
    });
}

function SendMail() {
    var hdnSOId = $("#hdnSOId");
    var requestData = { soId: hdnSOId.val(), reportType: "PDF" };
    $.ajax({
        url: "../SO/SOMail",
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
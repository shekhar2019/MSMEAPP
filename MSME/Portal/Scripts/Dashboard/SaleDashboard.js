﻿$(document).ready(function () {
    var hdnCurrentFinyearId = $("#hdnCurrentFinyearId");
    BindFinYearList(hdnCurrentFinyearId.val());
    $("#ddlFinYear").val(hdnCurrentFinyearId);
});

function BindFinYearList(selectedFinYear) {
    $.ajax({
        type: "GET",
        url: "../Product/GetFinYearList",
        data: "{}",
        dataType: "json",
        asnc: false,
        success: function (data) {
            $.each(data, function (i, item) {
                $("#ddlFinYear").append($("<option></option>").val(item.FinYearId).html(item.FinYearDesc));
            });
            $("#ddlFinYear").val(selectedFinYear);
        },
        error: function (Result) {
            
        }
    });
}

function LoadAllSellerProducts() {
    BindSellerProductDetails("../SellerProduct/GetAllSellerProducts", null)
}


function LoadFilteredSellerProducts() {
    $(".list_product").empty();
    var productMainGroupId = $("#ddlProductMainGroup option:selected").val();
    var productSubGroupId = $("#ddlProductSubGroup option:selected").val();
    var productChildSubGroupId = $("#ddlProductSubChildGroup option:selected").val();
    if (productChildSubGroupId == undefined || productChildSubGroupId == "0") {
        return;
    }

    if (productMainGroupId != undefined && productSubGroupId != undefined && productMainGroupId != "0" && productSubGroupId != "0") {
        var data = { productMainGroupId: productMainGroupId, productSubGroupId: productSubGroupId, productChildSubGroupId: productChildSubGroupId };
        BindSellerProductDetails("../SellerProduct/GetSellerProducts", data);
        //Disable UOM list if already exixts
        Check_T_UOMList("../Product/GetUOMId", data);
    }
    else {
        ShowModel("Alert", "Please select appropriate options");
    }
    BindSpecNames();
    BindBrandList();
    BindItemList();
}




function BindSellerProductDetails(url, data) {

    $.ajax({
        type: "GET",
        url: url,
        data: data,
        asnc: false,
        dataType: "html",
        success: function (html) {
            $(".list_product").html(html);
        },
        error: function (Result) {
            ShowModel("Alert", "Problem in Request");
        }
    });

}

function SetFinancialYearSession()
{
    var finYearId = $("#ddlFinYear option:selected").val();
    var data = { finYearId: finYearId };
    $.ajax({
        type: "POST",
        url: "../Dashboard/SetFinancialYear",
        data: data,
        asnc: false,
        success: function (data) {
        
        },
        error: function (Result) {
        
        }
    });
}

function SaveSellerProduct() {
    var ddlProductMainGroup = $("#ddlProductMainGroup");
    var ddlProductSubGroup = $("#ddlProductSubGroup");
    var ddlProductSubChildGroup = $("#ddlProductSubChildGroup");
    var hdnProductId = $("#hdnProductId");
    var txtProductName = $("#txtProductName");
    //var txtSpecificationName = $("#txtSpecificationName");
    //var txtSpecificationValue = $("#txtSpecificationValue");
    //var ddlUOM = $("#ddlUOM");
    var txtBrandName = $("#txtBrandName");
    var txtItemCode = $("#txtItemCode");
    var ddlGST = $("#ddlGST");
    var ddlGSTPercentage = $("#ddlGSTPercentage");
    var ddlDeliveryStatus = $("#ddlDeliveryStatus");
    
    var FinalProductUOM = $("#ddlProductUOM");


    if (ddlProductMainGroup.val() == "" || ddlProductMainGroup.val() == "0") {
        ShowModel("Alert", "Please select Product Main Group");
        ddlProductMainGroup.focus();
        return false;
    }
    if (ddlProductSubGroup.val() == "" || ddlProductSubGroup.val() == "0") {
        ShowModel("Alert", "Please select Product Sub Group");
        ddlProductSubGroup.focus();
        return false;
    }
    if (ddlProductSubChildGroup.val() == "" || ddlProductSubChildGroup.val() == "0") {
        ShowModel("Alert", "Please select Product Sub  Child Group");
        ddlProductSubChildGroup.focus();
        return false;
    }

    if (ddlGST.val() == "" || ddlGST.val() == "0") {
        ShowModel("Alert", "Please select Product Sub  Child Group");
        ddlGST.focus();
        return false;
    }
    if (ddlGSTPercentage.val() == "" || ddlGSTPercentage.val() == "0") {
        ShowModel("Alert", "Please select Gst Percentage");
        ddlGSTPercentage.focus();
        return false;
    }
    //if (ddlDeliveryStatus.val() == "" || ddlDeliveryStatus.val() == "0") {
    //    ShowModel("Alert", "Please select Product Sub  Child Group");
    //    ddlDeliveryStatus.focus();
    //    return false;
    //}

    if (txtProductName.val().trim() == "") {
        ShowModel("Alert", "Please enter Product Name")
        txtProductName.focus();
        return false;
    }
    //if (txtSpecificationName.val().trim() == "") {
    //    ShowModel("Alert", "Please enter Specification Name")
    //    txtSpecificationName.focus();
    //    return false;
    //}
    //if (txtSpecificationValue.val().trim() == "") {
    //    ShowModel("Alert", "Please enter Specification Value")
    //    txtSpecificationValue.focus();
    //    return false;
    //}
    //if (ddlUOM.val() == "" || ddlUOM.val() == "0") {
    //    ShowModel("Alert", "Please select Product UOM");
    //    ddlUOM.focus();
    //    return false;
    //}
    //if (txtBrandName.val().trim() == "") {
    //    ShowModel("Alert", "Please enter Brand Name")
    //    txtBrandName.focus();
    //    return false;
    //}
    //if (txtItemCode.val().trim() == "") {
    //    ShowModel("Alert", "Please enter Item Code")
    //    txtItemCode.focus();
    //    return false;
    //}
    if (FinalProductUOM.val() == "" || FinalProductUOM.val() == "0") {
        ShowModel("Alert", "Please select Product UOM");
        FinalProductUOM.focus();
        return false;
    }

    //************ multiple***************

    //var hdnProductId = $("#hdnProductId");



    var Specifications = [];
    $(".spec-row").each(function () {
        var ddlSpecificationName = $(this).find(".ddl-spec-name").first();
        //var ddlSpecificationValue = $(this).find(".ddl-spec-value").first()
        var ddlSpecificationValue = $(this).find("#txtSpecificationValue").first();
        //var ddlUOM = $(this).find(".ddl-spec-uom").first()
        var ddlUOM = $(this).find("#hdnUOMId").first();//$(this).find(".ddl-spec-uom").first()
        var specName = ddlSpecificationName.find("option:selected").text();
       

        var techSpecId = ddlSpecificationName.val();

        //if (techSpecId == undefined || techSpecId == null || techSpecId == "0" || techSpecId == "") {
        //    ShowModel("Alert", "Please enter Specification Name")
        //    ddlSpecificationName.focus();
        //    Specifications = []
        //    return false;
        //}

        //if (techSpecId == undefined || techSpecId == null || techSpecId == "0" || techSpecId == "") {
        //    ShowModel("Alert", "Please enter Specification Value")
        //    ddlSpecificationValue.focus();
        //    Specifications = []
        //    return false;
        //}

       // var specValue = ddlSpecificationValue.find("option:selected").text();
        var specValue = ddlSpecificationValue.val();;

        var uomId = ddlUOM.val();

        //Not Mendetory

        //if (uomId == undefined || uomId == null || uomId == "0" || uomId == "") {
        //    ShowModel("Alert", "Please select Product UOM");
        //    ddlUOM.focus();
        //    Specifications = []
        //    return false;
        //}

        Specifications.push({ TechSpecId: techSpecId, SpecValue: specValue, SpecName: specName, UomId: uomId })
    });

    if (Specifications.length == 0)
        return false;
    //***************************************
    var Brands = $("#txtBrandName option:selected").html();
    var items = $("#txtItemCode option:selected").html();
    var requestData = {
        ParentClassId: ddlProductMainGroup.find("option:selected").val(),
        CategoryId: ddlProductSubGroup.find("option:selected").val(),
        SubCategoryId: ddlProductSubChildGroup.find("option:selected").val(),
        ProductId: hdnProductId.val(),
        ProductName: txtProductName.val(),
        //SpecificationName: txtSpecificationName.val(),
        //SpecificationValue: txtSpecificationValue.val(),
        UomId: FinalProductUOM.val().trim(),
        GSTID: ddlGST.find("option:selected").val(),
        GSTPercentageID: ddlGSTPercentage.find("option:selected").val(),
        DeliveryStatusID: ddlDeliveryStatus.find("option:selected").val(),
        BrandName: txtBrandName.val(),
        BrandId: txtBrandName.val(),
        ProductCode: txtItemCode.val(),
        Specifications: Specifications,
        Brands: Brands,
        items: items
    };

    $.ajax({
        url: "../SellerProduct/AddEditSellerProduct",
        cache: false,
        type: "POST",
        dataType: "json",
        data: JSON.stringify(requestData),
        contentType: 'application/json',
        success: function (data) {
            if (data.status == "SUCCESS") {

                ShowModel("Alert", data.message);
                setTimeout(
                    function () {
                        window.location.href = "../SellerProduct/AddEditSellerProduct";
                    }, 4000);
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


function BindGSTList() {
    $.ajax({
        type: "GET",
        url: "../SellerProduct/GetGSTList",
        dataType: "json",
        async: false,
        success: function (data) {
            $("#ddlGST").append($("<option></option>").val(0).html("-Select GST-"));
            $.each(data, function (i, item) {
                $("#ddlGST").append($("<option></option>").val(item.GSTID).html(item.GSTName));
            });
        },
        error: function (Result) {
            $("#ddlGST").append($("<option></option>").val(0).html("-Select GST-"));
        }
    });
}

function BindGSTListPercenta() {
    $.ajax({
        type: "GET",
        url: "../SellerProduct/GetGSTPercentageList",
        dataType: "json",
        async: false,
        success: function (data) {
            $("#ddlGSTPercentage").append($("<option></option>").val(0).html("-Select GSTPercentage-"));
            $.each(data, function (i, item) {
                $("#ddlGSTPercentage").append($("<option></option>").val(item.GSTPercentageID).html(item.GSTPercentageName));
            });
        },
        error: function (Result) {
            $("#ddlGSTPercentage").append($("<option></option>").val(0).html("-Select GSTPercentage-"));
        }
    });
}

function BindDeliveryStatusList() {
    $.ajax({
        type: "GET",
        url: "../SellerProduct/GetDeliveryStatusList",
        dataType: "json",
        async: false,
        success: function (data) {
            $("#ddlDeliveryStatus").append($("<option></option>").val(0).html("-Select Delivery Status-"));
            $.each(data, function (i, item) {
                $("#ddlDeliveryStatus").append($("<option></option>").val(item.DelivryStatusID).html(item.DeliveryName));
            });
        },
        error: function (Result) {
            $("#ddlDeliveryStatus").append($("<option></option>").val(0).html("-Select Delivery Status-"));
        }
    });
}

function DeleteSellerProduct(sender) {
    if (confirm("Do you want to delete the product detail?")) {
        var sellerProductDetailId = $(sender).closest("td").find(".DeleteId").first().val();
        if (sellerProductDetailId != undefined && sellerProductDetailId != "0") {
            var requestData = { sellerProductDetailId: sellerProductDetailId };
            $.ajax({
                url: "../SellerProduct/DeleteSellerProductDetail",
                cache: false,
                type: "POST",
                dataType: "json",
                data: JSON.stringify(requestData),
                contentType: 'application/json',
                success: function (data) {
                    if (data.status == "SUCCESS") {
                        ShowModel("Alert", data.message);
                        setTimeout(
                          function () {
                              BindSellerProductDetails("../SellerProduct/GetAllSellerProducts", null)
                          }, 2000);
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
}


function Check_T_UOMList(url, data) {

    $.ajax({
        type: "GET",
        url: url,
        data: data,
        asnc: false,
        dataType: "json",
        success: function (data) {
            disableUOM(data);
        },
        error: function (Result) {
            ShowModel("Alert", "Problem in Request");
        }
    });

}

function disableUOM(UOMId) {
    if (UOMId > 0) {
        $("#ddlProductUOM").val(UOMId);
        $("#ddlProductUOM").prop("disabled", true);
    }
    else {
        $("#ddlProductUOM").prop("disabled", false);
    }
}


function CheckUOM_SUM_List(sender) {
    
    var specName = sender.value;

    var txtUOMName = $(sender).closest(".spec-row").find(".ddl-spec-uom");

    var productMainGroupId = $("#ddlProductMainGroup option:selected").val();
    var productSubGroupId = $("#ddlProductSubGroup option:selected").val();
    var productChildSubGroupId = $("#ddlProductSubChildGroup option:selected").val();



    if (productMainGroupId != undefined && productSubGroupId != undefined && productMainGroupId != "0" && productSubGroupId != "0" && specName != "0") {
        var requestData = { productMainGroupId: productMainGroupId, productSubGroupId: productSubGroupId, productChildSubGroupId: productChildSubGroupId, ProductTechSpecId: specName };
        //Disable UOM list if already exixts

        $.ajax({
            type: "GET",
            url: "../Product/GetUOMListforSpecification",
            data: requestData,
            dataType: "json",
            asnc: false,
            success: function (data) {
                $(sender).closest(".spec-row").find(".ddl-spec-uom").val("None");
                $(sender).closest(".spec-row").find(".hdn-spec-uom").val("0");
                $(sender).closest(".spec-row").find(".ddl-spec-uom").prop("disabled", false);
                $.each(data, function (i, item) {

                    if (txtUOMName != undefined && item.UOMId > 0) {
                        $(sender).closest(".spec-row").find(".ddl-spec-uom").val(item.UOMName);
                        $(sender).closest(".spec-row").find(".hdn-spec-uom").val(item.UOMId);
                        $(sender).closest(".spec-row").find(".ddl-spec-uom").prop("disabled", true);
                    }

                    else {
                        $(sender).closest(".spec-row").find(".ddl-spec-uom").prop("disabled", false);
                        $(sender).closest(".spec-row").find(".ddl-spec-uom").val(0);
                    }
                });
            },
            error: function (Result) {
                ShowModel("Alert", "Problem in Request");
            }
        });

    }





}
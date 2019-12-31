/// <reference path="../bootstrap.min.js" />
$(document).ready(function () {
    BindProductTypeList();
    BindProductMainGroupList();
    BindUOMList();
    BindProductNameGeneration();
    SetUOMSearch();
    $("#ddlProductSubGroup").append($("<option></option>").val(0).html("-Select Category-"));
    $("#ddlProductSubChildGroup").append($("<option></option>").val(0).html("-Select Sub Category-"));
    var hdnAccessMode = $("#hdnAccessMode");
    var hdnProductId = $("#hdnProductId");
    if (hdnProductId.val() != "" && hdnProductId.val() != "0" && hdnAccessMode.val() != "" && hdnAccessMode.val() != "0") {
        setTimeout(
            function () {
                GetProductDetail(hdnProductId.val());
            }, 3000);


        if (hdnAccessMode.val() == "3") {
            $("#btnSave").hide();
            $("#btnUpdate").hide();
            $("#btnReset").hide();
            $("input").attr('readOnly', true);
            $("select").attr('disabled', true);
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
    //$("#txtProductName").focus();
    /*var producImageList = [];
    GetProductImageList(producImageList);
    var productTechSpecificationList = [];
    GetProductTechSpecificationList(productTechSpecificationList);*/
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
function ValidEmailCheck(emailAddress) {
    var pattern = /^([a-z\d!#$%&'*+\-\/=?^_`{|}~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]+(\.[a-z\d!#$%&'*+\-\/=?^_`{|}~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]+)*|"((([ \t]*\r\n)?[ \t]+)?([\x01-\x08\x0b\x0c\x0e-\x1f\x7f\x21\x23-\x5b\x5d-\x7e\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|\\[\x01-\x09\x0b\x0c\x0d-\x7f\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))*(([ \t]*\r\n)?[ \t]+)?")@(([a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|[a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF][a-z\d\-._~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]*[a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])\.)+([a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|[a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF][a-z\d\-._~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]*[a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])\.?$/i;
    return pattern.test(emailAddress);
};

function SetUOMSearch() {
    
    $(".ddl-spec-uom").autocomplete({

    minLength: 0,
    source: function (request, response) {
        var isSum = true;
        var dataObj = { searchTerm: request.term, IsSUM: isSum }
        $.ajax({
            url: "../Product/GetUOMAutoCompleteList",
            type: "GET",
            dataType: "json",
            data: dataObj,
            success: function (data) {
                response($.map(data, function (item) {
                    return { label: item.UOMName, value: item.UOMId };
                }))
            }
        })
    },
        focus: function (event, ui) {
            $(this).val(ui.item.label);
        return false;
    },
    select: function (event, ui) {
        $(this).val(ui.item.label);
        $(this).closest("div").find(".hdn-spec-uom").val(ui.item.value);
        
        return false;
    },
    change: function (event, ui) {
        if (ui.item == null) {
            $(this).val("None");
            $(this).closest("div").find(".hdn-spec-uom").val("0");

        }
       
    }

})
.autocomplete("instance")._renderItem = function (ul, item) {

    return $("<li>")
        .append("<div><b>" + item.label + " || " + item.value + "</b><br>" +  "</div>")
      .appendTo(ul);
};
}

function SaveBuyerProduct() {
    var ddlProductMainGroup = $("#ddlProductMainGroup");
    var ddlProductSubGroup = $("#ddlProductSubGroup");
    var ddlProductSubChildGroup = $("#ddlProductSubChildGroup");

    var ddlProductUOM = $("#ddlProductUOM");

    var txtProductName = $("#txtProductName");
    var txtBrandName = $("#txtBrandName");
    var txtItemCode = $("#txtItemCode");

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
    if (txtProductName.val().trim() == "") {
        ShowModel("Alert", "Please enter Product Name")
        txtProductName.focus();
        return false;
    }
    if (ddlProductUOM.val().trim() == "") {
        ShowModel("Alert", "Please select UOM")
        ddlProductUOM.focus();
        return false;
    }

    //************ multiple***************

    //var hdnProductId = $("#hdnProductId");



    //var Specifications = [];
    //$(".spec-row").each(function () {
    //    var ddlSpecificationName = $(this).find(".ddl-spec-name").first();
    //    var ddlSpecificationValue = $(this).find(".ddl-spec-value").first()
    //    var ddlUOM = $(this).find(".ddl-spec-uom").first()

    //    var specName = ddlSpecificationName.val();
    //    if (specName == undefined || specName == null || specName == "0" || specName == "") {
    //        ShowModel("Alert", "Please enter Specification Name")
    //        ddlSpecificationName.focus();
    //        Specifications = []
    //        return false;
    //    }

    //    var techSpecId = ddlSpecificationValue.val();
    //    if (techSpecId == undefined || techSpecId == null || techSpecId == "0" || techSpecId == "") {
    //        ShowModel("Alert", "Please enter Specification Value")
    //        ddlSpecificationValue.focus();
    //        Specifications = []
    //        return false;
    //    }

    //    var specValue = ddlSpecificationValue.find("option:selected").text();

    //    var uomId = ddlUOM.val();
    //    if (uomId == undefined || uomId == null || uomId == "0" || uomId == "") {
    //        ShowModel("Alert", "Please select Product UOM");
    //        ddlUOM.focus();
    //        Specifications = []
    //        return false;
    //    }

    //    Specifications.push({ TechSpecId: techSpecId, SpecValue: specValue, SpecName: specName, UomId: uomId })
    //});


    var Specifications = [];
    $(".spec-row").each(function () {
        var ddlSpecificationName = $(this).find(".ddl-spec-name").first();
        //var ddlSpecificationValue = $(this).find(".ddl-spec-value").first()
        var ddlSpecificationValue = $(this).find("#txtSpecificationValue").first();
        var ddlUOM = $(this).find(".hdn-spec-uom").first()

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
        if (uomId == undefined || uomId == null || uomId == "0" || uomId == "") {
            //ShowModel("Alert", "Please select Product UOM");
            //ddlUOM.focus();
            //Specifications = []
            //return false;
            uomId = 0;
        }

        Specifications.push({ TechSpecId: techSpecId, SpecValue: specValue, SpecName: specName, UomId: uomId })
    });

    if (Specifications.length == 0)
        return false;
    //***************************************


    //if (txtBrandName.val().trim() == "" || txtBrandName.val() == "0") {
    //    ShowModel("Alert", "Please select Brand Name")
    //    txtBrandName.focus();
    //    return false;
    //}
    //if (txtItemCode.val().trim() == "" || txtItemCode.val() == "0") {
    //    ShowModel("Alert", "Please select Item Code")
    //    txtItemCode.focus();
    //    return false;
    //}
    var Brands = $("#txtBrandName option:selected").html();
    var items = $("#txtItemCode option:selected").html();
    var requestData = {
        ParentClassId: ddlProductMainGroup.find("option:selected").val(),
        CategoryId: ddlProductSubGroup.find("option:selected").val(),
        SubCategoryId: ddlProductSubChildGroup.find("option:selected").val(),
        ProductName: txtProductName.val(),
        Specifications: Specifications,
        BrandId: txtBrandName.val(),
        BrandName: txtBrandName.val(),
        ProductCode: txtItemCode.val(),
        Brands: Brands,
        items: items,
        UomId: ddlProductUOM.val().trim()
    };

    $.ajax({
        url: "../BuyerProduct/AddEditBuyerProduct",
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
                        window.location.href = "../BuyerProduct/AddEditBuyerProduct";
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

function DeleteBuyerProduct(sender) {
    if (confirm("Do you want to delete the product detail?")) {
        var buyerProductDetailId = $(sender).closest("td").find(".DeleteId").first().val();
        if (buyerProductDetailId != undefined && buyerProductDetailId != "0") {
            var requestData = { buyerProductDetailId: buyerProductDetailId };
            $.ajax({
                url: "../BuyerProduct/DeleteBuyerProductDetail",
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
                                BindBuyerProductDetails();
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

function ShowAddProductSubChildGroup() {
    var ddlProductMainGroup = $("#ddlProductMainGroup");
    var ddlProductSubGroup = $("#ddlProductSubGroup");
    if (ddlProductMainGroup.val() == "" || ddlProductMainGroup.val() == "0") {
        ShowModel("Alert", "Please Select Parent Class (C1)");
        ddlProductMainGroup.focus();
        return false;
    }
    if (ddlProductSubGroup.val() == "" || ddlProductSubGroup.val() == "0") {
        ShowModel("Alert", "Please Select Category(C2)");
        ddlProductSubGroup.focus();
        return false;
    }
    var popup = $("#AddSubCategory").modal();
    $("#AddSubCategory").css("z-index", 1045);
}


function ShowAddProductBrand() {
    $(".brand-message").text("");
    var ddlProductMainGroup = $("#ddlProductMainGroup");
    var ddlProductSubGroup = $("#ddlProductSubGroup");

    var ddlProductsubSubGroup = $("#ddlProductSubChildGroup");

    if (ddlProductMainGroup.val() == "" || ddlProductMainGroup.val() == "0") {
        ShowModel("Alert", "Please Select Parent Class (C1)");
        ddlProductMainGroup.focus();
        return false;
    }
    if (ddlProductSubGroup.val() == "" || ddlProductSubGroup.val() == "0") {
        ShowModel("Alert", "Please Select Category(C2)");
        ddlProductSubGroup.focus();
        return false;
    }

    if (ddlProductsubSubGroup.val() == "" || ddlProductsubSubGroup.val() == "0") {
        ShowModel("Alert", "Please Select sub Category(3)");
        ddlProductsubSubGroup.focus();
        return false;
    }

    var popup = $("#AddNewBrandModal").modal();
    $("#AddNewBrandModal").css("z-index", 1045);
}

function ShowAddProductItem() {
    $(".Item-message").text("");
    var ddlProductMainGroup = $("#ddlProductMainGroup");
    var ddlProductSubGroup = $("#ddlProductSubGroup");

    var ddlProductsubSubGroup = $("#ddlProductSubChildGroup");


    if (ddlProductMainGroup.val() == "" || ddlProductMainGroup.val() == "0") {
        ShowModel("Alert", "Please Select Parent Class (C1)");
        ddlProductMainGroup.focus();
        return false;
    }
    if (ddlProductSubGroup.val() == "" || ddlProductSubGroup.val() == "0") {
        ShowModel("Alert", "Please Select Category(C2)");
        ddlProductSubGroup.focus();
        return false;
    }

    if (ddlProductsubSubGroup.val() == "" || ddlProductsubSubGroup.val() == "0") {
        ShowModel("Alert", "Please Select sub Category(3)");
        ddlProductsubSubGroup.focus();
        return false;
    }
    var popup = $("#AddNewProductItemModal").modal();
    $("#AddNewProductItemModal").css("z-index", 1045);

}


function BindBuyerProductDetails(url, data) {

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


function LoadAllBuyerProducts() {
    BindBuyerProductDetails("../BuyerProduct/GetAllBuyerProducts", null)
}

function LoadFilteredBuyerProducts() {
    $(".list_product").empty();
    var productMainGroupId = $("#ddlProductMainGroup option:selected").val();
    var productSubGroupId = $("#ddlProductSubGroup option:selected").val();
    var productChildSubGroupId = $("#ddlProductSubChildGroup option:selected").val();
    if (productChildSubGroupId == undefined || productChildSubGroupId == "0") {
        return;
    }

    if (productMainGroupId != undefined && productSubGroupId != undefined && productMainGroupId != "0" && productSubGroupId != "0") {
        var data = { productMainGroupId: productMainGroupId, productSubGroupId: productSubGroupId, productChildSubGroupId: productChildSubGroupId };
        BindBuyerProductDetails("../BuyerProduct/GetBuyerProducts", data);
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


function BindBuyerDashboardProducts() {
    $(".creative_tabs_container").empty();
    $.ajax({
        type: "GET",
        url: "../BuyerProduct/GetDashboardBuyerProducts",
        async: false,
        dataType: "html",
        success: function (html) {
            $(".creative_tabs_container").html(html);
            const portItem = jQuery('.portItem');
            const portItemCont = jQuery('.portItemCont');
            portItem.find(portItemCont).filter(":not(:first)").slideUp();
            portItem.find('h3').on('click', function () {
                portItemCont.slideUp();
                jQuery(this).next(portItemCont).slideDown();
            });
        },
        error: function (Result) {
            ShowModel("Alert", "Problem in Request");
        }
    });

}

function SearchProduct() {

    var txtProductName = $("#txtProductName");
    var txtProductCode = $("#txtProductCode");
    var txtProductShortDesc = $("#txtProductShortDesc");
    var txtProductFullDesc = $("#txtProductFullDesc");
    var ddlProductType = $("#ddlProductType");
    var ddlProductMainGroup = $("#ddlProductMainGroup");
    var ddlAssemblyType = $("#ddlAssemblyType");
    var txtBrandName = $("#txtBrandName");

    //var requestData = { productName: txtProductName.val().trim(), productCode: txtProductCode.val().trim(), productShortDesc: txtProductShortDesc.val().trim(), productFullDesc: txtProductFullDesc.val().trim(), productTypeId: ddlProductType.val(), productMainGroupId: ddlProductMainGroup.val(), assemblyType: ddlAssemblyType.val(), brandName: txtBrandName.val().trim() };
    var requestData = { productName: '', productCode: '', productShortDesc: '', productFullDesc: '', productTypeId: '0', productMainGroupId: '0', assemblyType: '0', brandName: '' };
    $.ajax({
        url: "../Product/GetProductList",
        data: requestData,
        dataType: "html",
        asnc: true,
        type: "GET",
        error: function (err) {
            $("#list_product").html("");
            $("#list_product").html(err);
        },
        success: function (data) {
            $("#list_product").html("");
            $("#list_product").html(data);

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
                if (item.ProductTypeId == 4) {
                    $("#ddlProductType").append($("<option selected></option>").val(item.ProductTypeId).html(item.ProductTypeName));
                }
                else {
                    $("#ddlProductType").append($("<option></option>").val(item.ProductTypeId).html(item.ProductTypeName));
                }
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
            $(".category-1").append($("<option></option>").val(0).html("-Select Parent Class-"));
            $.each(data, function (i, item) {
                $(".category-1").append($("<option></option>").val(item.ProductMainGroupId).html(item.ProductMainGroupName));
            });
        },
        error: function (Result) {
            $(".category-1").append($("<option></option>").val(0).html("-Select Parent Class-"));
        }
    });
}

function BindProductSubGroupList(sender) {
    BindSpecNames();
    var category1Dropdown = $(sender);
    var productMainGroupId = category1Dropdown.find("option:selected").val();
    var category2Dropdown = category1Dropdown.closest("form").find(".category-2");
    $(".category-1-txt").val(category1Dropdown.find("option:selected").text());
    category2Dropdown.val(0);
    category2Dropdown.html("");
    if (productMainGroupId != undefined && productMainGroupId != "" && productMainGroupId != "0") {
        var data = { productMainGroupId: productMainGroupId };
        $.ajax({
            type: "GET",
            url: "../Product/GetProductSubGroupList",
            data: data,
            asnc: false,
            dataType: "json",
            success: function (data) {
                category2Dropdown.append($("<option></option>").val(0).html("-Select Category-"));
                $.each(data, function (i, item) {
                    category2Dropdown.append($("<option></option>").val(item.ProductSubGroupId).html(item.ProductSubGroupName));
                });
                category2Dropdown.val(0);
            },
            error: function (Result) {
                category2Dropdown.append($("<option></option>").val(0).html("-Select Category-"));
            }
        });
    }
    else {

        category2Dropdown.append($("<option></option>").val(0).html("-Select Category-"));
    }

}

function BindProductSubGroupListNew(SubGroupName) {


    var productMainGroupId = $('#ddlProductMainGroup').find("option:selected").val();
    var category2Dropdown = $('#ddlProductMainGroup').closest("form").find(".category-2");
    $(".category-1-txt").val($('#ddlProductMainGroup').find("option:selected").text());
    category2Dropdown.val(0);
    category2Dropdown.html("");
    if (productMainGroupId != undefined && productMainGroupId != "" && productMainGroupId != "0") {
        var data = { productMainGroupId: productMainGroupId };
        $.ajax({
            type: "GET",
            url: "../Product/GetProductSubGroupList",
            data: data,
            asnc: false,
            dataType: "json",
            success: function (data) {
                category2Dropdown.append($("<option></option>").val(0).html("-Select Category-"));
                $.each(data, function (i, item) {
                    category2Dropdown.append($("<option></option>").val(item.ProductSubGroupId).html(item.ProductSubGroupName));
                });
                $("#ddlProductSubGroup").val(SubGroupName);
            },
            error: function (Result) {
                category2Dropdown.append($("<option></option>").val(0).html("-Select Category-"));
            }
        });
    }
    else {

        category2Dropdown.append($("<option></option>").val(0).html("-Select Category-"));
    }

    // alert(SubGroupName);
    //$('#ddlProductSubGroup option[value=' + SubGroupName + ']').attr("selected", "selected");
    //category2Dropdown.val(SubGroupName);

}

function BindProductSubChildGroupList(sender) {
    BindSpecNames();
    var category2Dropdown = $(sender);
    var category1Dropdown = category2Dropdown.closest("form").find(".category-1");
    var category3Dropdown = category2Dropdown.closest("form").find(".category-3");

    var productMainGroupId = category1Dropdown.find("option:selected").val();
    var productSubGroupId = category2Dropdown.find("option:selected").val();

    $(".category-2-txt").val(category2Dropdown.find("option:selected").text());

    category3Dropdown.val(0);
    category3Dropdown.html("");
    if (productMainGroupId != undefined && productMainGroupId != "" || productSubGroupId != undefined && productSubGroupId != "" && productSubGroupId != "0") {
        var data = { productMainGroupId: productMainGroupId, productSubGroupId: productSubGroupId };
        $.ajax({
            type: "GET",
            url: "../Product/GetProductSubChildGroupList",
            data: data,
            asnc: false,
            dataType: "json",
            success: function (data) {
                category3Dropdown.append($("<option></option>").val(0).html("-Select Sub Category-"));
                $.each(data, function (i, item) {
                    category3Dropdown.append($("<option></option>").val(item.ProductSubGroupId).html(item.ProductSubGroupName));
                });
                //#Commented
                ///category3Dropdown.val(productSubGroupId);
            },
            error: function (Result) {
                category3Dropdown.append($("<option></option>").val(0).html("-Select Sub Category-"));
            }
        });
    }
    else {

        $("#ddlProductSubGroup").append($("<option></option>").val(0).html("-Select Sub Category-"));
    }

}

function BindProductSubChildGroupListNew(subgroupchildid) {

    var productMainGroupId = $("#ddlProductMainGroup option:selected").val();
    var productSubGroupId = $("#ddlProductSubGroup option:selected").val();

    $("#ddlProductSubChildGroup").val(0);
    $("#ddlProductSubChildGroup").html("");
    if (productMainGroupId != undefined && productMainGroupId != "" || productSubGroupId != undefined && productSubGroupId != "" && productSubGroupId != "0") {
        var data = { productMainGroupId: productMainGroupId, productSubGroupId: productSubGroupId };
        $.ajax({
            type: "GET",
            url: "../Product/GetProductSubChildGroupList",
            data: data,
            asnc: false,
            dataType: "json",
            success: function (data) {
                $("#ddlProductSubChildGroup").append($("<option></option>").val(0).html("-Select Sub Category -"));
                $.each(data, function (i, item) {
                    $("#ddlProductSubChildGroup").append($("<option></option>").val(item.ProductSubGroupId).html(item.ProductSubGroupName));
                });
                $("#ddlProductSubChildGroup").val(subgroupchildid);
            },
            error: function (Result) {
                $("#ddlProductSubChildGroup").append($("<option></option>").val(0).html("-Select Sub Category -"));
            }
        });
    }
    else {

        $("#ddlProductSubChildGroup").append($("<option></option>").val(0).html("-Select Sub Category-"));
    }

}

function BindProductSubGroupList1(productMainGroupId) {
    
    $("#ddlProductSubGroup").val(0);
    $("#ddlProductSubGroup").html("");
    BindSpecNames();
       if (productMainGroupId != undefined && productMainGroupId != "" && productMainGroupId != "0") {
        var data = { productMainGroupId: productMainGroupId };
        $.ajax({
            type: "GET",
            url: "../Product/GetProductSubGroupList",
            data: data,
            asnc: false,
            dataType: "json",
            success: function (data) {
                $("#ddlProductSubGroup").append($("<option></option>").val(0).html("-Select Category-"));
                $.each(data, function (i, item) {
                    $("#ddlProductSubGroup").append($("<option></option>").val(item.ProductSubGroupId).html(item.ProductSubGroupName));
                });
                $("#ddlProductSubGroup").val(productMainGroupId);
            },
            error: function (Result) {
                $("#ddlProductSubGroup").append($("<option></option>").val(0).html("-Select Category-"));
            }
        });
    }
    else {

           $("#ddlProductSubGroup").append($("<option></option>").val(0).html("-Select Category-"));
    }

}

function BindProductSubChildGroupListNew1(productMainGroupId, productSubGroupId,subgroupchildid) {
    
    $("#ddlProductSubChildGroup").val(0);
    $("#ddlProductSubChildGroup").html("");
    if (productMainGroupId != undefined && productMainGroupId != "" || productSubGroupId != undefined && productSubGroupId != "" && productSubGroupId != "0") {
        var data = { productMainGroupId: productMainGroupId, productSubGroupId: productSubGroupId };
        $.ajax({
            type: "GET",
            url: "../Product/GetProductSubChildGroupList",
            data: data,
            asnc: false,
            dataType: "json",
            success: function (data) {
                $("#ddlProductSubChildGroup").append($("<option></option>").val(0).html("-Select Sub Category -"));
                $.each(data, function (i, item) {
                    $("#ddlProductSubChildGroup").append($("<option></option>").val(item.ProductSubGroupId).html(item.ProductSubGroupName));
                });
                $("#ddlProductSubChildGroup").val(subgroupchildid);
            },
            error: function (Result) {
                $("#ddlProductSubChildGroup").append($("<option></option>").val(0).html("-Select Sub Category -"));
            }
        });
    }
    else {

        $("#ddlProductSubChildGroup").append($("<option></option>").val(0).html("-Select Sub Category-"));
    }

}

function BindUOMList() {

    $.ajax({
        type: "GET",
        url: "../Product/GetUOMList",
        data: "{}",
        dataType: "json",
        asnc: false,
        success: function (data) {
            var filteredData = data.filter(x => x.IsSUM == false);
            $(".uom-list").append($("<option></option>").val(0).html("-Select UOM-"));
            $.each(filteredData,function (i, item) {
                $(".uom-list").append($("<option></option>").val(item.UOMId).html(item.UOMName));
            });
        },
        error: function (Result) {

            $(".uom-list").append($("<option></option>").val(0).html("-Select UOM-"));
        }
    });
}


function BindBrandList() {
    $("#txtBrandName option").remove();
    var ParentClass = $("#ddlProductMainGroup").val();
    var Category = $("#ddlProductSubGroup").val();
    var SubCategory = $("#ddlProductSubChildGroup").val();
    var data = { ProductMainGroupId: ParentClass, ProductSubGroupId: Category, ProductSubChildGroupId: SubCategory };

    $.ajax({
        type: "GET",
        url: "../Product/GetBrandList",
        data: data,
        dataType: "json",
        asnc: false,
        success: function (data) {

            $(".Brand-list").append($("<option></option>").val(0).html("-Select Brand-"));
            $.each(data, function (i, item) {
                $(".Brand-list").append($("<option></option>").val(item.BrandID).html(item.BrandName));
            });
        },
        error: function (Result) {

            $(".Brand-list").append($("<option></option>").val(0).html("-Select Brand-"));
        }
    });
}


function BindItemList() {
    $("#txtItemCode option").remove();
    var ParentClass = $("#ddlProductMainGroup").val();
    var Category = $("#ddlProductSubGroup").val();
    var SubCategory = $("#ddlProductSubChildGroup").val();
    var data = { ProductMainGroupId: ParentClass, ProductSubGroupId: Category, ProductSubChildGroupId: SubCategory };

    $.ajax({
        type: "GET",
        url: "../Product/GetItemList",
        data: data,
        dataType: "json",
        asnc: false,
        success: function (data) {

            $(".Item-list").append($("<option></option>").val(0).html("-Select Item-"));
            $.each(data, function (i, item) {
                $(".Item-list").append($("<option></option>").val(item.ProductCodeID).html(item.ProductCodeValue));
            });
        },
        error: function (Result) {

            $(".Item-list").append($("<option></option>").val(0).html("-Select Item-"));
        }
    });
}



function BindSpecNames() {
    $(".ddl-spec-name").val("0").html("");
    $(".ddl-spec-value").val("0").html("");
    $(".ddl-spec-uom").val("None")

    var productMainGroupId = $("#ddlProductMainGroup option:selected").val();
    var productSubGroupId = $("#ddlProductSubGroup option:selected").val();
    var productChildSubGroupId = $("#ddlProductSubChildGroup option:selected").val();
    if (productChildSubGroupId == undefined || productChildSubGroupId == "0") {
        return;
    }

    if (productMainGroupId != undefined && productSubGroupId != undefined && productChildSubGroupId != undefined
        && productMainGroupId != "0" && productSubGroupId != "0" && productChildSubGroupId != "0") {

        var data = { productMainGroupId: productMainGroupId, productSubGroupId: productSubGroupId, productChildSubGroupId: productChildSubGroupId };
        $.ajax({
            type: "GET",
            url: "../Specification/GetSpecifications",
            dataType: "json",
            data: data,
            asnc: false,
            success: function (data) {
                $(".ddl-spec-name").append($("<option></option>").val(0).html("-Select Specification Name"));
                $.each(data, function (i, item) {
                    $(".ddl-spec-name").append($("<option></option>").val(item.ProductTechSpecId).html(item.ProductTechSpecName));
                });
            },
            error: function (Result) {
                $(".ddl-spec-name").append($("<option></option>").val(0).html("-Select Specification Name"));
            }
        });

    }
}

function BindSpecNames1(productMainGroupId, productSubGroupId, productChildSubGroupId) {
    $(".ddl-spec-name").val("0").html("");
    $(".ddl-spec-value").val("0").html("");
    $(".ddl-spec-uom").val("None")
    if (productChildSubGroupId == undefined || productChildSubGroupId == "0") {
        return;
    }

    if (productMainGroupId != undefined && productSubGroupId != undefined && productChildSubGroupId != undefined
        && productMainGroupId != "0" && productSubGroupId != "0" && productChildSubGroupId != "0") {

        var data = { productMainGroupId: productMainGroupId, productSubGroupId: productSubGroupId, productChildSubGroupId: productChildSubGroupId };
        $.ajax({
            type: "GET",
            url: "../Specification/GetSpecifications",
            dataType: "json",
            data: data,
            asnc: false,
            success: function (data) {
                $(".ddl-spec-name").append($("<option></option>").val(0).html("-Select Specification Name"));
                $.each(data, function (i, item) {
                    $(".ddl-spec-name").append($("<option></option>").val(item.ProductTechSpecId).html(item.ProductTechSpecName));
                });
            },
            error: function (Result) {
                $(".ddl-spec-name").append($("<option></option>").val(0).html("-Select Specification Name"));
            }
        });

    }
}

function BindSpecNamesNew() {

    $(".ddl-spec-name").append($("<option></option>").val(0).html("-Select Specification Name"));

}

function BindSpecValues(sender) {
    var specName = sender.value;
    var ddlSpecValue = $(sender).closest(".spec-row").find(".ddl-spec-value");
    ddlSpecValue.html("");
    $.ajax({
        type: "GET",
        url: "../Specification/GetSpecificationsByName?specName=" + specName,
        dataType: "json",
        asnc: false,
        success: function (data) {

            ddlSpecValue.append($("<option></option>").val(0).html("-Select Specification Value"));
            $.each(data, function (i, item) {
                ddlSpecValue.append($("<option></option>").val(item.ProductTechSpecId).html(item.ProductTechSpecValue));
            });

            // since addedSpecs are yet to be saved in DB, we load them from temp memory(addedSpecs)
            $.each(addedSpecs, function (i, item) {
                if (item.Name == specName)
                    ddlSpecValue.append($("<option></option>").val(item.Id).html(item.Value));
            });
        },
        error: function (Result) {
            ddlSpecValue.append($("<option></option>").val(0).html("-Select Specification Value"));
        }
    });
}

function AddSpecRow(sender) {
    var specRow = $(".spec-row").first().clone();
    specRow.find("label").hide();
    specRow.find(".add-product-icon").hide();
    specRow.find(".remove-product-icon").show();
    specRow.find(".ddl-spec-value").html("");
    specRow.insertBefore("#btnAddSpecRoe");
    var txtUOMName = specRow.find("#txtUOMName").first();

    SetUOMSearch();

}


function RemoveAddNewSpec(sender) {
    var specRow = $(sender).closest(".spec-row").remove();
}

function OpenAddNewSpec(sender) {
    var popup = $("#AddSpecModal").modal();
    $("#AddSpecModal input").val("");
    $(".spec-message").hide();
    $("#ddlUOMCreate").val(0);
}

var addedSpecs = [];

function CreateNewSpec() {
    var name = $("#txtSpecName").val();
    var value = $("#txtSpecValue").val();
    var uom = $("#ddlUOMCreate").val();

    var errorMsg = "";
    if (name.trim() == "")
        errorMsg = "Please enter specification name";

    //if (errorMsg == "" && value.trim() == "")
    //    errorMsg = "Please enter specification value";

    //if (errorMsg == "" && (uom == "" || uom == "0"))
    //    errorMsg = "Please select UOM";

    if (errorMsg != "") {
        $(".spec-message").removeClass("alert-success").addClass("alert-danger").show();
        $(".spec-message").text(errorMsg);
        setTimeout(function () { $(".spec-message").hide() }, 1000);
        return;
    }

    addedSpecs.push({ Id: "-1", Name: name, Value: value, UOM: uom });

    $(".ddl-spec-name").each(function () {
        // Add to name ddl if name not existing already
        //if (($(this).find("option[value='-1']")).length == 0)
        //if ($(this).text == name) {
            $(this).find("option").first().after($("<option></option>").val('-1').html(name))
       // }

        //// if the current item already selected add the spec value to spec-value ddl
        //if ($(this).val() == name) {
        //    var ddlSpecValue = $(this).closest(".spec-row").find(".ddl-spec-value")
        //    ddlSpecValue.find("option").first().after($("<option></option>").val("-1").html(value));
        //
    });

    $(".spec-message").addClass("alert-success").removeClass("alert-danger").show();
    $(".spec-message").text("Specification added successfully");
    setTimeout(function () { $(".spec-message").hide() }, 100000);

}

function GenerateProductName() {
    $("#txtProductName").val("");

    var C1Val = $("#ddlProductSubGroup").val();
    var C1 = $("#ddlProductSubGroup option:selected").text();
    if (C1Val == undefined || C1Val == null || C1Val == "0" || C1Val == "")
        C1 = "";

    var C2Val = $("#ddlProductSubChildGroup").val();
    var C2 = $("#ddlProductSubChildGroup option:selected").text();
    if (C2Val == undefined || C2Val == null || C2Val == "0" || C2Val == "")
        C2 = "";

    var SPECPARA = " (";
    $(".spec-row").each(function () {
        var A11Val = $(this).find(".ddl-spec-name").first().val();
        var A11 = $(this).find(".ddl-spec-name").first().find("option:selected").text();
        if (A11Val == undefined || A11Val == null || A11Val == "0" || A11Val == "")
            A11 = "";
        SPECPARA = SPECPARA + A11 + " ";
        var A12Val = $(this).find("#txtSpecificationValue").val();
        var A12 = $(this).find("#txtSpecificationValue").val();

        if (A12Val == undefined || A12Val == null || A12Val == "0" || A12Val == "")
            A12 = "";
        SPECPARA = SPECPARA + A12 + " ";
        var A13Val = $(this).find(".hdn-spec-uom").first().val(); 
        var A13 = $(this).find(".ddl-spec-uom").first().val();
        
        if (A13Val == undefined || A13Val == null || A13Val == "0" || A13Val == "")
            A13 = "";
        SPECPARA = SPECPARA + A13 + " ";
    });
    SPECPARA = SPECPARA + ") ";

    //var A11Val = $(".ddl-spec-name").first().val();
    //var A11 = $(".ddl-spec-name").first().find("option:selected").text();
    //if (A11Val == undefined || A11Val == null || A11Val == "0" || A11Val == "")
    //    A11 = "";

    ////var A12Val = $(".ddl-spec-value").first().val();
    ////var A12 = $(".ddl-spec-value").first().find("option:selected").text();
    ////if (A12Val == undefined || A12Val == null || A12Val == "0" || A12Val == "")
    ////    A12 = "";
    //var A12Val = $("#txtSpecificationValue").val();
    //var A12 = $("#txtSpecificationValue").val();

    //if (A12Val == undefined || A12Val == null || A12Val == "0" || A12Val == "")
    //    A12 = "";

    //var A13Val = $(".ddl-spec-uom").first().val();
    //var A13 = $(".ddl-spec-uom").first().find("option:selected").text();
    //if (A13Val == undefined || A13Val == null || A13Val == "0" || A13Val == "")
    //    A13 = "";

    var A2 = $("#txtBrandName").val();
    if (A2 == undefined || A2 == null || A2 == "0" || A2 == "") {
        A2 = "";
    }
    else {
        A2 = $("#txtBrandName option:selected").html();

    }

    var A3 = $("#txtItemCode").val();
    if (A3 == undefined || A3 == null || A3 == "0" || A3 == "") {
        A3 = "";
    }
    else {
        A3 = $("#txtItemCode option:selected").html();
    }



    //  $("#txtProductName").val(C1 + " " + C2 + " (" + A11 + " " + A12 + " " + A13 + ") " + A2 + " " + A3);
    $("#txtProductName").val(C1 + " " + C2 + SPECPARA + A2 + " " + A3);
}

function BindProductNameGeneration() {
    $("#txtItemCode, #txtBrandName, #txtUOMName").blur(function () {
        GenerateProductName();
    });
    $("#ddlProductSubChildGroup, #ddlProductSubGroup, .ddl-spec-name, .ddl-spec-value, .ddl-spec-uom").change(function () {
        GenerateProductName();
    });
}
/*  ADD PRODUCT CODE */
function ShowAddProductToSeller() {

    var ddlProductMainGroup = $(".category-1");
    var ddlProductSubGroup = $(".category-2");
    var ddlProductSubChildGroup = $(".category-3");
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
    ResetSellerAddProduct();
    var popup = $("#AddNewVendor").modal();
    $("#AddNewVendor").css("z-index", 1045);
}
/*  ADD PRODUCT Category   */
function ShowAddProductCategory() {
    var ddlProductMainGroup = $("#ddlProductMainGroup");
    if (ddlProductMainGroup.val() == "" || ddlProductMainGroup.val() == "0") {
        ShowModel("Alert", "Please Select Parent Class First");
        ddlProductMainGroup.focus();
        return false;
    }
    var popup = $("#AddCategory").modal();
    $("#AddCategory").css("z-index", 1045);
}

function ShowAddProductIcon(sender) {
    var category3Dropdown = $(sender);
    $(".category-3-txt").val(category3Dropdown.find("option:selected").text());
    if ($(sender).val() == undefined || $(sender).val() == "0") {
        //HideProductIcon();
    }
    else {
        $(".add-product-icon").show();
    }
}

function HideProductIcon() {
    $(".add-product-icon").hide();
}


//function CancelSellerAddProduct() {
//    ResetSellerAddProduct();
//    $(".creative_tabs_container > div").show();
//    $(".creative_tabs_container > .add-product").hide();
//    return false;
//}
function SaveNewProduct() {

    var hdnProductId = $("#hdnProductId");
    var txtProductName = $("#txtProductNameNew");
    var txtProductCode = $("#txtProductCodeNew");
    var txtSpecificationName = $("#txtSpecificationNameNew");
    var txtSpecificationValue = $("#txtSpecificationValueNew");
    var ddlProductMainGroup = $(".category-1");
    var ddlProductSubGroup = $(".category-2");
    var ddlProductSubChildGroup = $(".category-3");
    var txtProductShortDesc = $("#txtProductShortDesc");
    var txtProductFullDesc = $("#txtProductFullDesc");
    var ddlProductType = $("#ddlProductType");
    var ddlAssemblyType = $("#ddlAssemblyType");
    var ddlUOM = $("#ddlUOMNew");
    var txtPurchasePrice = $("#txtPurchasePrice");
    var txtSalePrice = $("#txtSalePrice");
    var txtLocalTaxRate = $("#txtLocalTaxRate");
    var txtCentralTaxRate = $("#txtCentralTaxRate");
    var txtOtherTaxRate = $("#txtOtherTaxRate");
    var chkIsSerializedProduct = $("#chkIsSerializedProduct");
    var txtReOrderQty = $("#txtReOrderQty");
    var txtMinOrderQty = $("#txtMinOrderQty");
    var chkStatus = $("#chkStatus");
    var txtEditor = $("#txtEditor");
    var txtProductHSNCode = $("#txtProductHSNCode");
    var txtBrandName = $("#txtBrandNameNew");
    var txtItemCode = $("#txtItemCodeNew");


    if (txtProductName.val().trim() == "") {
        ShowModel("Alert", "Please enter Product Name")
        txtProductName.focus();
        return false;
    }

    if (txtProductCode.val().length < 2) {
        ShowModel("Alert", "Please enter at least 2 character long Product Code");
        txtProductCode.focus();
        return false;
    }
    if (ddlProductType.val() == "" || ddlProductType.val() == "0") {
        ShowModel("Alert", "Please select Product Type");
        ddlProductType.focus();
        return false;
    }
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
    if (txtSpecificationName.val().trim() == "") {
        ShowModel("Alert", "Please enter Specification Name")
        txtSpecificationName.focus();
        return false;
    }

    if (txtSpecificationValue.val().trim() == "") {
        ShowModel("Alert", "Please enter Specification Value")
        txtSpecificationValue.focus();
        return false;
    }

    //if (ddlUOM.val() == "" || ddlUOM.val() == "0") {
    //    ShowModel("Alert", "Please select Product UOM");
    //    ddlUOM.focus();
    //    return false;
    //}


    var productStatus = true;

    var serializedProduct = true;
    if (chkIsSerializedProduct.prop("checked") == true) { serializedProduct = true; }
    else { serializedProduct = false; }

    var purchasePrice = "0";
    var salePrice = "0";
    var localTaxRate = "0";
    var centralTaxRate = "0";
    var otherTaxRate = "0";
    var reOrderQty = "0";
    var minOrderQty = "0";
    var hsncode = 0;

    var productViewModel = {
        Productid: hdnProductId.val(), ProductName: txtProductName.val().trim(), ProductCode: txtProductCode.val().trim(),
        ProductShortDesc: '', ProductFullDesc: '', ProductTypeId: '',
        ProductMainGroupId: ddlProductMainGroup.val(),
        ProductSubGroupId: ddlProductSubGroup.val(),
        ProductSubChildGroupId: ddlProductSubChildGroup.val(),
        AssemblyType: '', UOMId: FinalProductUOM.val().trim(),
        PurchasePrice: purchasePrice, SalePrice: salePrice, LocalTaxRate: localTaxRate, CentralTaxRate: centralTaxRate, OtherTaxRate: otherTaxRate,
        IsSerializedProduct: serializedProduct, BrandName: txtBrandName.val().trim(), ReOrderQty: reOrderQty, MinOrderQty: minOrderQty, Product_Status: productStatus,
        HSN_Code: hsncode,
    };

    var productImageList = [];
    var productImage = {
        ProductImageDetailId: 0,
        ImageTitle: '',
        ImageAlt: '',
        ImageNavigateUrl: '',
        imageUrl: '',
        ImageSequence: 0
    };

    var productTechSpecificationList = [];
    var specification = {
        ProductTechSpecId: 0,
        ProductTechSpecName: $("#txtSpecificationNameNew").val(),
        ProductTechSpecValue: $("#txtSpecificationValueNew").val(),
        UomId: 0,
        UomName: '',
        BrandName: '',
        ItemCode: ''
    };
    productTechSpecificationList.push(specification)

    var requestData = { productViewModel: productViewModel, productImageDetails: productImageList, productTechSpecifications: productTechSpecificationList };
    $.ajax({
        url: "../SellerProduct/AddEditProduct",
        cache: false,
        type: "POST",
        dataType: "json",
        data: JSON.stringify(requestData),
        contentType: 'application/json',
        success: function (data) {
            $("#txtProductName").val(txtProductName.val());
            $("#txtItemCode").val(txtProductCode.val());
            $("#txtSpecificationName").val(txtSpecificationName.val());
            $("#txtSpecificationValue").val(txtSpecificationValue.val());
            $("#hdnUOMId").val(ddlUOM.val());
            $("#txtBrandName").val(txtBrandName.val());
            ShowModel("Success", data.message);
        },
        error: function (err) {
            ShowModel("Error", err)
        }
    });

}
/* END ADD PRODUCT CODE*/
/*Get Specification */


function ResetSellerAddProduct() {
    var form = $(".btn-add-producat").closest("form");
    form.find("#txtProductNameNew").val("");
    form.find("#txtProductCodeNew").val("");
    form.find("i#txtSpecificationNameNewnput").val("");
    form.find(".uom-list").val(0);
    return false;
}

function GetProductDetail(productId) {
    $.ajax({
        type: "GET",
        asnc: false,
        url: "../Product/GetProductDetail",
        data: { productId: productId },
        dataType: "json",
        success: function (data) {

            $("#txtProductName").val(data.ProductName);
            $("#txtProductCode").val(data.ProductCode);
            $("#txtProductShortDesc").val(data.ProductShortDesc);
            $("#txtProductFullDesc").val(data.ProductFullDesc);
            $("#ddlProductType").val(data.ProductTypeId);
            $("#ddlProductMainGroup").val(data.ProductMainGroupId);
            BindProductSubGroupList(data.ProductSubGroupId);
            $("#ddlProductSubGroup").val(data.ProductSubGroupId);
            setTimeout(
                function () {
                    BindProductSubChildGroupList(data.ProductSubChildGroupId);
                }, 1000);

            $("#ddlProductSubChildGroup").val(data.ProductSubChildGroupId);
            $("#ddlAssemblyType").val(data.AssemblyType);
            $("#hdnUOMId").val(data.UOMId);
            $("#txtPurchasePrice").val(data.PurchasePrice);
            $("#txtSalePrice").val(data.SalePrice);
            $("#txtLocalTaxRate").val(data.LocalTaxRate);
            $("#txtCentralTaxRate").val(data.CentralTaxRate);
            $("#txtOtherTaxRate").val(data.OtherTaxRate);
            $("#txtBrandName").val(data.BrandName);
            if (data.IsSerializedProduct) {
                $("#chkIsSerializedProduct").attr("checked", true);
            }
            else {
                $("#chkIsSerializedProduct").attr("checked", false);
            }
            $("#txtMinOrderQty").val(data.MinOrderQty);
            $("#txtReOrderQty").val(data.ReOrderQty);

            if (data.Product_Status) {
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
    var hdnProductId = $("#hdnProductId");
    var txtProductName = $("#txtProductName");
    var txtProductCode = $("#txtProductCode");
    var txtProductShortDesc = $("#txtProductShortDesc");
    var txtProductFullDesc = $("#txtProductFullDesc");
    var ddlProductType = $("#ddlProductType");
    var ddlProductMainGroup = $("#ddlProductMainGroup");
    var ddlProductSubGroup = $("#ddlProductSubGroup");
    var ddlProductSubChildGroup = $("#ddlProductSubChildGroup");
    var ddlAssemblyType = $("#ddlAssemblyType");
    var ddlUOM = $("#ddlUOM");
    var txtPurchasePrice = $("#txtPurchasePrice");
    var txtSalePrice = $("#txtSalePrice");
    var txtLocalTaxRate = $("#txtLocalTaxRate");
    var txtCentralTaxRate = $("#txtCentralTaxRate");
    var txtOtherTaxRate = $("#txtOtherTaxRate");
    var chkIsSerializedProduct = $("#chkIsSerializedProduct");
    var txtBrandName = $("#txtBrandName");
    var txtReOrderQty = $("#txtReOrderQty");
    var txtMinOrderQty = $("#txtMinOrderQty");
    var chkStatus = $("#chkStatus");
    var txtEditor = $("#txtEditor");
    var txtProductHSNCode = $("#txtProductHSNCode");
    if (txtProductName.val().trim() == "") {
        ShowModel("Alert", "Please enter Product Name")
        txtProductName.focus();
        return false;
    }

    if (txtProductCode.val().length < 2) {
        ShowModel("Alert", "Please enter at least 2 character long Product Code");
        txtProductCode.focus();
        return false;
    }
    if (ddlProductType.val() == "" || ddlProductType.val() == "0") {
        ShowModel("Alert", "Please select Product Type");
        ddlProductType.focus();
        return false;
    }
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

    var productStatus = true;
    if (chkStatus.prop("checked") == true) { productStatus = true; }
    else { productStatus = false; }
    var serializedProduct = true;
    if (chkIsSerializedProduct.prop("checked") == true) { serializedProduct = true; }
    else { serializedProduct = false; }

    var purchasePrice = txtPurchasePrice.val().trim() == "" ? "0" : txtPurchasePrice.val().trim();
    var salePrice = txtSalePrice.val().trim() == "" ? "0" : txtSalePrice.val().trim();
    var localTaxRate = txtLocalTaxRate.val().trim() == "" ? "0" : txtLocalTaxRate.val().trim();
    var centralTaxRate = txtCentralTaxRate.val().trim() == "" ? "0" : txtCentralTaxRate.val().trim();
    var otherTaxRate = txtOtherTaxRate.val().trim() == "" ? "0" : txtOtherTaxRate.val().trim();
    var reOrderQty = txtReOrderQty.val().trim() == "" ? "0" : txtReOrderQty.val().trim();
    var minOrderQty = txtMinOrderQty.val().trim() == "" ? "0" : txtMinOrderQty.val().trim();
    var hsncode = txtProductHSNCode.val();

    var productViewModel = {
        Productid: hdnProductId.val(), ProductName: txtProductName.val().trim(), ProductCode: txtProductCode.val().trim(),
        ProductShortDesc: txtProductShortDesc.val().trim(), ProductFullDesc: txtEditor.Editor("getText").trim(), ProductTypeId: ddlProductType.val(),
        ProductMainGroupId: ddlProductMainGroup.val(),
        ProductSubGroupId: ddlProductSubGroup.val(),
        ProductSubChildGroupId: ddlProductSubChildGroup.val(),
        AssemblyType: ddlAssemblyType.val().trim(), UOMId: ddlUOM.val().trim(),
        PurchasePrice: purchasePrice, SalePrice: salePrice, LocalTaxRate: localTaxRate, CentralTaxRate: centralTaxRate, OtherTaxRate: otherTaxRate,
        IsSerializedProduct: serializedProduct, BrandName: txtBrandName.val().trim(), ReOrderQty: reOrderQty, MinOrderQty: minOrderQty, Product_Status: productStatus,
        HSN_Code: hsncode
    };

    var productImageList = [];

    $('#tblLogoList tr').each(function (i, row) {
        var $row = $(row);
        var productId = $row.find("#hdnProductId").val();
        var imageTitle = $row.find("#hdnImageTitle").val();
        var imageAlt = $row.find("#hdnImageAlt").val();
        var imageNavigateUrl = $row.find("#hdnImageNavigateUrl").val();
        var imageSequence = $row.find("#hdnImageSequence").val();
        var imageUrl = $row.find("#hdnImageUrl").val();

        if (imageTitle != undefined) {
            var productImage = {
                ProductImageDetailId: 0,
                ImageTitle: imageTitle,
                ImageAlt: imageAlt,
                ImageNavigateUrl: imageNavigateUrl,
                imageUrl: imageUrl,
                ImageSequence: imageSequence
            };
            productImageList.push(productImage);
        }
    });

    var productTechSpecificationList = [];
    $('#tblSpecificationList tr').each(function (i, row) {
        var $row = $(row);
        var productTechSpecId = $row.find("#hdnProductTechSpecId").val();
        var productTechSpecName = $row.find("#hdnProductTechSpecName").val();
        var productTechSpecValue = $row.find("#hdnProductTechSpecValue").val();
        var uomId = $row.find("#hdnUomId").val();
        var uomName = $row.find("#hdnUomName").val();
        var brandName = $row.find("#hdnBrandName").val();
        var itemCode = $row.find("#hdnItemCode").val();
        if (productTechSpecName != undefined) {
            var specification = {
                ProductTechSpecId: productTechSpecId,
                ProductTechSpecName: productTechSpecName,
                ProductTechSpecValue: productTechSpecValue,
                UomId: uomId,
                UomName: uomName,
                BrandName: brandName,
                ItemCode: itemCode
            };
            productTechSpecificationList.push(specification)
        }
    });


    var requestData = { productViewModel: productViewModel, productImageDetails: productImageList, productTechSpecifications: productTechSpecificationList };
    $.ajax({
        url: "../Product/AddEditProduct",
        cache: false,
        type: "POST",
        dataType: "json",
        data: JSON.stringify(requestData),
        contentType: 'application/json',
        success: function (data) {
            if (data.status == "SUCCESS") {
                ShowModel("Alert", data.message);
                // ClearFields();
                setTimeout(
                    function () {
                        window.location.href = "../Product/AddEditProduct";
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
    $("#hdnProductId").val("0");
    $("#txtProductName").val("");
    $("#txtProductCode").val("");
    $("#txtProductShortDesc").val("");
    $("#txtProductFullDesc").val("");
    $("#ddlProductType").val("0");
    $("#ddlProductMainGroup").val("0");
    $("#ddlProductSubGroup").val("0");
    $("#ddlAssemblyType").val("0");
    $("#hdnUOMId").val("0");
    $("#txtUOMName").val("");
    $("#txtPurchasePrice").val("");
    $("#txtSalePrice").val("");
    $("#txtLocalTaxRate").val("");
    $("#txtCentralTaxRate").val("");
    $("#txtOtherTaxRate").val("");
    $("#txtBrandName").val("");
    $("#chkIsSerializedProduct").attr("checked", false);
    $("#txtMinOrderQty").val("");
    $("#txtReOrderQty").val("");
    $("#chkStatus").attr("checked", true);
}

function ShowImagePreview(input, fileupload, imageid) {
    var fname = input.value;
    var ext = fname.split(".");
    var x = ext.length;
    var extstr = ext[x - 1].toLowerCase();
    if (extstr == 'jpg' || extstr == 'jpeg' || extstr == 'png' || extstr == 'gif') {

    }
    else {
        alert("File doesnt match png, jpg or gif");
        input.focus();
        input.value = "";
        return false;
    }
    if (typeof (FileReader) != "undefined") {
        if (input.files && input.files[0]) {
            if (input.files[0].name.length < 1) {
            }
            else if (input.files[0].size > 50000000) {
                input.files[0].name.length = 0;
                alert("File is too big");
                input.value = "";
                return false;
            }
            else if (input.files[0].type != 'image/png' && input.files[0].type != 'image/jpg' && !input.files[0].type != 'image/gif' && input.files[0].type != 'image/jpeg') {
                input.files[0].name.length = 0;
                alert("File doesnt match png, jpg or gif");
                input.value = "";
                return false;
            }
            var reader = new FileReader();
            reader.onload = function (e) {
                $("#" + imageid).prop('src', e.target.result)
                    .width(70)
                    .height(70);

            };
            reader.readAsDataURL(input.files[0]);

            if ($("#" + fileupload).val() != '') {
                $("#btnRemoveImg").show();
            }
            else {
                $("#btnRemoveImg").hide();
            }
        }
    }
    else {

        alert("This browser does not support FileReader.");
        input.value = "";
        //return false;
    }
}

function SaveHomeSliderImage(sliderId) {
    if (parseInt(sliderId) <= 0) {
        ShowModel("Alert", "Home Slider Id not available")
        return false;
    }
    if (window.FormData !== undefined) {
        var uploadfile = document.getElementById('FileUpload1');
        var fileData = new FormData();
        if (uploadfile.value != '') {

            var fileUpload = $("#FileUpload1").get(0);
            var files = fileUpload.files;

            if (uploadfile.files[0].size > 50000000) {
                uploadfile.files[0].name.length = 0;
                ShowModel("Alert", "File is too big")
                uploadfile.value = "";
                return false;
            }

            for (var i = 0; i < files.length; i++) {
                fileData.append(files[i].name, files[i]);
            }
        }
        fileData.append("sliderId", sliderId);

    } else {

        ShowModel("Alert", "FormData is not supported.")
    }

    $.ajax({
        url: "../HomeSlider/UpdateHomeSliderImage",
        type: "POST",
        contentType: false, // Not to set any content header  
        processData: false, // Not to process data  
        data: fileData,
        error: function () {
            ShowModel("Alert", "An error occured")
            return;
        },
        success: function (result) {
            if (result.status == "SUCCESS") {
                document.getElementById('FileUpload1').value = "";
                document.getElementById('imgUserPic').src = "";
            }
            else {
                ShowModel("Alert", result.message);
            }
        }
    });
}

function ConfirmRemoveImage() {
    if (confirm("Do you want to remove selected Image?")) {
        $("#hdnRemoveImage").val(1);
        document.getElementById('FileUpload1').value = "";
        document.getElementById('imgUserPic').src = "";
        $("#btnRemoveImg").hide();
    }
}

function GetProductImageList(productImageList) {
    var hdnProductId = $("#hdnProductId");
    var requestData = { productImageList: productImageList, productId: hdnProductId.val() };
    $.ajax({
        url: "../Product/GetProductImageDetailList",
        data: JSON.stringify(requestData),
        dataType: "html",
        contentType: "application/json; charset=utf-8",
        type: "POST",
        error: function (err) {
            $("#divProductImageList").html("");
            $("#divProductImageList").html(err);
        },
        success: function (data) {
            $("#divProductImageList").html("");
            $("#divProductImageList").html(data);
        }
    });
}

function GetProductTechSpecificationList(productTechSpecificationList) {
    var hdnProductId = $("#hdnProductId");
    var requestData = { productTechSpecificationList: productTechSpecificationList, productId: hdnProductId.val() };
    $.ajax({
        url: "../Product/GetProductTechSpecificationList",
        data: JSON.stringify(requestData),
        dataType: "html",
        contentType: "application/json; charset=utf-8",
        type: "POST",
        error: function (err) {
            $("#divTechnicalSpecification").html("");
            $("#divTechnicalSpecification").html(err);
        },
        success: function (data) {
            $("#divTechnicalSpecification").html("");
            $("#divTechnicalSpecification").html(data);
        }
    });
}

function AddProductTechSpecification(action) {
    var txtNameofSpecification = $("#txtNameofSpecification");
    var txtSpecificationvalue = $("#txtSpecificationvalue");
    var ddlUOM = $("#hdnUOMId");
    var txtSpecifiBrandName = $("#txtSpecifiBrandName");
    var txtItemCode = $("#txtItemCode");
    //if(txtNameofSpecification.val()=="")
    //{
    //    ShowModel("Alert", "Please Enter Specification Name");
    //    txtNameofSpecification.focus();
    //}
    //if (txtSpecificationvalue.val() == "" || txtSpecificationvalue.val()=="0") {
    //    ShowModel("Alert", "Please Enter Specification Value");
    //    txtSpecificationvalue.focus();
    //}
    //if (ddlUOM.val() == "0") {
    //    ShowModel("Alert", "Please Select UOM");
    //    ddlUOM.focus();
    //}
    var productTechSpecificationList = [];
    $('#tblSpecificationList tr').each(function (i, row) {
        var $row = $(row);
        var productTechSpecId = $row.find("#hdnProductTechSpecId").val();
        var productTechSpecName = $row.find("#hdnProductTechSpecName").val();
        var productTechSpecValue = $row.find("#hdnProductTechSpecValue").val();
        var uomId = $row.find("#hdnUomId").val();
        var uomName = $row.find("#txtUOMName").val();
        var brandName = $row.find("#hdnBrandName").val();
        var itemCode = $row.find("#hdnItemCode").val();
        if (productTechSpecName != undefined) {
            var specification = {
                ProductTechSpecId: productTechSpecId,
                ProductTechSpecName: productTechSpecName,
                ProductTechSpecValue: productTechSpecValue,
                UomId: uomId,
                UomName: uomName,
                BrandName: brandName,
                ItemCode: itemCode
            };
            productTechSpecificationList.push(specification)
        }
    });
    if (action == 1) {
        var specification = {
            ProductTechSpecId: 0,
            ProductTechSpecName: txtNameofSpecification.val(),
            ProductTechSpecValue: txtSpecificationvalue.val(),
            UomId: ddlUOM.val(),
            UomName: $("#ddlUOM option:selected").text(),
            BrandName: txtSpecifiBrandName.val(),
            ItemCode: txtItemCode.val()
        };
        productTechSpecificationList.push(specification)
    }
    GetProductTechSpecificationList(productTechSpecificationList);
    ClearTechSpecification();
}
function ClearTechSpecification() {
    $("#txtNameofSpecification").val("");
    $("#txtSpecificationvalue").val("");
    $("#ddlUOM").val(0);
    $("#txtSpecifiBrandName").val("");
    $("#txtItemCode").val("");
}
function SaveProductImage() {
    if (window.FormData !== undefined) {
        var uploadfile = document.getElementById('FileUpload2');
        var fileData = new FormData();
        if (uploadfile.value != '') {
            var fileUpload = $("#FileUpload2").get(0);
            var files = fileUpload.files;

            if (uploadfile.files[0].size > 50000000) {
                uploadfile.files[0].name.length = 0;
                ShowModel("Alert", "File is too big")
                uploadfile.value = "";
                return "";
            }

            for (var i = 0; i < files.length; i++) {
                fileData.append(files[i].name, files[i]);
            }
        }
        else {
            ShowModel("Alert", "Please Select File")
            return false;
        }
    } else {

        ShowModel("Alert", "FormData is not supported.")
        return "";
    }

    $.ajax({
        url: "../Product/SaveProductImage",
        type: "POST",
        asnc: false,
        contentType: false, // Not to set any content header  
        processData: false, // Not to process data  
        data: fileData,
        error: function () {
            ShowModel("Alert", "An error occured")
            return "";
        },
        success: function (result) {
            if (result.status == "SUCCESS") {

                var newFileName = result.message;

                var txtImageTitle = $("#txtImageTitle");
                var txtImageAlt = $("#txtImageAlt");
                var txtImageNavigateUrl = $("#txtImageNavigateUrl");
                var hdnProductId = $("#hdnProductId");
                var FileUpload1 = $("#FileUpload2");
                var txtImageSequence = $("#txtImageSequence");
                if (FileUpload1.val() == undefined || FileUpload1.val() == "") {
                    ShowModel("Alert", "Please select File To Upload")
                    return false;
                }
                var productImageList = [];

                $('#tblLogoList tr').each(function (i, row) {
                    var $row = $(row);
                    var productId = $row.find("#hdnProductId").val();
                    var imageTitle = $row.find("#hdnImageTitle").val();
                    var imageAlt = $row.find("#hdnImageAlt").val();
                    var imageNavigateUrl = $row.find("#hdnImageNavigateUrl").val();
                    var imageSequence = $row.find("#hdnImageSequence").val();
                    var imageUrl = $row.find("#hdnImageUrl").val();

                    if (imageTitle != undefined) {
                        var productImage = {
                            ProductImageDetailId: 0,
                            ImageTitle: imageTitle,
                            ImageAlt: imageAlt,
                            ImageNavigateUrl: imageNavigateUrl,
                            imageUrl: imageUrl,
                            ImageSequence: imageSequence
                        };
                        productImageList.push(productImage);
                    }
                });

                var productImage = {
                    ProductImageDetailId: 0,
                    ImageTitle: txtImageTitle.val(),
                    ImageAlt: txtImageAlt.val(),
                    ImageNavigateUrl: txtImageNavigateUrl.val(),
                    imageUrl: newFileName,
                    ImageSequence: txtImageSequence.val()
                };
                productImageList.push(productImage);
                GetProductImageList(productImageList);
                ClearText();
            }
            else {
                ShowModel("Alert", result.message);
            }
        }
    });
}

function ClearText() {
    $("#txtImageTitle").val("");
    $("#txtImageAlt").val("");
    $("#txtImageNavigateUrl").val("");
    $("#FileUpload2").val("");
    $("#txtImageSequence").val("");
    document.getElementById('FileUpload2').value = "";
    document.getElementById('imgUserPic2').src = "";
    $("#btnRemoveImg").hide();
}
function RemoveProductImageRow(obj) {
    if (confirm("Do you want to remove selected Product Image?")) {
        var row = $(obj).closest("tr");
        row.remove();
    }
}

function RemoveProductTechSpecificationRow(obj) {
    if (confirm("Do you want to remove selected Product Tech Specification ?")) {
        var row = $(obj).closest("tr");
        row.remove();
    }
}
function GetTechSpecificationList(productTechSpecificationList, id) {
    var hdnProductId = id;
    var requestData = { productTechSpecificationList: productTechSpecificationList, productId: hdnProductId };
    $.ajax({
        url: "../Product/GetTechSpecificationList",
        data: JSON.stringify(requestData),
        dataType: "html",
        contentType: "application/json; charset=utf-8",
        type: "POST",
        error: function (err) {
            $("#divSpecificationList").html("");
            $("#divSpecificationList").html(err);
        },
        success: function (data) {
            $("#divSpecificationList").html("");
            $("#divSpecificationList").html(data);
        }
    });
}
function OpenModel(productId) {
    var productTechSpecificationList = [];
    $("#SearchSpecificationModel").modal();
    GetTechSpecificationList(productTechSpecificationList, productId)
}



function CreateNewProductBrand() {

    var addedbrand = [];
    var ParentClass = $("#ddlProductMainGroup").val();
    var Category = $("#ddlProductSubGroup").val();
    var SubCategory = $("#ddlProductSubChildGroup").val();

    var Brand = $("#txtbrandName").val();

    var errorMsg = "";
    if (Brand.trim() == "")
        errorMsg = "Please enter Brand name";


    if (SubCategory.trim() == "")
        errorMsg = "Please Select Sub Category";

    if (errorMsg != "") {
        $(".brand-message").removeClass("alert-success").addClass("alert-danger").show();
        $(".brand-message").text(errorMsg);
        setTimeout(function () { $(".brand-message").hide() }, 100000);
        return;
    }

    addedbrand.push({ Id: "-1", ParentClass: ParentClass, Category: Category, SubCategory: SubCategory, Brand: Brand });

    $(".ddl-spec-brand").each(function () {
        // Add to name ddl if name not existing already
        if (($(this).find("option[value='" + Brand + "']")).length == 0)
            $(this).find("option").first().after($("<option></option>").val("-1").html(Brand))

    });
    $("#txtbrandName").val("");
    $("#txtBrandName").val("-1");

    $('#AddNewBrandModal').modal('hide');
    GenerateProductName();
    ShowModel("Alert", "Brand Created Sucessfully");

}


function CreateNewProductItem() {

    var addedProductItem = [];
    var ParentClass = $("#ddlProductMainGroup").val();
    var Category = $("#ddlProductSubGroup").val();
    var SubCategory = $("#ddlProductSubChildGroup").val();


    var ProductItem = $("#txtProductItem").val();

    var errorMsg = "";
    if (ProductItem.trim() == "")
        errorMsg = "Please enter Item name";


    if (SubCategory.trim() == "")
        errorMsg = "Please Select Sub Category";

    if (errorMsg != "") {
        $(".Item-message").removeClass("alert-success").addClass("alert-danger").show();
        $(".Item-message").text(errorMsg);
        setTimeout(function () { $(".Item-message").hide() }, 100000);
        return;
    }

    addedProductItem.push({ Id: "-1", ParentClass: ParentClass, Category: Category, SubCategory: SubCategory, ProductItem: ProductItem });

    $(".ddl-spec-itemCode").each(function () {
        // Add to name ddl if name not existing already
        if (($(this).find("option[value='" + ProductItem + "']")).length == 0)
            $(this).find("option").first().after($("<option></option>").val("-1").html(ProductItem))
    });
    $("#txtProductItem").val("");
    $("#txtItemCode").val("-1");
    GenerateProductName();
    $('#AddNewProductItemModal').modal('hide');

    ShowModel("Alert", "Commercial(Grade/CODE) Created Sucessfully");

}

function GetBuyerProductTechSpecification(buyerProductId) {
    var requestData = { buyerProductId: buyerProductId };
    $.ajax({
        url: "../BuyerProduct/GetBuyerProductTechSpecification",
        cache: false,
        data: JSON.stringify(requestData),
        dataType: "html",
        contentType: "application/json; charset=utf-8",
        type: "POST",
        error: function (err) {
            $(".spec-row").html("");
            $(".spec-row").html(err);
        },
        success: function (data) {
            $(".spec-row").html("");
            $(".spec-row").html(data);
        }
    });
}
function BindItemList1(parentClassId, categoryId, subCategoryId) {
    $("#txtItemCode option").remove();
    var data = { ProductMainGroupId: parentClassId, ProductSubGroupId: categoryId, ProductSubChildGroupId: subCategoryId };
    $.ajax({
        type: "GET",
        url: "../Product/GetItemList",
        data: data,
        dataType: "json",
        asnc: false,
        success: function (data) {

            $(".Item-list").append($("<option></option>").val(0).html("-Select Item-"));
            $.each(data, function (i, item) {
                $(".Item-list").append($("<option></option>").val(item.ProductCodeID).html(item.ProductCodeValue));
            });
        },
        error: function (Result) {

            $(".Item-list").append($("<option></option>").val(0).html("-Select Item-"));
        }
    });
}

function BindBrandList1(parentClassId, categoryId, subCategoryId) {
    $("#txtBrandName option").remove();
    var ParentClass = $("#ddlProductMainGroup").val();
    var Category = $("#ddlProductSubGroup").val();
    var SubCategory = $("#ddlProductSubChildGroup").val();
    var data = { ProductMainGroupId: parentClassId, ProductSubGroupId: categoryId, ProductSubChildGroupId: subCategoryId };

    $.ajax({
        type: "GET",
        url: "../Product/GetBrandList",
        data: data,
        dataType: "json",
        asnc: false,
        success: function (data) {

            $(".Brand-list").append($("<option></option>").val(0).html("-Select Brand-"));
            $.each(data, function (i, item) {
                $(".Brand-list").append($("<option></option>").val(item.BrandID).html(item.BrandName));
            });
        },
        error: function (Result) {

            $(".Brand-list").append($("<option></option>").val(0).html("-Select Brand-"));
        }
    });
}

function EditRow(obj) {
    var row = $(obj).closest("tr");
    var buyerProductDetailId = $(row).find("#hdnBuyerProductDetailId").val();
    var productName = $(row).find("#hdnProductName").val();
    //var productName = $(row).find("#hdnProductName").val();
    var parentClassId = $(row).find("#hdnParentClassId").val();
    var categoryId = $(row).find("#hdnCategoryId").val();
    var subCategoryId = $(row).find("#hdnSubCategoryId").val();
    var specValue = $(row).find("#hdnSpecValue").val();
    var uomName = $(row).find("#hdnUomName").val();
    var brandName = $(row).find("#hdnBrandName").val();

    $("#txtProductName").val(productName);
    $("#ddlProductMainGroup").val(parentClassId);
    BindProductSubGroupList1(parentClassId);
    $("#ddlProductSubGroup").val(categoryId)
    BindProductSubChildGroupListNew1(parentClassId, categoryId, subCategoryId);
    
    $(".dl-spec-name").val(specValue);
    BindBrandList1();
    $("#txtBrandName").val(brandName);
    BindItemList1(parentClassId, categoryId, subCategoryId)
    $("#ddlProductUOM").val(uomName);
    $(".spec-row").html("");
    GetBuyerProductTechSpecification(buyerProductDetailId);
    BindSpecNames1(parentClassId, categoryId, subCategoryId);

    //$("#hdnMSMEId").val(msmeId);
    //$("#txtMSMETitle").val(msmeTitle);
    //$("#ddlMSMEContentArea").val(msmeContentArea);
    //$("#txtEditor").Editor("setText", msmeDescription);
   

    //if (msmeStatus == 'true') {
    //    $("#chkstatus").attr("checked", true);
    //}
    //else {
    //    $("#chkstatus").attr("checked", false);
    //}

    $("#btnSave").hide();
    $("#btnUpdate").show();
    //$("html, body").animate({ scrollTop: 0 }, 600);
    return false;
}
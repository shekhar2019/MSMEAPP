
$(document).ready(function () {
    

    BindProductMainGroupList();
    BindUOMList();
  
    $("#ddlProductSubGroup").append($("<option></option>").val(0).html("-Select Category-"));
    $("#ddlProductSubChildGroup").append($("<option></option>").val(0).html("-Select Sub Category-"));
    var hdnAccessMode = $("#hdnAccessMode");
    var hdnProductId = $("#hdnProductId");
    
   
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




function BindUOMList() {

    $.ajax({
        type: "GET",
        url: "../Product/GetUOMList",
        data: "{}",
        dataType: "json",
        asnc: false,
        success: function (data) {
            $("#ddlProductUOM").append($("<option></option>").val(0).html("-Select UOM-"));
            $("#ddlUOM_SPC").append($("<option></option>").val(0).html("-Select UOM-"));

            var filteredData = data.filter(x => x.IsSUM == false);


            var filtered_SUM_Data = data.filter(x => !x.IsSUM == false);
            //TUM
            $.each(filteredData, function (i, item) {
                $("#ddlProductUOM").append($("<option></option>").val(item.UOMId).html(item.UOMName));
                $("#ddlUOM_SPC").append($("<option></option>").val(item.UOMId).html(item.UOMName));
            });
            //SUM
            $.each(filtered_SUM_Data, function (i, item) {
                $("#ddlProductUOM").append($("<option></option>").val(item.UOMId).html(item.UOMName));
                $("#ddlUOM_SPC").append($("<option></option>").val(item.UOMId).html(item.UOMName));
            });
        },
        error: function (Result) {
            $("#ddlProductUOM").append($("<option></option>").val(0).html("-Select UOM-"));
            $("#ddlUOM_SPC").append($("<option></option>").val(0).html("-Select UOM-"));

        }
    });
}

function BindSpecNames() {


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
                    $("#ddlSpecification").append($("<option></option>").val(item.ProductTechSpecId).html(item.ProductTechSpecName));
                });
            },
            error: function (Result) {
                $("#ddlSpecification").append($("<option></option>").val(0).html("-Select Specification Name"));
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
/* END ADD PRODUCT CODE*/



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
    $("#ddlUOM").val("0");
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

function BindSpecificationNamesForUOM_EDIT() {
    $(".ddl-spec-name").val("0").html("");

    var productMainGroupId = $("#ddlProductMainGroup option:selected").val();
    var productSubGroupId = $("#ddlProductSubGroup option:selected").val();
    var productChildSubGroupId = $("#ddlProductSubChildGroup option:selected").val();
    if (productChildSubGroupId == undefined || productChildSubGroupId == "0") {
        return;
    }

    if (productMainGroupId != undefined && productSubGroupId != undefined && productChildSubGroupId != undefined
        && productMainGroupId != "0" && productSubGroupId != "0" && productChildSubGroupId != "0") {

        var data = { productMainGroupId: productMainGroupId, productSubGroupId: productSubGroupId, productChildSubGroupId: productChildSubGroupId };

        ////Set UOM list if already exixts
       // Set_T_UOMList("../Product/GetUOMId", data);

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

        // set UOM for Non Specification
        $.ajax({
            type: "GET",
            url: "../Product/GetUOMId",
            data: data,
            asnc: false,
            dataType: "json",
            success: function (data) {
                //SetUOMDefault(data);
                if (data > 0) {
                    $("#ddlProductUOM").val(data);
                }
                else {
                    $("#ddlProductUOM").val(0);
                }
            },
            error: function (Result) {
                ShowModel("Alert", "Problem in Request");
            }
        });
    }
}


function UpdateUOM_SPC() {
    

    var ddlProductMainGroup = $("#ddlProductMainGroup");
    var ddlProductSubGroup = $("#ddlProductSubGroup");
    var ddlProductSubChildGroup = $("#ddlProductSubChildGroup");




    var ddlSpecification = $("#ddlSpecification");

    var ddlUOM = $("#ddlProductUOM");

    var ddlUOM_SPC = $("#ddlUOM_SPC");


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






    var requestData = {
        ParentClassId: ddlProductMainGroup.find("option:selected").val(),
        CategoryId: ddlProductSubGroup.find("option:selected").val(),
        SubCategoryId: ddlProductSubChildGroup.find("option:selected").val(),
        ProductTechSpecId: ddlSpecification.find("option:selected").val(),
        UomId: ddlUOM.find("option:selected").val(),
        UOM_sps_Id: ddlUOM_SPC.find("option:selected").val()
    };

    $.ajax({
        url: "../Product/AddEditUOM_SPC",
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
                        window.location.href = "../Product/AddEditUOM_SPC";
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

function SetUOM_SUM_SPC(sender) {

    var specName = sender.value;

    var ddlUOM = $(sender).closest(".spec-row").find(".ddl-spec-uom");

    var productMainGroupId = $("#ddlProductMainGroup option:selected").val();
    var productSubGroupId = $("#ddlProductSubGroup option:selected").val();
    var productChildSubGroupId = $("#ddlProductSubChildGroup option:selected").val();



    if (productMainGroupId != undefined && productSubGroupId != undefined && productMainGroupId != "0" && productSubGroupId != "0" && specName != "0") {
        var requestData = { productMainGroupId: productMainGroupId, productSubGroupId: productSubGroupId, productChildSubGroupId: productChildSubGroupId, ProductTechSpecId: specName };

        $.ajax({
            type: "GET",
            url: "../Product/GetUOMListforSpecification",
            data: requestData,
            dataType: "json",
            asnc: false,
            success: function (data) {

                if (ddlUOM != undefined && data > 0) {
                    $(sender).closest(".spec-row").find(".ddl-spec-uom").val(data);
                }

                else {
                    $(sender).closest(".spec-row").find(".ddl-spec-uom").val(0);
                }
            },
            error: function (Result) {
                ShowModel("Alert", "Problem in Request");
            }
        });

    }
}






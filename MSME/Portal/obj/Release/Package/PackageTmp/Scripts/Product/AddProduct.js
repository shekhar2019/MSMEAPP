
$(document).ready(function () {
    BindProductTypeList();
    BindProductMainGroupList();
    BindUOMList();
    $("#ddlProductSubGroup").append($("<option></option>").val(0).html("-Select Category-"));
    $("#ddlProductSubChildGroup").append($("<option></option>").val(0).html("-Select Sub Category-"));
    var hdnAccessMode = $("#hdnAccessMode");
    var hdnProductId = $("#hdnProductId");
    if (hdnProductId.val() != "" && hdnProductId.val() != "0" && hdnAccessMode.val() != "" && hdnAccessMode.val() != "0")
    {
        setTimeout(
        function () {
            GetProductDetail(hdnProductId.val());
        }, 3000);
        
        
        if (hdnAccessMode.val() == "3")
        {
            $("#btnSave").hide();
            $("#btnUpdate").hide();
            $("#btnReset").hide();
            $("input").attr('readOnly', true);
            $("select").attr('disabled', true);
        }
        else
        {
            $("#btnSave").hide();
            $("#btnUpdate").show();
            $("#btnReset").show();
        }
    }
    else
    {
        $("#btnSave").show();
        $("#btnUpdate").hide();
        $("#btnReset").show();
    }
    $("#txtProductName").focus();
    var producImageList = [];
    GetProductImageList(producImageList);
    var productTechSpecificationList = [];
    GetProductTechSpecificationList(productTechSpecificationList);
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

function BindProductTypeList()
{
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
                else
                {
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
            $("#ddlProductMainGroup").append($("<option></option>").val(0).html("-Select Parent Class-"));
            $.each(data, function (i, item) {
                $("#ddlProductMainGroup").append($("<option></option>").val(item.ProductMainGroupId).html(item.ProductMainGroupName));
            });
        },
        error: function (Result) {
            $("#ddlProductMainGroup").append($("<option></option>").val(0).html("-Select Parent Class-"));
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
                $("#ddlProductSubGroup").append($("<option></option>").val(0).html("-Select Category-"));
                $.each(data, function (i, item) {
                    $("#ddlProductSubGroup").append($("<option></option>").val(item.ProductSubGroupId).html(item.ProductSubGroupName));
                });
                $("#ddlProductSubGroup").val(productSubGroupId);
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

function BindProductSubChildGroupList(productSubChildGroupId) {
    var productMainGroupId = $("#ddlProductMainGroup option:selected").val();
    var productSubGroupId = $("#ddlProductSubGroup option:selected").val();

    $("#ddlProductSubChildGroup").val(0);
    $("#ddlProductSubChildGroup").html("");
    if (productMainGroupId != undefined && productMainGroupId != "" || productSubGroupId != undefined && productSubGroupId != "" && productSubGroupId != "0")
    {
        var data = { productMainGroupId: productMainGroupId, productSubGroupId: productSubGroupId };
        $.ajax({
            type: "GET",
            url: "../Product/GetProductSubChildGroupList",
            data: data,
            asnc: false,
            dataType: "json",
            success: function (data) {
                $("#ddlProductSubChildGroup").append($("<option></option>").val(0).html("-Select Sub Category-"));
                $.each(data, function (i, item) {
                    $("#ddlProductSubChildGroup").append($("<option></option>").val(item.ProductSubGroupId).html(item.ProductSubGroupName));
                });
                $("#ddlProductSubChildGroup").val(productSubGroupId);
            },
            error: function (Result) {
                $("#ddlProductSubChildGroup").append($("<option></option>").val(0).html("-Select Sub Category-"));
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
            $("#ddlUOM").append($("<option></option>").val(0).html("-Select UOM-"));
            $.each(data, function (i, item) {
                $("#ddlUOM").append($("<option></option>").val(item.UOMId).html(item.UOMName));
            });
        },
        error: function (Result) {
            $("#ddlUOM").append($("<option></option>").val(0).html("-Select UOM-"));
        }
    });
}

function GetProductDetail(productId) {
    $.ajax({
        type: "GET",
        asnc:false,
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
            $("#ddlUOM").val(data.UOMId);
            $("#txtPurchasePrice").val(data.PurchasePrice);
            $("#txtSalePrice").val(data.SalePrice);
            $("#txtLocalTaxRate").val(data.LocalTaxRate);
            $("#txtCentralTaxRate").val(data.CentralTaxRate);
            $("#txtOtherTaxRate").val(data.OtherTaxRate);
            $("#txtBrandName").val(data.BrandName);
            if (data.IsSerializedProduct)
            {
                $("#chkIsSerializedProduct").attr("checked", true);
            }
            else
            {
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
function SaveData()
{
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
    if (txtProductName.val().trim() == "")
    {
        ShowModel("Alert","Please enter Product Name")
        txtProductName.focus();
        return false;
    }
    //if (txtProductCode.val().trim() =="") {
    //    ShowModel("Alert", "Please enter Product Code")
    //    txtProductCode.focus();
    //    return false;
    //}
    if (txtProductCode.val().length <2) {
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
    //if (ddlAssemblyType.val() == "" || ddlAssemblyType.val() == "0") {
    //    ShowModel("Alert", "Please select Assembly Type")
    //    ddlAssemblyType.focus();
    //    return false;
    //}
    //if (ddlUOM.val() == "" || ddlUOM.val() == "0") {
    //    ShowModel("Alert", "Please select Unit of Measurement (UOM)")
    //    ddlUOM.focus();
    //    return false;
    //}
    //if (txtReOrderQty.val().trim() == "" || txtReOrderQty.val().trim() == "0") {
    //    ShowModel("Alert", "Please enter Reorder Qty.")
    //    txtReOrderQty.focus();
    //    return false;
    //}
    var productStatus = true;
    if (chkStatus.prop("checked") == true)
    { productStatus = true; }
    else
    { productStatus = false; }
    var serializedProduct = true;
    if (chkIsSerializedProduct.prop("checked") == true)
    { serializedProduct = true; }
    else
    { serializedProduct = false; }
        
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
        ProductSubChildGroupId:ddlProductSubChildGroup.val(),
        AssemblyType: ddlAssemblyType.val().trim(), UOMId: ddlUOM.val().trim(),
        PurchasePrice: purchasePrice, SalePrice: salePrice, LocalTaxRate: localTaxRate, CentralTaxRate: centralTaxRate, OtherTaxRate: otherTaxRate,
        IsSerializedProduct: serializedProduct, BrandName: txtBrandName.val().trim(), ReOrderQty: reOrderQty, MinOrderQty: minOrderQty, Product_Status: productStatus,
        HSN_Code:hsncode
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
            if (data.status=="SUCCESS")
            {
                ShowModel("Alert", data.message);
               // ClearFields();
                setTimeout(
                  function () {
                      window.location.href = "../Product/AddEditProduct";
                  }, 2000);
                $("#btnSave").show();
                $("#btnUpdate").hide();
            }
            else
            {
                ShowModel("Error", data.message)
            }
        },
        error: function (err) {
            ShowModel("Error", err)
        }
    });

}
function ShowModel(headerText,bodyText)
{
    $("#alertModel").modal();
    $("#modelHeader").html(headerText);
    $("#modelText").html(bodyText);

}
function ClearFields()
{
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

function ShowImagePreview(input,fileupload,imageid) {
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

            if ($("#"+fileupload).val() != '') {
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
    var requestData ={ productImageList:productImageList,productId:hdnProductId.val() };
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

function AddProductTechSpecification(action)
{
    var txtNameofSpecification = $("#txtNameofSpecification");
    var txtSpecificationvalue = $("#txtSpecificationvalue");
    var ddlUOM = $("#ddlUOM");
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
    if (ddlUOM.val() == "0") {
        ShowModel("Alert", "Please Select UOM");
        ddlUOM.focus();
    }
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
    if(action==1)
    {
        var specification = {
            ProductTechSpecId:0,
            ProductTechSpecName:txtNameofSpecification.val(),
            ProductTechSpecValue: txtSpecificationvalue.val(),
            UomId: ddlUOM.val(),
            UomName:$("#ddlUOM option:selected").text(),
            BrandName: txtSpecifiBrandName.val(),
            ItemCode: txtItemCode.val()
        };
        productTechSpecificationList.push(specification)
    }
    GetProductTechSpecificationList(productTechSpecificationList);
    ClearTechSpecification();
}
function ClearTechSpecification()
{
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
        if (uploadfile.value != '')
        {
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
                if (FileUpload1.val() == undefined || FileUpload1.val() == "")
                {
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
                            imageUrl:imageUrl,
                            ImageSequence: imageSequence
                        };
                        productImageList.push(productImage);
                    }
                });

                var productImage = {
                    ProductImageDetailId:0,
                    ImageTitle: txtImageTitle.val(),
                    ImageAlt: txtImageAlt.val(),
                    ImageNavigateUrl:txtImageNavigateUrl.val(),
                    imageUrl:newFileName,
                    ImageSequence:txtImageSequence.val()
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

function ClearText()
{
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

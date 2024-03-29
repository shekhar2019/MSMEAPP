﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Portal.Core.ViewModel;
using Portal.DAL;
using Portal.Common;
using System.Reflection;
using System.Data;

namespace Portal.Core
{
    public class ProductBL
    {
        DBInterface dbInterface;
        public ProductBL()
        {
            dbInterface = new DBInterface();
        }
        public ResponseOut AddEditProduct(ProductViewModel productViewModel, List<ProductImageDetailViewModel> productImageDetails, List<ProductTechSpecificationViewModel> productTechSpecifications)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                Product product = new Product
                {
                    Productid = productViewModel.Productid,
                    ProductName = productViewModel.ProductName,
                    ProductCode = productViewModel.ProductCode,
                    ProductShortDesc = productViewModel.ProductShortDesc,
                    ProductFullDesc = productViewModel.ProductFullDesc,
                    ProductTypeId = productViewModel.ProductTypeId,
                    ProductMainGroupId = productViewModel.ProductMainGroupId,
                    ProductSubGroupId = productViewModel.ProductSubGroupId,
                    ProductSubChildGroupId = productViewModel.ProductSubChildGroupId,
                    AssemblyType = productViewModel.AssemblyType,
                    UOMId = productViewModel.UOMId,
                    PurchasePrice = productViewModel.PurchasePrice,
                    SalePrice = productViewModel.SalePrice,
                    LocalTaxRate = productViewModel.LocalTaxRate,
                    CentralTaxRate = productViewModel.CentralTaxRate,
                    OtherTaxRate = productViewModel.OtherTaxRate,
                    IsSerializedProduct = productViewModel.IsSerializedProduct,
                    BrandName = productViewModel.BrandName,
                    ReOrderQty = productViewModel.ReOrderQty,
                    MinOrderQty = productViewModel.MinOrderQty,
                    CreatedBy = productViewModel.CreatedBy,
                    Status = productViewModel.Product_Status,
                    CompanyId = productViewModel.CompanyId,
                    HSN_Code = productViewModel.HSN_Code

                };
                List<ProductImageDetail> productImageDetailList = new List<ProductImageDetail>();

                if (productImageDetails != null && productImageDetails.Count > 0)
                {
                    foreach (ProductImageDetailViewModel item in productImageDetails)
                    {
                        productImageDetailList.Add(new ProductImageDetail
                        {
                            ProductImageDetailId = item.ProductImageDetailId,
                            ImageTitle = item.ImageTitle,
                            ImageAlt = item.ImageAlt,
                            ImageNavigateUrl = item.ImageNavigateUrl,
                            ImageUrl = item.ImageUrl,
                            ImageSequence = item.ImageSequence
                        });

                    }
                }

                List<ProductTechSpecification> productTechSpecification = new List<ProductTechSpecification>();
                if (productTechSpecifications != null && productTechSpecifications.Count > 0)
                {
                    foreach (ProductTechSpecificationViewModel item in productTechSpecifications)
                    {
                        productTechSpecification.Add(new ProductTechSpecification
                        {
                            ProductTechSpecId = item.ProductTechSpecId,
                            ProductTechSpecName = item.ProductTechSpecName,
                            ProductTechSpecValue = item.ProductTechSpecValue,
                            UomId = item.UomId,
                            BrandName = item.BrandName,
                            ItemCode = item.ItemCode
                        });
                    }
                }
                responseOut = dbInterface.AddEditProduct(product, productImageDetailList, productTechSpecification);
            }
            catch (Exception ex)
            {
                responseOut.status = ActionStatus.Fail;
                responseOut.message = ActionMessage.ApplicationException;
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return responseOut;
        }
        public List<ProductViewModel> GetBuyerSellerProductList(string productName, int companyid, string productCode, string productShortDesc,
            string productFullDesc, int productTypeId, int productMainGroupId, string assemblyType, string brandName)
        {
            List<ProductViewModel> products = new List<ProductViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtProducts = sqlDbInterface.GetBuyerSellerProductList(productName, companyid, productCode, productShortDesc, productFullDesc, productTypeId, productMainGroupId, assemblyType, brandName);
                if (dtProducts != null && dtProducts.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtProducts.Rows)
                    {
                        products.Add(new ProductViewModel
                        {
                            Productid = Convert.ToInt32(dr["ProductId"]),
                            ProductName = Convert.ToString(dr["ProductName"]),
                            ProductCode = Convert.ToString(dr["ProductCode"]),
                            ProductShortDesc = Convert.ToString(dr["ProductShortDesc"]),
                            ProductFullDesc = Convert.ToString(dr["ProductFullDesc"]),
                            ProductTypeId = Convert.ToInt16(dr["ProductTypeId"]),
                            ProductTypeName = Convert.ToString(dr["ProductTypeName"]),
                            ProductMainGroupId = Convert.ToInt16(dr["ProductMainGroupId"]),
                            ProductMainGroupName = Convert.ToString(dr["ProductMainGroupName"]),
                            ProductSubGroupId = Convert.ToInt16(dr["ProductSubGroupId"]),
                            ProductSubGroupName = Convert.ToString(dr["ProductSubGroupName"]),
                            ProductSubChildGroupId = Convert.ToInt32(dr["ProductSubChildGroupId"]),
                            ProductSubChildGroupName = Convert.ToString(dr["ProductSubChildGroupName"]),
                            AssemblyType = Convert.ToString(dr["AssemblyType"]),
                            UOMId = Convert.ToInt16(dr["UOMId"]),
                            UOMName = Convert.ToString(dr["UOMName"]),
                            PurchasePrice = Convert.ToDecimal(dr["PurchasePrice"]),
                            SalePrice = Convert.ToDecimal(dr["SalePrice"]),
                            LocalTaxRate = Convert.ToDecimal(dr["LocalTaxRate"]),
                            CentralTaxRate = Convert.ToDecimal(dr["CentralTaxRate"]),
                            OtherTaxRate = Convert.ToDecimal(dr["OtherTaxRate"]),
                            IsSerializedProduct = Convert.ToBoolean(dr["IsSerializedProduct"]),
                            BrandName = Convert.ToString(dr["BrandName"]),
                            ReOrderQty = Convert.ToDecimal(dr["ReOrderQty"]),
                            MinOrderQty = Convert.ToDecimal(dr["MinOrderQty"]),
                            Product_Status = Convert.ToBoolean(dr["Status"]),
                            ImageTitle = Convert.ToString(dr["ImageTitle"]),
                            ImageAlt = Convert.ToString(dr["ImageAlt"]),
                            ImageUrl = Convert.ToString(dr["ImageUrl"]),
                            ImageNavigateUrl = Convert.ToString(dr["ImageNavigateUrl"]),
                            HSN_Code = Convert.ToString(dr["HSN_Code"])

                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return products;
        }
        public List<ProductViewModel> GetProductList(string productName, int companyid, string productCode, string productShortDesc, string productFullDesc, int productTypeId, int productMainGroupId, string assemblyType, string brandName)
        {
            List<ProductViewModel> products = new List<ProductViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtProducts = sqlDbInterface.GetProductList(productName, companyid, productCode, productShortDesc, productFullDesc, productTypeId, productMainGroupId, assemblyType, brandName);
                if (dtProducts != null && dtProducts.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtProducts.Rows)
                    {
                        products.Add(new ProductViewModel
                        {
                            Productid = Convert.ToInt32(dr["ProductId"]),
                            ProductName = Convert.ToString(dr["ProductName"]),
                            ProductCode = Convert.ToString(dr["ProductCode"]),
                            ProductShortDesc = Convert.ToString(dr["ProductShortDesc"]),
                            ProductFullDesc = Convert.ToString(dr["ProductFullDesc"]),
                            ProductTypeId = Convert.ToInt16(dr["ProductTypeId"]),
                            ProductTypeName = Convert.ToString(dr["ProductTypeName"]),
                            ProductMainGroupId = Convert.ToInt16(dr["ProductMainGroupId"]),
                            ProductMainGroupName = Convert.ToString(dr["ProductMainGroupName"]),
                            ProductSubGroupId = Convert.ToInt16(dr["ProductSubGroupId"]),
                            ProductSubGroupName = Convert.ToString(dr["ProductSubGroupName"]),
                            ProductSubChildGroupId = Convert.ToInt32(dr["ProductSubChildGroupId"]),
                            ProductSubChildGroupName = Convert.ToString(dr["ProductSubChildGroupName"]),
                            AssemblyType = Convert.ToString(dr["AssemblyType"]),
                            UOMId = Convert.ToInt16(dr["UOMId"]),
                            UOMName = Convert.ToString(dr["UOMName"]),
                            PurchasePrice = Convert.ToDecimal(dr["PurchasePrice"]),
                            SalePrice = Convert.ToDecimal(dr["SalePrice"]),
                            LocalTaxRate = Convert.ToDecimal(dr["LocalTaxRate"]),
                            CentralTaxRate = Convert.ToDecimal(dr["CentralTaxRate"]),
                            OtherTaxRate = Convert.ToDecimal(dr["OtherTaxRate"]),
                            IsSerializedProduct = Convert.ToBoolean(dr["IsSerializedProduct"]),
                            BrandName = Convert.ToString(dr["BrandName"]),
                            ReOrderQty = Convert.ToDecimal(dr["ReOrderQty"]),
                            MinOrderQty = Convert.ToDecimal(dr["MinOrderQty"]),
                            Product_Status = Convert.ToBoolean(dr["Status"]),
                            ImageTitle = Convert.ToString(dr["ImageTitle"]),
                            ImageAlt = Convert.ToString(dr["ImageAlt"]),
                            ImageUrl = Convert.ToString(dr["ImageUrl"]),
                            ImageNavigateUrl = Convert.ToString(dr["ImageNavigateUrl"]),
                            HSN_Code = Convert.ToString(dr["HSN_Code"])

                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return products;
        }
        public List<ProductImageDetailViewModel> GetProductImageDetailList(int productId = 0)
        {
            List<ProductImageDetailViewModel> productImages = new List<ProductImageDetailViewModel>();
            try
            {
                List<ProductImageDetail> productImageList = dbInterface.GetProductImageList(productId);
                if (productImageList != null && productImageList.Count > 0)
                {
                    foreach (ProductImageDetail item in productImageList)
                    {
                        productImages.Add(
                            new ProductImageDetailViewModel
                            {
                                ProductImageDetailId = Convert.ToInt32(item.ProductImageDetailId),
                                ImageTitle = item.ImageTitle,
                                ImageAlt = item.ImageAlt,
                                ImageNavigateUrl = item.ImageNavigateUrl,
                                ImageUrl = item.ImageUrl,
                                ImageSequence = Convert.ToInt32(item.ImageSequence)
                            });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return productImages;
        }
        public ProductViewModel GetProductDetail(long productId = 0)
        {
            ProductViewModel product = new ProductViewModel();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtProducts = sqlDbInterface.GetProductDetail(productId);
                if (dtProducts != null && dtProducts.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtProducts.Rows)
                    {
                        product = new ProductViewModel
                        {
                            Productid = Convert.ToInt32(dr["ProductId"]),
                            ProductName = Convert.ToString(dr["ProductName"]),
                            ProductCode = Convert.ToString(dr["ProductCode"]),
                            ProductShortDesc = Convert.ToString(dr["ProductShortDesc"]),
                            ProductFullDesc = Convert.ToString(dr["ProductFullDesc"]),
                            ProductTypeId = Convert.ToInt16(dr["ProductTypeId"]),
                            ProductTypeName = Convert.ToString(dr["ProductTypeName"]),
                            ProductMainGroupId = Convert.ToInt16(dr["ProductMainGroupId"]),
                            ProductMainGroupName = Convert.ToString(dr["ProductMainGroupName"]),
                            ProductSubGroupId = Convert.ToInt16(dr["ProductSubGroupId"]),
                            ProductSubGroupName = Convert.ToString(dr["ProductSubGroupName"]),
                            ProductSubChildGroupId = Convert.ToInt32(dr["ProductSubChildGroupId"]),
                            ProductSubChildGroupName = Convert.ToString(dr["ProductSubChildGroupName"]),
                            AssemblyType = Convert.ToString(dr["AssemblyType"]),
                            UOMId = Convert.ToInt16(dr["UOMId"]),
                            UOMName = Convert.ToString(dr["UOMName"]),
                            PurchasePrice = Convert.ToDecimal(dr["PurchasePrice"]),
                            SalePrice = Convert.ToDecimal(dr["SalePrice"]),
                            LocalTaxRate = Convert.ToDecimal(dr["LocalTaxRate"]),
                            CentralTaxRate = Convert.ToDecimal(dr["CentralTaxRate"]),
                            OtherTaxRate = Convert.ToDecimal(dr["OtherTaxRate"]),
                            IsSerializedProduct = Convert.ToBoolean(dr["IsSerializedProduct"]),
                            BrandName = Convert.ToString(dr["BrandName"]),
                            ReOrderQty = Convert.ToDecimal(dr["ReOrderQty"]),
                            MinOrderQty = Convert.ToDecimal(dr["MinOrderQty"]),
                            Product_Status = Convert.ToBoolean(dr["Status"])
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return product;
        }

        public List<ProductViewModel> GetProductAutoCompleteList(string searchTerm, int companyId)
        {
            List<ProductViewModel> products = new List<ProductViewModel>();
            try
            {
                List<Product> productList = dbInterface.GetProductAutoCompleteList(searchTerm, companyId);
                if (productList != null && productList.Count > 0)
                {
                    foreach (Product product in productList)
                    {
                        products.Add(new ProductViewModel { Productid = product.Productid, ProductName = product.ProductName, ProductCode = product.ProductCode, ProductShortDesc = product.ProductShortDesc });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return products;
        }
        public List<ProductViewModel> GetProductAutoCompleteList(string searchTerm, int companyId, string assemblyType)
        {
            List<ProductViewModel> products = new List<ProductViewModel>();
            try
            {
                List<Product> productList = dbInterface.GetProductAutoCompleteList(searchTerm, companyId, assemblyType);
                if (productList != null && productList.Count > 0)
                {
                    foreach (Product product in productList)
                    {
                        products.Add(new ProductViewModel { Productid = product.Productid, ProductName = product.ProductName, ProductCode = product.ProductCode, ProductShortDesc = product.ProductShortDesc });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return products;
        }
        public List<ProductViewModel> GetSubAssemblyAutoCompleteList(string searchTerm, int companyId)
        {
            List<ProductViewModel> products = new List<ProductViewModel>();
            try
            {
                List<Product> productList = dbInterface.GetSubAssemblyAutoCompleteList(searchTerm, companyId);
                if (productList != null && productList.Count > 0)
                {
                    foreach (Product product in productList)
                    {
                        products.Add(new ProductViewModel { Productid = product.Productid, ProductName = product.ProductName, ProductCode = product.ProductCode, ProductShortDesc = product.ProductShortDesc });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return products;
        }

        public List<ProductViewModel> GetProductAutoCompleteList(string searchTerm, int productMainGroupId, int productSubGroupId, int productSubChildGroupId)
        {
            List<ProductViewModel> products = new List<ProductViewModel>();
            try
            {
                List<Product> productList = dbInterface.GetProductAutoCompleteList(searchTerm, productMainGroupId, productSubGroupId, productSubChildGroupId);
                if (productList != null && productList.Count > 0)
                {
                    foreach (Product product in productList)
                    {
                        products.Add(new ProductViewModel { Productid = product.Productid, ProductName = product.ProductName, ProductCode = product.ProductCode });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return products;
        }
        public List<ProductTechSpecificationViewModel> GetProductTechSpecificationList(int productId)
        {
            List<ProductTechSpecificationViewModel> productTechSpecifications = new List<ProductTechSpecificationViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtProductTechs = sqlDbInterface.GetProductTechSpecificationList(productId);
                if (dtProductTechs != null && dtProductTechs.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtProductTechs.Rows)
                    {
                        productTechSpecifications.Add(new ProductTechSpecificationViewModel
                        {
                            ProductTechSpecId = Convert.ToInt64(dr["ProductTechSpecId"]),
                            ProductTechSpecName = Convert.ToString(dr["ProductTechSpecName"]),
                            ProductTechSpecValue = Convert.ToString(dr["ProductTechSpecValue"]),
                            UomId = Convert.ToInt32(dr["UomId"]),
                            UomName = Convert.ToString(dr["UOMName"]),
                            BrandName = Convert.ToString(dr["BrandName"]),
                            ItemCode = Convert.ToString(dr["ItemCode"])
                        });
                    }

                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return productTechSpecifications;
        }
        public ResponseOut ImportProduct(ProductViewModel productViewModel)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                Product product = new Product
                {
                    Productid = productViewModel.Productid,
                    ProductName = productViewModel.ProductName,
                    ProductCode = productViewModel.ProductCode,
                    ProductShortDesc = productViewModel.ProductShortDesc,
                    ProductFullDesc = productViewModel.ProductFullDesc,
                    ProductTypeId = productViewModel.ProductTypeId,
                    ProductMainGroupId = productViewModel.ProductMainGroupId,
                    ProductSubGroupId = productViewModel.ProductSubGroupId,
                    AssemblyType = productViewModel.AssemblyType,
                    UOMId = productViewModel.UOMId,
                    PurchasePrice = productViewModel.PurchasePrice,
                    SalePrice = productViewModel.SalePrice,
                    LocalTaxRate = productViewModel.LocalTaxRate,
                    CentralTaxRate = productViewModel.CentralTaxRate,
                    OtherTaxRate = productViewModel.OtherTaxRate,
                    IsSerializedProduct = productViewModel.IsSerializedProduct,
                    BrandName = productViewModel.BrandName,
                    ReOrderQty = productViewModel.ReOrderQty,
                    MinOrderQty = productViewModel.MinOrderQty,
                    CreatedBy = productViewModel.CreatedBy,
                    Status = productViewModel.Product_Status,
                    CompanyId = productViewModel.CompanyId
                };
                //responseOut = dbInterface.AddEditProduct(product);
            }
            catch (Exception ex)
            {
                responseOut.status = ActionStatus.Fail;
                responseOut.message = ActionMessage.ApplicationException;
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return responseOut;
        }

        public List<ProductViewModel> GetProductsByGroupList(int productMainGroupId, int productSubGroupId, int productSubChildGroupId)
        {
            List<ProductViewModel> products = new List<ProductViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtProducts = sqlDbInterface.GetProductsByGroupList(productMainGroupId, productSubGroupId, productSubChildGroupId);
                if (dtProducts != null && dtProducts.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtProducts.Rows)
                    {
                        products.Add(new ProductViewModel
                        {
                            Productid = Convert.ToInt32(dr["ProductId"]),
                            ProductName = Convert.ToString(dr["ProductName"]),
                            ProductCode = Convert.ToString(dr["ProductCode"]),
                            ProductShortDesc = Convert.ToString(dr["ProductShortDesc"]),
                            ProductFullDesc = Convert.ToString(dr["ProductFullDesc"]),
                            ProductTypeId = Convert.ToInt16(dr["ProductTypeId"]),
                            ProductTypeName = Convert.ToString(dr["ProductTypeName"]),
                            ProductMainGroupId = Convert.ToInt16(dr["ProductMainGroupId"]),
                            ProductMainGroupName = Convert.ToString(dr["ProductMainGroupName"]),
                            ProductSubGroupId = Convert.ToInt16(dr["ProductSubGroupId"]),
                            ProductSubGroupName = Convert.ToString(dr["ProductSubGroupName"]),
                            ProductSubChildGroupId = Convert.ToInt32(dr["ProductSubChildGroupId"]),
                            ProductSubChildGroupName = Convert.ToString(dr["ProductSubChildGroupName"]),
                            UOMId = Convert.ToInt16(dr["UOMId"]),
                            UOMName = Convert.ToString(dr["UOMName"]),
                            PurchasePrice = Convert.ToDecimal(dr["PurchasePrice"]),
                            SalePrice = Convert.ToDecimal(dr["SalePrice"]),
                            BrandName = Convert.ToString(dr["BrandName"]),
                            ReOrderQty = Convert.ToDecimal(dr["ReOrderQty"]),
                            MinOrderQty = Convert.ToDecimal(dr["MinOrderQty"]),
                            Product_Status = Convert.ToBoolean(dr["Status"]),
                            ImageTitle = Convert.ToString(dr["ImageTitle"]),
                            ImageAlt = Convert.ToString(dr["ImageAlt"]),
                            ImageUrl = Convert.ToString(dr["ImageUrl"]),
                            ImageNavigateUrl = Convert.ToString(dr["ImageNavigateUrl"]),
                            HSN_Code = Convert.ToString(dr["HSN_Code"])

                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return products;
        }


        public ResponseOut UpdateSellerProductPrice(string sellerproductdetailsID, string price, string MinimumOrderQuantity)
        {
            ResponseOut responseOut = new ResponseOut();
            SQLDbInterface db = new SQLDbInterface();
            try
            {
                responseOut = db.UpdateSellerProductPrice(sellerproductdetailsID, price, MinimumOrderQuantity);
            }
            catch (Exception ex)
            {
                responseOut.status = ActionStatus.Fail;
                responseOut.message = ActionMessage.ApplicationException;
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return responseOut;
        }



        public ResponseOut UpdateBuyerProductQTY(string buyerproductdetailsID, string OrderQuantity)
        {
            ResponseOut responseOut = new ResponseOut();
            SQLDbInterface db = new SQLDbInterface();
            try
            {
                responseOut = db.UpdateBuyerProductQTY(buyerproductdetailsID, OrderQuantity);
            }
            catch (Exception ex)
            {
                responseOut.status = ActionStatus.Fail;
                responseOut.message = ActionMessage.ApplicationException;
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return responseOut;
        }

        public List<ProductViewModel> GetProductsByGroupListKnowledgeCenter(int productMainGroupId, int productSubGroupId, int productSubChildGroupId, string category)
        {
            List<ProductViewModel> products = new List<ProductViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtProducts = sqlDbInterface.GetProductsByGroupListKnowledgeCenter(productMainGroupId, productSubGroupId, productSubChildGroupId, category);
                if (dtProducts != null && dtProducts.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtProducts.Rows)
                    {
                        products.Add(new ProductViewModel
                        {
                            ProductFullDesc = Convert.ToString(dr["ProductId"]),
                            ProductName = Convert.ToString(dr["ProductName"])


                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return products;
        }


        public List<GrpahViewModel> GetProductsRateGroupbyKnowledgeCenter(string productID, string FromDate, string ToDate, string GraphType, string Year, string Fmonth, string Tmonth, string FYear, string TYear, string BSType)
        {
            GrpahViewModel gv = new GrpahViewModel();
            List<GrpahViewModel> RateGrpah = new List<GrpahViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
              
                DataTable dt = sqlDbInterface.GetProductsRateGroupbyKnowledgeCenter(productID, FromDate, ToDate, GraphType, Year, Fmonth, Tmonth, FYear, TYear, BSType);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        RateGrpah.Add(new GrpahViewModel
                        {
                            Text = Convert.ToString(dr["Text"]),
                            Value = Convert.ToString(dr["Value"])

                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return RateGrpah;
        }

        public int GetUOMId(long ParentClassId, long CategoryId, long SubCategoryId)
        {
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            int UOMId = 0;
            try
            {
                UOMId = sqlDbInterface.GetUOMId(ParentClassId, CategoryId, SubCategoryId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return UOMId;
        }

        public List<UOMViewModel> GetUOMListforSpecification(long ParentClassId, long CategoryId, long SubCategoryId, long ProductTechSpecId)
        {
            List<UOMViewModel> uo = new List<UOMViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtUOM = sqlDbInterface.GetUOMListforSpecification(ParentClassId, CategoryId, SubCategoryId, ProductTechSpecId);
                if (dtUOM != null && dtUOM.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtUOM.Rows)
                    {
                        uo.Add(new UOMViewModel
                        {
                            UOMId = Convert.ToInt32(dr["UOMId"]),
                            UOMName = Convert.ToString(dr["UOMName"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return uo;
        }

        public ResponseOut AddEditUOM_SPC(AddEditUOM_SPCViewModel UOMViewModel)
        {
            ResponseOut responseOut = new ResponseOut();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                AddEditUOM_SPC UOM_SPC = new AddEditUOM_SPC
                {
                    ParentClassId = UOMViewModel.ParentClassId,

                    CategoryId = UOMViewModel.CategoryId,
                    SubCategoryId = UOMViewModel.SubCategoryId,
                    ProductTechSpecId = UOMViewModel.ProductTechSpecId,
                    UomId = UOMViewModel.UomId,
                    UOM_sps_Id = UOMViewModel.UOM_sps_Id,

                };
                responseOut = sqlDbInterface.AddEditUOM_SPC(UOM_SPC);
            }

            catch (Exception ex)
            {
                responseOut.status = ActionStatus.Fail;
                responseOut.message = ActionMessage.ApplicationException;
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }

            return responseOut;
        }


    }
}

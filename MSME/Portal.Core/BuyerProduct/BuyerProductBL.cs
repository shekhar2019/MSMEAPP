using Portal.Common;
using Portal.Core.ViewModel;
using Portal.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Core
{
    public class BuyerProductBL
    {
        DBInterface dbInterface;
        SQLDbInterface sQLDbInterface;
        public BuyerProductBL()
        {
            dbInterface = new DBInterface();
            sQLDbInterface = new SQLDbInterface();
        }
        public ResponseOut AddUpdateBuyerProduct(BuyerProductViewModel viewModel, int companyId)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                BuyerProduct buyerProduct = new BuyerProduct
                {
                    BrandId = viewModel.BrandId,
                    BrandName = viewModel.BrandName,
                    BuyerCode = viewModel.BuyerCode,
                    BuyerId = viewModel.BuyerId,
                    BuyerProductDetailId = viewModel.BuyerProductDetailId,
                    CategoryId = viewModel.CategoryId,
                    ParentClassId = viewModel.ParentClassId,
                    ProductCode = viewModel.ProductCode,
                    ProductId = viewModel.ProductId,
                    ProductName = viewModel.ProductName,
                    //SpecificationName = viewModel.SpecificationName,
                    //SpecificationValue = viewModel.SpecificationValue,
                    SubCategoryId = viewModel.SubCategoryId,
                    UomId = viewModel.UomId
                };

                IEnumerable<ProductTechSpecification> specifications = viewModel.Specifications.Select((x) =>
                {
                    return new ProductTechSpecification
                    {
                        ProductTechSpecId = x.TechSpecId,
                        ProductTechSpecName = x.SpecName,
                        ProductTechSpecValue = x.SpecValue,
                        UomId = x.UomId
                    };
                });

                IEnumerable<Brand> Brand = viewModel.Brands.Select((x) =>
                {
                    return new Brand
                    {
                        ProductMainGroupId = viewModel.ParentClassId,
                        ProductSubGroupId = viewModel.CategoryId,
                        ProductSubChildGroupId = viewModel.SubCategoryId,
                        BrandName = viewModel.Brands,
                        BrandID = viewModel.BrandId
                    };
                });


                IEnumerable<ProductCode> productCode = viewModel.ProductCode.Select((x) =>
                {
                    return new ProductCode
                    {
                        ProductMainGroupId = viewModel.ParentClassId,
                        ProductSubGroupId = viewModel.CategoryId,
                        ProductSubChildGroupId = viewModel.SubCategoryId,
                        ProductCodeID = Convert.ToInt64(viewModel.ProductCode),
                        ProductCodeValue = viewModel.items,
                    };
                });

                responseOut = dbInterface.AddUpdateBuyerProduct(buyerProduct, specifications, companyId, Brand, productCode);
            }
            catch (Exception ex)
            {
                responseOut.status = ActionStatus.Fail;
                responseOut.message = ActionMessage.ApplicationException;
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
            return responseOut;
        }

        public IEnumerable<BuyerProductViewModel> GetBuyerProductDetails(long parentClassId, long categoryid, long subCategoryid, long buyerId)
        {
            var buyerProduct = dbInterface.GetBuyerProductDetails(parentClassId, categoryid, subCategoryid, buyerId).
            Select(x =>
                new BuyerProductViewModel
                {
                    BuyerProductDetailId = x.BuyerProductDetailId,
                    BrandId = x.BrandId.Value,
                    BrandName = x.BrandName,
                    BuyerId = x.BuyerId.Value,
                    CategoryId = x.CategoryId.Value,
                    ParentClassId = x.ParentClassId.Value,
                    ProductCode = x.ProductCode,
                    ProductId = x.ProductId.Value,
                    ProductName = x.ProductName,
                    SubCategoryId = x.SubCategoryId.Value,
                }
            ).ToList();

            foreach (var item in buyerProduct)
            {
                item.Specifications = dbInterface.GetSpecifications(item.BuyerProductDetailId).ToList();
            }
            return buyerProduct;
        }

        public IEnumerable<BuyerProductTechSpecificationViewModel> GetBuyerProductTechSpecification(long buyerProductId)
        {
            DataTable dt = sQLDbInterface.GetBuyerProductTechSpecificationList(buyerProductId);
            List<BuyerProductTechSpecificationViewModel> buyerProductTechSpecification = new List<BuyerProductTechSpecificationViewModel>();
           if(dt.Rows.Count>0)
            {
                foreach(DataRow row in dt.Rows)
                {
                    buyerProductTechSpecification.Add(new BuyerProductTechSpecificationViewModel
                    {
                        Id=Convert.ToInt64(row["Id"]),
                        BuyerProductId= Convert.ToInt64(row["BuyerProductId"]),
                        ProductTechSpecId=Convert.ToInt64(row["ProductTechSpecId"]),
                        ProductTechSpecName= Convert.ToString(row["ProductTechSpecName"]),
                        ProductTechSpecValue= Convert.ToString(row["ProductTechSpecValue"]),
                        UomId=Convert.ToInt32(row["UomId"]),
                        UomName=Convert.ToString(row["UOMName"])


                    });
                }
            }

            return buyerProductTechSpecification;
        }


        public IEnumerable<BuyerProductViewModel> GetProductByBuyerId(long buyerId)
        {
            var buyerProduct = dbInterface.GetProductByBuyerId(buyerId).
            Select(x =>
                new BuyerProductViewModel
                {
                    BuyerProductDetailId = x.BuyerProductDetailId,
                    BrandId = x.BrandId.Value,
                    BrandName = x.BrandName,
                    BuyerId = x.BuyerId.Value,
                    CategoryId = x.CategoryId.Value,
                    ParentClassId = x.ParentClassId.Value,
                    ProductCode = x.ProductCode,
                    ProductId = x.ProductId.Value,
                    ProductName = x.ProductName,
                    SubCategoryId = x.SubCategoryId.Value,
                }
            ).ToList();

            foreach (var item in buyerProduct)
            {
                item.Specifications = dbInterface.GetSpecifications(item.BuyerProductDetailId).ToList();
            }
            return buyerProduct;
        }

        public IEnumerable<BuyerProductDashboardViewModel> GetBuyerDashBoardProducts(long buyerId, string category = "", string city = "")
        {
            var buyerProds = dbInterface.GetProductByBuyerId(buyerId);

            List<BuyerProductDashboardViewModel> result = new List<BuyerProductDashboardViewModel>();
            if (buyerProds.Count() > 0)
            {
                if (!string.IsNullOrEmpty(category))
                {
                    category = category.ToLower();
                    buyerProds = buyerProds.Where(x => x.ParentClassName.ToLower().Contains(category) || x.CategoryName.ToLower().Contains(category) || x.SubCategoryName.ToLower().Contains(category) || x.ProductName.ToLower().Contains(category));
                }

                //var sellers = dbInterface.GetSellerDetails(buyerProds.Select(x => x.ProductId), city);

                foreach (var item in buyerProds)
                {
                    var product = new BuyerProductDashboardViewModel
                    {
                        BuyerProductDetailId = item.BuyerProductDetailId,
                        BrandId = item.BrandId.Value,
                        BrandName = item.BrandName,
                        BuyerId = item.BuyerId.Value,
                        CategoryId = item.CategoryId.Value,
                        ParentClassId = item.ParentClassId.Value,
                        ProductCode = item.ProductCode,
                        ProductId = item.ProductId.Value,
                        ProductName = item.ProductName,
                        //SpecificationName = item.SpecificationName,
                        //SpecificationValue = item.SpecificationValue,
                        SubCategoryId = item.SubCategoryId.Value,
                        //UomId = item.UomId.Value,
                        ProductFullDesc = item.ProductFullDesc,
                        ParentClassName = item.ParentClassName,
                        CategoryName = item.CategoryName,
                        SubCategoryName = item.SubCategoryName,
                        //UOMName = item.UOMName,
                        Specifications = item.Specifications
                    };
                    product.Sellers = new List<SellerItem>();
                    var sellersForProduct = dbInterface.GetSellersForBuyerProduct(item.ProductId, item.ParentClassId, item.CategoryId, item.SubCategoryId, city, Convert.ToInt32(item.BrandId), item.BuyerName);

                    if (sellersForProduct != null && sellersForProduct.Count() > 0)
                    {
                        IEnumerable<SellerItem> range = sellersForProduct.Select(x =>
                        {
                            return new SellerItem
                            {
                                SellerProductDetailId = x.SellerProductDetailId,
                                SellerId = x.SellerId == null ? 0 : x.SellerId.Value,
                                SalePrice = x.SalePrice == null ? 0 : x.SalePrice.Value,
                                SellerName = x.SellerName,
                                Contact = x.Contact,
                                ProductDate = x.ProductDate,
                                UOMName=x.UOMName
                            };
                        });
                        product.Sellers.AddRange(range);
                    }
                    result.Add(product);
                }
            }

            return result;
        }


        public IEnumerable<SellerProductViewModel> GetSellerAsProductId(int categoryId, int subCategoryId, int productId, int sellerId)
        {
            return dbInterface.GetSellerAsProductId(sellerId, categoryId, subCategoryId, productId).
            Select(x =>
                new SellerProductViewModel
                {
                    SellerName = x.SellerName,
                    SellerId = x.SellerId,
                    ProductName = x.ProductName,
                    SalePrice = x.SalePrice,
                    Email = x.Email,
                    Contact = x.Contact
                }
            );
        }
        public ResponseOut DeleteUpdateBuyerProduct(long buyerProductDetailId)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                responseOut = dbInterface.DeleteUpdateBuyerProduct(buyerProductDetailId);
            }
            catch (Exception ex)
            {
                responseOut.status = ActionStatus.Fail;
                responseOut.message = ActionMessage.ApplicationException;
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
            return responseOut;
        }

        public SellerProductBuyer GetBuyerProductDetail(long buyerProductDetailId)
        {
            var dt = sQLDbInterface.GetBuyerProductDetail(buyerProductDetailId);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    return new SellerProductBuyer
                    {
                        FullName = row["FullName"].ToString(),
                        ContactNo = row["ContactNo"].ToString(),
                        Email = row["Email"].ToString(),
                        Address = row["Address"].ToString(),
                        UOMName = row["UOMName"].ToString(),
                        CompanyName = row["CompanyName"].ToString(),
                    };
                }
            }
            return new SellerProductBuyer();
        }

        public IEnumerable<BuyerProductQtyDashboardViewModel> GetDashboardBuyerProductsQtydetails(long BuyerProductDetailId)
        {
            SQLDbInterface db = new SQLDbInterface();
            DataTable dt = db.GetProductQtydetailsByBuyerrProductId(BuyerProductDetailId);

            List<BuyerProductQtyDashboardViewModel> result = new List<BuyerProductQtyDashboardViewModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var product = new BuyerProductQtyDashboardViewModel
                {
                    ProductFullDesc = dt.Rows[i]["ProductName"].ToString(),
                    BuyerDemandQty = Convert.ToDecimal(dt.Rows[i]["OrderQty"] == null ? 0 : dt.Rows[i]["OrderQty"]),
                    Qtyupdatedate = Convert.ToDateTime(dt.Rows[i]["Createddate"].ToString()),
                    UOMName = dt.Rows[i]["UOM"].ToString()
            };
            result.Add(product);
        }

            return result;
        }


}
}

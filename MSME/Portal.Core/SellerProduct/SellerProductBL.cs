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
    public class SellerProductBL
    {
        SQLDbInterface sQLDbInterface;
        DBInterface dbInterface;
        public SellerProductBL()
        {
            dbInterface = new DBInterface();
            sQLDbInterface = new SQLDbInterface();
        }
        public ResponseOut AddUpdateSellerProduct(SellerProductViewModel viewModel, int companyId)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {

                SellerProduct sellerProduct = new SellerProduct
                {
                    BrandId = viewModel.BrandId,
                    BrandName = viewModel.BrandName,
                    SellerCode = viewModel.SellerCode,
                    SellerId = viewModel.SellerId,
                    SellerProductDetailId = viewModel.SellerProductDetailId,
                    CategoryId = viewModel.CategoryId,
                    ParentClassId = viewModel.ParentClassId,
                    ProductCode = viewModel.ProductCode.ToString(),
                    ProductId = viewModel.ProductId,
                    ProductName = viewModel.ProductName,
                    SubCategoryId = viewModel.SubCategoryId,
                    GSTID = viewModel.GSTID,
                    GSTPercentageID = viewModel.GSTPercentageID,
                    DeliveryStatusID = viewModel.DeliveryStatusID,
                    UomId=viewModel.UomId
                };

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

                responseOut = dbInterface.AddUpdateSellerProduct(sellerProduct, specifications, companyId, Brand, productCode);
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

        public IEnumerable<SellerProductDashboardViewModel> GetSellerDashboardProducts(long sellerId, string category = "", string city = "")
        {
            var sellerProds = dbInterface.GetProductBySellerId(sellerId);

            List<SellerProductDashboardViewModel> result = new List<SellerProductDashboardViewModel>();
            if (sellerProds.Count() > 0)
            {
                if (!string.IsNullOrEmpty(category))
                {
                    category = category.ToLower();
                    sellerProds = sellerProds.Where(x => x.ParentClassName.ToLower().Contains(category) || x.CategoryName.ToLower().Contains(category) || x.SubCategoryName.ToLower().Contains(category) || x.ProductName.ToLower().Contains(category));
                }

                //var buyers = dbInterface.GetBuyerDetails(sellerProds.Select(x => x.ProductId), city);

                foreach (var item in sellerProds)
                {
                    var product = new SellerProductDashboardViewModel
                    {
                        SellerProductDetailId = item.SellerProductDetailId,
                        BrandId = item.BrandId.Value,
                        BrandName = item.BrandName,
                        SellerId = item.SellerId.Value,
                        CategoryId = item.CategoryId.Value,
                        ParentClassId = item.ParentClassId.Value,
                        ProductCode = item.ProductCode,
                        ProductId = item.ProductId.Value,
                        ProductName = item.ProductName,
                        SubCategoryId = item.SubCategoryId.Value,
                        ProductFullDesc = item.ProductFullDesc,
                        ParentClassName = item.ParentClassName,
                        CategoryName = item.CategoryName,
                        SubCategoryName = item.SubCategoryName,
                        Specifications = item.Specifications
                    };
                    product.Buyers = new List<BuyeItem>();
                    var buyersForProduct = dbInterface.GetBuyersForSellerProduct(item.ProductId, item.ParentClassId, item.CategoryId, item.SubCategoryId, city, Convert.ToInt32(item.BrandId.ToString()), item.SellerName.ToString());

                    if (buyersForProduct != null && buyersForProduct.Count() > 0)
                        product.Buyers.AddRange(buyersForProduct.Select(x =>
                        {
                            return new BuyeItem
                            {
                                BuyerProductDetailId = x.BuyerProductDetailId,
                                BuyerId = x.BuyerId == null ? 0 : x.BuyerId.Value,
                                BuyerName = x.BuyerName,
                                Contact = x.Contact,
                                ProductDate = x.ProductDate,
                                BuyerQty= sQLDbInterface.MaxOrderQtyForBuyer(x.BuyerProductDetailId),
                                UOMName=x.UOMName


                            };
                        }));

                    result.Add(product);
                }
            }

            return result;
        }


        public IEnumerable<SellerProductViewModel> GetSellerProductDetails(long parentClassId, long categoryid, long subCategoryid, long sellerId)
        {
            var sellerProducts = dbInterface.GetSellerProductDetails(parentClassId, categoryid, subCategoryid, sellerId).
            Select(x =>
                new SellerProductViewModel
                {
                    SellerProductDetailId = x.SellerProductDetailId,
                    BrandId = x.BrandId.Value,
                    BrandName = x.BrandName,
                    SellerId = x.SellerId.Value,
                    CategoryId = x.CategoryId.Value,
                    ParentClassId = x.ParentClassId.Value,
                    ProductCode = x.ProductCode.ToString(),
                    ProductId = x.ProductId.Value,
                    ProductName = x.ProductName,
                    SubCategoryId = x.SubCategoryId.Value,
                    GSTID = x.GSTID.Value,
                    GSTPercentageID = x.GSTPercentageID.Value,
                    DeliveryStatusID = x.DeliveryStatusID.Value
                }
            ).ToList();

            foreach (var item in sellerProducts)
            {
                item.Specifications = dbInterface.GetSellerProductSpecifications(item.SellerProductDetailId).ToList();
                item.BrandName = dbInterface.GetBrandsNameByBrandID(Convert.ToInt64(item.SellerProductDetailId.ToString())).ToString();
                item.ProductCode = dbInterface.GetProductCodeByProductID(Convert.ToInt64(item.SellerProductDetailId.ToString())).ToString();
            }

            return sellerProducts;
        }


        public DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties by using reflection   
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names  
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {

                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }

            return dataTable;
        }


        public IEnumerable<SellerProductDashboardViewModel> GetProductBySellerId(long SellerId)
        {
            var sellerProducts = dbInterface.GetProductBySellerId(SellerId).
            Select(x =>
                new SellerProductDashboardViewModel
                {
                    SellerProductDetailId = x.SellerProductDetailId,
                    BrandId = x.BrandId.Value,
                    BrandName = x.BrandName,
                    SellerId = x.SellerId.Value,
                    CategoryId = x.CategoryId.Value,
                    ParentClassId = x.ParentClassId.Value,
                    ProductCode = x.ProductCode,
                    ProductId = x.ProductId.Value,
                    ProductName = x.ProductName,
                    SubCategoryId = x.SubCategoryId.Value,
                    CategoryName = x.CategoryName,
                    SubCategoryName = x.SubCategoryName,
                    HSN_Code = x.HSN_Code,
                    PurchasePrice = x.PurchasePrice,
                    SalePrice = x.SalePrice,
                    ProductFullDesc = x.ProductFullDesc,
                }
            ).ToList();

            foreach (var item in sellerProducts)
            {
                item.Specifications = dbInterface.GetSellerProductSpecifications(item.SellerProductDetailId).ToList();
            }

            return sellerProducts;
        }

        public ResponseOut DeleteUpdateSellerProduct(long sellerProductDetailId)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                responseOut = dbInterface.DeleteUpdateSellerProduct(sellerProductDetailId);
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

        public IEnumerable<BuyerProductViewModel> GetBuyerAsProductId(int categoryId, int subCategoryId, int productId, int sellerId)
        {
            return dbInterface.GetBuyerAsProductId(sellerId, categoryId, subCategoryId, productId).
            Select(x =>
                new BuyerProductViewModel
                {
                    ProductName = x.ProductName,
                    Email = x.Email,
                    Contact = x.Contact,
                    UserName = x.BuyerName,
                    BuyerId = x.BuyerId
                }
            );
        }

        public BuyerProductSeller GetSellerProductDetail(long sellerrProductDetailId)
        {
            var dt = sQLDbInterface.GetSellerProductDetail(sellerrProductDetailId);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    return new BuyerProductSeller
                    {
                        FullName = row["FullName"].ToString(),
                        ContactNo = row["ContactNo"].ToString(),
                        Email = row["Email"].ToString(),
                        Address = row["Address"].ToString(),
                        CompanyName = row["CompanyName"].ToString(),
                        MOQ = row["MOQ"].ToString(),
                        SalePrice = row["SalePrice"].ToString(),
                        GSTName = row["GSTName"].ToString(),
                        UOMName = row["UOMName"].ToString(),
                        DeliveryName = row["DeliveryName"].ToString()
                    };
                }
            }
            return new BuyerProductSeller();
        }


        public IEnumerable<SellerProductPriceDashboardViewModel> GetDashboardSellerProductsPricedetails(long sellerrProductDetailId)
        {
            SQLDbInterface db = new SQLDbInterface();
            DataTable dt = db.GetProductpricedetailsBySellerProductId(sellerrProductDetailId);

            List<SellerProductPriceDashboardViewModel> result = new List<SellerProductPriceDashboardViewModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var product = new SellerProductPriceDashboardViewModel
                {
                    ProductFullDesc = dt.Rows[i]["ProductName"].ToString(),
                    SelllerProductPrice = Convert.ToDecimal(dt.Rows[i]["Price"].ToString()),
                    SellerMinimumorderQty = Convert.ToDecimal(dt.Rows[i]["MinimumorderQty"].ToString()),
                    Priceupdatedate = Convert.ToDateTime(dt.Rows[i]["Createddate"].ToString()),
                    UOMName = dt.Rows[i]["UOMNAME"].ToString()
                };
                result.Add(product);
            }

            return result;
        }


    }
}

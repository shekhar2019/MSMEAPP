using Portal.DAL.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Portal.Core.ViewModel
{
    public partial class BuyerProductViewModel
    {
        public long BuyerProductDetailId { get; set; }
        public long ParentClassId { get; set; }
        public long CategoryId { get; set; }
        public long SubCategoryId { get; set; }
        public long ProductId { get; set; }


        public string ParentClassName { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }

        public string ProductName { get; set; }
        public string SpecificationName { get; set; }
        public string SpecificationValue { get; set; }
        public int UomId { get; set; }
        public string UOMName { get; set; }
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public string ProductCode { get; set; }
        public Nullable<long> BuyerId { get; set; }
        public string BuyerCode { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
        public string UserName { get; set; }


        public string Brands { get; set; }
        public string items { get; set; }
        public IEnumerable<TechSpecificationVM> Specifications { get; set; }
    }
    public partial class BuyerProductTechSpecificationViewModel
    {
        public long Id { get; set; }
        public long ProductTechSpecId { get; set; }
        public string ProductTechSpecName { get; set; }
        public long BuyerProductId { get; set; }
        public string ProductTechSpecValue { get; set; }
        public Nullable<int> UomId { get; set; }
        public string UomName { get; set; }
    }

    public partial class BuyerProductDashboardViewModel : BuyerProductViewModel
    {
        public DateTime MaxSellerDate { get { return Sellers == null || Sellers.Count == 0 ? DateTime.MinValue : Sellers.Max(x => x.ProductDate == null ? DateTime.MinValue : x.ProductDate.Value); } }
        public decimal MaxSellerPrice { get { return Sellers == null || Sellers.Count == 0 ? 0 : Sellers.Max(x => x.SalePrice); } }
        public string ProductFullDesc { get; set; }

        public List<SellerItem> Sellers { get; set; }
    }

    public class SellerItem
    {
        public long SellerProductDetailId { get; set; }
        public long SellerId { get; set; }
        public decimal SalePrice { get; set; }
        public string SellerName { get; set; }
        public string Contact { get; set; }
        public DateTime? ProductDate { get; set; }
        public string UOMName { get; set; }
    }

    public partial class BuyerProductQtyDashboardViewModel
    {
        public string ProductFullDesc { get; set; }
        public Nullable<decimal> BuyerDemandQty { get; set; }
        public DateTime Qtyupdatedate { get; set; }
        public string UOMName { get; set; }
    }
}
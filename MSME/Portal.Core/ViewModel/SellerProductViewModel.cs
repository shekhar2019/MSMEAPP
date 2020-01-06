using Portal.DAL.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Portal.Core.ViewModel
{
    public partial class SellerProductViewModel
    {
        public long SellerProductDetailId { get; set; }
        public long ParentClassId { get; set; }
        public long CategoryId { get; set; }
        public long SubCategoryId { get; set; }
        public string ParentClassName { get; set; }
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public string SpecificationName { get; set; }
        public string SpecificationValue { get; set; }
        public int UomId { get; set; }
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public string ProductCode { get; set; }
        public Nullable<long> SellerId { get; set; }
        public string SellerCode { get; set; }
        public Nullable<byte> GSTID { get; set; }
        public Nullable<byte> GSTPercentageID { get; set; }
        public Nullable<byte> DeliveryStatusID { get; set; }
        public short MOQ { get; set; }
        public Nullable<decimal> SalePrice { get; set; }
        public string SellerName { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
        public IEnumerable<TechSpecificationVM> Specifications { get; set; }


        public string Brands { get; set; }
        public string items { get; set; }


    }

    public partial class SellerProductDashboardViewModel : SellerProductViewModel
    {
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public string HSN_Code { get; set; }
        public Nullable<decimal> PurchasePrice { get; set; }
        public string ProductFullDesc { get; set; }
        public string UOMName { get; set; }

        public System.Collections.Generic.List<BuyeItem> Buyers { get; set; }
        public DateTime MaxBuyerDate { get { return Buyers == null || Buyers.Count == 0 ? DateTime.MinValue : Buyers.Max(x => x.ProductDate == null ? DateTime.MinValue : x.ProductDate.Value); } }


    }

    public partial class SellerProductPriceDashboardViewModel 
    {
        public string ProductFullDesc { get; set; }
        public Nullable<decimal> SelllerProductPrice { get; set; }
        public Nullable<decimal> SellerMinimumorderQty { get; set; }
        public DateTime Priceupdatedate { get; set; }
        public string UOMName { get; set; }

    }

    public class BuyeItem
    {
        public long BuyerProductDetailId { get; set; }
        public long BuyerId { get; set; }
        public string BuyerName { get; set; }
        public string Contact { get; set; }
        public DateTime? ProductDate { get; set; }
        public Nullable<decimal> BuyerQty { get; set; }
        public string UOMName { get; set; }
    }

}

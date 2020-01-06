﻿using Portal.Common;
using Portal.DAL.EntityModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Portal.DAL
{
    /// <summary>
    /// Class to Provide Services of DB
    /// </summary>
    public partial class DBInterface : IDisposable
    {
        private readonly ERPEntities entities = new ERPEntities();

        public IEnumerable<ProductTechSpecification> GetSpecifications(long productMainGroupId, long productSubGroupId, long productChildSubGroupId)
        {
            return (from pt in entities.ProductTechSpecifications
                    join p in entities.Products on pt.ProductId equals p.Productid
                    where p.ProductMainGroupId == productMainGroupId && p.ProductSubGroupId == productSubGroupId && p.ProductSubChildGroupId == productChildSubGroupId
                    select pt);
        }

        public IEnumerable<TechSpecificationVM> GetSpecifications(long buyerProductDetailId)
        {
            return (from pt in entities.ProductTechSpecifications
                    join bps in entities.BuyerProductTechSpecifications on pt.ProductTechSpecId equals bps.ProductTechSpecId
                    join u in entities.UOMs on bps.UomId equals u.UOMId into empUOM
                    from ed in empUOM.DefaultIfEmpty()
                    where bps.BuyerProductId == buyerProductDetailId
                    select new TechSpecificationVM
                    {
                        SpecName = pt.ProductTechSpecName,
                        SpecValue = bps.ProductTechSpecValue,
                        UomName = ed == null ? "" : ed.UOMName
                    });
        }

        public IEnumerable<TechSpecificationVM> GetSellerProductSpecifications(long sellerProductId)
        {
            return (from pt in entities.ProductTechSpecifications
                    join sps in entities.SellerProductTechSpecifications on pt.ProductTechSpecId equals sps.ProductTechSpecId
                    join u in entities.UOMs on sps.UomId equals u.UOMId into empUOM
                    from ed in empUOM.DefaultIfEmpty()
                    where sps.SellerProductId == sellerProductId
                    select new TechSpecificationVM
                    {
                        SpecName = pt.ProductTechSpecName,
                        SpecValue = sps.ProductTechSpecValue,
                        UomName = ed == null ? "" : ed.UOMName
                    });
        }

        public String GetBrandsNameByBrandID(long SellerProductDetailId)
        {
            String BrandNamestr = "";
            var res = from pt in entities.Brands
                      join sp in entities.SellerProducts on pt.BrandID.ToString() equals sp.BrandId.ToString() into brandeom

                      from mo in brandeom.DefaultIfEmpty()
                      where mo.SellerProductDetailId == SellerProductDetailId
                      select new { pt.BrandName };
            foreach (var r in res)
            {
                BrandNamestr = r.BrandName;
                break;
            }
            return BrandNamestr;

        }

        public String GetProductCodeByProductID(long SellerProductDetailId)
        {
            String BrandNamestr = "";
            var res = from pt in entities.ProductCodes
                      join sp in entities.SellerProducts on pt.ProductCodeID.ToString() equals sp.ProductCode.ToString() into brandeom

                      from mo in brandeom.DefaultIfEmpty()
                      where mo.SellerProductDetailId == SellerProductDetailId
                      select new { pt.ProductCodeValue };
            foreach (var r in res)
            {
                BrandNamestr = r.ProductCodeValue;
                break;
            }
            return BrandNamestr;
        }


        public IEnumerable<ProductTechSpecification> GetSpecifications(string specName)
        {
            return entities.ProductTechSpecifications.Where(x => x.ProductTechSpecName.Equals(specName, StringComparison.CurrentCultureIgnoreCase));
        }

        public DBInterface()
        {

        }
        #region Dispose Methods
        public void Dispose()
        {
            try
            {
                entities.Dispose();
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
        }
        #endregion
        public User AuthenticateUser(string userName, string password)
        {
            User user = new User();
            try
            {
                if (entities.Users.Any(x => x.UserName == userName && x.Password == password))
                {
                    user = entities.Users.Where(u => u.UserName == userName && u.Password == password).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return user;
        }

        public IEnumerable<State> GetAllStates()
        {
            return entities.States;
        }

        public List<proc_GetRoleWiseParentUI_Result> GetRoleWiseParentUI(int roleId)
        {
            List<proc_GetRoleWiseParentUI_Result> roleWiseParentUI = new List<proc_GetRoleWiseParentUI_Result>();
            try
            {
                roleWiseParentUI = entities.proc_GetRoleWiseParentUI(roleId).ToList();
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return roleWiseParentUI;
        }

        public void UpdateProfile(UserRegistration userRegistration)
        {
            var user = entities.UserRegistrations.FirstOrDefault(x => x.UserId == userRegistration.UserId);
            if (user != null)
            {
                user.FirstName = userRegistration.FirstName;
                user.LastName = userRegistration.LastName;
                user.Email = userRegistration.Email;
                user.ContactNo = userRegistration.ContactNo;
                user.AlternateContactno = userRegistration.AlternateContactno;
                user.Fax = userRegistration.Fax;
                user.StateId = userRegistration.StateId;
                user.City = userRegistration.City;
                user.SubCity = userRegistration.SubCity;
                user.Landmark = userRegistration.LastName;
                user.BuildingNo = userRegistration.BuildingNo;
                user.PINCode = userRegistration.PINCode;
                user.GSTNo = userRegistration.GSTNo;
                user.PANNo = userRegistration.PANNo;
                user.UdyogAadhaarNo = userRegistration.UdyogAadhaarNo;
                user.CompanyName = userRegistration.CompanyName;

                entities.SaveChanges();
            }
        }

        public UserRegistration GetRegistration(long userId)
        {
            return entities.UserRegistrations.Where(x => x.UserId == userId).FirstOrDefault();
        }

        public List<proc_GetRoleWiseChildUI_Result> GetRoleWiseChildUI(int roleId, int parentId)
        {
            List<proc_GetRoleWiseChildUI_Result> roleWiseChildUI = new List<proc_GetRoleWiseChildUI_Result>();
            try
            {
                roleWiseChildUI = entities.proc_GetRoleWiseChildUI(parentId, roleId).ToList();
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return roleWiseChildUI;
        }

        public IEnumerable<BuyerProduct> GetBuyerProductDetails(long parentClassId, long categoryid, long subCategoryid, long buyerId)
        {
            return entities.BuyerProducts.Where(x => x.ParentClassId == parentClassId && x.CategoryId == categoryid && x.SubCategoryId == subCategoryid && x.BuyerId == buyerId);
        }

        public IEnumerable<BuyerProductTechSpecification> GetBuyerProductTechSpecification(long buyerProductDetailId)
        {
            var res = entities.BuyerProductTechSpecifications.Join(entities.UOMs,
                bpts => bpts.UomId,
                u => u.UOMId,
                (bpts, u) => new {
                    
                });
            return entities.BuyerProductTechSpecifications.Where(x => x.BuyerProductId== buyerProductDetailId);

        }
        public IEnumerable<City> GetStateCities(int stateId)
        {
            return entities.Cities.Where(x => x.StateId == stateId);
        }


        public List<SellerProductDetailList> GetSellerAsProductId(long sellerId, long categoryid, long subCategoryid, long productId)
        {
            var BuyerProductsCategory = (from sp in entities.SellerProducts
                                         join ur in entities.UserRegistrations on sp.SellerId equals ur.UserId
                                         where sp.CategoryId == categoryid && sp.SubCategoryId == subCategoryid && sp.ProductId == productId
                                         select new
                                         {
                                             sp.SalePrice,
                                             sp.ProductName,
                                             ur.FirstName,
                                             ur.LastName,
                                             sp.SellerId,
                                             ur.Email,
                                             ur.ContactNo
                                         }).ToList();

            List<SellerProductDetailList> lstSellerProductList = new List<SellerProductDetailList>();
            if (BuyerProductsCategory != null && BuyerProductsCategory.Count > 0)
            {
                foreach (var item in BuyerProductsCategory)
                {
                    lstSellerProductList.Add(new SellerProductDetailList
                    {
                        ProductName = item.ProductName,
                        SalePrice = item.SalePrice,
                        SellerName = item.FirstName + " " + item.LastName,
                        SellerId = item.SellerId,
                        Email = item.Email,
                        Contact = item.ContactNo
                    });

                }
            }
            return lstSellerProductList;
        }

        public IEnumerable<BuyerProductDetailList> GetBuyersForSellerProduct(long? productId, long? mainGroupId, long? subGroupId, long? subChildGroupId, string city, int brandID, string Product_Code)
        {
            return from x in entities.BuyerProducts
                   join user in entities.UserRegistrations on x.BuyerId equals user.UserId
                   join uo in entities.UOMs on x.UomId equals uo.UOMId 
                   where x.ParentClassId == mainGroupId && x.CategoryId == subGroupId && x.SubCategoryId == subChildGroupId
                   && user.City.ToLower().Contains(string.IsNullOrEmpty(city) ? user.City.ToLower() : city.ToLower()) && uo.IsSUM==false 
                   select new BuyerProductDetailList
                   {
                       BuyerProductDetailId = x.BuyerProductDetailId,
                       BuyerId = x.BuyerId,
                       BuyerName = user.FirstName + " " + user.LastName,
                       Contact = user.ContactNo,
                       ProductId = x.ProductId,
                       ProductDate = x.ModifiedDate == null ? x.CreatedDate : x.ModifiedDate,
                       UOMName = uo.UOMName,
                   };
        }


        public List<BuyerProductDetailList> GetBuyerAsProductId(long buyerId, long categoryid, long subCategoryid, long productId)
        {
            var BuyerProductsCategory = (from bp in entities.BuyerProducts
                                         join ur in entities.UserRegistrations on bp.BuyerId equals ur.UserId
                                         where bp.CategoryId == categoryid && bp.SubCategoryId == subCategoryid && bp.ProductId == productId
                                         select new
                                         {
                                             ur.Email,
                                             ur.ContactNo,
                                             bp.ProductName,
                                             ur.FirstName,
                                             ur.LastName,
                                             bp.BuyerId
                                         }).ToList();

            List<BuyerProductDetailList> lstBuyerProductList = new List<BuyerProductDetailList>();
            if (BuyerProductsCategory != null && BuyerProductsCategory.Count > 0)
            {
                foreach (var item in BuyerProductsCategory)
                {
                    lstBuyerProductList.Add(new BuyerProductDetailList
                    {
                        ProductName = item.ProductName,
                        Email = item.Email,
                        BuyerName = item.FirstName + " " + item.LastName,
                        Contact = item.ContactNo,
                        BuyerId = item.BuyerId
                    });

                }
            }
            return lstBuyerProductList;
        }

        public IEnumerable<TechSpecificationVM> GetBuyerProductSpecifications(long buyerProductDetailId)
        {
            return (from b in entities.BuyerProductTechSpecifications
                    join p in entities.ProductTechSpecifications on b.ProductTechSpecId equals p.ProductTechSpecId
                    join u in entities.UOMs on b.UomId equals u.UOMId
                    where b.BuyerProductId == buyerProductDetailId
                    select new TechSpecificationVM
                    {
                        SpecName = p.ProductTechSpecName,
                        SpecValue = b.ProductTechSpecValue,
                        TechSpecId = p.ProductTechSpecId,
                        UomName = u.UOMName
                    });
        }

        //public IEnumerable<TechSpecificationVM> GetSellerProductSpecifications(long sellrProductDetailId)
        //{
        //    return (from b in entities.SellerProductTechSpecifications
        //            join p in entities.ProductTechSpecifications on b.ProductTechSpecId equals p.ProductTechSpecId
        //            join u in entities.UOMs on p.UomId equals u.UOMId
        //            where b.SellerProductId == sellrProductDetailId
        //            select new TechSpecificationVM
        //            {
        //                SpecName = p.ProductTechSpecName,
        //                SpecValue = p.ProductTechSpecValue,
        //                TechSpecId = p.ProductTechSpecId,
        //                UomName = u.UOMName
        //            });
        //}

        public IEnumerable<SellerProductDetailList> GetSellersForBuyerProduct(long? productId, long? mainGroupId, long? subGroupId, long? subChildGroupId, string city, int brandID, string ProductCode)
        {
            return from x in entities.SellerProducts
                   join user in entities.UserRegistrations on x.SellerId equals user.UserId
                   join um in entities.UOMs on x.UomId equals um.UOMId
                   where x.ProductCode == ProductCode && x.ParentClassId == mainGroupId && x.CategoryId == subGroupId && x.SubCategoryId == subChildGroupId && x.BrandId == brandID
                   && user.City.ToLower().Contains(string.IsNullOrEmpty(city) ? user.City.ToLower() : city.ToLower())
                   select new SellerProductDetailList
                   {
                       SellerProductDetailId = x.SellerProductDetailId,
                       SellerId = x.SellerId,
                       SellerName = user.FirstName + " " + user.LastName,
                       SalePrice = x.SalePrice,
                       Contact = user.ContactNo,
                       ProductId = x.ProductId,
                       ProductDate = x.ModifiedDate == null ? x.CreatedDate : x.ModifiedDate,
                       UOMName = um.UOMName
                   };
        }


        public IEnumerable<SellerProductDetailList> GetSellerDetails(IEnumerable<long?> buyerProds)
        {
            return from x in entities.SellerProducts
                   join user in entities.UserRegistrations on x.SellerId equals user.UserId
                   where buyerProds.Any(y => y == x.ProductId)
                   select new SellerProductDetailList
                   {
                       SellerId = x.SellerId,
                       SellerName = user.FirstName + " " + user.LastName,
                       SalePrice = x.SalePrice,
                       Contact = user.ContactNo,
                       ProductId = x.ProductId
                   };
        }

        public IEnumerable<BuyerProductDetailList> GetBuyerDetails(IEnumerable<long?> sellerProds)
        {
            return from x in entities.BuyerProducts
                   join user in entities.UserRegistrations on x.BuyerId equals user.UserId
                   where sellerProds.Any(y => y == x.ProductId)
                   select new BuyerProductDetailList
                   {
                       BuyerId = x.BuyerId,
                       BuyerName = user.FirstName + " " + user.LastName,
                       Contact = user.ContactNo,
                       ProductId = x.ProductId
                   };
        }

        //public List<BuyerProductDetailList> GetProductByBuyerId(long buyerId)
        //{
        //    var BuyerProductsCategory = (from bp in entities.BuyerProducts
        //                                 join psg in entities.ProductSubGroups on bp.CategoryId equals psg.ProductSubGroupId
        //                                 join pscg in entities.ProductSubChildGroups on bp.SubCategoryId equals pscg.ProductSubChildGroupId
        //                                 join p in entities.Products on bp.ProductId equals p.Productid
        //                                 join u in entities.UOMs on bp.UomId equals u.UOMId
        //                                 where bp.BuyerId == buyerId
        //                                 select new
        //                                 {
        //                                     bp.BuyerProductDetailId,
        //                                     bp.ParentClassId,
        //                                     bp.CategoryId,
        //                                     bp.SubCategoryId,
        //                                     bp.ProductId,
        //                                     bp.ProductName,
        //                                     bp.SpecificationName,
        //                                     bp.SpecificationValue,
        //                                     bp.UomId,
        //                                     bp.BrandId,
        //                                     bp.BrandName,
        //                                     bp.ProductCode,
        //                                     bp.BuyerId,
        //                                     bp.BuyerCode,
        //                                     psg.ProductSubGroupName,
        //                                     pscg.ProductSubChildGroupName,
        //                                     p.HSN_Code,
        //                                     p.PurchasePrice,
        //                                     p.SalePrice,
        //                                     p.ProductFullDesc,
        //                                     u.UOMName
        //                                 }).ToList();

        //    List<BuyerProductDetailList> lstBuyerProductList = new List<BuyerProductDetailList>();
        //    if (BuyerProductsCategory != null && BuyerProductsCategory.Count > 0)
        //    {
        //        foreach (var item in BuyerProductsCategory)
        //        {
        //            lstBuyerProductList.Add(new BuyerProductDetailList
        //            {
        //                BuyerProductDetailId = item.BuyerProductDetailId,
        //                ParentClassId = item.ParentClassId,
        //                CategoryId = item.CategoryId,
        //                SubCategoryId = item.SubCategoryId,
        //                ProductId = item.ProductId,
        //                ProductName = item.ProductName,
        //                SpecificationName = item.SpecificationName,
        //                SpecificationValue = item.SpecificationValue,
        //                UomId = item.UomId,
        //                BrandId = item.BrandId,
        //                BrandName = item.BrandName,
        //                ProductCode = item.ProductCode,
        //                BuyerId = item.BuyerId,
        //                BuyerCode = item.BuyerCode,
        //                CategoryName = item.ProductSubGroupName,
        //                SubCategoryName = item.ProductSubChildGroupName,
        //                HSN_Code = item.HSN_Code,
        //                PurchasePrice = item.PurchasePrice,
        //                SalePrice = item.SalePrice,
        //                ProductFullDesc = item.ProductFullDesc,
        //                UOMName = item.UOMName
        //            });

        //        }
        //    }
        //    return lstBuyerProductList;
        //    //return entities.BuyerProducts.Where(x => x.BuyerId == buyerId);
        //}


        public IEnumerable<BuyerProductDetailList> GetProductByBuyerId(long buyerId)
        {
            var buyerProds = (from bp in entities.BuyerProducts
                              join pmg in entities.ProductMainGroups on bp.ParentClassId equals pmg.ProductMainGroupId
                              join psg in entities.ProductSubGroups on bp.CategoryId equals psg.ProductSubGroupId
                              join pscg in entities.ProductSubChildGroups on bp.SubCategoryId equals pscg.ProductSubChildGroupId
                              join p in entities.Products on bp.ProductId equals p.Productid
                              join Brn in entities.Brands on bp.BrandId.ToString() equals Brn.BrandID.ToString() into Brntemp
                              from ed in Brntemp.DefaultIfEmpty()
                                  //join PCode in entities.ProductCodes on sp.ProductCode.ToString() equals PCode.ProductCodeID.ToString() into PCodetemp
                                  //from edPCode in PCodetemp.DefaultIfEmpty()

                              where bp.BuyerId == buyerId
                              orderby bp.CreatedDate descending
                              select new BuyerProductDetailList
                              {
                                  BuyerProductDetailId = bp.BuyerProductDetailId,
                                  ParentClassId = bp.ParentClassId,
                                  CategoryId = bp.CategoryId,
                                  SubCategoryId = bp.SubCategoryId,
                                  ProductId = bp.ProductId,
                                  ProductName = bp.ProductName,
                                  SpecificationName = bp.SpecificationName,
                                  SpecificationValue = bp.SpecificationValue,
                                  BrandId = bp.BrandId,
                                  BrandName = ed == null ? "" : ed.BrandName,
                                  ProductCode = bp.ProductCode.ToString(),
                                  BuyerId = bp.BuyerId,
                                  BuyerCode = bp.BuyerCode,
                                  ParentClassName = pmg.ProductMainGroupName,
                                  CategoryName = psg.ProductSubGroupName,
                                  SubCategoryName = pscg.ProductSubChildGroupName,
                                  HSN_Code = p.HSN_Code,
                                  PurchasePrice = p.PurchasePrice,
                                  SalePrice = p.SalePrice,
                                  ProductFullDesc = p.ProductFullDesc,
                                  ProductDate = bp.CreatedDate ?? DateTime.MinValue,
                                  BuyerName = bp.ProductCode.ToString()
                              })?.ToList() ?? new List<BuyerProductDetailList>();

            foreach (var buyerProd in buyerProds)
            {
                buyerProd.Specifications = GetBuyerProductSpecifications(buyerProd.BuyerProductDetailId).ToList();
                buyerProd.ProductCode = (new SQLDbInterface().GetPRoductCodeName(buyerProd.ProductCode.ToString()));
            }
            buyerProds = buyerProds.OrderByDescending(x => x.ProductDate)?.ToList();
            return buyerProds;
        }

        public IEnumerable<SellerProductDetailList> GetSellerDetails(IEnumerable<long?> buyerProds, string city)
        {
            return from x in entities.SellerProducts
                   join user in entities.UserRegistrations on x.SellerId equals user.UserId
                   where buyerProds.Any(y => y == x.ProductId) && user.City.Contains(String.IsNullOrEmpty(city) ? user.City : city)
                   select new SellerProductDetailList
                   {
                       SellerId = x.SellerId,
                       SellerName = user.FirstName + " " + user.LastName,
                       SalePrice = x.SalePrice,
                       Contact = user.ContactNo,
                       ProductId = x.ProductId
                   };
        }

        public ResponseOut DeleteUpdateBuyerProduct(long buyerProductDetailId)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                BuyerProduct buyerProducExisting = entities.BuyerProducts.FirstOrDefault(x => x.BuyerProductDetailId == buyerProductDetailId);
                if (buyerProducExisting != null)
                {
                    entities.BuyerProducts.Remove(buyerProducExisting);
                    entities.SaveChanges();
                    responseOut.status = ActionStatus.Success;
                    responseOut.message = ActionMessage.BuyerProductDeleted;
                }
                else
                {
                    responseOut.status = ActionStatus.Fail;
                }
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

        #region Selller Product
        public IEnumerable<SellerProduct> GetSellerProductDetails(long parentClassId, long categoryid, long subCategoryid, long SellerId)
        {
            return entities.SellerProducts.Where(x => x.ParentClassId == parentClassId && x.CategoryId == categoryid && x.SubCategoryId == subCategoryid && x.SellerId == SellerId).
                OrderByDescending(x => x.CreatedDate ?? DateTime.MinValue);
        }

        public IEnumerable<SellerProductDetailList> GetProductBySellerId(long SellerId)
        {
            var SellerProductsCategory = (from sp in entities.SellerProducts
                                          join pmg in entities.ProductMainGroups on sp.ParentClassId equals pmg.ProductMainGroupId
                                          join psg in entities.ProductSubGroups on sp.CategoryId equals psg.ProductSubGroupId
                                          join pscg in entities.ProductSubChildGroups on sp.SubCategoryId equals pscg.ProductSubChildGroupId
                                          join p in entities.Products on sp.ProductId equals p.Productid
                                          join Brn in entities.Brands on sp.BrandId.ToString() equals Brn.BrandID.ToString() into Brntemp
                                          from ed in Brntemp.DefaultIfEmpty()
                                              //join PCode in entities.ProductCodes on sp.ProductCode.ToString() equals PCode.ProductCodeID.ToString() into PCodetemp
                                              //from edPCode in PCodetemp.DefaultIfEmpty()



                                          where sp.SellerId == SellerId
                                          orderby sp.CreatedDate descending
                                          select new
                                          {
                                              sp.SellerProductDetailId,
                                              sp.ParentClassId,
                                              sp.CategoryId,
                                              sp.SubCategoryId,
                                              sp.ProductId,
                                              sp.ProductName,
                                              sp.BrandId,
                                              BrandName = ed == null ? "" : ed.BrandName,
                                              // ProductCodeValue = PCodetemp == null ? "0" : edPCode.ProductCodeValue,
                                              ProductCodeID = sp.ProductCode,
                                              sp.SellerId,
                                              sp.SellerCode,
                                              psg.ProductSubGroupName,
                                              pscg.ProductSubChildGroupName,
                                              p.HSN_Code,
                                              p.PurchasePrice,
                                              p.SalePrice,
                                              p.ProductFullDesc,
                                              pmg.ProductMainGroupName,
                                              sp.CreatedDate
                                          }).ToList();

            List<SellerProductDetailList> lstSellerProductList = new List<SellerProductDetailList>();
            if (SellerProductsCategory != null && SellerProductsCategory.Count > 0)
            {
                foreach (var item in SellerProductsCategory)
                {
                    lstSellerProductList.Add(new SellerProductDetailList
                    {
                        SellerProductDetailId = item.SellerProductDetailId,
                        ParentClassId = item.ParentClassId,
                        CategoryId = item.CategoryId,
                        SubCategoryId = item.SubCategoryId,
                        ProductId = item.ProductId,
                        ProductName = item.ProductName,
                        BrandId = item.BrandId,
                        BrandName = item.BrandName,
                        ProductCode = (new SQLDbInterface().GetPRoductCodeName(item.ProductCodeID.ToString())),
                        SellerId = item.SellerId,
                        SellerCode = item.SellerCode,
                        CategoryName = item.ProductSubGroupName,
                        SubCategoryName = item.ProductSubChildGroupName,
                        HSN_Code = item.HSN_Code,
                        PurchasePrice = item.PurchasePrice,
                        SalePrice = item.SalePrice,
                        ProductFullDesc = item.ProductFullDesc,
                        ParentClassName = item.ProductMainGroupName,
                        SellerName = item.ProductCodeID.ToString()
                    });

                }
            }

            foreach (var prod in lstSellerProductList)
            {
                prod.Specifications = GetSellerProductSpecifications(prod.SellerProductDetailId).ToList();
            }
            return lstSellerProductList;
        }

        public IEnumerable<BuyerProductDetailList> GetBuyerDetails(IEnumerable<long?> sellerProds, string city)
        {
            return from x in entities.BuyerProducts
                   join user in entities.UserRegistrations on x.BuyerId equals user.UserId
                   where sellerProds.Any(y => y == x.ProductId) && user.City.Contains(String.IsNullOrEmpty(city) ? user.City : city)
                   select new BuyerProductDetailList
                   {
                       BuyerId = x.BuyerId,
                       BuyerName = user.FirstName + " " + user.LastName,
                       Contact = user.ContactNo,
                       ProductId = x.ProductId,
                       ProductDate = x.ModifiedDate == null ? x.CreatedDate : x.ModifiedDate,
                   };
        }

        public ResponseOut DeleteUpdateSellerProduct(long sellerProductDetailId)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                SellerProduct sellerProducExisting = entities.SellerProducts.FirstOrDefault(x => x.SellerProductDetailId == sellerProductDetailId);
                if (sellerProducExisting != null)
                {
                    entities.SellerProducts.Remove(sellerProducExisting);
                    entities.SaveChanges();
                    responseOut.status = ActionStatus.Success;
                    responseOut.message = ActionMessage.BuyerProductDeleted;
                }
                else
                {
                    responseOut.status = ActionStatus.Fail;
                }
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

        #endregion
        public bool AuthorizeUser(int roleId, int interfaceId, int accessMode)
        {
            bool isAuthorized = false;
            try
            {
                switch (accessMode)
                {
                    case ((int)AccessMode.AddAccess):
                        {
                            if (entities.RoleUIActionMappings.Any(x => x.RoleId == roleId && x.InterfaceId == interfaceId && x.AddAccess == true && x.Status == true))
                            { isAuthorized = true; }
                            else
                            { isAuthorized = false; }
                            break;
                        }
                    case ((int)AccessMode.EditAccess):
                        {
                            if (entities.RoleUIActionMappings.Any(x => x.RoleId == roleId && x.InterfaceId == interfaceId && x.EditAccess == true && x.Status == true))
                            { isAuthorized = true; }
                            else
                            { isAuthorized = false; }
                            break;
                        }
                    case ((int)AccessMode.ViewAccess):
                        {
                            if (entities.RoleUIActionMappings.Any(x => x.RoleId == roleId && x.InterfaceId == interfaceId && x.ViewAccess == true && x.Status == true))
                            { isAuthorized = true; }
                            else
                            { isAuthorized = false; }
                            break;
                        }
                    default:
                        {
                            isAuthorized = false;
                            break;
                        }
                }


            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                isAuthorized = false;
                throw ex;
            }
            return isAuthorized;

        }

        public List<Country> GetCountryList()
        {
            List<Country> countryList = new List<Country>();
            try
            {
                var countries = entities.Countries.Where(x => x.Status == true).Select(s => new
                {
                    CountryId = s.CountryId,
                    CountryName = s.CountryName,
                    CountryCode = s.CountryCode
                }).ToList();
                if (countries != null && countries.Count > 0)
                {
                    foreach (var item in countries)
                    {
                        countryList.Add(new Country { CountryId = item.CountryId, CountryCode = item.CountryCode, CountryName = item.CountryName });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return countryList;
        }

        public List<State> GetStateList(int countryId)
        {
            List<State> stateList = new List<State>();
            try
            {
                var states = entities.States.Where(x => x.CountryId == countryId).Select(s => new
                {
                    StateId = s.Stateid,
                    StateName = s.StateName,
                    StateCode = s.StateCode
                }).ToList();
                if (states != null && states.Count > 0)
                {
                    foreach (var item in states)
                    {
                        stateList.Add(new State { Stateid = item.StateId, StateCode = item.StateCode, StateName = item.StateName });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return stateList;
        }
        public List<PaymentMode> GetPaymentModeList(int paymentModeId)
        {
            List<PaymentMode> paymentModeList = new List<PaymentMode>();
            try
            {
                var paymentModes = entities.PaymentModes.Where(x => x.Status == true).Select(s => new
                {
                    PaymentModeId = s.PaymentModeId,
                    PaymentModeName = s.PaymentModeName
                }).ToList();
                if (paymentModes != null && paymentModes.Count > 0)
                {
                    foreach (var item in paymentModes)
                    {
                        paymentModeList.Add(new PaymentMode { PaymentModeId = item.PaymentModeId, PaymentModeName = item.PaymentModeName });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return paymentModeList;
        }


        #region Company
        public ResponseOut AddEditCompany(Company company)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                if (entities.Companies.Any(x => x.CompanyCode == company.CompanyCode && x.CompanyId != company.CompanyId))
                {
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.DuplicateCompanyCode;
                }
                else if (entities.Companies.Any(x => x.CompanyName == company.CompanyName && x.Email == company.Email && x.CompanyId != company.CompanyId))
                {
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.DuplicateCompanyName;
                }
                else
                {
                    if (company.CompanyId == 0)
                    {
                        company.CreatedDate = DateTime.Now;
                        entities.Companies.Add(company);
                        responseOut.message = ActionMessage.CompanyCreatedSuccess;
                    }
                    else
                    {
                        entities.Companies.Where(a => a.CompanyId == company.CompanyId).ToList().ForEach(a =>
                        {
                            a.CompanyName = company.CompanyName;
                            a.ContactPersonName = company.ContactPersonName;
                            a.Phone = company.Phone;
                            a.Email = company.Email;
                            a.Fax = company.Fax;
                            a.Logo = company.Logo;
                            a.Website = company.Website;
                            a.Address = company.Address;
                            a.City = company.City;
                            a.State = company.State;
                            a.CountryId = company.CountryId;
                            a.ZipCode = company.ZipCode;
                            a.CompanyDesc = company.CompanyDesc;
                            a.PANNo = company.PANNo;
                            a.TINNo = company.TINNo;
                            a.ServiceTaxNo = company.ServiceTaxNo;
                            a.CompanyCode = company.CompanyCode;
                            a.ModifiedDate = DateTime.Now;
                        });
                        responseOut.message = ActionMessage.CompanyUpdatedSuccess;
                    }
                    entities.SaveChanges();
                    responseOut.status = ActionStatus.Success;

                }
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
        public List<Company> GetCompanyList()
        {
            List<Company> companyList = new List<Company>();
            try
            {
                var companies = entities.Companies.Select(s => new
                {
                    CompanyId = s.CompanyId,
                    CompanyName = s.CompanyName
                }).ToList();
                if (companies != null && companies.Count > 0)
                {
                    foreach (var item in companies)
                    {
                        companyList.Add(new Company { CompanyId = item.CompanyId, CompanyName = item.CompanyName });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return companyList;
        }

        #endregion
        #region CompanyBranch
        public ResponseOut AddEditCompanyBranch(ComapnyBranch comapnyBranch)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                if (comapnyBranch.CompanyBranchId == 0)
                {
                    entities.ComapnyBranches.Add(comapnyBranch);
                    responseOut.message = ActionMessage.CompanyBranchCreatedSuccess;

                }
                else
                {
                    entities.ComapnyBranches.Where(a => a.CompanyBranchId == comapnyBranch.CompanyBranchId).ToList().ForEach(a =>
                    {
                        a.BranchName = comapnyBranch.BranchName;
                        a.ContactPersonName = comapnyBranch.ContactPersonName;
                        a.Designation = comapnyBranch.Designation;
                        a.Email = comapnyBranch.Email;
                        a.MobileNo = comapnyBranch.MobileNo;
                        a.ContactNo = comapnyBranch.ContactNo;
                        a.Fax = comapnyBranch.Fax;
                        a.PrimaryAddress = comapnyBranch.PrimaryAddress;
                        a.City = comapnyBranch.City;
                        a.StateId = comapnyBranch.StateId;
                        a.CountryId = comapnyBranch.CountryId;
                        a.PinCode = comapnyBranch.PinCode;
                        a.CSTNo = comapnyBranch.CSTNo;
                        a.TINNo = comapnyBranch.TINNo;
                        a.PANNo = comapnyBranch.PANNo;
                        a.GSTNo = comapnyBranch.GSTNo;
                        a.CompanyId = comapnyBranch.CompanyId;
                        a.Status = comapnyBranch.Status;
                    });
                    responseOut.message = ActionMessage.CompanyBranchUpdatedSuccess;
                }
                entities.SaveChanges();
                responseOut.status = ActionStatus.Success;

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
        public List<ComapnyBranch> GetCompanyBranchList(int companyId)
        {
            List<ComapnyBranch> ComapnyBranchList = new List<ComapnyBranch>();
            try
            {
                var companies = entities.ComapnyBranches.Where(x => x.CompanyId == companyId && x.Status == true).Select(s => new
                {
                    CompanyBranchId = s.CompanyBranchId,
                    BranchName = s.BranchName
                }).ToList();
                if (companies != null && companies.Count > 0)
                {
                    foreach (var item in companies)
                    {
                        ComapnyBranchList.Add(new ComapnyBranch { CompanyBranchId = item.CompanyBranchId, BranchName = item.BranchName });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return ComapnyBranchList;
        }

        public List<ComapnyBranch> GetCompanyBranchByStateIdList(int companyBranchID)
        {
            List<ComapnyBranch> ComapnyBranchList = new List<ComapnyBranch>();
            try
            {
                var companies = entities.ComapnyBranches.Where(x => x.CompanyBranchId == companyBranchID && x.Status == true).Select(s => new
                {
                    StateId = s.StateId

                }).ToList();
                if (companies != null && companies.Count > 0)
                {
                    foreach (var item in companies)
                    {
                        ComapnyBranchList.Add(new ComapnyBranch { StateId = item.StateId });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return ComapnyBranchList;
        }

        public List<Company> GetCompanyIDByStateIdList(int companyID)
        {
            List<Company> ComapnyList = new List<Company>();
            try
            {
                var companies = entities.Companies.Where(x => x.CompanyId == companyID).Select(s => new
                {
                    State = s.State

                }).ToList();
                if (companies != null && companies.Count > 0)
                {
                    foreach (var item in companies)
                    {
                        ComapnyList.Add(new Company { State = item.State });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return ComapnyList;
        }

        public int GetCompanyStateId(int companyId)
        {
            int stateId = 0;
            try
            {
                stateId = (int)(entities.Companies.Where(x => x.CompanyId == companyId).Select(s => s.State).SingleOrDefault());

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return stateId;
        }
        #endregion

        #region CompanyForm
        public ResponseOut AddEditCustomerForm(CustomerForm customerForm)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                if (customerForm.CustomerFormTrnId == 0)
                {

                    customerForm.CreatedDate = DateTime.Now;
                    entities.CustomerForms.Add(customerForm);
                    responseOut.message = ActionMessage.CustomerFormCreatedSuccess;

                }
                else
                {
                    entities.CustomerForms.Where(a => a.CustomerFormTrnId == customerForm.CustomerFormTrnId).ToList().ForEach(a =>
                    {
                        a.CustomerId = customerForm.CustomerId;
                        a.InvoiceId = customerForm.InvoiceId;
                        a.FormTypeId = customerForm.FormTypeId;
                        a.RefNo = customerForm.RefNo;
                        a.RefDate = customerForm.RefDate;
                        a.Amount = customerForm.Amount;
                        a.Remarks = customerForm.Remarks;
                        a.ModifiedBy = customerForm.CreatedBy;
                        a.ModifiedDate = DateTime.Now;
                        a.FormStatus = customerForm.FormStatus;
                        a.CompanyId = customerForm.CompanyId;
                        a.Status = customerForm.Status;
                    });
                    responseOut.message = ActionMessage.CustomerFormUpdatedSuccess;
                }
                entities.SaveChanges();
                responseOut.status = ActionStatus.Success;

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
        #endregion

        #region User
        public ResponseOut AddEditUser(User user)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                if (entities.Users.Any(x => x.UserName == user.UserName && x.UserId != user.UserId))
                {
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.DuplicateUsername;
                }

                else
                {
                    if (user.UserId == 0)
                    {
                        user.CreatedDate = DateTime.Now;
                        entities.Users.Add(user);
                        responseOut.message = ActionMessage.UserCreatedSuccess;
                    }
                    else
                    {
                        user.ModifiedBy = user.CreatedBy;
                        user.ModifiedDate = DateTime.Now;

                        entities.Users.Where(a => a.UserId == user.UserId).ToList().ForEach(a =>
                        {
                            a.UserName = user.UserName;
                            a.FullName = user.FullName;
                            a.MobileNo = user.MobileNo;
                            a.Email = user.Email;
                            a.Password = user.Password;
                            a.RoleId = user.RoleId;
                            a.CompanyId = user.CompanyId;
                            a.ModifiedBy = user.ModifiedBy;
                            a.ModifiedDate = user.ModifiedDate;
                            a.ExpiryDate = user.ExpiryDate;
                            a.Status = user.Status;
                        });
                        responseOut.message = ActionMessage.UserUpdatedSuccess;
                    }
                    entities.SaveChanges();
                    responseOut.status = ActionStatus.Success;

                }
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
        #endregion

        #region Country
        public ResponseOut AddEditCountry(Country country)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                if (entities.Countries.Any(x => x.CountryCode == country.CountryCode && x.CountryId != country.CountryId))
                {
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.DuplicateCountryCode;
                }
                else if (entities.Countries.Any(x => x.CountryName == country.CountryName && x.CountryId != country.CountryId))
                {
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.DuplicateCountryName;
                }
                else
                {
                    if (country.CountryId == 0)
                    {

                        entities.Countries.Add(country);
                        responseOut.message = ActionMessage.CountryCreatedSuccess;
                    }
                    else
                    {
                        entities.Countries.Where(a => a.CountryId == country.CountryId).ToList().ForEach(a =>
                        {
                            a.CountryName = country.CountryName;
                            a.CountryCode = country.CountryCode;
                            a.Status = country.Status;
                        });
                        responseOut.message = ActionMessage.CountryUpdatedSuccess;
                    }
                    entities.SaveChanges();
                    responseOut.status = ActionStatus.Success;

                }
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
        #endregion



        #region FinYear
        public ResponseOut AddEditFinYear(FinancialYear finyear)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                if (entities.FinancialYears.Any(x => x.FinYearCode == finyear.FinYearCode && x.FinYearId != finyear.FinYearId))
                {
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.DuplicateFinYear;
                }

                else
                {
                    if (!entities.FinancialYears.Any(x => x.FinYearId == finyear.FinYearId))
                    {
                        entities.FinancialYears.Add(finyear);
                        responseOut.message = ActionMessage.FinYearCreatedSuccess;
                    }
                    else
                    {
                        entities.FinancialYears.Where(a => a.FinYearId == finyear.FinYearId).ToList().ForEach(a =>
                        {
                            a.StartDate = finyear.StartDate;
                            a.EndDate = finyear.EndDate;
                            a.FinYearCode = finyear.FinYearCode;
                            a.FinYearDesc = finyear.FinYearDesc;
                            a.Status = finyear.Status;
                        });
                        responseOut.message = ActionMessage.FinYearUpdatedSuccess;
                    }
                    entities.SaveChanges();
                    responseOut.status = ActionStatus.Success;

                }
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
        public List<FinancialYear> GetFinYearList()
        {
            List<FinancialYear> finYearList = new List<FinancialYear>();
            try
            {
                var finYears = entities.FinancialYears.Where(x => x.Status == true).Select(s => new
                {
                    FinYearId = s.FinYearId,
                    FinYearDesc = s.FinYearDesc
                }).ToList();
                if (finYears != null && finYears.Count > 0)
                {
                    foreach (var item in finYears)
                    {
                        finYearList.Add(new FinancialYear { FinYearId = item.FinYearId, FinYearDesc = item.FinYearDesc });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return finYearList;
        }

        public FinancialYear GetCurrentFinYear(int finYearId = 0)
        {
            FinancialYear currentfinYear = new FinancialYear();
            try
            {
                if (finYearId == 0)
                {
                    var finYears = entities.FinancialYears.Where(x => x.Status == true).OrderByDescending(o => o.FinYearId).Select(s => new
                    {
                        FinYearId = s.FinYearId,
                        FinYearDesc = s.FinYearDesc,
                        StartDate = s.StartDate,
                        EndDate = s.EndDate,
                        FinYearCode = s.FinYearCode
                    }).FirstOrDefault();
                    if (finYears != null)
                    {
                        currentfinYear.FinYearId = finYears.FinYearId;
                        currentfinYear.FinYearDesc = finYears.FinYearDesc;
                        currentfinYear.StartDate = finYears.StartDate;
                        currentfinYear.EndDate = finYears.EndDate;
                        currentfinYear.FinYearCode = finYears.FinYearCode;
                    }
                }
                else
                {
                    var finYears = entities.FinancialYears.Where(x => x.Status == true && x.FinYearId == finYearId).Select(s => new
                    {
                        FinYearId = s.FinYearId,
                        FinYearDesc = s.FinYearDesc,
                        StartDate = s.StartDate,
                        EndDate = s.EndDate,
                        FinYearCode = s.FinYearCode
                    }).FirstOrDefault();
                    if (finYears != null)
                    {
                        currentfinYear.FinYearId = finYears.FinYearId;
                        currentfinYear.FinYearDesc = finYears.FinYearDesc;
                        currentfinYear.StartDate = finYears.StartDate;
                        currentfinYear.EndDate = finYears.EndDate;
                        currentfinYear.FinYearCode = finYears.FinYearCode;
                    }

                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return currentfinYear;
        }


        #endregion

        #region State
        public ResponseOut AddEditState(State state)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                if (entities.States.Any(x => x.Stateid != state.Stateid && x.StateCode == state.StateCode))
                {
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.DuplicateStateCode;
                }
                else if (entities.States.Any(x => x.StateName == state.StateName && x.Stateid != state.Stateid))
                {
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.DuplicateStateName;
                }
                else
                {
                    if (state.Stateid == 0)
                    {
                        entities.States.Add(state);
                        responseOut.message = ActionMessage.StateCreatedSuccess;
                    }
                    else
                    {
                        entities.States.Where(a => a.Stateid == state.Stateid).ToList().ForEach(a =>
                        {
                            a.StateName = state.StateName;
                            a.StateCode = state.StateCode;
                            a.CountryId = state.CountryId;
                        });
                        responseOut.message = ActionMessage.StateUpdatedSuccess;
                    }
                    entities.SaveChanges();
                    responseOut.status = ActionStatus.Success;

                }
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
        #endregion

        #region PaymentMode
        public ResponseOut AddEditPaymentMode(PaymentMode paymentMode)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                //if (entities.PaymentModes.Any(x => x.PaymentModeId != paymentMode.PaymentModeId && x.PaymentModeName == paymentMode.PaymentModeName))
                //{
                //    responseOut.status = ActionStatus.Fail;
                //    responseOut.message = ActionMessage.DuplicateStateCode;
                //}
                //else
                if (entities.PaymentModes.Any(x => x.PaymentModeName == paymentMode.PaymentModeName && x.PaymentModeId != paymentMode.PaymentModeId))
                {
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.DuplicatePaymentModeName;
                }
                else
                {
                    if (paymentMode.PaymentModeId == 0)
                    {
                        entities.PaymentModes.Add(paymentMode);
                        responseOut.message = ActionMessage.PaymentModeCreatedSuccess;
                    }
                    else
                    {
                        entities.PaymentModes.Where(a => a.PaymentModeId == paymentMode.PaymentModeId).ToList().ForEach(a =>
                        {
                            a.PaymentModeName = paymentMode.PaymentModeName;
                            a.Status = paymentMode.Status;

                        });
                        responseOut.message = ActionMessage.PaymentModeUpdatedSuccess;
                    }
                    entities.SaveChanges();
                    responseOut.status = ActionStatus.Success;

                }
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
        public List<PaymentMode> GetPaymentModeList()
        {
            List<PaymentMode> paymentmodeList = new List<PaymentMode>();
            try
            {
                var paymentmodes = entities.PaymentModes.Where(x => x.Status == true).Select(s => new
                {
                    PaymentModeId = s.PaymentModeId,
                    PaymentModeName = s.PaymentModeName
                }).ToList();
                if (paymentmodes != null && paymentmodes.Count > 0)
                {
                    foreach (var item in paymentmodes)
                    {
                        paymentmodeList.Add(new PaymentMode { PaymentModeId = item.PaymentModeId, PaymentModeName = item.PaymentModeName });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return paymentmodeList;
        }
        #endregion

        #region FormType
        public List<FormType> GetFormTypeList()
        {
            List<FormType> FormTypeList = new List<FormType>();
            try
            {
                var FormType = entities.FormTypes.Where(x => x.Status == true).Select(s => new
                {
                    FormTypeId = s.FormTypeId,
                    FormTypeDesc = s.FormTypeDesc
                }).ToList();
                if (FormType != null && FormType.Count > 0)
                {
                    foreach (var item in FormType)
                    {
                        FormTypeList.Add(new FormType { FormTypeId = item.FormTypeId, FormTypeDesc = item.FormTypeDesc });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return FormTypeList;
        }
        #endregion

        #region Role
        public ResponseOut AddEditRole(Role role)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                if (entities.Roles.Any(x => x.RoleName == role.RoleName && x.CompanyId == role.CompanyId && x.RoleId != role.RoleId))
                {
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.DuplicateRoleName;
                }
                else
                {
                    if (role.RoleId == 0)
                    {
                        entities.Roles.Add(role);
                        responseOut.message = ActionMessage.RoleCreatedSuccess;
                    }
                    else
                    {
                        entities.Roles.Where(a => a.RoleId == role.RoleId).ToList().ForEach(a =>
                        {
                            a.RoleId = role.RoleId;
                            a.RoleName = role.RoleName;
                            a.RoleDesc = role.RoleDesc;
                            a.CompanyId = role.CompanyId;
                            a.IsAdmin = role.IsAdmin;
                            a.Status = role.Status;
                        });
                        responseOut.message = ActionMessage.RoleUpdatedSuccess;
                    }
                    entities.SaveChanges();
                    responseOut.status = ActionStatus.Success;
                }
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
        public List<Role> GetRoleList(int companyId)
        {
            List<Role> roleList = new List<Role>();
            try
            {
                var roles = entities.Roles.Where(x => x.CompanyId == companyId && (x.RoleId != (int)Roles.SuperAdmin && x.RoleId != (int)Roles.Admin) && x.Status == true).Select(s => new
                {
                    RoleId = s.RoleId,
                    RoleName = s.RoleName
                }).ToList();
                if (roles != null && roles.Count > 0)
                {
                    foreach (var item in roles)
                    {
                        roleList.Add(new Role { RoleId = item.RoleId, RoleName = item.RoleName });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return roleList;
        }
        #endregion

        #region Role UI Mapping
        public ResponseOut AddEditRoleUIMapping(RoleUIActionMapping roleUIActionMapping)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                if (entities.RoleUIActionMappings.Any(x => x.RoleId == roleUIActionMapping.RoleId && x.InterfaceId == roleUIActionMapping.InterfaceId))
                {
                    entities.RoleUIActionMappings.Where(a => a.RoleId == roleUIActionMapping.RoleId && a.InterfaceId == roleUIActionMapping.InterfaceId).ToList().ForEach(a =>
                    {
                        a.AddAccess = roleUIActionMapping.AddAccess;
                        a.EditAccess = roleUIActionMapping.EditAccess;
                        a.ViewAccess = roleUIActionMapping.ViewAccess;
                        a.Status = true;
                    });
                }
                else
                {
                    entities.RoleUIActionMappings.Add(roleUIActionMapping);
                }

                entities.SaveChanges();
                responseOut.message = ActionMessage.RoleUIMappingSuccessful;
                responseOut.status = ActionStatus.Success;
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

        #endregion

        #region LeadStatus
        public ResponseOut AddEditLeadStatus(LeadStatu leadstatus)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                if (entities.LeadStatus.Any(x => x.LeadStatusName == leadstatus.LeadStatusName && x.CompanyId == leadstatus.CompanyId && x.LeadStatusId != leadstatus.LeadStatusId))
                {
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.DuplicateLeadStatusName;
                }
                else
                {
                    if (leadstatus.LeadStatusId == 0)
                    {
                        entities.LeadStatus.Add(leadstatus);
                        responseOut.message = ActionMessage.LeadStatusCreatedSuccess;
                    }
                    else
                    {
                        entities.LeadStatus.Where(a => a.LeadStatusId == leadstatus.LeadStatusId).ToList().ForEach(a =>
                        {
                            a.LeadStatusId = leadstatus.LeadStatusId;
                            a.LeadStatusName = leadstatus.LeadStatusName;
                            a.CompanyId = leadstatus.CompanyId;
                            a.Status = leadstatus.Status;
                        });
                        responseOut.message = ActionMessage.LeadStatusUpdatedSuccess;
                    }
                    entities.SaveChanges();
                    responseOut.status = ActionStatus.Success;
                }
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
        #endregion


        #region LeadSource
        public ResponseOut AddEditLeadSource(LeadSource leadsource)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                if (entities.LeadSources.Any(x => x.LeadSourceName == leadsource.LeadSourceName && x.CompanyId == leadsource.CompanyId && x.LeadSourceId != leadsource.LeadSourceId))
                {
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.DuplicateLeadStatusName;
                }
                else
                {
                    if (leadsource.LeadSourceId == 0)
                    {
                        entities.LeadSources.Add(leadsource);
                        responseOut.message = ActionMessage.LeadSourceCreatedSuccess;
                    }
                    else
                    {
                        entities.LeadSources.Where(a => a.LeadSourceId == leadsource.LeadSourceId).ToList().ForEach(a =>
                        {
                            a.LeadSourceId = leadsource.LeadSourceId;
                            a.LeadSourceName = leadsource.LeadSourceName;
                            a.CompanyId = leadsource.CompanyId;
                            a.Status = leadsource.Status;
                        });
                        responseOut.message = ActionMessage.LeadSourceUpdatedSuccess;
                    }
                    entities.SaveChanges();
                    responseOut.status = ActionStatus.Success;
                }
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
        #endregion


        #region ProductType
        public ResponseOut AddEditProductType(ProductType producttype)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                if (entities.ProductTypes.Any(x => x.ProductTypeName == producttype.ProductTypeName && x.ProductTypeId != producttype.ProductTypeId))
                {
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.DuplicateProductTypeName;
                }
                else if (entities.ProductTypes.Any(x => x.ProductTypeCode == producttype.ProductTypeCode && x.ProductTypeId != producttype.ProductTypeId))
                {
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.DuplicateProductTypeCode;
                }
                else
                {
                    if (producttype.ProductTypeId == 0)
                    {
                        entities.ProductTypes.Add(producttype);
                        responseOut.message = ActionMessage.ProductTypeCreatedSuccess;
                    }
                    else
                    {
                        entities.ProductTypes.Where(a => a.ProductTypeId == producttype.ProductTypeId).ToList().ForEach(a =>
                        {
                            a.ProductTypeId = producttype.ProductTypeId;
                            a.ProductTypeName = producttype.ProductTypeName;
                            a.ProductTypeCode = producttype.ProductTypeCode;
                            a.Status = producttype.Status;
                        });
                        responseOut.message = ActionMessage.ProductTypeUpdatedSuccess;
                    }
                    entities.SaveChanges();
                    responseOut.status = ActionStatus.Success;
                }
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
        public List<ProductType> GetProductTypeList()
        {
            List<ProductType> productTypeList = new List<ProductType>();
            try
            {
                var productTypes = entities.ProductTypes.Where(x => x.Status == true).Select(s => new
                {
                    ProductTypeId = s.ProductTypeId,
                    ProductTypeName = s.ProductTypeName
                }).ToList();
                if (productTypes != null && productTypes.Count > 0)
                {
                    foreach (var item in productTypes)
                    {
                        productTypeList.Add(new ProductType { ProductTypeId = item.ProductTypeId, ProductTypeName = item.ProductTypeName });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return productTypeList;
        }
        #endregion

        #region Product Main Group 

        public ResponseOut AddEditProductMainGroup(ProductMainGroup productmaingroup)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                if (entities.ProductMainGroups.Any(x => x.ProductMainGroupName == productmaingroup.ProductMainGroupName && x.ProductMainGroupId != productmaingroup.ProductMainGroupId))
                {
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.DuplicateProductMainGroupName;
                }
                else if (entities.ProductMainGroups.Any(x => x.ProductMainGroupCode == productmaingroup.ProductMainGroupCode && x.ProductMainGroupId != productmaingroup.ProductMainGroupId))
                {
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.DuplicateProductMainGroupCode;
                }
                else
                {
                    if (productmaingroup.ProductMainGroupId == 0)
                    {
                        entities.ProductMainGroups.Add(productmaingroup);
                        responseOut.message = ActionMessage.ProductMainGroupCreatedSuccess;
                        responseOut.trnId = productmaingroup.ProductMainGroupId;
                    }
                    else
                    {
                        entities.ProductMainGroups.Where(a => a.ProductMainGroupId == productmaingroup.ProductMainGroupId).ToList().ForEach(a =>
                        {
                            a.ProductMainGroupId = productmaingroup.ProductMainGroupId;
                            a.ProductMainGroupName = productmaingroup.ProductMainGroupName;
                            a.ProductMainGroupCode = productmaingroup.ProductMainGroupCode;
                            a.ProductMainGroupDesc = productmaingroup.ProductMainGroupDesc;
                            a.Status = productmaingroup.Status;
                            responseOut.trnId = productmaingroup.ProductMainGroupId;
                        });
                        responseOut.message = ActionMessage.ProductMainGroupUpdatedSuccess;
                    }
                    entities.SaveChanges();

                    responseOut.status = ActionStatus.Success;
                }
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


        public List<ProductMainGroup> GetProductMainGroupList()
        {
            List<ProductMainGroup> productMainGroupList = new List<ProductMainGroup>();
            try
            {
                var productMainGroups = entities.ProductMainGroups.Where(x => x.Status == true).Select(s => new
                {
                    ProductMainGroupId = s.ProductMainGroupId,
                    ProductMainGroupName = s.ProductMainGroupName
                }).ToList();
                if (productMainGroups != null && productMainGroups.Count > 0)
                {
                    foreach (var item in productMainGroups)
                    {
                        productMainGroupList.Add(new ProductMainGroup { ProductMainGroupId = item.ProductMainGroupId, ProductMainGroupName = item.ProductMainGroupName });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return productMainGroupList;
        }

        public ResponseOut UpdateProductMainGroupPic(ProductMainGroup productMainGroup)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                entities.ProductMainGroups.Where(x => x.ProductMainGroupId == productMainGroup.ProductMainGroupId).ToList().ForEach(x =>
                {
                    x.ProductMainGroupImageUrl = productMainGroup.ProductMainGroupImageUrl;
                });
                entities.SaveChanges();
                responseOut.status = ActionStatus.Success;
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

        #endregion

        #region Product Sub Group

        public ResponseOut AddEditProductSubGroup(ProductSubGroup productsubgroup)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                if (entities.ProductSubGroups.Any(x => x.ProductSubGroupName == productsubgroup.ProductSubGroupName && x.ProductSubGroupId != productsubgroup.ProductSubGroupId))
                {
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.DuplicateProductSubGroupName;
                }
                //else if (entities.ProductSubGroups.Any(x => x.ProductSubGroupCode == productsubgroup.ProductSubGroupCode && x.ProductSubGroupId != productsubgroup.ProductSubGroupId))
                //{
                //    responseOut.status = ActionStatus.Fail;
                //    responseOut.message = ActionMessage.DuplicateProductSubGroupCode;
                //}
                else
                {
                    if (productsubgroup.ProductSubGroupId == 0)
                    {
                        entities.ProductSubGroups.Add(productsubgroup);
                        responseOut.message = ActionMessage.ProductSubGroupCreatedSuccess;
                    }
                    else
                    {
                        entities.ProductSubGroups.Where(a => a.ProductSubGroupId == productsubgroup.ProductSubGroupId).ToList().ForEach(a =>
                        {
                            a.ProductSubGroupId = productsubgroup.ProductSubGroupId;
                            a.ProductSubGroupName = productsubgroup.ProductSubGroupName;
                            a.ProductSubGroupCode = productsubgroup.ProductSubGroupCode;
                            a.ProductSubGroupDesc = productsubgroup.ProductSubGroupDesc;
                            a.ProductMainGroupId = productsubgroup.ProductMainGroupId;
                            a.Status = productsubgroup.Status;
                        });
                        responseOut.trnId = productsubgroup.ProductSubGroupId;
                        responseOut.message = ActionMessage.ProductSubGroupUpdatedSuccess;
                    }
                    entities.SaveChanges();
                    responseOut.trnId = productsubgroup.ProductSubGroupId;
                    responseOut.status = ActionStatus.Success;
                }
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


        public List<ProductSubGroup> GetProductSubGroupList(int productMainGroupId)
        {
            List<ProductSubGroup> productSubGroupList = new List<ProductSubGroup>();
            try
            {
                var productSubGroups = entities.ProductSubGroups.Where(x => x.Status == true && x.ProductMainGroupId == productMainGroupId).Select(s => new
                {
                    ProductSubGroupId = s.ProductSubGroupId,
                    ProductSubGroupName = s.ProductSubGroupName
                }).ToList();
                if (productSubGroups != null && productSubGroups.Count > 0)
                {
                    foreach (var item in productSubGroups)
                    {
                        productSubGroupList.Add(new ProductSubGroup { ProductSubGroupId = item.ProductSubGroupId, ProductSubGroupName = item.ProductSubGroupName });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return productSubGroupList;
        }
        public List<ProductSubChildGroup> GetProductSubChildGroupList(int productMainGroupId, int productSubGroupId)
        {
            List<ProductSubChildGroup> productSubChildGroup = new List<ProductSubChildGroup>();
            try
            {
                var productSubChildGroups = entities.ProductSubChildGroups.Where(x => x.Status == true && x.ProductMainGroupId == productMainGroupId && x.ProductSubGroupId == productSubGroupId).Select(s => new
                {
                    ProductSubChildGroupId = s.ProductSubChildGroupId,
                    ProductSubChildGroupName = s.ProductSubChildGroupName,
                    ProductSubChildGroupCode = s.ProductSubChildGroupCode,
                    ProductMainGroupId = s.ProductMainGroupId,
                    ProductSubGroupId = s.ProductSubGroupId,
                    ProductSubChildGroupDesc = s.ProductSubChildGroupDesc,
                    ProductSubChildGroupImageUrl = s.ProductSubChildGroupImageUrl//,
                                                                                 //  ProductMainGroupName = (s.ProductMainGroupName == null) ? "" : s.ProductMainGroupName

                }).ToList();
                if (productSubChildGroups != null && productSubChildGroups.Count > 0)
                {
                    foreach (var item in productSubChildGroups)
                    {
                        productSubChildGroup.Add(new ProductSubChildGroup
                        {
                            ProductSubChildGroupId = item.ProductSubChildGroupId,
                            ProductSubChildGroupName = item.ProductSubChildGroupName,
                            ProductSubChildGroupCode = item.ProductSubChildGroupCode,
                            ProductMainGroupId = item.ProductMainGroupId,
                            ProductSubGroupId = item.ProductSubGroupId,
                            ProductSubChildGroupDesc = item.ProductSubChildGroupDesc,
                            ProductSubChildGroupImageUrl = item.ProductSubChildGroupImageUrl//,
                                                                                            //  ProductMainGroupName = (item.ProductMainGroupName == null) ? "" : item.ProductMainGroupName
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return productSubChildGroup;
        }

        public ResponseOut UpdateProductSubGroupPic(ProductSubGroup productSubGroup)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                entities.ProductSubGroups.Where(x => x.ProductSubGroupId == productSubGroup.ProductSubGroupId).ToList().ForEach(x =>
                {
                    x.ProductSubGroupImageUrl = productSubGroup.ProductSubGroupImageUrl;
                });
                entities.SaveChanges();
                responseOut.status = ActionStatus.Success;
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
        #endregion

        #region ProductSubChildGroup
        public ResponseOut AddEditProductSubChildGroup(ProductSubChildGroup productSubChildGroup)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                if (entities.ProductSubChildGroups.Any(x => x.ProductSubChildGroupName == productSubChildGroup.ProductSubChildGroupName && x.ProductSubChildGroupId != productSubChildGroup.ProductSubChildGroupId && x.ProductMainGroupId == productSubChildGroup.ProductMainGroupId && x.ProductSubGroupId == productSubChildGroup.ProductSubGroupId))
                {
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.ProductSubChildGroupDuplicate;
                }
                //else if (entities.ProductSubChildGroups.Any(x => x.ProductSubChildGroupCode == productSubChildGroup.ProductSubChildGroupCode && x.ProductSubChildGroupId != productSubChildGroup.ProductSubChildGroupId && x.ProductMainGroupId == productSubChildGroup.ProductMainGroupId && x.ProductSubGroupId == productSubChildGroup.ProductSubGroupId))
                //{
                //    responseOut.status = ActionStatus.Fail;
                //    responseOut.message = ActionMessage.ProductSubChildGroupDuplicate;
                //}
                else
                {
                    if (productSubChildGroup.ProductSubChildGroupId == 0)
                    {
                        entities.ProductSubChildGroups.Add(productSubChildGroup);
                        responseOut.trnId = productSubChildGroup.ProductSubChildGroupId;
                        responseOut.message = ActionMessage.ProductSubGroupCreatedSuccess;

                    }
                    else
                    {
                        entities.ProductSubChildGroups.Where(a => a.ProductSubChildGroupId == productSubChildGroup.ProductSubChildGroupId).ToList().ForEach(a =>
                        {
                            a.ProductSubChildGroupId = productSubChildGroup.ProductSubChildGroupId;
                            a.ProductSubChildGroupName = productSubChildGroup.ProductSubChildGroupName;
                            a.ProductSubChildGroupCode = productSubChildGroup.ProductSubChildGroupCode;
                            a.ProductMainGroupId = productSubChildGroup.ProductMainGroupId;
                            a.ProductSubChildGroupId = productSubChildGroup.ProductSubChildGroupId;
                            a.ProductSubChildGroupDesc = productSubChildGroup.ProductSubChildGroupDesc;
                            a.Status = productSubChildGroup.Status;
                        });
                        responseOut.trnId = productSubChildGroup.ProductSubChildGroupId;
                        responseOut.message = ActionMessage.ProductSubChildGroupUpdatedSuccess;

                    }
                    entities.SaveChanges();
                    responseOut.trnId = productSubChildGroup.ProductSubChildGroupId;
                    responseOut.status = ActionStatus.Success;
                }
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

        public ResponseOut UpdateProductSubChildGroupPic(ProductSubChildGroup productSubChildGroup)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                entities.ProductSubChildGroups.Where(x => x.ProductSubChildGroupId == productSubChildGroup.ProductSubChildGroupId).ToList().ForEach(x =>
                {
                    x.ProductSubChildGroupImageUrl = productSubChildGroup.ProductSubChildGroupImageUrl;
                });
                entities.SaveChanges();
                responseOut.status = ActionStatus.Success;
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
        #endregion


        #region UOM 
        public ResponseOut AddEditUOM(UOM uom)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                if (entities.UOMs.Any(x => x.UOMName == uom.UOMName && x.UOMId != uom.UOMId))
                {
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.DuplicateUOMName;
                }
                else if (entities.UOMs.Any(x => x.UOMDesc == uom.UOMDesc && x.UOMId != uom.UOMId))
                {
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.DuplicateUOMDesc;
                }
                else
                {
                    if (uom.UOMId == 0)
                    {
                        entities.UOMs.Add(uom);
                        responseOut.message = ActionMessage.UOMCreatedSuccess;
                    }
                    else
                    {
                        entities.UOMs.Where(a => a.UOMId == uom.UOMId).ToList().ForEach(a =>
                        {
                            a.UOMId = uom.UOMId;
                            a.UOMName = uom.UOMName;
                            a.UOMDesc = uom.UOMDesc;
                            a.Status = uom.Status;
                        });
                        responseOut.message = ActionMessage.UOMUpdatedSuccess;
                    }
                    entities.SaveChanges();
                    responseOut.status = ActionStatus.Success;
                }
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

        public List<UOM> GetUOMList()
        {
            List<UOM> uomList = new List<UOM>();
            try
            {
                var uoms = entities.UOMs.Where(x => x.Status == true).Select(s => new
                {
                    UOMId = s.UOMId,
                    UOMName = s.UOMName,
                    IsSUM = s.IsSUM
                  
                }).ToList();
                if (uoms != null && uoms.Count > 0)
                {
                    foreach (var item in uoms)
                    {
                        uomList.Add(new UOM { UOMId = item.UOMId, UOMName = item.UOMName, IsSUM = item.IsSUM });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return uomList;
        }


        public List<Brand> GetBrandList(long ProductMainGroupId, long ProductSubGroupId, long ProductSubChildGroupId)
        {
            List<Brand> brandList = new List<Brand>();
            try
            {
                var brands = entities.Brands.Where(x => x.ProductMainGroupId == ProductMainGroupId && x.ProductSubChildGroupId == ProductSubChildGroupId && x.ProductSubGroupId == ProductSubGroupId).Select(s => new
                {
                    BrandID = s.BrandID,
                    BrandName = s.BrandName

                }).ToList();
                if (brands != null && brands.Count > 0)
                {
                    foreach (var item in brands)
                    {
                        brandList.Add(new Brand { BrandID = item.BrandID, BrandName = item.BrandName });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return brandList;
        }


        public List<ProductCode> GetProductList(long ProductMainGroupId, long ProductSubGroupId, long ProductSubChildGroupId)
        {
            List<ProductCode> ProductList = new List<ProductCode>();
            try
            {
                var items = entities.ProductCodes.Where(x => x.ProductMainGroupId == ProductMainGroupId && x.ProductSubChildGroupId == ProductSubChildGroupId && x.ProductSubGroupId == ProductSubGroupId).Select(s => new
                {
                    ProductCodeID = s.ProductCodeID,
                    ProductCodeValue = s.ProductCodeValue

                }).ToList();
                if (items != null && items.Count > 0)
                {
                    foreach (var item in items)
                    {
                        ProductList.Add(new ProductCode { ProductCodeID = item.ProductCodeID, ProductCodeValue = item.ProductCodeValue });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return ProductList;
        }

        #region GSTPerc
        public ResponseOut AddEditGSTPercent(GSTPercent gstPercent)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                if (entities.GSTPercents.Any(x => x.GSTPercentageName == gstPercent.GSTPercentageName && x.GSTPercentageID != gstPercent.GSTPercentageID))
                {
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.DuplicateGSTName;
                }
                else
                {
                    if (gstPercent.GSTPercentageID == 0)
                    {
                        entities.GSTPercents.Add(gstPercent);
                        responseOut.message = ActionMessage.GSTCreatedSuccess;
                    }
                    else
                    {
                        entities.GSTPercents.Where(a => a.GSTPercentageID == gstPercent.GSTPercentageID).ToList().ForEach(a =>
                        {
                            a.GSTPercentageID = gstPercent.GSTPercentageID;
                            a.GSTPercentageName = gstPercent.GSTPercentageName;
                            a.GSTPercentageStatus = gstPercent.GSTPercentageStatus;
                        });
                        responseOut.message = ActionMessage.GSTUpdatedSuccess;
                    }
                    entities.SaveChanges();
                    responseOut.status = ActionStatus.Success;
                }
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
        public List<GSTPercent> GetGSTPercentList()
        {
            List<GSTPercent> gstPercentList = new List<GSTPercent>();
            try
            {
                var gstPercents = entities.GSTPercents.Where(x => x.GSTPercentageStatus == true).Select(s => new
                {
                    GSTPercentageID = s.GSTPercentageID,
                    GSTPercentageName = s.GSTPercentageName,
                    GSTPercentageStatus=s.GSTPercentageStatus
                }).ToList();
                if (gstPercents != null && gstPercents.Count > 0)
                {
                    foreach (var item in gstPercents)
                    {
                        gstPercentList.Add(new GSTPercent { GSTPercentageID = item.GSTPercentageID, GSTPercentageName = item.GSTPercentageName, GSTPercentageStatus=item.GSTPercentageStatus });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return gstPercentList;
        }

        public ResponseOut RemoveGSTPercent(int gSTPercentageID)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                var gstPercents = entities.GSTPercents.First(x => x.GSTPercentageID == gSTPercentageID);
                entities.GSTPercents.Remove(gstPercents);
                entities.SaveChanges();
                responseOut.status = ActionStatus.Success;
                responseOut.message = ActionMessage.RemoveGSTPerc;
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


        #endregion

        public List<GST> GetGSTList()
        {
            List<GST> gstList = new List<GST>();
            try
            {
                var gsts = entities.GSTs.Where(x => x.GSTStatus == true).Select(s => new
                {
                    GSTID = s.GSTID,
                    GSTName = s.GSTName
                }).ToList();
                if (gsts != null && gsts.Count > 0)
                {
                    foreach (var item in gsts)
                    {
                        gstList.Add(new GST { GSTID = item.GSTID, GSTName = item.GSTName });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return gstList;
        }
        //public List<GSTPercent> GetGSTPercentList()
        //{
        //    List<GSTPercent> gstPercentList = new List<GSTPercent>();
        //    try
        //    {
        //        var gstPercents = entities.GSTPercents.Where(x => x.GSTPercentageStatus == true).Select(s => new
        //        {
        //            GSTPercentageID = s.GSTPercentageID,
        //            GSTPercentageName = s.GSTPercentageName
        //        }).ToList();
        //        if (gstPercents != null && gstPercents.Count > 0)
        //        {
        //            foreach (var item in gstPercents)
        //            {
        //                gstPercentList.Add(new GSTPercent { GSTPercentageID = item.GSTPercentageID, GSTPercentageName = item.GSTPercentageName });
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
        //        throw ex;
        //    }
        //    return gstPercentList;
        //}
        public List<DeliveryStatu> GetDeliveryStatusList()
        {
            List<DeliveryStatu> deliveryStatusList = new List<DeliveryStatu>();
            try
            {
                var deliverStatus = entities.DeliveryStatus.Where(x => x.DeliveryStatus == true).Select(s => new
                {
                    DelivryStatusID = s.DelivryStatusID,
                    DeliveryName = s.DeliveryName
                }).ToList();
                if (deliverStatus != null && deliverStatus.Count > 0)
                {
                    foreach (var item in deliverStatus)
                    {
                        deliveryStatusList.Add(new DeliveryStatu { DelivryStatusID = item.DelivryStatusID, DeliveryName = item.DeliveryName });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return deliveryStatusList;
        }

        #endregion

        #region Product
        public ResponseOut AddEditProduct(Product product, List<ProductImageDetail> productImageList, List<ProductTechSpecification> productTechSpecifications)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                if (entities.Products.Any(x => x.ProductCode == product.ProductCode && x.CompanyId == product.CompanyId && x.Productid != product.Productid))
                {
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.DuplicateProductCode;
                }

                else
                {
                    if (product.Productid == 0)
                    {
                        product.CreatedDate = DateTime.Now;
                        entities.Products.Add(product);
                        responseOut.message = ActionMessage.ProductCreatedSuccess;
                    }
                    else
                    {
                        product.ModifiedBy = product.CreatedBy;
                        product.ModifiedDate = DateTime.Now;

                        entities.ProductImageDetails.RemoveRange(entities.ProductImageDetails.Where(x => x.ProductId == product.Productid));
                        entities.SaveChanges();

                        entities.ProductTechSpecifications.RemoveRange(entities.ProductTechSpecifications.Where(x => x.ProductId == product.Productid));
                        entities.SaveChanges();

                        entities.Products.Where(a => a.Productid == product.Productid).ToList().ForEach(a =>
                        {

                            a.ProductName = product.ProductName;
                            a.ProductCode = product.ProductCode;
                            a.ProductShortDesc = product.ProductShortDesc;
                            a.ProductFullDesc = product.ProductFullDesc;
                            a.ProductTypeId = product.ProductTypeId;
                            a.ProductMainGroupId = product.ProductMainGroupId;
                            a.ProductSubGroupId = product.ProductSubGroupId;
                            a.ProductSubChildGroupId = product.ProductSubChildGroupId;
                            a.AssemblyType = product.AssemblyType;
                            a.UOMId = product.UOMId;
                            a.PurchasePrice = product.PurchasePrice;
                            a.SalePrice = product.SalePrice;
                            a.LocalTaxRate = product.LocalTaxRate;
                            a.CentralTaxRate = product.CentralTaxRate;
                            a.OtherTaxRate = product.OtherTaxRate;
                            a.IsSerializedProduct = product.IsSerializedProduct;
                            a.BrandName = product.BrandName;
                            a.ReOrderQty = product.ReOrderQty;
                            a.MinOrderQty = product.MinOrderQty;
                            a.ModifiedBy = product.ModifiedBy;
                            a.ModifiedDate = product.ModifiedDate;
                            a.Status = product.Status;
                            a.CGST_Perc = product.CGST_Perc;
                            a.SGST_Perc = product.SGST_Perc;
                            a.IGST_Perc = product.IGST_Perc;
                            a.HSN_Code = product.HSN_Code;
                            a.GST_Exempt = product.GST_Exempt;
                            a.BarCode = product.BarCode;
                            a.BarCodeImage = product.BarCodeImage;
                        });

                        responseOut.message = ActionMessage.ProductUpdatedSuccess;
                    }
                    entities.SaveChanges();
                    responseOut.trnId = product.Productid;

                    foreach (ProductImageDetail productImage in productImageList)
                    {
                        productImage.ProductId = Convert.ToInt32(product.Productid);
                        entities.ProductImageDetails.Add(productImage);
                        entities.SaveChanges();
                    }

                    foreach (ProductTechSpecification prodSpecification in productTechSpecifications)
                    {
                        prodSpecification.ProductId = Convert.ToInt32(product.Productid);
                        entities.ProductTechSpecifications.Add(prodSpecification);
                        entities.SaveChanges();
                    }
                    responseOut.status = ActionStatus.Success;

                }
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

        public List<Product> GetProductAutoCompleteList(string searchTerm, int companyId)
        {
            List<Product> productList = new List<Product>();
            try
            {
                var products = (from p in entities.Products
                                where (p.ProductName.ToLower().Contains(searchTerm.ToLower()) || p.ProductCode.ToLower().Contains(searchTerm.ToLower()) || p.ProductShortDesc.ToLower().Contains(searchTerm.ToLower())) && p.CompanyId == companyId && p.Status == true
                                select new
                                {
                                    ProductId = p.Productid,
                                    ProductName = p.ProductName,
                                    ProductCode = p.ProductCode,
                                    ProductShortDesc = p.ProductShortDesc,
                                    CGST_Perc = p.CGST_Perc,
                                    SGST_Perc = p.SGST_Perc,
                                    IGST_Perc = p.IGST_Perc,
                                    HSN_Code = p.HSN_Code
                                }).ToList();


                if (products != null && products.Count > 0)
                {
                    foreach (var item in products)
                    {
                        productList.Add(new Product { Productid = item.ProductId, ProductName = item.ProductName, ProductCode = item.ProductCode, ProductShortDesc = item.ProductShortDesc, CGST_Perc = item.CGST_Perc, SGST_Perc = item.SGST_Perc, IGST_Perc = item.IGST_Perc, HSN_Code = item.HSN_Code });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return productList;
        }

        public List<Product> GetProductAutoCompleteList(string searchTerm, int companyId, string assemblyType)
        {
            List<Product> productList = new List<Product>();
            try
            {
                if (assemblyType != "")
                {
                    var products = (from p in entities.Products
                                    where (p.ProductName.ToLower().Contains(searchTerm.ToLower()) || p.ProductCode.ToLower().Contains(searchTerm.ToLower()) || p.ProductShortDesc.ToLower().Contains(searchTerm.ToLower())) && p.CompanyId == companyId && p.AssemblyType == assemblyType && p.Status == true
                                    select new
                                    {
                                        ProductId = p.Productid,
                                        ProductName = p.ProductName,
                                        ProductCode = p.ProductCode,
                                        ProductShortDesc = p.ProductShortDesc
                                    }).ToList();
                    if (products != null && products.Count > 0)
                    {
                        foreach (var item in products)
                        {
                            productList.Add(new Product { Productid = item.ProductId, ProductName = item.ProductName, ProductCode = item.ProductCode, ProductShortDesc = item.ProductShortDesc });
                        }
                    }

                }
                else
                {
                    var products = (from p in entities.Products
                                    where (p.ProductName.ToLower().Contains(searchTerm.ToLower()) || p.ProductCode.ToLower().Contains(searchTerm.ToLower()) || p.ProductShortDesc.ToLower().Contains(searchTerm.ToLower())) && p.CompanyId == companyId && (p.AssemblyType == "MA" || p.AssemblyType == "SA") && p.Status == true
                                    select new
                                    {
                                        ProductId = p.Productid,
                                        ProductName = p.ProductName,
                                        ProductCode = p.ProductCode,
                                        ProductShortDesc = p.ProductShortDesc
                                    }).ToList();

                    if (products != null && products.Count > 0)
                    {
                        foreach (var item in products)
                        {
                            productList.Add(new Product { Productid = item.ProductId, ProductName = item.ProductName, ProductCode = item.ProductCode, ProductShortDesc = item.ProductShortDesc });
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return productList;
        }
        public List<Product> GetSubAssemblyAutoCompleteList(string searchTerm, int companyId)
        {
            List<Product> productList = new List<Product>();
            try
            {
                var products = (from p in entities.Products
                                where (p.ProductName.ToLower().Contains(searchTerm.ToLower()) || p.ProductCode.ToLower().Contains(searchTerm.ToLower()) || p.ProductShortDesc.ToLower().Contains(searchTerm.ToLower())) && p.CompanyId == companyId && (p.AssemblyType == "RC" || p.AssemblyType == "SA") && p.Status == true
                                select new
                                {
                                    ProductId = p.Productid,
                                    ProductName = p.ProductName,
                                    ProductCode = p.ProductCode,
                                    ProductShortDesc = p.ProductShortDesc
                                }).ToList();


                if (products != null && products.Count > 0)
                {
                    foreach (var item in products)
                    {
                        productList.Add(new Product { Productid = item.ProductId, ProductName = item.ProductName, ProductCode = item.ProductCode, ProductShortDesc = item.ProductShortDesc });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return productList;
        }

        public List<ProductImageDetail> GetProductImageList(int productId)
        {
            List<ProductImageDetail> productImageDetailList = new List<ProductImageDetail>();
            try
            {
                var productImageList = entities.ProductImageDetails.Where(p => p.ProductId == productId).ToList();

                if (productImageList != null && productImageList.Count > 0)
                {
                    foreach (var item in productImageList)
                    {

                        productImageDetailList.Add(
                            new ProductImageDetail
                            {
                                ProductImageDetailId = item.ProductImageDetailId,
                                ProductId = item.ProductId,
                                ImageTitle = item.ImageTitle,
                                ImageAlt = item.ImageAlt,
                                ImageNavigateUrl = item.ImageNavigateUrl,
                                ImageUrl = item.ImageUrl,
                                ImageSequence = item.ImageSequence
                            });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return productImageDetailList;
        }

        public List<Product> GetProductAutoCompleteList(string searchTerm, int productMainGroupId, int productSubGroupId, int productSubChildGroupId)
        {
            List<Product> productList = new List<Product>();
            try
            {
                var products = (from p in entities.Products
                                where (p.ProductName.ToLower().Contains(searchTerm.ToLower())) && p.ProductMainGroupId == productMainGroupId && p.ProductSubGroupId == productSubGroupId && p.ProductSubChildGroupId == productSubChildGroupId && p.Status == true
                                select new
                                {
                                    ProductId = p.Productid,
                                    ProductName = p.ProductName,
                                    ProductCode = p.ProductCode

                                }).ToList();
                if (products != null && products.Count > 0)
                {
                    foreach (var item in products)
                    {
                        productList.Add(new Product { Productid = item.ProductId, ProductName = item.ProductName, ProductCode = item.ProductCode });
                    }
                }


            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return productList;
        }

        #endregion

        #region PaymentTerm
        public ResponseOut AddEditPaymentTerm(PaymentTerm paymentterm)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                if (entities.PaymentTerms.Any(x => x.PaymentTermDesc == paymentterm.PaymentTermDesc && x.CompanyId != paymentterm.CompanyId && x.PaymentTermId == paymentterm.PaymentTermId))
                {
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.DuplicatePaymentTermDesc;
                }
                else
                {
                    if (paymentterm.PaymentTermId == 0)
                    {
                        entities.PaymentTerms.Add(paymentterm);
                        responseOut.message = ActionMessage.PaymentTermCreatedSuccess;
                    }
                    else
                    {
                        entities.PaymentTerms.Where(a => a.PaymentTermId == paymentterm.PaymentTermId).ToList().ForEach(a =>
                        {
                            a.PaymentTermId = paymentterm.PaymentTermId;
                            a.PaymentTermDesc = paymentterm.PaymentTermDesc;
                            a.Status = paymentterm.Status;
                        });
                        responseOut.message = ActionMessage.PaymentTermUpdatedSuccess;
                    }
                    entities.SaveChanges();
                    responseOut.status = ActionStatus.Success;
                }
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
        #endregion

        #region Department
        public ResponseOut AddEditDepartment(Department department)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                if (entities.Departments.Any(x => x.DepartmentName == department.DepartmentName && x.CompanyId != department.CompanyId && x.DepartmentId == department.DepartmentId))
                {
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.DuplicateDepartmentName;
                }
                else if (entities.Departments.Any(x => x.DepartmentCode == department.DepartmentCode && x.CompanyId != department.CompanyId && x.DepartmentId == department.DepartmentId))
                {
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.DuplicateDepartmentCode;
                }
                else
                {
                    if (department.DepartmentId == 0)
                    {
                        entities.Departments.Add(department);
                        responseOut.message = ActionMessage.DepartmentCreatedSuccess;
                    }
                    else
                    {
                        entities.Departments.Where(a => a.DepartmentId == department.DepartmentId).ToList().ForEach(a =>
                        {
                            a.DepartmentId = department.DepartmentId;
                            a.DepartmentName = department.DepartmentName;
                            a.DepartmentCode = department.DepartmentCode;
                            a.Status = department.Status;
                        });
                        responseOut.message = ActionMessage.DepartmentUpdatedSuccess;
                    }
                    entities.SaveChanges();
                    responseOut.status = ActionStatus.Success;
                }
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

        public List<Department> GetDepartmentList(int companyId)
        {
            List<Department> departmentList = new List<Department>();
            try
            {
                var departments = entities.Departments.Where(x => x.CompanyId == companyId && x.Status == true).Select(s => new
                {
                    DepartmentId = s.DepartmentId,
                    DepartmentName = s.DepartmentName,
                    DepartmentCode = s.DepartmentCode
                }).ToList();
                if (departments != null && departments.Count > 0)
                {
                    foreach (var item in departments)
                    {
                        departmentList.Add(new Department { DepartmentId = item.DepartmentId, DepartmentName = item.DepartmentName, DepartmentCode = item.DepartmentCode });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return departmentList;
        }

        #endregion

        #region Designation
        public ResponseOut AddEditDesignation(Designation designation)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                if (entities.Designations.Any(x => x.DesignationName == designation.DesignationName && x.DesignationId != designation.DesignationId))
                {
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.DuplicateDesignationName;
                }
                else if (entities.Designations.Any(x => x.DesignationCode == designation.DesignationCode && x.DesignationId != designation.DesignationId))
                {
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.DuplicateDesignationCode;
                }
                else
                {
                    if (designation.DesignationId == 0)
                    {
                        entities.Designations.Add(designation);
                        responseOut.message = ActionMessage.DesignationCreatedSuccess;
                    }
                    else
                    {
                        entities.Designations.Where(a => a.DesignationId == designation.DesignationId).ToList().ForEach(a =>
                        {
                            a.DesignationId = designation.DesignationId;
                            a.DesignationName = designation.DesignationName;
                            a.DesignationCode = designation.DesignationCode;
                            a.DepartmentId = designation.DepartmentId;
                            a.Status = designation.Status;
                        });
                        responseOut.message = ActionMessage.DesignationUpdatedSuccess;
                    }
                    entities.SaveChanges();
                    responseOut.status = ActionStatus.Success;
                }
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

        public List<Designation> GetDesignationList(int departmentId)
        {
            List<Designation> designationList = new List<Designation>();
            try
            {
                var designations = entities.Designations.Where(x => x.DepartmentId == departmentId && x.Status == true).Select(s => new
                {
                    DesignationId = s.DesignationId,
                    DesignationName = s.DesignationName,
                    DesignationCode = s.DesignationCode
                }).ToList();
                if (designations != null && designations.Count > 0)
                {
                    foreach (var item in designations)
                    {
                        designationList.Add(new Designation { DesignationId = item.DesignationId, DesignationName = item.DesignationName, DesignationCode = item.DesignationCode });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return designationList;
        }
        #endregion


        #region Product Opening Stock
        public ResponseOut AddEditProductOpening(ProductOpeningStock productOpeningStock)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                if (entities.ProductOpeningStocks.Any(x => x.ProductId == productOpeningStock.ProductId && x.CompanyId == productOpeningStock.CompanyId && x.CompanyBranchId == productOpeningStock.CompanyBranchId && x.FinYearId == productOpeningStock.FinYearId && x.OpeningTrnId != productOpeningStock.OpeningTrnId))
                {
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.DuplicateProductOpening;
                }

                else
                {
                    if (productOpeningStock.OpeningTrnId == 0)
                    {
                        productOpeningStock.CreatedDate = DateTime.Now;
                        entities.ProductOpeningStocks.Add(productOpeningStock);
                        responseOut.message = ActionMessage.ProductOpeningCreatedSuccess;
                    }
                    else
                    {
                        productOpeningStock.ModifiedBy = productOpeningStock.CreatedBy;
                        productOpeningStock.ModifiedDate = DateTime.Now;

                        entities.ProductOpeningStocks.Where(a => a.OpeningTrnId == productOpeningStock.OpeningTrnId).ToList().ForEach(a =>
                        {

                            a.ProductId = productOpeningStock.ProductId;
                            a.FinYearId = productOpeningStock.FinYearId;
                            a.CompanyBranchId = productOpeningStock.CompanyBranchId;
                            a.OpeningQty = productOpeningStock.OpeningQty;
                            a.ModifiedBy = productOpeningStock.ModifiedBy;
                            a.ModifiedDate = productOpeningStock.ModifiedDate;

                        });
                        responseOut.message = ActionMessage.ProductOpeningUpdatedSuccess;
                    }
                    entities.SaveChanges();
                    responseOut.status = ActionStatus.Success;

                }
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

        #endregion

        #region Lead


        public List<LeadSource> GetLeadSourceList()
        {
            List<LeadSource> leadsourceList = new List<LeadSource>();
            try
            {
                var leadsources = entities.LeadSources.Where(x => x.Status == true).Select(s => new
                {
                    LeadSourceId = s.LeadSourceId,
                    LeadSourceName = s.LeadSourceName
                }).ToList();
                if (leadsources != null && leadsources.Count > 0)
                {
                    foreach (var item in leadsources)
                    {
                        leadsourceList.Add(new LeadSource { LeadSourceId = item.LeadSourceId, LeadSourceName = item.LeadSourceName });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return leadsourceList;
        }

        public List<FollowUpActivityType> GetFollowUpActivityTypeList()
        {

            List<FollowUpActivityType> followupactivitytypelist = new List<FollowUpActivityType>();
            try
            {
                var followupactivitys = entities.FollowUpActivityTypes.Where(x => x.Status == true).Select(s => new
                {
                    FollowUpActivityTypeId = s.FollowUpActivityTypeId,
                    FollowUpActivityTypeName = s.FollowUpActivityTypeName
                }).ToList();
                if (followupactivitys != null && followupactivitys.Count > 0)
                {
                    foreach (var item in followupactivitys)
                    {
                        followupactivitytypelist.Add(new FollowUpActivityType { FollowUpActivityTypeId = item.FollowUpActivityTypeId, FollowUpActivityTypeName = item.FollowUpActivityTypeName });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return followupactivitytypelist;
        }


        public List<LeadStatu> GetLeadStatusList()
        {
            List<LeadStatu> leadstatusList = new List<LeadStatu>();
            try
            {
                var leadstatuses = entities.LeadStatus.Where(x => x.Status == true).Select(s => new
                {
                    LeadStatusId = s.LeadStatusId,
                    LeadStatusName = s.LeadStatusName
                }).ToList();
                if (leadstatuses != null && leadstatuses.Count > 0)
                {
                    foreach (var item in leadstatuses)
                    {
                        leadstatusList.Add(new LeadStatu { LeadStatusId = item.LeadStatusId, LeadStatusName = item.LeadStatusName });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return leadstatusList;
        }

        public ResponseOut AddEditLead(Lead lead, out int leadId)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                leadId = lead.LeadId;
                if (entities.Leads.Any(x => x.LeadCode == lead.LeadCode && x.CompanyId == lead.CompanyId && x.LeadId != lead.LeadId))
                {
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.DuplicateLeadCode;
                }
                else if (entities.Leads.Any(x => x.CompanyName == lead.CompanyName && x.ContactNo == lead.ContactNo && x.CompanyId == lead.CompanyId && x.LeadId != lead.LeadId))
                {
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.DuplicateLead;
                }
                else
                {
                    if (lead.LeadId == 0)
                    {
                        lead.CreatedDate = DateTime.Now;
                        entities.Leads.Add(lead);
                        responseOut.message = ActionMessage.LeadCreatedSuccess;
                    }
                    else
                    {
                        lead.ModifiedBy = lead.CreatedBy;
                        lead.ModifiedDate = DateTime.Now;
                        entities.Leads.Where(a => a.LeadId == lead.LeadId).ToList().ForEach(a =>
                        {
                            a.LeadCode = lead.LeadCode;
                            a.CompanyId = lead.CompanyId;
                            a.CompanyName = lead.CompanyName;
                            a.ContactPersonName = lead.ContactPersonName;
                            a.Email = lead.Email;
                            a.AlternateEmail = lead.AlternateEmail;
                            a.ContactNo = lead.ContactNo;
                            a.AlternateContactNo = lead.AlternateContactNo;
                            a.Fax = lead.Fax;
                            a.Designation = lead.Designation;
                            a.CompanyAddress = lead.CompanyAddress;
                            a.BranchAddress = lead.BranchAddress;
                            a.City = lead.City;
                            a.StateId = lead.StateId;
                            a.CountryId = lead.CountryId;
                            a.PinCode = lead.PinCode;
                            a.CompanyCity = lead.CompanyCity;
                            a.CompanyStateId = lead.CompanyStateId;
                            a.CompanyCountryId = lead.CompanyCountryId;
                            a.CompanyPinCode = lead.CompanyPinCode;
                            a.LeadStatusId = lead.LeadStatusId;
                            a.LeadSourceId = lead.LeadSourceId;
                            a.OtherLeadSourceDescription = lead.OtherLeadSourceDescription;
                            a.ModifiedBy = lead.ModifiedBy;
                            a.ModifiedDate = lead.ModifiedDate;
                            a.Status = lead.Status;
                        });
                        responseOut.message = ActionMessage.LeadUpdatedSuccess;
                    }
                    entities.SaveChanges();
                    leadId = lead.LeadId;
                    responseOut.status = ActionStatus.Success;

                }
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
        public ResponseOut AddEditLeadFollowUp(LeadFollowUp leadFollowUp)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                if (leadFollowUp.LeadFollowUpId == 0)
                {
                    leadFollowUp.CreatedDate = DateTime.Now;
                    entities.LeadFollowUps.Add(leadFollowUp);

                }
                else
                {
                    leadFollowUp.ModifiedDate = DateTime.Now;
                    entities.LeadFollowUps.Where(a => a.LeadFollowUpId == leadFollowUp.LeadFollowUpId).ToList().ForEach(a =>
                    {
                        a.LeadId = leadFollowUp.LeadId;
                        a.FollowUpActivityTypeId = leadFollowUp.FollowUpActivityTypeId;
                        a.FollowUpDueDateTime = leadFollowUp.FollowUpDueDateTime;
                        a.FollowUpReminderDateTime = leadFollowUp.FollowUpReminderDateTime;
                        a.FollowUpRemarks = leadFollowUp.FollowUpRemarks;
                        a.Priority = leadFollowUp.Priority;
                        a.LeadStatusId = leadFollowUp.LeadStatusId;
                        a.LeadStatusReason = leadFollowUp.LeadStatusReason;
                        a.FollowUpByUserId = leadFollowUp.FollowUpByUserId;
                        a.CreatedBy = leadFollowUp.CreatedBy;
                        a.CreatedDate = leadFollowUp.CreatedDate;
                        a.ModifiedBy = leadFollowUp.ModifiedBy;
                        a.ModifiedDate = leadFollowUp.ModifiedDate;
                    });
                }
                entities.SaveChanges();
                responseOut.status = ActionStatus.Success;
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
        public void UpdateLastLeadStatus(int leadid, int leadStatusId)
        {
            var l = (from s in entities.Leads
                     where s.LeadId == leadid
                     select s).FirstOrDefault();
            l.LeadStatusId = leadStatusId;
            entities.SaveChanges();
        }

        public List<User> GetUserAutoCompleteList(string searchTerm)
        {
            List<User> userList = new List<User>();
            try
            {
                var allusers = (from user in entities.Users
                                where ((user.FullName.ToLower().Contains(searchTerm.ToLower()) || user.UserName.ToLower().Contains(searchTerm.ToLower()) || user.MobileNo.Contains(searchTerm)) && user.Status == true)
                                select new
                                {

                                    FullName = user.FullName,
                                    UserName = user.UserName,
                                    UserId = user.UserId,
                                    MobileNo = user.MobileNo
                                }).ToList();
                if (allusers != null && allusers.Count > 0)
                {
                    foreach (var item in allusers)
                    {

                        userList.Add(new User
                        {

                            FullName = item.FullName,
                            UserName = item.UserName,
                            UserId = item.UserId,
                            MobileNo = item.MobileNo

                        });

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return userList;
        }
        #endregion

        #region Product BOM
        public ResponseOut AddEditProductBOM(ProductBOM productBOM)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                if (entities.ProductBOMs.Any(x => x.AssemblyId == productBOM.AssemblyId && x.ProductId == productBOM.ProductId && x.BOMId != productBOM.BOMId))
                {
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.DuplicateBOM;
                }

                else
                {
                    if (productBOM.BOMId == 0)
                    {
                        productBOM.CreatedDate = DateTime.Now;
                        entities.ProductBOMs.Add(productBOM);
                        responseOut.message = ActionMessage.ProductBOMCreatedSuccess;
                    }
                    else
                    {
                        productBOM.ModifiedBy = productBOM.CreatedBy;
                        productBOM.ModifiedDate = DateTime.Now;

                        entities.ProductBOMs.Where(a => a.BOMId == productBOM.BOMId).ToList().ForEach(a =>
                        {
                            a.AssemblyId = productBOM.AssemblyId;
                            a.ProductId = productBOM.ProductId;
                            a.BOMQty = productBOM.BOMQty;
                            a.CompanyId = productBOM.CompanyId;
                            a.ModifiedBy = productBOM.ModifiedBy;
                            a.ModifiedDate = productBOM.ModifiedDate;
                            a.Status = productBOM.Status;

                        });
                        responseOut.message = ActionMessage.ProductBOMUpdatedSuccess;
                    }
                    entities.SaveChanges();
                    responseOut.status = ActionStatus.Success;

                }
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
        public ResponseOut CopyProductBOM(long copyFromAssemblyId, long copyToAssemblyId, int createdBy)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                if (entities.ProductBOMs.Any(x => x.AssemblyId == copyToAssemblyId))
                {
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.DuplicateBOM;
                }

                else
                {
                    int rowsInserted = entities.proc_CopyAssemblyBOM(copyFromAssemblyId, copyToAssemblyId, createdBy);
                    if (rowsInserted > 0)
                    {
                        responseOut.message = ActionMessage.ProductBOMCopySuccess;
                        responseOut.status = ActionStatus.Success;
                    }
                    else
                    {
                        responseOut.status = ActionStatus.Fail;
                        responseOut.message = ActionMessage.ProductBOMCopyFail;
                    }
                }
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

        #endregion

        #region SLType
        public ResponseOut AddEditSLType(SLType sLType)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                if (entities.SLTypes.Any(x => x.SLTypeName == sLType.SLTypeName && x.SLTypeId != sLType.SLTypeId))
                {
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.DuplicateSLTypeName;
                }
                else
                {
                    if (sLType.SLTypeId == 0)
                    {
                        entities.SLTypes.Add(sLType);
                        responseOut.message = ActionMessage.SLTypeCreatedSuccess;
                    }
                    else
                    {
                        entities.SLTypes.Where(a => a.SLTypeId == sLType.SLTypeId).ToList().ForEach(a =>
                        {
                            a.SLTypeName = sLType.SLTypeName;
                            a.Status = sLType.Status;

                        });
                        responseOut.message = ActionMessage.SLTypeUpdatedSuccess;
                    }
                    entities.SaveChanges();
                    responseOut.status = ActionStatus.Success;

                }
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
        public List<SLType> GetSLTypeList()
        {
            List<SLType> slTypeList = new List<SLType>();
            try
            {
                var slTypes = entities.SLTypes.Where(x => x.Status == true).Select(s => new
                {
                    SLTypeId = s.SLTypeId,
                    SLTypeName = s.SLTypeName
                }).ToList();
                if (slTypes != null && slTypes.Count > 0)
                {
                    foreach (var item in slTypes)
                    {
                        slTypeList.Add(new SLType { SLTypeId = item.SLTypeId, SLTypeName = item.SLTypeName });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return slTypeList;
        }
        #endregion

        #region Services
        public ResponseOut AddEditServices(Service services)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                //if (entities.PaymentModes.Any(x => x.PaymentModeId != paymentMode.PaymentModeId && x.PaymentModeName == paymentMode.PaymentModeName))
                //{
                //    responseOut.status = ActionStatus.Fail;
                //    responseOut.message = ActionMessage.DuplicateStateCode;
                //}
                //else
                if (entities.Services.Any(x => x.ServicesName == services.ServicesName && x.ServicesId != services.ServicesId))
                {
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.DuplicateServicesName;
                }
                else
                {
                    if (services.ServicesId == 0)
                    {
                        entities.Services.Add(services);
                        responseOut.message = ActionMessage.ServicesCreatedSuccess;
                    }
                    else
                    {
                        entities.Services.Where(a => a.ServicesId == services.ServicesId).ToList().ForEach(a =>
                        {
                            a.ServicesName = services.ServicesName;
                            a.Status = services.Status;

                        });
                        responseOut.message = ActionMessage.ServicesUpdatedSuccess;
                    }
                    entities.SaveChanges();
                    responseOut.status = ActionStatus.Success;

                }
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

        #endregion

        #region City
        public ResponseOut AddEditCity(City city)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                //&& x.CityId!=city.CityId
                if (entities.Cities.Any(x => x.CityName == city.CityName && x.StateId == city.StateId))
                {
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.DuplicateCityName;
                }
                else
                {
                    if (city.CityId == 0)
                    {
                        entities.Cities.Add(city);
                        responseOut.message = ActionMessage.CityCreatedSuccess;
                    }
                    else
                    {
                        entities.Cities.Where(a => a.CityId == city.CityId).ToList().ForEach(a =>
                        {
                            a.CityName = city.CityName;
                            a.StateId = city.StateId;
                            a.Status = city.Status;

                        });
                        responseOut.message = ActionMessage.CityUpdatedSuccess;
                    }
                    entities.SaveChanges();
                    responseOut.status = ActionStatus.Success;
                }
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
        public List<City> GetCityList(int stateId)
        {
            List<City> cityList = new List<City>();
            try
            {
                var cities = entities.Cities.Where(x => x.StateId == stateId).Select(s => new
                {
                    CityId = s.CityId,
                    CityName = s.CityName
                }).ToList();
                if (cities != null && cities.Count > 0)
                {
                    foreach (var item in cities)
                    {
                        cityList.Add(new City { CityId = item.CityId, CityName = item.CityName });

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return cityList;
        }

        #endregion

        #region Employee State Mapping
        public ResponseOut AddEditEmpStateMapping(EmployeeStateMapping employeeStateMapping)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {

                if (entities.EmployeeStateMappings.Any(x => x.EmployeeStateMappingId == employeeStateMapping.EmployeeStateMappingId))
                {
                    entities.EmployeeStateMappings.Where(a => a.EmployeeStateMappingId == employeeStateMapping.EmployeeStateMappingId).ToList().ForEach(a =>
                    {
                        a.EmployeeStateMappingId = employeeStateMapping.EmployeeStateMappingId;
                        a.EmployeeId = employeeStateMapping.EmployeeId;
                        a.StateId = employeeStateMapping.StateId;
                        a.ModifiedBy = employeeStateMapping.CreatedBy;
                        a.ModifiedDate = DateTime.Now;
                        a.Status = employeeStateMapping.Status;
                    });
                }
                else
                {
                    entities.EmployeeStateMappings.Add(employeeStateMapping);
                }
                entities.SaveChanges();
                responseOut.message = ActionMessage.EmployeeStateMappingSuccessful;
                responseOut.status = ActionStatus.Success;
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

        public List<Employee> GetEmployeeAutoCompleteList(string searchTerm)
        {
            List<Employee> employeeList = new List<Employee>();
            try
            {
                var employees = (from emp in entities.Employees
                                 where ((emp.FirstName.ToLower().Contains(searchTerm.ToLower()) || emp.EmployeeCode.ToLower().Contains(searchTerm.ToLower()) || emp.MobileNo.Contains(searchTerm)) && emp.Status == true && (emp.Division == "Sale" || emp.Division == "Marketing"))
                                 select new
                                 {
                                     EmployeeId = emp.EmployeeId,
                                     FirstName = emp.FirstName + " " + emp.LastName,
                                     EmployeeCode = emp.EmployeeCode,
                                     MobileNo = emp.MobileNo
                                 }).ToList();
                if (employees != null && employees.Count > 0)
                {
                    foreach (var item in employees)
                    {

                        employeeList.Add(new Employee
                        {
                            EmployeeId = item.EmployeeId,
                            FirstName = item.FirstName,
                            EmployeeCode = item.EmployeeCode,
                            MobileNo = item.MobileNo

                        });

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return employeeList;
        }
        #endregion

        #region Employee
        public ResponseOut AddEditEmployee(Employee employee, EmployeeReportingInfo employeeReportinginfo, EmployeePayInfo employeePayInfo)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                if (entities.Employees.Any(x => x.EmployeeCode == employee.EmployeeCode && x.EmployeeId != employee.EmployeeId))
                {
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.DuplicateEmployeeCode;
                }
                else if (entities.Employees.Any(x => x.FirstName == employee.FirstName && x.LastName == employee.LastName && x.MobileNo == employee.MobileNo && x.EmployeeId != employee.EmployeeId))
                {
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.DuplicateEmployeeDetail;
                }
                else
                {
                    if (employee.EmployeeId == 0)
                    {
                        employee.CreatedDate = DateTime.Now;
                        entities.Employees.Add(employee);
                        entities.SaveChanges();
                        int employeeId = employee.EmployeeId;
                        if (employeePayInfo != null && employeePayInfo.BasicPay != 0)
                        {
                            employeePayInfo.EmployeeId = employeeId;
                            entities.EmployeePayInfoes.Add(employeePayInfo);
                            entities.SaveChanges();
                        }
                        if (employeeReportinginfo != null && employeeReportinginfo.ReportingEmployeeId != 0)
                        {
                            employeeReportinginfo.EmployeeId = employeeId;
                            entities.EmployeeReportingInfoes.Add(employeeReportinginfo);
                            entities.SaveChanges();
                        }

                        responseOut.message = ActionMessage.EmployeeCreatedSuccess;
                    }
                    else
                    {
                        employee.Modifiedby = employee.CreatedBy;
                        employee.ModifiedDate = DateTime.Now;
                        entities.Employees.Where(a => a.EmployeeId == employee.EmployeeId).ToList().ForEach(a =>
                        {
                            a.EmployeeCode = employee.EmployeeCode;
                            a.FirstName = employee.FirstName;
                            a.LastName = employee.LastName;
                            a.FatherSpouseName = employee.FatherSpouseName;
                            a.Gender = employee.Gender;
                            a.DateOfBirth = employee.DateOfBirth;
                            a.MaritalStatus = employee.MaritalStatus;
                            a.BloodGroup = employee.BloodGroup;
                            a.Email = employee.Email;
                            a.AlternateEmail = employee.AlternateEmail;
                            a.ContactNo = employee.ContactNo;
                            a.AlternateContactno = employee.AlternateContactno;
                            a.MobileNo = employee.MobileNo;
                            a.CAddress = employee.CAddress;
                            a.CCity = employee.CCity;
                            a.CStateId = employee.CStateId;
                            a.CCountryId = employee.CCountryId;
                            a.CPinCode = employee.CPinCode;
                            a.PAddress = employee.PAddress;
                            a.PCity = employee.PCity;
                            a.PStateId = employee.PStateId;
                            a.PCountryId = employee.PCountryId;
                            a.PPinCode = employee.PPinCode;
                            a.DateOfJoin = employee.DateOfJoin;
                            a.DateOfLeave = employee.DateOfLeave;
                            a.PANNo = employee.PANNo;
                            a.AadharNo = employee.AadharNo;
                            a.BankDetail = employee.BankDetail;
                            a.BankAccountNo = employee.BankAccountNo;
                            a.PFApplicable = employee.PFApplicable;
                            a.PFNo = employee.PFNo;
                            a.ESIApplicable = employee.ESIApplicable;
                            a.ESINo = employee.ESINo;
                            a.CompanyId = employee.CompanyId;
                            a.Division = employee.Division;
                            a.DepartmentId = employee.DepartmentId;
                            a.DesignationId = employee.DesignationId;
                            a.EmploymentType = employee.EmploymentType;
                            a.EmployeeCurrentStatus = employee.EmployeeCurrentStatus;
                            a.EmployeeStatusPeriod = employee.EmployeeStatusPeriod;
                            a.EmployeeStatusStartDate = employee.EmployeeStatusStartDate;
                            a.Modifiedby = employee.Modifiedby;
                            a.ModifiedDate = employee.ModifiedDate;
                            a.Status = employee.Status;

                        });
                        if (employeePayInfo != null && employeePayInfo.BasicPay != 0)
                        {
                            if (entities.EmployeePayInfoes.Any(x => x.EmployeeId == employee.EmployeeId))
                            {
                                entities.EmployeePayInfoes.Where(a => a.EmployeeId == employee.EmployeeId).ToList().ForEach(a =>
                                {
                                    a.OTRate = employeePayInfo.OTRate;
                                    a.BasicPay = employeePayInfo.BasicPay;
                                    a.HRA = employeePayInfo.HRA;
                                    a.ConveyanceAllow = employeePayInfo.ConveyanceAllow;
                                    a.SpecialAllow = employeePayInfo.SpecialAllow;
                                    a.OtherAllow = employeePayInfo.OtherAllow;
                                    a.OtherDeduction = employeePayInfo.OtherDeduction;
                                });
                            }
                            else
                            {
                                employeePayInfo.EmployeeId = employee.EmployeeId;
                                entities.EmployeePayInfoes.Add(employeePayInfo);

                            }
                        }

                        if (employeeReportinginfo != null && employeeReportinginfo.ReportingEmployeeId != 0)
                        {
                            if (entities.EmployeeReportingInfoes.Any(x => x.EmployeeId == employee.EmployeeId))
                            {
                                entities.EmployeeReportingInfoes.Where(a => a.EmployeeId == employee.EmployeeId).ToList().ForEach(a =>
                                {
                                    a.ReportingEmployeeId = employeeReportinginfo.ReportingEmployeeId;
                                });
                            }
                            else
                            {
                                employeeReportinginfo.EmployeeId = employee.EmployeeId;
                                entities.EmployeeReportingInfoes.Add(employeeReportinginfo);

                            }
                        }



                        entities.SaveChanges();
                        responseOut.message = ActionMessage.EmployeeUpdatedSuccess;
                    }

                    responseOut.status = ActionStatus.Success;
                }
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
        public List<Employee> GetEmployeeAutoCompleteList(string searchTerm, int departmentId, int designationId)
        {
            List<Employee> employeeList = new List<Employee>();
            try
            {
                var employees = (from emp in entities.Employees
                                 where ((emp.FirstName.ToLower().Contains(searchTerm.ToLower()) || emp.EmployeeCode.ToLower().Contains(searchTerm.ToLower()) || emp.MobileNo.Contains(searchTerm)) && emp.DepartmentId == (departmentId == 0 ? emp.DepartmentId : departmentId) && emp.DesignationId == (designationId == 0 ? emp.DesignationId : designationId) && emp.Status == true)
                                 select new
                                 {
                                     EmployeeId = emp.EmployeeId,
                                     FirstName = emp.FirstName + " " + emp.LastName,
                                     EmployeeCode = emp.EmployeeCode,
                                     MobileNo = emp.MobileNo
                                 }).ToList();
                if (employees != null && employees.Count > 0)
                {
                    foreach (var item in employees)
                    {

                        employeeList.Add(new Employee
                        {
                            EmployeeId = item.EmployeeId,
                            FirstName = item.FirstName,
                            EmployeeCode = item.EmployeeCode,
                            MobileNo = item.MobileNo

                        });

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return employeeList;
        }

        public ResponseOut ImportEmployee(Employee employee)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                if (entities.Employees.Any(x => x.EmployeeCode == employee.EmployeeCode && x.EmployeeId != employee.EmployeeId))
                {
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.DuplicateEmployeeCode;
                }
                else if (entities.Employees.Any(x => x.FirstName == employee.FirstName && x.LastName == employee.LastName && x.MobileNo == employee.MobileNo && x.EmployeeId != employee.EmployeeId))
                {
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.DuplicateEmployeeDetail;
                }
                else
                {
                    if (employee.EmployeeId == 0)
                    {
                        employee.CreatedDate = DateTime.Now;
                        entities.Employees.Add(employee);
                        entities.SaveChanges();
                        responseOut.message = ActionMessage.EmployeeCreatedSuccess;
                    }
                    else
                    {
                        employee.Modifiedby = employee.CreatedBy;
                        employee.ModifiedDate = DateTime.Now;
                        entities.Employees.Where(a => a.EmployeeId == employee.EmployeeId).ToList().ForEach(a =>
                        {
                            a.EmployeeCode = employee.EmployeeCode;
                            a.FirstName = employee.FirstName;
                            a.LastName = employee.LastName;
                            a.FatherSpouseName = employee.FatherSpouseName;
                            a.Gender = employee.Gender;
                            a.DateOfBirth = employee.DateOfBirth;
                            a.MaritalStatus = employee.MaritalStatus;
                            a.BloodGroup = employee.BloodGroup;
                            a.Email = employee.Email;
                            a.AlternateEmail = employee.AlternateEmail;
                            a.ContactNo = employee.ContactNo;
                            a.AlternateContactno = employee.AlternateContactno;
                            a.MobileNo = employee.MobileNo;
                            a.CAddress = employee.CAddress;
                            a.CCity = employee.CCity;
                            a.CStateId = employee.CStateId;
                            a.CCountryId = employee.CCountryId;
                            a.CPinCode = employee.CPinCode;
                            a.PAddress = employee.PAddress;
                            a.PCity = employee.PCity;
                            a.PStateId = employee.PStateId;
                            a.PCountryId = employee.PCountryId;
                            a.PPinCode = employee.PPinCode;
                            a.DateOfJoin = employee.DateOfJoin;
                            a.DateOfLeave = employee.DateOfLeave;
                            a.PANNo = employee.PANNo;
                            a.AadharNo = employee.AadharNo;
                            a.BankDetail = employee.BankDetail;
                            a.BankAccountNo = employee.BankAccountNo;
                            a.PFApplicable = employee.PFApplicable;
                            a.PFNo = employee.PFNo;
                            a.ESIApplicable = employee.ESIApplicable;
                            a.ESINo = employee.ESINo;
                            a.CompanyId = employee.CompanyId;
                            a.Division = employee.Division;
                            a.DepartmentId = employee.DepartmentId;
                            a.DesignationId = employee.DesignationId;
                            a.EmploymentType = employee.EmploymentType;
                            a.EmployeeCurrentStatus = employee.EmployeeCurrentStatus;
                            a.EmployeeStatusPeriod = employee.EmployeeStatusPeriod;
                            a.EmployeeStatusStartDate = employee.EmployeeStatusStartDate;
                            a.Modifiedby = employee.Modifiedby;
                            a.ModifiedDate = employee.ModifiedDate;
                            a.Status = employee.Status;

                        });

                        entities.SaveChanges();
                        responseOut.message = ActionMessage.EmployeeUpdatedSuccess;
                    }

                    responseOut.status = ActionStatus.Success;
                }
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
        #endregion

        #region Tax
        public ResponseOut AddEditTax(Tax tax)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                //if (entities.Taxes.Any(x => x.TaxName == tax.TaxName && x.CompanyId == tax.CompanyId && x.TaxId != tax.TaxId))
                //{
                //  responseOut.status = ActionStatus.Fail;
                //     responseOut.message = ActionMessage.DuplicateTaxName;
                //  }


                if (tax.TaxId == 0)
                {
                    tax.CreatedDate = DateTime.Now;
                    entities.Taxes.Add(tax);
                    responseOut.message = ActionMessage.TaxCreatedSuccess;
                }
                else
                {
                    tax.ModifiedBy = tax.CreatedBy;
                    tax.ModifiedDate = DateTime.Now;

                    entities.Taxes.Where(a => a.TaxId == tax.TaxId).ToList().ForEach(a =>
                    {
                        a.TaxId = tax.TaxId;
                        a.CompanyId = tax.CompanyId;
                        a.TaxName = tax.TaxName;
                        a.TaxType = tax.TaxType;
                        a.TaxSubType = tax.TaxSubType;
                        a.TaxGLCode = tax.TaxGLCode;
                        a.TaxSLCode = tax.TaxSLCode;
                        a.FormTypeId = tax.FormTypeId;
                        a.WithOutCFormTaxPercentae = tax.WithOutCFormTaxPercentae;
                        a.CFormApplicable = tax.CFormApplicable;
                        a.TaxPercentage = tax.TaxPercentage;
                        a.SurchargeName_1 = tax.SurchargeName_1;
                        a.SurchargeName_2 = tax.SurchargeName_2;
                        a.SurchargeName_3 = tax.SurchargeName_3;
                        a.SurchargePercentage_1 = tax.SurchargePercentage_1;
                        a.SurchargePercentage_2 = tax.SurchargePercentage_2;
                        a.SurchargePercentage_3 = tax.SurchargePercentage_3;
                        a.ModifiedBy = tax.ModifiedBy;
                        a.ModifiedDate = tax.ModifiedDate;
                        a.Status = tax.Status;
                    });
                    responseOut.message = ActionMessage.TaxUpdatedSuccess;
                }
                entities.SaveChanges();
                responseOut.status = ActionStatus.Success;


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

        public List<Tax> GetTaxAutoCompleteList(string searchTerm, string taxSubType, int companyId)
        {
            List<Tax> taxList = new List<Tax>();
            try
            {
                var taxes = (from tax in entities.Taxes
                             where (tax.TaxName.ToLower().Contains(searchTerm.ToLower()) && tax.CompanyId == companyId && tax.TaxSubType.ToUpper() == taxSubType.ToUpper() && tax.Status == true)
                             select new
                             {
                                 TaxId = tax.TaxId,
                                 TaxName = tax.TaxName,
                                 TaxPercentage = tax.TaxPercentage,
                                 SurchargeName_1 = tax.SurchargeName_1,
                                 SurchargePercentage_1 = tax.SurchargePercentage_1,
                                 SurchargeName_2 = tax.SurchargeName_2,
                                 SurchargePercentage_2 = tax.SurchargePercentage_2,
                                 SurchargeName_3 = tax.SurchargeName_3,
                                 SurchargePercentage_3 = tax.SurchargePercentage_3
                             }).ToList();
                if (taxes != null && taxes.Count > 0)
                {
                    foreach (var item in taxes)
                    {

                        taxList.Add(new Tax
                        {
                            TaxId = item.TaxId,
                            TaxName = item.TaxName,
                            TaxPercentage = item.TaxPercentage,
                            SurchargeName_1 = item.SurchargeName_1,
                            SurchargePercentage_1 = item.SurchargePercentage_1,
                            SurchargeName_2 = item.SurchargeName_2,
                            SurchargePercentage_2 = item.SurchargePercentage_2,
                            SurchargeName_3 = item.SurchargeName_3,
                            SurchargePercentage_3 = item.SurchargePercentage_3
                        });

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return taxList;
        }


        #endregion

        #region Customer
        public ResponseOut AddEditCustomer(Customer customer, out int customerId)
        {

            ResponseOut responseOut = new ResponseOut();
            try
            {
                customerId = customer.CustomerId;
                if (entities.Customers.Any(x => x.CustomerCode == customer.CustomerCode && x.CustomerId != customer.CustomerId))
                {
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.DuplicateCustomerCode;
                }
                else if (entities.Customers.Any(x => x.CustomerName == customer.CustomerName && x.MobileNo == customer.MobileNo && x.CustomerTypeId == customer.CustomerTypeId && x.CustomerId != customer.CustomerId))
                {
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.DuplicateCustomerDetail;
                }
                else
                {
                    if (customer.CustomerId == 0)
                    {
                        customer.CreatedDate = DateTime.Now;
                        entities.Customers.Add(customer);
                        responseOut.message = ActionMessage.CustomerCreatedSuccess;
                    }
                    else
                    {
                        customer.Modifiedby = customer.CreatedBy;
                        customer.ModifiedDate = DateTime.Now;
                        entities.Customers.Where(a => a.CustomerId == customer.CustomerId).ToList().ForEach(a =>
                        {
                            a.CustomerCode = customer.CustomerCode;
                            a.CustomerName = customer.CustomerName;
                            a.ContactPersonName = customer.ContactPersonName;
                            a.Designation = customer.Designation;
                            a.Email = customer.Email;
                            a.MobileNo = customer.MobileNo;
                            a.ContactNo = customer.ContactNo;
                            a.Fax = customer.Fax;
                            a.PrimaryAddress = customer.PrimaryAddress;
                            a.City = customer.City;
                            a.StateId = customer.StateId;
                            a.CountryId = customer.CountryId;
                            a.PinCode = customer.PinCode;
                            a.CSTNo = customer.CSTNo;
                            a.TINNo = customer.TINNo;
                            a.PANNo = customer.PANNo;
                            a.GSTNo = customer.GSTNo;
                            a.ExciseNo = customer.ExciseNo;
                            a.EmployeeId = customer.EmployeeId;
                            a.CustomerTypeId = customer.CustomerTypeId;
                            a.CreditDays = customer.CreditDays;
                            a.CreditLimit = customer.CreditLimit;
                            a.CompanyId = customer.CompanyId;
                            a.Modifiedby = customer.Modifiedby;
                            a.ModifiedDate = customer.ModifiedDate;
                            a.Status = customer.Status;

                        });
                        responseOut.message = ActionMessage.CustomerUpdatedSuccess;
                    }
                    entities.SaveChanges();
                    customerId = customer.CustomerId;
                    responseOut.status = ActionStatus.Success;
                }
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
        public ResponseOut AddEditCustomerBranch(CustomerBranch customerBranch)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                if (customerBranch.CustomerBranchId == 0)
                {
                    entities.CustomerBranches.Add(customerBranch);
                }
                else
                {
                    entities.CustomerBranches.Where(a => a.CustomerBranchId == customerBranch.CustomerBranchId).ToList().ForEach(a =>
                    {
                        a.BranchName = customerBranch.BranchName;
                        a.ContactPersonName = customerBranch.ContactPersonName;
                        a.Designation = customerBranch.Designation;
                        a.Email = customerBranch.Email;
                        a.MobileNo = customerBranch.MobileNo;
                        a.ContactNo = customerBranch.ContactNo;
                        a.Fax = customerBranch.Fax;
                        a.PrimaryAddress = customerBranch.PrimaryAddress;
                        a.City = customerBranch.City;
                        a.StateId = customerBranch.StateId;
                        a.CountryId = customerBranch.CountryId;
                        a.PinCode = customerBranch.PinCode;
                        a.CSTNo = customerBranch.CSTNo;
                        a.TINNo = customerBranch.TINNo;
                        a.PANNo = customerBranch.PANNo;
                        a.GSTNo = customerBranch.GSTNo;
                    });

                }
                entities.SaveChanges();
                responseOut.status = ActionStatus.Success;

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
        public ResponseOut AddEditCustomerProduct(CustomerProductMapping customerProduct)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                if (customerProduct.MappingId == 0)
                {
                    entities.CustomerProductMappings.Add(customerProduct);
                }
                else
                {
                    entities.CustomerProductMappings.Where(a => a.MappingId == customerProduct.MappingId).ToList().ForEach(a =>
                    {
                        a.ProductId = customerProduct.ProductId;
                    });

                }
                entities.SaveChanges();
                responseOut.status = ActionStatus.Success;

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
        public ResponseOut RemoveCustomerBranch(long customerBranchId)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                if (customerBranchId == 0)
                {

                }
                else
                {
                    entities.CustomerBranches.Where(a => a.CustomerBranchId == customerBranchId).ToList().ForEach(a =>
                    {
                        a.Status = false;

                    });
                    entities.SaveChanges();
                }

                responseOut.status = ActionStatus.Success;
                responseOut.message = ActionMessage.CustomerBranchRemovedSuccess;

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
        public ResponseOut RemoveCustomerProduct(long mappingId)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                if (mappingId == 0)
                {

                }
                else
                {
                    entities.CustomerProductMappings.Where(a => a.MappingId == mappingId).ToList().ForEach(a =>
                    {
                        a.Status = false;

                    });
                    entities.SaveChanges();
                }

                responseOut.status = ActionStatus.Success;
                responseOut.message = ActionMessage.CustomerProductRemovedSuccess;

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
        public List<Customer> GetCustomerAutoCompleteList(string searchTerm, int companyId)
        {
            List<Customer> customerList = new List<Customer>();
            try
            {
                var customers = (from cust in entities.Customers
                                 where ((cust.CustomerName.ToLower().Contains(searchTerm.ToLower()) || cust.CustomerCode.ToLower().Contains(searchTerm.ToLower())) && cust.CompanyId == companyId && cust.Status == true)
                                 select new
                                 {
                                     CustomerId = cust.CustomerId,
                                     CustomerName = cust.CustomerName,
                                     CustomerCode = cust.CustomerCode,
                                     PrimaryAddress = cust.PrimaryAddress
                                 }).ToList();
                if (customers != null && customers.Count > 0)
                {
                    foreach (var item in customers)
                    {

                        customerList.Add(new Customer
                        {
                            CustomerId = item.CustomerId,
                            CustomerName = item.CustomerName,
                            CustomerCode = item.CustomerCode,
                            PrimaryAddress = item.PrimaryAddress

                        });

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return customerList;
        }
        public List<CustomerBranch> GetCustomerBranchList(int customerId)
        {
            List<CustomerBranch> customerList = new List<CustomerBranch>();
            try
            {
                var customerBranches = entities.CustomerBranches.Where(x => x.CustomerId == customerId && x.Status == true).Select(s => new
                {
                    CustomerBranchId = s.CustomerBranchId,
                    BranchName = s.BranchName
                }).ToList();
                if (customerBranches != null && customerBranches.Count > 0)
                {
                    foreach (var item in customerBranches)
                    {
                        customerList.Add(new CustomerBranch { CustomerBranchId = item.CustomerBranchId, BranchName = item.BranchName });

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return customerList;
        }
        public CustomerBranch GetCustomerBranchDetail(long customerBranchId)
        {
            CustomerBranch customerBranch = new CustomerBranch();
            try
            {
                var customerBranches = entities.CustomerBranches.Where(x => x.CustomerBranchId == customerBranchId).Select(s => new
                {
                    PrimaryAddress = s.PrimaryAddress,
                    BranchId = s.CustomerBranchId,
                    BranchName = s.BranchName,
                    City = s.City,
                    StateId = s.StateId,
                    CountryId = s.CountryId,
                    PinCode = s.PinCode,
                    TINNo = s.TINNo,
                    ContactPersonName = s.ContactPersonName,
                    Email = s.Email,
                    MobileNo = s.MobileNo,
                    ContactNo = s.ContactNo,
                    Fax = s.Fax,
                    GSTNo = s.GSTNo
                }).FirstOrDefault();
                if (customerBranches != null)
                {
                    customerBranch = new CustomerBranch
                    {
                        PrimaryAddress = customerBranches.PrimaryAddress,
                        CustomerBranchId = customerBranches.BranchId,
                        BranchName = customerBranches.BranchName,
                        City = customerBranches.City,
                        StateId = customerBranches.StateId,
                        CountryId = customerBranches.CountryId,
                        PinCode = customerBranches.PinCode,
                        TINNo = customerBranches.TINNo,
                        ContactPersonName = customerBranches.ContactPersonName,
                        Email = customerBranches.Email,
                        MobileNo = customerBranches.MobileNo,
                        ContactNo = customerBranches.ContactNo,
                        Fax = customerBranches.Fax,
                        GSTNo = customerBranches.GSTNo
                    };

                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return customerBranch;
        }


        public List<Customer> GetCustomerAutoCompleteForPaymentModeList(string searchTerm, int companyId)
        {
            List<Customer> customerList = new List<Customer>();
            try
            {
                var customers = (from cust in entities.Customers
                                 where ((cust.CustomerName.ToLower().Contains(searchTerm.ToLower()) || cust.CustomerCode.ToLower().Contains(searchTerm.ToLower())) && cust.CompanyId == companyId && cust.Status == true)
                                 select new
                                 {
                                     CustomerId = cust.CustomerId,
                                     CustomerName = cust.CustomerName,
                                 }).ToList();
                if (customers != null && customers.Count > 0)
                {
                    foreach (var item in customers)
                    {

                        customerList.Add(new Customer
                        {
                            CustomerId = item.CustomerId,
                            CustomerName = item.CustomerName,


                        });

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return customerList;
        }

        //Customer Pop Up Master------------
        public ResponseOut AddEditCustomerMaster(Customer customer)
        {

            ResponseOut responseOut = new ResponseOut();
            try
            {
                if (entities.Customers.Any(x => x.CustomerCode == customer.CustomerCode))
                {
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.DuplicateCustomerCode;
                }
                else if (entities.Customers.Any(x => x.CustomerName == customer.CustomerName && x.MobileNo == customer.MobileNo && x.CustomerTypeId == customer.CustomerTypeId))
                {
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.DuplicateCustomerDetail;
                }
                else
                {
                    customer.CreatedDate = DateTime.Now;
                    entities.Customers.Add(customer);
                    responseOut.message = ActionMessage.CustomerCreatedSuccess;
                    entities.SaveChanges();
                    responseOut.trnId = customer.CustomerId;
                    responseOut.status = ActionStatus.Success;
                }
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

        public ResponseOut AddEditCustomerBranchMaster(CustomerBranch customerBranch)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                if (customerBranch.CustomerBranchId == 0)
                {
                    entities.CustomerBranches.Add(customerBranch);
                }
                entities.SaveChanges();
                responseOut.trnId = customerBranch.CustomerBranchId;
                // responseOut.message = ActionMessage.CustomerBranchCreatedSuccess;
                responseOut.status = ActionStatus.Success;

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

        public List<Customer> GetCustomerDetailsById(int customerId, int companyId)
        {
            List<Customer> customerList = new List<Customer>();
            try
            {
                var customers = entities.Customers.Where(x => x.CustomerId == customerId && x.CompanyId == companyId && x.Status == true)
                               .Select(cust => new
                               {
                                   CustomerId = cust.CustomerId,
                                   CustomerName = cust.CustomerName,
                                   CustomerCode = cust.CustomerCode,
                                   PrimaryAddress = cust.PrimaryAddress
                               }).ToList();

                if (customers != null && customers.Count > 0)
                {
                    foreach (var item in customers)
                    {

                        customerList.Add(new Customer
                        {
                            CustomerId = item.CustomerId,
                            CustomerName = item.CustomerName,
                            CustomerCode = item.CustomerCode,
                            PrimaryAddress = item.PrimaryAddress
                        });

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return customerList;
        }

        #endregion

        #region Customer Type
        public List<CustomerType> GetCustomerTypeList()
        {
            List<CustomerType> customerTypeList = new List<CustomerType>();
            try
            {
                var customerTypes = entities.CustomerTypes.Where(x => x.Status == true).Select(s => new
                {
                    CustomerTypeId = s.CustomerTypeId,
                    CustomerTypeDesc = s.CustomerTypeDesc
                }).ToList();
                if (customerTypes != null && customerTypes.Count > 0)
                {
                    foreach (var item in customerTypes)
                    {
                        customerTypeList.Add(new CustomerType { CustomerTypeId = item.CustomerTypeId, CustomerTypeDesc = item.CustomerTypeDesc });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return customerTypeList;
        }

        public ResponseOut AddEditCustomerType(CustomerType customertype)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                if (entities.CustomerTypes.Any(x => x.CustomerTypeId != customertype.CustomerTypeId && x.CustomerTypeDesc == customertype.CustomerTypeDesc))
                {
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.DuplicateCustomerTypeDesc;
                }
                else
                {
                    if (customertype.CustomerTypeId == 0)
                    {
                        entities.CustomerTypes.Add(customertype);
                        responseOut.message = ActionMessage.CustomerTypeCreatedSuccess;
                    }
                    else
                    {
                        entities.CustomerTypes.Where(a => a.CustomerTypeId == customertype.CustomerTypeId).ToList().ForEach(a =>
                        {
                            a.CustomerTypeId = customertype.CustomerTypeId;
                            a.CustomerTypeDesc = customertype.CustomerTypeDesc;
                            a.Status = customertype.Status;

                        });
                        responseOut.message = ActionMessage.CustomerTypeUpdatedSuccess;
                    }
                    entities.SaveChanges();
                    responseOut.status = ActionStatus.Success;

                }
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


        #endregion

        #region Book
        public ResponseOut AddEditBook(Book book)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                if (entities.Books.Any(x => x.BookName == book.BookName && x.CompanyId == book.CompanyId && x.BookId != book.BookId))
                {
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.DuplicateBookName;
                }
                else if (entities.Books.Any(x => x.BookCode == book.BookCode && x.CompanyId == book.CompanyId && x.BookId != book.BookId))
                {
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.DuplicateBookCode;
                }
                else if (entities.Books.Any(x => x.GLCode == book.GLCode && x.CompanyId == book.CompanyId && x.BookId != book.BookId))
                {
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.DuplicateBookGLCode;
                }

                else
                {
                    if (book.BookId == 0)
                    {
                        book.CreatedDate = DateTime.Now;
                        entities.Books.Add(book);
                        responseOut.message = ActionMessage.BookCreatedSuccess;
                    }
                    else
                    {
                        book.Modifiedby = book.CreatedBy;
                        book.ModifiedDate = DateTime.Now;

                        entities.Books.Where(a => a.BookId == book.BookId).ToList().ForEach(a =>
                        {
                            a.BookId = book.BookId;
                            a.CompanyId = book.CompanyId;
                            a.BookName = book.BookName;
                            a.BookType = book.BookType;
                            a.BookCode = book.BookCode;
                            a.BankBranch = book.BankBranch;
                            a.BankAccountNo = book.BankAccountNo;
                            a.GLCode = book.GLCode;
                            a.IFSC = book.IFSC;
                            a.OverDraftLimit = book.OverDraftLimit;
                            a.Modifiedby = book.Modifiedby;
                            a.ModifiedDate = book.ModifiedDate;
                            a.Status = book.Status;
                        });
                        responseOut.message = ActionMessage.BookUpdatedSuccess;
                    }
                    entities.SaveChanges();
                    responseOut.status = ActionStatus.Success;


                }
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


        public List<Book> GetBookList(int companyId)
        {
            List<Book> bookList = new List<Book>();
            try
            {
                var books = entities.Books.Where(x => x.Status == true && x.CompanyId == companyId && (x.BookType == "C" || x.BookType == "B")).Select(s => new
                {
                    BookId = s.BookId,
                    BookName = s.BookName,
                    BankAccountNo = s.BankAccountNo,
                    BankBranch = s.BankBranch

                }).ToList();
                if (books != null && books.Count > 0)
                {
                    foreach (var item in books)
                    {
                        bookList.Add(new Book { BookId = item.BookId, BookName = item.BookName, BankAccountNo = item.BankAccountNo, BankBranch = item.BankBranch });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return bookList;
        }
        public List<Book> GetBookList(string bookType, int companyId)
        {
            List<Book> bookList = new List<Book>();
            try
            {
                var books = entities.Books.Where(x => x.Status == true && x.BookType == bookType && x.CompanyId == companyId && (x.BookType == "C" || x.BookType == "B")).Select(s => new
                {
                    BookId = s.BookId,
                    BookName = s.BookName,
                    BankAccountNo = s.BankAccountNo,
                    BankBranch = s.BankBranch

                }).ToList();
                if (books != null && books.Count > 0)
                {
                    foreach (var item in books)
                    {
                        bookList.Add(new Book { BookId = item.BookId, BookName = item.BookName, BankAccountNo = item.BankAccountNo, BankBranch = item.BankBranch });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return bookList;
        }

        public List<Book> GetBookAutoCompleteList(string searchTerm, string bookType, int companyId)
        {
            List<Book> bookList = new List<Book>();
            try
            {
                var books = (from cust in entities.Books
                             where ((cust.BookName.ToLower().Contains(searchTerm.ToLower()) || cust.BankBranch.ToLower().Contains(searchTerm.ToLower()) || cust.IFSC.ToLower().Contains(searchTerm.ToLower())) && cust.CompanyId == companyId && cust.BookType == bookType && cust.Status == true)
                             select new
                             {
                                 BookId = cust.BookId,
                                 BookCode = cust.BookCode,
                                 BookName = cust.BookName,
                                 BankBranch = cust.BankBranch,
                                 IFSC = cust.IFSC
                             }).ToList();
                if (books != null && books.Count > 0)
                {
                    foreach (var item in books)
                    {

                        bookList.Add(new Book
                        {
                            BookId = item.BookId,
                            BookCode = item.BookCode,
                            BookName = item.BookName,
                            BankBranch = item.BankBranch,
                            IFSC = item.IFSC
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return bookList;
        }

        public List<GL> GetBookGLAutoCompleteList(string searchTerm, int companyId)
        {
            List<GL> glList = new List<GL>();
            try
            {
                var gls = (from p in entities.GLs
                           where (p.GLCode.ToLower().Contains(searchTerm.ToLower()) || p.GLHead.ToLower().Contains(searchTerm.ToLower())) && p.IsBookGL == true && p.CompanyId == companyId && p.Status == true
                           select new
                           {
                               GLId = p.GLId,
                               GLHead = p.GLHead,
                               GLCode = p.GLCode,
                               SLTypeId = p.SLTypeId
                           }).ToList();


                if (gls != null && gls.Count > 0)
                {
                    foreach (var item in gls)
                    {
                        glList.Add(new GL { GLId = item.GLId, GLHead = item.GLHead, GLCode = item.GLCode, SLTypeId = item.SLTypeId });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return glList;
        }









        #endregion

        #region Vendor
        public ResponseOut AddEditVendor(Vendor vendor, out int vendorId)
        {

            ResponseOut responseOut = new ResponseOut();
            try
            {
                vendorId = vendor.VendorId;
                if (entities.Vendors.Any(x => x.VendorCode == vendor.VendorCode && x.VendorId != vendor.VendorId))
                {
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.DuplicateVendorCode;
                }
                else if (entities.Vendors.Any(x => x.VendorName == vendor.VendorName && x.MobileNo == vendor.MobileNo && x.VendorId != vendor.VendorId))
                {
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.DuplicateVendorDetail;
                }
                else
                {
                    if (vendor.VendorId == 0)
                    {
                        vendor.CreatedDate = DateTime.Now;
                        entities.Vendors.Add(vendor);
                        responseOut.message = ActionMessage.VendorCreatedSuccess;
                    }
                    else
                    {
                        vendor.Modifiedby = vendor.CreatedBy;
                        vendor.ModifiedDate = DateTime.Now;
                        entities.Vendors.Where(a => a.VendorId == vendor.VendorId).ToList().ForEach(a =>
                        {
                            a.VendorCode = vendor.VendorCode;
                            a.VendorName = vendor.VendorName;
                            a.ContactPersonName = vendor.ContactPersonName;
                            a.Email = vendor.Email;
                            a.MobileNo = vendor.MobileNo;
                            a.ContactNo = vendor.ContactNo;
                            a.Fax = vendor.Fax;
                            a.Address = vendor.Address;
                            a.City = vendor.City;
                            a.StateId = vendor.StateId;
                            a.CountryId = vendor.CountryId;
                            a.PinCode = vendor.PinCode;
                            a.CSTNo = vendor.CSTNo;
                            a.TINNo = vendor.TINNo;
                            a.PANNo = vendor.PANNo;
                            a.GSTNo = vendor.GSTNo;
                            a.ExciseNo = vendor.ExciseNo;
                            a.CompanyId = vendor.CompanyId;
                            a.CreditDays = vendor.CreditDays;
                            a.CreditLimit = vendor.CreditLimit;
                            a.Modifiedby = vendor.Modifiedby;
                            a.ModifiedDate = vendor.ModifiedDate;
                            a.Status = vendor.Status;

                        });
                        responseOut.message = ActionMessage.VendorUpdatedSuccess;
                    }
                    entities.SaveChanges();
                    vendorId = vendor.VendorId;
                    responseOut.status = ActionStatus.Success;
                }
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


        public ResponseOut AddEditVendorProduct(VendorProductMapping vendorProduct)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                if (vendorProduct.MappingId == 0)
                {
                    entities.VendorProductMappings.Add(vendorProduct);
                }
                else
                {
                    entities.VendorProductMappings.Where(a => a.MappingId == vendorProduct.MappingId).ToList().ForEach(a =>
                    {
                        a.ProductId = vendorProduct.ProductId;
                    });

                }
                entities.SaveChanges();
                responseOut.status = ActionStatus.Success;

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



        public List<Vendor> GetVendorAutoCompleteList(string searchTerm, int companyId)
        {
            List<Vendor> vendorList = new List<Vendor>();
            try
            {
                var vendors = (from vend in entities.Vendors
                               where ((vend.VendorName.ToLower().Contains(searchTerm.ToLower()) || vend.VendorCode.ToLower().Contains(searchTerm.ToLower())) && vend.CompanyId == companyId && vend.Status == true)
                               select new
                               {
                                   VendorId = vend.VendorId,
                                   VendorName = vend.VendorName,
                                   VendorCode = vend.VendorCode,
                                   Address = vend.Address

                               }).ToList();
                if (vendors != null && vendors.Count > 0)
                {
                    foreach (var item in vendors)
                    {

                        vendorList.Add(new Vendor
                        {
                            VendorId = item.VendorId,
                            VendorName = item.VendorName,
                            VendorCode = item.VendorCode,
                            Address = item.Address
                        });

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return vendorList;
        }
        #endregion

        #region GL Main Group 

        public ResponseOut AddEditGLMainGroup(GLMainGroup glmaingroup)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                if (entities.GLMainGroups.Any(x => x.GLMainGroupName == glmaingroup.GLMainGroupName && x.CompanyId == glmaingroup.CompanyId && x.GLMainGroupId != glmaingroup.GLMainGroupId))
                {
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.DuplicateGLMainGroupName;
                }
                else if (entities.GLMainGroups.Any(x => x.GLType == glmaingroup.GLType && x.CompanyId == glmaingroup.CompanyId && x.GLMainGroupName == glmaingroup.GLMainGroupName && x.GLMainGroupId != glmaingroup.GLMainGroupId))
                {
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.DuplicateGLType;
                }
                else
                {
                    if (glmaingroup.GLMainGroupId == 0)
                    {
                        glmaingroup.CreatedDate = DateTime.Now;
                        entities.GLMainGroups.Add(glmaingroup);
                        responseOut.message = ActionMessage.GLMainGroupCreatedSuccess;
                    }
                    else
                    {
                        glmaingroup.ModifiedBy = glmaingroup.CreatedBy;
                        glmaingroup.ModifiedDate = DateTime.Now;
                        entities.GLMainGroups.Where(a => a.GLMainGroupId == glmaingroup.GLMainGroupId).ToList().ForEach(a =>
                        {
                            a.GLMainGroupId = glmaingroup.GLMainGroupId;
                            a.GLMainGroupName = glmaingroup.GLMainGroupName;
                            a.GLType = glmaingroup.GLType;
                            a.SequenceNo = glmaingroup.SequenceNo;
                            a.CompanyId = glmaingroup.CompanyId;
                            a.Status = glmaingroup.Status;
                        });
                        responseOut.message = ActionMessage.GLMainGroupUpdatedSuccess;
                    }
                    entities.SaveChanges();
                    responseOut.status = ActionStatus.Success;
                }
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
        #endregion

        #region GL Sub Group 
        public List<GLMainGroup> GetGLMainGroupList()
        {
            List<GLMainGroup> glmaingroupList = new List<GLMainGroup>();
            try
            {
                var glmaingroups = entities.GLMainGroups.Where(x => x.Status == true).Select(s => new
                {
                    GLMainGroupId = s.GLMainGroupId,
                    GLMainGroupName = s.GLMainGroupName,
                    GLType = s.GLType,
                    SequenceNo = s.SequenceNo
                }).ToList();
                if (glmaingroups != null && glmaingroups.Count > 0)
                {
                    foreach (var item in glmaingroups)
                    {
                        glmaingroupList.Add(new GLMainGroup { GLMainGroupId = item.GLMainGroupId, GLMainGroupName = item.GLMainGroupName, GLType = item.GLType, SequenceNo = item.SequenceNo });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return glmaingroupList;
        }


        public ResponseOut AddEditGLSubGroup(GLSubGroup glSubgroup)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                if (entities.GLSubGroups.Any(x => x.GLSubGroupName == glSubgroup.GLSubGroupName && x.CompanyId == glSubgroup.CompanyId && x.GLSubGroupId != glSubgroup.GLSubGroupId))
                {
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.DuplicateGLSubGroupName;
                }
                else if (entities.GLSubGroups.Any(x => x.GLMainGroupId == glSubgroup.GLMainGroupId && x.CompanyId == glSubgroup.CompanyId && x.GLSubGroupName == glSubgroup.GLSubGroupName && x.GLMainGroupId != glSubgroup.GLMainGroupId))
                {
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.DuplicateGLMainGroupId;
                }
                else
                {
                    if (glSubgroup.GLSubGroupId == 0)
                    {
                        glSubgroup.CreatedDate = DateTime.Now;
                        entities.GLSubGroups.Add(glSubgroup);
                        responseOut.message = ActionMessage.GLSubGroupCreatedSuccess;
                    }
                    else
                    {
                        glSubgroup.ModifiedBy = glSubgroup.CreatedBy;
                        glSubgroup.ModifiedDate = DateTime.Now;
                        entities.GLSubGroups.Where(a => a.GLSubGroupId == glSubgroup.GLSubGroupId).ToList().ForEach(a =>
                        {
                            a.GLSubGroupId = glSubgroup.GLSubGroupId;
                            a.GLSubGroupName = glSubgroup.GLSubGroupName;
                            a.GLMainGroupId = glSubgroup.GLMainGroupId;
                            a.SequenceNo = glSubgroup.SequenceNo;
                            a.CompanyId = glSubgroup.CompanyId;
                            a.Status = glSubgroup.Status;
                        });
                        responseOut.message = ActionMessage.GLSubGroupUpdatedSuccess;
                    }
                    entities.SaveChanges();
                    responseOut.status = ActionStatus.Success;
                }
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

        #endregion

        #region QuotationSetting

        //public List<User> GetUserAutoCompleteList(string searchTerm)
        //{
        //    List<User> userList = new List<User>();
        //    try
        //    {
        //        var users = (from usr in entities.Users
        //                     where ((usr.UserName.ToLower().Contains(searchTerm.ToLower()) || usr.FullName.ToLower().Contains(searchTerm.ToLower()) || usr.MobileNo.Contains(searchTerm))  && usr.Status == true)
        //                         select new
        //                         {
        //                             UserId = usr.UserId,
        //                             UserName = usr.UserName,
        //                             FullName = usr.FullName,
        //                             MobileNo = usr.MobileNo
        //                         }).ToList();
        //        if (users != null && users.Count > 0)
        //        {
        //            foreach (var item in users)
        //            {

        //                userList.Add(new User
        //                {
        //                    UserId = item.UserId,
        //                    UserName = item.UserName,
        //                    FullName = item.FullName,
        //                    MobileNo = item.MobileNo

        //                });

        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
        //        throw ex;
        //    }
        //    return userList;
        //}

        public ResponseOut AddEditQuotationSetting(QuotationSetting quotationsetting)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                if (entities.QuotationSettings.Any(x => x.Status == true && x.CompanyId == quotationsetting.CompanyId && x.QuotationSettingId != quotationsetting.QuotationSettingId))
                {
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.QuotationSettingExist;
                }
                else
                {
                    if (quotationsetting.QuotationSettingId == 0)
                    {
                        quotationsetting.CreatedDate = DateTime.Now;
                        entities.QuotationSettings.Add(quotationsetting);
                        responseOut.message = ActionMessage.QuotationSettingCreatedSuccess;
                    }
                    else
                    {
                        quotationsetting.ModifiedBy = quotationsetting.CreatedBy;
                        quotationsetting.ModifiedDate = DateTime.Now;

                        entities.QuotationSettings.Where(a => a.QuotationSettingId == quotationsetting.QuotationSettingId).ToList().ForEach(a =>
                        {
                            a.QuotationSettingId = quotationsetting.QuotationSettingId;
                            a.CompanyId = quotationsetting.CompanyId;
                            a.NormalApprovalRequired = quotationsetting.NormalApprovalRequired;
                            a.NormalApprovalByUserId = quotationsetting.NormalApprovalByUserId;
                            a.RevisedApprovalRequired = quotationsetting.RevisedApprovalRequired;
                            a.RevisedApprovalByUserId = quotationsetting.RevisedApprovalByUserId;
                            a.ModifiedBy = quotationsetting.ModifiedBy;
                            a.ModifiedDate = quotationsetting.ModifiedDate;
                            a.Status = quotationsetting.Status;
                        });
                        responseOut.message = ActionMessage.QuotationSettingUpdatedSuccess;
                    }
                    entities.SaveChanges();
                    responseOut.status = ActionStatus.Success;


                }
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

        #endregion

        #region Term Template
        public ResponseOut RemoveTermTemplate(long trnId)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                if (trnId == 0)
                {

                }
                else
                {
                    entities.TermTemplateDetails.Where(a => a.TrnId == trnId).ToList().ForEach(a =>
                    {
                        a.Status = false;

                    });
                    entities.SaveChanges();
                }

                responseOut.status = ActionStatus.Success;
                responseOut.message = ActionMessage.TermTemplateRemovedSuccess;

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


        public ResponseOut AddEditTermTemplate(TermsTemplate termtemplate, out int termtemplateId)
        {

            ResponseOut responseOut = new ResponseOut();
            try
            {
                termtemplateId = termtemplate.TermTemplateId;
                if (entities.TermsTemplates.Any(x => x.TermTempalteName == termtemplate.TermTempalteName && x.TermTemplateId != termtemplate.TermTemplateId))
                {
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.DuplicateTermTempalteName;
                }

                else
                {
                    if (termtemplate.TermTemplateId == 0)
                    {
                        termtemplate.CreatedDate = DateTime.Now;
                        entities.TermsTemplates.Add(termtemplate);
                        responseOut.message = ActionMessage.TermTemplateCreatedSuccess;
                    }
                    else
                    {
                        termtemplate.ModifiedBy = termtemplate.CreatedBy;
                        termtemplate.ModifiedDate = DateTime.Now;
                        entities.TermsTemplates.Where(a => a.TermTemplateId == termtemplate.TermTemplateId).ToList().ForEach(a =>
                        {
                            a.TermTempalteName = termtemplate.TermTempalteName;
                            a.CompanyId = termtemplate.CompanyId;
                            a.ModifiedBy = termtemplate.ModifiedBy;
                            a.ModifiedDate = termtemplate.ModifiedDate;
                            a.Status = termtemplate.Status;
                        });
                        responseOut.message = ActionMessage.TermTemplateUpdatedSuccess;
                    }
                    entities.SaveChanges();
                    termtemplateId = termtemplate.TermTemplateId;
                    responseOut.status = ActionStatus.Success;
                }
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


        public ResponseOut AddEditTermTemplateDetail(TermTemplateDetail termTemplate)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                if (termTemplate.TrnId == 0)
                {
                    entities.TermTemplateDetails.Add(termTemplate);
                }
                else
                {
                    entities.TermTemplateDetails.Where(a => a.TrnId == termTemplate.TrnId).ToList().ForEach(a =>
                    {
                        a.TermTemplateId = termTemplate.TermTemplateId;
                    });

                }
                entities.SaveChanges();
                responseOut.status = ActionStatus.Success;

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

        public List<TermsTemplate> GetTermTemplateList(int companyId)
        {
            List<TermsTemplate> termList = new List<TermsTemplate>();
            try
            {
                var terms = entities.TermsTemplates.Where(x => x.Status == true && x.CompanyId == companyId).Select(s => new
                {
                    TermTemplateId = s.TermTemplateId,
                    TermTempalteName = s.TermTempalteName
                }).ToList();
                if (terms != null && terms.Count > 0)
                {
                    foreach (var item in terms)
                    {
                        termList.Add(new TermsTemplate { TermTemplateId = item.TermTemplateId, TermTempalteName = item.TermTempalteName });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return termList;
        }
        public List<TermTemplateDetail> GetTermTemplateDetailList(int termTemplateId)
        {
            List<TermTemplateDetail> termDescList = new List<TermTemplateDetail>();
            try
            {
                var termDesc = entities.TermTemplateDetails.Where(x => x.Status == true && x.TermTemplateId == termTemplateId).Select(s => new
                {
                    TrnId = s.TrnId,
                    TermsDesc = s.TermsDesc
                }).ToList();
                if (termDesc != null && termDesc.Count > 0)
                {
                    foreach (var item in termDesc)
                    {
                        termDescList.Add(new TermTemplateDetail { TrnId = item.TrnId, TermsDesc = item.TermsDesc });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return termDescList;
        }
        #endregion

        #region Document Type
        public ResponseOut AddEditDocumentType(DocumentType documenttype)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                if (entities.DocumentTypes.Any(x => x.DocumentTypeId != documenttype.DocumentTypeId && x.DocumentTypeDesc == documenttype.DocumentTypeDesc && x.CompanyId == documenttype.CompanyId))
                {
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.DuplicateCustomerTypeDesc;
                }
                else
                {
                    if (documenttype.DocumentTypeId == 0)
                    {
                        entities.DocumentTypes.Add(documenttype);
                        responseOut.message = ActionMessage.DocumentTypeCreatedSuccess;
                    }
                    else
                    {
                        entities.DocumentTypes.Where(a => a.DocumentTypeId == documenttype.DocumentTypeId).ToList().ForEach(a =>
                        {
                            a.DocumentTypeId = documenttype.DocumentTypeId;
                            a.DocumentTypeDesc = documenttype.DocumentTypeDesc;
                            a.CompanyId = documenttype.CompanyId;
                            a.Status = documenttype.Status;

                        });
                        responseOut.message = ActionMessage.DocumentTypeUpdatedSuccess;
                    }
                    entities.SaveChanges();
                    responseOut.status = ActionStatus.Success;

                }
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

        #endregion

        #region SO Setting



        public ResponseOut AddEditSOSetting(SOSetting sosetting)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                if (entities.SOSettings.Any(x => x.Status == true && x.CompanyId == sosetting.CompanyId && x.SOSettingId != sosetting.SOSettingId))
                {
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.SOSettingExist;
                }
                else
                {
                    if (sosetting.SOSettingId == 0)
                    {
                        sosetting.CreatedDate = DateTime.Now;
                        entities.SOSettings.Add(sosetting);
                        responseOut.message = ActionMessage.SOSettingCreatedSuccess;
                    }
                    else
                    {
                        sosetting.ModifiedBy = sosetting.CreatedBy;
                        sosetting.ModifiedDate = DateTime.Now;

                        entities.SOSettings.Where(a => a.SOSettingId == sosetting.SOSettingId).ToList().ForEach(a =>
                        {
                            a.SOSettingId = sosetting.SOSettingId;
                            a.CompanyId = sosetting.CompanyId;
                            a.NormalApprovalRequired = sosetting.NormalApprovalRequired;
                            a.NormalApprovalByUserId = sosetting.NormalApprovalByUserId;
                            a.RevisedApprovalRequired = sosetting.RevisedApprovalRequired;
                            a.RevisedApprovalByUserId = sosetting.RevisedApprovalByUserId;
                            a.ModifiedBy = sosetting.ModifiedBy;
                            a.ModifiedDate = sosetting.ModifiedDate;
                            a.Status = sosetting.Status;
                        });
                        responseOut.message = ActionMessage.SOSettingUpdatedSuccess;
                    }
                    entities.SaveChanges();
                    responseOut.status = ActionStatus.Success;


                }
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

        #endregion

        #region Product State Tax Mapping
        public ResponseOut AddEditProductTaxMapping(ProductSubCategoryStateTaxMapping productSubCategoryStateTaxMapping)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {

                if (productSubCategoryStateTaxMapping.MappingId == 0)
                {
                    productSubCategoryStateTaxMapping.CreatedDate = DateTime.Now;
                    entities.ProductSubCategoryStateTaxMappings.Add(productSubCategoryStateTaxMapping);
                    responseOut.message = ActionMessage.ProductStateTaxMappingCreatedSuccessful;
                }
                else
                {
                    entities.ProductSubCategoryStateTaxMappings.Where(a => a.ProductSubGroupId == productSubCategoryStateTaxMapping.ProductSubGroupId).ToList().ForEach(a =>
                    {

                        a.ProductSubGroupId = productSubCategoryStateTaxMapping.ProductSubGroupId;
                        a.StateId = productSubCategoryStateTaxMapping.StateId;
                        a.TaxId = productSubCategoryStateTaxMapping.TaxId;
                        a.CompanyId = productSubCategoryStateTaxMapping.CompanyId;
                        a.Status = productSubCategoryStateTaxMapping.Status;
                        a.CreatedBy = productSubCategoryStateTaxMapping.CreatedBy;
                        a.CreatedDate = DateTime.Now;

                    });
                    responseOut.message = ActionMessage.ProductStateTaxMappingUpdateSuccessful;

                }
                entities.SaveChanges();
                responseOut.status = ActionStatus.Success;
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




        #endregion

        //#region Customer Payment

        //public ResponseOut AddEditCustomerPayment(CustomerPayment customerpayment)
        //{

        //    ResponseOut responseOut = new ResponseOut();
        //    try
        //    { 

        //        if (entities.CustomerPayments.Any(x => x.InvoiceId == customerpayment.InvoiceId && x.RefNo == customerpayment.RefNo && x.CustomerId == customerpayment.CustomerId && x.PaymentTrnId != customerpayment.PaymentTrnId))
        //        {
        //            responseOut.status = ActionStatus.Fail;
        //            responseOut.message = ActionMessage.DuplicateCustomerPaymentDetail;
        //        }
        //        else
        //        {
        //            if (customerpayment.PaymentTrnId == 0)
        //            {
        //                customerpayment.CreatedDate = DateTime.Now;
        //                entities.CustomerPayments.Add(customerpayment);
        //                responseOut.message = ActionMessage.CustomerPaymentCreatedSuccess;
        //            }
        //            else
        //            {
        //                customerpayment.ModifiedBy = customerpayment.CreatedBy;
        //                customerpayment.ModifiedDate = DateTime.Now;
        //                entities.CustomerPayments.Where(a => a.PaymentTrnId == customerpayment.PaymentTrnId).ToList().ForEach(a =>
        //                {
        //                    a.PaymentNo = customerpayment.PaymentNo;
        //                    a.PaymentDate= customerpayment.PaymentDate;
        //                    a.CustomerId = customerpayment.CustomerId;
        //                    a.InvoiceId = customerpayment.InvoiceId;
        //                    a.BookId = customerpayment.BookId;
        //                    a.PaymentModeId = customerpayment.PaymentModeId;
        //                    a.RefNo = customerpayment.RefNo;
        //                    a.RefDate = customerpayment.RefDate;
        //                    a.Remarks = customerpayment.Remarks;
        //                    a.ValueDate = customerpayment.ValueDate;
        //                    a.Amount = customerpayment.Amount;
        //                    a.CompanyId = customerpayment.CompanyId;
        //                    a.ModifiedBy = customerpayment.ModifiedBy;
        //                    a.ModifiedDate = customerpayment.ModifiedDate;
        //                    a.Status = customerpayment.Status;

        //                });
        //                responseOut.message = ActionMessage.CustomerPaymentUpdatedSuccess;
        //            }
        //            entities.SaveChanges(); 
        //            responseOut.status = ActionStatus.Success;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        responseOut.status = ActionStatus.Fail;
        //        responseOut.message = ActionMessage.ApplicationException;
        //        Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
        //        throw ex;
        //    }
        //    return responseOut;
        //}
        //#endregion

        #region FollowUpActivityType
        public ResponseOut AddEditFollowUpActivityType(FollowUpActivityType followUpActivityType)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                //if (entities.PaymentModes.Any(x => x.PaymentModeId != paymentMode.PaymentModeId && x.PaymentModeName == paymentMode.PaymentModeName))
                //{
                //    responseOut.status = ActionStatus.Fail;
                //    responseOut.message = ActionMessage.DuplicateStateCode;
                //}
                //else
                if (entities.FollowUpActivityTypes.Any(x => x.FollowUpActivityTypeName == followUpActivityType.FollowUpActivityTypeName && x.FollowUpActivityTypeId != followUpActivityType.FollowUpActivityTypeId))
                {
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.DuplicateFollowUpActivityTypeName;
                }
                else
                {
                    if (followUpActivityType.FollowUpActivityTypeId == 0)
                    {
                        entities.FollowUpActivityTypes.Add(followUpActivityType);
                        responseOut.message = ActionMessage.FollowUpActivityTypeCreatedSuccess;
                    }
                    else
                    {
                        entities.FollowUpActivityTypes.Where(a => a.FollowUpActivityTypeId == followUpActivityType.FollowUpActivityTypeId).ToList().ForEach(a =>
                        {
                            a.FollowUpActivityTypeName = followUpActivityType.FollowUpActivityTypeName;
                            a.Status = followUpActivityType.Status;

                        });
                        responseOut.message = ActionMessage.FollowUpActivityTypeUpdatedSuccess;
                    }
                    entities.SaveChanges();
                    responseOut.status = ActionStatus.Success;

                }
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

        #endregion

        #region Schedule
        public ResponseOut AddEditSchedule(Schedule schedule)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                if (entities.Schedules.Any(x => x.ScheduleName == schedule.ScheduleName && x.CompanyId == schedule.CompanyId && x.ScheduleId != schedule.ScheduleId))
                {
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.DuplicateGLMainGroupName;
                }
                else if (entities.Schedules.Any(x => x.CompanyId == schedule.CompanyId && x.ScheduleName == schedule.ScheduleName && x.ScheduleId != schedule.ScheduleId))
                {
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.DuplicateGLType;
                }
                else
                {
                    if (schedule.ScheduleId == 0)
                    {
                        schedule.CreatedDate = DateTime.Now;
                        entities.Schedules.Add(schedule);
                        responseOut.message = ActionMessage.ScheduleCreatedSuccess;
                    }
                    else
                    {
                        schedule.ModifiedBy = schedule.CreatedBy;
                        schedule.ModifiedDate = DateTime.Now;
                        entities.Schedules.Where(a => a.ScheduleId == schedule.ScheduleId).ToList().ForEach(a =>
                        {
                            a.ScheduleId = schedule.ScheduleId;
                            a.ScheduleName = schedule.ScheduleName;
                            a.CompanyId = schedule.CompanyId;
                            a.Status = schedule.Status;
                        });
                        responseOut.message = ActionMessage.ScheduleUpdatedSuccess;
                    }
                    entities.SaveChanges();
                    responseOut.status = ActionStatus.Success;
                }
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
        public List<Schedule> GetScheduleList()
        {
            // List<GLMainGroup> glmaingroupList = new List<GLMainGroup>();
            List<Schedule> scheduleList = new List<Schedule>();
            try
            {
                var schedule = entities.Schedules.Where(x => x.Status == true).Select(s => new
                {
                    ScheduleId = s.ScheduleId,
                    ScheduleName = s.ScheduleName,
                    //GLType = s.GLType,
                    //SequenceNo = s.SequenceNo
                }).ToList();
                if (schedule != null && schedule.Count > 0)
                {
                    foreach (var item in schedule)
                    {
                        scheduleList.Add(new Schedule { ScheduleId = item.ScheduleId, ScheduleName = item.ScheduleName });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return scheduleList;
        }
        #endregion

        #region Form Type
        public ResponseOut AddEditFormType(FormType formType)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                if (entities.FormTypes.Any(x => x.FormTypeDesc == formType.FormTypeDesc && x.CompanyId == formType.CompanyId && x.FormTypeId != formType.FormTypeId))
                {
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.DuplicateFormType;
                }
                else if (entities.FormTypes.Any(x => x.CompanyId == formType.CompanyId && x.FormTypeDesc == formType.FormTypeDesc && x.FormTypeId != formType.FormTypeId))
                {
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.DuplicateFormType;
                }
                else
                {
                    if (formType.FormTypeId == 0)
                    {

                        entities.FormTypes.Add(formType);
                        responseOut.message = ActionMessage.FormTypeCreatedSuccess;
                    }
                    else
                    {

                        entities.FormTypes.Where(a => a.FormTypeId == formType.FormTypeId).ToList().ForEach(a =>
                        {
                            a.FormTypeId = formType.FormTypeId;
                            a.FormTypeDesc = formType.FormTypeDesc;
                            a.CompanyId = formType.CompanyId;
                            a.Status = formType.Status;
                        });
                        responseOut.message = ActionMessage.FormTypeUpdatedSuccess;
                    }
                    entities.SaveChanges();
                    responseOut.status = ActionStatus.Success;
                }
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
        #endregion

        #region Change Password
        public ResponseOut VerifyOldPassword(int userId, string oldPassword)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                if (entities.Users.Any(x => x.UserId == userId && x.Password != oldPassword))
                {
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.PasswordDoesNotMatch;
                }

                else
                {
                    responseOut.status = ActionStatus.Success;
                }
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
        public ResponseOut ChangePassword(User user)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {

                user.ModifiedBy = user.CreatedBy;
                user.ModifiedDate = DateTime.Now;

                entities.Users.Where(a => a.UserId == user.UserId).ToList().ForEach(a =>
                {
                    a.Password = user.Password;
                    a.ModifiedBy = user.ModifiedBy;
                    a.ModifiedDate = user.ModifiedDate;

                });
                responseOut.message = ActionMessage.PasswordChangeSuccessfully;

                entities.SaveChanges();
                responseOut.status = ActionStatus.Success;


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
        #endregion

        #region UserEmailSetting
        public ResponseOut AddEditUserEmailSetting(UserEmailSetting userEmailSetting)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                if (entities.UserEmailSettings.Any(x => x.SmtpUser == userEmailSetting.SmtpUser && x.SettingId != userEmailSetting.SettingId))
                {
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.DuplicateUserEmailSetting;
                }

                else
                {
                    if (userEmailSetting.SettingId == 0)
                    {
                        userEmailSetting.CreatedDate = DateTime.Now;
                        entities.UserEmailSettings.Add(userEmailSetting);
                        responseOut.message = ActionMessage.UserEmailSettingCreatedSuccess;
                    }
                    else
                    {
                        userEmailSetting.ModifiedBy = userEmailSetting.CreatedBy;
                        userEmailSetting.ModifiedDate = DateTime.Now;

                        entities.UserEmailSettings.Where(a => a.SettingId == userEmailSetting.SettingId).ToList().ForEach(a =>
                        {
                            a.SmtpUser = userEmailSetting.SmtpUser;
                            a.SmtpDisplayName = userEmailSetting.SmtpDisplayName;
                            a.SmtpPass = userEmailSetting.SmtpPass;
                            a.SmtpServer = userEmailSetting.SmtpServer;
                            a.EnableSsl = userEmailSetting.EnableSsl;
                            a.SmtpPort = userEmailSetting.SmtpPort;
                            a.SmtpDisplayName = userEmailSetting.SmtpDisplayName;
                            a.Status = userEmailSetting.Status;
                        });
                        responseOut.message = ActionMessage.UserEmailSettingUpdatedSuccess;
                    }
                    entities.SaveChanges();
                    responseOut.status = ActionStatus.Success;

                }
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
        public List<User> GetUserEmailAutoCompleteList(string searchTerm)
        {
            List<User> userList = new List<User>();
            try
            {
                var allusers = (from user in entities.Users
                                where ((user.FullName.ToLower().Contains(searchTerm.ToLower()) || user.UserName.ToLower().Contains(searchTerm.ToLower()) || user.MobileNo.Contains(searchTerm)) && user.Status == true)
                                select new
                                {

                                    FullName = user.FullName,
                                    UserName = user.UserName,
                                    UserId = user.UserId,
                                    MobileNo = user.MobileNo,
                                    Email = user.Email

                                }).ToList();
                if (allusers != null && allusers.Count > 0)
                {
                    foreach (var item in allusers)
                    {

                        userList.Add(new User
                        {

                            FullName = item.FullName,
                            UserName = item.UserName,
                            UserId = item.UserId,
                            MobileNo = item.MobileNo,
                            Email = item.Email

                        });

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return userList;
        }
        #endregion

        #region VendorForm
        public ResponseOut AddEditVendorForm(VendorForm vendorForm)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                if (vendorForm.VendorFormTrnId == 0)
                {
                    vendorForm.CreatedDate = DateTime.Now;
                    entities.VendorForms.Add(vendorForm);
                    responseOut.message = ActionMessage.VendorFormCreatedSuccess;

                }
                else
                {
                    entities.VendorForms.Where(a => a.VendorFormTrnId == vendorForm.VendorFormTrnId).ToList().ForEach(a =>
                    {
                        a.VendorId = vendorForm.VendorId;
                        a.InvoiceId = vendorForm.InvoiceId;
                        a.FormTypeId = vendorForm.FormTypeId;
                        a.RefNo = vendorForm.RefNo;
                        a.RefDate = vendorForm.RefDate;
                        a.Amount = vendorForm.Amount;
                        a.Remarks = vendorForm.Remarks;
                        a.CreatedBy = vendorForm.CreatedBy;
                        a.FormStatus = vendorForm.FormStatus;
                        a.CompanyId = vendorForm.CompanyId;
                        a.ModifiedBy = vendorForm.CreatedBy;
                        a.ModifiedDate = DateTime.Now;
                    });
                    responseOut.message = ActionMessage.VendorUpdatedSuccess;
                }
                entities.SaveChanges();
                responseOut.status = ActionStatus.Success;

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
        #endregion

        #region GL
        public List<GLSubGroup> GetGLSubGroupList(int mainGroupId)
        {
            List<GLSubGroup> glsubgroupList = new List<GLSubGroup>();
            try
            {
                var glsubgroup = entities.GLSubGroups.Where(x => x.GLMainGroupId == mainGroupId).Select(s => new
                {
                    GLSubGroupId = s.GLSubGroupId,
                    GLSubGroupName = s.GLSubGroupName

                }).ToList();

                if (glsubgroup != null && glsubgroup.Count > 0)
                {
                    foreach (var item in glsubgroup)
                    {
                        glsubgroupList.Add(new GLSubGroup { GLSubGroupId = item.GLSubGroupId, GLSubGroupName = item.GLSubGroupName });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return glsubgroupList;
        }

        public List<GL> GetGLAutoCompleteList(string searchTerm, int companyId)
        {
            List<GL> glList = new List<GL>();
            try
            {
                var gls = (from p in entities.GLs
                           where (p.GLCode.ToLower().Contains(searchTerm.ToLower()) || p.GLHead.ToLower().Contains(searchTerm.ToLower())) && p.CompanyId == companyId && p.Status == true
                           select new
                           {
                               GLId = p.GLId,
                               GLHead = p.GLHead,
                               GLCode = p.GLCode,
                               SLTypeId = p.SLTypeId
                           }).ToList();


                if (gls != null && gls.Count > 0)
                {
                    foreach (var item in gls)
                    {
                        glList.Add(new GL { GLId = item.GLId, GLHead = item.GLHead, GLCode = item.GLCode, SLTypeId = item.SLTypeId });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return glList;
        }
        #endregion
        public List<GL> GetGLAutoCompleteList(string searchTerm, int slTypeId, int companyId)
        {
            List<GL> glList = new List<GL>();
            try
            {
                var sls = (from p in entities.GLs
                           where ((p.GLCode.ToLower().Contains(searchTerm.ToLower()) || p.GLHead.ToLower().Contains(searchTerm.ToLower())) && p.CompanyId == companyId && p.SLTypeId == slTypeId && p.Status == true)
                           select new
                           {
                               GLId = p.GLId,
                               GLHead = p.GLHead,
                               GLCode = p.GLCode
                           }).ToList();


                if (sls != null && sls.Count > 0)
                {
                    foreach (var item in sls)
                    {
                        glList.Add(new GL { GLId = item.GLId, GLHead = item.GLHead, GLCode = item.GLCode });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return glList;
        }
        #region Forgot Password

        public User GetUserFromEmail(string userEmail)
        {
            User user = new User();
            try
            {
                if (entities.Users.Any(x => x.Email == userEmail))
                {
                    user = entities.Users.Where(u => u.Email == userEmail).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return user;
        }
        #endregion


        #region SL
        public ResponseOut AddEditSL(SL sL)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                if (entities.SLs.Any(x => x.SLCode == sL.SLCode && x.SLId != sL.SLId))
                {
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.DuplicateSLName;
                }
                else
                {
                    if (sL.SLId == 0)
                    {
                        sL.CreatedDate = DateTime.Now;
                        entities.SLs.Add(sL);
                        responseOut.message = ActionMessage.SLCreatedSuccess;
                    }
                    else
                    {
                        sL.ModifiedBy = sL.CreatedBy;
                        sL.ModifiedDate = DateTime.Now;
                        entities.SLs.Where(a => a.SLId == sL.SLId).ToList().ForEach(a =>
                        {
                            a.SLCode = sL.SLCode;
                            a.SLHead = sL.SLHead;
                            a.RefCode = sL.RefCode;
                            a.SLTypeId = sL.SLTypeId;
                            a.CostCenterId = sL.CostCenterId;
                            a.ModifiedBy = sL.ModifiedBy;
                            a.ModifiedDate = sL.ModifiedDate;
                            a.Status = sL.Status;

                        });
                        responseOut.message = ActionMessage.SLUpdatedSuccess;
                    }
                    entities.SaveChanges();
                    responseOut.status = ActionStatus.Success;

                }
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


        public List<SLType> GetSLTList()
        {
            List<SLType> slTypeList = new List<SLType>();
            try
            {
                var slTypes = entities.SLTypes.Where(x => x.Status == true && x.SLTypeId != 0).Select(s => new
                {
                    SLTypeId = s.SLTypeId,
                    SLTypeName = s.SLTypeName
                }).ToList();
                if (slTypes != null && slTypes.Count > 0)
                {
                    foreach (var item in slTypes)
                    {
                        slTypeList.Add(new SLType { SLTypeId = item.SLTypeId, SLTypeName = item.SLTypeName });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return slTypeList;
        }

        public List<CostCenter> GetCostCenterList()
        {
            List<CostCenter> costCenterList = new List<CostCenter>();
            try
            {
                var costCenters = entities.CostCenters.Where(x => x.Status == true).Select(s => new
                {
                    CostCenterId = s.CostCenterId,
                    CostCenterName = s.CostCenterName

                }).ToList();

                if (costCenters != null && costCenters.Count > 0)
                {
                    foreach (var item in costCenters)
                    {
                        costCenterList.Add(new CostCenter { CostCenterId = item.CostCenterId, CostCenterName = item.CostCenterName });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return costCenterList;
        }

        public List<SL> GetSLAutoCompleteList(string searchTerm, int slTypeId, int companyId)
        {
            List<SL> slList = new List<SL>();
            try
            {
                var sls = (from p in entities.SLs
                           where (p.SLCode.ToLower().Contains(searchTerm.ToLower()) || p.SLHead.ToLower().Contains(searchTerm.ToLower()) || p.RefCode.ToLower().Contains(searchTerm.ToLower())) && p.CompanyId == companyId && p.SLTypeId == slTypeId && p.Status == true
                           select new
                           {
                               SLId = p.SLId,
                               SLHead = p.SLHead,
                               SLCode = p.SLCode
                           }).ToList();


                if (sls != null && sls.Count > 0)
                {
                    foreach (var item in sls)
                    {
                        slList.Add(new SL { SLId = item.SLId, SLHead = item.SLHead, SLCode = item.SLCode });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return slList;
        }

        #endregion

        #region Cot Center
        public List<CostCenter> GetCostCenterList(int companyId)
        {
            List<CostCenter> costCenterList = new List<CostCenter>();
            try
            {
                var costCenters = entities.CostCenters.Where(x => x.CompanyId == companyId && x.Status == true).Select(s => new
                {
                    CostCenterId = s.CostCenterId,
                    CostCenterName = s.CostCenterName
                }).ToList();

                if (costCenters != null && costCenters.Count > 0)
                {
                    foreach (var item in costCenters)
                    {
                        costCenterList.Add(new CostCenter { CostCenterId = item.CostCenterId, CostCenterName = item.CostCenterName });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return costCenterList;
        }
        #endregion

        #region Sale Invoice
        public ResponseOut CancelSaleInvoice(SaleInvoice saleinvoice)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {

                entities.SaleInvoices.Where(a => a.InvoiceId == saleinvoice.InvoiceId).ToList().ForEach(a =>
                {

                    a.ApprovalStatus = saleinvoice.ApprovalStatus;
                    a.CancelStatus = saleinvoice.CancelStatus;
                    a.CancelBy = saleinvoice.CreatedBy;
                    a.CancelDate = DateTime.Now;
                    a.CancelReason = saleinvoice.CancelReason;
                });
                responseOut.message = ActionMessage.SaleInvoiceCancelSuccess;

                entities.SaveChanges();
                responseOut.status = ActionStatus.Success;

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

        public ComapnyBranch GetCompanyBranchDetails(int companyBranchID)
        {
            ComapnyBranch ComapnyBranchList = new ComapnyBranch();
            try
            {
                var companies = entities.ComapnyBranches.Where(x => x.CompanyBranchId == companyBranchID && x.Status == true).FirstOrDefault();
                if (companies != null)
                {
                    ComapnyBranchList = new ComapnyBranch
                    {
                        StateId = companies.StateId,
                        FreightCGST_Perc = companies.FreightCGST_Perc,
                        FreightSGST_Perc = companies.FreightSGST_Perc,
                        FreightIGST_Perc = companies.FreightIGST_Perc,
                        LoadingCGST_Perc = companies.LoadingCGST_Perc,
                        LoadingSGST_Perc = companies.LoadingSGST_Perc,
                        LoadingIGST_Perc = companies.LoadingIGST_Perc,
                        InsuranceCGST_Perc = companies.InsuranceCGST_Perc,
                        InsuranceSGST_Perc = companies.InsuranceSGST_Perc,
                        InsuranceIGST_Perc = companies.InsuranceIGST_Perc
                    };

                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return ComapnyBranchList;
        }
        #endregion

        #region Purchase Invoice
        public ResponseOut CancelPI(PurchaseInvoice purchaseinvoice)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                entities.PurchaseInvoices.Where(a => a.InvoiceId == purchaseinvoice.InvoiceId).ToList().ForEach(a =>
                {
                    a.ApprovalStatus = purchaseinvoice.ApprovalStatus;
                    a.CancelStatus = purchaseinvoice.CancelStatus;
                    a.CancelBy = purchaseinvoice.CreatedBy;
                    a.CancelDate = DateTime.Now;
                    a.CancelReason = purchaseinvoice.CancelReason;
                });
                responseOut.message = ActionMessage.PurchaseInvoiceCancelSuccess;

                entities.SaveChanges();
                responseOut.status = ActionStatus.Success;

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
        #endregion

        #region Cost Center
        public ResponseOut AddEditCostCenter(CostCenter costcenter)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                if (entities.CostCenters.Any(x => x.CostCenterName == costcenter.CostCenterName && x.CompanyId == costcenter.CompanyId && x.CostCenterId != costcenter.CostCenterId))
                {
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.DuplicateCostCenterName;
                }

                else
                {
                    if (costcenter.CostCenterId == 0)
                    {
                        costcenter.CreatedDate = DateTime.Now;
                        entities.CostCenters.Add(costcenter);
                        responseOut.message = ActionMessage.CostCenterCreatedSuccess;
                    }
                    else
                    {
                        costcenter.Modifiedby = costcenter.CreatedBy;
                        costcenter.ModifiedDate = DateTime.Now;
                        entities.CostCenters.Where(a => a.CostCenterId == costcenter.CostCenterId).ToList().ForEach(a =>
                        {
                            a.CostCenterId = costcenter.CostCenterId;
                            a.CostCenterName = costcenter.CostCenterName;
                            a.CompanyId = costcenter.CompanyId;
                            a.Status = costcenter.Status;
                        });
                        responseOut.message = ActionMessage.CostCenterUpdatedSuccess;
                    }
                    entities.SaveChanges();
                    responseOut.status = ActionStatus.Success;
                }
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
        #endregion

        #region SLDetails
        public ResponseOut AddEditSLDetail(SLDetail sLDetail)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {

                if (entities.SLDetails.Any(x => x.SLDetailId == sLDetail.SLDetailId))
                {
                    entities.SLDetails.Where(a => a.SLDetailId == sLDetail.SLDetailId).ToList().ForEach(a =>
                    {
                        a.SLDetailId = sLDetail.SLDetailId;
                        a.GLId = sLDetail.GLId;
                        a.SLId = sLDetail.SLId;
                        a.CompanyId = sLDetail.CompanyId;
                        a.FinYearId = sLDetail.FinYearId;
                        a.OpeningBalance = sLDetail.OpeningBalance;
                        a.OpeningBalanceDebit = sLDetail.OpeningBalanceDebit;
                        a.OpeningBalanceCredit = sLDetail.OpeningBalanceCredit;
                        a.ModifiedBy = sLDetail.CreatedBy;
                        a.ModifiedDate = DateTime.Now;
                        a.Status = sLDetail.Status;
                    });
                }
                else
                {
                    entities.SLDetails.Add(sLDetail);

                }
                entities.SaveChanges();
                responseOut.message = ActionMessage.SLDetailSuccessful;
                responseOut.status = ActionStatus.Success;
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
        #endregion

        #region Upload Utility
        public int GetIdByStateName(string stateName)
        {
            int stateId = 0;
            try
            {
                stateId = entities.States.Where(s => s.StateName.Trim().ToUpper() == stateName.Trim().ToUpper()).Select(x => x.Stateid).SingleOrDefault();
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return stateId;
        }
        public int GetIdByCountryName(string countryName)
        {
            int countryId = 0;
            try
            {
                countryId = entities.Countries.Where(s => s.CountryName.Trim().ToUpper() == countryName.Trim().ToUpper()).Select(x => x.CountryId).SingleOrDefault();
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return countryId;
        }

        public int GetIdByCustomerName(string customerName)
        {
            int customerId = 0;
            try
            {
                customerId = entities.Customers.Where(s => s.CustomerName.Trim().ToUpper() == customerName.Trim().ToUpper()).Select(x => x.CustomerId).SingleOrDefault();
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return customerId;
        }


        public int GetIdByLeadSourceName(string leadSourceName)
        {
            int leadSourceId = 0;
            try
            {
                leadSourceId = entities.LeadSources.Where(s => s.LeadSourceName.Trim().ToUpper() == leadSourceName.Trim().ToUpper()).Select(x => x.LeadSourceId).SingleOrDefault();
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return leadSourceId;
        }
        public int GetIdByLeadStatusName(string leadStatusName)
        {
            int leadStatusId = 0;
            try
            {
                leadStatusId = entities.LeadStatus.Where(s => s.LeadStatusName.Trim().ToUpper() == leadStatusName.Trim().ToUpper()).Select(x => x.LeadStatusId).SingleOrDefault();
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return leadStatusId;
        }
        public int GetIdByFollowUpActivityTypeName(string followUpActivityTypeName)
        {
            int followUpActivityTypeId = 0;
            try
            {
                followUpActivityTypeId = entities.FollowUpActivityTypes.Where(s => s.FollowUpActivityTypeName.Trim().ToUpper() == followUpActivityTypeName.Trim().ToUpper()).Select(x => x.FollowUpActivityTypeId).SingleOrDefault();
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return followUpActivityTypeId;
        }

        public int GetIdByGLMainGroupName(string gLMainGroupName)
        {
            int glMainGroupId = 0;
            try
            {
                glMainGroupId = entities.GLMainGroups.Where(s => s.GLMainGroupName.Trim().ToUpper() == gLMainGroupName.Trim().ToUpper()).Select(x => x.GLMainGroupId).SingleOrDefault();
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return glMainGroupId;
        }
        public int GetIdByScheduleName(string scheduleName)
        {
            int scheduleID = 0;
            try
            {
                scheduleID = entities.Schedules.Where(s => s.ScheduleName.Trim().ToUpper() == scheduleName.Trim().ToUpper()).Select(x => x.ScheduleId).SingleOrDefault();
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return scheduleID;
        }
        public int GetIdBySLTypeName(string sLTypeName)
        {
            int sLTypeId = 0;
            try
            {
                sLTypeId = entities.SLTypes.Where(s => s.SLTypeName.Trim().ToUpper() == sLTypeName.Trim().ToUpper()).Select(x => x.SLTypeId).SingleOrDefault();
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return sLTypeId;
        }
        public int GetIdByCostCenterName(string costCenterName)
        {
            int costCenterId = 0;
            try
            {
                costCenterId = entities.CostCenters.Where(s => s.CostCenterName.Trim().ToUpper() == costCenterName.Trim().ToUpper()).Select(x => x.CostCenterId).SingleOrDefault();
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return costCenterId;
        }
        public int GetIdBySubCostCenterName(string subcostCenterName)
        {
            int subcostCenterId = 0;
            try
            {
                subcostCenterId = entities.SubCostCenters.Where(s => s.SubCostCenterName.Trim().ToUpper() == subcostCenterName.Trim().ToUpper()).Select(x => x.SubCostCenterId).SingleOrDefault();
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return subcostCenterId;
        }
        public int GetIdByGLHead(string gLHead)
        {
            int postingGLId = 0;
            try
            {
                postingGLId = entities.GLs.Where(s => s.GLHead.Trim().ToUpper() == gLHead.Trim().ToUpper()).Select(x => x.GLId).SingleOrDefault();
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return postingGLId;
        }

        public int GetIdByEmployeeName(string employeeName)
        {
            int employeeId = 0;
            try
            {
                employeeId = entities.Employees.Where(s => s.FirstName.Trim().ToUpper() == employeeName.Trim().ToUpper()).Select(x => x.EmployeeId).SingleOrDefault();
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return employeeId;
        }
        public int GetIdByCustomerTypeDesc(string customerTypeDesc)
        {
            int customerTypeId = 0;
            try
            {
                customerTypeId = entities.CustomerTypes.Where(s => s.CustomerTypeDesc.Trim().ToUpper() == customerTypeDesc.Trim().ToUpper()).Select(x => x.CustomerTypeId).SingleOrDefault();
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return customerTypeId;
        }
        public int GetIdByDepartmentName(string departmentName)
        {
            int departmentId = 0;
            try
            {
                departmentId = entities.Departments.Where(s => s.DepartmentName.Trim().ToUpper() == departmentName.Trim().ToUpper()).Select(x => x.DepartmentId).SingleOrDefault();
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return departmentId;
        }
        public int GetIdByDesignationName(string designationName)
        {
            int designationId = 0;
            try
            {
                designationId = entities.Designations.Where(s => s.DesignationName.Trim().ToUpper() == designationName.Trim().ToUpper()).Select(x => x.DesignationId).SingleOrDefault();
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return designationId;
        }
        public int GetIdByProductMainGroupName(string mainGroupName)
        {
            int productMainGroupID = 0;
            try
            {
                productMainGroupID = entities.ProductMainGroups.Where(s => s.ProductMainGroupName.Trim().ToUpper() == mainGroupName.Trim().ToUpper()).Select(x => x.ProductMainGroupId).SingleOrDefault();
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return productMainGroupID;
        }
        public int GetIdByProductTypeName(string productTypeName)
        {
            int productTypeId = 0;
            try
            {
                productTypeId = entities.ProductTypes.Where(s => s.ProductTypeName.Trim().ToUpper() == productTypeName.Trim().ToUpper()).Select(x => x.ProductTypeId).SingleOrDefault();
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return productTypeId;
        }
        public int GetIdByProductSubGroupName(string subGroupName)
        {
            int productSubGroupID = 0;
            try
            {
                productSubGroupID = entities.ProductSubGroups.Where(s => s.ProductSubGroupName.Trim().ToUpper() == subGroupName.Trim().ToUpper()).Select(x => x.ProductSubGroupId).SingleOrDefault();
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return productSubGroupID;
        }
        public int GetIdByUOMName(string uOMName)
        {
            int UOMId = 0;
            try
            {
                UOMId = entities.UOMs.Where(s => s.UOMName.Trim().ToUpper() == uOMName.Trim().ToUpper()).Select(x => x.UOMId).SingleOrDefault();
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return UOMId;
        }
        #endregion
        #region Currency
        public List<Currency> GetCurrencyList()
        {
            List<Currency> currencyList = new List<Currency>();
            try
            {
                var currencies = entities.Currencies.Where(x => x.Status == true).Select(s => new
                {
                    CurrencyId = s.CurrencyId,
                    CurrencyCode = s.CurrencyCode,
                    CurrencyName = s.CurrencyName
                }).ToList();
                if (currencies != null && currencies.Count > 0)
                {
                    foreach (var item in currencies)
                    {
                        currencyList.Add(new Currency { CurrencyId = item.CurrencyId, CurrencyCode = item.CurrencyCode, CurrencyName = item.CurrencyName });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return currencyList;
        }
        #endregion
        #region FreightType
        public List<FreightType> GetFreightTypeList()
        {
            List<FreightType> freightTypeList = new List<FreightType>();
            try
            {
                var freightTypes = entities.FreightTypes.Where(x => x.Status == true).Select(
                    s => new
                    {
                        FreightId = s.FreightId,
                        FreightTypeName = s.FreightTypeName,
                        FreightTypeCode = s.FreightTypeCode

                    }).ToList();

                if (freightTypes != null && freightTypes.Count > 0)
                {
                    foreach (var item in freightTypes)
                    {
                        freightTypeList.Add(
                            new FreightType
                            {
                                FreightId = item.FreightId,
                                FreightTypeName = item.FreightTypeName,
                                FreightTypeCode = item.FreightTypeCode
                            });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return freightTypeList;
        }
        #endregion

        #region Logo
        public ResponseOut UpdateLogoPic(Logo logo)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                entities.Logoes.Where(x => x.LogoId == logo.LogoId).ToList().ForEach(x =>
                {
                    x.LogoPath = logo.LogoPath;
                });
                entities.SaveChanges();
                responseOut.status = ActionStatus.Success;
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
        public ResponseOut AddEditLogo(Logo logo)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                if (entities.Logoes.Any(x => x.Title == logo.Title))
                {
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.DuplicateLogoTitle;
                }
                else
                {
                    if (logo.LogoId == 0)
                    {
                        logo.CreateDate = DateTime.Now;
                        entities.Logoes.Add(logo);
                        responseOut.message = ActionMessage.LogoCreatedSuccess;
                    }
                    else
                    {
                        logo.ModifyBy = logo.CreatedBy;
                        logo.ModifyDate = DateTime.Now;
                        entities.Logoes.Where(x => x.LogoId == logo.LogoId).ToList().ForEach(x =>
                        {
                            x.LogoId = logo.LogoId;
                            x.Title = logo.Title;
                            x.LogoPath = logo.LogoPath;
                            x.Link = logo.Link;
                            x.ModifyBy = logo.ModifyBy;
                            x.ModifyDate = logo.ModifyDate;
                            x.LogoStatus = logo.LogoStatus;
                        });
                        responseOut.message = ActionMessage.LogoUpdatedSuccess;
                    }

                    entities.SaveChanges();
                    responseOut.trnId = logo.LogoId;
                    responseOut.status = ActionStatus.Success;
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                var errorList = new List<string>();

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        errorList.Add(String.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage));
                    }
                }

            }
            return responseOut;
        }

        public List<Logo> GetLogoList()
        {
            List<Logo> logoList = new List<Logo>();
            try
            {
                var logoes = entities.Logoes.Join(entities.Users,
                    l => l.CreatedBy,
                    u => u.UserId, (l, u) => new
                    {
                        LogoId = l.LogoId,
                        Title = l.Title,
                        LogoPath = l.LogoPath,
                        Link = l.Link,
                        LogoStatus = l.LogoStatus,
                        CreateDate = l.CreateDate,
                        UserName = u.UserName
                    }).ToList();
                //var logoes = entities.Logoes.Where(x => x.LogoStatus == true).Select(s => new
                //{
                //    LogoId = s.LogoId,
                //    Title = s.Title,
                //    LogoPath = s.LogoPath,
                //    Link=s.Link,
                //    LogoStatus=s.LogoStatus
                //}).ToList();

                if (logoes != null && logoes.Count > 0)
                {
                    foreach (var item in logoes)
                    {
                        logoList.Add(new Logo { LogoId = item.LogoId, Title = item.Title, LogoPath = item.LogoPath, Link = item.Link, LogoStatus = item.LogoStatus, UserName = item.UserName });

                    }
                }

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return logoList;
        }

        public ResponseOut RemoveLogo(int logoId)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                var logo = entities.Logoes.First(x => x.LogoId == logoId);
                entities.Logoes.Remove(logo);
                entities.SaveChanges();
                responseOut.status = ActionStatus.Success;
                responseOut.message = ActionMessage.RemoveLogo;
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

        public Logo GetLogo()
        {
            Logo logo = new Logo();
            try
            {
                var logos = entities.Logoes.FirstOrDefault(x => x.LogoStatus == true);
                logo.LogoId = logos.LogoId;
                logo.Title = logos.Title;
                logo.LogoPath = logos.LogoPath;
                logo.Link = logos.Link;


            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return logo;
        }

        #endregion

        #region Aboutus
        public ResponseOut UpdateAboutusPic(Aboutu aboutUs)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                entities.Aboutus.Where(x => x.AboutusId == aboutUs.AboutusId).ToList().ForEach(x =>
                {
                    x.ImageUrl = aboutUs.ImageUrl;
                });
                entities.SaveChanges();
                responseOut.status = ActionStatus.Success;
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
        public ResponseOut AddEditAboutus(Aboutu aboutUs)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                if (entities.Aboutus.Any(x => x.AboutusTitle == aboutUs.AboutusTitle && aboutUs.AboutusId == 0))
                {
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.DuplicateAboutus;
                }
                else
                {
                    if (aboutUs.AboutusId == 0)
                    {
                        aboutUs.CreateDate = DateTime.Now;
                        entities.Aboutus.Add(aboutUs);
                        responseOut.message = ActionMessage.AboutusCreatedSuccess;
                    }
                    else
                    {
                        aboutUs.ModifyBy = aboutUs.CreatedBy;
                        aboutUs.ModifyDate = DateTime.Now;
                        entities.Aboutus.Where(x => x.AboutusId == aboutUs.AboutusId).ToList().ForEach(x =>
                        {
                            x.AboutusId = aboutUs.AboutusId;
                            x.AboutusTitle = aboutUs.AboutusTitle;
                            x.Description = aboutUs.Description;
                            x.ModifyBy = aboutUs.ModifyBy;
                            x.ModifyDate = aboutUs.ModifyDate;
                            x.AboutStatus = aboutUs.AboutStatus;

                        });
                        responseOut.message = ActionMessage.AboutusUpdatedSuccess;
                    }

                    entities.SaveChanges();
                    responseOut.trnId = aboutUs.AboutusId;
                    responseOut.status = ActionStatus.Success;
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                var errorList = new List<string>();

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        errorList.Add(String.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage));
                    }
                }

            }
            return responseOut;
        }

        public List<Aboutu> GetAboutusList()
        {
            List<Aboutu> aboutusList = new List<Aboutu>();
            try
            {
                var aboutUs = entities.Aboutus.Join(entities.Users,
                    a => a.CreatedBy,
                    u => u.UserId, (a, u) => new
                    {
                        AboutusId = a.AboutusId,
                        AboutusTitle = a.AboutusTitle,
                        Description = a.Description,
                        ImageUrl = a.ImageUrl,
                        AboutStatus = a.AboutStatus,
                        CreateDate = a.CreateDate,
                        Username = u.UserName
                    }).ToList();

                if (aboutUs != null && aboutUs.Count > 0)
                {
                    foreach (var item in aboutUs)
                    {
                        aboutusList.Add(new Aboutu { AboutusId = item.AboutusId, AboutusTitle = item.AboutusTitle, Description = item.Description, ImageUrl = item.ImageUrl, AboutStatus = item.AboutStatus, UserName = item.Username });
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return aboutusList;
        }


        public List<ContactInfo> GetContactInfoList()
        {
            List<ContactInfo> contactInfoList = new List<ContactInfo>();
            try
            {
                var contactInfo = entities.ContactInfoes.Join(entities.Users,
                    a => a.CreatedBy,
                    u => u.UserId, (a, u) => new
                    {
                        ContactInfoId = a.ContactInfoId,
                        ContactTitle = a.ContactTitle,
                        Phone = a.Phone,
                        Email = a.Email,
                        Website = a.Website,
                        Address = a.Address,
                        UserName = u.UserName,
                        Status = a.Status


                    }).ToList();

                if (contactInfo != null && contactInfo.Count > 0)
                {
                    foreach (var item in contactInfo)
                    {
                        contactInfoList.Add(new ContactInfo { ContactInfoId = item.ContactInfoId, ContactTitle = item.ContactTitle, Phone = item.Phone, Email = item.Email, Website = item.Website, Address = item.Address, UserName = item.UserName, Status = item.Status });
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return contactInfoList;
        }

        public ResponseOut RemoveContactInfo(int contactInfoId)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                var contactInfo = entities.ContactInfoes.First(x => x.ContactInfoId == contactInfoId);
                entities.ContactInfoes.Remove(contactInfo);
                entities.SaveChanges();
                responseOut.status = ActionStatus.Success;
                responseOut.message = ActionMessage.ContactInfoRemove;
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

        public List<Aboutu> GetAboutList()
        {
            List<Aboutu> aboutList = new List<Aboutu>();
            try
            {
                var abouts = entities.Aboutus.Where(x => x.AboutStatus == true).ToList();
                foreach (Aboutu item in abouts)
                {
                    aboutList.Add(new Aboutu
                    {
                        AboutusTitle = item.AboutusTitle,
                        Description = item.Description,
                        ImageUrl = item.ImageUrl
                    });
                }

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return aboutList;
        }
        public List<Aboutu> GetOurMissionList()
        {
            List<Aboutu> aboutList = new List<Aboutu>();
            try
            {
                var abouts = entities.Aboutus.Where(x => x.AboutStatus == true && x.AboutusTitle.Trim() == "Mission statement").ToList();
                foreach (Aboutu item in abouts)
                {
                    aboutList.Add(new Aboutu
                    {
                        AboutusTitle = item.AboutusTitle,
                        Description = item.Description,
                        ImageUrl = item.ImageUrl
                    });
                }

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return aboutList;
        }
        #endregion


        #region  MSME
        public ResponseOut UpdateMSMEPic(MSME msme)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                entities.MSMEs.Where(x => x.MSMEId == msme.MSMEId).ToList().ForEach(x =>
                {
                    x.MSMEImageUrl = msme.MSMEImageUrl;
                });
                entities.SaveChanges();
                responseOut.status = ActionStatus.Success;
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
        public ResponseOut AddEditMSME(MSME msme)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                if (entities.MSMEs.Any(x => x.MSMETitle == msme.MSMETitle && msme.MSMEId == 0))
                {
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.MSMEDuplicate;
                }
                else
                {
                    if (msme.MSMEId == 0)
                    {
                        msme.CreateDate = DateTime.Now;
                        entities.MSMEs.Add(msme);
                        responseOut.message = ActionMessage.MSMECreatedSuccess;
                    }
                    else
                    {
                        msme.ModifyBy = msme.CreatedBy;
                        msme.ModifyDate = DateTime.Now;
                        entities.MSMEs.Where(x => x.MSMEId == msme.MSMEId).ToList().ForEach(x =>
                        {
                            x.MSMEId = msme.MSMEId;
                            x.MSMETitle = msme.MSMETitle;
                            x.MSMEDescription = msme.MSMEDescription;
                            x.ModifyBy = msme.ModifyBy;
                            x.ModifyDate = msme.ModifyDate;
                            x.MSMEStatus = msme.MSMEStatus;

                        });
                        responseOut.message = ActionMessage.MSMEUpdatedSuccess;
                    }

                    entities.SaveChanges();
                    responseOut.trnId = msme.MSMEId;
                    responseOut.status = ActionStatus.Success;
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                var errorList = new List<string>();

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        errorList.Add(String.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage));
                    }
                }

            }
            return responseOut;
        }

        public List<MSME> GetMSMEList()
        {
            List<MSME> msmeList = new List<MSME>();
            try
            {
                var msme = entities.MSMEs.Where(x => x.MSMEStatus == true).Select(s => new
                {
                    MSMEId = s.MSMEId,
                    MSMETitle = s.MSMETitle,
                    MSMEDescription = s.MSMEDescription,
                    MSMEContentArea = s.MSMEContentArea,
                    MSMEImageUrl = s.MSMEImageUrl,
                    MSMEStatus = s.MSMEStatus
                }).ToList();
                //var msme = entities.MSMEs.Join(entities.Users,
                //    a => a.CreatedBy,
                //    u => u.UserId, (a, u) => new {
                //        MSMEId = a.MSMEId,
                //        MSMETitle = a.MSMETitle,
                //        MSMEDescription = a.MSMEDescription,
                //        MSMEImageUrl = a.MSMEImageUrl,
                //        MSMEStatus = a.MSMEStatus,
                //        CreateDate = a.CreateDate,
                //        Username = u.UserName
                //    }).ToList();

                if (msme != null && msme.Count > 0)
                {
                    foreach (var item in msme)
                    {
                        msmeList.Add(new MSME
                        {
                            MSMEId = item.MSMEId,
                            MSMETitle = item.MSMETitle,
                            MSMEDescription = item.MSMEDescription,
                            MSMEContentArea = item.MSMEContentArea,
                            MSMEImageUrl = item.MSMEImageUrl,
                            MSMEStatus = item.MSMEStatus
                        });
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return msmeList;
        }

        public ResponseOut RemoveMSME(int msmeId)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                var msme = entities.MSMEs.First(x => x.MSMEId == msmeId);
                entities.MSMEs.Remove(msme);
                entities.SaveChanges();
                responseOut.status = ActionStatus.Success;
                responseOut.message = ActionMessage.MSMERemove;
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
        #endregion

        #region ContactInfo

        public ResponseOut AddEditContactInfo(ContactInfo contactInfo)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                if (entities.ContactInfoes.Any(x => x.ContactTitle == contactInfo.ContactTitle && x.ContactInfoId == 0))
                {
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.ContactInfoTitle;
                }
                else
                {
                    if (contactInfo.ContactInfoId == 0)
                    {
                        contactInfo.CreateDate = DateTime.Now;
                        entities.ContactInfoes.Add(contactInfo);
                        responseOut.message = ActionMessage.ContactInfoCreatedSuccess;
                    }
                    else
                    {
                        contactInfo.ModifyBy = contactInfo.CreatedBy;
                        contactInfo.ModifyDate = DateTime.Now;
                        entities.ContactInfoes.Where(x => x.ContactInfoId == contactInfo.ContactInfoId).ToList().ForEach(x =>
                        {

                            x.ContactTitle = contactInfo.ContactTitle;
                            x.Phone = contactInfo.Phone;
                            x.Email = contactInfo.Email;
                            x.Website = contactInfo.Website;
                            x.Address = contactInfo.Address;
                            x.Status = contactInfo.Status;
                            x.ModifyBy = contactInfo.ModifyBy;
                            x.ModifyDate = contactInfo.ModifyDate;

                        });
                        responseOut.message = ActionMessage.ContactInfoUpdatedSuccess;
                    }

                    entities.SaveChanges();
                    responseOut.trnId = contactInfo.ContactInfoId;
                    responseOut.status = ActionStatus.Success;
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                var errorList = new List<string>();

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        errorList.Add(String.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage));
                    }
                }

            }
            return responseOut;
        }
        #endregion


        #region CopyRight

        public ResponseOut AddEditCopyRight(Copyright copyright)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                if (entities.Copyrights.Any(x => x.Description == copyright.Description && copyright.Id == 0))
                {
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.DuplicateCopyRight;
                }
                else
                {
                    if (copyright.Id == 0)
                    {
                        copyright.CreateDate = DateTime.Now;
                        entities.Copyrights.Add(copyright);
                        responseOut.message = ActionMessage.CopyRightCreatedSuccess;
                    }
                    else
                    {
                        copyright.ModifyBy = copyright.CreatedBy;
                        copyright.ModifyDate = DateTime.Now;
                        entities.Copyrights.Where(x => x.Id == copyright.Id).ToList().ForEach(x =>
                        {


                            x.Description = copyright.Description;
                            x.ModifyBy = copyright.ModifyBy;
                            x.ModifyDate = copyright.ModifyDate;
                            x.CopyrightStatus = copyright.CopyrightStatus;

                        });
                        responseOut.message = ActionMessage.CopyRightUpdatedSuccess;
                    }

                    entities.SaveChanges();
                    responseOut.trnId = copyright.Id;
                    responseOut.status = ActionStatus.Success;
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                var errorList = new List<string>();

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        errorList.Add(String.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage));
                    }
                }

            }
            return responseOut;
        }


        public List<Copyright> GetCopyRightList()
        {
            List<Copyright> copyrightList = new List<Copyright>();
            try
            {
                var copyright = entities.Copyrights.Join(entities.Users,
                    a => a.CreatedBy,
                    u => u.UserId, (a, u) => new
                    {
                        Id = a.Id,
                        Description = a.Description,
                        CopyrightStatus = a.CopyrightStatus,
                        UserName = u.UserName


                    }).ToList();

                if (copyright != null && copyright.Count > 0)
                {
                    foreach (var item in copyright)
                    {
                        copyrightList.Add(new Copyright
                        {
                            Id = item.Id,
                            Description = item.Description,
                            CopyrightStatus = item.CopyrightStatus,
                            UserName = item.UserName
                        });
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return copyrightList;
        }


        #endregion

        #region HomeAbout

        public ResponseOut AddEditHomeAbout(HomeAbout homeAbout)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                if (entities.HomeAbouts.Any(x => x.Title == homeAbout.Title && homeAbout.Id == 0))
                {
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.HomeAboutCopyRight;
                }
                else
                {
                    if (homeAbout.Id == 0)
                    {
                        homeAbout.CreateDate = DateTime.Now;
                        entities.HomeAbouts.Add(homeAbout);
                        responseOut.message = ActionMessage.HomeAboutCreatedSuccess;
                    }
                    else
                    {
                        homeAbout.ModifyBy = homeAbout.CreatedBy;
                        homeAbout.ModifyDate = DateTime.Now;
                        entities.HomeAbouts.Where(x => x.Id == homeAbout.Id).ToList().ForEach(x =>
                        {

                            x.Title = homeAbout.Title;
                            x.Description = homeAbout.Description;
                            x.ModifyBy = homeAbout.ModifyBy;
                            x.ModifyDate = homeAbout.ModifyDate;
                            x.HomeAboutStatus = homeAbout.HomeAboutStatus;

                        });
                        responseOut.message = ActionMessage.HomeAboutUpdatedSuccess;
                    }

                    entities.SaveChanges();
                    responseOut.trnId = homeAbout.Id;
                    responseOut.status = ActionStatus.Success;
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                var errorList = new List<string>();

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        errorList.Add(String.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage));
                    }
                }

            }
            return responseOut;
        }


        public List<HomeAbout> GetHomeAboutList()
        {
            List<HomeAbout> homeAboutList = new List<HomeAbout>();
            try
            {
                var homeAbout = entities.HomeAbouts.Join(entities.Users,
                    a => a.CreatedBy,
                    u => u.UserId, (a, u) => new
                    {
                        Id = a.Id,
                        Titl = a.Title,
                        Description = a.Description,
                        HomeAboutStatus = a.HomeAboutStatus,
                        UserName = u.UserName
                    }).ToList();

                if (homeAbout != null && homeAbout.Count > 0)
                {
                    foreach (var item in homeAbout)
                    {
                        homeAboutList.Add(new HomeAbout
                        {
                            Id = item.Id,
                            Description = item.Description,
                            Title = item.Titl,
                            HomeAboutStatus = item.HomeAboutStatus,
                            UserName = item.UserName
                        });
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return homeAboutList;
        }


        #endregion

        #region ButtonTitle

        public ResponseOut AddEditButtonTitle(ButtonTitle buttonTitle)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                if (entities.ButtonTitles.Any(x => x.Title == buttonTitle.Title && buttonTitle.Id == 0))
                {
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.ButtonTitleDuplicate;
                }
                else
                {
                    if (buttonTitle.Id == 0)
                    {
                        buttonTitle.CreateDate = DateTime.Now;
                        entities.ButtonTitles.Add(buttonTitle);
                        responseOut.message = ActionMessage.ButtonTitleCreatedSuccess;
                    }
                    else
                    {
                        buttonTitle.ModifyBy = buttonTitle.CreatedBy;
                        buttonTitle.ModifyDate = DateTime.Now;
                        entities.ButtonTitles.Where(x => x.Id == buttonTitle.Id).ToList().ForEach(x =>
                        {


                            x.Title = buttonTitle.Title;
                            x.ModifyBy = buttonTitle.ModifyBy;
                            x.ModifyDate = buttonTitle.ModifyDate;
                            x.ButtonTitleStatus = buttonTitle.ButtonTitleStatus;

                        });
                        responseOut.message = ActionMessage.ButtonTitleUpdatedSuccess;
                    }

                    entities.SaveChanges();
                    responseOut.trnId = buttonTitle.Id;
                    responseOut.status = ActionStatus.Success;
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                var errorList = new List<string>();

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        errorList.Add(String.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage));
                    }
                }

            }
            return responseOut;
        }
        public List<ButtonTitle> GetButtonTitleList()
        {
            List<ButtonTitle> buttonTitleList = new List<ButtonTitle>();
            try
            {
                var buttonTitles = entities.ButtonTitles.Join(entities.Users,
                    a => a.CreatedBy,
                    u => u.UserId, (a, u) => new
                    {
                        Id = a.Id,
                        Title = a.Title,
                        ButtonTitleStatus = a.ButtonTitleStatus,
                        UserName = u.UserName


                    }).ToList();

                if (buttonTitles != null && buttonTitles.Count > 0)
                {
                    foreach (var item in buttonTitles)
                    {
                        buttonTitleList.Add(new ButtonTitle
                        {
                            Id = item.Id,
                            Title = item.Title,
                            ButtonTitleStatus = item.ButtonTitleStatus,
                            UserName = item.UserName
                        });
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return buttonTitleList;
        }
        #endregion

        #region CommanMethod
        public string GetAboutUSList()
        {
            string aboutuList = "";
            try
            {
                //aboutuList = entities.Aboutus.Where(x => x.AboutStatus == true).Select(s => s.Description).FirstOrDefault().ToString();
                aboutuList = entities.Aboutus.Where(x => x.AboutStatus == true).Select(s => s.Description).FirstOrDefault().ToString();

            }
            catch (Exception ex)
            {
                //Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return aboutuList;
        }
        #endregion

        #region HomeSlider
        public List<HomeSlider> GetHomeSlidersList()
        {
            List<HomeSlider> homeSliderList = new List<HomeSlider>();
            try
            {
                var homeSliders = entities.HomeSliders.Where(x => x.HomeSiderStatus == true).Select(s => new
                {
                    id = s.Sliderid,
                    Title = s.SliderTitle,
                    Image = s.Image,
                    Description = s.Description,

                }).ToList();
                if (homeSliders != null && homeSliders.Count > 0)
                {
                    foreach (var item in homeSliders)
                    {
                        homeSliderList.Add(new HomeSlider { Sliderid = item.id, SliderTitle = item.Title, Image = item.Image, Description = item.Description });
                    }
                }
            }
            catch (Exception ex)
            {
                //Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return homeSliderList;
        }
        public List<Product> GetProdcutImagesList(string productname)
        {
            List<Product> productlist = new List<Product>();
            try
            {
                var profuctImages = (from p in entities.Products
                                     join pid in entities.ProductImageDetails on p.Productid equals pid.ProductId
                                     where p.ProductName == productname && pid.ImageSequence == 1
                                     select new
                                     {
                                         p.ProductName,
                                         p.Productid,
                                         p.ProductFullDesc,
                                         pid.ImageUrl
                                     }).ToList();

                if (profuctImages != null && profuctImages.Count > 0)
                {
                    foreach (var item in profuctImages)
                    {
                        productlist.Add(new Product
                        {
                            Productid = item.Productid,
                            ProductName = item.ProductName,
                            ProductFullDesc = item.ProductFullDesc
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return productlist;
        }
        public List<ProductImageDetail> GetProdcutImagesList1(string productname)
        {
            List<ProductImageDetail> productlist = new List<ProductImageDetail>();
            try
            {
                var profuctImages = (from p in entities.Products
                                     join pid in entities.ProductImageDetails on p.Productid equals pid.ProductId
                                     where p.ProductName.Trim() == productname.Trim()
                                     select new
                                     {
                                         pid.ProductId,
                                         pid.ImageTitle,
                                         pid.ImageUrl,
                                         pid.ImageAlt,
                                         pid.ImageNavigateUrl,
                                         pid.ImageSequence

                                     }).ToList();



                if (profuctImages != null && profuctImages.Count > 0)
                {
                    foreach (var item in profuctImages)
                    {
                        productlist.Add(new ProductImageDetail
                        {
                            ProductId = item.ProductId,
                            ImageTitle = item.ImageTitle,
                            ImageUrl = item.ImageUrl,
                            ImageNavigateUrl = item.ImageNavigateUrl,
                            ImageAlt = item.ImageAlt,
                            ImageSequence = item.ImageSequence
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return productlist;
        }

        #endregion

        #region Home Update
        public ResponseOut UpdateImage(HomeUpdate homeUpdate)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                entities.HomeUpdates.Where(x => x.UpdateId == homeUpdate.UpdateId).ToList().ForEach(x =>
                {
                    x.ImageUrl = homeUpdate.ImageUrl;
                });
                entities.SaveChanges();
                responseOut.status = ActionStatus.Success;
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
        public ResponseOut AddEditHomeUpdate(HomeUpdate homeUpdate)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                if (entities.HomeUpdates.Any(x => x.UpdateTitle == homeUpdate.UpdateTitle && x.UpdateId == 0))
                {
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.DuplicateHomeUpdate;
                }
                else
                {
                    if (homeUpdate.UpdateId == 0)
                    {
                        homeUpdate.CreateDate = DateTime.Now;
                        entities.HomeUpdates.Add(homeUpdate);
                        responseOut.message = ActionMessage.HomeUpdatesCreatedSuccess;

                    }
                    else
                    {
                        homeUpdate.ModifyBy = homeUpdate.CreatedBy;
                        homeUpdate.ModifyDate = DateTime.Now;
                        entities.HomeUpdates.Where(x => x.UpdateId == homeUpdate.UpdateId).ToList().ForEach(x =>
                        {
                            x.UpdateId = homeUpdate.UpdateId;
                            x.UpdateTitle = homeUpdate.UpdateTitle;
                            x.UpdateDate = homeUpdate.UpdateDate;
                            x.UpdateDec = homeUpdate.UpdateDec;
                            x.UpdateUrl = homeUpdate.UpdateUrl;
                            x.ImageTitile = homeUpdate.ImageTitile;
                            x.ImageUrl = homeUpdate.ImageUrl;
                            x.ImageNavigateUrl = homeUpdate.ImageNavigateUrl;
                            x.ImageAlt = homeUpdate.ImageAlt;
                            x.UpdateStatus = homeUpdate.UpdateStatus;
                            x.UpdateArea = homeUpdate.UpdateArea;

                        }
                        );
                        responseOut.message = ActionMessage.HomeUpdatesUpdatedSuccess;
                    }
                    entities.SaveChanges();
                    responseOut.trnId = homeUpdate.UpdateId;
                    responseOut.status = ActionStatus.Success;
                }
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

        public ResponseOut RemoveHomeUpdate(int updateId)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                var homeupdate = entities.HomeUpdates.First(x => x.UpdateId == updateId);
                entities.HomeUpdates.Remove(homeupdate);
                entities.SaveChanges();
                responseOut.status = ActionStatus.Success;
                responseOut.message = ActionMessage.RemoveHomeUpdate;
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
        #endregion

        #region Parent Menu
        public List<proc_GetHomeParentMenu_Result1> GetHomeParentMenu()
        {
            List<proc_GetHomeParentMenu_Result1> parentMenuList = new List<proc_GetHomeParentMenu_Result1>();
            try
            {
                parentMenuList = entities.proc_GetHomeParentMenu().ToList();
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }

            return parentMenuList;
        }



        public List<proc_GetHomeSubMenu_Result> GetHomeSubMenu(int parentMenuId)
        {
            List<proc_GetHomeSubMenu_Result> subMenuList = new List<proc_GetHomeSubMenu_Result>();
            try
            {
                subMenuList = entities.proc_GetHomeSubMenu(parentMenuId).ToList();
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return subMenuList;
        }

        public List<proc_GetHomeSubChildMenu_Result> GetHomeSubChildMenu(int subMenuId)
        {
            List<proc_GetHomeSubChildMenu_Result> subChildMenuList = new List<proc_GetHomeSubChildMenu_Result>();
            try
            {
                subChildMenuList = entities.proc_GetHomeSubChildMenu(subMenuId).ToList();
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return subChildMenuList;
        }

        #endregion

        #region Menu
        public ResponseOut AddEditMenu(Menu menu)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                if (entities.Menus.Any(x => x.MenuName.Trim().ToUpper() == menu.MenuName.Trim().ToUpper() && menu.MenuId == 0))
                {
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.DuplicateMenu;

                }
                else
                {
                    if (menu.MenuId == 0)
                    {
                        menu.CreateDate = DateTime.Now;
                        entities.Menus.Add(menu);
                        responseOut.message = ActionMessage.MenuCreatedSuccess;
                    }
                    else
                    {
                        menu.ModifyBy = menu.CreatedBy;
                        menu.ModifyDate = DateTime.Now;
                        entities.Menus.Where(x => x.MenuId == menu.MenuId).ToList().ForEach(x =>
                        {
                            x.MenuId = menu.MenuId;
                            x.MenuName = menu.MenuName;
                            x.MenuLink = menu.MenuLink;
                            x.MenuStatus = menu.MenuStatus;
                            x.SequenceNo = menu.SequenceNo;
                            x.ModifyBy = menu.ModifyBy;
                            x.ModifyDate = menu.ModifyDate;
                        });
                        responseOut.message = ActionMessage.MenuUpdatedSuccess;
                    }
                    entities.SaveChanges();
                    responseOut.status = ActionStatus.Success;
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return responseOut;
        }

        public List<Menu> GetMenuList()
        {
            List<Menu> menuList = new List<Menu>();
            try
            {
                var menus = entities.Menus.ToList();
                foreach (var item in menus)
                {
                    menuList.Add(new Menu
                    {
                        MenuId = item.MenuId,
                        MenuName = item.MenuName,
                        MenuLink = item.MenuLink,
                        MenuStatus = item.MenuStatus,
                        SequenceNo = item.SequenceNo
                    });
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return menuList;
        }

        public ResponseOut RemoveMenu(int menuId)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                var menu = entities.Menus.First(x => x.MenuId == menuId);
                entities.Menus.Remove(menu);
                entities.SaveChanges();
                responseOut.status = ActionStatus.Success;
                responseOut.message = ActionMessage.RemoveMenu;
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
        #endregion

        #region Sub Menu
        public ResponseOut AddEditSubMenu(SubMenu subMenu)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                if (entities.SubMenus.Any(x => x.SubMenuName.Trim().ToUpper() == subMenu.SubMenuName.Trim().ToUpper() && subMenu.SubMenuId == 0))
                {
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.SubMenuDuplicate;
                }
                else
                {
                    if (subMenu.SubMenuId == 0)
                    {

                        entities.SubMenus.Add(subMenu);
                        entities.SaveChanges();
                        responseOut.message = ActionMessage.SubMenuCreatedSuccess;
                    }
                    else
                    {
                        entities.SubMenus.Where(x => x.SubMenuId == subMenu.SubMenuId).ToList().ForEach(x =>
                        {
                            x.MenuId = subMenu.MenuId;
                            x.PageName = subMenu.PageName;
                            x.SubMenuName = subMenu.SubMenuName;
                            x.SubMenuLink = subMenu.SubMenuLink;
                            x.SubMenuStatus = subMenu.SubMenuStatus;
                            x.SequenceNo = subMenu.SequenceNo;
                        });
                        responseOut.message = ActionMessage.SubMenuUpdatedSuccess;
                    }
                    entities.SaveChanges();
                    responseOut.status = ActionStatus.Success;
                }


            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);

            }
            return responseOut;
        }

        public List<SubMenu> GetSubMenuList()
        {
            List<SubMenu> subMenuList = new List<SubMenu>();
            try
            {
                var subMenus = entities.SubMenus.Join(entities.Menus,
                   sm => sm.MenuId,
                   m => m.MenuId, (sm, m) => new
                   {
                       SubMenuId = sm.SubMenuId,
                       MenuId = sm.MenuId,
                       PageName = sm.PageName,
                       SubMenuName = sm.SubMenuName,
                       SubMenuLink = sm.SubMenuLink,
                       SubMenuStatus = sm.SubMenuStatus,
                       SequenceNo = sm.SequenceNo,
                       MenuName = m.MenuName
                   }).ToList();


                foreach (var item in subMenus)
                {
                    subMenuList.Add(new SubMenu
                    {
                        SubMenuId = item.SubMenuId,
                        MenuId = item.MenuId,
                        SubMenuName = item.SubMenuName,
                        PageName = item.PageName,
                        SubMenuLink = item.SubMenuLink,
                        SubMenuStatus = item.SubMenuStatus,
                        SequenceNo = item.SequenceNo,
                        MenuName = item.MenuName
                    });
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return subMenuList;
        }

        public ResponseOut RemoveSubMenu(int subMenuId)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                var submenu = entities.SubMenus.First(x => x.SubMenuId == subMenuId);
                entities.SubMenus.Remove(submenu);
                entities.SaveChanges();
                responseOut.status = ActionStatus.Success;
                responseOut.message = ActionMessage.SubMenuRemove;
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

        public List<SubMenu> GetSubMenuList(int menuId = 0)
        {
            List<SubMenu> subMenuList = new List<SubMenu>();
            try
            {
                var subMenus = entities.SubMenus.Join(entities.Menus,
                   sm => sm.MenuId,
                   m => m.MenuId, (sm, m) => new
                   {
                       SubMenuId = sm.SubMenuId,
                       MenuId = sm.MenuId,
                       PageName = sm.PageName,
                       SubMenuName = sm.SubMenuName,
                       SubMenuLink = sm.SubMenuLink,
                       SubMenuStatus = sm.SubMenuStatus,
                       SequenceNo = sm.SequenceNo,
                       MenuName = m.MenuName
                   }).Where(x => x.MenuId == menuId).ToList();


                foreach (var item in subMenus)
                {
                    subMenuList.Add(new SubMenu
                    {
                        SubMenuId = item.SubMenuId,
                        MenuId = item.MenuId,
                        SubMenuName = item.SubMenuName,
                        PageName = item.PageName,
                        SubMenuLink = item.SubMenuLink,
                        SubMenuStatus = item.SubMenuStatus,
                        SequenceNo = item.SequenceNo,
                        MenuName = item.MenuName
                    });
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return subMenuList;
        }
        #endregion


        #region Sub Child Menu
        public ResponseOut AddEditSubChildMenu(SubChildMenu subChildMenu)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                if (entities.SubChildMenus.Any(x => x.SubChildMenuName.Trim().ToUpper() == subChildMenu.SubChildMenuName.Trim().ToUpper() && x.SubChildMenuId == 0))
                {
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.SubChildMenuDuplicate;
                }
                else
                {
                    if (subChildMenu.SubChildMenuId == 0)
                    {
                        entities.SubChildMenus.Add(subChildMenu);
                        entities.SaveChanges();
                        responseOut.message = ActionMessage.SubChildMenuCreatedSuccess;
                        responseOut.status = ActionStatus.Success;
                    }
                    else
                    {
                        entities.SubChildMenus.Where(x => x.SubChildMenuId == subChildMenu.SubChildMenuId).ToList().ForEach(x =>
                        {
                            x.MenuId = subChildMenu.MenuId;
                            x.SubChildMenuId = subChildMenu.SubChildMenuId;
                            x.SubMenuId = subChildMenu.SubMenuId;
                            x.SubChildMenuName = subChildMenu.SubChildMenuName;
                            x.SubChildPageName = subChildMenu.SubChildPageName;
                            x.SubChildMenuLink = subChildMenu.SubChildMenuLink;
                            x.SubChildMenuStatus = subChildMenu.SubChildMenuStatus;
                        });
                        responseOut.message = ActionMessage.SubChildMenuUpdatedSuccess;

                    }
                    entities.SaveChanges();
                    responseOut.status = ActionStatus.Success;
                }

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return responseOut;
        }

        public List<SubChildMenu> GetSubChildMenuList(int subMenuId = 0)
        {
            List<SubChildMenu> subChildMenuList = new List<SubChildMenu>();
            try
            {

                if (subMenuId > 0)
                {
                    var subChildMenus = entities.SubChildMenus.Join(entities.SubMenus,
                          scm => scm.SubMenuId,
                          sm => sm.SubMenuId, (scm, sm) => new
                          {
                              SubChildMenuId = scm.SubChildMenuId,
                              SubMenuId = scm.SubMenuId,
                              SubChildMenuName = scm.SubChildMenuName,
                              SubChildPageName = scm.SubChildPageName,
                              SubChildMenuLink = scm.SubChildMenuLink,
                              SubChildMenuStatus = scm.SubChildMenuStatus,
                              SequenceNo = scm.SequenceNo,
                              SubMenuName = sm.SubMenuName,
                              MenuId = scm.MenuId

                          }).Where(x => x.SubMenuId == subMenuId).ToList();

                    foreach (var item in subChildMenus)
                    {
                        subChildMenuList.Add(new SubChildMenu
                        {
                            SubChildMenuId = item.SubChildMenuId,
                            SubMenuId = item.SubMenuId,
                            SubChildMenuName = item.SubChildMenuName,
                            SubChildMenuLink = item.SubChildMenuLink,
                            SubChildPageName = item.SubChildPageName,
                            SubChildMenuStatus = item.SubChildMenuStatus,
                            SubMenuName = item.SubMenuName,
                            SequenceNo = item.SequenceNo,
                            MenuId = item.MenuId
                        });
                    }
                }
                else
                {
                    var subChildMenus = entities.SubChildMenus.Join(entities.SubMenus,
                       scm => scm.SubMenuId,
                       sm => sm.SubMenuId, (scm, sm) => new
                       {
                           SubChildMenuId = scm.SubChildMenuId,
                           SubMenuId = scm.SubMenuId,
                           SubChildMenuName = scm.SubChildMenuName,
                           SubChildPageName = scm.SubChildPageName,
                           SubChildMenuLink = scm.SubChildMenuLink,
                           SubChildMenuStatus = scm.SubChildMenuStatus,
                           SequenceNo = scm.SequenceNo,
                           MenuId = scm.MenuId,
                           SubMenuName = sm.SubMenuName
                       }).ToList();

                    foreach (var item in subChildMenus)
                    {
                        subChildMenuList.Add(new SubChildMenu
                        {
                            SubChildMenuId = item.SubChildMenuId,
                            SubMenuId = item.SubMenuId,
                            SubChildMenuName = item.SubChildMenuName,
                            SubChildMenuLink = item.SubChildMenuLink,
                            SubChildPageName = item.SubChildPageName,
                            SubChildMenuStatus = item.SubChildMenuStatus,
                            SubMenuName = item.SubMenuName,
                            SequenceNo = item.SequenceNo,
                            MenuId = item.MenuId
                        });
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);

            }
            return subChildMenuList;
        }

        public ResponseOut RemoveSubChildMenu(int subChildMenuId)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                var subChildMenu = entities.SubChildMenus.First(x => x.SubChildMenuId == subChildMenuId);
                entities.SubChildMenus.Remove(subChildMenu);
                entities.SaveChanges();
                responseOut.status = ActionStatus.Success;
                responseOut.message = ActionMessage.SubChildMenuRemove;
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
        #endregion


        #region UserRegistration
        public ResponseOut UserRegistration(UserRegistration userRegistration, out int userId)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                userId = Convert.ToInt32(userRegistration.UserId);

                //if (entities.UserRegistrations.Any(x => x.UserName == userRegistration.UserName && x.UserStatus == true))
                //{
                //    responseOut.status = ActionStatus.Fail;
                //    responseOut.message = ActionMessage.UserRegistrationDuplicate;
                //}
                //else
                //{

                //}

                if (userRegistration.UserId == 0)
                {
                    userRegistration.RegistrationDate = DateTime.Now;
                    entities.UserRegistrations.Add(userRegistration);
                    responseOut.message = ActionMessage.UserRegistrationSuccesswithCode + userRegistration.UserCode;
                    responseOut.UserCode = userRegistration.UserCode;
                    responseOut.Name = userRegistration.FirstName + ' ' + userRegistration.LastName;
                    responseOut.Email = userRegistration.Email;
                }
                else
                {
                    entities.UserRegistrations.Where(x => x.UserId == userRegistration.UserId && x.UserName == userRegistration.UserName).ToList().ForEach(x =>
                    {
                        x.FirstName = userRegistration.FirstName;
                        x.LastName = userRegistration.LastName;
                        x.Email = userRegistration.Email;
                        x.ContactNo = userRegistration.ContactNo;
                        x.AlternateContactno = userRegistration.AlternateContactno;
                        x.WhatsaapPhone = userRegistration.WhatsaapPhone;
                        x.Fax = userRegistration.Fax;
                        x.StateId = userRegistration.StateId;
                        x.City = userRegistration.City;
                        x.SubCity = userRegistration.SubCity;
                        x.Landmark = userRegistration.Landmark;
                        x.BuildingNo = userRegistration.BuildingNo;
                        x.PINCode = userRegistration.PINCode;
                        x.GSTNo = userRegistration.GSTNo;
                        x.PANNo = userRegistration.PANNo;
                        x.UdyogAadhaarNo = userRegistration.UdyogAadhaarNo;
                        x.KYCCode = userRegistration.KYCCode;
                        x.CompanyName = userRegistration.CompanyName;
                        x.ExpireDate = userRegistration.ExpireDate;
                        x.UserStatus = userRegistration.UserStatus;
                        x.UserCode = userRegistration.UserCode;
                    });
                    responseOut.message = ActionMessage.UserRegistrationUpdatedSuccess;
                }
                entities.SaveChanges();
                userId = Convert.ToInt32(userRegistration.UserId);
                responseOut.status = ActionStatus.Success;
                responseOut.UserCode = userRegistration.UserCode;
                responseOut.Name = userRegistration.FirstName + ' ' + userRegistration.LastName;
                responseOut.Email = userRegistration.Email;

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }

            return responseOut;
        }

        public ResponseOut UpdateUserPic(UserSupportingDocument userSupportingDocument)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                entities.UserSupportingDocuments.Where(x => x.UserId == userSupportingDocument.UserId).ToList().ForEach(x =>
                {
                    x.DocumentPath = userSupportingDocument.DocumentPath;
                });
                entities.SaveChanges();
                responseOut.status = ActionStatus.Success;
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

        #endregion

        #region Buyer seller Login

        public UserRegistration AuthenticateBSUser(string userName, string password)
        {

            UserRegistration userRegistration = new UserRegistration();
            try
            {
                if (entities.UserRegistrations.Any(x => x.UserCode == userName && x.Password == password && x.UserStatus == true))
                {
                    userRegistration = entities.UserRegistrations.Where(u => u.UserCode == userName && u.Password == password && u.UserStatus == true).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return userRegistration;
        }
        #endregion


        #region Buyer product
        //public List<GST> BindGSTList()
        //{
        //    List<GST> GSTList = new List<GST>();
        //    try
        //    {
        //        var uoms = entities.GST.Where(x => x.Status == true).Select(s => new
        //        {
        //            UOMId = s.UOMId,
        //            UOMName = s.UOMName
        //        }).ToList();
        //        if (uoms != null && uoms.Count > 0)
        //        {
        //            foreach (var item in uoms)
        //            {
        //                uomList.Add(new UOM { UOMId = item.UOMId, UOMName = item.UOMName });
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
        //        throw ex;
        //    }
        //    return uomList;
        //}

        public ResponseOut AddUpdateBuyerProduct(BuyerProduct buyerProduct, IEnumerable<ProductTechSpecification> specifications, int companyId, IEnumerable<Brand> brand, IEnumerable<ProductCode> productCode)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                string currentBrandID = "";
                String CurrentProductID = "";

                BuyerProduct buyerProducExisting = entities.BuyerProducts.FirstOrDefault(x => x.BuyerProductDetailId == buyerProduct.BuyerProductDetailId);
                if (buyerProducExisting == null)
                {
                    using (var transaction = entities.Database.BeginTransaction())
                    {

                        foreach (var item in brand)
                        {
                            if (item.BrandID == -1)
                            {
                                entities.Brands.Add(item);
                                entities.SaveChanges();
                                currentBrandID = item.BrandID.ToString();
                                break;
                            }

                        }


                        foreach (var item in productCode)
                        {
                            if (item.ProductCodeID == -1)
                            {
                                entities.ProductCodes.Add(item);
                                entities.SaveChanges();
                                CurrentProductID = item.ProductCodeID.ToString();
                                break;
                            }

                        }
                        if (currentBrandID == "")
                        {
                            currentBrandID = buyerProduct.BrandName;
                        }

                        if (CurrentProductID == "")
                        {
                            CurrentProductID = buyerProduct.ProductCode;
                        }


                        Product product = new Product
                        {
                            ProductName = buyerProduct.ProductName,

                            ProductCode = CurrentProductID,
                            ProductShortDesc = buyerProduct.ProductName,
                            ProductFullDesc = buyerProduct.ProductName,
                            CompanyId = companyId,
                            ProductTypeId = 0,
                            ProductMainGroupId = Convert.ToInt32(buyerProduct.ParentClassId),
                            ProductSubGroupId = Convert.ToInt32(buyerProduct.CategoryId),
                            ProductSubChildGroupId = Convert.ToInt32(buyerProduct.SubCategoryId),
                            Status = true,
                            CreatedBy = Convert.ToInt32(buyerProduct.BuyerId),
                            CreatedDate = DateTime.Now,
                            BrandName = currentBrandID
                        };
                        entities.Products.Add(product);
                        entities.SaveChanges();

                        buyerProduct.ProductId = product.Productid;
                        buyerProduct.CreatedDate = DateTime.Now;
                        buyerProduct.ProductCode = CurrentProductID;
                        buyerProduct.BrandName = currentBrandID;
                        buyerProduct.BrandId = Convert.ToInt32(currentBrandID);

                        entities.BuyerProducts.Add(buyerProduct);
                        entities.SaveChanges();

                        List<long> specs = new List<long>();
                        foreach (var item in specifications)
                        {
                            item.ProductId = product.Productid;
                            if (item.ProductTechSpecId == -1)
                            {
                                entities.ProductTechSpecifications.Add(item);
                                entities.SaveChanges();
                            }
                            entities.BuyerProductTechSpecifications.Add(new BuyerProductTechSpecification { BuyerProductId = buyerProduct.BuyerProductDetailId, ProductTechSpecId = item.ProductTechSpecId, ProductTechSpecValue = item.ProductTechSpecValue, UomId = item.UomId });

                            specs.Add(item.ProductTechSpecId);

                        }

                        foreach (var item in specs)
                        {
                        }
                        entities.SaveChanges();
                        transaction.Commit();
                    };
                }
                else
                {

                    foreach (var item in brand)
                    {
                        if (item.BrandID == -1)
                        {
                            entities.Brands.Add(item);
                            entities.SaveChanges();
                            currentBrandID = item.BrandID.ToString();
                        }

                    }


                    foreach (var item in productCode)
                    {
                        if (item.ProductCodeID == -1)
                        {
                            entities.ProductCodes.Add(item);
                            entities.SaveChanges();
                            CurrentProductID = item.ProductCodeID.ToString();
                        }

                    }
                    if (currentBrandID != "")
                    {
                        buyerProducExisting.BrandName = currentBrandID;
                    }
                    else
                    { buyerProducExisting.BrandName = buyerProduct.BrandName; }

                    buyerProducExisting.BrandId = buyerProduct.BrandId;

                    buyerProducExisting.BuyerCode = buyerProduct.BuyerCode;
                    buyerProducExisting.BuyerId = buyerProduct.BuyerId;
                    buyerProducExisting.CategoryId = buyerProduct.CategoryId;
                    buyerProducExisting.ParentClassId = buyerProduct.ParentClassId;

                    if (CurrentProductID != "")
                    {
                        buyerProducExisting.ProductCode = CurrentProductID;
                    }
                    else
                    {
                        buyerProducExisting.ProductCode = buyerProduct.ProductCode;
                    }
                    buyerProducExisting.ProductId = buyerProduct.ProductId;
                    buyerProducExisting.ProductName = buyerProduct.ProductName;
                    //buyerProducExisting.SpecificationName = buyerProduct.SpecificationName;
                    //buyerProducExisting.SpecificationValue = buyerProduct.SpecificationValue;
                    buyerProducExisting.SubCategoryId = buyerProduct.SubCategoryId;
                    //buyerProducExisting.UomId = buyerProduct.UomId;
                }
                entities.SaveChanges();
                responseOut.status = ActionStatus.Success;
                responseOut.message = ActionMessage.BuyerProductCreated;
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

        public ResponseOut AddUpdateSellerProduct(SellerProduct sellerProduct, IEnumerable<ProductTechSpecification> specifications, int companyId, IEnumerable<Brand> brand, IEnumerable<ProductCode> productCode)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                string currentBrandID = "";
                String CurrentProductID = "";

                SellerProduct sellerProducExisting = entities.SellerProducts.FirstOrDefault(x => x.SellerProductDetailId == sellerProduct.SellerProductDetailId);
                if (sellerProducExisting == null)
                {
                    using (var transaction = entities.Database.BeginTransaction())
                    {
                        foreach (var item in brand)
                        {
                            if (item.BrandID == -1)
                            {
                                entities.Brands.Add(item);
                                entities.SaveChanges();
                                currentBrandID = item.BrandID.ToString();
                                break;
                            }

                        }


                        foreach (var item in productCode)
                        {
                            if (item.ProductCodeID == -1)
                            {
                                entities.ProductCodes.Add(item);
                                entities.SaveChanges();
                                CurrentProductID = item.ProductCodeID.ToString();
                                break;
                            }

                        }
                        if (currentBrandID == "")
                        {
                            currentBrandID = sellerProduct.BrandName;
                        }

                        if (CurrentProductID == "")
                        {
                            CurrentProductID = sellerProduct.ProductCode;
                        }

                        Product product = new Product
                        {
                            ProductName = sellerProduct.ProductName,
                            ProductCode = CurrentProductID,
                            ProductShortDesc = sellerProduct.ProductName,
                            ProductFullDesc = sellerProduct.ProductName,
                            CompanyId = companyId,
                            ProductTypeId = 0,
                            ProductMainGroupId = Convert.ToInt32(sellerProduct.ParentClassId),
                            ProductSubGroupId = Convert.ToInt32(sellerProduct.CategoryId),
                            ProductSubChildGroupId = Convert.ToInt32(sellerProduct.SubCategoryId),
                            Status = true,
                            CreatedBy = Convert.ToInt32(sellerProduct.SellerId),
                            CreatedDate = DateTime.Now,
                            BrandName = currentBrandID,

                        };
                        entities.Products.Add(product);
                        entities.SaveChanges();

                        sellerProduct.ProductId = product.Productid;
                        sellerProduct.CreatedDate = DateTime.Now;
                        sellerProduct.ProductCode = CurrentProductID;
                        sellerProduct.BrandName = currentBrandID;
                        sellerProduct.BrandId = Convert.ToInt32(currentBrandID);

                        entities.SellerProducts.Add(sellerProduct);
                        entities.SaveChanges();

                        List<long> specs = new List<long>();
                        foreach (var item in specifications)
                        {
                            item.ProductId = product.Productid;
                            if (item.ProductTechSpecId == -1)
                            {
                                entities.ProductTechSpecifications.Add(item);
                                entities.SaveChanges();
                            }
                            entities.SellerProductTechSpecifications.Add(new SellerProductTechSpecification { SellerProductId = sellerProduct.SellerProductDetailId, ProductTechSpecId = item.ProductTechSpecId, ProductTechSpecValue = item.ProductTechSpecValue, UomId = item.UomId });


                            specs.Add(item.ProductTechSpecId);
                        }

                        foreach (var item in specs)
                        {
                            // entities.SellerProductTechSpecifications.Add(new SellerProductTechSpecification { SellerProductId = sellerProduct.SellerProductDetailId, ProductTechSpecId = item });
                        }
                        entities.SaveChanges();
                        transaction.Commit();
                    };
                }
                else
                {
                    foreach (var item in brand)
                    {
                        if (item.BrandID == -1)
                        {
                            entities.Brands.Add(item);
                            entities.SaveChanges();
                            currentBrandID = item.BrandID.ToString();
                        }

                    }


                    foreach (var item in productCode)
                    {
                        if (item.ProductCodeID == -1)
                        {
                            entities.ProductCodes.Add(item);
                            entities.SaveChanges();
                            CurrentProductID = item.ProductCodeID.ToString();
                        }

                    }
                    if (currentBrandID != "")
                    {
                        sellerProducExisting.BrandName = currentBrandID;
                    }
                    else
                    { sellerProducExisting.BrandName = sellerProducExisting.BrandName; }


                    sellerProducExisting.BrandId = sellerProduct.BrandId;
                    // sellerProducExisting.BrandName = sellerProduct.BrandName;
                    sellerProducExisting.SellerCode = sellerProduct.SellerCode;
                    sellerProducExisting.SellerId = sellerProduct.SellerId;
                    sellerProducExisting.CategoryId = sellerProduct.CategoryId;
                    sellerProducExisting.ParentClassId = sellerProduct.ParentClassId;
                    // sellerProducExisting.ProductCode = sellerProduct.ProductCode;
                    if (CurrentProductID != "")
                    {
                        sellerProducExisting.ProductCode = CurrentProductID;
                    }
                    else
                    {
                        sellerProducExisting.ProductCode = sellerProducExisting.ProductCode;
                    }

                    sellerProducExisting.ProductId = sellerProduct.ProductId;
                    sellerProducExisting.ProductName = sellerProduct.ProductName;
                    sellerProducExisting.SpecificationName = sellerProduct.SpecificationName;
                    sellerProducExisting.SpecificationValue = sellerProduct.SpecificationValue;
                    sellerProducExisting.SubCategoryId = sellerProduct.SubCategoryId;
                    sellerProducExisting.UomId = sellerProduct.UomId;
                    sellerProducExisting.GSTID = sellerProduct.GSTID;
                    sellerProducExisting.GSTPercentageID = sellerProduct.GSTPercentageID;
                    sellerProducExisting.DeliveryStatusID = sellerProduct.DeliveryStatusID;
                }
                entities.SaveChanges();
                responseOut.status = ActionStatus.Success;
                responseOut.message = ActionMessage.SellerProductCreated;
            }
            catch (Exception ex)
            {
                responseOut.status = ActionStatus.Fail;
                responseOut.message = ActionMessage.ApplicationException;
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            //catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            //{
            //    Exception raise = dbEx;
            //    foreach (var validationErrors in dbEx.EntityValidationErrors)
            //    {
            //        foreach (var validationError in validationErrors.ValidationErrors)
            //        {
            //            string message = string.Format("{0}:{1}",
            //                validationErrors.Entry.Entity.ToString(),
            //                validationError.ErrorMessage);
            //            // raise a new exception nesting  
            //            // the current instance as InnerException  
            //            raise = new InvalidOperationException(message, raise);
            //        }
            //    }
            //    throw raise;
            //}
            return responseOut;
        }




        #endregion

        public List<UOM> GetUOMAutoCompleteList(string searchTerm, bool IsSUM)
        {
            List<UOM> uomList = new List<UOM>();
            try
            {
                var uom = (from p in entities.UOMs
                                where (p.UOMName.ToLower().Contains(searchTerm.ToLower())) && p.IsSUM == IsSUM  && p.Status == true
                                select new
                                {
                                    UOMId = p.UOMId,
                                    UOMName = p.UOMName
                                   

                                }).ToList();
                if (uom != null && uom.Count > 0)
                {
                    foreach (var item in uom)
                    {
                        uomList.Add(new UOM { UOMId = item.UOMId, UOMName = item.UOMName });
                    }
                }


            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return uomList;
        }


    }
}

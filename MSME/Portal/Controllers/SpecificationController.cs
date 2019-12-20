using System.Web.Mvc;
using Portal.Core;
using Portal.Core.Specification;
using Portal.Common;
using System.Reflection;
using System.Collections.Generic;
using Portal.DAL;
using System.Linq;

namespace Portal.Controllers
{
    public class SpecificationController : BaseController
    {
        SpecificationBL _specificationBL = new SpecificationBL();
        public ActionResult GetSpecifications(long productMainGroupId, long productSubGroupId, long productChildSubGroupId)
        {
            try
            {
                var specs = _specificationBL.GetSpecifications(productMainGroupId, productSubGroupId, productChildSubGroupId);
                specs = specs.Where(x => !string.IsNullOrWhiteSpace(x.ProductTechSpecName));
                specs = specs.OrderBy(x => x.ProductTechSpecName);
                List<ProductTechSpecification> distinctSpecs = new List<ProductTechSpecification>();
                foreach (ProductTechSpecification spec in specs)
                {
                    if (distinctSpecs.Any(x => x.ProductTechSpecName.Trim().Equals(spec.ProductTechSpecName.Trim(), System.StringComparison.CurrentCultureIgnoreCase)))
                        continue;
                    distinctSpecs.Add(spec);
                }
                return Json(distinctSpecs, JsonRequestBehavior.AllowGet);
            }
            catch (System.Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(new List<ProductTechSpecification>(), JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetSpecificationsByName(string specName)
        {
            try
            {
                var specs = _specificationBL.GetSpecifications(specName);
                specs = specs.Where(x => !string.IsNullOrWhiteSpace(x.ProductTechSpecValue));
                specs = specs.OrderBy(x => x.ProductTechSpecValue);
                List<ProductTechSpecification> distinctSpecs = new List<ProductTechSpecification>();
                foreach (ProductTechSpecification spec in specs)
                {
                    if (distinctSpecs.Any(x => x.ProductTechSpecValue.Trim().Equals(spec.ProductTechSpecValue.Trim(), System.StringComparison.CurrentCultureIgnoreCase)))
                        continue;
                    distinctSpecs.Add(spec);
                }
                return Json(distinctSpecs, JsonRequestBehavior.AllowGet);
            }
            catch (System.Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(new List<ProductTechSpecification>(), JsonRequestBehavior.AllowGet);

        }

    }
}

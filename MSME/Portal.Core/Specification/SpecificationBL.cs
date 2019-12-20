using Portal.DAL;
using System.Collections.Generic;

namespace Portal.Core.Specification
{
    public class SpecificationBL
    {
        DBInterface _dbInterface;
        public SpecificationBL()
        {
            _dbInterface = new DBInterface();
        }

        public IEnumerable<ProductTechSpecification> GetSpecifications(long productMainGroupId, long productSubGroupId, long productChildSubGroupId)
        {
            return _dbInterface.GetSpecifications(productMainGroupId, productSubGroupId, productChildSubGroupId);
        }

        public IEnumerable<ProductTechSpecification> GetSpecifications(string specName)
        {
            return _dbInterface.GetSpecifications(specName);
        }
    }
}

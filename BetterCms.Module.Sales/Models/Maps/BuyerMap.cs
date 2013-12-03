using BetterCms.Core.Models;

namespace BetterCms.Module.Sales.Models.Maps
{
    public class BuyerMap : EntitySubClassMapBase<Buyer>
    {
        public BuyerMap()
            : base(SalesModuleDescriptor.ModuleName)
        {
            Table("Buyers");
        }
    }
}
using BetterCms.Core.Models;

namespace BetterCms.Module.Sales.Models.Maps
{
    public class SaleProductMap : EntityMapBase<SaleProduct>
    {
        public SaleProductMap()
            : base(SalesModuleDescriptor.ModuleName)
        {
            Table("SaleProducts");

            Map(f => f.Quantity).Not.Nullable();
            Map(f => f.Price).Not.Nullable();

            References(x => x.Product).Cascade.SaveUpdate().LazyLoad().Not.Nullable();
            References(x => x.Sale).Cascade.SaveUpdate().LazyLoad().Not.Nullable();
        }
    }
}
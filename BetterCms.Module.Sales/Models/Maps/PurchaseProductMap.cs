using BetterCms.Core.Models;

namespace BetterCms.Module.Sales.Models.Maps
{
    public class PurchaseProductMap : EntityMapBase<PurchaseProduct>
    {
        public PurchaseProductMap()
            : base(SalesModuleDescriptor.ModuleName)
        {
            Table("PurchaseProducts");

            Map(f => f.Quantity).Not.Nullable();
            Map(f => f.Price).Not.Nullable();

            References(x => x.Product).Cascade.SaveUpdate().LazyLoad().Not.Nullable();
            References(x => x.Purchase).Cascade.SaveUpdate().LazyLoad().Not.Nullable();
        }
    }
}
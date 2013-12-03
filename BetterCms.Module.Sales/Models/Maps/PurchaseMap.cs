using BetterCms.Core.Models;

namespace BetterCms.Module.Sales.Models.Maps
{
    public class PurchaseMap : EntityMapBase<Purchase>
    {
        public PurchaseMap()
            : base(SalesModuleDescriptor.ModuleName)
        {
            Table("Purchases");

            Map(f => f.Status).Not.Nullable();

            References(x => x.Supplier).Cascade.SaveUpdate().LazyLoad().Not.Nullable();

            HasMany(x => x.Products).KeyColumn("PurchaseId").Cascade.SaveUpdate().Inverse().LazyLoad().Where("IsDeleted = 0");
        }
    }
}
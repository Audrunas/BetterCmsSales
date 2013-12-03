using BetterCms.Core.Models;

namespace BetterCms.Module.Sales.Models.Maps
{
    public class SaleMap : EntityMapBase<Sale>
    {
        public SaleMap()
            : base(SalesModuleDescriptor.ModuleName)
        {
            Table("Sales");

            Map(f => f.Status).Not.Nullable();

            References(x => x.Buyer).Cascade.SaveUpdate().LazyLoad().Not.Nullable();

            HasMany(x => x.Products).KeyColumn("SaleId").Cascade.SaveUpdate().Inverse().LazyLoad().Where("IsDeleted = 0");
        }
    }
}
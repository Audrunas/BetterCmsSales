using BetterCms.Core.Models;

namespace BetterCms.Module.Sales.Models.Maps
{
    public class ProductMap : EntityMapBase<Product>
    {
        public ProductMap()
            : base(SalesModuleDescriptor.ModuleName)
        {
            Table("Products");

            Map(f => f.Name).Not.Nullable().Length(MaxLength.Name);
            Map(f => f.Price).Not.Nullable();
            
            References(x => x.Category).Cascade.SaveUpdate().LazyLoad().Nullable();
            References(x => x.Unit).Cascade.SaveUpdate().LazyLoad().Not.Nullable();
            References(x => x.Image).Column("MediaImageId").Cascade.SaveUpdate().LazyLoad().Nullable();
        }
    }
}
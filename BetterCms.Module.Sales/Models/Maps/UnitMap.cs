using BetterCms.Core.Models;

namespace BetterCms.Module.Sales.Models.Maps
{
    public class UnitMap : EntityMapBase<Unit>
    {
        public UnitMap()
            : base(SalesModuleDescriptor.ModuleName)
        {
            Table("Units");

            Map(f => f.Title).Not.Nullable().Length(MaxLength.Name);
            Map(f => f.ShortTitle).Not.Nullable().Length(MaxLength.Name);
        }
    }
}
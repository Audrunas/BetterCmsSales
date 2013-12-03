using BetterCms.Core.Models;

namespace BetterCms.Module.Sales.Models.Maps
{
    public class PartnerMap : EntityMapBase<Partner>
    {
        public PartnerMap()
            : base(SalesModuleDescriptor.ModuleName)
        {
            Table("Partners");

            Map(f => f.Name).Not.Nullable().Length(MaxLength.Name);
            Map(f => f.Email).Nullable().Length(MaxLength.Email);
            Map(f => f.Address).Nullable().Length(MaxLength.Text);
            Map(f => f.PhoneNumber).Nullable().Length(MaxLength.Name);
        }
    }
}
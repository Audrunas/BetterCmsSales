using BetterCms.Core.Models;

namespace BetterCms.Module.Sales.Models.Maps
{
    public class SupplierMap : EntitySubClassMapBase<Supplier>
    {
        public SupplierMap()
            : base(SalesModuleDescriptor.ModuleName)
        {
            Table("Suppliers");
        }
    }
}
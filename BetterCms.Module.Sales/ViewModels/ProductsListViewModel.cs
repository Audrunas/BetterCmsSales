using System.Collections.Generic;
using BetterCms.Module.Root.Mvc.Grids.GridOptions;
using BetterCms.Module.Root.ViewModels.SiteSettings;

namespace BetterCms.Module.Sales.ViewModels
{
    public class ProductsListViewModel : SearchableGridViewModel<ProductViewModel>
    {
        public ProductsListViewModel(IEnumerable<ProductViewModel> items, SearchableGridOptions options, int totalCount) 
            : base(items, options, totalCount)
        {
        }

        public List<UnitViewModel> Units { get; set; } 
    }
}
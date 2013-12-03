using System.Linq;

using BetterCms.Core.DataAccess.DataContext;
using BetterCms.Core.Mvc.Commands;

using BetterCms.Module.Root.Mvc;
using BetterCms.Module.Root.Mvc.Grids.Extensions;
using BetterCms.Module.Root.Mvc.Grids.GridOptions;
using BetterCms.Module.Root.ViewModels.SiteSettings;

using BetterCms.Module.Sales.Models;
using BetterCms.Module.Sales.ViewModels;

namespace BetterCms.Module.Sales.Command.GetProductList
{
    public class GetProductListCommand : CommandBase, ICommand<SearchableGridOptions, SearchableGridViewModel<ProductViewModel>>
    {
        /// <summary>
        /// Executes the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>List with product view models</returns>
        public SearchableGridViewModel<ProductViewModel> Execute(SearchableGridOptions request)
        {
            SearchableGridViewModel<ProductViewModel> model;

            request.SetDefaultSortingOptions("Name");

            var query = Repository
                .AsQueryable<Product>();

            if (!string.IsNullOrWhiteSpace(request.SearchQuery))
            {
                query = query.Where(a => a.Name.Contains(request.SearchQuery));
            }

            var products = query
                .Select(product =>
                    new ProductViewModel
                    {
                        Id = product.Id,
                        Version = product.Version,
                        Name = product.Name
                    });

            var count = query.ToRowCountFutureValue();
            products = products.AddSortingAndPaging(request);

            model = new SearchableGridViewModel<ProductViewModel>(products.ToList(), request, count.Value);

            return model;
        }
    }
}
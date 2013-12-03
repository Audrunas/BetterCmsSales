using System.Linq;

using BetterCms.Core.DataAccess.DataContext;
using BetterCms.Core.Mvc.Commands;

using BetterCms.Module.Root.Mvc;
using BetterCms.Module.Root.Mvc.Grids.Extensions;
using BetterCms.Module.Root.Mvc.Grids.GridOptions;
using BetterCms.Module.Root.ViewModels.SiteSettings;
using BetterCms.Module.Sales.Services;
using BetterCms.Module.Sales.ViewModels;

namespace BetterCms.Module.Sales.Command.Product.GetProductList
{
    public class GetProductListCommand : CommandBase, ICommand<SearchableGridOptions, ProductsListViewModel>
    {
        private readonly IUnitService unitService;

        public GetProductListCommand(IUnitService unitService)
        {
            this.unitService = unitService;
        }

        /// <summary>
        /// Executes the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>List with product view models</returns>
        public ProductsListViewModel Execute(SearchableGridOptions request)
        {
            ProductsListViewModel model;

            request.SetDefaultSortingOptions("Name");

            var query = Repository
                .AsQueryable<Models.Product>();

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
                        Name = product.Name,
                        Unit = product.Unit.Id,
                        UnitName = product.Unit.Title
                    });

            var count = query.ToRowCountFutureValue();
            products = products.AddSortingAndPaging(request);

            model = new ProductsListViewModel(products.ToList(), request, count.Value);
            model.Units = unitService.GetAllUnits();

            return model;
        }
    }
}
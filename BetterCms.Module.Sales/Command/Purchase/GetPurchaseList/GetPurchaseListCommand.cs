using System.Linq;

using BetterCms.Core.DataAccess.DataContext;
using BetterCms.Core.Mvc.Commands;

using BetterCms.Module.Root.Mvc;
using BetterCms.Module.Root.Mvc.Grids.Extensions;
using BetterCms.Module.Root.Mvc.Grids.GridOptions;
using BetterCms.Module.Root.ViewModels.SiteSettings;

using BetterCms.Module.Sales.ViewModels;

namespace BetterCms.Module.Sales.Command.Purchase.GetPurchaseList
{
    public class GetPurchaseListCommand : CommandBase, ICommand<SearchableGridOptions, SearchableGridViewModel<PurchaseViewModel>>
    {
        /// <summary>
        /// Executes the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>List with purchase view models</returns>
        public SearchableGridViewModel<PurchaseViewModel> Execute(SearchableGridOptions request)
        {
            SearchableGridViewModel<PurchaseViewModel> model;

            request.SetDefaultSortingOptions("CreatedOn", true);

            var query = Repository
                .AsQueryable<Models.Purchase>();

            if (!string.IsNullOrWhiteSpace(request.SearchQuery))
            {
                query = query.Where(a => a.Supplier.Name.Contains(request.SearchQuery)
                    || a.Products.Any(p => p.Product.Name.Contains(request.SearchQuery)));
            }

            var purchases = query
                .Select(purchase =>
                    new PurchaseViewModel
                    {
                        Id = purchase.Id,
                        Version = purchase.Version,
                        CreatedOn = purchase.CreatedOn,
                        SupplierName = purchase.Supplier.Name,
                        Status = purchase.Status
                    });

            var count = query.ToRowCountFutureValue();
            purchases = purchases.AddSortingAndPaging(request);

            model = new SearchableGridViewModel<PurchaseViewModel>(purchases.ToList(), request, count.Value);

            return model;
        }
    }
}
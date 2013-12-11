using System.Linq;

using BetterCms.Core.DataAccess.DataContext;
using BetterCms.Core.Mvc.Commands;

using BetterCms.Module.Root.Mvc;
using BetterCms.Module.Root.Mvc.Grids.Extensions;
using BetterCms.Module.Root.Mvc.Grids.GridOptions;
using BetterCms.Module.Root.ViewModels.SiteSettings;

using BetterCms.Module.Sales.ViewModels;

namespace BetterCms.Module.Sales.Command.Supplier.GetSupplierList
{
    public class GetSupplierListCommand : CommandBase, ICommand<SearchableGridOptions, SearchableGridViewModel<PartnerViewModel>>
    {
        /// <summary>
        /// Executes the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>List with supplier view models</returns>
        public SearchableGridViewModel<PartnerViewModel> Execute(SearchableGridOptions request)
        {
            SearchableGridViewModel<PartnerViewModel> model;

            request.SetDefaultSortingOptions("Name");

            var query = Repository
                .AsQueryable<Models.Supplier>();

            if (!string.IsNullOrWhiteSpace(request.SearchQuery))
            {
                query = query.Where(a => a.Name.Contains(request.SearchQuery)
                    || a.Email.Contains(request.SearchQuery)
                    || a.Address.Contains(request.SearchQuery)
                    || a.PhoneNumber.Contains(request.SearchQuery));
            }

            var suppliers = query
                .Select(supplier =>
                    new PartnerViewModel
                    {
                        Id = supplier.Id,
                        Version = supplier.Version,
                        Name = supplier.Name,
                        Address = supplier.Address,
                        Email = supplier.Email,
                        PhoneNumber = supplier.PhoneNumber
                    });

            var count = suppliers.ToRowCountFutureValue();
            suppliers = suppliers.AddSortingAndPaging(request);

            model = new SearchableGridViewModel<PartnerViewModel>(suppliers.ToList(), request, count.Value);

            return model;
        }
    }
}
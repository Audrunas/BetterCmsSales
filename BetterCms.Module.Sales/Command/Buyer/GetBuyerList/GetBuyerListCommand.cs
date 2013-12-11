using System.Linq;

using BetterCms.Core.DataAccess.DataContext;
using BetterCms.Core.Mvc.Commands;

using BetterCms.Module.Root.Mvc;
using BetterCms.Module.Root.Mvc.Grids.Extensions;
using BetterCms.Module.Root.Mvc.Grids.GridOptions;
using BetterCms.Module.Root.ViewModels.SiteSettings;

using BetterCms.Module.Sales.ViewModels;
using NHibernate.Linq;

namespace BetterCms.Module.Sales.Command.Buyer.GetBuyerList
{
    public class GetBuyerListCommand : CommandBase, ICommand<SearchableGridOptions, SearchableGridViewModel<PartnerViewModel>>
    {
        /// <summary>
        /// Executes the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>List with buyer view models</returns>
        public SearchableGridViewModel<PartnerViewModel> Execute(SearchableGridOptions request)
        {
            SearchableGridViewModel<PartnerViewModel> model;

            request.SetDefaultSortingOptions("Name");

            var query = Repository
                .AsQueryable<Models.Buyer>();

            if (!string.IsNullOrWhiteSpace(request.SearchQuery))
            {
                query = query.Where(a => a.Name.Contains(request.SearchQuery)
                    || a.Email.Contains(request.SearchQuery)
                    || a.Address.Contains(request.SearchQuery)
                    || a.PhoneNumber.Contains(request.SearchQuery));
            }

            var buyers = query
                .Select(buyer =>
                    new PartnerViewModel
                    {
                        Id = buyer.Id,
                        Version = buyer.Version,
                        Name = buyer.Name,
                        Address = buyer.Address,
                        Email = buyer.Email,
                        PhoneNumber = buyer.PhoneNumber
                    });

            var count = buyers.ToRowCountFutureValue();
            buyers = buyers.AddOrder(request);

            model = new SearchableGridViewModel<PartnerViewModel>(buyers.ToFuture().ToList(), request, count.Value);

            return model;
        }
    }
}
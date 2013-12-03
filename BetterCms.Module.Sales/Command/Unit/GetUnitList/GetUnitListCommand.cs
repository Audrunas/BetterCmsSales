using System.Linq;

using BetterCms.Core.DataAccess.DataContext;
using BetterCms.Core.Mvc.Commands;

using BetterCms.Module.Root.Mvc;
using BetterCms.Module.Root.Mvc.Grids.Extensions;
using BetterCms.Module.Root.Mvc.Grids.GridOptions;
using BetterCms.Module.Root.ViewModels.SiteSettings;

using BetterCms.Module.Sales.ViewModels;

namespace BetterCms.Module.Sales.Command.Unit.GetUnitList
{
    public class GetUnitListCommand : CommandBase, ICommand<SearchableGridOptions, SearchableGridViewModel<UnitViewModel>>
    {
        /// <summary>
        /// Executes the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>List with unit view models</returns>
        public SearchableGridViewModel<UnitViewModel> Execute(SearchableGridOptions request)
        {
            SearchableGridViewModel<UnitViewModel> model;

            request.SetDefaultSortingOptions("Title");

            var query = Repository
                .AsQueryable<Models.Unit>();

            if (!string.IsNullOrWhiteSpace(request.SearchQuery))
            {
                query = query.Where(a => a.Title.Contains(request.SearchQuery) || a.ShortTitle.Contains(request.SearchQuery));
            }

            var units = query
                .Select(unit =>
                    new UnitViewModel
                    {
                        Id = unit.Id,
                        Version = unit.Version,
                        Title = unit.Title,
                        ShortTitle = unit.ShortTitle
                    });

            var count = query.ToRowCountFutureValue();
            units = units.AddSortingAndPaging(request);

            model = new SearchableGridViewModel<UnitViewModel>(units.ToList(), request, count.Value);

            return model;
        }
    }
}
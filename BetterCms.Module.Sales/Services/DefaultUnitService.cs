using System.Linq;
using BetterCms.Core.DataAccess;
using BetterCms.Module.Sales.ViewModels;

namespace BetterCms.Module.Sales.Services
{
    public class DefaultUnitService : IUnitService
    {
        private readonly IRepository repository;

        public DefaultUnitService(IRepository repository)
        {
            this.repository = repository;
        }

        public System.Collections.Generic.List<ViewModels.UnitViewModel> GetAllUnits()
        {
            return repository
                .AsQueryable<Models.Unit>()
                .OrderBy(u => u.Title)
                .Select(u => new UnitViewModel
                                 {
                                     Title = u.Title,
                                     ShortTitle = u.ShortTitle,
                                     Id = u.Id,
                                     Version = u.Version
                                 })
                .ToList();
        }
    }
}
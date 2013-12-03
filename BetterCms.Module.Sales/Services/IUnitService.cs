using System.Collections.Generic;
using BetterCms.Module.Sales.ViewModels;

namespace BetterCms.Module.Sales.Services
{
    public interface IUnitService
    {
        List<UnitViewModel> GetAllUnits();
    }
}
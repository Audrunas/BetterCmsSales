using BetterCms.Core.Mvc.Commands;
using BetterCms.Module.Root.Mvc;
using BetterCms.Module.Sales.ViewModels;

namespace BetterCms.Module.Sales.Command.Unit.DeleteUnit
{
    public class DeleteUnitCommand : CommandBase, ICommand<UnitViewModel, bool>
    {
        /// <summary>
        /// Executes the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns><c>True</c>, if unit was deleted successfully.</returns>
        public bool Execute(UnitViewModel request)
        {
            Repository.Delete<Models.Unit>(request.Id, request.Version);
            UnitOfWork.Commit();

            return true;
        }
    }
}
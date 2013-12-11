using BetterCms.Core.Mvc.Commands;
using BetterCms.Module.Root.Mvc;
using BetterCms.Module.Sales.ViewModels;

namespace BetterCms.Module.Sales.Command.Supplier.DeleteSupplier
{
    public class DeleteSupplierCommand : CommandBase, ICommand<PartnerViewModel, bool>
    {
        /// <summary>
        /// Executes the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns><c>True</c>, if supplier was deleted successfully.</returns>
        public bool Execute(PartnerViewModel request)
        {
            Repository.Delete<Models.Supplier>(request.Id, request.Version);
            UnitOfWork.Commit();

            return true;
        }
    }
}
using BetterCms.Core.Mvc.Commands;
using BetterCms.Module.Root.Mvc;
using BetterCms.Module.Sales.ViewModels;

namespace BetterCms.Module.Sales.Command.Purchase.DeletePurchase
{
    public class DeletePurchaseCommand : CommandBase, ICommand<PurchaseViewModel, bool>
    {
        /// <summary>
        /// Executes the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns><c>True</c>, if purchase was deleted successfully.</returns>
        public bool Execute(PurchaseViewModel request)
        {
            Repository.Delete<Models.Purchase>(request.Id, request.Version);
            UnitOfWork.Commit();

            return true;
        }
    }
}
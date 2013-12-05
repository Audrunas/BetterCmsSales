using BetterCms.Core.Mvc.Commands;
using BetterCms.Module.Root.Mvc;
using BetterCms.Module.Sales.ViewModels;

namespace BetterCms.Module.Sales.Command.Buyer.DeleteBuyer
{
    public class DeleteBuyerCommand : CommandBase, ICommand<BuyerViewModel, bool>
    {
        /// <summary>
        /// Executes the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns><c>True</c>, if buyer was deleted successfully.</returns>
        public bool Execute(BuyerViewModel request)
        {
            Repository.Delete<Models.Buyer>(request.Id, request.Version);
            UnitOfWork.Commit();

            return true;
        }
    }
}
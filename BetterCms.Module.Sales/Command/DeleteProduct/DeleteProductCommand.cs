using BetterCms.Core.Mvc.Commands;

using BetterCms.Module.Sales.Models;
using BetterCms.Module.Root.Mvc;
using BetterCms.Module.Sales.ViewModels;

namespace BetterCms.Module.Sales.Command.DeleteProduct
{
    public class DeleteProductCommand : CommandBase, ICommand<ProductViewModel, bool>
    {
        /// <summary>
        /// Executes the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns><c>True</c>, if product was deleted successfully.</returns>
        public bool Execute(ProductViewModel request)
        {
            Repository.Delete<Product>(request.Id, request.Version);
            UnitOfWork.Commit();

            return true;
        }
    }
}
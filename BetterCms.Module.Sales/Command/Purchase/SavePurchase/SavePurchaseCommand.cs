using BetterCms.Core.DataAccess.DataContext;
using BetterCms.Core.Mvc.Commands;

using BetterCms.Module.Root.Mvc;

using BetterCms.Module.Sales.Models;
using BetterCms.Module.Sales.ViewModels;

namespace BetterCms.Module.Sales.Command.Purchase.SavePurchase
{
    public class SavePurchaseCommand : CommandBase, ICommand<PurchaseViewModel, PurchaseViewModel>
    {
        /// <summary>
        /// Executes the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public PurchaseViewModel Execute(PurchaseViewModel request)
        {
            var isNew = request.Id.HasDefaultValue();
            Models.Purchase purchase;

            if (isNew)
            {
                purchase = new Models.Purchase
                               {
                                   Status = PurchaseStatus.New
                               };
            }
            else
            {
                purchase = Repository.AsQueryable<Models.Purchase>(w => w.Id == request.Id).FirstOne();
            }

            purchase.Version = request.Version;
            purchase.Supplier = Repository.AsProxy<Models.Supplier>(request.SupplierId.Value);

            Repository.Save(purchase);
            UnitOfWork.Commit();

            return new PurchaseViewModel
                {
                    Id = purchase.Id,
                    Version = purchase.Version,
                    CreatedOn = purchase.CreatedOn,
                    SupplierName = purchase.Supplier.Name,
                    Status = purchase.Status
                };
        }
    }
}
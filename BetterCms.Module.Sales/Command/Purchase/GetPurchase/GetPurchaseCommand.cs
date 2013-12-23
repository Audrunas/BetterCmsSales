using System;
using System.Linq;
using BetterCms.Core.DataAccess.DataContext;
using BetterCms.Core.Mvc.Commands;

using BetterCms.Module.Root.Mvc;
using BetterCms.Module.Sales.ViewModels;

namespace BetterCms.Module.Sales.Command.Purchase.GetPurchase
{
    public class GetPurchaseCommand : CommandBase, ICommand<Guid, PurchaseViewModel>
    {
        public PurchaseViewModel Execute(Guid request)
        {
            PurchaseViewModel model;
            
            if (!request.HasDefaultValue())
            {
                model = Repository
                    .AsQueryable<Models.Purchase>()
                    .Select(p => new PurchaseViewModel
                                     {
                                         Id = p.Id,
                                         Version = p.Version,
                                         SupplierId = p.Supplier.Id,
                                         SupplierName = p.Supplier.Name,
                                         Status = p.Status,
                                         CreatedOn = p.CreatedOn
                                     })
                    .FirstOne();
            }
            else
            {
                model = new PurchaseViewModel();
            }

            return model;
        }
    }
}
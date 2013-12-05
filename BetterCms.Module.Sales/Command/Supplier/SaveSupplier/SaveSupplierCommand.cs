using System.Linq;
using BetterCms.Core.DataAccess.DataContext;
using BetterCms.Core.Exceptions.Mvc;
using BetterCms.Core.Mvc.Commands;

using BetterCms.Module.Root.Mvc;
using BetterCms.Module.Sales.Content.Resources;
using BetterCms.Module.Sales.ViewModels;

namespace BetterCms.Module.Sales.Command.Supplier.SaveSupplier
{
    public class SaveSupplierCommand : CommandBase, ICommand<SupplierViewModel, SupplierViewModel>
    {
        /// <summary>
        /// Executes the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public SupplierViewModel Execute(SupplierViewModel request)
        {
            var isNew = request.Id.HasDefaultValue();
            Models.Supplier supplier;

            ValidateRequest(isNew, request);

            if (isNew)
            {
                supplier = new Models.Supplier();
            }
            else
            {
                supplier = Repository.AsQueryable<Models.Supplier>(w => w.Id == request.Id).FirstOne();
            }

            supplier.Name = request.Name;
            supplier.Email = request.Email;
            supplier.Address = request.Address;
            supplier.PhoneNumber = request.PhoneNumber;
            supplier.Version = request.Version;

            Repository.Save(supplier);
            UnitOfWork.Commit();

            return new SupplierViewModel
                {
                    Id = supplier.Id,
                    Version = supplier.Version,
                    Name = supplier.Name,
                    Email = supplier.Email
                };
        }

        private void ValidateRequest(bool isNew, SupplierViewModel request)
        {
            var query = Repository.AsQueryable<Models.Supplier>();
            if (!isNew)
            {
                query = query.Where(w => w.Id == request.Id);
            }

            if (query.Any(u => u.Name == request.Name))
            {
                var message = SalesGlobalization.SaveSupplier_NotUniqueName_Message;
                var logMessage = string.Format("Supplier with such a name already exists. Id: {0}, Title: {1}.", request.Id, request.Name);
                throw new ValidationException(() => message, logMessage);
            }
        }
    }
}
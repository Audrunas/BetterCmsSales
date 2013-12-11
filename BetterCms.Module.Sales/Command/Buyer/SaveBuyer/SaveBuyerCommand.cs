using System.Linq;
using BetterCms.Core.DataAccess.DataContext;
using BetterCms.Core.Exceptions.Mvc;
using BetterCms.Core.Mvc.Commands;

using BetterCms.Module.Root.Mvc;
using BetterCms.Module.Sales.Content.Resources;
using BetterCms.Module.Sales.ViewModels;

namespace BetterCms.Module.Sales.Command.Buyer.SaveBuyer
{
    public class SaveBuyerCommand : CommandBase, ICommand<PartnerViewModel, PartnerViewModel>
    {
        /// <summary>
        /// Executes the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public PartnerViewModel Execute(PartnerViewModel request)
        {
            var isNew = request.Id.HasDefaultValue();
            Models.Buyer buyer;

            ValidateRequest(isNew, request);

            if (isNew)
            {
                buyer = new Models.Buyer();
            }
            else
            {
                buyer = Repository.AsQueryable<Models.Buyer>(w => w.Id == request.Id).FirstOne();
            }

            buyer.Name = request.Name;
            buyer.Email = request.Email;
            buyer.Address = request.Address;
            buyer.PhoneNumber = request.PhoneNumber;
            buyer.Version = request.Version;

            Repository.Save(buyer);
            UnitOfWork.Commit();

            return new PartnerViewModel
                {
                    Id = buyer.Id,
                    Version = buyer.Version,
                    Name = buyer.Name,
                    Email = buyer.Email
                };
        }

        private void ValidateRequest(bool isNew, PartnerViewModel request)
        {
            var query = Repository.AsQueryable<Models.Buyer>();
            if (!isNew)
            {
                query = query.Where(w => w.Id != request.Id);
            }

            if (query.Any(u => u.Name == request.Name))
            {
                var message = SalesGlobalization.SaveBuyer_NotUniqueName_Message;
                var logMessage = string.Format("Buyer with such a name already exists. Id: {0}, Title: {1}.", request.Id, request.Name);
                throw new ValidationException(() => message, logMessage);
            }
        }
    }
}
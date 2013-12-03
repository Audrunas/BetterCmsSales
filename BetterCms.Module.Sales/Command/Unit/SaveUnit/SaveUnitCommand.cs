using System.Linq;
using BetterCms.Core.DataAccess.DataContext;
using BetterCms.Core.Exceptions.Mvc;
using BetterCms.Core.Mvc.Commands;

using BetterCms.Module.Root.Mvc;
using BetterCms.Module.Sales.Content.Resources;
using BetterCms.Module.Sales.ViewModels;

namespace BetterCms.Module.Sales.Command.Unit.SaveUnit
{
    public class SaveUnitCommand : CommandBase, ICommand<UnitViewModel, UnitViewModel>
    {
        /// <summary>
        /// Executes the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public UnitViewModel Execute(UnitViewModel request)
        {
            var isNew = request.Id.HasDefaultValue();
            Models.Unit unit;

            ValidateRequest(isNew, request);

            if (isNew)
            {
                unit = new Models.Unit();
            }
            else
            {
                unit = Repository.AsQueryable<Models.Unit>(w => w.Id == request.Id).FirstOne();
            }

            unit.Title = request.Title;
            unit.ShortTitle = request.ShortTitle;
            unit.Version = request.Version;

            Repository.Save(unit);
            UnitOfWork.Commit();

            return new UnitViewModel
                {
                    Id = unit.Id,
                    Version = unit.Version,
                    Title = unit.Title,
                    ShortTitle = unit.ShortTitle
                };
        }

        private void ValidateRequest(bool isNew, UnitViewModel request)
        {
            var query = Repository.AsQueryable<Models.Unit>();
            if (!isNew)
            {
                query = query.Where(w => w.Id == request.Id);
            }

            if (query.Any(u => u.Title == request.Title))
            {
                var message = SalesGlobalization.SaveUnit_NotUniqueTitle_Message;
                var logMessage = string.Format("Unit with such a title already exists. Id: {0}, Title: {1}.", request.Id, request.Title);
                throw new ValidationException(() => message, logMessage);
            }

            if (query.Any(u => u.ShortTitle == request.ShortTitle))
            {
                var message = SalesGlobalization.SaveUnit_NotUniqueShortTitle_Message;
                var logMessage = string.Format("Unit with such a short title already exists. Id: {0}, ShortTitle: {1}.", request.Id, request.ShortTitle);
                throw new ValidationException(() => message, logMessage);
            }
        }
    }
}
using System.Web.Mvc;

using BetterCms.Core.Security;

using BetterCms.Module.Root;
using BetterCms.Module.Root.Mvc;
using BetterCms.Module.Root.Mvc.Grids.GridOptions;

using BetterCms.Module.Sales.Command.Buyer.DeleteBuyer;
using BetterCms.Module.Sales.Command.Buyer.GetBuyerList;
using BetterCms.Module.Sales.Command.Buyer.SaveBuyer;
using BetterCms.Module.Sales.Content.Resources;
using BetterCms.Module.Sales.ViewModels;

using Microsoft.Web.Mvc;

namespace BetterCms.Module.Sales.Controllers
{
    [ActionLinkArea(SalesModuleDescriptor.SalesAreaName)]
    public class BuyerController : CmsControllerBase
    {
        /// <summary>
        /// Lists the template for displaying buyers list.
        /// </summary>
        /// <returns>Json result.</returns>
        [BcmsAuthorize(RootModuleConstants.UserRoles.Administration)]
        public ActionResult ListTemplate()
        {
            var view = RenderView("List", null);
            var request = new SearchableGridOptions();
            request.SetDefaultPaging();

            var buyers = GetCommand<GetBuyerListCommand>().ExecuteCommand(request);

            return ComboWireJson(buyers != null, view, buyers, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Lists the buyers.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Json result.</returns>
        [BcmsAuthorize(RootModuleConstants.UserRoles.Administration)]
        public ActionResult BuyersList(SearchableGridOptions request)
        {
            request.SetDefaultPaging();
            var model = GetCommand<GetBuyerListCommand>().ExecuteCommand(request);
            return WireJson(model != null, model);
        }

        /// <summary>
        /// Saves the buyer.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Json result.</returns>
        [HttpPost]
        [BcmsAuthorize(RootModuleConstants.UserRoles.Administration)]
        public ActionResult SaveBuyer(PartnerViewModel model)
        {
            var success = false;
            PartnerViewModel response = null;
            if (ModelState.IsValid)
            {
                response = GetCommand<SaveBuyerCommand>().ExecuteCommand(model);
                if (response != null)
                {
                    if (model.Id.HasDefaultValue())
                    {
                        Messages.AddSuccess(SalesGlobalization.CreateBuyer_CreatedSuccessfully_Message);
                    }

                    success = true;
                }
            }

            return WireJson(success, response);
        }

        /// <summary>
        /// Deletes the buyer.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="version">The version.</param>
        /// <returns>Json result.</returns>
        [HttpPost]
        [BcmsAuthorize(RootModuleConstants.UserRoles.Administration)]
        public ActionResult DeleteBuyer(string id, string version)
        {
            var request = new PartnerViewModel { Id = id.ToGuidOrDefault(), Version = version.ToIntOrDefault() };
            var success = GetCommand<DeleteBuyerCommand>().ExecuteCommand(request);
            if (success)
            {
                if (!request.Id.HasDefaultValue())
                {
                    Messages.AddSuccess(SalesGlobalization.DeleteBuyer_DeletedSuccessfully_Message);
                }
            }

            return WireJson(success);
        }
    }
}
using System.Web.Mvc;

using BetterCms.Core.Security;

using BetterCms.Module.Root;
using BetterCms.Module.Root.Mvc;
using BetterCms.Module.Root.Mvc.Grids.GridOptions;
using BetterCms.Module.Sales.Command.Purchase.DeletePurchase;
using BetterCms.Module.Sales.Command.Purchase.GetPurchase;
using BetterCms.Module.Sales.Command.Purchase.GetPurchaseList;
using BetterCms.Module.Sales.Command.Purchase.SavePurchase;
using BetterCms.Module.Sales.Content.Resources;
using BetterCms.Module.Sales.ViewModels;
using Microsoft.Web.Mvc;

namespace BetterCms.Module.Sales.Controllers
{
    [ActionLinkArea(SalesModuleDescriptor.SalesAreaName)]
    public class PurchaseController : CmsControllerBase
    {
        /// <summary>
        /// Lists the template for displaying purchases list.
        /// </summary>
        /// <returns>Json result.</returns>
        [BcmsAuthorize(RootModuleConstants.UserRoles.Administration)]
        public ActionResult ListTemplate()
        {
            var view = RenderView("List", null);
            var request = new SearchableGridOptions();
            request.SetDefaultPaging();

            var purchases = GetCommand<GetPurchaseListCommand>().ExecuteCommand(request);

            return ComboWireJson(purchases != null, view, purchases, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Deletes the purchase.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="version">The version.</param>
        /// <returns>Json result.</returns>
        [HttpPost]
        [BcmsAuthorize(RootModuleConstants.UserRoles.Administration)]
        public ActionResult DeletePurchase(string id, string version)
        {
            var request = new PurchaseViewModel { Id = id.ToGuidOrDefault(), Version = version.ToIntOrDefault() };
            var success = GetCommand<DeletePurchaseCommand>().ExecuteCommand(request);
            if (success)
            {
                if (!request.Id.HasDefaultValue())
                {
                    Messages.AddSuccess(SalesGlobalization.DeletePurchase_DeletedSuccessfully_Message);
                }
            }

            return WireJson(success);
        }

        /// <summary>
        /// Saves the purchase.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Json result.</returns>
        [HttpPost]
        [BcmsAuthorize(RootModuleConstants.UserRoles.Administration)]
        public ActionResult SavePurchase(PurchaseViewModel model)
        {
            var success = false;
            PurchaseViewModel response = null;
            if (ModelState.IsValid)
            {
                response = GetCommand<SavePurchaseCommand>().ExecuteCommand(model);
                if (response != null)
                {
                    if (model.Id.HasDefaultValue())
                    {
                        Messages.AddSuccess(SalesGlobalization.CreatePurchase_CreatedSuccessfully_Message);
                    }

                    success = true;
                }
            }

            return WireJson(success, response);
        }

        /// <summary>
        /// Edits the purchase.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>Json result with view and data</returns>
        [HttpGet]
        [BcmsAuthorize(RootModuleConstants.UserRoles.Administration)]
        public ActionResult EditPurchase(string id)
        {
            var model = GetCommand<GetPurchaseCommand>().ExecuteCommand(id.ToGuidOrDefault());
            var view = RenderView("EditPurchase", null);

            return ComboWireJson(model != null, view, model, JsonRequestBehavior.AllowGet);
        }
        
        /// <summary>
        /// Creates the purchase.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>Json result with view and data</returns>
        [HttpGet]
        [BcmsAuthorize(RootModuleConstants.UserRoles.Administration)]
        public ActionResult CreatePurchase()
        {
            var model = GetCommand<GetPurchaseCommand>().ExecuteCommand(System.Guid.Empty);
            var view = RenderView("EditPurchase", null);

            return ComboWireJson(model != null, view, model, JsonRequestBehavior.AllowGet);
        }
    }
}
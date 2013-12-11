using System.Web.Mvc;

using BetterCms.Core.Security;

using BetterCms.Module.Root;
using BetterCms.Module.Root.Mvc;
using BetterCms.Module.Root.Mvc.Grids.GridOptions;

using BetterCms.Module.Sales.Command.Supplier.DeleteSupplier;
using BetterCms.Module.Sales.Command.Supplier.GetSupplierList;
using BetterCms.Module.Sales.Command.Supplier.SaveSupplier;
using BetterCms.Module.Sales.Content.Resources;
using BetterCms.Module.Sales.ViewModels;

using Microsoft.Web.Mvc;

namespace BetterCms.Module.Sales.Controllers
{
    [ActionLinkArea(SalesModuleDescriptor.SalesAreaName)]
    public class SupplierController : CmsControllerBase
    {
        /// <summary>
        /// Lists the template for displaying suppliers list.
        /// </summary>
        /// <returns>Json result.</returns>
        [BcmsAuthorize(RootModuleConstants.UserRoles.Administration)]
        public ActionResult ListTemplate()
        {
            var view = RenderView("List", null);
            var request = new SearchableGridOptions();
            request.SetDefaultPaging();

            var suppliers = GetCommand<GetSupplierListCommand>().ExecuteCommand(request);

            return ComboWireJson(suppliers != null, view, suppliers, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Lists the suppliers.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Json result.</returns>
        [BcmsAuthorize(RootModuleConstants.UserRoles.Administration)]
        public ActionResult SuppliersList(SearchableGridOptions request)
        {
            request.SetDefaultPaging();
            var model = GetCommand<GetSupplierListCommand>().ExecuteCommand(request);
            return WireJson(model != null, model);
        }

        /// <summary>
        /// Saves the supplier.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Json result.</returns>
        [HttpPost]
        [BcmsAuthorize(RootModuleConstants.UserRoles.Administration)]
        public ActionResult SaveSupplier(PartnerViewModel model)
        {
            var success = false;
            PartnerViewModel response = null;
            if (ModelState.IsValid)
            {
                response = GetCommand<SaveSupplierCommand>().ExecuteCommand(model);
                if (response != null)
                {
                    if (model.Id.HasDefaultValue())
                    {
                        Messages.AddSuccess(SalesGlobalization.CreateSupplier_CreatedSuccessfully_Message);
                    }

                    success = true;
                }
            }

            return WireJson(success, response);
        }

        /// <summary>
        /// Deletes the supplier.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="version">The version.</param>
        /// <returns>Json result.</returns>
        [HttpPost]
        [BcmsAuthorize(RootModuleConstants.UserRoles.Administration)]
        public ActionResult DeleteSupplier(string id, string version)
        {
            var request = new PartnerViewModel { Id = id.ToGuidOrDefault(), Version = version.ToIntOrDefault() };
            var success = GetCommand<DeleteSupplierCommand>().ExecuteCommand(request);
            if (success)
            {
                if (!request.Id.HasDefaultValue())
                {
                    Messages.AddSuccess(SalesGlobalization.DeleteSupplier_DeletedSuccessfully_Message);
                }
            }

            return WireJson(success);
        }
    }
}

using System.Web.Mvc;

using BetterCms.Core.Security;

using BetterCms.Module.Root;
using BetterCms.Module.Root.Mvc;
using BetterCms.Module.Root.Mvc.Grids.GridOptions;

using BetterCms.Module.Sales.Command.Unit.DeleteUnit;
using BetterCms.Module.Sales.Command.Unit.GetUnitList;
using BetterCms.Module.Sales.Command.Unit.SaveUnit;
using BetterCms.Module.Sales.Content.Resources;
using BetterCms.Module.Sales.ViewModels;

using Microsoft.Web.Mvc;

namespace BetterCms.Module.Sales.Controllers
{
    [ActionLinkArea(SalesModuleDescriptor.SalesAreaName)]
    public class UnitController : CmsControllerBase
    {
        /// <summary>
        /// Lists the template for displaying units list.
        /// </summary>
        /// <returns>Json result.</returns>
        [BcmsAuthorize(RootModuleConstants.UserRoles.Administration)]
        public ActionResult ListTemplate()
        {
            var view = RenderView("List", null);
            var request = new SearchableGridOptions();
            request.SetDefaultPaging();

            var units = GetCommand<GetUnitListCommand>().ExecuteCommand(request);

            return ComboWireJson(units != null, view, units, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Lists the units.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Json result.</returns>
        [BcmsAuthorize(RootModuleConstants.UserRoles.Administration)]
        public ActionResult UnitsList(SearchableGridOptions request)
        {
            request.SetDefaultPaging();
            var model = GetCommand<GetUnitListCommand>().ExecuteCommand(request);
            return WireJson(model != null, model);
        }

        /// <summary>
        /// Saves the unit.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Json result.</returns>
        [HttpPost]
        [BcmsAuthorize(RootModuleConstants.UserRoles.Administration)]
        public ActionResult SaveUnit(UnitViewModel model)
        {
            var success = false;
            UnitViewModel response = null;
            if (ModelState.IsValid)
            {
                response = GetCommand<SaveUnitCommand>().ExecuteCommand(model);
                if (response != null)
                {
                    if (model.Id.HasDefaultValue())
                    {
                        Messages.AddSuccess(SalesGlobalization.CreateUnit_CreatedSuccessfully_Message);
                    }

                    success = true;
                }
            }

            return WireJson(success, response);
        }
        
        /// <summary>
        /// Deletes the unit.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="version">The version.</param>
        /// <returns>Json result.</returns>
        [HttpPost]
        [BcmsAuthorize(RootModuleConstants.UserRoles.Administration)]
        public ActionResult DeleteUnit(string id, string version)
        {
            var request = new UnitViewModel { Id = id.ToGuidOrDefault(), Version = version.ToIntOrDefault() };
            var success = GetCommand<DeleteUnitCommand>().ExecuteCommand(request);
            if (success)
            {
                if (!request.Id.HasDefaultValue())
                {
                    Messages.AddSuccess(SalesGlobalization.DeleteUnit_DeletedSuccessfully_Message);
                }
            }

            return WireJson(success);
        }
    }
}
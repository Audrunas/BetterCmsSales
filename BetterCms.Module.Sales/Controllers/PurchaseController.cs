using System.Web.Mvc;

using BetterCms.Core.Security;

using BetterCms.Module.Root;
using BetterCms.Module.Root.Mvc;
using BetterCms.Module.Root.Mvc.Grids.GridOptions;

using BetterCms.Module.Sales.Command.Purchase.GetPurchaseList;

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
    }
}
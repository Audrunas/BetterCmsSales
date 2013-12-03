using System.Web.Mvc;

using BetterCms.Core.Security;

using BetterCms.Module.Root;
using BetterCms.Module.Root.Mvc;
using BetterCms.Module.Root.Mvc.Grids.GridOptions;

using BetterCms.Module.Sales.Command.DeleteProduct;
using BetterCms.Module.Sales.Command.GetProductList;
using BetterCms.Module.Sales.Command.SaveProduct;
using BetterCms.Module.Sales.Content.Resources;
using BetterCms.Module.Sales.ViewModels;

using Microsoft.Web.Mvc;

namespace BetterCms.Module.Sales.Controllers
{
    [ActionLinkArea(SalesModuleDescriptor.SalesAreaName)]
    public class ProductController : CmsControllerBase
    {
        /// <summary>
        /// Lists the template for displaying products list.
        /// </summary>
        /// <returns>Json result.</returns>
        [BcmsAuthorize(RootModuleConstants.UserRoles.Administration)]
        public ActionResult ListTemplate()
        {
            var view = RenderView("List", null);
            var request = new SearchableGridOptions();
            request.SetDefaultPaging();

            var products = GetCommand<GetProductListCommand>().ExecuteCommand(request);

            return ComboWireJson(products != null, view, products, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Lists the products.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Json result.</returns>
        [BcmsAuthorize(RootModuleConstants.UserRoles.Administration)]
        public ActionResult ProductsList(SearchableGridOptions request)
        {
            request.SetDefaultPaging();
            var model = GetCommand<GetProductListCommand>().ExecuteCommand(request);
            return WireJson(model != null, model);
        }

        /// <summary>
        /// Saves the product.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Json result.</returns>
        [HttpPost]
        [BcmsAuthorize(RootModuleConstants.UserRoles.Administration)]
        public ActionResult SaveProduct(ProductViewModel model)
        {
            var success = false;
            ProductViewModel response = null;
            if (ModelState.IsValid)
            {
                response = GetCommand<SaveProductCommand>().ExecuteCommand(model);
                if (response != null)
                {
                    if (model.Id.HasDefaultValue())
                    {
                        Messages.AddSuccess(SalesGlobalization.CreateProduct_CreatedSuccessfully_Message);
                    }

                    success = true;
                }
            }

            return WireJson(success, response);
        }
        
        /// <summary>
        /// Deletes the product.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="version">The version.</param>
        /// <returns>Json result.</returns>
        [HttpPost]
        [BcmsAuthorize(RootModuleConstants.UserRoles.Administration)]
        public ActionResult DeleteProduct(string id, string version)
        {
            var request = new ProductViewModel { Id = id.ToGuidOrDefault(), Version = version.ToIntOrDefault() };
            var success = GetCommand<DeleteProductCommand>().ExecuteCommand(request);
            if (success)
            {
                if (!request.Id.HasDefaultValue())
                {
                    Messages.AddSuccess(SalesGlobalization.DeleteProduct_DeletedSuccessfully_Message);
                }
            }

            return WireJson(success);
        }
    }
}
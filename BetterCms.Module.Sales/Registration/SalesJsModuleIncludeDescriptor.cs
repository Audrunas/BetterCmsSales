﻿using BetterCms.Core.Modules;
using BetterCms.Core.Modules.Projections;
using BetterCms.Module.Sales.Content.Resources;
using BetterCms.Module.Sales.Controllers;

namespace BetterCms.Module.Sales.Registration
{
    /// <summary>
    /// bcms.sales.js module descriptor.
    /// </summary>
    public class SalesJsModuleIncludeDescriptor : JsIncludeDescriptor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SalesJsModuleIncludeDescriptor" /> class.
        /// </summary>
        /// <param name="module">The container module.</param>
        public SalesJsModuleIncludeDescriptor(ModuleDescriptor module)
            : base(module, "bcms.sales")
        {
            Links = new IActionProjection[]
                {
                    new JavaScriptModuleLinkTo<ProductController>(this, "loadSiteSettingsProductsUrl", c => c.ListTemplate()),
                    new JavaScriptModuleLinkTo<ProductController>(this, "loadProductsUrl", c => c.ProductsList(null)),
                    new JavaScriptModuleLinkTo<ProductController>(this, "saveProductUrl", c => c.SaveProduct(null)),
                    new JavaScriptModuleLinkTo<ProductController>(this, "deleteProductUrl", c => c.DeleteProduct(null, null)),

                    new JavaScriptModuleLinkTo<UnitController>(this, "loadSiteSettingsUnitsUrl", c => c.ListTemplate()),
                    new JavaScriptModuleLinkTo<UnitController>(this, "loadUnitsUrl", c => c.UnitsList(null)),
                    new JavaScriptModuleLinkTo<UnitController>(this, "saveUnitUrl", c => c.SaveUnit(null)),
                    new JavaScriptModuleLinkTo<UnitController>(this, "deleteUnitUrl", c => c.DeleteUnit(null, null))
                };

            Globalization = new IActionProjection[]
                {
                    new JavaScriptModuleGlobalization(this, "deleteProductDialogTitle", () => SalesGlobalization.DeleteProduct_Confirmation_Message), 
                    new JavaScriptModuleGlobalization(this, "deleteUnitDialogTitle", () => SalesGlobalization.DeleteUnit_Confirmation_Message), 
                };
        }
    }
}
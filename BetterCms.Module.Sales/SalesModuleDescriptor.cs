using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using Autofac;

using BetterCms.Core.Modules;
using BetterCms.Core.Modules.Projections;
using BetterCms.Module.Sales.Content.Resources;
using BetterCms.Module.Sales.Registration;
using BetterCms.Module.Root;

namespace BetterCms.Module.Sales
{
    /// <summary>
    /// Pages module descriptor.
    /// </summary>
    public class SalesModuleDescriptor : ModuleDescriptor
    {
        /// <summary>
        /// The module name.
        /// </summary>
        internal const string ModuleName = "sales";

        /// <summary>
        /// The sales area name.
        /// </summary>
        internal const string SalesAreaName = "bcms-sales";

        /// <summary>
        /// The sales java script module descriptor.
        /// </summary>
        private readonly SalesJsModuleIncludeDescriptor salesJsModuleIncludeDescriptor;

        /// <summary>
        /// Initializes a new instance of the <see cref="SalesModuleDescriptor" /> class.
        /// </summary>
        public SalesModuleDescriptor(ICmsConfiguration cmsConfiguration)
            : base(cmsConfiguration)
        {
            salesJsModuleIncludeDescriptor = new SalesJsModuleIncludeDescriptor(this);
        }

        /// <summary>
        /// Gets the name of module.
        /// </summary>
        /// <value>
        /// The name of pages module.
        /// </value>
        public override string Name
        {
            get
            {
                return ModuleName;
            }
        }

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>
        /// The module description.
        /// </value>
        public override string Description
        {
            get
            {
                return "A sales module for Better CMS.";
            }
        }

        /// <summary>
        /// Gets the name of the module area.
        /// </summary>
        /// <value>
        /// The name of the module area.
        /// </value>
        public override string AreaName
        {
            get
            {
                return SalesAreaName;
            }
        }

        /// <summary>
        /// Registers module types.
        /// </summary>
        /// <param name="context">The area registration context.</param>
        /// <param name="containerBuilder">The container builder.</param>        
        public override void RegisterModuleTypes(ModuleRegistrationContext context, ContainerBuilder containerBuilder)
        {
            // containerBuilder.RegisterType<DefaultSubscriberService>().AsImplementedInterfaces().InstancePerLifetimeScope();
        }

        /// <summary>
        /// Gets known client side modules in page module.
        /// </summary>        
        /// <returns>List of known client side modules in page module.</returns>
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1305:FieldNamesMustNotUseHungarianNotation", Justification = "Reviewed. Suppression is OK here.")]
        public override IEnumerable<JsIncludeDescriptor> RegisterJsIncludes()
        {
            return new[] { salesJsModuleIncludeDescriptor };
        }

        /// <summary>
        /// Registers the site settings projections.
        /// </summary>
        /// <param name="containerBuilder">The container builder.</param>
        /// <returns>List of page action projections.</returns>
        public override IEnumerable<IPageActionProjection> RegisterSiteSettingsProjections(ContainerBuilder containerBuilder)
        {
            return new IPageActionProjection[]
                {
                    new SeparatorProjection(8800),

                    new LinkActionProjection(salesJsModuleIncludeDescriptor, page => "loadSiteSettingsSalesProducts")
                        {
                            Order = 8801,
                            Title = page => SalesGlobalization.SiteSettings_SalesProductsMenuItem,
                            CssClass = page => "bcms-sidebar-link",
                            AccessRole = RootModuleConstants.UserRoles.MultipleRoles(RootModuleConstants.UserRoles.Administration)
                        },

                    new LinkActionProjection(salesJsModuleIncludeDescriptor, page => "loadSiteSettingsSalesPartners")
                        {
                            Order = 8802,
                            Title = page => SalesGlobalization.SiteSettings_SalesPartnersMenuItem,
                            CssClass = page => "bcms-sidebar-link",
                            AccessRole = RootModuleConstants.UserRoles.MultipleRoles(RootModuleConstants.UserRoles.Administration)
                        }
                };
        }
    }
}

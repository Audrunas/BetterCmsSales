using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using BetterCms.Module.Root.Content.Resources;
using BetterCms.Module.Root.Mvc.Grids;
using BetterCms.Module.Sales.Models;

namespace BetterCms.Module.Sales.ViewModels
{
    public class PurchaseViewModel : IEditableGridItem
    {
        /// <summary>
        /// Gets or sets the purchase id.
        /// </summary>
        /// <value>
        /// The purchase id.
        /// </value>
        [Required(ErrorMessageResourceType = typeof(RootGlobalization), ErrorMessageResourceName = "Validation_RequiredAttribute_Message")]
        public virtual Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the entity version.
        /// </summary>
        /// <value>
        /// The entity version.
        /// </value>
        [Required(ErrorMessageResourceType = typeof(RootGlobalization), ErrorMessageResourceName = "Validation_RequiredAttribute_Message")]
        public virtual int Version { get; set; }

        /// <summary>
        /// Gets or sets the purchase supplier id.
        /// </summary>
        /// <value>
        /// The purchase supplier id.
        /// </value>
        [Required(ErrorMessageResourceType = typeof(RootGlobalization), ErrorMessageResourceName = "Validation_RequiredAttribute_Message")]
        public virtual Guid? SupplierId { get; set; }

        /// <summary>
        /// Gets or sets the name of the purchase supplier.
        /// </summary>
        /// <value>
        /// The name of the purchase supplier.
        /// </value>
        public virtual string SupplierName { get; set; }

        /// <summary>
        /// Gets or sets the purchase status.
        /// </summary>
        /// <value>
        /// The purchase status.
        /// </value>
        public virtual PurchaseStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the purchased products.
        /// </summary>
        /// <value>
        /// The purchased products.
        /// </value>
        public virtual List<ProductsListViewModel> Products { get; set; }

        /// <summary>
        /// Gets or sets the date purchase created on.
        /// </summary>
        /// <value>
        /// The date purchase created on.
        /// </value>
        public virtual DateTime CreatedOn { get; set; }
    }
}
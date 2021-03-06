﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using BetterCms.Core.Models;

using BetterCms.Module.Root.Content.Resources;
using BetterCms.Module.Root.Mvc.Grids;

namespace BetterCms.Module.Sales.ViewModels
{
    public class ProductViewModel : IEditableGridItem
    {
        /// <summary>
        /// Gets or sets the product id.
        /// </summary>
        /// <value>
        /// The product id.
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
        /// Gets or sets the product name.
        /// </summary>
        /// <value>
        /// The product name.
        /// </value>
        [Required(ErrorMessageResourceType = typeof(RootGlobalization), ErrorMessageResourceName = "Validation_RequiredAttribute_Message")]
        [StringLength(MaxLength.Name, ErrorMessageResourceType = typeof(RootGlobalization), ErrorMessageResourceName = "Validation_StringLengthAttribute_Message")]
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the unit.
        /// </summary>
        /// <value>
        /// The unit.
        /// </value>
        public virtual Guid Unit { get; set; }

        /// <summary>
        /// Gets or sets the name of the unit.
        /// </summary>
        /// <value>
        /// The name of the unit.
        /// </value>
        public virtual string UnitName { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("{0}, Id: {1}, Version: {2}, Name: {3}", base.ToString(), Id, Version, Name);
        }
    }
}
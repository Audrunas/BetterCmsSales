using System;
using System.ComponentModel.DataAnnotations;

using BetterCms.Core.Models;

using BetterCms.Module.Root.Content.Resources;
using BetterCms.Module.Root.Mvc.Grids;

namespace BetterCms.Module.Sales.ViewModels
{
    public class UnitViewModel : IEditableGridItem
    {
        /// <summary>
        /// Gets or sets the unit id.
        /// </summary>
        /// <value>
        /// The unit id.
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
        /// Gets or sets the unit title.
        /// </summary>
        /// <value>
        /// The unit title.
        /// </value>
        [Required(ErrorMessageResourceType = typeof(RootGlobalization), ErrorMessageResourceName = "Validation_RequiredAttribute_Message")]
        [StringLength(MaxLength.Name, ErrorMessageResourceType = typeof(RootGlobalization), ErrorMessageResourceName = "Validation_StringLengthAttribute_Message")]
        public virtual string Title { get; set; }
        
        /// <summary>
        /// Gets or sets the unit short title.
        /// </summary>
        /// <value>
        /// The unit short title.
        /// </value>
        [Required(ErrorMessageResourceType = typeof(RootGlobalization), ErrorMessageResourceName = "Validation_RequiredAttribute_Message")]
        [StringLength(MaxLength.Name, ErrorMessageResourceType = typeof(RootGlobalization), ErrorMessageResourceName = "Validation_StringLengthAttribute_Message")]
        public virtual string ShortTitle { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("{0}, Id: {1}, Version: {2}, Title: {3}, ShortTitle: {4}", base.ToString(), Id, Version, Title, ShortTitle);
        }
    }
}
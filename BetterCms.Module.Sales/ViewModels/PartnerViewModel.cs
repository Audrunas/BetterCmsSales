using System;
using System.ComponentModel.DataAnnotations;
using BetterCms.Core.Models;
using BetterCms.Module.Root;
using BetterCms.Module.Root.Content.Resources;
using BetterCms.Module.Root.Mvc.Grids;
using BetterCms.Module.Sales.Content.Resources;

namespace BetterCms.Module.Sales.ViewModels
{
    public class PartnerViewModel : IEditableGridItem
    {
        /// <summary>
        /// Gets or sets the partner id.
        /// </summary>
        /// <value>
        /// The partner id.
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
        /// Gets or sets the partner name.
        /// </summary>
        /// <value>
        /// The partner name.
        /// </value>
        [Required(ErrorMessageResourceType = typeof(RootGlobalization), ErrorMessageResourceName = "Validation_RequiredAttribute_Message")]
        [StringLength(MaxLength.Name, ErrorMessageResourceType = typeof(RootGlobalization), ErrorMessageResourceName = "Validation_StringLengthAttribute_Message")]
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the partner email.
        /// </summary>
        /// <value>
        /// The partner email.
        /// </value>
        [StringLength(MaxLength.Email, ErrorMessageResourceType = typeof(RootGlobalization), ErrorMessageResourceName = "Validation_StringLengthAttribute_Message")]
        [RegularExpression(RootModuleConstants.EmailRegularExpression, ErrorMessageResourceType = typeof(SalesGlobalization), ErrorMessageResourceName = "EditPartner_IvalidEmail_Message")]
        public virtual string Email { get; set; }

        /// <summary>
        /// Gets or sets the partner address.
        /// </summary>
        /// <value>
        /// The partner address.
        /// </value>
        [StringLength(MaxLength.Name, ErrorMessageResourceType = typeof(RootGlobalization), ErrorMessageResourceName = "Validation_StringLengthAttribute_Message")]
        public virtual string Address { get; set; }

        /// <summary>
        /// Gets or sets the partner phone number.
        /// </summary>
        /// <value>
        /// The partner phone number.
        /// </value>
        [StringLength(MaxLength.Name, ErrorMessageResourceType = typeof(RootGlobalization), ErrorMessageResourceName = "Validation_StringLengthAttribute_Message")]
        public virtual string PhoneNumber { get; set; }
    }
}
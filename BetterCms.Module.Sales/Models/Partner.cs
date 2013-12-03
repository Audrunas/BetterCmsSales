using System;
using BetterCms.Core.Models;

namespace BetterCms.Module.Sales.Models
{
    [Serializable]
    public class Partner : EquatableEntity<Partner>
    {
        public virtual string Name { get; set; }
        
        public virtual string Address { get; set; }
        
        public virtual string Email { get; set; }
        
        public virtual string PhoneNumber { get; set; }
    }
}
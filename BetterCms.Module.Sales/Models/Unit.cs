using System;
using BetterCms.Core.Models;

namespace BetterCms.Module.Sales.Models
{
    [Serializable]
    public class Unit : EquatableEntity<Unit>
    {
        public virtual string Title { get; set; }
        
        public virtual string ShortTitle { get; set; }
    }
}
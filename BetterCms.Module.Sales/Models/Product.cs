using System;
using BetterCms.Core.Models;
using BetterCms.Module.MediaManager.Models;
using BetterCms.Module.Root.Models;

namespace BetterCms.Module.Sales.Models
{
    [Serializable]
    public class Product : EquatableEntity<Product>
    {
        public virtual string Name { get; set; }
        
        public virtual decimal Price { get; set; }
        
        public virtual Category Category { get; set; }
        
        public virtual Unit Unit { get; set; }
        
        public virtual MediaImage Image { get; set; }
    }
}
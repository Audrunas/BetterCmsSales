using System;
using BetterCms.Core.Models;

namespace BetterCms.Module.Sales.Models
{
    [Serializable]
    public class PurchaseProduct : EquatableEntity<PurchaseProduct>
    {
        public virtual Purchase Purchase { get; set; }
        
        public virtual Product Product { get; set; }
        
        public virtual int Quantity { get; set; }
        
        public virtual decimal Price { get; set; }
    }
}
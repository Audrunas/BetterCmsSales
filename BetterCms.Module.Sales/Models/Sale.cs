using System;
using System.Collections.Generic;
using BetterCms.Core.Models;

namespace BetterCms.Module.Sales.Models
{
    [Serializable]
    public class Sale : EquatableEntity<Sale>
    {
        public virtual Buyer Buyer { get; set; }
        
        public virtual SaleStatus Status { get; set; }

        public virtual IList<SaleProduct> Products { get; set; }
    }
}
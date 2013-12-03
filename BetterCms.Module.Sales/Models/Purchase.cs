using System;
using System.Collections.Generic;
using BetterCms.Core.Models;

namespace BetterCms.Module.Sales.Models
{
    [Serializable]
    public class Purchase : EquatableEntity<Purchase>
    {
        public virtual Supplier Supplier { get; set; }

        public virtual PurchaseStatus Status { get; set; }

        public virtual IList<PurchaseProduct> Products { get; set; }
    }
}
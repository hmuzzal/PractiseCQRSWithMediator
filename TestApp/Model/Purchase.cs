using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TestApp.Model
{
    public class Purchase
    {
        public int  Id { get; set; }
        public string PurchaseCode { get; set; }
        public string SupplierName { get; set; }
        public double TotalAmount { get; set; }
        public DateTime PurchaseDate { get; set; }
        public virtual ICollection<PurchaseDetail> PurchaseDetails { get; set; }

        public Purchase()
        {
            PurchaseDetails = new Collection<PurchaseDetail>();
        }
    }
}

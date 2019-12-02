using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeroMerch.Models.Order
{
    
    
        public class DetailsVmOrderedKey
        {
            public int? Id { get; set; }
            public string Manufacturer { get; set; }
            public string Product { get; set; }
            public decimal Price { get; set; }
            public string Key { get; set; }
            public bool UsedKey { get; set; }
        }

        public class DetailsVm
        {
            public int OrderId { get; set; }

            public string BillingTitle { get; set; }
            public string BillingFirstName { get; set; }
            public string BillingLastName { get; set; }
            public string BillingStreet { get; set; }
            public string BillingCity { get; set; }
            public string BillingZipCode { get; set; }

            public DateTime OrderDate { get; set; }
            public bool IsPaid { get; set; }

            public decimal OrderTotal { get; set; }
            public decimal OrderNetTotal { get; set; }

            public List<DetailsVmOrderedKey> OrderedKeys { get; set; }

            public DetailsVm()
            {
                OrderedKeys = new List<DetailsVmOrderedKey>();
            }
        }
    
}
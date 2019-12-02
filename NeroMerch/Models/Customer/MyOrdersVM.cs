using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeroMerch.Models
{
    
    
        public class MyOrdersVmOrder
    {
            public int Id { get; set; }
            public DateTime OrderedAt { get; set; }
            public bool IsPaid { get; set; }
            public decimal Total { get; set; }
        }

        public class MyOrdersVm
    {
            public List<MyOrdersVmOrder> Orders { get; set; }

            public MyOrdersVm()
            {
                Orders = new List<MyOrdersVmOrder>();
            }
        }
    
}
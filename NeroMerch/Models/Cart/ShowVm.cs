using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NeroMerch.DATA;


namespace NeroMerch.Models
{
    public class ShowVmProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal LinePrice { get; set; }
        public string ImagePath { get; set; }
    }

    public class ShowVm
    {
        public List<ShowVmProduct> Products { get; set; }
        public decimal TotalPrice { get; set; }

        public ShowVm()
        {
            Products = new List<ShowVmProduct>();
        }
    }
}
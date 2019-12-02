using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeroMerch.Models
{
    

        public class OverviewVmProduct
        {
            public int Id { get; set; }
            public string ProductName { get; set; }
            public string ManufacturerName { get; set; }
            public string CategoryName { get; set; }
            public string ImagePath { get; set; }
            public decimal Price { get; set; }
            public string Description { get; set; }

        }
        public class OverviewVm
        {
            public List<OverviewVmProduct> Products { get; set; }
            public Dictionary<int, string> Categories { get; set; }
            public Dictionary<int, string> Manufactures { get; set; }

            public string SearchText { get; set; }
            public int SelectedCategory { get; set; }
            public int SelectedManufacture { get; set; }

            public OverviewVm()
            {
                Products = new List<OverviewVmProduct>();
                Categories = new Dictionary<int, string>();
                Manufactures = new Dictionary<int, string>();
            }
        }
    
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NeroMerch.DATA;

namespace NeroMerch.Models.Order
{
   
    

        public class NewVmProduct
        {
            public string ProductName { get; set; }
            public string ManufacturerName { get; set; }
            public int Amount { get; set; }

            public decimal LinePrice { get; set; }
        }

        public class NewVm
        {
            public List<NewVmProduct> Products { get; set; }

            public decimal TotalPrice { get; set; }

            public string Title { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Street { get; set; }
            public string Zip { get; set; }
            public string City { get; set; }

            public int PaymentMethod { get; set; }

            public NewVm()
            {
                Products = new List<NewVmProduct>();
            }
        }
   
}
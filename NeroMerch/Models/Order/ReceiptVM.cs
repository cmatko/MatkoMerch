using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeroMerch.Models.Order
{
    public class ReceiptVMProduct
    {
        public string Name { get; set; }
        public int Amount { get; set; }
        public decimal LinePrice { get; set; }
    }

    public class ReceiptVM
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalGross { get; set; }
        public decimal TotalNet { get; set; }
        public double VatPercent { get; set; }
        public decimal VatAmount { get; set; }

        public string CustomerTitle { get; set; }
        public string CustomerFirstname { get; set; }
        public string CustomerLastname { get; set; }
        public string CustomerStreet { get; set; }
        public string CustomerCity { get; set; }
        public string CustomerZip { get; set; }

        public List<ReceiptVMProduct> Products { get; set; }

        public ReceiptVM()
        {
            Products = new List<ReceiptVMProduct>();
        }
    }
}

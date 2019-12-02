using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeroMerch.Models.Order
{
    public class ThankYouVm
    {
        public int OrderId { get; set; }
        public bool IsPaid { get; set; }
    }
}
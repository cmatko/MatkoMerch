using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace NeroMerch.Models.Order
{
    public class EnterCardVm
    {
        [Required]
        public int OrderId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string CardNumber { get; set; }

        [Required]
        public string CVV { get; set; }

        [Required]
        public byte Month { get; set; }

        [Required]
        public ushort Year { get; set; }
    }
}
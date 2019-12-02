using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NeroMerch.DATA;

namespace NeroMerch.Models
{
    public class DetailVm
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImagePath { get; set; }
        public string Manufacturer { get; set; }
        public string Category { get; set; }

        public bool CanRate { get; set; }

        public double AvgRating { get; set; }
        public int NumRatings { get; set; }
        public List<string> Comments { get; set; }

        public DetailVm()
        {
            Comments = new List<string>();
        }
    }

    
}
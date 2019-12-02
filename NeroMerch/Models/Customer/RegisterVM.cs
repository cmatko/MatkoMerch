using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;

namespace NeroMerch.Models
{
    public class RegisterVM
    {
        //public List<string> AnredeList { get; }
        //[Required]
        //[EmailAddress]
        //public string Email { get; set; }
        //[Required]
        //[MinLength(8)]
        //public string Passwort { get; set; }
        //[Required]
        //[Compare("Passwort")]
        //public string Passwort_bestätigen { get; set; }
        //[Required]
        //public string Title { get; set; }
        //[Required]
        //public string FirstName { get; set; }
        //[Required]
        //public string LastName { get; set; }
        //[Required]
        //public string Street { get; set; }
        //[Required]
        //public string Zip { get; set; }
        //[Required]
        //public string City { get; set; }
        //public string HasReadTerms { get; set; }
        //public bool AcceptedTerms
        //{
        //    get
        //    {
        //        return HasReadTerms == "on";
        //    }
        //}
        //public RegisterVM()
        //{
        //    AnredeList = new List<string>
        //    {
        //        "Herr", "Frau"
        //    };
        //}

        public string Email { get; set; }
        public string Passwort { get; set; }
        public string Passwort_bestätigen { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string Zip{ get; set; }
        public string City { get; set; }
        public bool AcceptedTerms { get; set; }
    }
}

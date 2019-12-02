using NeroMerch.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;


namespace NeroMerch.Manager
{
    public static class CustomerManager
    {
        public static bool RegisterCustomer(Customer newCustomer, string passwort)
        {            
            //Formate usw. überprüfen

            //Prüfen ob Email schon vorhanden ist
            if(DoesUserEmailExist(newCustomer.Email))
            {
                return false;
            }

            //Salt erzeugen
            var salt = Helper.GetRandomBytes(256 / 8); //32 Byte

            //Password Hashen
            var pwHash = Helper.HashAndSaltString256(passwort, salt);

            //Werte zuweisen
            newCustomer.PwHash = pwHash;
            newCustomer.Salt = salt;

            //Benutzer in DB anlegen
            using (var db = new IN31_NeroMerch_WebshopEntities())
            {
                newCustomer.RoleId = 2;
                db.Customer.Add(newCustomer);
                db.SaveChanges();
                return true;
            }
        }

        public static bool IsLoginValid(string email, string password)
        {
            //Benutzer laden
            var dbCustomer = GetCustomer(email);
            if(dbCustomer == null)
            {
                return false;
            }

            //Login Passwort mit Db Passwort vergleichen
            var loginPwHash = Helper.HashAndSaltString256(password, dbCustomer.Salt);

            //Je übereinstimmung True oder False zurückgeben
            return loginPwHash.SequenceEqual(dbCustomer.PwHash);
        }

        private static bool DoesUserEmailExist(string email)
        {
            using (var db = new IN31_NeroMerch_WebshopEntities()) 
            {
                return db.Customer.Any(u => u.Email == email);
            }
        }

        public static Customer GetCustomer(string email)
        {
            using (var db = new IN31_NeroMerch_WebshopEntities())
            {
                return db.Customer.Where(u => u.Email == email).SingleOrDefault();
            }
        }
    }
}
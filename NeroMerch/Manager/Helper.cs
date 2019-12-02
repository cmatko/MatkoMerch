using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;

namespace NeroMerch.Manager
{
    public static class Helper
    {
        public static byte[] GetRandomBytes(int size)
        {
            var rndBytes = new byte[size];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetNonZeroBytes(rndBytes);
                return rndBytes;
            }
        }

        public static byte[] HashAndSaltString256(string str, byte[] salt)
        {
            using (var hasher = new SHA256Managed())
            {
                //Bytes aus dem String holen
                byte[] strBytes = Encoding.ASCII.GetBytes(str);

                //Byte Arrays von Password und String zusammenfügen zu einem Array
                byte[] saltedPw = strBytes.Concat(salt).ToArray();

                //Password+Salt hashen
                var hashedAndSaltedPw = hasher.ComputeHash(saltedPw);

                //Hashwert zurückgeben
                return hashedAndSaltedPw;
            }
        }
    }
}
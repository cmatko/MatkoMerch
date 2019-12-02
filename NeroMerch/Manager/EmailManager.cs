using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;

namespace NeroMerch.Manager
{
    public static class EmailManager
    {
        static SmtpClient smtp = new SmtpClient("srv009.edu.local", 587);
        static MailMessage email = null;
        static string from = "nenad.djurdjevic@qualifizierung.at";

        static EmailManager()
        {
            var credentials = new NetworkCredential("DjurNena", "IchLiebe3der2");

            smtp.Credentials = credentials;
            smtp.EnableSsl = false;
        }

        private static void CreateBasicMail(string to, string content, string subject, bool useHTML = true)
        {
            email = new MailMessage(from, to, subject, content);
            email.IsBodyHtml = useHTML;
        }

        public static bool SendMail(string to, string content, string subject, bool useHTML = true)
        {
            CreateBasicMail(to, content, subject, useHTML);

            try
            {
                smtp.Send(email);
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }

        public static bool SendMailWithPdf(string to, string Content, string subject, byte[] pdf, bool useHTML = true)
        {
           
            CreateBasicMail(to, Content, subject, useHTML);

            var streamX = new System.IO.MemoryStream(pdf);

            Attachment attachment = new Attachment(streamX, "Rechnung.pdf", "application/pdf");

            email.Attachments.Add(attachment);

            try
            {
                smtp.Send(email);
            }
            catch (Exception e)
            {
                return false;

            }
            return true;
        }      
    }
    
}
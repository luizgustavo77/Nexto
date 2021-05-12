using System;
using System.Net;
using System.Net.Mail;

namespace Nexto.Web.Helpers
{
    public class EMail
    {
        public static void Send(string to, string title, string body)
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress("nexto@email.com.br");
                    mail.To.Add(to);
                    mail.Subject = title;
                    mail.Body = body;

                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtp.Credentials = new NetworkCredential(AppSettings.Get("email"), AppSettings.Get("emailPassword"));
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
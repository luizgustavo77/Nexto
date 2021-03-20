using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace Commom.Util
{
    public class EMail
    {
        private static AppSettings _settings { get; set; }
        public EMail()
        {
            _settings = new AppSettings();
         }

        public static void Send(string to, string title, string body)
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress("");
                    mail.To.Add(to);
                    mail.Subject = title;
                    mail.Body = body;

                    using (SmtpClient smtp = new SmtpClient("", 000))
                    {
                        smtp.Credentials = new NetworkCredential(_settings.Get("email"), _settings.Get("emailPassword"));
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
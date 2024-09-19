using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;
using System.Text;

namespace ZenMind.Utils
{
    public class EmailUtil
    {
        public static void SendNotificationEmail(string toEmail, string subject, string body, out string StatusMessage)
        {
            StatusMessage=string.Empty;

            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress("mkmomy@gmail.com");
                    mail.To.Add(toEmail);
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true;
                 
                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtp.Credentials = new NetworkCredential("mkmomy@gmail.com", "rmtm lvhe fohh bfty");
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                    }
                }
            }
            catch (SmtpException ex)
            {
                StatusMessage = $"SMTP error: {ex.Message}";               
            }
            catch (Exception ex)
            {
                StatusMessage= $"Error while sending email: {ex.Message}";
            }
        }

    }
}

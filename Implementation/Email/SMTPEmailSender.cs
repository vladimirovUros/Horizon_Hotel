using Application;
using Application.DTO.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Email
{
    public class SMTPEmailSender : IEmailSender
    {
        public void SendEmail(SendEmailDto dto)
        {
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("hotelhorizon@gmail.com", "Lozinka1234!")
            };
            MailMessage message = new MailMessage("hotelhorizon@gmail.com", dto.SendTo);
            
            message.Subject = dto.Subject;
            
            message.Body = dto.Content;
            
            message.IsBodyHtml = true;
            
            smtp.Send(message);
        }
    }
}

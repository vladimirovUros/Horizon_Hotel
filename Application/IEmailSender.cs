using Application.DTO.Email;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application
{
    public interface IEmailSender
    {
        void SendEmail(SendEmailDto dto);
    }
}

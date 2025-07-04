using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using SocialApp.APPLICATION.Abstractions.Services;
using SocialApp.DOMAIN.Models.IdentityModels;
using SocialApp.DOMAIN.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SocialApp.INFRASTRUCTURE.Concretes.Servcies;

public class EmailService : IEmailSender
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IConfiguration _config;


    public EmailService(UserManager<AppUser> userManager, IConfiguration config)
    {
        _userManager = userManager;
        _config = config;
    }

    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var mail = new MailMessage();
        mail.From = new MailAddress("seninmail@gmail.com", "Sociala");
        mail.To.Add(email);
        mail.Subject = subject;
        mail.Body = htmlMessage;
        mail.IsBodyHtml = true;

        var smtp = new SmtpClient("smtp.gmail.com", 587)
        {
            Credentials = new NetworkCredential(_config["SMPTSettings:Mail"], _config["SMTPSettings:Pass"] ),
            EnableSsl = true
        };

        return smtp.SendMailAsync(mail);
    }
}


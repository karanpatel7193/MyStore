using CommonLibrary;
using Microsoft.Extensions.Hosting.Internal;
using ECommerce.Entity.Master;
using System.Net.Mail;
using Microsoft.Extensions.Hosting;

namespace ECommerce.Api.Common
{
    public class Emails
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public Emails(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }

        public void ResetPassword(UserEntity User)
        {
            Email email = new Email(_configuration);
            email.From = new MailAddress(AppSettings.EmailFrom);
            email.To.Add(new MailAddress(User.Email));
            email.Priority = MailPriority.Normal;
            email.Subject = "Pms reset password";

            string contentRootPath = _webHostEnvironment.ContentRootPath;

            string path = Path.Combine(contentRootPath, "EmailTemplate", "ResetPassword.html");

            string html = File.ReadAllText(path);

            string ActivationLink = string.Empty;

            ActivationLink = AppSettings.WebsiteUrl + "/auth/resetPassword/" + (User.Id + "#" + User.Email + "#" + DateTime.Now.ToString("yyyyMMddHHmmss")).ToHex();

            email.Body = html.Replace("##Username##", User.Username).Replace("##FirstName##", User.FirstName).Replace("##LastName##", User.LastName).Replace("##ActivationLink##", ActivationLink);
            //email.Send();
        }

        public void Registration(UserEntity User)
        {
            Email email = new Email(_configuration);
            email.From = new MailAddress(AppSettings.EmailFrom);
            email.To.Add(new MailAddress(User.Email));
            email.Priority = MailPriority.High;
            email.Subject = "Portfolio Management registration";

            string contentRootPath = _webHostEnvironment.ContentRootPath;

            string path = Path.Combine(contentRootPath, "EmailTemplate", "Registration.html");

            string html = File.ReadAllText(path);

            string ActivationLink = string.Empty;
            ActivationLink = AppSettings.WebsiteUrl + "/auth/activate/" + (User.Id + "#" + User.Email + "#" + User.LastUpdateDateTime.ToString("yyyyMMddHHmmss")).ToHex();

            email.Body = html.Replace("##Username##", User.Username).Replace("##FirstName##", User.FirstName).Replace("##LastName##", User.LastName).Replace("##ActivationLink##", ActivationLink);
            //email.Send();
        }

        public string RegenerateRegistrationActivation(UserEntity User)
        {
            Email email = new Email(_configuration);
            email.From = new MailAddress(AppSettings.EmailFrom);
            email.To.Add(new MailAddress(User.Email));
            email.Priority = MailPriority.High;
            email.Subject = "Portfolio Management registration";

            string contentRootPath = _webHostEnvironment.ContentRootPath;

            string path = Path.Combine(contentRootPath, "EmailTemplate", "Registration.html");

            string html = File.ReadAllText(path);

            string ActivationLink = string.Empty;
            ActivationLink = AppSettings.WebsiteUrl + "/auth/activate/" + (User.Id + "#" + User.Email + "#" + User.LastUpdateDateTime.ToString("yyyyMMddHHmmss")).ToHex();

            return ActivationLink;
        }
    }
}

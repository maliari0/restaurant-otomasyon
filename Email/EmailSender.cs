using Microsoft.AspNetCore.Identity.UI.Services;
using System.Threading.Tasks;

namespace restaurant.Email
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            throw new System.NotImplementedException();
        }
    }
}

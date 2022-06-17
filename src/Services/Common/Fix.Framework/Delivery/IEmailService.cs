using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fix.Delivery
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string message);

        Task SendEmailAsync(string[] to, string subject, string message, List<string> attachments, string[] cc = null, string[] bcc = null);
    }
}

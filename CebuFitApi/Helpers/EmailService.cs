using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System.Text;

namespace CebuFitApi.Helpers
{
    public static class EmailService
    {
        public static bool SendEmail(string email, string username, string tempPassword)
        {
            try
            {
                string smtpUsername = Environment.GetEnvironmentVariable("SMTP_USERNAME");
                string smtpPassword = Environment.GetEnvironmentVariable("SMTP_PASSWORD");

                var message = new MimeMessage();
                message.From.Add(MailboxAddress.Parse(smtpUsername));
                message.To.Add(MailboxAddress.Parse(email));
                message.Subject = "CebuFit - Password Reset";
                message.Body = new TextPart("html")
                {
                    Text = $@"
                            <html>
                                <body style='font-family: Arial, sans-serif;'>
                                    <div style='text-align: center; padding: 20px; border: 1px solid #28a745; border-radius: 10px; max-width: 600px; margin: auto;'>
                                        <h2 style='color: #ffa500;'>CebuFit - Password Reset</h2>
                                        <p style='color: #28a745;'>Dear {username},</p>
                                        <p style='color: #28a745;'>You have requested to reset your password. Below are your reset details:</p>
                                        <div style='padding: 10px; border: 1px solid #28a745; border-radius: 5px; background-color: #d4edda;'>
                                            <p><strong>Email:</strong> {email}</p>
                                            <p><strong>Temporary Password:</strong> {tempPassword}</p>
                                        </div>
                                        <p style='color: #28a745;'>Please use this password to log in and remember to change your password immediately after logging in.</p>
                                        <p style='color: #28a745;'>Thank you for choosing CebuFit!</p>
                                        <hr>
                                        <small style='color: #28a745;'>If you did not request this change, please contact our support immediately.</small>
                                    </div>
                                </body>
                            </html>"
                };


                using (var smtp = new SmtpClient())
                {
                    smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);

                    smtp.Authenticate(smtpUsername, smtpPassword);
                    smtp.Send(message);
                    smtp.Disconnect(true);
                }

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}

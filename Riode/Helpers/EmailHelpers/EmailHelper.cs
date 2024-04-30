﻿using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
namespace Riode.Helpers.EmailHelpers;

public class EmailHelper
{
    private readonly IConfiguration _configuration;

    public EmailHelper(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmailAsync(MailRequest mailRequest)
    {
        try
        {
            var email = new MimeMessage();
            if (_configuration["MailSettings:Mail"] != null)
            {
                email.Sender = MailboxAddress.Parse(_configuration["MailSettings:Mail"]);
            }
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.Subject = mailRequest.Subject;
            var builder = new BodyBuilder();
            if (mailRequest.Attachments != null)
            {
                byte[] fileBytes;
                foreach (var file in mailRequest.Attachments)
                {
                    if (file.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            fileBytes = ms.ToArray();
                        }
                        builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                    }
                }
            }
            builder.HtmlBody = mailRequest.Body;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_configuration["MailSettings:Host"], int.Parse(_configuration["MailSettings:Port"]),
            SecureSocketOptions.StartTls);
            smtp.Authenticate(_configuration["MailSettings:Mail"], _configuration["MailSettings:Password"]);
            await smtp.SendAsync(email);

            smtp.Disconnect(true);
        }
        catch (Exception ex)
        {

            throw;
        }
    }
}

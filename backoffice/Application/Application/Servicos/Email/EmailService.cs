using Application.DTOs.Email.Interface;
using Application.DTOs.Email.ViewModel;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Application.Application.Servicos.Email;

public class EmailService : IEmailService
{
    public async Task SendMailAsync(EmailViewModel emailContent)
    {
        try
        {
            var msg = new StringBuilder();
            if (!string.IsNullOrEmpty(emailContent.Link) && !string.IsNullOrEmpty(emailContent.LinkText))
            {
                msg.Append(
                    $@"
                        <!DOCTYPE html>
                            <html lang=""en"">
                                <head>
                                    <meta charset=""UTF-8"">
                                    <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"">
                                    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                                </head>
                                <body>
                                    <div style=""font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto;"">
                                        <h2 style=""color: #333;"">{emailContent.Title}</h2>
                                        <p>{emailContent.Body}</p>
                                        <p>
                                            <a href=""{emailContent.Link}"" style=""color: #007bff;"">{emailContent.LinkText}</a>
                                        </p>
                                        <p>Atenciosamente,</p>
                                        <i>Flytec</i>
                                    </div>
                                </body>
                        </html>
                    "
                );
            }
            else
            {
                msg.Append(
                    $@"
                        <!DOCTYPE html>
                            <html lang=""en"">
                                <head>
                                    <meta charset=""UTF-8"">
                                    <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"">
                                    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                                </head>
                                <body>
                                    <div style=""font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto;"">
                                        <h2 style=""color: #333;"">{emailContent.Title}</h2>
                                        <p>{emailContent.Body}</p>
                                        <p>Atenciosamente,</p>
                                        <i>Flytec</i>
                                    </div>
                                </body>
                        </html>
                    "
                );
            }

            var mailSend = new MailMessage();
            mailSend.From = new MailAddress(""); //colocar email da flytec aqui
            mailSend.To.Add(emailContent.Recipient);
            mailSend.Body = msg.ToString();
            mailSend.Subject = emailContent.Title;
            mailSend.IsBodyHtml = true;

            // CONFIGURAÇÃO DO EMAIL
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            SmtpClient client = new SmtpClient();
            client.Credentials = new NetworkCredential("apikey", "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"); //colocar credenciais da flytec (essas são do eag)
            client.Port = 587; //verificar se essa porta esta correta
            client.Host = "smtp.sendgrid.net";
            client.EnableSsl = true;
            //enviar
            await client.SendMailAsync(mailSend);
            client.Dispose();
        }
        catch(SmtpException smtpEx)
        {
            throw new SmtpException($"Erro de smtp: {smtpEx.Message}");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}

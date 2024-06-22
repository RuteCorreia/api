using Application.DTOs.Email.Interface;
using Application.DTOs.Email.ViewModel;
using Domain.Entidades.User;
using Microsoft.AspNetCore.Identity;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Application.Application.Servicos.Email;

public class EmailService : IEmailService
{
    private readonly UserManager<IdentityUser> _userManager;
    public EmailService(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }
    public async Task<string> GeneratePasswordResetTokenAsync(string email)
    {
        var resultMsg = new StringBuilder().Append("Email não Existe");
        var identityUser = await _userManager.FindByEmailAsync(email);
        if (identityUser != null)
        {
            var token = Guid.NewGuid().ToString();
            return await Task.FromResult(token);
        }
        return null;

    }

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
            mailSend.From = new MailAddress("atendimento@keltech"); //colocar email da flytec aqui
            mailSend.To.Add(emailContent.Recipient);
            mailSend.Body = msg.ToString();
            mailSend.Subject = emailContent.Title;
            mailSend.IsBodyHtml = true;

            // CONFIGURAÇÃO DO EMAIL
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            SmtpClient client = new SmtpClient();
            client.Credentials = new NetworkCredential("atendimento@keltech.app", "mbfepucevpscjijz"); //colocar credenciais da flytec (essas são do eag)
            client.Port = 25; //verificar se essa porta esta correta
            client.Host = "smtp.gmail.com";
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

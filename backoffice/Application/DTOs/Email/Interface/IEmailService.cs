using Application.DTOs.Email.ViewModel;

namespace Application.DTOs.Email.Interface;

public interface IEmailService
{
    Task SendMailAsync(EmailViewModel emailContent);
    Task<string> GeneratePasswordResetTokenAsync(string email);
}

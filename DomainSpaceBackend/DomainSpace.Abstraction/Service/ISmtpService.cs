namespace DomainSpace.Abstraction.Service;

/// <summary>
/// Smtp service
/// </summary>
public interface ISmtpService
{
    /// <summary>
    /// Send email asynchronously
    /// </summary>
    /// <param name="template">Email template</param>
    /// <param name="subject">Subject</param>
    /// <param name="recipients">Recipient</param>
    /// <param name="data">Data</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Service result</returns>
    Task<ServiceResult> SendEmailAsync(string template, string subject, EmailRecipientDto recipient, object data, CancellationToken cancellationToken = default);

    /// <summary>
    /// Send emails asynchronously
    /// </summary>
    /// <param name="template">Email template</param>
    /// <param name="subject">Subject</param>
    /// <param name="recipients">Recipients</param>
    /// <param name="data">Data</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Service result</returns>
    Task<ServiceResult> SendEmailAsync(string template, string subject, List<EmailRecipientDto> recipients, object data, CancellationToken cancellationToken = default);
}


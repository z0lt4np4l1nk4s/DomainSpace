namespace DomainSpace.Service;

/// <inheritdoc cref="ISmtpService" />
public class SmtpService : ISmtpService
{
    private readonly SmtpOptions _smtpOptions;
    private readonly AppOptions _appOptions;
    private readonly MailboxAddress _senderAddress;
    private readonly IGenericRepository<EmailEntity> _repository;

    public SmtpService(IOptions<SmtpOptions> smtpOptionsAccessor, IOptions<AppOptions> appOptionsAccessor, IGenericRepository<EmailEntity> repository)
    {
        _smtpOptions = smtpOptionsAccessor.Value;
        _appOptions = appOptionsAccessor.Value;
        _senderAddress = new MailboxAddress(_smtpOptions.FromName, _smtpOptions.FromEmail);
        _repository = repository;
    }

    /// <inheritdoc cref="ISmtpService.SendEmailAsync(string, string, EmailRecipientDto, object, CancellationToken)" />
    public Task<ServiceResult> SendEmailAsync(string template, string subject, EmailRecipientDto recipient, object data, CancellationToken cancellationToken = default)
    {
        return SendEmailAsync(template, subject, new List<EmailRecipientDto> { recipient }, data, cancellationToken);
    }

    /// <inheritdoc cref="ISmtpService.SendEmailAsync(string, string, List{EmailRecipientDto}, object, CancellationToken)" />
    public async Task<ServiceResult> SendEmailAsync(string template, string subject, List<EmailRecipientDto> recipients, object data, CancellationToken cancellationToken = default)
    {
        var emailTemplate = await LoadTemplateAsync(EmailTemplates.EmailTemplate);
        var header = await LoadTemplateAsync(EmailTemplates.Header);
        var footer = await LoadTemplateAsync(EmailTemplates.Footer);
        var content = await LoadTemplateAsync(template);

        emailTemplate = emailTemplate
            .Replace("@{{Header}}", header)
            .Replace("@{{Content}}", content)
            .Replace("@{{Footer}}", footer);

        emailTemplate = ReplaceTemplatePlaceholders(emailTemplate, data);

        using var client = await GetSmtpClientAsync(cancellationToken);

        var emailMessage = new MimeMessage();

        emailMessage.From.Add(_senderAddress);
        var emailEntities = new List<EmailEntity>();
        var utcNow = DateTime.UtcNow;

        foreach (var recipient in recipients)
        {
            emailMessage.To.Add(new MailboxAddress(recipient.Name, recipient.Email));

            emailEntities.Add(new EmailEntity
            {
                Id = Guid.NewGuid(),
                CreationTime = utcNow,
                UpdateTime = utcNow,
                UserId = recipient.UserId,
                Title = subject,
                Template = emailTemplate,
                Data = JsonSerializer.Serialize(data),
            });
        }

        emailMessage.Subject = subject;
        emailMessage.Body = new TextPart("html")
        {
            Text = emailTemplate,
        };

        var tasks = new Task[]
        {
            client.SendAsync(emailMessage, cancellationToken),
            _repository.AddRangeAsync(emailEntities, cancellationToken)
        };

        await Task.WhenAll(tasks);

        return ServiceResult.Success();
    }

    private async Task<string> LoadTemplateAsync(string templateFileName)
    {
        var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Files", "Private", "Templates", "Email", templateFileName);
        return await File.ReadAllTextAsync(templatePath);
    }

    private string ReplaceTemplatePlaceholders<T>(string template, T data)
    {
        template = template.Replace("@{{FrontendAppUrl}}", _appOptions.FrontendAppUrl);
        template = template.Replace("@{{Year}}", DateTime.UtcNow.Year.ToString());
        template = template.Replace("@{{AppName}}", _appOptions.AppName);

        if (data != null)
        {
            var properties = data.GetType().GetProperties();
            foreach (var property in properties)
            {
                var placeholder = $"@{{{{{property.Name}}}}}";
                var value = property.GetValue(data)?.ToString() ?? string.Empty;
                template = template.Replace(placeholder, value);
            }
        }

        return template;
    }

    private async Task<SmtpClient> GetSmtpClientAsync(CancellationToken cancellationToken = default)
    {
        var client = new SmtpClient();
        await client.ConnectAsync(_smtpOptions.Host, _smtpOptions.Port!.Value, _smtpOptions.UseSsl ? SecureSocketOptions.StartTls : SecureSocketOptions.None, cancellationToken);

        if (_smtpOptions.RequireAuth)
        {
            await client.AuthenticateAsync(_smtpOptions.Username, _smtpOptions.Password, cancellationToken);
        }

        return client;
    }
}

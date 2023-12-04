namespace Notifications.Infrastructure.Domain.Entities;

public class EmailTemplate
{
    public EmailTemplate()
    {
        Type = Type.Email;
    }
    public string Subject { get; set; } = default!;
}

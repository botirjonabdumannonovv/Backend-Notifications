namespace Notifications.Infrastructure.Domain.Entities;

public class SmsTemplate : NotificationTemplate
{
    public SmsTemplate()
    {
        Type = Type.S;
    }
}

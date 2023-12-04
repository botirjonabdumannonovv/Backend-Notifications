using Notifications.Application.Common.Enums;
using Notifications.Application.Common.Notifications.Models;
using Notifications.Application.Common.Notifications.Services;
using Notifications.Domain.Common.Exceptions;
using Notifications.Domain.Entites;
using Notifications.Domain.Extentions;

namespace Notifications.Infrastructure.Common.Notifications.Services;

public class NotificationAggregatorService : INotificationAggregatorService
{
    private readonly ISmsOrchestrationService _smsOrchestrationService;

    public NotificationAggregatorService(ISmsOrchestrationService smsOrchestrationService)
    {
        _smsOrchestrationService = smsOrchestrationService;
    }

    public async ValueTask<FuncResult<bool>> SendAsync(
        NotificationRequest notificationRequest,
        CancellationToken cancellationToken = default
    )
    {
        var test = async () =>
        {
            var senderUser = new User
            {
                PhoneNumber = "+12132931337"
            };
            var receiverUser = new User
            {
                PhoneNumber = "+998500093579"
            };
            var sendNotificationTask = notificationRequest.NotificationType switch
            {
                NotificationType.Sms => SendSmsAsync(senderUser,
                    receiverUser,
                    notificationRequest.TemplateType,
                    notificationRequest.Variables,
                    cancellationToken),
                _ => throw new NotImplementedException("Email is not supported yet.")
            };

            var sendNotificationResult = await sendNotificationTask;
            return sendNotificationResult.Data;
        };

        return await test.GetValueAsync();
    }

    private async ValueTask<FuncResult<bool>> SendSmsAsync(
        User senderUser,
        User receiverUser,
        NotificationTemplateType templateType,
        Dictionary<string, string> variables,
        CancellationToken cancellationToken = default
    )
    {
        return await _smsOrchestrationService.SendAsync(senderUser.PhoneNumber,
            receiverUser.PhoneNumber,
            templateType,
            variables,
            cancellationToken);
    }
}
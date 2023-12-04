using Notifications.Application.Common.Notifications.Models;
using Notifications.Domain.Common.Exceptions;

namespace Notifications.Application.Common.Notifications.Services;

public interface INotificationAggregatorService
{
    ValueTask<FuncResult<bool>> SendAsync(
        NotificationRequest notificationRequest,
        CancellationToken cancellationToken = default
        );
}

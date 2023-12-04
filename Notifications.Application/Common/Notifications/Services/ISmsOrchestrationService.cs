using Notifications.Application.Common.Enums;
using Notifications.Domain.Common.Exceptions;

namespace Notifications.Application.Common.Notifications.Services;

public interface ISmsOrchestrationService
{
    ValueTask<FuncResult<bool>> SendAsync(
        string senderPhoneNumber,
        string receiverPhoneNumber,
        NotificationTemplateType templateType,
        Dictionary<string, string> variables,
        CancellationToken cancellationToken = default
        );
}

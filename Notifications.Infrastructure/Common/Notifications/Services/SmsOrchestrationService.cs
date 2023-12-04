using System.Text;
using System.Text.RegularExpressions;

using Notifications.Application.Common.Enums;
using Notifications.Application.Common.Notifications.Services;
using Notifications.Domain.Common.Exceptions;
using Notifications.Domain.Extentions;

namespace Notifications.Infrastructure.Common.Notifications.Services;

public class SmsOrchestrationService : ISmsOrchestrationService
{
    private readonly ISmsSenderService _smsSenderService;

    public SmsOrchestrationService(ISmsSenderService smsSenderService)
    {
        _smsSenderService = smsSenderService;
    }

    public async ValueTask<FuncResult<bool>> SendAsync(
        string senderPhoneNumber, 
        string receiverPhoneNumber,
        NotificationTemplateType templateType,
        Dictionary<string, string> variables, 
        CancellationToken cancellationToken = default
        )
    {
        var test = async () =>
        {
            var template = GetTemplate(templateType);
            var message = GetMessage (template, variables);
            await _smsSenderService.SendAsync(senderPhoneNumber, receiverPhoneNumber, message, cancellationToken);

            return true;
        };

        return await test.GetValueAsync();
    }

    private string GetTemplate(NotificationTemplateType templateType)
    {
        var template = templateType switch
        {
            NotificationTemplateType.EmialVerificationNotification => "Welcome to the system, {{UserName}}",
            NotificationTemplateType.SystemWelomeNotification => "Verify your email by clicking the link, {{VerificationLink}}",
            _ => throw new ArgumentOutOfRangeException(nameof(templateType), "")
        };
        return template;
    }

    private string GetMessage(string template,Dictionary<string, string> variables)
    {
        var messageBuilder = new StringBuilder(template);

        var pattern = @"\{\{([^\{\}]+)\}\}";
        var matchValuePattern = "{{(.*?)}}";

        var matches = Regex.Matches(template, pattern)
            .Select(match =>
            {
                var placeHolder = match.Value;
                var placeHolderValue = Regex.Match(placeHolder, matchValuePattern).Groups[1].Value;
                var valid = variables.TryGetValue(placeHolderValue, out var value);

                return new
                {
                    PlaceHolder = placeHolder,
                    Value = value,
                    IsValid = valid
                };
            });
        foreach(var match in matches)
        {
            messageBuilder.Replace(match.PlaceHolder,match.Value);
        }
        var message = messageBuilder.ToString();
        return message; 
    }
}

using Notifications.Application.Common.Notifications.Brokers;
using Notifications.Persistence.Common.Notifications.Brokers;
using Notifications.Application.Common.Notifications.Services;
using Notifications.Infrastructure.Common.Notifications.Services;

namespace Notifications.Api.Configurations;

public static partial class HostConfiguration
{
    private static WebApplicationBuilder AddNotificationInfrastructure(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ISmsSenderBroker, TwilioSmsSenderBroker>();

        builder.Services.AddScoped<ISmsSenderService, SmsSenderService>();

        builder.Services
            .AddScoped<ISmsOrchestrationService, SmsOrchestrationService>()
            .AddScoped<INotificationAggregatorService, NotificationAggregatorService>();

        return builder;
    }
    private static WebApplicationBuilder AddExposers(this WebApplicationBuilder builder)
    {
        builder.Services.AddRouting(x => x.LowercaseUrls = true);
        builder.Services.AddControllers();

        return builder;
    }

    private static WebApplication UseExposers(this WebApplication app)
    {
        app.MapControllers();

        return app;
    }
}

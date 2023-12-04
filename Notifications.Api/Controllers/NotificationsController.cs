using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Notifications.Application.Common.Notifications.Models;
using Notifications.Application.Common.Notifications.Services;

namespace Notifications.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NotificationsController : ControllerBase
{
    private readonly INotificationAggregatorService _notificationAggregatorService;

    public NotificationsController(INotificationAggregatorService notificationAggregatorService)
    {
        _notificationAggregatorService = notificationAggregatorService;
    }
    [HttpPost]
    public async ValueTask<IActionResult> Send([FromBody] NotificationRequest notificationRequest)
    {
        var result = await _notificationAggregatorService.SendAsync(notificationRequest);
        return result.IsSucces ? Ok(result) : BadRequest(result);
    }
}

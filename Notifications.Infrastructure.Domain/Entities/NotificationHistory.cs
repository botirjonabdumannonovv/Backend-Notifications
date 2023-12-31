﻿using Notifications.Infrastructure.Domain.Common.Entities;
using Notifications.Infrastructure.Domain.Enums;

namespace Notifications.Infrastructure.Domain.Entities;

public class NotificationHistory : IEntity
{
    public Guid Id { get; set; }

    public Guid TemplateId { get; set; }

    public Guid SenderUserId { get; set; }  

    public Guid ReceiverUserId { get; set; }

    public NotificationType Type { get; set; }

    public string Content { get; set; } = default!;

    public NotificationTemplate Template { get; set; }
}

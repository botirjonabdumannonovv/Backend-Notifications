﻿namespace Notifications.Domain.Entites;

public class User
{
    public Guid Id { get; set; }

    public string UserName { get; set; } = default!;

    public string PhoneNumber { get; set; } = default!;

    public string EmailAddress { get; set; } = default!;
}

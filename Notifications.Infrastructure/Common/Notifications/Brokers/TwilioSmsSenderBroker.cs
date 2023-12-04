﻿using Notifications.Application.Common.Notifications.Brokers;

using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Notifications.Persistence.Common.Notifications.Brokers;

public class TwilioSmsSenderBroker : ISmsSenderBroker
{

    public ValueTask<bool> SendAsync(
        string senderPhoneNumber, 
        string receiverPhoneNumber, 
        string message,
        CancellationToken cancellationToken
        )
    {
        var testA = "ACe09f7247dfbdf25dbe2ef0acdf2279f9";
        var testB = "e1fdedded3a1a2ddf74da5336dd7687d";

        TwilioClient.Init( testA, testB );

        var messageContent = MessageResource.Create(
            body: message,
            from: new Twilio.Types.PhoneNumber(senderPhoneNumber),
            to: new Twilio.Types.PhoneNumber(receiverPhoneNumber)
            );

        return new ValueTask<bool>(true);
    }
}

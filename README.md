#Gmail API Simple Console Demo

Simple console application to use the GMail API to send an email, couldn't get a C# example of this anywhere and it didn't seem clear what needed to be done but the following sources are what got it going:

What needs done
http://stackoverflow.com/questions/24460422/how-to-send-a-message-successfully-using-the-new-gmail-rest-api

.Net web safe UTF-8 encoding
http://stackoverflow.com/questions/13017703/java-and-net-base64-conversion-confusion

Test encoded messages with the Gmail API Reference:
https://developers.google.com/gmail/api/v1/reference/users/messages/send#try-it

Not perfect has some issues around the encoding but as an example it works.

To use set up application credentials for an installed application (this will produce a Client ID and Client Secret): https://developers.google.com/google-apps/calendar/instantiate

Get the API packages to build: https://www.nuget.org/packages/Google.Apis.Gmail.v1/

Replace the six keys in the app.config:
```
    <add key="ClientId" value="YOUR_CLIENT_ID" />
    <add key="ClientSecret" value="YOUR_CLIENT_SECRET" />
    <add key="EmailSenderName" value="NAME_TO_APPEAR_FOR_EMAIL_SENDER" />
    <add key="EmailSenderAddress" value="EMAIL_ADDRESS_FOR_EMAIL_SENDER" />
    <add key="EmailReceiverName" value="NAME_TO_APPEAR_FOR_EMAIL_RECEIVER" />
    <add key="EmailReceiverAddress" value="EMAIL_ADDRESS_FOR_EMAIL_RECEIVER" />
```

When first run you may be prompted to grant compose email access to the application (this appears in a browser), approve this

Want to read email instead?

Change scope for access to
```
	GmailService.Scope.GmailModify
```

And try something like:
```
	var query = service.Users.Messages.List("me");

	var mail = query.Execute();

	foreach (var item in mail.Messages)
	{
	    Message massage = service.Users.Messages.Get("me", item.Id).Execute();

	     Console.WriteLine("Message dump: {0}", massage.Payload.Body.Data);
	}
```
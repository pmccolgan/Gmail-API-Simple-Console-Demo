using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;

namespace GmailAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Google Calender API v3");

            var clientId = ConfigurationManager.AppSettings["ClientId"];
            var clientSecret = ConfigurationManager.AppSettings["ClientSecret"];

			var SenderName = ConfigurationManager.AppSettings["EmailSenderName"];
			var SenderAddress = ConfigurationManager.AppSettings["EmailSenderAddress"];
			var ReceiverName = ConfigurationManager.AppSettings["EmailReceiverName"];
			var ReceiverAddress = ConfigurationManager.AppSettings["EmailReceiverAddress"];

            try
            {
                var credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    new ClientSecrets
                    {
                        ClientId = clientId,
                        ClientSecret = clientSecret,
                    },
                    new[] { GmailService.Scope.GmailCompose },
                    "user",
                    CancellationToken.None).Result;

                var service = new GmailService(new BaseClientService.Initializer
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "Gmail API",
                });

				// only the raw parameter of the message resource needs set, see: http://stackoverflow.com/questions/24460422/how-to-send-a-message-successfully-using-the-new-gmail-rest-api
				// from that there is a working example in the RFC 2822nspecification: "From: John Doe <jdoe@machine.example>\nTo: Mary Smith <mary@example.net>\nSubject: Saying Hello\nDate: Fri, 21 Nov 1997 09:55:06 -0600\nMessage-ID: <1234@local.machine.example>\n\nThis is a message just to say hello.\nSo, \"Hello\".";
				// use that as a working base
				// the values Date and Message-ID have no bearing on the final email (or didn't to me) so have keep them as placeholders and haven't tried to replace them

				var subject = "Email Subject";

				// there are some issues around the body encoding/decoding
				// this message decoded will have a '5' at the end
				// a full stop at the end will make an invalid raw parameter
				// but this was good enough for my purposes...
				var body = "Hello this is an email wriien by a very simple console application";

				// format the message
				var text = string.Format("From: {0} <{1}>\nTo: {2} <{3}>\nSubject: {4}\nDate: Fri, 21 Nov 1997 09:55:06 -0600\nMessage-ID: <1234@local.machine.example>\n\n{5}", 
					SenderName,
					SenderAddress,
					ReceiverName,
					ReceiverAddress,
					subject,
					body);

				Console.WriteLine("Send email:\n{0}", text);

				// must be base64 encoded but also web safe and also initially UTF8
				// http://stackoverflow.com/questions/24460422/how-to-send-a-message-successfully-using-the-new-gmail-rest-api
				// http://stackoverflow.com/questions/13017703/java-and-net-base64-conversion-confusion
				var encodedText = System.Web.HttpServerUtility.UrlTokenEncode(System.Text.Encoding.UTF8.GetBytes(text));

				Console.WriteLine("Raw email:\n{0}", encodedText);

				var message = new Message();

				message.Raw = encodedText;

				var request = service.Users.Messages.Send(message, "me").Execute();

				Console.WriteLine(
					string.IsNullOrEmpty(request.Id) ? "Issue sending, returned id: {0}" : "Email looks good, id populated: {0}", 
					request.Id);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception encountered: {0}", e.Message);
            }

            Console.WriteLine("Press any key to continue...");

            while (!Console.KeyAvailable)
            {
            }
        }

        static byte[] GetBytes(string str)
        {
			// From http://stackoverflow.com/questions/472906/converting-a-string-to-byte-array
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }
    }
}

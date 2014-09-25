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

            try
            {
                var credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    new ClientSecrets
                    {
                        ClientId = clientId,
                        ClientSecret = clientSecret,
                    },
                    new[] { GmailService.Scope.GmailReadonly },
                    "user",
                    CancellationToken.None).Result;

                var service = new GmailService(new BaseClientService.Initializer
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "Gmail API",
                });

                //var message = new Message();
                //message.Payload = new MessagePart();
                //message.Payload.Body = new MessagePartBody();
                //message.Payload.Body.Data = "";

                //message.Payload.Body.da

                //// use special value me to send from authenticated account
                //service.Users.Messages.Send(message, "LentBot@gmail.com").Execute();

                var query = service.Users.Messages.List("LentBot@gmail.com");

                var mail = query.Execute();

                int badger = 7;

                //var queryStart = DateTime.Now;
                //var queryEnd = queryStart.AddYears(1);

                //var query = service.Events.List(calendarId);
                //// query.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime; - not supported :(
                //query.TimeMin = queryStart;
                //query.TimeMax = queryEnd;

                //var events = query.Execute().Items;

                //var eventList = events.Select(e => new Tuple<DateTime, string>(DateTime.Parse(e.Start.Date), e.Summary)).ToList();
                //eventList.Sort((e1, e2) => e1.Item1.CompareTo(e2.Item1));

                //Console.WriteLine("Query from {0} to {1} returned {2} results", queryStart, queryEnd, eventList.Count);

                //foreach (var item in eventList)
                //{
                //    Console.WriteLine("{0}\t{1}", item.Item1, item.Item2);
                //}
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
    }
}

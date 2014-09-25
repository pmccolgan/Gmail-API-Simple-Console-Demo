#Twitter API Simple Console Demo

Simple console application to use the GMail API to send an email.

To use set up application credentials for an installed application (this will produce a Client ID and Client Secret): https://developers.google.com/google-apps/calendar/instantiate

Get the API packages to build: https://www.nuget.org/packages/Google.Apis.Gmail.v1/

Replace the three keys in the app.config:
```
        <add key="ClientId" value="YOUR_CLIENT_ID" />
        <add key="ClientSecret" value="YOUR_CLIENT_SECRET" />
        <add key="CalendarId" value="YOUR_CALENDAR_ID" />???????????????????????????????
```

When first run you may be prompted to grant calendar access to the application (this appears in a browser), approve this and the calendar should be readable?????????????????????????????????

http://webstackoflove.com/read-google-gmail-using-dot-net-api-client-library-for-csharp/
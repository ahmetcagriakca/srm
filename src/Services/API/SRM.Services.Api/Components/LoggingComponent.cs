using Fix.Logging;
using Fix.Mvc.Middleware;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fix.Hosting.App.Components
{
    public class LoggingComponent : IMiddlewareComponent
    {
        private const string TOKEN_KEY = "Authorization";
        private readonly ILogManager logManager;

        public DateTime Begin { get; set; }
        public DateTime End { get; set; }
        public string Url { get; set; }
        public string RemoteIpAddress { get; set; }
        public string Token { get; set; }
        public LoggingComponent(ILogManager logManager)
        {
            this.logManager = logManager ?? throw new ArgumentNullException(nameof(logManager));
        }
        public int SequenceNo => 1;

        public bool CanInvoke => true;

        public async Task<bool> OnRequest(HttpContext context)
        {
            RemoteIpAddress = context.Connection.RemoteIpAddress.ToString();
            Url = $"{context.Request.Host}{context.Request.Path}{context.Request.QueryString.Value}";
            Begin = DateTime.Now;
            if (context.Request.Headers.TryGetValue(TOKEN_KEY, out StringValues values))
            {
                Token = values.First();
            }
            await logManager.Logger.TraceAsync(await FormatRequest(context.Request));
            //Stream requestBody=null;
            //context.Request.Body.CopyTo(requestBody, context.Request.Body.Length.ToInteger());
            //using (var reader = new StreamReader(requestBody))
            //{
            //    var body = reader.ReadToEnd();
            //}
            return true;
        }
        private async Task<string> FormatRequest(HttpRequest request)
        {
            var body = request.Body;
            // Enable seeking
            request.EnableBuffering();
            // Read the stream as text
            var bodyAsText = await new System.IO.StreamReader(request.Body).ReadToEndAsync();
            // Set the position of the stream to 0 to enable rereading
            request.Body.Position = 0;
            var builder = new StringBuilder();

            builder
                .Append(Begin.ToString("dd.MM.yyyy HH:mm:ss:fff"))
                .Append(" ")
                .Append(request.Scheme)
                .Append(" ")
                .Append(RemoteIpAddress)
                .Append(" ")
                .Append(Url)
                .Append(" ")
                .AppendLine("Token:" + Token)
                .AppendLine("Body:" + bodyAsText);

            return builder.ToString();
        }
        public async Task<bool> OnResponse(HttpContext context)
        {
            End = DateTime.Now;
            await logManager.Logger.TraceAsync(await FormatResponse(context.Response));
            return true;
        }
        private async Task<string> FormatResponse(HttpResponse response)
        {
            response.Body.Position = 0;
            string responseBody = await new StreamReader(response.Body).ReadToEndAsync();
            var builder = new StringBuilder();
            TimeSpan span = End - Begin;
            int ms = (int)span.TotalMilliseconds;
            builder
                .Append($"Request begin {Begin.ToString("dd.MM.yyyy HH:mm:ss:fff")} - End {End.ToString("dd.MM.yyyy HH:mm:ss:fff")} - {ms}ms")
                .Append(" ")
                .Append(Url)
                .Append(" ")
                .Append(RemoteIpAddress)
                .AppendLine()
                .AppendLine("Response Body:" + responseBody);

            return builder.ToString();
        }
    }
}

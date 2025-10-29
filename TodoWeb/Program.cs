using Microsoft.Net.Http.Headers;
using System.Net.Mime;

namespace TodoWeb
{
    public class Program
    {
        public const string HttpClientName = "MyApiClient";

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder();
            builder.Services.AddControllersWithViews();
            builder.Services.AddHttpClient(HttpClientName, client => 
            {
                client.BaseAddress = new("http://localhost:5116/todo/");
                //client.DefaultRequestHeaders.Add(HeaderNames.Accept, MediaTypeNames.Application.Json);
            });

            var app = builder.Build();

            app.UseStaticFiles();
            app.UseRouting();
            app.UseStatusCodePagesWithReExecute("/Errors/Index", "?code={0}");
            app.MapDefaultControllerRoute();

            app.Run();
        }
    }
}

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
                client.DefaultRequestHeaders.Add("X-Tenant-Id", "Tenant-A");
                client.DefaultRequestHeaders.Add("X-Tenant-Key", "ee14fde1-fb2e-4100-9a07-fa52a0e4d2a8");
            });

            var app = builder.Build();

            app.UseMiddleware<MaintenanceGateway>();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseStatusCodePagesWithReExecute("/Errors/Index", "?code={0}");
            app.MapDefaultControllerRoute();

            app.Run();
        }
    }
}

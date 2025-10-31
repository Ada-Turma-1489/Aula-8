namespace DemoMvc
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder();

            // PARZINHO (Add, Use/Map)

            // Em vez .AddControllers() a gente usa
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            app.MapDefaultControllerRoute();

            app.Run();
        }
    }
}

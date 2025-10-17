namespace TodoApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder();

            // Configurando a Injeção de Dependência
            builder.Services.AddControllers();
            builder.Services.AddDbContext<TodoDbContext>();


            var app = builder.Build();

            // Configura os Endpoints
            // https://localhost:7109/todo/nova
            app.MapControllers();

            app.Run();
        }
    }
}

using System.Reflection;
using System.Text.Json.Serialization;
using TodoApi.Security;

namespace TodoApi
{
    public class Program
    {
        const string DEFAULT_CORS_POLICY = "DefaultCors";

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.Configure<ApiAccessOptions>(builder.Configuration.GetSection("ApiAccess"));
            builder.Services.AddCors(option => 
            {
                option.AddPolicy(DEFAULT_CORS_POLICY, policy =>
                {
                    policy.WithOrigins("http://localhost:3000")                  //.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new()
                {
                    Version = "v1",
                    Title = "Caixaverso - Turma 1489 - TODO Api",
                    Description = "API de Exemplo do Módulo Web II",
                    TermsOfService = new Uri("http://www.caixa.gov.br/tos"),
                    Contact = new()
                    {
                        Name = "Paulo Ricardo Stradioti",
                        Email = "paulo@paulo.eti.br",
                        Url = new Uri("http://www.paulo.bio")
                    },
                    License = new()
                    {
                        Name = "MIT",
                        Url = new Uri("http://www.mit.org")
                    }
                });

                var documentationFile = Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
                c.IncludeXmlComments(documentationFile);
            });

            builder.Services.AddControllers(options => 
            {
                options.Filters.Add<ApiTenantFilter>();
            })
                .AddJsonOptions(option => option.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));


            builder.Services.AddDbContext<CaixaDbContext>();

            var app = builder.Build();
            app.UseCors(DEFAULT_CORS_POLICY);
            app.UseSwagger();
            app.UseSwaggerUI();
            app.MapControllers();


            app.Run();
        }
    }
}

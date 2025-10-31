using System.Net.Mime;

namespace TodoWeb
{
    public class MaintenanceGateway
    {
        private readonly RequestDelegate next;

        // 2 coisas especiais sobre middlewares:
        //  1 - construtor que recebe um RequestDelegate
        //  2 - Método InvokeAsync que recebe um HttpContext
        public MaintenanceGateway(RequestDelegate next) 
            => this.next = next;

        public async Task InvokeAsync(HttpContext context)
        {
            var isMaintenance = true;

            if (isMaintenance)
            {
                var html = """
                        <!DOCTYPE html>
                        <html lang="pt-br">
                        <head>
                          <meta charset="utf-8" />
                          <title>Site em Manutenção</title>
                        </head>
                        <body>
                          <h1>Estamos em Manutenção.</h1>
                          <p>Tente novamente mais tarde.</p>
                        </body>
                        </html>
                        """;

                context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
                context.Response.ContentType = MediaTypeNames.Text.Html;

                await context.Response.WriteAsync(html);

                return;
            }

            await next(context);
        }
    }
}

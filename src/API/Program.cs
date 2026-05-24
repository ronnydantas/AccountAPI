using API.Extensions;
using API.Extensions.SwaggerConfigurations;
using Domain.Entities;
using Domain.UseCases.AddAccount;
using Infrastructure;

/// <summary>
/// Classe principal do aplicativo Account API.
/// </summary>
public class Program
{
    /// <summary>
    /// Ponto de entrada principal do aplicativo.
    /// </summary>
    /// <param name="args">Argumentos de linha de comando.</param>
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Configuração de serviços
        builder.Services
            .AddSwaggerConfig(builder.Configuration)
            .AddControllers();

        builder.Services.AddCustomCors();

        builder.Services.AddRepository(builder.Configuration);
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AddAccountUseCase).Assembly));

        builder.Services.Configure<KafkaSettings>(builder.Configuration.GetSection("Kafka"));

        var app = builder.Build();

        app.UsePathBase("/account-api");

        app.UseCustomCors();

        app.UseRouting();

        // Swagger
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/account-api/swagger/v1/swagger.json", "Account API V1");
            options.RoutePrefix = string.Empty;
        });

        app.MapControllers();

        await app.RunAsync();
    }
}
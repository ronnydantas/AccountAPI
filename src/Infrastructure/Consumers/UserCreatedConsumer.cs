using Confluent.Kafka;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Infrastructure.Consumers;

public class UserCreatedConsumer : BackgroundService
{
    private readonly KafkaSettings _settings;

    private readonly IServiceScopeFactory _scopeFactory;

    public UserCreatedConsumer(IOptions<KafkaSettings> settings, IServiceScopeFactory scopeFactory)
    {
        _settings = settings.Value;
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var config = new ConsumerConfig
        {
            BootstrapServers = _settings.BootstrapServers,

            GroupId = _settings.GroupId,

            AutoOffsetReset = AutoOffsetReset.Earliest,

            EnableAutoCommit = true,

            SessionTimeoutMs = 10000,

            SocketTimeoutMs = 10000,

            AllowAutoCreateTopics = true
        };

        using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();

        consumer.Subscribe(_settings.TopicUserCreated);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var result = consumer.Consume(stoppingToken);

                var message = result.Message.Value;

                var userCreated = JsonSerializer.Deserialize<ClienteConsumer>(message);

                if (userCreated == null) continue;

                using var scope = _scopeFactory.CreateScope();

                var context = scope.ServiceProvider.GetRequiredService<AccountContext>();

                var existe = await context.Contas.AnyAsync(x => x.UserId == Guid.Parse(userCreated.Id));

                if (existe) continue;

                var repository = scope.ServiceProvider.GetRequiredService<IAccountRepository>();

                await repository.CriarContaComPreDados(userCreated);

            }
            catch (ConsumeException ex)
            {
                Console.WriteLine($"Erro Kafka Consume: {ex.Error.Reason}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro geral consumer: {ex.Message}");
            }
        }
    }
}
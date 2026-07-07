using MassTransit;
using Notification.Worker.Consumers;

var builder = Host.CreateApplicationBuilder(args);

// MassTransit — register both consumers
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<OrderPlacedNotificationConsumer>();
    x.AddConsumer<OrderConfirmedNotificationConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMQ:Host"] ?? "localhost", "/", h =>
        {
            h.Username(builder.Configuration["RabbitMQ:Username"] ?? "guest");
            h.Password(builder.Configuration["RabbitMQ:Password"] ?? "guest");
        });

        cfg.ConfigureEndpoints(context);
    });
});

var host = builder.Build();
host.Run();

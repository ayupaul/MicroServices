using Common.Global_Exception_Handler;
using MassTransit;

using ProductAPIService;
using ProductAPIService.DataSource;
using Steeltoe.Discovery.Client;
using Steeltoe.Discovery.Eureka;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddServiceDiscovery(o => o.UseEureka());
// Add services to the container.
builder.Services.AddSingleton<IProductData,ProductData>();
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<ProductConsumer>();
    x.UsingRabbitMq((ctx, config) =>
    {
        //config.Host("amqp://guest:guest@localhost:5672");
        config.Host("rabbitmq", "/", h => { });
        config.ReceiveEndpoint("order-queue", c => {
            c.ConfigureConsumer<ProductConsumer>(ctx);
        });
    });
});
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();
app.UseMiddleware<GlobalExceptionHandler>();
app.MapControllers();

app.Run();

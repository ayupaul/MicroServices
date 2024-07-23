
using CartAPIService.DataSource;
using CartAPIService.RabbitMQMessageService;
using Common.Global_Exception_Handler;
using MassTransit;

using Steeltoe.Discovery.Client;
using Steeltoe.Discovery.Eureka;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ProductVerificationService>();
builder.Services.AddControllers();
builder.Services.AddServiceDiscovery(o => o.UseEureka());
builder.Services.AddSingleton<CartData>();
builder.Services.AddMassTransit(x =>
{
   
    x.UsingRabbitMq((ctx, config) =>
    {
      
        config.Host("rabbitmq", "/", h => { }); //if i am giving configuration of rabbit mq in this way masstransit by default assume username and password as guest
     
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();
app.UseMiddleware<GlobalExceptionHandler>();
app.MapControllers();

app.Run();

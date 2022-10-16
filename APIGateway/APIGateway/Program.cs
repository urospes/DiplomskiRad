using APIGateway.IServices;
using APIGateway.Services;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient("CarsHttpClient", client =>
{
    client.BaseAddress = new Uri("http://cars-service");
}).SetHandlerLifetime(TimeSpan.FromMinutes(5));

builder.Services.AddHttpClient("DefectsHttpClient", client =>
{
    client.BaseAddress = new Uri("http://defects-service");
}).SetHandlerLifetime(TimeSpan.FromMinutes(5));

builder.Services.AddScoped<IGatewayService, GatewayService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseRouting();

app.UseHttpMetrics();

builder.Services.AddSystemMeti

app.UseAuthorization();

app.MapControllers();

app.UseEndpoints(endpoints => {
    endpoints.MapMetrics();
});

app.Run();

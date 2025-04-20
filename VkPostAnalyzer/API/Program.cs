using Domain.Interfaces;
using Infrastructure;
using Infrastructure.VkSettings;
using Service;
using VkPostAnalyzer.Logs;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<VkApiSettings>(builder.Configuration.GetSection("Vk"));
builder.Services.AddInfrastructureDependencies(builder.Configuration);
builder.Services.AddServces();
builder.Services.AddHttpClient<IVkApiClient, VkApiClient>();
// Add services to the container
builder.Services.AddControllers();

// Настраиваем Swagger
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ActionLogger>();



var app = builder.Build();
// Добавьте это перед app.Run()
app.UseSwagger();
app.UseSwaggerUI(c => 
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

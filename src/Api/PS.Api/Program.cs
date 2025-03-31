using PS.Api.Middleware;
using System.Text.Json.Serialization;
using System.Text.Json;
using PS.Infrastructure.Utilities;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

string corsOpenPolicy = "OpenCORSPolicy";

builder.Services.AddExceptionHandler<ApiExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddCors(options =>
{
    options.AddPolicy(
      name: corsOpenPolicy,
      corsPolicyBuilder => {
          corsPolicyBuilder
          .AllowAnyOrigin()
          .AllowAnyHeader()
          .AllowAnyMethod();
      });
});

builder.Services.AddHttpClient();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.WriteIndented = true;

    });

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setup => setup.SwaggerDoc("v1", new OpenApiInfo()
{
    Description = "Tax Calculation",
    Title = "PaySpace Api",
    Version = "v1",
    Contact = new OpenApiContact()
    {
        Name = "Allan Alex",
        Url = new Uri("https://www.linkedin.com/in/allanalexodhiambo/")
    }
}));


var app = builder.Build();

app.MapOpenApi();
app.UseSwagger();
app.UseSwaggerUI();
app.UseExceptionHandler((_ => { }));
app.UseHsts();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors(corsOpenPolicy);
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
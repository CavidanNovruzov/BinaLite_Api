using API.Extensions;
using Application.Options;


var builder = WebApplication.CreateBuilder(args);
var jwtOptions = builder.Configuration.GetSection("Jwt").Get<JwtOptions>();
Console.WriteLine("VALIDATION SECRET: " + jwtOptions.Secret);
builder.Services.AddApplicationServices(builder.Configuration);
Console.WriteLine(builder.Environment.EnvironmentName);
var app = builder.Build();

app.ConfigurePipeLine().GetAwaiter().GetResult();    

app.Run();

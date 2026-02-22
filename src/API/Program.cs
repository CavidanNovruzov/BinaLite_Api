using API.Extensions;
using Application.Options;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices(builder.Configuration);
Console.WriteLine(builder.Environment.EnvironmentName);
var app = builder.Build();

app.ConfigurePipeLine().GetAwaiter().GetResult();    

app.Run();

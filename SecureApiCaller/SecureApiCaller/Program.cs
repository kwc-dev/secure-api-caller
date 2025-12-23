using Azure.Identity;
using Microsoft.AspNetCore.Mvc;
using SecureApiCaller;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddAzureKeyVault(
    new Uri(builder.Configuration["KeyVaultUrl"]!),
    new DefaultAzureCredential()
);

builder.Services.AddTransient<AuthHeaderHandler>();
builder.Services.AddHttpClient<WebhookApiClient>()
    .AddHttpMessageHandler<AuthHeaderHandler>();

var app = builder.Build();

app.MapGet("/call-webhook", async ([FromServices] WebhookApiClient client) =>
{
    var result = await client.SendTestRequestAsync();
    return Results.Ok(result);
});

app.Run();


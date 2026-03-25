using BLAZOR_WASM_CLEAN_ARQUITECTURE.Server.Data.DbContext;
using BLAZOR_WASM_CLEAN_ARQUITECTURE.Server.Data.Helpers;
using BLAZOR_WASM_CLEAN_ARQUITECTURE.Server.Data.Helpers.StronglyTypedIds;
using BLAZOR_WASM_CLEAN_ARQUITECTURE.Server.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add NSwag services
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddOpenApiDocument();

// Add Endpoints API Explorer
builder.Services.AddEndpointsApiExplorer();

// Configure app cookie
//
// The default values, which are appropriate for hosting the Backend and
// BlazorWasmAuth apps on the same domain, are Lax and SameAsRequest. 
// For more information on these settings, see:
// https://learn.microsoft.com/aspnet/core/blazor/security/webassembly/standalone-with-identity#cross-domain-hosting-same-site-configuration
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.SameSite = SameSiteMode.Lax;
    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
});

// Configure authorization
builder.Services.AddAuthorizationBuilder();

// Add the database (in memory for the sample)
builder.Services
    .AddDbContext<ApplicationDbContext>(options =>
    {
        options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]);
        //options.UseInMemoryDatabase("AppDb");
        //For debugging only: options.EnableDetailedErrors(true);
        //For debugging only: options.EnableSensitiveDataLogging(true);
        //options.ReplaceService<IValueConverterSelector, StrongIdValueConverterSelector>();
    });

// Add identity and opt-in to endpoints
builder.Services
    //.AddIdentityCore<ApplicationUser>()
    //.AddRoles<ApplicationRole>()
    .AddIdentity<ApplicationUser, ApplicationRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders()
    .AddSignInManager()
    .AddApiEndpoints();

// Add converters for strongly typed ids
builder.Services
     .AddStrongIds(typeof(Program).Assembly) // TypeConverters
     .ConfigureHttpJsonOptions(options =>
     {
         options.SerializerOptions.AddStrongIdSupport(); // JSON
     });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Seed the database
    await using var scope = app.Services.CreateAsyncScope();
    await SeedData.InitializeAsync(scope.ServiceProvider);

    app.MapOpenApi();
    app.UseSwaggerUi();
}

app.UseRouting();

app.MapIdentityApi<ApplicationUser>();
app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

// Provide an endpoint to clear the cookie for logout
// For more information on the logout endpoint and antiforgery, see:
// https://learn.microsoft.com/aspnet/core/blazor/security/webassembly/standalone-with-identity#antiforgery-support
app.MapPost("/logout", async (SignInManager<ApplicationUser> signInManager, [FromBody] object empty) =>
{
    if (empty is not null)
    {
        await signInManager.SignOutAsync();

        return Results.Ok();
    }

    return Results.Unauthorized();
})
.RequireAuthorization();

// Provide an endpoint for user roles
app.MapGet("/roles", (ClaimsPrincipal user) =>
{
    if (user.Identity is not null && user.Identity.IsAuthenticated)
    {
        var identity = (ClaimsIdentity)user.Identity;
        var roles = identity.FindAll(identity.RoleClaimType)
            .Select(c =>
                new
                {
                    c.Issuer,
                    c.OriginalIssuer,
                    c.Type,
                    c.Value,
                    c.ValueType
                });

        return TypedResults.Json(roles);
    }

    return Results.Unauthorized();
})
.RequireAuthorization();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.MapFallbackToFile("index.html");

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

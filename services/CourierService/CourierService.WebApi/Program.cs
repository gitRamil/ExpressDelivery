using CourierService.WebApi.Infrastructure.Middleware;
using CourierService.WebApi.Infrastructure.Security;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.HttpOverrides;
using Serilog;

try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.AddDefaultSerilog();

    // Add services to the container.
    builder.Services.AddDefaultControllers();
    builder.Services.AddDefaultAuthentication();
    builder.Services.AddDefaultApiVersioning();
    builder.Services.AddDefaultSwagger();
    builder.Services.AddDefaultMediatr();
    builder.Services.AddDefaultFluentValidation();
    builder.Services.AddDefaultEfCore();
    builder.Services.AddDefaultCorsPolicy();
    builder.Services.AddDefaultProblemDetails();
    builder.Services.AddDefaultAutoMapper();
    builder.Services.AddDefaultHealthChecks();
    var app = builder.Build();
    app.AddAutomaticMigrations();
    app.UseSerilogRequestLogging();

    app.UseForwardedHeaders(new ForwardedHeadersOptions
    {
        ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
    });
    app.UseProblemDetails();
    app.UseHttpsRedirection();
    app.UseRouting();
    app.UseCors();
    app.UseAuthentication();
    app.UseAuthorization();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.MapCustomHealthChecks("/health");

    app.MapHealthChecksUI()
       .RequireAuthorization(AuthorizationPolicies.CookiesPolicy);
    app.MapControllers();
    app.Run();
    return 0;
}
catch (HostAbortedException)
{
    throw;
}
catch (Exception e)
{
    Log.Fatal(e, "Хост неожиданно прекратил работу");
    return 1;
}
finally
{
    Log.CloseAndFlush();
}

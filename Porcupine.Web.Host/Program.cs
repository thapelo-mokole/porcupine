using FluentValidation;
using FluentValidation.AspNetCore;
using Porcupine.EntityFrameworkCore.EntityFrameworkCore;
using Porcupine.Web.Host.Filters;
using Porcupine.Web.Host.Middleware;
using Porcupine.Web.Host;
using Porcupine.Application.Common;
using Porcupine.EntityFrameworkCore;
using Porcupine.Application;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(
    config => config.Filters.Add(typeof(ValidateModelAttribute))
);

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining(typeof(IValidationsMarker));

builder.Services.AddSwagger();

builder.Services.AddDataAccess(builder.Configuration)
    .AddApplication(builder.Environment);

//builder.Services.AddJwt(builder.Configuration);

var app = builder.Build();

using var scope = app.Services.CreateScope();

await AutomatedMigration.MigrateAsync(scope.ServiceProvider);

app.UseSwagger();
app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Porcupine V1"); });

app.UseHttpsRedirection();

app.UseCors(corsPolicyBuilder =>
    corsPolicyBuilder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
);

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<PerformanceMiddleware>();

app.UseMiddleware<TransactionMiddleware>();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();

namespace Porcupine.Web.Host
{
    public partial class Program
    { }
}
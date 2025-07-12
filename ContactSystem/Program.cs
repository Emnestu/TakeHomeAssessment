using Asp.Versioning;
using ContactSystem.Application.Entities;
using ContactSystem.Application.Repositories.Interfaces;
using ContactSystem.Application.Services;
using ContactSystem.Application.Services.Interfaces;
using ContactSystem.Infrastructure;
using ContactSystem.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace ContactSystem.API;

internal class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(
                policy =>
                {
                    policy
                        .WithOrigins(builder.Configuration.GetSection("AllowedHosts").Get<string>()!)
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
        });


        builder.Services.AddScoped<IContactsRepository, ContactsRepository>();
        builder.Services.AddScoped<IOfficesRepository, OfficesRepository>();
        builder.Services.AddScoped<IContactsService, ContactsService>();
        builder.Services.AddScoped<IOfficesService, OfficesService>();


        builder.Services.AddDbContext<GraniteDataContext>(options =>
            options.UseInMemoryDatabase("InMemoryDatabase"));


        builder.Services.AddResponseCompression(options => { options.EnableForHttps = true; });

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddHttpContextAccessor();

        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Granite Home Api"
            });

            options.TagActionsBy(api =>
            {
                if (api.GroupName != null) return new[] { api.GroupName };

                if (api.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
                    return new[] { controllerActionDescriptor.ControllerName };

                throw new InvalidOperationException("Unable to determine tag for endpoint.");
            });

            options.DocInclusionPredicate((_, _) => true);
        });

        builder.Services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            })
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

        builder.Services.AddHealthChecks();

        var app = builder.Build();

        using (var serviceScope = app.Services.CreateScope())
        {
            var dbContext = serviceScope.ServiceProvider.GetRequiredService<GraniteDataContext>();

            // In real world do a proper migration, but here's the test data

            dbContext.Offices.Add(new OfficeEntity
            {
                Id = new Guid("ff0c022e-1aff-4ad8-2231-08db0378ac98"),
                Name = "Default office"
            });

            dbContext.Contacts.Add(new ContactEntity
            {
                Id = new Guid("c00b9ff3-b1b6-42fe-8b5a-4c28408fb64a"),
                FirstName = "Alejandro",
                LastName = "Alfonso",
                Email = "aalfonso@gmail.com",
                ContactOffices = new List<ContactOfficeRelation>
                {
                    new()
                    {
                        ContactId = new Guid("c00b9ff3-b1b6-42fe-8b5a-4c28408fb64a"),
                        OfficeId = new Guid("ff0c022e-1aff-4ad8-2231-08db0378ac98"),
                    }
                }
            });
            dbContext.Contacts.Add(
                new ContactEntity
                {
                    Id = new Guid("1ec2d3f7-8aa8-4bf5-91b8-045378919049"),
                    FirstName = "Mark",
                    LastName = "Costello",
                    Email = "mcostello@gmail.com"
                });

            dbContext.SaveChanges();
        }


        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseCors();

        app.UseResponseCompression();

        app.MapControllers();

        app.Run();
    }
}
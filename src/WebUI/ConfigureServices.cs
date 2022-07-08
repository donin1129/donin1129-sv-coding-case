using SvCodingCase.Application.Common.Interfaces;
using SvCodingCase.Infrastructure.Persistence;
using SvCodingCase.WebUI.Filters;
using SvCodingCase.WebUI.Services;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using NSwag;
using NSwag.Generation.Processors.Security;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddWebUIServices(this IServiceCollection services)
    {
        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddSingleton<ICurrentUserService, CurrentUserService>();

        services.AddHttpContextAccessor();

        services.AddHealthChecks()
            .AddDbContextCheck<ApplicationDbContext>();

        services.AddControllersWithViews(options =>
            options.Filters.Add<ApiExceptionFilterAttribute>())
                .AddFluentValidation(x => x.AutomaticValidationEnabled = false);

        services.AddRazorPages();

        /*
        services.AddSwaggerGen(configure =>
        {
            OAuth2Config oAuth2Config = applicationConfig.OAuth2;
            configure.SwaggerDoc(_apiVersion, new OpenApiInfo
            {
                Title = _openApiTitle,
                Version = _apiVersion,
                Contact = new OpenApiContact
                {
                    Name = "PreciPoint GmbH",
                    Email = "johannes.mueller@precipoint.de"
                },
                Description = "Web API to handle task management."
            });

            new List<string>
                {
                    typeof(Startup).GetTypeInfo().Assembly.GetName().Name,
                    typeof(MetaResponse).GetTypeInfo().Assembly.GetName().Name,
                    typeof(TaskDto).GetTypeInfo().Assembly.GetName().Name
                }.ForEach(documentationFile =>
                    configure.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{documentationFile}.xml")));

            const string securitySchemeId = "oauth2";
            configure.AddSecurityDefinition(securitySchemeId, new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    Implicit = new OpenApiOAuthFlow
                    {
                        Scopes = new Dictionary<string, string>
                            {
                                {"swagger", "Interact via the swagger browser UI."}
                            },
                        AuthorizationUrl = new Uri(oAuth2Config.AuthorizationUrl, UriKind.Absolute),
                        TokenUrl = new Uri(oAuth2Config.TokenUrl, UriKind.Absolute)
                    }
                }
            });

            configure.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = securitySchemeId
                            }
                        },
                        Enumerable.Empty<string>().ToList()
                    }
                });
            // Use method name as operation id to make open api typescript generator happy.
            configure.CustomOperationIds(apiDescription =>
                apiDescription.TryGetMethodInfo(out var methodInfo) ? methodInfo.Name : null);
            configure.SchemaFilter<HttpStatusCodeSchemaFilter>();

            // These following schemas are necessary in order to correctly auto-generate the python client. 
            configure.SchemaFilter<RemoveReadonlyPropertyOfDiscriminatedType>();
            configure.SchemaFilter<UseAllOfToExtendReferencedProperty>();

            configure.SchemaFilter<PolymorphismSchemaFilter<ITaskInputDto>>();
            configure.SchemaFilter<PolymorphismSchemaFilter<ITaskResultDto>>();
            configure.SchemaFilter<PolymorphismSchemaFilter<IShape>>();
        });
        */

        // Customise default API behaviour
        services.Configure<ApiBehaviorOptions>(options =>
            options.SuppressModelStateInvalidFilter = true);

        services.AddOpenApiDocument(configure =>
        {
            configure.Title = "SvCodingCase API";
            configure.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme
            {
                Type = OpenApiSecuritySchemeType.ApiKey,
                Name = "Authorization",
                In = OpenApiSecurityApiKeyLocation.Header,
                Description = "Type into the textbox: Bearer {your JWT token}."
            });

            configure.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
        });

        return services;
    }
}

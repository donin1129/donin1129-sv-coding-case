using SvCodingCase.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddWebUIServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();

    // Initialise and seed database
    using (var scope = app.Services.CreateScope())
    {
        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();
        await initialiser.InitialiseAsync();
        await initialiser.FillDataAsync();
    }
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHealthChecks("/health");
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseOpenApi();
// app.UseSwaggerUi3();

app.UseSwaggerUi3(settings =>
{
    settings.Path = "/api";
    settings.DocumentPath = "/api/swagger_specification_new.json";
});

/*applicationBuilder.UseSwagger(configure =>
{
    configure.RouteTemplate = _apiDocumentName + "/{documentName}/" + taskManagementJson;
    configure.PreSerializeFilters.Add((swaggerOptions, httpRequest) =>
    {
        var openApiServer = httpRequest.ExtractOpenApiServer();
        swaggerOptions.Servers = new List<OpenApiServer>
                    {
                        openApiServer
                    };
    });
});
applicationBuilder.UseSwaggerUI(configure =>
{
    configure.RoutePrefix = _apiDocumentName;
    configure.SwaggerEndpoint($"/{_apiDocumentName}/{_apiVersion}/{taskManagementJson}", _openApiTitle);
    configure.OAuthClientId(applicationConfig.OAuth2.Swagger.ClientId);
    configure.OAuthClientSecret(applicationConfig.OAuth2.Swagger.ClientSecret);
    configure.OAuthRealm(applicationConfig.OAuth2.Realm);
    configure.OAuthAppName(applicationConfig.OAuth2.Jwt.ClientId);
    configure.OAuthUseBasicAuthenticationWithAccessCodeGrant();
});*/

app.UseRouting();

app.UseAuthentication();
app.UseIdentityServer();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapRazorPages();

app.MapFallbackToFile("index.html"); ;

app.Run();

// Make the implicit Program class public so test projects can access it
public partial class Program { }
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using Microsoft.Net.Http.Headers;
using WebMarkupMin.AspNet.Common.Compressors;
using WebMarkupMin.AspNetCore5;
using reCAPTCHA.AspNetCore;
using Serilog;
using BoilerPlate.Utils;
using BoilerPlate.Bootstrapper;
using BoilerPlate.Data;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
var services = builder.Services;
var configuration = builder.Configuration;
IWebHostEnvironment hostEnvironment = builder.Environment;
ApplicationSettings applicationSettings;
ConnectionStrings connectionStrings;
MarketConfiguration marketConfiguration;

services.AddControllersWithViews();
services.AddConfigurationServices(configuration, hostEnvironment, out applicationSettings, out connectionStrings);
services.AddMarkets(applicationSettings, out marketConfiguration);
services.AddBusinessServices();
services.AddRepositoryServices(connectionStrings);
services.AddLogging(connectionStrings);
services.AddHangfire(connectionStrings);
services.AddCors();
services.AddDataProtection()
 .SetApplicationName("boilerplate")
 .PersistKeysToDbContext<ApplicationDataContext>();

services.AddSwaggerGen();
services.AddResponseCaching();
services.AddMemoryCache();
services.AddHsts(options =>
{
    options.Preload = true;
    options.IncludeSubDomains = true;
    options.MaxAge = TimeSpan.FromSeconds(31536000); // 1 Year
});

services.AddHttpsRedirection((options) =>
{
    options.HttpsPort = 443;
    options.RedirectStatusCode = StatusCodes.Status301MovedPermanently;
});

services.AddRecaptcha(configuration.GetSection("RecaptchaSettings"));
services.AddCulture(applicationSettings, marketConfiguration);

services.AddWebMarkupMin(options =>
{
    options.AllowMinificationInDevelopmentEnvironment = true;
    options.AllowCompressionInDevelopmentEnvironment = true;
    options.DisablePoweredByHttpHeaders = true;
})
    .AddHttpCompression(options =>
    {
        options.CompressorFactories = new List<ICompressorFactory>
        {
                        new BuiltInBrotliCompressorFactory(new BuiltInBrotliCompressionSettings
                        {
                            Level = CompressionLevel.Optimal
                        }),
                        new DeflateCompressorFactory(new DeflateCompressionSettings
                        {
                            Level = CompressionLevel.Optimal
                        }),
                        new GZipCompressorFactory(new GZipCompressionSettings
                        {
                            Level = CompressionLevel.Optimal
                        })
        };
    });

services.AddAntiforgery(options =>
{
    // Set Cookie properties using CookieBuilder properties.
    options.FormFieldName = "AntiforgeryFieldname";
    options.HeaderName = "X-CSRF-TOKEN-HEADERNAME";
    options.SuppressXFrameOptionsHeader = false;
});

builder.Host.UseSerilog();
var app = builder.Build();
app.UseSecurityHeaders(policies =>
               policies
                   .AddDefaultSecurityHeaders()
                   .RemoveServerHeader()
                   .RemoveCustomHeader("X-Powered-By")
                   .RemoveCustomHeader("Server")
           );

if (hostEnvironment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/error");
    app.UseHsts();
}
app.UseSerilogRequestLogging();

app.UseResponseCaching();
app.UseHttpsRedirection();

app.UseCors(x => x
           .AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader());

app.UseStaticFiles(new StaticFileOptions
{
    ServeUnknownFileTypes = true,
    DefaultContentType = "text/plain",
    OnPrepareResponse = ctx =>
    {
        const int durationInSeconds = 60 * 60 * 1;
        ctx.Context.Response.Headers[HeaderNames.CacheControl] =
            "public,max-age=" + durationInSeconds;
    }
});

app.UseRouting();
//app.UseMiddleware<ResponseMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.UseRequestLocalization(app.Services.GetService<IOptions<RequestLocalizationOptions>>().Value);
app.ConfigureSwagger(applicationSettings);
app.ConfigureHangfire();
app.UseWebMarkupMin();
app.ConfigureEndpoints(applicationSettings);

app.UseSerilogRequestLogging();
RegisterSeedUsers.Initialize();

app.Run();
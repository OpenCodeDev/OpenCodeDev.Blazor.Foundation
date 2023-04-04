using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Options;
using OpenCodeDev.Blazor.Foundation;
using System.Net;

string currentDir = Directory.GetCurrentDirectory();
WebApplicationBuilder builder = WebApplication.CreateBuilder();
builder.WebHost.UseKestrel().UseIISIntegration();
builder.WebHost.UseUrls("http://*:5000");
//builder.WebHost.UseSetting("https_port", "5000");
builder.Services.AddRazorPages();
builder.Services.AddLocalization();
builder.Services.AddScoped(sp => new HttpClient { });
builder.Services.AddBlazorFoundationServices(false);


var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
ForwardedHeadersOptions forwardOptions = new ForwardedHeadersOptions();
forwardOptions.AllowedHosts.Concat(new string[] { "bf.opencodedev.com", "bf-doc.opencodedev.com", "*.opencodedev.com" });
forwardOptions.KnownProxies.Add(IPAddress.Parse("10.0.0.100"));
forwardOptions.ForwardedHeaders = ForwardedHeaders.All;
app.UseForwardedHeaders(forwardOptions);
app.UseExceptionHandler("/Error");

app.UseHttpLogging();
app.UseStaticFiles();
app.UseBlazorFrameworkFiles();
//app.UseHttpsRedirection();
//app.UseStaticWebAssets();
//app.UseDirectoryBrowser();
app.UseRouting();
app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");
app.Run();

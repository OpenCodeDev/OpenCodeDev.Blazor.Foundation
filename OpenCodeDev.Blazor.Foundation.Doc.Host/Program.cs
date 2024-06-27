using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using OpenCodeDev.Blazor.Foundation;
using OpenCodeDev.Blazor.Foundation.Doc.Host;
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
builder.Services.AddMarkdownSystem();
builder.Services.AddCors();
builder.Services.AddHttpLogging(logging => {
    
    logging.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.ResponseBody;
});
builder.Services.AddResponseCaching(p=>p.MaximumBodySize = 0);
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
ForwardedHeadersOptions forwardOptions = new ForwardedHeadersOptions();
JObject ServerConfig = JObject.Parse(File.ReadAllText($"{currentDir}/server_settings.json"));

forwardOptions.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
app.UseForwardedHeaders(forwardOptions);
app.UseExceptionHandler("/Error");
app.UseCors(p => {
    p.AllowAnyOrigin();
});
//app.UseHttpLogging();
app.UseStaticFiles();
app.UseBlazorFrameworkFiles();
app.UseResponseCaching();
//app.UseHttpsRedirection();
//app.UseStaticWebAssets();
//app.UseDirectoryBrowser();
app.UseRouting();
app.MapRazorPages();
app.MapControllers();
app.UseMiddleware<DebugMiddleware>();
app.MapFallbackToPage("/_Host");
app.Run();

using AxiomSysPortal.Conventions;
using AxiomSysPortal.Pages.Localization;
using AxiomSysPortal.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Localization.Routing;
using Microsoft.Extensions.Options;
using System.Globalization;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages(o =>
{
    // put {culture} in front of every page route
    o.Conventions.Add(new CulturePrefixConvention());
}).AddJsonOptions(o => o.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase); ;
var expirationMinutes = int.Parse(builder.Configuration["jwt:expiresInMin"] ?? "60");
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(o =>
    {
        o.LoginPath = "/Account/Login";
        o.AccessDeniedPath = "/Account/Login";
        o.ExpireTimeSpan = TimeSpan.FromMinutes(expirationMinutes);   // same as jwt:expiresInMin
        o.SlidingExpiration = false;
        o.Events.OnRedirectToLogin = ctx =>
        {
            var culture = ctx.Request.RouteValues["culture"]?.ToString() ?? "en";
            var returnUrl = Uri.EscapeDataString(ctx.Request.Path + ctx.Request.QueryString);
            ctx.Response.Redirect($"/{culture}/Account/Login?ReturnUrl={returnUrl}");
            return Task.CompletedTask;
        };

        o.Events.OnRedirectToAccessDenied = ctx =>
        {
            var culture = ctx.Request.RouteValues["culture"]?.ToString() ?? "en";
            ctx.Response.Redirect($"/{culture}/Account/Login");
            return Task.CompletedTask;
        };
    });
builder.Services.Configure<RequestLocalizationOptions>(o =>
{
    var cultures = new[] { new CultureInfo("en"), new CultureInfo("ar") };
    o.DefaultRequestCulture = new RequestCulture("en");
    o.SupportedCultures = cultures;
    o.SupportedUICultures = cultures;
    o.RequestCultureProviders.Insert(0, new RouteDataRequestCultureProvider());

});

builder.Services.AddSingleton<ISharedLocalizer, SharedLocalizer>();

var apiBase = new Uri("https://localhost:7087/");
builder.Services.AddHttpClient<AuthApiService>(c => c.BaseAddress = apiBase);
builder.Services.AddHttpClient<SaDeliveryTermService>(c => c.BaseAddress = apiBase).AddHttpMessageHandler<BearerTokenHandler>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<BearerTokenHandler>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.UseRequestLocalization(
    app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);
app.MapRazorPages();

app.Run();

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;

namespace SampleWebApp
{
    public class Program
    {
        public const string NaiAuthScheme = "NovelAiAuthScheme";

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddSession();
            builder.Services.AddMemoryCache();
            // Add services to the container.
            builder.Services.AddRazorPages(options =>
            {
            });

            builder.Services.AddAuthentication(NaiAuthScheme)
                .AddCookie(NaiAuthScheme, options => {
                    options.AccessDeniedPath = "/Account/Denied";
                    options.LoginPath = "/Account/Login";
                    options.LogoutPath = "/Account/Logout";
                });

            builder.Services.AddSingleton<IConfigureOptions<CookieAuthenticationOptions>, ConfigureNaiAuth>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}

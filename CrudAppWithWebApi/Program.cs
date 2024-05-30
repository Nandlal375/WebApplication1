using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CrudAppWithWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            // Add session services
            builder.Services.AddDistributedMemoryCache(); // Adds a default in-memory implementation of IDistributedCache
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(20); // Set session timeout
                options.Cookie.HttpOnly = true; // Makes the session cookie HttpOnly
                options.Cookie.IsEssential = true; // Makes the session cookie essential
            });

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                //options.LoginPath = "/Home/Login";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStatusCodePagesWithRedirects("/Error/{0}");
            app.UseStaticFiles();
            app.UseRouting();
            app.UseSession();
            

            app.Use(async (context, next) =>
            {
                var sessionValue = context.Session.GetString("username");

                // Check if the session is null and the request is not for the Login page
                if (sessionValue == null && !context.Request.Path.Value.Contains("/Login"))
                {
                    // Allow access to the Registration page without session
                    if (context.Request.Path.Value.Contains("/Registration"))
                    {
                        // Do not redirect and allow the request to proceed to the Registration page
                        await next.Invoke();
                        return;
                    }

                    // Redirect to the Login page if accessing any other page without session
                    context.Response.Redirect("/Login");
                    return;
                }

                // Proceed to the next middleware/component if session exists or accessing Login page
                await next.Invoke();
            });
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Login}/{action=Index}/{id?}");

            app.Run();
        }
    }
}

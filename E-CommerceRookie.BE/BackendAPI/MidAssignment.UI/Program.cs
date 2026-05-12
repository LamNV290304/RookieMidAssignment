using MidAssignment.UI.Services;
using Microsoft.Extensions.Options;

namespace MidAssignment.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiSettings"));
            builder.Services.AddHttpClient<IProductApiService, ProductApiService>((sp, client) =>
            {
                var apiSettings = sp.GetRequiredService<IOptions<ApiSettings>>().Value;
                client.BaseAddress = new Uri(apiSettings.BaseUrl);
            });
            builder.Services.AddHttpClient<ICategoryApiService, CategoryApiService>((sp, client) =>
            {
                var apiSettings = sp.GetRequiredService<IOptions<ApiSettings>>().Value;
                client.BaseAddress = new Uri(apiSettings.BaseUrl);
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Products}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}

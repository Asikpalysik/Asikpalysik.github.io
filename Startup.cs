using Portfolio.Models;
using Portfolio.Services;
using System.Text.Json;
namespace Portfolio
{
    public class Startup(IConfiguration configuration)
    {
        public IConfiguration Configuration { get; } = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddHttpClient();
            services.AddControllers();
            services.AddTransient<JsonFileProductService>();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapBlazorHub();

                endpoints.MapGet("/products", async context =>
                {
                    var jsonFileProductService = app.ApplicationServices.GetService<JsonFileProductService>();

                    if (jsonFileProductService != null)
                    {
                        var products = jsonFileProductService.GetProducts();
                        var json = JsonSerializer.Serialize<IEnumerable<ProjectModel>>(products);
                        await context.Response.WriteAsync(json);
                    }
                    else
                    {
                        await context.Response.WriteAsync("Error: JsonFileProductService is not registered.");
                    }
                });
            });

        }
    }
}
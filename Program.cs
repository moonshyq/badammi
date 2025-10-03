using BadammiSite.Services;
using BadammiSite.Services.Base;

namespace BadammiSite;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        if (!Directory.Exists("Resourse"))
        {
            Directory.CreateDirectory("Resourse");
            Directory.CreateDirectory("Resourse/Photo");
            Directory.CreateDirectory("Resourse/Json");
        }
        else if (!File.Exists("Resourse/Json/Fruits.json"))
        {
            File.Create("Resourse/Json/Fruits.json");
        }

        builder.Services.AddRazorPages();

        builder.Services.AddScoped<IJsonService, JsonService>();

        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseExceptionHandler("/Error");
        
        app.UseHsts();

        app.UseRouting();
        app.UseAuthorization();
        app.UseStaticFiles();
        app.MapStaticAssets();
        app.MapRazorPages()
           .WithStaticAssets();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}

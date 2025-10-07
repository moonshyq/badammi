using BadammiAPI.Services.Base;
using BadammiAPI.Services;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();

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
builder.Services.AddScoped<IJsonService, JsonService>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}


app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "Resourse", "Photo")),
    RequestPath = "/photos"
});
app.UseRouting();
app.UseHttpsRedirection();
app.MapControllers();
app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseCors("AllowAll");
app.Run();

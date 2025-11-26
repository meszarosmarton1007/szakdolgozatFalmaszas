using ClimbingApplication.Context;
using ClimbingApplication.Service;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<EFContextcs>();

//Cookie alapú autentikáció
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.AccessDeniedPath = "/Account/Accesdenied";
    });

//Firebase útvonalak bégetésének megszüntetése
var firebaseConfig = builder.Configuration.GetSection("Firebase");
string seviceAccountPath = firebaseConfig["ServiceAccountPath"];
string storageBucket = firebaseConfig["StorageBucket"];

FirebaseApp.Create(new AppOptions()
{
    Credential = GoogleCredential.FromFile(seviceAccountPath)
});

//Képek törlése a controllereknél
builder.Services.AddScoped<IImageService, FirebaseImageService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

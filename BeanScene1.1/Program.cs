using BeanScene1._1.Models;
using BeanScene1._1.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Mail;
using BeanScene1._1.Services;
using Microsoft.AspNetCore.Identity.UI.Services;
using BeanScene1._1.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddTransient<IEmailSender, SmtpEmailSender>();

builder.Services.AddIdentity<AppUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders()
    .AddDefaultUI();
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services =  scope.ServiceProvider;
    await BeanScene1._1.Data.SeedData.InitializeAsync(services);
    var userManager = services.GetRequiredService<UserManager<AppUser>>();
    var adminUser = await userManager.FindByEmailAsync("samusenkodenis14@gmail.com");
    if (adminUser != null)
    {
        var roles = await userManager.GetRolesAsync(adminUser);

        if (!roles.Contains("Admin"))
        {
            var result = await userManager.AddToRoleAsync(adminUser, "Admin");

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine(error.Description);
                }
            }
        }
    }
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();
app.MapHub<ChatHub>("/chathub");

app.MapRazorPages();


app.Run();

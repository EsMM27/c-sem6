using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RP1.DataAccess;
using RP1.DataAccess.Repository;
using RP1.Services;
using RP1_L00148202.Pages.PageViewModels;
using Stripe;


public class Program {
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorPages();
        // needs package entityframeworkcore.sqlserver
        //builder.Services.AddDbContext<AppDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddDbContext<AppDBContext>(options =>
            options.UseSqlServer(
                builder.Configuration.GetConnectionString("DefaultConnection"),
                sqlServerOptions => sqlServerOptions.MigrationsAssembly("RP1_L00148202") // Specify migrations assembly
            ));

        builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppDBContext>();

        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));

        builder.Services.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = "/Login";
            options.AccessDeniedPath = "/AccessDenied";
        });

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

        app.UseRouting();
        string key = builder.Configuration.GetSection("Stripe:SecretKey").Get<string>();
        StripeConfiguration.ApiKey = key;
        await app.CreateRolesAsync(builder.Configuration);
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapRazorPages();



        app.Run();
    }
}


public static class WebApplicationExtensions
{
    public static async Task<WebApplication> CreateRolesAsync(this WebApplication app, IConfiguration configuration)
    { 
        using var scope = app.Services.CreateScope();
        var roleManager = (RoleManager<IdentityRole>)scope.ServiceProvider.GetService(typeof(RoleManager<IdentityRole>));
        var roles = configuration.GetSection("Roles").Get<List<string>>();

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
        return app;

    }
}
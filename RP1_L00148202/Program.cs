using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RP1.DataAccess;
using RP1.DataAccess.Repository;
using RP1.Services;


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
        await app.CreateRolesAsync(builder.Configuration);
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapRazorPages();

        //using (var scope = app.Services.CreateScope())
        //{
        //    var db = scope.ServiceProvider.GetRequiredService<AppDBContext>();

        //    db.Categories.Add(new Category { Name = "test1" });
        //    db.Categories.Add(new Category { Name = "test2" });
        //    db.Categories.Add(new Category { Name = "test3" });
        //    db.Categories.Add(new Category { Name = "test4" });
        //    db.SaveChanges();
        //}
        //user 24eb65f0-e63a-4125-b29d-d596e7506d03
        //role d14c5dd0-4ad8-4d1e-a6d6-5415af3faaad

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
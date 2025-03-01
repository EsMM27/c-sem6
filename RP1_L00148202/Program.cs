using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RP1.DataAccess;
using RP1.DataAccess.Repository;
using RP1.Services;

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

app.Run();

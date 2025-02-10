using BlazorEFIdentity.Components;
using BlazorEFIdentity.Components.Account;
using BlazorEFIdentity.Data;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlazorEFIdentity
{
    public class Program
    {
        public static async Task Main(string[] args) //change from void to async
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents();

            builder.Services.AddCascadingAuthenticationState();
            builder.Services.AddScoped<IdentityUserAccessor>();
            builder.Services.AddScoped<IdentityRedirectManager>();
            builder.Services.AddScoped<AuthenticationStateProvider, ServerAuthenticationStateProvider>();

            builder.Services.AddAuthorization();
            builder.Services.AddAuthentication(options =>
                {
                    options.DefaultScheme = IdentityConstants.ApplicationScheme;
                    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
                })
                .AddIdentityCookies();

            //var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            //builder.Services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseSqlServer(connectionString));

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite("Data Source=sysdb.db"));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>() //to add roles
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddSignInManager()
                .AddDefaultTokenProviders();

            builder.Services.AddScoped<UserManager<ApplicationUser>>();


            builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>();

            // Add additional endpoints required by the Identity /Account Razor components.
            app.MapAdditionalIdentityEndpoints();

            using (var scope = app.Services.CreateScope())
            {
                var roleManager = 
                    scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var roles = new[] { "Admin", "User" }; //array of roles
                foreach (var role in roles)
                {
                    if(!await roleManager.RoleExistsAsync(role)) //en funktioin true elle falsk
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }
            } 
            using (var scope = app.Services.CreateScope())
            {
                var userManager = 
                    scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                string email = "admin@admin.se";
                string password = "Abc123!";

                if(await userManager.FindByEmailAsync(email) == null)
                {
                    var user = new ApplicationUser();
                    user.UserName = email;
                    user.Email = email;

                    await userManager.CreateAsync(user, password); //hash password
                    await userManager.AddToRoleAsync(user, "Admin");

                }
            }

            app.Run();
        }
    }
}

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using MyProject.ToDo.Business.Concrete;
using MyProject.ToDo.Business.Interfaces;
using MyProject.ToDo.DataAccess.Concrete.EntitiyFrameworkCore.Contexts;
using MyProject.ToDo.DataAccess.Concrete.EntitiyFrameworkCore.Repositories;
using MyProject.ToDo.DataAccess.Interfaces;
using MyProject.ToDo.Entities.Concrete;

namespace MyProject.ToDo.Web
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IGorevService, GorevManager>();  // IGorevService i g�rd���n zaman GorevManagerin �rne�ini f�rlat dedik.              // services.AddScoped -> iste�i ger�ekle�tiren mesela session da ilgili interface in sadece 1 tane �rne�i al�n�r.  
            services.AddScoped<IAciliyetService, AciliyetManager>();                                                //AddTransient->Her istekte yeni bir nesne �retilir.AddSingleton->iste�in nerden gelmi� kimden gelmi� bir �nemi yok hep tek bir nesne �rne�i �retir
            services.AddScoped<IRaporService, RaporManager>();
            services.AddScoped<IAppUserService, AppUserManager>();
            services.AddScoped<IDosyaService, DosyaManager>();
            services.AddScoped<IBildirimService, BildirimManager>();
            

            services.AddScoped<IGorevDal, EfGorevRepository>();  // IGorevDal � g�r���n zaman EfGorevRepository i �rnekle
            services.AddScoped<IAciliyetDal, EfAcilliyetRepository>();
            services.AddScoped<IRaporDal, EfRaporRepository>();
            services.AddScoped<IAppUserDal, EfAppUserRepository>();
            services.AddScoped<IBildirimDal, EfBildirimRepository>();
            

            services.AddDbContext<TodoContext>();   // de�i�iklerin kaydedilmesi i�in Service mize ToDoContext i ekledik.
            services.AddIdentity<AppUser, AppRole>(opt => {
                opt.Password.RequireDigit = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequiredLength = 1;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireNonAlphanumeric = false;
            })
                .AddEntityFrameworkStores<TodoContext>();
            services.AddControllersWithViews();

            services.ConfigureApplicationCookie(opt =>
            {
                opt.Cookie.Name = "IsTakipCookie";
                opt.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict; // bu cookienin ba�ka web sayfalar�yla payla��lmas�n� istemiyoruz
                opt.Cookie.HttpOnly = true; // ilgili kullan�c�ya document.cookie yi kullanarak bu cookie a ula�amas�n
                opt.ExpireTimeSpan = TimeSpan.FromDays(20); // ya�am s�resi
                opt.Cookie.SecurePolicy = Microsoft.AspNetCore.Http.CookieSecurePolicy.SameAsRequest;  // istek ne ise o �ekilde davran http ise http || https ise https
                opt.LoginPath = "/Home/Index";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager) // seed data i�in gereken usermanager ve role manageri ekledik
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            IdentityInitializer.SeedData(userManager, roleManager).Wait();  // metod senkronik ama SeedData Asenkronik oldu�undan dolay� sonunda .Wait ekledik
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area}/{controller=Home}/{action=Index}/{id?}"
                    );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                    );
            });
        }
    }
}

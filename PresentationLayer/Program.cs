using DataAccessLayer.Contracts;
using DataAccessLayer.Data;
using DataAccessLayer.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace PresentationLayer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // إضافة خدمات DbContext
            builder.Services.AddDbContext<MasaqDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // إضافة خدمات الـ MVC
            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<IDataSeeding, DataSeeding>();

            var app = builder.Build();

            #region Data Seeding
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<MasaqDbContext>();
                    context.Database.EnsureCreated();
                    var seedingService = services.GetRequiredService<IDataSeeding>();

                    seedingService.AddFirstYearData(); // إضافة بيانات الصف الأول الثانوي
                    seedingService.AddSecondYearData(); // إضافة بيانات الصف الثاني الثانوي
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error at adding data :) {ex.Message}");
                }
            }
            #endregion

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
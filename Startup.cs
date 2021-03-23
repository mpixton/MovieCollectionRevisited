using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MovieCollectionRevisited.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCollectionRevisited
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            // Add SqLite Db to the project.
            services.AddDbContext<RevisitedDbContext>(options => { 
                options.UseSqlite(Configuration["ConnectionStrings:RevisitedConnection"]);
            });

            // Register UnitOfWork for Dependency Injection with Controllers.
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if(env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                // New pattern for all movies
                endpoints.MapControllerRoute(
                    name: "allMovies",
                    pattern: "AllMovies",
                    new { Controller = "Home", action = "FilmList" }
                    );

                // New pattern for add movie
                endpoints.MapControllerRoute(
                    name: "addMovie",
                    pattern: "AddMovie",
                    new { Controller = "Home", action = "AddMovie" }
                    );

                // New pattern for edit movie
                endpoints.MapControllerRoute(
                    name: "editMovie",
                    pattern: "EditMovie/{MovieID?}",
                    new { Controller = "Home", action = "EditMovie" }
                    );

                // New pattern for delete movie
                endpoints.MapControllerRoute(
                    name: "deleteMovie",
                    pattern: "DeleteMovie/{MovieID}",
                    new { Controller = "Home", action = "DeleteMovie" }
                    );

                // New pattern for podcasts
                endpoints.MapControllerRoute(
                    name: "podcasts",
                    pattern: "Podcast",
                    new { Controller = "Home", action = "Podcast" }
                    );

                // New pattern for home
                endpoints.MapControllerRoute(
                    name: "home",
                    pattern: "Home",
                    new { Controller = "Home", action = "Home" });

                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}

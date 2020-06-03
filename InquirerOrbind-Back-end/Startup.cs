using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InquirerOrbind_Back_end.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace InquirerOrbind_Back_end {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddControllers();

            services.AddDbContext<ApplicationDbContext>(options =>
              options.UseSqlServer(
                  Configuration.GetConnectionString("DefaultConnection")));

            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            app.UseCors(builder =>
           builder.WithOrigins("https://dev1myprojects24.xyz/", "https://dev1myprojects24.xyz", "http://127.0.0.1:5500/index.html", "http://127.0.0.1:5500/", "http://127.0.0.1:5500")
         .AllowAnyHeader()
         .AllowAnyMethod()
         //.WithHeaders("X-Header1", "X-Header1")
         //.WithMethods("GET", "POST", "PUT", "DELETE")
         );

            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}

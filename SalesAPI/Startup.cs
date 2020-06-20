using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SalesAPI.Data;
using SalesAPI.Repository;

namespace SalesAPI
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
            services.AddScoped<ISalesRepository, SalesRepository>();
            
            services.AddControllers();
            services.AddDbContext<SalesDBContext>(option=>option.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog = SalesDB;"));
            //Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = master; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",
                  new Microsoft.OpenApi.Models.OpenApiInfo
                  {
                    Title = "Swagger demo SalesAPI",
                    Description = "Sales API demo using swagger",
                    Version="v1"
                  });
            });
            services.AddDistributedRedisCache(option =>
            {
                option.Configuration = "SalesRCache.redis.cache.windows.net:6380,password=srYvoPN1eFkrowjX+yHD2h443sPRFaP3oT5Toqgz7dU=,ssl=True,abortConnect=False";
                //option.Configuration = "localhost:6379";
                option.InstanceName = "SalesRCache";
            });
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            });
        }
                
        

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, SalesDBContext salesDBContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            salesDBContext.Database.EnsureCreated();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json","Swagger Sales API");
                options.RoutePrefix = "";
            });
        }
    }
}

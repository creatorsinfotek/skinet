using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using AutoMapper;
using API.Helpers;
using API.Middleware;
using API.Extensions;

namespace API
{
    public class Startup
    {
        private readonly IConfiguration _config;
        public Startup(IConfiguration config)
        {
            _config= config;            
        }

       

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
             //Moved to Extension Class

            // services.AddScoped<IProductRepository, ProductRepository>();
            // services.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));
            services.AddAutoMapper(typeof(MappingProfiles));
            services.AddControllers();
            services.AddDbContext<StoreContext>(x =>
            {
                x.UseSqlite(_config.GetConnectionString("DefaultConnection"));
            });

            //Moved to Extension Class

            // services.Configure<ApiBehaviorOptions>(options =>  {
            //     options.InvalidModelStateResponseFactory = actionContext =>
            //     {
            //         var errors = actionContext.ModelState
            //         .Where(w => w.Value.Errors.Count > 0 )
            //         .SelectMany( s => s.Value.Errors)
            //         .Select( x => x.ErrorMessage).ToArray();

            //         var errorResponse = new ApiValidationErrorResponse 
            //         {
            //             Errors = errors
            //         };

            //         return new BadRequestObjectResult(errorResponse);
            //     };
            // });

            services.AddSwaggerDocumentation();

            services.AddCors(opt => 
            {
                opt.AddPolicy("CorsPolicy", policy => {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
                });
            });

            services.AddApplicationServices();

            // services.AddSwaggerGen(c => 
            // {
            //     c.SwaggerDoc("v1", new OpenApiInfo { Title = "SkinNet API", Version ="v1"});
            // });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // if (env.IsDevelopment())
            // {
            //     app.UseDeveloperExceptionPage();
            // }

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseStaticFiles();

            app.UseCors("CorsPolicy");

            app.UseAuthorization();

            app.UseSwaggerDocumentation();

           

            // app.UseSwagger();
            // app.UseSwaggerUI(c => {c.SwaggerEndpoint("/swagger/v1/swagger.json", "Skinet API v1");});

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

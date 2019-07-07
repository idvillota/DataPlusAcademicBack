using DataPlus.Contracts.Logger;
using DataPlus.Contracts.Repositories;
using DataPlus.Contracts.Services;
using DataPlus.Entities;
using DataPlus.Repository;
using DataPlus.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace DataPlus.Academic.Extensions
{
    public static class ServiceExtensions
    {

        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });
        }

        public static void ConfigureIISIntegration(this IServiceCollection services)
        {
            services.Configure<IISOptions>(options => { });
        }

        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerService>();
        }

        public static void ConfigureSQLContext(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config["ConnectionStrings:ConnectionString"];
            services.AddDbContext<RepositoryContext>(o => o.UseSqlServer(connectionString));
        }
 
        public static void ConfigureRepositoryWrapper(this IServiceCollection services)
        {
            services.AddScoped<IWrapperRepository, WrapperRepository>();
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "MyAPI",
                    Description = ""
                });
                c.DescribeAllEnumsAsStrings();
                c.DescribeStringEnumsInCamelCase();

            });
        }

        public static void ConfigureDependencies(this IServiceCollection services)
        {
            services.AddTransient<IStudentRepository, StudentRepository>();
            services.AddTransient<IStudentService, StudentService>();
            services.AddTransient<ITeacherRepository, TeacherRepository>();
            services.AddTransient<ITeacherService, TeacherService>();
            services.AddTransient<ISignatureRepository, SignatureRepository>();
            services.AddTransient<ISignatureService, SignatureService>();
            services.AddTransient<ICourseRepository, CourseRepository>();
            services.AddTransient<ICourseService, CourseService>();

            services.AddTransient<IWrapperRepository, WrapperRepository>();
        }

    }
}

using Domain.Interfaces;
using Infrastructure.Dapper;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySqlConnector;
using System.Data;
using System.Reflection;
using FluentValidation;
using Application.Contacts.Commands.Validations;
using Microsoft.AspNetCore.Identity;


namespace CrossCutting.AppDependencies
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services, IConfiguration configuration)
        {

            var Connection = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AppDbContext>(options =>options.UseMySql(Connection, ServerVersion.AutoDetect(Connection)));

            services.AddSingleton<IDbConnection>(provider =>
            {
                var connectionDapper = new MySqlConnection(Connection);
                connectionDapper.Open();
                return connectionDapper;
            });


            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<IContactDapperRepository, ContactDapperRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserDapperRepository, UserDapperRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            var handlers = AppDomain.CurrentDomain.Load("Application");
            services.AddMediatR(m =>
            {
                m.RegisterServicesFromAssemblies(handlers);
                m.AddOpenBehavior(typeof(ValidationBehaviour<,>));
            });
            

            services.AddValidatorsFromAssembly(Assembly.Load("Application"));
            services.AddScoped<IPasswordHasher<Domain.Entities.User>, PasswordHasher<Domain.Entities.User>>();


            return services;    
        }

    }
}

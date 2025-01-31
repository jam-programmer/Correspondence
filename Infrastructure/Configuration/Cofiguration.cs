

using Application.Contract;
using Infrastructure.Configuration.Context;
using Infrastructure.Implement;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Configuration;



public static class Cofiguration
{
    public static IServiceCollection Infrastructure
          (this IServiceCollection service, IConfiguration configuration)
    {
        service.AddDbContext<SqlServerContext>(option =>
        {
            option.UseSqlServer(configuration.
                GetConnectionString("SqlServerConnectionString"),
                sqlServerOptionsAction: sqlConfig =>
                {
                    sqlConfig.EnableRetryOnFailure(

                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null
                        );
                });

        });
       



        service.AddScoped<IContext, ContextProvider>();
        service.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        service.AddScoped<IApiService, ApiService>();

        return service;
    }
}
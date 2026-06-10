using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MiApp.Application.Interfaces;
using MiApp.Domain.Interfaces;
using MiApp.Infrastructure.Persistence;
using MiApp.Infrastructure.Persistence.Repositories; // Asegúrate que aquí estén tus clases
using MiApp.Infrastructure.Services;

namespace MiApp.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Usamos el nombre ApplicationDbContext que definimos en el archivo de Persistencia
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

        // Registro de repositorios que implementan las interfaces del Dominio
        services.AddScoped<IEventRepository, EventRepository>();
        services.AddScoped<ITicketZoneRepository, TicketZoneRepository>();
        services.AddScoped<ITicketPurchaseRepository, TicketPurchaseRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IJwtTokenService, JwtTokenService>();
        services.AddScoped<IPasswordHasher, BcryptPasswordHasher>();

        return services;
    }
}
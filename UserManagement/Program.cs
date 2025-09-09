using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using UserManagement.DataAccess;
using UserManagement.Interfaces;
using UserManagement.Services;

namespace UserManagement;

public class Program
{
    static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
        builder.Services.AddControllers()
            .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IGroupService, GroupService>();
        builder.Services.AddScoped<IDataAccess, DataAccess.DataAccess>();
        builder.Logging.AddConsole();

        var app = builder.Build();

        app.MapControllers();

        app.Run();
    }

}
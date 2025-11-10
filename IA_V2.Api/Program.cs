using FluentValidation;
using IA_V2.Core.Interfaces;
using IA_V2.Core.Services;
using IA_V2.Infrastructure.Data;
using IA_V2.Infrastructure.Filters;
using IA_V2.Infrastructure.Repositories;
using IA_V2.Infrastructure.Validators;
using Microsoft.EntityFrameworkCore;

public class Program
{
    public static void Main (string[] args)

    {
        var builder = WebApplication.CreateBuilder(args);

        // Configurar User Secrets solo en Desarrollo
        if (builder.Environment.IsDevelopment())
        {
            builder.Configuration.AddUserSecrets<Program>();
        }
        #region Configurar la BD SqlServer
        var connectionString = builder.Configuration.GetConnectionString("ConnectionSqlServer");
        builder.Services.AddDbContext<InteligenciaArtificialV2Context>(options => options.UseSqlServer(connectionString));
        #endregion

        //Dapper
        builder.Services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();
        builder.Services.AddScoped<IDapperContext, DapperContext>();

        //Automapper
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        #region Repositorios y servicios
        builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<ITextService, TextService>();
        builder.Services.AddScoped<IPredictionService, PredictionService>();
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        #endregion

        //Validaciones
        builder.Services.AddScoped<IValidationService, ValidationService>();
        builder.Services.AddValidatorsFromAssemblyContaining<UserDTOValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<TextDTOValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<PredictionDTOValidator>();

        //ML
        builder.Services.AddSingleton<ModeloIAService>();
        // Add services to the container.

        builder.Services.AddControllers(options =>
        {
            options.Filters.Add<GlobalExceptionFilter>();
        });
        // Configurar Swagger
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v2", new()
            {
                Title = "Backend IA Text Analysis API",
                Version = "v2",
                Description = "API para análisis de textos usando IA",
                Contact = new()
                {
                    Name = "Equipo de Desarrollo",
                    Email = "liam.lopez@ucb.edu.bo"
                }
            });

            var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);

            options.EnableAnnotations();
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v2/swagger.json", "Backend IA Text Analysis API v2");
                options.RoutePrefix = string.Empty;
            });
        }
        app.UseHttpsRedirection();
    
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
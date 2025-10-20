using FluentValidation;
using IA_V2.Core.Interfaces;
using IA_V2.Core.Services;
using IA_V2.Infrastructure.Data;
using IA_V2.Infrastructure.Repositories;
using IA_V2.Infrastructure.Validators;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

#region Configurar la BD SqlServer
var connectionString = builder.Configuration.GetConnectionString("ConnectionSqlServer");
builder.Services.AddDbContext<InteligenciaArtificialV2Context>(options => options.UseSqlServer(connectionString));
#endregion

//Automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

#region Repositorios
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITextService, TextService>();
builder.Services.AddScoped<IPredictionService, PredictionService>();
#endregion

//Validaciones
builder.Services.AddScoped<IValidationService, ValidationService>();
builder.Services.AddValidatorsFromAssemblyContaining<UserDTOValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<TextDTOValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<PredictionDTOValidator>();

//ML
builder.Services.AddSingleton<ModeloIAService>();
// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

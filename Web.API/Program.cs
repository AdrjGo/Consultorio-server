using System.Text;
using Application.Services;
using Domain.Interfaces;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// -------------------- Configuración de servicios --------------------
// Base de datos
var connectionString = builder.Configuration.GetConnectionString("PostgreSQLConnection");
builder.Services.AddDbContext<DBContext>(options =>
{
    options.UseNpgsql(connectionString);
});

var config = builder.Configuration;

builder.Services.AddScoped<DbContext>(provider => provider.GetRequiredService<DBContext>());


// Repositories
builder.Services.AddScoped<IPersonRepository, PersonRespository>();
builder.Services.AddScoped<IUserRepository, UserRespository>();
builder.Services.AddScoped<IClinicRepository, ClinicRespository>();
builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRespository>();
builder.Services.AddScoped<ISubmoduleRepository, SubmoduleRespository>();
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IMonitoringRepository, MonitoringRespository>();
builder.Services.AddScoped<IEvidenceFileRepository, EvidenceFileRepository>();
builder.Services.AddScoped<IPatientRepository, PatientRespository>();
builder.Services.AddScoped<IContractRepository, ContractRespository>();
builder.Services.AddScoped<IClinicHistoryRepository, ClinicHistoryRespository>();
builder.Services.AddScoped<IGeneralHistoryRepository, GeneralHistoryRespository>();
builder.Services.AddScoped<IPretreatmentExamRepository, PretreatmentExamRespository>();
builder.Services.AddScoped<ITreatmentProgressRepository, TreatmentProgressRespository>();
builder.Services.AddScoped<IFormVersionRepository, FormVersionRespository>();
builder.Services.AddScoped<ITreatmentSumaryRepository, TreatmentSumaryRespository>();
builder.Services.AddScoped<IFormResRepository, FormResRespository>();

// Services
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<ClinicService>();

builder.Services.AddControllers();

// Configuración de FluentValidation
builder.Services
    .AddFluentValidationAutoValidation()
    .AddFluentValidationClientsideAdapters()
    .AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());

// Swagger
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(config["JWT:Key"])
            ),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = config["JWT:Issuer"],
            ValidAudience = config["JWT:Audience"],
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddEndpointsApiExplorer();
// Swagger
builder.Services.AddAuthorization();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Web API", Version = "v1" });

    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Por favor ingresa el token con el prefijo 'Bearer '",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

// -------------------- App pipeline --------------------
var app = builder.Build();

// Middleware
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Web API V1");
});

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

// Mapear controllers
app.MapControllers();

app.Run();

using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var connectionString = builder.Configuration.GetConnectionString("PostgreSQLConnection");
builder.Services.AddDbContext<DBContext>(options =>
{
    options.UseNpgsql(connectionString);
});

// repositories
builder.Services.AddScoped<IPersonRepository, PersonRespository>();
builder.Services.AddScoped<IClinicRepository, ClinicRespository>();
builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRespository>();
builder.Services.AddScoped<ISubmoduleRepository, SubmoduleRespository>();
builder.Services.AddScoped<IUserRepository, UserRespository>();
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

var app = builder.Build();

app.UseHttpsRedirection();


app.Run();


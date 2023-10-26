using APIClient.Data;
using APIClient.Interfaces;
using APIClient.Repository;
using DataNoSQL.DAL;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<AvaliacaoDALNoSQL>();
builder.Services.AddScoped<EmpresaDALNoSQL>();
builder.Services.AddScoped<HistoricoPontoRecargaDALNoSQL>();
builder.Services.AddScoped<PontoRecargaDALNoSQL>();
builder.Services.AddScoped<UsuariosDALNoSQL>();

builder.Services.AddScoped<IEmpresaRepository, EmpresaRepository>();
builder.Services.AddScoped<IPontoRecargaRepository, PontoRecargaRepository>();
builder.Services.AddScoped<IQRCodeRepository, QRCodeRepository>();
builder.Services.AddScoped<IHistoricoRepository, HistoricoRepository>();
builder.Services.AddScoped<IAvaliacaoRepository, AvaliacaoRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>(); /* sempre que um IUsuarioRepository for necessário, cria um UsuarioRepository() e passa para ele */
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins("http://localhost:5173", "http://localhost:4200")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors();

app.MapControllers();

app.Run();

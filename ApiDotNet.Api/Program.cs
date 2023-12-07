using ApiDotNet.Api.Authentication;
using ApiDotNet.Domain.Authentication;
using ApiDotNet.Infra.IoC;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Api DotNet",
        Version = "v1",
        Description = "Criando uma Api em DotNet Core"
    });
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = @"Autenticação em JWT. \r\n\r\n
                        Ex: Bearer {token}",
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

builder.Services.AddInfrastructure(builder.Configuration); //infra
builder.Services.AddServices(builder.Configuration); //serviço
builder.Services.AddHttpContextAccessor(); //injetar dependência
builder.Services.AddScoped<ICurrentUser, CurrentUser>(); //referenciar

//remove os nulos do nosso retorno
builder.Services.AddMvc().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

//implementar token
var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("projetoDotNetCoreprojetoDotNetCoreprojetoDotNetCore"));
builder.Services.AddAuthentication(authOptions =>
{
    authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; //padrão para inicialização da autenticação
    authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer("Bearer", options => //tipo de autenticação que vai usar
{
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true, //vamos autenticar pela nossa chave que geramos (key)
        ValidateLifetime = true, //tempo de vida do token
        IssuerSigningKey = key, // nossa chave
        ValidateAudience = false, //não iremos validar
        ValidateIssuer = false
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// chamar aqui para usar autenticação
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

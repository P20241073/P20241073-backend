using System.Text;
using Activities.Domain.Repositories;
using Activities.Domain.Services;
using Activities.Persistence;
using Activities.Services;
using MachineLearning.Domain.Services;
using MachineLearning.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Reports.Domain.Repositories;
using Reports.Domain.Services;
using Reports.Persistence;
using Reports.Services;
using Security.Domain.Services.Communication;
using Shared.Domain.Repositories;
using Shared.Mapping;
using Shared.Persistence.Context;
using Shared.Persistence.Repositories;
using Users.Domain.Model;
using Users.Domain.Repositories;
using Users.Domain.Services;
using Users.Persistence;
using Users.Services;
using Users.Services.EmailConfirmation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "bearer",
        Description = "Put Bearer + your token in the box below",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme,
        }
    };

    c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, jwtSecurityScheme);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            jwtSecurityScheme, Array.Empty<string>()
        }
    });
});

builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));
builder.Services.AddTransient<EmailService>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));


builder.Services.AddRouting(options => 
    options.LowercaseUrls = true);

builder.Services.AddCors();
builder.Services.AddIdentityCore<User>(opt => 
{
    opt.SignIn.RequireConfirmedEmail = true;
    opt.User.RequireUniqueEmail = true;
    opt.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
})
    .AddRoles<Role>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTSettings:TokenKey"])),
        };
    });
builder.Services.AddAuthorization();
builder.Services.AddScoped<TokenService>();

builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();
builder.Services.AddScoped<IDeviceService, DeviceService>();
builder.Services.AddScoped<IActivityRepository, ActivityRepository>();
builder.Services.AddScoped<IActivityService, ActivityService>();
builder.Services.AddScoped<ISasSvRepository, SasSvRepository>();
builder.Services.AddScoped<ISasSvService, SasSvService>();
builder.Services.AddScoped<IReportRepository, ReportRepository>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddSingleton<IBusinessLogicService, BusinessLogicService>();
builder.Services.AddHttpClient();
builder.Services.AddScoped<HttpClientService>();
builder.Services.AddLogging();


builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddAutoMapper(
    typeof(ModelToResourceProfile),
    typeof(ResourceToModelProfile));

var app = builder.Build();

// using (var scope = app.Services.CreateScope())
// using (var context = scope.ServiceProvider.GetService<AppDbContext>())
// {
//     context.Database.EnsureCreated();
// }

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    try
    {
        context.Database.EnsureCreated();
    }
    catch (Exception ex)
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred creating the DB.");
        throw;
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI( c => 
    {
        c.SwaggerEndpoint("v1/swagger.json", "v1");
        c.RoutePrefix = "swagger";
        c.ConfigObject.AdditionalItems.Add("persistAuthorization", "true");
    });
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod() 
    .AllowAnyHeader());;
app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

using API.WhoIsParking.Extensions;
using API.WhoIsParking.UserClaims;
using App.WhoIsParking;
using Domain.WhoIsParking.Models;
using Infrastructure.WhoIsParking;
using Infrastructure.WhoIsParking.Data.EntitiesConfig;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

// DB
builder.Services.AddDbContext<DataContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultPostgresConnection")));

// Auth
builder.Services.AddAuthorization();

// ASP.NET Identity
builder.Services.AddIdentityApiEndpoints<ApplicationUser>(options => 
    {
        options.SignIn.RequireConfirmedAccount = true;
        options.Lockout.MaxFailedAccessAttempts = 10; 
    })
    .AddRoles<ApplicationRole>()
    .AddEntityFrameworkStores<DataContext>();

//builder.Services.AddOptions<BearerTokenOptions>(IdentityConstants.BearerScheme).Configure(options =>
//{
//    options.BearerTokenExpiration = TimeSpan.FromSeconds(5);
//    options.RefreshTokenExpiration = TimeSpan.FromSeconds(5);
//});

// ClaimsPrincipalFactory
builder.Services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, ApplicationUserClaimsPrincipalFactory>();

// Inject Layers
builder.Services.AddInjectionApplication();
builder.Services.AddInjectionInfrastructure(builder.Configuration);

// DB Seeder for admin user
builder.Services.AddTransient<DatabaseSeeder>();

// Allow CORS for Angular dev
builder.Services.AddCors(options => 
    options.AddPolicy(name: "NgOrigins", 
    policy => 
        policy.WithOrigins("https://localhost:4200", "http://localhost:4200", "http://192.168.0.25:4200")
        .AllowAnyMethod().AllowAnyHeader()));

var app = builder.Build();

// DB migrations
app.ApplyMigrations();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseStaticFiles();
    app.UseSwaggerUI(c => c.InjectStylesheet("/swagger-ui/SwaggerDark.css"));

    app.UseCors("NgOrigins");
}

using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<DatabaseSeeder>();
    await seeder.SeedAsync().ConfigureAwait(false);
}

app.MapIdentityApi<ApplicationUser>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

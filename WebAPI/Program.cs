using Business;
using DBHelper;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Infrastructure.JWT;
using Domain.Common;
using Appliction.Interfaces.Student;
using Appliction.Interfaces;
using Appliction.Services;
using Infrasture;

var builder = WebApplication.CreateBuilder(args);

// **1️⃣ Load Configuration from appsettings.json**
//builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// **2️⃣ Retrieve Connection String**
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// **3️⃣ Register Services**
builder.Services.AddControllers();

// **4️⃣ Register SQLHelper & Other Services**
builder.Services.AddScoped<ISQLHelper>(provider => new SQLHelper(connectionString));
builder.Services.AddScoped<ILoginRepository, LoginRepository>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICourseRepository, CoursesRepository>();
builder.Services.AddScoped<ICourseService, CourseService>();

builder.Services.AddScoped<IRolesRepository, RolesRepository>();
builder.Services.AddScoped<IRolesService, RoleService>();
// Fix incorrect interface name and service name
//builder.Services.AddScoped<IStateRepository, StateRepository>(); 
//builder.Services.AddScoped<IStateService, StateService>();      
builder.Services.AddSingleton<JWTToken>();

// **5️⃣ Configure JWT Authentication**
var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSettings>();
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));


if (jwtSettings == null)
{
    throw new InvalidOperationException("JWT settings are missing in configuration.");
}

if (string.IsNullOrWhiteSpace(jwtSettings.Key))
{
    throw new InvalidOperationException("JWT Key is missing or empty.");
}

// Ensure the JWT key is properly formatted (Base64 or UTF-8)
byte[] keyBytes;
try
{
    keyBytes = Convert.FromBase64String(jwtSettings.Key);
}
catch (FormatException)
{
    keyBytes = Encoding.UTF8.GetBytes(jwtSettings.Key); // Fallback if it's not Base64 encoded
}

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(keyBytes) // Properly formatted key
        };
    });
//Configuring Authorization
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
    options.AddPolicy("UserPolicy", policy => policy.RequireRole("Teacher"));
});

// **6️⃣ Configure API Versioning**
builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(2, 0);
    options.ReportApiVersions = true;
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
});

// **7️⃣ Configure CORS Policy**
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:5174")  // Allow frontend
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

// **8️⃣ Configure Swagger**
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "My API - V1", Version = "v1" });
    options.SwaggerDoc("v2", new OpenApiInfo { Title = "My API - V2", Version = "v2" });

    // Ensure correct API versioning in Swagger
    options.DocInclusionPredicate((version, apiDescription) =>
    {
        var model = apiDescription.ActionDescriptor.EndpointMetadata
            .OfType<ApiVersionAttribute>()
            .FirstOrDefault();

        return model == null ? version == "v1" : model.Versions.Any(v => $"v{v.MajorVersion}" == version);
    });
});

// **9️⃣ Build the Application**
var app = builder.Build();
app.UseCors("AllowReactApp");

// **🔟 Enable Middleware (CORS, Swagger, etc.)**
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
        c.SwaggerEndpoint("/swagger/v2/swagger.json", "API V2");
    });
}

// ✅ Enable HTTPS Redirection
app.UseHttpsRedirection();

// ✅ Ensure Authentication & Authorization Middleware
app.UseAuthentication();
app.UseAuthorization();

// ✅ Map Controllers
app.MapControllers();

// **🏁 Run the Application**
app.Run();

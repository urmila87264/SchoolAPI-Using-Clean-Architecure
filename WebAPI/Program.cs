using Appliction.Interfaces;
using Appliction.Services;
using Business;
using DBHelper;
using Infrasture;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Appliction.Interfaces.Student;

var builder = WebApplication.CreateBuilder(args);

// **1️⃣ Add Configuration to Read appsettings.json**
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

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
// **5️⃣ Configure API Versioning**
builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(2, 0);
    options.ReportApiVersions = true;
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
});

// **6️⃣ Configure CORS Policy**
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:5173")  // Allow frontend
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials(); // Allow cookies/auth tokens if needed
    });
});

// **7️⃣ Configure Swagger**
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

// **8️⃣ Build the Application**
var app = builder.Build();
app.UseCors("AllowReactApp");
// **9️⃣ Enable Middleware (CORS, Swagger, etc.)**
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
        c.SwaggerEndpoint("/swagger/v2/swagger.json", "API V2");
    });
}

// ✅ CORS must be enabled before routing


// ✅ Enable HTTPS Redirection
app.UseHttpsRedirection();

// ✅ Ensure Authentication & Authorization Middleware
app.UseAuthentication();
app.UseAuthorization();

// ✅ Map Controllers
app.MapControllers();

// **🔟 Run the Application**
app.Run();

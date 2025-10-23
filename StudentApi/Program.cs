using FastEndpoints;
using FastEndpoints.Swagger;
using StudentApi.Models;
using StudentApi.Services.Classe;
using StudentApi.Services.Enrollments;
using StudentApi.Services.Marks;
using StudentApi.Services.Students;

var builder = WebApplication.CreateBuilder(args);

// Register FastEndpoints services
builder.Services.AddFastEndpoints();

// Swagger for FastEndpoints
builder.Services.SwaggerDocument(o =>
{
    o.DocumentSettings = s =>
    {
        s.Title = "Student API";
        s.Version = "v1";
    };
});

// Register your services
builder.Services.AddSingleton<IStudentService, StudentService>();
builder.Services.AddSingleton<IClassService, ClassService>();
builder.Services.AddSingleton<IEnrollmentService, EnrollmentService>();
builder.Services.AddSingleton<IMarkService, MarkService>();

builder.Services.AddAuthorization();

var app = builder.Build();
app.ConfigureExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();     // generates swagger.json
    app.UseSwaggerUI();  // swagger UI
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseFastEndpoints();

app.Run();

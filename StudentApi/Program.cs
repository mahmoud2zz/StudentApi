using FastEndpoints;
using StudentApi.Models;
using StudentApi.Services.Classe;
using StudentApi.Services.Enrollments;
using StudentApi.Services.Marks;
using StudentApi.Services.Students;

var builder = WebApplication.CreateBuilder(args);

// Register FastEndpoints services
builder.Services.AddFastEndpoints();

// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register your services
builder.Services.AddSingleton<IStudentService, StudentService>();
builder.Services.AddSingleton<IClassService, ClassService>();
builder.Services.AddSingleton<IEnrollmentService, EnrollmentService>();
builder.Services.AddSingleton<IMarkService, MarkService>();


// Add authorization services
builder.Services.AddAuthorization(); // <-- This fixes the error

var app = builder.Build();
app.ConfigureExceptionHandler();


// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Authorization middleware
app.UseAuthorization();

// Map FastEndpoints
app.UseFastEndpoints();

app.Run();

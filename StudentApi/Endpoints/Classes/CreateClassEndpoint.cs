using FastEndpoints;
using StudentApi.Dtos;
using StudentApi.Helpers;
using StudentApi.Models;
using StudentApi.Services.Classe;
using StudentApi.Validators;

namespace StudentApi.Endpoints.Classes;

public class CreateClassEndpoint : Endpoint<ClassDto>
{
    private readonly IClassService _classService;

    public CreateClassEndpoint(IClassService classService)
    {
        _classService = classService;
    }

    public override void Configure()
    {
        Post("/api/classes");
        AllowAnonymous();
    }

    public override async Task HandleAsync(ClassDto req, CancellationToken ct)
    {
        var validator = new ClassValidator();
        var validationResult = validator.Validate(req);

        if (!validationResult.IsValid)
        {
            var errors = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
            await SendAsync(ResponseBuilder.Failure<Class>(errors), 400, ct);
            return;
        }

        var newClass = new Class
        {
            Name = req.Name,
            Teacher = req.Teacher,
            Description = req.Description
        };

        var result = _classService.AddClass(newClass);

        await SendAsync(ResponseBuilder.Success("Class created successfully", result), 200, ct);
    }
}

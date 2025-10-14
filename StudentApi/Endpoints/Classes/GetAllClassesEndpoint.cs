
using FastEndpoints;
using StudentApi.Dtos;
using StudentApi.Helpers;
using StudentApi.Services.Classe;

public class GetAllClassesEndpoint : Endpoint<GetAllClassesRequest>
{
    private readonly IClassService _classService;

    public GetAllClassesEndpoint(IClassService classService)
    {
        _classService = classService;
    }

    public override void Configure()
    {
        Get("/api/classes");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetAllClassesRequest req, CancellationToken ct)
    {
        var allClasses = _classService.GetAllClasses();

        
        if (!string.IsNullOrWhiteSpace(req.Name))
            allClasses = allClasses
                .Where(c => c.Name.Contains(req.Name, StringComparison.OrdinalIgnoreCase))
                .ToList();

        if (!string.IsNullOrWhiteSpace(req.Teacher))
            allClasses = allClasses
                .Where(c => c.Teacher.Contains(req.Teacher, StringComparison.OrdinalIgnoreCase))
                .ToList();

       
        var totalCount = allClasses.Count;
        var paged = allClasses
            .Skip((req.Page - 1) * req.PageSize)
            .Take(req.PageSize)
            .ToList();

        var response = new
        {
            TotalCount = totalCount,
            Data = paged
        };

        await SendAsync(ResponseBuilder.Success("Classes retrieved successfully", response), 200, ct);
    }
}
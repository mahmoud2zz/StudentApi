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

        var page = req.Page <= 0 ? 1 : req.Page;
        var pageSize = req.PageSize <= 0 ? 10 : req.PageSize;

        var totalCount = allClasses.Count;
        var paged = allClasses
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var response = new
        {
            TotalCount = totalCount,
            Data = paged
        };

        await SendAsync(ResponseBuilder.Success("Classes retrieved successfully", response), 200, ct);
    }
}

using FastEndpoints;
using StudentApi.Helpers;
using StudentApi.Models;
using StudentApi.Services.Classe;
using StudentApi.Services.Marks;

namespace StudentApi.Endpoints.Classes;

public class GetAverageMarksEndpoint : EndpointWithoutRequest
{
    private readonly IClassService _classService;
    private readonly IMarkService _markService;

    public GetAverageMarksEndpoint(IClassService classService, IMarkService markService)
    {
        _classService = classService;
        _markService = markService;
    }

    public override void Configure()
    {
        Get("/api/classes/{classId:int}/average-marks");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var classId = Route<int>("classId");

        var classObj = _classService.GetClassById(classId);
        if (classObj == null)
        {
            await SendAsync(ResponseBuilder.Failure<decimal>($"Class with id {classId} not found"), 404, ct);
            return;
        }

        var average = _markService.CalculateAverageMarksForClass(classId);
        if (average == null)
        {
            await SendAsync(ResponseBuilder.Failure<decimal>($"No marks recorded for class with id {classId}"), 404, ct);
            return;
        }

        await SendAsync(ResponseBuilder.Success("Average marks calculated successfully", average), 200, ct);
    }
}

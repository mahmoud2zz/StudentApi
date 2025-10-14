using FastEndpoints;
using StudentApi.Helpers;
using StudentApi.Models;
using StudentApi.Services.Classe;

namespace StudentApi.Endpoints.Classes;

public class DeleteClassEndpoint : EndpointWithoutRequest
{
    private readonly IClassService _classService;

    public DeleteClassEndpoint(IClassService classService)
    {
        _classService = classService;
    }

    public override void Configure()
    {
        Delete("/api/classes/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var id = Route<int>("id");

        var deleted = _classService.DeleteClass(id);

        if (!deleted)
        {
            await SendAsync(ResponseBuilder.Failure<Class>($"Class with id {id} not found."), 404, ct);
            return;
        }

        await SendAsync(ResponseBuilder.Success<Class>($"Class with id {id} deleted successfully."), 200, ct);
    }
}

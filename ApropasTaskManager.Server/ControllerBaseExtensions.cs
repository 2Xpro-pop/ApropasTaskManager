using System.Reactive;
using ApropasTaskManager.Shared;
using Microsoft.AspNetCore.Mvc;

namespace ApropasTaskManager.Server;

public static class ControllerBaseExtensions
{
    public static IActionResult OkOrBadRequest<T>(this ControllerBase controller, Result<T> result)
    {
        if (result)
        {
            return controller.Ok(result.Value);
        }

        return controller.BadRequest(result.Error);
    }

    public static IActionResult OkOrBadRequest(this ControllerBase controller, Result<Unit> result)
    {
        if (result)
        {
            return controller.Ok();
        }

        return controller.BadRequest(result.Error);
    }
}

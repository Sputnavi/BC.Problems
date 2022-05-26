using Microsoft.AspNetCore.Mvc;

namespace BC.Problems.Helpers.Extensions;

public static class ControllerExtensions
{
    public static void ValidateObject(this ControllerBase controller)
    {
        if (!controller.ModelState.IsValid)
        {
            throw new ArgumentException(string.Join(", ", controller.ModelState.Values.SelectMany(m => m.Errors).Select(e => e.ErrorMessage)));
        }
    }
}

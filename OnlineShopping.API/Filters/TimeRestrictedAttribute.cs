
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

public class TimeRestrictedAttribute : ActionFilterAttribute
{
    private readonly int _startHour;
    private readonly int _endHour;

    public TimeRestrictedAttribute(int startHour, int endHour)
    {
        _startHour = startHour;
        _endHour = endHour;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var hour = DateTime.Now.Hour;
        if (hour < _startHour || hour > _endHour)
        {
            context.Result = new ContentResult
            {
                Content = "This API is not available at this time.",
                StatusCode = 403
            };
        }
    }
}

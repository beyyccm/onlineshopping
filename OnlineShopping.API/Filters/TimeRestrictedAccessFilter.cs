// Filters/TimeRestrictedAccessFilter.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace OnlineShopping.API.Filters
{
    public class TimeRestrictedAccessFilter : IActionFilter
    {
        private readonly TimeSpan _startTime;
        private readonly TimeSpan _endTime;

        public TimeRestrictedAccessFilter(int startHour, int endHour)
        {
            _startTime = TimeSpan.FromHours(startHour);
            _endTime = TimeSpan.FromHours(endHour);
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var now = DateTime.Now.TimeOfDay;
            if (now < _startTime || now > _endTime)
            {
                context.Result = new ContentResult
                {
                    Content = "API is not accessible at this time.",
                    StatusCode = 403
                };
            }
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}

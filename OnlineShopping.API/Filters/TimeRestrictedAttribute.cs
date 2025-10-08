using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;


namespace OnlineShopping.API.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class TimeRestrictedAttribute : ActionFilterAttribute
    {
        private readonly int _startHour;
        private readonly int _endHour;


        public TimeRestrictedAttribute(int startHour = 9, int endHour = 17)
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
}
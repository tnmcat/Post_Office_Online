using Microsoft.AspNetCore.Mvc;
using PostOffice.API.DTOs.Common;

namespace PostOffice.Admin.Controllers.Components
{
    public class PagerViewComponent : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(PagedResultBase result)
        {
            return Task.FromResult((IViewComponentResult)View("Default", result));
        }
    }
}

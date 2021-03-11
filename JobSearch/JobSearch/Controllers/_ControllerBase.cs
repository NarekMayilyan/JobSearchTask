using JobSearch.WEB.Core;
using Microsoft.AspNetCore.Mvc;

namespace JobSearch.WEB.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ControllerBase : Controller
    {
        protected ApiContext ApiContext { get; private set; }

        protected ControllerBase()
        {
            ApiContext = new ApiContext(() => this);
        }
    }
}

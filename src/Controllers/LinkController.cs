using Microsoft.AspNetCore.Mvc;
using SafeLinks.Managers;
using SafeLinks.Models;

namespace SafeLinks.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LinkController : ControllerBase
    {
        private readonly ILinkManager _manager;

        public LinkController(ILinkManager manager)
        {
            _manager = manager;
        }

        [HttpGet("{url}")]
        public ActionResult<RedirectInfo> GetLinkLocation(string url)
        {
            return _manager.GetLinkLocation(url);
        }
    }
}

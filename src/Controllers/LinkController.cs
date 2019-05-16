using Microsoft.AspNetCore.Mvc;
using SafeLinks.Managers;

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
        public ActionResult<string> GetLinkLocation(string url)
        {
            var linkLocation = _manager.GetLinkLocation(url);

            if (linkLocation == null)
            {
                // Need to throw an exception in the manager when we know its not a valid uri
                // Have the exception filter handle sending back the 400
                // Because not every null result is a bad request
                return BadRequest();
            }

            return linkLocation;
        }
    }
}

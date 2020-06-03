using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SafeLinks.Managers;
using SafeLinks.Models;

namespace SafeLinks.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LinkController : ControllerBase
    {
        private readonly ILinkManager manager;

        public LinkController(ILinkManager manager)
        {
            this.manager = manager;
        }

        [HttpGet("{url}")]
        public async Task<ActionResult<LinkInfo>> GetLinkInfo(string url)
        {
            return await manager.GetLinkInfoAsync(url);
        }
    }
}

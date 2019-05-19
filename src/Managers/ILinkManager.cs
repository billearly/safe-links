using SafeLinks.Models;

namespace SafeLinks.Managers
{
    public interface ILinkManager
    {
        RedirectInfo GetLinkLocation(string url);
    }
}
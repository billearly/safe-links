using System.Threading.Tasks;
using SafeLinks.Models;

namespace SafeLinks.Managers
{
    public interface ILinkManager
    {
        Task<LinkInfo> GetLinkInfoAsync(string url);
    }
}
using System;
using System.Threading.Tasks;

namespace SafeLinks.Source
{
    public interface ILinkSource
    {
        Task<string> GetLinkLocationAsync(Uri uri);
    }
}
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SafeLinks.Source
{
    public interface ILinkSource
    {
        Task<HttpResponseMessage> GetLinkInfoAsync(Uri uri);
    }
}
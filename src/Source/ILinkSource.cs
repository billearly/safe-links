using System;

namespace SafeLinks.Source
{
    public interface ILinksSource
    {
        string GetLinkLocation(Uri uri);
    }
}
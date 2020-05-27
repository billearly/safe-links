using System;

namespace SafeLinks.Source
{
    public interface ILinkSource
    {
        string GetLinkLocation(Uri uri);
    }
}
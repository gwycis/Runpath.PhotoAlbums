using System;
using System.Threading.Tasks;

namespace Runpath.PhotoAlbums.Core.Infrastructure
{
    public interface IHttpClient
    {
        Task<string> GetStringAsync(Uri requestUri);
    }
}

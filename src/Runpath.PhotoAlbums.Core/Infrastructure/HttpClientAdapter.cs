using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Runpath.PhotoAlbums.Core.Infrastructure
{
    public sealed class HttpClientAdapter : IHttpClient
    {
        public async Task<string> GetStringAsync(Uri requestUri)
        {
            using (var client = new HttpClient())
            {
                return await client.GetStringAsync(requestUri);
            }
        }
    }
}
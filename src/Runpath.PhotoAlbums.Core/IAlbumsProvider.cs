using System.Collections.Generic;
using System.Threading.Tasks;
using Runpath.PhotoAlbums.Core.Domain;

namespace Runpath.PhotoAlbums.Core
{
    public interface IAlbumsProvider
    {
        Task<IReadOnlyList<Album>> GetAlbumsAsync();
    }
}
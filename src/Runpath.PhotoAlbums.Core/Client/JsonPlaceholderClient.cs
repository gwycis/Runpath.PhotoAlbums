using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Runpath.PhotoAlbums.Core.Domain;
using Runpath.PhotoAlbums.Core.Infrastructure;

namespace Runpath.PhotoAlbums.Core.Client
{
    public sealed class JsonPlaceholderClient : IAlbumsProvider
    {
        private const string AlbumsResource = "albums";
        private const string PhotosResource = "photos";

        private readonly IHttpClient _client;
        private readonly Uri _baseUrl;

        public JsonPlaceholderClient(IHttpClient client, Uri baseUrl)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _baseUrl = baseUrl ?? throw new ArgumentNullException(nameof(baseUrl));
        }

        public async Task<IReadOnlyList<Album>> GetAlbumsAsync()
        {
           var tasks = new List<Task<string>>
           {
               _client.GetStringAsync(new Uri(_baseUrl, new Uri(AlbumsResource, UriKind.Relative))),
               _client.GetStringAsync(new Uri(_baseUrl, new Uri(PhotosResource, UriKind.Relative)))
           };

            var results = await Task.WhenAll(tasks);

            var albumsResponse = results.Single(r => r.Contains("albumId") == false);
            var photosResponse = results.Single(r => r.Contains("albumId"));

            var albums = JsonConvert.DeserializeObject<List<AlbumDto>>(albumsResponse);
            var photos = JsonConvert.DeserializeObject<List<PhotoDto>>(photosResponse);

            return albums.Select(a =>
            {
                var albumPhotos = photos.Where(p => p.AlbumId == a.Id)
                    .Select(p => new Photo(p.Title, new Uri(p.Url), new Uri(p.ThumbnailUrl)));

                return new Album(a.Title, albumPhotos);
            }).ToList();
        }
    }
}

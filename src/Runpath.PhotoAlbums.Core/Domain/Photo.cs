using System;

namespace Runpath.PhotoAlbums.Core.Domain
{
    public sealed class Photo
    {
        public Photo(string title, Uri url, Uri thumbnailUrl)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(title));
            Title = title;
            Url = url ?? throw new ArgumentNullException(nameof(url));
            ThumbnailUrl = thumbnailUrl ?? throw new ArgumentNullException(nameof(thumbnailUrl));
        }

        public string Title { get; }
        public Uri Url { get; }
        public Uri ThumbnailUrl { get; }
    }
}

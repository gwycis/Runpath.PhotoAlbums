using System;
using System.Collections.Generic;

namespace Runpath.PhotoAlbums.Core.Domain
{
    public sealed class Album
    {
        public string Title { get; }
        public IEnumerable<Photo> Photos { get; }

        public Album(string title, IEnumerable<Photo> photos)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(title));
            Title = title;
            Photos = photos ?? throw new ArgumentNullException(nameof(photos));
        }
    }
}
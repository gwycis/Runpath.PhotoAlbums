using System;
using Runpath.PhotoAlbums.Core.Domain;

namespace Runpath.PhotoAlbums.WebApp.Models
{
    public sealed class PhotoViewModel
    {
        private readonly Photo _photo;

        public PhotoViewModel(string albumTitle, Photo photo)
        {
            if (string.IsNullOrWhiteSpace(albumTitle))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(albumTitle));
            _photo = photo ?? throw new ArgumentNullException(nameof(photo));
            Album = albumTitle;
        }

        public string Album { get; }
        public string Title => _photo.Title;
        public string Url => _photo.Url.ToString();
        public string ThumbnailUrl => _photo.ThumbnailUrl.ToString();
    }
}

using System;
using System.Threading.Tasks;
using NSubstitute;
using Runpath.PhotoAlbums.Core.Client;
using Runpath.PhotoAlbums.Core.Infrastructure;
using Xunit;

namespace Runpath.PhotoAlbums.Core.UnitTests
{
    public class JsonPlaceholderClientTest
    {
        [Fact]
        public async Task Given_AlbumsRequested_When_Retrieved_Then_MergeAlbumsAndPhotos_And_Return()
        {
            // Arrange
            const string albumsResponse = @"
                [
                    {
                        userId: 1,
                        id: 1,
                        title: ""Album 1""
                    },
                    {
                        userId: 1,
                        id: 2,
                        title: ""Album 2""
                    }
                ]";

            const string photosResponse = @"[
                {
                    albumId: 1,
                    id: 1,
                    title: ""Photo 1"",
                    url: ""https://via.placeholder.com/600/92c952"",
                    thumbnailUrl: ""https://via.placeholder.com/150/92c952""
                },
                {
                    albumId: 1,
                    id: 2,
                    title: ""Photo 2"",
                    url: ""https://via.placeholder.com/600/771796"",
                    thumbnailUrl: ""https://via.placeholder.com/150/771796""
                },
                {
                    albumId: 2,
                    id: 3,
                    title: ""Photo 3"",
                    url: ""https://via.placeholder.com/600/771796"",
                    thumbnailUrl: ""https://via.placeholder.com/150/771796""
                }
            ]";


            var client = Substitute.For<IHttpClient>();
            var sut = new JsonPlaceholderClient(client, new Uri("http://fake-api.com"));

            client.GetStringAsync(Arg.Is<Uri>(uri => uri.AbsoluteUri.EndsWith("albums"))).Returns(albumsResponse);
            client.GetStringAsync(Arg.Is<Uri>(uri => uri.AbsoluteUri.EndsWith("photos"))).Returns(photosResponse);

            // Act
            var result = await sut.GetAlbumsAsync();

            // Assert
            Assert.Collection(result, a =>
            {
                Assert.Equal("Album 1", a.Title);
                Assert.Collection(a.Photos, p =>
                {
                    Assert.Equal("Photo 1", p.Title);
                    Assert.Equal(new Uri("https://via.placeholder.com/600/92c952", UriKind.Absolute), p.Url);
                    Assert.Equal(new Uri("https://via.placeholder.com/150/92c952", UriKind.Absolute), p.ThumbnailUrl);
                }, p =>
                {
                    Assert.Equal("Photo 2", p.Title);
                    Assert.Equal(new Uri("https://via.placeholder.com/600/771796", UriKind.Absolute), p.Url);
                    Assert.Equal(new Uri("https://via.placeholder.com/150/771796", UriKind.Absolute), p.ThumbnailUrl);
                });
            }, a =>
            {
                Assert.Equal("Album 2", a.Title);
                Assert.Collection(a.Photos, p =>
                {
                    Assert.Equal("Photo 3", p.Title);
                    Assert.Equal(new Uri("https://via.placeholder.com/600/771796", UriKind.Absolute), p.Url);
                    Assert.Equal(new Uri("https://via.placeholder.com/150/771796", UriKind.Absolute), p.ThumbnailUrl);
                });
            });
        }
    }
}

using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Runpath.PhotoAlbums.Core;
using Runpath.PhotoAlbums.WebApp.Models;

namespace Runpath.PhotoAlbums.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private const int TotalItemsCount = 50;
        private readonly IAlbumsProvider _albumsProvider;

        public HomeController(IAlbumsProvider albumsProvider)
        {
            _albumsProvider = albumsProvider ?? throw new ArgumentNullException(nameof(albumsProvider));
        }

        public async Task<IActionResult> Index()
        {
            var albums = await _albumsProvider.GetAlbumsAsync();
            var photoVms = albums.Take(TotalItemsCount).Select(a => a.Photos.Select(p => new PhotoViewModel(a.Title, p))).SelectMany(vm => vm)
                .ToList();

            return View(photoVms);
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

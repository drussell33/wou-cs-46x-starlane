using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using iCollections.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using iCollections.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net.Http.Json;


namespace iCollections.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ICollectionsDbContext _collectionsDbContext;

        public HomeController(ILogger<HomeController> logger, UserManager<IdentityUser> userManager, ICollectionsDbContext collectionsDbContext)
        {
            _logger = logger;
            _userManager = userManager;
            _collectionsDbContext = collectionsDbContext;
        }

        public IActionResult Index()
        {

            return View();
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [Route("/ocean_environment")]
        public IActionResult Ocean_environment(int collectionID)
        {
            Collection newCollection = new Collection();
            newCollection = _collectionsDbContext.Collections.Where(m => m.Id == collectionID).Include(s => s.CollectionPhotoes).ThenInclude(x => x.Photo).FirstOrDefault();
            var collectionPhotos = newCollection.CollectionPhotoes.Where(m => m.CollectId == collectionID).ToList();
            var photos = _collectionsDbContext.Photos.Where(p => p.UserId == newCollection.UserId);

            List<RenderingPhoto> AllPhotos = new List<RenderingPhoto>();

            foreach (var image in collectionPhotos)
            {
                foreach (var photo in photos)
                {
                    if (image.PhotoId == photo.Id)
                    {
                        RenderingPhoto renderingPhoto = new RenderingPhoto(Convert.ToBase64String(photo.Data), photo.Name, image.PhotoRank, image.Description);
                        AllPhotos.Add(renderingPhoto);
                    }
                }
            }
            ViewData["collectionTitle"] = newCollection.Name;
            Console.WriteLine(AllPhotos.Count());
            return View(AllPhotos);
        }

        
        [Route("/gallery_environment")]
        public IActionResult gallery_environment(int collectionID)
        {
            Collection newCollection = new Collection();
            newCollection = _collectionsDbContext.Collections.Where(m => m.Id == collectionID).Include(s => s.CollectionPhotoes).ThenInclude(x => x.Photo).FirstOrDefault();
            var collectionPhotos = newCollection.CollectionPhotoes.Where(m => m.CollectId == collectionID).ToList();
            var photos = _collectionsDbContext.Photos.Where(p => p.UserId == newCollection.UserId);

            List<RenderingPhoto> AllPhotos = new List<RenderingPhoto>();
            
            foreach (var image in collectionPhotos)
            {
                foreach (var photo in photos)
                {
                    if (image.PhotoId == photo.Id)
                    {
                        RenderingPhoto renderingPhoto = new RenderingPhoto(Convert.ToBase64String(photo.Data), photo.Name, image.PhotoRank, image.Description);
                        AllPhotos.Add(renderingPhoto);
                    }
                }
            }
            ViewData["collectionTitle"] = newCollection.Name;
            Console.WriteLine(AllPhotos.Count());
            return View(AllPhotos);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}

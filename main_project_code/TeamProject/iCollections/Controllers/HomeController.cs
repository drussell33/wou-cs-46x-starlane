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
            /*Collection newCollection = new Collection();
            newCollection = _collectionsDbContext.Collections.Where(m => m.Id == collectionID).Include(s => s.CollectionPhotoes).ThenInclude(x => x.Photo).FirstOrDefault();
            var collectionPhotos = newCollection.CollectionPhotoes.Where(m => m.CollectId == collectionID).ToList();
            var photos = _collectionsDbContext.Photos.Where(p => p.UserId == newCollection.UserId);
            RendingTransfer rendingTransfer = new RendingTransfer();
            foreach (var image in collectionPhotos)
            {
                foreach (var photo in photos)
                {
                    if (image.PhotoId == photo.Id)
                    {
                        RenderingPhoto renderingPhoto = new RenderingPhoto(photo.Data, image.Title, image.PhotoRank, image.Description);
                        rendingTransfer.AddPhoto(renderingPhoto);
                        //var lookatme = photo.Data;
                        //var words = image.Title;
                        //var rank = image.PhotoRank;
                        //var longwords = image.Description;
                    }
                }
            }*/

            return View();
        }

        [Route("/gallery_environment")]
        public IActionResult gallery_environment(int collectionID)
        {
            Collection newCollection = new Collection();
            newCollection = _collectionsDbContext.Collections.Where(m => m.Id == collectionID).Include(s => s.CollectionPhotoes).ThenInclude(x => x.Photo).FirstOrDefault();
            var collectionPhotos = newCollection.CollectionPhotoes.Where(m => m.CollectId == collectionID).ToList();
            var photos = _collectionsDbContext.Photos.Where(p => p.UserId == newCollection.UserId);
            //RendingTransfer rendingTransfer = new RendingTransfer();

            List<RenderingPhoto> AllPhotos = new List<RenderingPhoto>();
            
            foreach (var image in collectionPhotos)
            {
                foreach (var photo in photos)
                {
                    if (image.PhotoId == photo.Id)
                    {
                        RenderingPhoto renderingPhoto = new RenderingPhoto(Convert.ToBase64String(photo.Data), photo.Name, image.PhotoRank, image.Description);
                        //rendingTransfer.AddPhoto(renderingPhoto);
                        AllPhotos.Add(renderingPhoto);
                    }
                }
            }
            //Console.WriteLine(AllPhotos);
            Console.WriteLine(AllPhotos.Count());
            //ICollectionDataTransferToJs(AllPhotos);
            return View(AllPhotos);
        }


        public IActionResult ICollectionDataTransferToJs(List<RenderingPhoto> AllPhotos)
        {
            /*if(AllPhotos == null)
            {
                
                return Json();
            }*/
            //Second Attempt -------------------------------------------
            var JSONresult = JsonConvert.SerializeObject(AllPhotos);
            return Content(JSONresult, "application/json");

            //return Json(JSONresult);







            //first attempt -----------------------------------------------
            //Array JsonData = new Array({ });
            //foreach (var photo in AllPhotos)
            //{
            //string imagedata = Convert.ToBase64String(photo.Data);
            //string title = photo.Title;
            //int rank = photo.Rank;
            //string description = photo.Description;
            //}

            //var json = JsonSerializer.Serialize(rendingTransfer);
            //string JSONresult;
            //var JSONresult = JsonConvert.SerializeObject(AllPhotos);
            //Console.Write(JSONresult);

            //return Json(new { ImageData = JSONresult });
            //return Json(AllPhotos);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // Users not logged in who try to upload photos will be redirected to the login page.
        [Authorize]
        public IActionResult PhotoUpload()
        {
            return View();
        }
    }
}

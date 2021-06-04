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
using iCollections.Data.Abstract;


namespace iCollections.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        //private readonly ICollectionsDbContext _collectionsDbContext;

        private readonly IIcollectionUserRepository _userRepo;
        private readonly IPhotoRepository _photoRepo;
        private readonly IcollectionRepository _colRepo;
        private readonly ICollectionPhotoRepository _collectionPhotoRepo;

        public HomeController(ILogger<HomeController> logger, UserManager<IdentityUser> userManager, /*ICollectionsDbContext collectionsDbContext,*/ IIcollectionUserRepository userRepo, IPhotoRepository photoRepo, IcollectionRepository colRepo, ICollectionPhotoRepository collectionphotoRepo)
        {
            _logger = logger;
            _userManager = userManager;
            //_collectionsDbContext = collectionsDbContext;
            _userRepo = userRepo;
            _photoRepo = photoRepo;
            _colRepo = colRepo;
            _collectionPhotoRepo = collectionphotoRepo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AboutUs()
        {
            return View();
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [Route("/ocean_environment")]
        public IActionResult ocean_environment(int? collectionID)
        {
            List<RenderingPhoto> AllPhotos = new List<RenderingPhoto>();
            
            if (collectionID == null)
            {
                ViewData["collectionTitle"] = "Example Ocean Environment";
                ViewData["collectionDescription"] = "This is the 360 Degree View environment with the Ocean Setting.";
                return View();
            }
            else
            {
                Collection newCollection = new Collection();
                //newCollection = _collectionsDbContext.Collections.Where(m => m.Id == collectionID).Include(s => s.CollectionPhotoes).ThenInclude(x => x.Photo).FirstOrDefault();
                newCollection = _colRepo.GetCollectionById((int)collectionID);

                if (newCollection != null)
                {
                    int collectionId = (int)collectionID;
                    int collectionOwnerId = (int)newCollection.UserId;
                    var collectionPhotos = _collectionPhotoRepo.GetAllCollectionPhotosbyCollectionId(collectionId);
                    //var photos = _collectionsDbContext.Photos.Where(p => p.UserId == newCollection.UserId);

                    var photos = _photoRepo.GetAllUserPhotos(collectionOwnerId);

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
                    ViewData["collectionDescription"] = newCollection.Description;
                    return View(AllPhotos);
                }


                return View(AllPhotos);
            }
            
            
            
        }

        
        [Route("/gallery_environment")]
        public IActionResult gallery_environment(int? collectionID)
        {
            if (collectionID == null)
            {
                ViewData["collectionTitle"] = "Example Gallery Environment";
                ViewData["collectionDescription"] = "This is the full virtual environment in an evening gallery setting";
                return View();
            }
            List<RenderingPhoto> AllPhotos = new List<RenderingPhoto>();
            Collection newCollection = new Collection();
            //newCollection = _collectionsDbContext.Collections.Where(m => m.Id == collectionID).Include(s => s.CollectionPhotoes).ThenInclude(x => x.Photo).FirstOrDefault();
            newCollection = _colRepo.GetCollectionById((int)collectionID);

            if (newCollection != null)
            {
                int collectionId = (int)collectionID;
                int collectionOwnerId = (int)newCollection.UserId;
                var collectionPhotos = _collectionPhotoRepo.GetAllCollectionPhotosbyCollectionId(collectionId);
                //var photos = _collectionsDbContext.Photos.Where(p => p.UserId == newCollection.UserId);

                var photos = _photoRepo.GetAllUserPhotos(collectionOwnerId);

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
                ViewData["collectionDescription"] = newCollection.Description;
                return View(AllPhotos);
            }


            return View(AllPhotos);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}

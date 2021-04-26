using System;
using System.Linq;
using iCollections.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using iCollections.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using iCollections.Data.Abstract;

namespace iCollections.Controllers
{
    public class ViewPhotosController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IPhotoRepository _photoRepo;
        private readonly IIcollectionUserRepository _userRepo;

        public ViewPhotosController(ILogger<HomeController> logger, UserManager<IdentityUser> userManager, IPhotoRepository photoRepo, IIcollectionUserRepository userRepo)
        {
            _logger = logger;
            _userManager = userManager;
            _photoRepo = photoRepo;
            _userRepo = userRepo;
        }

        public IActionResult Index()
        {
            string nastyStringId = _userManager.GetUserId(User);
            int userId = _userRepo.GetReadableUserID(nastyStringId);
            var photos = _photoRepo.GetMyPhotosInfo(userId);
            return View(photos);
        }
    }
}

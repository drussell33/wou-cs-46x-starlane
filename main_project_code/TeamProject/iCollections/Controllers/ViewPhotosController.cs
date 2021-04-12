using System;
using System.Linq;
using iCollections.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using iCollections.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace iCollections.Controllers
{
    public class ViewPhotosController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ICollectionsDbContext _collectionsDbContext;

        public ViewPhotosController(ILogger<HomeController> logger, UserManager<IdentityUser> userManager, ICollectionsDbContext collectionsDbContext)
        {
            _logger = logger;
            _userManager = userManager;
            _collectionsDbContext = collectionsDbContext;
        }

        public IActionResult Index()
        {
            var databaseReader = new DatabaseHelper(_userManager, _collectionsDbContext);
            string nastyStringId = _userManager.GetUserId(User);
            int userId = DatabaseHelper.GetReadableUserID(nastyStringId, _collectionsDbContext);
            var photos = databaseReader.GetMyPhotosInfo(userId);
            return View(photos);
        }
    }
}

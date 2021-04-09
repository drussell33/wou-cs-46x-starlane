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

namespace iCollections.Controllers
{
    public class CreateCollectionController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ICollectionsDbContext _collectionsDbContext;

        public CreateCollectionController(ILogger<HomeController> logger, UserManager<IdentityUser> userManager, ICollectionsDbContext collectionsDbContext)
        {
            _logger = logger;
            _userManager = userManager;
            _collectionsDbContext = collectionsDbContext;
        }


        public async Task<IActionResult> EnvironmentSelection()
        {
            IdentityUser user = await _userManager.GetUserAsync(User);  // does go to the db
            return View();
        }

        public async Task<IActionResult> PhotoSelection()
        {
            IdentityUser user = await _userManager.GetUserAsync(User);  // does go to the db
            return View();
        }

        public async Task<IActionResult> PublishingOptionsSelection()
        {
            IdentityUser user = await _userManager.GetUserAsync(User);  // does go to the db
            return View();
        }

        public async Task<IActionResult> PublishingSuccess()
        {
            IdentityUser user = await _userManager.GetUserAsync(User);  // does go to the db
            return View();
        }
    }
}

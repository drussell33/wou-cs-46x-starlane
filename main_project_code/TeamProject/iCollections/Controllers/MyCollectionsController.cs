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

namespace iCollections.Controllers
{
    public class MyCollections : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ICollectionsDbContext _collectionsDbContext;

        public MyCollections(ILogger<HomeController> logger, UserManager<IdentityUser> userManager, ICollectionsDbContext collectionsDbContext)
        {
            _logger = logger;
            _userManager = userManager;
            _collectionsDbContext = collectionsDbContext;
        }


        public async Task<IActionResult> MyCollectionsPage()
        {
            IdentityUser user = await _userManager.GetUserAsync(User);  // does go to the db
            return View();
        }

        [Authorize]
        [Route("mycollections/{name}")]
        public IActionResult MyCollectionsPage(string name)
        {
            if (name == null)
            {
                return RedirectToAction("Index", "Home");
            }
            IcollectionUser user = _collectionsDbContext.IcollectionUsers.FirstOrDefault(m => m.UserName == name);
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            string sessionusername = _userManager.GetUserId(User);
            if (sessionusername != user.AspnetIdentityId)
            {
                return RedirectToAction("Index", "Home");
            }

            var collections = _collectionsDbContext.IcollectionUsers.Include("Collections").FirstOrDefault(m => m.UserName == name).Collections;
            return View(collections);
        }

    }
}

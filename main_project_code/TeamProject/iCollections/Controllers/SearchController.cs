using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using iCollections.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using iCollections.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
namespace iCollections.Controllers
{
    public class SearchController : Controller
    {
        private readonly ILogger<SearchController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ICollectionsDbContext _db;

        public SearchController(ILogger<SearchController> logger, UserManager<IdentityUser> userManager, ICollectionsDbContext db)
        {
            _logger = logger;
            _userManager = userManager;
            _db = db;
        }

        public IActionResult SearchUser(string user)
        {
            IcollectionUser loggedInUser = null;
            if(User.Identity.IsAuthenticated)
            {
                 loggedInUser = _db.IcollectionUsers
                    .Include(u => u.FollowFollowedNavigations)
                    .ThenInclude(f => f.FollowedNavigation)
                    .FirstOrDefault(x => x.AspnetIdentityId == _userManager.GetUserId(User));
            }
            List<IcollectionUser> results = _db.IcollectionUsers.Where(x => x.UserName.Contains(user)).ToList();
            return View(new SearchList { loggedInUser = loggedInUser, results = results });
        }
    }
}

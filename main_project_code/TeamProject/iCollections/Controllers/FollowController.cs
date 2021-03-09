using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iCollections.Data;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using iCollections.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace iCollections.Controllers
{
    public class FollowController : Controller
    {
        private readonly ILogger<FollowController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ICollectionsDbContext _db;

        public FollowController(ILogger<FollowController> logger, UserManager<IdentityUser> userManager, ICollectionsDbContext db)
        {
            _logger = logger;
            _userManager = userManager;
            _db = db;
        }
        // GET: FollowController
        public ActionResult Index()
        {
            return View();
        }

        // PUT: FollowController/Follow
        [Authorize]
        [HttpPut]
        public void Follow(int follower, int followed)
        {
            var user_1 = _db.IcollectionUsers.FirstOrDefault(x => x.Id == follower);
            var user_2 = _db.IcollectionUsers.FirstOrDefault(x => x.Id == followed);
            if (_db.Follows.FirstOrDefault(x => x.Follower == follower && x.Followed == followed) == null)
            {
                var newFollow = new Follow { Follower = follower, FollowerNavigation = user_1, Followed = followed, FollowedNavigation = user_2 };
                _db.Follows.Add(newFollow);
                _db.SaveChanges();
            }
        }

        // DELETE: FollowController/Unfollow
        [Authorize]
        [HttpDelete]
        public void Unfollow(int follower, int followed)
        {
            Follow follow = _db.Follows.FirstOrDefault(x => x.Follower == follower && x.Followed == followed);
            if (follow != null)
            {
                _db.Follows.Remove(follow);
                _db.SaveChanges();
            }
        }
    }
}

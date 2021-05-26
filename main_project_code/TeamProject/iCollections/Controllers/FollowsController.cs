using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using iCollections.Data;
using iCollections.Models;
using Microsoft.AspNetCore.Authorization;
using iCollections.Data.Abstract;

namespace iCollections.Controllers
{
    public class FollowsController : Controller
    {
        private readonly IFollowRepository _followRepo;
        private readonly IIcollectionUserRepository _userRepo;
        public FollowsController(IFollowRepository followRepo, IIcollectionUserRepository userRepo)
        {
            _followRepo = followRepo;
            _userRepo = userRepo;
        }

        // GET: Follows
        public async Task<IActionResult> Index()
        {
            var allFollows = _followRepo.GetAll();
            return View(await allFollows.ToListAsync());
        }

        // GET: Follows/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var follow = await _followRepo.GetFollowAsync(id.Value);
            if (follow == null)
            {
                return NotFound();
            }

            return View(follow);
        }

        // GET: Follows/Create
        public IActionResult Create()
        {
            ViewData["Followed"] = new SelectList(_userRepo.GetAll(), "Id", "FirstName");
            ViewData["Follower"] = new SelectList(_userRepo.GetAll(), "Id", "FirstName");
            return View();
        }

        // POST: Follows/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Follower,Followed,Began")] Follow follow)
        {
            if (ModelState.IsValid)
            {
                await _followRepo.AddOrUpdateAsync(follow);
                return RedirectToAction(nameof(Index));
            }
            ViewData["Followed"] = new SelectList(_userRepo.GetAll(), "Id", "FirstName", follow.Followed);
            ViewData["Follower"] = new SelectList(_userRepo.GetAll(), "Id", "FirstName", follow.Follower);
            return View(follow);
        }

        // GET: Follows/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var follow = await _followRepo.FindByIdAsync(id.Value);
            if (follow == null)
            {
                return NotFound();
            }
            ViewData["Followed"] = new SelectList(_userRepo.GetAll(), "Id", "FirstName", follow.Followed);
            ViewData["Follower"] = new SelectList(_userRepo.GetAll(), "Id", "FirstName", follow.Follower);
            return View(follow);
        }

        // POST: Follows/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Follower,Followed,Began")] Follow follow)
        {
            if (id != follow.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _followRepo.AddOrUpdateAsync(follow);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FollowExists(follow.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Followed"] = new SelectList(_userRepo.GetAll(), "Id", "FirstName", follow.Followed);
            ViewData["Follower"] = new SelectList(_userRepo.GetAll(), "Id", "FirstName", follow.Follower);
            return View(follow);
        }

        // GET: Follows/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var follow = await _followRepo.GetFollowAsync(id.Value);
            if (follow == null)
            {
                return NotFound();
            }

            return View(follow);
        }

        // POST: Follows/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var follow = await _followRepo.FindByIdAsync(id);
            await _followRepo.DeleteAsync(follow);
            return RedirectToAction(nameof(Index));
        }

        private bool FollowExists(int id)
        {
            return _followRepo.Exists(id);
        }

        // POST: FollowController/Follow
        [Authorize]
        [HttpPost]
        [Route("/api/follow")]
        public JsonResult Follow(int follower, int followed)
        {
            if (follower == followed)
            {
                return Json(new { success = false, follower = follower, followed = followed, message = "can't follow yourself" });
            }
            var user_1 = _userRepo.GetUserById(follower);
            var user_2 = _userRepo.GetUserById(followed);
            if (user_1 == null || user_2 == null)
            {
                return Json(new { success = false, follower = follower, followed = followed, message = "User does not exist." });
            }
            if (_followRepo.GetFollowLight(x => x.Follower == follower && x.Followed == followed) == null)
            {
                var newFollow = new Follow { Follower = follower, FollowerNavigation = user_1, Followed = followed, FollowedNavigation = user_2, Began = DateTime.Now };
                try
                {
                    _followRepo.AddOrUpdate(newFollow);
                }
                catch (DbUpdateException dbe)
                {
                    return Json(new { success = false, follower = user_1.Id, followed = user_2.Id, message = dbe.ToString() });
                }
                return Json(new { success = true, follower = follower, followed = followed, message = "success" });
            }
            return Json(new { success = false, follower = user_1.Id, followed = user_2.Id, message = "follow already exists!" });
        }

        // POST: FollowController/Unfollow
        [Authorize]
        [HttpPost]
        [Route("api/unfollow")]
        public async Task<JsonResult> Unfollow(int follower, int followed)
        {
            Follow follow = _followRepo.GetFollow(x => x.Follower == follower && x.Followed == followed);
            if (follow != null)
            {
                try {
                    await _followRepo.DeleteAsync(follow);
                }
                catch (DbUpdateException dbe)
                {
                    return Json(new { success = false, follower = follower, followed = followed, message = dbe.ToString() });
                }
                return Json(new { success = true, follower = follower, followed = followed, message = "success" });
            }
            else
            {
                return Json(new { success = false, follower = follower, followed = followed, message = "bad input" });
            }
        }
    }
}

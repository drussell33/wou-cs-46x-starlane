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


namespace iCollections.Controllers
{
    public class FollowsController : Controller
    {
        private readonly ICollectionsDbContext _db;

        public FollowsController(ICollectionsDbContext context)
        {
            _db = context;
        }

        // GET: Follows
        public async Task<IActionResult> Index()
        {
            var iCollectionsDbContext = _db.Follows.Include(f => f.FollowedNavigation).Include(f => f.FollowerNavigation);
            return View(await iCollectionsDbContext.ToListAsync());
        }

        // GET: Follows/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var follow = await _db.Follows
                .Include(f => f.FollowedNavigation)
                .Include(f => f.FollowerNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (follow == null)
            {
                return NotFound();
            }

            return View(follow);
        }

        // GET: Follows/Create
        public IActionResult Create()
        {
            ViewData["Followed"] = new SelectList(_db.IcollectionUsers, "Id", "FirstName");
            ViewData["Follower"] = new SelectList(_db.IcollectionUsers, "Id", "FirstName");
            return View();
        }

        // POST: Follows/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Follower,Followed,Began")] Follow follow)
        {
            if (ModelState.IsValid)
            {
                _db.Add(follow);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Followed"] = new SelectList(_db.IcollectionUsers, "Id", "FirstName", follow.Followed);
            ViewData["Follower"] = new SelectList(_db.IcollectionUsers, "Id", "FirstName", follow.Follower);
            return View(follow);
        }

        // GET: Follows/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var follow = await _db.Follows.FindAsync(id);
            if (follow == null)
            {
                return NotFound();
            }
            ViewData["Followed"] = new SelectList(_db.IcollectionUsers, "Id", "FirstName", follow.Followed);
            ViewData["Follower"] = new SelectList(_db.IcollectionUsers, "Id", "FirstName", follow.Follower);
            return View(follow);
        }

        // POST: Follows/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
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
                    _db.Update(follow);
                    await _db.SaveChangesAsync();
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
            ViewData["Followed"] = new SelectList(_db.IcollectionUsers, "Id", "FirstName", follow.Followed);
            ViewData["Follower"] = new SelectList(_db.IcollectionUsers, "Id", "FirstName", follow.Follower);
            return View(follow);
        }

        // GET: Follows/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var follow = await _db.Follows
                .Include(f => f.FollowedNavigation)
                .Include(f => f.FollowerNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (follow == null)
            {
                return NotFound();
            }

            return View(follow);
        }

        // POST: Follows/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var follow = await _db.Follows.FindAsync(id);
            _db.Follows.Remove(follow);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FollowExists(int id)
        {
            return _db.Follows.Any(e => e.Id == id);
        }

        // POST: FollowController/Follow
        [Authorize]
        [HttpPost]
        [Route("/api/follow")]
        public JsonResult Follow(int follower, int followed)
        {
            var user_1 = _db.IcollectionUsers.FirstOrDefault(x => x.Id == follower);
            var user_2 = _db.IcollectionUsers.FirstOrDefault(x => x.Id == followed);
            if (_db.Follows.FirstOrDefault(x => x.Follower == follower && x.Followed == followed) == null)
            {
                var newFollow = new Follow { Follower = follower, FollowerNavigation = user_1, Followed = followed, FollowedNavigation = user_2 };
                _db.Follows.Add(newFollow);
                try
                {
                    _db.SaveChanges();
                }
                catch (DbUpdateException dbe)
                {
                    return Json(new { success = false, follower = user_1.Id, followed = user_2.Id, message = dbe.ToString() });
                }
                return Json(new { success = true, follower = follower, followed = followed, message = "success" });
            }
            return Json(new { success = false, follower = user_1.Id, followed = user_2.Id, message = "follow already exists?" });
        }

        // POST: FollowController/Unfollow
        [Authorize]
        [HttpPost]
        [Route("api/unfollow")]
        public JsonResult Unfollow(int follower, int followed)
        {
            Follow follow = _db.Follows
                                .Include(f => f.FollowedNavigation)
                                .Include(f => f.FollowerNavigation)
                                .FirstOrDefault(x => x.Follower == follower && x.Followed == followed);
            if (follow != null)
            {
                _db.Follows.Remove(follow);
                try { 
                    _db.SaveChanges(); 
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

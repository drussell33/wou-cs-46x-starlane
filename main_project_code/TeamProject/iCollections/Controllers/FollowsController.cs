using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using iCollections.Data;
using iCollections.Models;

namespace iCollections.Controllers
{
    public class FollowsController : Controller
    {
        private readonly ICollectionsDbContext _context;

        public FollowsController(ICollectionsDbContext context)
        {
            _context = context;
        }

        // GET: Follows
        public async Task<IActionResult> Index()
        {
            var iCollectionsDbContext = _context.Follows.Include(f => f.FollowedNavigation).Include(f => f.FollowerNavigation);
            return View(await iCollectionsDbContext.ToListAsync());
        }

        // GET: Follows/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var follow = await _context.Follows
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
            ViewData["Followed"] = new SelectList(_context.IcollectionUsers, "Id", "FirstName");
            ViewData["Follower"] = new SelectList(_context.IcollectionUsers, "Id", "FirstName");
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
                _context.Add(follow);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Followed"] = new SelectList(_context.IcollectionUsers, "Id", "FirstName", follow.Followed);
            ViewData["Follower"] = new SelectList(_context.IcollectionUsers, "Id", "FirstName", follow.Follower);
            return View(follow);
        }

        // GET: Follows/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var follow = await _context.Follows.FindAsync(id);
            if (follow == null)
            {
                return NotFound();
            }
            ViewData["Followed"] = new SelectList(_context.IcollectionUsers, "Id", "FirstName", follow.Followed);
            ViewData["Follower"] = new SelectList(_context.IcollectionUsers, "Id", "FirstName", follow.Follower);
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
                    _context.Update(follow);
                    await _context.SaveChangesAsync();
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
            ViewData["Followed"] = new SelectList(_context.IcollectionUsers, "Id", "FirstName", follow.Followed);
            ViewData["Follower"] = new SelectList(_context.IcollectionUsers, "Id", "FirstName", follow.Follower);
            return View(follow);
        }

        // GET: Follows/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var follow = await _context.Follows
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
            var follow = await _context.Follows.FindAsync(id);
            _context.Follows.Remove(follow);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FollowExists(int id)
        {
            return _context.Follows.Any(e => e.Id == id);
        }
    }
}

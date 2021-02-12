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
    [Authorize(Roles = "admin")]
    //[Authorize]
    public class ICollectionsUsersController : Controller
    {
        private readonly ICollectionsDbContext _context;

        public ICollectionsUsersController(ICollectionsDbContext context)
        {
            _context = context;
        }

        // GET: ICollectionsUsers
        public async Task<IActionResult> Index()
        {
            return View(await _context.IcollectionUsers.ToListAsync());
        }

        // GET: ICollectionsUsers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var icollectionUser = await _context.IcollectionUsers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (icollectionUser == null)
            {
                return NotFound();
            }

            return View(icollectionUser);
        }

        // GET: ICollectionsUsers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ICollectionsUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AspnetIdentityId,FirstName,LastName,UserName,DateJoined,AboutMe")] IcollectionUser icollectionUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(icollectionUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(icollectionUser);
        }

        // GET: ICollectionsUsers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var icollectionUser = await _context.IcollectionUsers.FindAsync(id);
            if (icollectionUser == null)
            {
                return NotFound();
            }
            return View(icollectionUser);
        }

        // POST: ICollectionsUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AspnetIdentityId,FirstName,LastName,UserName,DateJoined,AboutMe")] IcollectionUser icollectionUser)
        {
            if (id != icollectionUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(icollectionUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IcollectionUserExists(icollectionUser.Id))
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
            return View(icollectionUser);
        }

        // GET: ICollectionsUsers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var icollectionUser = await _context.IcollectionUsers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (icollectionUser == null)
            {
                return NotFound();
            }

            return View(icollectionUser);
        }

        // POST: ICollectionsUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var icollectionUser = await _context.IcollectionUsers.FindAsync(id);
            _context.IcollectionUsers.Remove(icollectionUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IcollectionUserExists(int id)
        {
            return _context.IcollectionUsers.Any(e => e.Id == id);
        }
    }
}

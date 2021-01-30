using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HimalayanProject.Models;

namespace HimalayanProject.Controllers
{
    public class PeakController : Controller
    {
        private readonly HimalayanContext _context;

        public PeakController(HimalayanContext context)
        {
            _context = context;
        }

        // GET: Peak
        public async Task<IActionResult> Index()
        {
            // SELECT Peak.ID, Name, Height, ClimbingStatus, FirstAscentYear, MAX(StartDate) FROM Peak
            // LEFT JOIN Expedition ON PeakID = Peak.ID
            // GROUP BY Peak.ID, Name, Height, ClimbingStatus, FirstAscentYear
            // ORDER BY MAX(StartDate) DESC;

            var newPeaks = _context.Expeditions
                                .Include(e => e.Peak)
                                .OrderByDescending(e => e.StartDate)
                                .Select(e => e.Peak)
                                .Distinct()
                                .Take(40);

            return View(await newPeaks.ToListAsync());
        }

//         // GET: Peak/Details/5
//         public async Task<IActionResult> Details(int? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }

//             var expedition = await _context.Expeditions
//                 .Include(e => e.Peak)
//                 .Include(e => e.TrekkingAgency)
//                 .FirstOrDefaultAsync(m => m.Id == id);
//             if (expedition == null)
//             {
//                 return NotFound();
//             }

//             return View(expedition);
//         }

//         // GET: Peak/Create
//         public IActionResult Create()
//         {
//             ViewData["PeakId"] = new SelectList(_context.Peaks, "Id", "Name");
//             ViewData["TrekkingAgencyId"] = new SelectList(_context.TrekkingAgencies, "Id", "Name");
//             return View();
//         }

//         // POST: Peak/Create
//         // To protect from overposting attacks, enable the specific properties you want to bind to, for 
//         // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> Create([Bind("Id,Season,Year,StartDate,TerminationReason,OxygenUsed,PeakId,TrekkingAgencyId")] Expedition expedition)
//         {
//             if (ModelState.IsValid)
//             {
//                 _context.Add(expedition);
//                 await _context.SaveChangesAsync();
//                 return RedirectToAction(nameof(Index));
//             }
//             ViewData["PeakId"] = new SelectList(_context.Peaks, "Id", "Name", expedition.PeakId);
//             ViewData["TrekkingAgencyId"] = new SelectList(_context.TrekkingAgencies, "Id", "Name", expedition.TrekkingAgencyId);
//             return View(expedition);
//         }

//         // GET: Peak/Edit/5
//         public async Task<IActionResult> Edit(int? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }

//             var expedition = await _context.Expeditions.FindAsync(id);
//             if (expedition == null)
//             {
//                 return NotFound();
//             }
//             ViewData["PeakId"] = new SelectList(_context.Peaks, "Id", "Name", expedition.PeakId);
//             ViewData["TrekkingAgencyId"] = new SelectList(_context.TrekkingAgencies, "Id", "Name", expedition.TrekkingAgencyId);
//             return View(expedition);
//         }

//         // POST: Peak/Edit/5
//         // To protect from overposting attacks, enable the specific properties you want to bind to, for 
//         // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> Edit(int id, [Bind("Id,Season,Year,StartDate,TerminationReason,OxygenUsed,PeakId,TrekkingAgencyId")] Expedition expedition)
//         {
//             if (id != expedition.Id)
//             {
//                 return NotFound();
//             }

//             if (ModelState.IsValid)
//             {
//                 try
//                 {
//                     _context.Update(expedition);
//                     await _context.SaveChangesAsync();
//                 }
//                 catch (DbUpdateConcurrencyException)
//                 {
//                     if (!ExpeditionExists(expedition.Id))
//                     {
//                         return NotFound();
//                     }
//                     else
//                     {
//                         throw;
//                     }
//                 }
//                 return RedirectToAction(nameof(Index));
//             }
//             ViewData["PeakId"] = new SelectList(_context.Peaks, "Id", "Name", expedition.PeakId);
//             ViewData["TrekkingAgencyId"] = new SelectList(_context.TrekkingAgencies, "Id", "Name", expedition.TrekkingAgencyId);
//             return View(expedition);
//         }

//         // GET: Peak/Delete/5
//         public async Task<IActionResult> Delete(int? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }

//             var expedition = await _context.Expeditions
//                 .Include(e => e.Peak)
//                 .Include(e => e.TrekkingAgency)
//                 .FirstOrDefaultAsync(m => m.Id == id);
//             if (expedition == null)
//             {
//                 return NotFound();
//             }

//             return View(expedition);
//         }

//         // POST: Peak/Delete/5
//         [HttpPost, ActionName("Delete")]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> DeleteConfirmed(int id)
//         {
//             var expedition = await _context.Expeditions.FindAsync(id);
//             _context.Expeditions.Remove(expedition);
//             await _context.SaveChangesAsync();
//             return RedirectToAction(nameof(Index));
//         }

//         private bool ExpeditionExists(int id)
//         {
//             return _context.Expeditions.Any(e => e.Id == id);
//         }
    }
}

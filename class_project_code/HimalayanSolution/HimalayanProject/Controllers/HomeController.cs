using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HimalayanProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace HimalayanProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private HimalayanContext db;

        public HomeController(ILogger<HomeController> logger, HimalayanContext db)
        {
            _logger = logger;
            this.db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(int id)
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Search(string search_term)
        {
            if (search_term != null)
            {

                //Search expeditions by peak name or trekking agency name. 
                IEnumerable<Expedition> result = db.Expeditions.Include(p => p.Peak).Where(p => p.Peak.Name.Contains(search_term)).OrderByDescending(O => O.Year).AsEnumerable();
                return View("Index", result);
            }
            else 
            {
                return View("Index");
            }
            
        }

        public IActionResult GetStats()
        {
            int numExpeditions = db.Expeditions.Count();
            int numPeaks = db.Peaks.Count();
            int numUnclimbedPeaks = db.Peaks.Where(p => p.ClimbingStatus == false).Count();
            return Json(new { NumExp = numExpeditions, NumPeaks = numPeaks, NumUnclimbed = numUnclimbedPeaks });
        }

        public IActionResult MostRecentExpeditionGenerator()
        {
            var singleExpedition = db.Expeditions.Include(e => e.Peak)
                .Include(e => e.TrekkingAgency)
                .OrderByDescending(O => O.Year).FirstOrDefault();
            string peakName = "N/A";
            int expeditionId = 0;
            string trekkingName = "N/A";
            string terminationReason = "N/A";

            if (singleExpedition != null)
            {
                peakName = singleExpedition.Peak.Name;
                expeditionId = singleExpedition.Id;
                trekkingName = singleExpedition.TrekkingAgency.Name;
                terminationReason = singleExpedition.TerminationReason;
            }
            
            return Json(new {PeakName = peakName, ExpeditionId = expeditionId, TrekkingName = trekkingName, TerminationReason = terminationReason });
        }

        public IActionResult RandomPeakGenerator()
        {

            int total = db.Peaks.Count();
            Random r = new Random();
            int offset = r.Next(0, total);

            var threePeaks = db.Peaks.Skip(offset).Take(3);

            return Json(threePeaks);
        }

    }
}

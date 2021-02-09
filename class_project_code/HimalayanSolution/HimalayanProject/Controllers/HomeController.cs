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
            IEnumerable<Expedition> result = db.Expeditions.Select(e => e.Year).Distinct().Select(y => db.Expeditions.First(Ex => Ex.Year == y)).OrderBy(yr => yr.Year).ToList();

            return View(result);
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

        public IActionResult Search(string search_term, string collection, string outcome, string season, int? year)
        {

            if (collection == "Peak")
            {
                if (outcome != "Outcome?:" && season != "Season:" && year > 0)
                {
                    if (outcome == "Success")
                    {
                        IEnumerable<Expedition> result = db.Expeditions.Include(p => p.Peak).Where(p => p.Peak.Name.Contains(search_term) && p.TerminationReason.Contains(outcome) && p.Year == year && p.Season == season).AsEnumerable();
                        return View("Index", result);
                    }
                    else
                    {
                        IEnumerable<Expedition> result = db.Expeditions.Include(p => p.Peak).Where(p => p.Peak.Name.Contains(search_term) && !p.TerminationReason.Contains(outcome) && p.Year == year && p.Season == season).AsEnumerable();
                        return View("Index", result);
                    }
                }

                else if (outcome != "Outcome?:" && season != "Season:")
                {
                    if (outcome == "Success")
                    {
                        IEnumerable<Expedition> result = db.Expeditions.Include(p => p.Peak).Where(p => p.Peak.Name.Contains(search_term) && p.TerminationReason.Contains(outcome) && p.Season == season).AsEnumerable();
                        return View("Index", result);
                    }
                    else
                    {
                        IEnumerable<Expedition> result = db.Expeditions.Include(p => p.Peak).Where(p => p.Peak.Name.Contains(search_term) && !p.TerminationReason.Contains(outcome) && p.Season == season).AsEnumerable();
                        return View("Index", result);
                    }
                }

                else if (outcome != "Outcome?:" && year > 0)
                {
                    if (outcome == "Success")
                    {
                        IEnumerable<Expedition> result = db.Expeditions.Include(p => p.Peak).Where(p => p.Peak.Name.Contains(search_term) && p.TerminationReason.Contains(outcome) && p.Year == year).AsEnumerable();
                        return View("Index", result);
                    }
                    else
                    {
                        IEnumerable<Expedition> result = db.Expeditions.Include(p => p.Peak).Where(p => p.Peak.Name.Contains(search_term) && !p.TerminationReason.Contains(outcome) && p.Year == year).AsEnumerable();
                        return View("Index", result);
                    }
                }

                else if (season != "Season:" && year > 0)
                {
                    IEnumerable<Expedition> result = db.Expeditions.Include(p => p.Peak).Where(e => e.Peak.Name.Contains(search_term) && e.Season == season && e.Year == year).AsEnumerable();
                    return View("Index", result);
                }

                else if (outcome != "Outcome?:")
                {


                    if (outcome == "Success")
                    {
                        IEnumerable<Expedition> result = db.Expeditions.Include(p => p.Peak).Where(p => p.Peak.Name.Contains(search_term) && p.TerminationReason.Contains(outcome)).AsEnumerable();
                        return View("Index", result);
                    }
                    else
                    {
                        IEnumerable<Expedition> result = db.Expeditions.Include(p => p.Peak).Where(p => p.Peak.Name.Contains(search_term) && !p.TerminationReason.Contains(outcome)).AsEnumerable();
                        return View("Index", result);
                    }

                }

                else if (season != "Season:")
                {
                    IEnumerable<Expedition> result = db.Expeditions.Include(p => p.Peak).Where(p => p.Peak.Name.Contains(search_term) && p.Season == season).AsEnumerable();
                    return View("Index", result);
                }

                else if (year > 0)
                {
                    IEnumerable<Expedition> result = db.Expeditions.Include(p => p.Peak).Where(p => p.Peak.Name.Contains(search_term) && p.Year == year).AsEnumerable();
                    return View("Index", result);
                }


                IEnumerable<Expedition> results = db.Expeditions.Include(p => p.Peak).Where(p => p.Peak.Name.Contains(search_term)).AsEnumerable();
                return View("Index", results);

            }

            else if (collection == "Trekking Agency")
            {

                if (outcome != "Outcome?:" && season != "Season:" && year > 0)
                {
                    if (outcome == "Success")
                    {
                        IEnumerable<Expedition> result = db.Expeditions.Include(p => p.TrekkingAgency).Where(p => p.TrekkingAgency.Name.Contains(search_term) && p.TerminationReason.Contains(outcome) && p.Year == year && p.Season == season).AsEnumerable();
                        return View("Index", result);
                    }
                    else
                    {
                        IEnumerable<Expedition> result = db.Expeditions.Include(p => p.TrekkingAgency).Where(p => p.TrekkingAgency.Name.Contains(search_term) && !p.TerminationReason.Contains(outcome) && p.Year == year && p.Season == season).AsEnumerable();
                        return View("Index", result);
                    }
                }

                else if (outcome != "Outcome?:" && season != "Season:")
                {

                    if (outcome == "Success")
                    {
                        IEnumerable<Expedition> result = db.Expeditions.Include(p => p.TrekkingAgency).Where(p => p.TrekkingAgency.Name.Contains(search_term) && p.TerminationReason.Contains(outcome) && p.Season == season).AsEnumerable();
                        return View("Index", result);
                    }
                    else
                    {
                        IEnumerable<Expedition> result = db.Expeditions.Include(p => p.TrekkingAgency).Where(p => p.TrekkingAgency.Name.Contains(search_term) && !p.TerminationReason.Contains(outcome) && p.Season == season).AsEnumerable();
                        return View("Index", result);
                    }
                }

                else if (outcome != "Outcome?:" && year > 0)
                {

                    if (outcome == "Success")
                    {
                        IEnumerable<Expedition> result = db.Expeditions.Include(p => p.TrekkingAgency).Where(p => p.TrekkingAgency.Name.Contains(search_term) && p.TerminationReason.Contains(outcome) && p.Year == year).AsEnumerable();
                        return View("Index", result);
                    }
                    else
                    {
                        IEnumerable<Expedition> result = db.Expeditions.Include(p => p.TrekkingAgency).Where(p => p.TrekkingAgency.Name.Contains(search_term) && !p.TerminationReason.Contains(outcome) && p.Year == year).AsEnumerable();
                        return View("Index", result);
                    }
                }

                else if (season != "Season:" && year > 0)
                {
                    IEnumerable<Expedition> result = db.Expeditions.Include(p => p.TrekkingAgency).Where(e => e.TrekkingAgency.Name.Contains(search_term) && e.Season == season && e.Year == year).AsEnumerable();
                    return View("Index", result);
                }

                else if (outcome != "Outcome?:")
                {

                    if (outcome == "Success")
                    {
                        IEnumerable<Expedition> result = db.Expeditions.Include(p => p.TrekkingAgency).Where(p => p.TrekkingAgency.Name.Contains(search_term) && p.TerminationReason.Contains(outcome)).AsEnumerable();
                        return View("Index", result);
                    }
                    else
                    {
                        IEnumerable<Expedition> result = db.Expeditions.Include(p => p.TrekkingAgency).Where(p => p.TrekkingAgency.Name.Contains(search_term) && !p.TerminationReason.Contains(outcome)).AsEnumerable();
                        return View("Index", result);
                    }

                }

                else if (season != "Season:")
                {
                    IEnumerable<Expedition> result = db.Expeditions.Include(p => p.TrekkingAgency).Where(p => p.TrekkingAgency.Name.Contains(search_term) && p.Season == season).AsEnumerable();
                    return View("Index", result);
                }

                else if (year > 0)
                {
                    IEnumerable<Expedition> result = db.Expeditions.Include(p => p.TrekkingAgency).Where(p => p.TrekkingAgency.Name.Contains(search_term) && p.Year == year).AsEnumerable();
                    return View("Index", result);
                }

                IEnumerable<Expedition> results = db.Expeditions.Include(p => p.TrekkingAgency).Where(p => p.TrekkingAgency.Name.Contains(search_term)).AsEnumerable();
                return View("Index", results);
            }

            IEnumerable<Expedition> res = db.Expeditions.Include(p => p.TrekkingAgency).Where(p => p.TrekkingAgency.Name.Contains(search_term)).AsEnumerable();
            return View("Index", res);
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

            return Json(new { PeakName = peakName, ExpeditionId = expeditionId, TrekkingName = trekkingName, TerminationReason = terminationReason });
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

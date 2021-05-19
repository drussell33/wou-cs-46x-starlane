using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iCollections.Models;

namespace iCollections.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index(ErrorMessage error)
        {
            return View(error);
        }

        public IActionResult Login()
        {
            return View();
        }
    }
}

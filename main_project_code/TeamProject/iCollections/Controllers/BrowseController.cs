using iCollections.Data;
using iCollections.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace iCollections.Controllers
{

    public class BrowseController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ICollectionsDbContext _collectionsDbContext;

        private DatabaseHelper dbHelper;

        public BrowseController(UserManager<IdentityUser> userManager, ICollectionsDbContext collectionsDbContext)
        {
            _userManager = userManager;
            _collectionsDbContext = collectionsDbContext;
            dbHelper = new DatabaseHelper(_userManager, _collectionsDbContext);
        }


        public IActionResult Index()
        {
            var init_user = _collectionsDbContext.IcollectionUsers.Include(c => c.Collections).FirstOrDefault(u => u.AspnetIdentityId == _userManager.GetUserId(User));





            return View();
        }

        public IActionResult Search(string keywords)
        {

            return View();
        }

    }
}

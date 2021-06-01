using iCollections.Data;
using iCollections.Data.Abstract;
using iCollections.Models;
using iCollections.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;


namespace iCollections.Controllers
{

    public class BrowseController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ICollectionsDbContext _collectionsDbContext;
        private readonly IIcollectionUserRepository _userRepo;

        private DatabaseHelper dbHelper;

        public BrowseController(UserManager<IdentityUser> userManager, ICollectionsDbContext collectionsDbContext)
        {
            _userManager = userManager;
            _collectionsDbContext = collectionsDbContext;
            dbHelper = new DatabaseHelper(_userManager, _collectionsDbContext, _userRepo);
        }

        [Authorize]
        public IActionResult Index(string keywords)
        {
            var init_user = _collectionsDbContext.IcollectionUsers.FirstOrDefault(u => u.AspnetIdentityId == _userManager.GetUserId(User));

            if (keywords == null)
            {
                var init_browselist = new BrowseList
                {
                    LoggedInUser = init_user,
                    SearchResults = null,
                    SuggestedKeywords = _collectionsDbContext.Keywords.ToList()

                };

                return View(init_browselist);
            }

            else
            {
                //string[] keys = keywords.Split(" "); // parse strings separated by space or whitespace
                string[] keys = StringUtilities.SplitBySpace(keywords); // parse strings separated by space or whitespace
                List<CollectionKeyword> filtered = new List<CollectionKeyword>();
                foreach(string token in keys)
                {
                    var coll_keys = _collectionsDbContext.CollectionKeywords.Include(c=>c.Collect).ThenInclude(u=>u.User).Where(k => k.Keyword.Name == token && k.Collect.Visibility == 1).ToList();
                    
                    filtered.AddRange(coll_keys);
                                       
                }

                filtered.Union(filtered);  // remove duplicate collections(edited)

                var init_browselist = new BrowseList
                {
                    LoggedInUser = init_user,
                    SearchResults = filtered,
                    SuggestedKeywords = _collectionsDbContext.Keywords.ToList()

                };

                return View(init_browselist);
            }
        }
    }
}

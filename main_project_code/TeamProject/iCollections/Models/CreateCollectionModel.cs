using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using iCollections.Models;
using iCollections.Data;
using System.IO;
using iCollections.Controllers;

namespace iCollections.Models
{
    public class CreateCollectionModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        private readonly ICollectionsDbContext _iCollectionsDbContext;


        public CreateCollectionModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ICollectionsDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _iCollectionsDbContext = context;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Display(Name = "iCollection Name")]
            public string Name { get; set; }
            [Display(Name = "Visibility")]
            public int Visibility { get; set; }
            public int? UserId { get; set; }
            public DateTime? DateMade { get; set; }
            //added in sprint 3
            public string Route { get; set; }

            public virtual IcollectionUser User { get; set; }
            public virtual ICollection<CollectionKeyword> CollectionKeywords { get; set; }
            public virtual ICollection<CollectionPhoto> CollectionPhotoes { get; set; }
        }

        private async Task LoadAsync(IdentityUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            string id = await _userManager.GetUserIdAsync(user);
            IcollectionUser appUser = _iCollectionsDbContext.IcollectionUsers.Where(u => u.AspnetIdentityId == id).FirstOrDefault();

            Username = userName;

            Input = new InputModel
            {
                Name = phoneNumber,
            };
        }
    }
}

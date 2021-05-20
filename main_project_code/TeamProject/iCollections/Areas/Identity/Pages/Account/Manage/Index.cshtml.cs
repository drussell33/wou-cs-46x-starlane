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
using iCollections.Data.Abstract;

namespace iCollections.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        //private readonly ICollectionsDbContext _iCollectionsDbContext;
        private readonly IIcollectionUserRepository _userRepo;
        private readonly IPhotoRepository _photoRepo;


        public IndexModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IIcollectionUserRepository userRepo,
            IPhotoRepository photoRepo)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userRepo = userRepo;
            _photoRepo = photoRepo;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
        }

        private async Task LoadAsync(IdentityUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            //var userr = _iCollectionsDbContext.IcollectionUsers.First(i => i.AspnetIdentityId == user.Id);
            var userInDb = _userRepo.GetIcollectionUserByIdentityId(user.Id);
            int numericUserId = userInDb.Id;

            try
            {
                PhotoUploader photoUploader = new PhotoUploader(_photoRepo, numericUserId);
                int photoId = photoUploader.UploadProfilePicture(Request.Form.Files[0].Name, Request.Form.Files[0]);
                userInDb.ProfilePicId = photoId;
                //_iCollectionsDbContext.SaveChanges();
                _userRepo.AddOrUpdate(userInDb);
            }
            catch (BadImageFormatException exception)
            {
                StatusMessage = exception.Message;
                return RedirectToPage();
            }
            catch (Exception)
            {
                return RedirectToPage();
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}

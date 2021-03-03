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


namespace iCollections.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        private readonly ICollectionsDbContext _iCollectionsDbContext;


        public IndexModel(
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
                PhoneNumber = phoneNumber
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

        private bool isProperImage(string type)
        {
            return type == "image/jpeg" || type == "image/png" || type == "image/gif";
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

            var userr = _iCollectionsDbContext.IcollectionUsers.First(i => i.AspnetIdentityId == user.Id);
            int numericUserId = userr.Id;

            int profilePicId = 0;

            try
            {
                foreach (var file in Request.Form.Files)
                {
                    if (file.Length <= 1048576 && isProperImage(file.ContentType))
                    {
                        Photo photo = new Photo();
                        photo.Name = file.FileName;

                        MemoryStream ms = new MemoryStream();
                        file.CopyTo(ms);
                        photo.Data = ms.ToArray();
                        photo.DateUploaded = DateTime.Now;

                        photo.UserId = userr.Id;

                        ms.Close();
                        ms.Dispose();

                        _iCollectionsDbContext.Photos.Add(photo);
                        _iCollectionsDbContext.SaveChanges();
                        profilePicId = photo.Id;
                        userr.ProfilePicId = profilePicId;
                        _iCollectionsDbContext.SaveChanges();
                    }
                    else
                    {
                        StatusMessage = "Error: Profile pictures cannot be larger than 1 MB.";
                        return RedirectToPage();
                    }
                }
            }
            catch (Exception)
            {

            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}

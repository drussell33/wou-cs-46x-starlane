using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using iCollections.Models;
using iCollections.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System.IO;
using Microsoft.AspNetCore.Http;


namespace iCollections.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly ICollectionsDbContext _iCollectionsDbContext;

        public RegisterModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            ICollectionsDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _iCollectionsDbContext = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            //Add fields to get further user info
            [Required]
            [Display(Name = "First Name")]
            public string FirstName { get; set; }

            [Required]
            [Display(Name = "Last Name")]
            public string LastName { get; set; }

            [Required]
            [Display(Name = "User Name")]
            public string UserName { get; set; }

            [Required]
            [Display(Name = "About Me")]
            public string AboutMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        private bool isProperImage(string type)
        {
            return type == "image/jpeg" || type == "image/png" || type == "image/gif";
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = Input.Email, Email = Input.Email };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    // Check and see if the id was set in the IdentityUser object when it was successfully created
                    _logger.LogInformation($"User created a new account with password, id is {user.Id}");

                    // Create one of our users
                    IcollectionUser fu = new IcollectionUser
                    {
                        AspnetIdentityId = user.Id,
                        FirstName = Input.FirstName,
                        LastName = Input.LastName,
                        UserName = Input.UserName,
                        AboutMe = Input.AboutMe
                    };
                    _iCollectionsDbContext.Add(fu);
                    await _iCollectionsDbContext.SaveChangesAsync();

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
                                fu.ProfilePicId = profilePicId;
                                _iCollectionsDbContext.SaveChanges();
                            }
                        }
                    }
                    catch (Exception)
                    {

                    }

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}

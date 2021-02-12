Main Project Code 
===========================
# First Video - Setting up team project with individual user accounts
## dotnet command line instructions used to create the started project with Identity+ for the user account functionality. 
````bash
dotnet new globaljson --output TeamProject/iCollections
dotnet new mvc --auth Individual -uld -o TeamProject/iCollections
````

dotnet --list-sdks
- need to have version 5?
- When I downloaded it was 5.0.3

````bash
dotnet new sln -o TeamProject
dotnet sln TeamProject add TeamProject/iCollections
````

dotnet list package
- previous commands should have installed the following.
    1. EntityFrameworkCote
    2. Identity.UI
    3. SQLServer
    4. Tools

dotnet tool install dotnet-aspnet-codegenerator -g
- needs to be version 5.0.1
dotnet tool update dotnet-aspnet-codegenerator -g

dotnet add package Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design


# Second Video - Identity Walkthrough
looking at the lists of the files from the identity so that we can select which ones we want to take out of the library and customize. 
````bash
dotnet aspnet-codegenerator identity --listFiles
dotnet aspnet-codegenerator identity -dc iCollections.Data.ApplicationDbContext --files "Account.Login;Account.Register"
````
## the list of Identity files when I ran the command  (only extracted login and register)
````
Account._StatusMessage
Account.AccessDenied
Account.ConfirmEmail
Account.ConfirmEmailChange
Account.ExternalLogin
Account.ForgotPassword
Account.ForgotPasswordConfirmation
Account.Lockout
Account.Login
Account.LoginWith2fa
Account.LoginWithRecoveryCode
Account.Logout
Account.Manage._Layout
Account.Manage._ManageNav
Account.Manage._StatusMessage
Account.Manage.ChangePassword
Account.Manage.DeletePersonalData
Account.Manage.Disable2fa
Account.Manage.DownloadPersonalData
Account.Manage.Email
Account.Manage.EnableAuthenticator
Account.Manage.ExternalLogins
Account.Manage.GenerateRecoveryCodes
Account.Manage.Index
Account.Manage.PersonalData
Account.Manage.ResetAuthenticator
Account.Manage.SetPassword
Account.Manage.ShowRecoveryCodes
Account.Manage.TwoFactorAuthentication
Account.Register
Account.RegisterConfirmation
Account.ResendEmailConfirmation
Account.ResetPassword
Account.ResetPasswordConfirmation
````

# Third Video - Razor runtime compilation
inorder to test out view changes without stoping and rerunning, use this command, but need to remember to take this out before deploying.
````bash
dotnet add package Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation
````
also need to take out the modification in startup
````c#
services.AddRazorPages().AddRazorRuntimeCompilation();
````

# Fourth Video - Identity DB and migrations
no code or commands

# Fifth Video - Team project starting architecture

insert screenshot

# Sixth Video - Getting basic data logged in user 
practicing getting the user authenticationed and stuff by adding the following code to the home controller and Index() method:

````c#
public IActionResult Index()
{
    bool isAdmin = User.IsInRole("Admin");
    bool isAuthenticated = User.Identity.IsAuthenticated;
    string name = User.Identity.Name;
    string authType = User.Identity.AuthenticationType;
    ViewBag.Message = $"User {name} is authenticated? {isAuthenticated} using type {authType} and is an Admin? {isAdmin}";
    return View();
}
````
and then making the privacy page one that only logged in users can view 
````c#
[Authorize]
public IActionResult Privacy()
{
    return View();
}
````

and then we put the usermanager in the home controler
````c#
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly UserManager<IdentityUser> _userManager;

    public HomeController(ILogger<HomeController> logger, UserManager<IdentityUser> userManager)
    {
        _logger = logger;
        _userManager = userManager;
    }
````

with the user manager we then modified the index code to access the database directly. 
````c#
public async Task <IActionResult> Index()
{
    // Information straight from the Controller (does not need to do to the database)
    bool isAdmin = User.IsInRole("Admin");
    bool isAuthenticated = User.Identity.IsAuthenticated;
    string name = User.Identity.Name;
    string authType = User.Identity.AuthenticationType;
    
    // Information from Identity through the user manager
    string id = _userManager.GetUserId(User);         // reportedly does not need to hit db
    IdentityUser user = await _userManager.GetUserAsync(User);  // does go to the db
    string email = user?.Email ?? "no email";
    string phone = user?.PhoneNumber ?? "no phone number";
    ViewBag.Message = $"User {name} is authenticated? {isAuthenticated} using type {authType} and is an" +
                        $" Admin? {isAdmin}. ID from Identity {id}, email is {email}, and phone is {phone}";
    return View();
}
````

# Seventh Video - Identity: a tale of 2 databases 
* updated our previous database design to have the user include the identity id for the user table 
* then added a follower table like the exisiting friends with table, 
* then created a new db call ICollectionsAuthentication locally,
* and added in the new connection string to the appsettings.
* then reddi the database design again and finally got it working
* Wrote the up, down, and seed script, but the only thing the seed did was populate the keyword table with data.
* data migration worked, auth is now in the new db. 




# 8th Video  Identity: create our own user at registration

````bash
dotnet ef dbcontext scaffold Name=ICollectionsConnection Microsoft.EntityFrameworkCore.SqlServer --context ICollectionsDbContext --context-dir Data --output-dir Models --verbose --force
````

````bash
dotnet aspnet-codegenerator controller -name ICollectionsUsersController -m IcollectionUser -dc ICollectionsDbContext --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries --force
````

# 9th Video 
just added the option from this video to require the emails to be uniche
````c#
// Customize some settings that Identity uses
services.Configure<IdentityOptions>(opts =>
{
    opts.User.RequireUniqueEmail = true;
});
````



# 10th Video - Identity: authorization policies and use
used the everything is open, then we block parts off. 
````c#
[Authorize(Roles = "admin")]
or
[Authorize(Roles = "user")]
or
[Authorize]
````

finished making the project up to video 10 where it is deployed on azure, havent deployed yet and have documented the changes and commandline instructions in the Readme.md
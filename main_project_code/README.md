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

# How to reset models and database on Azure (assuming VSCode)

## Use case:

You're using a Mac and you need to change your models or something about the database so that your code can work. Mac doesn't have SQL Server. For Windows users, it would probably be easier to set up the databases locally for very simple changes or to make sure completely your changes don't break anything, feel free to go through this tutorial or run the deploy scripts on your own Azure account. I assume the steps are almost identical on Visual Studio.

## Background:

Copy the following into a separate folder in case you mess something up:
+ All the models made by scaffolding
+ appsettings.json
+ old dbcontexts

## Step 1: Azure prep

+ Make Azure servers and databases (CS 460)

## Step 2: Setting up App database

**Start here if you already have Azure servers and databases set up**

+ Get connection string from Azure (write all credentials down)
+ Use VSCode mssql extension to run down (if needed) and up sql scripts

*May need to restart connection to get this to work*

## Step 3: Setting up connection strings

+ Put connection string in appsettings.json in "ConnectionStrings" {}
+ Take out password field from string ie "Password={}"
+ Run:  \
    `dotnet user-secrets set "ICollections:ServerPassword" "{password}"`
+ Add these lines to ConfigureServices() in Startup.cs (assuming your connection strings have that name and both use the same password) \
    `var authBuilder = new SqlConnectionStringBuilder(Configuration.GetConnectionString("AuthenticationConnection"));`
            `var appBuilder = new SqlConnectionStringBuilder(Configuration.GetConnectionString("ICollectionsConnection"));`
            `authBuilder.Password = Configuration["ICollections:ServerPassword"];`
            `appBuilder.Password = Configuration["ICollections:ServerPassword"];`

## Step 4: Entity Framework Scaffolding

+ Do migration stuff
+ Run: \
    `dotnet ef database update --context ApplicationDbContext`
+ Scaffold database tables (FROM APP DB ONLY!!!)
+ RUN: \
    `dotnet ef dbcontext scaffold "{Put your connection string to app db here}" Microsoft.EntityFrameworkCore.SqlServer --context ICollectionsDbContext --context-dir Data --output-dir Models --verbose --force`
+ Change the optionsBuilder.UseSqlServer() to have reference to connection string rather than your actual string for security reasons
+ Error when I run this (which I guess I have to do???)
+ RUN: \
    `dotnet aspnet-codegenerator controller -name ICollectionsUsersController -m IcollectionUser -dc ICollectionsDbContext --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries --force`

## Step 5: Setting up Auth database

*If you have not made an auth db on Azure yet, do it now*

+ Make a up script automatically
+ Run: \
    `dotnet ef migrations script --context ApplicationDbContext --output Data/DataUpdateIdentityAzure.sql`
+ Run up script for auth db generated by above command using mssql extension

## Step 6: Populate databases using seed scripts

+ Seed accounts (Hareem, Talia, etc) by simply running the web app locally
+ Run: \
    `dotnet run`
+ NOTE: You need to seed the accounts first! Run any other seed scripts after that.
+ Seed photos? (ask Derek)

## Step 7: More secrets

+ Set up secret for seeded user's password `dotnet user-secrets set "SeedUserPW" <pw>` OR set it up in Azure Key Vault via Portal (Warning: Costs money probably)
+ If you want deploy your app but I don't see why since we just want to reset database stuff

## Step 8: If the deployed app uses someone else's databases

+ Tell whoever is running the production Azure apps to reset database.


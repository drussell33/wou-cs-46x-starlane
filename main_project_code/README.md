Main Project Code 
===========================

# dotnet command line instructions used to create the started project with Identity+ for the user account functionality. 

dotnet new globaljson --output TeamProject/iCollections

dotnet new mvc --auth Individual -uld -o TeamProject/iCollections

dotnet --list-sdks
- need to have version 5?

dotnet new sln -o TeamProject

dotnet sln TeamProject add TeamProject/iCollections

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


Project Inception
=====================================

## List of Needs and Features
    1.Not in any particular format
    2.
    3.

## Identify Non-Functional Requirements
1. English is the default language, but we must support visitors and data in other character sets
2.
3.

## Initial Architecture Envisioning
    Diagrams and drawings, lists of components

## Initial Modeling
    Diagrams

## Agile Data Modeling
    Diagrams, SQL modeling (dbdiagram.io), UML diagrams

### Use Case Diagrams
    Diagrams

### Sequence Diagrams

### Other Modeling
    Diagrams, UI wireframes, page flows, ...

## Timeline and Release Plan
    Schedule: meaningful dates, milestones, sprint cadence, how releases are made (CI/CD, or fixed releases, or ?)

## Identify Functional Requirements (In User Story Format)

E: Epic  
U: User Story  
T: Task  
1. [U] As a visitor to the site I would like to see a fantastic and modern homepage that introduces me to the site and the features currently available.
   1. [T] Create starter ASP dot NET Core MVC Web Application with Individual User Accounts and no unit test project
   2. [T] Choose CSS library (Bootstrap 4, or ?) and use it for all pages
   3. [T] Create nice bare homepage: write initial welcome content, customize navbar, hide links to login/register
   4. [T] Create SQL Server database on Azure and configure web app to use it. Hide credentials.
   5. [T] Deploy it on Azure
2. [U] As someone who wishes to submit new information on an expedition I would like to be able to register an account so I will be able to use your services (once they're built)
   1. [T] Copy SQL schema from an existing ASP.NET Identity database and integrate it into our UP, DOWN scripts
   2. [T] Configure web app to use our db with Identity tables in it
   3. [T] Create a user table and customize user pages to display additional data
   4. [T] Re-enable login/register links
   5. [T] Manually test register and login; user should easily be able to see that they are logged in
3. [E] 
    1. [U]
        a. [T]
        b. [T]
    2. [U]
        a. [T]


MIlestone 2
===========================

# Milestone 2 Assignment Link
[Milestone 2 Assignment Link](https://wou-cs46x-resources.netlify.app/cs461/milestones/m2) 

The Milestone 2 Rubric

![Image](img/milestone2_rubric.png)

# Evidence of everyone following Git workflow for real work. (10pts)

    When we look at the contributors sidebar, we can see that everyone has contributed, and when we look at the repos network stats to see the commits in graph format 

# Refined and expanded list of primary needs and features (10pts)
## List of Needs and Features
1. Secure and reliable
    - Write privileges to admin and member users
    - Only read for general public
    - Login system
    - Database backup
2. Search data on website
    - Search bar for specific content
3. Get all expedition mountain news from sources
    - Link to external news sources and APIs (example: www.planetmountain.com)
4. Verify incoming expeditions
    - Trekking agencies can add expeditions
5. Personalize dynamic content
    - Filters for content (expeditions, peaks, etc.)

## Identify Non-Functional Requirements
1. Meeting with stakeholders every 4 weeks
2. Data Storage 
3. Data Transmission 
4. Hardware
5. User Interface 
6. Availability 
7. Reliability 
8. Maintainability
9. Accuracy 
10. Regulations
11. Security 
12. Validation


# Functional requirements as user stories: epics and user stories  (20pts)

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
3. [E] We want a secure and reliable website where the general public, administrators, and authenticated users can each have appropriate level access to the web application.
    1. [U] As a user I want to be able to log in to my account.
        a. [T] Make a login page
        b. [T] Get POST request and parse it
        c. [T] Compare hashed password
    2. [U] As a user I want to be able to sign out. 
        a. [T] Add a menu on the top left of page when signed in
        b. [T] When user clicks, “sign out” send POST request
        c. [T] Transition user state to “signed out”
    3. [U] As an administrator I want to be able to monitor the usage of our members to ensure the accuracy of their expedition data.
    4. [U] As a member I want to be able to submit my latest or upcoming expeditions so that my data is saved and submitted once.
        a. [T] When user submits form data, send POST request
        b. [T] Parse POST request data model and error check
        c. [T] If legit data, update database
        d. [T] Else print error message
    5. [U] As a general public user I want to be able to search for data by member, peak, expedition, and other criteria so that I can find the information I want.
    6. [U] As a user I don't want my personal data to be leaked to the public to ensure my privacy.
        a. [T] 
4. [E] We want to be able to search for data on the website, and have an accurate result sent back to us. 
    1. [U] As a general public user I want to be able 
        

# Appropriate initial modeling outputs UML (10pts)
    This is where the UML output should go, but i thought the ER diagram showed it in UML output, so.....

![Image](img/forms_whiteboard.JPG)

# E-R diagram output from agile data modeling (10pts)

[Link to dbdiagram](https://dbdiagram.io/d/5fcfdf459a6c525a03ba513f) 

![Image](img/er_diagram.png)

# Initial system Architecture Drawing (10pts)

![Image](img/initial_system_architecture.png)

# Vision Statement (10pts)
    This is where the vision statement 1.0 from the video should go.


# 2 Refined Project Ideas (20pts)
## Project 1 
## Project 2
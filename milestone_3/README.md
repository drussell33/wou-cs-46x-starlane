Milestone 3
===========================

## Milestone 3 Assignment Link
[Milestone 3 Assignment Link](https://wou-cs46x-resources.netlify.app/cs461/milestones/m3) 

The Milestone 3 Rubric

![Image](img/milestone3_rubric.png)


# Product backlog populated with all available epics and user stories; is properly prioriitized (10pts)

<table>
    <thead>
        <tr>
            <th>ID</th> <th>State</th> <th>Story Type</th> <th>Points</th> <th>Owner</th>
            <th>Title</th>
            <th>Description</th>
            <th>Links</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td>1</td> <td>Started</td> <td>E</td> <td>-</td> <td>Everyone</td>
            <td>I want to visit a website about Himalayan mountain climbing</td>
            <td>We Need to create a shared development evironment and create the basic model of a website that will be deployed on Azure</td>
            <td>Sprint 1 & ID: 2-8 </td>
        <tr>
            <td>2</td> <td>Started</td> <td>U</td> <td>-</td> <td>Everyone</td>
            <td>As a visitor to the site I would like to see a fantastic and modern homepage that introduces me to the site and the features currently available.</td>
            <td>We will be developing this project using ASP.NET Core MVC 3.1, with our Database being deployed on Azure. We need to create a project, database, and basic template website that shows the database is connected.</td>
            <td>Tasks: ID 3-8</td>
        </tr>
        <tr>
            <td>3</td> <td>Started</td> <td>T</td> <td>-</td> <td>Derek</td>
            <td>Create starter ASP dot NET Core MVC Web Application with Individual User Accounts and no unit test project</td>
            <td>Building the basic ASP.NET CORE MVC 3.1 application template </td>
            <td>Task for User Story ID: 2</td>
        </tr>
        <tr>
            <td>4</td> <td>-</td> <td>T</td> <td>-</td> <td>Baltazar</td>
            <td>Create SQL Server database on Azure and configure web app to use it. Hide credentials.</td>
            <td>Building the azure database and deploying for everyone to use. </td>
            <td>Task for User Story ID: 2</td>
        </tr>
        <tr>
            <td>5</td> <td>Not Started</td> <td>T</td> <td>-</td> <td>Cuauhtemoc</td>
            <td>Choose CSS library (Bootstrap 4, or ?) and use it for all pages</td>
            <td>We could always re-do this later on, so anything works.</td>
            <td>Task for User Story ID: 2</td>
        </tr>
        <tr>
            <td>6</td> <td>Not Started</td> <td>T</td> <td>-</td> <td>Cuauhtemoc</td>
            <td>Create nice bare homepage: write initial welcome content, customize navbar, hide links to login/register</td>
            <td>Building some basic page views and navbar that are essentially empty right now, but will be filled later. </td>
            <td>Task for User Story ID: 2</td>
        </tr>
        <tr>
            <td>7</td> <td>Not Started</td> <td>T</td> <td>-</td> <td>Rafael</td>
            <td>Create simple search to query the database and show connectivity</td>
            <td>Building the basic models in the ASP.NET Core project from the azure database.</td>
            <td>Task for User Story ID: 2</td>
        </tr>
                <tr>
            <td>8</td> <td>Not Started</td> <td>T</td> <td>-</td> <td></td>
            <td>Deploy it on Azure</td>
            <td></td>
            <td>Task for User Story ID: 2</td>
        </tr>
        <tr>
            <td>9</td> <td>-</td> <td></td> <td>-</td> <td></td>
            <td></td>
            <td></td>
            <td></td>
        </tr>
    </tbody>
</table>


## The following needs to be developed and entered into the Sprint 2 (final sprint) product backlog.
2. [U] As someone who wishes to submit new information on an expedition I would like to be able to register an account so I will be able to use your services (once they're built)
   1. [T] Copy SQL schema from an existing ASP.NET Identity database and integrate it into our UP, DOWN scripts
   2. [T] Configure web app to use our db with Identity tables in it
   3. [T] Create a user table and customize user pages to display additional data
   4. [T] Re-enable login/register links
   5. [T] Manually test register and login; user should easily be able to see that they are logged in
3. [E] We want a secure and reliable website where the general public, administrators, and authenticated users can each have appropriate level access to the web application.
    1. [U] As a user I want to be able to log in to my account.
        + [T] Make a login page
        + [T] Get POST request and parse it
        + [T] Compare hashed password
    2. [U] As a user I want to be able to sign out. 
        + [T] Add a menu on the top left of page when signed in
        + [T] When user clicks, “sign out” send POST request
        + [T] Transition user state to “signed out”
    3. [U] As an administrator I want to be able to monitor the usage of our members to ensure the accuracy of their expedition data.
    4. [U] As a member I want to be able to submit my latest or upcoming expeditions so that my data is saved and submitted once.
        + [T] When user submits form data, send POST request
        + [T] Parse POST request data model and error check
        + [T] If legit data, update database
        + [T] Else print error message
    5. [U] As a general public user I want to be able to search for data by member, peak, expedition, and other criteria so that I can find the information I want.
        + [T] If not authenticated user or admin, show only data about members, peaks, expeditions, etc.
        + [T] See below #5.
    6. [U] As a user I don't want my personal data to be leaked to the public to ensure my privacy.
        + [T] 
4. [E] We want to be able to search for data on the website, and have an accurate result sent back to us. 
    1. [U] As a general public user I want to be able 

5. [E] Users want to be able to filter the content so they can find the information they seek.
    1. [U] As a user, when I open the "see expeditions" page, I want to see the last 100 expeditions done so that I can see how the data is rendered and not overwhelm other users with potentially thousands of expeditions at once. Note: do this for peaks and members too.
    2. [U] As a user, I want to filter expeditions by year, season, nation, leader, sponsor/name, and host so that I need not fill out a giant form with data I may not know beforehand.
        + [T] Have a side panel with all filters. The text entry filters will be year, nation, leader, and sponser. The drop down menu filters will be season and host.
        + [T] When a user applies the filters by clicking a button, a GET request is sent along with a query string of the filters.
        + [T] The server parses the query string and gets the parameters.
        + [T] It does a select query and populates the list of expeditions.
        + [T] It sends the list back to the view. The view renders the list.
    3. [U] As a user, I want to filter peaks by peak name so that I need not fill out a giant form with data I may not know beforehand.
        + [T] Have a side panel with all filters. The text entry filters will be peak name.
        + [T] When a user applies the filters by clicking a button, a GET request is sent along with a query string of the filters.
        + [T] The server parses the query string and gets the parameters.
        + [T] It does a select query and populates the list of peaks.
        + [T] It sends the list back to the view. The view renders the list.
    4. [U] As a user, I want to filter members by last name, citizenship, and agency so that I need not fill out a giant form with data I may not know beforehand.
        + [T] Have a side panel with all filters. The text entry filters will be last name, citizenship, and agency.
        + [T] When a user applies the filters by clicking a button, a GET request is sent along with a query string of the filters.
        + [T] The server parses the query string and gets the parameters.
        + [T] It does a select query and populates the list of members.
        + [T] It sends the list back to the view. The view renders the list.


# Class Project Sprint 1 Planning: good user story for each team member; is properly written and entered in the backlog (10pts)

This Sprint is built from the top level PBI: Getting Shared Development Environment Set-up

<table>
    <thead>
        <tr>
            <th>ID</th> <th>State</th> <th>Story Type</th> <th>Points</th> <th>Owner</th>
            <th>Title</th>
            <th>Description</th>
            <th>Links</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td>1</td> <td>Started</td> <td>U</td> <td>-</td> <td>Everyone</td>
            <td>As a visitor to the site I would like to see a fantastic and modern homepage that introduces me to the site and the features currently available.</td>
            <td>We will be developing this project using ASP.NET Core MVC 3.1, with our Database being deployed on Azure. We need to create a project, database, and basic template website that shows the database is connected.</td>
            <td>Tasks: ID 2-6</td>
        </tr>
        <tr>
            <td>2</td> <td>Completed</td> <td>T</td> <td>-</td> <td>Derek</td>
            <td>Create starter ASP dot NET Core MVC Web Application with Individual User Accounts and no unit test project</td>
            <td>Building the basic ASP.NET CORE MVC 3.1 application template </td>
            <td>Task for User Story ID: 1</td>
        </tr>
        <tr>
            <td>3</td> <td>Completed</td> <td>T</td> <td>-</td> <td>Baltazar</td>
            <td>Create SQL Server database on Azure and configure web app to use it. Hide credentials.</td>
            <td>Building the azure database and deploying for everyone to use. </td>
            <td>Task for User Story ID: 1</td>
        </tr>
        <tr>
            <td>4</td> <td>Started</td> <td>T</td> <td>-</td> <td>Cuauhtemoc</td>
            <td>Choose CSS library (Bootstrap 4, or ?) and use it for all pages</td>
            <td>We could always re-do this later on, so anything works.</td>
            <td>Task for User Story ID: 1</td>
        </tr>
        <tr>
            <td>5</td> <td>Started</td> <td>T</td> <td>-</td> <td>Cuauhtemoc</td>
            <td>Create nice bare homepage: write initial welcome content, customize navbar, hide links to login/register</td>
            <td>Building some basic page views and navbar that are essentially empty right now, but will be filled later. </td>
            <td>Task for User Story ID: 1</td>
        </tr>
        <tr>
            <td>6</td> <td>Completed</td> <td>T</td> <td>-</td> <td>Rafael</td>
            <td>Create simple search to query the database and show connectivity</td>
            <td>Building the basic models in the ASP.NET Core project from the azure database.</td>
            <td>Task for User Story ID: 1</td>
        </tr>
                <tr>
            <td>7</td> <td>Not Started</td> <td>T</td> <td>-</td> <td></td>
            <td>Deploy it on Azure</td>
            <td></td>
            <td>Task for User Story ID: 1</td>
        </tr>
        <tr>
            <td>8</td> <td>-</td> <td></td> <td>-</td> <td></td>
            <td></td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td>9</td> <td>-</td> <td></td> <td>-</td> <td></td>
            <td></td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td>10</td> <td>-</td> <td></td> <td>-</td> <td></td>
            <td></td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td>11</td> <td>-</td> <td></td> <td>-</td> <td></td>
            <td></td>
            <td></td>
            <td></td>
        </tr>
    </tbody>
</table>

# Class Project Sprint 1 Construction: process followed, status updated according to state diagram (10pts)
    The Status in the Sprint 1 Product Backlog reflects the updates that we took:
    - Started
    - Finished
    - Accepted 
    - Rejected
    - Delivered

# Class Project Sprint 1 Construction: user stories implemented and deployed; features deployed match user story descriptions and all other specifications and requirements (30pts)
    Here is the link to the basic demo deployed on Azure

# Team Project Inception: Mindmap or other brainstorming artifacts (15pts)

[Flow Chart / Mindmap ](https://miro.com/app/board/o9J_lXPB2mE=/) 

![Image](img/miro_mindmap.png)


# Team Project Inception: Vision statement (10pts)

**For** anyone **who** wants to request or submit information requarding, **the** moutaining climbing peaks, members, and expeditions data, **can** use a dynamic updating website **that** will provide a single point of access to the Himalayan Database. The system will store the data available for mountain peaks, registered members, and expeditions, and it will allow the general public the abilitiy to search for this data, while having the most up to date info, news stories, publications, ect., so that it will be the central hub for all up to date climbing information. **Unlike** the current website, **our users** will be able to have the most up to date information in a much more user friendly and dynamic way, and our data can become a trusted resource.

# Team Project Inception: Needs and Features (15pts)
    - Users have private accounts that store any data they upload.
    - Users can upload photos of their items.
    - An API removes the background of the item photo, or can render the object in 3D, and with further development prepare the object for Augmented Reality Viewing.
    - Users can select various backgrounds to use to display their items.
    - The backgrounds can either be a 2D view, a 360 degree view, and with further development reach an Augmented Reality View.
    - The backgrounds have specific slots that allows the user to change their layout with ease.
    - Users can make their collections public, or private and share only with their friends.
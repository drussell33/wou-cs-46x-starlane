MIlestone 4
===========================

## Milestone 4 Assignment Link
[Milestone 4 Assignment Link](https://wou-cs46x-resources.netlify.app/cs461/milestones/m4) 

The Milestone 3 Rubric

![Image](img/milestone4_rubric.png)

# Class Project Requirements: 

1. Class project backlog: prioritized, estimated for 8 - 10 points worth of stories, and updated; links to story descriptions and required resources (10 pts)
2. Class project sprint planned: appropriate number of points and allocated to team members equitably (10 pts)
3. Class project: all stories in sprint 2 have descriptions like the exemplar, in separate markdown files and are linked from the backlog. (20 pts)
4. Class project construction: all stories completed, match descriptions and are deployed on Azure. (20 pts)

[Class Project Live Demo on Azure](https://himalayanapp.azurewebsites.net/) 

# Class Project Final Backlog: (Includes Sprint 2 Backlog)

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
            <td>1</td> <td>Delivered</td> <td>E</td> <td>-</td> <td>Everyone</td>
            <td>I want to visit a website about Himalayan mountain climbing</td>
            <td>We Need to create a shared development evironment and create the basic model of a website that will be deployed on Azure</td>
            <td>Sprint 1 & ID: 2-8 </td>
        <tr>
            <td>2</td> <td>Delivered</td> <td>E</td> <td>-</td> <td>Everyone</td>
            <td>As a visitor to the site I would like to see a fantastic and modern homepage that introduces me to the site and the features currently available.</td>
            <td>We will be developing this project using ASP.NET Core MVC 3.1, with our Database being deployed on Azure. We need to create a project, database, and basic template website that shows the database is connected.</td>
            <td>Tasks: ID 3-8</td>
        </tr>
        <tr>
            <td>3</td> <td>Delivered</td> <td>U</td> <td>-</td> <td>Derek</td>
            <td>Create starter ASP dot NET Core MVC Web Application with Individual User Accounts and no unit test project</td>
            <td>Building the basic ASP.NET CORE MVC 3.1 application template </td>
            <td>Task for User Story ID: 2</td>
        </tr>
        <tr>
            <td>4</td> <td>Delivered</td> <td>U</td> <td>-</td> <td>Baltazar</td>
            <td>Create SQL Server database on Azure and configure web app to use it. Hide credentials.</td>
            <td>Building the azure database and deploying for everyone to use. </td>
            <td>Task for User Story ID: 2</td>
        </tr>
        <tr>
            <td>5</td> <td>Delivered</td> <td>U</td> <td>-</td> <td>Cuauhtemoc</td>
            <td>Choose CSS library (Bootstrap 4, or ?) and use it for all pages</td>
            <td>We could always re-do this later on, so anything works.</td>
            <td>Task for User Story ID: 2</td>
        </tr>
        <tr>
            <td>6</td> <td>Delivered</td> <td>U</td> <td>-</td> <td>Cuauhtemoc</td>
            <td>Create nice bare homepage: write initial welcome content, customize navbar, hide links to login/register</td>
            <td>Building some basic page views and navbar that are essentially empty right now, but will be filled later. </td>
            <td>Task for User Story ID: 2</td>
        </tr>
        <tr>
            <td>7</td> <td>Delivered</td> <td>U</td> <td>-</td> <td>Rafael</td>
            <td>Create simple search to query the database and show connectivity</td>
            <td>Building the basic models in the ASP.NET Core project from the azure database.</td>
            <td>Task for User Story ID: 2</td>
        </tr>
        <tr>
            <td>8</td> <td>Delivered</td> <td>T</td> <td>-</td> <td>Rafael / Baltazar</td>
            <td>Deploy it on Azure</td>
            <td>Get both the database and the trial site functional on Azure for a demo </td>
            <td>Task for User Story ID: 2</td>
        </tr>
        <tr>
            <td>9</td> <td>Delivered</td> <td>U</td> <td>2</td> <td>Baltazar</td>
            <td>As a user, when I open the "see expeditions" page, I want to see the last 40 expeditions done so that I can see how good the data is rendered and not overwhelm other users with potentially thousands of expeditions at once./td>
            <td>The database contains hundreds of expeditions. Users do not want to be overwhelmed with so many results at once when they open the expeditions page. Plus, the bigger our models, the slower our page will load. Our stakeholders want a good looking expedition page. They want a modern-looking UI, not a web page that looks like it was made in 2004. </td>
            <td> <a href="exemplars/baltazar_user_story1.md">Exemplar Link</a></td>
        </tr>
        <tr>
            <td>10</td> <td>Delivered</td> <td>U</td> <td>2</td> <td>Baltazar</td>
            <td>As a user, when I open the "see peaks" page, I want to see the last 40 peaks climbed so that I can see how good the data is rendered and not overwhelm other users with potentially thousands of peaks at once.</td>
            <td>The database contains many peaks. Users do not want to be overwhelmed with so many results at once when they open the peaks page. Plus, the bigger our models, the slower our page will load. Our stakeholders want a good looking peaks page. </td>
            <td> <a href="exemplars/baltazar_user_story2.md">Exemplar Link</a></td>
        </tr>
        <tr>
            <td>11</td> <td>Delivered</td> <td>U</td> <td>2</td> <td>Derek</td>
            <td>As a user, when I open the home page, I want a dynamic engaging experience. I want to see I want to see live stats and data about expeditions and peaks. I also want to obtain all the basic information about the website itself so I can full utilize it.
            </td>
            <td>The Home page of our site needs to be engaging the users, allowing them to easily navigate around our site, as well as be provided with live stats and data from our internal database, and be provided with links from our partners that allow cross site navigation to increase their web traffic as well. On the home page there needs to be stats on the expeditions and peaks, as well as members who wish to be public. The links to our external partners should be obvious to the user so that they know they are navigating away from our website.</td>
            <td> <a href="exemplars/derek_user_story.md">Exemplar Link</a></td>
        </tr>
        <tr>
            <td>12</td> <td>Delivered</td> <td>U</td> <td>2</td> <td>Rafael</td>
            <td>As a user I want to have the ability to apply search filters to the expedition's search feature in order to designate in which places to look for my search term so I am able to more quickly find and better understand what I am looking for.</td>
            <td>Adds filtering functionality for expeditions search feature by creating UI elements the user can select from when searching that will determine which entity properties the search will attempt to match to the user's term. Filters Include: Expedition season and year. Peak name and Trekking agency name.</td>
            <td> <a href="exemplars/rafael_user_story.md">Exemplar Link</a></td>
        </tr>
        <tr>
            <td>13</td> <td>Delivered</td> <td>U</td> <td>2</td> <td>Cuauhtemoc</td>
            <td>As a user I want to be able to gather more information from the search results.</td>
            <td>Browsing with links from the search results for more data</td>
            <td> <a href="">Exemplar Link</a></td>
        </tr>
        <tr>
            <td>14</td> <td>Un-Assigned</td> <td>U</td> <td>2</td> <td>-</td>
            <td>The title</td>
            <td>the description</td>
            <td> <a href="">Exemplar Link</a></td>
        </tr>
        <tr>
            <td>14</td> <td>Un-Assigned</td> <td>U</td> <td>2</td> <td>-</td>
            <td>The title</td>
            <td>the description</td>
            <td> <a href="">Exemplar Link</a></td>
        </tr>
    </tbody>
</table>



# Team Project Inception: appropriate activities were undertaken, output generates and placed into repo. Appropriate and adequate? (40 pts)

<a href="team_inception.md">Inception Documentation Link </a>
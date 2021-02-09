# As a user, when I open the home page, I want a dynamic engaging experience. I want to see I want to see live stats and data about expeditions and peaks. I also want to  obtain all the basic information about the website itself so I can full utilize it. 

## ID: 14
## Effort Points: 2
## Owner: Derek Russell
## Feature branch name: ajax_feature

## Assumptions/Preconditions
I assume I am going to do most of my development in the home controller, home controller index.cshtml, and site.js. The homepage modifications from other features will not effect my feature work.

## Description
The Home page of our site needs to be engaging the users, allowing them to easily navigate around our site, as well as be provided with live stats and data from our internal database, and be provided with links from our partners that allow cross site navigation to increase their web traffic as well. On the home page there needs to be stats on the expeditions and peaks, as well as members who wish to be public. The links to our external partners should be obvious to the user so that they know they are navigating away from our website. 

## Acceptance Criteria
When you open the Home page, you will see live stats, member profiles, and external links, on an easy to navigate home page. All of the links work properly when connected to the azure datadata. The search feature and ajax feature do not conflict. 

## Tasks
1. Make sure that the database is functioning properly for my development environment.
2. Write the Iaction in the home Controller to query the data base and return the data in json
3. Gather up the links and photos for the partnered wedsites
4. Put the photos of the partners inside the images folder in wwwroot.
5. Add the routes for the stats and latest db entries in the startup file using api/stats and api/laest
6. Write the javascript and ajax to get the stats data on page load and dynamically insert it into the home page
7. Edit the home page so that there are clear sections for the external partner links, stats, and latest data.
8. Modifiy the home controller index.cshtml view for the javascript injection of data.
9. Use bootstrap card decks for the news stores on the index.cshtml.
10. Organize the home page into sections to display this data.
11. Ensure that the home page search feature is not modified or changed during devleopment.


## Dependencies
Need to work with Rafael to ensure that any style changes to the homepage work for both of our features.

## Any notes written while implementing this story
I: Yes
N: Not quite, it looks more like a contract
V: Good aesthetic could make it more user-friendly and attract more users
E: Given an effort score
S: Couple person-days work
T: Yes


<a href="../README.md">Return to Backlog</a>
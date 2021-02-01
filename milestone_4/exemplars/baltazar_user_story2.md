# As a user, when I open the "see peaks" page, I want to see the last 40 peaks climbed so that I can see how good the data is rendered and not overwhelm other users with potentially thousands of peaks at once.

## ID: __
## Effort Points: 2
## Owner: Baltazar Ortiz
## Feature branch name: peak_list

## Assumptions/Preconditions
I assume this is similar to the "see expeditions" page.

## Description
The database contains many peaks. Users do not want to be overwhelmed with so many results at once when they open the peaks page. Plus, the bigger our models, the slower our page will load. Our stakeholders want a good looking peaks page. They want a modern-looking UI, not a web page that looks like it was made in 2004. The only challenging thing will be designing the query to get the relevant data. I also need to create a web page to put this data in.

## Acceptance Criteria
If you go to the peaks page, you should see 40 peaks with more updated styling after the page loads.

## Tasks
1. Make a new controller
2. Link the link to the new controller
3. In the new controller, make an endpoint
4. Make a new web page for peaks
5. New endpoint queries database and sends result to peak page
6. Render the results
7. Style web page

## Dependencies
None other user stories in this sprint 

## Any notes written while implementing this story
I: Yes
N: Not quite, it looks more like a contract
V: Good aesthetic could make it more user-friendly and attract more users
E: Given an effort score
S: Couple person-days work
T: Yes


<a href="../README.md">Return to Backlog</a>
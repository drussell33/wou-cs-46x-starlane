# As a user, when I open the "see expeditions" page, I want to see the last 40 expeditions done so that I can see how good the data is rendered and not overwhelm other users with potentially thousands of expeditions at once. Note: do this for peaks and members too.

## ID: 13
## Effort Points: 2
## Owner: Baltazar Ortiz
## Feature branch name: exp_list

## Assumptions/Preconditions
I assume I am going to get the expeditions by latest date. That is chronologically.

## Description
The database contains hundreds of expeditions. Users do not want to be overwhelmed with so many results at once when they open the expeditions page. Plus, the bigger our models, the slower our page will load. Our stakeholders want a good looking expedition page. They want a modern-looking UI, not a web page that looks like it was made in 2004. This is easy to implement as the current deployment already retreives less than 100 expeditions and has some Bootstrap styling. Also, the stakeholders wanted 40 expeditions shown, not 100; thus, this user story was amended.

## Acceptance Criteria
If you go to the expeditions page, you should see 40 expeditions with more updated styling after the page loads.

## Tasks
1. Change controller to query 40 expeditions from database by year
2. Style web page using Bootstrap

## Effort Points: 2

## Dependencies
None other user stories in this sprint 

## Any notes written while implementing this story
I: Yes
N: Not quite, it looks more like a contract
V: Good aesthetic could make it more user-friendly and attract more users
E: Given an effort score
S: Couple person-days work
T: Yes
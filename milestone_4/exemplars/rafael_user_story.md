# As a user I want to have the ability to apply search filters to the expedition's search feature in order to designate in which places to look for my search term so I am able to more quickly find and better understand what I am looking for.

## ID: 12
## Effort Points: 2
## Owner: Rafael Arellano
## Feature branch name: feature_cp/expeditions-search-filters

## Assumptions/Preconditions
- Expeditions search feature has the ability to search and return results successfully and accurately according to the search term a user has entered.

## Description
- Adds filtering functionality for expeditions search feature by creating UI elements the user can select from when searching that will determine which entity properties the search will attempt to match to the user's term.
- Filters Include: Expedition season and year. Peak name and Trekking agency name.

## Acceptance Criteria

1. If the UI elements are clear, concise, and the user can interact with them in a way where it does not greatly complicate their search experience, then these filters will be a great asset in reducing the overall time the user spends searching as well as the amount of items the query returns once it is performed.
2. If the filter properties are practical to filter by and substantially reduce the number of items returned in the search result, then the user will better understand the data they are looking at and find what they are looking for much faster.

## Tasks

1. Modify UI by adding appropriate select/input elements for the user to interact with that will set flags and notify the controller to query the database based on selected flags.
2. Modify HomeController "Search" method to accept search filter flags incoming from the view and build logic to query the database based on flags.
3. Expand UI search results to display additional entity properties once the query is returned.

## Dependencies

- User Story ID: 7 (Create simple search to query the database and show connectivity)

## Any notes written while implementing this story

- Considering and covering every possible search combination became a very arduous process and I'm not sure if the solution utilized to cover them was the most effective.


<a href="../README.md">Return to Backlog</a>
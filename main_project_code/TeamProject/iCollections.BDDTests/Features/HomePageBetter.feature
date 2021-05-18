Feature: HomePageBetter
	This is going to test the route functions, that take in a collection id, and return a list of rending photos That will be passed through the DOM. Yes.

	Then continuing with: 
	Derek Russell
	User Story ID: 178035014, Sprint 6, 4 Points.
			The Original Form of Acceptance Criteria that is being sought after in BDD testing:
			* As a user of this site, that clicks any link to another user's published iCollection, I would like to see the collections title on the same page as the rendered environment.
			* As a user of this site, that clicks any link to another user's published iCollection, I would like to see the collections description on the same page as the rendered environment.
			* As a user of this site, that clicks any link to another user's published iCollection, I would like to see the collections keywords on the same page as the rendered environment.

Background:
	Given the following users exist
	  | UserName   | Email                 | FirstName | LastName | Password  |
	  | TaliaK     | knott@example.com     | Talia     | Knott    | Abcd987?6 |
	  | ZaydenC    | clark@example.com     | Zayden    | Clark    | Abcd987?6 |
	  | DavilaH    | hareem@example.com    | Hareem    | Davila   | Abcd987?6 |
	  | KrzysztofP | krzysztof@example.com | Krzysztof | Ponce    | Abcd987?6 |


Scenario Outline: Clicking on the Preview Buttons will redirect to environments page.
	Given I am on the Home Page
	When I Click the '<SelectedButton>' button
	Then I am redirected to the '<Page>' page
	Examples:
	| SelectedButton              | Page                 |
	| ocean_environment_preview   | ocean_environment    |
	| gallery_environment_preview | gallery_environment  |



Scenario Outline: Being a logged in User I can click the nav bar links
	Given I am a User
	When I am a logged in user on the HomePage
	  And I Click the '<SelectedButton>' Dropdown button
	Then I am redirected to the '<Page>' page
	Examples:
	| SelectedButton              | Page                 |
	| create_collection           | EnvironmentSelection |



Scenario Outline: Creating a new collection, User doesnt selecte an environment and hits continue but the page is refreshed.
	Given I am a User
	  And I am a logged in user on the HomePage
	  And I am on the 'EnvironmentSelection' page
	  When I Click the 'continue_on' button
	Then I am redirected to the 'EnvironmentSelection' page


Scenario Outline: Creating a new collection, User can select an environment and continue to photo selection
	Given I am a User
	  And I am a logged in user on the HomePage
	  And I am on the 'EnvironmentSelection' page
	  And I select the '<selectedCheckbox>' checkbox
	  When I Click the 'continue_on' button
	Then I am redirected to the '<Page>' page
	Examples:
	| selectedCheckbox    | Page            |
	| ocean_environment   | PhotoSelection  |
	| gallery_environment | PhotoSelection  |
	| null                | EnvironmentSelection  |
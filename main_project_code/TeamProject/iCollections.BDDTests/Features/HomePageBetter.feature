Feature: HomePageBetter
	This is going to test the route functions, that take in a collection id, and return a list of rending photos That will be passed through the DOM. Yes.

	Then continuing with: 
	Derek Russell
	User Story ID: 178035014, Sprint 6, 4 Points.
			The Original Form of Acceptance Criteria that is being sought after in BDD testing:
			* As a user of this site, that clicks any link to another user's published iCollection, I would like to see the collections title on the same page as the rendered environment.
			* As a user of this site, that clicks any link to another user's published iCollection, I would like to see the collections description on the same page as the rendered environment.
			* As a user of this site, that clicks any link to another user's published iCollection, I would like to see the collections keywords on the same page as the rendered environment.

Scenario Outline: Clicking on the Preview Button for the Ocean Environment on the HomePage will redirect to the sample ocean environment page.
	Given I am on the Home Page
	When I Click the '<SelectedButton>' button
	Then I am redirected to the '<Page>' page
	Examples:
	| SelectedButton              | Page                 |
	| ocean_environment_preview   | Ocean_environment    |
	| gallery_environment_preview | gallery_environment  |

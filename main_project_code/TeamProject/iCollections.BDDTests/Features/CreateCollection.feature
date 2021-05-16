Feature: CreateCollection
	Creating A Collection 
	Derek Russell
	User Story ID: 177878474, Sprint 5, 4 Points.
		The Original Acceptance Criteria:
			* As a user of this site with an account that is currently using the site, the user should not receive any cookie error messages from the web console available in their web browser. 
			* As a user of this site that is in the process of creating a collection, a tempdata cookie will appear, and then disposed of when the user continues using the site.
	Then continuing with: 
	Derek Russell
	User Story ID: 177895357, Sprint 6, 2 Points.
		The Original Form of Acceptance Criteria that is being sought after in BDD testing:
			* As a user of this site that is creating an icollection I would like to select exisiting Keywords(Tags) to attach to the collection when I publish it.
			* As a user of this site that is creating an icollection I would like to create new Keywords(Tags) to attach to the collection when I publish it.
	

Going through the process from logging in,  to publishing a newly made iCollection. 


Background:
	Given the following photos exist
	  | Id | Name           | Data               | UserId  | PhotoGuid  |
	  | 40 |  Photo Fish 1  | new byte[] {1,2,3} | 8       | new Guid() |
	  | 41 |  Photo Fish 2  | new byte[] {1,2,3} | 8       | new Guid() |
	  | 45 |  Photo Shoes 1 | new byte[] {1,2,3} | 3       | new Guid() |
	  | 48 |  Photo Dogs 1  | new byte[] {1,2,3} | 64      | new Guid() |
	  | 50 |  Photo Fish 3  | new byte[] {1,2,3} | 8       | new Guid() |
	  | 51 |  Photo Fish 4  | new byte[] {1,2,3} | 8       | new Guid() |
	  | 52 |  Photo Shoes 2 | new byte[] {1,2,3} | 3       | new Guid() |
	  | 53 |  Photo Dogs 2  | new byte[] {1,2,3} | 64      | new Guid() |
	  | 54 |  Photo Fish 5  | new byte[] {1,2,3} | 8       | new Guid() |
	  | 55 |  Photo Fish 6  | new byte[] {1,2,3} | 8       | new Guid() |
	  | 65 |  Photo Shoes 3 | new byte[] {1,2,3} | 3       | new Guid() |
	  | 66 |  Photo Dogs 3  | new byte[] {1,2,3} | 64      | new Guid() |
	  | 62 |  Photo Fish 7  | new byte[] {1,2,3} | 8       | new Guid() |
	  | 87 |  Photo Fish 8  | new byte[] {1,2,3} | 8       | new Guid() |
	  | 88 |  Photo Shoes 4 | new byte[] {1,2,3} | 3       | new Guid() |
	  | 89 |  Photo Dogs 4  | new byte[] {1,2,3} | 64      | new Guid() |
	  | 90 |  Photo Fish 9  | new byte[] {1,2,3} | 8       | new Guid() |
	  | 91 |  Photo Fish 10 | new byte[] {1,2,3} | 8       | new Guid() |
	  | 92 |  Photo Shoes 5 | new byte[] {1,2,3} | 3       | new Guid() |
	  | 93 |  Photo Dogs 5  | new byte[] {1,2,3} | 64      | new Guid() |
	
	And the following photos do not exist
	  | Id | Name              | Data               | UserId  | PhotoGuid  |
	  | 0  | First Photo Bad   | new byte[] {1,2,3} | 4       | new Guid() |
	  | -1 | Second Photo Bad  | new byte[] {1,2,3} | 8       | new Guid() |
	  | 16 | Third Photo Bad   | new byte[] {1,2,3} | 0       | new Guid() |
	  | 45 | Fourth Photo Bad  | 9001               | 64      | new Guid() |


@ignore
Scenario Outline: Clicking on The Create Collection button in the nav bar will direct to the environment selection page.
	Given I am a logged in user with first name '<FirstName>'
	When I Click Create new Collection
	Then I am redirected to the '<Page>' page
	  And I can see the two differnt environment options for me to select 
	Examples:
	| FirstName | Page                 |
	| Talia     | EnvironmentSelection |
	| Zayden    | EnvironmentSelection |
	| Hareem    | EnvironmentSelection |
	| Krzysztof | EnvironmentSelection |

@ignore
Scenario Outline: Non-user cannot click on The Create Collection button in the nav bar will direct to the environment selection page.
	Given I am a logged in user with first name '<FirstName>'
	When I Click Create new Collection
	Then I can see a 404 error message
	Examples:
	| FirstName |
	| Andre     |
	| Joanna    |
	| People    |

@ignore
Scenario Outline: On the environment selection page the user must click the checkbox of one of the environment options before clicking continue and be directed to the photo selection page. 
	Given I am a logged in user creating a collection on the environment selection page and select the '<Checkbox>' Environment
	When I Click the Continue button
	Then I am redirected to the '<Page>' page
	  And The http context contains an encrypted tempdata cookie
	Examples:
	| checkbox								   | Page            |
	| Ocean_Environment						   | PhotoSelection |
	| Gallery_Environment					   | PhotoSelection |
	| Null									   | EnvironmentSelection |
	| Ocean_Environment && Gallery_Environment | EnvironmentSelection |
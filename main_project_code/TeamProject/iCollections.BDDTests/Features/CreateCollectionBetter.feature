Feature: CreateCollectionBetter
	Creating A Collection 

	Derek Russell
	User Story ID: 177895357, Sprint 6, 2 Points.
		The Original Form of Acceptance Criteria that is being sought after in BDD testing:
			* As a user of this site that is creating an icollection I would like to select exisiting Keywords(Tags) to attach to the collection when I publish it.
			* As a user of this site that is creating an icollection I would like to create new Keywords(Tags) to attach to the collection when I publish it.
	

Going through the process from logging in,  to publishing a newly made iCollection. 

Background:
	Given the following users exist
	  | UserName   | Email                 | FirstName | LastName | Password  |
	  | TaliaK     | knott@example.com     | Talia     | Knott    | Abcd987?6 |
	  | ZaydenC    | clark@example.com     | Zayden    | Clark    | Abcd987?6 |
	  | DavilaH    | hareem@example.com    | Hareem    | Davila   | Abcd987?6 |
	  | KrzysztofP | krzysztof@example.com | Krzysztof | Ponce    | Abcd987?6 |


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


Scenario Outline: A Logged In User can view all the public seeded gallery iCollections
	Given I am a User
	  And I am a logged in user on the HomePage
	  When I view the gallery iCollection with '<CollectionId>' as the Id
	Then I can view the '<CollectionTitle>' title on the page
	And I can view the '<CollectionDescription>' description on the page
	Examples:
	| CollectionId | CollectionDescription                                        | CollectionTitle           |
	| 2            | The only Cards I got left                                    | Card Gallery              |
	| 3            | This is the dog toy gallery description section area that ha | Dog Toy Gallery           |
	| 5            | The fish in the gallery                                      | Fish Gallery              |
	| 7            | All the covid puzzels                                        | Puzzel Gallery            |
	| 9            | Some of my most used tools                                   | Tool Gallery              |
	| 11           | toys                                                         | FLUFFYS FRIENDS           |
	| 13           | blah                                                         | 8 puzzel gallery          |
	| 15           | last one                                                     | plyer screwdriver gallery |                                         

Scenario Outline: A Logged In User can view all the public seeded ocean iCollections
	Given I am a User
	  And I am a logged in user on the HomePage
	  When I view the ocean iCollection with '<CollectionId>' as the Id
	Then I can view the '<CollectionTitle>' title on the page
	And I can view the '<CollectionDescription>' description on the page
	Examples:
	| CollectionId | CollectionDescription                        | CollectionTitle                |
	| 1            | Royal Caribbean                              | Royal Ocean                    |
	| 4            | THIS IS THE DESCRIPTION ICEBERG              | Toys Overboard                 |
	| 6            | How did he get there                         | How did I get Here Ocean       |
	| 8            | The puzzels that have ocean in them or water | Water Puzzels in Ocean         |
	| 10           | oh noes the rust is coming                   | sea saws and clam clamps       |
	| 12           | baths                                        | Bath Time Buddies              |
	| 14           | tired                                        | puzzel ocean                   |
	| 16           | get it                                       | ocean with the allens and phil |
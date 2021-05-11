Feature: Collection Visibility
	Setting Visibility Flags on Collections 

	* If a user visits their private collections page, then their will be a setting where they will be able to set each of their individual iCollections to private. 
	* If a user sets their iCollection to private and visits their public collections page, then they will not be able to find or access their private collections on their public collections page.
	* If a visitor visits a user's public collections page, then that visitor will not be able to find or access that user's private collections. 

Going through the process from logging in, to publishing a newly made iCollection. 


Scenario Outline: I will find a setting on the my collection page that corresponds to collection visibility for each of my collections.
	Given I am a logged in user with collections
	When I navigate to the 'my collections' page
	Then I will find a setting that will allow to me to set the visibility of each of my collections


Scenario Outline: If I am a user who has collections that are set to private, I will not be able to find those collections on public pages.
	Given I am a logged in user with collections that have been configured to private
	When I navigate to any on the application that displays public collections
	Then I will not find my private collections displayed on those pages

Scenario Outline: If I am a visitor or user who has accessed a public page, I should not be able to find any user collections that have been set to private.
	Given I am a visitor or user of the site and have navigated to a page on the site that display public collections
	When I browse though any of the collections displayed on those pages
	Then I will not any collectoins a user has set to private.
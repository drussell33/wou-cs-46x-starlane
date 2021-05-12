Feature: ProfilePictures

Baltazar Ortiz #178115905
Small profile pictures show up when in the following page

Scenario Outline: Dashboard profile pictures show up
    Given I am logged in
    And others I follow/friends with have posted iCollections
    | Owner         | CollectionName      |
	| Hareem        | Superbad            |
	| Talia         | Puzzel Gallery Test |
	| Talia         | My Fish             |
    | Talia         | My Beer             |
    When I go to the 'Dashboard' page
    Then the event will show the profile picture of the user that posted 

# Scenario Outline: Follows shows profile pictures
#     Given I am on a registered user's profile page
#     When I go to the user's following page
#     Then the users the registered user follows profile pictures show


Feature: ProfilePictures

Baltazar Ortiz #178115905
Small profile pictures show up when in the following page

Background:
	Given the following users exist
	  | UserName   | Email                 | FirstName | LastName | Password  |
	  | TaliaK     | knott@example.com     | Talia     | Knott    | Abcd987?6 |
	  | ZaydenC    | clark@example.com     | Zayden    | Clark    | Abcd987?6 |
	  | DavilaH    | hareem@example.com    | Hareem    | Davila   | Abcd987?6 |
	  | KrzysztofP | krzysztof@example.com | Krzysztof | Ponce    | Abcd987?6 |

Scenario Outline: Dashboard profile pictures show up
    Given I am a user with first name '<FirstName>'
    And I am logged in
    And others I follow/friends with have posted iCollections
    | Owner         | CollectionName      |
	| Hareem        | Superbad            |
	| Talia         | Puzzel Gallery Test |
	| Talia         | My Fish             |
    | Talia         | My Beer             |
    When I go to the 'Dashboard' page
    Then the event will show the profile picture of the user that posted 
    Examples:
	| FirstName |
	| Talia     |
	| Zayden    |
	| Hareem    |
	| Krzysztof |

Scenario Outline: Follows shows profile pictures
    Given I am on '<User>' profile page
    When I go to '<User>' following page
    Then the followees profile pictures show
    Examples:
    | User          |
    | DavilaH       |
    | TaliaK        |
    | ZaydenC       |
    | KrzysztofP    |


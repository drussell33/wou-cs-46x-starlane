Feature: PublicICollections

Baltazar Ortiz #178115988
Being able to see registered users public iCollections.
Public iCollections can be viewed via the profile page.

Background:
    Given I am in the profile page

Scenario Outline: General public can view iCollections for a registered user
	Given the user has iCollections
    When I click on the <Link>
    Then I can see a registered user's public iCollections
    Examples:
    | Link         |
    | iCollections |
    | Show all     |

Scenario Outline: General public cannot see iCollections if registered user does not have any
    Given the user has no iCollections
    When I click on the <Link>
    Then no public iCollections show
    Examples:
    | Link         |
    | iCollections |
    | Show all     |

